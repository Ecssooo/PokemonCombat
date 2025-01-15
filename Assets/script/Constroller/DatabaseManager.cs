using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public PokemonDataBase database;
    public AttackDatabase AtkDatabase;
    
    private void Start()
    {
        // database.InitData();
        // database.CreateData();

    }

    public PokemonData GetData(int id) => database.datas[id];
    public AttackData GetAttackData(int id) => AtkDatabase.AttackDatas[id];
}
