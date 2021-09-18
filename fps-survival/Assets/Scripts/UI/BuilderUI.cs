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

    public void SetBuild(int buildIDX)
    {
        BuildManager.instance.BuildBuilding(builder.inventory, builder.player,builder.buildPos[buildIDX], builder.buildings[buildIDX], builder.alternativeInventory);
    }
}
