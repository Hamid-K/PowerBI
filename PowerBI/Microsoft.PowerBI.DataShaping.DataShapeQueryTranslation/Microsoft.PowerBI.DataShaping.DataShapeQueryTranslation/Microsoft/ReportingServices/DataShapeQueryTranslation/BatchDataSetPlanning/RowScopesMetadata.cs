using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A4 RID: 420
	internal sealed class RowScopesMetadata : IEquatable<RowScopesMetadata>
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003BD52 File Offset: 0x00039F52
		internal RowScopesMetadata(IReadOnlyList<IScope> rowScopes)
		{
			this.Scopes = rowScopes;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0003BD61 File Offset: 0x00039F61
		internal IReadOnlyList<IScope> Scopes { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003BD69 File Offset: 0x00039F69
		internal IScope InnermostScope
		{
			get
			{
				return this.Scopes[this.Scopes.Count - 1];
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003BD83 File Offset: 0x00039F83
		internal IScope OutermostScope
		{
			get
			{
				return this.Scopes[0];
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003BD91 File Offset: 0x00039F91
		internal bool IsInnermostScope(ScopeTree scopeTree, IScope scope)
		{
			return scopeTree.AreSameScope(this.InnermostScope, scope);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		internal bool IsSameWithAny(ScopeTree scopeTree, IScope innerScope)
		{
			return scopeTree.IsSameWithAny(this.Scopes, innerScope);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003BDAF File Offset: 0x00039FAF
		internal bool IsPrefixOfScopes(RowScopesMetadata prefixScopes)
		{
			return this.IsPrefixOfScopes(prefixScopes.Scopes);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003BDC0 File Offset: 0x00039FC0
		internal bool IsPrefixOfScopes(IReadOnlyList<IScope> prefixScopes)
		{
			if (prefixScopes.Count > this.Scopes.Count)
			{
				return false;
			}
			for (int i = 0; i < prefixScopes.Count; i++)
			{
				if (!this.Scopes[i].Equals(prefixScopes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003BE10 File Offset: 0x0003A010
		internal IReadOnlyList<DataMember> GetAllGroups()
		{
			if (this.m_groups == null)
			{
				this.m_groups = (from dm in this.Scopes.OfType<DataMember>()
					where dm.IsDynamic
					select dm).ToReadOnlyList<DataMember>();
			}
			return this.m_groups;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003BE65 File Offset: 0x0003A065
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash<string>(this.Scopes.Select((IScope s) => s.Id.Value).ToList<string>(), null);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003BE9C File Offset: 0x0003A09C
		public override bool Equals(object obj)
		{
			IReadOnlyList<IScope> readOnlyList = obj as IReadOnlyList<IScope>;
			if (readOnlyList != null)
			{
				return this.Scopes.SequenceEqual(readOnlyList, ReferenceEqualityComparer<IScope>.Instance);
			}
			return this.Equals(obj as RowScopesMetadata);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003BED1 File Offset: 0x0003A0D1
		public bool Equals(RowScopesMetadata other)
		{
			return other != null && this.Scopes.SequenceEqual(other.Scopes, ReferenceEqualityComparer<IScope>.Instance);
		}

		// Token: 0x040006FE RID: 1790
		private IReadOnlyList<DataMember> m_groups;
	}
}
