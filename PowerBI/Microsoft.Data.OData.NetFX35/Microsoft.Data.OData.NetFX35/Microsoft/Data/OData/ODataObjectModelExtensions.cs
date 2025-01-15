using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011A RID: 282
	public static class ODataObjectModelExtensions
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00019384 File Offset: 0x00017584
		public static void SetSerializationInfo(this ODataFeed feed, ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			feed.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019398 File Offset: 0x00017598
		public static void SetSerializationInfo(this ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			entry.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000193AC File Offset: 0x000175AC
		public static void SetSerializationInfo(this ODataProperty property, ODataPropertySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			property.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000193C0 File Offset: 0x000175C0
		public static void SetSerializationInfo(this ODataCollectionStart collectionStart, ODataCollectionStartSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collectionStart");
			collectionStart.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000193D4 File Offset: 0x000175D4
		public static void SetSerializationInfo(this ODataEntityReferenceLink entityReferenceLink, ODataEntityReferenceLinkSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			entityReferenceLink.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000193E8 File Offset: 0x000175E8
		public static void SetSerializationInfo(this ODataEntityReferenceLinks entityReferenceLinks, ODataEntityReferenceLinksSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(entityReferenceLinks, "entityReferenceLinks");
			entityReferenceLinks.SerializationInfo = serializationInfo;
		}
	}
}
