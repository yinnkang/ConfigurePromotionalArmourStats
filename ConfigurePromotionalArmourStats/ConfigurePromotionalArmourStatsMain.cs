using Base.Core;
using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects;
using Base.Entities.Statuses;
using Base.Levels;
using Base.UI;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Geoscape.Entities;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using System.Collections.Generic;
using PhoenixPoint.Tactical.Entities.Animations;
using PhoenixPoint.Tactical.Entities.Effects.ApplicationConditions;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using System.Linq;
using UnityEngine;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;

namespace ConfigurePromotionalArmourStats
{
    /// <summary>
    /// This is the main mod class. Only one can exist per assembly.
    /// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
    /// </summary>
    public class ConfigurePromotionalArmourStats : ModMain
    {
        /// <summary>
        /// defines the modifiable values for any given armor.
        /// </summary>
        private struct ArmorValues
        {
            public float Armor;
            public float Speed;
            public float Perception;
            public float Stealth;
            public float Accuracy;
            public int Weight;
            public int FumblePerc;

            public ArmorValues(float armor, float speed, float perception,
                float stealth, float accuracy, int weight, int fumblePerc)
            {
                Armor = armor;
                Speed = speed;
                Perception = perception;
                Stealth = stealth;
                Accuracy = accuracy;
                Weight = weight;
                FumblePerc = fumblePerc;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new ConfigurePromotionalArmourStatsConfig Config => (ConfigurePromotionalArmourStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef GoldOdinHelmetItem, GoldOdinBodyItem, GoldOdinLegItem,
            GoldGolemHelmetItem, GoldGolemBodyItem, GoldGolemLegItem,
            GoldBansheeHelmetItem, GoldBansheeBodyItem, GoldBansheeLegItem,
            PRBansheeHelmetItem, PRBansheeBodyItem, PRBansheeLegItem,
            NWPhlegethonHelmetItem, NWPhlegethonBodyItem, NWPhlegethonLegItem,
            VikingHelmetItem, VikingBodyItem, VikingLegItem, VikingRightLegItem, VikingMainLegsItem;
        private ArmorValues DefaultGoldOdinHelmetValues, DefaultGoldOdinBodyValues, DefaultGoldOdinLegValues,
            DefaultGoldGolemHelmetValues, DefaultGoldGolemBodyValues, DefaultGoldGolemLegValues,
            DefaultGoldBansheeHelmetValues, DefaultGoldBansheeBodyValues, DefaultGoldBansheeLegValues,
            DefaultPRBansheeHelmetValues, DefaultPRBansheeBodyValues, DefaultPRBansheeLegValues,
            DefaultNWPhlegethonHelmetValues, DefaultNWPhlegethonBodyValues, DefaultNWPhlegethonLegValues,
            DefaultVikingHelmetValues, DefaultVikingBodyValues, DefaultVikingLegValues, DefaultVikingRightLegValues, DefaultVikingMainLegsValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            
            Logger.LogInfo("[ConfigurePromotionalArmourStats] Starting OnModEnabled...");
            
            GoldOdinHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Helmet_Gold_BodyPartDef"));
            if (GoldOdinHelmetItem == null) Logger.LogWarning("Could not find PX_Assault_Helmet_Gold_BodyPartDef");
            
            GoldOdinBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Torso_Gold_BodyPartDef"));
            if (GoldOdinBodyItem == null) Logger.LogWarning("Could not find PX_Assault_Torso_Gold_BodyPartDef");
            
            GoldOdinLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Legs_Gold_ItemDef"));
            if (GoldOdinLegItem == null) Logger.LogWarning("Could not find PX_Assault_Legs_Gold_ItemDef");
            
            GoldGolemHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Helmet_Gold_BodyPartDef"));
            if (GoldGolemHelmetItem == null) Logger.LogWarning("Could not find PX_Heavy_Helmet_Gold_BodyPartDef");
            
            GoldGolemBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Torso_Gold_BodyPartDef"));
            if (GoldGolemBodyItem == null) Logger.LogWarning("Could not find PX_Heavy_Torso_Gold_BodyPartDef");
            
            GoldGolemLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Legs_Gold_ItemDef"));
            if (GoldGolemLegItem == null) Logger.LogWarning("Could not find PX_Heavy_Legs_Gold_ItemDef");
            
            GoldBansheeHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Helmet_Gold_BodyPartDef"));
            if (GoldBansheeHelmetItem == null) Logger.LogWarning("Could not find PX_Sniper_Helmet_Gold_BodyPartDef");
            
            GoldBansheeBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Torso_Gold_BodyPartDef"));
            if (GoldBansheeBodyItem == null) Logger.LogWarning("Could not find PX_Sniper_Torso_Gold_BodyPartDef");
            
            GoldBansheeLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Legs_Gold_ItemDef"));
            if (GoldBansheeLegItem == null) Logger.LogWarning("Could not find PX_Sniper_Legs_Gold_ItemDef");
            
            PRBansheeHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Helmet_RisingSun_BodyPartDef"));
            if (PRBansheeHelmetItem == null) Logger.LogWarning("Could not find PX_Sniper_Helmet_RisingSun_BodyPartDef");
            
            PRBansheeBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Torso_RisingSun_BodyPartDef"));
            if (PRBansheeBodyItem == null) Logger.LogWarning("Could not find PX_Sniper_Torso_RisingSun_BodyPartDef");
            
            PRBansheeLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Legs_RisingSun_ItemDef"));
            if (PRBansheeLegItem == null) Logger.LogWarning("Could not find PX_Sniper_Legs_RisingSun_ItemDef");
            
            NWPhlegethonHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Helmet_WhiteNeon_BodyPartDef"));
            if (NWPhlegethonHelmetItem == null) Logger.LogWarning("Could not find SY_Assault_Helmet_WhiteNeon_BodyPartDef");
            
            NWPhlegethonBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Torso_WhiteNeon_BodyPartDef"));
            if (NWPhlegethonBodyItem == null) Logger.LogWarning("Could not find SY_Assault_Torso_WhiteNeon_BodyPartDef");
            
            NWPhlegethonLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Legs_WhiteNeon_ItemDef"));
            if (NWPhlegethonLegItem == null) Logger.LogWarning("Could not find SY_Assault_Legs_WhiteNeon_ItemDef");
            
            VikingHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Helmet_Viking_BodyPartDef"));
            if (VikingHelmetItem == null) Logger.LogWarning("Could not find AN_Berserker_Helmet_Viking_BodyPartDef");
            
            VikingBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Torso_Viking_BodyPartDef"));
            if (VikingBodyItem == null) Logger.LogWarning("Could not find AN_Berserker_Torso_Viking_BodyPartDef");
            
            VikingLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_LeftLeg_Viking_BodyPartDef"));
            if (VikingLegItem == null) Logger.LogWarning("Could not find AN_Berserker_LeftLeg_Viking_BodyPartDef");
            
            VikingRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_RightLeg_Viking_BodyPartDef"));
            if (VikingRightLegItem == null) Logger.LogWarning("Could not find AN_Berserker_RightLeg_Viking_BodyPartDef");
            
            VikingMainLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Legs_Viking_ItemDef"));
            if (VikingMainLegsItem == null) Logger.LogWarning("Could not find AN_Berserker_Legs_Viking_ItemDef");


            DefaultGoldOdinHelmetValues = getArmorValuesFromArmorDef(GoldOdinHelmetItem);
            DefaultGoldOdinBodyValues = getArmorValuesFromArmorDef(GoldOdinBodyItem);
            DefaultGoldOdinLegValues = getArmorValuesFromArmorDef(GoldOdinLegItem);
            DefaultGoldGolemHelmetValues = getArmorValuesFromArmorDef(GoldGolemHelmetItem);
            DefaultGoldGolemBodyValues = getArmorValuesFromArmorDef(GoldGolemBodyItem);
            DefaultGoldGolemLegValues = getArmorValuesFromArmorDef(GoldGolemLegItem);
            DefaultGoldBansheeHelmetValues = getArmorValuesFromArmorDef(GoldBansheeHelmetItem);
            DefaultGoldBansheeBodyValues = getArmorValuesFromArmorDef(GoldBansheeBodyItem);
            DefaultGoldBansheeLegValues = getArmorValuesFromArmorDef(GoldBansheeLegItem);
            DefaultPRBansheeHelmetValues = getArmorValuesFromArmorDef(PRBansheeHelmetItem);
            DefaultPRBansheeBodyValues = getArmorValuesFromArmorDef(PRBansheeBodyItem);
            DefaultPRBansheeLegValues = getArmorValuesFromArmorDef(PRBansheeLegItem);
            DefaultNWPhlegethonHelmetValues = getArmorValuesFromArmorDef(NWPhlegethonHelmetItem);
            DefaultNWPhlegethonBodyValues = getArmorValuesFromArmorDef(NWPhlegethonBodyItem);
            DefaultNWPhlegethonLegValues = getArmorValuesFromArmorDef(NWPhlegethonLegItem);
            DefaultVikingHelmetValues = getArmorValuesFromArmorDef(VikingHelmetItem);
            DefaultVikingBodyValues = getArmorValuesFromArmorDef(VikingBodyItem);
            DefaultVikingLegValues = getArmorValuesFromArmorDef(VikingLegItem);
            DefaultVikingRightLegValues = getArmorValuesFromArmorDef(VikingRightLegItem);
            DefaultVikingMainLegsValues = getArmorValuesFromArmorDef(VikingMainLegsItem);

            OnConfigChanged();
            Logger.LogInfo("[ConfigurePromotionalArmourStats] OnModEnabled completed.");
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultGoldOdinHelmetValues, GoldOdinHelmetItem);
            setDefsFromArmorValues(DefaultGoldOdinBodyValues, GoldOdinBodyItem);
            setDefsFromArmorValues(DefaultGoldOdinLegValues, GoldOdinLegItem);
            setDefsFromArmorValues(DefaultGoldGolemHelmetValues, GoldGolemHelmetItem);
            setDefsFromArmorValues(DefaultGoldGolemBodyValues, GoldGolemBodyItem);
            setDefsFromArmorValues(DefaultGoldGolemLegValues, GoldGolemLegItem);
            setDefsFromArmorValues(DefaultGoldBansheeHelmetValues, GoldBansheeHelmetItem);
            setDefsFromArmorValues(DefaultGoldBansheeBodyValues, GoldBansheeBodyItem);
            setDefsFromArmorValues(DefaultGoldBansheeLegValues, GoldBansheeLegItem);
            setDefsFromArmorValues(DefaultPRBansheeHelmetValues, PRBansheeHelmetItem);
            setDefsFromArmorValues(DefaultPRBansheeBodyValues, PRBansheeBodyItem);
            setDefsFromArmorValues(DefaultPRBansheeLegValues, PRBansheeLegItem);
            setDefsFromArmorValues(DefaultNWPhlegethonHelmetValues, NWPhlegethonHelmetItem);
            setDefsFromArmorValues(DefaultNWPhlegethonBodyValues, NWPhlegethonBodyItem);
            setDefsFromArmorValues(DefaultNWPhlegethonLegValues, NWPhlegethonLegItem);
            setDefsFromArmorValues(DefaultVikingHelmetValues, VikingHelmetItem);
            setDefsFromArmorValues(DefaultVikingBodyValues, VikingBodyItem);
            setDefsFromArmorValues(DefaultVikingLegValues, VikingLegItem);
            setDefsFromArmorValues(DefaultVikingRightLegValues, VikingRightLegItem);
            setDefsFromArmorValues(DefaultVikingMainLegsValues, VikingMainLegsItem);

            // Remove all added abilities when mod is disabled
            RemoveAllAddedAbilities();
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            Logger.LogInfo("[ConfigurePromotionalArmourStats] OnConfigChanged called - applying new values...");
            
            ArmorValues GoldOdinHelmetValues = new ArmorValues(
                Config.GoldOdinHelmetArmor,
                Config.GoldOdinHelmetSpeed,
                Config.GoldOdinHelmetPerception,
                Config.GoldOdinHelmetStealth,
                Config.GoldOdinHelmetAccuracy,
                Config.GoldOdinHelmetWeight,
                0
            );
            ArmorValues GoldOdinBodyValues = new ArmorValues(
                Config.GoldOdinBodyArmor,
                Config.GoldOdinBodySpeed,
                Config.GoldOdinBodyPerception,
                Config.GoldOdinBodyStealth,
                Config.GoldOdinBodyAccuracy,
                Config.GoldOdinBodyWeight,
                0
            );
            ArmorValues GoldOdinLegValues = new ArmorValues(
                Config.GoldOdinLegArmor,
                Config.GoldOdinLegSpeed,
                Config.GoldOdinLegPerception,
                Config.GoldOdinLegStealth,
                Config.GoldOdinLegAccuracy,
                Config.GoldOdinLegWeight,
                0
            );
            ArmorValues GoldGolemHelmetValues = new ArmorValues(
                Config.GoldGolemHelmetArmor,
                Config.GoldGolemHelmetSpeed,
                Config.GoldGolemHelmetPerception,
                Config.GoldGolemHelmetStealth,
                Config.GoldGolemHelmetAccuracy,
                Config.GoldGolemHelmetWeight,
                0
            );
            ArmorValues GoldGolemBodyValues = new ArmorValues(
                Config.GoldGolemBodyArmor,
                Config.GoldGolemBodySpeed,
                Config.GoldGolemBodyPerception,
                Config.GoldGolemBodyStealth,
                Config.GoldGolemBodyAccuracy,
                Config.GoldGolemBodyWeight,
                Config.GoldGolemBodyFumblePerc
            );
            ArmorValues GoldGolemLegValues = new ArmorValues(
                Config.GoldGolemLegArmor,
                Config.GoldGolemLegSpeed,
                Config.GoldGolemLegPerception,
                Config.GoldGolemLegStealth,
                Config.GoldGolemLegAccuracy,
                Config.GoldGolemLegWeight,
                0
            );
            ArmorValues GoldBansheeHelmetValues = new ArmorValues(
                Config.GoldBansheeHelmetArmor,
                Config.GoldBansheeHelmetSpeed,
                Config.GoldBansheeHelmetPerception,
                Config.GoldBansheeHelmetStealth,
                Config.GoldBansheeHelmetAccuracy,
                Config.GoldBansheeHelmetWeight,
                0
            );
            ArmorValues GoldBansheeBodyValues = new ArmorValues(
                Config.GoldBansheeBodyArmor,
                Config.GoldBansheeBodySpeed,
                Config.GoldBansheeBodyPerception,
                Config.GoldBansheeBodyStealth,
                Config.GoldBansheeBodyAccuracy,
                Config.GoldBansheeBodyWeight,
                0
            );
            ArmorValues GoldBansheeLegValues = new ArmorValues(
                Config.GoldBansheeLegArmor,
                Config.GoldBansheeLegSpeed,
                Config.GoldBansheeLegPerception,
                Config.GoldBansheeLegStealth,
                Config.GoldBansheeLegAccuracy,
                Config.GoldBansheeLegWeight,
                0
            );
            ArmorValues PRBansheeHelmetValues = new ArmorValues(
                Config.PRBansheeHelmetArmor,
                Config.PRBansheeHelmetSpeed,
                Config.PRBansheeHelmetPerception,
                Config.PRBansheeHelmetStealth,
                Config.PRBansheeHelmetAccuracy,
                Config.PRBansheeHelmetWeight,
                0
            );
            ArmorValues PRBansheeBodyValues = new ArmorValues(
                Config.PRBansheeBodyArmor,
                Config.PRBansheeBodySpeed,
                Config.PRBansheeBodyPerception,
                Config.PRBansheeBodyStealth,
                Config.PRBansheeBodyAccuracy,
                Config.PRBansheeBodyWeight,
                0
            );
            ArmorValues PRBansheeLegValues = new ArmorValues(
                Config.PRBansheeLegArmor,
                Config.PRBansheeLegSpeed,
                Config.PRBansheeLegPerception,
                Config.PRBansheeLegStealth,
                Config.PRBansheeLegAccuracy,
                Config.PRBansheeLegWeight,
                0
            );
            ArmorValues NWPhlegethonHelmetValues = new ArmorValues(
                Config.NWPhlegethonHelmetArmor,
                Config.NWPhlegethonHelmetSpeed,
                Config.NWPhlegethonHelmetPerception,
                Config.NWPhlegethonHelmetStealth,
                Config.NWPhlegethonHelmetAccuracy,
                Config.NWPhlegethonHelmetWeight,
                0
            );
            ArmorValues NWPhlegethonBodyValues = new ArmorValues(
                Config.NWPhlegethonBodyArmor,
                Config.NWPhlegethonBodySpeed,
                Config.NWPhlegethonBodyPerception,
                Config.NWPhlegethonBodyStealth,
                Config.NWPhlegethonBodyAccuracy,
                Config.NWPhlegethonBodyWeight,
                0
            );
            ArmorValues NWPhlegethonLegValues = new ArmorValues(
                Config.NWPhlegethonLegArmor,
                Config.NWPhlegethonLegSpeed,
                Config.NWPhlegethonLegPerception,
                Config.NWPhlegethonLegStealth,
                Config.NWPhlegethonLegAccuracy,
                Config.NWPhlegethonLegWeight,
                0
            );
            ArmorValues VikingHelmetValues = new ArmorValues(
                Config.VikingHelmetArmor,
                Config.VikingHelmetSpeed,
                Config.VikingHelmetPerception,
                Config.VikingHelmetStealth,
                Config.VikingHelmetAccuracy,
                Config.VikingHelmetWeight,
                0
            );
            ArmorValues VikingBodyValues = new ArmorValues(
                Config.VikingBodyArmor,
                Config.VikingBodySpeed,
                Config.VikingBodyPerception,
                Config.VikingBodyStealth,
                Config.VikingBodyAccuracy,
                Config.VikingBodyWeight,
                0
            );
            ArmorValues VikingLegValues = new ArmorValues(
                Config.VikingLegArmor,
                Config.VikingLegSpeed,
                Config.VikingLegPerception,
                Config.VikingLegStealth,
                Config.VikingLegAccuracy,
                Config.VikingLegWeight,
                0
            );
            
            setDefsFromArmorValues(GoldOdinHelmetValues, GoldOdinHelmetItem);
            setDefsFromArmorValues(GoldOdinBodyValues, GoldOdinBodyItem);
            setDefsFromArmorValues(GoldOdinLegValues, GoldOdinLegItem);
            setDefsFromArmorValues(GoldGolemHelmetValues, GoldGolemHelmetItem);
            setDefsFromArmorValues(GoldGolemBodyValues, GoldGolemBodyItem);
            setDefsFromArmorValues(GoldGolemLegValues, GoldGolemLegItem);
            setDefsFromArmorValues(GoldBansheeHelmetValues, GoldBansheeHelmetItem);
            setDefsFromArmorValues(GoldBansheeBodyValues, GoldBansheeBodyItem);
            setDefsFromArmorValues(GoldBansheeLegValues, GoldBansheeLegItem);
            setDefsFromArmorValues(PRBansheeHelmetValues, PRBansheeHelmetItem);
            setDefsFromArmorValues(PRBansheeBodyValues, PRBansheeBodyItem);
            setDefsFromArmorValues(PRBansheeLegValues, PRBansheeLegItem);
            setDefsFromArmorValues(NWPhlegethonHelmetValues, NWPhlegethonHelmetItem);
            setDefsFromArmorValues(NWPhlegethonBodyValues, NWPhlegethonBodyItem);
            setDefsFromArmorValues(NWPhlegethonLegValues, NWPhlegethonLegItem);
            setDefsFromArmorValues(VikingHelmetValues, VikingHelmetItem);
            setDefsFromArmorValues(VikingBodyValues, VikingBodyItem);
            setDefsFromArmorValues(VikingLegValues, VikingLegItem);
            setDefsFromArmorValues(VikingLegValues, VikingRightLegItem); // Use same values for right leg
            setDefsFromArmorValues(VikingLegValues, VikingMainLegsItem); // Use same values for main legs item

            // Apply additional abilities
            ApplyArmorAbilities();
            Logger.LogInfo("[ConfigurePromotionalArmourStats] OnConfigChanged completed.");
        }

