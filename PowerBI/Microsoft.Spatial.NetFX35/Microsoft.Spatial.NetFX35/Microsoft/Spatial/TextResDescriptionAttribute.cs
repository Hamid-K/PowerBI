using System;
using System.ComponentModel;

namespace Microsoft.Spatial
{
	// Token: 0x0200008C RID: 140
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600037B RID: 891 RVA: 0x000099AF File Offset: 0x00007BAF
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000099B8 File Offset: 0x00007BB8
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

		// Token: 0x04000116 RID: 278
		private bool replaced;
	}
}
