using System;

namespace Microsoft.OData
{
	// Token: 0x0200007A RID: 122
	public static class ODataObjectModelExtensions
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0000D255 File Offset: 0x0000B455
		public static void SetSerializationInfo(this ODataResourceSet resourceSet, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceSet>(resourceSet, "resourceSet");
			resourceSet.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000D26A File Offset: 0x0000B46A
		public static void SetSerializationInfo(this ODataResource resource, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResource>(resource, "resource");
			resource.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000D27F File Offset: 0x0000B47F
		public static void SetSerializationInfo(this ODataNestedResourceInfo nestedResourceInfo, ODataNestedResourceInfoSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNestedResourceInfo>(nestedResourceInfo, "resource");
			nestedResourceInfo.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000D294 File Offset: 0x0000B494
		public static void SetSerializationInfo(this ODataProperty property, ODataPropertySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			property.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000D2A9 File Offset: 0x0000B4A9
		public static void SetSerializationInfo(this ODataCollectionStart collectionStart, ODataCollectionStartSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collectionStart");
			collectionStart.SerializationInfo = serializationInfo;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000D2BE File Offset: 0x0000B4BE
		public static void SetSerializationInfo(this ODataDeltaResourceSet deltaResourceSet, ODataDeltaResourceSetSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaResourceSet>(deltaResourceSet, "deltaResourceSet");
			deltaResourceSet.SerializationInfo = serializationInfo;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000D2D3 File Offset: 0x0000B4D3
		public static void SetSerializationInfo(this ODataDeltaDeletedEntry deltaDeletedEntry, ODataDeltaSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaDeletedEntry>(deltaDeletedEntry, "deltaDeletedEntry");
			deltaDeletedEntry.SerializationInfo = serializationInfo;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		public static void SetSerializationInfo(this ODataDeltaLinkBase deltalink, ODataDeltaSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaLinkBase>(deltalink, "deltalink");
			deltalink.SerializationInfo = serializationInfo;
		}
	}
}
