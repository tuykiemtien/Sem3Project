﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem3Project.Entites
{
    public class ProductViewStore
    {
            public List<ProductDTO> Products { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int RecordCount { get; set; }
            public string Search { get; set; }
            public string Order { get; set; }
    }
}
