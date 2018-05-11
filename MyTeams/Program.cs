using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Facet.Combinatorics;


namespace MyTeams
{
    public class Members
    {
        public int id { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public string Skill { get; set; }
        public float Points { get; set; }
        public float Credit { get; set; }
    }

    public class Team
    {
        public List<Members> teamMemberList = new List<Members>();
        public int id { get; set; }
        public float TotalPoints { get; set; }
    }
    class Program
    {
        public static List<Members> masterList = new List<Members>();
        public static List<Members> master_wicketkeeper = new List<Members>();
        public static List<Members> master_batsman = new List<Members>();
        public static List<Members> master_allrounder = new List<Members>();
        public static List<Members> master_bowler = new List<Members>();

        public static List<Team> collection_teams = new List<Team>();
        //public static void MakeAllPossibleTeams()
        //{
        //    int counter_teams = 0;



        //    foreach ( Members mb1 in masterList )
        //    {
        //        List<Members> nonMb1 = new List<Members>(masterList);
        //        //remove the primary member
        //        nonMb1.Remove(mb1);


        //        for(int start = 0; start < nonMb1.Count-10; start++ )
        //        {

        //            Team team = new Team();

        //            // take 10 members and add mb1
        //            team.teamMemberList.Add(mb1);

        //            for (int i = start; i < start+10; i++)
        //            {
        //                team.teamMemberList.Add(nonMb1[i]);

        //            }
        //            team.id = counter_teams++;
        //            collection_teams.Add(team);
        //        }

        //    }

        //}

        //public static List<Team> ApplyFilter_100Credits()
        //{
        //    List<Team> ls_valid_credits = new List<Team>();
        //    foreach( Team team in collection_teams )
        //    {
        //        float sum = 0;
        //        for(int i =0;i<team.teamMemberList.Count; ++i )
        //        {
        //            Members m = team.teamMemberList[i];
        //            sum += m.Credit;
        //        }
        //        if (sum <= 100.0f)
        //            ls_valid_credits.Add(team);

        //    }
        //    return ls_valid_credits;
        //}

        //public static List<Team> ApplyFilter_1W( List<Team> ls_valid   )
        //{
        //    List<Team> ls_validOut = new List<Team>();
        //    foreach (Team team in ls_valid)
        //    {
        //        Members[] mem = team.teamMemberList.FindAll(t => t.Skill == "W").ToArray();

        //        if (mem.Length < 2 )
        //            ls_validOut.Add(team);

        //    }
        //    return ls_validOut;
        //}

        //public static List<Team> ApplyFilter_1W_3_5B(List<Team> ls_valid)
        //{
        //    List<Team> ls_validOut = new List<Team>();
        //    foreach (Team team in ls_valid)
        //    {
        //        Members[] mem = team.teamMemberList.FindAll(t => t.Skill == "B").ToArray();

        //        if (mem.Length >= 3 && mem.Length <= 5 )
        //            ls_validOut.Add(team);

        //    }
        //    return ls_validOut;
        //}
        //public static List<Team> ApplyFilter_0_7P(List<Team> ls_valid)
        //{
        //    List<Team> ls_validOut = new List<Team>();
        //    foreach (Team team in ls_valid)
        //    {
        //        Members[] mem = team.teamMemberList.FindAll(t => t.TeamName == "HYD").ToArray();

        //        if (mem.Length >= 4 && mem.Length <= 7)
        //            ls_validOut.Add(team);

        //    }
        //    return ls_validOut;
        //}

