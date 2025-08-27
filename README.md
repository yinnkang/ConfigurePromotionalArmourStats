# ConfigurePromotionalArmourStats

Phoenix Point mod for configuring promotional armor stats and abilities with full customization options.

## Armor Sets

- **Gold Odin** (Assault) - Mind Control Immunity, Poison Resistant, Combat Matrix, Fire Resistant, Melee Proficiency, Jump, Shadowstep, +5 Strength
- **Gold Golem** (Heavy) - Jet Jump, Heavy Lifter, Demolition State, Heavy Weapons Expert, **Armor Buff** (configurable AP/WP/value)
- **Gold Banshee** (Sniper) - **Gunslinger** (configurable AP/WP), Rocket Leap, Landing Absorption, +4 Will Points  
- **PR Banshee** - Night Vision, Silent Echo, +4 Will Points
- **NW Phlegethon** - Instill Frenzy, Virus Resistant, Radiant Hope, Goo Immunity, +5 Will Points
- **Viking** - Regeneration, Fire Resistance, Crystal Supercharge

## Configuration

### Stats (per armor piece)
- Armor, Speed, Perception, Stealth %, Accuracy %, Weight
- Jet Jump fumble % (Gold Golem Body only)

### Abilities 
- **Armor Buff**: AP Cost (1-4), WP Cost, Armor Value (Gold Golem Body)
- **Gunslinger**: AP Cost (0-4), WP Cost (Gold Banshee Helmet)
- All other abilities: Enable/disable toggles

### Usage
1. Enable mod in Phoenix Point Mods menu
2. Configure values in mod settings (main menu only)
3. Changes apply immediately to all armor pieces

## Technical Details

- Modifies promotional armor TacticalItemDefs directly
- Creates custom abilities: `BonusArmor2_AbilityDef`, `Custom_Gunslinger_AbilityDef`
- Uses existing game abilities where possible
- AP costs convert: User input (1-4) → Game format (0.25-1.0)
- Safe to enable/disable mid-campaign

## Installation

Extract to `Documents/My Games/Phoenix Point/Mods/` directory.

## Build

Requires ModSDK in parent directory. Output in `Dist/` folder.