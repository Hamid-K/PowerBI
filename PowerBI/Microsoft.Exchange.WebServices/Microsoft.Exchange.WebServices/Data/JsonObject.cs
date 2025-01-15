using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D7 RID: 215
	internal class JsonObject : Dictionary<string, object>
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00023944 File Offset: 0x00022944
		private static void ValidateObject(object entry)
		{
			if (entry != null && !(entry is JsonObject) && !(entry is Enum) && !(entry is bool) && !(entry is string) && !(entry is int) && !(entry is float) && !(entry is double) && !(entry is long) && !(entry is DateTime) && !entry.GetType().IsArray)
			{
				throw new InvalidOperationException(string.Format("Object [{0}] in the array is not serializable to JSON", entry));
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000239BA File Offset: 0x000229BA
		internal JsonObject()
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000239C2 File Offset: 0x000229C2
		internal void Add(string name, JsonObject value)
		{
			this.InternalAdd(name, value);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000239CC File Offset: 0x000229CC
		internal void Add(string name, string value)
		{
			if (value != null)
			{
				this.InternalAdd(name, value);
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000239D9 File Offset: 0x000229D9
		internal void Add(string name, Enum value)
		{
			if (value != null)
			{
				this.InternalAdd(name, value.ToString());
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000239EB File Offset: 0x000229EB
		internal void Add(string name, bool value)
		{
			this.InternalAdd(name, value);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000239FA File Offset: 0x000229FA
		internal void Add(string name, int value)
		{
			this.InternalAdd(name, value);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00023A09 File Offset: 0x00022A09
		internal void Add(string name, float value)
		{
			this.InternalAdd(name, value);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00023A18 File Offset: 0x00022A18
		internal void Add(string name, double value)
		{
			this.InternalAdd(name, value);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00023A27 File Offset: 0x00022A27
		internal void Add(string name, long value)
		{
			this.InternalAdd(name, value);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00023A36 File Offset: 0x00022A36
		public void Add(string name, object value)
		{
			JsonObject.ValidateObject(value);
			this.InternalAdd(name, value);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00023A46 File Offset: 0x00022A46
		private void InternalAdd(string name, object value)
		{
			base.Add(name, value);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00023A50 File Offset: 0x00022A50
		internal void AddTypeParameter(string typeName)
		{
			this.InternalAdd("__type", string.Format("{0}:#{1}", typeName, "Exchange"));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00023A70 File Offset: 0x00022A70
		internal void Add(string name, IEnumerable<object> value)
		{
			object[] array = Enumerable.ToArray<object>(value);
			foreach (object obj in array)
			{
				JsonObject.ValidateObject(obj);
			}
			this.InternalAdd(name, array);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00023AA8 File Offset: 0x00022AA8
		internal void AddBase64(string key, Stream stream)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[4096];
				int num;
				while ((num = stream.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, num);
				}
				this.AddBase64(key, memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00023B14 File Offset: 0x00022B14
		internal void AddBase64(string key, byte[] buffer)
		{
			this.AddBase64(key, buffer, 0, buffer.Length);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00023B22 File Offset: 0x00022B22
		internal void AddBase64(string key, byte[] buffer, int offset, int count)
		{
			this.InternalAdd(key, Convert.ToBase64String(buffer, offset, count));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00023B34 File Offset: 0x00022B34
		internal void SerializeToJson(Stream stream)
		{
			this.SerializeToJson(stream, false);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00023B40 File Offset: 0x00022B40
		internal void SerializeToJson(Stream stream, bool prettyPrint)
		{
			using (JsonWriter jsonWriter = new JsonWriter(stream, prettyPrint))
			{
				this.WriteValue(jsonWriter, this);
				jsonWriter.Flush();
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00023B80 File Offset: 0x00022B80
		private void WriteKeyValuePair(JsonWriter writer, string key, object value)
		{
			writer.WriteKey(key);
			this.WriteValue(writer, value);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00023B94 File Offset: 0x00022B94
		private void WriteValue(JsonWriter writer, object value)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			if (value is string)
			{
				writer.WriteValue((string)value);
				return;
			}
			if (value.GetType().IsEnum)
			{
				writer.WriteValue((Enum)value);
				return;
			}
			if (value is int)
			{
				writer.WriteValue((int)value);
				return;
			}
			if (value is long)
			{
				writer.WriteValue((long)value);
				return;
			}
			if (value is float)
			{
				writer.WriteValue((float)value);
				return;
			}
			if (value is double)
			{
				writer.WriteValue((double)value);
				return;
			}
			if (value is bool)
			{
				writer.WriteValue((bool)value);
				return;
			}
			if (value is DateTime)
			{
				writer.WriteValue((DateTime)value);
				return;
			}
			if (value is JsonObject)
			{
				writer.PushObjectClosure();
				JsonObject jsonObject = value as JsonObject;
				if (jsonObject.ContainsKey("__type"))
				{
					this.WriteKeyValuePair(writer, "__type", jsonObject["__type"]);
				}
				foreach (KeyValuePair<string, object> keyValuePair in jsonObject)
				{
					if (keyValuePair.Key != "__type")
					{
						this.WriteKeyValuePair(writer, keyValuePair.Key, keyValuePair.Value);
					}
				}
				writer.PopClosure();
				return;
			}
			if (value.GetType().IsArray)
			{
				writer.PushArrayClosure();
				foreach (object obj in ((Array)value))
				{
					this.WriteValue(writer, obj);
				}
				writer.PopClosure();
				return;
			}
			throw new InvalidOperationException(string.Format("Object [{0}] is not JSON serializable", value));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00023D74 File Offset: 0x00022D74
		internal int ReadAsInt(string key)
		{
			if (!base.ContainsKey(key))
			{
				throw new ServiceJsonDeserializationException();
			}
			object obj = base[key];
			if (!(obj is long))
			{
				throw new ServiceJsonDeserializationException();
			}
			return (int)((long)obj);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00023DB0 File Offset: 0x00022DB0
		internal double ReadAsDouble(string key)
		{
			if (!base.ContainsKey(key))
			{
				throw new ServiceJsonDeserializationException();
			}
			object obj = base[key];
			if (!(obj is long))
			{
				throw new ServiceJsonDeserializationException();
			}
			return (double)((long)obj);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00023DEC File Offset: 0x00022DEC
		internal string ReadAsString(string key)
		{
			if (!base.ContainsKey(key))
			{
				throw new ServiceJsonDeserializationException();
			}
			object obj = base[key];
			if (obj != null && !(obj is string))
			{
				throw new ServiceJsonDeserializationException();
			}
			return obj as string;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00023E28 File Offset: 0x00022E28
		internal JsonObject ReadAsJsonObject(string key)
		{
			if (!base.ContainsKey(key))
			{
				throw new ServiceJsonDeserializationException();
			}
			object obj = base[key];
			if (obj != null && !(obj is JsonObject))
			{
				throw new ServiceJsonDeserializationException();
			}
			return obj as JsonObject;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00023E64 File Offset: 0x00022E64
		internal object[] ReadAsArray(string key)
		{
			if (!base.ContainsKey(key))
			{
				return new object[0];
			}
			object obj = base[key];
			if (obj != null && !(obj is object[]))
			{
				throw new ServiceJsonDeserializationException();
			}
			return obj as object[];
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00023EA0 File Offset: 0x00022EA0
		internal bool HasTypeProperty()
		{
			return base.ContainsKey("__type");
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00023EB0 File Offset: 0x00022EB0
		internal string ReadTypeString()
		{
			if (!this.HasTypeProperty())
			{
				throw new ServiceJsonDeserializationException();
			}
			string text = this.ReadAsString("__type");
			return text.Substring(0, text.IndexOf(string.Format(":#{0}", "Exchange")));
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00023EF3 File Offset: 0x00022EF3
		internal T ReadEnumValue<T>(string key)
		{
			return EwsUtilities.Parse<T>(this.ReadAsString(key));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00023F04 File Offset: 0x00022F04
		internal bool ReadAsBool(string key)
		{
			if (!base.ContainsKey(key))
			{
				throw new ServiceJsonDeserializationException();
			}
			object obj = base[key];
			if (!(obj is bool))
			{
				throw new ServiceJsonDeserializationException();
			}
			return (bool)obj;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00023F3C File Offset: 0x00022F3C
		internal void ReadAsBase64Content(string key, Stream stream)
		{
			byte[] array = this.ReadAsBase64Content(key);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00023F5C File Offset: 0x00022F5C
		internal byte[] ReadAsBase64Content(string key)
		{
			string text = this.ReadAsString(key);
			return Convert.FromBase64String(text);
		}

		// Token: 0x0400031D RID: 797
		private const string TypeAttribute = "__type";

		// Token: 0x0400031E RID: 798
		private const string JsonTypeNamespace = "Exchange";

		// Token: 0x0400031F RID: 799
		internal const string JsonValueString = "Value";
	}
}
