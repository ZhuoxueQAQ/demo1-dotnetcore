using SqlSugar;

namespace demo1.Model
{
    [SugarTable("Posts")]
    public partial class Post
    {
        // 主键
        [SugarColumn(IsPrimaryKey = true)]
        public string createdDate { get; set; }
        public string title { get; set; }
        public string categories { get; set; }
        public string context { get; set; }

        public Post() {

        }
    }
}
