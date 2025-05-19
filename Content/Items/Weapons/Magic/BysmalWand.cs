using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Materials;
using Laugicality.Content.Projectiles.Magic;
using Laugicality.Content.Projectiles.Special;
using Laugicality.Content.Tiles;
using Laugicality.Utilities;
using Laugicality.Utilities.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Items.Weapons.Magic
{
    public class BysmalWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bysmal Wand");
            // Tooltip.SetDefault("'Frigid Breeze'\nWhen in the Etherial after defeating Etheria, shoot an additional blast.");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.rare = 9;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BysmalBlast>();
            Item.shootSpeed = 24f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.MouseWorld;
            Vector2 vel = player.DirectionTo(target) * Item.shootSpeed;
            int n = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, vel.X, vel.Y, ModContent.ProjectileType<Projectiles.Magic.BysmalWandProjectile>(), (int)(Item.damage), 3, Main.myPlayer);
            Main.projectile[n].ai[0] = 1;
            int M = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, vel.X, vel.Y, ModContent.ProjectileType<BysmalWandProjectile>(), (int)(Item.damage), 3, Main.myPlayer);
            Main.projectile[M].ai[0] = -1;
            if ((LaugicalityWorld.downedEtheria || LaugicalityPlayer.Get(player).Etherable > 2) && LaugicalityWorld.downedTrueEtheria)
                return true;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 8);
            recipe.AddTile(ModContent.TileType<AlchemicalInfuser>());
            recipe.Register();
        }
    }
}