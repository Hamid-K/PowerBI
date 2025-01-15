using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026B RID: 619
	internal static class EpmWriterUtils
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x00048514 File Offset: 0x00046714
		internal static string GetPropertyValueAsText(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			return AtomValueUtils.ConvertPrimitiveToString(propertyValue);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00048524 File Offset: 0x00046724
		internal static EntityPropertyMappingAttribute GetEntityPropertyMapping(EpmSourcePathSegment epmParentSourcePathSegment, string propertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(propertyName, "propertyName");
			EpmSourcePathSegment propertySourcePathSegment = EpmWriterUtils.GetPropertySourcePathSegment(epmParentSourcePathSegment, propertyName);
			return EpmWriterUtils.GetEntityPropertyMapping(propertySourcePathSegment);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004854C File Offset: 0x0004674C
		internal static EntityPropertyMappingAttribute GetEntityPropertyMapping(EpmSourcePathSegment epmSourcePathSegment)
		{
			if (epmSourcePathSegment == null)
			{
				return null;
			}
			EntityPropertyMappingInfo epmInfo = epmSourcePathSegment.EpmInfo;
			if (epmInfo == null)
			{
				return null;
			}
			return epmInfo.Attribute;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00048590 File Offset: 0x00046790
		internal static EpmSourcePathSegment GetPropertySourcePathSegment(EpmSourcePathSegment epmParentSourcePathSegment, string propertyName)
		{
			EpmSourcePathSegment epmSourcePathSegment = null;
			if (epmParentSourcePathSegment != null)
			{
				epmSourcePathSegment = Enumerable.FirstOrDefault<EpmSourcePathSegment>(epmParentSourcePathSegment.SubProperties, (EpmSourcePathSegment subProperty) => subProperty.PropertyName == propertyName);
			}
			return epmSourcePathSegment;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000485D0 File Offset: 0x000467D0
		internal static void CacheEpmProperties(EntryPropertiesValueCache propertyValueCache, EpmSourceTree sourceTree)
		{
			EpmSourcePathSegment root = sourceTree.Root;
			EpmWriterUtils.CacheEpmSourcePathSegments(propertyValueCache, root.SubProperties, propertyValueCache.EntryProperties);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000485F8 File Offset: 0x000467F8
		private static void CacheEpmSourcePathSegments(EpmValueCache valueCache, List<EpmSourcePathSegment> segments, IEnumerable<ODataProperty> properties)
		{
			if (properties == null)
			{
				return;
			}
			foreach (EpmSourcePathSegment epmSourcePathSegment in segments)
			{
				ODataComplexValue odataComplexValue;
				if (epmSourcePathSegment.EpmInfo == null && EpmWriterUtils.TryGetPropertyValue<ODataComplexValue>(properties, epmSourcePathSegment.PropertyName, out odataComplexValue))
				{
					IEnumerable<ODataProperty> enumerable = valueCache.CacheComplexValueProperties(odataComplexValue);
					EpmWriterUtils.CacheEpmSourcePathSegments(valueCache, epmSourcePathSegment.SubProperties, enumerable);
				}
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00048690 File Offset: 0x00046890
		private static bool TryGetPropertyValue<T>(IEnumerable<ODataProperty> properties, string propertyName, out T propertyValue) where T : class
		{
			propertyValue = default(T);
			ODataProperty odataProperty = Enumerable.FirstOrDefault<ODataProperty>(Enumerable.Where<ODataProperty>(properties, (ODataProperty p) => string.CompareOrdinal(p.Name, propertyName) == 0));
			if (odataProperty != null)
			{
				propertyValue = odataProperty.Value as T;
				return propertyValue != null || odataProperty.Value == null;
			}
			return false;
		}
	}
}
