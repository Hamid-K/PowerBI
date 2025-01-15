using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000560 RID: 1376
	public class ValueConditionMapping : ConditionPropertyMapping
	{
		// Token: 0x0600432A RID: 17194 RVA: 0x000E6F1C File Offset: 0x000E511C
		public ValueConditionMapping(EdmProperty propertyOrColumn, object value)
			: base(Check.NotNull<EdmProperty>(propertyOrColumn, "propertyOrColumn"), Check.NotNull<object>(value, "value"), null)
		{
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x000E6F4E File Offset: 0x000E514E
		public new object Value
		{
			get
			{
				return base.Value;
			}
		}
	}
}
