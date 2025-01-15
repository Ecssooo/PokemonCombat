using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AttackData
{
    

    public enum CATEGORIES
    {
        PHYSICS,
        PHYCHIC,
        STATUT
    }

    public int id;
    public string _name;
    public CATEGORIES categorie;
    public Type type;
    public int dgt;

    public AttackData(int id, string name, CATEGORIES categorie, Type type, int dgt)
    {
        this.id = id;
        _name = name;
        this.categorie = categorie;
        this.type = type;
        this.dgt = dgt;
    }
}
