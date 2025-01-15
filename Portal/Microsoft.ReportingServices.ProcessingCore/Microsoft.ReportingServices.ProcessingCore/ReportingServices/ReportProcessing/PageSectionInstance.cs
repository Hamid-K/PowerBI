using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000754 RID: 1876
	[Serializable]
	internal sealed class PageSectionInstance : ReportItemInstance, IIndexInto
	{
		// Token: 0x0600681C RID: 26652 RVA: 0x00195987 File Offset: 0x00193B87
		internal PageSectionInstance(ReportProcessing.ProcessingContext pc, int pageNumber, PageSection reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new PageSectionInstanceInfo(pc, reportItemDef, this);
			this.m_pageNumber = pageNumber;
			this.m_reportItemColInstance = new ReportItemColInstance(pc, reportItemDef.ReportItems);
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x001959BD File Offset: 0x00193BBD
		internal PageSectionInstance()
		{
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x0600681E RID: 26654 RVA: 0x001959C5 File Offset: 0x00193BC5
		// (set) Token: 0x0600681F RID: 26655 RVA: 0x001959CD File Offset: 0x00193BCD
		internal int PageNumber
		{
			get
			{
				return this.m_pageNumber;
			}
			set
			{
				this.m_pageNumber = value;
			}
		}

		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x001959D6 File Offset: 0x00193BD6
		// (set) Token: 0x06006821 RID: 26657 RVA: 0x001959DE File Offset: 0x00193BDE
		internal ReportItemColInstance ReportItemColInstance
		{
			get
			{
				return this.m_reportItemColInstance;
			}
			set
			{
				this.m_reportItemColInstance = value;
			}
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x001959E7 File Offset: 0x00193BE7
		object IIndexInto.GetChildAt(int index, out NonComputedUniqueNames nonCompNames)
		{
			return ((IIndexInto)this.m_reportItemColInstance).GetChildAt(index, out nonCompNames);
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x001959F6 File Offset: 0x00193BF6
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadPageSectionInstanceInfo((PageSection)this.m_reportItemDef);
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x00195A24 File Offset: 0x00193C24
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.PageNumber, Token.Int32),
				new MemberInfo(MemberName.ReportItemColInstance, ObjectType.ReportItemColInstance)
			});
		}

		// Token: 0x04003384 RID: 13188
		private int m_pageNumber;

		// Token: 0x04003385 RID: 13189
		private ReportItemColInstance m_reportItemColInstance;
	}
}
