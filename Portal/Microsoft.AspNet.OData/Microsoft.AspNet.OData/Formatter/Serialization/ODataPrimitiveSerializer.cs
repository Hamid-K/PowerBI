using System;
using System.Data.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001AC RID: 428
	public class ODataPrimitiveSerializer : ODataEdmTypeSerializer
	{
		// Token: 0x06000E3A RID: 3642 RVA: 0x0003A33C File Offset: 0x0003853C
		public ODataPrimitiveSerializer()
			: base(ODataPayloadKind.Property)
		{
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003A348 File Offset: 0x00038548
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (writeContext.RootElementName == null)
			{
				throw Error.Argument("writeContext", SRResources.RootElementNameMissing, new object[] { typeof(ODataSerializerContext).Name });
			}
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, type);
			messageWriter.WriteProperty(this.CreateProperty(graph, edmType, writeContext.RootElementName, writeContext));
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003A3C4 File Offset: 0x000385C4
		public sealed override ODataValue CreateODataValue(object graph, IEdmTypeReference expectedType, ODataSerializerContext writeContext)
		{
			if (!expectedType.IsPrimitive())
			{
				throw Error.InvalidOperation(SRResources.CannotWriteType, new object[]
				{
					typeof(ODataPrimitiveSerializer),
					expectedType.FullName()
				});
			}
			ODataPrimitiveValue odataPrimitiveValue = this.CreateODataPrimitiveValue(graph, expectedType.AsPrimitive(), writeContext);
			if (odataPrimitiveValue == null)
			{
				return new ODataNullValue();
			}
			return odataPrimitiveValue;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003A419 File Offset: 0x00038619
		public virtual ODataPrimitiveValue CreateODataPrimitiveValue(object graph, IEdmPrimitiveTypeReference primitiveType, ODataSerializerContext writeContext)
		{
			return ODataPrimitiveSerializer.CreatePrimitive(graph, primitiveType, writeContext);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003A424 File Offset: 0x00038624
		internal static void AddTypeNameAnnotationAsNeeded(ODataPrimitiveValue primitive, IEdmPrimitiveTypeReference primitiveType, ODataMetadataLevel metadataLevel)
		{
			object value = primitive.Value;
			string text = null;
			if (!ODataPrimitiveSerializer.ShouldSuppressTypeNameSerialization(value, metadataLevel))
			{
				text = primitiveType.FullName();
			}
			primitive.TypeAnnotation = new ODataTypeAnnotation(text);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003A454 File Offset: 0x00038654
		internal static ODataPrimitiveValue CreatePrimitive(object value, IEdmPrimitiveTypeReference primitiveType, ODataSerializerContext writeContext)
		{
			if (value == null)
			{
				return null;
			}
			ODataPrimitiveValue odataPrimitiveValue = new ODataPrimitiveValue(ODataPrimitiveSerializer.ConvertPrimitiveValue(value, primitiveType));
			if (writeContext != null)
			{
				ODataPrimitiveSerializer.AddTypeNameAnnotationAsNeeded(odataPrimitiveValue, primitiveType, writeContext.MetadataLevel);
			}
			return odataPrimitiveValue;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003A484 File Offset: 0x00038684
		internal static object ConvertPrimitiveValue(object value, IEdmPrimitiveTypeReference primitiveType)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (primitiveType != null && primitiveType.IsDate() && TypeHelper.IsDateTime(type))
			{
				return (DateTime)value;
			}
			if (primitiveType != null && primitiveType.IsTimeOfDay() && TypeHelper.IsTimeSpan(type))
			{
				return (TimeSpan)value;
			}
			return ODataPrimitiveSerializer.ConvertUnsupportedPrimitives(value);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003A4EC File Offset: 0x000386EC
		internal static object ConvertUnsupportedPrimitives(object value)
		{
			if (value != null)
			{
				Type type = value.GetType();
				TypeCode typeCode = Type.GetTypeCode(type);
				if (typeCode == TypeCode.Char)
				{
					return new string((char)value, 1);
				}
				switch (typeCode)
				{
				case TypeCode.UInt16:
					return (int)((ushort)value);
				case TypeCode.Int32:
				case TypeCode.Int64:
					break;
				case TypeCode.UInt32:
					return (long)((ulong)((uint)value));
				case TypeCode.UInt64:
					return checked((long)((ulong)value));
				default:
					if (typeCode == TypeCode.DateTime)
					{
						DateTime dateTime = (DateTime)value;
						TimeZoneInfo timeZone = TimeZoneInfoHelper.TimeZone;
						TimeSpan utcOffset = timeZone.GetUtcOffset(dateTime);
						if (utcOffset >= TimeSpan.Zero)
						{
							if (dateTime <= DateTime.MinValue + utcOffset)
							{
								return DateTimeOffset.MinValue;
							}
						}
						else if (dateTime >= DateTime.MaxValue + utcOffset)
						{
							return DateTimeOffset.MaxValue;
						}
						if (dateTime.Kind == DateTimeKind.Local)
						{
							TimeSpan utcOffset2 = TimeZoneInfo.Local.GetUtcOffset(dateTime);
							if (utcOffset2 < TimeSpan.Zero)
							{
								if (dateTime >= DateTime.MaxValue + utcOffset2)
								{
									return DateTimeOffset.MaxValue;
								}
							}
							else if (dateTime <= DateTime.MinValue + utcOffset2)
							{
								return DateTimeOffset.MinValue;
							}
							return TimeZoneInfo.ConvertTime(new DateTimeOffset(dateTime), timeZone);
						}
						if (dateTime.Kind == DateTimeKind.Utc)
						{
							return TimeZoneInfo.ConvertTime(new DateTimeOffset(dateTime), timeZone);
						}
						return new DateTimeOffset(dateTime, timeZone.GetUtcOffset(dateTime));
					}
					break;
				}
				if (type == typeof(char[]))
				{
					return new string(value as char[]);
				}
				if (type == typeof(XElement))
				{
					return ((XElement)value).ToString();
				}
				if (type == typeof(Binary))
				{
					return ((Binary)value).ToArray();
				}
			}
			return value;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003A6CC File Offset: 0x000388CC
		internal static bool CanTypeBeInferredInJson(object value)
		{
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode <= TypeCode.Int32)
			{
				if (typeCode != TypeCode.Boolean && typeCode != TypeCode.Int32)
				{
					return false;
				}
			}
			else
			{
				if (typeCode == TypeCode.Double)
				{
					double num = (double)value;
					return !double.IsNaN(num) && !double.IsInfinity(num);
				}
				if (typeCode != TypeCode.String)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003A71D File Offset: 0x0003891D
		internal static bool ShouldSuppressTypeNameSerialization(object value, ODataMetadataLevel metadataLevel)
		{
			return metadataLevel != ODataMetadataLevel.FullMetadata || ODataPrimitiveSerializer.CanTypeBeInferredInJson(value);
		}
	}
}
