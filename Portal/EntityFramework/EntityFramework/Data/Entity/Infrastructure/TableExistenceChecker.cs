using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000260 RID: 608
	public abstract class TableExistenceChecker
	{
		// Token: 0x06001EED RID: 7917
		public abstract bool AnyModelTableExistsInDatabase(ObjectContext context, DbConnection connection, IEnumerable<EntitySet> modelTables, string edmMetadataContextTableName);

		// Token: 0x06001EEE RID: 7918 RVA: 0x00055E48 File Offset: 0x00054048
		protected virtual string GetTableName(EntitySet modelTable)
		{
			if (!modelTable.MetadataProperties.Contains("Table") || modelTable.MetadataProperties["Table"].Value == null)
			{
				return modelTable.Name;
			}
			return (string)modelTable.MetadataProperties["Table"].Value;
		}
	}
}
