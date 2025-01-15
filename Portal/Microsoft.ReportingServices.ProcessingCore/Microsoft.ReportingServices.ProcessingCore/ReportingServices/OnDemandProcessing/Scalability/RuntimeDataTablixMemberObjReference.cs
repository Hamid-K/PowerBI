using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000878 RID: 2168
	internal class RuntimeDataTablixMemberObjReference : RuntimeMemberObjReference, IReference<RuntimeDataTablixMemberObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007781 RID: 30593 RVA: 0x001EDA60 File Offset: 0x001EBC60
		internal RuntimeDataTablixMemberObjReference()
		{
		}

		// Token: 0x06007782 RID: 30594 RVA: 0x001EDA68 File Offset: 0x001EBC68
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObjReference;
		}

		// Token: 0x06007783 RID: 30595 RVA: 0x001EDA6C File Offset: 0x001EBC6C
		[DebuggerStepThrough]
		public RuntimeDataTablixMemberObj Value()
		{
			return (RuntimeDataTablixMemberObj)base.InternalValue();
		}
	}
}
