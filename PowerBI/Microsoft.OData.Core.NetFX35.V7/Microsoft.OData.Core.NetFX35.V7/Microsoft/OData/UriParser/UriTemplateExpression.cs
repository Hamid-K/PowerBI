using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000164 RID: 356
	public sealed class UriTemplateExpression
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0002BA90 File Offset: 0x00029C90
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0002BA98 File Offset: 0x00029C98
		public string LiteralText { get; internal set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0002BAA1 File Offset: 0x00029CA1
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0002BAA9 File Offset: 0x00029CA9
		public IEdmTypeReference ExpectedType { get; internal set; }
	}
}
