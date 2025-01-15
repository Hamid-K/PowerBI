using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000A4 RID: 164
	[ODataQueryParameterBinding]
	public class ODataQueryOptions
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x000130C8 File Offset: 0x000112C8
		public ODataQueryOptions(ODataQueryContext context, HttpRequestMessage request)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			context.RequestContainer = request.GetRequestContainer();
			this.Context = context;
			this.Request = request;
			this.InternalRequest = new WebApiRequestMessage(request);
			this.InternalHeaders = new WebApiRequestHeaders(request.Headers);
			this.Initialize(context);
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00013135 File Offset: 0x00011335
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001313D File Offset: 0x0001133D
		public HttpRequestMessage Request { get; private set; }

		// Token: 0x06000588 RID: 1416 RVA: 0x00013148 File Offset: 0x00011348
		private void Initialize(ODataQueryContext context)
		{
			ODataUriResolver requiredService = ServiceProviderServiceExtensions.GetRequiredService<ODataUriResolver>(context.RequestContainer);
			if (requiredService != null)
			{
				this._enableNoDollarSignQueryOptions = requiredService.EnableNoDollarQueryOptions;
			}
			this.RawValues = new ODataRawQueryOptions();
			IDictionary<string, string> odataQueryParameters = this.GetODataQueryParameters();
			this._queryOptionParser = new ODataQueryOptionParser(context.Model, context.ElementType, context.NavigationSource, odataQueryParameters);
			this._queryOptionParser.Resolver = ServiceProviderServiceExtensions.GetRequiredService<ODataUriResolver>(context.RequestContainer);
			this.BuildQueryOptions(odataQueryParameters);
			this.Validator = ODataQueryValidator.GetODataQueryValidator(context);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000131C9 File Offset: 0x000113C9
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x000131D1 File Offset: 0x000113D1
		internal IWebApiRequestMessage InternalRequest { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000131DA File Offset: 0x000113DA
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x000131E2 File Offset: 0x000113E2
		public ODataQueryContext Context { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000131EB File Offset: 0x000113EB
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x000131F3 File Offset: 0x000113F3
		public ODataRawQueryOptions RawValues { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x000131FC File Offset: 0x000113FC
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00013204 File Offset: 0x00011404
		public SelectExpandQueryOption SelectExpand { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001320D File Offset: 0x0001140D
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x00013215 File Offset: 0x00011415
		public ApplyQueryOption Apply { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001321E File Offset: 0x0001141E
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00013226 File Offset: 0x00011426
		public FilterQueryOption Filter { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001322F File Offset: 0x0001142F
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00013237 File Offset: 0x00011437
		public OrderByQueryOption OrderBy { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00013240 File Offset: 0x00011440
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00013248 File Offset: 0x00011448
		public SkipQueryOption Skip { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00013251 File Offset: 0x00011451
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00013259 File Offset: 0x00011459
		public SkipTokenQueryOption SkipToken { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00013262 File Offset: 0x00011462
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001326A File Offset: 0x0001146A
		public TopQueryOption Top { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00013273 File Offset: 0x00011473
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001327B File Offset: 0x0001147B
		public CountQueryOption Count { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00013284 File Offset: 0x00011484
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001328C File Offset: 0x0001148C
		public ODataQueryValidator Validator { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00013295 File Offset: 0x00011495
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0001329D File Offset: 0x0001149D
		private IWebApiHeaders InternalHeaders { get; set; }

		// Token: 0x060005A3 RID: 1443 RVA: 0x000132A6 File Offset: 0x000114A6
		public static bool IsSystemQueryOption(string queryOptionName)
		{
			return ODataQueryOptions.IsSystemQueryOption(queryOptionName, false);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000132B0 File Offset: 0x000114B0
		public static bool IsSystemQueryOption(string queryOptionName, bool isDollarSignOptional)
		{
			string text = queryOptionName;
			if (isDollarSignOptional && !queryOptionName.StartsWith("$", StringComparison.Ordinal))
			{
				text = "$" + queryOptionName;
			}
			return text.Equals("$orderby", StringComparison.Ordinal) || text.Equals("$filter", StringComparison.Ordinal) || text.Equals("$top", StringComparison.Ordinal) || text.Equals("$skip", StringComparison.Ordinal) || text.Equals("$count", StringComparison.Ordinal) || text.Equals("$expand", StringComparison.Ordinal) || text.Equals("$select", StringComparison.Ordinal) || text.Equals("$format", StringComparison.Ordinal) || text.Equals("$skiptoken", StringComparison.Ordinal) || text.Equals("$deltatoken", StringComparison.Ordinal) || text.Equals("$apply", StringComparison.Ordinal);
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001337C File Offset: 0x0001157C
		public virtual ETag IfMatch
		{
			get
			{
				IEnumerable<string> enumerable;
				if (!this._etagIfMatchChecked && this._etagIfMatch == null && this.InternalHeaders.TryGetValues("If-Match", out enumerable))
				{
					EntityTagHeaderValue entityTagHeaderValue = EntityTagHeaderValue.Parse(enumerable.SingleOrDefault<string>());
					this._etagIfMatch = this.GetETag(entityTagHeaderValue);
					this._etagIfMatchChecked = true;
				}
				return this._etagIfMatch;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000133D4 File Offset: 0x000115D4
		public virtual ETag IfNoneMatch
		{
			get
			{
				if (!this._etagIfNoneMatchChecked && this._etagIfNoneMatch == null)
				{
					IEnumerable<string> enumerable;
					if (this.InternalHeaders.TryGetValues("If-None-Match", out enumerable))
					{
						EntityTagHeaderValue entityTagHeaderValue = EntityTagHeaderValue.Parse(enumerable.SingleOrDefault<string>());
						this._etagIfNoneMatch = this.GetETag(entityTagHeaderValue);
						if (this._etagIfNoneMatch != null)
						{
							this._etagIfNoneMatch.IsIfNoneMatch = true;
						}
						this._etagIfNoneMatchChecked = true;
					}
					this._etagIfNoneMatchChecked = true;
				}
				return this._etagIfNoneMatch;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00013446 File Offset: 0x00011646
		internal virtual ETag GetETag(EntityTagHeaderValue etagHeaderValue)
		{
			return this.InternalRequest.GetETag(etagHeaderValue);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00013454 File Offset: 0x00011654
		public bool IsSupportedQueryOption(string queryOptionName)
		{
			if (!((this._queryOptionParser != null) ? this._queryOptionParser.Resolver : ServiceProviderServiceExtensions.GetRequiredService<ODataUriResolver>(this.Request.GetRequestContainer())).EnableCaseInsensitive)
			{
				return ODataQueryOptions.IsSystemQueryOption(queryOptionName, this._enableNoDollarSignQueryOptions);
			}
			return ODataQueryOptions.IsSystemQueryOption(queryOptionName.ToLowerInvariant(), this._enableNoDollarSignQueryOptions);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000134AB File Offset: 0x000116AB
		public virtual IQueryable ApplyTo(IQueryable query)
		{
			return this.ApplyTo(query, new ODataQuerySettings());
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000134B9 File Offset: 0x000116B9
		public virtual IQueryable ApplyTo(IQueryable query, AllowedQueryOptions ignoreQueryOptions)
		{
			this._ignoreQueryOptions = ignoreQueryOptions;
			return this.ApplyTo(query, new ODataQuerySettings());
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000134CE File Offset: 0x000116CE
		public virtual IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings, AllowedQueryOptions ignoreQueryOptions)
		{
			this._ignoreQueryOptions = ignoreQueryOptions;
			return this.ApplyTo(query, querySettings);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000134E0 File Offset: 0x000116E0
		public virtual IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings)
		{
			if (query == null)
			{
				throw Error.ArgumentNull("query");
			}
			if (querySettings == null)
			{
				throw Error.ArgumentNull("querySettings");
			}
			IQueryable queryable = query;
			if (this.IsAvailableODataQueryOption(this.Apply, AllowedQueryOptions.Apply))
			{
				queryable = this.Apply.ApplyTo(queryable, querySettings);
				this.InternalRequest.Context.ApplyClause = this.Apply.ApplyClause;
				this.Context.ElementClrType = this.Apply.ResultClrType;
			}
			if (this.IsAvailableODataQueryOption(this.Filter, AllowedQueryOptions.Filter))
			{
				queryable = this.Filter.ApplyTo(queryable, querySettings);
			}
			if (this.IsAvailableODataQueryOption(this.Count, AllowedQueryOptions.Count))
			{
				if (this.InternalRequest.Context.TotalCountFunc == null)
				{
					Func<long> entityCountFunc = this.Count.GetEntityCountFunc(queryable);
					if (entityCountFunc != null)
					{
						this.InternalRequest.Context.TotalCountFunc = entityCountFunc;
					}
				}
				if (this.InternalRequest.IsCountRequest())
				{
					return queryable;
				}
			}
			OrderByQueryOption orderByQueryOption = this.OrderBy;
			if (querySettings.EnsureStableOrdering && (this.IsAvailableODataQueryOption(this.Skip, AllowedQueryOptions.Skip) || this.IsAvailableODataQueryOption(this.Top, AllowedQueryOptions.Top) || querySettings.PageSize != null))
			{
				orderByQueryOption = this.GenerateStableOrder();
			}
			if (this.IsAvailableODataQueryOption(orderByQueryOption, AllowedQueryOptions.OrderBy))
			{
				queryable = orderByQueryOption.ApplyTo(queryable, querySettings);
			}
			if (this.IsAvailableODataQueryOption(this.SkipToken, AllowedQueryOptions.SkipToken))
			{
				queryable = this.SkipToken.ApplyTo(queryable, querySettings, this);
			}
			this.AddAutoSelectExpandProperties();
			if (this.SelectExpand != null)
			{
				IQueryable queryable2 = this.ApplySelectExpand<IQueryable>(queryable, querySettings);
				if (queryable2 != null)
				{
					queryable = queryable2;
				}
			}
			if (this.IsAvailableODataQueryOption(this.Skip, AllowedQueryOptions.Skip))
			{
				queryable = this.Skip.ApplyTo(queryable, querySettings);
			}
			if (this.IsAvailableODataQueryOption(this.Top, AllowedQueryOptions.Top))
			{
				queryable = this.Top.ApplyTo(queryable, querySettings);
			}
			return this.ApplyPaging(queryable, querySettings);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000136AC File Offset: 0x000118AC
		internal IQueryable ApplyPaging(IQueryable result, ODataQuerySettings querySettings)
		{
			int num = -1;
			if (querySettings.PageSize != null)
			{
				num = querySettings.PageSize.Value;
			}
			else if (querySettings.ModelBoundPageSize != null)
			{
				num = querySettings.ModelBoundPageSize.Value;
			}
			int num2 = -1;
			if (RequestPreferenceHelpers.RequestPrefersMaxPageSize(this.InternalRequest.Headers, out num2))
			{
				num = Math.Min(num, num2);
			}
			if (num > 0)
			{
				bool flag;
				result = ODataQueryOptions.LimitResults(result, num, querySettings.EnableConstantParameterization, out flag);
				if (flag && this.InternalRequest.RequestUri != null && this.InternalRequest.Context.NextLink == null)
				{
					this.InternalRequest.Context.PageSize = num;
				}
			}
			this.InternalRequest.Context.QueryOptions = this;
			return result;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00013780 File Offset: 0x00011980
		public virtual OrderByQueryOption GenerateStableOrder()
		{
			if (this._stableOrderBy != null)
			{
				return this._stableOrderBy;
			}
			List<string> applySortOptions = ODataQueryOptions.GetApplySortOptions((this.Apply != null) ? this.Apply.ApplyClause : null);
			this._stableOrderBy = ((this.OrderBy == null) ? this.GenerateDefaultOrderBy(this.Context, applySortOptions) : this.EnsureStableSortOrderBy(this.OrderBy, this.Context, applySortOptions));
			return this._stableOrderBy;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000137F0 File Offset: 0x000119F0
		private static List<string> GetApplySortOptions(ApplyClause apply)
		{
			Func<TransformationNode, bool> func = (TransformationNode t) => t.Kind == TransformationNodeKind.Aggregate || t.Kind == TransformationNodeKind.GroupBy;
			if (apply == null || !apply.Transformations.Any(func))
			{
				return null;
			}
			List<string> list = new List<string>();
			TransformationNode transformationNode = apply.Transformations.Last(func);
			if (transformationNode.Kind == TransformationNodeKind.Aggregate)
			{
				using (IEnumerator<AggregateExpression> enumerator = (transformationNode as AggregateTransformationNode).AggregateExpressions.OfType<AggregateExpression>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AggregateExpression aggregateExpression = enumerator.Current;
						list.Add(aggregateExpression.Alias);
					}
					return list;
				}
			}
			if (transformationNode.Kind == TransformationNodeKind.GroupBy)
			{
				IEnumerable<GroupByPropertyNode> groupingProperties = (transformationNode as GroupByTransformationNode).GroupingProperties;
				ODataQueryOptions.ExtractGroupingProperties(list, groupingProperties, null);
			}
			return list;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000138BC File Offset: 0x00011ABC
		private static void ExtractGroupingProperties(List<string> result, IEnumerable<GroupByPropertyNode> groupingProperties, string prefix = null)
		{
			foreach (GroupByPropertyNode groupByPropertyNode in groupingProperties)
			{
				string text = ((prefix != null) ? (prefix + "/" + groupByPropertyNode.Name) : groupByPropertyNode.Name);
				if (groupByPropertyNode.ChildTransformations != null && groupByPropertyNode.ChildTransformations.Any<GroupByPropertyNode>())
				{
					ODataQueryOptions.ExtractGroupingProperties(result, groupByPropertyNode.ChildTransformations, text);
				}
				else
				{
					result.Add(text);
				}
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00013948 File Offset: 0x00011B48
		public virtual object ApplyTo(object entity, ODataQuerySettings querySettings, AllowedQueryOptions ignoreQueryOptions)
		{
			this._ignoreQueryOptions = ignoreQueryOptions;
			return this.ApplyTo(entity, new ODataQuerySettings());
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00013960 File Offset: 0x00011B60
		public virtual object ApplyTo(object entity, ODataQuerySettings querySettings)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			if (querySettings == null)
			{
				throw Error.ArgumentNull("querySettings");
			}
			if (this.Filter != null || this.OrderBy != null || this.Top != null || this.Skip != null || this.Count != null)
			{
				throw Error.InvalidOperation(SRResources.NonSelectExpandOnSingleEntity, new object[0]);
			}
			this.AddAutoSelectExpandProperties();
			if (this.SelectExpand != null)
			{
				object obj = this.ApplySelectExpand<object>(entity, querySettings);
				if (obj != null)
				{
					return obj;
				}
			}
			return entity;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000139DF File Offset: 0x00011BDF
		public virtual void Validate(ODataValidationSettings validationSettings)
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

		// Token: 0x060005B4 RID: 1460 RVA: 0x00013A04 File Offset: 0x00011C04
		private static void ThrowIfEmpty(string queryValue, string queryName)
		{
			if (string.IsNullOrWhiteSpace(queryValue))
			{
				throw new ODataException(Error.Format(SRResources.QueryCannotBeEmpty, new object[] { queryName }));
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00013A28 File Offset: 0x00011C28
		private static IEnumerable<IEdmStructuralProperty> GetAvailableOrderByProperties(ODataQueryContext context)
		{
			IEdmEntityType edmEntityType = context.ElementType as IEdmEntityType;
			if (edmEntityType == null)
			{
				return Enumerable.Empty<IEdmStructuralProperty>();
			}
			IEnumerable<IEdmStructuralProperty> enumerable2;
			if (!edmEntityType.Key().Any<IEdmStructuralProperty>())
			{
				IEnumerable<IEdmStructuralProperty> enumerable = from property in edmEntityType.StructuralProperties()
					where property.Type.IsPrimitive() && !property.Type.IsStream()
					select property into p
					orderby p.Name
					select p;
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = edmEntityType.Key();
			}
			return enumerable2.ToList<IEdmStructuralProperty>();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00013AB4 File Offset: 0x00011CB4
		private OrderByQueryOption GenerateDefaultOrderBy(ODataQueryContext context, List<string> applySortOptions)
		{
			string text = string.Empty;
			if (applySortOptions != null)
			{
				text = string.Join(",", applySortOptions);
				return new OrderByQueryOption(text, context, this.Apply.RawValue);
			}
			text = string.Join(",", from property in ODataQueryOptions.GetAvailableOrderByProperties(context)
				select property.Name);
			if (!string.IsNullOrEmpty(text))
			{
				return new OrderByQueryOption(text, context);
			}
			return null;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00013B30 File Offset: 0x00011D30
		private OrderByQueryOption EnsureStableSortOrderBy(OrderByQueryOption orderBy, ODataQueryContext context, List<string> applySortOptions)
		{
			Func<OrderByPropertyNode, string> func;
			if (applySortOptions != null)
			{
				func = (OrderByPropertyNode node) => node.PropertyPath;
			}
			else
			{
				func = (OrderByPropertyNode node) => node.Property.Name;
			}
			HashSet<string> usedPropertyNames = new HashSet<string>(orderBy.OrderByNodes.OfType<OrderByPropertyNode>().Select(func));
			if (applySortOptions != null)
			{
				IOrderedEnumerable<string> orderedEnumerable = from p in applySortOptions
					where !usedPropertyNames.Contains(p)
					orderby p
					select p;
				if (orderedEnumerable.Any<string>())
				{
					orderBy = new OrderByQueryOption(orderBy.RawValue + "," + string.Join(",", orderedEnumerable), context, this.Apply.RawValue);
				}
			}
			else
			{
				IEnumerable<IEdmStructuralProperty> enumerable = from prop in ODataQueryOptions.GetAvailableOrderByProperties(context)
					where !usedPropertyNames.Contains(prop.Name)
					select prop;
				if (enumerable.Any<IEdmStructuralProperty>())
				{
					orderBy = new OrderByQueryOption(orderBy);
					foreach (IEdmStructuralProperty edmStructuralProperty in enumerable)
					{
						orderBy.OrderByNodes.Add(new OrderByPropertyNode(edmStructuralProperty, OrderByDirection.Ascending));
					}
				}
			}
			return orderBy;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00013C94 File Offset: 0x00011E94
		internal static IQueryable LimitResults(IQueryable queryable, int limit, bool parameterize, out bool resultsLimited)
		{
			MethodInfo methodInfo = ODataQueryOptions._limitResultsGenericMethod.MakeGenericMethod(new Type[] { queryable.ElementType });
			object[] array = new object[4];
			array[0] = queryable;
			array[1] = limit;
			array[2] = parameterize;
			object[] array2 = array;
			IQueryable queryable2 = methodInfo.Invoke(null, array2) as IQueryable;
			resultsLimited = (bool)array2[3];
			return queryable2;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00013CEF File Offset: 0x00011EEF
		public static IQueryable<T> LimitResults<T>(IQueryable<T> queryable, int limit, out bool resultsLimited)
		{
			return ODataQueryOptions.LimitResults<T>(queryable, limit, false, out resultsLimited);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00013CFC File Offset: 0x00011EFC
		public static IQueryable<T> LimitResults<T>(IQueryable<T> queryable, int limit, bool parameterize, out bool resultsLimited)
		{
			TruncatedCollection<T> truncatedCollection = new TruncatedCollection<T>(queryable, limit, parameterize);
			resultsLimited = truncatedCollection.IsTruncated;
			return truncatedCollection.AsQueryable<T>();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00013D20 File Offset: 0x00011F20
		internal void AddAutoSelectExpandProperties()
		{
			bool flag = false;
			string text = this.GetAutoExpandRawValue();
			string text2 = this.GetAutoSelectRawValue();
			IDictionary<string, string> odataQueryParameters = this.GetODataQueryParameters();
			if (!string.IsNullOrEmpty(text) && !text.Equals(this.RawValues.Expand))
			{
				odataQueryParameters["$expand"] = text;
				flag = true;
			}
			else
			{
				text = this.RawValues.Expand;
			}
			if (!string.IsNullOrEmpty(text2) && !text2.Equals(this.RawValues.Select))
			{
				odataQueryParameters["$select"] = text2;
				flag = true;
			}
			else
			{
				text2 = this.RawValues.Select;
			}
			if (flag)
			{
				this._queryOptionParser = new ODataQueryOptionParser(this.Context.Model, this.Context.ElementType, this.Context.NavigationSource, odataQueryParameters, this.Context.RequestContainer);
				SelectExpandQueryOption selectExpand = this.SelectExpand;
				this.SelectExpand = new SelectExpandQueryOption(text2, text, this.Context, this._queryOptionParser);
				if (selectExpand != null && selectExpand.LevelsMaxLiteralExpansionDepth > 0)
				{
					this.SelectExpand.LevelsMaxLiteralExpansionDepth = selectExpand.LevelsMaxLiteralExpansionDepth;
				}
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00013E30 File Offset: 0x00012030
		private IDictionary<string, string> GetODataQueryParameters()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> keyValuePair in this.InternalRequest.QueryParameters)
			{
				string text = keyValuePair.Key.Trim();
				if (!this._enableNoDollarSignQueryOptions)
				{
					if (text.StartsWith("$", StringComparison.Ordinal))
					{
						dictionary.Add(text, keyValuePair.Value);
					}
				}
				else if (this.IsSupportedQueryOption(keyValuePair.Key))
				{
					dictionary.Add((!text.StartsWith("$", StringComparison.Ordinal)) ? ("$" + text) : text, keyValuePair.Value);
				}
				if (text.StartsWith("@", StringComparison.Ordinal))
				{
					dictionary.Add(text, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00013F10 File Offset: 0x00012110
		private string GetAutoSelectRawValue()
		{
			string text = this.RawValues.Select;
			string text2 = string.Empty;
			IEdmEntityType edmEntityType = this.Context.TargetStructuredType as IEdmEntityType;
			if (string.IsNullOrEmpty(text))
			{
				foreach (IEdmStructuralProperty edmStructuralProperty in EdmLibHelpers.GetAutoSelectProperties(this.Context.TargetProperty, this.Context.TargetStructuredType, this.Context.Model, null))
				{
					if (!string.IsNullOrEmpty(text2))
					{
						text2 += ",";
					}
					if (edmEntityType != null && edmStructuralProperty.DeclaringType != edmEntityType)
					{
						text2 += string.Format(CultureInfo.InvariantCulture, "{0}/", new object[] { edmStructuralProperty.DeclaringType.FullTypeName() });
					}
					text2 += edmStructuralProperty.Name;
				}
				if (!string.IsNullOrEmpty(text2))
				{
					if (!string.IsNullOrEmpty(text))
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[] { text2, text });
					}
					else
					{
						text = text2;
					}
				}
			}
			return text;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00014034 File Offset: 0x00012234
		private string GetAutoExpandRawValue()
		{
			string text = this.RawValues.Expand;
			IEdmEntityType edmEntityType = this.Context.TargetStructuredType as IEdmEntityType;
			string text2 = string.Empty;
			foreach (IEdmNavigationProperty edmNavigationProperty in EdmLibHelpers.GetAutoExpandNavigationProperties(this.Context.TargetProperty, this.Context.TargetStructuredType, this.Context.Model, !string.IsNullOrEmpty(this.RawValues.Select), null))
			{
				if (!string.IsNullOrEmpty(text2))
				{
					text2 += ",";
				}
				if (edmNavigationProperty.DeclaringEntityType() != edmEntityType)
				{
					text2 += string.Format(CultureInfo.InvariantCulture, "{0}/", new object[] { edmNavigationProperty.DeclaringEntityType().FullTypeName() });
				}
				text2 += edmNavigationProperty.Name;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (!string.IsNullOrEmpty(text))
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[] { text2, text });
				}
				else
				{
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001415C File Offset: 0x0001235C
		private void BuildQueryOptions(IDictionary<string, string> queryParameters)
		{
			foreach (KeyValuePair<string, string> keyValuePair in queryParameters)
			{
				string text = keyValuePair.Key.ToLowerInvariant();
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1469037347U)
					{
						if (num <= 789605668U)
						{
							if (num != 456103474U)
							{
								if (num == 789605668U)
								{
									if (text == "$orderby")
									{
										ODataQueryOptions.ThrowIfEmpty(keyValuePair.Value, "$orderby");
										this.RawValues.OrderBy = keyValuePair.Value;
										this.OrderBy = new OrderByQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
									}
								}
							}
							else if (text == "$count")
							{
								ODataQueryOptions.ThrowIfEmpty(keyValuePair.Value, "$count");
								this.RawValues.Count = keyValuePair.Value;
								this.Count = new CountQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
							}
						}
						else if (num != 1171897744U)
						{
							if (num != 1211134378U)
							{
								if (num == 1469037347U)
								{
									if (text == "$select")
									{
										this.RawValues.Select = keyValuePair.Value;
									}
								}
							}
							else if (text == "$top")
							{
								ODataQueryOptions.ThrowIfEmpty(keyValuePair.Value, "$top");
								this.RawValues.Top = keyValuePair.Value;
								this.Top = new TopQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
							}
						}
						else if (text == "$deltatoken")
						{
							this.RawValues.DeltaToken = keyValuePair.Value;
						}
					}
					else if (num <= 2272632476U)
					{
						if (num != 1805205693U)
						{
							if (num != 2110202789U)
							{
								if (num == 2272632476U)
								{
									if (text == "$format")
									{
										this.RawValues.Format = keyValuePair.Value;
									}
								}
							}
							else if (text == "$apply")
							{
								ODataQueryOptions.ThrowIfEmpty(keyValuePair.Value, "$apply");
								this.RawValues.Apply = keyValuePair.Value;
								this.Apply = new ApplyQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
							}
						}
						else if (text == "$skiptoken")
						{
							this.RawValues.SkipToken = keyValuePair.Value;
							this.SkipToken = new SkipTokenQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
						}
					}
					else if (num != 2649853531U)
					{
						if (num != 3803867261U)
						{
							if (num == 4027612776U)
							{
								if (text == "$skip")
								{
									ODataQueryOptions.ThrowIfEmpty(keyValuePair.Value, "$skip");
									this.RawValues.Skip = keyValuePair.Value;
									this.Skip = new SkipQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
								}
							}
						}
						else if (text == "$filter")
						{
							ODataQueryOptions.ThrowIfEmpty(keyValuePair.Value, "$filter");
							this.RawValues.Filter = keyValuePair.Value;
							this.Filter = new FilterQueryOption(keyValuePair.Value, this.Context, this._queryOptionParser);
						}
					}
					else if (text == "$expand")
					{
						this.RawValues.Expand = keyValuePair.Value;
					}
				}
			}
			if (this.RawValues.Select != null || this.RawValues.Expand != null)
			{
				this.SelectExpand = new SelectExpandQueryOption(this.RawValues.Select, this.RawValues.Expand, this.Context, this._queryOptionParser);
			}
			if (this.InternalRequest.IsCountRequest())
			{
				this.Count = new CountQueryOption("true", this.Context, new ODataQueryOptionParser(this.Context.Model, this.Context.ElementType, this.Context.NavigationSource, new Dictionary<string, string> { { "$count", "true" } }, this.Context.RequestContainer));
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001462C File Offset: 0x0001282C
		private bool IsAvailableODataQueryOption(object queryOption, AllowedQueryOptions queryOptionFlag)
		{
			return queryOption != null && (this._ignoreQueryOptions & queryOptionFlag) == AllowedQueryOptions.None;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014640 File Offset: 0x00012840
		private T ApplySelectExpand<T>(T entity, ODataQuerySettings querySettings)
		{
			T t = default(T);
			bool flag = this.IsAvailableODataQueryOption(this.SelectExpand.RawSelect, AllowedQueryOptions.Select);
			bool flag2 = this.IsAvailableODataQueryOption(this.SelectExpand.RawExpand, AllowedQueryOptions.Expand);
			if (flag || flag2)
			{
				if ((!flag && this.SelectExpand.RawSelect != null) || (!flag2 && this.SelectExpand.RawExpand != null))
				{
					this.SelectExpand = new SelectExpandQueryOption(flag ? this.RawValues.Select : null, flag2 ? this.RawValues.Expand : null, this.SelectExpand.Context);
				}
				SelectExpandClause processedSelectExpandClause = this.SelectExpand.ProcessedSelectExpandClause;
				SelectExpandQueryOption selectExpandQueryOption = new SelectExpandQueryOption(this.SelectExpand.RawSelect, this.SelectExpand.RawExpand, this.SelectExpand.Context, processedSelectExpandClause);
				this.InternalRequest.Context.ProcessedSelectExpandClause = processedSelectExpandClause;
				this.InternalRequest.Context.QueryOptions = this;
				Type typeFromHandle = typeof(T);
				if (typeFromHandle == typeof(IQueryable))
				{
					t = (T)((object)selectExpandQueryOption.ApplyTo((IQueryable)((object)entity), querySettings));
				}
				else if (typeFromHandle == typeof(object))
				{
					t = (T)((object)selectExpandQueryOption.ApplyTo(entity, querySettings));
				}
			}
			return t;
		}

		// Token: 0x04000137 RID: 311
		private static readonly MethodInfo _limitResultsGenericMethod = typeof(ODataQueryOptions).GetMethods(BindingFlags.Static | BindingFlags.Public).Single((MethodInfo mi) => mi.Name == "LimitResults" && mi.ContainsGenericParameters && mi.GetParameters().Length == 4);

		// Token: 0x04000138 RID: 312
		private ODataQueryOptionParser _queryOptionParser;

		// Token: 0x04000139 RID: 313
		private AllowedQueryOptions _ignoreQueryOptions;

		// Token: 0x0400013A RID: 314
		private ETag _etagIfMatch;

		// Token: 0x0400013B RID: 315
		private bool _etagIfMatchChecked;

		// Token: 0x0400013C RID: 316
		private ETag _etagIfNoneMatch;

		// Token: 0x0400013D RID: 317
		private bool _etagIfNoneMatchChecked;

		// Token: 0x0400013E RID: 318
		private bool _enableNoDollarSignQueryOptions;

		// Token: 0x0400013F RID: 319
		private OrderByQueryOption _stableOrderBy;
	}
}
