using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000070 RID: 112
	internal class JsonSerializationWriter : ISerializationWriter
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public JsonSerializationWriter(TextWriter textWriter)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000F4DF File Offset: 0x0000D6DF
		public void WriteStartObject()
		{
			this.textWriter.Write('{');
			this.currentObjectHasProperties = false;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000F4F5 File Offset: 0x0000D6F5
		public void WriteStartObject(string name)
		{
			this.WritePropertyName(name);
			this.textWriter.Write('{');
			this.currentObjectHasProperties = false;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F512 File Offset: 0x0000D712
		public void WriteProperty(string name, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.WritePropertyName(name);
				this.WriteString(value);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F52C File Offset: 0x0000D72C
		public void WriteProperty(string name, int? value)
		{
			if (value != null)
			{
				this.WritePropertyName(name);
				this.textWriter.Write(value.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000F568 File Offset: 0x0000D768
		public void WriteProperty(string name, bool? value)
		{
			if (value != null)
			{
				this.WritePropertyName(name);
				this.textWriter.Write(value.Value ? "true" : "false");
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F59C File Offset: 0x0000D79C
		public void WriteProperty(string name, double? value)
		{
			if (value != null)
			{
				this.WritePropertyName(name);
				this.textWriter.Write(value.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		public void WriteProperty(string name, TimeSpan? value)
		{
			if (value != null)
			{
				this.WriteProperty(name, value.Value.ToString(string.Empty, CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F610 File Offset: 0x0000D810
		public void WriteProperty(string name, DateTimeOffset? value)
		{
			if (value != null)
			{
				this.WriteProperty(name, value.Value.ToString("o", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F648 File Offset: 0x0000D848
		public void WriteProperty(string name, IList<string> items)
		{
			bool flag = false;
			if (items != null && items.Count > 0)
			{
				this.WritePropertyName(name);
				this.WriteStartArray();
				foreach (string text in items)
				{
					if (flag)
					{
						this.WriteComma();
					}
					this.WriteString(text);
					flag = true;
				}
				this.WriteEndArray();
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		public void WriteProperty(string name, IList<ISerializableWithWriter> items)
		{
			bool flag = false;
			if (items != null && items.Count > 0)
			{
				this.WritePropertyName(name);
				this.WriteStartArray();
				foreach (ISerializableWithWriter serializableWithWriter in items)
				{
					if (flag)
					{
						this.WriteComma();
					}
					this.WriteStartObject();
					serializableWithWriter.Serialize(this);
					flag = true;
					this.WriteEndObject();
				}
				this.WriteEndArray();
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000F73C File Offset: 0x0000D93C
		public void WriteProperty(string name, ISerializableWithWriter value)
		{
			if (value != null)
			{
				this.WriteStartObject(name);
				value.Serialize(this);
				this.WriteEndObject();
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000F755 File Offset: 0x0000D955
		public void WriteProperty(ISerializableWithWriter value)
		{
			if (value != null)
			{
				value.Serialize(this);
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000F764 File Offset: 0x0000D964
		public void WriteProperty(string name, IDictionary<string, double> values)
		{
			if (values != null && values.Count > 0)
			{
				this.WritePropertyName(name);
				this.WriteStartObject();
				foreach (KeyValuePair<string, double> keyValuePair in values)
				{
					this.WriteProperty(keyValuePair.Key, new double?(keyValuePair.Value));
				}
				this.WriteEndObject();
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public void WriteProperty(string name, IDictionary<string, string> values)
		{
			if (values != null && values.Count > 0)
			{
				this.WritePropertyName(name);
				this.WriteStartObject();
				foreach (KeyValuePair<string, string> keyValuePair in values)
				{
					this.WriteProperty(keyValuePair.Key, keyValuePair.Value);
				}
				this.WriteEndObject();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000F854 File Offset: 0x0000DA54
		public void WriteEndObject()
		{
			this.textWriter.Write('}');
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F864 File Offset: 0x0000DA64
		internal void WritePropertyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			if (this.currentObjectHasProperties)
			{
				this.textWriter.Write(',');
			}
			else
			{
				this.currentObjectHasProperties = true;
			}
			this.WriteString(name);
			this.textWriter.Write(':');
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F8C9 File Offset: 0x0000DAC9
		internal void WriteStartArray()
		{
			this.textWriter.Write('[');
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		internal void WriteEndArray()
		{
			this.textWriter.Write(']');
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000F8E7 File Offset: 0x0000DAE7
		internal void WriteComma()
		{
			this.textWriter.Write(',');
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000F8F6 File Offset: 0x0000DAF6
		internal void WriteRawValue(object value)
		{
			this.textWriter.Write(string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { value }));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000F91C File Offset: 0x0000DB1C
		internal void WriteString(string value)
		{
			this.textWriter.Write('"');
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (c)
				{
				case '\b':
					this.textWriter.Write("\\b");
					break;
				case '\t':
					this.textWriter.Write("\\t");
					break;
				case '\n':
					this.textWriter.Write("\\n");
					break;
				case '\v':
					goto IL_00D2;
				case '\f':
					this.textWriter.Write("\\f");
					break;
				case '\r':
					this.textWriter.Write("\\r");
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							goto IL_00D2;
						}
						this.textWriter.Write("\\\\");
					}
					else
					{
						this.textWriter.Write("\\\"");
					}
					break;
				}
				IL_0116:
				i++;
				continue;
				IL_00D2:
				if (!char.IsControl(c))
				{
					this.textWriter.Write(c);
					goto IL_0116;
				}
				this.textWriter.Write("\\u");
				TextWriter textWriter = this.textWriter;
				ushort num = (ushort)c;
				textWriter.Write(num.ToString("x4", CultureInfo.InvariantCulture));
				goto IL_0116;
			}
			this.textWriter.Write('"');
		}

		// Token: 0x0400016B RID: 363
		private readonly TextWriter textWriter;

		// Token: 0x0400016C RID: 364
		private bool currentObjectHasProperties;
	}
}
