using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200004E RID: 78
	public sealed class ODataUriParser
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00007D44 File Offset: 0x00005F44
		public ODataUriParser(IEdmModel model, Uri serviceRoot)
		{
			this.configuration = new ODataUriParserConfiguration(model, serviceRoot);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007D59 File Offset: 0x00005F59
		public ODataUriParserSettings Settings
		{
			get
			{
				return this.configuration.Settings;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007D66 File Offset: 0x00005F66
		public IEdmModel Model
		{
			get
			{
				return this.configuration.Model;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00007D73 File Offset: 0x00005F73
		public Uri ServiceRoot
		{
			get
			{
				return this.configuration.ServiceRoot;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007D80 File Offset: 0x00005F80
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00007D8D File Offset: 0x00005F8D
		public ODataUrlConventions UrlConventions
		{
			get
			{
				return this.configuration.UrlConventions;
			}
			set
			{
				this.configuration.UrlConventions = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00007D9B File Offset: 0x00005F9B
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public Func<string, BatchReferenceSegment> BatchReferenceCallback
		{
			get
			{
				return this.configuration.BatchReferenceCallback;
			}
			set
			{
				this.configuration.BatchReferenceCallback = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00007DB6 File Offset: 0x00005FB6
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00007DC3 File Offset: 0x00005FC3
		public Func<string, string> FunctionParameterAliasCallback
		{
			get
			{
				return this.configuration.FunctionParameterAliasCallback;
			}
			set
			{
				this.configuration.FunctionParameterAliasCallback = value;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public static FilterClause ParseFilter(string filter, IEdmModel model, IEdmType elementType)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseFilter(filter, elementType, null);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public static FilterClause ParseFilter(string filter, IEdmModel model, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseFilter(filter, elementType, entitySet);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007E14 File Offset: 0x00006014
		public static OrderByClause ParseOrderBy(string orderBy, IEdmModel model, IEdmType elementType)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseOrderBy(orderBy, elementType, null);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007E34 File Offset: 0x00006034
		public static OrderByClause ParseOrderBy(string orderBy, IEdmModel model, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ODataUriParser odataUriParser = new ODataUriParser(model, null);
			return odataUriParser.ParseOrderBy(orderBy, elementType, entitySet);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007E52 File Offset: 0x00006052
		public FilterClause ParseFilter(string filter, IEdmType elementType, IEdmEntitySet entitySet)
		{
			return this.ParseFilterImplementation(filter, elementType, entitySet);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007E5D File Offset: 0x0000605D
		public OrderByClause ParseOrderBy(string orderBy, IEdmType elementType, IEdmEntitySet entitySet)
		{
			return this.ParseOrderByImplementation(orderBy, elementType, entitySet);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007E68 File Offset: 0x00006068
		public ODataPath ParsePath(Uri pathUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(pathUri, "pathUri");
			if (this.configuration.ServiceRoot == null)
			{
				throw new ODataException(Strings.UriParser_NeedServiceRootForThisOverload);
			}
			if (!pathUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.UriParser_UriMustBeAbsolute(pathUri));
			}
			UriPathParser uriPathParser = new UriPathParser(this.Settings.PathLimit);
			ICollection<string> collection = uriPathParser.ParsePathIntoSegments(pathUri, this.configuration.ServiceRoot);
			return ODataPathFactory.BindPath(collection, this.configuration);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007EE2 File Offset: 0x000060E2
		public SelectExpandClause ParseSelectAndExpand(string select, string expand, IEdmEntityType elementType, IEdmEntitySet entitySet)
		{
			return this.ParseSelectAndExpandImplementation(select, expand, elementType, entitySet);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007EEF File Offset: 0x000060EF
		internal ODataUri ParseUri(Uri fullUri)
		{
			return this.ParseUriImplementation(fullUri);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007EF8 File Offset: 0x000060F8
		internal InlineCountKind ParseInlineCount(string inlineCount)
		{
			return this.ParseInlineCountImplementation(inlineCount);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007F04 File Offset: 0x00006104
		private ODataUri ParseUriImplementation(Uri fullUri)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<Uri>(this.configuration.ServiceRoot, "serviceRoot");
			ExceptionUtils.CheckArgumentNotNull<Uri>(fullUri, "fullUri");
			SyntacticTree syntacticTree = SyntacticTree.ParseUri(fullUri, this.configuration.ServiceRoot, this.Settings.FilterLimit);
			ExceptionUtils.CheckArgumentNotNull<SyntacticTree>(syntacticTree, "syntax");
			BindingState bindingState = new BindingState(this.configuration);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			ODataUriSemanticBinder odataUriSemanticBinder = new ODataUriSemanticBinder(bindingState, new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return odataUriSemanticBinder.BindTree(syntacticTree);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007F9C File Offset: 0x0000619C
		private FilterClause ParseFilterImplementation(string filter, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriParserConfiguration>(this.configuration, "this.configuration");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(filter, "filter");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.Settings.FilterLimit);
			QueryToken queryToken = uriQueryExpressionParser.ParseFilter(filter);
			BindingState bindingState = new BindingState(this.configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), entitySet);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			FilterBinder filterBinder = new FilterBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind), bindingState);
			return filterBinder.BindFilter(queryToken);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008040 File Offset: 0x00006240
		private OrderByClause ParseOrderByImplementation(string orderBy, IEdmType elementType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(elementType, "elementType");
			ExceptionUtils.CheckArgumentNotNull<string>(orderBy, "orderBy");
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(this.Settings.OrderByLimit);
			IEnumerable<OrderByToken> enumerable = uriQueryExpressionParser.ParseOrderBy(orderBy);
			BindingState bindingState = new BindingState(this.configuration);
			bindingState.ImplicitRangeVariable = NodeFactory.CreateImplicitRangeVariable(elementType.ToTypeReference(), entitySet);
			bindingState.RangeVariables.Push(bindingState.ImplicitRangeVariable);
			MetadataBinder metadataBinder = new MetadataBinder(bindingState);
			OrderByBinder orderByBinder = new OrderByBinder(new MetadataBinder.QueryTokenVisitor(metadataBinder.Bind));
			return orderByBinder.BindOrderBy(bindingState, enumerable);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000080E8 File Offset: 0x000062E8
		private SelectExpandClause ParseSelectAndExpandImplementation(string select, string expand, IEdmEntityType elementType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(this.configuration.Model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(elementType, "elementType");
			ISelectExpandTermParser selectExpandTermParser = SelectExpandTermParserFactory.Create(select, this.Settings);
			SelectToken selectToken = selectExpandTermParser.ParseSelect();
			ISelectExpandTermParser selectExpandTermParser2 = SelectExpandTermParserFactory.Create(expand, this.Settings);
			ExpandToken expandToken = selectExpandTermParser2.ParseExpand();
			return SelectExpandSemanticBinder.Parse(elementType, entitySet, expandToken, selectToken, this.configuration);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008150 File Offset: 0x00006350
		private InlineCountKind ParseInlineCountImplementation(string inlineCount)
		{
			inlineCount = inlineCount.Trim();
			string text;
			if ((text = inlineCount) != null)
			{
				if (text == "allpages")
				{
					return InlineCountKind.AllPages;
				}
				if (text == "none")
				{
					return InlineCountKind.None;
				}
			}
			throw new ODataException(Strings.ODataUriParser_InvalidInlineCount(inlineCount));
		}

		// Token: 0x04000083 RID: 131
		private readonly ODataUriParserConfiguration configuration;
	}
}
