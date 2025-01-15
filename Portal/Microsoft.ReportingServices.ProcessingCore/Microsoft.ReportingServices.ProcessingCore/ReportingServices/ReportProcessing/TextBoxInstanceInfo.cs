using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000727 RID: 1831
	[Serializable]
	internal sealed class TextBoxInstanceInfo : ReportItemInstanceInfo, IShowHideSender
	{
		// Token: 0x06006604 RID: 26116 RVA: 0x00190CBC File Offset: 0x0018EEBC
		internal TextBoxInstanceInfo(ReportProcessing.ProcessingContext pc, TextBox reportItemDef, TextBoxInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x00190CC9 File Offset: 0x0018EEC9
		internal TextBoxInstanceInfo(TextBox reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06006606 RID: 26118 RVA: 0x00190CD2 File Offset: 0x0018EED2
		// (set) Token: 0x06006607 RID: 26119 RVA: 0x00190CDA File Offset: 0x0018EEDA
		internal string FormattedValue
		{
			get
			{
				return this.m_formattedValue;
			}
			set
			{
				this.m_formattedValue = value;
			}
		}

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x06006608 RID: 26120 RVA: 0x00190CE3 File Offset: 0x0018EEE3
		// (set) Token: 0x06006609 RID: 26121 RVA: 0x00190CEB File Offset: 0x0018EEEB
		internal object OriginalValue
		{
			get
			{
				return this.m_originalValue;
			}
			set
			{
				this.m_originalValue = value;
			}
		}

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x0600660A RID: 26122 RVA: 0x00190CF4 File Offset: 0x0018EEF4
		// (set) Token: 0x0600660B RID: 26123 RVA: 0x00190CFC File Offset: 0x0018EEFC
		internal bool Duplicate
		{
			get
			{
				return this.m_duplicate;
			}
			set
			{
				this.m_duplicate = value;
			}
		}

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x0600660C RID: 26124 RVA: 0x00190D05 File Offset: 0x0018EF05
		// (set) Token: 0x0600660D RID: 26125 RVA: 0x00190D0D File Offset: 0x0018EF0D
		internal ActionInstance Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x0600660E RID: 26126 RVA: 0x00190D16 File Offset: 0x0018EF16
		// (set) Token: 0x0600660F RID: 26127 RVA: 0x00190D1E File Offset: 0x0018EF1E
		internal bool InitialToggleState
		{
			get
			{
				return this.m_initialToggleState;
			}
			set
			{
				this.m_initialToggleState = value;
			}
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x00190D27 File Offset: 0x0018EF27
		void IShowHideSender.ProcessSender(ReportProcessing.ProcessingContext context, int uniqueName)
		{
			this.m_initialToggleState = context.ProcessSender(uniqueName, this.m_startHidden, (TextBox)this.m_reportItemDef);
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x00190D48 File Offset: 0x0018EF48
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.FormattedValue, Token.String),
				new MemberInfo(MemberName.OriginalValue, ObjectType.Variant),
				new MemberInfo(MemberName.Duplicate, Token.Boolean),
				new MemberInfo(MemberName.Action, ObjectType.ActionInstance),
				new MemberInfo(MemberName.InitialToggleState, Token.Boolean)
			});
		}

		// Token: 0x040032E1 RID: 13025
		private string m_formattedValue;

		// Token: 0x040032E2 RID: 13026
		private object m_originalValue;

		// Token: 0x040032E3 RID: 13027
		private bool m_duplicate;

		// Token: 0x040032E4 RID: 13028
		private ActionInstance m_action;

		// Token: 0x040032E5 RID: 13029
		private bool m_initialToggleState;
	}
}
