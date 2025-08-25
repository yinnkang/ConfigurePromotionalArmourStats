# Configure Promotional Armor Stats

A Phoenix Point mod that allows configuration of statistics for promotional armor pieces through the mod settings interface.

## Supported Armor Sets

### Available Promotional Armor
- **Gold Odin Set** (Assault class): Helmet, Body, Legs
- **Gold Golem Set** (Heavy class): Helmet, Body, Legs  
- **Gold Banshee Set** (Sniper class): Helmet, Body, Legs
- **Phoenix Rising Banshee Set**: Helmet, Body, Legs
- **New World Phlegethon Set**: Helmet, Body, Legs
- **Viking Berserker Set**: Helmet, Body, Legs (multiple leg pieces)

## Configurable Statistics

Each armor piece can have the following values modified:

### Basic Stats
- **Armor Rating**: Damage reduction provided by the piece
- **Speed Modifier**: Movement speed change (positive or negative)
- **Perception**: Detection and awareness bonuses
- **Stealth**: Concealment rating (percentage)
- **Accuracy**: Weapon precision modifier (percentage)  
- **Weight**: Equipment weight affecting movement

## Installation

1. Extract the mod to your Phoenix Point Mods directory
2. Enable "Configure Promotional Armor Stats" in the Mods menu
3. Configure desired armor values in the mod settings
4. Changes apply to equipped armor pieces

## Configuration

Access configuration through the in-game mod settings menu. Each armor piece has individual sliders/inputs for:

- Armor rating values
- Speed penalties/bonuses
- Perception bonuses
- Stealth percentages
- Accuracy percentages
- Weight values

Default values match the original game statistics.

## Technical Notes

- Changes apply to armor definitions when the mod loads
- Mod can be safely enabled/disabled during campaigns
- Original values are restored when the mod is disabled
- Uses Phoenix Point's DefRepository system for armor modification

## Build Instructions

1. Ensure ModSDK is available in the parent directory
2. Build using Visual Studio or MSBuild
3. Output will be in the Dist directory
4. Copy Dist contents to Phoenix Point Mods folder

## Compatibility

- Compatible with other armor modification mods
- Can be enabled/disabled mid-campaign
- Preserves original armor values when disabled