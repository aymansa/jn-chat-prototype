using System;
using System.Collections.Generic;
using System.Text;

namespace chatproto
{
    public class Interaction
    {
        public int encounterID { get; set; }
        public bool showMessage { get; set; }
        public string SpokenWord { get; set; }
        public bool IsUser { get; set; }
        public byte[] image { get; set; }
    }
}
