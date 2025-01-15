using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E0 RID: 480
	public class NotNullConditionConfiguration
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00043E17 File Offset: 0x00042017
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x00043E1F File Offset: 0x0004201F
		internal PropertyPath PropertyPath { get; set; }

		// Token: 0x06001938 RID: 6456 RVA: 0x00043E28 File Offset: 0x00042028
		internal NotNullConditionConfiguration(EntityMappingConfiguration entityMapConfiguration, PropertyPath propertyPath)
		{
			this._entityMappingConfiguration = entityMapConfiguration;
			this.PropertyPath = propertyPath;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00043E3E File Offset: 0x0004203E
		private NotNullConditionConfiguration(EntityMappingConfiguration owner, NotNullConditionConfiguration source)
		{
			this._entityMappingConfiguration = owner;
			this.PropertyPath = source.PropertyPath;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00043E59 File Offset: 0x00042059
		internal virtual NotNullConditionConfiguration Clone(EntityMappingConfiguration owner)
		{
			return new NotNullConditionConfiguration(owner, this);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00043E62 File Offset: 0x00042062
		public void HasValue()
		{
			this._entityMappingConfiguration.AddNullabilityCondition(this);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00043E70 File Offset: 0x00042070
		internal void Configure(DbDatabaseMapping databaseMapping, MappingFragment fragment, EntityType entityType)
		{
			IEnumerable<EdmPropertyPath> edmPropertyPath = EntityMappingConfiguration.PropertyPathToEdmPropertyPath(this.PropertyPath, entityType);
			if (edmPropertyPath.Count<EdmPropertyPath>() > 1)
			{
				throw Error.InvalidNotNullCondition(this.PropertyPath.ToString(), entityType.Name);
			}
			EdmProperty edmProperty = (from pm in fragment.ColumnMappings
				where pm.PropertyPath.SequenceEqual(edmPropertyPath.Single<EdmPropertyPath>())
				select pm.ColumnProperty).SingleOrDefault<EdmProperty>();
			if (edmProperty == null || !fragment.Table.Properties.Contains(edmProperty))
			{
				throw Error.InvalidNotNullCondition(this.PropertyPath.ToString(), entityType.Name);
			}
			if (ValueConditionConfiguration.AnyBaseTypeToTableWithoutColumnCondition(databaseMapping, entityType, fragment.Table, edmProperty))
			{
				edmProperty.Nullable = true;
			}
			new PrimitivePropertyConfiguration
			{
				IsNullable = new bool?(false),
				OverridableConfigurationParts = OverridableConfigurationParts.OverridableInSSpace
			}.Configure(edmPropertyPath.Single<EdmPropertyPath>().Last<EdmProperty>());
			fragment.AddNullabilityCondition(edmProperty, false);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00043F74 File Offset: 0x00042174
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00043F7C File Offset: 0x0004217C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00043F85 File Offset: 0x00042185
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00043F8D File Offset: 0x0004218D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A75 RID: 2677
		private readonly EntityMappingConfiguration _entityMappingConfiguration;
	}
}
