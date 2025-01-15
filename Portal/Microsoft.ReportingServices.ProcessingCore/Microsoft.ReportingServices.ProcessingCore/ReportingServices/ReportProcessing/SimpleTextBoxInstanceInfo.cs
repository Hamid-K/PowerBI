using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000726 RID: 1830
	[Serializable]
	internal sealed class SimpleTextBoxInstanceInfo : InstanceInfo
	{
		// Token: 0x060065FC RID: 26108 RVA: 0x00190BE4 File Offset: 0x0018EDE4
		internal SimpleTextBoxInstanceInfo(ReportProcessing.ProcessingContext pc, TextBox reportItemDef, TextBoxInstance owner, int index)
		{
			this.m_reportItemDef = reportItemDef;
			ReportProcessing.RuntimeRICollection.ResetSubtotalReferences(pc);
			if (pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
			{
				pc.ChunkManager.AddInstance(this, reportItemDef, owner, index, pc.InPageSection);
			}
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x00190C1F File Offset: 0x0018EE1F
		internal SimpleTextBoxInstanceInfo(TextBox reportItemDef)
		{
			this.m_reportItemDef = reportItemDef;
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x00190C2E File Offset: 0x0018EE2E
		internal SimpleTextBoxInstanceInfo(TextBox reportItemDef, TextBoxInstanceInfo instanceInfo)
		{
			this.m_reportItemDef = reportItemDef;
			this.m_originalValue = instanceInfo.OriginalValue;
			this.m_formattedValue = instanceInfo.FormattedValue;
		}

		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x00190C55 File Offset: 0x0018EE55
		// (set) Token: 0x06006600 RID: 26112 RVA: 0x00190C5D File Offset: 0x0018EE5D
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

		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06006601 RID: 26113 RVA: 0x00190C66 File Offset: 0x0018EE66
		// (set) Token: 0x06006602 RID: 26114 RVA: 0x00190C6E File Offset: 0x0018EE6E
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

		// Token: 0x06006603 RID: 26115 RVA: 0x00190C78 File Offset: 0x0018EE78
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.FormattedValue, Token.String),
				new MemberInfo(MemberName.OriginalValue, ObjectType.Variant)
			});
		}

		// Token: 0x040032DE RID: 13022
		private string m_formattedValue;

		// Token: 0x040032DF RID: 13023
		private object m_originalValue;

		// Token: 0x040032E0 RID: 13024
		[NonSerialized]
		private ReportItem m_reportItemDef;
	}
}
