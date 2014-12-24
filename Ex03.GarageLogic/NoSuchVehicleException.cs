using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class NoSuchVehicleException : Exception
    {
        private string m_LicenseNumber;

        public NoSuchVehicleException(string i_Message, string i_LicenseNumber) : base(i_Message)
        {
            m_LicenseNumber = i_LicenseNumber;
        }
    }
}
