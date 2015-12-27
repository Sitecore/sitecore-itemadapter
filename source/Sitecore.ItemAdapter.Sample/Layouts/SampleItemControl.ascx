<%@ Control Language="c#" AutoEventWireup="true" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="System.Runtime.CompilerServices" %>
<%@ Import Namespace="Sitecore.ItemAdapter" %>
<%@ Import Namespace="Sitecore.ItemAdapter.Model" %>
<%@ Import Namespace="Sitecore.ItemAdapter.Sample.Models" %>


 <script runat="server">
     protected SampleItem ItemModel { get; set; }

     void Page_Init(object sender, System.EventArgs e)
     {
         
     }

     void Page_Load(object sender, System.EventArgs e)
     {
         ItemModel = StandardItemAdapter<SampleItem>.GetExtendedModel(Sitecore.Context.Item);
     }

 </script>

<div>
    <h1>Title: <%= ItemModel.Title %></h1>
    <div>Text: <%= ItemModel.Text %></div>
    <div>Date: <%= ItemModel.Date.ToString() %></div>
    <div>DateTime: <%= ItemModel.DateTime.ToString() %></div>
    <div>Checkbox: <%= ItemModel.Checkbox.ToString() %></div>
	<div>MultiLineText: <br/>
        <pre><%= ItemModel.MultiLineText %></pre></div>
	<div>Image: <br/>
        <img src="<%= ItemModel.Image %>"/>
	</div>
    <div>Integer: <%= ItemModel.Integer.ToString() %></div>
    <div>Number: <%= ItemModel.Number.ToString() %></div>

    <div>General Link: <%= ItemModel.GeneralLink != null ? ItemModel.GeneralLink.Url : string.Empty %></div>

    <div>Link: <%= ItemModel.Link != null ? ItemModel.Link.Name : string.Empty %></div>
    
    <%--<div>
        Multi-list (<%= ItemModel.MultiList.Count %>): <br/>
        <%  foreach (IItemAdapterModel adapterItem in ItemModel.MultiList) { %>
            <%= adapterItem.Name %> <br/>     
        <% } %>
    </div>--%>
                
    <div>File: <%= ItemModel.FileUrl  %></div>

<%--    <div>
        Checklist (<%= ItemModel.Checklist.Count %>): <br/>
        <%  foreach (IItemAdapterModel adapterItem in ItemModel.Checklist) { %>
            <%= adapterItem.Name %> <br/>     
        <% } %>
    </div>--%>
                
    <%--<div>
        NameValueList (<%= ItemModel.NameValueList.Count %>): <br/>
        <%  foreach (KeyValuePair<string,string> kvp in ItemModel.NameValueList) { %>
            Key: <%= kvp.Key %>, Value: <%= kvp.Value %>  <br/>     
        <% } %>
    </div>--%>
                
    <%--<div>
        NameLookupList (<%= ItemModel.NameLookupList.Count %>): <br/>
        <%  foreach (KeyValuePair<string,IItemAdapterModel> kvp in ItemModel.NameLookupList) { %>
            Key: <%= kvp.Key %>, Value: <%= kvp.Value != null ? kvp.Value.Name : string.Empty %>  <br/>     
        <% } %>
    </div>--%>

</div>
