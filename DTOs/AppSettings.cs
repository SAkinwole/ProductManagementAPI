using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.DTOs
{
    public class AppSettings
    {
        public string SecretKey { get; set; } = default!;
    }
}
