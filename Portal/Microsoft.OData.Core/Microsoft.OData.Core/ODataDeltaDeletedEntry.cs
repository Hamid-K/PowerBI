using System;

namespace Microsoft.OData
{
	// Token: 0x02000070 RID: 112
	public sealed class ODataDeltaDeletedEntry : ODataItem
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x0000B89F File Offset: 0x00009A9F
		public ODataDeltaDeletedEntry(string id, DeltaDeletedEntryReason reason)
		{
			this.Id = id;
			this.Reason = new DeltaDeletedEntryReason?(reason);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000B8BA File Offset: 0x00009ABA
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000B8C2 File Offset: 0x00009AC2
		public string Id { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000B8CB File Offset: 0x00009ACB
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000B8D3 File Offset: 0x00009AD3
		public DeltaDeletedEntryReason? Reason { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000B8DC File Offset: 0x00009ADC
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		internal ODataDeltaSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataDeltaSerializationInfo.Validate(value);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		internal static ODataDeltaDeletedEntry GetDeltaDeletedEntry(ODataDeletedResource entry)
		{
			ODataDeltaDeletedEntry odataDeltaDeletedEntry = new ODataDeltaDeletedEntry(entry.Id.OriginalString, entry.Reason ?? DeltaDeletedEntryReason.Deleted);
			if (entry.SerializationInfo != null)
			{
				odataDeltaDeletedEntry.SetSerializationInfo(entry.SerializationInfo);
			}
			return odataDeltaDeletedEntry;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000B944 File Offset: 0x00009B44
		internal static ODataDeletedResource GetDeletedResource(ODataDeltaDeletedEntry entry)
		{
			Uri uri = UriUtils.StringToUri(entry.Id);
			ODataDeletedResource odataDeletedResource = new ODataDeletedResource
			{
				Id = uri,
				Reason = entry.Reason
			};
			if (entry.SerializationInfo != null)
			{
				odataDeletedResource.SerializationInfo = new ODataResourceSerializationInfo
				{
					NavigationSourceName = ((entry.SerializationInfo == null) ? null : entry.SerializationInfo.NavigationSourceName)
				};
			}
			return odataDeletedResource;
		}

		// Token: 0x040001D5 RID: 469
		private ODataDeltaSerializationInfo serializationInfo;
	}
}