        /* WEAPON DATA FUNCTIONS */

        private ArmorValues getArmorValuesFromArmorDef(TacticalItemDef armorDef)
        {
            if (armorDef == null) return new ArmorValues(0, 0, 0, 0, 0, 0, 0);
            
            int FumblePerc = 0;
            for (int i = 0; i < armorDef.Abilities.Length; i++)
            {
                if (armorDef.Abilities[i].GetType() == typeof(JetJumpAbilityDef))
                {
                    FumblePerc = ((JetJumpAbilityDef)(armorDef.Abilities[i])).FumblePerc;
                    break;
                }
            }
            return new ArmorValues(
                armorDef.Armor,
                armorDef.BodyPartAspectDef.Speed,
                armorDef.BodyPartAspectDef.Perception,
                armorDef.BodyPartAspectDef.Stealth * 100f,
                armorDef.BodyPartAspectDef.Accuracy * 100f,
                armorDef.Weight,
                FumblePerc
            );
        }
        
        private void setDefsFromArmorValues(ArmorValues armorValues, TacticalItemDef armorDef)
        {
            if (armorDef == null)
            {
                Logger.LogWarning("Cannot set armor values - armor definition is null");
                return;
            }
            
            try
            {
                armorDef.Armor = armorValues.Armor;
                armorDef.BodyPartAspectDef.Speed = armorValues.Speed;
                armorDef.BodyPartAspectDef.Perception = armorValues.Perception;
                armorDef.BodyPartAspectDef.Stealth = armorValues.Stealth / 100f;
                armorDef.BodyPartAspectDef.Accuracy = armorValues.Accuracy / 100f;
                armorDef.Weight = armorValues.Weight;
                
                for (int i = 0; i < armorDef.Abilities.Length; i++)
                {
                    if (armorDef.Abilities[i].GetType() == typeof(JetJumpAbilityDef))
                    {
                        ((JetJumpAbilityDef)(armorDef.Abilities[i])).FumblePerc = armorValues.FumblePerc;
                        break;
                    }
                }
                
                Logger.LogInfo($"Applied values to {armorDef.name}: Armor={armorValues.Armor}, Speed={armorValues.Speed}, Weight={armorValues.Weight}");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"Error setting armor values for {armorDef.name}: {e.Message}");
            }
        }

