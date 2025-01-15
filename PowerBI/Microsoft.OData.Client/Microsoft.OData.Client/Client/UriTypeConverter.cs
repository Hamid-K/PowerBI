using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200007D RID: 125
	internal sealed class UriTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000E99B File Offset: 0x0000CB9B
		internal override object Parse(string text)
		{
			return UriUtil.CreateUri(text, UriKind.RelativeOrAbsolute);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		internal override string ToString(object instance)
		{
			return UriUtil.UriToString((Uri)instance);
		}
	}
}
