﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCR.Helper.ViewModel
{
    public class IMEIViewModel
    {
        //public int IMEIId { get; set; }
        public int ProductId { get; set; }
        public string IMEIONE { get; set; }
        public string IMEITWO { get; set; }
        public string IMEIStatus { get; set; }
        public string DeviceType { get; set; }
        public DateTime ActivationDate { get; set; }
    }
}
