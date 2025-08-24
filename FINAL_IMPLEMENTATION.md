# ConfigurePromotionalArmourStats - Final Implementation (Option A)

## Implementation Choice: Direct Ability References

**Approach**: Use existing game abilities directly from DefRepository without customization
**Benefits**: ✅ No missing key errors ✅ Guaranteed UI display ✅ Stable functionality
**Trade-off**: ❌ No configurability for AP/WP costs or effect values

## Current Ability Assignments

### Gold Golem Body (2 abilities)
- **Expert Heavy Weapons** (Passive)
  - Source: `ExpertHeavyWeapons_AbilityDef`
  - Effect: Combat bonuses with heavy weapons
  - Config: `GoldGolemBodyExpertHeavyWeapons = true`

- **Crystal SuperCharge** (Active) 
  - Source: `CrystalStacks_DamageAmplification_AbilityDef`
  - Effect: Damage amplification through crystal energy
  - Config: `GoldGolemBodyCrystalSuperCharge = true`

### Gold Banshee Helmet (1 ability)
- **Gunslinger** (Active)
  - Source: `Gunslinger_AbilityDef` 
  - Effect: Extra shots per turn (game default values)
  - Config: `GoldBansheeHelmetGunslinger = true`

### PR Banshee Helmet (1 ability) 
- **Frenzy** (Active)
  - Source: `Priest_InstilFrenzy_AbilityDef`
  - Effect: Speed bonus + panic immunity (game default values)
  - Config: `PRBansheeHelmetFrenzy = true`

### Gold Odin Legs (1 ability)
- **Jump** (Active)
  - Source: `Exo_Leap_AbilityDef`
  - Effect: Rocket-powered leap movement (game default values)
  - Config: `GoldOdinLegJump = true`

### NW Phlegethon Body (1 ability)
- **Armor Buff** (Active) - *Custom ability matching SuperCheatsModPlus*
  - Source: Custom `BonusArmor2_AbilityDef` 
  - Effect: +10 armor to allies for mission duration
  - Cost: 4 WP, 1 use per turn
  - Config: `NWPhlegethonBodyArmorBuff = true`

## Removed Configurations
The following config options were removed as they're no longer needed:
- `GoldGolemBodyShredResistance`
- `GoldGolemBodyShredResistanceValue`
- All custom AP/WP cost configurations for existing abilities

## Build Status
✅ **Build Successful**: No errors or warnings
✅ **DLL Generated**: `ConfigurePromotionalArmourStats.dll` ready for deployment
✅ **Compatibility**: Should work without missing key errors

## Expected Game Behavior
1. **Geoscape UI**: All 6 abilities display properly with original game names/descriptions
2. **No Errors**: No missing key errors in game logs
3. **Full Functionality**: All abilities work exactly as in base game
4. **Config Control**: Players can enable/disable individual abilities through mod settings

## Future Enhancement Path
If you need configurable AP/WP costs later:
- **Option B**: Implement configurable clones with SuperCheatsModPlus pattern
- **Option C**: Hybrid approach (configurable for some, direct reference for others)
- Both options are documented and ready for implementation

## Installation
1. Copy `Dist/ConfigurePromotionalArmourStats.dll` to Phoenix Point mods folder
2. Enable mod in Phoenix Point mod manager  
3. Configure abilities through in-game mod settings
4. Equip promotional armor pieces to see abilities in Geoscape UI