using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000114 RID: 276
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Microsoft.IdentityModel.Json.Bson for more details.")]
	internal class BsonWriter : JsonWriter
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00037081 File Offset: 0x00035281
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0003708E File Offset: 0x0003528E
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003709C File Offset: 0x0003529C
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000370C0 File Offset: 0x000352C0
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000370DF File Offset: 0x000352DF
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000370EC File Offset: 0x000352EC
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00037114 File Offset: 0x00035314
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00037122 File Offset: 0x00035322
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00037130 File Offset: 0x00035330
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003713E File Offset: 0x0003533E
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003714C File Offset: 0x0003534C
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003715F File Offset: 0x0003535F
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00037172 File Offset: 0x00035372
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00037182 File Offset: 0x00035382
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				BsonBinaryWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000371A2 File Offset: 0x000353A2
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000371B2 File Offset: 0x000353B2
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000371C5 File Offset: 0x000353C5
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000371D4 File Offset: 0x000353D4
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				BsonObject bsonObject = this._parent as BsonObject;
				if (bsonObject != null)
				{
					bsonObject.Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw JsonWriterException.Create(this, "Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, token.Type), null);
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00037264 File Offset: 0x00035464
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				base.SetWriteState(JsonToken.Integer, null);
				this.AddToken(new BsonBinary(bigInteger.ToByteArray(), BsonBinaryType.Binary));
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000372A3 File Offset: 0x000354A3
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddToken(BsonEmpty.Null);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000372B6 File Offset: 0x000354B6
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddToken(BsonEmpty.Undefined);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000372C9 File Offset: 0x000354C9
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddToken((value == null) ? BsonEmpty.Null : new BsonString(value, true));
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000372E9 File Offset: 0x000354E9
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00037300 File Offset: 0x00035500
		public override void WriteValue(uint value)
		{
			if (value > 2147483647U)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003732C File Offset: 0x0003552C
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00037343 File Offset: 0x00035543
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00037373 File Offset: 0x00035573
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00037389 File Offset: 0x00035589
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003739F File Offset: 0x0003559F
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddToken(value ? BsonBoolean.True : BsonBoolean.False);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000373BD File Offset: 0x000355BD
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000373D4 File Offset: 0x000355D4
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000373EC File Offset: 0x000355EC
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddToken(new BsonString(text, true));
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003741C File Offset: 0x0003561C
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00037433 File Offset: 0x00035633
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003744A File Offset: 0x0003564A
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00037460 File Offset: 0x00035660
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00037485 File Offset: 0x00035685
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003749C File Offset: 0x0003569C
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x000374BC File Offset: 0x000356BC
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000374D8 File Offset: 0x000356D8
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000374FA File Offset: 0x000356FA
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00037525 File Offset: 0x00035725
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00037557 File Offset: 0x00035757
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x04000460 RID: 1120
		private readonly BsonBinaryWriter _writer;

		// Token: 0x04000461 RID: 1121
		private BsonToken _root;

		// Token: 0x04000462 RID: 1122
		private BsonToken _parent;

		// Token: 0x04000463 RID: 1123
		private string _propertyName;
	}
}
