using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SheepFinance.Control
{
    public class ControlEditTransaction
    {
        LocalDatabase database;

        public ListCollectionView GetCategoryList()
        {
            database = LocalDatabase.GetInstance();

            var categories = database.GetCategories().ToList();
            List<ItemCategory> items = new List<ItemCategory>();

            foreach (var item in categories)
            {
                items.Add(new ItemCategory { Group = item.Group.Name, Name = item.Name });
            }

            ListCollectionView lcv = new ListCollectionView(items);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            return lcv;
        }

        internal void Salvar(DateTime date, object transaction)
        {
            var t = (Transaction)transaction;

            t.UpdateDate(date);

            database = LocalDatabase.GetInstance();
            database.UpdateTransaction();
        }
    }
}
