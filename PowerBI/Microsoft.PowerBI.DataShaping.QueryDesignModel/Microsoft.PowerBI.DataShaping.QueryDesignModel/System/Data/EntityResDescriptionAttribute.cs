using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003137 File Offset: 0x00001337
		public EntityResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003140 File Offset: 0x00001340
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

		// Token: 0x04000046 RID: 70
		private bool replaced;
	}
}
