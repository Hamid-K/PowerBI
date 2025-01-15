using System;
using System.ComponentModel;

namespace System.Spatial
{
	// Token: 0x0200008B RID: 139
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00009A3F File Offset: 0x00007C3F
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009A48 File Offset: 0x00007C48
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

		// Token: 0x04000114 RID: 276
		private bool replaced;
	}
}
