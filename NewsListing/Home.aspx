<%@ Page Title="Home" Language="C#" MasterPageFile="~/template/News.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NewsListing.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="NewsRepeater" runat="server" DataSourceID="SqlDataSource1" OnItemCreated="NewsRepeater_ItemCreated">
        <ItemTemplate>
            <div class="card mb-3" style="box-shadow: rgba(0,0,0,0.1) 1px inset 0px;">
              <div class="row no-gutters">
                <div class="col-md-4" style="max-height:270px">
                  <img runat="server" class="card-img" style="height: 100%;" src=<%# Eval("Image")!=DBNull.Value? ("data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("Image"))) : "~/resources/images/empty-image.png"%>/>
                </div>
                <div class="col-md-8">
                  <div class="card-body">
                    <h4 class="card-title"><%#Eval("Headline") %></h4>
                    <p class="card-text">By <%#Eval("Author") %></p>
                    <p class="card-text" style="overflow: hidden; max-height:100px; text-overflow:ellipsis; white-space:nowrap"><%#Eval("Content") %></p>
                    <p class="card-text"><small class="text-muted">Posted on <%# Eval("CreatedAt").ToString().Substring(0, Eval("CreatedAt").ToString().IndexOf(' ')) %></small></p>
                    <a runat="server" data-target="#readModal" data-toggle="modal" class="openNewsModalLink btn btn-primary" style="cursor:pointer; color: white" data-headline='<%#Eval("Headline")%>' data-content='<%#Eval("content") %>' data-image=<%# Eval("Image")!=DBNull.Value? ("data:Image/jpg;base64," + Convert.ToBase64String((byte[])Eval("Image"))) : "resources/images/empty-image.png"%> data-postdate=<%# Eval("CreatedAt").ToString().Substring(0, Eval("CreatedAt").ToString().IndexOf(' '))%> data-author=<%#Eval("Author") %>>Read</a>
                    <a runat="server" id="openCommentModalBtn" data-target="#commentModal" data-toggle="modal" data-postid='<%#Eval("Id") %>' class="openCommentModalLink btn btn-info" style="cursor:pointer; color:white">Comment</a>
                  </div>
                </div>
              </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
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
                Posted on <span id="postDate"></span> by <span id="author"></span>
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
    <div class="modal fade" id="commentModal" tabindex="-1" role="dialog" aria-labelledby="modalCommentTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width:700px" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="modalCommentTitle">
                Leave your comment
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            </div>
            <div class="modal-body">
                <div id="comment-section">
                    
                </div>
            </div>
            <div class="modal-footer">
             <textarea id="comment" class="form-control w-100"></textarea>
            <button type="button" class="btn btn-success" data-dismiss="modal" id="postCommentBtn">Post</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
            </div>
        </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Article].[Id], [Article].[Headline], [Article].[Content], [Article].[CreatedAt], [Article].[Image], CONCAT([User].[Fname], ' ', [User].[Lname]) as Author FROM [Article] JOIN [User] ON [Article].[Author]=[User].[Id] ORDER BY [Article].[CreatedAt] DESC"></asp:SqlDataSource>

    <script>
        $(function () {
            var postId = "";
            $(".openNewsModalLink").click(function () {
                var headline = $(this).data('headline');
                var content = $(this).data('content');
                var image = $(this).data('image');
                var date = $(this).data('postdate');
                var author = $(this).data('author');

                $("#modalHeadline").text(headline);
                $("#content").text(content);
                $("#newsImage").attr("src", image);
                $("#postDate").text(date);
                $("#author").text(author);
            });

            $(".openCommentModalLink").click(function () {
                $("#comment").val("");
                postId = $(this).data('postid');
                $.ajax({
                    type: "POST",
                    url: "CommentService.asmx/GetComments",
                    data: JSON.stringify({ postId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#comment-section").html(data.d);
                    },
                    error: function (data) {
                        $("#comment-section").text(data.d);
                    }
                });
            });

            $("#postCommentBtn").click(function () {
                var comment = $.trim($('#comment').val());
                var userId = '<%=Session["UID"] %>';
                if (comment != "") {
                    var jsonData = JSON.stringify({
                        postId,
                        userId,
                        comment
                    });

                    $.ajax({
                        type: "POST",
                        url: "CommentService.asmx/SubmitComment",
                        data: jsonData,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: false,
                        success: function (response) {
                            alert("Comment posted successfully!");
                        },
                        failure: function (errror) {
                            alert("Comment failed");
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>
