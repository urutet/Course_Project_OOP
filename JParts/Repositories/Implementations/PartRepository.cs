﻿using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace JParts.Repositories.Implementations
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        public PartRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }
    }
}
