using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime,string message)
        {
            await jSRuntime.InvokeVoidAsync("Showtoastr", "success", "Success Message");
        }
        public static async ValueTask ToastrError(this IJSRuntime jSRuntime,string message)
        {
            await jSRuntime.InvokeVoidAsync("Showtoastr", "error", "Error Message");
        }
    }
}
