using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000869 RID: 2153
	internal class RuntimeTablixCellReference : RuntimeCellReference, IReference<RuntimeTablixCell>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007753 RID: 30547 RVA: 0x001ED8C7 File Offset: 0x001EBAC7
		internal RuntimeTablixCellReference()
		{
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x001ED8CF File Offset: 0x001EBACF
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCellReference;
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x001ED8D3 File Offset: 0x001EBAD3
		[DebuggerStepThrough]
		public new RuntimeTablixCell Value()
		{
			return (RuntimeTablixCell)base.InternalValue();
		}
	}
}
