using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderUI : MonoBehaviour
{
    Builder builder;

    public void SetBuilder(Builder _builder)
    {
        builder = _builder;
    }

    public void SetBuild(int ind)
    {
        BuildManager.instance.BuildBuilding(builder.inventory, builder.player, builder.buildPos[ind], builder.buildings[ind], builder.alternativeInventory);
    }
}
