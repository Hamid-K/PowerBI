using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000219 RID: 537
	[DebuggerDisplay("{Table.Name}")]
	internal class TableMapping
	{
		// Token: 0x06001C47 RID: 7239 RVA: 0x000510E1 File Offset: 0x0004F2E1
		public TableMapping(EntityType table)
		{
			this._table = table;
			this._entityTypes = new SortedEntityTypeIndex();
			this._columns = new List<ColumnMapping>();
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00051106 File Offset: 0x0004F306
		public EntityType Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0005110E File Offset: 0x0004F30E
		public SortedEntityTypeIndex EntityTypes
		{
			get
			{
				return this._entityTypes;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00051116 File Offset: 0x0004F316
		public IEnumerable<ColumnMapping> ColumnMappings
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00051120 File Offset: 0x0004F320
		public void AddEntityTypeMappingFragment(EntitySet entitySet, EntityType entityType, MappingFragment fragment)
		{
			this._entityTypes.Add(entitySet, entityType);
			EdmProperty defaultDiscriminator = fragment.GetDefaultDiscriminator();
			using (IEnumerator<ColumnMappingBuilder> enumerator = fragment.ColumnMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ColumnMappingBuilder cm = enumerator.Current;
					this.FindOrCreateColumnMapping(cm.ColumnProperty).AddMapping(entityType, cm.PropertyPath, fragment.ColumnConditions.Where((ConditionPropertyMapping cc) => cc.Column == cm.ColumnProperty), defaultDiscriminator == cm.ColumnProperty);
				}
			}
			IEnumerable<ConditionPropertyMapping> columnConditions = fragment.ColumnConditions;
			Func<ConditionPropertyMapping, bool> <>9__1;
			Func<ConditionPropertyMapping, bool> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = (ConditionPropertyMapping cc) => fragment.ColumnMappings.All((ColumnMappingBuilder pm) => pm.ColumnProperty != cc.Column));
			}
			foreach (ConditionPropertyMapping conditionPropertyMapping in columnConditions.Where(func))
			{
				this.FindOrCreateColumnMapping(conditionPropertyMapping.Column).AddMapping(entityType, null, new ConditionPropertyMapping[] { conditionPropertyMapping }, defaultDiscriminator == conditionPropertyMapping.Column);
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00051278 File Offset: 0x0004F478
		private ColumnMapping FindOrCreateColumnMapping(EdmProperty column)
		{
			ColumnMapping columnMapping = this._columns.SingleOrDefault((ColumnMapping c) => c.Column == column);
			if (columnMapping == null)
			{
				columnMapping = new ColumnMapping(column);
				this._columns.Add(columnMapping);
			}
			return columnMapping;
		}

		// Token: 0x04000AEC RID: 2796
		private readonly EntityType _table;

		// Token: 0x04000AED RID: 2797
		private readonly SortedEntityTypeIndex _entityTypes;

		// Token: 0x04000AEE RID: 2798
		private readonly List<ColumnMapping> _columns;
	}
}
