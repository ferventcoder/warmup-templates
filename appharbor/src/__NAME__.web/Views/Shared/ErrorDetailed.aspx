<%@ page language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:content id="errorTitle" contentplaceholderid="TitleContent" runat="server">
    Error - Detailed
</asp:content>
<asp:content id="errorContent" contentplaceholderid="MainContent" runat="server">
    <h2>Sorry, an error occurred while processing your request. </h2>
    <div>
        <ul>
            <li>Action Name <%: Model.ActionName %></li>
            <li>Controller Name <%: Model.ControllerName %></li>
            <li>Exception <%: Model.Exception %></li>
        </ul>
    </div>
</asp:content>
