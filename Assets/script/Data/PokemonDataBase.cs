using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Database/Pokemon", order = 0)]
public class PokemonDataBase : ScriptableObject
{
    public List<PokemonData> datas = new();

    public void InitData()
    {
        datas.RemoveAll(datas => datas.infos.number == 10);
    }
    // public void CreateData()
    // {
    //     if (!datas.Exists(data => data.infos.number == 10))
    //     {
    //         PokemonData.Infos.Type[] types = new PokemonData.Infos.Type[] { PokemonData.Infos.Type.Bug};
    //         PokemonData.Infos.Type[] faiblesse = new PokemonData.Infos.Type[] { PokemonData.Infos.Type.Fire, PokemonData.Infos.Type.Flying, PokemonData.Infos.Type.Rock};
    //         PokemonData.Infos.Type[] resistance = new PokemonData.Infos.Type[] { PokemonData.Infos.Type.Fighting, PokemonData.Infos.Type.Grass, PokemonData.Infos.Type.Ground};
    //         PokemonData.Infos infos = new(
    //             "Chenipan",
    //             10,
    //             types,
    //             "0.3m",
    //             "2.9kg",
    //             "Essaim",
    //             "Pour se prot�ger, il �met par ses antennes une odeur naus�abonde qui fait fuir ses ennemis.",
    //             faiblesse,
    //             resistance
    //             );
    //         Sprite sprite=(Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprite/Pokemon_Sprite/Chenipan.png", typeof(Sprite));
    //         PokemonData.Stats stats = new(3,2,3,2,2,3);
    //         PokemonData data = new("Chenipan", sprite, infos, stats,null);
    //         
    //         datas.Add(data);
    //     }
    //     
    // }
}