using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000543 RID: 1347
	public class IsNullConditionMapping : ConditionPropertyMapping
	{
		// Token: 0x060041F8 RID: 16888 RVA: 0x000E0186 File Offset: 0x000DE386
		public IsNullConditionMapping(EdmProperty propertyOrColumn, bool isNull)
			: base(Check.NotNull<EdmProperty>(propertyOrColumn, "propertyOrColumn"), null, new bool?(isNull))
		{
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x000E01A0 File Offset: 0x000DE3A0
		public new bool IsNull
		{
			get
			{
				return base.IsNull.Value;
			}
		}
	}
}
