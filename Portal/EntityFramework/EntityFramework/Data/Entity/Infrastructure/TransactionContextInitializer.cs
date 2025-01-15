using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Transactions;
using System.Xml.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000265 RID: 613
	internal class TransactionContextInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : TransactionContext
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x00056784 File Offset: 0x00054984
		public void InitializeDatabase(TContext context)
		{
			EntityConnection entityConnection = (EntityConnection)context.ObjectContext.Connection;
			if (entityConnection.State == ConnectionState.Open && entityConnection.CurrentTransaction != null)
			{
				try
				{
					using (new TransactionScope(TransactionScopeOption.Suppress))
					{
						context.Transactions.AsNoTracking<TransactionRow>().WithExecutionStrategy(new DefaultExecutionStrategy()).Count<TransactionRow>();
					}
				}
				catch (EntityException)
				{
					DbContextInfo currentInfo = DbContextInfo.CurrentInfo;
					DbContextInfo.CurrentInfo = null;
					try
					{
						IEnumerable<MigrationStatement> enumerable = TransactionContextInitializer<TContext>.GenerateMigrationStatements(context);
						DbMigrator dbMigrator = new DbMigrator(context.InternalContext.MigrationsConfiguration, context, DatabaseExistenceState.Exists, true);
						using (new TransactionScope(TransactionScopeOption.Suppress))
						{
							dbMigrator.ExecuteStatements(enumerable, entityConnection.CurrentTransaction.StoreTransaction);
						}
					}
					finally
					{
						DbContextInfo.CurrentInfo = currentInfo;
					}
				}
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00056890 File Offset: 0x00054A90
		internal static IEnumerable<MigrationStatement> GenerateMigrationStatements(TransactionContext context)
		{
			if (DbConfiguration.DependencyResolver.GetService(context.InternalContext.ProviderName) != null)
			{
				MigrationSqlGenerator sqlGenerator = context.InternalContext.MigrationsConfiguration.GetSqlGenerator(context.InternalContext.ProviderName);
				DbConnection connection = context.Database.Connection;
				XDocument model = new DbModelBuilder().Build(connection).GetModel();
				CreateTableOperation createTableOperation = (CreateTableOperation)new EdmModelDiffer().Diff(model, context.GetModel(), null, null, null, null).Single<MigrationOperation>();
				string text = ((context.InternalContext.ModelProviderInfo != null) ? context.InternalContext.ModelProviderInfo.ProviderManifestToken : DbConfiguration.DependencyResolver.GetService<IManifestTokenResolver>().ResolveManifestToken(connection));
				return sqlGenerator.Generate(new CreateTableOperation[] { createTableOperation }, text);
			}
			return new MigrationStatement[]
			{
				new MigrationStatement
				{
					Sql = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript(),
					SuppressTransaction = true
				}
			};
		}
	}
}
