using BTD_Mod_Helper.Api.Towers;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Utils;
using Il2CppSystem.IO;
using MelonLoader;
using Octokit;
using static Il2CppTMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Il2CppAssets.Scripts.Data.Quests;
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons.Behaviors;
using static Il2CppSystem.TypeIdentifiers;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using System.Collections.Immutable;
using Harmony;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using static Il2CppAssets.Scripts.Unity.Utils.NkLibrary;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Unity.Towers.Weapons;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.ServerEvents;
using ExtraTowers;
using MonkeyFortressMonkey;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;

namespace weapondisplays
{
    public class MonkeyFortressDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "BananaDisplay");
        }
    }
}
namespace ExtraTowers
{


    public class MonkeyFortress : ModTower
    {

        public override string Name => "Monkey Fortress";
        public override string DisplayName => "Monkey Fortress";
        public override string Description => "The Monkey Fortress can summon a temporary army of multiple Monkey. He also have a machine gun on top of the fortress.";
        public override string BaseTower => "MonkeyVillage";
        public override int Cost => 1800;
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;
        //public override ParagonMode ParagonMode => ParagonMode.Base555;



        public override Il2CppAssets.Scripts.Models.TowerSets.TowerSet TowerSet => Il2CppAssets.Scripts.Models.TowerSets.TowerSet.Military;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.RemoveBehavior<RangeSupportModel>();
            var machineGun = Game.instance.model.GetTowerFromId("SniperMonkey").GetAttackModel().Duplicate();
            machineGun.weapons[0].rate = 0.9f;
            machineGun.GetDescendant<RotateToTargetModel>().rotateTower = false;
            machineGun.weapons[0].projectile.GetDamageModel().damage = 1;
            machineGun.weapons[0].animation = -200;
            machineGun.name = "MachineGun";
            towerModel.AddBehavior(machineGun);

            towerModel.range = 48f;
            var dart = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel(1).Duplicate();
            dart.range = towerModel.range;
            dart.name = "Dart_Weapon";
            dart.weapons[0].Rate = 16f;
            dart.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            dart.weapons[0].projectile.ApplyDisplay<weapondisplays.MonkeyFortressDisplay>();
            dart.GetDescendant<RotateToTargetModel>().rotateTower = false;
            dart.weapons[0].projectile.AddBehavior(new CreateTowerModel("DartTower", GetTowerModel<DartMonkey1>().Duplicate(), 0f, true, false, false, true, true));
            towerModel.AddBehavior(dart);
            var sniper = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel(1).Duplicate();
            sniper.range = towerModel.range;
            sniper.name = "Sniper_Weapon";
            sniper.weapons[0].Rate = 20f;
            sniper.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            sniper.weapons[0].projectile.ApplyDisplay<weapondisplays.MonkeyFortressDisplay>();
            sniper.GetDescendant<RotateToTargetModel>().rotateTower = false;
            sniper.weapons[0].projectile.AddBehavior(new CreateTowerModel("SniperTower", GetTowerModel<SniperMonkey1>().Duplicate(), 0f, true, false, false, true, true));
            towerModel.AddBehavior(sniper);


        }


        public override string Portrait => "Portrait000MonkeyFortress";

