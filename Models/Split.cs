public class Split
{
    public uint Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public uint BottleId { get; private set; }
    public Bottle Bottle { get; private set; }
    public uint Volume { get; private set; }
    public string Status { get; private set; }

    public Split(uint bottleId, uint volume)
    {
        OrderDate = DateTime.Now;
        BottleId = bottleId;
        Volume = volume;
        Status = "Pendente";

    }
}