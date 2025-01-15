using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000299 RID: 665
	internal sealed class SelectPropertyVisitor : PathSegmentTokenVisitor
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x0004E891 File Offset: 0x0004CA91
		public SelectPropertyVisitor(IEdmModel model, IEdmStructuredType edmType, int maxDepth, SelectExpandClause expandClauseToDecorate, ODataUriResolver resolver)
		{
			this.model = model;
			this.edmType = edmType;
			this.maxDepth = maxDepth;
			this.expandClauseToDecorate = expandClauseToDecorate;
			this.resolver = resolver ?? ODataUriResolver.Default;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0004E8C7 File Offset: 0x0004CAC7
		public SelectExpandClause DecoratedExpandClause
		{
			get
			{
				return this.expandClauseToDecorate;
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004E8CF File Offset: 0x0004CACF
		public override void Visit(SystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<SystemToken>(tokenIn, "tokenIn");
			throw new ODataException(Strings.SelectPropertyVisitor_SystemTokenInSelect(tokenIn.Identifier));
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0004E8EC File Offset: 0x0004CAEC
		public override void Visit(NonSystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<NonSystemToken>(tokenIn, "tokenIn");
			SelectItem selectItem;
			if (tokenIn.NextToken == null && SelectPathSegmentTokenBinder.TryBindAsWildcard(tokenIn, this.model, out selectItem))
			{
				this.expandClauseToDecorate.AddToSelectedItems(selectItem);
				return;
			}
			this.ProcessTokenAsPath(tokenIn);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0004E95C File Offset: 0x0004CB5C
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "It makes sense to keep all of this logic in one place")]
		private void ProcessTokenAsPath(NonSystemToken tokenIn)
		{
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			IEdmStructuredType edmStructuredType = this.edmType;
			if (tokenIn.IsNamespaceOrContainerQualified())
			{
				PathSegmentToken pathSegmentToken;
				list.AddRange(SelectExpandPathBinder.FollowTypeSegments(tokenIn, this.model, this.maxDepth, this.resolver, ref edmStructuredType, out pathSegmentToken));
				tokenIn = pathSegmentToken as NonSystemToken;
				if (tokenIn == null)
				{
					throw new ODataException(Strings.SelectPropertyVisitor_SystemTokenInSelect(pathSegmentToken.Identifier));
				}
			}
			ODataPathSegment odataPathSegment = SelectPathSegmentTokenBinder.ConvertNonTypeTokenToSegment(tokenIn, this.model, edmStructuredType, this.resolver);
			if (odataPathSegment != null)
			{
				list.Add(odataPathSegment);
				bool flag = false;
				for (;;)
				{
					edmStructuredType = odataPathSegment.EdmType as IEdmStructuredType;
					IEdmCollectionType edmCollectionType = odataPathSegment.EdmType as IEdmCollectionType;
					if ((edmStructuredType == null || edmStructuredType.TypeKind != EdmTypeKind.Complex) && (edmCollectionType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Complex))
					{
						break;
					}
					NonSystemToken nonSystemToken = tokenIn.NextToken as NonSystemToken;
					if (nonSystemToken == null)
					{
						break;
					}
					odataPathSegment = null;
					if (edmStructuredType == null)
					{
						edmStructuredType = edmCollectionType.ElementType.Definition as IEdmStructuredType;
						flag = true;
					}
					else if (!flag)
					{
						odataPathSegment = SelectPathSegmentTokenBinder.ConvertNonTypeTokenToSegment(nonSystemToken, this.model, edmStructuredType, this.resolver);
					}
					if (odataPathSegment == null)
					{
						IEdmStructuredType edmStructuredType2 = UriEdmHelpers.FindTypeFromModel(this.model, nonSystemToken.Identifier, this.resolver) as IEdmStructuredType;
						if (edmStructuredType2.IsOrInheritsFrom(edmStructuredType))
						{
							odataPathSegment = new TypeSegment(edmStructuredType2, null);
						}
					}
					if (odataPathSegment == null)
					{
						break;
					}
					tokenIn = nonSystemToken;
					list.Add(odataPathSegment);
				}
			}
			ODataSelectPath selectedPath = new ODataSelectPath(list);
			PathSelectItem pathSelectItem = new PathSelectItem(selectedPath);
			if (tokenIn.NextToken != null)
			{
				throw new ODataException(Strings.SelectBinder_MultiLevelPathInSelect);
			}
			NavigationPropertySegment navigationPropertySegment = pathSelectItem.SelectedPath.LastSegment as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				if (Enumerable.Any<SelectItem>(this.expandClauseToDecorate.SelectedItems, (SelectItem x) => x is PathSelectItem && ((PathSelectItem)x).SelectedPath.Equals(selectedPath)))
				{
					return;
				}
			}
			this.expandClauseToDecorate.AddToSelectedItems(pathSelectItem);
		}

		// Token: 0x04000A00 RID: 2560
		private readonly IEdmModel model;

		// Token: 0x04000A01 RID: 2561
		private readonly int maxDepth;

		// Token: 0x04000A02 RID: 2562
		private readonly SelectExpandClause expandClauseToDecorate;

		// Token: 0x04000A03 RID: 2563
		private readonly IEdmStructuredType edmType;

		// Token: 0x04000A04 RID: 2564
		private readonly ODataUriResolver resolver;
	}
}
