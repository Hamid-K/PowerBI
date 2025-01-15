using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000045 RID: 69
	public sealed class NamedValue
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000096EA File Offset: 0x000078EA
		public NamedValue(string name, LiteralQueryToken value)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralQueryToken>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000970B File Offset: 0x0000790B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009713 File Offset: 0x00007913
		public LiteralQueryToken Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040001B6 RID: 438
		private readonly string name;

		// Token: 0x040001B7 RID: 439
		private readonly LiteralQueryToken value;
	}
}
