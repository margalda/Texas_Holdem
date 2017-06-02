﻿using System;
using System.Web.Http;
using Server.Models;
using System.Security.Cryptography;

namespace Server.Controllers
{
    public class UserController : ApiController
    {
        // logout -->GET: api/User?username=elad&mode=logout
        //mode: logout | isloggedin
        public string Get(string username, string mode, string token)
        {
            try
            {
                Server.CheckToken(token);
                switch (mode)
                {
                    case "logout":
                        if (Server.UserFacade.Logout(username))
                        {
                            Server.GuidDic.Remove(new Guid(token));
                        }
                        break;
                    case "isloggedin":
                        Server.UserFacade.IsUserLoggedInn(username);
                        break;
                    default:
                        throw new Exception("comunication error: unkown mode");
                }
                
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
        // DeleteUser -->GET: api/User?username=elad&passwordOrRank=123456&mod=delete
        //mode: delete  | register
        public string Get(string username,string passwordOrRank ,string mode, string token)
        {
            try
            {
                switch (mode)
                {
                    case "delete":
                        Server.CheckToken(token);
                        Server.UserFacade.DeleteUser(username, passwordOrRank);
                        break;
                    case "register":
                        Server.UserFacade.Register(username, passwordOrRank);
                        break;
                    default:
                        throw new Exception("comunication error: unkown mode");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }

        public UserData Put(string userName)
        {
            var ret = new UserData();
            try
            {
                var u = Server.UserFacade.GetUser(userName);
                ret.AvatarPath = u.AvatarPath;
                ret.Chips = u.ChipsAmount;
                ret.Email = u.Email;
                ret.Password = u.Password;
                ret.Rank = u.League;
                ret.Username = u.Username;
                ret.Wins = u.Wins;
            }
            catch (Exception e)
            {
                ret.Message = e.Message;
            }

            return ret;
        }


        // login -->POST: api/User
        public UserData Post([FromBody]UserData value)
        {
            var ret = new UserData();
            try
            {
                var u = Server.UserFacade.Login(value.Username, value.Password);
                ret.AvatarPath = u.AvatarPath;
                ret.Chips = u.ChipsAmount;
                ret.Email = u.Email;
                ret.Password = u.Password;
                ret.Rank = u.League;
                ret.Username = u.Username;
                ret.Wins = u.Wins;
                Guid g = Guid.NewGuid();
                ret.token = g.ToString();
                Server.GuidDic.Add(g, new Tuple<string, DateTime>(value.Username,DateTime.Now));
            }
            catch (Exception e)
            {
                ret.Message = e.Message;
            }

            return ret;
        }
        //editUser --> POST: api/User?username=elad
        public UserData Post([FromBody]UserData value, string username, string token)
        {
            var ret = new UserData();
            try
            {
                Server.CheckToken(token);
                var u= Server.UserFacade.EditUser(username, value.Username, value.Password, value.AvatarPath, value.Email);
                if (u != null)
                {
                    ret.AvatarPath = u.AvatarPath;
                    ret.Chips = u.ChipsAmount;
                    ret.Email = u.Email;
                    ret.Password = u.Password;
                    ret.Rank = u.League;
                    ret.Username = u.Username;
                    ret.Wins = u.Wins;
                }
               
            }
            catch (Exception e)
            {
               ret.Message=e.Message;
            }

            return ret;
        }


    }
}
