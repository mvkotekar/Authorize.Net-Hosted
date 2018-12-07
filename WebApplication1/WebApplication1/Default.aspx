<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Submit Transaction to proceed to payment gateway</h1>
    <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />


    <h1>Authorize.net IFrame Example</h1>
    <div id="divAuthorizeNetPopup" style="display:none;" class="AuthorizeNetPopupGrayFrameTheme">
	<div class="AuthorizeNetPopupOuter">
		<div class="AuthorizeNetPopupTop">
			<div class="AuthorizeNetPopupClose">
				<a href="javascript:;" onclick="AuthorizeNetPopup.closePopup();" title="Close"> </a>
			</div>
		</div>
		<div class="AuthorizeNetPopupInner">
			<iframe name="iframeAuthorizeNet" id="iframeAuthorizeNet" src="empty.html" frameborder="0" scrolling="no"></iframe>
		</div>
		<div class="AuthorizeNetPopupBottom">
			<div class="AuthorizeNetPopupLogo" title="Powered by Authorize.Net"></div>
		</div>
		<div id="divAuthorizeNetPopupScreen" style="display:none;"></div>
	</div>
</div>

    		<script type="text/javascript">
			//<![CDATA[
				function callParentFunction(str) {
					if (str && str.length > 0 
						&& window.parent 
						&& window.parent.parent
						&& window.parent.parent.AuthorizeNetPopup 
						&& window.parent.parent.AuthorizeNetPopup.onReceiveCommunication)
						{
// Errors indicate a mismatch in domain between the page containing the iframe and this page.
							window.parent.parent.AuthorizeNetPopup.onReceiveCommunication(str);
						}
					}

				function receiveMessage(event) {
					if (event && event.data) {
						callParentFunction(event.data);
						}
					}

				if (window.addEventListener) {
					window.addEventListener("message", receiveMessage, false);
					} else if (window.attachEvent) {
						window.attachEvent("onmessage", receiveMessage);
					}

				if (window.location.hash && window.location.hash.length > 1) {
					callParentFunction(window.location.hash.substring(1));
					}
			//]]/>
		</script>
</asp:Content>


