using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000105 RID: 261
	public sealed class NamedValue
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x00021829 File Offset: 0x0001FA29
		public NamedValue(string name, LiteralToken value)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0002184B File Offset: 0x0001FA4B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00021853 File Offset: 0x0001FA53
		public LiteralToken Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040006BB RID: 1723
		private readonly string name;

		// Token: 0x040006BC RID: 1724
		private readonly LiteralToken value;
	}
}
