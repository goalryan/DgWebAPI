﻿using System;
using System.Collections.Generic;
using DgWebAPI.Model;
namespace DgWebAPI.Service
{
    public interface IPositionRepository
    {
        List<Position> GetAll(Passport passport);
    }
}
