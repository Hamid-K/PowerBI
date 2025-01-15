using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200085E RID: 2142
	public class RuntimeGroupLeafObjReference : RuntimeGroupObjReference, IReference<RuntimeGroupLeafObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007724 RID: 30500 RVA: 0x001ED6E6 File Offset: 0x001EB8E6
		internal RuntimeGroupLeafObjReference()
		{
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x001ED6EE File Offset: 0x001EB8EE
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference;
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x001ED6F5 File Offset: 0x001EB8F5
		[DebuggerStepThrough]
		public new RuntimeGroupLeafObj Value()
		{
			return (RuntimeGroupLeafObj)base.InternalValue();
		}
	}
}
