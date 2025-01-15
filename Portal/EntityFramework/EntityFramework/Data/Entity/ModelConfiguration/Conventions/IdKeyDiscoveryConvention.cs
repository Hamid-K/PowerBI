using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001AC RID: 428
	public class IdKeyDiscoveryConvention : KeyDiscoveryConvention
	{
		// Token: 0x0600177C RID: 6012 RVA: 0x0003F734 File Offset: 0x0003D934
		protected override IEnumerable<EdmProperty> MatchKeyProperty(EntityType entityType, IEnumerable<EdmProperty> primitiveProperties)
		{
			Check.NotNull<EntityType>(entityType, "entityType");
			Check.NotNull<IEnumerable<EdmProperty>>(primitiveProperties, "primitiveProperties");
			IEnumerable<EdmProperty> enumerable = primitiveProperties.Where((EdmProperty p) => "Id".Equals(p.Name, StringComparison.OrdinalIgnoreCase));
			if (!enumerable.Any<EdmProperty>())
			{
				enumerable = primitiveProperties.Where((EdmProperty p) => (entityType.Name + "Id").Equals(p.Name, StringComparison.OrdinalIgnoreCase));
			}
			if (enumerable.Count<EdmProperty>() > 1)
			{
				throw Error.MultiplePropertiesMatchedAsKeys(enumerable.First<EdmProperty>().Name, entityType.Name);
			}
			return enumerable;
		}

		// Token: 0x04000A35 RID: 2613
		private const string Id = "Id";
	}
}
