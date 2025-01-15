using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Spatial;
using System.Text;
using System.Xml;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000029 RID: 41
	public class ODataUriBuilder
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000049D6 File Offset: 0x00002BD6
		protected ODataUriBuilder(QueryToken queryToken)
		{
			this.queryToken = queryToken;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000049F0 File Offset: 0x00002BF0
		protected StringBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000049F8 File Offset: 0x00002BF8
		protected QueryToken QueryToken
		{
			get
			{
				return this.queryToken;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004A00 File Offset: 0x00002C00
		public static Uri CreateUri(Uri baseUri, QueryDescriptorQueryToken queryDescriptor)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(baseUri, "baseUri");
			ExceptionUtils.CheckArgumentNotNull<QueryDescriptorQueryToken>(queryDescriptor, "queryDescriptor");
			ODataUriBuilder odataUriBuilder = new ODataUriBuilder(queryDescriptor);
			string text = odataUriBuilder.Build();
			if (text.StartsWith("?", 4))
			{
				return new UriBuilder(baseUri)
				{
					Query = text
				}.Uri;
			}
			return new Uri(baseUri, new Uri(text, 0));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004A64 File Offset: 0x00002C64
		public static string GetUriRepresentation(object clrLiteral)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ODataUriBuilder.WriteClrLiteral(stringBuilder, clrLiteral);
			return stringBuilder.ToString();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A84 File Offset: 0x00002C84
		public virtual void WriteQueryDescriptor(QueryDescriptorQueryToken queryDescriptor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryDescriptorQueryToken>(queryDescriptor, "queryDescriptor");
			this.WriteQuery(queryDescriptor.Path);
			bool flag = true;
			if (queryDescriptor.Filter != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$filter");
				this.builder.Append("=");
				this.WriteQuery(queryDescriptor.Filter);
			}
			if (queryDescriptor.Select != null && Enumerable.Count<QueryToken>(queryDescriptor.Select.Properties) > 0)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.WriteSelect(queryDescriptor.Select);
			}
			if (queryDescriptor.Expand != null && Enumerable.Count<QueryToken>(queryDescriptor.Expand.Properties) > 0)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.WriteExpand(queryDescriptor.Expand);
			}
			if (Enumerable.Count<OrderByQueryToken>(queryDescriptor.OrderByTokens) > 0)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$orderby");
				this.builder.Append("=");
				this.WriteOrderBys(queryDescriptor.OrderByTokens);
			}
			foreach (QueryOptionQueryToken queryOptionQueryToken in queryDescriptor.QueryOptions)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.WriteQueryOption(queryOptionQueryToken);
			}
			if (queryDescriptor.Top != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$top");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.Top);
			}
			if (queryDescriptor.Skip != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$skip");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.Skip);
			}
			if (queryDescriptor.Format != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$format");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.Format);
			}
			if (queryDescriptor.InlineCount != null)
			{
				this.WriteQueryPrefixOrSeparator(flag);
				flag = false;
				this.builder.Append("$inlinecount");
				this.builder.Append("=");
				this.builder.Append(queryDescriptor.InlineCount.Value.ToText());
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004D20 File Offset: 0x00002F20
		internal void Append(string text)
		{
			this.builder.Append(text);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004D30 File Offset: 0x00002F30
		protected internal virtual void WriteQuery(QueryToken query)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(query, "query");
			switch (query.Kind)
			{
			case QueryTokenKind.QueryDescriptor:
				this.WriteQueryDescriptor((QueryDescriptorQueryToken)query);
				return;
			case QueryTokenKind.Segment:
			case QueryTokenKind.KeywordSegment:
				this.WriteSegment((SegmentQueryToken)query);
				return;
			case QueryTokenKind.BinaryOperator:
				this.WriteBinary((BinaryOperatorQueryToken)query);
				return;
			case QueryTokenKind.UnaryOperator:
				this.WriteUnary((UnaryOperatorQueryToken)query);
				return;
			case QueryTokenKind.Literal:
				this.WriteLiteral((LiteralQueryToken)query);
				return;
			case QueryTokenKind.FunctionCall:
				this.WriteFunctionCall((FunctionCallQueryToken)query);
				return;
			case QueryTokenKind.PropertyAccess:
				this.WritePropertyAccess((PropertyAccessQueryToken)query);
				return;
			case QueryTokenKind.OrderBy:
				this.WriteOrderBy((OrderByQueryToken)query);
				return;
			case QueryTokenKind.QueryOption:
				this.WriteQueryOption((QueryOptionQueryToken)query);
				return;
			case QueryTokenKind.Select:
				this.WriteSelect((SelectQueryToken)query);
				return;
			case QueryTokenKind.Star:
				this.WriteStar((StarQueryToken)query);
				return;
			}
			ODataUriBuilderUtils.NotSupported(query.Kind);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004E28 File Offset: 0x00003028
		protected virtual string Build()
		{
			this.WriteQuery(this.queryToken);
			return this.builder.ToString();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004E44 File Offset: 0x00003044
		protected virtual void WriteBinary(BinaryOperatorQueryToken binary)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorQueryToken>(binary, "binary");
			BinaryOperatorUriBuilder binaryOperatorUriBuilder = new BinaryOperatorUriBuilder(this);
			binaryOperatorUriBuilder.Write(binary);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004E6C File Offset: 0x0000306C
		protected virtual void WriteFunctionCall(FunctionCallQueryToken functionQueryToken)
		{
			ExceptionUtils.CheckArgumentNotNull<FunctionCallQueryToken>(functionQueryToken, "functionQueryToken");
			this.builder.Append(functionQueryToken.Name);
			this.builder.Append("(");
			bool flag = false;
			foreach (QueryToken queryToken in functionQueryToken.Arguments)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				else
				{
					flag = true;
				}
				this.WriteQuery(queryToken);
			}
			this.builder.Append(")");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004F14 File Offset: 0x00003114
		protected virtual void WriteLiteral(LiteralQueryToken literal)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralQueryToken>(literal, "literal");
			ODataUriBuilder.WriteClrLiteral(this.builder, literal.Value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004F34 File Offset: 0x00003134
		protected virtual void WriteOrderBys(IEnumerable<OrderByQueryToken> orderBys)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<OrderByQueryToken>>(orderBys, "orderBys");
			bool flag = false;
			foreach (OrderByQueryToken orderByQueryToken in orderBys)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				this.WriteOrderBy(orderByQueryToken);
				flag = true;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004FA0 File Offset: 0x000031A0
		protected virtual void WriteOrderBy(OrderByQueryToken orderBy)
		{
			ExceptionUtils.CheckArgumentNotNull<OrderByQueryToken>(orderBy, "orderBy");
			this.WriteQuery(orderBy.Expression);
			if (orderBy.Direction == OrderByDirection.Descending)
			{
				this.builder.Append("%20");
				this.builder.Append("desc");
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004FF0 File Offset: 0x000031F0
		protected virtual void WriteSegment(SegmentQueryToken segment)
		{
			ExceptionUtils.CheckArgumentNotNull<SegmentQueryToken>(segment, "segment");
			if (string.IsNullOrEmpty(segment.Name))
			{
				return;
			}
			if (segment.Parent != null)
			{
				this.WriteSegment(segment.Parent);
				this.builder.Append("/");
			}
			this.builder.Append(segment.Name);
			if (segment.NamedValues != null)
			{
				this.builder.Append("(");
				bool flag = false;
				foreach (NamedValue namedValue in segment.NamedValues)
				{
					if (flag)
					{
						this.builder.Append(",");
					}
					this.builder.Append(namedValue.Name);
					this.builder.Append("=");
					this.WriteLiteral(namedValue.Value);
					flag = true;
				}
				this.builder.Append(")");
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000050FC File Offset: 0x000032FC
		protected virtual void WritePropertyAccess(PropertyAccessQueryToken propertyAccess)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertyAccessQueryToken>(propertyAccess, "propertyAccess");
			if (propertyAccess.Parent != null)
			{
				this.WriteQuery(propertyAccess.Parent);
				this.builder.Append("/");
			}
			this.builder.Append(propertyAccess.Name);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000514C File Offset: 0x0000334C
		protected virtual void WriteQueryOption(QueryOptionQueryToken queryOption)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryOptionQueryToken>(queryOption, "queryOption");
			this.builder.Append(queryOption.Name);
			this.builder.Append("=");
			this.builder.Append(queryOption.Value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005199 File Offset: 0x00003399
		protected virtual void WriteQueryPrefixOrSeparator(bool writeQueryPrefix)
		{
			if (writeQueryPrefix)
			{
				this.builder.Append("?");
				return;
			}
			this.builder.Append("&");
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000051C4 File Offset: 0x000033C4
		protected virtual void WriteSelect(SelectQueryToken selectQueryToken)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectQueryToken>(selectQueryToken, "selectQueryToken");
			this.builder.Append("$select");
			this.builder.Append("=");
			bool flag = false;
			foreach (QueryToken queryToken in selectQueryToken.Properties)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				this.WriteQuery(queryToken);
				flag = true;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005258 File Offset: 0x00003458
		protected virtual void WriteExpand(ExpandQueryToken expand)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpandQueryToken>(expand, "expandQueryToken");
			this.builder.Append("$expand");
			this.builder.Append("=");
			bool flag = false;
			foreach (QueryToken queryToken in expand.Properties)
			{
				if (flag)
				{
					this.builder.Append(",");
				}
				this.WriteQuery(queryToken);
				flag = true;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000052EC File Offset: 0x000034EC
		protected virtual void WriteStar(StarQueryToken star)
		{
			ExceptionUtils.CheckArgumentNotNull<StarQueryToken>(star, "star");
			if (star.Parent != null)
			{
				this.WriteQuery(star.Parent);
				this.builder.Append("/");
			}
			this.builder.Append("*");
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000533C File Offset: 0x0000353C
		protected virtual void WriteUnary(UnaryOperatorQueryToken unary)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorQueryToken>(unary, "unary");
			switch (unary.OperatorKind)
			{
			case UnaryOperatorKind.Negate:
				this.builder.Append("-");
				break;
			case UnaryOperatorKind.Not:
				this.builder.Append("not");
				this.builder.Append("%20");
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUriBuilder_WriteUnary_UnreachableCodePath));
			}
			this.WriteQuery(unary.Operand);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000053C0 File Offset: 0x000035C0
		private static void WriteClrLiteral(StringBuilder builder, object clrLiteral)
		{
			if (clrLiteral == null)
			{
				builder.Append("null");
				return;
			}
			switch (Type.GetTypeCode(clrLiteral.GetType()))
			{
			case 3:
				builder.Append(((bool)clrLiteral) ? "true" : "false");
				return;
			case 5:
				builder.Append(((sbyte)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case 6:
				builder.Append(((byte)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case 7:
				builder.Append(((short)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case 9:
				builder.Append(((int)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				return;
			case 11:
				builder.Append(((long)clrLiteral).ToString("D", CultureInfo.InvariantCulture));
				builder.Append("L");
				return;
			case 13:
				builder.Append(((float)clrLiteral).ToString("F", CultureInfo.InvariantCulture));
				builder.Append("f");
				return;
			case 14:
				builder.Append(((double)clrLiteral).ToString("R", ODataUriBuilderUtils.DoubleFormatInfo));
				return;
			case 15:
				builder.Append(((decimal)clrLiteral).ToString(ODataUriBuilderUtils.DecimalFormatInfo));
				builder.Append("M");
				return;
			case 16:
				builder.Append("datetime");
				builder.Append("'");
				builder.Append(((DateTime)clrLiteral).ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture));
				builder.Append("'");
				return;
			case 18:
				builder.Append("'");
				builder.Append(Uri.EscapeDataString(ODataUriBuilderUtils.Escape(clrLiteral.ToString())));
				builder.Append("'");
				return;
			}
			if (clrLiteral is DateTimeOffset)
			{
				builder.Append("datetimeoffset");
				builder.Append("'");
				builder.Append(Uri.EscapeDataString(((DateTimeOffset)clrLiteral).ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzzz", CultureInfo.InvariantCulture)));
				builder.Append("'");
				return;
			}
			if (clrLiteral is TimeSpan)
			{
				builder.Append("time");
				builder.Append("'");
				builder.Append(XmlConvert.ToString((TimeSpan)clrLiteral));
				builder.Append("'");
				return;
			}
			if (clrLiteral is Guid)
			{
				builder.Append("guid");
				builder.Append("'");
				builder.Append(((Guid)clrLiteral).ToString("D"));
				builder.Append("'");
				return;
			}
			byte[] array = clrLiteral as byte[];
			if (array != null)
			{
				builder.Append("binary");
				builder.Append("'");
				foreach (byte b in array)
				{
					builder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
				builder.Append("'");
				return;
			}
			Geography geography = clrLiteral as Geography;
			if (geography != null)
			{
				builder.Append("geography");
				builder.Append("'");
				builder.Append(LiteralUtils.ToWellKnownText(geography));
				builder.Append("'");
				return;
			}
			Geometry geometry = clrLiteral as Geometry;
			if (geometry != null)
			{
				builder.Append("geometry");
				builder.Append("'");
				builder.Append(LiteralUtils.ToWellKnownText(geometry));
				builder.Append("'");
				return;
			}
			ODataUriBuilderUtils.NotSupported(clrLiteral.GetType());
		}

		// Token: 0x04000138 RID: 312
		private readonly QueryToken queryToken;

		// Token: 0x04000139 RID: 313
		private readonly StringBuilder builder = new StringBuilder();
	}
}
