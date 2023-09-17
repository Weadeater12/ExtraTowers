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





public class SpikeOverlord : ModTower
{

    public override string Name => "Spike Overlord";
    public override string DisplayName => "Spike Overlord";
    public override string Description => "Generate spikes everywhere on the map at a fast rate";
    public override string BaseTower => "SpikeFactory";
    public override int Cost => 1000;
    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 5;
    public override int BottomPathUpgrades => 0;
    //public override ParagonMode ParagonMode => ParagonMode.Base555;



    public override Il2CppAssets.Scripts.Models.TowerSets.TowerSet TowerSet => Il2CppAssets.Scripts.Models.TowerSets.TowerSet.Support;

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        attackModel.range = 999999999;
        weapons.rate = 0.2f;
        projectile.pierce = 1;
        projectile.GetDamageModel().damage = 1;
        projectile.display = new PrefabReference() { guidRef = GetDisplayGUID<Projectile000SpikeOverlord>() };
        projectile.RemoveBehavior<SetSpriteFromPierceModel>();

        towerModel.ApplyDisplay<Display000SpikeOverlord>();
    }


    public override string Portrait => "Portrait000SpikeOverlord";

    public override string Icon => "Icon000SpikeOverlord";
}
public class Display000SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => GetDisplay(TowerType.SpikeFactory);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.genericRenderers)
        {
            renderer.material.mainTexture = GetTexture("SpikeOverlord000");
        }
    }
}

public class Projectile000SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => Generic2dDisplay;
    public override float Scale => 1f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "Projectile000SpikeOverlord");
    }
}
public class PiercingSpike : ModUpgrade<SpikeOverlord>
{
    public override string Name => "Piercing Spike";
    public override string DisplayName => "Piercing Spike";
    public override string Description => "Increase spikes pierce.";
    public override int Cost => 950;
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;

        projectile.pierce += 1;
        projectile.display = new PrefabReference() { guidRef = GetDisplayGUID<Projectile010SpikeOverlord>() };

        towerModel.ApplyDisplay<Display010SpikeOverlord>();

    }

    public override string Icon => "Icon010SpikeOverlord";
    public override string Portrait => "Portrait010SpikeOverlord";
}

public class Display010SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => GetDisplay(TowerType.SpikeFactory);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.genericRenderers)
        {
            renderer.material.mainTexture = GetTexture("SpikeOverlord010");
        }
    }
}
public class Projectile010SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => Generic2dDisplay;
    public override float Scale => 1f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "Projectile010SpikeOverlord");
    }
}

public class OverlordSpeed : ModUpgrade<SpikeOverlord>
{
    public override string Name => "Overlord Speed";
    public override string DisplayName => "Overlord Speed";
    public override string Description => "Shoot twice as fast than before.";
    public override int Cost => 1800;
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;

        weapons.rate /= 2;

        towerModel.ApplyDisplay<Display020SpikeOverlord>();
    }

    public override string Icon => "Icon020SpikeOverlord";
    public override string Portrait => "Portrait020SpikeOverlord";

}

public class Display020SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => GetDisplay(TowerType.SpikeFactory);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.genericRenderers)
        {
            renderer.material.mainTexture = GetTexture("SpikeOverlord020");
        }
    }
}

public class ClosestDefence : ModUpgrade<SpikeOverlord>
{
    public override string Name => "Closest Defence";
    public override string DisplayName => "Closest Defence";
    public override string Description => "Spawn better spike in a smaller range and main spikes can pop lead Bloons.";
    public override int Cost => 8500;
    public override int Path => MIDDLE;
    public override int Tier => 3;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;
        projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;

        var closestspike = Game.instance.model.GetTowerFromId("SpikeFactory-300").GetAttackModel().Duplicate();
        var closestspikeweapons = closestspike.weapons[0];
        var closestspikeprojectile = closestspikeweapons.projectile;
        closestspikeweapons.rate = 1.1f;
        closestspikeprojectile.pierce = 12;
        closestspikeprojectile.GetDamageModel().damage = 3;
        closestspikeprojectile.display = new PrefabReference() { guidRef = GetDisplayGUID<ProjectileBall030SpikeOverlord>() };
        closestspikeprojectile.RemoveBehavior<SetSpriteFromPierceModel>();
        closestspike.range = towerModel.range;
        closestspike.name = "AttackModel_ClosestSpike";


        towerModel.AddBehavior(closestspike);

