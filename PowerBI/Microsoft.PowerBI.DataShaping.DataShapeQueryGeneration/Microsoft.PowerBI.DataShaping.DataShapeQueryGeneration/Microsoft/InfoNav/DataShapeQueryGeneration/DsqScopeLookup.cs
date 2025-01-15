using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000065 RID: 101
	internal sealed class DsqScopeLookup
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001125C File Offset: 0x0000F45C
		private Dictionary<QueryMember, DataMemberBuilder> PrimaryQueryMemberToDsqMember
		{
			get
			{
				return Util.GetLazyDictionary<QueryMember, DataMemberBuilder>(ref this._primaryQueryGroupToDsqMember, null);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001126A File Offset: 0x0000F46A
		private Dictionary<QueryMember, DataMemberBuilder> SecondaryQueryMemberToDsqMember
		{
			get
			{
				return Util.GetLazyDictionary<QueryMember, DataMemberBuilder>(ref this._secondaryQueryGroupToDsqMember, null);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00011278 File Offset: 0x0000F478
		private Dictionary<DsqScopeLookup.IntersectionKey, Identifier> DsqIntersectionScopeMapping
		{
			get
			{
				return Util.GetLazyDictionary<DsqScopeLookup.IntersectionKey, Identifier>(ref this._dsqIntersectionScopeMapping, null);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00011286 File Offset: 0x0000F486
		internal void AddQueryMemberToDsqMemberMapping(QueryMember queryMember, DataMemberBuilder dsqMember, bool isPrimary)
		{
			if (isPrimary)
			{
				Util.AddToLazyDictionary<QueryMember, DataMemberBuilder>(ref this._primaryQueryGroupToDsqMember, queryMember, dsqMember, null);
				return;
			}
			Util.AddToLazyDictionary<QueryMember, DataMemberBuilder>(ref this._secondaryQueryGroupToDsqMember, queryMember, dsqMember, null);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000112A8 File Offset: 0x0000F4A8
		internal bool TryGetGroupScope(QueryMember queryMember, bool isPrimary, out DataMemberBuilder member)
		{
			if (isPrimary)
			{
				return this.PrimaryQueryMemberToDsqMember.TryGetValue(queryMember, out member);
			}
			return this.SecondaryQueryMemberToDsqMember.TryGetValue(queryMember, out member);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000112C8 File Offset: 0x0000F4C8
		internal void AddIntersectionScopeMapping(Identifier primaryParent, Identifier secondaryParent, Identifier intersection)
		{
			Util.AddToLazyDictionary<DsqScopeLookup.IntersectionKey, Identifier>(ref this._dsqIntersectionScopeMapping, new DsqScopeLookup.IntersectionKey(primaryParent, secondaryParent), intersection, null);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000112DE File Offset: 0x0000F4DE
		internal bool TryGetIntersectionScope(DataMemberBuilder primaryMember, DataMemberBuilder secondaryMember, out Identifier intersectionScope)
		{
			return this.DsqIntersectionScopeMapping.TryGetValue(new DsqScopeLookup.IntersectionKey(primaryMember.Result.Id, secondaryMember.Result.Id), out intersectionScope);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00011307 File Offset: 0x0000F507
		internal void SetInnermostScopeId(Identifier innerScopeId)
		{
			this._innermostScopeId = innerScopeId;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00011310 File Offset: 0x0000F510
		internal bool TryGetInnermostScopeId(out Identifier innerScope)
		{
			innerScope = this._innermostScopeId;
			return this._innermostScopeId != null;
		}

		// Token: 0x04000284 RID: 644
		private Dictionary<QueryMember, DataMemberBuilder> _primaryQueryGroupToDsqMember;

		// Token: 0x04000285 RID: 645
		private Dictionary<QueryMember, DataMemberBuilder> _secondaryQueryGroupToDsqMember;

		// Token: 0x04000286 RID: 646
		private Dictionary<DsqScopeLookup.IntersectionKey, Identifier> _dsqIntersectionScopeMapping;

		// Token: 0x04000287 RID: 647
		private Identifier _innermostScopeId;

		// Token: 0x02000134 RID: 308
		private sealed class IntersectionKey : IEquatable<DsqScopeLookup.IntersectionKey>
		{
			// Token: 0x06000975 RID: 2421 RVA: 0x00025723 File Offset: 0x00023923
			internal IntersectionKey(Identifier primary, Identifier secondary)
			{
				this._primary = primary;
				this._secondary = secondary;
			}

			// Token: 0x06000976 RID: 2422 RVA: 0x00025739 File Offset: 0x00023939
			public override bool Equals(object obj)
			{
				return this.Equals(obj as DsqScopeLookup.IntersectionKey);
			}

			// Token: 0x06000977 RID: 2423 RVA: 0x00025747 File Offset: 0x00023947
			public bool Equals(DsqScopeLookup.IntersectionKey key)
			{
				return key._primary.Equals(this._primary) && key._secondary.Equals(this._secondary);
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x0002576F File Offset: 0x0002396F
			public override int GetHashCode()
			{
				return Hashing.CombineHash(this._primary.GetHashCode(), this._secondary.GetHashCode());
			}

			// Token: 0x040004F1 RID: 1265
			private readonly Identifier _primary;

			// Token: 0x040004F2 RID: 1266
			private readonly Identifier _secondary;
		}
	}
}
