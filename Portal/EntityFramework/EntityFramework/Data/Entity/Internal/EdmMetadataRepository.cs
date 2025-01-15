using System;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000FE RID: 254
	internal class EdmMetadataRepository : RepositoryBase
	{
		// Token: 0x0600127C RID: 4732 RVA: 0x00030663 File Offset: 0x0002E863
		public EdmMetadataRepository(InternalContext usersContext, string connectionString, DbProviderFactory providerFactory)
			: base(usersContext, connectionString, providerFactory)
		{
			this._existingTransaction = usersContext.TryGetCurrentStoreTransaction();
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0003067C File Offset: 0x0002E87C
		public virtual string QueryForModelHash(Func<DbConnection, EdmMetadataContext> createContext)
		{
			DbConnection dbConnection = base.CreateConnection();
			string text;
			try
			{
				using (EdmMetadataContext edmMetadataContext = createContext(dbConnection))
				{
					if (this._existingTransaction != null && this._existingTransaction.Connection == dbConnection)
					{
						edmMetadataContext.Database.UseTransaction(this._existingTransaction);
					}
					try
					{
						EdmMetadata edmMetadata = (from m in edmMetadataContext.Metadata.AsNoTracking<EdmMetadata>()
							orderby m.Id descending
							select m).FirstOrDefault<EdmMetadata>();
						text = ((edmMetadata != null) ? edmMetadata.ModelHash : null);
					}
					catch (EntityCommandExecutionException)
					{
						text = null;
					}
				}
			}
			finally
			{
				base.DisposeConnection(dbConnection);
			}
			return text;
		}

		// Token: 0x0400091F RID: 2335
		private readonly DbTransaction _existingTransaction;
	}
}
