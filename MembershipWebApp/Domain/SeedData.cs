using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MembershipWebApp.Interfaces;

namespace MembershipWebApp.Domain
{
    /// <summary>
    /// SeedData
    /// Use this class to generate once off or periodic regeneration of rendomized membership
    /// data for mocking or testing purposes.
    /// </summary>
    public class SeedData: ISeedData
    {
        private MembershipContext db;

        public SeedData(MembershipContext _db)
        {
            db = _db;
        }

        public void GenerateMembers(int membersize)
        {
            for (int i = 0; i < membersize; i++)
            {
                Member member = GenerateRandomMember();
                db.Add(member);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Return random integer of maxmimum size max.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandomInteger(int max)
        {
            long rndval = 0;
            string strTick = "";
            for (int i = 0; i < 3; i++)
            {
                strTick = DateTime.Now.Ticks.ToString();
                strTick = strTick.Substring(strTick.Length - 5, 5);
                rndval = (Convert.ToInt32(strTick) * DateTime.Now.Millisecond * DateTime.Now.Second) % max;
                if (rndval < max)
                    break;
            }
            return (int)Math.Abs(rndval);
        }

    public Member GenerateRandomMember()
        {
            string[] malenames = { "Andrew", "Bob", "Colin", "Dave", "Edward", "Fred", "George", "Harry", "Ian", "John", "Kevin", "Peter", "Steve", "Thomas" };

            string[] femalenames = { "Alice", "Brenda", "Carol", "Daisy", "Ellen", "Grace", "Harriot", "Jenny", "Jill", "Lily", "Mary", "Peta", "Rose", "Sally", "Wendy" };
            string[] surnames = { "Andrews", "Barnes", "Collins", "Dixon", "Elliot", "Gordon", "Hall", "Jackson", "Moore", "Norton", "Oliver", "Peters", "Smith" };
            string[] firstnames = malenames.Concat(femalenames).ToArray<string>();

            double nextval;
            string rndfirstname;
            string rndlastname;
            string rndphone;
            string rndemail;
            DateTime rnddob;

            // Generate member

            // random name.
            rndfirstname = firstnames[GetRandomInteger(firstnames.Length)];

            rndlastname = surnames[GetRandomInteger(surnames.Length)];

            // random phone.
            string rndstr1 = "";
            for (int i=0; i<5; i++) { rndstr1 = rndstr1 + Convert.ToString(GetRandomInteger(999)); }
            rndphone = rndstr1.Substring(0, 10);

            // random email
            rndemail = Path.GetRandomFileName().Replace(".", "").Substring(0,4) + "@" + 
                       Path.GetRandomFileName().Replace(".", "").Substring(0, 4) + ".com";
            
            // random DOB
            nextval = (((double)GetRandomInteger(365*40)/(365*40)) * 365 * 40);
            DateTime dtStart = new DateTime(1960, 1, 1);
            rnddob = dtStart.AddDays(nextval);

            Member newMember = new Member()
            {
                FirstName = rndfirstname,
                LastName = rndlastname,
                ContactNumber = rndphone,
                EmailAddress = rndemail,
                DateOfBirth = rnddob,
                AccountStatus = "Active",
                LastUpdated = DateTime.Today
            };
            return newMember;
        }

    }
}
