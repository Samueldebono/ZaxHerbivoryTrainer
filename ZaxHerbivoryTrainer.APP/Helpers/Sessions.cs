using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ZaxHerbivoryTrainer.APP.Helpers
{

    [Serializable()]
    public class Session
    {
        private static Session instance = null;
        
        private Session()
        {
        }

        private string AccessToken;
        private Role UserRole;
        private bool IsLoggedIn;
        private DateTime ExpiryTime;
        private int AccessId;

        public string _accessToken
        {
            get => AccessToken;
            set => AccessToken = value;
        }
        public Role _userRole
        {
            get => UserRole;
            set => UserRole = (Role)value;
        }        
        public bool _isLoggedin
        {
            get => IsLoggedIn;
            set => IsLoggedIn = value;
        }       
        public DateTime _expiryDate
        {
            get => ExpiryTime;
            set => ExpiryTime = value;
        }       
        public int _accessId
        {
            get => AccessId;
            set => AccessId = value;
        }

        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Session();
                }
                
                return instance;
            }
        }

        public enum Role : byte
        {
            Guest =0,
            Admin = 1
        }
    }
}
