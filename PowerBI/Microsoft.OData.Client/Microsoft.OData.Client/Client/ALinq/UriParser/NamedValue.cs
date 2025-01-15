using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200012C RID: 300
	public sealed class NamedValue
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x0002CF5C File Offset: 0x0002B15C
		public NamedValue(string name, LiteralToken value)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0002CF7E File Offset: 0x0002B17E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0002CF86 File Offset: 0x0002B186
		public LiteralToken Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000678 RID: 1656
		private readonly string name;

		// Token: 0x04000679 RID: 1657
		private readonly LiteralToken value;
	}
}
