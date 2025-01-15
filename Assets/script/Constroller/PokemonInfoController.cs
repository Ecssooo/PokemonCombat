using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PokemonInfoController : MonoBehaviour
{

    [Header("Main")]
    [SerializeField] private Image _ImgPokemon;
    [SerializeField] private Text _TxtName;
    [SerializeField] private Text _TxtNumber;
    [SerializeField] private Text _Txtsize;
    [SerializeField] private Text _TxtWeight;
    [SerializeField] private Text _TxtType;
    [SerializeField] private Text _TxtAbility;
    [SerializeField] private Text _TxtCaption;
    [SerializeField] private Text _TxtAlive;
    [SerializeField] private Text _TxtTakeDamage;
    [SerializeField] private Text _TxtHP;


    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI _StatsHP;
    [SerializeField] private TextMeshProUGUI _StatsAttack;
    [SerializeField] private TextMeshProUGUI _StatsDefense;
    [SerializeField] private TextMeshProUGUI _StatsAttackSpe;
    [SerializeField] private TextMeshProUGUI _StatsDefenseSpe;
    [SerializeField] private TextMeshProUGUI _StatsSpeed;
    public GameObject StatsWindow;

    [Header("Battle")]
    [SerializeField] private Image _BIMGPokemonPlayer;
    [SerializeField] private Image _BIMGPokemonBot;
    [SerializeField] private TextMeshProUGUI _HPPokemonPlayer;
    [SerializeField] private TextMeshProUGUI _HPPokemonBot;


    [SerializeField] private GameObject _ficheWindow;
    [SerializeField] private GameObject _battleWindow;

    [SerializeField] private DatabaseManager _databaseMgr;

    private int StatsPoints;
    
    private int _currentLife;
    public int CurrentLife { get => _currentLife; set => _currentLife = value; }

    private int _opponentCurrentLife;
    public int OpponentCurrentLife { get => _opponentCurrentLife; set => _opponentCurrentLife = value; }
    
    private PokemonData _pokemonPlayer;
    public PokemonData PokemonPlayer { get => _pokemonPlayer; set => _pokemonPlayer = value; }
    
    private PokemonData _pokemonOpponent;
    public PokemonData PokemonOpponent { get => _pokemonOpponent; set => _pokemonOpponent = value; }
    
    private int _indexPokemon = 0;
    public int IndexPokemon { get => _indexPokemon; set => _indexPokemon = value; }
    
    private void Awake()
    {
        _databaseMgr = FindObjectOfType<DatabaseManager>();

    }
    void Start()
    {
        _pokemonPlayer = _databaseMgr.GetData(_indexPokemon);
        _pokemonOpponent = _databaseMgr.GetData(9);
        InitPokemonOpponent(_pokemonOpponent);
        ChangeInfo(_pokemonPlayer, _pokemonOpponent);
    }
    
    public void InitPokemonOpponent(PokemonData PokemonOpponent)
    {
        OpponentCurrentLife = PokemonOpponent.stats.hp;
    }
    public void GetNextPokemon()
    {
        var datalenght = _databaseMgr.database.datas.Count;
        if (_indexPokemon >= datalenght - 1)
        {
            Debug.Log("Plus de pokemon disponible");
        }
        else
        {
            _indexPokemon++;
            PokemonData PokemonPlayer = _databaseMgr.GetData(_indexPokemon);
            PokemonData PokemonOpponent = _databaseMgr.GetData(9);
            ChangeInfo(PokemonPlayer, PokemonOpponent);
        }
    }

    public void GoPrevPokemon()
    {
        if (_indexPokemon <= 0)
        {
            Debug.Log("Plus de pokemon disponible");
        }
        else
        {
            _indexPokemon--;
            PokemonData PokemonPlayer = _databaseMgr.GetData(_indexPokemon);
            PokemonData PokemonOpponent = _databaseMgr.GetData(9);
            ChangeInfo(PokemonPlayer, PokemonOpponent);
        }
    }

    public void InitCurrentlife(PokemonData PokemonPlayer)
    {
        _currentLife = PokemonPlayer.stats.hp;
    }
    public void InitStatsPoints(PokemonData PokemonPlayer)
    {
        StatsPoints = PokemonPlayer.stats.hp + PokemonPlayer.stats.attack + PokemonPlayer.stats.defense;
    }

    void ChangeInfo(PokemonData PokemonPlayer, PokemonData PokemonOpponent)
    {

        InitCurrentlife(PokemonPlayer);
        InitStatsPoints(PokemonPlayer);

        _TxtName.text = PokemonPlayer.infos.name;
        _TxtNumber.text = PokemonPlayer.infos.number.ToString();
        _Txtsize.text = PokemonPlayer.infos.size;
        _TxtWeight.text = PokemonPlayer.infos.weight;
        _TxtAbility.text = PokemonPlayer.infos.ability;
        _TxtCaption.text = PokemonPlayer.infos.description;
        _ImgPokemon.sprite = PokemonPlayer.sprite;
        _TxtType.text = "";
        foreach (var type in PokemonPlayer.infos.types)
        {
            _TxtType.text += type.ToString() + " ; ";
        }
        _TxtHP.text = $"{_currentLife}/{PokemonPlayer.stats.hp}";
        _TxtTakeDamage.text = $"";


        _StatsHP.text = $"hp :{PokemonPlayer.stats.hp}";
        _StatsAttack.text = $"atk :{PokemonPlayer.stats.attack}";
        _StatsDefense.text = $"def :{PokemonPlayer.stats.defense}";
        _StatsAttackSpe.text = $"atkS :{PokemonPlayer.stats.specialAttack}";
        _StatsDefenseSpe.text = $"defS :{PokemonPlayer.stats.specialDefense}";
        _StatsSpeed.text = $"spd :{PokemonPlayer.stats.speed}";

    }

    public bool IsPokemonAlive()
    {
        if (_currentLife > 0)
        {
            _TxtAlive.text = "Le pok�mon est en vie !";
            return true;
        }
        else
        {
            _TxtAlive.text = "Le pok�mon est K.O.";
            return false;
        }
    }

    public void TakeDamage(PokemonData Pokemon, int Damage, Type type_attack)
    {
        if (Damage <= 0 || _currentLife <= 0)
        {
            Debug.Log("D�gats n�gatif ou pokemon K.O.");
        }
        else
        {
            if (checkType(Pokemon.infos.faiblesse, type_attack))
            {
                _currentLife -= Damage * 2;
                _TxtTakeDamage.text = $"Le pok�mon a pris {Damage * 2} d�gats";
                _TxtHP.text = $"{_currentLife}/{Pokemon.stats.hp}";

            }
            else if (checkType(Pokemon.infos.resistance, type_attack))
            {
                _currentLife -= Damage / 2;
                _TxtTakeDamage.text = $"Le pok�mon a pris {Damage / 2} d�gats";
                _TxtHP.text = $"{_currentLife}/{Pokemon.stats.hp}";
            }
            else
            {
                _currentLife -= Damage;
                _TxtTakeDamage.text = $"Le pok�mon a pris {Damage} d�gats";
                _TxtHP.text = $"{_currentLife}/{Pokemon.stats.hp}";
            }
            IsPokemonAlive();
        }
    }

    public void BTNStatsWindow()
    {
        //Bouton pour afficher ou cacher la fen�tre des stats
        if(StatsWindow.activeInHierarchy == false) { StatsWindow.SetActive(true); }
        else { StatsWindow.SetActive(false); }
    }

    
    public static AttackData Attaque1;
    public static AttackData Attaque2;


    [SerializeField] private TextMeshProUGUI _attack1;
    [SerializeField] private TextMeshProUGUI _attack2;

    public void BTNBattleScreen()
    {
        _pokemonPlayer = _databaseMgr.GetData(_indexPokemon);
        _ficheWindow.SetActive(false);
        _battleWindow.SetActive(true);
        _BIMGPokemonBot.sprite = _pokemonOpponent.sprite;
        _BIMGPokemonPlayer.sprite = _pokemonPlayer.sprite;
        SetAttack();
    }

    public void ExitBattleScreen()
    {
        _ficheWindow.SetActive(true);
        _battleWindow.SetActive(false);
    }

    public void SetAttack()
    {
        int index = 1;
        foreach(int idAttack in _pokemonPlayer.attacks)
        {
            
            if(index == 1)
            {
                if(idAttack > 1)
                {
                    Attaque1 = _databaseMgr.GetAttackData(idAttack-1);
                    
                    _attack1.text = Attaque1._name;
                }
                else
                {
                    Attaque1 = _databaseMgr.GetAttackData(idAttack);
                    
                    _attack1.text = Attaque1._name;
                }
                
            }
            else if(index == 2)
            {
                
                Attaque2 = _databaseMgr.GetAttackData(idAttack-1);
                _attack2.text = Attaque2._name;
            }
            else if(index > 2)
            {
                Debug.Log("soucis");
                break;
            }
            index++;
        }
    }

    public void BTNTakeDamage()
    {
        TakeDamage(_databaseMgr.GetData(_indexPokemon), 1, Type.Grass);
    }

    private bool checkType(Type[] tab, Type type_attack)
    {
        foreach (var type in tab)
        {
            if (type == type_attack)
            {
                return true;
            }
        }
        return false;
    }
}
