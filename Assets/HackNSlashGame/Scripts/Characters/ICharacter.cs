using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface meant to be implemented by NPCs and Enemies
//makes player character interact with other characters
public interface ICharacter {

    void Interact();

    void cancel();
}