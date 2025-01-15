using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000745 RID: 1861
	[Serializable]
	internal sealed class TableDetailInstanceInfo : InstanceInfo
	{
		// Token: 0x0600675C RID: 26460 RVA: 0x00193AB8 File Offset: 0x00191CB8
		internal TableDetailInstanceInfo(ReportProcessing.ProcessingContext pc, TableDetail tableDetailDef, TableDetailInstance owner, Table tableDef)
		{
			if (pc.ShowHideType != Report.ShowHideTypes.None)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, tableDetailDef.Visibility, tableDetailDef.ExprHost, tableDef.ObjectType, tableDef.Name);
			}
			tableDetailDef.StartHidden = this.m_startHidden;
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x00193B1E File Offset: 0x00191D1E
		internal TableDetailInstanceInfo()
		{
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x0600675E RID: 26462 RVA: 0x00193B26 File Offset: 0x00191D26
		// (set) Token: 0x0600675F RID: 26463 RVA: 0x00193B2E File Offset: 0x00191D2E
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

		// Token: 0x06006760 RID: 26464 RVA: 0x00193B38 File Offset: 0x00191D38
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.StartHidden, Token.Boolean)
			});
		}

		// Token: 0x04003347 RID: 13127
		private bool m_startHidden;
	}
}
