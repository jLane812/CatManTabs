<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CategoryManager.aspx.cs" Inherits="Admin_CategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <h1 style="font-size:3.5rem;line-height:2;text-align:left">Create/Edit Categories</h1>
        <div class="row row-offcanvas row-offcanvas-right">
            <div class="col-12 col-md-9">
                <div class="row">
                    <div ID="MegaDiv" runat="server">
                        <div class="col-6 col-lg-6">
                            <h3><%= this.megaTitle %></h3>
                            <asp:DropDownList ID="ddlMegaCategory" runat="server" ontextchanged="ddlMegaCategory_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                            <asp:Button ID="btnEditMegaCategory" runat="server" Text="Edit" OnClick="btnEditMegaCategory_Click" /> 
                            <asp:TextBox ID="txtMegaCategory" Width="300px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnCreateMegaCategory" runat="server" Text="Create" OnClick="btnAddMegaCategory_Click"/>
                            <asp:Button ID="btnSubmitMegaCategoryChanges" runat="server" Text="Submit" OnClick ="btnSubmitMegaCategoryChanges_Click" />
                            <asp:Button ID="btnBackToSelectMegaCategory" runat="server" Text="Back" OnClick="btnBackToSelectMegaCategory_Click"/>
                        </div>
                    </div>
                    <div ID="SuperDiv" runat="server">
                        <div class="col-6 col-lg-8">
                            <h3><%= this.superTitle %></h3>
                            <asp:DropDownList ID="ddlSuperCategory" runat="server" ontextchanged="ddlSuperCategory_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                            <asp:Button ID="btnEditSuperCategory" runat="server" Text="Edit" OnClick="btnEditSuperCategory_Click" /> 
                            <asp:TextBox ID="txtSuperCategory" Width="300px" runat="server"></asp:TextBox>
                            <asp:Button ID="btnCreateSuperCategory" runat="server" Text="Create" OnClick="btnAddSuperCategory_Click"/>
                            <asp:Button ID="btnSubmitSuperCategoryChanges" runat="server" Text="Submit" OnClick ="btnSubmitSuperCategoryChanges_Click" />
                            <asp:Button ID="btnBackToSelectSuperCategory" runat="server" Text="Back" OnClick="btnBackToSelectSuperCategory_Click"/>
                        </div>
                    </div>
                    <div ID="CatDiv" runat="server">
                        <div class="col-6 col-lg-10">
                            <h3><%= this.catTitle %></h3>
                            <asp:DropDownList ID="ddlCategory" runat="server" ontextchanged="ddlCategory_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                            <asp:Button ID="btnEditCategory" runat="server" Text="Edit" OnClick="btnEditCategory_Click" /> 
                            <asp:TextBox ID="txtCreateCategory" Width="300px" runat="server" ></asp:TextBox>
                            <asp:Button ID="btnCreateCategory" runat="server" Text="Create" OnClick="btnAddCategory_Click"/>
                            <asp:Button ID="btnSubmitCategoryChanges" runat="server" Text="Submit" OnClick ="btnSubmitCategoryChanges_Click" />
                            <asp:Button ID="btnBackToSelectCategory" runat="server" Text="Back" OnClick="btnBackToSelectCategory_Click"/>
                        </div>
                    </div>
                    <div ID="SubDiv" runat="server">
                        <div class="col-6 col-lg-10">
                            <h3><%= this.subTitle %></h3>
                            <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" ></asp:DropDownList>
                            <asp:Button ID="btnEditSubCategory" runat="server" Text="Edit" OnClick="btnEditSubCategory_Click" /> 
                            <asp:TextBox ID="txtCreateSubCategory" Width="300px" runat="server" ></asp:TextBox>
                            <asp:Button ID="btnCreateSubCategory" runat="server" Text="Create" OnClick="btnAddSubCategory_Click"/>
                            <asp:Button ID="btnSubmitSubCategoryChanges" runat="server" Text="Submit" OnClick ="btnSubmitSubCategoryChanges_Click" />
                            <asp:Button ID="btnBackToSelectSubCategory" runat="server" Text="Back" OnClick="btnBackToSelectSubCategory_Click"/>
                        </div>
                    </div>     
                </div>
            </div>
            <div ID="PreviewDiv" class="col-6 col-md-3" runat="server">
                <h3>Preview of <%= this.previewTitle %></h3>
                <div style="padding-top:2px">
                    <asp:TextBox ID="txtPreviewWindow" runat="server" Height="300px" Width="300px" ReadOnly="true" TextMode="MultiLine" BorderWidth="4px" BorderColor="#e6e6e6"></asp:TextBox>
                </div>
            </div>
        </div>
        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="#FF3300" EnableViewState="False"></asp:Label>
     </div>
</asp:Content>

