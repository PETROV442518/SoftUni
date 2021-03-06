﻿using System;
using System.Xml.Serialization;

namespace Cinema.DataProcessor
{
    [XmlType("Customer")]
    public class ExportTopCustomerDto
    {
        [XmlAttribute("FirstName")]
        public string FirstName { get; set; }
        [XmlAttribute("LastName")]
        public string LastName { get; set; }
        [XmlElement("SpentMoney")]
        public string SpentMoney { get; set; }
        [XmlElement("SpentTime")]
        public string SpentTime { get; set; }
    }
}