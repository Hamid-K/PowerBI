using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D2 RID: 466
	internal static class TmdlSchemaExtensions
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x000C39CC File Offset: 0x000C1BCC
		public static TmdlSchema WithRootObjects(this TmdlSchema schema, params TmdlObjectInfo[] objects)
		{
			if (objects != null && objects.Length != 0)
			{
				foreach (TmdlObjectInfo tmdlObjectInfo in objects)
				{
					schema.AddRootObject(tmdlObjectInfo);
				}
			}
			return schema;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000C39FC File Offset: 0x000C1BFC
		public static TmdlSchema WithRootObjects(this TmdlSchema schema, IEnumerable<TmdlObjectInfo> objects)
		{
			foreach (TmdlObjectInfo tmdlObjectInfo in objects)
			{
				schema.AddRootObject(tmdlObjectInfo);
			}
			return schema;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000C3A48 File Offset: 0x000C1C48
		internal static IEnumerable<Type> GetAllMetadataEnums(this TmdlSchema schema)
		{
			HashSet<Type> enums = new HashSet<Type>();
			foreach (TmdlObjectInfo objectInfo in schema.GetAllMetadataObjects())
			{
				if (objectInfo.ObjectType == ObjectType.TimeUnitColumnAssociation)
				{
					yield return typeof(TimeUnit);
				}
				TmdlPropertyInfo tmdlPropertyInfo;
				if (objectInfo.IsDefaultPropertyAllowed(out tmdlPropertyInfo) && tmdlPropertyInfo.Type == TmdlValueType.Scalar && tmdlPropertyInfo.ScalarValueType != null && tmdlPropertyInfo.ScalarValueType.Value == TmdlScalarValueType.Enum && tmdlPropertyInfo.EnumType != null && tmdlPropertyInfo.EnumType.IsEnum && enums.Add(tmdlPropertyInfo.EnumType))
				{
					yield return tmdlPropertyInfo.EnumType;
				}
				if (objectInfo.HasVariants)
				{
					foreach (TmdlObjectInfo tmdlObjectInfo in from v in objectInfo.Variants
						select v.Value into vi
						where vi.HasAnyProperty(false, false)
						select vi)
					{
						foreach (TmdlPropertyInfo tmdlPropertyInfo2 in from p in tmdlObjectInfo.Properties
							where !p.IsDeprecated
							where p.Type == TmdlValueType.Scalar && p.ScalarValueType != null && p.ScalarValueType.Value == TmdlScalarValueType.Enum
							where p.EnumType != null && p.EnumType.IsEnum
							select p)
						{
							if (enums.Add(tmdlPropertyInfo2.EnumType))
							{
								yield return tmdlPropertyInfo2.EnumType;
							}
						}
						IEnumerator<TmdlPropertyInfo> enumerator3 = null;
					}
					IEnumerator<TmdlObjectInfo> enumerator2 = null;
				}
				else if (objectInfo.HasAnyProperty(false, false))
				{
					foreach (TmdlPropertyInfo tmdlPropertyInfo3 in from p in objectInfo.Properties
						where !p.IsDeprecated
						where p.Type == TmdlValueType.Scalar && p.ScalarValueType != null && p.ScalarValueType.Value == TmdlScalarValueType.Enum
						where p.EnumType != null && p.EnumType.IsEnum
						select p)
					{
						if (enums.Add(tmdlPropertyInfo3.EnumType))
						{
							yield return tmdlPropertyInfo3.EnumType;
						}
					}
					IEnumerator<TmdlPropertyInfo> enumerator3 = null;
				}
				objectInfo = null;
			}
			IEnumerator<TmdlObjectInfo> enumerator = null;
			yield break;
			yield break;
		}
	}
}
