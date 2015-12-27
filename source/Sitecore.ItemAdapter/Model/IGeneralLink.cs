using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter.Model
{
    public interface IGeneralLink
    {
        string Text { get; set; }

        string Url { get; set; }

        string CssClass { get; set; }

        string Target { get; set; }
    }
}
