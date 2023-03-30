﻿using Mango.Web.Models;
using System;
using System.Threading.Tasks;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDTO ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
