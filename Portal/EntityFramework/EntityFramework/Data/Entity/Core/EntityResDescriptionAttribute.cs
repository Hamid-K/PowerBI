using System;
using System.ComponentModel;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000631D0 File Offset: 0x000613D0
		public override string Description
		{
			get
			{
				if (!this._replaced)
				{
					this._replaced = true;
					base.DescriptionValue = EntityRes.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000631F8 File Offset: 0x000613F8
		public EntityResDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x04000C05 RID: 3077
		private bool _replaced;
	}
}
