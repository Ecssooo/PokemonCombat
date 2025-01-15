using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{

    [SerializeField] private GameObject _ficheWindow;
    [SerializeField] private GameObject _battleWindow;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _attackMenu;

    [SerializeField] private Image _imgPokemonPlayer;
    [SerializeField] private TextMeshProUGUI _hpPlayer;
    [SerializeField] private TextMeshProUGUI _namePokemonPlayer;

    [SerializeField] private Image _imgPokemonBot;
    [SerializeField] private TextMeshProUGUI _hpOpponent;
    [SerializeField] private TextMeshProUGUI _namePokemonBot;

    [SerializeField] private TextMeshProUGUI _consoleTXT;

    private int _botInitHP;
    private int _playerInitHP;

    [SerializeField] private PokemonInfoController _pokemonController;
    
    
    
    public void Start()
    { 
        PokemonData PokemonPlayer = _pokemonController.PokemonPlayer;
        InitBattle();
    }

    public void InitBattle()
    {
        _botInitHP = _pokemonController.OpponentCurrentLife;
        _hpOpponent.text = $"{_pokemonController.OpponentCurrentLife}/{_botInitHP}";
        _namePokemonBot.text = _pokemonController.PokemonOpponent.name;

        _playerInitHP = _pokemonController.CurrentLife;
        _hpPlayer.text = $"{_pokemonController.CurrentLife}/{_playerInitHP}";
        _namePokemonPlayer.text = _pokemonController.PokemonPlayer.name;
    }


    public void BTNEscape()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void BTNAttackMenu()
    {
        if(!_attackMenu.activeInHierarchy) { 
            _attackMenu.SetActive(true); 
            _mainMenu.SetActive(false);
        }
    }
    public void BTNBackMainMenu()
    {
        if (!_mainMenu.activeInHierarchy)
        {
            _mainMenu.SetActive(true);
            _attackMenu.SetActive(false);
        }
    }
    
    public void BTNBattleAttack1()
    {
        StartCoroutine(MakeDamage(PokemonInfoController.Attaque1.dgt));
        StartCoroutine(EnnemyAttack());
    }
    public void BTNBattleAttack2()
    {
        StartCoroutine(MakeDamage(PokemonInfoController.Attaque2.dgt));
        StartCoroutine(EnnemyAttack());
    }
    

    public IEnumerator MakeDamage(int dmg)
    {
        if(_pokemonController.OpponentCurrentLife - dmg <= 0)
        {
            _consoleTXT.text = "Vous avez gagne";
            yield return new WaitForSeconds(3);
            _pokemonController.ExitBattleScreen();
        }
        else
        {
            _pokemonController.OpponentCurrentLife -= dmg;
            _hpOpponent.text = $"{_pokemonController.OpponentCurrentLife}/{_botInitHP}";
        }
    }


    
    IEnumerator EnnemyAttack()
    {
        if(_pokemonController.CurrentLife <= 0 || _pokemonController.OpponentCurrentLife <= 0)
        {
            _consoleTXT.text = "Fin du combat";
        }
        else
        {
            _consoleTXT.text = "L'adversaire attaque";
            yield return new WaitForSeconds(1);
            int dmg = UnityEngine.Random.Range(1, 2);
            _pokemonController.CurrentLife -= dmg;
            _hpPlayer.text = $"{_pokemonController.CurrentLife}/{_playerInitHP}";
            _consoleTXT.text = "L'adversaire vous inflige " + dmg + " de degats";

            if(_pokemonController.CurrentLife <= 0)
            {
                _consoleTXT.text = "Vous avez perdu";
                yield return new WaitForSeconds(3);
                _pokemonController.ExitBattleScreen();
            }
        }
    }
}
