using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000860 RID: 2144
	internal class RuntimeTablixGroupLeafObjReference : RuntimeDataTablixGroupLeafObjReference, IReference<RuntimeTablixGroupLeafObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<ISortDataHolder>
	{
		// Token: 0x0600772D RID: 30509 RVA: 0x001ED745 File Offset: 0x001EB945
		internal RuntimeTablixGroupLeafObjReference()
		{
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x001ED74D File Offset: 0x001EB94D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObjReference;
		}

		// Token: 0x0600772F RID: 30511 RVA: 0x001ED751 File Offset: 0x001EB951
		[DebuggerStepThrough]
		public new RuntimeTablixGroupLeafObj Value()
		{
			return (RuntimeTablixGroupLeafObj)base.InternalValue();
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x001ED75E File Offset: 0x001EB95E
		[DebuggerStepThrough]
		ISortDataHolder IReference<ISortDataHolder>.Value()
		{
			return (ISortDataHolder)base.InternalValue();
		}
	}
}
