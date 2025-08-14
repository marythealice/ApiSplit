namespace ApiSplit.Models;
public class Perfume
{
    public uint Id { get; private set; }
    public string Name { get; private set; }
    public uint Volume { get; set; }

    public Perfume(string name, uint volume)
    {
        Name = name;
        Volume = volume;
    }
}