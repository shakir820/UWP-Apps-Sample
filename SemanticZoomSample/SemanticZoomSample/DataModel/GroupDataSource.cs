using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticZoomSample.DataModel
{
    public class GroupDataSource
    {
        public static IEnumerable<GroupData> GetGroupData()
        {
            List<Contact> Contacts = new List<Contact>();
            Contacts.Add(new Contact("Shakir", "01670074271", @"ms-appx:///Assets/icons/male.png"));
            Contacts.Add(new Contact("Saima", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Shimi", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Ritu", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Raequa", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Rima", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Rumi", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Belal", "01670074271", @"ms-appx:///Assets/icons/male.png"));
            Contacts.Add(new Contact("Bejoy", "01670074271", @"ms-appx:///Assets/icons/male.png"));
            Contacts.Add(new Contact("Anamika", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Amina", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Rahul", "01670074271", @"ms-appx:///Assets/icons/male.png"));
            Contacts.Add(new Contact("Rudro", "01670074271", @"ms-appx:///Assets/icons/male.png"));
            Contacts.Add(new Contact("Rajon", "01670074271", @"ms-appx:///Assets/icons/male.png"));
            Contacts.Add(new Contact("Shipa", "01670074271", @"ms-appx:///Assets/icons/female.png"));
            Contacts.Add(new Contact("Mohon", "01670074271", @"ms-appx:///Assets/icons/male.png"));

            var gpData = Contacts.GroupBy(a => a.Name[0], (key, con) => new GroupData() { HeaderTitle = key, Items = con.ToList() });

            return gpData;
        }
    }
}
