using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000EB RID: 235
	internal class DataMemberBuilder<TParent> : BuilderBase<DataMember, TParent>, IWithDataShape<DataMemberBuilder<TParent>>, ICalculationContainer<DataMemberBuilder<TParent>>, IDataMemberContainer<DataMemberBuilder<TParent>>
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x0000DE6C File Offset: 0x0000C06C
		internal DataMemberBuilder(TParent parent, DataMember activeObject)
			: base(parent, activeObject)
		{
			this.m_calculationsByExpressionNode = new Dictionary<ExpressionNode, Calculation>();
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000DE81 File Offset: 0x0000C081
		public Identifier Id
		{
			get
			{
				return base.ActiveObject.Id;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000DE90 File Offset: 0x0000C090
		public Calculation GetCalculationOrDefault(ExpressionNode node)
		{
			Calculation calculation;
			if (this.m_calculationsByExpressionNode.TryGetValue(node, out calculation))
			{
				return calculation;
			}
			return null;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public DataMemberBuilder<TParent> WithCalculation(string identifier, Expression value, bool suppressJoinPredicate = false, bool? respectInstanceFilters = null, string nativeReferenceName = null, bool isContextOnly = false)
		{
			base.ActiveObject.Calculations = base.AddCalculation(base.ActiveObject.Calculations, identifier, value, suppressJoinPredicate, respectInstanceFilters, nativeReferenceName, isContextOnly);
			if (!this.m_calculationsByExpressionNode.ContainsKey(value.OriginalNode))
			{
				this.m_calculationsByExpressionNode.Add(value.OriginalNode, base.ActiveObject.Calculations[base.ActiveObject.Calculations.Count - 1]);
			}
			return this;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000DF2C File Offset: 0x0000C12C
		public DataMemberBuilder<TParent> WithVisualCalculation(string identifier, string nativeReferenceName, string daxExpression)
		{
			return this.WithCalculation(identifier, daxExpression.VisualCalculation(), false, null, nativeReferenceName, false);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000DF59 File Offset: 0x0000C159
		public DataMemberBuilder<TParent> EnsureCalculations()
		{
			base.ActiveObject.Calculations = base.ActiveObject.Calculations ?? new List<Calculation>();
			return this;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public GroupBuilder<DataMemberBuilder<TParent>> WithGroup(bool suppressSortByMeasureRollup = false)
		{
			DataMember activeObject = base.ActiveObject;
			Contract.RetailAssert(activeObject.Group == null, "Group exists.");
			Group group = new Group();
			group.SuppressSortByMeasureRollup = suppressSortByMeasureRollup;
			activeObject.Group = group;
			return new GroupBuilder<DataMemberBuilder<TParent>>(this, group);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public GroupBuilder<DataMemberBuilder<TParent>> WithGroup(Expression groupExpr, SortDirection sortDirection = SortDirection.Ascending, ScalarValue? startAtValue = null, Identifier groupKeyIdentifier = null, bool suppressSortByMeasureRollup = false, bool? showItemsWithNoData = null, bool suppressScopeValue = false)
		{
			DataMember activeObject = base.ActiveObject;
			Contract.RetailAssert(activeObject.Group == null, "Group exists.");
			Group group = new Group();
			group.SuppressSortByMeasureRollup = suppressSortByMeasureRollup;
			activeObject.Group = group;
			GroupBuilder<DataMemberBuilder<TParent>> groupBuilder = new GroupBuilder<DataMemberBuilder<TParent>>(this, group);
			bool? flag = showItemsWithNoData;
			groupBuilder.WithGroupAndSortKey(groupExpr, sortDirection, (flag != null) ? flag.GetValueOrDefault() : null, groupKeyIdentifier, null, suppressScopeValue);
			groupBuilder.WithStartPosition(startAtValue);
			return groupBuilder;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000E038 File Offset: 0x0000C238
		public DataMemberBuilder<TParent> WithPrimaryStaticMember(string identifier, bool? subtotalStartPosition = null)
		{
			return this.WithDataMember(identifier, true, subtotalStartPosition).Parent();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000E048 File Offset: 0x0000C248
		public DataMemberBuilder<TParent> WithSecondaryStaticMember(string identifier, bool? subtotalStartPosition = null)
		{
			return this.WithDataMember(identifier, false, subtotalStartPosition).Parent();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000E058 File Offset: 0x0000C258
		public DataMemberBuilder<DataMemberBuilder<TParent>> WithStaticMember(string identifier, bool isPrimary, bool? subtotalStartPosition)
		{
			return this.WithDataMember(identifier, isPrimary, subtotalStartPosition);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000E063 File Offset: 0x0000C263
		public DataMemberBuilder<DataMemberBuilder<TParent>> WithPrimaryMember(Identifier identifier, bool? subtotalStartPosition = null)
		{
			return this.WithDataMember(identifier.Value, true, subtotalStartPosition);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000E074 File Offset: 0x0000C274
		public DataMemberBuilder<DataMemberBuilder<TParent>> WithSecondaryMember(Identifier identifier)
		{
			return this.WithDataMember(identifier.Value, false, null);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000E098 File Offset: 0x0000C298
		private DataMemberBuilder<DataMemberBuilder<TParent>> WithDataMember(string identifier, bool isPrimary, bool? subtotalStartPosition = null)
		{
			DataMember dataMember = new DataMember();
			dataMember.Id = new Identifier(identifier);
			bool? flag = subtotalStartPosition;
			dataMember.SubtotalStartPosition = ((flag != null) ? flag.GetValueOrDefault() : null);
			DataMember dataMember2 = dataMember;
			DataMember activeObject = base.ActiveObject;
			List<DataMember> list = activeObject.DataMembers;
			if (list == null)
			{
				list = new List<DataMember>();
				activeObject.DataMembers = list;
			}
			list.Add(dataMember2);
			return new DataMemberBuilder<DataMemberBuilder<TParent>>(this, dataMember2);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000E104 File Offset: 0x0000C304
		public DataShapeBuilder<DataMemberBuilder<TParent>> WithDataShape(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query)
		{
			return base.AddDataShape<DataMemberBuilder<TParent>>(this, this.GetDataShapesList(), identifier, dataSourceId, filterEmptyGroups, contextOnly, independent, usage);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000E127 File Offset: 0x0000C327
		public DataShapeBuilder<DataMemberBuilder<TParent>> WithDataShape(DataShape dataShape)
		{
			return base.AddDataShape<DataMemberBuilder<TParent>>(this, this.GetDataShapesList(), dataShape);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000E137 File Offset: 0x0000C337
		public DataMemberBuilder<TParent> WithExplicitSubtotal()
		{
			base.ActiveObject.HasExplicitSubtotal = true;
			return this;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000E146 File Offset: 0x0000C346
		public DataMemberBuilder<TParent> WithInstanceFilters(List<FilterCondition> conditions)
		{
			if (base.ActiveObject.InstanceFilters == null)
			{
				base.ActiveObject.InstanceFilters = new List<FilterCondition>(conditions.Count);
			}
			base.ActiveObject.InstanceFilters.AddRange(conditions);
			return this;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000E180 File Offset: 0x0000C380
		public FilterConditionBuilder<DataMemberBuilder<TParent>> WithInstanceFilter()
		{
			Action<FilterCondition> action = delegate(FilterCondition cond)
			{
				if (base.ActiveObject.InstanceFilters == null)
				{
					base.ActiveObject.InstanceFilters = new List<FilterCondition>(1);
				}
				base.ActiveObject.InstanceFilters.Add(cond);
			};
			return new FilterConditionBuilder<DataMemberBuilder<TParent>>(this, action);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000E1A1 File Offset: 0x0000C3A1
		public DataMemberBuilder<TParent> WithContextOnlyTrue()
		{
			base.ActiveObject.ContextOnly = true;
			return this;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		protected List<DataShape> GetDataShapesList()
		{
			return base.ActiveObject.DataShapes = base.ActiveObject.DataShapes ?? new List<DataShape>();
		}

		// Token: 0x040002BF RID: 703
		private Dictionary<ExpressionNode, Calculation> m_calculationsByExpressionNode;
	}
}
