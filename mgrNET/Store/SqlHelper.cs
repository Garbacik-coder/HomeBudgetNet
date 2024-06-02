using System;
namespace mgrNET.Store;

public class SqlHelper<T>
{
    public string GetSqlFromEmbeddedResource(string name)
    {
        // var name2 = typeof(T).Namespace + ".Sql." + name + ".sql";
        var ss = @$"Store\MySql\Sql\{name}.sql";

        var content = File.ReadAllText(ss);

        //using var resourceStream = typeof(T).Assembly.GetManifestResourceStream(ss);
        //using var reader = new StreamReader(resourceStream!);
        return content;
    }
}