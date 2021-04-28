using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonComponent : MonoBehaviour {

    public Pokemon pokemon;
    public Pokemon p2;

	// Use this for initialization
	void Start () {
        pokemon = PokemonFactory.Create(2, "pikachu");
        // You can also crate random pokemons:
        p2 = PokemonFactory.CreateRandom();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
