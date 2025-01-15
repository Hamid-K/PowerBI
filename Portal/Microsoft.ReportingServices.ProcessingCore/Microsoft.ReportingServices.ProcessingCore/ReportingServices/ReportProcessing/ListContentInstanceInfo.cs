using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000737 RID: 1847
	[Serializable]
	internal sealed class ListContentInstanceInfo : InstanceInfo
	{
		// Token: 0x06006692 RID: 26258 RVA: 0x00191998 File Offset: 0x0018FB98
		internal ListContentInstanceInfo(ReportProcessing.ProcessingContext pc, ListContentInstance owner, List listDef)
		{
			if (pc.ShowHideType != Report.ShowHideTypes.None)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, listDef.Visibility, listDef.ExprHost, listDef.ObjectType, listDef.Name);
			}
			listDef.StartHidden = this.m_startHidden;
			if (listDef.Grouping != null)
			{
				if (listDef.Grouping.GroupLabel != null)
				{
					this.m_label = pc.NavigationInfo.RegisterLabel(pc.ReportRuntime.EvaluateGroupingLabelExpression(listDef.Grouping, listDef.ObjectType, listDef.Name));
				}
				if (listDef.Grouping.CustomProperties != null)
				{
					this.m_customPropertyInstances = listDef.Grouping.CustomProperties.EvaluateExpressions(listDef.ObjectType, listDef.Name, listDef.Grouping.Name + ".", pc);
				}
			}
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x00191A87 File Offset: 0x0018FC87
		internal ListContentInstanceInfo()
		{
		}

		// Token: 0x1700243F RID: 9279
		// (get) Token: 0x06006694 RID: 26260 RVA: 0x00191A8F File Offset: 0x0018FC8F
		// (set) Token: 0x06006695 RID: 26261 RVA: 0x00191A97 File Offset: 0x0018FC97
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

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x06006696 RID: 26262 RVA: 0x00191AA0 File Offset: 0x0018FCA0
		// (set) Token: 0x06006697 RID: 26263 RVA: 0x00191AA8 File Offset: 0x0018FCA8
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

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x00191AB1 File Offset: 0x0018FCB1
		// (set) Token: 0x06006699 RID: 26265 RVA: 0x00191AB9 File Offset: 0x0018FCB9
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

		// Token: 0x0600669A RID: 26266 RVA: 0x00191AC4 File Offset: 0x0018FCC4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.StartHidden, Token.Boolean),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x04003305 RID: 13061
		private bool m_startHidden;

		// Token: 0x04003306 RID: 13062
		private string m_label;

		// Token: 0x04003307 RID: 13063
		private DataValueInstanceList m_customPropertyInstances;
	}
}
