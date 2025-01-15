using System;
using System.ComponentModel;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200027A RID: 634
	[AttributeUsage(32767)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x0002B95B File Offset: 0x00029B5B
		public EntityResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002B964 File Offset: 0x00029B64
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

		// Token: 0x0400077A RID: 1914
		private bool replaced;
	}
}
