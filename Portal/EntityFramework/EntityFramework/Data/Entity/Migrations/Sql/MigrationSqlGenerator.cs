using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Linq;

namespace System.Data.Entity.Migrations.Sql
{
	// Token: 0x020000A8 RID: 168
	public abstract class MigrationSqlGenerator
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0001FA10 File Offset: 0x0001DC10
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x0001FA18 File Offset: 0x0001DC18
		protected DbProviderManifest ProviderManifest { get; set; }

		// Token: 0x06000EF4 RID: 3828
		public abstract IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken);

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0001FA21 File Offset: 0x0001DC21
		public virtual string GenerateProcedureBody(ICollection<DbModificationCommandTree> commandTrees, string rowsAffectedParameter, string providerManifestToken)
		{
			return null;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0001FA24 File Offset: 0x0001DC24
		public virtual bool IsPermissionDeniedError(Exception exception)
		{
			return false;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0001FA28 File Offset: 0x0001DC28
		protected virtual TypeUsage BuildStoreTypeUsage(string storeTypeName, PropertyModel propertyModel)
		{
			PrimitiveType primitiveType = this.ProviderManifest.GetStoreTypes().SingleOrDefault((PrimitiveType p) => string.Equals(p.Name, storeTypeName, StringComparison.OrdinalIgnoreCase));
			if (primitiveType != null)
			{
				return TypeUsage.Create(primitiveType, propertyModel.ToFacetValues());
			}
			return null;
		}
	}
}
