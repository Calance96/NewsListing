<%@ Page Title="Add News Article" Language="C#" MasterPageFile="~/template/Author.Master" AutoEventWireup="true" CodeBehind="AddArticle.aspx.cs" Inherits="NewsListing.author.addArticle" UnobtrusiveValidationMode="none"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 80%; margin: 0 auto;">
        <h2><b>Add News Article</b></h2>
        <hr />
        <div class="form-group">
            <div class="row">
                <div class="col-sm-4">
                    <span class="font-weight-bold">Author ID: </span>
                    <asp:Label runat="server" ID="authorIdLbl" />
                </div>
                <div class="col-sm-4">
                    <span class="font-weight-bold">Name: </span>
                    <asp:Label runat="server" ID="authorNameLbl"/>
                </div>
                <div class="col-sm-4">
                    <span class="font-weight-bold">Date: </span>
                    <asp:Label runat="server" ID="dateLbl"/>
                </div>
            </div>
        </div>
        <hr />
        <asp:Panel ID="publishMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="publishMessageLabel" runat="server" />
        </asp:Panel>
        <div class="form-group">
            <asp:Label runat="server" Text="Headline" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="HeadlineTb" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="HeadlineTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" Text="Content" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="ContentTb" CssClass="form-control" TextMode="MultiLine" Rows="10"/>
             <asp:RequiredFieldValidator runat="server" ControlToValidate="ContentTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <hr />
        <div class="form-group">
            <asp:Label CssClass="font-weight-bold" Text="News Image" runat="server" />
            <asp:FileUpload ID="newsImage" runat="server" CssClass="form-control-file"/>
        </div>
        <hr />
        <br />
        <asp:Button runat="server" ID="SubmitBtn" CssClass="btn btn-success" Text="Publish" OnClick="SubmitBtn_Click"/>
        <asp:Button runat="server" ID="CancelBtn" CausesValidation="false" CssClass="btn btn-danger" Text="Cancel" OnClick="CancelBtn_Click"/>
    </div>
</asp:Content>
