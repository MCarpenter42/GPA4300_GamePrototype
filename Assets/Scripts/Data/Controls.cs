using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The point of the classes in this script is to store the various control keys, specifically
// the Unity Input Manager references for them. This allows them to be referenced in a much
// more readable manner in code (see below), but also so that, in the event that we were to
// have the time to implement it, it would be possible to have the player modify the controls
// in a settings menu.

// None of the classes or attributes in this are static, so you wouldn't be able to access
// them by default, so an instance of this class is present in another script.

// Use case example:
// Normally, you'd write something like this for handling the player moving forward:
    // if(Input.GetKey("w"))
// Using this implementation, though, you can instead write this:
    // if(Input.GetKey(controls.movement.forward))

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
    public string interact = "e";
}

public class Controls_Menu
{
    public string pause = "escape";
    public string inventory = "tab";
}

// This class exists solely for the purpose of being able
// to translate Unity's key input reference names into
// something a little more pleasing to read in a game.
public class Keynames
{
    public Dictionary<string, string> names = new Dictionary<string, string>
    {
        // LETTERS
        { "a", "A" },
        { "b", "B" },
        { "c", "C" },
        { "d", "D" },
        { "e", "E" },
        { "f", "F" },
        { "g", "G" },
        { "h", "H" },
        { "i", "I" },
        { "j", "J" },
        { "k", "K" },
        { "l", "L" },
        { "m", "M" },
        { "n", "N" },
        { "o", "O" },
        { "p", "P" },
        { "q", "Q" },
        { "r", "R" },
        { "s", "S" },
        { "t", "T" },
        { "u", "U" },
        { "v", "V" },
        { "w", "W" },
        { "x", "X" },
        { "y", "Y" },
        { "z", "Z" },

        // NUMBERS
        { "0", "0" },
        { "1", "1" },
        { "2", "2" },
        { "3", "3" },
        { "4", "4" },
        { "5", "5" },
        { "6", "6" },
        { "7", "7" },
        { "8", "8" },
        { "9", "9" },

        // SYMBOLS
        { "-", "-" },
        { "=", "=" },
        { "!", "!" },
        { "@", "@" },
        { "#", "#" },
        { "$", "$" },
        { "£", "£" },
        { "€", "€" },
        { "%", "%" },
        { "^", "^" },
        { "&", "&" },
        { "*", "*" },
        { "(", "(" },
        { ")", ")" },
        { "_", "_" },
        { "+", "+" },
        { "[", "[" },
        { "]", "]" },
        { "`", "`" },
        { "{", "{" },
        { "}", "}" },
        { "~", "~" },
        { ";", ";" },
        { "'", "'" },
        { "\\", "\\" },
        { ":", ":" },
        { "\"", "\"" },
        { "|", "|" },
        { ",", "," },
        { ".", "." },
        { "/", "/" },
        { "<", "<" },
        { ">", ">" },
        { "?", "?" },
        { "equals", "=" },
        { "euro", "€" },

        // NUMPAD
        { "[0]", "NUM 0" },
        { "[1]", "NUM 1" },
        { "[2]", "NUM 2" },
        { "[3]", "NUM 3" },
        { "[4]", "NUM 4" },
        { "[5]", "NUM 5" },
        { "[6]", "NUM 6" },
        { "[7]", "NUM 7" },
        { "[8]", "NUM 8" },
        { "[9]", "NUM 9" },
        { "[.]", "NUM POINT" },
        { "[/]", "NUM SLASH" },
        { "[*]", "NUM MULTIPLY" },
        { "[-]", "NUM MINUS" },
        { "[+]", "NUM PLUS" },

        // ARROW KEYS
        { "up", "UP ARROW" },
        { "down", "DOWN ARROW" },
        { "left", "LEFT ARROW" },
        { "right", "RIGHT ARROW" },

        // TEXT FUNCTIONS
        { "backspace", "BACKSPACE" },
        { "delete", "DEL" },
        { "tab", "TAB" },
        { "clear", "CLR" },
        { "return", "RTN" },
        { "space", "SPACEBAR" },
        { "enter", "ENTER" },
        { "insert", "INSERT" },

        // MODIFIER
        { "left shift", "L SHIFT" },
        { "right shift", "R SHIFT" },
        { "left ctrl", "L CTRL" },
        { "right ctrl", "R CTRL" },
        { "left alt", "L ALT" },
        { "right alt", "R ALT" },
        { "left cmd", "L CMD" },
        { "right cmd", "R CMD" },
        { "left super", "L SUPER" },
        { "right super", "R SUPER" },
        { "alt gr", "ALT GR" },

        // CONTROL
        { "pause", "PAUSE" },
        { "escape", "ESC" },
        { "home", "HOME" },
        { "end", "END" },
        { "page up", "PG UP" },
        { "page down", "PG DOWN" },
        { "compose", "COMPOSE" },
        { "help", "HELP" },
        { "print screen", "PRNT SCRN" },
        { "sys req", "SYS REQ" },
        { "break", "BREAK" },
        { "menu", "MENU" },
        { "power", "POWER" },
        { "undo", "UNDO" },

        // LOCK
        { "numlock", "NUM LOCK" },
        { "caps lock", "CAPS LOCK" },
        { "scroll lock", "SCROLL LOCK" },

        // FUNCTION
        { "f1", "F1" },
        { "f2", "F2" },
        { "f3", "F3" },
        { "f4", "F4" },
        { "f5", "F5" },
        { "f6", "F6" },
        { "f7", "F7" },
        { "f8", "F8" },
        { "f9", "F9" },
        { "f10", "F10" },
        { "f11", "F11" },
        { "f12", "F12" },
        { "f13", "F13" },
        { "f14", "F14" },
        { "f15", "F15" },

        // MOUSE
        { "mouse 0", "LMB" },
        { "mouse 1", "RMB" },
        { "mouse 2", "MMB" },
        { "mouse 3", "MSE 4" },
        { "mouse 4", "MSE 5" },
        { "mouse 5", "MSE 6" },
        { "mouse 6", "MSE 7" },
    };
}
