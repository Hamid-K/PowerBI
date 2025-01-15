using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D0 RID: 2256
	[PersistedWithinRequestOnly]
	internal abstract class BTreeNodeValue : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007B86 RID: 31622
		internal abstract void AddRow(IHierarchyObj ownerRef);

		// Token: 0x06007B87 RID: 31623
		internal abstract void Traverse(ProcessingStages operation, ITraversalContext traversalContext);

		// Token: 0x06007B88 RID: 31624 RVA: 0x001FC1F5 File Offset: 0x001FA3F5
		internal int CompareTo(object keyValue, OnDemandProcessingContext odpContext)
		{
			return odpContext.ProcessingComparer.Compare(this.Key, keyValue);
		}

		// Token: 0x17002883 RID: 10371
		// (get) Token: 0x06007B89 RID: 31625
		protected abstract object Key { get; }

		// Token: 0x06007B8A RID: 31626
		public abstract void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer);

		// Token: 0x06007B8B RID: 31627
		public abstract void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader);

		// Token: 0x06007B8C RID: 31628
		public abstract void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems);

		// Token: 0x06007B8D RID: 31629
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();

		// Token: 0x17002884 RID: 10372
		// (get) Token: 0x06007B8E RID: 31630
		public abstract int Size { get; }
	}
}
