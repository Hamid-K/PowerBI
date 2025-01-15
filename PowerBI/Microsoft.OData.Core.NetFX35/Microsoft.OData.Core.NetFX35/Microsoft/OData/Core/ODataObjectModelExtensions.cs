using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200018B RID: 395
	public static class ODataObjectModelExtensions
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003469F File Offset: 0x0003289F
		public static void SetSerializationInfo(this ODataFeed feed, ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			feed.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000346B3 File Offset: 0x000328B3
		public static void SetSerializationInfo(this ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			entry.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000346C7 File Offset: 0x000328C7
		public static void SetSerializationInfo(this ODataProperty property, ODataPropertySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			property.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x000346DB File Offset: 0x000328DB
		public static void SetSerializationInfo(this ODataCollectionStart collectionStart, ODataCollectionStartSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collectionStart");
			collectionStart.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x000346EF File Offset: 0x000328EF
		public static void SetSerializationInfo(this ODataDeltaFeed deltaFeed, ODataDeltaFeedSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaFeed>(deltaFeed, "deltaFeed");
			deltaFeed.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00034703 File Offset: 0x00032903
		public static void SetSerializationInfo(this ODataDeltaDeletedEntry deltaDeletedEntry, ODataDeltaSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaDeletedEntry>(deltaDeletedEntry, "deltaDeletedEntry");
			deltaDeletedEntry.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00034717 File Offset: 0x00032917
		public static void SetSerializationInfo(this ODataDeltaLinkBase deltalink, ODataDeltaSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaLinkBase>(deltalink, "deltalink");
			deltalink.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003472B File Offset: 0x0003292B
		public static void SetPayloadValueConverter(this IEdmModel model, ODataPayloadValueConverter converter)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<ODataPayloadValueConverter>(converter, "converter");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitivePayloadValueConverter", converter);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00034758 File Offset: 0x00032958
		public static ODataPayloadValueConverter GetPayloadValueConverter(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataPayloadValueConverter odataPayloadValueConverter = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitivePayloadValueConverter") as ODataPayloadValueConverter;
			return odataPayloadValueConverter ?? ODataPayloadValueConverter.Default;
		}
	}
}
