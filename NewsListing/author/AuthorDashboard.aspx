<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/template/Author.Master" AutoEventWireup="true" CodeBehind="AuthorDashboard.aspx.cs" Inherits="NewsListing.author.AuthorDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label runat="server" ID="welcomeLabel" CssClass="h1 font-italic"/>
        <hr />
        <h2><b>Published News Article</b></h2>
    </div>
    <asp:Panel runat="server" ID="NoArticlePanel">
        <div class="text-center h4 text-secondary">No article published yet.</div>
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%" OnDataBound="GridView1_DataBound" CssClass="table-responsive">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="Id">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Headline" SortExpression="Headline">
                <ItemTemplate>
                    <asp:Label ID="lblHeadline" runat="server" Text='<%# Bind("Headline") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Content" SortExpression="Content">
                <ItemStyle HorizontalAlign="Justify" />
                <ItemTemplate>
                    <asp:Label ID="lblContent" runat="server" Text='<%# Bind("Content") %>' CssClass="text-justify"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date" SortExpression="CreatedAt">
                <ItemTemplate>
                    <asp:Label ID="lblCreatedAt" runat="server" Text='<%# Bind("CreatedAt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Author" SortExpression="Author" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblAuthor" runat="server" Text='<%# Bind("Author") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a runat="server" data-target="#readModal" data-toggle="modal" class="openModalLink btn btn-success" style="cursor:pointer; color: white" data-headline=<%#Eval("headline") %> data-content=<%#Eval("content") %> data-postdate=<%# Eval("CreatedAt").ToString().Substring(0, Eval("CreatedAt").ToString().IndexOf(' '))%> data-image=<%# Eval("Image")!=DBNull.Value? ("data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("Image"))) : "../resources/images/empty-image.png"%> >View</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:LinkButton ID="EditButton" runat="server" CommandArgument='<%#Eval("Id") %>' OnCommand="EditNews" Text="Edit" CssClass="btn btn-primary"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandArgument='<%#Eval("Id") %>' OnCommand="DeleteNews" Text="Delete" OnClientClick='<%#Eval("Id", "return confirm(\"Are you certain you want to delete news article with ID={0}?\");") %>' CssClass="btn btn-danger mr-2"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
        <div class="modal fade" id="readModal" tabindex="-1" role="dialog" aria-labelledby="modalHeadline" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width:700px" role="document">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title" id="modalHeadline">
                    <!-- Title goes here -->
                </h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                </div>
                <div class="modal-body">
                    <img class="img-fluid" id="newsImage" src="..."/>
                    <hr />
                    <div class="text-secondary">
                        Posted on <span id="postDate"></span>
                    </div>
                    <br />
                    <div id="content" class="text-justify" style="white-space: pre-line">
                        <!-- Content goes here -->
                    </div>
                </div>
                <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            $(".openModalLink").click(function () {
                var headline = $(this).data('headline');
                var content = $(this).data('content');
                var image = $(this).data('image');
                var date = $(this).data('postdate');

                $("#modalHeadline").text(headline);
                $("#content").text(content);
                $("#newsImage").attr("src", image);
                $("#postDate").text(date);
            })

        });
    </script>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Article] WHERE ([Author] = @Author) ORDER BY CreatedAt DESC, Id DESC">
        <SelectParameters>
            <asp:SessionParameter Name="Author" SessionField="UID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
