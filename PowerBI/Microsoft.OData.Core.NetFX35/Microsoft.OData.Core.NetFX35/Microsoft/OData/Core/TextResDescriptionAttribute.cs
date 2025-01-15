using System;
using System.ComponentModel;

namespace Microsoft.OData.Core
{
	// Token: 0x020002C4 RID: 708
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x00053248 File Offset: 0x00051448
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00053251 File Offset: 0x00051451
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

		// Token: 0x04000A51 RID: 2641
		private bool replaced;
	}
}
