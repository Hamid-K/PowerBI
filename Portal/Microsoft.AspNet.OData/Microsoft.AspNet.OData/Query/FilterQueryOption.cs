using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D3 RID: 211
	public class FilterQueryOption
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x00017E8C File Offset: 0x0001608C
		public FilterQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
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
			this.Validator = FilterQueryValidator.GetFilterQueryValidator(context);
			this._queryOptionParser = queryOptionParser;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00017EF0 File Offset: 0x000160F0
		internal FilterQueryOption(string rawValue, ODataQueryContext context)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this.Validator = FilterQueryValidator.GetFilterQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string> { { "$filter", rawValue } }, context.RequestContainer);
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00017F72 File Offset: 0x00016172
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00017F7A File Offset: 0x0001617A
		public ODataQueryContext Context { get; private set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00017F83 File Offset: 0x00016183
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00017F8B File Offset: 0x0001618B
		public FilterQueryValidator Validator { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00017F94 File Offset: 0x00016194
		public FilterClause FilterClause
		{
			get
			{
				if (this._filterClause == null)
				{
					this._filterClause = this._queryOptionParser.ParseFilter();
					SingleValueNode singleValueNode = this._filterClause.Expression.Accept<QueryNode>(new ParameterAliasNodeTranslator(this._queryOptionParser.ParameterAliasNodes)) as SingleValueNode;
					singleValueNode = singleValueNode ?? new ConstantNode(null);
					this._filterClause = new FilterClause(singleValueNode, this._filterClause.RangeVariable);
				}
				return this._filterClause;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00018009 File Offset: 0x00016209
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00018011 File Offset: 0x00016211
		public string RawValue { get; private set; }

		// Token: 0x06000717 RID: 1815 RVA: 0x0001801C File Offset: 0x0001621C
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
			FilterClause filterClause = this.FilterClause;
			ODataQuerySettings odataQuerySettings = this.Context.UpdateQuerySettings(querySettings, query);
			Expression expression = FilterBinder.Bind(query, filterClause, this.Context.ElementClrType, this.Context, odataQuerySettings);
			query = ExpressionHelpers.Where(query, expression, this.Context.ElementClrType);
			return query;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000180B5 File Offset: 0x000162B5
		public void Validate(ODataValidationSettings validationSettings)
		{
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			if (this.Validator != null)
			{
				this.Validator.Validate(this, validationSettings);
			}
		}

		// Token: 0x04000218 RID: 536
		private FilterClause _filterClause;

		// Token: 0x04000219 RID: 537
		private ODataQueryOptionParser _queryOptionParser;
	}
}
