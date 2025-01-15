using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200072B RID: 1835
	[Serializable]
	internal sealed class CheckBoxInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006629 RID: 26153 RVA: 0x0019101E File Offset: 0x0018F21E
		internal CheckBoxInstanceInfo(ReportProcessing.ProcessingContext pc, CheckBox reportItemDef, ReportItemInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		// Token: 0x0600662A RID: 26154 RVA: 0x0019102B File Offset: 0x0018F22B
		internal CheckBoxInstanceInfo(CheckBox reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x00191034 File Offset: 0x0018F234
		// (set) Token: 0x0600662C RID: 26156 RVA: 0x0019103C File Offset: 0x0018F23C
		internal bool Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x00191045 File Offset: 0x0018F245
		// (set) Token: 0x0600662E RID: 26158 RVA: 0x0019104D File Offset: 0x0018F24D
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

		// Token: 0x0600662F RID: 26159 RVA: 0x00191058 File Offset: 0x0018F258
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.Value, Token.Boolean),
				new MemberInfo(MemberName.Duplicate, Token.Boolean)
			});
		}

		// Token: 0x040032E9 RID: 13033
		private bool m_value;

		// Token: 0x040032EA RID: 13034
		private bool m_duplicate;
	}
}
