﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerCRM.Models;
using WorkerCRM.Services.Contract.Dto;

namespace WorkerCRM.Services.Contract
{
    public interface ICommentService
    {
        public Task<CommentDetailDto> GetById(int id);

        public List<CommentListDto> GetAll();

        public Task<string> Delete(int id);
        public Task AddComment(Comment comment);

        public void PutComment(int id, Comment comment);

        public int GetRating(int id);
    }
}