        public override string Icon => "Icon000MonkeyFortress";
    }

    public class NinjaDefender : ModUpgrade<MonkeyFortress>
    {
        public override string Name => "Ninja Defender";
        public override string DisplayName => "Ninja Defender";
        public override string Description => "Send Ninja Monkeys to the battle field";
        public override int Cost => 1100;
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var ninja = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel(1).Duplicate();
            ninja.range = towerModel.range;
            ninja.name = "Ninja_Weapon";
            ninja.weapons[0].Rate = 12f;
            ninja.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            ninja.weapons[0].projectile.ApplyDisplay<weapondisplays.MonkeyFortressDisplay>();
            ninja.GetDescendant<RotateToTargetModel>().rotateTower = false;
            ninja.weapons[0].projectile.AddBehavior(new CreateTowerModel("NinjaTower", GetTowerModel<NinjaMonkey1>().Duplicate(), 0f, true, false, false, true, true));
            towerModel.AddBehavior(ninja);

            towerModel.display = new PrefabReference() { guidRef = "eaba5748b1f5ce245b0f257fb3a021a6" };
            towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "eaba5748b1f5ce245b0f257fb3a021a6" };
        }
        public override string Portrait => "Portrait010MonkeyFortress";

        public override string Icon => "Icon010MonkeyFortress";
    }
    public class StrongerBase : ModUpgrade<MonkeyFortress>
    {
        public override string Name => "Stronger Base";
        public override string DisplayName => "Stronger Base";
        public override string Description => "The Machine Gun get stronger and send an army faster than before";
        public override int Cost => 2200;
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel("MachineGun");
            attackModel.weapons[0].rate *= 0.82f;
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage += 1;
            towerModel.display = new PrefabReference() { guidRef = "1c62e8da4341499459459a9e696e9511" };
            towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "1c62e8da4341499459459a9e696e9511" };
            foreach (var attacks in towerModel.GetAttackModels())
            {
                if (attacks.name.Contains("Weapon"))
                {
                    attacks.weapons[0].Rate *= 0.78f;
                }

            }

        }
        public override string Portrait => "Portrait020MonkeyFortress";

        public override string Icon => "Icon020MonkeyFortress";
    }
    public class BattlefieldSupport : ModUpgrade<MonkeyFortress>
    {
        public override string Name => "Battlefield Support";
        public override string DisplayName => "Battlefield Support";
        public override string Description => "The Battlefield Support is send into the battlefield to help the other Monkeys";
        public override int Cost => 5000;
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel("MachineGun");
            attackModel.weapons[0].rate *= 0.88f;
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage += 1;
            var alch = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel(1).Duplicate();
            alch.range = towerModel.range;
            alch.name = "Alch_Weapon";
            alch.weapons[0].Rate = 30f;
            alch.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            alch.weapons[0].projectile.ApplyDisplay<weapondisplays.MonkeyFortressDisplay>();
            alch.GetDescendant<RotateToTargetModel>().rotateTower = false;
            alch.weapons[0].projectile.AddBehavior(new CreateTowerModel("AlchTower", GetTowerModel<AlchMonkey1>().Duplicate(), 0f, true, false, false, true, true));
            towerModel.AddBehavior(alch);
            towerModel.display = new PrefabReference() { guidRef = "e743963171605674ab509f017c22e146" };
            towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "e743963171605674ab509f017c22e146" };
        }
        public override string Portrait => "Portrait030MonkeyFortress";

        public override string Icon => "Icon030MonkeyFortress";
    }
     public class NukeLuncher : ModUpgrade<MonkeyFortress>
     {
         public override string Name => "Nuke Luncher";
         public override string DisplayName => "Nuke Luncher";
         public override string Description => "Nuke Luncher Ability: Lunch a strong Nuke to the strongest Bloon on the map with a huge area of affect. All the attacking Monkey from the Fortress are stronger";
         public override int Cost => 13500;
         public override int Path => MIDDLE;
         public override int Tier => 4;
         public override void ApplyUpgrade(TowerModel towerModel)
         {
             var attackModel = towerModel.GetAttackModel("MachineGun");
             attackModel.weapons[0].rate *= 0.79f;
             var projectile = attackModel.weapons[0].projectile;
             projectile.GetDamageModel().damage += 2;

             var nuke = Game.instance.model.GetTowerFromId("MonkeySub-040").GetAbility().Duplicate();
             nuke.GetBehavior<ActivateAttackModel>().GetDescendant<DamageModel>().damage = 2500;
             towerModel.AddBehavior(nuke);
            towerModel.display = new PrefabReference() { guidRef = "0cd30cfd4f8eae147b05bb530b697224" };
            towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "0cd30cfd4f8eae147b05bb530b697224" };
            foreach (var attacks in towerModel.GetAttackModels())
             {
                 if (attacks.name.Contains("Dart"))
                 {
                     attacks.weapons[0].projectile.GetBehavior<CreateTowerModel>().tower = GetTowerModel<DartMonkey2>().Duplicate();
                 }
                 if (attacks.name.Contains("Sniper"))
                 {
                     attacks.weapons[0].projectile.GetBehavior<CreateTowerModel>().tower = GetTowerModel<SniperMonkey2>().Duplicate();
                 }
                 if (attacks.name.Contains("Ninja"))
                 {
                     attacks.weapons[0].projectile.GetBehavior<CreateTowerModel>().tower = GetTowerModel<NinjaMonkey2>().Duplicate();
                 } 
                 if (attacks.name.Contains("Weapon"))
                 {
                     attacks.weapons[0].rate *= 0.93f;
                 }
             } 

         }
        public override string Portrait => "Portrait040MonkeyFortress";

        public override string Icon => "Icon040MonkeyFortress";
    }
    public class PerfectDarkDefence : ModUpgrade<MonkeyFortress>
    {
        public override string Name => "Perfect Dark Defence";
        public override string DisplayName => "Perfect Dark Defence";
        public override string Description => "Send the strongest of them all...";
        public override int Cost => 120000;
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel("MachineGun");
            attackModel.weapons[0].rate *= 0.45f;
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage += 6;

            var nuke = towerModel.GetAbility();
            nuke.GetBehavior<ActivateAttackModel>().GetDescendant<DamageModel>().damage = 6250;
            foreach (var attacks in towerModel.GetAttackModels())
            {
              
                if (attacks.name.Contains("Weapon"))
                {
                    attacks.weapons[0].rate *= 0.88f;
                }
            }
            towerModel.display = new PrefabReference() { guidRef = "06b880ab7e2941b4f9de3e132ba1e11e" };
            towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "06b880ab7e2941b4f9de3e132ba1e11e" };
            var super = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel(1).Duplicate();
            super.range = towerModel.range;
            super.name = "Super_Weapon";
            super.weapons[0].Rate = 45f;
            super.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            super.weapons[0].projectile.ApplyDisplay<weapondisplays.MonkeyFortressDisplay>();
            super.GetDescendant<RotateToTargetModel>().rotateTower = false;
            super.weapons[0].projectile.AddBehavior(new CreateTowerModel("SuperTower", GetTowerModel<SuperMonkey1>().Duplicate(), 0f, true, false, false, true, true));
            towerModel.AddBehavior(super);
        }
        public override string Portrait => "Portrait050MonkeyFortress";

        public override string Icon => "Icon050MonkeyFortress";
    }
}
namespace MonkeyFortressMonkey
{
    public class DartMonkey1 : ModTower
    {
        public override string Portrait => "000-DartMonkey";
        public override string Name => "Dart Monkey";
        public override TowerSet TowerSet => TowerSet.Primary;
        public override string BaseTower => TowerType.DartMonkey;

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Dart Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 40f, 3, false, false));
            weapons.rate *= 0.9f;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay1 : ModTowerDisplay<DartMonkey1>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.DartMonkey, 0, 0, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    }
    public class DartMonkey2 : ModTower
    {
        public override string Portrait => "030-DartMonkey";
        public override string Name => "Dart Monkey2";
        public override TowerSet TowerSet => TowerSet.Primary;
        public override string BaseTower => TowerType.DartMonkey + "-032";

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Dart Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
            weapons.rate *= 0.85f;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay5 : ModTowerDisplay<DartMonkey2>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.DartMonkey, 0, 3, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    } 
    public class SniperMonkey1 : ModTower
    {
        public override string Portrait => "000-SniperMonkey";
        public override string Name => "Snipe Monkey";
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.SniperMonkey;

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Sniper Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 43f, 3, false, false));
            weapons.rate *= 0.85f;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay2 : ModTowerDisplay<SniperMonkey1>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.SniperMonkey, 0, 0, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    }
    public class SniperMonkey2 : ModTower
    {
        public override string Portrait => "030-SniperMonkey";
        public override string Name => "Sniper Monkey2";
        public override TowerSet TowerSet => TowerSet.Military;
        public override string BaseTower => TowerType.SniperMonkey + "-230";

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Sniper Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 48f, 3, false, false));
            weapons.rate *= 0.83f;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay6 : ModTowerDisplay<SniperMonkey2>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.SniperMonkey, 0, 3, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    } 
    public class NinjaMonkey1 : ModTower
    {
        public override string Portrait => "000-NinjaMonkey";
        public override string Name => "Ninja Monkey";
        public override TowerSet TowerSet => TowerSet.Magic;
        public override string BaseTower => TowerType.NinjaMonkey;

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Ninja Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
            weapons.rate *= 0.8f;
            weapons.projectile.pierce += 1;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay3 : ModTowerDisplay<NinjaMonkey1>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.NinjaMonkey, 0, 0, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    }
    public class NinjaMonkey2 : ModTower
    {
        public override string Portrait => "300-NinjaMonkey";
        public override string Name => "Ninja Monkey2";
        public override TowerSet TowerSet => TowerSet.Magic;
        public override string BaseTower => TowerType.NinjaMonkey + "-301";

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Ninja Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 50f, 3, false, false));
            weapons.rate *= 0.76f;
            weapons.projectile.pierce += 3;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay7 : ModTowerDisplay<NinjaMonkey2>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.NinjaMonkey, 3, 0, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    } 
    public class AlchMonkey1 : ModTower
    {
        public override string Portrait => "300-Alchemist";
        public override string Name => "Alchemist Monkey";
        public override TowerSet TowerSet => TowerSet.Magic;
        public override string BaseTower => TowerType.Alchemist + "-320";

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Alchemist Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
            weapons.rate *= 2.2f;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay4 : ModTowerDisplay<AlchMonkey1>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.Alchemist, 3, 0, 0);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    }
    public class SuperMonkey1 : ModTower
    {
        public override string Portrait => "004-SuperMonkey";
        public override string Name => "Super Monkey";
        public override TowerSet TowerSet => TowerSet.Magic;
        public override string BaseTower => TowerType.SuperMonkey + "-204";

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Super Monkey";
        public override string Description => "";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetBehavior<AttackModel>();
            var weapons = attackModel.weapons[0];
            var projectile = weapons.projectile;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 60f, 3, false, false));
            weapons.rate *= 0.65f;
            weapons.projectile.pierce *= 1.75f;
            weapons.projectile.GetDamageModel().damage *= 1.7f;
            towerModel.radius = 0;
            var Pops = Game.instance.model.GetTowerFromId("Sentry").GetBehavior<CreditPopsToParentTowerModel>().Duplicate();
            towerModel.AddBehavior(Pops);
        }
        public class MonkeyFortressDisplay8 : ModTowerDisplay<SuperMonkey1>
        {
            public override float Scale => .7f;
            public override string BaseDisplay => GetDisplay(TowerType.SuperMonkey, 2, 0, 4);

            public override bool UseForTower(int[] tiers)
            {
                return true;
            }
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {

            }
        }

    }
}


