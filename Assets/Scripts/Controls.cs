using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls
{
    public Controls_Movement movement = new Controls_Movement();
    public Controls_Actions actions = new Controls_Actions();
    public Controls_Menu menu = new Controls_Menu();
}

public class Controls_Movement
{
    public string forward = "w";
    public string back = "s";
    public string left = "a";
    public string right = "d";
    public string jump = "space";
    public string sprint = "left shift";
    public string crouch = " left ctrl";
}

public class Controls_Actions
{
    public string interact = "mouse 0";
}

public class Controls_Menu
{
    public string pause = "escape";
    public string inventory = "e";
}
