using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001EA RID: 490
	public abstract class AssociationMappingConfiguration
	{
		// Token: 0x060019D3 RID: 6611
		internal abstract void Configure(AssociationSetMapping associationSetMapping, EdmModel database, PropertyInfo navigationProperty);

		// Token: 0x060019D4 RID: 6612
		internal abstract AssociationMappingConfiguration Clone();
	}
}
