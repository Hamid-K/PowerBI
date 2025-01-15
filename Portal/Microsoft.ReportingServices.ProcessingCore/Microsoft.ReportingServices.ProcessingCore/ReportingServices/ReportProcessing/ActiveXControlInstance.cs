using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000732 RID: 1842
	[Serializable]
	internal sealed class ActiveXControlInstance : ReportItemInstance
	{
		// Token: 0x06006666 RID: 26214 RVA: 0x001914FA File Offset: 0x0018F6FA
		internal ActiveXControlInstance(ReportProcessing.ProcessingContext pc, ActiveXControl reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ActiveXControlInstanceInfo(pc, reportItemDef, this, index);
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x00191518 File Offset: 0x0018F718
		internal ActiveXControlInstance()
		{
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x00191520 File Offset: 0x0018F720
		internal ActiveXControlInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (ActiveXControlInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x0019154C File Offset: 0x0018F74C
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstance, memberInfoList);
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x00191567 File Offset: 0x0018F767
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadActiveXControlInstanceInfo((ActiveXControl)this.m_reportItemDef);
		}
	}
}
