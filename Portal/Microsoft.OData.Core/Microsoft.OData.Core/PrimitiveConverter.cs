using System;
using System.Collections.Generic;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x020000C4 RID: 196
	internal sealed class PrimitiveConverter
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x000163EC File Offset: 0x000145EC
		internal PrimitiveConverter(KeyValuePair<Type, IPrimitiveTypeConverter>[] spatialPrimitiveTypeConverters)
		{
			this.spatialPrimitiveTypeConverters = new Dictionary<Type, IPrimitiveTypeConverter>(EqualityComparer<Type>.Default);
			foreach (KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair in spatialPrimitiveTypeConverters)
			{
				this.spatialPrimitiveTypeConverters.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00016440 File Offset: 0x00014640
		internal static PrimitiveConverter Instance
		{
			get
			{
				return PrimitiveConverter.primitiveConverter;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00016448 File Offset: 0x00014648
		internal void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			this.TryGetConverter(type, out primitiveTypeConverter);
			primitiveTypeConverter.WriteJsonLight(instance, jsonWriter);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00016470 File Offset: 0x00014670
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

		// Token: 0x0400033C RID: 828
		private static readonly IPrimitiveTypeConverter geographyTypeConverter = new GeographyTypeConverter();

		// Token: 0x0400033D RID: 829
		private static readonly IPrimitiveTypeConverter geometryTypeConverter = new GeometryTypeConverter();

		// Token: 0x0400033E RID: 830
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

		// Token: 0x0400033F RID: 831
		private readonly Dictionary<Type, IPrimitiveTypeConverter> spatialPrimitiveTypeConverters;
	}
}
