using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlarme.src.AppAlarme.Core.Models
{
    internal class Alarm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public bool RepeatDaily { get; set; }
        public bool RepeatWeekly { get; set; }
    }
}
