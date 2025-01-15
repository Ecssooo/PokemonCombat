using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;


public enum Type
{
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy
}


[Serializable]
public struct PokemonData
{

    public string name;
    public Sprite sprite;
    public Infos infos;
    public Stats stats;
    public List<int> attacks;

    [Serializable]
    public struct Infos
    {

        public string name;
        public int number;
        public string size;
        public string weight;
        public string ability;
        public string description;
        public Type[] types;
        public Type[] faiblesse;
        public Type[] resistance;

        

        public Infos(string name, int number, Type[] types, string size, string weight, string ability, string description, Type[] faiblesse, Type[] resistance)
        {
            this.name = name;
            this.number = number;
            this.types = types;
            this.size = size;
            this.weight = weight;
            this.ability = ability;
            this.description = description;
            this.faiblesse = faiblesse;
            this.resistance = resistance;
        }
    }

    [Serializable]
    public struct Stats
    {
        public int hp;
        public int attack;
        public int defense;
        public int specialAttack;
        public int specialDefense;
        public int speed;


        public Stats(int hp, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.specialAttack = specialAttack;
            this.specialDefense = specialDefense;
            this.speed = speed;
        }

        
    }
    
    
    public PokemonData(string name, Sprite sprite, Infos infos, Stats stats, List<int> attacks)
    {
        this.name = name;
        this.sprite = sprite;
        this.infos = infos;
        this.stats = stats;
        this.attacks = attacks;
    }

}
