using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface BaseRepository<T> where T : class
    {
        string Insert(T data);
        string Update(T data);
        string Delete(T data);
        void Save();
    }
}