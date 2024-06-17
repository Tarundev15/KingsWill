//namespace CRUD_Project.Models
//{
//    public class TodoItemDto
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public bool IsComplete { get; set; }
//    }
//}

using System.Collections.Generic;

namespace CRUD_Project.Models
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public List<AssociationDataDto> AssociationData { get; set; }  // New field
    }
}

