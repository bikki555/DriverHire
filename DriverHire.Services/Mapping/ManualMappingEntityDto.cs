using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Mapping
{
    public static class ManualMappingEntityDto
    {
       
       
        public static DriverForm ToEntity(this DriverFormPostDto dTO)
            => dTO is null
                ? null
                : new DriverForm
                {
                    Name=dTO.Name,
                    DateOfBirth=dTO.DateOfBirth,
                    VehicleType=dTO.VehicleType,
                    Rate=dTO.Rate,
                    Shift=dTO.Shift
    };
        
    }
}
