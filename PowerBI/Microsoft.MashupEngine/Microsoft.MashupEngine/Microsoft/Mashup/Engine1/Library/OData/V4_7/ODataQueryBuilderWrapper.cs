using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000771 RID: 1905
	internal static class ODataQueryBuilderWrapper
	{
		// Token: 0x06003812 RID: 14354 RVA: 0x000B3B10 File Offset: 0x000B1D10
		public static ODataUri GetUnboundFunctionUri(Uri serviceUri, Microsoft.OData.Edm.IEdmFunctionImport functionImport, IEnumerable<OperationSegmentParameter> parameters)
		{
			return ODataQueryBuilderWrapper.GetODataUri(serviceUri, new ODataPath(new ODataPathSegment[] { ODataQueryBuilderWrapper.CreateOperationImportSegment(functionImport, null, parameters) }), RowRange.All, null, null, null, null, null);
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000B3B4C File Offset: 0x000B1D4C
		public static ODataUri GetBoundFunctionUri(Uri entryUri, Microsoft.OData.Edm.IEdmFunction function, IEnumerable<OperationSegmentParameter> parameters)
		{
			return ODataQueryBuilderWrapper.GetODataUri(entryUri, new ODataPath(new ODataPathSegment[]
			{
				new OperationSegment(new Microsoft.OData.Edm.IEdmOperation[] { function }, parameters, null)
			}), RowRange.All, null, null, null, null, null);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000B3B90 File Offset: 0x000B1D90
		public static Uri BuildUri(Uri serviceUri, ODataPath odataPath, RowRange range, OrderByClause orderByClause, FilterClause filterClause, SelectExpandClause selectExpandClause, ApplyClause applyClause, bool? countQuery)
		{
			return ODataQueryBuilderWrapper.GetODataUri(serviceUri, odataPath, range, orderByClause, filterClause, selectExpandClause, countQuery, applyClause).GetUri();
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000B3BA8 File Offset: 0x000B1DA8
		public static OperationImportSegment CreateOperationImportSegment(Microsoft.OData.Edm.IEdmFunctionImport functionImport, Microsoft.OData.Edm.IEdmEntitySetBase entitySet, IEnumerable<OperationSegmentParameter> parameters)
		{
			return new OperationImportSegment(new Microsoft.OData.Edm.IEdmOperationImport[] { functionImport }, entitySet, parameters);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000B3BBB File Offset: 0x000B1DBB
		public static Uri GetUri(this ODataUri odataUri)
		{
			return new ODataUriBuilderWrapper(ODataUrlKeyDelimiter.Parentheses, odataUri).BuildUri();
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000B3BD0 File Offset: 0x000B1DD0
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

		// Token: 0x06003818 RID: 14360 RVA: 0x000B3C20 File Offset: 0x000B1E20
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
	}
}
