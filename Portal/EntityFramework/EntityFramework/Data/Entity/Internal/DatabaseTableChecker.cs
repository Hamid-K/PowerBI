using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using System.Transactions;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F4 RID: 244
	internal class DatabaseTableChecker
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x0002EFC4 File Offset: 0x0002D1C4
		public DatabaseExistenceState AnyModelTableExists(InternalContext internalContext)
		{
			if (!internalContext.DatabaseOperations.Exists(internalContext.Connection, internalContext.CommandTimeout, new Lazy<StoreItemCollection>(() => DatabaseTableChecker.CreateStoreItemCollection(internalContext))))
			{
				return DatabaseExistenceState.DoesNotExist;
			}
			DatabaseExistenceState databaseExistenceState;
			using (ClonedObjectContext clonedObjectContext = internalContext.CreateObjectContextForDdlOps())
			{
				try
				{
					if (internalContext.CodeFirstModel == null)
					{
						databaseExistenceState = DatabaseExistenceState.Exists;
					}
					else
					{
						TableExistenceChecker service = DbConfiguration.DependencyResolver.GetService(internalContext.ProviderName);
						if (service == null)
						{
							databaseExistenceState = DatabaseExistenceState.Exists;
						}
						else
						{
							List<EntitySet> list = this.GetModelTables(internalContext).ToList<EntitySet>();
							if (!list.Any<EntitySet>())
							{
								databaseExistenceState = DatabaseExistenceState.Exists;
							}
							else if (this.QueryForTableExistence(service, clonedObjectContext, list))
							{
								databaseExistenceState = DatabaseExistenceState.Exists;
							}
							else
							{
								databaseExistenceState = (internalContext.HasHistoryTableEntry() ? DatabaseExistenceState.Exists : DatabaseExistenceState.ExistsConsideredEmpty);
							}
						}
					}
				}
				catch (Exception)
				{
					databaseExistenceState = DatabaseExistenceState.Exists;
				}
			}
			return databaseExistenceState;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		private static StoreItemCollection CreateStoreItemCollection(InternalContext internalContext)
		{
			StoreItemCollection storeItemCollection;
			using (ClonedObjectContext clonedObjectContext = internalContext.CreateObjectContextForDdlOps())
			{
				storeItemCollection = (StoreItemCollection)clonedObjectContext.ObjectContext.Connection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			}
			return storeItemCollection;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0002F11C File Offset: 0x0002D31C
		public virtual bool QueryForTableExistence(TableExistenceChecker checker, ClonedObjectContext clonedObjectContext, List<EntitySet> modelTables)
		{
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				if (checker.AnyModelTableExistsInDatabase(clonedObjectContext.ObjectContext, clonedObjectContext.Connection, modelTables, "EdmMetadata"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0002F174 File Offset: 0x0002D374
		public virtual IEnumerable<EntitySet> GetModelTables(InternalContext internalContext)
		{
			return from s in internalContext.ObjectContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace).GetItems<EntityContainer>().Single<EntityContainer>()
					.BaseEntitySets.OfType<EntitySet>()
				where !s.MetadataProperties.Contains("Type") || (string)s.MetadataProperties["Type"].Value == "Tables"
				select s;
		}
	}
}
