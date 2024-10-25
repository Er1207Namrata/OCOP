using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using OCOO.Models;
using System.Data;
using System.Reflection;
using System.Xml.Linq;

namespace OCOO.Controllers
{
    public class StpGraphController : Controller
    {
        public IActionResult StpGraph(StpTree stpTree, string klm)
        {
            if (!string.IsNullOrEmpty(klm))
            {
                stpTree.Fk_STPId = Crypto.Decrypt(klm);
            }
            return View(stpTree);
        }
        public ActionResult GetStpTree(StpTree stpTree)
        {
            DataSet dataSet = stpTree.GetStpTree();
            List<ParentStpTree> lstParentStpTree = new List<ParentStpTree>();
            List<ChildStpTree> lstChildStpTree = new List<ChildStpTree>();
            List<Drain> lstDrain = new List<Drain>();
            List<SeverLength> lstSeverLength = new List<SeverLength>();


            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    i = i + 1;
                    string drains = null;
                    string[] drainsData = new string[0];
                    string Sewer = null;
                    string[] SewerData = new string[0];
                    ChildStpTree childStpTree = new ChildStpTree();
                    childStpTree.Name = row["Name"].ToString();
                    childStpTree.ParentName = row["ParentName"].ToString();
                    childStpTree.Fk_ParentId = row["ParentId"].ToString();
                    childStpTree.Pk_Id = row["Pkmainpump_Id"].ToString();

                    // Find children for the current child

                    drains = row["FlowFromDrain"].ToString();
                    drainsData = drains.Split(',');
                    Sewer = row["FlowFromSewer"].ToString();
                    SewerData = Sewer.Split(',');
                    if (row["FlowFromDrain"].ToString() != "0" && !string.IsNullOrEmpty(row["FlowFromDrain"].ToString()))
                    {
                        foreach (var drainitem in drainsData)
                        {
                            if (!string.IsNullOrEmpty(drainitem))
                            {
                                Drain drain = new Drain();
                                drain.Name = drainitem;
                                drain.dName = drainitem + i.ToString();
                                drain.ParentName = row["Name"].ToString();
                                drain.Fk_ParentId = row["ParentId"].ToString();

                                lstDrain.Add(drain);
                            }

                        }
                    }
                    if (row["FlowFromSewer"].ToString() != "0" && !string.IsNullOrEmpty(row["FlowFromSewer"].ToString()))
                    {

                        foreach (var sewerItem in SewerData)
                        {
                            SeverLength severLength = new SeverLength();
                            if (!string.IsNullOrEmpty(sewerItem))
                            {

                                severLength.Name = sewerItem;
                                severLength.sName = sewerItem + i.ToString();

                                severLength.Fk_ParentId = row["ParentId"].ToString();
                                severLength.ParentName = row["Name"].ToString();
                                lstSeverLength.Add(severLength);
                            }
                        }

                    }
                    lstChildStpTree.Add(childStpTree);
                }
                stpTree.lstChild = lstChildStpTree;
                stpTree.lstDrain = lstDrain;
                stpTree.lstSeverLength = lstSeverLength;
            }

            return Json(stpTree);
        }
        //[HttpPost]
        //public ActionResult GetStpTree(StpTree stpTree)
        //{
        //    DataSet dataSet = new DataSet();
        //    dataSet = stpTree.GetStpTree();
        //    List<ParentStpTree> lstParentStpTree = new List<ParentStpTree>();
        //    List<ChildStpTree> lstChildStpTree = new List<ChildStpTree>();
        //    List<LevelTree> lstLevelTree = new List<LevelTree>();
        //    List<Drain> lstDrain = new List<Drain>();
        //    List<SeverLength> lstSeverLength = new List<SeverLength>();
        //    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        var str = "";
        //        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
        //        {
        //            ParentStpTree parentStpTree = new ParentStpTree();
        //            string drains = dataSet.Tables[0].Rows[i]["FlowFromDrain"].ToString();
        //            string[] drainsData = drains.Split(',');
        //            string Sewer = dataSet.Tables[0].Rows[i]["FlowFromSewer"].ToString();
        //            string[] SewerData = Sewer.Split(',');
        //            if (i == 0)
        //            {

        //                parentStpTree.Pk_Id = dataSet.Tables[0].Rows[0]["Pkmainpump_Id"].ToString();
        //                parentStpTree.ParentName = "undefined";
        //                parentStpTree.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();

        //                if (dataSet.Tables[0].Rows[i]["FlowFromDrain"].ToString() != "0" && !string.IsNullOrEmpty(dataSet.Tables[0].Rows[i]["FlowFromDrain"].ToString()))
        //                {
        //                    foreach (var drainitem in drainsData)
        //                    {
        //                        if (!string.IsNullOrEmpty(drainitem))
        //                        {
        //                            Drain drain = new Drain();
        //                            drain.FlowFromDrain = drainitem;
        //                            drain.ParentName = dataSet.Tables[0].Rows[i]["Name"].ToString();
        //                            lstDrain.Add(drain);
        //                        }

        //                    }
        //                }
        //                if (dataSet.Tables[0].Rows[i]["FlowFromSewer"].ToString() != "0" && !string.IsNullOrEmpty(dataSet.Tables[0].Rows[i]["FlowFromSewer"].ToString()))
        //                {

        //                    foreach (var sewerItem in SewerData)
        //                    {
        //                        SeverLength severLength = new SeverLength();
        //                        if (!string.IsNullOrEmpty(sewerItem))
        //                        {

        //                            severLength.FlowFromSewer = sewerItem;
        //                            severLength.ParentName = dataSet.Tables[0].Rows[i]["Name"].ToString();
        //                            lstSeverLength.Add(severLength);
        //                        }
        //                    }

        //                }

        //                stpTree.lstSeverLength = lstSeverLength;
        //                stpTree.lstDrain = lstDrain;
        //            }
        //            else
        //            {

        //                parentStpTree.Pk_Id = dataSet.Tables[0].Rows[i]["Pkmainpump_Id"].ToString();
        //                parentStpTree.ParentName = dataSet.Tables[0].Rows[i]["ParentName"].ToString();
        //                parentStpTree.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();



        //            }
        //            lstParentStpTree.Add(parentStpTree);
        //        }

        //        stpTree.lstParent = lstParentStpTree;
        //        //foreach (DataRow item1 in dataSet.Tables[1].Rows)
        //        //{
        //        //    ChildStpTree childStpTree = new ChildStpTree();
        //        //    childStpTree.Pk_Id = item1["Pkmainpump_Id"].ToString();
        //        //    childStpTree.ChildName = item1["ChildName"].ToString();
        //        //    childStpTree.ParentName = item1["ParentName"].ToString();
        //        //    childStpTree.LevelNo = int.Parse(item1["LevelNo"].ToString());
        //        //    lstChildStpTree.Add(childStpTree);
        //        //}
        //        //stpTree.lstChild = lstChildStpTree;
        //        //foreach (DataRow item1 in dataSet.Tables[1].Rows)
        //        //{
        //        //    LevelTree levelTree = new LevelTree();

        //        //    levelTree.LevelNo = long.Parse(item1["LevelNo"].ToString());
        //        //    lstLevelTree.Add(levelTree);
        //        //}
        //        //stpTree.lstLevelTree= lstLevelTree; 
        //    }


        //    return Json(stpTree);
        //}


        public ActionResult Tree()
        {
            return View();
        }

    }
}
