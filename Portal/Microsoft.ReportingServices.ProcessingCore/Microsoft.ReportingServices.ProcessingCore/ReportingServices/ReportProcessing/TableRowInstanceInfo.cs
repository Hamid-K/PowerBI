using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000748 RID: 1864
	[Serializable]
	internal sealed class TableRowInstanceInfo : InstanceInfo
	{
		// Token: 0x06006775 RID: 26485 RVA: 0x00193D90 File Offset: 0x00191F90
		internal TableRowInstanceInfo(ReportProcessing.ProcessingContext pc, TableRow rowDef, TableRowInstance owner, Table tableDef, IndexedExprHost rowVisibilityHiddenExprHost)
		{
			if (pc.ShowHideType != Report.ShowHideTypes.None)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, rowDef.Visibility, rowVisibilityHiddenExprHost, tableDef.ObjectType, tableDef.Name);
			}
			rowDef.StartHidden = this.m_startHidden;
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x00193DF2 File Offset: 0x00191FF2
		internal TableRowInstanceInfo()
		{
		}

		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x06006777 RID: 26487 RVA: 0x00193DFA File Offset: 0x00191FFA
		// (set) Token: 0x06006778 RID: 26488 RVA: 0x00193E02 File Offset: 0x00192002
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

		// Token: 0x06006779 RID: 26489 RVA: 0x00193E0C File Offset: 0x0019200C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.StartHidden, Token.Boolean)
			});
		}

		// Token: 0x0400334D RID: 13133
		private bool m_startHidden;
	}
}
