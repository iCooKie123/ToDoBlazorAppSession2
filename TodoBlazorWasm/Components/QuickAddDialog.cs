using Microsoft.AspNetCore.Components;
using Todo.Shared;
using System.Net.Http.Json;

namespace TodoBlazorWasm.Components
{
    public partial class QuickAddDialog
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
 
        public TodoItemModel TodoItem { get; set; } = new TodoItemModel();
        protected bool ShowModal { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        public void Close()
        {
            ShowModal = false;
        }
 
        public void Show()
        {
            ShowModal = true;
        }
 
        public void OnCancelClicked()
        {
            Close();
        }
 
        public async void OnOkClicked()
        {
            await HttpClient.PostAsJsonAsync($"/todos", TodoItem);
            Close();
            TodoItem = new TodoItemModel(); // create a new instance of TodoItem
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
