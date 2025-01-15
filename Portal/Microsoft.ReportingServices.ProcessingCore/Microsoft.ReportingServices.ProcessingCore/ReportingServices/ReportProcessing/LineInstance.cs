using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000723 RID: 1827
	[Serializable]
	internal sealed class LineInstance : ReportItemInstance
	{
		// Token: 0x060065EE RID: 26094 RVA: 0x00190977 File Offset: 0x0018EB77
		internal LineInstance(ReportProcessing.ProcessingContext pc, Line reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new LineInstanceInfo(pc, reportItemDef, this, index);
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x00190995 File Offset: 0x0018EB95
		internal LineInstance()
		{
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x001909A0 File Offset: 0x0018EBA0
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstance, memberInfoList);
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x001909BB File Offset: 0x0018EBBB
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadLineInstanceInfo((Line)this.m_reportItemDef);
		}
	}
}
