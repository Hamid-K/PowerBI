using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A4 RID: 420
	public class ManyToManyCascadeDeleteConvention : IDbMappingConvention, IConvention
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		void IDbMappingConvention.Apply(DbDatabaseMapping databaseMapping)
		{
			Check.NotNull<DbDatabaseMapping>(databaseMapping, "databaseMapping");
			(from asm in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.AssociationSetMappings)
				where asm.AssociationSet.ElementType.IsManyToMany() && !asm.AssociationSet.ElementType.IsSelfReferencing()
				select asm).SelectMany((AssociationSetMapping asm) => asm.Table.ForeignKeyBuilders).Each((ForeignKeyBuilder fk) => fk.DeleteAction = OperationAction.Cascade);
		}
	}
}