        towerModel.ApplyDisplay<Display030SpikeOverlord>();
    }

    public override string Icon => "Icon030SpikeOverlord";
    public override string Portrait => "Portrait030SpikeOverlord";

}
public class Display030SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => GetDisplay(TowerType.SpikeFactory, 3, 0, 0);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.genericRenderers)
        {
            renderer.material.mainTexture = GetTexture("SpikeOverlord030");
        }
    }
}
public class ProjectileBall030SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => Generic2dDisplay;
    public override float Scale => 1f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "ProjectileBall030SpikeOverlord");
    }
}

public class OverlordPower : ModUpgrade<SpikeOverlord>
{
    public override string Name => "Overlord Power";
    public override string DisplayName => "Overlord Power";
    public override string Description => "Shoot even faster, gain more pierce and damage. Closest spikes are stronger.";
    public override int Cost => 40000;
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;

        projectile.pierce += 7;
        projectile.GetDamageModel().damage += 1;
        projectile.display = new PrefabReference() { guidRef = GetDisplayGUID<Projectile040SpikeOverlord>() };
        weapons.rate *= 0.72f;


        var closestspike = towerModel.GetAttackModel("AttackModel_ClosestSpike");
        var closestspikeweapons = closestspike.weapons[0];
        var closestspikeprojectile = closestspikeweapons.projectile;
        closestspikeweapons.rate *= 0.76f;
        closestspikeprojectile.GetDamageModel().damage += 4;
        closestspikeprojectile.pierce += 15;
        closestspike.range = towerModel.range;

        towerModel.ApplyDisplay<Display040SpikeOverlord>();
    }

    public override string Icon => "Icon040SpikeOverlord";
    public override string Portrait => "Portrait040SpikeOverlord";


}
public class Display040SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => GetDisplay(TowerType.SpikeFactory, 0, 0, 5);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.genericRenderers)
        {
            renderer.material.mainTexture = GetTexture("SpikeOverlord040");
        }
    }
}
public class Projectile040SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => Generic2dDisplay;
    public override float Scale => 1f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "Projectile040SpikeOverlord");
    }
}

public class OverlordMaster : ModUpgrade<SpikeOverlord>
{
    public override string Name => "Overlord Master";
    public override string DisplayName => "Overlord Master";
    public override string Description => "Become an overlord of spike and deal huge damage";
    public override int Cost => 250000;
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetBehavior<AttackModel>();
        var weapons = attackModel.weapons[0];
        var projectile = weapons.projectile;

        projectile.GetDamageModel().damage += 4;
        projectile.pierce += 17;
        weapons.rate /= 0.48f;

        var closestspike = towerModel.GetAttackModel("AttackModel_ClosestSpike");
        var closestspikeweapons = closestspike.weapons[0];
        var closestspikeprojectile = closestspikeweapons.projectile;
        closestspikeweapons.rate *= 0.79f;
        closestspikeprojectile.GetDamageModel().damage += 6;
        closestspikeprojectile.pierce += 25;
        closestspikeprojectile.display = new PrefabReference() { guidRef = GetDisplayGUID<ProjectileBall050SpikeOverlord>() };

        var mines = Game.instance.model.GetTowerFromId("SpikeFactory-500").GetAttackModel().Duplicate();
        var minesweapons = mines.weapons[0];
        var minesprojectile = minesweapons.projectile;
        minesweapons.rate = 2.5f;
        minesprojectile.pierce = 16;
        minesprojectile.GetDamageModel().damage = 225;
        mines.range = towerModel.range;
        minesprojectile.scale *= 1.3f;
        minesprojectile.display = new PrefabReference() { guidRef = GetDisplayGUID<ProjectileMines050SpikeOverlord>() };
        mines.name = "AttackModel_Mines";
        minesprojectile.RemoveBehavior<SetSpriteFromPierceModel>();
        towerModel.AddBehavior(mines);

        towerModel.ApplyDisplay<Display050SpikeOverlord>();
    }

    public override string Icon => "Icon050SpikeOverlord";
    public override string Portrait => "Portrait050SpikeOverlord";

}
public class Display050SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => GetDisplay(TowerType.SpikeFactory, 5, 0, 0);
    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.genericRenderers)
        {
            renderer.material.mainTexture = GetTexture("SpikeOverlord050");
        }
    }
}

public class ProjectileBall050SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => Generic2dDisplay;
    public override float Scale => 1f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "ProjectileBall050SpikeOverlord");
    }
}
public class ProjectileMines050SpikeOverlord : ModDisplay
{

    public override string BaseDisplay => Generic2dDisplay;
    public override float Scale => 1f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "ProjectileMines050SpikeOverlord");
    }
}