        /// <summary>
        /// Apply additional abilities to armor pieces based on config - matches SuperCheatsModPlus approach
        /// </summary>
        private void ApplyArmorAbilities()
        {
            DefRepository repo = GameUtl.GameComponent<DefRepository>();
            
            Logger.LogInfo("[ConfigurePromotionalArmourStats] Starting ApplyArmorAbilities...");
            
            // Remove existing abilities first to ensure clean state
            RemoveAllAddedAbilities();
            
            // Create configurable abilities that worked in previous version
            CreateArmorBuffAbility();
            CreateGunslingerAbility();
            CreateStatBonusAbilities(repo);
            
            // Assign abilities to armor pieces using existing game abilities when possible
            AssignAbilitiesToArmor(repo);
        }

        /// <summary>
        /// Create and assign abilities to armor pieces based on config - RESTORED WORKING VERSION with new additions
        /// </summary>
        private void AssignAbilitiesToArmor(DefRepository repo)
        {
            Logger.LogInfo("[ConfigurePromotionalArmourStats] Starting ability assignment...");
            
            // NEON WHITE HELMET: Instill Frenzy, Virus Resistant, +5 Will Points
            var nwHelmetAbilities = new List<AbilityDef>();
            if (Config.NWHelmetInstillFrenzy)
            {
                var frenzyAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Priest_InstilFrenzy_AbilityDef"));
                if (frenzyAbility != null)
                {
                    nwHelmetAbilities.Add(frenzyAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Instill Frenzy ability to NW Helmet");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find Priest_InstilFrenzy_AbilityDef");
                }
            }
            if (Config.NWHelmetVirusResistant)
            {
                var virusResistantAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("VirusImmunity_DamageMultiplierAbilityDef"));
                if (virusResistantAbility != null)
                {
                    nwHelmetAbilities.Add(virusResistantAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Virus Resistant ability to NW Helmet");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find VirusImmunity_DamageMultiplierAbilityDef");
                }
            }
            if (NWPhlegethonHelmetItem != null)
                NWPhlegethonHelmetItem.Abilities = nwHelmetAbilities.ToArray();
            
