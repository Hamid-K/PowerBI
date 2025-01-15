using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E4F RID: 3663
	internal class JsonSerializer
	{
		// Token: 0x06006262 RID: 25186 RVA: 0x00151E34 File Offset: 0x00150034
		public string Serialize(object obj)
		{
			string text;
			try
			{
				this.sb = new StringBuilder();
				this.depth = 0;
				this.startOfLine = true;
				this.Append(obj);
				text = this.sb.ToString();
			}
			finally
			{
				this.sb = null;
			}
			return text;
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00151E88 File Offset: 0x00150088
		private void Append(object obj)
		{
			if (obj == null)
			{
				this.Append("null");
				return;
			}
			IJsonSerializable jsonSerializable = obj as IJsonSerializable;
			if (jsonSerializable != null)
			{
				this.Append(jsonSerializable.ToJson());
				return;
			}
			Value value = obj as Value;
			if (value != null)
			{
				this.AppendValue(value);
				return;
			}
			Type type = obj.GetType();
			type = Nullable.GetUnderlyingType(type) ?? type;
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode == TypeCode.Object)
			{
				this.AppendObjectOrArray(obj);
				return;
			}
			if (typeCode != TypeCode.Boolean)
			{
				switch (typeCode)
				{
				case TypeCode.Int16:
					this.AppendValue((long)((short)obj));
					return;
				case TypeCode.Int32:
					this.AppendValue((long)((int)obj));
					return;
				case TypeCode.Int64:
					this.AppendValue((long)obj);
					return;
				case TypeCode.Single:
					this.AppendValue((double)((float)obj));
					return;
				case TypeCode.Double:
					this.AppendValue((double)obj);
					return;
				case TypeCode.DateTime:
					this.AppendValue((DateTime)obj);
					return;
				case TypeCode.String:
					this.AppendValue((string)obj);
					return;
				}
				throw new NotSupportedException("Unsupported type '" + typeCode.ToString() + "'");
			}
			this.AppendValue((bool)obj);
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x00151FC0 File Offset: 0x001501C0
		private void AppendValue(Value value)
		{
			this.Append(JsonFormatter.FormatValue(value));
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00151FCE File Offset: 0x001501CE
		private void AppendValue(bool value)
		{
			this.Append(value ? "true" : "false");
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x00151FE5 File Offset: 0x001501E5
		private void AppendValue(string value)
		{
			this.Append(JsonFormatter.FormatString(value));
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x00151FF3 File Offset: 0x001501F3
		private void AppendValue(long value)
		{
			this.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00152007 File Offset: 0x00150207
		private void AppendValue(double value)
		{
			this.Append(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x0015201B File Offset: 0x0015021B
		private void AppendValue(DateTime value)
		{
			this.AppendValue(new DateTimeOffset(value));
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x00152029 File Offset: 0x00150229
		private void AppendValue(DateTimeOffset value)
		{
			this.AppendValue(DateTimeOffsetSerializer.ToString(new DateTimeOffset?(value)));
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x0015203C File Offset: 0x0015023C
		private void AppendObjectOrArray(object obj)
		{
			IEnumerable enumerable = obj as IEnumerable;
			if (enumerable != null)
			{
				this.AppendArray(enumerable);
				return;
			}
			this.AppendObject(obj);
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x00152064 File Offset: 0x00150264
		private void AppendObject(object obj)
		{
			this.AppendLine("{");
			using (this.NewScope())
			{
				bool flag = true;
				foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
				{
					DataMemberAttribute dataMemberAttribute = JsonSerializer.GetDataMemberAttribute(propertyInfo);
					if (dataMemberAttribute != null)
					{
						object value = propertyInfo.GetValue(obj, null);
						if (value != null || dataMemberAttribute.IsRequired)
						{
							if (!flag)
							{
								this.AppendLine(",");
							}
							flag = false;
							this.AppendValue(dataMemberAttribute.Name);
							this.Append(": ");
							this.Append(value);
						}
					}
				}
			}
			this.AppendLine();
			this.Append("}");
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x0015212C File Offset: 0x0015032C
		private void AppendArray(IEnumerable obj)
		{
			this.AppendLine("[");
			using (this.NewScope())
			{
				bool flag = true;
				foreach (object obj2 in obj)
				{
					if (!flag)
					{
						this.AppendLine(",");
					}
					flag = false;
					this.Append(obj2);
				}
			}
			this.AppendLine();
			this.Append("]");
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x001521D0 File Offset: 0x001503D0
		private void Append(string s)
		{
			if (this.startOfLine)
			{
				this.startOfLine = false;
				this.AppendIndent();
			}
			this.sb.Append(s);
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x001521F4 File Offset: 0x001503F4
		private void AppendLine()
		{
			this.AppendLine(string.Empty);
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x00152201 File Offset: 0x00150401
		private void AppendLine(string s)
		{
			this.Append(s);
			this.sb.AppendLine();
			this.startOfLine = true;
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x00152220 File Offset: 0x00150420
		private void AppendIndent()
		{
			for (int i = 0; i < this.depth; i++)
			{
				this.sb.Append("    ");
			}
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0015224F File Offset: 0x0015044F
		private JsonSerializer.Scope NewScope()
		{
			return new JsonSerializer.Scope(this);
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x00152257 File Offset: 0x00150457
		private static DataMemberAttribute GetDataMemberAttribute(MemberInfo info)
		{
			return (DataMemberAttribute)info.GetCustomAttributes(typeof(DataMemberAttribute), false).SingleOrDefault<object>();
		}

		// Token: 0x040035A6 RID: 13734
		private StringBuilder sb;

		// Token: 0x040035A7 RID: 13735
		private int depth;

		// Token: 0x040035A8 RID: 13736
		private bool startOfLine;

		// Token: 0x02000E50 RID: 3664
		private struct Scope : IDisposable
		{
			// Token: 0x06006275 RID: 25205 RVA: 0x00152274 File Offset: 0x00150474
			public Scope(JsonSerializer serializer)
			{
				this.serializer = serializer;
				this.serializer.depth++;
			}

			// Token: 0x06006276 RID: 25206 RVA: 0x00152290 File Offset: 0x00150490
			public void Dispose()
			{
				this.serializer.depth--;
			}

			// Token: 0x040035A9 RID: 13737
			private readonly JsonSerializer serializer;
		}
	}
}
