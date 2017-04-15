﻿using System;
using System.Collections.Generic;
using TexasHoldem;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string AvatarPath { get; set; }
 //   public bool Online { get; set; }
    public List<string> Notifications { get; set; }
    public int Rank;
    public int wins;

    private List<Player> players; //change to list of Games?
    private List<Room> rooms; //do we need this?

    public User(string username, string password, string avatarPath)
    {
        wins = 0;
        Rank = 0;
        Username = username;
        Password = password;
        AvatarPath = avatarPath;
        Notifications = new List<string>();
        players = new List<Player>();
        rooms = new List<Room>();
    }

    public void AddNotification(string notif)
    {
        if (notif == "")
        {
            throw new Exception("notification is empty.");
        }

        Notifications.Add(notif);
    }

    public void RemoveNotification(string notif)
    {
        if (notif == "")
        {
            throw new Exception("notification is empty.");
        }

        Notifications.Remove(notif);
    }
}
