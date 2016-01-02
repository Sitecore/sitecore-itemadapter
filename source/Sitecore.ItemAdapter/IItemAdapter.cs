using Sitecore.Data.Items;
using Sitecore.ItemAdapter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.ItemAdapter
{

    public interface IItemAdapter
    {
        IItemAdapterModel GetModel(Item item, int depth);
    }
}
