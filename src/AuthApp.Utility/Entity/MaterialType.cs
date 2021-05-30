using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Utility.Entity
{
    public class MaterialType
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }
    public class MaterialTypeDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }
    public class MaterialTypeToResult
    {
        public static List<MaterialType> QueryList()
        {
            List<MaterialType> materialTypes = new List<MaterialType>() {
                new MaterialType{ Id=1 ,Name="zhe",Age=56 },
                new MaterialType{ Id = 4, Name = "gg",Age=34 },
                new MaterialType{ Id = 3, Name = "hh",Age=237 }
            };
            return materialTypes;
        }
    }


}
