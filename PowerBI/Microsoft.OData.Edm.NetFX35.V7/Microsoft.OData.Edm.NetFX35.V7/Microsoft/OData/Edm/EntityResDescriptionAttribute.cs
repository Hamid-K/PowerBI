using System;
using System.ComponentModel;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CB RID: 203
	[AttributeUsage(32767)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000CC23 File Offset: 0x0000AE23
		public EntityResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000CC2C File Offset: 0x0000AE2C
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

		// Token: 0x0400018D RID: 397
		private bool replaced;
	}
}
