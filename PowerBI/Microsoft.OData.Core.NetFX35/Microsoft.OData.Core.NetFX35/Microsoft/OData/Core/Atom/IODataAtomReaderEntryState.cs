using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000027 RID: 39
	internal interface IODataAtomReaderEntryState
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015D RID: 349
		ODataEntry Entry { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015E RID: 350
		IEdmEntityType EntityType { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015F RID: 351
		// (set) Token: 0x06000160 RID: 352
		bool EntryElementEmpty { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000161 RID: 353
		// (set) Token: 0x06000162 RID: 354
		bool HasReadLink { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000163 RID: 355
		// (set) Token: 0x06000164 RID: 356
		bool HasEditLink { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000165 RID: 357
		// (set) Token: 0x06000166 RID: 358
		bool HasEditMediaLink { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000167 RID: 359
		// (set) Token: 0x06000168 RID: 360
		bool HasId { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000169 RID: 361
		// (set) Token: 0x0600016A RID: 362
		bool HasContent { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016B RID: 363
		// (set) Token: 0x0600016C RID: 364
		bool HasTypeNameCategory { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016D RID: 365
		// (set) Token: 0x0600016E RID: 366
		bool HasProperties { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x06000170 RID: 368
		bool? MediaLinkEntry { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000171 RID: 369
		// (set) Token: 0x06000172 RID: 370
		ODataAtomReaderNavigationLinkDescriptor FirstNavigationLinkDescriptor { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000173 RID: 371
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000174 RID: 372
		AtomEntryMetadata AtomEntryMetadata { get; }
	}
}
