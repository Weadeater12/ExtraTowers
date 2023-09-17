using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity;
using Il2CppNinjaKiwi.LiNK;

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

namespace ExtraTowers;





public class CustomTower : ModTower
{

    public override string Name => "Custom Tower";
    public override string DisplayName => "Custom Tower";
    public override string Description => "Combine vanilla upgardes to make the ultimate tower";
    public override string BaseTower => "MonkeyAce";
    public override int Cost => 800;
    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 5;
    public override int BottomPathUpgrades => 0;
    //public override ParagonMode ParagonMode => ParagonMode.Base555;



    public override Il2CppAssets.Scripts.Models.TowerSets.TowerSet TowerSet => Il2CppAssets.Scripts.Models.TowerSets.TowerSet.Primary;

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
    }


    public override string Portrait => "Portrait000CustomMonkey";

    public override string Icon => "Portrait000CustomMonkey";
}
public class MoreTacks : ModUpgrade<CustomTower>
{
    public override string Name => "More Tacks";
    public override string DisplayName => "More Tacks";
    public override string Description => "Shoot 18 darts instead of 8";
    public override int Cost => 100;
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 18, 0, 360, null, false, false);
    }

    public override string Icon => "Icon010CustomTower";
    public override string Portrait => "Portrait000CustomMonkey";

}

public class EvenFasterFiring : ModUpgrade<CustomTower>
{
    public override string Name => "Even Faster Firings";
    public override string DisplayName => "Even Faster Firing";
    public override string Description => "Shoot even faster!";
    public override int Cost => 400;
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        towerModel.GetWeapon().rate *= 0.49f;
    }

    public override string Icon => "Icon020CustomTower";
    public override string Portrait => "Portrait000CustomMonkey";

}
public class ArcaneMastery : ModUpgrade<CustomTower>
{
    public override string Name => "Arcane Mastery";
    public override string DisplayName => "Arcane Mastery";
    public override string Description => "Faster attacks with more pierce, more damage and a seeking ability";
    public override int Cost => 1300;
    public override int Path => MIDDLE;
    public override int Tier => 3;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        towerModel.GetWeapon().rate *= 0.5f;
        towerModel.GetWeapon().projectile.GetDamageModel().damage += 1;
        towerModel.GetWeapon().projectile.pierce += 4;
        var Seek = Game.instance.model.GetTowerFromId("WizardMonkey-300").GetAttackModel().weapons[0].projectile.GetBehavior<TrackTargetModel>().Duplicate();
        Seek.distance = 1000000;
        towerModel.GetWeapon().projectile.AddBehavior(Seek);
        towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 3;
        attackModel.weapons[0].projectile.display = new PrefabReference() { guidRef = "54bcc286971344146a1cec38858b6b16" };
    }

    public override string Icon => "Icon030CustomTower";
    public override string Portrait => "Portrait000CustomMonkey";

}


public class OperationDartStrom : ModUpgrade<CustomTower>
{
    public override string Name => "Operation: Dart Strom";
    public override string DisplayName => "Operation: Dart Strom";
    public override string Description => "Shoots 34 darts per volley, and twice as fast";
    public override int Cost => 3000;
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        towerModel.GetWeapon().rate *= 0.5f;
        towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 34, 0, 360, null, false, false);
    }

    public override string Icon => "Icon040CustomTower";
    public override string Portrait => "Portrait000CustomMonkey";

}


public class MAD : ModUpgrade<CustomTower>
{
    public override string Name => "M.A.D";
    public override string DisplayName => "M.A.D";
    public override string Description => "Moab Assured Destroyer. Fire mega missiles deal extreme damage to MOAB class Bloons.";
    public override int Cost => 60000;
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
        towerModel.GetWeapon().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        towerModel.GetWeapon().projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moab", "Moab", 1, 550, false, false));
        towerModel.GetWeapon().projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Bfb", "Bfb", 1, 550, false, false));
        towerModel.GetWeapon().projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Zomg", "Zomg", 1, 550, false, false));
        towerModel.GetWeapon().projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Ddt", "Ddt", 1, 550, false, false));
        towerModel.GetWeapon().projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Bad", "Bad", 1, 550, false, false));

        var explosion = Game.instance.model.GetTowerFromId("DartlingGunner-050").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>().Duplicate();
        attackModel.weapons[0].projectile.display = new PrefabReference() { guidRef = "17d97a491cfa0154095f42ec1c5dae2d" };

        towerModel.GetWeapon().projectile.AddBehavior(explosion);
    }

    public override string Icon => "Icon050CustomTower";
    public override string Portrait => "Portrait000CustomMonkey";

}