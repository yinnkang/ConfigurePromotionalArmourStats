using PhoenixPoint.Modding;

namespace ConfigurePromotionalArmourStats
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// 
    /// CURRENT IMPLEMENTATION: Option A - Direct Ability References
    /// - Abilities use existing game abilities with default AP/WP costs and effects
    /// - No missing key errors, guaranteed Geoscape UI display
    /// - Only boolean enable/disable options available
    /// - For configurable costs/effects, request Option B implementation
    /// </summary>
    public class ConfigurePromotionalArmourStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Gold Odin Helmet
        public float GoldOdinHelmetArmor = 23f;
        public float GoldOdinHelmetSpeed = 0f;
        public float GoldOdinHelmetPerception = 10f;
        [ConfigField(text: "GoldOdin Helmet Stealth (in %)")]
        public float GoldOdinHelmetStealth = 0f;
        [ConfigField(text: "GoldOdin Helmet Accuracy (in %)")]
        public float GoldOdinHelmetAccuracy = 5f;
        public int GoldOdinHelmetWeight = 1;

        /// Gold Odin Body
        public float GoldOdinBodyArmor = 24f;
        public float GoldOdinBodySpeed = 1f;
        public float GoldOdinBodyPerception = 0f;
        [ConfigField(text: "GoldOdin Body Stealth (in %)")]
        public float GoldOdinBodyStealth = 0f;
        [ConfigField(text: "GoldOdin Body Accuracy (in %)")]
        public float GoldOdinBodyAccuracy = 5f;
        public int GoldOdinBodyWeight = 3;

        /// Gold Odin Leg
        public float GoldOdinLegArmor = 22f;
        public float GoldOdinLegSpeed = 1f;
        public float GoldOdinLegPerception = 0f;
        [ConfigField(text: "GoldOdin Leg Stealth (in %)")]
        public float GoldOdinLegStealth = 0f;
        [ConfigField(text: "GoldOdin Leg Accuracy (in %)")]
        public float GoldOdinLegAccuracy = 2f;
        public int GoldOdinLegWeight = 2;

        /// Gold Golem Helmet
        public float GoldGolemHelmetArmor = 33f;
        public float GoldGolemHelmetSpeed = 0f;
        public float GoldGolemHelmetPerception = 0f;
        [ConfigField(text: "GoldGolem Helmet Stealth (in %)")]
        public float GoldGolemHelmetStealth = -10f;
        [ConfigField(text: "GoldGolem Helmet Accuracy (in %)")]
        public float GoldGolemHelmetAccuracy = 0f;
        public int GoldGolemHelmetWeight = 2;

        /// Gold Golem Body
        public float GoldGolemBodyArmor = 40f;
        public float GoldGolemBodySpeed = 0f;
        public float GoldGolemBodyPerception = 0f;
        [ConfigField(text: "GoldGolem Body Stealth (in %)")]
        public float GoldGolemBodyStealth = -20f;
        [ConfigField(text: "GoldGolem Body Accuracy (in %)")]
        public float GoldGolemBodyAccuracy = 0f;
        public int GoldGolemBodyWeight = 3;
        [ConfigField(text: "GoldGolem Body Jet Jump Fumble Perc (in %)")]
        public int GoldGolemBodyFumblePerc = 0;

        /// Gold Golem Leg
        public float GoldGolemLegArmor = 35f;
        public float GoldGolemLegSpeed = 0f;
        public float GoldGolemLegPerception = 0f;
        [ConfigField(text: "GoldGolem Leg Stealth (in %)")]
        public float GoldGolemLegStealth = -10f;
        [ConfigField(text: "GoldGolem Leg Accuracy (in %)")]
        public float GoldGolemLegAccuracy = 0f;
        public int GoldGolemLegWeight = 3;

        /// Gold Banshee Helmet
        public float GoldBansheeHelmetArmor = 18f;
        public float GoldBansheeHelmetSpeed = 0f;
        public float GoldBansheeHelmetPerception = 14f;
        [ConfigField(text: "GoldBanshee Helmet Stealth (in %)")]
        public float GoldBansheeHelmetStealth = 5f;
        [ConfigField(text: "GoldBanshee Helmet Accuracy (in %)")]
        public float GoldBansheeHelmetAccuracy = 10f;
        public int GoldBansheeHelmetWeight = 1;

        /// Gold Banshee Body
        public float GoldBansheeBodyArmor = 20f;
        public float GoldBansheeBodySpeed = 0f;
        public float GoldBansheeBodyPerception = 0f;
        [ConfigField(text: "GoldBanshee Body Stealth (in %)")]
        public float GoldBansheeBodyStealth = 10f;
        [ConfigField(text: "GoldBanshee Body Accuracy (in %)")]
        public float GoldBansheeBodyAccuracy = 5f;
        public int GoldBansheeBodyWeight = 2;

        /// Gold Banshee Leg
        public float GoldBansheeLegArmor = 18f;
        public float GoldBansheeLegSpeed = 0f;
        public float GoldBansheeLegPerception = 0f;
        [ConfigField(text: "GoldBanshee Leg Stealth (in %)")]
        public float GoldBansheeLegStealth = 10f;
        [ConfigField(text: "GoldBanshee Leg Accuracy (in %)")]
        public float GoldBansheeLegAccuracy = 5f;
        public int GoldBansheeLegWeight = 2;

        /// PR Banshee Helmet
        public float PRBansheeHelmetArmor = 20f;
        public float PRBansheeHelmetSpeed = 0f;
        public float PRBansheeHelmetPerception = 5f;
        [ConfigField(text: "PRBanshee Helmet Stealth (in %)")]
        public float PRBansheeHelmetStealth = 3f;
        [ConfigField(text: "PRBanshee Helmet Accuracy (in %)")]
        public float PRBansheeHelmetAccuracy = 7f;
        public int PRBansheeHelmetWeight = 1;

        /// PR Banshee Body
        public float PRBansheeBodyArmor = 20f;
        public float PRBansheeBodySpeed = 1f;
        public float PRBansheeBodyPerception = 0f;
        [ConfigField(text: "PRBanshee Body Stealth (in %)")]
        public float PRBansheeBodyStealth = 5f;
        [ConfigField(text: "PRBanshee Body Accuracy (in %)")]
        public float PRBansheeBodyAccuracy = 4f;
        public int PRBansheeBodyWeight = 2;

        /// PR Banshee Leg
        public float PRBansheeLegArmor = 20f;
        public float PRBansheeLegSpeed = 1f;
        public float PRBansheeLegPerception = 0f;
        [ConfigField(text: "PRBanshee Leg Stealth (in %)")]
        public float PRBansheeLegStealth = 6f;
        [ConfigField(text: "PRBanshee Leg Accuracy (in %)")]
        public float PRBansheeLegAccuracy = 4f;
        public int PRBansheeLegWeight = 2;

        /// NW Phlegethon Helmet
        public float NWPhlegethonHelmetArmor = 20f;
        public float NWPhlegethonHelmetSpeed = 0f;
        public float NWPhlegethonHelmetPerception = 7f;
        [ConfigField(text: "NWPhlegethon Helmet Stealth (in %)")]
        public float NWPhlegethonHelmetStealth = 5f;
        [ConfigField(text: "NWPhlegethon Helmet Accuracy (in %)")]
        public float NWPhlegethonHelmetAccuracy = 10f;
        public int NWPhlegethonHelmetWeight = 1;

        /// NW Phlegethon Body
        public float NWPhlegethonBodyArmor = 22f;
        public float NWPhlegethonBodySpeed = 1f;
        public float NWPhlegethonBodyPerception = 0f;
        [ConfigField(text: "NWPhlegethon Body Stealth (in %)")]
        public float NWPhlegethonBodyStealth = 0f;
        [ConfigField(text: "NWPhlegethon Body Accuracy (in %)")]
        public float NWPhlegethonBodyAccuracy = 3f;
        public int NWPhlegethonBodyWeight = 2;

        /// NW Phlegethon Leg
        public float NWPhlegethonLegArmor = 20f;
        public float NWPhlegethonLegSpeed = 2f;
        public float NWPhlegethonLegPerception = 0f;
        [ConfigField(text: "NWPhlegethon Leg Stealth (in %)")]
        public float NWPhlegethonLegStealth = 0f;
        [ConfigField(text: "NWPhlegethon Leg Accuracy (in %)")]
        public float NWPhlegethonLegAccuracy = 2f;
        public int NWPhlegethonLegWeight = 2;

        // Additional Armor Abilities (for NewAbilitiesforGoldArmor mod compatibility)
        
        /// Gold Golem Body - Expert Heavy Weapons Ability (passive)
        [ConfigField(text: "GoldGolem Body Expert Heavy Weapons")]
        public bool GoldGolemBodyExpertHeavyWeapons = true;

        /// Gold Banshee Helmet - Gunslinger Ability
        [ConfigField(text: "GoldBanshee Helmet Gunslinger")]
        public bool GoldBansheeHelmetGunslinger = true;
        [ConfigField(text: "GoldBanshee Helmet Gunslinger Extra Shots")]
        public int GoldBansheeHelmetGunslingerShots = 1;
        [ConfigField(text: "GoldBanshee Helmet Gunslinger AP Cost")]
        public int GoldBansheeHelmetGunslingerAPCost = 0;
        [ConfigField(text: "GoldBanshee Helmet Gunslinger WP Cost")]
        public int GoldBansheeHelmetGunslingerWPCost = 5;

        /// PR Banshee Helmet - Silent Echo (Echo Head bionic ability)
        [ConfigField(text: "PRBanshee Helmet Silent Echo")]
        public bool PRBansheeHelmetSilentEcho = true;
        
        /// PR Banshee Helmet - Night Vision (Enhanced Vision bionic ability)
        [ConfigField(text: "PRBanshee Helmet Night Vision")]
        public bool PRBansheeHelmetNightVision = true;

        /// Gold Odin Helmet - Frenzy Ability (moved from PR Banshee Helmet)
        [ConfigField(text: "GoldOdin Helmet Frenzy")]
        public bool GoldOdinHelmetFrenzy = true;
        
        /// Gold Odin Body - Radiant Hope Passive
        [ConfigField(text: "GoldOdin Body Radiant Hope")]
        public bool GoldOdinBodyRadiantHope = true;

        /// NW Phlegethon Legs - Jump Ability (moved from Gold Odin)
        [ConfigField(text: "NWPhlegethon Legs Jump")]
        public bool NWPhlegethonLegJump = true;
        
        /// NW Phlegethon Legs - Shadowstep (Mirage Legs bionic ability)
        [ConfigField(text: "NWPhlegethon Legs Shadowstep")]
        public bool NWPhlegethonLegsShadowstep = true;

        /// Gold Golem Helmet - Daze Immunity
        [ConfigField(text: "GoldGolem Helmet Daze Immunity")]
        public bool GoldGolemHelmetDazeImmunity = true;
        
        /// Gold Golem Helmet - Armor Buff Ability (moved from Gold Golem Body)
        [ConfigField(text: "GoldGolem Helmet Armor Buff")]
        public bool GoldGolemHelmetArmorBuff = true;
        [ConfigField(text: "GoldGolem Helmet Armor Buff Strength")]
        public float GoldGolemHelmetArmorBuffStrength = 10f;
        [ConfigField(text: "GoldGolem Helmet Armor Buff AP Cost")]
        public int GoldGolemHelmetArmorBuffAPCost = 1;
        [ConfigField(text: "GoldGolem Helmet Armor Buff WP Cost")]
        public int GoldGolemHelmetArmorBuffWPCost = 4;
        
        /// Gold Golem Body - Crystal SuperCharge (replaces shred resistance)
        [ConfigField(text: "GoldGolem Body Crystal SuperCharge")]
        public bool GoldGolemBodyCrystalSuperCharge = true;
        
        // Viking Armor Set
        
        /// Viking Helmet
        public float VikingHelmetArmor = 30f;
        public float VikingHelmetSpeed = 0f;
        public float VikingHelmetPerception = 0f;
        [ConfigField(text: "Viking Helmet Stealth (in %)")]
        public float VikingHelmetStealth = 0f;
        [ConfigField(text: "Viking Helmet Accuracy (in %)")]
        public float VikingHelmetAccuracy = 0f;
        public int VikingHelmetWeight = 1;
        
        /// Viking Helmet - Daze Immunity
        [ConfigField(text: "Viking Helmet Daze Immunity")]
        public bool VikingHelmetDazeImmunity = true;
        
        /// NW Phlegethon Helmet - Mind Control Immunity
        [ConfigField(text: "NWPhlegethon Helmet Mind Control Immunity")]
        public bool NWPhlegethonHelmetMindControlImmunity = true;
        
        /// Viking Body
        public float VikingBodyArmor = 34f;
        public float VikingBodySpeed = 2f;
        public float VikingBodyPerception = 0f;
        [ConfigField(text: "Viking Body Stealth (in %)")]
        public float VikingBodyStealth = 0f;
        [ConfigField(text: "Viking Body Accuracy (in %)")]
        public float VikingBodyAccuracy = 0f;
        public int VikingBodyWeight = 3;
        
        /// Viking Body - Regenerate Ability
        [ConfigField(text: "Viking Body Regenerate")]
        public bool VikingBodyRegenerate = true;
        
        /// Viking Body - Fire Resistance
        [ConfigField(text: "Viking Body Fire Resistance")]
        public bool VikingBodyFireResistance = true;
        
        /// Viking Legs
        public float VikingLegArmor = 30f;
        public float VikingLegSpeed = 4f;
        public float VikingLegPerception = 0f;
        [ConfigField(text: "Viking Leg Stealth (in %)")]
        public float VikingLegStealth = 0f;
        [ConfigField(text: "Viking Leg Accuracy (in %)")]
        public float VikingLegAccuracy = 0f;
        public int VikingLegWeight = 2;
        
        /// Viking Legs - Goo Immunity
        [ConfigField(text: "Viking Legs Goo Immunity")]
        public bool VikingLegsGooImmunity = true;
        
        /// Viking Legs - Jump Ability
        [ConfigField(text: "Viking Legs Jump")]
        public bool VikingLegsJump = true;

    }

}
