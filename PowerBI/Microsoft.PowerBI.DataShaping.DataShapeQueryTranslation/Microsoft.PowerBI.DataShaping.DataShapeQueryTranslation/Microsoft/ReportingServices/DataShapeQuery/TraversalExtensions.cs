using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x0200000F RID: 15
	internal static class TraversalExtensions
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003636 File Offset: 0x00001836
		public static IEnumerable<DataMember> GetAllMembersDepthFirst(this DataHierarchy hierarchy)
		{
			if (hierarchy == null)
			{
				return Enumerable.Empty<DataMember>();
			}
			return hierarchy.DataMembers.GetAllMembersDepthFirst();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000364C File Offset: 0x0000184C
		public static IEnumerable<DataMember> GetAllMembersDepthFirst(this DataMember member)
		{
			if (member == null)
			{
				yield break;
			}
			yield return member;
			foreach (DataMember dataMember in member.DataMembers.GetAllMembersDepthFirst())
			{
				yield return dataMember;
			}
			IEnumerator<DataMember> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000365C File Offset: 0x0000185C
		public static IEnumerable<DataMember> GetAllMembersDepthFirst(this List<DataMember> members)
		{
			if (members == null)
			{
				return Enumerable.Empty<DataMember>();
			}
			return members.SelectMany((DataMember m) => m.GetAllMembersDepthFirst());
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000368C File Offset: 0x0000188C
		public static IEnumerable<DataMember> GetAllDynamicMembers(this DataHierarchy hierarchy)
		{
			return from m in hierarchy.GetAllMembersDepthFirst()
				where m.IsDynamic
				select m;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036B8 File Offset: 0x000018B8
		public static IEnumerable<DataMember> GetAllDynamicMembers(this DataMember member)
		{
			return from m in member.GetAllMembersDepthFirst()
				where m.IsDynamic
				select m;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036E4 File Offset: 0x000018E4
		public static DataMember DynamicChildDataMemberOrDefault(this DataMember member)
		{
			List<DataMember> dataMembers = member.DataMembers;
			if (dataMembers == null)
			{
				return null;
			}
			return dataMembers.SingleOrDefault((DataMember d) => d.IsDynamic);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003716 File Offset: 0x00001916
		public static DataMember StaticChildDataMemberOrDefault(this DataMember member)
		{
			List<DataMember> dataMembers = member.DataMembers;
			if (dataMembers == null)
			{
				return null;
			}
			return dataMembers.SingleOrDefault((DataMember d) => !d.IsDynamic);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003748 File Offset: 0x00001948
		public static DataMember StaticChildDataMemberOrDefault(this DataHierarchy hierarchy)
		{
			if (hierarchy == null)
			{
				return null;
			}
			List<DataMember> dataMembers = hierarchy.DataMembers;
			if (dataMembers == null)
			{
				return null;
			}
			return dataMembers.SingleOrDefault((DataMember d) => !d.IsDynamic);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000377F File Offset: 0x0000197F
		public static bool HasDynamic(this DataHierarchy hierarchy)
		{
			return hierarchy.GetAllDynamicMembers().Count<DataMember>() > 0;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000378F File Offset: 0x0000198F
		public static bool HasPrimaryMembers(this DataShape dataShape)
		{
			return dataShape.PrimaryHierarchy != null && dataShape.PrimaryHierarchy.DataMembers != null && dataShape.PrimaryHierarchy.DataMembers.Count != 0;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000037BC File Offset: 0x000019BC
		public static bool HasDynamicChild(this DataMember member)
		{
			IEnumerable<DataMember> enumerable = member.GetAllDynamicMembers();
			if (member.IsDynamic)
			{
				enumerable = enumerable.Skip(1);
			}
			return enumerable.Any<DataMember>();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037E8 File Offset: 0x000019E8
		public static bool References(this IEnumerable<Filter> filters, ExpressionTable inputExpressionTable, IIdentifiable target)
		{
			return filters != null && filters.Any((Filter f) => TraversalExtensions.RefersToTargetStructure(f.Target, target, inputExpressionTable));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003820 File Offset: 0x00001A20
		internal static Filter GetFilterForTargetStructure(this List<Filter> filters, ExpressionTable inputExpressionTable, IIdentifiable target)
		{
			if (filters == null)
			{
				return null;
			}
			return filters.FirstOrDefault((Filter f) => f.Condition.ObjectType != ObjectType.FilterEmptyGroupsCondition && TraversalExtensions.RefersToTargetStructure(f.Target, target, inputExpressionTable));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003858 File Offset: 0x00001A58
		internal static IReadOnlyList<Filter> GetFiltersForTargetStructureType<T>(this List<Filter> filters, ExpressionTable inputExpressionTable)
		{
			if (filters == null)
			{
				return null;
			}
			return filters.Where((Filter f) => f.Condition.ObjectType != ObjectType.FilterEmptyGroupsCondition && TraversalExtensions.RefersToTargetStructureType<T>(f.Target, inputExpressionTable)).ToList<Filter>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003890 File Offset: 0x00001A90
		private static bool RefersToTargetStructure(Expression expression, IIdentifiable target, ExpressionTable inputExpressionTable)
		{
			ResolvedStructureReferenceExpressionNode resolvedStructureReferenceExpressionNode = inputExpressionTable.GetNode(expression) as ResolvedStructureReferenceExpressionNode;
			return resolvedStructureReferenceExpressionNode != null && resolvedStructureReferenceExpressionNode.Target.Equals(target);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000038BC File Offset: 0x00001ABC
		private static bool RefersToTargetStructureType<T>(Expression expression, ExpressionTable inputExpressionTable)
		{
			ResolvedStructureReferenceExpressionNode resolvedStructureReferenceExpressionNode = inputExpressionTable.GetNode(expression) as ResolvedStructureReferenceExpressionNode;
			return resolvedStructureReferenceExpressionNode != null && resolvedStructureReferenceExpressionNode.Target is T;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038E9 File Offset: 0x00001AE9
		internal static IScope GetResolvedScope(this Expression expression, ExpressionTable inputExpressionTable)
		{
			return ((ResolvedScopeReferenceExpressionNode)inputExpressionTable.GetNode(expression)).Scope;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000038FC File Offset: 0x00001AFC
		internal static DataMember GetResolvedMember(this Expression expression, ExpressionTable inputExpressionTable)
		{
			IScope resolvedScope = expression.GetResolvedScope(inputExpressionTable);
			DataMember dataMember = resolvedScope as DataMember;
			Microsoft.DataShaping.Contract.RetailAssert(dataMember != null, "Expected expression to refer to a data member. Reference type: {0}, Expression:{1}", ((resolvedScope != null) ? resolvedScope.GetType().ToString() : null) ?? "<null>", expression.ExpressionId);
			return dataMember;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000394A File Offset: 0x00001B4A
		internal static IIdentifiable GetResolvedTargetStructure(this Expression expression, ExpressionTable inputExpressionTable)
		{
			return ((ResolvedStructureReferenceExpressionNode)inputExpressionTable.GetNode(expression)).Target;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003960 File Offset: 0x00001B60
		internal static bool HasFilterEmptyGroups(this DataShape dataShape)
		{
			List<Filter> filters = dataShape.Filters;
			if (filters == null)
			{
				return false;
			}
			return filters.Any((Filter f) => f.Condition.ObjectType == ObjectType.FilterEmptyGroupsCondition);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000039A0 File Offset: 0x00001BA0
		internal static Filter GetFilterForTargetExpression(this List<Filter> filters, Expression target)
		{
			if (filters == null)
			{
				return null;
			}
			return filters.FirstOrDefault((Filter f) => f.Condition.ObjectType != ObjectType.FilterEmptyGroupsCondition && f.Target.OriginalNode.Equals(target.OriginalNode));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000039D4 File Offset: 0x00001BD4
		internal static IEnumerable<Limit> GetLimitsForTargetScope(this List<Limit> limits, ExpressionTable inputExpressionTable, IScope scope)
		{
			if (limits == null)
			{
				return null;
			}
			return limits.Where((Limit l) => TraversalExtensions.RefersToTargetStructure(l.GetInnermostTarget(), scope, inputExpressionTable));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A0C File Offset: 0x00001C0C
		internal static DataMember GetLeafMember(this DataShape dataShape, DataMemberAnnotations dataMemberAnnotations, bool primaryMember, int leafIndex, out IScope parentScope)
		{
			DataMember leafMember = TraversalExtensions.GetLeafMember(primaryMember ? dataShape.PrimaryHierarchy.DataMembers : dataShape.SecondaryHierarchy.DataMembers, dataMemberAnnotations, leafIndex, out parentScope);
			if (leafMember != null && parentScope == null)
			{
				parentScope = dataShape;
			}
			return leafMember;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A40 File Offset: 0x00001C40
		private static DataMember GetLeafMember(List<DataMember> members, DataMemberAnnotations dataMemberAnnotations, int leafIndex, out IScope parentScope)
		{
			parentScope = null;
			if (members == null)
			{
				return null;
			}
			for (int i = 0; i < members.Count; i++)
			{
				DataMember dataMember = members[i];
				if (dataMemberAnnotations.IsLeaf(dataMember))
				{
					if (dataMemberAnnotations.GetLeafIndex(dataMember) == leafIndex)
					{
						return dataMember;
					}
				}
				else
				{
					DataMember leafMember = TraversalExtensions.GetLeafMember(dataMember.DataMembers, dataMemberAnnotations, leafIndex, out parentScope);
					if (leafMember != null)
					{
						if (parentScope == null && dataMember.IsDynamic)
						{
							parentScope = dataMember;
						}
						return leafMember;
					}
				}
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AA8 File Offset: 0x00001CA8
		internal static List<DataMember> GetPeerMembers(DataShape dataShape, bool inPrimaryHierarchy)
		{
			List<DataMember> list = (inPrimaryHierarchy ? dataShape.PrimaryHierarchy.DataMembers : dataShape.SecondaryHierarchy.DataMembers);
			if (list == null)
			{
				return null;
			}
			List<DataMember> list2 = new List<DataMember>();
			TraversalExtensions.GetPeerMembers(list, list2);
			return list2;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003AE4 File Offset: 0x00001CE4
		internal static List<DataMember> GetPeerMembers(DataMember dataMember)
		{
			if (dataMember.DataMembers == null)
			{
				return null;
			}
			List<DataMember> list = new List<DataMember>();
			TraversalExtensions.GetPeerMembers(dataMember.DataMembers, list);
			return list;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003B10 File Offset: 0x00001D10
		private static void GetPeerMembers(List<DataMember> members, List<DataMember> peerMembers)
		{
			if (members == null)
			{
				return;
			}
			for (int i = 0; i < members.Count; i++)
			{
				DataMember dataMember = members[i];
				peerMembers.Add(dataMember);
				if (!dataMember.IsDynamic)
				{
					TraversalExtensions.GetPeerMembers(dataMember.DataMembers, peerMembers);
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B58 File Offset: 0x00001D58
		internal static IEnumerable<Expression> GetAllGroupKeyExpressions(this DataHierarchy dataHierarchy, DataShapeAnnotations annotations)
		{
			return dataHierarchy.GetAllDynamicMembers().SelectMany((DataMember d) => d.GetGroupKeyExpressions(annotations));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B8C File Offset: 0x00001D8C
		internal static IEnumerable<Expression> GetAllGroupAndSortKeyExpressions(this DataHierarchy dataHierarchy, DataShapeAnnotations annotations)
		{
			return dataHierarchy.GetAllDynamicMembers().SelectMany((DataMember d) => d.GetSortKeyExpressions(annotations).ConcatNullable(d.GetGroupKeyExpressions(annotations)));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003BC0 File Offset: 0x00001DC0
		internal static IEnumerable<Expression> GetGroupKeyExpressions(this DataMember dataMember, DataShapeAnnotations annotations)
		{
			Group group = dataMember.GetGroup(annotations);
			IEnumerable<Expression> enumerable;
			if (group == null)
			{
				enumerable = null;
			}
			else
			{
				List<GroupKey> groupKeys = group.GroupKeys;
				if (groupKeys == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = groupKeys.Select((GroupKey d) => d.Value);
				}
			}
			return enumerable ?? Microsoft.DataShaping.Util.EmptyReadOnlyCollection<Expression>();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003C14 File Offset: 0x00001E14
		internal static IEnumerable<Expression> GetSortKeyExpressions(this DataMember dataMember, DataShapeAnnotations annotations)
		{
			Group group = dataMember.GetGroup(annotations);
			IEnumerable<Expression> enumerable;
			if (group == null)
			{
				enumerable = null;
			}
			else
			{
				List<SortKey> sortKeys = group.SortKeys;
				if (sortKeys == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = sortKeys.Select((SortKey d) => d.Value);
				}
			}
			return enumerable ?? Microsoft.DataShaping.Util.EmptyReadOnlyCollection<Expression>();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C68 File Offset: 0x00001E68
		internal static IReadOnlyList<GroupKey> GetGroupKeys(this DataMember dataMember, DataShapeAnnotations annotations)
		{
			Group group = dataMember.GetGroup(annotations);
			if (group == null)
			{
				return null;
			}
			return group.GroupKeys;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C88 File Offset: 0x00001E88
		internal static bool UsesShowItemsWithNoData(this DataMember dataMember)
		{
			if (dataMember.Group == null || dataMember.Group.GroupKeys == null)
			{
				return false;
			}
			return dataMember.Group.GroupKeys.Any((GroupKey k) => k.ShowItemsWithNoData.GetValueOrDefault<bool>());
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003CDB File Offset: 0x00001EDB
		internal static IEnumerable<ScopeId> GetAllStartPositions(this List<DataMember> dateMembers)
		{
			return dateMembers.Select((DataMember d) => d.Group.StartPosition).WhereNonNull<ScopeId>();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003D08 File Offset: 0x00001F08
		internal static IEnumerable<SortKey> GetAllSortKeys(this List<DataMember> dateMembers, DataShapeAnnotations annotations)
		{
			return dateMembers.SelectMany((DataMember d) => d.GetSortKeys(annotations).EmptyIfNull<SortKey>());
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D34 File Offset: 0x00001F34
		private static IEnumerable<SortKey> GetSortKeys(this DataMember dataMember, DataShapeAnnotations annotations)
		{
			Group group = dataMember.GetGroup(annotations);
			if (group == null)
			{
				return null;
			}
			return group.SortKeys;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003D54 File Offset: 0x00001F54
		internal static bool TryGetDetailGroupIdentityEntity(this DetailGroupIdentity detailGroupIdentity, ExpressionTable expressionTable, out IConceptualEntity entity)
		{
			entity = null;
			if (detailGroupIdentity == null)
			{
				return false;
			}
			ResolvedEntitySetExpressionNode resolvedEntitySetExpressionNode = expressionTable.GetNode(detailGroupIdentity.Value) as ResolvedEntitySetExpressionNode;
			if (resolvedEntitySetExpressionNode == null)
			{
				return false;
			}
			entity = resolvedEntitySetExpressionNode.Entity;
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003D8C File Offset: 0x00001F8C
		internal static bool HasGroupStartPosition(this DataShape dataShape)
		{
			DataMember dataMember = dataShape.PrimaryHierarchy.GetAllDynamicMembers().FirstOrDefault<DataMember>();
			return dataMember != null && dataMember.Group.StartPosition != null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003DBD File Offset: 0x00001FBD
		internal static bool HasStartPosition(this DataShape dataShape)
		{
			if (dataShape.PrimaryHierarchy == null)
			{
				return false;
			}
			return dataShape.PrimaryHierarchy.DataMembers.Any((DataMember member) => member.HasStartPosition());
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003DF8 File Offset: 0x00001FF8
		internal static int ComputeLeafCount(this IList<DataMember> members)
		{
			int num = 0;
			foreach (DataMember dataMember in members)
			{
				if (dataMember.DataMembers == null)
				{
					num++;
				}
				else
				{
					num += dataMember.DataMembers.ComputeLeafCount();
				}
			}
			return num;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E58 File Offset: 0x00002058
		internal static Group GetGroup(this DataMember dataMember, DataShapeAnnotations annotations)
		{
			if (!dataMember.IsDynamic)
			{
				return null;
			}
			return dataMember.Group;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003E6C File Offset: 0x0000206C
		public static FilterCondition RemoveCondition(this CompoundFilterCondition condition, FilterCondition otherCondition)
		{
			if (otherCondition == condition)
			{
				return null;
			}
			List<FilterCondition> list = condition.Conditions.Except(new FilterCondition[] { otherCondition }).ToList<FilterCondition>();
			if (!list.Any<FilterCondition>())
			{
				return null;
			}
			return condition.Clone(list);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EAB File Offset: 0x000020AB
		internal static bool HasStartPosition(this DataMember member)
		{
			if (member.IsDynamic)
			{
				return member.Group.StartPosition != null;
			}
			return member.SubtotalStartPosition.IsSpecified<bool>();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003ECF File Offset: 0x000020CF
		internal static bool ParticipatesInWindowing(this DataMember member, BatchSubtotalAnnotations annotations)
		{
			return !member.ContextOnly && (member.IsDynamic || member.IsSubtotal(annotations));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003EEC File Offset: 0x000020EC
		internal static bool IsSubtotal(this DataMember member, BatchSubtotalAnnotations annotations)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			return annotations.TryGetSubtotalSourceAnnotation(member, out batchSubtotalAnnotation);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F02 File Offset: 0x00002102
		internal static bool ShouldHaveRestartDefinitions(this DataShape dataShape)
		{
			return dataShape.IncludeRestartToken.GetValueOrDefault<bool>() || dataShape.RestartTokens != null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F1C File Offset: 0x0000211C
		internal static IEnumerable<DataMember> GetSegmentationMembers(this DataShape dataShape, BatchSubtotalAnnotations subtotalAnnotations)
		{
			if (dataShape.PrimaryHierarchy == null || dataShape.PrimaryHierarchy.DataMembers == null)
			{
				return null;
			}
			return dataShape.PrimaryHierarchy.GetAllMembersDepthFirst().GetSegmentationMembers(subtotalAnnotations);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F48 File Offset: 0x00002148
		internal static IEnumerable<DataMember> GetSegmentationMembers(this IEnumerable<DataMember> members, BatchSubtotalAnnotations subtotalAnnotations)
		{
			if (members.IsNullOrEmpty<DataMember>())
			{
				return null;
			}
			return members.Where((DataMember m) => m.ParticipatesInWindowing(subtotalAnnotations));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F7E File Offset: 0x0000217E
		internal static IEnumerable<DataMember> GetRestartMembers(this DataShape dataShape, BatchSubtotalAnnotations subtotalAnnotations)
		{
			if (!dataShape.ShouldHaveRestartDefinitions())
			{
				return null;
			}
			return dataShape.GetSegmentationMembers(subtotalAnnotations);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F94 File Offset: 0x00002194
		internal static bool TryGetColumn(this DataTransformTable table, Identifier columnId, out DataTransformTableColumn column)
		{
			foreach (DataTransformTableColumn dataTransformTableColumn in table.Columns)
			{
				if (dataTransformTableColumn.Id == columnId)
				{
					column = dataTransformTableColumn;
					return true;
				}
			}
			column = null;
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003FFC File Offset: 0x000021FC
		internal static DataTransformTableColumn GetColumn(this DataTransformTable table, Identifier columnId)
		{
			DataTransformTableColumn dataTransformTableColumn;
			Microsoft.DataShaping.Contract.RetailAssert(table.TryGetColumn(columnId, out dataTransformTableColumn), "Missing expected column {0}", columnId);
			return dataTransformTableColumn;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004020 File Offset: 0x00002220
		public static DataShape GetParentDataShape(this IContextItem contextItem, ScopeTree scopeTree, DataShapeAnnotations annotations)
		{
			IContextItem contextItem2 = contextItem;
			DataShape dataShape = contextItem2 as DataShape;
			if (dataShape != null)
			{
				contextItem2 = scopeTree.GetParentScope(dataShape);
				if (contextItem2 == null)
				{
					return null;
				}
			}
			return annotations.GetContainingDataShape(contextItem2);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004050 File Offset: 0x00002250
		public static bool IsFilterContextDataShape(this DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations)
		{
			DataShape parentDataShape = dataShape.GetParentDataShape(scopeTree, annotations);
			DataShape dataShape2;
			return parentDataShape != null && annotations.TryGetFilterContextDataShape(parentDataShape, out dataShape2) && dataShape2 == dataShape;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000407C File Offset: 0x0000227C
		public static Expression GetInnermostTarget(this Limit limit)
		{
			return limit.Targets[limit.Targets.Count - 1];
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004098 File Offset: 0x00002298
		public static List<DataMember> GetGroupScopesFromTargets(this Limit limit, ExpressionTable expressionTable)
		{
			List<DataMember> list = new List<DataMember>(limit.Targets.Count);
			foreach (Expression expression in limit.Targets)
			{
				DataMember dataMember = expression.GetResolvedScope(expressionTable) as DataMember;
				if (dataMember != null)
				{
					list.Add(dataMember);
				}
			}
			return list;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000410C File Offset: 0x0000230C
		internal static bool IsDictionaryEncodingNeeded(this DataShapeContext dsContext)
		{
			if (!dsContext.HasAnyScopedLimits)
			{
				if (!dsContext.HasBinnedLineSampleLimit && !dsContext.HasOverlappingPointsSampleLimit)
				{
					Limit primaryHierarchyLimit = dsContext.PrimaryHierarchyLimit;
					if (!(((primaryHierarchyLimit != null) ? primaryHierarchyLimit.Operator : null) is SampleLimitOperator))
					{
						Limit secondaryHierarchyLimit = dsContext.SecondaryHierarchyLimit;
						return !(((secondaryHierarchyLimit != null) ? secondaryHierarchyLimit.Operator : null) is SampleLimitOperator);
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000416B File Offset: 0x0000236B
		internal static bool IsDataShapeLevelProjection(this Calculation calculation, DataShapeAnnotations annotations, IScope containingScope, ExpressionNode node)
		{
			return containingScope is DataShape && (annotations.GetReferencedScopes(calculation).Count == 0 && ModelReferenceAnalyzer.ContainsModelReference(node) && !annotations.IsSubtotal(calculation)) && !annotations.CanBeHandledByProcessing(calculation);
		}
	}
}