        public static List<Team> GetCombinationList(List<Members> lsInput, int k)
        {
            List<Team> lsOut = new List<Team>();
            int counter_teams = 0;

            //char[] inputSet = { 'A', 'B', 'C', 'D' };

            //Combinations<char> combinations = new Combinations<char>(inputSet, 3);
            //string cformat = "Combinations of {{A B C D}} choose 3: size = {0}";
            //Console.WriteLine(String.Format(cformat, combinations.Count));
            //foreach (IList<char> c in combinations)
            //{
            //    Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0], c[1], c[2]));
            //}

            Combinations<Members> combinations = new Combinations<Members>(lsInput, k);

            //string cformat = "Combinations of {{A B C D}} choose 3: size = {0}";
            //Console.WriteLine(String.Format(cformat, combinations.Count));
            foreach (IList<Members> c in combinations)
            {
                //Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0].id, c[1].id, c[2].id));
                Team team = new Team();


                team.id = counter_teams++;
                for (int cnt = 0; cnt < k; cnt++)
                    team.teamMemberList.Add(c[cnt]);

                lsOut.Add(team);
            }

            return lsOut;

        }
        static void OptimizedDatabase( )
        {
            int before = master_wicketkeeper.Count + master_batsman.Count + master_allrounder.Count + master_bowler.Count;
            //remove all members with 0 points - means they have not played this tournament so far
            List<Members> lost_players_coll = master_wicketkeeper.FindAll(s => s.Points == 0).ToList();
            master_wicketkeeper.RemoveAll(s => s.Points == 0);

            lost_players_coll.AddRange(  master_batsman.FindAll(s => s.Points == 0).ToList());
            master_batsman.RemoveAll(s => s.Points == 0);

            lost_players_coll.AddRange(master_allrounder.FindAll(s => s.Points == 0).ToList());
            master_allrounder.RemoveAll(s => s.Points == 0);

            lost_players_coll.AddRange(master_bowler.FindAll(s => s.Points == 0).ToList());
            master_bowler.RemoveAll(s => s.Points == 0);

            int after = master_wicketkeeper.Count + master_batsman.Count + master_allrounder.Count + master_bowler.Count;

            Console.WriteLine("\nDataBase Optimized \"ON\" _Lost Player : {0}", before - after );
            foreach( Members member in lost_players_coll )
            {
                Console.WriteLine(" {0}, {1}", member.Name, member.TeamName);
            }

        }
        static void Main(string[] args)
        {

            var currentDirectory = System.IO.Directory.GetCurrentDirectory();

            string InputFileName = args[0];
            string OutputFileName = InputFileName+".d11";
            String InputFilePath = currentDirectory + "\\" + InputFileName;
            String OutputFilePath = currentDirectory + "\\" + OutputFileName;
            
            using (StreamReader st = new StreamReader(InputFilePath))
            {
                String data = st.ReadToEnd();
                string sep = "\n";
                String[] collection = data.Split(sep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("\nScanning teams details Intput file Path: {0}", InputFilePath);

                int id_counter = 0;
                foreach (string line in collection)
                {
                    string[] id = line.Split(',');
                    Members member = new Members();
                    member.id = id_counter++;
                    member.TeamName = id[0];
                    member.Name = id[1];
                    member.Skill = id[2];
                    member.Points = float.Parse(id[3]);
                    member.Credit = float.Parse(id[4]);
                    switch (member.Skill)
                    {
                        case "W":
                        case "w":
                            master_wicketkeeper.Add(member);
                            break;
                        case "B":
                        case "b":
                            master_batsman.Add(member);
                            break;
                        case "AR":
                        case "ar":
                            master_allrounder.Add(member);
                            break;
                        case "O":
                        case "o":
                            master_bowler.Add(member);
                            break;

                        default:

                            break;
                    }
                    masterList.Add(member);
                }
            } // end stream builder


            Console.WriteLine("\nGenerating DataBase in progress...");
            OptimizedDatabase();

            List<Team> lsteam_mW = GetAllCombinationList(master_wicketkeeper, 1, 1);

            List<Team> lsteam_mB = GetAllCombinationList(master_batsman, 3, 5);

            List<Team> lsteam_mAR = GetAllCombinationList(master_allrounder, 1, 3);


            List<Team> lsteam_mBOW = GetAllCombinationList(master_bowler, 3, 5);

            Console.WriteLine("\nGenerated DataBase, Analysis via BruteForce in progress...[SitBack & Relax] come back afer 10 minutes.");

            List<Team> ls_LargestSet = MakeAllCollectionsLargestSet(lsteam_mW, lsteam_mB, lsteam_mAR, lsteam_mBOW);

            // now make all possible combinations of 11 members
            /* 
             * filters
             * -- Credit sum <= 100 
             *  1 W
             *  3,4,5 B
             *  1,2,3 AR 
             *  3,4,5 O
             *  0-7 players of 1 team                        
            */

            var secondFiveItems = ls_LargestSet.OrderByDescending(s => s.TotalPoints).Take(100).ToList();

            Console.WriteLine("\nAnalysis Completed. Writing Top-100 Teams details in file.");

            using (StreamWriter writer = new StreamWriter(OutputFilePath, true))
            {
                writer.AutoFlush = true;
                foreach (Team team in secondFiveItems)
                {
                    string st = team.id.ToString() + ",";
                    foreach (Members m in team.teamMemberList)
                    {
                        st += m.Name;
                        st += ", ";
                    }
                    st += "\n";
                    foreach (Members m in team.teamMemberList)
                    {
                        st += m.Points;
                        st += ", ";
                    }
                    st += team.teamMemberList.Sum(s=>s.Points).ToString() +"\n";
                    foreach (Members m in team.teamMemberList)
                    {
                        st += m.Credit;
                        st += ", ";
                    }
                    st += team.teamMemberList.Sum(s => s.Credit).ToString() + "\n";
                    writer.WriteLine(st);

                    writer.WriteLine("\n");

                }
                writer.Flush();
            }


            Console.WriteLine("\nTop-100 teams created ___ output file Path: {0}", OutputFilePath);
        }

        private static List<Team> GetAllCombinationList(List<Members> master_batsman, int min, int max)
        {
            List<Team> lsOutTeams = new List<Team>();
            for (int i = min; i <= max; ++i)
            {
                lsOutTeams.AddRange(GetCombinationList(master_batsman, i).ToList());
            }

            return lsOutTeams;
        }


        private static List<Team> MakeAllCollectionsLargestSet(List<Team> lsW, List<Team> lsB, List<Team> lsAR, List<Team> lsBow)
        {
            List<Team> lsOutTeams = new List<Team>();
            List<Members> TempLs = new List<Members>();

            int counter = 0;
            foreach (Team tW in lsW)
            {
                int twsz = tW.teamMemberList.Count;
                foreach (Team tbat in lsB)
                {
                    int tbsz = tbat.teamMemberList.Count;


                    foreach (Team tAR in lsAR)
                    {
                        int tarsz = tAR.teamMemberList.Count;

                        foreach (Team tBow in lsBow)
                        {
                            TempLs.Clear();

                            int tbowsz = tBow.teamMemberList.Count;
                            int tCount = twsz + tbsz + tarsz + tbowsz;
                            float tCredit = tW.teamMemberList.Sum(t => t.Credit) +
                                         tbat.teamMemberList.Sum(t => t.Credit) +
                                         tAR.teamMemberList.Sum(t => t.Credit) +
                                         tBow.teamMemberList.Sum(t => t.Credit);




                            if (tCount != 11 || tCredit > 100.0f)
                                continue;

                            TempLs.AddRange(tW.teamMemberList);
                            TempLs.AddRange(tbat.teamMemberList);
                            TempLs.AddRange(tAR.teamMemberList);
                            TempLs.AddRange(tBow.teamMemberList);
                            Members[] memHYD = TempLs.FindAll(s => s.TeamName == "HYD").ToArray();
                            Members[] memDEL = TempLs.FindAll(s => s.TeamName == "DEL").ToArray();

                            if (memHYD.Length > 7 || memDEL.Length > 7)
                                continue;

                            Team tFinal = new Team();

                            tFinal.id = counter++;
                            tFinal.TotalPoints = TempLs.Sum(t => t.Points);

                            tFinal.teamMemberList.AddRange(TempLs);


                            lsOutTeams.Add(tFinal);
                            //if( counter % 100000 == 0 )
                            //    lsOutTeams = lsOutTeams.OrderByDescending(s => s.TotalPoints).ToList();


                        }//end bowler

                    } //end all rounder

                } //end batsman

            } //end wicket keeper


            return lsOutTeams;
        }


    }
}
