using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000863 RID: 2147
	public class RuntimeDataTablixGroupRootObjReference : RuntimeGroupRootObjReference, IReference<RuntimeDataTablixGroupRootObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007739 RID: 30521 RVA: 0x001ED7BD File Offset: 0x001EB9BD
		internal RuntimeDataTablixGroupRootObjReference()
		{
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x001ED7C5 File Offset: 0x001EB9C5
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObjReference;
		}

		// Token: 0x0600773B RID: 30523 RVA: 0x001ED7C9 File Offset: 0x001EB9C9
		[DebuggerStepThrough]
		public new RuntimeDataTablixGroupRootObj Value()
		{
			return (RuntimeDataTablixGroupRootObj)base.InternalValue();
		}
	}
}