            // NEON WHITE BODY ARMOR: Radiant Hope
            if (NWPhlegethonBodyItem != null)
            {
                if (Config.NWBodyRadiantHope)
                {
                    var radiantHopeAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("RadiantHope_AbilityDef"));
                    if (radiantHopeAbility != null)
                    {
                        NWPhlegethonBodyItem.Abilities = new AbilityDef[] { radiantHopeAbility };
                    }
                    else
                    {
                        NWPhlegethonBodyItem.Abilities = new AbilityDef[0];
                    }
                }
                else
                {
                    NWPhlegethonBodyItem.Abilities = new AbilityDef[0];
                }
            }
            
            // NEON WHITE LEGS: Immune to Goo
            if (NWPhlegethonLegItem != null)
            {
                if (Config.NWLegsGooImmunity)
                {
                    var gooImmunityAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("GooImmunity_AbilityDef"));
                    if (gooImmunityAbility != null)
                    {
                        NWPhlegethonLegItem.Abilities = new AbilityDef[] { gooImmunityAbility };
                    }
                    else
                    {
                        NWPhlegethonLegItem.Abilities = new AbilityDef[0];
                    }
                }
                else
                {
                    NWPhlegethonLegItem.Abilities = new AbilityDef[0];
                }
            }
            
            // GOLD ODIN HELMET: Mind Control Immunity, Poison Resistant
            var goldOdinHelmetAbilities = new List<AbilityDef>();
            if (Config.GoldOdinHelmetMindControlImmunity)
            {
                var mindControlImmunityAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("PsychicImmunity_DamageMultiplierAbilityDef"));
                if (mindControlImmunityAbility != null)
                {
                    goldOdinHelmetAbilities.Add(mindControlImmunityAbility);
                }
            }
            if (Config.GoldOdinHelmetPoisonResistant)
            {
                var poisonResistantAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("PoisonImmunity_DamageMultiplierAbilityDef"));
                if (poisonResistantAbility != null)
                {
                    goldOdinHelmetAbilities.Add(poisonResistantAbility);
                }
            }
            if (GoldOdinHelmetItem != null)
                GoldOdinHelmetItem.Abilities = goldOdinHelmetAbilities.ToArray();
            
            // GOLD ODIN BODY ARMOR: Combat Matrix, Fire Resistant, Melee Weapon Proficiency, +5 Strength
            var goldOdinBodyAbilities = new List<AbilityDef>();
            if (Config.GoldOdinBodyCombatMatrix)
            {
                var combatMatrixAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("MartialArtist_AbilityDef"));
                if (combatMatrixAbility != null)
                {
                    goldOdinBodyAbilities.Add(combatMatrixAbility);
                }
            }
            if (Config.GoldOdinBodyFireResistant)
            {
                var fireResistantAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("FireStatusImmunity_AbilityDef"));
                if (fireResistantAbility != null)
                {
                    goldOdinBodyAbilities.Add(fireResistantAbility);
                }
            }
            // NEW: Add Melee Weapon Proficiency from Vengeance Torso Augmentation
            if (Config.GoldOdinBodyMeleeWeaponProficiency)
            {
                var meleeWeaponProficiencyAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("MeleeWeapons_AbilityDef"));
                if (meleeWeaponProficiencyAbility != null)
                {
                    goldOdinBodyAbilities.Add(meleeWeaponProficiencyAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Melee Weapon Proficiency to Gold Odin Body");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find MeleeWeapons_AbilityDef");
                }
            }
            if (GoldOdinBodyItem != null)
                GoldOdinBodyItem.Abilities = goldOdinBodyAbilities.ToArray();
            
            // GOLD ODIN LEGS: Can jump up one elevation, Shadowstep
            var goldOdinLegsAbilities = new List<AbilityDef>();
            if (Config.GoldOdinLegsJumpElevation)
            {
                var jumpAbility = repo.GetAllDefs<AddNavAreasAbilityDef>().FirstOrDefault(a => a.name.Equals("Humanoid_HighJump_AbilityDef"));
                if (jumpAbility != null)
                {
                    goldOdinLegsAbilities.Add(jumpAbility);
                }
            }
            if (Config.GoldOdinLegsShadowstep)
            {
                var shadowstepAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("ShadowStep_AbilityDef"));
                if (shadowstepAbility != null)
                {
                    goldOdinLegsAbilities.Add(shadowstepAbility);
                }
            }
            if (GoldOdinLegItem != null)
                GoldOdinLegItem.Abilities = goldOdinLegsAbilities.ToArray();
            
            // GOLD BANSHEE HELMET: Gunslinger, +4 Will Points
            if (GoldBansheeHelmetItem != null)
            {
                if (Config.GoldBansheeHelmetGunslinger)
                {
                    var gunslingerAbility = repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Custom_Gunslinger_AbilityDef"));
                    if (gunslingerAbility != null)
                    {
                        GoldBansheeHelmetItem.Abilities = new AbilityDef[] { gunslingerAbility };
                    }
                    else
                    {
                        GoldBansheeHelmetItem.Abilities = new AbilityDef[0];
                    }
                }
                else
                {
                    GoldBansheeHelmetItem.Abilities = new AbilityDef[0];
                }
            }
            
            // PR BANSHEE HELMET: Night Vision, Silent Echo, +4 Will Points
            var prBansheeHelmetAbilities = new List<AbilityDef>();
            if (Config.PRBansheeHelmetNightVision)
            {
                var nightVisionAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("EnhancedVision_AbilityDef"));
                if (nightVisionAbility != null)
                {
                    prBansheeHelmetAbilities.Add(nightVisionAbility);
                }
            }
            if (Config.PRBansheeHelmetSilentEcho)
            {
                var silentEchoAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("SilentEcho_AbilityDef"));
                if (silentEchoAbility != null)
                {
                    prBansheeHelmetAbilities.Add(silentEchoAbility);
                }
            }
            if (PRBansheeHelmetItem != null)
                PRBansheeHelmetItem.Abilities = prBansheeHelmetAbilities.ToArray();
            
            // GOLD BANSHEE LEGS: Rocket Leap, Landing Shock Absorption
            var goldBansheeLegsAbilities = new List<AbilityDef>();
            if (Config.GoldBansheeLegsRocketLeap)
            {
                var rocketLeapAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Exo_Leap_AbilityDef"));
                if (rocketLeapAbility != null)
                {
                    goldBansheeLegsAbilities.Add(rocketLeapAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Rocket Leap to Gold Banshee Legs");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find Exo_Leap_AbilityDef for Rocket Leap");
                }
            }
            if (Config.GoldBansheeLegsShockAbsorption)
            {
                var shockAbsorptionAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("SafeLanding_AbilityDef"));
                if (shockAbsorptionAbility != null)
                {
                    goldBansheeLegsAbilities.Add(shockAbsorptionAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Landing Shock Absorption to Gold Banshee Legs");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find SafeLanding_AbilityDef for Landing Shock Absorption");
                }
            }
            if (GoldBansheeLegItem != null)
                GoldBansheeLegItem.Abilities = goldBansheeLegsAbilities.ToArray();
            
            // GOLD GOLEM BODY ARMOR: Heavy Body Armor Jet Jump, Heavy Lifter, Demolition State, Heavy Weapons Expert
            var goldGolemBodyAbilities = new List<AbilityDef>();
            if (Config.GoldGolemBodyJetJump)
            {
                var jetpackAbility = repo.GetAllDefs<JetJumpAbilityDef>().FirstOrDefault(a => a.name.Equals("JetJump_AbilityDef"));
                if (jetpackAbility != null)
                {
                    goldGolemBodyAbilities.Add(jetpackAbility);
                }
            }
            if (Config.GoldGolemBodyHeavyLifter)
            {
                var heavyLifterAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("HeavyLifter_AbilityDef"));
                if (heavyLifterAbility != null)
                {
                    goldGolemBodyAbilities.Add(heavyLifterAbility);
                }
            }
            if (Config.GoldGolemBodyDemolitionState)
            {
                var demolitionStateAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("DemolitionMan_AbilityDef"));
                if (demolitionStateAbility != null)
                {
                    goldGolemBodyAbilities.Add(demolitionStateAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Demolition Man to Gold Golem Body");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find DemolitionMan_AbilityDef");
                }
            }
            if (Config.GoldGolemBodyHeavyWeaponsExpert)
            {
                var heavyWeaponsExpertAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("ExpertHeavyWeapons_AbilityDef"));
                if (heavyWeaponsExpertAbility != null)
                {
                    goldGolemBodyAbilities.Add(heavyWeaponsExpertAbility);
                }
            }
            if (GoldGolemBodyItem != null)
                GoldGolemBodyItem.Abilities = goldGolemBodyAbilities.ToArray();
            
            // GOLD GOLEM LEGS: Landing Shock Absorption
            if (GoldGolemLegItem != null)
            {
                if (Config.GoldGolemLegsShockAbsorption)
                {
                    var shockAbsorptionAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("SafeLanding_AbilityDef"));
                    if (shockAbsorptionAbility != null)
                    {
                        GoldGolemLegItem.Abilities = new AbilityDef[] { shockAbsorptionAbility };
                    }
                    else
                    {
                        GoldGolemLegItem.Abilities = new AbilityDef[0];
                    }
                }
                else
                {
                    GoldGolemLegItem.Abilities = new AbilityDef[0];
                }
            }
            
            // VIKING BODY ARMOR: Regeneration, Fire Resistance, Living Crystal Supercharge
            var vikingBodyAbilities = new List<AbilityDef>();
            if (Config.VikingBodyRegeneration)
            {
                var regenerationAbility = repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(a => a.name.Equals("Regeneration_Torso_Passive_AbilityDef"));
                if (regenerationAbility != null)
                {
                    vikingBodyAbilities.Add(regenerationAbility);
                }
            }
            if (Config.VikingBodyFireResistance)
            {
                var fireResistanceAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("FireStatusImmunity_AbilityDef"));
                if (fireResistanceAbility != null)
                {
                    vikingBodyAbilities.Add(fireResistanceAbility);
                    Logger.LogInfo("[ConfigurePromotionalArmourStats] Found and added Fire Resistance to Viking Body");
                }
                else
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find FireStatusImmunity_AbilityDef");
                }
            }
            if (Config.VikingBodyCrystalSupercharge)
            {
                var crystalSuperchargeAbility = repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(a => a.name.Equals("CrystalStacks_DamageAmplification_AbilityDef"));
                if (crystalSuperchargeAbility != null)
                {
                    vikingBodyAbilities.Add(crystalSuperchargeAbility);
                }
            }
            if (VikingBodyItem != null)
                VikingBodyItem.Abilities = vikingBodyAbilities.ToArray();
            
            // Clear abilities for items that should have no abilities based on config
            if (GoldGolemHelmetItem != null)
                GoldGolemHelmetItem.Abilities = new AbilityDef[0];
            if (GoldBansheeBodyItem != null)
                GoldBansheeBodyItem.Abilities = new AbilityDef[0];
            if (PRBansheeBodyItem != null)
                PRBansheeBodyItem.Abilities = new AbilityDef[0];
            if (PRBansheeLegItem != null)
                PRBansheeLegItem.Abilities = new AbilityDef[0];
            if (VikingHelmetItem != null)
                VikingHelmetItem.Abilities = new AbilityDef[0];
            if (VikingLegItem != null)
                VikingLegItem.Abilities = new AbilityDef[0];
            if (VikingRightLegItem != null)
                VikingRightLegItem.Abilities = new AbilityDef[0];
            if (VikingMainLegsItem != null)
                VikingMainLegsItem.Abilities = new AbilityDef[0];
                
            Logger.LogInfo("[ConfigurePromotionalArmourStats] Ability assignment completed.");
        }

        /// <summary>
        /// Create Armor Buff ability - exact copy from SuperCheatsModPlus (RESTORED)
        /// </summary>
        private void CreateArmorBuffAbility()
        {
            DefRepository repo = GameUtl.GameComponent<DefRepository>();
            
            string skillName = "BonusArmor2_AbilityDef";
            
            // Check if already created
            var existing = repo.GetAllDefs<ApplyEffectAbilityDef>().FirstOrDefault(a => a.name.Equals(skillName));
            if (existing != null) return;
            
            ApplyEffectAbilityDef source = repo.GetAllDefs<ApplyEffectAbilityDef>().FirstOrDefault(p => p.name.Equals("Acheron_RestorePandoranArmor_AbilityDef"));
            ItemSlotStatsModifyStatusDef sourceStatus = repo.GetAllDefs<ItemSlotStatsModifyStatusDef>().FirstOrDefault(p => p.name.Equals("E_Status [Acheron_RestorePandoranArmor_AbilityDef]"));
            
            if (source == null || sourceStatus == null) 
            {
                Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find source abilities for Armor Buff creation");
                return;
            }
            
            try
            {
                ApplyEffectAbilityDef addarmour = Helper.CreateDefFromClone(
                    source,
                    "251E62C2-F652-481E-B043-A2B1D6525B75",
                    skillName);
                    
                if (addarmour == null) 
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Failed to create Armor Buff ability");
                    return;
                }
                    
                addarmour.ViewElementDef = Helper.CreateDefFromClone(
                    source.ViewElementDef,
                   "8E49AFB8-E450-49A2-A732-9231EE8CDBA2",
                   skillName);
                addarmour.CharacterProgressionData = Helper.CreateDefFromClone(
                    source.CharacterProgressionData,
                   "3DE5F496-7515-4975-AE3C-8E68AE35DF0C",
                   skillName);
                addarmour.EffectDef = Helper.CreateDefFromClone(
                    source.EffectDef,
                   "E31F7344-8F19-4AAE-8FE7-141865E34760",
                   "E_Effect [BonusArmor2_AbilityDef]");
                
                StatusEffectDef addarmourEffect = (StatusEffectDef)addarmour.EffectDef;

                addarmourEffect.StatusDef = Helper.CreateDefFromClone(
                    sourceStatus,
                   "5262FA8D-5F25-44C2-A50F-3B32F39CC978",
                   "E_Status [BonusArmor2_AbilityDef]");

                addarmour.CharacterProgressionData = null;
                ItemSlotStatsModifyStatusDef addarmourStatus = (ItemSlotStatsModifyStatusDef)addarmourEffect.StatusDef;

                addarmour.TargetingDataDef.Origin.TargetTags = new GameTagsList
                {
                    repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("Human_TagDef"))
                };

                addarmourStatus.StatsModifications[0].Type = ItemSlotStatsModifyStatusDef.StatType.Armour;
                addarmourStatus.StatsModifications[0].ModificationType = StatModificationType.Add;
                addarmourStatus.StatsModifications[0].Value = 10f; // Fixed armor bonus

                addarmour.ViewElementDef.DisplayName1 = new LocalizedTextBind("Armor Buff", true);
                addarmour.ViewElementDef.Description = new LocalizedTextBind("Add +10 armor to allies for the duration of the mission", true);
                addarmour.UsesPerTurn = 1;
                addarmour.ActionPointCost = 0.25f; // 1 AP
                addarmour.WillPointCost = 4;

                // Add to animation definitions exactly like SuperCheatsModPlus
                foreach (TacActorSimpleAbilityAnimActionDef animActionDef in repo.GetAllDefs<TacActorSimpleAbilityAnimActionDef>().Where(aad => aad.name.Contains("Soldier_Utka_AnimActionsDef")))
                {
                    if (animActionDef.AbilityDefs != null && !animActionDef.AbilityDefs.Contains(addarmour))
                    {
                        animActionDef.AbilityDefs = animActionDef.AbilityDefs.Append(addarmour).ToArray();
                    }
                }
                
                Logger.LogInfo("[ConfigurePromotionalArmourStats] Successfully created Armor Buff ability");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"[ConfigurePromotionalArmourStats] Error creating Armor Buff ability: {e.Message}");
            }
        }
        
        /// <summary>
        /// Create configurable Gunslinger ability (RESTORED)
        /// </summary>
        private void CreateGunslingerAbility()
        {
            DefRepository repo = GameUtl.GameComponent<DefRepository>();
            
            string skillName = "Custom_Gunslinger_AbilityDef";
            
            // Check if already created
            var existing = repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals(skillName));
            if (existing != null) return;
            
            ShootAbilityDef source = repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Gunslinger_AbilityDef"));
            
            if (source == null) 
            {
                Logger.LogWarning("[ConfigurePromotionalArmourStats] Could not find Gunslinger_AbilityDef");
                return;
            }
            
            try
            {
                ShootAbilityDef gunslingerAbility = Helper.CreateDefFromClone(
                    source,
                    "4D5B8C9F-0E3A-5B2C-9F6D-8A3E4B5C6D7E",
                    skillName);
                    
                if (gunslingerAbility == null) 
                {
                    Logger.LogWarning("[ConfigurePromotionalArmourStats] Failed to create Gunslinger ability");
                    return;
                }
                
                gunslingerAbility.ViewElementDef = Helper.CreateDefFromClone(
                    source.ViewElementDef,
                    "8C4E7B2D-9A5F-6E1C-4B8A-3D7E9F0C1A2B",
                    skillName);
                    
                // Configure costs and effects
                gunslingerAbility.ActionPointCost = 0f; // Free action
                gunslingerAbility.WillPointCost = 0; // No will cost
                
                gunslingerAbility.ViewElementDef.DisplayName1 = new LocalizedTextBind("Gunslinger", true);
                gunslingerAbility.ViewElementDef.Description = new LocalizedTextBind("Gain additional shots this turn", true);
                
                // Add to animation definitions for proper animations
                foreach (TacActorSimpleAbilityAnimActionDef animActionDef in repo.GetAllDefs<TacActorSimpleAbilityAnimActionDef>().Where(aad => aad.name.Contains("Soldier_Utka_AnimActionsDef")))
                {
                    if (animActionDef.AbilityDefs != null && !animActionDef.AbilityDefs.Contains(gunslingerAbility))
                    {
                        animActionDef.AbilityDefs = animActionDef.AbilityDefs.Append(gunslingerAbility).ToArray();
                    }
                }
                
                Logger.LogInfo("[ConfigurePromotionalArmourStats] Successfully created Gunslinger ability");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"[ConfigurePromotionalArmourStats] Error creating Gunslinger ability: {e.Message}");
            }
        }

        /// <summary>
        /// Create stat bonus abilities for armor pieces based on config
        /// </summary>
        private void CreateStatBonusAbilities(DefRepository repo)
        {
            // Create Will Points bonus abilities for helmets
            if (Config.NWHelmetWillPointsBonus != 0)
            {
                CreateWillPointBonusAbility(NWPhlegethonHelmetItem, "NWHelmet_WillBonus", Config.NWHelmetWillPointsBonus);
            }
            if (Config.GoldBansheeHelmetWillPointsBonus != 0)
            {
                CreateWillPointBonusAbility(GoldBansheeHelmetItem, "GoldBansheeHelmet_WillBonus", Config.GoldBansheeHelmetWillPointsBonus);
            }
            if (Config.PRBansheeHelmetWillPointsBonus != 0)
            {
                CreateWillPointBonusAbility(PRBansheeHelmetItem, "PRBansheeHelmet_WillBonus", Config.PRBansheeHelmetWillPointsBonus);
            }
            
            // Create Strength bonus ability for Gold Odin Body
            if (Config.GoldOdinBodyStrengthBonus != 0)
            {
                CreateStrengthBonusAbility(GoldOdinBodyItem, "GoldOdinBody_StrengthBonus", Config.GoldOdinBodyStrengthBonus);
            }
        }

        /// <summary>
        /// Create a Will Points bonus ability for armor piece
        /// </summary>
        private void CreateWillPointBonusAbility(TacticalItemDef armorItem, string abilityName, int willBonus)
        {
            if (armorItem == null) return;
            
            var repo = GameUtl.GameComponent<DefRepository>();
            
            // Check if already created
            var existing = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals($"{abilityName}_AbilityDef"));
            if (existing != null) 
            {
                // Add existing ability to armor
                var currentAbilities = armorItem.Abilities?.ToList() ?? new List<AbilityDef>();
                if (!currentAbilities.Contains(existing))
                {
                    currentAbilities.Add(existing);
                    armorItem.Abilities = currentAbilities.ToArray();
                }
                return;
            }
            
            // Use EditPersonalPerkStats approach for stat modifications
            var sourceAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Resourceful_AbilityDef"));
            if (sourceAbility == null)
            {
                // Fallback to any PassiveModifierAbilityDef with StatModifications
                sourceAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.StatModifications?.Length > 0);
                if (sourceAbility == null) 
                {
                    Logger.LogWarning($"[ConfigurePromotionalArmourStats] Could not find source ability for {abilityName}");
                    return;
                }
            }
            
            try
            {
                var willBonusAbility = Helper.CreateDefFromClone(
                    sourceAbility,
                    System.Guid.NewGuid().ToString(),
                    $"{abilityName}_AbilityDef");
                
                if (willBonusAbility == null) 
                {
                    Logger.LogWarning($"[ConfigurePromotionalArmourStats] Failed to create {abilityName}");
                    return;
                }
                
                // Configure will point bonus using StatModifications (like EditPersonalPerkStats does)
                willBonusAbility.StatModifications = new ItemStatModification[]
                {
                    new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Willpower,
                        Modification = StatModificationType.Add,
                        Value = willBonus
                    },
                    new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Willpower,
                        Modification = StatModificationType.AddMax,
                        Value = willBonus
                    }
                };
                
                // Clear other modifications from source ability
                willBonusAbility.ItemTagStatModifications = new EquipmentItemTagStatModification[0];
                willBonusAbility.DamageKeywordPairs = new DamageKeywordPair[0];
                
                // Add to armor abilities
                var currentAbilities = armorItem.Abilities?.ToList() ?? new List<AbilityDef>();
                currentAbilities.Add(willBonusAbility);
                armorItem.Abilities = currentAbilities.ToArray();
                
                Logger.LogInfo($"[ConfigurePromotionalArmourStats] Created Will Points bonus ability (+{willBonus}) for {armorItem.name}");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"[ConfigurePromotionalArmourStats] Error creating {abilityName}: {e.Message}");
            }
        }

        /// <summary>
        /// Create a Strength bonus ability for armor piece
        /// </summary>
        private void CreateStrengthBonusAbility(TacticalItemDef armorItem, string abilityName, int strengthBonus)
        {
            if (armorItem == null) return;
            
            var repo = GameUtl.GameComponent<DefRepository>();
            
            // Check if already created
            var existing = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals($"{abilityName}_AbilityDef"));
            if (existing != null) 
            {
                // Add existing ability to armor
                var currentAbilities = armorItem.Abilities?.ToList() ?? new List<AbilityDef>();
                if (!currentAbilities.Contains(existing))
                {
                    currentAbilities.Add(existing);
                    armorItem.Abilities = currentAbilities.ToArray();
                }
                return;
            }
            
            // Use EditPersonalPerkStats approach for stat modifications
            var sourceAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Resourceful_AbilityDef"));
            if (sourceAbility == null)
            {
                // Fallback to any PassiveModifierAbilityDef with StatModifications
                sourceAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.StatModifications?.Length > 0);
                if (sourceAbility == null) 
                {
                    Logger.LogWarning($"[ConfigurePromotionalArmourStats] Could not find source ability for {abilityName}");
                    return;
                }
            }
            
            try
            {
                var strengthBonusAbility = Helper.CreateDefFromClone(
                    sourceAbility,
                    System.Guid.NewGuid().ToString(),
                    $"{abilityName}_AbilityDef");
                
                if (strengthBonusAbility == null) 
                {
                    Logger.LogWarning($"[ConfigurePromotionalArmourStats] Failed to create {abilityName}");
                    return;
                }
                
                // Configure strength bonus using StatModifications (like EditPersonalPerkStats does)
                strengthBonusAbility.StatModifications = new ItemStatModification[]
                {
                    new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Endurance,
                        Modification = StatModificationType.Add,
                        Value = strengthBonus
                    },
                    new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Endurance,
                        Modification = StatModificationType.AddMax,
                        Value = strengthBonus
                    }
                };
                
                // Clear other modifications from source ability
                strengthBonusAbility.ItemTagStatModifications = new EquipmentItemTagStatModification[0];
                strengthBonusAbility.DamageKeywordPairs = new DamageKeywordPair[0];
                
                // Add to armor abilities
                var currentAbilities = armorItem.Abilities?.ToList() ?? new List<AbilityDef>();
                currentAbilities.Add(strengthBonusAbility);
                armorItem.Abilities = currentAbilities.ToArray();
                
                Logger.LogInfo($"[ConfigurePromotionalArmourStats] Created Strength bonus ability (+{strengthBonus}) for {armorItem.name}");
            }
            catch (System.Exception e)
            {
                Logger.LogError($"[ConfigurePromotionalArmourStats] Error creating {abilityName}: {e.Message}");
            }
        }

        /// <summary>
        /// Remove all added abilities when mod is disabled
        /// </summary>
        private void RemoveAllAddedAbilities()
        {
            // Clear all abilities from armor pieces
            if (GoldGolemHelmetItem != null)
                GoldGolemHelmetItem.Abilities = new AbilityDef[0];
            if (GoldGolemBodyItem != null)
                GoldGolemBodyItem.Abilities = new AbilityDef[0];
            if (GoldOdinHelmetItem != null)
                GoldOdinHelmetItem.Abilities = new AbilityDef[0];
            if (GoldOdinBodyItem != null)
                GoldOdinBodyItem.Abilities = new AbilityDef[0];
            if (GoldOdinLegItem != null)
                GoldOdinLegItem.Abilities = new AbilityDef[0];
            if (GoldBansheeHelmetItem != null)
                GoldBansheeHelmetItem.Abilities = new AbilityDef[0];
            if (GoldBansheeBodyItem != null)
                GoldBansheeBodyItem.Abilities = new AbilityDef[0];
            if (GoldBansheeLegItem != null)
                GoldBansheeLegItem.Abilities = new AbilityDef[0];
            if (PRBansheeHelmetItem != null)
                PRBansheeHelmetItem.Abilities = new AbilityDef[0];
            if (PRBansheeBodyItem != null)
                PRBansheeBodyItem.Abilities = new AbilityDef[0];
            if (PRBansheeLegItem != null)
                PRBansheeLegItem.Abilities = new AbilityDef[0];
            if (NWPhlegethonHelmetItem != null)
                NWPhlegethonHelmetItem.Abilities = new AbilityDef[0];
            if (NWPhlegethonBodyItem != null)
                NWPhlegethonBodyItem.Abilities = new AbilityDef[0];
            if (NWPhlegethonLegItem != null)
                NWPhlegethonLegItem.Abilities = new AbilityDef[0];
            if (VikingHelmetItem != null)
                VikingHelmetItem.Abilities = new AbilityDef[0];
            if (VikingBodyItem != null)
                VikingBodyItem.Abilities = new AbilityDef[0];
            if (VikingLegItem != null)
                VikingLegItem.Abilities = new AbilityDef[0];
            if (VikingRightLegItem != null)
                VikingRightLegItem.Abilities = new AbilityDef[0];
            if (VikingMainLegsItem != null)
                VikingMainLegsItem.Abilities = new AbilityDef[0];
            if (GoldGolemLegItem != null)
                GoldGolemLegItem.Abilities = new AbilityDef[0];
        }
    }
}