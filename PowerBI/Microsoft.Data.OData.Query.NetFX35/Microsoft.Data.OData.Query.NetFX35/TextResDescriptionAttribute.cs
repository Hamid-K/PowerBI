using System;
using System.ComponentModel;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(32767)]
	internal sealed class TextResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000CD1A File Offset: 0x0000AF1A
		public TextResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000CD23 File Offset: 0x0000AF23
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

		// Token: 0x04000227 RID: 551
		private bool replaced;
	}
}
