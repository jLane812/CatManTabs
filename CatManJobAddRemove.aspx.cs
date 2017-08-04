using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public partial class Admin_CatManJobAddRemove : System.Web.UI.Page
{
    SqlDataSource sds = new SqlDataSource();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

            sds.SelectCommand = Resources.CatalogManagerSQLQueries.GetJobList;

            ddlJobs.DataSource = sds;
            ddlJobs.DataTextField = "Job_Name";
            ddlJobs.DataValueField = "Job_ID";
            ddlJobs.DataBind();
        }
        if (ddlJobs.SelectedIndex != 0)
        {
            AddVariantsToTxtPreviewJobVariants();
        }
    }

    protected void btnAddCatManJob_Click(object sender, EventArgs e)
    {
        string vettedCreateJobText = Regex.Replace(txtCreateJob.Text.ToLower(), @"[\s]+", "");
        try
        {
            if (vettedCreateJobText != null && vettedCreateJobText != "" && vettedCreateJobText.Length <= 50)
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.SelectCommand = @"WITH X AS (SELECT Job_Name FROM [DataOnboarding_Staging].[CatalogManager].[Jobs] WHERE LOWER(Tools.dbo.RegExReplace(Job_Name,' +','')) LIKE '%|" + vettedCreateJobText + "%') SELECT * FROM X";
                DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                dssa.RetrieveTotalRowCount = true;
                DataView dv = (DataView)sds.Select(dssa);

                if (dv.Count == 0)
                {
                    sds.InsertCommand = @"INSERT INTO ATHENA.DataOnboarding_Staging.CatalogManager.Jobs(Job_Name,Date_Created,Filter)
                                      VALUES ('Job #' + LTRIM(RTRIM(STR((SELECT MAX(Job_ID) + 1 FROM ATHENA.DataOnboarding_Staging.CatalogManager.Jobs))))  + ' | ' + LTRIM(RTRIM('" + Helpers.CleanValue(txtCreateJob.Text) + "')), GETDATE(), 'Direct Assignment')";
                    sds.Insert();

                    sds.UpdateCommand = @"WITH X AS
                                  (
                                  SELECT [Job_ID],[Job_Name],[User],[Date_Created],[Filter],[Date_Finished]
                                  FROM ATHENA.[DataOnboarding_Staging].[CatalogManager].[Jobs]
                                  WHERE Job_Name NOT LIKE'%' + CAST(Job_ID AS NVARCHAR(15)) +'%'
                                  )

                                  UPDATE X
                                  SET Job_Name = Tools.dbo.RegExReplace(Job_Name,'#[0-9]+\s', CONCAT('#',Job_ID,' '))";
                    sds.Update();
                }
            }
            txtCreateJob.Text = String.Empty;
        }
        catch(InvalidCastException ex) { }
    }

    protected void AddVariantsToTxtPreviewJobVariants() 
    {
        txtPreviewJobVariants.Text = "";
        SqlConnection conn = new SqlConnection(Resources.ConnectionStrings.ATHENA);
        SqlCommand command = new SqlCommand();
        VariantsArray[] va = null;
        command.CommandText = @"SELECT Variant_NetSuiteID
                                FROM Product.HumanDecisionFactor.GlobalVariant gv
                                INNER JOIN Product.HumanDecisionFactor.GlobalProduct gp ON gv.GlobalProductID=gp.GlobalProductID
                                WHERE cm_tags LIKE'%" + ddlJobs.Text + "%' ORDER BY Variant_NetSuiteID";
        command.Connection = conn;
        conn.Open();
        using (var reader = command.ExecuteReader())
        {
            var list = new List<VariantsArray>();
            while (reader.Read())
            {
                list.Add(new VariantsArray { variant = reader.GetInt32(0) });
                va = list.ToArray();
            }

            if (va != null)
            {
                foreach (var i in va)
                {
                    txtPreviewJobVariants.Text = txtPreviewJobVariants.Text + i.variant.ToString() + "\n";
                }
            }
        }
        conn.Dispose();
        conn.Close();
    }

    protected void btnAddVariants_Click(object sender, EventArgs e)
    {
        try
        {
            string vettedVariants = Regex.Replace(txtEditJobVariants.Text, "[^0-9]+", ",");
            string finalVettedVariants = Regex.Replace(vettedVariants, "[^0-9]$", "");
            string vettedddlJobsText = Regex.Replace(ddlJobs.Text, "Job #", "");
            string finalVettedddlJobsText = Regex.Replace(vettedddlJobsText, " | [0-9a-zA-Z]", "");

            if (txtEditJobVariants.Text != null && txtEditJobVariants.Text != "")
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.UpdateCommand = @"WITH x AS
                                  (
                                  SELECT DISTINCT gp.GlobalProductID
                                  FROM Product.HumanDecisionFactor.GlobalProduct gp
                                  INNER JOIN Product.HumanDecisionFactor.GlobalVariant gv ON gp.GlobalProductID = gv.GlobalProductID
                                  WHERE Variant_NetSuiteID IN(
                                  " + finalVettedVariants +
                                      ")), y AS (SELECT gp.* FROM X INNER JOIN Product.HumanDecisionFactor.GlobalProduct gp ON x.globalProductID = gp.GlobalProductID) Update y SET cm_tags = CONCAT(cm_tags,'," + finalVettedddlJobsText + "') WHERE cm_tags IS NOT NULL AND cm_tags NOT LIKE'%" + finalVettedddlJobsText + "%'";
                sds.Update();

                sds.UpdateCommand = @"WITH x AS
                                 (
                                 SELECT DISTINCT gp.GlobalProductID
                                 FROM Product.HumanDecisionFactor.GlobalProduct gp
                                 INNER JOIN Product.HumanDecisionFactor.GlobalVariant gv ON gp.GlobalProductID = gv.GlobalProductID
                                 WHERE Variant_NetSuiteID IN(
                                 " + finalVettedVariants +
                                     ")), y AS (SELECT gp.* FROM X INNER JOIN Product.HumanDecisionFactor.GlobalProduct gp ON x.globalProductID = gp.GlobalProductID) Update y SET cm_tags = '" + finalVettedddlJobsText + "' WHERE cm_tags IS NULL";
                sds.Update();
                txtEditJobVariants.Text = String.Empty;
            }
        }
        catch (Exception ex) { }
    }

    protected void btnRemoveVariants_Click(object sender, EventArgs e)
    {
        try
        {
            string vettedVariants = Regex.Replace(txtEditJobVariants.Text, "[^0-9]+", ",");
            string finalVettedVariants = Regex.Replace(vettedVariants, "[^0-9]$", "");

            if (txtEditJobVariants.Text != null && txtEditJobVariants.Text != "")
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.UpdateCommand = @"WITH X AS
                                (
                                SELECT cm_tags, Variant_NetSuiteID
                                FROM Product.HumanDecisionFactor.GlobalVariant gv
                                INNER JOIN Product.HumanDecisionFactor.GlobalProduct gp ON gv.GlobalProductID=gp.GlobalProductID
                                WHERE Variant_NetSuiteID IN(" + finalVettedVariants + ")) UPDATE X SET cm_tags = REPLACE(cm_tags,'," + ddlJobs.Text + "','')";

                sds.Update();

                sds.UpdateCommand = @"WITH X AS
                                (
                                SELECT cm_tags, Variant_NetSuiteid
                                FROM Product.HumanDecisionFactor.GlobalVariant gv
                                INNER JOIN Product.HumanDecisionFactor.GlobalProduct gp ON gv.GlobalProductID=gp.GlobalProductID
                                WHERE Variant_NetSuiteID IN(" + finalVettedVariants + ")) UPDATE X SET cm_tags = REPLACE(cm_tags,'" + ddlJobs.Text + "','')";

                sds.Update();

                txtEditJobVariants.Text = String.Empty;
            }
        }
        catch (Exception ex) { }
    }
}