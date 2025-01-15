using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000215 RID: 533
	[DebuggerDisplay("{Column.Name}")]
	internal class ColumnMapping
	{
		// Token: 0x06001C26 RID: 7206 RVA: 0x0004FCFA File Offset: 0x0004DEFA
		public ColumnMapping(EdmProperty column)
		{
			this._column = column;
			this._propertyMappings = new List<PropertyMappingSpecification>();
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0004FD14 File Offset: 0x0004DF14
		public EdmProperty Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0004FD1C File Offset: 0x0004DF1C
		public IList<PropertyMappingSpecification> PropertyMappings
		{
			get
			{
				return this._propertyMappings;
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0004FD24 File Offset: 0x0004DF24
		public void AddMapping(EntityType entityType, IList<EdmProperty> propertyPath, IEnumerable<ConditionPropertyMapping> conditions, bool isDefaultDiscriminatorCondition)
		{
			this._propertyMappings.Add(new PropertyMappingSpecification(entityType, propertyPath, conditions.ToList<ConditionPropertyMapping>(), isDefaultDiscriminatorCondition));
		}

		// Token: 0x04000AE1 RID: 2785
		private readonly EdmProperty _column;

		// Token: 0x04000AE2 RID: 2786
		private readonly List<PropertyMappingSpecification> _propertyMappings;
	}
}
