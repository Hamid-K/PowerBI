using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200084F RID: 2127
	internal class SimpleReference<T> : Reference<T> where T : IStorable
	{
		// Token: 0x060076B4 RID: 30388 RVA: 0x001EB96C File Offset: 0x001E9B6C
		internal SimpleReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceType)
		{
			this.m_objectType = referenceType;
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x001EB97B File Offset: 0x001E9B7B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return this.m_objectType;
		}

		// Token: 0x04003C12 RID: 15378
		[NonSerialized]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType m_objectType;
	}
}
