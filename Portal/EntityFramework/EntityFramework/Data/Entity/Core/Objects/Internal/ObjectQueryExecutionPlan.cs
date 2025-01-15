using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200044F RID: 1103
	internal class ObjectQueryExecutionPlan
	{
		// Token: 0x060035BF RID: 13759 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
		public ObjectQueryExecutionPlan(DbCommandDefinition commandDefinition, ShaperFactory resultShaperFactory, TypeUsage resultType, MergeOption mergeOption, bool streaming, EntitySet singleEntitySet, IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> compiledQueryParameters)
		{
			this.CommandDefinition = commandDefinition;
			this.ResultShaperFactory = resultShaperFactory;
			this.ResultType = resultType;
			this.MergeOption = mergeOption;
			this.Streaming = streaming;
			this._singleEntitySet = singleEntitySet;
			this.CompiledQueryParameters = compiledQueryParameters;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000ACDF4 File Offset: 0x000AAFF4
		internal string ToTraceString()
		{
			EntityCommandDefinition entityCommandDefinition = this.CommandDefinition as EntityCommandDefinition;
			if (entityCommandDefinition == null)
			{
				return string.Empty;
			}
			return entityCommandDefinition.ToTraceString();
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000ACE1C File Offset: 0x000AB01C
		internal virtual ObjectResult<TResultType> Execute<TResultType>(ObjectContext context, ObjectParameterCollection parameterValues)
		{
			DbDataReader dbDataReader = null;
			BufferedDataReader bufferedDataReader = null;
			ObjectResult<TResultType> objectResult;
			try
			{
				using (EntityCommand entityCommand = this.PrepareEntityCommand(context, parameterValues))
				{
					dbDataReader = entityCommand.GetCommandDefinition().ExecuteStoreCommands(entityCommand, this.Streaming ? CommandBehavior.Default : CommandBehavior.SequentialAccess);
				}
				ShaperFactory<TResultType> shaperFactory = (ShaperFactory<TResultType>)this.ResultShaperFactory;
				Shaper<TResultType> shaper;
				if (this.Streaming)
				{
					shaper = shaperFactory.Create(dbDataReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				else
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)context.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedDataReader = new BufferedDataReader(dbDataReader);
					bufferedDataReader.Initialize(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns);
					shaper = shaperFactory.Create(bufferedDataReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				TypeUsage typeUsage;
				if (this.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
				{
					typeUsage = ((CollectionType)this.ResultType.EdmType).TypeUsage;
				}
				else
				{
					typeUsage = this.ResultType;
				}
				objectResult = new ObjectResult<TResultType>(shaper, this._singleEntitySet, typeUsage);
			}
			catch (Exception)
			{
				if (this.Streaming && dbDataReader != null)
				{
					dbDataReader.Dispose();
				}
				if (!this.Streaming && bufferedDataReader != null)
				{
					bufferedDataReader.Dispose();
				}
				throw;
			}
			return objectResult;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000ACF98 File Offset: 0x000AB198
		internal virtual async Task<ObjectResult<TResultType>> ExecuteAsync<TResultType>(ObjectContext context, ObjectParameterCollection parameterValues, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DbDataReader storeReader = null;
			BufferedDataReader bufferedReader = null;
			ObjectResult<TResultType> objectResult;
			try
			{
				using (EntityCommand entityCommand = this.PrepareEntityCommand(context, parameterValues))
				{
					DbDataReader dbDataReader = await entityCommand.GetCommandDefinition().ExecuteStoreCommandsAsync(entityCommand, this.Streaming ? CommandBehavior.Default : CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
					storeReader = dbDataReader;
				}
				EntityCommand entityCommand = null;
				ShaperFactory<TResultType> shaperFactory = (ShaperFactory<TResultType>)this.ResultShaperFactory;
				Shaper<TResultType> shaper;
				if (this.Streaming)
				{
					shaper = shaperFactory.Create(storeReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				else
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)context.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedReader = new BufferedDataReader(storeReader);
					await bufferedReader.InitializeAsync(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns, cancellationToken).WithCurrentCulture();
					shaper = shaperFactory.Create(bufferedReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				TypeUsage typeUsage;
				if (this.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
				{
					typeUsage = ((CollectionType)this.ResultType.EdmType).TypeUsage;
				}
				else
				{
					typeUsage = this.ResultType;
				}
				objectResult = new ObjectResult<TResultType>(shaper, this._singleEntitySet, typeUsage);
			}
			catch (Exception)
			{
				if (this.Streaming && storeReader != null)
				{
					storeReader.Dispose();
				}
				if (!this.Streaming && bufferedReader != null)
				{
					bufferedReader.Dispose();
				}
				throw;
			}
			return objectResult;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000ACFF8 File Offset: 0x000AB1F8
		private EntityCommand PrepareEntityCommand(ObjectContext context, ObjectParameterCollection parameterValues)
		{
			EntityCommandDefinition entityCommandDefinition = (EntityCommandDefinition)this.CommandDefinition;
			EntityConnection entityConnection = (EntityConnection)context.Connection;
			EntityCommand entityCommand = new EntityCommand(entityConnection, entityCommandDefinition, context.InterceptionContext, null);
			if (context.CommandTimeout != null)
			{
				entityCommand.CommandTimeout = context.CommandTimeout.Value;
			}
			if (parameterValues != null)
			{
				foreach (ObjectParameter objectParameter in parameterValues)
				{
					int num = entityCommand.Parameters.IndexOf(objectParameter.Name);
					if (num != -1)
					{
						entityCommand.Parameters[num].Value = objectParameter.Value ?? DBNull.Value;
					}
				}
			}
			if (entityConnection.CurrentTransaction != null)
			{
				entityCommand.Transaction = entityConnection.CurrentTransaction;
			}
			return entityCommand;
		}

		// Token: 0x04001159 RID: 4441
		internal readonly DbCommandDefinition CommandDefinition;

		// Token: 0x0400115A RID: 4442
		internal readonly bool Streaming;

		// Token: 0x0400115B RID: 4443
		internal readonly ShaperFactory ResultShaperFactory;

		// Token: 0x0400115C RID: 4444
		internal readonly TypeUsage ResultType;

		// Token: 0x0400115D RID: 4445
		internal readonly MergeOption MergeOption;

		// Token: 0x0400115E RID: 4446
		internal readonly IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> CompiledQueryParameters;

		// Token: 0x0400115F RID: 4447
		private readonly EntitySet _singleEntitySet;
	}
}
