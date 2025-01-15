using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000CA RID: 202
	[NullableContext(2)]
	[Nullable(0)]
	internal class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x0002D6E5 File Offset: 0x0002B8E5
		[NullableContext(0)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002D709 File Offset: 0x0002B909
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002D711 File Offset: 0x0002B911
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002D728 File Offset: 0x0002B928
		[NullableContext(0)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002D749 File Offset: 0x0002B949
		public JTokenWriter()
		{
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002D751 File Offset: 0x0002B951
		public override void Flush()
		{
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D753 File Offset: 0x0002B953
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D75B File Offset: 0x0002B95B
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002D76E File Offset: 0x0002B96E
		[NullableContext(0)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002D79C File Offset: 0x0002B99C
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D7ED File Offset: 0x0002B9ED
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D800 File Offset: 0x0002BA00
		[NullableContext(0)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002D815 File Offset: 0x0002BA15
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D81D File Offset: 0x0002BA1D
		[NullableContext(0)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D84A File Offset: 0x0002BA4A
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D85C File Offset: 0x0002BA5C
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent.TryAdd(value))
				{
					this._current = this._parent.Last;
					if (this._parent.Type == JTokenType.Property)
					{
						this._parent = this._parent.Parent;
						return;
					}
				}
			}
			else
			{
				this._value = value ?? JValue.CreateNull();
				this._current = this._value;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002D8CC File Offset: 0x0002BACC
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D8ED File Offset: 0x0002BAED
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D8FE File Offset: 0x0002BAFE
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D90F File Offset: 0x0002BB0F
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002D925 File Offset: 0x0002BB25
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002D93B File Offset: 0x0002BB3B
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002D94D File Offset: 0x0002BB4D
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002D963 File Offset: 0x0002BB63
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002D98F File Offset: 0x0002BB8F
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002D9A5 File Offset: 0x0002BBA5
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002D9BB File Offset: 0x0002BBBB
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002D9D1 File Offset: 0x0002BBD1
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D9E8 File Offset: 0x0002BBE8
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D9FE File Offset: 0x0002BBFE
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002DA14 File Offset: 0x0002BC14
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddValue(text, JsonToken.String);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002DA3E File Offset: 0x0002BC3E
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002DA54 File Offset: 0x0002BC54
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002DA6A File Offset: 0x0002BC6A
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002DA80 File Offset: 0x0002BC80
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002DAA5 File Offset: 0x0002BCA5
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002DABC File Offset: 0x0002BCBC
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002DACE File Offset: 0x0002BCCE
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002DAE5 File Offset: 0x0002BCE5
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002DB10 File Offset: 0x0002BD10
		[NullableContext(0)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken();
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = jtoken as JContainer;
					this._value = jtoken as JValue;
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x0400039D RID: 925
		private JContainer _token;

		// Token: 0x0400039E RID: 926
		private JContainer _parent;

		// Token: 0x0400039F RID: 927
		private JValue _value;

		// Token: 0x040003A0 RID: 928
		private JToken _current;
	}
}
