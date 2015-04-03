using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;

namespace OreToManufacturing
{
    public partial class Form1 : Form
    {
        List<string> typeids = new List<string>();
        List<string> reprocessing_skills = new List<string>();
        List<SkillLevel> industry_skills = new List<SkillLevel>();
        List<Ore> ores = new List<Ore>();
        List<Mineral> minerals = new List<Mineral>();
        const float station_base = 0.57F;
        const float station_tax = 0.97F;

        public Form1()
        {
            InitializeComponent();
        }

        /**********************************************************************
         * 
         *   PURPOSE: Handles all necessary tasks in support of the function
         *   of the program that do not require user input.
         *   ENTRY: Called at program start.
         *   EXIT: Calls LoadTypeIDs, creates the CharacterSheet directory if
         *   it does not exist, checks to see if the character sheet is fresh,
         *   if it exists, loads it if it is, otherwise downloads a new one.
         * 
        **********************************************************************/
        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadTypeIDs();
            HardcodeContents();

            // check to see if charsheet is stored locally and still fresh
            if (!Directory.Exists("CharacterSheet"))
            {
                Directory.CreateDirectory("CharacterSheet");
            }

            if (CharacterSheetIsFresh())
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(new StreamReader("CharacterSheet/Thirtyone Organism.xml").ReadToEnd());
                ProcessCharacterSheet(xmldoc);
            }
            else
            {
                DownloadCharacterSheet();
            }
        }

        /**********************************************************************
         * 
         *   PURPOSE: Loads the complete list of typeIDs and the specific
         *   reprocessing skills from the internal resources file into two
         *   collections: typeids and reprocessing skills.
         *   ENTRY: Called at form load.
         *   EXIT: After the skills have been loaded into their collections.
         * 
        **********************************************************************/
        private void LoadTypeIDs()
        {
            typeids = Properties.Resources.tiamat_typeids.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            reprocessing_skills = Properties.Resources.reprocessing_skills.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /**********************************************************************
         * 
         * Hardcoding mineral and ore values.
         * 
         *********************************************************************/
        private void HardcodeContents()
        {
            minerals.Add(new Mineral("Tritanium"));
            minerals.Add(new Mineral("Pyerite"));
            minerals.Add(new Mineral("Mexallon"));
            minerals.Add(new Mineral("Isogen"));
            minerals.Add(new Mineral("Nocxium"));
            minerals.Add(new Mineral("Zydrine"));
            minerals.Add(new Mineral("Megacyte"));
            minerals.Add(new Mineral("Morphite"));

            ores.Add(new Ore("Veldspar", 0, 0.1F));
            ores[0].Add(new Mineral("Tritanium", 415));

            ores.Add(new Ore("Scordite", 0, 0.15F));
            ores[1].Add(new Mineral("Pyerite", 173));
            ores[1].Add(new Mineral("Tritanium", 346));

            ores.Add(new Ore("Pyroxeres", 0, 0.3F));
            ores[2].Add(new Mineral("Nocxium", 5));
            ores[2].Add(new Mineral("Mexallon", 50));
            ores[2].Add(new Mineral("Pyerite", 25));
            ores[2].Add(new Mineral("Tritanium", 351));

            ores.Add(new Ore("Plagioclase", 0, 0.35F));
            ores[3].Add(new Mineral("Mexallon", 107));
            ores[3].Add(new Mineral("Pyerite", 213));
            ores[3].Add(new Mineral("Tritanium", 107));

            ores.Add(new Ore("Omber", 0, 0.6F));
            ores[4].Add(new Mineral("Isogen", 85));
            ores[4].Add(new Mineral("Pyerite", 34));
            ores[4].Add(new Mineral("Tritanium", 85));

            ores.Add(new Ore("Kernite", 0, 1.2F));
            ores[5].Add(new Mineral("Isogen", 134));
            ores[5].Add(new Mineral("Mexallon", 267));
            ores[5].Add(new Mineral("Tritanium", 134));

            ores.Add(new Ore("Jaspet", 0, 2.0F));
            ores[6].Add(new Mineral("Zydrine", 3));
            ores[6].Add(new Mineral("Nocxium", 72));
            ores[6].Add(new Mineral("Mexallon", 144));
            ores[6].Add(new Mineral("Pyerite", 121));
            ores[6].Add(new Mineral("Tritanium", 72));

            ores.Add(new Ore("Hemorphite", 0, 3.0F));
            ores[7].Add(new Mineral("Zydrine", 8));
            ores[7].Add(new Mineral("Nocxium", 118));
            ores[7].Add(new Mineral("Isogen", 59));
            ores[7].Add(new Mineral("Mexallon", 17));
            ores[7].Add(new Mineral("Pyerite", 72));
            ores[7].Add(new Mineral("Tritanium", 180));

            ores.Add(new Ore("Hedbergite", 0, 30F));
            ores[8].Add(new Mineral("Zydrine", 9));
            ores[8].Add(new Mineral("Nocxium", 98));
            ores[8].Add(new Mineral("Isogen", 196));
            ores[8].Add(new Mineral("Pyerite", 81));

            ores.Add(new Ore("Spodumain", 0, 16.0F));
            ores[9].Add(new Mineral("Megacyte", 78));
            ores[9].Add(new Mineral("Pyerite", 4972));
            ores[9].Add(new Mineral("Tritanium", 39221));

            ores.Add(new Ore("Gneiss", 0, 5.0F));
            ores[10].Add(new Mineral("Zydrine", 60));
            ores[10].Add(new Mineral("Isogen", 242));
            ores[10].Add(new Mineral("Mexallon", 1278));
            ores[10].Add(new Mineral("Tritanium", 1278));

            ores.Add(new Ore("Dark Ochre", 0, 8.0F));
            ores[11].Add(new Mineral("Zydrine", 87));
            ores[11].Add(new Mineral("Nocxium", 173));
            ores[11].Add(new Mineral("Tritanium", 8804));

            ores.Add(new Ore("Crokite", 0, 16.0F));
            ores[12].Add(new Mineral("Zydrine", 367));
            ores[12].Add(new Mineral("Nocxium", 275));
            ores[12].Add(new Mineral("Tritanium", 20992));


            ores.Add(new Ore("Bistot", 0, 16.0F));
            ores[13].Add(new Mineral("Megacyte", 118));
            ores[13].Add(new Mineral("Zydrine", 236));
            ores[13].Add(new Mineral("Pyerite", 16572));


            ores.Add(new Ore("Arkonor", 0, 16.0F));
            ores[14].Add(new Mineral("Megacyte", 230));
            ores[14].Add(new Mineral("Zydrine", 115));
            ores[14].Add(new Mineral("Mexallon", 1278));
            ores[14].Add(new Mineral("Tritanium", 6905));

            ores.Add(new Ore("Mercoxit", 0, 40.0F));
            ores[15].Add(new Mineral("Morphite", 293));
        }

        /**********************************************************************
         * 
         *   PURPOSE: Checks to see if the character sheet stored on the 
         *   harddrive, if it exists, is still fresh. Freshness if determined
         *   by comparing the cachedUntil value with the current UTC time.
         *   If the cachedUntil time is greater than (or more recent) than the
         *   current UTC time, then the sheet is considered fresh.
         *   ENTRY: Called at form load.
         *   EXIT: Returns a bool value that is later used to determine whether
         *   to use the sheet on the harddrive or download a newer one from
         *   CCP's API.
         * 
        **********************************************************************/
        private bool CharacterSheetIsFresh()
        {
            bool isfresh = false;

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                StreamReader reader = new StreamReader("CharacterSheet/Thirtyone Organism.xml");
                xmldoc.LoadXml(reader.ReadToEnd());
                reader.Close();

                XmlNode cachedUntilNode = xmldoc.GetElementsByTagName("cachedUntil")[0];
                DateTime cachedUntilDT = DateTime.Parse(cachedUntilNode.InnerText);

                if (cachedUntilDT > DateTime.UtcNow)
                {
                    isfresh = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return isfresh;
        }

        /**********************************************************************
         * 
         *   PURPOSE: Determines the charater's reprocessing skills.
         *   ENTRY: Takes an XmlDocument orginating from either the harddrive
         *   if the sheet is still fresh, or from CCP's API server.
         *   EXIT: When all reprocessing sills contained in the collection of
         *   reprocessing_skills with all the skills listed in the character
         *   sheet. A colelction of industry_skills is created that holds
         *   SkillLevel objects containing the skill name (which is matched by
         *   typeid), the skill typeid, and the level of the skill - which
         *   influences reprocessing efficiency.
         * 
        **********************************************************************/
        private void ProcessCharacterSheet(XmlDocument xmldoc)
        {
            XmlNodeList nodes = xmldoc.GetElementsByTagName("rowset");
            XmlNode targetNode = null;
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes[0].Value == "skills")
                {
                    targetNode = node;
                }
            }

            // grab all rows in here, and keep only the ones
            // that match up with the reprocessing list.

            nodes = null;
            nodes = targetNode.SelectNodes("row");

            for (int i = 0; i < nodes.Count; ++i)
            {
                bool matchfound = false;
                for (int j = 0; j < reprocessing_skills.Count && !matchfound; ++j)
                {
                    if (Convert.ToInt32(nodes[i].Attributes[0].InnerText) == Convert.ToInt32(reprocessing_skills[j].Split(',')[0]))
                    {
                        matchfound = true;
                        int typeid = Convert.ToInt32(nodes[i].Attributes[0].InnerText);
                        string name = MatchTypeIDtoName(typeid);
                        int skill_level = Convert.ToInt32(nodes[i].Attributes[2].InnerText);
                        industry_skills.Add(new SkillLevel(name, typeid, skill_level));
                    }
                }
            }
        }

        /**********************************************************************
         * 
         *   PURPOSE: Matches the input typeid with its friendly name.
         *   ENTRY: Accepts an integer typeid.
         *   EXIT: Searches through the complete list of typeids contained in
         *   the collection "typeids". If a match is found, then the friendly
         *   name is returned.
         * 
        **********************************************************************/
        private string MatchTypeIDtoName(int typeid)
        {
            string name = null;
            bool match_found = false;

            for (int i = 0; i < typeids.Count && !match_found; ++i)
            {
                if (Convert.ToInt32(typeids[i].Split(',')[0]) == typeid)
                {
                    match_found = true;
                    name = typeids[i].Split(',')[2];
                }
            }

            return name;
        }

        /**********************************************************************
         * 
         *   PURPOSE: If the character sheet on the hard drive does not exist
         *   or is not fresh, then a new sheet will be downloaded from the
         *   CCP API using the full access API key.
         *   ENTRY: Called during form load if the character sheet is not 
         *   fresh.
         *   EXIT: The character sheet is downloaded asynchronously and the
         *   server response is handled by HandleCharSheetResponse().
         * 
        **********************************************************************/
        private void DownloadCharacterSheet()
        {
            try
            {
                string api_key = "https://api.eveonline.com/Char/CharacterSheet.xml.aspx?&characterID=91810030&keyID=3890775&vCode=8w2EoSi0UyXXiSaagZnUN1ep2B6bkcFFCNd5CBsMnE7X5CHB3iHqYxEGubzBWP3c";
                WebClient web = new WebClient();
                web.Proxy = null;
                web.DownloadStringCompleted += new DownloadStringCompletedEventHandler(HandleCharSheetResponse);
                web.DownloadStringAsync(new Uri(api_key));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong at load. Message: " + ex.Message);
            }
        }

        /**********************************************************************
         * 
         *   PURPOSE: Handles CCP API response.
         *   ENTRY: Called when the CCP API responds to the async request.
         *   EXIT: If the CharacterSheet directory does not exist, then it is 
         *   created. The server response is parsed as an XML document (because
         *   that's what it is), then saved to the harddrive. The document is 
         *   then passed to ProcessCharacterSheet().
         * 
        **********************************************************************/
        private void HandleCharSheetResponse(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!Directory.Exists("CharacterSheet"))
                {
                    Directory.CreateDirectory("CharacterSheet");
                }

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(e.Result);
                xmldoc.Save("CharacterSheet/Thirtyone Organism.xml");
                ProcessCharacterSheet(xmldoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load character sheet. Message: " + ex.Message);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            DetermineMineralNeeds();
            float base_reprocessing_pct = (station_base * station_tax) * CharacterReprocessing();
            CrunchOres(base_reprocessing_pct);
            OutputResults();
            ResetVariables();
        }

        private void ResetVariables()
        {
            foreach (Ore o in ores)
            {
                o.Quantity = 0;
            }

            foreach (Mineral m in minerals)
            {
                m.ClearAll();
            }
        }

        private void OutputResults()
        {
            rtbDisplay.Text = "Ore Name, Batches\n";
            rtbDisplay.Text += "-----------------\n";
            foreach (Ore o in ores)
            {
                if (o.Quantity != 0)
                {
                    rtbDisplay.Text += o.Name + ": " + o.Quantity + "\n";
                }
            }

            rtbDisplay.Text += "\nLeftover minerals\n";
            rtbDisplay.Text += "-----------------\n";

            foreach (Mineral m in minerals)
            {
                if (m.QuantityNeeded < 0)
                {
                    rtbDisplay.Text += m.Name + ": " + (-1 * m.QuantityNeeded) + "\n";
                }
            }

            rtbDisplay.Text += "\nMinerals still needed\n";
            rtbDisplay.Text += "---------------------\n";

            foreach (Mineral m in minerals)
            {
                if (m.QuantityNeeded > 0)
                {
                    rtbDisplay.Text += m.Name + ": " + (m.QuantityNeeded) + "\n";
                }
            }
        }

        private void Reprocess(float base_reprocessing_pct, string ore_name, string mineral_name)
        {
            Ore ore = OreName(ore_name);
            float ore_refine_efficiency = base_reprocessing_pct * OreReprocessing(ore_name); // refine pct for all minerals contained
            int minerals_per_batch = ore.MineralByName(mineral_name).Quantity;
            int batches = Convert.ToInt32(Math.Ceiling(MineralName(mineral_name).QuantityNeeded / (ore_refine_efficiency * minerals_per_batch)));
            ore.Quantity = batches;
            
            // iterate through here and add to the 
            for (int i = 0; i < ore.Contents.Count; ++i)
            {
                bool match_found = false;
                for (int j = 0; j < minerals.Count && !match_found; ++j)
                {
                    if (ore.Contents[i].Name == minerals[j].Name)
                    {
                        match_found = true;
                        int minerals_refined = Convert.ToInt32(ore.Quantity * ore_refine_efficiency * ore.Contents[i].Quantity);
                        minerals[j].Quantity += minerals_refined;
                        minerals[j].QuantityNeeded -= minerals_refined;
                    }
                }
            }
        }

        private void CrunchOres(float base_reprocessing_pct)
        {
            if (MineralName("Morphite").QuantityNeeded > 0 && chbMercoxit.Checked)
            {
                string mineral_name = "Morphite";
                Reprocess(base_reprocessing_pct, "Mercoxit", mineral_name);
            }

            if (MineralName("Megacyte").QuantityNeeded > 0)
            {
                string mineral_name = "Megacyte";

                if (chbArkonor.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Arkonor", mineral_name);
                }
                else if (chbBistot.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Bistot", mineral_name);
                }
                else if (chbSpodumain.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Spodumain", mineral_name);
                }
            }

            if (MineralName("Zydrine").QuantityNeeded > 0)
            {
                string mineral_name = "Zydrine";

                if (chbCrokite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Crokite", mineral_name);
                }
                else if (chbBistot.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Bistot", mineral_name);
                }
                else if (chbGneiss.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Gneiss", mineral_name);
                }
                else if (chbDarkOchre.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Dark Ochre", mineral_name);
                }
                else if (chbArkonor.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Arkonor", mineral_name);
                }
                else if (chbHedbergite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hedbergite", mineral_name);
                }
                else if (chbHemorphite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hemorphite", mineral_name);
                }
                else if (chbJaspet.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Jaspet", mineral_name);
                }
            }

            if (MineralName("Nocxium").QuantityNeeded > 0)
            {
                string mineral_name = "Nocxium";

                if (chbHemorphite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hemorphite", mineral_name);
                }
                else if (chbJaspet.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Jaspet", mineral_name);
                }
                else if (chbHedbergite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hedbergite", mineral_name);
                }
                else if (chbDarkOchre.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Dark Ochre", mineral_name);
                }
                else if (chbPyroxeres.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Pyroxeres", mineral_name);
                }
                else if (chbCrokite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Crokite", mineral_name);
                }
            }

            if (MineralName("Isogen").QuantityNeeded > 0)
            {
                string mineral_name = "Isogen";

                if (chbOmber.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Omber", mineral_name);
                }
                else if (chbKernite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Kernite", mineral_name);
                }
                else if (chbHedbergite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hedbergite", mineral_name);
                }
                else if (chbGneiss.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Gneiss", mineral_name);
                }
                else if (chbHemorphite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hemorphite", mineral_name);
                }
            }

            if (MineralName("Mexallon").QuantityNeeded > 0)
            {
                string mineral_name = "Mexallon";

                if (chbPlagioclase.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Plagioclase", mineral_name);
                }
                else if (chbGneiss.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Gneiss", mineral_name);
                }
                else if (chbKernite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Kernite", mineral_name);
                }
                else if (chbPyroxeres.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Pyroxeres", mineral_name);
                }
                else if (chbArkonor.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Arkonor", mineral_name);
                }
                else if (chbJaspet.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Jaspet", mineral_name);
                }
                else if (chbHemorphite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hemorphite", mineral_name);
                }
            }

            if (MineralName("Pyerite").QuantityNeeded > 0)
            {
                string mineral_name = "Pyerite";

                if (chbScordite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Scordite", mineral_name);
                }
                else if (chbBistot.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Bistot", mineral_name);
                }
                else if (chbPlagioclase.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Plagioclase", mineral_name);
                }
                else if (chbSpodumain.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Spodumain", mineral_name);
                }
                else if (chbPyroxeres.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Pyroxeres", mineral_name);
                }
                else if (chbJaspet.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Jaspet", mineral_name);
                }
                else if (chbOmber.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Omber", mineral_name);
                }
                else if (chbHedbergite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hedbergite", mineral_name);
                }
                else if (chbHemorphite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hemorphite", mineral_name);
                }
            }

            if (MineralName("Tritanium").QuantityNeeded > 0)
            {
                string mineral_name = "Tritanium";
                if (chbVeldspar.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Veldspar", mineral_name);
                }
                else if (chbSpodumain.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Spodumain", mineral_name);
                }
                else if (chbScordite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Scordite", mineral_name);
                }
                else if (chbCrokite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Crokite", mineral_name);
                }
                else if (chbPyroxeres.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Pyroxeres", mineral_name);
                }
                else if (chbDarkOchre.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Dark Ochre", mineral_name);
                }
                else if (chbArkonor.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Arkonor", mineral_name);
                }
                else if (chbPlagioclase.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Plagioclase", mineral_name);
                }
                else if (chbGneiss.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Gneiss", mineral_name);
                }
                else if (chbOmber.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Omber", mineral_name);
                }
                else if (chbKernite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Kernite", mineral_name);
                }
                else if (chbHemorphite.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Hemorphite", mineral_name);
                }
                else if (chbJaspet.Checked)
                {
                    Reprocess(base_reprocessing_pct, "Jaspet", mineral_name);
                }
            }
        }

        private Ore OreName(string ore_name)
        {
            Ore o = null;

            for (int i = 0; i < ores.Count; ++i)
            {
                if (ores[i].Name == ore_name)
                {
                    o = ores[i];
                }
            }

            return o;
        }

        private Mineral MineralName(string mineral_name)
        {
            Mineral m = null;

            for (int i = 0; i < minerals.Count; ++i)
            {
                if (minerals[i].Name == mineral_name)
                {
                    m = minerals[i];
                }
            }
            
            return m;
        }

        private void DetermineMineralNeeds()
        {
            try
            {
                if (rtbParse.Text == "")
                {
                    if (txbIsogen.Text != "")
                    {
                        MineralName("Isogen").QuantityNeeded = Convert.ToInt32(txbIsogen.Text);
                    }

                    if (txbMegacyte.Text != "")
                    {
                        MineralName("Megacyte").QuantityNeeded = Convert.ToInt32(txbMegacyte.Text);
                    }

                    if (txbMexallon.Text != "")
                    {
                        MineralName("Mexallon").QuantityNeeded = Convert.ToInt32(txbMexallon.Text);
                    }

                    if (txbMorphite.Text != "")
                    {
                        MineralName("Morphite").QuantityNeeded = Convert.ToInt32(txbMorphite.Text);
                    }

                    if (txbNocxium.Text != "")
                    {
                        MineralName("Nocxium").QuantityNeeded = Convert.ToInt32(txbNocxium.Text);
                    }

                    if (txbPyerite.Text != "")
                    {
                        MineralName("Pyerite").QuantityNeeded = Convert.ToInt32(txbPyerite.Text);
                    }

                    if (txbTritanium.Text != "")
                    {
                        MineralName("Tritanium").QuantityNeeded = Convert.ToInt32(txbTritanium.Text);
                    }

                    if (txbZydrine.Text != "")
                    {
                        MineralName("Zydrine").QuantityNeeded = Convert.ToInt32(txbZydrine.Text);
                    }
                }
                else
                {
                    Parse();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("One or more fields contain a non-integer value. Message: " + ex.Message);
            }

        }

        private void Parse()
        {
            try
            {
                AbstractParseStyle parser = null;

                if (rtbParse.Text.Contains('('))
                {
                    parser = new ConcreteParenthesisStyle();
                }
                else
                {
                    parser = new ConcreteXStyle();
                }

                List<NameAndQuantity> items = parser.Parse(rtbParse.Text);
                int multiplier = Convert.ToInt32(txbUnits.Text);
                foreach (NameAndQuantity item in items)
                {
                    MineralName(item.Name).QuantityNeeded = item.Quantity * multiplier;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input in Parse box. Message: " + ex.Message);
            }
        }

        /**********************************************************************
         * 
         * Takes the name of the ore and returns the player's
         * reprocessing percentage modifier.
         * 
         *********************************************************************/
        private float OreReprocessing(string ore_name)
        {
            float pct = 1F;

            for (int i = 0; i < industry_skills.Count; ++i)
            {
                if (industry_skills[i].Name.Contains(ore_name))
                {
                    pct = 1 + .02F * industry_skills[i].Level;
                }
            }

            return pct;
        }

        /**********************************************************************
        * 
        * Determines the percentage value of the character's reprocessing and
        * reprocessing efficiency skills.
        * 
        **********************************************************************/
        private float CharacterReprocessing()
        {
            float reprocessing_pct = 1F;
            float reprocessing_effeciency_pct = 1F;

            for (int i = 0; i < industry_skills.Count; ++i)
            {
                if (industry_skills[i].Name == "Reprocessing")
                {
                    reprocessing_pct = 1 + (Convert.ToInt32(industry_skills[i].Level) * .03F);
                }

                if (industry_skills[i].Name == "Reprocessing Efficiency")
                {
                    reprocessing_effeciency_pct = 1 + (Convert.ToInt32(industry_skills[i].Level) * .02F);
                }
            }

            return reprocessing_effeciency_pct * reprocessing_pct;
        }

        /**********************************************************************
         * 
         *   PURPOSE: Checks to see if all the minerals needed can be obtained
         *   from the available ores.
         *   ENTRY: Called from the btnCalculate_Click event.
         *   EXIT: If an ore type is not available with which to obtain a
         *   needed mineral, than a MissingOreException is thrown. As of right
         *   now, only one exception can be handled at a time. If multiple
         *   needed ores cannot be obtained, then the user won't be told.
         * 
        **********************************************************************/
        private bool HaveAllNecessaryOres()
        {
            bool have_all = true;

            try
            {
                if (txbIsogen.Text != "")
                {
                    if (!chbOmber.Checked && !chbKernite.Checked &&
                        !chbHemorphite.Checked && !chbHedbergite.Checked &&
                        !chbGneiss.Checked)
                    {
                        throw new MissingOreException("Isogen");
                    }
                }

                if (txbMegacyte.Text != "")
                {
                    if (!chbSpodumain.Checked && !chbBistot.Checked &&
                        !chbArkonor.Checked)
                    {
                        throw new MissingOreException("Megacyte");
                    }
                }

                if (txbMexallon.Text != "")
                {
                    if (!chbPyroxeres.Checked && !chbPlagioclase.Checked &&
                        !chbKernite.Checked && !chbJaspet.Checked &&
                        !chbHemorphite.Checked && !chbGneiss.Checked)
                    {
                        throw new MissingOreException("Mexallon");
                    }
                }

                if (txbMorphite.Text != "")
                {
                    if (!chbMercoxit.Checked)
                    {
                        throw new MissingOreException("Morphite");
                    }
                }

                if (txbNocxium.Text != "")
                {
                    if (!chbPyroxeres.Checked && !chbJaspet.Checked &&
                        !chbHemorphite.Checked && !chbHedbergite.Checked &&
                        !chbDarkOchre.Checked && !chbCrokite.Checked)
                    {
                        throw new MissingOreException("Nocxium");
                    }
                }

                if (txbPyerite.Text != "")
                {
                    if (!chbScordite.Checked && !chbPyroxeres.Checked &&
                        !chbPlagioclase.Checked && !chbOmber.Checked &&
                        !chbJaspet.Checked && !chbHemorphite.Checked &&
                        !chbHedbergite.Checked && !chbSpodumain.Checked &&
                        !chbBistot.Checked)
                    {
                        throw new MissingOreException("Pyerite");
                    }

                }

                if (txbTritanium.Text != "")
                {
                    if (!chbVeldspar.Checked && !chbScordite.Checked &&
                        !chbPyroxeres.Checked && !chbPlagioclase.Checked &&
                        !chbOmber.Checked && !chbKernite.Checked &&
                        !chbJaspet.Checked && !chbHemorphite.Checked &&
                        !chbSpodumain.Checked && !chbGneiss.Checked &&
                        !chbDarkOchre.Checked && !chbArkonor.Checked)
                    {
                        throw new MissingOreException("Tritanium");
                    }
                }

                if (txbZydrine.Text != "")
                {
                    if (!chbJaspet.Checked && !chbHemorphite.Checked &&
                        !chbHedbergite.Checked && !chbGneiss.Checked &&
                        !chbDarkOchre.Checked && !chbCrokite.Checked &&
                        !chbBistot.Checked && !chbArkonor.Checked)
                    {
                        throw new MissingOreException("Zydrine");
                    }
                }
            }
            catch (MissingOreException ex)
            {
                have_all = false;
                MessageBox.Show(ex.ToString() +
                    " is required, but no containing Ores are available.");
            }

            return have_all;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            chbArkonor.Checked = true;
            chbBistot.Checked = true;
            chbCrokite.Checked = true;
            chbDarkOchre.Checked = true;
            chbGneiss.Checked = true;
            chbHedbergite.Checked = true;
            chbHemorphite.Checked = true;
            chbJaspet.Checked = true;
            chbKernite.Checked = true;
            chbMercoxit.Checked = true;
            chbOmber.Checked = true;
            chbPlagioclase.Checked = true;
            chbPyroxeres.Checked = true;
            chbScordite.Checked = true;
            chbSpodumain.Checked = true;
            chbVeldspar.Checked = true;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            chbArkonor.Checked = false;
            chbBistot.Checked = false;
            chbCrokite.Checked = false;
            chbDarkOchre.Checked = false;
            chbGneiss.Checked = false;
            chbHedbergite.Checked = false;
            chbHemorphite.Checked = false;
            chbJaspet.Checked = false;
            chbKernite.Checked = false;
            chbMercoxit.Checked = false;
            chbOmber.Checked = false;
            chbPlagioclase.Checked = false;
            chbPyroxeres.Checked = false;
            chbScordite.Checked = false;
            chbSpodumain.Checked = false;
            chbVeldspar.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txbIsogen.Text = "";
            txbMegacyte.Text = "";
            txbMexallon.Text = "";
            txbMorphite.Text = "";
            txbNocxium.Text = "";
            txbPyerite.Text = "";
            txbTritanium.Text = "";
            txbZydrine.Text = "";
            rtbDisplay.Text = "";
            rtbParse.Text = "";
        }

        private void aPIKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APIKeyForm form = new APIKeyForm();
            form.Show();
        }
        
    }
}
