using System;
using System.ComponentModel;

namespace Microsoft.Data.Edm
{
	// Token: 0x02000240 RID: 576
	[AttributeUsage(32767)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x0002A28A File Offset: 0x0002848A
		public EntityResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0002A293 File Offset: 0x00028493
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = EntityRes.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x0400074E RID: 1870
		private bool replaced;
	}
}
