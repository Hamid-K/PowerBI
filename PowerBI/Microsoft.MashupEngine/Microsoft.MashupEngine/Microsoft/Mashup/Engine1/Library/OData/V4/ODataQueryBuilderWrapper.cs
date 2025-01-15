using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriBuilder;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000882 RID: 2178
	internal static class ODataQueryBuilderWrapper
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x000CD1C4 File Offset: 0x000CB3C4
		public static ODataUri GetUnboundFunctionUri(Uri serviceUri, Microsoft.OData.Edm.IEdmFunctionImport functionImport, IEnumerable<OperationSegmentParameter> parameters)
		{
			return ODataQueryBuilderWrapper.GetODataUri(serviceUri, new ODataPath(new ODataPathSegment[] { ODataQueryBuilderWrapper.CreateOperationImportSegment(functionImport, null, parameters) }), RowRange.All, null, null, null, null, null);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x000CD200 File Offset: 0x000CB400
		public static ODataUri GetBoundFunctionUri(Uri entryUri, Microsoft.OData.Edm.IEdmFunction function, IEnumerable<OperationSegmentParameter> parameters)
		{
			return ODataQueryBuilderWrapper.GetODataUri(entryUri, new ODataPath(new ODataPathSegment[]
			{
				new OperationSegment(new Microsoft.OData.Edm.IEdmOperation[] { function }, parameters, null)
			}), RowRange.All, null, null, null, null, null);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x000CD244 File Offset: 0x000CB444
		public static Uri BuildUri(Uri serviceUri, ODataPath odataPath, RowRange range, OrderByClause orderByClause, FilterClause filterClause, SelectExpandClause selectExpandClause, ApplyClause applyClause, bool? countQuery)
		{
			return ODataQueryBuilderWrapper.GetODataUri(serviceUri, odataPath, range, orderByClause, filterClause, selectExpandClause, countQuery, applyClause).GetUri();
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000CD25C File Offset: 0x000CB45C
		public static OperationImportSegment CreateOperationImportSegment(Microsoft.OData.Edm.IEdmFunctionImport functionImport, Microsoft.OData.Edm.IEdmEntitySet entitySet, IEnumerable<OperationSegmentParameter> parameters)
		{
			OperationImportSegment operationImportSegment = new OperationImportSegment(new Microsoft.OData.Edm.IEdmOperationImport[] { functionImport }, entitySet, parameters);
			ODataQueryBuilderWrapper.WorkAroundSetOperationImportIdentifierValue(functionImport.Name, operationImportSegment);
			return operationImportSegment;
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000CD288 File Offset: 0x000CB488
		public static Uri GetUri(this ODataUri odataUri)
		{
			return new ODataUriBuilderWrapper(ODataUrlConventions.Default, odataUri).BuildUri();
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000CD29C File Offset: 0x000CB49C
		private static ODataUri GetODataUri(Uri serviceUri, ODataPath odataPath, RowRange range, OrderByClause orderByClause, FilterClause filterClause, SelectExpandClause selectExpandClause, bool? countQuery, ApplyClause applyClause = null)
		{
			ODataUri odataUri = new ODataUri();
			odataUri.ServiceRoot = serviceUri;
			odataUri.QueryCount = countQuery;
			ODataQueryBuilderWrapper.AddTopSkipIfRequired(odataUri, range);
			odataUri.OrderBy = orderByClause;
			odataUri.Filter = filterClause;
			odataUri.SelectAndExpand = selectExpandClause;
			odataUri.Apply = applyClause;
			odataUri.Path = odataPath;
			return odataUri;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000CD2EC File Offset: 0x000CB4EC
		private static void AddTopSkipIfRequired(ODataUri odataUri, RowRange range)
		{
			if (!range.IsAll)
			{
				RowCount takeCount = range.TakeCount;
				if (!takeCount.IsInfinite)
				{
					odataUri.Top = new long?(takeCount.Value);
				}
				RowCount skipCount = range.SkipCount;
				if (!skipCount.IsZero)
				{
					odataUri.Skip = new long?(skipCount.Value);
				}
			}
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000CD348 File Offset: 0x000CB548
		private static void WorkAroundSetOperationImportIdentifierValue(string functionImportName, OperationImportSegment segment)
		{
			ODataQueryBuilderWrapper.IdentifierPropertyInfo.SetValue(segment, functionImportName, new object[0]);
		}

		// Token: 0x040020E4 RID: 8420
		private static readonly PropertyInfo IdentifierPropertyInfo = typeof(OperationImportSegment).GetProperty("Identifier", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
