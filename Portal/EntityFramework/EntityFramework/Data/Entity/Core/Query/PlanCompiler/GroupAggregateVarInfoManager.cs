using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000345 RID: 837
	internal class GroupAggregateVarInfoManager
	{
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x00075D33 File Offset: 0x00073F33
		internal IEnumerable<GroupAggregateVarInfo> GroupAggregateVarInfos
		{
			get
			{
				return this._groupAggregateVarInfos;
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x00075D3B File Offset: 0x00073F3B
		internal void Add(Var var, GroupAggregateVarInfo groupAggregateVarInfo, Node computationTemplate, bool isUnnested)
		{
			this._groupAggregateVarRelatedVarToInfo.Add(var, new GroupAggregateVarRefInfo(groupAggregateVarInfo, computationTemplate, isUnnested));
			this._groupAggregateVarInfos.Add(groupAggregateVarInfo);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x00075D60 File Offset: 0x00073F60
		internal void Add(Var var, GroupAggregateVarInfo groupAggregateVarInfo, Node computationTemplate, bool isUnnested, EdmMember property)
		{
			if (property == null)
			{
				this.Add(var, groupAggregateVarInfo, computationTemplate, isUnnested);
				return;
			}
			if (this._groupAggregateVarRelatedVarPropertyToInfo == null)
			{
				this._groupAggregateVarRelatedVarPropertyToInfo = new Dictionary<Var, Dictionary<EdmMember, GroupAggregateVarRefInfo>>();
			}
			Dictionary<EdmMember, GroupAggregateVarRefInfo> dictionary;
			if (!this._groupAggregateVarRelatedVarPropertyToInfo.TryGetValue(var, out dictionary))
			{
				dictionary = new Dictionary<EdmMember, GroupAggregateVarRefInfo>();
				this._groupAggregateVarRelatedVarPropertyToInfo.Add(var, dictionary);
			}
			dictionary.Add(property, new GroupAggregateVarRefInfo(groupAggregateVarInfo, computationTemplate, isUnnested));
			this._groupAggregateVarInfos.Add(groupAggregateVarInfo);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x00075DD1 File Offset: 0x00073FD1
		internal bool TryGetReferencedGroupAggregateVarInfo(Var var, out GroupAggregateVarRefInfo groupAggregateVarRefInfo)
		{
			return this._groupAggregateVarRelatedVarToInfo.TryGetValue(var, out groupAggregateVarRefInfo);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x00075DE0 File Offset: 0x00073FE0
		internal bool TryGetReferencedGroupAggregateVarInfo(Var var, EdmMember property, out GroupAggregateVarRefInfo groupAggregateVarRefInfo)
		{
			if (property == null)
			{
				return this.TryGetReferencedGroupAggregateVarInfo(var, out groupAggregateVarRefInfo);
			}
			Dictionary<EdmMember, GroupAggregateVarRefInfo> dictionary;
			if (this._groupAggregateVarRelatedVarPropertyToInfo == null || !this._groupAggregateVarRelatedVarPropertyToInfo.TryGetValue(var, out dictionary))
			{
				groupAggregateVarRefInfo = null;
				return false;
			}
			return dictionary.TryGetValue(property, out groupAggregateVarRefInfo);
		}

		// Token: 0x04000DEB RID: 3563
		private readonly Dictionary<Var, GroupAggregateVarRefInfo> _groupAggregateVarRelatedVarToInfo = new Dictionary<Var, GroupAggregateVarRefInfo>();

		// Token: 0x04000DEC RID: 3564
		private Dictionary<Var, Dictionary<EdmMember, GroupAggregateVarRefInfo>> _groupAggregateVarRelatedVarPropertyToInfo;

		// Token: 0x04000DED RID: 3565
		private readonly HashSet<GroupAggregateVarInfo> _groupAggregateVarInfos = new HashSet<GroupAggregateVarInfo>();
	}
}
