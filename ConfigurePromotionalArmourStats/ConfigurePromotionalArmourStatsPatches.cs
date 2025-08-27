using Base.Core;
using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Statuses;
using Base.Levels;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurePromotionalArmourStats
{
    /// <summary>
    /// Harmony patches to apply stat bonuses when promotional armor is equipped
    /// This approach intercepts stat calculations and adds bonuses dynamically
    /// </summary>
    internal static class ConfigurePromotionalArmourStatsPatches
    {
        private static readonly DefRepository Repo = GameUtl.GameComponent<DefRepository>();
        
        // Cache the promotional armor items for performance
        private static TacticalItemDef _goldOdinBody;
        private static TacticalItemDef _goldBansheeHelmet;
        private static TacticalItemDef _prBansheeHelmet;
        private static TacticalItemDef _nwPhlegethonHelmet;
        
        private static bool _initialized = false;
        
        /// <summary>
        /// Initialize cached references to promotional armor items
        /// </summary>
        private static void InitializeArmorCache()
        {
            if (_initialized) return;
            
            try
            {
                _goldOdinBody = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Assault_Body_Gold_BodyPartDef"));
                _goldBansheeHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Sniper_Helmet_Gold_BodyPartDef"));
                _prBansheeHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("PX_Sniper_Helmet_PR_BodyPartDef"));
                _nwPhlegethonHelmet = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(p => p.name.Equals("NJ_Priest_Helmet_NW_BodyPartDef"));
                
                _initialized = true;
                Logger.LogInfo("[ConfigurePromotionalArmourStats] Armor cache initialized for stat bonus patches");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"[ConfigurePromotionalArmourStats] Error initializing armor cache: {e.Message}");
            }
        }
        
        /// <summary>
        /// Patch TacticalActor.GetStat to add promotional armor stat bonuses
        /// This intercepts all stat requests and adds bonuses when the armor is equipped
        /// </summary>
        [HarmonyPatch(typeof(TacticalActor), "GetStat")]
        internal static class TacticalActor_GetStat_Patch
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051")]
            private static void Postfix(ref float __result, TacticalActor __instance, StatModificationTarget stat)
            {
                try
                {
                    if (!_initialized)
                        InitializeArmorCache();
                    
                    var config = ConfigurePromotionalArmourStatsMain.Config;
                    if (config == null) return;
                    
                    // Get equipped items
                    var equippedItems = __instance.Equipment?.GetAllItems()?.ToList() ?? new List<TacticalItem>();
                    
                    float bonus = 0f;
                    
                    // Check for Strength bonuses (Endurance in Phoenix Point)
                    if (stat == StatModificationTarget.Endurance)
                    {
                        // Gold Odin Body +Strength bonus
                        if (config.GoldOdinBodyStrengthBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _goldOdinBody))
                        {
                            bonus += config.GoldOdinBodyStrengthBonus;
                        }
                    }
                    
                    // Check for Will Points bonuses
                    else if (stat == StatModificationTarget.Willpower)
                    {
                        // NW Phlegethon Helmet +Will Points bonus
                        if (config.NWHelmetWillPointsBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _nwPhlegethonHelmet))
                        {
                            bonus += config.NWHelmetWillPointsBonus;
                        }
                        
                        // Gold Banshee Helmet +Will Points bonus
                        if (config.GoldBansheeHelmetWillPointsBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _goldBansheeHelmet))
                        {
                            bonus += config.GoldBansheeHelmetWillPointsBonus;
                        }
                        
                        // PR Banshee Helmet +Will Points bonus
                        if (config.PRBansheeHelmetWillPointsBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _prBansheeHelmet))
                        {
                            bonus += config.PRBansheeHelmetWillPointsBonus;
                        }
                    }
                    
                    // Apply the bonus to the result
                    if (bonus != 0f)
                    {
                        __result += bonus;
                        // Optional: Log for debugging (remove in production)
                        // Logger.LogInfo($"[ConfigurePromotionalArmourStats] Applied +{bonus} {stat} bonus to {__instance.DisplayName}");
                    }
                }
                catch (System.Exception e)
                {
                    Logger.LogError($"[ConfigurePromotionalArmourStats] Error in GetStat patch: {e.Message}");
                }
            }
        }
        
        /// <summary>
        /// Patch TacticalActor.GetStatModificationValue to add promotional armor stat bonuses
        /// This handles the max stat values as well
        /// </summary>
        [HarmonyPatch(typeof(TacticalActor), "GetStatModificationValue")]
        internal static class TacticalActor_GetStatModificationValue_Patch
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051")]
            private static void Postfix(ref float __result, TacticalActor __instance, StatModificationTarget stat, StatModificationType modificationType)
            {
                try
                {
                    // Only apply to Add and AddMax modifications
                    if (modificationType != StatModificationType.Add && modificationType != StatModificationType.AddMax)
                        return;
                    
                    if (!_initialized)
                        InitializeArmorCache();
                    
                    var config = ConfigurePromotionalArmourStatsMain.Config;
                    if (config == null) return;
                    
                    // Get equipped items
                    var equippedItems = __instance.Equipment?.GetAllItems()?.ToList() ?? new List<TacticalItem>();
                    
                    float bonus = 0f;
                    
                    // Check for Strength bonuses (Endurance in Phoenix Point)
                    if (stat == StatModificationTarget.Endurance)
                    {
                        // Gold Odin Body +Strength bonus
                        if (config.GoldOdinBodyStrengthBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _goldOdinBody))
                        {
                            bonus += config.GoldOdinBodyStrengthBonus;
                        }
                    }
                    
                    // Check for Will Points bonuses
                    else if (stat == StatModificationTarget.Willpower)
                    {
                        // NW Phlegethon Helmet +Will Points bonus
                        if (config.NWHelmetWillPointsBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _nwPhlegethonHelmet))
                        {
                            bonus += config.NWHelmetWillPointsBonus;
                        }
                        
                        // Gold Banshee Helmet +Will Points bonus
                        if (config.GoldBansheeHelmetWillPointsBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _goldBansheeHelmet))
                        {
                            bonus += config.GoldBansheeHelmetWillPointsBonus;
                        }
                        
                        // PR Banshee Helmet +Will Points bonus
                        if (config.PRBansheeHelmetWillPointsBonus != 0 && 
                            equippedItems.Any(item => item?.TacticalItemDef == _prBansheeHelmet))
                        {
                            bonus += config.PRBansheeHelmetWillPointsBonus;
                        }
                    }
                    
                    // Apply the bonus to the result
                    if (bonus != 0f)
                    {
                        __result += bonus;
                    }
                }
                catch (System.Exception e)
                {
                    Logger.LogError($"[ConfigurePromotionalArmourStats] Error in GetStatModificationValue patch: {e.Message}");
                }
            }
        }
    }
}