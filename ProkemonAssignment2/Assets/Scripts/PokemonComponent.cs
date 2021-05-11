using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonComponent : MonoBehaviour {

    public Pokemon playerPokemon;
    Pokemon enemyPokemon;
	public Sprite[] pokeSprite;

	// Use this for initialization
	void Start () {
        playerPokemon = PokemonFactory.Create(2, "charmander"); //, pokeSprite
		// You can also crate random pokemons:
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RandomEncounter()
    {
		enemyPokemon = PokemonFactory.CreateRandom();
    }


}
