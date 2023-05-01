using Dapper;
using System.Data.SqlClient;
using TodoListASP.ClassHelper;
using TodoListASP.Models;

namespace TodoListASP.ViewModels
{
    public class TodoListViewModel
    {
        public TodoListViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new TodoListItem();
                this.TodoListItems = db.Query<TodoListItem>("SELECT * FROM TodoListItems ORDER BY AddDate DESC").ToList();
            }
        }

        public List<TodoListItem> TodoListItems { get; set; }
        public TodoListItem EditableItem { get; set; }
    }
}
