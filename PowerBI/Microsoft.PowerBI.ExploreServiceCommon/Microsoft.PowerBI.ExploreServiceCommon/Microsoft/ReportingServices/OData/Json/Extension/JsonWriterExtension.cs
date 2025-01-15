using System;
using System.Collections;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OData.Query;

namespace Microsoft.ReportingServices.OData.Json.Extension
{
	// Token: 0x0200001B RID: 27
	internal static class JsonWriterExtension
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003EE6 File Offset: 0x000020E6
		public static AutoScope GetObjectScope(this JsonWriter writer)
		{
			return new AutoScope(new Action(writer.StartObjectScope), new Action(writer.EndObjectScope));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003F07 File Offset: 0x00002107
		public static AutoScope GetObjectPropertyScope(this JsonWriter writer, string name)
		{
			writer.WriteName(name);
			return new AutoScope(new Action(writer.StartObjectScope), new Action(writer.EndObjectScope));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003F2F File Offset: 0x0000212F
		public static AutoScope GetArrayScope(this JsonWriter writer)
		{
			return new AutoScope(new Action(writer.StartArrayScope), new Action(writer.EndArrayScope));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003F50 File Offset: 0x00002150
		public static AutoScope GetArrayPropertyScope(this JsonWriter writer, string name)
		{
			writer.WriteName(name);
			return new AutoScope(new Action(writer.StartArrayScope), new Action(writer.EndArrayScope));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003F78 File Offset: 0x00002178
		public static void WriteProperty(this JsonWriter writer, string name, string value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F88 File Offset: 0x00002188
		public static void WriteProperty(this JsonWriter writer, string name, bool value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003F98 File Offset: 0x00002198
		public static void WriteProperty(this JsonWriter writer, string name, byte value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003FA8 File Offset: 0x000021A8
		public static void WriteProperty(this JsonWriter writer, string name, byte[] value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003FB8 File Offset: 0x000021B8
		public static void WriteProperty(this JsonWriter writer, string name, sbyte value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003FC8 File Offset: 0x000021C8
		public static void WriteProperty(this JsonWriter writer, string name, short value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static void WriteProperty(this JsonWriter writer, string name, int value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003FE8 File Offset: 0x000021E8
		public static void WriteProperty(this JsonWriter writer, string name, long value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003FF8 File Offset: 0x000021F8
		public static void WriteProperty(this JsonWriter writer, string name, float value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004008 File Offset: 0x00002208
		public static void WriteProperty(this JsonWriter writer, string name, double value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004018 File Offset: 0x00002218
		public static void WriteProperty(this JsonWriter writer, string name, decimal value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004028 File Offset: 0x00002228
		public static void WriteProperty(this JsonWriter writer, string name, Guid value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004038 File Offset: 0x00002238
		public static void WriteProperty(this JsonWriter writer, string name, TimeSpan value)
		{
			writer.WriteName(name);
			writer.WriteValue(value);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004048 File Offset: 0x00002248
		public static void WriteProperty(this JsonWriter writer, string name, DateTime value, ODataVersion odataVersion)
		{
			writer.WriteName(name);
			writer.WriteValue(value, odataVersion);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004059 File Offset: 0x00002259
		public static void WriteProperty(this JsonWriter writer, string name, DateTime value)
		{
			writer.WriteName(name);
			writer.WriteValue(value, ODataVersion.V3);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000406A File Offset: 0x0000226A
		public static void WriteProperty(this JsonWriter writer, string name, DateTimeOffset value, ODataVersion odataVersion)
		{
			writer.WriteName(name);
			writer.WriteValue(value, odataVersion);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000407B File Offset: 0x0000227B
		public static void WriteProperty(this JsonWriter writer, string name, DateTimeOffset value)
		{
			writer.WriteName(name);
			writer.WriteValue(value, ODataVersion.V3);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000408C File Offset: 0x0000228C
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

		// Token: 0x060000F0 RID: 240 RVA: 0x00004164 File Offset: 0x00002364
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
