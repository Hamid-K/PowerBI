using System;
using System.ComponentModel;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AE RID: 686
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x0004F452 File Offset: 0x0004D652
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0004F45B File Offset: 0x0004D65B
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = TextRes.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x0400099F RID: 2463
		private bool replaced;
	}
}
