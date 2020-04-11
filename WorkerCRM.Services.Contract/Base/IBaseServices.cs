﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerCRM.Models.Interfaces;

namespace WorkerCRM.Services.Contract.Base
{
    public interface IBaseServices<TEntity> where TEntity : class, IEntity
    {
        List<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Add(TEntity entity);

        Task<String> Delete(int id);

        void Update(TEntity entity);


    }
}
