using System;
using System.ComponentModel;

namespace Microsoft.Data
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0000AA2B File Offset: 0x00008C2B
		public ResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000AA34 File Offset: 0x00008C34
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = StringsHelper.GetString(base.Description, Array.Empty<object>());
				}
				return base.Description;
			}
		}

		// Token: 0x0400000D RID: 13
		private bool replaced;
	}
}
