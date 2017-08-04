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

                sds.SelectCommand = Resources.CatalogManagerSQLQueries.VerifyRegexSelect;
                DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                dssa.RetrieveTotalRowCount = true;
                DataView dv = (DataView)sds.Select(dssa);

                if (dv.Count == 0)
                {
                    sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertCleanValues;
                    sds.Insert();

                    sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateJobs;
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
        command.CommandText = Resources.CatalogManagerSQLQueries.AddVariantsToJobPreview;
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

                sds.UpdateCommand = Resources.CatalogManagerSQLQueries.AddVariantsToJob;
                sds.Update();

                sds.UpdateCommand = Resources.CatalogManagerSQLQueries.AddNewJobs;
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

                sds.UpdateCommand = Resources.CatalogManagerSQLQueries.RemoveVariantsFromJob;

                sds.Update();

                sds.UpdateCommand = Resources.CatalogManagerSQLQueries.RemoveJob;

                sds.Update();

                txtEditJobVariants.Text = String.Empty;
            }
        }
        catch (Exception ex) { }
    }
}