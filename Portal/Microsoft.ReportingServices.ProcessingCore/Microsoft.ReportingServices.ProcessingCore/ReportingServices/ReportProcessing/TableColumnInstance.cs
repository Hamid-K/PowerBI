using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000746 RID: 1862
	[Serializable]
	internal sealed class TableColumnInstance
	{
		// Token: 0x06006761 RID: 26465 RVA: 0x00193B6C File Offset: 0x00191D6C
		internal TableColumnInstance(ReportProcessing.ProcessingContext pc, TableColumn tableColumnDef, Table tableDef)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			if (pc.ShowHideType != Report.ShowHideTypes.None)
			{
				this.m_startHidden = pc.ProcessReceiver(this.m_uniqueName, tableColumnDef.Visibility, (tableDef.TableExprHost != null) ? tableDef.TableExprHost.TableColumnVisibilityHiddenExpressions : null, tableDef.ObjectType, tableDef.Name);
			}
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x00193BCD File Offset: 0x00191DCD
		internal TableColumnInstance()
		{
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x06006763 RID: 26467 RVA: 0x00193BD5 File Offset: 0x00191DD5
		// (set) Token: 0x06006764 RID: 26468 RVA: 0x00193BDD File Offset: 0x00191DDD
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x06006765 RID: 26469 RVA: 0x00193BE6 File Offset: 0x00191DE6
		// (set) Token: 0x06006766 RID: 26470 RVA: 0x00193BEE File Offset: 0x00191DEE
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

		// Token: 0x06006767 RID: 26471 RVA: 0x00193BF8 File Offset: 0x00191DF8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.StartHidden, Token.Boolean)
			});
		}

		// Token: 0x04003348 RID: 13128
		private int m_uniqueName;

		// Token: 0x04003349 RID: 13129
		private bool m_startHidden;
	}
}
