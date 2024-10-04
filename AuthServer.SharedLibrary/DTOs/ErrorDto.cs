using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.SharedLibrary.DTOs
{
    public class ErrorDto
    {
        public List<String> Errors { get; private set; } = new List<String>();
        public bool IsShow { get; private set; }
        

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            isShow = true;
        }
        public ErrorDto(List<String> errors, bool isShow)
        {
            Errors=errors;
            IsShow = isShow;
        }
    }
}
