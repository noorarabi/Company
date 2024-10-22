namespace Company.Models
{
    public class Comment
    {
        public int CommentId { get; set; } 
        public string Name { get; set; }
        public string CommentText { get; set; } 
        public DateTime Creation {  get; set; } 
        public byte[] img { get; set; }
    }
}
