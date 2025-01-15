using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Navigator
{
	// Token: 0x020001A7 RID: 423
	[DomName("NavigatorContentUtils")]
	[DomNoInterfaceObject]
	public interface INavigatorContentUtilities
	{
		// Token: 0x06000F06 RID: 3846
		[DomName("registerProtocolHandler")]
		void RegisterProtocolHandler(string scheme, string url, string title);

		// Token: 0x06000F07 RID: 3847
		[DomName("registerContentHandler")]
		void RegisterContentHandler(string mimeType, string url, string title);

		// Token: 0x06000F08 RID: 3848
		[DomName("isProtocolHandlerRegistered")]
		bool IsProtocolHandlerRegistered(string scheme, string url);

		// Token: 0x06000F09 RID: 3849
		[DomName("isContentHandlerRegistered")]
		bool IsContentHandlerRegistered(string mimeType, string url);

		// Token: 0x06000F0A RID: 3850
		[DomName("unregisterProtocolHandler")]
		void UnregisterProtocolHandler(string scheme, string url);

		// Token: 0x06000F0B RID: 3851
		[DomName("unregisterContentHandler")]
		void UnregisterContentHandler(string mimeType, string url);
	}
}
