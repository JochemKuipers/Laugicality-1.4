using Laugicality.Content.Tiles;
using Laugicality.Utilities.Base;
using Laugicality.Utilities.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Items.Accessories
{
    public class CursedRelic : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Cursed? \nMastery of violence");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.10f;
            LaugicalityPlayer modPlayer = LaugicalityPlayer.Get(player);
            if (modPlayer.SoulStoneVisuals && modPlayer.inf)
                player.AddBuff(116, 2);
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            player.manaRegenBonus += 25;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.kbBuff = true;
            if (modPlayer.battle)
                player.enemySpawns = true;
            if (player.thorns < 1f)
            {
                player.thorns = 0.333333343f;
            }
            player.ammoCost80 = true;
            player.GetDamage(DamageClass.Ranged) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TitanStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AggressionStone>(), 1);
            recipe.AddTile(ModContent.TileType<AncientEnchanter>());
            recipe.Register();
        }
    }
}