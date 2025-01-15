using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000894 RID: 2196
	internal sealed class ScalableDictionaryNodeReference : Reference<ScalableDictionaryNode>, IScalableDictionaryEntry, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007856 RID: 30806 RVA: 0x001F00F9 File Offset: 0x001EE2F9
		internal ScalableDictionaryNodeReference()
		{
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x001F0101 File Offset: 0x001EE301
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference;
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x001F0105 File Offset: 0x001EE305
		public ScalableDictionaryNode Value()
		{
			return ((IReference<ScalableDictionaryNode>)this).Value();
		}
	}
}
