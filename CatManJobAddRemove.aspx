<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CatManJobAddRemove.aspx.cs" Inherits="Admin_CatManJobAddRemove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <h1 style="font-size:3.5rem;line-height:2;text-align:left">Create/Edit Jobs</h1>
        <div class="row">
            <div class="col-xs-3"></div>
            <div class="col-xs-7">
                <h3>Create Job</h3>
                <asp:TextBox ID="txtCreateJob" runat="server" Width="300px"></asp:TextBox>
                <asp:Button ID="btnCreateJob" runat="server" Text="Create" OnClick="btnAddCatManJob_Click"/>
            </div>
            <div class="col-xs-6 col-md-4"></div>
        </div>
        <div class="row">
            <div class="col-xs-3"></div>
            <div class="col-xs-7">
                <h3>Select Job</h3>
                <asp:DropDownList ID="ddlJobs" runat="server" AutoPostBack="true" ></asp:DropDownList>
            </div>
            <div class="col-xs-6 col-md-4"></div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-md-3">
                <h3>Edit Job Variants</h3>
                <div style="vertical-align:top">
                    <asp:TextBox ID="txtEditJobVariants" runat="server" Height="300px" TextMode="MultiLine"></asp:TextBox>
                    <asp:Button ID="btnAddVariants" runat="server" Text="Add Variants" OnClick="btnAddVariants_Click"/>
                    <asp:Button ID="btnRemoveVariants" runat="server" Text="Remove Variants" OnClick="btnRemoveVariants_Click"/>
                </div>
            </div>
            <div class="col-xs-4"></div>
            <div class="col-xs-6 col-md-3">
                <h3>Preview Job Variants</h3>
                <div style="padding-top:2px" >
                    <asp:TextBox ID="txtPreviewJobVariants" runat="server" Height="300px" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
