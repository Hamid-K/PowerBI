using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000EC RID: 236
	internal sealed class GroupBuilder<TParent> : BuilderBase<Group, TParent>
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x0000E210 File Offset: 0x0000C410
		internal GroupBuilder(TParent parent, Group activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000E21C File Offset: 0x0000C41C
		public GroupBuilder<TParent> WithGroupKey(Expression groupExpr, Candidate<bool> showItemsWithNoData = null, Identifier groupKeyIdentifier = null)
		{
			Group activeObject = base.ActiveObject;
			List<GroupKey> list = activeObject.GroupKeys;
			if (list == null)
			{
				list = new List<GroupKey>();
				activeObject.GroupKeys = list;
			}
			list.Add(new GroupKey
			{
				Value = groupExpr,
				ShowItemsWithNoData = showItemsWithNoData,
				Id = groupKeyIdentifier
			});
			return this;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000E268 File Offset: 0x0000C468
		public GroupBuilder<TParent> WithSortKey(Expression sortExpr, SortDirection sortDirection = SortDirection.Ascending, Identifier sortKeyIdentifier = null)
		{
			Group activeObject = base.ActiveObject;
			List<SortKey> list = activeObject.SortKeys;
			if (list == null)
			{
				list = new List<SortKey>();
				activeObject.SortKeys = list;
			}
			list.Add(new SortKey
			{
				Value = sortExpr,
				SortDirection = sortDirection,
				Id = sortKeyIdentifier
			});
			return this;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public GroupBuilder<TParent> WithScopeValueDefinition(Expression scopeValueDefExpr, Identifier scopeValueDefinitionIdentifier = null)
		{
			Group activeObject = base.ActiveObject;
			ScopeIdDefinition scopeIdDefinition = activeObject.ScopeIdDefinition;
			if (scopeIdDefinition == null)
			{
				scopeIdDefinition = new ScopeIdDefinition
				{
					Values = new List<ScopeValueDefinition>()
				};
				activeObject.ScopeIdDefinition = scopeIdDefinition;
			}
			scopeIdDefinition.Values.Add(new ScopeValueDefinition
			{
				Id = scopeValueDefinitionIdentifier,
				Value = scopeValueDefExpr
			});
			return this;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000E314 File Offset: 0x0000C514
		public GroupBuilder<TParent> WithDetailGroupIdentity(Expression expr, Identifier id = null)
		{
			Group activeObject = base.ActiveObject;
			if (activeObject.DetailGroupIdentity == null)
			{
				DetailGroupIdentity detailGroupIdentity = new DetailGroupIdentity
				{
					Id = id,
					Value = expr
				};
				activeObject.DetailGroupIdentity = detailGroupIdentity;
			}
			return this;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000E34C File Offset: 0x0000C54C
		public GroupBuilder<TParent> WithStartPosition(ScalarValue? startAtValue = null)
		{
			return this.WithStartPosition((startAtValue == null) ? null : Candidate<ScalarValue>.Valid(startAtValue.Value));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000E36C File Offset: 0x0000C56C
		public GroupBuilder<TParent> WithStartPosition(Candidate<ScalarValue> startAtValue = null)
		{
			Group activeObject = base.ActiveObject;
			if (startAtValue != null)
			{
				ScopeId scopeId = activeObject.StartPosition;
				if (scopeId == null)
				{
					scopeId = new ScopeId
					{
						Values = new List<ScopeValue>()
					};
					activeObject.StartPosition = scopeId;
				}
				ScopeValue scopeValue = new ScopeValue
				{
					Value = startAtValue
				};
				scopeId.Values.Add(scopeValue);
			}
			return this;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000E3C5 File Offset: 0x0000C5C5
		public GroupBuilder<TParent> WithGroupAndSortKey(Expression groupExpr, SortDirection sortDirection = SortDirection.Ascending, Candidate<bool> showItemsWithNoData = null, Identifier groupKeyIdentifier = null, ScalarValue? startAtValue = null, bool suppressScopeValue = false)
		{
			this.WithGroupKey(groupExpr, showItemsWithNoData, groupKeyIdentifier);
			this.WithSortKey(base.Clone(groupExpr), sortDirection, null);
			if (!suppressScopeValue)
			{
				this.WithScopeValueDefinition(base.Clone(groupExpr), null);
			}
			this.WithStartPosition(startAtValue);
			return this;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000E3FF File Offset: 0x0000C5FF
		public GroupBuilder<TParent> WithSortKeyAndScopeValue(Expression sortExpression, SortDirection sortDirection = SortDirection.Ascending, ScalarValue? startAtValue = null)
		{
			this.WithSortKey(sortExpression, sortDirection, null);
			this.WithScopeValueDefinition(base.Clone(sortExpression), null);
			this.WithStartPosition(startAtValue);
			return this;
		}
	}
}
