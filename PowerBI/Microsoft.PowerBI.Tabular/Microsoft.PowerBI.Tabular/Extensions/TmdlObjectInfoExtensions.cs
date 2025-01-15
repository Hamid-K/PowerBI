using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001CD RID: 461
	internal static class TmdlObjectInfoExtensions
	{
		// Token: 0x06001BEC RID: 7148 RVA: 0x000C3748 File Offset: 0x000C1948
		public static TmdlObjectInfo WithDescription(this TmdlObjectInfo objectInfo, string descriptionPropertyName = null)
		{
			if (string.IsNullOrEmpty(descriptionPropertyName))
			{
				descriptionPropertyName = "description";
			}
			objectInfo.DescriptionProperty = new TmdlPropertyInfo(descriptionPropertyName, TmdlValueType.String, null, null, null, null, null, false, false);
			return objectInfo;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000C3791 File Offset: 0x000C1991
		public static TmdlObjectInfo WithDefaultProperty(this TmdlObjectInfo objectInfo, TmdlPropertyInfo property)
		{
			objectInfo.DefaultProperty = property;
			return objectInfo;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000C379C File Offset: 0x000C199C
		public static TmdlObjectInfo WithDefaultProperty(this TmdlObjectInfo objectInfo, string name, TmdlValueType type = TmdlValueType.String, TmdlScalarValueType scalarValueType = TmdlScalarValueType.Int, Type enumType = null)
		{
			objectInfo.DefaultProperty = new TmdlPropertyInfo(name, type, new TmdlScalarValueType?(scalarValueType), null, enumType, null, null, true, false);
			return objectInfo;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000C37D6 File Offset: 0x000C19D6
		public static TmdlObjectInfo WithProperty(this TmdlObjectInfo objectInfo, TmdlPropertyInfo property)
		{
			objectInfo.AddProperty(property);
			return objectInfo;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000C37E0 File Offset: 0x000C19E0
		public static TmdlObjectInfo WithProperties(this TmdlObjectInfo objectInfo, params TmdlPropertyInfo[] properties)
		{
			if (properties != null && properties.Length != 0)
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo in properties)
				{
					objectInfo.AddProperty(tmdlPropertyInfo);
				}
			}
			return objectInfo;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000C3810 File Offset: 0x000C1A10
		public static TmdlObjectInfo WithProperties(this TmdlObjectInfo objectInfo, IEnumerable<TmdlPropertyInfo> properties)
		{
			foreach (TmdlPropertyInfo tmdlPropertyInfo in properties)
			{
				objectInfo.AddProperty(tmdlPropertyInfo);
			}
			return objectInfo;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000C385C File Offset: 0x000C1A5C
		public static TmdlObjectInfo WithChildObject(this TmdlObjectInfo objectInfo, TmdlObjectInfo childObject)
		{
			objectInfo.AddChildObject(childObject);
			return objectInfo;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000C3868 File Offset: 0x000C1A68
		public static TmdlObjectInfo WithChildObjects(this TmdlObjectInfo objectInfo, params TmdlObjectInfo[] childObjects)
		{
			if (childObjects != null && childObjects.Length != 0)
			{
				foreach (TmdlObjectInfo tmdlObjectInfo in childObjects)
				{
					objectInfo.AddChildObject(tmdlObjectInfo);
				}
			}
			return objectInfo;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000C3898 File Offset: 0x000C1A98
		public static TmdlObjectInfo WithChildObjects(this TmdlObjectInfo objectInfo, IEnumerable<TmdlObjectInfo> childObjects)
		{
			foreach (TmdlObjectInfo tmdlObjectInfo in childObjects)
			{
				objectInfo.AddChildObject(tmdlObjectInfo);
			}
			return objectInfo;
		}
	}
}
