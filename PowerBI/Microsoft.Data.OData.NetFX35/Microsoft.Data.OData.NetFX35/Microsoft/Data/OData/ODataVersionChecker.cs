using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028E RID: 654
	internal static class ODataVersionChecker
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x0004C583 File Offset: 0x0004A783
		internal static void CheckCount(ODataVersion version)
		{
			if (version < ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_InlineCountNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0004C59A File Offset: 0x0004A79A
		internal static void CheckCollectionValueProperties(ODataVersion version, string propertyName)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_CollectionPropertiesNotSupported(propertyName, ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0004C5B2 File Offset: 0x0004A7B2
		internal static void CheckCollectionValue(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_CollectionNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004C5C9 File Offset: 0x0004A7C9
		internal static void CheckNextLink(ODataVersion version)
		{
			if (version < ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_NextLinkNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		internal static void CheckDeltaLink(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_DeltaLinkNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004C5F7 File Offset: 0x0004A7F7
		internal static void CheckStreamReferenceProperty(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_StreamPropertiesNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0004C60E File Offset: 0x0004A80E
		internal static void CheckAssociationLinks(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_AssociationLinksNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0004C625 File Offset: 0x0004A825
		internal static void CheckCustomTypeScheme(ODataVersion version)
		{
			if (version > ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_PropertyNotSupportedForODataVersionGreaterThanX("TypeScheme", ODataUtils.ODataVersionToString(ODataVersion.V2)));
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0004C641 File Offset: 0x0004A841
		internal static void CheckCustomDataNamespace(ODataVersion version)
		{
			if (version > ODataVersion.V2)
			{
				throw new ODataException(Strings.ODataVersionChecker_PropertyNotSupportedForODataVersionGreaterThanX("DataNamespace", ODataUtils.ODataVersionToString(ODataVersion.V2)));
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0004C65D File Offset: 0x0004A85D
		internal static void CheckParameterPayload(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_ParameterPayloadNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004C674 File Offset: 0x0004A874
		internal static void CheckEntityPropertyMapping(ODataVersion version, IEdmEntityType entityType, IEdmModel model)
		{
			ODataEntityPropertyMappingCache epmCache = model.GetEpmCache(entityType);
			if (epmCache != null && version < epmCache.EpmTargetTree.MinimumODataProtocolVersion)
			{
				throw new ODataException(Strings.ODataVersionChecker_EpmVersionNotSupported(entityType.ODataFullName(), ODataUtils.ODataVersionToString(epmCache.EpmTargetTree.MinimumODataProtocolVersion), ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0004C6C1 File Offset: 0x0004A8C1
		internal static void CheckSpatialValue(ODataVersion version)
		{
			if (version < ODataVersion.V3)
			{
				throw new ODataException(Strings.ODataVersionChecker_GeographyAndGeometryNotSupported(ODataUtils.ODataVersionToString(version)));
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004C6D8 File Offset: 0x0004A8D8
		internal static void CheckVersionSupported(ODataVersion version, ODataMessageReaderSettings messageReaderSettings)
		{
			if (version > messageReaderSettings.MaxProtocolVersion)
			{
				throw new ODataException(Strings.ODataVersionChecker_MaxProtocolVersionExceeded(ODataUtils.ODataVersionToString(version), ODataUtils.ODataVersionToString(messageReaderSettings.MaxProtocolVersion)));
			}
		}
	}
}
