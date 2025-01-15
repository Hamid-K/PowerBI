using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B4 RID: 180
	public class ApplyQueryOption
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0001583C File Offset: 0x00013A3C
		public ApplyQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			if (queryOptionParser == null)
			{
				throw Error.ArgumentNull("queryOptionParser");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this._queryOptionParser = queryOptionParser;
			this.ResultClrType = this.Context.ElementClrType;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000158A4 File Offset: 0x00013AA4
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x000158AC File Offset: 0x00013AAC
		public ODataQueryContext Context { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000158B5 File Offset: 0x00013AB5
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x000158BD File Offset: 0x00013ABD
		public Type ResultClrType { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000158C6 File Offset: 0x00013AC6
		public ApplyClause ApplyClause
		{
			get
			{
				if (this._applyClause == null)
				{
					this._applyClause = this._queryOptionParser.ParseApply();
				}
				return this._applyClause;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000158E7 File Offset: 0x00013AE7
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x000158EF File Offset: 0x00013AEF
		public string RawValue { get; private set; }

		// Token: 0x06000622 RID: 1570 RVA: 0x000158F8 File Offset: 0x00013AF8
		public IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings)
		{
			if (query == null)
			{
				throw Error.ArgumentNull("query");
			}
			if (querySettings == null)
			{
				throw Error.ArgumentNull("querySettings");
			}
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			if (query.Provider.GetType().Namespace == "System.Data.Linq")
			{
				throw Error.NotSupported(SRResources.ApplyQueryOptionNotSupportedForLinq2SQL, new object[0]);
			}
			ApplyClause applyClause = this.ApplyClause;
			ODataQuerySettings odataQuerySettings = this.Context.UpdateQuerySettings(querySettings, query);
			IWebApiAssembliesResolver webApiAssembliesResolver = WebApiAssembliesResolver.Default;
			if (this.Context.RequestContainer != null)
			{
				IWebApiAssembliesResolver service = ServiceProviderServiceExtensions.GetService<IWebApiAssembliesResolver>(this.Context.RequestContainer);
				if (service != null)
				{
					webApiAssembliesResolver = service;
				}
			}
			foreach (TransformationNode transformationNode in applyClause.Transformations)
			{
				if (transformationNode.Kind == TransformationNodeKind.Aggregate || transformationNode.Kind == TransformationNodeKind.GroupBy)
				{
					AggregationBinder aggregationBinder = new AggregationBinder(odataQuerySettings, webApiAssembliesResolver, this.ResultClrType, this.Context.Model, transformationNode);
					query = aggregationBinder.Bind(query);
					this.ResultClrType = aggregationBinder.ResultClrType;
				}
				else if (transformationNode.Kind == TransformationNodeKind.Filter)
				{
					FilterTransformationNode filterTransformationNode = transformationNode as FilterTransformationNode;
					Expression expression = FilterBinder.Bind(query, filterTransformationNode.FilterClause, this.ResultClrType, this.Context, querySettings);
					query = ExpressionHelpers.Where(query, expression, this.ResultClrType);
				}
			}
			return query;
		}

		// Token: 0x04000172 RID: 370
		private ApplyClause _applyClause;

		// Token: 0x04000173 RID: 371
		private ODataQueryOptionParser _queryOptionParser;
	}
}
