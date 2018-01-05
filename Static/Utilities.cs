namespace Static
{
    public static class Utilities
    {
        public static Address CreateAddress(params string[] fields)
        {
            return new Address(fields[0], fields[1], fields[2], fields[3], fields[4]);
        }
    }
}