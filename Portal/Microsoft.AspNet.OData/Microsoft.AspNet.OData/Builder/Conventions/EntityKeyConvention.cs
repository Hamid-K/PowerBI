using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000154 RID: 340
	internal class EntityKeyConvention : EntityTypeConvention
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x00030588 File Offset: 0x0002E788
		public override void Apply(EntityTypeConfiguration entity, ODataConventionModelBuilder model)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			if (entity.Keys.Any<PrimitivePropertyConfiguration>() || entity.EnumKeys.Any<EnumPropertyConfiguration>())
			{
				return;
			}
			if (entity.BaseType != null && entity.BaseType.Keys().Any<PropertyConfiguration>())
			{
				return;
			}
			PropertyConfiguration keyProperty = EntityKeyConvention.GetKeyProperty(entity);
			if (keyProperty != null)
			{
				entity.HasKey(keyProperty.PropertyInfo);
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000305F0 File Offset: 0x0002E7F0
		private static PropertyConfiguration GetKeyProperty(EntityTypeConfiguration entityType)
		{
			IEnumerable<PropertyConfiguration> enumerable = entityType.Properties.Where((PropertyConfiguration p) => (p.Name.Equals(entityType.Name + "Id", StringComparison.OrdinalIgnoreCase) || p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)) && (EdmLibHelpers.GetEdmPrimitiveTypeOrNull(p.PropertyInfo.PropertyType) != null || TypeHelper.IsEnum(p.PropertyInfo.PropertyType)));
			if (enumerable.Count<PropertyConfiguration>() == 1)
			{
				return enumerable.Single<PropertyConfiguration>();
			}
			return null;
		}
	}
}
