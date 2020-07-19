<%@ Page Title="Login" Language="C#" MasterPageFile="~/template/News.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NewsListing.Author.Login" UnobtrusiveValidationMode="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="login_background"></div>
    <div id="loginBox">
        <div id="loginBox-header">
            <span class="h1 font-weight-bold">Login</span>
        </div>
        <div id="loginBox-body">
            <asp:Panel ID="loginMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <asp:Label ID="loginMessageLabel" runat="server" />
            </asp:Panel>
            <div class="form-group">
                <asp:Label runat="server" Text="Username" CssClass="font-bold"/>
                <asp:TextBox runat="server" ID="usernameTb" CssClass="form-control"/>
                <asp:RequiredFieldValidator ControlToValidate="usernameTb" runat="server" ErrorMessage="*Required" ForeColor="Red"/>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Password"/>
                <asp:TextBox runat="server" ID="passwordTb" TextMode="Password" CssClass="form-control"/>
                <asp:RequiredFieldValidator ControlToValidate="passwordTb" runat="server" ErrorMessage="*Required" ForeColor="Red"/>                
            </div>
            <asp:Button runat="server" ID="SubmitBtn" Text="Login" CssClass="btn btn-success w-100" OnClick="LoginBtn_Click"/>
        </div>
    </div>    
</asp:Content>
