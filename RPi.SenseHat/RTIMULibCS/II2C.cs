namespace RTIMULibCS
{
    public interface II2C
    {
        byte[] Read(int length);

        void Write(byte[] data);
    }
}
