using System;
using System.Collections.Generic;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x020000A6 RID: 166
	internal sealed class PrimitiveConverter
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00011B2C File Offset: 0x0000FD2C
		internal PrimitiveConverter(KeyValuePair<Type, IPrimitiveTypeConverter>[] spatialPrimitiveTypeConverters)
		{
			this.spatialPrimitiveTypeConverters = new Dictionary<Type, IPrimitiveTypeConverter>(EqualityComparer<Type>.Default);
			foreach (KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair in spatialPrimitiveTypeConverters)
			{
				this.spatialPrimitiveTypeConverters.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00011B80 File Offset: 0x0000FD80
		internal static PrimitiveConverter Instance
		{
			get
			{
				return PrimitiveConverter.primitiveConverter;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00011B88 File Offset: 0x0000FD88
		internal void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			this.TryGetConverter(type, out primitiveTypeConverter);
			primitiveTypeConverter.WriteJsonLight(instance, jsonWriter);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		private bool TryGetConverter(Type type, out IPrimitiveTypeConverter primitiveTypeConverter)
		{
			if (typeof(ISpatial).IsAssignableFrom(type))
			{
				KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair = new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(object), null);
				foreach (KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair2 in this.spatialPrimitiveTypeConverters)
				{
					if (keyValuePair2.Key.IsAssignableFrom(type) && keyValuePair.Key.IsAssignableFrom(keyValuePair2.Key))
					{
						keyValuePair = keyValuePair2;
					}
				}
				primitiveTypeConverter = keyValuePair.Value;
				return keyValuePair.Value != null;
			}
			primitiveTypeConverter = null;
			return false;
		}

		// Token: 0x040002DC RID: 732
		private static readonly IPrimitiveTypeConverter geographyTypeConverter = new GeographyTypeConverter();

		// Token: 0x040002DD RID: 733
		private static readonly IPrimitiveTypeConverter geometryTypeConverter = new GeometryTypeConverter();

		// Token: 0x040002DE RID: 734
		private static readonly PrimitiveConverter primitiveConverter = new PrimitiveConverter(new KeyValuePair<Type, IPrimitiveTypeConverter>[]
		{
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyPoint), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyLineString), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyPolygon), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyCollection), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyMultiPoint), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyMultiLineString), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyMultiPolygon), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(Geography), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryPoint), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryLineString), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryPolygon), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryCollection), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryMultiPoint), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryMultiLineString), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryMultiPolygon), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(Geometry), PrimitiveConverter.geometryTypeConverter)
		});

		// Token: 0x040002DF RID: 735
		private readonly Dictionary<Type, IPrimitiveTypeConverter> spatialPrimitiveTypeConverters;
	}
}
