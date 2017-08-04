using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

/**REPORTING Comes from ATHENA.Data_OnBoardingStaging.Reporting.NewCategoriesCreated**/

public partial class Admin_CategoryManager : System.Web.UI.Page
{
    protected SqlDataSource sds = new SqlDataSource();
    protected List<string> errorMessage = new List<string>();
    protected string targetTable = Resources.CatalogManagerSQLQueries.TargetTable; 
    //Dynamic h1 front-end
    public string megaTitle = "Select MegaCategory";
    public string superTitle = "Select SuperCategory";
    public string catTitle = "Select Category";
    public string subTitle = "Select SubCategory";
    public string previewTitle = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack || ddlMegaCategory.SelectedIndex == 0)
        {   
            //Hide MegaCat Front-end Create
            txtMegaCategory.Visible = false;
            btnCreateMegaCategory.Visible = false;
            btnBackToSelectMegaCategory.Visible = false;
            btnEditMegaCategory.Visible = false;
            btnSubmitMegaCategoryChanges.Visible = false;
            //Hide SuperCat Front-end All
            SuperDiv.Visible = false;
            txtSuperCategory.Visible = false;
            btnCreateSuperCategory.Visible = false;
            btnBackToSelectSuperCategory.Visible = false;
            btnEditSuperCategory.Visible = false;
            btnSubmitSuperCategoryChanges.Visible = false;
            //Hide Cat Front-end All
            CatDiv.Visible = false;
            txtCreateCategory.Visible = false;
            btnCreateCategory.Visible = false;
            btnBackToSelectCategory.Visible = false;
            btnEditCategory.Visible = false;
            btnSubmitCategoryChanges.Visible = false;
            //Hide SubCat Front-end All
            SubDiv.Visible = false;
            txtCreateSubCategory.Visible = false;
            btnCreateSubCategory.Visible = false;
            btnBackToSelectSubCategory.Visible = false;
            btnEditSubCategory.Visible = false;
            btnSubmitSubCategoryChanges.Visible = false;
            //Hide Preview
            PreviewDiv.Visible = false;
            //Fill drop down lists
            ShowMegaCategories();
            ShowSuperCategories();
            ShowCategories();
            ShowSubCategories();
        }
        //Skip Create if Submit button is active
        else if(btnSubmitMegaCategoryChanges.Visible == true)
        {

        }
        else if (btnSubmitSuperCategoryChanges.Visible == true)
        {

        }
        else if (btnSubmitCategoryChanges.Visible == true)
        {

        }
        else if (btnSubmitSubCategoryChanges.Visible == true)
        {

        }
        else
        {
            //Create MegaCategory Page
            if (ddlMegaCategory.SelectedIndex == 1)
            {
                ddlMegaCategory.Visible = false;
                txtMegaCategory.Visible = true;
                btnCreateMegaCategory.Visible = true;
                btnBackToSelectMegaCategory.Visible = true;
                btnSubmitMegaCategoryChanges.Visible = false;
                btnEditMegaCategory.Visible = false;
                megaTitle = "Create MegaCategory";
                SuperDiv.Visible = false;
                CatDiv.Visible = false;
                SubDiv.Visible = false;
                PreviewDiv.Visible = true;
                ShowPreview(1);
            }
            else
            {
                //Create SuperCategory Page
                if (ddlSuperCategory.SelectedIndex == 1)
                {
                    ddlSuperCategory.Visible = false;
                    txtSuperCategory.Visible = true;
                    btnCreateSuperCategory.Visible = true;
                    btnBackToSelectSuperCategory.Visible = true;
                    btnSubmitSuperCategoryChanges.Visible = false;
                    btnEditSuperCategory.Visible = false;
                    superTitle = "Create SuperCategory";
                    MegaDiv.Visible = false;
                    CatDiv.Visible = false;
                    SubDiv.Visible = false;
                    PreviewDiv.Visible = true;
                    ShowPreview(2);
                }
                else
                {
                    //Create Category Page
                    if(ddlCategory.SelectedIndex == 1)
                    {
                        ddlCategory.Visible = false;
                        txtCreateCategory.Visible = true;
                        btnCreateCategory.Visible = true;
                        btnBackToSelectCategory.Visible = true;
                        btnSubmitCategoryChanges.Visible = false;
                        btnEditCategory.Visible = false;
                        catTitle = "Create Category";
                        MegaDiv.Visible = false;
                        SuperDiv.Visible = false;
                        SubDiv.Visible = false;
                        PreviewDiv.Visible = true;
                        ShowPreview(3);
                    }
                    else
                    {
                        //Create SubCategory Page
                        if (ddlSubCategory.SelectedIndex == 1)
                        {
                            if (ddlCategory.Text != "")
                            {
                                ddlSubCategory.Visible = false;
                                txtCreateSubCategory.Visible = true;
                                btnCreateSubCategory.Visible = true;
                                btnBackToSelectSubCategory.Visible = true;
                                btnSubmitSubCategoryChanges.Visible = false;
                                btnEditSubCategory.Visible = false;
                                subTitle = "Create SubCategory";
                                MegaDiv.Visible = false;
                                SuperDiv.Visible = false;
                                CatDiv.Visible = false;
                                PreviewDiv.Visible = true;
                                ShowPreview(4);
                            }
                            else
                            {
                                // error message for selecting a subCat without a Cat
                                ddlSubCategory.SelectedIndex = 0;
                                errorMessage.Insert(0, "First Select a Category");
                                lblErrorMessage.Text = string.Join("<br />", errorMessage);
                                lblErrorMessage.Visible = true;
                            }
                        }
                        else
                        {
                            //Select Page
                            if (ddlCategory.SelectedIndex == 0)
                            {
                                PreviewDiv.Visible = false;
                                SuperDiv.Visible = true;
                                CatDiv.Visible = true;
                                SubDiv.Visible = false;
                                txtSuperCategory.Visible = false;
                                btnCreateSuperCategory.Visible = false;
                                btnBackToSelectSuperCategory.Visible = false;
                                txtCreateCategory.Visible = false;
                                btnCreateCategory.Visible = false;
                                btnBackToSelectCategory.Visible = false;
                                txtCreateSubCategory.Visible = false;
                                btnCreateSubCategory.Visible = false;
                                btnBackToSelectSubCategory.Visible = false;
                                btnSubmitCategoryChanges.Visible = false;
                                btnSubmitMegaCategoryChanges.Visible = false;
                                btnSubmitSuperCategoryChanges.Visible = false;
                                btnSubmitSubCategoryChanges.Visible = false;
                            }
                            else
                            {
                                PreviewDiv.Visible = false;
                                SuperDiv.Visible = true;
                                CatDiv.Visible = true;
                                SubDiv.Visible = true;
                                txtSuperCategory.Visible = false;
                                btnCreateSuperCategory.Visible = false;
                                btnBackToSelectSuperCategory.Visible = false;
                                txtCreateCategory.Visible = false;
                                btnCreateCategory.Visible = false;
                                btnBackToSelectCategory.Visible = false;
                                txtCreateSubCategory.Visible = false;
                                btnCreateSubCategory.Visible = false;
                                btnBackToSelectSubCategory.Visible = false;
                                btnSubmitCategoryChanges.Visible = false;
                                btnSubmitMegaCategoryChanges.Visible = false;
                                btnSubmitSuperCategoryChanges.Visible = false;
                                btnSubmitSubCategoryChanges.Visible = false;
                                
                            } 
                            // Hide Edit button of if the ddl equals '' or --Add New--
                            if (ddlSubCategory.SelectedIndex > 1)
                            {
                                btnEditSubCategory.Visible = true;
                            }
                            else
                            {
                                btnEditSubCategory.Visible = false;
                            } // Select Page
                        } // SubCat
                        if (ddlCategory.SelectedIndex > 1)
                        {
                            btnEditCategory.Visible = true;
                        }
                        else
                        {
                            btnEditCategory.Visible = false;
                        }
                    } // Cat
                    if (ddlSuperCategory.SelectedIndex > 1)
                    {
                        btnEditSuperCategory.Visible = true;
                    }
                    else
                    {
                        btnEditSuperCategory.Visible = false;
                    }
                } // Super
                if (ddlMegaCategory.SelectedIndex > 1)
                {
                    btnEditMegaCategory.Visible = true;
                }
                else
                {
                    btnEditMegaCategory.Visible = false;
                }
            } // Mega
        } // !Postback
    }

    // Fill Drop Down Lists
    protected void ShowMegaCategories()
    {
        sds.ConnectionString = Resources.ConnectionStrings.ATHENA;
        sds.SelectCommand = Resources.CatalogManagerSQLQueries.GetMegaCatList;
        ddlMegaCategory.DataSource = sds;
        ddlMegaCategory.DataTextField = "MegaCategory";
        ddlMegaCategory.DataValueField = "MegaCategory";
        ddlMegaCategory.DataBind();
        ddlMegaCategory.Items.Insert(0, new ListItem("", ""));
        ddlMegaCategory.Items.Insert(1, new ListItem("--Add New--", "--Add New--"));
    }

    protected void ShowSuperCategories()
    {
        sds.ConnectionString = Resources.ConnectionStrings.ATHENA;
        sds.SelectCommand = Resources.CatalogManagerSQLQueries.OrderBySuper;
        ddlSuperCategory.DataSource = sds;
        ddlSuperCategory.DataTextField = "SuperCategory";
        ddlSuperCategory.DataValueField = "SuperCategory";
        ddlSuperCategory.DataBind();
        if(ddlSuperCategory.Items.FindByValue("") == null)
        {
            ddlSuperCategory.Items.Insert(0, new ListItem("", ""));
            ddlSuperCategory.Items.Insert(1, new ListItem("--Add New--", "--Add New--"));
        }
        else
        {
            ddlSuperCategory.Items.Insert(1, new ListItem("--Add New--", "--Add New--"));
        }
    }

    protected void ShowCategories()
    {
        if (ddlMegaCategory.Text != "")
        {
            sds.ConnectionString = Resources.ConnectionStrings.ATHENA;
            if (ddlSuperCategory.Text == "")
            {
                sds.SelectCommand = Resources.CatalogManagerSQLQueries.SelectMega;
            }
            else if (ddlSuperCategory.Text != "")
            {
                sds.SelectCommand = Resources.CatalogManagerSQLQueries.SelectMegaWithSuper;
            }
            ddlCategory.DataSource = sds;
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "Category";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("", ""));
            ddlCategory.Items.Insert(1, new ListItem("--Add New--", "--Add New--"));
        }
    }

    protected void ShowSubCategories()
    {
        sds.ConnectionString = Resources.ConnectionStrings.ATHENA;
        if (ddlCategory.Text != "")
        {
            sds.SelectCommand = Resources.CatalogManagerSQLQueries.SelectSubWithCat;
            ddlSubCategory.DataSource = sds;
            ddlSubCategory.DataTextField = "SubCategory";
            ddlSubCategory.DataValueField = "SubCategory";
            ddlSubCategory.DataBind();
            if (ddlSubCategory.Items.FindByValue("") == null)
            {
                ddlSubCategory.Items.Insert(0, new ListItem("", ""));
                ddlSubCategory.Items.Insert(1, new ListItem("--Add New--", "--Add New--"));
            }
            else
            {
                ddlSubCategory.Items.Insert(1, new ListItem("--Add New--", "--Add New--"));
            }
        }

    }

    //Fill Preview Window
    protected void ShowPreview(int instance)
    {
        int caseSwitch = instance;
        txtPreviewWindow.Text = "";
        SqlConnection conn = new SqlConnection(Resources.ConnectionStrings.ATHENA);
        SqlCommand command = new SqlCommand();
        VariantsArray[] va = null;
        switch (caseSwitch)
        {
            case 1:
                command.CommandText = Resources.CatalogManagerSQLQueries.Mega;
                previewTitle = "MegaCategory";
                break;
            case 2:
                command.CommandText = Resources.CatalogManagerSQLQueries.Super;
                previewTitle = "SuperCategory";
                break;
            case 3:
                if (ddlMegaCategory.Text != "" && ddlSuperCategory.Text == "")
                {
                    command.CommandText = Resources.CatalogManagerSQLQueries.CatWithMega;
                    previewTitle = "Category";
                }
                else if (ddlMegaCategory.Text != "" && ddlSuperCategory.Text != "")
                {
                    command.CommandText = Resources.CatalogManagerSQLQueries.CatWithMegaAndSuper;
                    previewTitle = "Category";
                }
                break;
            case 4:
                command.CommandText = Resources.CatalogManagerSQLQueries.SubWithCat;
                previewTitle = "SubCategory";
                break;
        }
        command.Connection = conn;
        conn.Open();
        using (var reader = command.ExecuteReader())
        {
            var list = new List<VariantsArray>();
            while (reader.Read())
            {
                list.Add(new VariantsArray { variant = reader.GetString(0) });
                va = list.ToArray();
            }

            if (va != null)
            {
                foreach (var i in va)
                {
                    txtPreviewWindow.Text = txtPreviewWindow.Text + i.variant.ToString() + "\n";
                }
            }
        }
        conn.Dispose();
        
    }

    protected class VariantsArray
    {
        public string variant { get; set; }
    }

    // Reload drop down lists upon event change
    protected void ddlMegaCategory_TextChanged(object sender, EventArgs e)
    {
        ShowSuperCategories();
        ShowCategories();
        ShowSubCategories();
        SubDiv.Visible = false;
    }

    protected void ddlSuperCategory_TextChanged(object sender, EventArgs e)
    {
        ShowCategories();
        ShowSubCategories();
        SubDiv.Visible = false;
        btnEditCategory.Visible = false;
    }

    protected void ddlCategory_TextChanged(object sender, EventArgs e)
    {
        ShowSubCategories();
        btnEditSubCategory.Visible = false;
    }

    // Create Buttons
    protected void btnAddMegaCategory_Click(object sender, EventArgs e)
    {
        string vettedTxtCreateMegaCategory1 = Regex.Replace(txtMegaCategory.Text.Trim(), "[^a-zA-Z0-9 ]+", "");
        //string furtherVettedTxtCreateMegaCategory = vettedTxtCreateMegaCategory.Trim();
        //if (furtherVettedTxtCreateMegaCategory.Length == txtMegaCategory.Text.Length)
        //{
            try
            {
                string vettedTxtCreateMegaCategory2 = Regex.Replace(vettedTxtCreateMegaCategory1.ToLower(), "[ \n\t]", "");
                if (vettedTxtCreateMegaCategory2 != null && vettedTxtCreateMegaCategory2 != "" && vettedTxtCreateMegaCategory2.Length <= 75)
                {
                    sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                    sds.SelectCommand = Resources.CatalogManagerSQLQueries.FormattedMega;
                    DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                    dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                    dssa.RetrieveTotalRowCount = true;
                    DataView dv = (DataView)sds.Select(dssa);

                    if (dv.Count == 0)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand(Resources.CatalogManagerSQLQueries.InsertNewMegaCategory);
                        command.Connection = new SqlConnection(sds.ConnectionString);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Mega", SqlDbType.NVarChar, 50).Value = txtMegaCategory.Text;

                        adapter.InsertCommand = command;
                        command.Connection.Open();
                        adapter.InsertCommand.ExecuteNonQuery();
                        command.Connection.Dispose();
                    }
                    else
                    {
                        errorMessage.Insert(0, "You're doing it wrong");
                        lblErrorMessage.Text = string.Join("<br />", errorMessage);
                        lblErrorMessage.Visible = true;
                    }
                }
                else
                {
                    errorMessage.Insert(0, "You're doing it wrong");
                    lblErrorMessage.Text = string.Join("<br />", errorMessage);
                    lblErrorMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                errorMessage.Insert(0, "The machine is confused");
                lblErrorMessage.Text = string.Join("<br />", errorMessage);
                lblErrorMessage.Visible = true;
                txtMegaCategory.Text = String.Empty;
                ddlMegaCategory.SelectedIndex = 0;
                ddlSuperCategory.SelectedIndex = 0;
                ddlCategory.SelectedIndex = 0;
                ddlSubCategory.SelectedIndex = 0;
            }
        //}
        //else
        //{
        //    errorMessage.Insert(0, "You're doing it wrong");
        //    lblErrorMessage.Text = string.Join("<br />", errorMessage);
        //    lblErrorMessage.Visible = true;
        //    txtMegaCategory.Text = String.Empty;
        //}
        txtMegaCategory.Text = String.Empty;
    }

    protected void btnAddSuperCategory_Click(object sender, EventArgs e)
    {
        string vettedTxtCreateSuperCategory1 = Regex.Replace(txtSuperCategory.Text.Trim(), "[^a-zA-Z0-9 ]+", "");
        //string furtherVettedTxtCreateSuperCategory = vettedTxtCreateSuperCategory.Trim();
        //if (furtherVettedTxtCreateSuperCategory.Length == txtSuperCategory.Text.Length)
        //{
            try
            {
                string vettedTxtCreateSuperCategory2 = Regex.Replace(vettedTxtCreateSuperCategory1.ToLower(), "[ \n\t]", "");
                if (vettedTxtCreateSuperCategory2 != null && vettedTxtCreateSuperCategory2 != "" && vettedTxtCreateSuperCategory2.Length <= 75 && vettedTxtCreateSuperCategory2.Length >= 4)
                {
                    sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                    sds.SelectCommand = Resources.CatalogManagerSQLQueries.FormattedSuper;
                    DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                    dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                    dssa.RetrieveTotalRowCount = true;
                    DataView dv = (DataView)sds.Select(dssa);

                    if (dv.Count == 0)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand(Resources.CatalogManagerSQLQueries.InsertNewSuperCategory);
                        command.Connection = new SqlConnection(sds.ConnectionString);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Super", SqlDbType.NVarChar, 50).Value = txtSuperCategory.Text;

                        adapter.InsertCommand = command;
                        command.Connection.Open();
                        adapter.InsertCommand.ExecuteNonQuery();
                        command.Connection.Dispose();
                    }
                    else
                    {
                        errorMessage.Insert(0, "You're doing it wrong");
                        lblErrorMessage.Text = string.Join("<br />", errorMessage);
                        lblErrorMessage.Visible = true;
                    }
                }
                else
                {
                    errorMessage.Insert(0, "You're doing it wrong");
                    lblErrorMessage.Text = string.Join("<br />", errorMessage);
                    lblErrorMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                errorMessage.Insert(0, "The machine is confused");
                lblErrorMessage.Text = string.Join("<br />", errorMessage);
                lblErrorMessage.Visible = true;
                txtSuperCategory.Text = String.Empty;
                ddlMegaCategory.SelectedIndex = 0;
                ddlSuperCategory.SelectedIndex = 0;
                ddlCategory.SelectedIndex = 0;
                ddlSubCategory.SelectedIndex = 0;
            }
        txtSuperCategory.Text = String.Empty;
    }

    protected void btnAddCategory_Click(object sender, EventArgs e) 
    {
        string vettedTxtCreateCategory1 = Regex.Replace(txtCreateCategory.Text.Trim(), "[^a-zA-Z0-9 ]+", "");
            try
            {
                string vettedTxtCreateCategory2 = Regex.Replace(vettedTxtCreateCategory1.ToLower(), "[ \n\t]", "");
                if (vettedTxtCreateCategory2 != null && vettedTxtCreateCategory2 != "" && vettedTxtCreateCategory2.Length <= 75 && vettedTxtCreateCategory2.Length >= 4)
                {
                    sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                    sds.SelectCommand = Resources.CatalogManagerSQLQueries.FormattedCat;
                    DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                    dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                    dssa.RetrieveTotalRowCount = true;
                    DataView dv = (DataView)sds.Select(dssa);

                    if (dv.Count == 0)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand(Resources.CatalogManagerSQLQueries.InsertNewCategory);
                        command.Connection = new SqlConnection(sds.ConnectionString);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Mega", SqlDbType.NVarChar, 50).Value = ddlMegaCategory.Text;
                        command.Parameters.Add("@Super", SqlDbType.NVarChar, 50).Value = ddlSuperCategory.Text;
                        command.Parameters.Add("@Cat", SqlDbType.NVarChar, 50).Value = txtCreateCategory.Text;

                        adapter.InsertCommand = command;
                        command.Connection.Open();
                        adapter.InsertCommand.ExecuteNonQuery();
                        command.Connection.Dispose();
                    }
                    else
                    {
                        errorMessage.Insert(0, "You're doing it wrong");
                        lblErrorMessage.Text = string.Join("<br />", errorMessage);
                        lblErrorMessage.Visible = true;
                    }
                }
                else
                {
                    errorMessage.Insert(0, "You're doing it wrong");
                    lblErrorMessage.Text = string.Join("<br />", errorMessage);
                    lblErrorMessage.Visible = true;
                }
            }
            catch (Exception ex) 
            {
                errorMessage.Insert(0, "The machine is confused");
                lblErrorMessage.Text = string.Join("<br />", errorMessage);
                lblErrorMessage.Visible = true;
                txtCreateCategory.Text = String.Empty;
                ddlMegaCategory.SelectedIndex = 0;
                ddlSuperCategory.SelectedIndex = 0;
                ddlCategory.SelectedIndex = 0;
                ddlSubCategory.SelectedIndex = 0;
            }
        txtCreateCategory.Text = String.Empty;
    }

    protected void btnAddSubCategory_Click(object sender, EventArgs e)
    {
        string vettedTxtCreateSubCategory1 = Regex.Replace(txtCreateSubCategory.Text.Trim(), "[^a-zA-Z0-9 ]+", "");
            try
            {
                string vettedTxtCreateSubCategory2 = Regex.Replace(vettedTxtCreateSubCategory1.ToLower(), "[ \n\t]", "");
                if (vettedTxtCreateSubCategory2 != null && vettedTxtCreateSubCategory2 != "" && vettedTxtCreateSubCategory2.Length <= 75 && vettedTxtCreateSubCategory2.Length >= 4)
                {
                    sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                    sds.SelectCommand = Resources.CatalogManagerSQLQueries.FormattedSub;
                    DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                    dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                    dssa.RetrieveTotalRowCount = true;
                    DataView dv = (DataView)sds.Select(dssa);

                    if (dv.Count == 0)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand(Resources.CatalogManagerSQLQueries.InsertNewSubCategory);
                        command.Connection = new SqlConnection(sds.ConnectionString);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cat", SqlDbType.NVarChar, 50).Value = ddlCategory.Text;
                        command.Parameters.Add("@Sub", SqlDbType.NVarChar, 50).Value = txtCreateSubCategory.Text;

                        adapter.InsertCommand = command;
                        command.Connection.Open();
                        adapter.InsertCommand.ExecuteNonQuery();
                        command.Connection.Dispose();
                    }
                    else
                    {
                        errorMessage.Insert(0, "You're doing it wrong");
                        lblErrorMessage.Text = string.Join("<br />", errorMessage);
                        lblErrorMessage.Visible = true;
                    }
                }
                else
                {
                    errorMessage.Insert(0, "You're doing it wrong");
                    lblErrorMessage.Text = string.Join("<br />", errorMessage);
                    lblErrorMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                errorMessage.Insert(0, "The machine is confused");
                lblErrorMessage.Text = string.Join("<br />", errorMessage);
                lblErrorMessage.Visible = true;
                txtCreateSubCategory.Text = String.Empty;
                ddlMegaCategory.SelectedIndex = 0;
                ddlSuperCategory.SelectedIndex = 0;
                ddlCategory.SelectedIndex = 0;
                ddlSubCategory.SelectedIndex = 0;
            }
        txtCreateSubCategory.Text = String.Empty;
    }

    // GoBack Buttons
    protected void btnBackToSelectMegaCategory_Click(object sender, EventArgs e)
    {
        btnBackToSelectMegaCategory.Visible = false;
        btnCreateMegaCategory.Visible = false;
        txtMegaCategory.Visible = false;
        ddlMegaCategory.Visible = true;
        ddlMegaCategory.SelectedIndex = 0;
        megaTitle = "Select MegaCategory";
        PreviewDiv.Visible = false;
        btnSubmitMegaCategoryChanges.Visible = false;
        btnEditMegaCategory.Visible = false;
        SuperDiv.Visible = false;
        CatDiv.Visible = false;
        SubDiv.Visible = false;
        txtMegaCategory.Text = String.Empty;
    }

    protected void btnBackToSelectSuperCategory_Click(object sender, EventArgs e)
    {
        MegaDiv.Visible = true;
        SuperDiv.Visible = true;
        btnEditMegaCategory.Visible = true;
        btnBackToSelectSuperCategory.Visible = false;
        btnCreateSuperCategory.Visible = false;
        txtSuperCategory.Visible = false;
        ddlSuperCategory.Visible = true;
        ddlSuperCategory.SelectedIndex = 0;
        superTitle = "Select SuperCategory";
        CatDiv.Visible = true;
        SubDiv.Visible = false;
        PreviewDiv.Visible = false;
        btnSubmitSuperCategoryChanges.Visible = false;
        btnEditSuperCategory.Visible = false;
        btnEditCategory.Visible = false;
        txtSuperCategory.Text = String.Empty;
        ShowCategories();
    }

    protected void btnBackToSelectCategory_Click(object sender, EventArgs e)
    {
        MegaDiv.Visible = true;
        CatDiv.Visible = true;
        btnEditMegaCategory.Visible = true;
        SuperDiv.Visible = true;
        SubDiv.Visible = false;
        btnBackToSelectCategory.Visible = false;
        btnCreateCategory.Visible = false;
        txtCreateCategory.Visible = false;
        ddlCategory.Visible = true;
        ddlCategory.SelectedIndex = 0;
        catTitle = "Select Category";
        PreviewDiv.Visible = false;
        btnSubmitCategoryChanges.Visible = false;
        btnEditCategory.Visible = false;
        btnEditSubCategory.Visible = false;
        txtCreateCategory.Text = String.Empty;
        if(ddlSuperCategory.SelectedIndex > 1)
        {
            btnEditSuperCategory.Visible = true;
        }
    }

    protected void btnBackToSelectSubCategory_Click(object sender, EventArgs e)
    {
        MegaDiv.Visible = true;
        btnEditMegaCategory.Visible = true;
        SuperDiv.Visible = true;
        CatDiv.Visible = true;
        SubDiv.Visible = true;
        btnEditCategory.Visible = true;
        btnBackToSelectSubCategory.Visible = false;
        btnCreateSubCategory.Visible = false;
        txtCreateSubCategory.Visible = false;
        ddlSubCategory.Visible = true;
        ddlSubCategory.SelectedIndex = 0;
        subTitle = "Select SubCategory";
        PreviewDiv.Visible = false;
        btnSubmitSubCategoryChanges.Visible = false;
        btnEditSubCategory.Visible = false;
        txtCreateSubCategory.Text = String.Empty;
        if (ddlSuperCategory.SelectedIndex > 1)
        {
            btnEditSuperCategory.Visible = true;
        }
    }

    // edit buttons
    protected void btnEditMegaCategory_Click(object sender, EventArgs e)
    {
        ddlMegaCategory.Visible = false;
        btnEditMegaCategory.Visible = false;
        txtMegaCategory.Visible = true;
        btnSubmitMegaCategoryChanges.Visible = true;
        btnBackToSelectMegaCategory.Visible = true;
        megaTitle = "Edit Mega Category";
        SuperDiv.Visible = false;
        CatDiv.Visible = false;
        SubDiv.Visible = false;
        txtMegaCategory.Text = ddlMegaCategory.Text;
        string preEditState = ddlMegaCategory.Text;
        Session["preEditState"] = preEditState;
    }

    protected void btnEditSuperCategory_Click(object sender, EventArgs e)
    {
        MegaDiv.Visible = false;
        ddlSuperCategory.Visible = false;
        btnEditSuperCategory.Visible = false;
        txtSuperCategory.Visible = true;
        btnSubmitSuperCategoryChanges.Visible = true;
        btnBackToSelectSuperCategory.Visible = true;
        superTitle = "Edit Super Category";
        btnEditMegaCategory.Visible = false;
        CatDiv.Visible = false;
        SubDiv.Visible = false;
        txtSuperCategory.Text = ddlSuperCategory.Text;
        string preEditState = ddlMegaCategory.Text;
        string preEditState2 = ddlSuperCategory.Text;
        Session["preEditState"] = preEditState;
        Session["preEditState2"] = preEditState2;
    }

    protected void btnEditCategory_Click(object sender, EventArgs e)
    {
        ddlCategory.Visible = false;
        btnEditCategory.Visible = false;
        txtCreateCategory.Visible = true;
        btnSubmitCategoryChanges.Visible = true;
        btnBackToSelectCategory.Visible = true;
        catTitle = "Edit Category";
        btnEditMegaCategory.Visible = false;
        btnEditSuperCategory.Visible = false;
        SubDiv.Visible = false;
        txtCreateCategory.Text = ddlCategory.Text;
        string preEditState = ddlMegaCategory.Text;
        string preEditState2 = ddlSuperCategory.Text;
        string preEditState3 = ddlCategory.Text;
        Session["preEditState"] = preEditState;
        Session["preEditState2"] = preEditState2;
        Session["preEditState3"] = preEditState3;
    }

    protected void btnEditSubCategory_Click(object sender, EventArgs e)
    {
        ddlSubCategory.Visible = false;
        btnEditSubCategory.Visible = false;
        txtCreateSubCategory.Visible = true;
        btnSubmitSubCategoryChanges.Visible = true;
        btnBackToSelectSubCategory.Visible = true;
        subTitle = "Edit Sub Category";
        MegaDiv.Visible = false;
        SuperDiv.Visible = false;
        btnEditCategory.Visible = false;
        txtCreateSubCategory.Text = ddlSubCategory.Text;
        string preEditState = ddlCategory.Text;
        string preEditState2 = ddlSubCategory.Text;
        Session["preEditState"] = preEditState;
        Session["preEditState2"] = preEditState2;
    }

    // Submit buttons
    protected void btnSubmitMegaCategoryChanges_Click(object sender, EventArgs e)
    {
        string preEditState = (string)(Session["preEditState"]);
        string vettedtxtMegaCategory = Regex.Replace(txtMegaCategory.Text.ToLower(), "[ \t\n]+", "");
        try
        {
            if (vettedtxtMegaCategory != null && vettedtxtMegaCategory != "" && vettedtxtMegaCategory.Length <= 50 && txtMegaCategory.Text != ddlMegaCategory.Text)
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.SelectCommand = Resources.CatalogManagerSQLQueries.VerifyMega;
                DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                dssa.RetrieveTotalRowCount = true;
                DataView dv = (DataView)sds.Select(dssa);

                if (dv.Count == 0)
                {
                    sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateMega;
                    sds.Update();
                    sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertMega;
                    sds.Insert();
                }
            }
            preEditState = String.Empty;
            txtMegaCategory.Text = String.Empty;
        }
        catch (InvalidCastException ex) { }
    }

    protected void btnSubmitSuperCategoryChanges_Click(object sender, EventArgs e)
    {
        string preEditState = (string)(Session["preEditState"]);
        string preEditState2 = (string)(Session["preEditState2"]);
        string vettedtxtSuperCategory = Regex.Replace(txtSuperCategory.Text.ToLower(), "[ \t\n]+", "");
        try
        {
            if (vettedtxtSuperCategory != null && vettedtxtSuperCategory != "" && vettedtxtSuperCategory.Length <= 50 && txtSuperCategory.Text != ddlSuperCategory.Text)
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.SelectCommand = Resources.CatalogManagerSQLQueries.VerifySuper;
                DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                dssa.RetrieveTotalRowCount = true;
                DataView dv = (DataView)sds.Select(dssa);
                if (preEditState == ddlMegaCategory.Text)
                {
                    if (dv.Count == 0)
                    {
                        sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateSuper;
                        sds.Update();
                        sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertSuper;
                        sds.Insert();
                        
                    }
                }
            }
            preEditState = String.Empty;
            preEditState2 = String.Empty;
            txtSuperCategory.Text = String.Empty;
        }
        catch (InvalidCastException ex) { }
    }

    protected void btnSubmitCategoryChanges_Click(object sender, EventArgs e)
    {
        string preEditState = (string)(Session["preEditState"]);
        string preEditState2 = (string)(Session["preEditState2"]);
        string preEditState3 = (string)(Session["preEditState3"]);
        string vettedtxtCategory = Regex.Replace(txtCreateCategory.Text.ToLower(), "[ \t\n]+", "");
        try
        {
            if (vettedtxtCategory != null && vettedtxtCategory != "" && vettedtxtCategory.Length <= 50 && txtCreateCategory.Text != ddlCategory.Text)
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.SelectCommand = Resources.CatalogManagerSQLQueries.VerifyCat;
                DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                dssa.RetrieveTotalRowCount = true;
                DataView dv = (DataView)sds.Select(dssa);
                if (preEditState == ddlMegaCategory.Text && preEditState2 == ddlSuperCategory.Text)
                {
                    if (dv.Count == 0)
                    {
                        sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertCatWithSuper;
                        sds.Insert();

                        sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateCatWithSuper;
                        sds.Update();

                        sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateCatInGlobalProduct;
                        sds.Update();

                        sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertCatIntoLog;
                        sds.Insert();
                    }
                }
                if (preEditState != ddlMegaCategory.Text && ddlMegaCategory.SelectedIndex > 1)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand(Resources.CatalogManagerSQLQueries.UpdateMegaCatUnderEditCat);
                    command.Connection = new SqlConnection(sds.ConnectionString);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@MegaCatInDDL", SqlDbType.NVarChar, 50).Value = ddlMegaCategory.Text;
                    command.Parameters.Add("@CatTxt", SqlDbType.NVarChar, 50).Value = txtCreateCategory.Text;

                    adapter.InsertCommand = command;
                    command.Connection.Open();
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Connection.Dispose();
                }
                if (preEditState2 != ddlSuperCategory.Text && ddlSuperCategory.Text != "--Add New--")
                {
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    SqlCommand command2 = new SqlCommand(Resources.CatalogManagerSQLQueries.UpdateSuperCatUnderEditCat);
                    command2.Connection = new SqlConnection(sds.ConnectionString);
                    command2.CommandType = CommandType.StoredProcedure;

                    command2.Parameters.Add("@SuperCatInDDL", SqlDbType.NVarChar, 50).Value = ddlSuperCategory.Text;
                    command2.Parameters.Add("@CatTxt", SqlDbType.NVarChar, 50).Value = txtCreateCategory.Text;

                    adapter2.InsertCommand = command2;
                    command2.Connection.Open();
                    adapter2.InsertCommand.ExecuteNonQuery();
                    command2.Connection.Dispose();
                }
            }
            preEditState = String.Empty;
            preEditState2 = String.Empty;
            preEditState3 = String.Empty;
        }
        catch (InvalidCastException ex) { }
    }

    protected void btnSubmitSubCategoryChanges_Click(object sender, EventArgs e)
    {
        string preEditState = (string)(Session["preEditState"]);
        string preEditState2 = (string)(Session["preEditState2"]);
        string vettedtxtSubCategory = Regex.Replace(txtCreateSubCategory.Text.ToLower(), "[ \t\n]+", "");
        try
        {
            if (vettedtxtSubCategory != null && vettedtxtSubCategory != "" && vettedtxtSubCategory.Length <= 50 && txtCreateSubCategory.Text != ddlSubCategory.Text)
            {
                sds.ConnectionString = Resources.ConnectionStrings.ATHENA;

                sds.SelectCommand = Resources.CatalogManagerSQLQueries.VerifySubCat;
                DataSourceSelectArguments dssa = new DataSourceSelectArguments();
                dssa.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
                dssa.RetrieveTotalRowCount = true;
                DataView dv = (DataView)sds.Select(dssa);
                if (preEditState == ddlCategory.Text)
                {
                    if (dv.Count == 0)
                    {
                        sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertSubCat;
                        sds.Insert();

                        sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateSubCat;
                        sds.Update();

                        sds.UpdateCommand = Resources.CatalogManagerSQLQueries.UpdateSubCatInGlobalProduct;
                        sds.Update();

                        sds.InsertCommand = Resources.CatalogManagerSQLQueries.InsertSubCatIntoLog;
                        sds.Insert();
                    }
                }
                if (preEditState != ddlCategory.Text && ddlCategory.SelectedIndex > 1)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand(Resources.CatalogManagerSQLQueries.UpdateCatUnderEditSubCat);
                    command.Connection = new SqlConnection(sds.ConnectionString);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@CatInDDL", SqlDbType.NVarChar, 50).Value = ddlCategory.Text;
                    command.Parameters.Add("@SubCatTxt", SqlDbType.NVarChar, 50).Value = txtCreateSubCategory.Text;

                    adapter.InsertCommand = command;
                    command.Connection.Open();
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Connection.Dispose();
                }
            }
            preEditState = String.Empty;
            preEditState2 = String.Empty;
        }
        catch (InvalidCastException ex) { }
    }
}