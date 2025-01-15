using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3.Compiler
{
	// Token: 0x020008F9 RID: 2297
	internal sealed class ODataQueryPlan : QueryPlanBase
	{
		// Token: 0x0600419F RID: 16799 RVA: 0x000DD09C File Offset: 0x000DB29C
		public ODataQueryPlan(IExpression originalQuery, TypeValue type, IEnumerable<QueryOptionQueryToken> queryOptions, int maxUriLength)
			: base(type)
		{
			this.originalQuery = originalQuery;
			this.queryOptions.AddRange(queryOptions);
			this.maxUriLength = maxUriLength;
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x000DD0F7 File Offset: 0x000DB2F7
		public IExpression OriginalQuery
		{
			get
			{
				return this.originalQuery;
			}
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000DD0FF File Offset: 0x000DB2FF
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x000DD107 File Offset: 0x000DB307
		public QueryToken Filter { get; set; }

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000DD110 File Offset: 0x000DB310
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x000DD118 File Offset: 0x000DB318
		public bool HasValue { get; set; }

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000DD121 File Offset: 0x000DB321
		public List<OrderByQueryToken> OrderByTokens
		{
			get
			{
				return this.orderByTokens;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x000DD129 File Offset: 0x000DB329
		// (set) Token: 0x060041A7 RID: 16807 RVA: 0x000DD131 File Offset: 0x000DB331
		public SegmentQueryToken Segment { get; set; }

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x000DD13A File Offset: 0x000DB33A
		public List<QueryOptionQueryToken> QueryOptions
		{
			get
			{
				return this.queryOptions;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x000DD142 File Offset: 0x000DB342
		public List<QueryToken> ExpandTokens
		{
			get
			{
				return this.expandProperties;
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x000DD14A File Offset: 0x000DB34A
		public List<QueryToken> SelectTokens
		{
			get
			{
				return this.selectProperties;
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x000DD152 File Offset: 0x000DB352
		public bool HasEmptyResult
		{
			get
			{
				return LiteralConverter.LiteralTokenFalse == this.Filter;
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000DD164 File Offset: 0x000DB364
		public IList<Uri> CreateUris(Uri baseUri, int? skip, int? top, bool hasCount)
		{
			SegmentQueryToken segmentQueryToken = this.Segment;
			if (hasCount)
			{
				segmentQueryToken = new KeywordSegmentQueryToken(KeywordKind.Count, this.Segment);
			}
			else if (this.HasValue)
			{
				segmentQueryToken = new KeywordSegmentQueryToken(KeywordKind.Value, this.Segment);
			}
			if (this.SelectTokens.Count > 0)
			{
				HashSet<TableTypeValue> tables = new ODataQueryPlan.TableExtractionVisitor(this.OriginalQuery).Tables;
				if (tables.Count == 1)
				{
					HashSet<string> hashSet = new HashSet<string>(tables.First<TableTypeValue>().KeyColumnNames);
					hashSet.ExceptWith(from t in this.SelectTokens.OfType<PropertyAccessQueryToken>()
						where t.Parent == null
						select EdmNameEncoder.Decode(t.Name));
					foreach (string text in hashSet)
					{
						this.SelectTokens.Add(new PropertyAccessQueryToken(EdmNameEncoder.Encode(text), null));
					}
				}
			}
			QueryDescriptorQueryToken queryDescriptorQueryToken = new QueryDescriptorQueryToken(segmentQueryToken, this.Filter, this.OrderByTokens, new SelectQueryToken(this.SelectTokens), new ExpandQueryToken(this.ExpandTokens), skip, top, null, null, this.QueryOptions);
			Uri uri = MashupODataUriBuilder.CreateUri(baseUri, queryDescriptorQueryToken);
			QueryToken queryToken;
			IList<QueryToken> list;
			if (uri.AbsoluteUri.Length > this.maxUriLength && this.Filter != null && this.OrderByTokens.Count == 0 && skip == null && top == null && this.QueryOptions.Count == 0 && !hasCount && ODataQueryPlan.TryGetFilterTokens(queryDescriptorQueryToken.Filter, out queryToken, out list) && list.Count > 1)
			{
				return this.CreateUris(baseUri, queryDescriptorQueryToken, queryToken, list);
			}
			return new Uri[] { uri };
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000DD34C File Offset: 0x000DB54C
		public void AddOrUpdateQueryOption(string name, string value)
		{
			for (int i = 0; i < this.QueryOptions.Count; i++)
			{
				if (this.QueryOptions[i].Name == name)
				{
					this.QueryOptions[i] = new QueryOptionQueryToken(name, value);
					return;
				}
			}
			this.QueryOptions.Add(new QueryOptionQueryToken(name, value));
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000DD3B0 File Offset: 0x000DB5B0
		private IList<Uri> CreateUris(Uri baseUri, QueryDescriptorQueryToken queryToken, QueryToken userFilterToken, IList<QueryToken> indexFilterTokens)
		{
			List<Uri> list = new List<Uri>();
			int num;
			for (int i = 0; i < indexFilterTokens.Count; i += num)
			{
				num = 1;
				Uri uri = this.CreateUri(baseUri, queryToken, userFilterToken, indexFilterTokens, i, num);
				while (i + num + 1 < indexFilterTokens.Count)
				{
					Uri uri2 = this.CreateUri(baseUri, queryToken, userFilterToken, indexFilterTokens, i, num + 1);
					if (uri2.AbsoluteUri.Length > this.maxUriLength)
					{
						break;
					}
					uri = uri2;
					num++;
				}
				list.Add(uri);
			}
			return list;
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000DD42C File Offset: 0x000DB62C
		private Uri CreateUri(Uri baseUri, QueryDescriptorQueryToken queryToken, QueryToken userFilterToken, IList<QueryToken> indexFilterTokens, int offset, int count)
		{
			QueryToken queryToken2 = indexFilterTokens[offset];
			for (int i = 1; i < count; i++)
			{
				queryToken2 = new BinaryOperatorQueryToken(BinaryOperatorKind.Or, queryToken2, indexFilterTokens[offset + i]);
			}
			if (userFilterToken != null)
			{
				queryToken2 = new BinaryOperatorQueryToken(BinaryOperatorKind.And, userFilterToken, queryToken2);
			}
			QueryDescriptorQueryToken queryDescriptorQueryToken = new QueryDescriptorQueryToken(queryToken.Path, queryToken2, queryToken.OrderByTokens, queryToken.Select, queryToken.Expand, queryToken.Skip, queryToken.Top, queryToken.InlineCount, queryToken.Format, queryToken.QueryOptions);
			return MashupODataUriBuilder.CreateUri(baseUri, queryDescriptorQueryToken);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000DD4B4 File Offset: 0x000DB6B4
		private static bool TryGetFilterTokens(QueryToken filterToken, out QueryToken userFilterToken, out IList<QueryToken> indexFilterTokens)
		{
			HashSet<object> hashSet = new HashSet<object>();
			string text = null;
			BinaryOperatorQueryToken binaryOperatorQueryToken;
			if (ODataQueryPlan.TryGetBinaryToken(filterToken, BinaryOperatorKind.And, out binaryOperatorQueryToken))
			{
				userFilterToken = binaryOperatorQueryToken.Left;
				filterToken = binaryOperatorQueryToken.Right;
			}
			else
			{
				userFilterToken = null;
			}
			indexFilterTokens = new List<QueryToken>();
			while (filterToken != null)
			{
				QueryToken queryToken;
				if (ODataQueryPlan.TryGetBinaryToken(filterToken, BinaryOperatorKind.Or, out binaryOperatorQueryToken))
				{
					queryToken = binaryOperatorQueryToken.Right;
					filterToken = binaryOperatorQueryToken.Left;
				}
				else
				{
					queryToken = filterToken;
					filterToken = null;
				}
				if (!ODataQueryPlan.TryGetBinaryToken(queryToken, BinaryOperatorKind.Equal, out binaryOperatorQueryToken))
				{
					return false;
				}
				PropertyAccessQueryToken propertyAccessQueryToken;
				if (!ODataQueryPlan.TryGetToken<PropertyAccessQueryToken>(binaryOperatorQueryToken.Left, QueryTokenKind.PropertyAccess, out propertyAccessQueryToken))
				{
					return false;
				}
				if (propertyAccessQueryToken.Name != text)
				{
					if (text != null || propertyAccessQueryToken.Parent != null)
					{
						return false;
					}
					text = propertyAccessQueryToken.Name;
				}
				LiteralQueryToken literalQueryToken;
				if (!ODataQueryPlan.TryGetToken<LiteralQueryToken>(binaryOperatorQueryToken.Right, QueryTokenKind.Literal, out literalQueryToken))
				{
					return false;
				}
				if (hashSet.Add(literalQueryToken.Value))
				{
					indexFilterTokens.Add(queryToken);
				}
			}
			return true;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x000DD58A File Offset: 0x000DB78A
		private static bool TryGetToken<T>(QueryToken token, QueryTokenKind kind, out T value) where T : QueryToken
		{
			if (token.Kind != kind)
			{
				value = default(T);
				return false;
			}
			value = (T)((object)token);
			return true;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x000DD5AB File Offset: 0x000DB7AB
		private static bool TryGetBinaryToken(QueryToken token, BinaryOperatorKind kind, out BinaryOperatorQueryToken binaryToken)
		{
			return ODataQueryPlan.TryGetToken<BinaryOperatorQueryToken>(token, QueryTokenKind.BinaryOperator, out binaryToken) && binaryToken.OperatorKind == kind;
		}

		// Token: 0x04002262 RID: 8802
		private readonly IExpression originalQuery;

		// Token: 0x04002263 RID: 8803
		private readonly List<QueryToken> expandProperties = new List<QueryToken>();

		// Token: 0x04002264 RID: 8804
		private readonly List<OrderByQueryToken> orderByTokens = new List<OrderByQueryToken>();

		// Token: 0x04002265 RID: 8805
		private readonly List<QueryToken> selectProperties = new List<QueryToken>();

		// Token: 0x04002266 RID: 8806
		private readonly List<QueryOptionQueryToken> queryOptions = new List<QueryOptionQueryToken>();

		// Token: 0x04002267 RID: 8807
		private readonly int maxUriLength;

		// Token: 0x020008FA RID: 2298
		private class TableExtractionVisitor : AstVisitor
		{
			// Token: 0x060041B3 RID: 16819 RVA: 0x000DD5C3 File Offset: 0x000DB7C3
			public TableExtractionVisitor(IExpression query)
			{
				this.VisitExpression(query);
			}

			// Token: 0x17001504 RID: 5380
			// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000DD5DE File Offset: 0x000DB7DE
			public HashSet<TableTypeValue> Tables
			{
				get
				{
					return this.tables;
				}
			}

			// Token: 0x060041B5 RID: 16821 RVA: 0x000DD5E6 File Offset: 0x000DB7E6
			protected override IExpression VisitConstant(IConstantExpression constant)
			{
				if (constant.Value.IsTable)
				{
					this.tables.Add(constant.Value.Type.AsTableType);
				}
				return constant;
			}

			// Token: 0x0400226B RID: 8811
			private readonly HashSet<TableTypeValue> tables = new HashSet<TableTypeValue>();
		}
	}
}
