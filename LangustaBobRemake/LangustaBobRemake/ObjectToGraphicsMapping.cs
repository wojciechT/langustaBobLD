using System;
using System.Collections.Generic;
using System.Text;

namespace LangustaBobRemake
{
    class TextRepresentation
    {
        public string[] Lines = new string[8];
    }
    class ObjectToGraphicsMapping
    {
        public Dictionary<string, TextRepresentation> Mapping;
    }
}
