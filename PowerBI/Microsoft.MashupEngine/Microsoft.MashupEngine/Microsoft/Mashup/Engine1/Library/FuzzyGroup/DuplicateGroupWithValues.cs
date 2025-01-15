using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B5E RID: 2910
	internal class DuplicateGroupWithValues
	{
		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x0010DA28 File Offset: 0x0010BC28
		public RecordValue[] DuplicateRecords { get; }

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x0010DA30 File Offset: 0x0010BC30
		public RecordValue RepresentativeRecord { get; }

		// Token: 0x0600508C RID: 20620 RVA: 0x0010DA38 File Offset: 0x0010BC38
		public DuplicateGroupWithValues(RecordValue representativeRecord, RecordValue[] duplicateRecords)
		{
			this.RepresentativeRecord = representativeRecord;
			this.DuplicateRecords = duplicateRecords;
		}
	}
}
