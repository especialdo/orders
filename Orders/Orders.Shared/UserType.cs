﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,
        [Description("Usuario")]
        User
    }
}
