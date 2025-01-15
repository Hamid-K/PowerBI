using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000217 RID: 535
	internal class PropertyMappingSpecification
	{
		// Token: 0x06001C3B RID: 7227 RVA: 0x00050F56 File Offset: 0x0004F156
		public PropertyMappingSpecification(EntityType entityType, IList<EdmProperty> propertyPath, IList<ConditionPropertyMapping> conditions, bool isDefaultDiscriminatorCondition)
		{
			this._entityType = entityType;
			this._propertyPath = propertyPath;
			this._conditions = conditions;
			this._isDefaultDiscriminatorCondition = isDefaultDiscriminatorCondition;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00050F7B File Offset: 0x0004F17B
		public EntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00050F83 File Offset: 0x0004F183
		public IList<EdmProperty> PropertyPath
		{
			get
			{
				return this._propertyPath;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00050F8B File Offset: 0x0004F18B
		public IList<ConditionPropertyMapping> Conditions
		{
			get
			{
				return this._conditions;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00050F93 File Offset: 0x0004F193
		public bool IsDefaultDiscriminatorCondition
		{
			get
			{
				return this._isDefaultDiscriminatorCondition;
			}
		}

		// Token: 0x04000AE6 RID: 2790
		private readonly EntityType _entityType;

		// Token: 0x04000AE7 RID: 2791
		private readonly IList<EdmProperty> _propertyPath;

		// Token: 0x04000AE8 RID: 2792
		private readonly IList<ConditionPropertyMapping> _conditions;

		// Token: 0x04000AE9 RID: 2793
		private readonly bool _isDefaultDiscriminatorCondition;
	}
}
