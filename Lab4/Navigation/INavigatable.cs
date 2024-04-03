using System;

namespace KMA.ProgrammingInCSharp.Navigation
{
    internal interface INavigatable<TObject> where TObject : Enum
    {
        TObject ViewType
        {
            get;
        }
    }
}