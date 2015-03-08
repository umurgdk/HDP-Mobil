using System;

namespace Hdp.CoreRx.Services
{
    public interface IServiceConstructor
    {
        object Construct(Type type);
    }
}

