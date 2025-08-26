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
            GoldOdinHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Helmet_Gold_BodyPartDef"));
            GoldOdinBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Torso_Gold_BodyPartDef"));
            GoldOdinLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Legs_Gold_ItemDef"));
            GoldGolemHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Helmet_Gold_BodyPartDef"));
            GoldGolemBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Torso_Gold_BodyPartDef"));
            GoldGolemLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Legs_Gold_ItemDef"));
            GoldBansheeHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Helmet_Gold_BodyPartDef"));
            GoldBansheeBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Torso_Gold_BodyPartDef"));
            GoldBansheeLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Legs_Gold_ItemDef"));
            PRBansheeHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Helmet_RisingSun_BodyPartDef"));
            PRBansheeBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Torso_RisingSun_BodyPartDef"));
            PRBansheeLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Legs_RisingSun_ItemDef"));
            NWPhlegethonHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Helmet_WhiteNeon_BodyPartDef"));
            NWPhlegethonBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Torso_WhiteNeon_BodyPartDef"));
            NWPhlegethonLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Legs_WhiteNeon_ItemDef"));
            VikingHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Helmet_Viking_BodyPartDef"));
            VikingBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Torso_Viking_BodyPartDef"));
            VikingLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_LeftLeg_Viking_BodyPartDef"));
            VikingRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_RightLeg_Viking_BodyPartDef"));
            VikingMainLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Legs_Viking_ItemDef"));


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
        }

        /* WEAPON DATA FUNCTIONS */

        private ArmorValues getArmorValuesFromArmorDef(TacticalItemDef armorDef)
        {
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
        }


        /// <summary>
        /// Apply additional abilities to armor pieces based on config
        /// </summary>
        private void ApplyArmorAbilities()
        {
            DefRepository repo = GameUtl.GameComponent<DefRepository>();
            
            // Remove existing abilities first to ensure clean state
            RemoveAllAddedAbilities();
            
            // Create configurable custom abilities
            CreateGunslingerAbility();
            
            // Assign abilities to armor pieces using existing game abilities
            AssignAbilitiesToArmor(repo);
        }


        /// <summary>
        /// Create and assign abilities to armor pieces based on config - Updated per user requirements
        /// </summary>
        private void AssignAbilitiesToArmor(DefRepository repo)
        {
            // NEON WHITE HELMET: Instill Frenzy, Virus Resistant, +5 Will Points
            var nwHelmetAbilities = new List<AbilityDef>();
            if (Config.NWHelmetInstillFrenzy)
            {
                var frenzyAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Priest_InstilFrenzy_AbilityDef"));
                if (frenzyAbility != null)
                {
                    nwHelmetAbilities.Add(frenzyAbility);
                }
            }
            if (Config.NWHelmetVirusResistant)
            {
                var virusResistantAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("VirusStatusImmunity_AbilityDef"));
                if (virusResistantAbility != null)
                {
                    nwHelmetAbilities.Add(virusResistantAbility);
                }
            }
            NWPhlegethonHelmetItem.Abilities = nwHelmetAbilities.ToArray();
            
            // NEON WHITE BODY ARMOR: Radiant Hope
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
            
            // NEON WHITE LEGS: Immune to Goo
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
            
            // GOLD ODIN HELMET: Mind Control Immunity, Poison Resistant
            var goldOdinHelmetAbilities = new List<AbilityDef>();
            if (Config.GoldOdinHelmetMindControlImmunity)
            {
                var mindControlImmunityAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("MindControlStatusImmunity_AbilityDef"));
                if (mindControlImmunityAbility != null)
                {
                    goldOdinHelmetAbilities.Add(mindControlImmunityAbility);
                }
            }
            if (Config.GoldOdinHelmetPoisonResistant)
            {
                var poisonResistantAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("PoisonStatusImmunity_AbilityDef"));
                if (poisonResistantAbility != null)
                {
                    goldOdinHelmetAbilities.Add(poisonResistantAbility);
                }
            }
            GoldOdinHelmetItem.Abilities = goldOdinHelmetAbilities.ToArray();
            
            // GOLD ODIN BODY ARMOR: Combat Matrix, Fire Resistant, +5 Strength
            var goldOdinBodyAbilities = new List<AbilityDef>();
            if (Config.GoldOdinBodyCombatMatrix)
            {
                var combatMatrixAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Vengeance_AbilityDef"));
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
            GoldOdinLegItem.Abilities = goldOdinLegsAbilities.ToArray();
            
            // GOLD BANSHEE HELMET: Night Vision, Silent Echo, +4 Will Points
            var goldBansheeHelmetAbilities = new List<AbilityDef>();
            if (Config.GoldBansheeHelmetNightVision)
            {
                var nightVisionAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("EnhancedVision_AbilityDef"));
                if (nightVisionAbility != null)
                {
                    goldBansheeHelmetAbilities.Add(nightVisionAbility);
                }
            }
            if (Config.GoldBansheeHelmetSilentEcho)
            {
                var silentEchoAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("SilentEcho_AbilityDef"));
                if (silentEchoAbility != null)
                {
                    goldBansheeHelmetAbilities.Add(silentEchoAbility);
                }
            }
            GoldBansheeHelmetItem.Abilities = goldBansheeHelmetAbilities.ToArray();
            
            // PR BANSHEE HELMET: Gunslinger (custom configurable), +4 Will Points
            if (Config.PRBansheeHelmetGunslinger)
            {
                var gunslingerAbility = repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Custom_Gunslinger_AbilityDef"));
                if (gunslingerAbility != null)
                {
                    PRBansheeHelmetItem.Abilities = new AbilityDef[] { gunslingerAbility };
                }
                else
                {
                    PRBansheeHelmetItem.Abilities = new AbilityDef[0];
                }
            }
            else
            {
                PRBansheeHelmetItem.Abilities = new AbilityDef[0];
            }
            
            // PR BANSHEE LEGS: Rocket Leap, Landing Shock Absorption
            var prBansheeLegsAbilities = new List<AbilityDef>();
            if (Config.PRBansheeLegsRocketLeap)
            {
                var rocketLeapAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("PropellerJump_AbilityDef"));
                if (rocketLeapAbility != null)
                {
                    prBansheeLegsAbilities.Add(rocketLeapAbility);
                }
            }
            if (Config.PRBansheeLegsShockAbsorption)
            {
                var shockAbsorptionAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("NoFallDamage_AbilityDef"));
                if (shockAbsorptionAbility != null)
                {
                    prBansheeLegsAbilities.Add(shockAbsorptionAbility);
                }
            }
            PRBansheeLegItem.Abilities = prBansheeLegsAbilities.ToArray();
            
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
                var heavyLifterAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("WeightCapacityIncrease_AbilityDef"));
                if (heavyLifterAbility != null)
                {
                    goldGolemBodyAbilities.Add(heavyLifterAbility);
                }
            }
            if (Config.GoldGolemBodyDemolitionState)
            {
                var demolitionStateAbility = repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Juggernaut_AbilityDef"));
                if (demolitionStateAbility != null)
                {
                    goldGolemBodyAbilities.Add(demolitionStateAbility);
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
            GoldGolemBodyItem.Abilities = goldGolemBodyAbilities.ToArray();
            
            // GOLD GOLEM LEGS: Landing Shock Absorption
            if (Config.GoldGolemLegsShockAbsorption)
            {
                var shockAbsorptionAbility = repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("NoFallDamage_AbilityDef"));
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
            VikingBodyItem.Abilities = vikingBodyAbilities.ToArray();
            
            // Clear abilities for items that should have no abilities
            if (GoldGolemHelmetItem != null)
                GoldGolemHelmetItem.Abilities = new AbilityDef[0];
            if (GoldBansheeBodyItem != null)
                GoldBansheeBodyItem.Abilities = new AbilityDef[0];
            if (GoldBansheeLegItem != null)
                GoldBansheeLegItem.Abilities = new AbilityDef[0];
            if (PRBansheeBodyItem != null)
                PRBansheeBodyItem.Abilities = new AbilityDef[0];
            if (VikingHelmetItem != null)
                VikingHelmetItem.Abilities = new AbilityDef[0];
            if (VikingLegItem != null)
                VikingLegItem.Abilities = new AbilityDef[0];
            if (VikingRightLegItem != null)
                VikingRightLegItem.Abilities = new AbilityDef[0];
            if (VikingMainLegsItem != null)
                VikingMainLegsItem.Abilities = new AbilityDef[0];
        }

        /// <summary>
        /// Create configurable Gunslinger ability
        /// </summary>
        private void CreateGunslingerAbility()
        {
            DefRepository repo = GameUtl.GameComponent<DefRepository>();
            
            string skillName = "Custom_Gunslinger_AbilityDef";
            ShootAbilityDef source = repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Gunslinger_AbilityDef"));
            
            if (source == null) return;
            
            ShootAbilityDef gunslingerAbility = Helper.CreateDefFromClone(
                source,
                "4D5B8C9F-0E3A-5B2C-9F6D-8A3E4B5C6D7E",
                skillName);
                
            if (gunslingerAbility == null) return;
            
            gunslingerAbility.ViewElementDef = Helper.CreateDefFromClone(
                source.ViewElementDef,
                "8C4E7B2D-9A5F-6E1C-4B8A-3D7E9F0C1A2B",
                skillName);
                
            // Configure costs and effects from config
            gunslingerAbility.ActionPointCost = 0; // Free action
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
            if (GoldOdinBodyItem != null)
                GoldOdinBodyItem.Abilities = new AbilityDef[0];
            if (GoldBansheeHelmetItem != null)
                GoldBansheeHelmetItem.Abilities = new AbilityDef[0];
            if (PRBansheeHelmetItem != null)
                PRBansheeHelmetItem.Abilities = new AbilityDef[0];
            if (NWPhlegethonHelmetItem != null)
                NWPhlegethonHelmetItem.Abilities = new AbilityDef[0];
            if (NWPhlegethonLegItem != null)
                NWPhlegethonLegItem.Abilities = new AbilityDef[0];
            if (VikingHelmetItem != null)
                VikingHelmetItem.Abilities = new AbilityDef[0];
            if (VikingBodyItem != null)
                VikingBodyItem.Abilities = new AbilityDef[0];
            if (VikingLegItem != null)
                VikingLegItem.Abilities = new AbilityDef[0];
        }

    }
}
