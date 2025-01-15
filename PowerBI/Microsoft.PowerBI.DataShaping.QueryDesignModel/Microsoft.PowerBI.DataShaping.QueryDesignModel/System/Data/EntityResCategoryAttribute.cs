using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003168 File Offset: 0x00001368
		public EntityResCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003171 File Offset: 0x00001371
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
