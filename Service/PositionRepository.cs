using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using DgWebAPI.DAL;
namespace DgWebAPI.Service
{
    public class PositionRepository : IPositionRepository
    {
        private PositionDal dal = new PositionDal();

        public List<Position> GetAll(Passport passport)
        {
            return CustomerDataConverter.RowToPositionList(dal.GetAll(passport));
        }
    }
}
