using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D5 RID: 213
	public class OrderByQueryOption
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00018178 File Offset: 0x00016378
		public OrderByQueryOption(string rawValue, ODataQueryContext context, ODataQueryOptionParser queryOptionParser)
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
			this.Validator = OrderByQueryValidator.GetOrderByQueryValidator(context);
			this._queryOptionParser = queryOptionParser;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000181DC File Offset: 0x000163DC
		internal OrderByQueryOption(string rawValue, ODataQueryContext context, string applyRaw)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (string.IsNullOrEmpty(rawValue))
			{
				throw Error.ArgumentNullOrEmpty("rawValue");
			}
			if (applyRaw == null)
			{
				throw Error.ArgumentNullOrEmpty("applyRaw");
			}
			this.Context = context;
			this.RawValue = rawValue;
			this.Validator = OrderByQueryValidator.GetOrderByQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string>
			{
				{ "$orderby", rawValue },
				{ "$apply", applyRaw }
			}, context.RequestContainer);
			this._queryOptionParser.ParseApply();
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00018284 File Offset: 0x00016484
		internal OrderByQueryOption(string rawValue, ODataQueryContext context)
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
			this.Validator = OrderByQueryValidator.GetOrderByQueryValidator(context);
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, new Dictionary<string, string> { { "$orderby", rawValue } }, context.RequestContainer);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00018308 File Offset: 0x00016508
		internal OrderByQueryOption(OrderByQueryOption orderBy)
		{
			this.Context = orderBy.Context;
			this.RawValue = orderBy.RawValue;
			this.Validator = orderBy.Validator;
			this._queryOptionParser = orderBy._queryOptionParser;
			this._orderByClause = orderBy._orderByClause;
			this._orderByNodes = orderBy._orderByNodes;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00018363 File Offset: 0x00016563
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001836B File Offset: 0x0001656B
		public ODataQueryContext Context { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00018374 File Offset: 0x00016574
		public IList<OrderByNode> OrderByNodes
		{
			get
			{
				if (this._orderByNodes == null)
				{
					this._orderByNodes = OrderByNode.CreateCollection(this.OrderByClause);
				}
				return this._orderByNodes;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00018395 File Offset: 0x00016595
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001839D File Offset: 0x0001659D
		public string RawValue { get; private set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x000183A6 File Offset: 0x000165A6
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x000183AE File Offset: 0x000165AE
		public OrderByQueryValidator Validator { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x000183B7 File Offset: 0x000165B7
		public OrderByClause OrderByClause
		{
			get
			{
				if (this._orderByClause == null)
				{
					this._orderByClause = this._queryOptionParser.ParseOrderBy();
					this._orderByClause = this.TranslateParameterAlias(this._orderByClause);
				}
				return this._orderByClause;
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000183EA File Offset: 0x000165EA
		public IOrderedQueryable<T> ApplyTo<T>(IQueryable<T> query)
		{
			return this.ApplyToCore(query, new ODataQuerySettings()) as IOrderedQueryable<T>;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000183FD File Offset: 0x000165FD
		public IOrderedQueryable<T> ApplyTo<T>(IQueryable<T> query, ODataQuerySettings querySettings)
		{
			return this.ApplyToCore(query, querySettings) as IOrderedQueryable<T>;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001840C File Offset: 0x0001660C
		public IOrderedQueryable ApplyTo(IQueryable query)
		{
			return this.ApplyToCore(query, new ODataQuerySettings());
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001841A File Offset: 0x0001661A
		public IOrderedQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings)
		{
			return this.ApplyToCore(query, querySettings);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00018424 File Offset: 0x00016624
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

		// Token: 0x06000730 RID: 1840 RVA: 0x0001844C File Offset: 0x0001664C
		private IOrderedQueryable ApplyToCore(IQueryable query, ODataQuerySettings querySettings)
		{
			if (this.Context.ElementClrType == null)
			{
				throw Error.NotSupported(SRResources.ApplyToOnUntypedQueryOption, new object[] { "ApplyTo" });
			}
			IEnumerable<OrderByNode> orderByNodes = this.OrderByNodes;
			bool flag = false;
			IQueryable queryable = query;
			HashSet<object> hashSet = new HashSet<object>();
			HashSet<string> hashSet2 = new HashSet<string>();
			bool flag2 = false;
			foreach (OrderByNode orderByNode in orderByNodes)
			{
				OrderByPropertyNode orderByPropertyNode = orderByNode as OrderByPropertyNode;
				OrderByOpenPropertyNode orderByOpenPropertyNode = orderByNode as OrderByOpenPropertyNode;
				OrderByCountNode orderByCountNode = orderByNode as OrderByCountNode;
				if (orderByPropertyNode != null)
				{
					var <>f__AnonymousType = new { orderByPropertyNode.Property, orderByPropertyNode.PropertyPath };
					OrderByDirection direction = orderByPropertyNode.Direction;
					if (hashSet.Contains(<>f__AnonymousType))
					{
						throw new ODataException(Error.Format(SRResources.OrderByDuplicateProperty, new object[] { <>f__AnonymousType.PropertyPath }));
					}
					hashSet.Add(<>f__AnonymousType);
					if (orderByPropertyNode.OrderByClause != null)
					{
						queryable = this.AddOrderByQueryForProperty(query, querySettings, orderByPropertyNode.OrderByClause, queryable, direction, flag);
					}
					else
					{
						queryable = ExpressionHelpers.OrderByProperty(queryable, this.Context.Model, <>f__AnonymousType.Property, direction, this.Context.ElementClrType, flag);
					}
					flag = true;
				}
				else if (orderByOpenPropertyNode != null)
				{
					if (hashSet2.Contains(orderByOpenPropertyNode.PropertyName))
					{
						throw new ODataException(Error.Format(SRResources.OrderByDuplicateProperty, new object[] { orderByOpenPropertyNode.PropertyPath }));
					}
					hashSet2.Add(orderByOpenPropertyNode.PropertyName);
					queryable = this.AddOrderByQueryForProperty(query, querySettings, orderByOpenPropertyNode.OrderByClause, queryable, orderByOpenPropertyNode.Direction, flag);
					flag = true;
				}
				else if (orderByCountNode != null)
				{
					queryable = this.AddOrderByQueryForProperty(query, querySettings, orderByCountNode.OrderByClause, queryable, orderByCountNode.Direction, flag);
					flag = true;
				}
				else
				{
					if (flag2)
					{
						throw new ODataException(Error.Format(SRResources.OrderByDuplicateIt, new object[0]));
					}
					queryable = ExpressionHelpers.OrderByIt(queryable, orderByNode.Direction, this.Context.ElementClrType, flag);
					flag = true;
					flag2 = true;
				}
			}
			return queryable as IOrderedQueryable;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00018668 File Offset: 0x00016868
		private IQueryable AddOrderByQueryForProperty(IQueryable query, ODataQuerySettings querySettings, OrderByClause orderbyClause, IQueryable querySoFar, OrderByDirection direction, bool alreadyOrdered)
		{
			ODataQuerySettings odataQuerySettings = this.Context.UpdateQuerySettings(querySettings, query);
			LambdaExpression lambdaExpression = FilterBinder.Bind(query, orderbyClause, this.Context.ElementClrType, this.Context, odataQuerySettings);
			querySoFar = ExpressionHelpers.OrderBy(querySoFar, lambdaExpression, direction, this.Context.ElementClrType, alreadyOrdered);
			return querySoFar;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000186B8 File Offset: 0x000168B8
		private OrderByClause TranslateParameterAlias(OrderByClause orderBy)
		{
			if (orderBy == null)
			{
				return null;
			}
			SingleValueNode singleValueNode = orderBy.Expression.Accept<QueryNode>(new ParameterAliasNodeTranslator(this._queryOptionParser.ParameterAliasNodes)) as SingleValueNode;
			singleValueNode = singleValueNode ?? new ConstantNode(null, "null");
			return new OrderByClause(this.TranslateParameterAlias(orderBy.ThenBy), singleValueNode, orderBy.Direction, orderBy.RangeVariable);
		}

		// Token: 0x0400021F RID: 543
		private OrderByClause _orderByClause;

		// Token: 0x04000220 RID: 544
		private IList<OrderByNode> _orderByNodes;

		// Token: 0x04000221 RID: 545
		private ODataQueryOptionParser _queryOptionParser;
	}
}
