using System;

namespace ProtoGUI.Exceptions
{
    internal class InvalidFileLocationException : Exception
    {
        public InvalidFileLocationException() : base("Select the file location!") { }
    }
}
