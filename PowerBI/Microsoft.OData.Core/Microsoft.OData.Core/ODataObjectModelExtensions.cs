using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200009F RID: 159
	public static class ODataObjectModelExtensions
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x000105F9 File Offset: 0x0000E7F9
		public static void SetSerializationInfo(this ODataResourceSet resourceSet, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceSet>(resourceSet, "resourceSet");
			resourceSet.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001060E File Offset: 0x0000E80E
		public static void SetSerializationInfo(this ODataResourceBase resource, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceBase>(resource, "resource");
			resource.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00010623 File Offset: 0x0000E823
		public static void SetSerializationInfo(this ODataResource resource, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResource>(resource, "resource");
			resource.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00010638 File Offset: 0x0000E838
		public static void SetSerializationInfo(this ODataNestedResourceInfo nestedResourceInfo, ODataNestedResourceInfoSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNestedResourceInfo>(nestedResourceInfo, "resource");
			nestedResourceInfo.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001064D File Offset: 0x0000E84D
		public static void SetSerializationInfo(this ODataProperty property, ODataPropertySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			property.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00010662 File Offset: 0x0000E862
		public static void SetSerializationInfo(this ODataCollectionStart collectionStart, ODataCollectionStartSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collectionStart");
			collectionStart.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00010678 File Offset: 0x0000E878
		public static void SetSerializationInfo(this ODataDeltaResourceSet deltaResourceSet, ODataDeltaResourceSetSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaResourceSet>(deltaResourceSet, "deltaResourceSet");
			ODataResourceSerializationInfo odataResourceSerializationInfo = new ODataResourceSerializationInfo
			{
				NavigationSourceName = serializationInfo.EntitySetName,
				NavigationSourceEntityTypeName = serializationInfo.EntityTypeName,
				NavigationSourceKind = EdmNavigationSourceKind.EntitySet,
				ExpectedTypeName = serializationInfo.ExpectedTypeName
			};
			deltaResourceSet.SerializationInfo = odataResourceSerializationInfo;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000106C9 File Offset: 0x0000E8C9
		public static void SetSerializationInfo(this ODataDeltaResourceSet deltaResourceSet, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaResourceSet>(deltaResourceSet, "deltaResourceSet");
			deltaResourceSet.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000106E0 File Offset: 0x0000E8E0
		public static void SetSerializationInfo(this ODataDeltaDeletedEntry deltaDeletedEntry, ODataResourceSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaDeletedEntry>(deltaDeletedEntry, "deltaDeletedEntry");
			ODataDeltaSerializationInfo odataDeltaSerializationInfo = new ODataDeltaSerializationInfo
			{
				NavigationSourceName = serializationInfo.NavigationSourceName
			};
			deltaDeletedEntry.SerializationInfo = odataDeltaSerializationInfo;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00010712 File Offset: 0x0000E912
		public static void SetSerializationInfo(this ODataDeltaDeletedEntry deltaDeletedEntry, ODataDeltaSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaDeletedEntry>(deltaDeletedEntry, "deltaDeletedEntry");
			deltaDeletedEntry.SerializationInfo = serializationInfo;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00010727 File Offset: 0x0000E927
		public static void SetSerializationInfo(this ODataDeltaLinkBase deltalink, ODataDeltaSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaLinkBase>(deltalink, "deltalink");
			deltalink.SerializationInfo = serializationInfo;
		}
	}
}
