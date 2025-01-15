using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000668 RID: 1640
	internal sealed class ScopeRegion
	{
		// Token: 0x06004E21 RID: 20001 RVA: 0x00118927 File Offset: 0x00116B27
		internal ScopeRegion(ScopeManager scopeManager, int firstScopeIndex, int scopeRegionIndex)
		{
			this._scopeManager = scopeManager;
			this._firstScopeIndex = firstScopeIndex;
			this._scopeRegionIndex = scopeRegionIndex;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x0011895A File Offset: 0x00116B5A
		internal int FirstScopeIndex
		{
			get
			{
				return this._firstScopeIndex;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x00118962 File Offset: 0x00116B62
		internal int ScopeRegionIndex
		{
			get
			{
				return this._scopeRegionIndex;
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0011896A File Offset: 0x00116B6A
		internal bool ContainsScope(int scopeIndex)
		{
			return scopeIndex >= this._firstScopeIndex;
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x00118978 File Offset: 0x00116B78
		internal void EnterGroupOperation(DbExpressionBinding groupAggregateBinding)
		{
			this._groupAggregateBinding = groupAggregateBinding;
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00118981 File Offset: 0x00116B81
		internal void RollbackGroupOperation()
		{
			this._groupAggregateBinding = null;
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x0011898A File Offset: 0x00116B8A
		internal bool IsAggregating
		{
			get
			{
				return this._groupAggregateBinding != null;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x00118995 File Offset: 0x00116B95
		internal DbExpressionBinding GroupAggregateBinding
		{
			get
			{
				return this._groupAggregateBinding;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0011899D File Offset: 0x00116B9D
		internal List<GroupAggregateInfo> GroupAggregateInfos
		{
			get
			{
				return this._groupAggregateInfos;
			}
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x001189A5 File Offset: 0x00116BA5
		internal void RegisterGroupAggregateName(string groupAggregateName)
		{
			this._groupAggregateNames.Add(groupAggregateName);
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x001189B4 File Offset: 0x00116BB4
		internal bool ContainsGroupAggregate(string groupAggregateName)
		{
			return this._groupAggregateNames.Contains(groupAggregateName);
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x001189C2 File Offset: 0x00116BC2
		// (set) Token: 0x06004E2D RID: 20013 RVA: 0x001189CA File Offset: 0x00116BCA
		internal bool WasResolutionCorrelated { get; set; }

		// Token: 0x06004E2E RID: 20014 RVA: 0x001189D4 File Offset: 0x00116BD4
		internal void ApplyToScopeEntries(Action<ScopeEntry> action)
		{
			for (int i = this.FirstScopeIndex; i <= this._scopeManager.CurrentScopeIndex; i++)
			{
				foreach (KeyValuePair<string, ScopeEntry> keyValuePair in this._scopeManager.GetScopeByIndex(i))
				{
					action(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x00118A50 File Offset: 0x00116C50
		internal void ApplyToScopeEntries(Func<ScopeEntry, ScopeEntry> action)
		{
			for (int i = this.FirstScopeIndex; i <= this._scopeManager.CurrentScopeIndex; i++)
			{
				Scope scope = this._scopeManager.GetScopeByIndex(i);
				List<KeyValuePair<string, ScopeEntry>> list = null;
				foreach (KeyValuePair<string, ScopeEntry> keyValuePair in scope)
				{
					ScopeEntry scopeEntry = action(keyValuePair.Value);
					if (keyValuePair.Value != scopeEntry)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<string, ScopeEntry>>();
						}
						list.Add(new KeyValuePair<string, ScopeEntry>(keyValuePair.Key, scopeEntry));
					}
				}
				if (list != null)
				{
					list.Each(delegate(KeyValuePair<string, ScopeEntry> updatedScopeEntry)
					{
						scope.Replace(updatedScopeEntry.Key, updatedScopeEntry.Value);
					});
				}
			}
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x00118B24 File Offset: 0x00116D24
		internal void RollbackAllScopes()
		{
			this._scopeManager.RollbackToScope(this.FirstScopeIndex - 1);
		}

		// Token: 0x04001C60 RID: 7264
		private readonly ScopeManager _scopeManager;

		// Token: 0x04001C61 RID: 7265
		private readonly int _firstScopeIndex;

		// Token: 0x04001C62 RID: 7266
		private readonly int _scopeRegionIndex;

		// Token: 0x04001C63 RID: 7267
		private DbExpressionBinding _groupAggregateBinding;

		// Token: 0x04001C64 RID: 7268
		private readonly List<GroupAggregateInfo> _groupAggregateInfos = new List<GroupAggregateInfo>();

		// Token: 0x04001C65 RID: 7269
		private readonly HashSet<string> _groupAggregateNames = new HashSet<string>();
	}
}
