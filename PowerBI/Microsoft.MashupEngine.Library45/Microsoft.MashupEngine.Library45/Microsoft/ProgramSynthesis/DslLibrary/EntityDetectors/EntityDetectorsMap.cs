using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors
{
	// Token: 0x02000815 RID: 2069
	public sealed class EntityDetectorsMap
	{
		// Token: 0x06002C9D RID: 11421 RVA: 0x0007DB00 File Offset: 0x0007BD00
		public EntityDetectorsMap(IEnumerable<EntityDetector> entityDetectorObjects)
		{
			IReadOnlyDictionary<string, EntityDetector> readOnlyDictionary;
			if (entityDetectorObjects == null)
			{
				readOnlyDictionary = null;
			}
			else
			{
				readOnlyDictionary = entityDetectorObjects.Distinct<EntityDetector>().ToDictionary((EntityDetector detector) => detector.Name, (EntityDetector detector) => detector);
			}
			this.EmployedEntityDetectors = readOnlyDictionary;
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x0007DB68 File Offset: 0x0007BD68
		public IReadOnlyDictionary<string, EntityDetector> EmployedEntityDetectors { get; }
	}
}
