using System;
using System.Collections.Generic;
using System.Text;

namespace LangustaBobRemake
{
    class Frame
    {
        public List<string> Content { get; }
        public Frame(List<string> content)
        {
            Content = content;
        }
    }
}
