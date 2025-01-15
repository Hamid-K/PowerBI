using System;
using System.ComponentModel;

namespace Microsoft.OData
{
	// Token: 0x020000DA RID: 218
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x00018025 File Offset: 0x00016225
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001802E File Offset: 0x0001622E
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

		// Token: 0x0400039B RID: 923
		private bool replaced;
	}
}
