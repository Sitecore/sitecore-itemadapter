using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter.Model
{
    public class GeneralLink : IGeneralLink
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public string CssClass { get; set; }

        public string Target { get; set; }
    }
}
