using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000743 RID: 1859
	[Serializable]
	internal sealed class TableGroupInstanceInfo : InstanceInfo
	{
		// Token: 0x06006746 RID: 26438 RVA: 0x00193740 File Offset: 0x00191940
		internal TableGroupInstanceInfo(ReportProcessing.ProcessingContext pc, TableGroup tableGroupDef, TableGroupInstance owner)
		{
			if (pc.ShowHideType != Report.ShowHideTypes.None)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, tableGroupDef.Visibility, tableGroupDef.ExprHost, tableGroupDef.DataRegionDef.ObjectType, tableGroupDef.DataRegionDef.Name);
			}
			tableGroupDef.StartHidden = this.m_startHidden;
			if (tableGroupDef.Grouping.GroupLabel != null)
			{
				this.m_label = pc.NavigationInfo.RegisterLabel(pc.ReportRuntime.EvaluateGroupingLabelExpression(tableGroupDef.Grouping, tableGroupDef.DataRegionDef.ObjectType, tableGroupDef.DataRegionDef.Name));
			}
			if (tableGroupDef.Grouping.CustomProperties != null)
			{
				this.m_customPropertyInstances = tableGroupDef.Grouping.CustomProperties.EvaluateExpressions(tableGroupDef.DataRegionDef.ObjectType, tableGroupDef.DataRegionDef.Name, tableGroupDef.Grouping.Name + ".", pc);
			}
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x00193842 File Offset: 0x00191A42
		internal TableGroupInstanceInfo()
		{
		}

		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x0019384A File Offset: 0x00191A4A
		// (set) Token: 0x06006749 RID: 26441 RVA: 0x00193852 File Offset: 0x00191A52
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x0019385B File Offset: 0x00191A5B
		// (set) Token: 0x0600674B RID: 26443 RVA: 0x00193863 File Offset: 0x00191A63
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x0600674C RID: 26444 RVA: 0x0019386C File Offset: 0x00191A6C
		// (set) Token: 0x0600674D RID: 26445 RVA: 0x00193874 File Offset: 0x00191A74
		internal DataValueInstanceList CustomPropertyInstances
		{
			get
			{
				return this.m_customPropertyInstances;
			}
			set
			{
				this.m_customPropertyInstances = value;
			}
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x00193880 File Offset: 0x00191A80
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.StartHidden, Token.Boolean),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x04003341 RID: 13121
		private bool m_startHidden;

		// Token: 0x04003342 RID: 13122
		private string m_label;

		// Token: 0x04003343 RID: 13123
		private DataValueInstanceList m_customPropertyInstances;
	}
}
