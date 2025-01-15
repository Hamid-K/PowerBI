using System;
using System.Reflection;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000778 RID: 1912
	internal sealed class ODataResponseMessage : ODataResponseMessageBase, IODataResponseMessage
	{
		// Token: 0x06003847 RID: 14407 RVA: 0x000B46AA File Offset: 0x000B28AA
		public ODataResponseMessage(HttpResponseData httpResponseData)
			: base(httpResponseData)
		{
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000B46B4 File Offset: 0x000B28B4
		public bool TryGetPreferenceApplied(string preferenceName, out HttpHeaderValueElement kvp)
		{
			ODataPreferenceHeader odataPreferenceHeader = this.PreferenceAppliedHeader();
			kvp = ODataResponseMessage.ODataPreferenceHeaderGetMethodInfo.Invoke(odataPreferenceHeader, new object[] { preferenceName }) as HttpHeaderValueElement;
			return kvp != null;
		}

		// Token: 0x04001D18 RID: 7448
		private static readonly MethodInfo ODataPreferenceHeaderGetMethodInfo = typeof(ODataPreferenceHeader).GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
