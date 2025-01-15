using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000218 RID: 536
	internal interface IODataAtomReaderEntryState
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000FB2 RID: 4018
		ODataEntry Entry { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000FB3 RID: 4019
		IEdmEntityType EntityType { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000FB4 RID: 4020
		// (set) Token: 0x06000FB5 RID: 4021
		bool EntryElementEmpty { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000FB6 RID: 4022
		// (set) Token: 0x06000FB7 RID: 4023
		bool HasReadLink { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000FB8 RID: 4024
		// (set) Token: 0x06000FB9 RID: 4025
		bool HasEditLink { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000FBA RID: 4026
		// (set) Token: 0x06000FBB RID: 4027
		bool HasEditMediaLink { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000FBC RID: 4028
		// (set) Token: 0x06000FBD RID: 4029
		bool HasId { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000FBE RID: 4030
		// (set) Token: 0x06000FBF RID: 4031
		bool HasContent { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000FC0 RID: 4032
		// (set) Token: 0x06000FC1 RID: 4033
		bool HasTypeNameCategory { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000FC2 RID: 4034
		// (set) Token: 0x06000FC3 RID: 4035
		bool HasProperties { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000FC4 RID: 4036
		// (set) Token: 0x06000FC5 RID: 4037
		bool? MediaLinkEntry { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000FC6 RID: 4038
		// (set) Token: 0x06000FC7 RID: 4039
		ODataAtomReaderNavigationLinkDescriptor FirstNavigationLinkDescriptor { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000FC8 RID: 4040
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000FC9 RID: 4041
		ODataEntityPropertyMappingCache CachedEpm { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000FCA RID: 4042
		AtomEntryMetadata AtomEntryMetadata { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000FCB RID: 4043
		EpmCustomReaderValueCache EpmCustomReaderValueCache { get; }
	}
}
