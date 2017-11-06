using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using Accord.MachineLearning.Rules;
using ShauliBlog.Utils;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", "mi+nombre", "en");
            string url = "https://translate.google.com/?hl=fi&ie=UTF8&text=my+name+is+mike&langpair=fi";
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string result = webClient.DownloadString(url);
            Debug.WriteLine(result);
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf(">") + 1);
            result = result.Substring(0, result.IndexOf("</span>"));
            Debug.WriteLine(result.Trim());
        }

        [TestMethod]
        public void testApriori()
        {
            string[] strings = new string[]
            {
               "", "football", "soccer", "baseball", "basketball"
            };

            SortedSet<string>[] dataset =
           {

                new SortedSet<string> { strings[1], strings[2], strings[3], strings[4] }, // bought 4 items
                 new SortedSet<string> { strings[1], strings[2], strings[4] },    // bought 3 items
                 new SortedSet<string> { strings[1], strings[2] },       // bought 2 items
                 new SortedSet<string> { strings[2], strings[3], strings[4] },    // ...
                 new SortedSet<string> { strings[2], strings[3] },
                 new SortedSet<string> { strings[3], strings[4] },
                 new SortedSet<string> { strings[2], strings[4] },

            };

            // We will use Apriori to determine the frequent item sets of this database.
            // To do this, we will say that an item set is frequent if it appears in at 
            // least 3 transactions of the database: the value 3 is the support threshold.

            // Create a new a-priori learning algorithm with support 3
            CustomApriori<string> apriori = new CustomApriori<string>(threshold: 3, confidence: 0);

            // Use the algorithm to learn a set matcher
            AssociationRuleMatcher<string> classifier = apriori.Learn(dataset);

            // Use the classifier to find orders that are similar to 
            // orders where clients have bought items 1 and 2 together:
            string[][] matches = classifier.Decide(new string[] { strings[1]/*, strings[2]*/ });

            // The result should be:
            //
            //   new int[][]
            //   {
            //       new int[] { 4 },
            //       new int[] { 3 }
            //   };

            // Meaning the most likely product to go alongside the products
            // being bought is item 4, and the second most likely is item 3.

            // We can also obtain the association rules from frequent itemsets:
            AssociationRule<string>[] rules = classifier.Rules;

            // The result will be:
            // {
            //     [1] -> [2]; support: 3, confidence: 1, 
            //     [2] -> [1]; support: 3, confidence: 0.5, 
            //     [2] -> [3]; support: 3, confidence: 0.5, 
            //     [3] -> [2]; support: 3, confidence: 0.75, 
            //     [2] -> [4]; support: 4, confidence: 0.66, 
            //     [4] -> [2]; support: 4, confidence: 0.8, 
            //     [3] -> [4]; support: 3, confidence: 0.75, 
            //     [4] -> [3]; support: 3, confidence: 0.6 
            // };
        }

        [TestMethod]
        public void testAdvancedApriori()
        {
            string firstArticleRaw = @" NBA After losing Gordon Hayward to free agency during the offseason the Utah Jazz were counting on younger players to fill the void. One of them may need to wait before stepping up to do so. Fourth - year guard Dante Exum fell heavily to the floor during Friday’s home game against Phoenix, immediately leaving the game and headed straight to the locker room.After Utah’s 112 - 101 victory, Jazz head coach Quin Snyder admitted that Exum’s injury “didn’t look good.” Exum missed the entire 2015 - 16 campaign after undergoing ACL surgery on his left knee.He appeared primed for a breakout season, however, after averaging 20.0 points, 6.3 assists and 4.3 rebounds per game during the 2017 NBA Summer League.";

            string[] firstArticle = sanitizeWords(firstArticleRaw).Split(' ');

            string secondsArticleRaw = @"NBA NBA Chicago is already waiting on one of its offseason acquisitions (Zach LaVine) to return from injury. They might have to wait for another. Kris Dunn suffered an open dislocation of his left finger during Friday’s 114-101 win over Milwaukee, the team announced. The second-year point guard could miss the Bulls’ regular season opener on October 19 as the injury will sit him down for at least a couple of weeks, says Coach Hoiberg. Sam Smith of the Bulls’ official website says the recovery could take up to a month. The injury took place when Dunn tried to contest Sterling Brown’s dunk attempt. He left the game with 11 points (5-7 FG) in 16 minutes of play. Dunn, who played behind Ricky Rubio in Minnesota after being drafted fifth overall in 2016, was hoping for a breakout season with the rebuilding Bulls after being traded during the offseason. He averaged just 17.1 minutes, 3.8 points and 2.4 assists per contest last year.";

            string[] secondArticle = sanitizeWords(secondsArticleRaw).Split(' ');

            string thirdArticleRaw = @"NBA NBA As one individual once told me their investment portfolio is like an untended garden. An interesting, but accurate analogy. Without enough water and care the plants/flowers in the garden will never grow. The same can be said about your investment portfolio. Without a well executed plan it may not grow to reach your own goals (i.e.. retirement, college, second home, etc.).  Similar to growing a garden, managing an investment portfolio is a process, not a one-time decision. An effective investment strategy involves discipline, research and a defined process. Why does it matter? Without pensions and an unknown future for Social Security, your investment results will help determine how much you can spend and how long you need to work.  Therefore, blindly picking investments is also an investment strategy but not one I recommend. (For more, see: Tailoring Your Investment Plan.) Read more: What Is Your Investment Strategy? | Investopedia http://www.investopedia.com/advisor-network/articles/what-your-investment-strategy-2/#ixzz4urABYf5f Follow us: Investopedia on Facebook";

            string[] thirdArticle = sanitizeWords(thirdArticleRaw).Split(' ');

            string newDataRaw = @" NBA There is no shame in signing for the veteran’s minimum, especially if the contract in question is offered by a championship contender. Still, the lack of digits in front of the dollar sign marks just how far Derrick Rose and Dwyane Wade have fallen from their respective glory days. The former became the youngest NBA Kia Most Valuable Player in 2011 at age 22 while in Chicago. The latter was Finals MVP in 2006 and named an Eastern Conference All-Star 12 seasons in a row, all with the Miami Heat. Injuries and age have sapped much of their prime, but perhaps not as much as some might have thought. On Friday night, with LeBron James out nursing a sore ankle, Wade and Rose put on a preseason show that might be enough to hold the curtain until All-Star point guard Isaiah Thomas returns from a hip injury. The veteran duo combined for 35 points on 15-of-22 shooting in just 36 minutes. Cleveland fell to Indiana, 106-102. With Rose on the floor, the Cavaliers outscored Indiana by 17 points. The veteran point guard hit both of his three-point attempts while mixing in his usual forays to the rim, a balance which will be key when James returns for regular season action.";

            string[] newData = sanitizeWords(newDataRaw).Split(' ');

            List<string> temp = new List<string>(newData);

            bool x = temp.Contains("one");

            SortedSet<string> setA = new SortedSet<string>();

            foreach (var item in firstArticle)
            {
                setA.Add(item);
            }

            SortedSet<string> setB = new SortedSet<string>();

            foreach (var item in secondArticle)
            {
                setB.Add(item);
            }

            SortedSet<string> setC = new SortedSet<string>();

            foreach (var item in thirdArticle)
            {
                setC.Add(item);
            }

            SortedSet<string>[] dataset =
           {

                //new SortedSet<string> { firstArticle }, // bought 4 items
                //new SortedSet<string> { secondArticle }, // bought 4 items
                //new SortedSet<string> { thirdArticle }, // bought 4 items
                setA,
                setB,
                setC
            };

            // We will use Apriori to determine the frequent item sets of this database.
            // To do this, we will say that an item set is frequent if it appears in at 
            // least 3 transactions of the database: the value 3 is the support threshold.

            // Create a new a-priori learning algorithm with support 3
            CustomApriori<string> apriori = new CustomApriori<string>(threshold: 3, confidence: 0);

            // Use the algorithm to learn a set matcher
            AssociationRuleMatcher<string> classifier = apriori.Learn(dataset);

            // Use the classifier to find orders that are similar to 
            // orders where clients have bought items 1 and 2 together:
            string[][][] matches = classifier.Decide(new string[][] { newData });

            // The result should be:
            //
            //   new int[][]
            //   {
            //       new int[] { 4 },
            //       new int[] { 3 }
            //   };

            // Meaning the most likely product to go alongside the products
            // being bought is item 4, and the second most likely is item 3.

            // We can also obtain the association rules from frequent itemsets:
            AssociationRule<string>[] rules = classifier.Rules;

            // The result will be:
            // {
            //     [1] -> [2]; support: 3, confidence: 1, 
            //     [2] -> [1]; support: 3, confidence: 0.5, 
            //     [2] -> [3]; support: 3, confidence: 0.5, 
            //     [3] -> [2]; support: 3, confidence: 0.75, 
            //     [2] -> [4]; support: 4, confidence: 0.66, 
            //     [4] -> [2]; support: 4, confidence: 0.8, 
            //     [3] -> [4]; support: 3, confidence: 0.75, 
            //     [4] -> [3]; support: 3, confidence: 0.6 
            // };
        }

        private string sanitizeWords(string words)
        {
            return words.ToLower().Replace(" and ", " ").Replace(" a ", " ").Replace(" the ", " ").Replace(" for ", " ").Replace(" to ", " ").Replace(" on ", " ").Replace(" before ", " ").Replace(" after ", " ");
        }
    }
  
}
