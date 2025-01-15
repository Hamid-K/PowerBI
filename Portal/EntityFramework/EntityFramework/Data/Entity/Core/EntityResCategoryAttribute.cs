using System;
using System.ComponentModel;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D2 RID: 722
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060022F5 RID: 8949 RVA: 0x000631BF File Offset: 0x000613BF
		public EntityResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000631C8 File Offset: 0x000613C8
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
