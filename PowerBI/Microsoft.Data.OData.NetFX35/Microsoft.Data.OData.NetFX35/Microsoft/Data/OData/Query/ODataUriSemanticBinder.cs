using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200001A RID: 26
	internal sealed class ODataUriSemanticBinder
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003884 File Offset: 0x00001A84
		public ODataUriSemanticBinder(BindingState bindingState, MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(bindingState, "bindingState");
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindingState = bindingState;
			this.bindMethod = bindMethod;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000038B0 File Offset: 0x00001AB0
		public ODataUri BindTree(SyntacticTree syntax)
		{
			ExceptionUtils.CheckArgumentNotNull<SyntacticTree>(syntax, "syntax");
			ExceptionUtils.CheckArgumentNotNull<ICollection<string>>(syntax.Path, "syntax.Path");
			this.bindingState.QueryOptions = new List<CustomQueryOptionToken>(syntax.QueryOptions);
			long? num = default(long?);
			long? num2 = default(long?);
			InlineCountKind? inlineCountKind = default(InlineCountKind?);
			ODataPath odataPath = ODataPathFactory.BindPath(syntax.Path, this.bindingState.Configuration);
			RangeVariable rangeVariable = NodeFactory.CreateImplicitRangeVariable(odataPath);
			if (rangeVariable != null)
			{
				this.bindingState.RangeVariables.Push(rangeVariable);
			}
			if (syntax.Filter != null || Enumerable.Any<OrderByToken>(syntax.OrderByTokens))
			{
				this.bindingState.ImplicitRangeVariable = this.bindingState.RangeVariables.Peek();
			}
			FilterClause filterClause = this.BindFilter(syntax, rangeVariable);
			OrderByClause orderByClause = this.BindOrderBy(syntax, rangeVariable, odataPath);
			num = ODataUriSemanticBinder.BindSkip(syntax, rangeVariable, odataPath);
			num2 = ODataUriSemanticBinder.BindTop(syntax, rangeVariable, odataPath);
			SelectExpandClause selectExpandClause = ODataUriSemanticBinder.BindSelectExpand(syntax, odataPath, this.bindingState.Configuration);
			inlineCountKind = ODataUriSemanticBinder.BindInlineCount(syntax, odataPath);
			List<QueryNode> list = MetadataBinder.ProcessQueryOptions(this.bindingState, this.bindMethod);
			this.bindingState.RangeVariables.Pop();
			this.bindingState.ImplicitRangeVariable = null;
			return new ODataUri(odataPath, list, selectExpandClause, filterClause, orderByClause, num, num2, inlineCountKind);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000039FC File Offset: 0x00001BFC
		public static InlineCountKind? BindInlineCount(SyntacticTree syntax, ODataPath path)
		{
			if (syntax.InlineCount == null)
			{
				return default(InlineCountKind?);
			}
			if (!path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$inlinecount"));
			}
			return new InlineCountKind?(syntax.InlineCount.Value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A54 File Offset: 0x00001C54
		public static SelectExpandClause BindSelectExpand(SyntacticTree syntax, ODataPath path, ODataUriParserConfiguration configuration)
		{
			if (syntax.Select == null && syntax.Expand == null)
			{
				return null;
			}
			if (!path.EdmType().IsEntityCollection() && !path.EdmType().IsEntity())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$select or $expand"));
			}
			return SelectExpandSemanticBinder.Parse((IEdmEntityType)((IEdmCollectionTypeReference)path.EdmType()).ElementType().Definition, path.EntitySet(), syntax.Expand, syntax.Select, configuration);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public static long? BindTop(SyntacticTree syntax, RangeVariable rangeVariable, ODataPath path)
		{
			if (syntax.Top == null)
			{
				return default(long?);
			}
			if (rangeVariable == null || !path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$top"));
			}
			int? top = syntax.Top;
			return MetadataBinder.ProcessTop((top != null) ? new long?((long)top.GetValueOrDefault()) : default(long?));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B44 File Offset: 0x00001D44
		public static long? BindSkip(SyntacticTree syntax, RangeVariable rangeVariable, ODataPath path)
		{
			if (syntax.Skip == null)
			{
				return default(long?);
			}
			if (rangeVariable == null || !path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$skip"));
			}
			int? skip = syntax.Skip;
			return MetadataBinder.ProcessSkip((skip != null) ? new long?((long)skip.GetValueOrDefault()) : default(long?));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public OrderByClause BindOrderBy(SyntacticTree syntax, RangeVariable rangeVariable, ODataPath path)
		{
			if (syntax.OrderByTokens == null || !Enumerable.Any<OrderByToken>(syntax.OrderByTokens))
			{
				return null;
			}
			if (rangeVariable == null || !path.EdmType().IsEntityCollection())
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$orderby"));
			}
			OrderByBinder orderByBinder = new OrderByBinder(this.bindMethod);
			return orderByBinder.BindOrderBy(this.bindingState, syntax.OrderByTokens);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C1C File Offset: 0x00001E1C
		public FilterClause BindFilter(SyntacticTree syntax, RangeVariable rangeVariable)
		{
			if (syntax.Filter == null)
			{
				return null;
			}
			if (rangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_QueryOptionNotApplicable("$filter"));
			}
			FilterBinder filterBinder = new FilterBinder(this.bindMethod, this.bindingState);
			return filterBinder.BindFilter(syntax.Filter);
		}

		// Token: 0x04000044 RID: 68
		private readonly BindingState bindingState;

		// Token: 0x04000045 RID: 69
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
