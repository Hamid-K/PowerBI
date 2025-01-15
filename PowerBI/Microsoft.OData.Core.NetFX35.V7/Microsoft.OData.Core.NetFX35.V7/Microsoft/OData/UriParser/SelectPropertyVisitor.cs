using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000199 RID: 409
	internal sealed class SelectPropertyVisitor : PathSegmentTokenVisitor
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x0002DAAB File Offset: 0x0002BCAB
		public SelectPropertyVisitor(IEdmModel model, IEdmStructuredType edmType, int maxDepth, SelectExpandClause expandClauseToDecorate, ODataUriResolver resolver)
		{
			this.model = model;
			this.edmType = edmType;
			this.maxDepth = maxDepth;
			this.expandClauseToDecorate = expandClauseToDecorate;
			this.resolver = resolver;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		public SelectExpandClause DecoratedExpandClause
		{
			get
			{
				return this.expandClauseToDecorate;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
		public override void Visit(SystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<SystemToken>(tokenIn, "tokenIn");
			throw new ODataException(Strings.SelectPropertyVisitor_SystemTokenInSelect(tokenIn.Identifier));
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002DB00 File Offset: 0x0002BD00
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

		// Token: 0x06001091 RID: 4241 RVA: 0x0002DB48 File Offset: 0x0002BD48
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
				for (;;)
				{
					edmStructuredType = odataPathSegment.EdmType as IEdmStructuredType;
					IEdmCollectionType edmCollectionType = odataPathSegment.EdmType as IEdmCollectionType;
					IEdmPrimitiveType edmPrimitiveType = odataPathSegment.EdmType as IEdmPrimitiveType;
					DynamicPathSegment dynamicPathSegment = odataPathSegment as DynamicPathSegment;
					if ((edmStructuredType == null || edmStructuredType.TypeKind != EdmTypeKind.Complex) && (edmCollectionType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Complex) && (edmPrimitiveType == null || edmPrimitiveType.TypeKind != EdmTypeKind.Primitive) && (dynamicPathSegment == null || tokenIn.NextToken == null))
					{
						goto IL_01C9;
					}
					NonSystemToken nonSystemToken = tokenIn.NextToken as NonSystemToken;
					if (nonSystemToken == null)
					{
						goto IL_01C9;
					}
					if (edmPrimitiveType == null && dynamicPathSegment == null)
					{
						if (edmStructuredType == null)
						{
							edmStructuredType = edmCollectionType.ElementType.Definition as IEdmStructuredType;
						}
						odataPathSegment = SelectPathSegmentTokenBinder.ConvertNonTypeTokenToSegment(nonSystemToken, this.model, edmStructuredType, this.resolver);
					}
					else
					{
						EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(nonSystemToken.Identifier);
						IEdmPrimitiveType primitiveType = EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
						if (primitiveType != null)
						{
							odataPathSegment = new TypeSegment(primitiveType, primitiveType, null);
						}
						else
						{
							if (dynamicPathSegment == null)
							{
								break;
							}
							odataPathSegment = new DynamicPathSegment(nonSystemToken.Identifier);
						}
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
						goto IL_01C9;
					}
					tokenIn = nonSystemToken;
					list.Add(odataPathSegment);
				}
				throw new ODataException(Strings.SelectBinder_MultiLevelPathInSelect);
			}
			IL_01C9:
			ODataSelectPath selectedPath = new ODataSelectPath(list);
			PathSelectItem pathSelectItem = new PathSelectItem(selectedPath);
			if (tokenIn.NextToken != null)
			{
				throw new ODataException(Strings.SelectBinder_MultiLevelPathInSelect);
			}
			NavigationPropertySegment navigationPropertySegment = pathSelectItem.SelectedPath.LastSegment as NavigationPropertySegment;
			if (navigationPropertySegment != null && Enumerable.Any<SelectItem>(this.expandClauseToDecorate.SelectedItems, (SelectItem x) => x is PathSelectItem && ((PathSelectItem)x).SelectedPath.Equals(selectedPath)))
			{
				return;
			}
			this.expandClauseToDecorate.AddToSelectedItems(pathSelectItem);
		}

		// Token: 0x040008A8 RID: 2216
		private readonly IEdmModel model;

		// Token: 0x040008A9 RID: 2217
		private readonly int maxDepth;

		// Token: 0x040008AA RID: 2218
		private readonly SelectExpandClause expandClauseToDecorate;

		// Token: 0x040008AB RID: 2219
		private readonly IEdmStructuredType edmType;

		// Token: 0x040008AC RID: 2220
		private readonly ODataUriResolver resolver;
	}
}
