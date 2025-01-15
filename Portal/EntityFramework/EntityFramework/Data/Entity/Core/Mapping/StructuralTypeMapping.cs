using System;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200055D RID: 1373
	public abstract class StructuralTypeMapping : MappingItem
	{
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06004317 RID: 17175
		public abstract ReadOnlyCollection<PropertyMapping> PropertyMappings { get; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06004318 RID: 17176
		public abstract ReadOnlyCollection<ConditionPropertyMapping> Conditions { get; }

		// Token: 0x06004319 RID: 17177
		public abstract void AddPropertyMapping(PropertyMapping propertyMapping);

		// Token: 0x0600431A RID: 17178
		public abstract void RemovePropertyMapping(PropertyMapping propertyMapping);

		// Token: 0x0600431B RID: 17179
		public abstract void AddCondition(ConditionPropertyMapping condition);

		// Token: 0x0600431C RID: 17180
		public abstract void RemoveCondition(ConditionPropertyMapping condition);
	}
}
