using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000891 RID: 2193
	internal static class ScalabilityUtils
	{
		// Token: 0x06007828 RID: 30760 RVA: 0x001EF114 File Offset: 0x001ED314
		public static IScalabilityCache CreateCacheForTransientAllocations(CreateAndRegisterStream createStreamCallback, string streamNamePrefix, IScalabilityObjectCreator objectCreator, IReferenceCreator referenceCreator, ComponentType componentType, int minReservedMemoryMB)
		{
			int num = 0;
			ISpaceManager spaceManager = new PromoteLocalitySpaceManager(52428800L);
			IStorage storage = new RIFStorage(new CreateAndRegisterStreamHandler(streamNamePrefix + "_Data", createStreamCallback), 4096, 200, 500, spaceManager, objectCreator, referenceCreator, null, false, num);
			IIndexStrategy indexStrategy = new IndexTable(new CreateAndRegisterStreamHandler(streamNamePrefix + "_Index", createStreamCallback), 1024, 100);
			return new ScalabilityCache(storage, indexStrategy, componentType, (long)(minReservedMemoryMB * 1048576));
		}
	}
}
