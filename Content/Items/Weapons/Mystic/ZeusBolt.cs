using Laugicality.Content.Projectiles.Mystic.Conjuration;
using Laugicality.Content.Projectiles.Mystic.Destruction;
using Laugicality.Content.Projectiles.Mystic.Illusion;
using Laugicality.Content.Projectiles.Special;
using Laugicality.Utilities.Base;
using Laugicality.Utilities.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Items.Weapons.Mystic
{
	public class ZeusBolt : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zeus' Bolt");
            /* Tooltip.SetDefault("''"
							+ "\nIllusion inflicts ''"
							+ "\nFires different projectiles based on Mysticism"); */
            Item.staff[Item.type] = true;
        }

        public override void SetMysticDefaults()
		{
			Item.damage = 44;
            Item.width = 64;
			Item.height = 64;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 5;
			Item.noMelee = true; 
			Item.knockBack = 2;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
            LuxCost = 10;
            VisCost = 10;
            MundusCost = 10;
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            LaugicalityPlayer modPlayer = LaugicalityPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
                if(Main.rand.Next(2) == 0)
                {
					Projectile.NewProjectile(player.GetSource_FromAI(), position.X, position.Y, speedX, speedY, ModContent.ProjectileType<ZuesBoltDestruction2>(), damage, 3f, player.whoAmI, 0f, 0f);
                }
                else
                {
                    Projectile.NewProjectile(player.GetSource_FromAI(), position.X, position.Y, speedX, speedY, ModContent.ProjectileType<ZuesBoltDestruction1>(), damage, 3f, player.whoAmI, 0f, 0f);
                }
            }
            if (modPlayer.MysticMode == 3)
            {
                
            }
            return true;
        }

        public override void Destruction(LaugicalityPlayer modPlayer)
        {
            Item.damage = 72;
            Item.useTime = 15;
            Item.useAnimation = (int)(Item.useTime);
            Item.knockBack = 0;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.UseSound = SoundID.Item1;
            Item.scale = 1.5f;
        }

        public override void Illusion(LaugicalityPlayer modPlayer)
        {
            Item.damage = 42;
            Item.useTime = 32;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<ZuesBoltIllusion1>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
        }

        public override void Conjuration(LaugicalityPlayer modPlayer)
        {
            Item.damage = 44;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<HallowsEveConjuration1>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1006, 16); //Chlorophyte
            recipe.AddIngredient(mod, nameof(SoulOfSought), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}