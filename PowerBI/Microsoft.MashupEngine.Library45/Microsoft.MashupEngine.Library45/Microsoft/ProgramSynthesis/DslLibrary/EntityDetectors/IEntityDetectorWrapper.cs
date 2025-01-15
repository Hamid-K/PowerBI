using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors
{
	// Token: 0x02000817 RID: 2071
	internal interface IEntityDetectorWrapper
	{
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002CA3 RID: 11427
		IEnumerable<EntityDetector> EntityDetectors { get; }
	}
}
