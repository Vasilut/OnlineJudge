﻿using GeekCoding.Data.Models;
using GeekCoding.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekCoding.Repository
{
    public class SubmisionRepository : RepositoryBase<Submision>, ISubmisionRepository
    {
        public SubmisionRepository(EvaluatorContext db) : base(db)
        {

        }

        public override async Task<Submision> GetAsync(Guid id)
        {
            return await RepositoryContext.Submision.Where(x => x.SubmisionId == id).Include(x => x.Problem).FirstOrDefaultAsync();
        }

        public override Submision GetItem(Guid id)
        {
            return RepositoryContext.Submision.Where(x => x.SubmisionId == id).Include(x => x.Problem).FirstOrDefault();
        }

        public override async Task<ICollection<Submision>> GetAllAsync()
        {
            return await RepositoryContext.Submision.Include(prob => prob.Problem).ToListAsync();
        }

        public IQueryable<Submision> GetSubmisionByProblemIdAndUserName(Guid problemId, string userName)
        {
            return RepositoryContext.Submision.Include(sbm => sbm.Problem).Where(sub => sub.ProblemId == problemId &&
                                                                           sub.UserName == userName);
        }
    }
}
