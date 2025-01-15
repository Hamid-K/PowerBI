using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000CB RID: 203
	[NullableContext(2)]
	[Nullable(0)]
	internal class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0002DE0D File Offset: 0x0002C00D
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002DE31 File Offset: 0x0002C031
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002DE39 File Offset: 0x0002C039
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

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002DE50 File Offset: 0x0002C050
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002DE71 File Offset: 0x0002C071
		public JTokenWriter()
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002DE79 File Offset: 0x0002C079
		public override void Flush()
		{
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002DE7B File Offset: 0x0002C07B
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002DE83 File Offset: 0x0002C083
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002DE96 File Offset: 0x0002C096
		[NullableContext(1)]
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

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002DF15 File Offset: 0x0002C115
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002DF28 File Offset: 0x0002C128
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002DF3D File Offset: 0x0002C13D
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002DF45 File Offset: 0x0002C145
		[NullableContext(1)]
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

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002DF72 File Offset: 0x0002C172
		private void AddRawValue(object value, JTokenType type, JsonToken token)
		{
			this.AddJValue(new JValue(value, type), token);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002DF84 File Offset: 0x0002C184
		internal void AddJValue(JValue value, JsonToken token)
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

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002E016 File Offset: 0x0002C216
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddJValue(JValue.CreateNull(), JsonToken.Null);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002E02B File Offset: 0x0002C22B
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddJValue(JValue.CreateUndefined(), JsonToken.Undefined);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002E040 File Offset: 0x0002C240
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddJValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002E056 File Offset: 0x0002C256
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddJValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002E06C File Offset: 0x0002C26C
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002E083 File Offset: 0x0002C283
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002E09A File Offset: 0x0002C29A
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002E0B1 File Offset: 0x0002C2B1
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002E0C7 File Offset: 0x0002C2C7
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002E0DD File Offset: 0x0002C2DD
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002E0F3 File Offset: 0x0002C2F3
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002E109 File Offset: 0x0002C309
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Boolean);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002E120 File Offset: 0x0002C320
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002E137 File Offset: 0x0002C337
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002E150 File Offset: 0x0002C350
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddJValue(new JValue(text), JsonToken.String);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002E17F File Offset: 0x0002C37F
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002E196 File Offset: 0x0002C396
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002E1AD File Offset: 0x0002C3AD
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002E1C3 File Offset: 0x0002C3C3
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002E1E8 File Offset: 0x0002C3E8
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002E1FF File Offset: 0x0002C3FF
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value, JTokenType.Bytes), JsonToken.Bytes);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002E218 File Offset: 0x0002C418
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002E22F File Offset: 0x0002C42F
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002E246 File Offset: 0x0002C446
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E260 File Offset: 0x0002C460
		[NullableContext(1)]
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

		// Token: 0x040003B9 RID: 953
		private JContainer _token;

		// Token: 0x040003BA RID: 954
		private JContainer _parent;

		// Token: 0x040003BB RID: 955
		private JValue _value;

		// Token: 0x040003BC RID: 956
		private JToken _current;
	}
}
