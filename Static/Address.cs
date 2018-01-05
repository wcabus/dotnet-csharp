namespace Static
{
    public struct Address
    {
        public Address(string street, string number, string zip, string city, string country)
        {
            Street = street;
            Number = number;
            Zip = zip;
            City = city;
            Country = country;

            CreatedAddresses++;
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public static int CreatedAddresses { get; private set; }
    }

    // This doesn't work:
    //public static struct StaticValueType
    //{

    //}
}