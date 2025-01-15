using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000450 RID: 1104
	internal class ObjectQueryExecutionPlanFactory
	{
		// Token: 0x060035C4 RID: 13764 RVA: 0x000AD0E0 File Offset: 0x000AB2E0
		public ObjectQueryExecutionPlanFactory(Translator translator = null)
		{
			this._translator = translator ?? new Translator();
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000AD0F8 File Offset: 0x000AB2F8
		public virtual ObjectQueryExecutionPlan Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, bool streaming, Span span, IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> compiledQueryParameters, AliasGenerator aliasGenerator)
		{
			TypeUsage resultType = tree.Query.ResultType;
			DbExpression dbExpression;
			SpanIndex spanIndex;
			if (ObjectSpanRewriter.TryRewrite(tree, span, mergeOption, aliasGenerator, out dbExpression, out spanIndex))
			{
				tree = DbQueryCommandTree.FromValidExpression(tree.MetadataWorkspace, tree.DataSpace, dbExpression, tree.UseDatabaseNullSemantics, tree.DisableFilterOverProjectionSimplificationForCustomFunctions);
			}
			else
			{
				spanIndex = null;
			}
			EntityCommandDefinition entityCommandDefinition = ObjectQueryExecutionPlanFactory.CreateCommandDefinition(context, tree);
			ShaperFactory shaperFactory = Translator.TranslateColumnMap(this._translator, elementType, entityCommandDefinition.CreateColumnMap(null), context.MetadataWorkspace, spanIndex, mergeOption, streaming, false);
			EntitySet entitySet = null;
			if (resultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType && entityCommandDefinition.EntitySets != null)
			{
				foreach (EntitySet entitySet2 in entityCommandDefinition.EntitySets)
				{
					if (entitySet2 != null && entitySet2.ElementType.IsAssignableFrom(((CollectionType)resultType.EdmType).TypeUsage.EdmType))
					{
						if (entitySet != null)
						{
							entitySet = null;
							break;
						}
						entitySet = entitySet2;
					}
				}
			}
			return new ObjectQueryExecutionPlan(entityCommandDefinition, shaperFactory, resultType, mergeOption, streaming, entitySet, compiledQueryParameters);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000AD214 File Offset: 0x000AB414
		private static EntityCommandDefinition CreateCommandDefinition(ObjectContext context, DbQueryCommandTree tree)
		{
			DbConnection connection = context.Connection;
			if (connection == null)
			{
				throw new InvalidOperationException(Strings.ObjectQuery_InvalidConnection);
			}
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(connection);
			DbCommandDefinition dbCommandDefinition;
			try
			{
				dbCommandDefinition = providerServices.CreateCommandDefinition(tree, context.InterceptionContext);
			}
			catch (EntityCommandCompilationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandCompilationException(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
			if (dbCommandDefinition == null)
			{
				throw new NotSupportedException(Strings.ADP_ProviderDoesNotSupportCommandTrees);
			}
			return (EntityCommandDefinition)dbCommandDefinition;
		}

		// Token: 0x04001160 RID: 4448
		private readonly Translator _translator;
	}
}
