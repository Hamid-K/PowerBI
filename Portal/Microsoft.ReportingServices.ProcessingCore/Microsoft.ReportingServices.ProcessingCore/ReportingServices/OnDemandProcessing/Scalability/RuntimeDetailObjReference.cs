using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000864 RID: 2148
	internal class RuntimeDetailObjReference : RuntimeHierarchyObjReference, IReference<RuntimeDetailObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600773C RID: 30524 RVA: 0x001ED7D6 File Offset: 0x001EB9D6
		internal RuntimeDetailObjReference()
		{
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x001ED7DE File Offset: 0x001EB9DE
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDetailObjReference;
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x001ED7E5 File Offset: 0x001EB9E5
		[DebuggerStepThrough]
		public new RuntimeDetailObj Value()
		{
			return (RuntimeDetailObj)base.InternalValue();
		}
	}
}
