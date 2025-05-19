using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Materials;
using Laugicality.Content.Projectiles.Mystic.Conjuration;
using Laugicality.Content.Projectiles.Mystic.Destruction;
using Laugicality.Content.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Laugicality.Utilities.Players;
using Laugicality.Utilities.Base;
using Laugicality.Utilities;

namespace Laugicality.Content.Items.Weapons.Mystic
{
    public class PlutosFrost : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pluto's Frost");
            // Tooltip.SetDefault("'Harness the void of Space' \nIllusion inflicts 'Frigid', which stops enemies in their tracks\nWhile in the Etherial after defeating Etheria, +50% Overflow and Potentia Discharge and +25% Damage\nFires different projectiles based on Mysticism");
            Item.staff[Item.type] = true;
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 60;
            Item.width = 48;
            Item.height = 48;
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
            if ((LaugicalityWorld.downedEtheria || LaugicalityPlayer.Get(player).Etherable > 0) && LaugicalityWorld.downedTrueEtheria)
            {
                modPlayer.MysticDamage += .25f;
                modPlayer.GlobalAbsorbRate *= 1.5f;
                modPlayer.GlobalOverflow += .5f;
            }
            if (modPlayer.MysticMode == 2)
            {
                int numberProjectiles = Main.rand.Next(1, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    if (Main.player[Main.myPlayer] == player)
                        Projectile.NewProjectile(player.GetSource_FromThis(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<PlutoIllusion>(), damage, knockBack, player.whoAmI);
                }

            }
            return true;
        }

        public override void Destruction(LaugicalityPlayer modPlayer)
        {
            Item.damage = 100;
            Item.useTime = 16;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 6;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<PlutoDestruction>();
            LuxCost = 5;
        }

        public override void Illusion(LaugicalityPlayer modPlayer)
        {
            Item.damage = 60;
            Item.useTime = 10;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<PlutoIllusion>();
            Item.noUseGraphic = false;
            VisCost = 6;
        }

        public override void Conjuration(LaugicalityPlayer modPlayer)
        {
            Item.damage = 54;
            Item.useTime = 24;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 24f;
            Item.shoot = ModContent.ProjectileType<PlutoConjuration>();
            Item.noUseGraphic = false;
            MundusCost = 16;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Yuletide>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 15);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSought>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}