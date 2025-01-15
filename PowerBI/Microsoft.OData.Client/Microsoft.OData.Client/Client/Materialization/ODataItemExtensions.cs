using System;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000108 RID: 264
	internal static class ODataItemExtensions
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x0002AB54 File Offset: 0x00028D54
		public static object GetMaterializedValue(this ODataProperty property)
		{
			ODataAnnotatable odataAnnotatable = (property.Value as ODataAnnotatable) ?? property;
			return ODataItemExtensions.GetMaterializedValueCore(odataAnnotatable);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002AB78 File Offset: 0x00028D78
		public static bool HasMaterializedValue(this ODataProperty property)
		{
			ODataAnnotatable odataAnnotatable = (property.Value as ODataAnnotatable) ?? property;
			return ODataItemExtensions.HasMaterializedValueCore(odataAnnotatable);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002AB9C File Offset: 0x00028D9C
		public static void SetMaterializedValue(this ODataProperty property, object materializedValue)
		{
			ODataAnnotatable odataAnnotatable = (property.Value as ODataAnnotatable) ?? property;
			ODataItemExtensions.SetMaterializedValueCore(odataAnnotatable, materializedValue);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002ABC4 File Offset: 0x00028DC4
		private static object GetMaterializedValueCore(ODataAnnotatable annotatableObject)
		{
			ODataItemExtensions.MaterializerPropertyValue annotation = annotatableObject.GetAnnotation<ODataItemExtensions.MaterializerPropertyValue>();
			return annotation.Value;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002ABDE File Offset: 0x00028DDE
		private static bool HasMaterializedValueCore(ODataAnnotatable annotatableObject)
		{
			return annotatableObject.GetAnnotation<ODataItemExtensions.MaterializerPropertyValue>() != null;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002ABEC File Offset: 0x00028DEC
		private static void SetMaterializedValueCore(ODataAnnotatable annotatableObject, object materializedValue)
		{
			ODataItemExtensions.MaterializerPropertyValue materializerPropertyValue = new ODataItemExtensions.MaterializerPropertyValue
			{
				Value = materializedValue
			};
			annotatableObject.SetAnnotation(materializerPropertyValue);
		}

		// Token: 0x020001DC RID: 476
		private class MaterializerPropertyValue
		{
			// Token: 0x17000393 RID: 915
			// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00032BD4 File Offset: 0x00030DD4
			// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00032BDC File Offset: 0x00030DDC
			public object Value { get; set; }
		}
	}
}
