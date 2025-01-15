using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.OData.Core.Json;
using Microsoft.Spatial;

namespace Microsoft.OData.Core
{
	// Token: 0x020001AA RID: 426
	internal sealed class PrimitiveConverter
	{
		// Token: 0x06000FDB RID: 4059 RVA: 0x00036570 File Offset: 0x00034770
		internal PrimitiveConverter(KeyValuePair<Type, IPrimitiveTypeConverter>[] spatialPrimitiveTypeConverters)
		{
			this.spatialPrimitiveTypeConverters = new Dictionary<Type, IPrimitiveTypeConverter>(EqualityComparer<Type>.Default);
			foreach (KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair in spatialPrimitiveTypeConverters)
			{
				this.spatialPrimitiveTypeConverters.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000365C9 File Offset: 0x000347C9
		internal static PrimitiveConverter Instance
		{
			get
			{
				return PrimitiveConverter.primitiveConverter;
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000365D0 File Offset: 0x000347D0
		internal bool TryTokenizeFromXml(XmlReader reader, Type targetType, out object tokenizedPropertyValue)
		{
			tokenizedPropertyValue = null;
			IPrimitiveTypeConverter primitiveTypeConverter;
			if (this.TryGetConverter(targetType, out primitiveTypeConverter))
			{
				tokenizedPropertyValue = primitiveTypeConverter.TokenizeFromXml(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00036614 File Offset: 0x00034814
		internal bool TryWriteAtom(object instance, XmlWriter writer)
		{
			return this.TryWriteValue(instance, delegate(IPrimitiveTypeConverter ptc)
			{
				ptc.WriteAtom(instance, writer);
			});
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003666C File Offset: 0x0003486C
		internal bool TryWriteAtom(object instance, TextWriter writer)
		{
			return this.TryWriteValue(instance, delegate(IPrimitiveTypeConverter ptc)
			{
				ptc.WriteAtom(instance, writer);
			});
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000366A8 File Offset: 0x000348A8
		internal void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			this.TryGetConverter(type, out primitiveTypeConverter);
			primitiveTypeConverter.WriteJsonLight(instance, jsonWriter);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000366D0 File Offset: 0x000348D0
		private bool TryWriteValue(object instance, Action<IPrimitiveTypeConverter> writeMethod)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			if (this.TryGetConverter(type, out primitiveTypeConverter))
			{
				writeMethod.Invoke(primitiveTypeConverter);
				return true;
			}
			return false;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000366FC File Offset: 0x000348FC
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

		// Token: 0x040006F9 RID: 1785
		private static readonly IPrimitiveTypeConverter geographyTypeConverter = new GeographyTypeConverter();

		// Token: 0x040006FA RID: 1786
		private static readonly IPrimitiveTypeConverter geometryTypeConverter = new GeometryTypeConverter();

		// Token: 0x040006FB RID: 1787
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

		// Token: 0x040006FC RID: 1788
		private readonly Dictionary<Type, IPrimitiveTypeConverter> spatialPrimitiveTypeConverters;
	}
}
