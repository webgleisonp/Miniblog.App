using System.Reflection;

namespace Miniblog.App.Data;

public static class DataAssembly
{
    public static Assembly Assembly => typeof(DataAssembly).Assembly;
}
