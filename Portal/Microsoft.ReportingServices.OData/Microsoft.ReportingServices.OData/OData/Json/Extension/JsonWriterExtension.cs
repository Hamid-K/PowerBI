using System;
using System.Collections;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OData.Query;

namespace Microsoft.ReportingServices.OData.Json.Extension
{
	// Token: 0x02000018 RID: 24
	internal static class JsonWriterExtension
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00003FD2 File Offset: 0x000021D2
		public static AutoScope GetObjectScope(this JsonWriter writer)
		{
			return new AutoScope(new Action(writer.StartObjectScope), new Action(writer.EndObjectScope));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003FF3 File Offset: 0x000021F3
		public static AutoScope GetObjectPropertyScope(this JsonWriter writer, string name)
		{
			writer.WriteName(name);
			return new AutoScope(new Action(writer.StartObjectScope), new Action(writer.EndObjectScope));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000401B File Offset: 0x0000221B
		public static AutoScope GetArrayScope(this JsonWriter writer)
		{
			return new AutoScope(new Action(writer.StartArrayScope), new Action(writer.EndArrayScope));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000403C File Offset: 0x0000223C
		public static AutoScope GetArrayPropertyScope(this JsonWriter writer, string name)
		{
			writer.WriteName(name);
			return new AutoScope(new Action(writer.StartArrayScope), new Action(writer.EndArrayScope));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004064 File Offset: 0x00002264
		public static void WriteProperty(this JsonWriter writer, string name, string value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004074 File Offset: 0x00002274
		public static void WriteProperty(this JsonWriter writer, string name, bool value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004084 File Offset: 0x00002284
		public static void WriteProperty(this JsonWriter writer, string name, byte value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004094 File Offset: 0x00002294
		public static void WriteProperty(this JsonWriter writer, string name, byte[] value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000040A4 File Offset: 0x000022A4
		public static void WriteProperty(this JsonWriter writer, string name, sbyte value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000040B4 File Offset: 0x000022B4
		public static void WriteProperty(this JsonWriter writer, string name, short value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000040C4 File Offset: 0x000022C4
		public static void WriteProperty(this JsonWriter writer, string name, int value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000040D4 File Offset: 0x000022D4
		public static void WriteProperty(this JsonWriter writer, string name, long value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000040E4 File Offset: 0x000022E4
		public static void WriteProperty(this JsonWriter writer, string name, float value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000040F4 File Offset: 0x000022F4
		public static void WriteProperty(this JsonWriter writer, string name, double value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004104 File Offset: 0x00002304
		public static void WriteProperty(this JsonWriter writer, string name, decimal value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004114 File Offset: 0x00002314
		public static void WriteProperty(this JsonWriter writer, string name, Guid value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004124 File Offset: 0x00002324
		public static void WriteProperty(this JsonWriter writer, string name, TimeSpan value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004134 File Offset: 0x00002334
		public static void WriteProperty(this JsonWriter writer, string name, DateTime value, ODataVersion odataVersion)
		{
			writer.WriteName(name);
			writer.WriteValue(value, odataVersion);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004145 File Offset: 0x00002345
		public static void WriteProperty(this JsonWriter writer, string name, DateTime value)
		{
			writer.WriteName(name);
			writer.WriteValue(value, ODataVersion.V3);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004156 File Offset: 0x00002356
		public static void WriteProperty(this JsonWriter writer, string name, DateTimeOffset value, ODataVersion odataVersion)
		{
			writer.WriteName(name);
			writer.WriteValue(value, odataVersion);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004167 File Offset: 0x00002367
		public static void WriteProperty(this JsonWriter writer, string name, DateTimeOffset value)
		{
			writer.WriteName(name);
			writer.WriteValue(value, ODataVersion.V3);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004178 File Offset: 0x00002378
		public static void WriteVariantValue(this JsonWriter writer, object value)
		{
			if (value == null)
			{
				writer.WriteValue(null);
				return;
			}
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode != TypeCode.Object)
			{
				if (typeCode == TypeCode.Boolean)
				{
					writer.WriteValue((bool)value);
					return;
				}
				switch (typeCode)
				{
				case TypeCode.Int16:
					writer.WriteValue((short)value);
					return;
				case TypeCode.Int32:
					writer.WriteValue((int)value);
					return;
				case TypeCode.Int64:
				case TypeCode.Double:
				case TypeCode.Decimal:
				case TypeCode.DateTime:
				case TypeCode.String:
					writer.WriteValue(ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value));
					return;
				}
			}
			else
			{
				if (value is byte[])
				{
					writer.WriteValue((byte[])value);
					return;
				}
				IEnumerable enumerable = value as IEnumerable;
				if (enumerable != null)
				{
					writer.WriteVariantArray(enumerable);
					return;
				}
			}
			throw new InvalidOperationException(Errors.UnsupportedType(typeCode.ToString()));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004250 File Offset: 0x00002450
		public static void WriteVariantArray(this JsonWriter writer, IEnumerable items)
		{
			if (items == null)
			{
				return;
			}
			writer.StartArrayScope();
			foreach (object obj in items)
			{
				writer.WriteVariantValue(obj);
			}
			writer.EndArrayScope();
		}
	}
}
