public class FileStorageService : IFileStorageService
{
  //Lógica para leer y escribir archivos
    public string Read(string filePath)
    {
        if(!File.Exists(filePath))
        {
          //manejo de archivos inexistentes
          File.WriteAllText(filePath,"[]");
        }

        return File.ReadAllText(filePath);
    }

    public void Write(string filePath, string fileContent)
    {
        File.WriteAllText(filePath, fileContent);
    }
}

public interface IFileStorageService
{
  //Métodos para leer, y escribir en una ruta específicada
  string Read(string filePath);
  void Write(string filePath, string fileContent);
}