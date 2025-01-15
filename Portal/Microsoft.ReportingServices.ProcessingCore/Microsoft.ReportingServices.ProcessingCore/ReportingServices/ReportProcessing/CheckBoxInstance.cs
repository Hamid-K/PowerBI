using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200072A RID: 1834
	[Serializable]
	internal sealed class CheckBoxInstance : ReportItemInstance
	{
		// Token: 0x06006624 RID: 26148 RVA: 0x00190F83 File Offset: 0x0018F183
		internal CheckBoxInstance(ReportProcessing.ProcessingContext pc, CheckBox reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new CheckBoxInstanceInfo(pc, reportItemDef, this, index);
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x00190FA1 File Offset: 0x0018F1A1
		internal CheckBoxInstance()
		{
		}

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x06006626 RID: 26150 RVA: 0x00190FA9 File Offset: 0x0018F1A9
		internal CheckBoxInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (CheckBoxInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x00190FD8 File Offset: 0x0018F1D8
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstance, memberInfoList);
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x00190FF3 File Offset: 0x0018F1F3
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadCheckBoxInstanceInfo((CheckBox)this.m_reportItemDef);
		}
	}
}
