using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200004D RID: 77
	public sealed class QueryDescriptorQueryToken : QueryToken
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000A93C File Offset: 0x00008B3C
		public QueryDescriptorQueryToken(QueryToken path, QueryToken filter, IEnumerable<OrderByQueryToken> orderByTokens, SelectQueryToken select, ExpandQueryToken expand, int? skip, int? top, InlineCountKind? inlineCount, string format, IEnumerable<QueryOptionQueryToken> queryOptions)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(path, "path");
			this.path = path;
			this.filter = filter;
			this.orderByTokens = new ReadOnlyEnumerable<OrderByQueryToken>(orderByTokens ?? ((IEnumerable<OrderByQueryToken>)new OrderByQueryToken[0]));
			this.select = select;
			this.expand = expand;
			this.skip = skip;
			this.top = top;
			this.inlineCount = inlineCount;
			this.format = format;
			this.queryOptions = new ReadOnlyEnumerable<QueryOptionQueryToken>(queryOptions ?? ((IEnumerable<QueryOptionQueryToken>)new QueryOptionQueryToken[0]));
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A9CF File Offset: 0x00008BCF
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.QueryDescriptor;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		public QueryToken Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A9DA File Offset: 0x00008BDA
		public QueryToken Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A9E2 File Offset: 0x00008BE2
		public IEnumerable<OrderByQueryToken> OrderByTokens
		{
			get
			{
				return this.orderByTokens;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A9EA File Offset: 0x00008BEA
		public SelectQueryToken Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000A9F2 File Offset: 0x00008BF2
		public ExpandQueryToken Expand
		{
			get
			{
				return this.expand;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A9FA File Offset: 0x00008BFA
		public int? Skip
		{
			get
			{
				return this.skip;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000AA02 File Offset: 0x00008C02
		public int? Top
		{
			get
			{
				return this.top;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000AA0A File Offset: 0x00008C0A
		public string Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000AA12 File Offset: 0x00008C12
		public InlineCountKind? InlineCount
		{
			get
			{
				return this.inlineCount;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000AA1A File Offset: 0x00008C1A
		public IEnumerable<QueryOptionQueryToken> QueryOptions
		{
			get
			{
				return this.queryOptions;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000AA22 File Offset: 0x00008C22
		public static QueryDescriptorQueryToken ParseUri(Uri queryUri, Uri serviceBaseUri)
		{
			return QueryDescriptorQueryToken.ParseUri(queryUri, serviceBaseUri, 800);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AA30 File Offset: 0x00008C30
		public static QueryDescriptorQueryToken ParseUri(Uri queryUri, Uri serviceBaseUri, int maxDepth)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(queryUri, "queryUri");
			if (!queryUri.IsAbsoluteUri)
			{
				throw new ArgumentException(Strings.QueryDescriptorQueryToken_UriMustBeAbsolute(queryUri), "queryUri");
			}
			ExceptionUtils.CheckArgumentNotNull<Uri>(serviceBaseUri, "serviceBaseUri");
			if (!serviceBaseUri.IsAbsoluteUri)
			{
				throw new ArgumentException(Strings.QueryDescriptorQueryToken_UriMustBeAbsolute(serviceBaseUri), "serviceBaseUri");
			}
			if (maxDepth <= 0)
			{
				throw new ArgumentException(Strings.QueryDescriptorQueryToken_MaxDepthInvalid, "maxDepth");
			}
			UriQueryPathParser uriQueryPathParser = new UriQueryPathParser(maxDepth);
			QueryToken queryToken = uriQueryPathParser.ParseUri(queryUri, serviceBaseUri);
			List<QueryOptionQueryToken> list = UriUtils.ParseQueryOptions(queryUri);
			QueryToken queryToken2 = null;
			string queryOptionValueAndRemove = list.GetQueryOptionValueAndRemove("$filter");
			if (queryOptionValueAndRemove != null)
			{
				UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(maxDepth);
				queryToken2 = uriQueryExpressionParser.ParseFilter(queryOptionValueAndRemove);
			}
			IEnumerable<OrderByQueryToken> enumerable = null;
			string queryOptionValueAndRemove2 = list.GetQueryOptionValueAndRemove("$orderby");
			if (queryOptionValueAndRemove2 != null)
			{
				UriQueryExpressionParser uriQueryExpressionParser2 = new UriQueryExpressionParser(maxDepth);
				enumerable = uriQueryExpressionParser2.ParseOrderBy(queryOptionValueAndRemove2);
			}
			SelectQueryToken selectQueryToken = null;
			string queryOptionValueAndRemove3 = list.GetQueryOptionValueAndRemove("$select");
			if (queryOptionValueAndRemove3 != null)
			{
				UriQueryExpressionParser uriQueryExpressionParser3 = new UriQueryExpressionParser(maxDepth);
				selectQueryToken = uriQueryExpressionParser3.ParseSelect(queryOptionValueAndRemove3);
			}
			ExpandQueryToken expandQueryToken = null;
			string queryOptionValueAndRemove4 = list.GetQueryOptionValueAndRemove("$expand");
			if (queryOptionValueAndRemove4 != null)
			{
				UriQueryExpressionParser uriQueryExpressionParser4 = new UriQueryExpressionParser(maxDepth);
				expandQueryToken = uriQueryExpressionParser4.ParseExpand(queryOptionValueAndRemove4);
			}
			int? num = default(int?);
			string queryOptionValueAndRemove5 = list.GetQueryOptionValueAndRemove("$skip");
			if (queryOptionValueAndRemove5 != null)
			{
				int num2;
				if (!UriPrimitiveTypeParser.TryUriStringToNonNegativeInteger(queryOptionValueAndRemove5, out num2))
				{
					throw new ODataException(Strings.QueryDescriptorQueryToken_InvalidSkipQueryOptionValue(queryOptionValueAndRemove5));
				}
				num = new int?(num2);
			}
			int? num3 = default(int?);
			string queryOptionValueAndRemove6 = list.GetQueryOptionValueAndRemove("$top");
			if (queryOptionValueAndRemove6 != null)
			{
				int num4;
				if (!UriPrimitiveTypeParser.TryUriStringToNonNegativeInteger(queryOptionValueAndRemove6, out num4))
				{
					throw new ODataException(Strings.QueryDescriptorQueryToken_InvalidTopQueryOptionValue(queryOptionValueAndRemove6));
				}
				num3 = new int?(num4);
			}
			string queryOptionValueAndRemove7 = list.GetQueryOptionValueAndRemove("$inlinecount");
			InlineCountKind? inlineCountKind = QueryTokenUtils.ParseInlineCountKind(queryOptionValueAndRemove7);
			string queryOptionValueAndRemove8 = list.GetQueryOptionValueAndRemove("$format");
			return new QueryDescriptorQueryToken(queryToken, queryToken2, enumerable, selectQueryToken, expandQueryToken, num, num3, inlineCountKind, queryOptionValueAndRemove8, (list.Count == 0) ? null : new ReadOnlyCollection<QueryOptionQueryToken>(list));
		}

		// Token: 0x040001C8 RID: 456
		private const int DefaultMaxDepth = 800;

		// Token: 0x040001C9 RID: 457
		private readonly QueryToken path;

		// Token: 0x040001CA RID: 458
		private readonly QueryToken filter;

		// Token: 0x040001CB RID: 459
		private readonly IEnumerable<OrderByQueryToken> orderByTokens;

		// Token: 0x040001CC RID: 460
		private readonly SelectQueryToken select;

		// Token: 0x040001CD RID: 461
		private readonly ExpandQueryToken expand;

		// Token: 0x040001CE RID: 462
		private readonly int? skip;

		// Token: 0x040001CF RID: 463
		private readonly int? top;

		// Token: 0x040001D0 RID: 464
		private readonly string format;

		// Token: 0x040001D1 RID: 465
		private readonly InlineCountKind? inlineCount;

		// Token: 0x040001D2 RID: 466
		private readonly IEnumerable<QueryOptionQueryToken> queryOptions;
	}
}
