﻿using SIS.MvcFramework;
using SIS.MvcFramework.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaWebApp
{
    public class StartUp : IMvcApplication
    {
        public MvcFrameworkSettings Configure()
        {
            return new MvcFrameworkSettings();
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            
        }
    }
}
