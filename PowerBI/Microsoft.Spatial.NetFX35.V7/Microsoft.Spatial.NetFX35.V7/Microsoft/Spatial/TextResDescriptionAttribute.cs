using System;
using System.ComponentModel;

namespace Microsoft.Spatial
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000290 RID: 656 RVA: 0x000068CF File Offset: 0x00004ACF
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000068D8 File Offset: 0x00004AD8
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

		// Token: 0x040000D2 RID: 210
		private bool replaced;
	}
}
