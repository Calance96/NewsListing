<%@ Page Title="" Language="C#" MasterPageFile="~/template/Author.Master" AutoEventWireup="true" CodeBehind="AddAuthor.aspx.cs" Inherits="NewsListing.author.AddAuthor" 
    UnobtrusiveValidationMode ="none"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 50%; margin: 0 auto">
        <h2><b>Add News Author</b></h2>
        <hr />
        <asp:Panel ID="registerMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="registerMessageLabel" runat="server" />
        </asp:Panel>
        <div class="form-group">
            <asp:Label runat="server" Text="Username" CssClass="font-weight-bold"/>
            <asp:TextBox runat="server" ID="usernameTb" CssClass="form-control"/>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="usernameTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Password" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="passwordTb" CssClass="form-control" TextMode="Password" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Confirm Password" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="passwordConfirmTb" CssClass="form-control" TextMode="Password"/>
            <asp:CompareValidator ControlToValidate="passwordConfirmTb" ControlToCompare="passwordTb" runat="server" ErrorMessage="Passwords do not match" ForeColor="red"/>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="First name" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="firstNameTb" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="firstNameTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Last name" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="lastNameTb" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="lastNameTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <asp:Button runat="server" ID="SubmitBtn" CssClass="btn btn-success" Text="Register" OnClick="RegisterBtn_Click"/>
        <asp:Button runat="server" ID="CancelBtn" CausesValidation="false" CssClass="btn btn-danger" Text="Cancel" OnClick="CancelBtn_Click"/>
    </div>
</asp:Content>
