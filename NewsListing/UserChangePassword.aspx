<%@ Page Title="" Language="C#" MasterPageFile="~/template/News.Master" AutoEventWireup="true" CodeBehind="UserChangePassword.aspx.cs" Inherits="NewsListing.UserChangePassword" UnobtrusiveValidationMode="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 50%; margin: 0 auto">
        <h2><b>Change Password</b></h2>
        <hr />
        <asp:Panel ID="changePasswordMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="changePasswordMessageLabel" runat="server" />
        </asp:Panel>
        <div class="form-group">
            <asp:Label runat="server" Text="Current Password" CssClass="font-weight-bold"/>
            <asp:TextBox runat="server" ID="currentPasswordTb" CssClass="form-control" TextMode="Password"/>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="currentPasswordTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="New Password" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="newPasswordTb" CssClass="form-control" TextMode="Password" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="newPasswordTb" ErrorMessage="*Required" ForeColor="Red"/>
        </div>
        <div class="form-group">
            <asp:Label runat="server" Text="Confirm New Password" CssClass="font-weight-bold" />
            <asp:TextBox runat="server" ID="confirmPasswordTb" CssClass="form-control" TextMode="Password" />
            <asp:CompareValidator runat="server" ControlToCompare="newPasswordTb" ControlToValidate="confirmPasswordTb" ErrorMessage="*Passwords do not match" ForeColor="Red"/>
        </div>
        <asp:Button runat="server" ID="SubmitBtn" CssClass="btn btn-success" Text="Confirm" OnClick="ConfirmBtn_Click"/>
        <asp:Button runat="server" ID="CancelBtn" CausesValidation="false" CssClass="btn btn-danger" Text="Cancel" OnClick="CancelBtn_Click"/>
    </div>
</asp:Content>
