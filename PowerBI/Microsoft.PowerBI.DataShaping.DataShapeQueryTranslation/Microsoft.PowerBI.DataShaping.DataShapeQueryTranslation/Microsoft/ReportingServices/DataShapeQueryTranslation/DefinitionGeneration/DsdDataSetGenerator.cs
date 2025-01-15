using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DefinitionGeneration
{
	// Token: 0x020000BA RID: 186
	internal sealed class DsdDataSetGenerator
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x0001E8EC File Offset: 0x0001CAEC
		private DsdDataSetGenerator(QueryTrimmer getGroupsToTrimFromQuery, string dataSourceId, string dataSetId, string tableId, ExtensionSchema extensionSchema, TranslationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			this.m_getGroupsToTrimFromQuery = getGroupsToTrimFromQuery;
			this.m_dataSourceId = dataSourceId;
			this.m_dataSetId = dataSetId;
			this.m_tableId = tableId;
			this.m_extensionSchema = extensionSchema;
			this.m_errorContext = errorContext;
			this.m_featureSwitchProvider = featureSwitchProvider;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001E93C File Offset: 0x0001CB3C
		public static DataSet CreateDataSet(string dataSourceId, string dataSetId, string tableId, TranslationErrorContext errorContext, QueryDefinition query, QueryTrimmer getGroupsToTrimFromQuery, bool isReusable, ExtensionSchema extensionSchema, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken, IList<QueryParameter> queryParameters)
		{
			if (query == null)
			{
				return null;
			}
			return new DsdDataSetGenerator(getGroupsToTrimFromQuery, dataSourceId, dataSetId, tableId, extensionSchema, errorContext, featureSwitchProvider, cancellationToken).Generate(query, isReusable, queryParameters);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001E960 File Offset: 0x0001CB60
		private DataSet Generate(QueryDefinition query, bool isReusable, IList<QueryParameter> queryParameters)
		{
			QueryTranslationResult queryTranslationResult = this.TranslateQuery(query);
			string commandText = queryTranslationResult.CommandText;
			IList<Field> list = this.CreateFields(query, queryTranslationResult);
			ResultTable resultTable = new ResultTable
			{
				Fields = list,
				Id = this.m_tableId,
				IsReusable = isReusable
			};
			return new DataSet
			{
				Query = commandText,
				Id = this.m_dataSetId,
				DataSourceId = this.m_dataSourceId,
				ResultTables = new List<ResultTable>(1) { resultTable },
				QuerySourceMap = DefinitionGenerationUtils.BuildQuerySourceMap(queryTranslationResult.QuerySourceMap),
				QueryParameters = queryParameters
			};
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		private IList<Field> CreateFields(QueryDefinition query, QueryTranslationResult translationResult)
		{
			List<Field> list = new List<Field>();
			ReadOnlyQdmNamedItemCollection<INamedProjection> readOnlyQdmNamedItemCollection = null;
			if (query != null)
			{
				readOnlyQdmNamedItemCollection = query.Projections;
			}
			foreach (QueryResultField queryResultField in translationResult.ResultFields)
			{
				if (readOnlyQdmNamedItemCollection != null)
				{
					INamedProjection namedProjection = readOnlyQdmNamedItemCollection[queryResultField.Field.EdmName];
					if (namedProjection == null)
					{
						continue;
					}
					Contract.RetailAssert(namedProjection.Name == queryResultField.Field.EdmName, "queryProjection.Name == resultField.Field.EdmName");
				}
				list.Add(DsdDataSetGenerator.CreateField(queryResultField, false));
			}
			return list;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001EA98 File Offset: 0x0001CC98
		internal static Field CreateField(QueryResultField queryField, bool generateComposableQueryColumnNames)
		{
			return new Field
			{
				Id = queryField.Field.EdmName,
				DataField = (generateComposableQueryColumnNames ? queryField.RawUnqualifiedFieldName : queryField.DataFieldName),
				SortInformation = DsdDataSetGenerator.CreateSortInformation(queryField.SortInfo)
			};
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		internal static SortInformation CreateSortInformation(QueryResultFieldSortInformation querySortInformation)
		{
			if (querySortInformation == null)
			{
				return null;
			}
			return new SortInformation
			{
				SortIndex = querySortInformation.Index,
				SortDirection = querySortInformation.Direction.ToDsdSortDirection()
			};
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001EB04 File Offset: 0x0001CD04
		private QueryTranslationResult TranslateQuery(QueryDefinition query)
		{
			QueryTranslationResult queryTranslationResult;
			try
			{
				queryTranslationResult = query.Translate(this.m_featureSwitchProvider, this.m_cancellationToken, this.m_getGroupsToTrimFromQuery);
			}
			catch (CommandTreeTranslationException ex)
			{
				DefinitionGenerationUtils.HandleCommandTreeTranslationException(ex, this.m_errorContext, this.m_dataSetId);
				throw;
			}
			return queryTranslationResult;
		}

		// Token: 0x040003F0 RID: 1008
		private readonly QueryTrimmer m_getGroupsToTrimFromQuery;

		// Token: 0x040003F1 RID: 1009
		private readonly string m_dataSourceId;

		// Token: 0x040003F2 RID: 1010
		private readonly string m_dataSetId;

		// Token: 0x040003F3 RID: 1011
		private readonly string m_tableId;

		// Token: 0x040003F4 RID: 1012
		private readonly ExtensionSchema m_extensionSchema;

		// Token: 0x040003F5 RID: 1013
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040003F6 RID: 1014
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x040003F7 RID: 1015
		private readonly CancellationToken m_cancellationToken;
	}
}
