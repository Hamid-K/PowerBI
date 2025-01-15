using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.MessageTemplates
{
	// Token: 0x0200008C RID: 140
	internal class ValueFormatter : IValueFormatter
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00019B7F File Offset: 0x00017D7F
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00019B95 File Offset: 0x00017D95
		public static IValueFormatter Instance
		{
			get
			{
				IValueFormatter valueFormatter;
				if ((valueFormatter = ValueFormatter._instance) == null)
				{
					valueFormatter = (ValueFormatter._instance = new ValueFormatter());
				}
				return valueFormatter;
			}
			set
			{
				ValueFormatter._instance = value ?? new ValueFormatter();
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00019BA6 File Offset: 0x00017DA6
		private ValueFormatter()
		{
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00019BC0 File Offset: 0x00017DC0
		public bool FormatValue(object value, string format, CaptureType captureType, IFormatProvider formatProvider, StringBuilder builder)
		{
			if (captureType == CaptureType.Serialize)
			{
				return ConfigurationItemFactory.Default.JsonConverter.SerializeObject(value, builder);
			}
			if (captureType != CaptureType.Stringify)
			{
				return this.FormatObject(value, format, formatProvider, builder);
			}
			builder.Append('"');
			ValueFormatter.FormatToString(value, null, formatProvider, builder);
			builder.Append('"');
			return true;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00019C18 File Offset: 0x00017E18
		public bool FormatObject(object value, string format, IFormatProvider formatProvider, StringBuilder builder)
		{
			if (this.SerializeSimpleObject(value, format, formatProvider, builder, false))
			{
				return true;
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable != null)
			{
				return this.SerializeWithoutCyclicLoop(enumerable, format, formatProvider, builder, default(SingleItemOptimizedHashSet<object>), 0);
			}
			ValueFormatter.SerializeConvertToString(value, formatProvider, builder);
			return true;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00019C60 File Offset: 0x00017E60
		private bool SerializeSimpleObject(object value, string format, IFormatProvider formatProvider, StringBuilder builder, bool convertToString = true)
		{
			string text;
			if ((text = value as string) != null)
			{
				ValueFormatter.SerializeStringObject(text, format, builder);
				return true;
			}
			if (value == null)
			{
				builder.Append("NULL");
				return true;
			}
			IConvertible convertible;
			if ((convertible = value as IConvertible) != null)
			{
				this.SerializeConvertibleObject(convertible, format, formatProvider, builder);
				return true;
			}
			IFormattable formattable;
			if (!string.IsNullOrEmpty(format) && (formattable = value as IFormattable) != null)
			{
				builder.Append(formattable.ToString(format, formatProvider));
				return true;
			}
			if (convertToString)
			{
				ValueFormatter.SerializeConvertToString(value, formatProvider, builder);
				return true;
			}
			return false;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00019CE0 File Offset: 0x00017EE0
		private void SerializeConvertibleObject(IConvertible value, string format, IFormatProvider formatProvider, StringBuilder builder)
		{
			TypeCode typeCode = value.GetTypeCode();
			if (typeCode == TypeCode.String)
			{
				ValueFormatter.SerializeStringObject(value.ToString(), format, builder);
				return;
			}
			IFormattable formattable;
			if (!string.IsNullOrEmpty(format) && (formattable = value as IFormattable) != null)
			{
				builder.Append(formattable.ToString(format, formatProvider));
				return;
			}
			switch (typeCode)
			{
			case TypeCode.Boolean:
				builder.Append(value.ToBoolean(CultureInfo.InvariantCulture) ? "true" : "false");
				return;
			case TypeCode.Char:
			{
				bool flag = format != "l";
				if (flag)
				{
					builder.Append('"');
				}
				builder.Append(value.ToChar(CultureInfo.InvariantCulture));
				if (flag)
				{
					builder.Append('"');
					return;
				}
				return;
			}
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			{
				Enum @enum;
				if ((@enum = value as Enum) != null)
				{
					this.AppendEnumAsString(builder, @enum);
					return;
				}
				builder.AppendIntegerAsString(value, typeCode);
				return;
			}
			}
			ValueFormatter.SerializeConvertToString(value, formatProvider, builder);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00019DE6 File Offset: 0x00017FE6
		private static void SerializeConvertToString(object value, IFormatProvider formatProvider, StringBuilder builder)
		{
			builder.Append(Convert.ToString(value, formatProvider));
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00019DF6 File Offset: 0x00017FF6
		private static void SerializeStringObject(string stringValue, string format, StringBuilder builder)
		{
			bool flag = format != "l";
			if (flag)
			{
				builder.Append('"');
			}
			builder.Append(stringValue);
			if (flag)
			{
				builder.Append('"');
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00019E24 File Offset: 0x00018024
		private void AppendEnumAsString(StringBuilder sb, Enum value)
		{
			string text;
			if (!this._enumCache.TryGetValue(value, out text))
			{
				text = value.ToString();
				this._enumCache.TryAddValue(value, text);
			}
			sb.Append(text);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00019E60 File Offset: 0x00018060
		private bool SerializeWithoutCyclicLoop(IEnumerable collection, string format, IFormatProvider formatProvider, StringBuilder builder, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			if (objectsInPath.Contains(collection))
			{
				return false;
			}
			if (depth > 2)
			{
				return false;
			}
			IDictionary dictionary = collection as IDictionary;
			if (dictionary != null)
			{
				using (new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(dictionary, ref objectsInPath, true, ValueFormatter._referenceEqualsComparer))
				{
					return this.SerializeDictionaryObject(dictionary, format, formatProvider, builder, objectsInPath, depth);
				}
			}
			bool flag;
			using (new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(collection, ref objectsInPath, true, ValueFormatter._referenceEqualsComparer))
			{
				flag = this.SerializeCollectionObject(collection, format, formatProvider, builder, objectsInPath, depth);
			}
			return flag;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00019F08 File Offset: 0x00018108
		private bool SerializeDictionaryObject(IDictionary dictionary, string format, IFormatProvider formatProvider, StringBuilder builder, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			bool flag = false;
			foreach (DictionaryEntry dictionaryEntry in new DictionaryEntryEnumerable(dictionary))
			{
				if (builder.Length > 524288)
				{
					return false;
				}
				if (flag)
				{
					builder.Append(", ");
				}
				this.SerializeCollectionItem(dictionaryEntry.Key, format, formatProvider, builder, ref objectsInPath, depth);
				builder.Append("=");
				this.SerializeCollectionItem(dictionaryEntry.Value, format, formatProvider, builder, ref objectsInPath, depth);
				flag = true;
			}
			return true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00019FBC File Offset: 0x000181BC
		private bool SerializeCollectionObject(IEnumerable collection, string format, IFormatProvider formatProvider, StringBuilder builder, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			bool flag = false;
			foreach (object obj in collection)
			{
				if (builder.Length > 524288)
				{
					return false;
				}
				if (flag)
				{
					builder.Append(", ");
				}
				this.SerializeCollectionItem(obj, format, formatProvider, builder, ref objectsInPath, depth);
				flag = true;
			}
			return true;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001A040 File Offset: 0x00018240
		private void SerializeCollectionItem(object item, string format, IFormatProvider formatProvider, StringBuilder builder, ref SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			IConvertible convertible;
			if ((convertible = item as IConvertible) != null)
			{
				this.SerializeConvertibleObject(convertible, format, formatProvider, builder);
				return;
			}
			IEnumerable enumerable;
			if ((enumerable = item as IEnumerable) != null)
			{
				this.SerializeWithoutCyclicLoop(enumerable, format, formatProvider, builder, objectsInPath, depth + 1);
				return;
			}
			this.SerializeSimpleObject(item, format, formatProvider, builder, true);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001A094 File Offset: 0x00018294
		public static void FormatToString(object value, string format, IFormatProvider formatProvider, StringBuilder builder)
		{
			string text = value as string;
			if (text != null)
			{
				builder.Append(text);
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				builder.Append(formattable.ToString(format, formatProvider));
				return;
			}
			builder.Append(Convert.ToString(value, formatProvider));
		}

		// Token: 0x04000248 RID: 584
		private static IValueFormatter _instance;

		// Token: 0x04000249 RID: 585
		private static readonly IEqualityComparer<object> _referenceEqualsComparer = SingleItemOptimizedHashSet<object>.ReferenceEqualityComparer.Default;

		// Token: 0x0400024A RID: 586
		private const int MaxRecursionDepth = 2;

		// Token: 0x0400024B RID: 587
		private const int MaxValueLength = 524288;

		// Token: 0x0400024C RID: 588
		private const string LiteralFormatSymbol = "l";

		// Token: 0x0400024D RID: 589
		private readonly MruCache<Enum, string> _enumCache = new MruCache<Enum, string>(1500);

		// Token: 0x0400024E RID: 590
		public const string FormatAsJson = "@";

		// Token: 0x0400024F RID: 591
		public const string FormatAsString = "$";
	}
}
