using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000CB RID: 203
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0002DF35 File Offset: 0x0002C135
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002DF59 File Offset: 0x0002C159
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002DF61 File Offset: 0x0002C161
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

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002DF78 File Offset: 0x0002C178
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002DF99 File Offset: 0x0002C199
		public JTokenWriter()
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002DFA1 File Offset: 0x0002C1A1
		public override void Flush()
		{
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002DFA3 File Offset: 0x0002C1A3
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002DFAB File Offset: 0x0002C1AB
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002DFBE File Offset: 0x0002C1BE
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

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002DFEC File Offset: 0x0002C1EC
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002E03D File Offset: 0x0002C23D
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002E050 File Offset: 0x0002C250
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002E065 File Offset: 0x0002C265
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002E06D File Offset: 0x0002C26D
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

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002E09A File Offset: 0x0002C29A
		private void AddRawValue(object value, JTokenType type, JsonToken token)
		{
			this.AddJValue(new JValue(value, type), token);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002E0AC File Offset: 0x0002C2AC
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

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002E11C File Offset: 0x0002C31C
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

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002E13E File Offset: 0x0002C33E
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddJValue(JValue.CreateNull(), JsonToken.Null);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002E153 File Offset: 0x0002C353
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddJValue(JValue.CreateUndefined(), JsonToken.Undefined);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002E168 File Offset: 0x0002C368
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddJValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002E17E File Offset: 0x0002C37E
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddJValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002E194 File Offset: 0x0002C394
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002E1AB File Offset: 0x0002C3AB
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002E1C2 File Offset: 0x0002C3C2
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002E1D9 File Offset: 0x0002C3D9
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002E1EF File Offset: 0x0002C3EF
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002E205 File Offset: 0x0002C405
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002E21B File Offset: 0x0002C41B
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002E231 File Offset: 0x0002C431
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Boolean);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002E248 File Offset: 0x0002C448
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002E25F File Offset: 0x0002C45F
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002E278 File Offset: 0x0002C478
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddJValue(new JValue(text), JsonToken.String);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E2A7 File Offset: 0x0002C4A7
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002E2BE File Offset: 0x0002C4BE
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002E2D5 File Offset: 0x0002C4D5
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002E2EB File Offset: 0x0002C4EB
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002E310 File Offset: 0x0002C510
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E327 File Offset: 0x0002C527
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value, JTokenType.Bytes), JsonToken.Bytes);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002E340 File Offset: 0x0002C540
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002E357 File Offset: 0x0002C557
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E36E File Offset: 0x0002C56E
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002E388 File Offset: 0x0002C588
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
			JToken jtoken = jtokenReader.CurrentToken.CloneToken(null);
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

		// Token: 0x040003BA RID: 954
		private JContainer _token;

		// Token: 0x040003BB RID: 955
		private JContainer _parent;

		// Token: 0x040003BC RID: 956
		private JValue _value;

		// Token: 0x040003BD RID: 957
		private JToken _current;
	}
}
