using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000214 RID: 532
	internal static class DatabaseOperations
	{
		// Token: 0x06001C25 RID: 7205 RVA: 0x0004FC18 File Offset: 0x0004DE18
		public static void AddTypeConstraint(EdmModel database, EntityType entityType, EntityType principalTable, EntityType dependentTable, bool isSplitting)
		{
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(database, string.Format(CultureInfo.InvariantCulture, "{0}_TypeConstraint_From_{1}_To_{2}", new object[] { entityType.Name, principalTable.Name, dependentTable.Name }))
			{
				PrincipalTable = principalTable
			};
			dependentTable.AddForeignKey(foreignKeyBuilder);
			if (isSplitting)
			{
				foreignKeyBuilder.SetIsSplitConstraint();
			}
			else
			{
				foreignKeyBuilder.SetIsTypeConstraint();
			}
			foreignKeyBuilder.DependentColumns = dependentTable.Properties.Where((EdmProperty c) => c.IsPrimaryKeyColumn);
			dependentTable.Properties.Where((EdmProperty c) => c.IsPrimaryKeyColumn).Each(delegate(EdmProperty c)
			{
				c.RemoveStoreGeneratedIdentityPattern();
			});
		}
	}
}
