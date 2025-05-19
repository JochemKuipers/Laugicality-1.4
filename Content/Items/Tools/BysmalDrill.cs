using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities.Base;

namespace Laugicality.Content.Items.Tools
{
    public class BysmalDrill : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Drill-o-Tron 5000");
            // Tooltip.SetDefault("'To the core.'");
        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee;
            Item.width = 82;
            Item.height = 34;
            Item.useTime = 1;
            Item.useAnimation = 11;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.pick = 2500;
            Item.tileBoost += 8;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Tools.BysmalDrillProjectile>();
            Item.shootSpeed = 40f;
        }

        public override bool? UseItem(Player player)
        {
            float mag = 14f;
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
            //player.pickSpeed = 0.00001f;
            for(int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    int k = Main.SmartCursorX + i;
                    int l = Main.SmartCursorY + j;
                    Terraria.WorldGen.KillTile(k, l);
                }
            }
            return true;
        }
        public override void HoldItem(Player player)
        {
            player.pickSpeed -= 0.25f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            float mag = 14f;
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
