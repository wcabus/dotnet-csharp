using System.Collections.Generic;

namespace Static
{
    public class Person
    {
        public string Name { get; set; }
        public string FirstName { get; set; }

        public Address Address { get; private set; }

        public void ChangeAddress(Address newAddress)
        {
            var oldAddress = Address;
            Address = newAddress;

            OnAddressChanged(oldAddress, newAddress);
        }

        protected virtual void OnAddressChanged(Address oldAddress, Address newAddress)
        {

        }
    }

    public class Employee : Person
    {
        public string EmployeeId { get; set; }
        public Manager Manager { get; }

        protected sealed override void OnAddressChanged(Address oldAddress, Address newAddress)
        {
            Manager.InformAbout(this, $"Address changed from {oldAddress} to {newAddress}");
        }

        //public override void ChangeAddress(Address newAddress)
        //{

        //}
    }

    public class Manager : Employee
    {
        public IReadOnlyCollection<Employee> Employees { get; } = new List<Employee>();

        public void InformAbout(Employee employee, string update)
        {

        }

        //protected override void OnAddressChanged(Address oldAddress, Address newAddress)
        //{

        //}
    }
}