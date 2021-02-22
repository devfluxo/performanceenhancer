using System;
using System.Collections.Generic;

namespace Booster.Misc
{
    public interface IAppEnumerator : IDisposable
    {
        void Init(List<string> apps);
        IEnumerable<uint> Enumerate();
    }
}
