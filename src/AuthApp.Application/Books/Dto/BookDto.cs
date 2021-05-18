using System;

namespace AuthApp.Books.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public Guid AuthorId { get; set; }
    }
}
