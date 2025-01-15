using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200072C RID: 1836
	[Serializable]
	internal sealed class ImageInstance : ReportItemInstance
	{
		// Token: 0x06006630 RID: 26160 RVA: 0x0019109C File Offset: 0x0018F29C
		internal ImageInstance(ReportProcessing.ProcessingContext pc, Image reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ImageInstanceInfo(pc, reportItemDef, this, index, false);
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x001910BB File Offset: 0x0018F2BB
		internal ImageInstance(ReportProcessing.ProcessingContext pc, Image reportItemDef, int index, bool customCreated)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ImageInstanceInfo(pc, reportItemDef, this, index, customCreated);
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x001910DB File Offset: 0x0018F2DB
		internal ImageInstance()
		{
		}

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x001910E3 File Offset: 0x0018F2E3
		internal ImageInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (ImageInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x00191110 File Offset: 0x0018F310
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstance, memberInfoList);
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x0019112B File Offset: 0x0018F32B
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadImageInstanceInfo((Image)this.m_reportItemDef);
		}
	}
}
