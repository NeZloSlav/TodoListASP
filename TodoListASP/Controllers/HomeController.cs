﻿using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoListASP.ClassHelper;
using TodoListASP.Models;
using TodoListASP.ViewModels;

namespace TodoListASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoListItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if (item != null) db.Delete(item);
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<TodoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        TodoListItem dbItem = db.Get<TodoListItem>(viewModel.EditableItem.Id);
                        TryUpdateModelAsync<TodoListItem>(dbItem, "EditableItem");
                        db.Update<TodoListItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Index", new TodoListViewModel());
        }

        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if(item != null)
                {
                    item.IsDone = !item.IsDone;
                    db.Update<TodoListItem>(item);
                }

                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}