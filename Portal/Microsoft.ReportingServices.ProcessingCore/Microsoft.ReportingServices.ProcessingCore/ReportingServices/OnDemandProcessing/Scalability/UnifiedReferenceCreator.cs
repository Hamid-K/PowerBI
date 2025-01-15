using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000857 RID: 2135
	internal sealed class UnifiedReferenceCreator : IReferenceCreator
	{
		// Token: 0x06007708 RID: 30472 RVA: 0x001EC597 File Offset: 0x001EA797
		internal UnifiedReferenceCreator(IReferenceCreator appReferenceCreator)
		{
			this.m_referenceCreators = new IReferenceCreator[2];
			this.m_referenceCreators[0] = CommonReferenceCreator.Instance;
			this.m_referenceCreators[1] = appReferenceCreator;
		}

		// Token: 0x06007709 RID: 30473 RVA: 0x001EC5C4 File Offset: 0x001EA7C4
		public bool TryCreateReference(IStorable refTarget, out BaseReference newReference)
		{
			bool flag = false;
			newReference = null;
			int num = 0;
			while (num < this.m_referenceCreators.Length && !flag)
			{
				flag = this.m_referenceCreators[num].TryCreateReference(refTarget, out newReference);
				num++;
			}
			return flag;
		}

		// Token: 0x0600770A RID: 30474 RVA: 0x001EC600 File Offset: 0x001EA800
		public bool TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType, out BaseReference newReference)
		{
			bool flag = false;
			newReference = null;
			int num = 0;
			while (num < this.m_referenceCreators.Length && !flag)
			{
				flag = this.m_referenceCreators[num].TryCreateReference(referenceObjectType, out newReference);
				num++;
			}
			return flag;
		}

		// Token: 0x04003C3F RID: 15423
		private IReferenceCreator[] m_referenceCreators;
	}
}
