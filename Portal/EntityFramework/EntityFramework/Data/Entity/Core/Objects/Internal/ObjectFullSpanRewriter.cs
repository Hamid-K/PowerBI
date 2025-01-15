using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200044E RID: 1102
	internal class ObjectFullSpanRewriter : ObjectSpanRewriter
	{
		// Token: 0x060035B8 RID: 13752 RVA: 0x000ACA98 File Offset: 0x000AAC98
		internal ObjectFullSpanRewriter(DbCommandTree tree, DbExpression toRewrite, Span span, AliasGenerator aliasGenerator)
			: base(tree, toRewrite, aliasGenerator)
		{
			EntityType entityType = null;
			if (!ObjectFullSpanRewriter.TryGetEntityType(base.Query.ResultType, out entityType))
			{
				throw new InvalidOperationException(Strings.ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection);
			}
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = new ObjectFullSpanRewriter.SpanPathInfo(entityType);
			foreach (Span.SpanPath spanPath in span.SpanList)
			{
				this.AddSpanPath(spanPathInfo, spanPath.Navigations);
			}
			this._currentSpanPath.Push(spanPathInfo);
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000ACB3C File Offset: 0x000AAD3C
		private void AddSpanPath(ObjectFullSpanRewriter.SpanPathInfo parentInfo, List<string> navPropNames)
		{
			this.ConvertSpanPath(parentInfo, navPropNames, 0);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000ACB48 File Offset: 0x000AAD48
		private void ConvertSpanPath(ObjectFullSpanRewriter.SpanPathInfo parentInfo, List<string> navPropNames, int pos)
		{
			NavigationProperty navigationProperty = null;
			if (!parentInfo.DeclaringType.NavigationProperties.TryGetValue(navPropNames[pos], true, out navigationProperty))
			{
				throw new InvalidOperationException(Strings.ObjectQuery_Span_NoNavProp(parentInfo.DeclaringType.FullName, navPropNames[pos]));
			}
			if (parentInfo.Children == null)
			{
				parentInfo.Children = new Dictionary<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo>();
			}
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = null;
			if (!parentInfo.Children.TryGetValue(navigationProperty, out spanPathInfo))
			{
				spanPathInfo = new ObjectFullSpanRewriter.SpanPathInfo(ObjectFullSpanRewriter.EntityTypeFromResultType(navigationProperty));
				parentInfo.Children[navigationProperty] = spanPathInfo;
			}
			if (pos < navPropNames.Count - 1)
			{
				this.ConvertSpanPath(spanPathInfo, navPropNames, pos + 1);
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000ACBE4 File Offset: 0x000AADE4
		private static EntityType EntityTypeFromResultType(NavigationProperty navProp)
		{
			EntityType entityType = null;
			ObjectFullSpanRewriter.TryGetEntityType(navProp.TypeUsage, out entityType);
			return entityType;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000ACC04 File Offset: 0x000AAE04
		private static bool TryGetEntityType(TypeUsage resultType, out EntityType entityType)
		{
			if (BuiltInTypeKind.EntityType == resultType.EdmType.BuiltInTypeKind)
			{
				entityType = (EntityType)resultType.EdmType;
				return true;
			}
			if (BuiltInTypeKind.CollectionType == resultType.EdmType.BuiltInTypeKind)
			{
				EdmType edmType = ((CollectionType)resultType.EdmType).TypeUsage.EdmType;
				if (BuiltInTypeKind.EntityType == edmType.BuiltInTypeKind)
				{
					entityType = (EntityType)edmType;
					return true;
				}
			}
			entityType = null;
			return false;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000ACC6B File Offset: 0x000AAE6B
		private AssociationEndMember GetNavigationPropertyTargetEnd(NavigationProperty property)
		{
			return base.Metadata.GetItem<AssociationType>(property.RelationshipType.FullName, DataSpace.CSpace).AssociationEndMembers[property.ToEndMember.Name];
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000ACC9C File Offset: 0x000AAE9C
		internal override ObjectSpanRewriter.SpanTrackingInfo CreateEntitySpanTrackingInfo(DbExpression expression, EntityType entityType)
		{
			ObjectSpanRewriter.SpanTrackingInfo spanTrackingInfo = default(ObjectSpanRewriter.SpanTrackingInfo);
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = this._currentSpanPath.Peek();
			if (spanPathInfo.Children != null)
			{
				int num = 1;
				foreach (KeyValuePair<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo> keyValuePair in spanPathInfo.Children)
				{
					if (spanTrackingInfo.ColumnDefinitions == null)
					{
						spanTrackingInfo = base.InitializeTrackingInfo(base.RelationshipSpan);
					}
					DbExpression dbExpression = expression.Property(keyValuePair.Key);
					this._currentSpanPath.Push(keyValuePair.Value);
					dbExpression = base.Rewrite(dbExpression);
					this._currentSpanPath.Pop();
					spanTrackingInfo.ColumnDefinitions.Add(new KeyValuePair<string, DbExpression>(spanTrackingInfo.ColumnNames.Next(), dbExpression));
					AssociationEndMember navigationPropertyTargetEnd = this.GetNavigationPropertyTargetEnd(keyValuePair.Key);
					spanTrackingInfo.SpannedColumns[num] = navigationPropertyTargetEnd;
					if (base.RelationshipSpan)
					{
						spanTrackingInfo.FullSpannedEnds[navigationPropertyTargetEnd] = true;
					}
					num++;
				}
			}
			return spanTrackingInfo;
		}

		// Token: 0x04001158 RID: 4440
		private readonly Stack<ObjectFullSpanRewriter.SpanPathInfo> _currentSpanPath = new Stack<ObjectFullSpanRewriter.SpanPathInfo>();

		// Token: 0x02000A52 RID: 2642
		private class SpanPathInfo
		{
			// Token: 0x0600618D RID: 24973 RVA: 0x0014FDD6 File Offset: 0x0014DFD6
			internal SpanPathInfo(EntityType declaringType)
			{
				this.DeclaringType = declaringType;
			}

			// Token: 0x04002A64 RID: 10852
			internal readonly EntityType DeclaringType;

			// Token: 0x04002A65 RID: 10853
			internal Dictionary<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo> Children;
		}
	}
}
