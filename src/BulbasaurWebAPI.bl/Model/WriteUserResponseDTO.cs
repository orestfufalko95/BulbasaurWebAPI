using System.Collections.Generic;

namespace BulbasaurWebAPI.bl.Model
{
    public class WriteUserResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public List<PropertyInfo> PropertyInfos { get; set; }

        public class PropertyInfo
        {
            public string PropertyName { get; set; }
            public string Message { get; set; }
        }
    }
}