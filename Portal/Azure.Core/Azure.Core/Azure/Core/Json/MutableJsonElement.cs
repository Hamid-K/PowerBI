using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Core.Json
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The constructor is marked as RequiresUnreferencedCode, since the whole struct is incompatible with trimming.")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL3050", Justification = "The constructor is marked as RequiresDynamicCode, since the whole struct is incompatible with AOT.")]
	[JsonConverter(typeof(MutableJsonElement.MutableJsonElementConverter))]
	internal readonly struct MutableJsonElement
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00012509 File Offset: 0x00010709
		private MutableJsonDocument.ChangeTracker Changes
		{
			get
			{
				return this._root.Changes;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00012516 File Offset: 0x00010716
		[RequiresUnreferencedCode("This struct utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		[RequiresDynamicCode("This struct utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		internal MutableJsonElement(MutableJsonDocument root, JsonElement element, string path, int highWaterMark = -1)
		{
			this._element = element;
			this._root = root;
			this._path = path;
			this._highWaterMark = highWaterMark;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00012538 File Offset: 0x00010738
		public JsonValueKind? ValueKind
		{
			get
			{
				MutableJsonChange mutableJsonChange;
				if (this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
				{
					return new JsonValueKind?(mutableJsonChange.ValueKind);
				}
				return new JsonValueKind?(this._element.ValueKind);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001257D File Offset: 0x0001077D
		public MutableJsonElement GetProperty(string name)
		{
			return this.GetProperty(MemoryExtensions.AsSpan(name));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001258C File Offset: 0x0001078C
		[NullableContext(0)]
		public MutableJsonElement GetProperty(ReadOnlySpan<char> name)
		{
			MutableJsonElement mutableJsonElement;
			if (!this.TryGetProperty(name, out mutableJsonElement))
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"'",
					this._path,
					"' does not contain property called '",
					MutableJsonElement.GetString(name, 0, name.Length),
					"'"
				}));
			}
			return mutableJsonElement;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000125E8 File Offset: 0x000107E8
		public bool TryGetProperty(string name, out MutableJsonElement value)
		{
			return this.TryGetProperty(MemoryExtensions.AsSpan(name), out value);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000125F8 File Offset: 0x000107F8
		[NullableContext(0)]
		public unsafe bool TryGetProperty(ReadOnlySpan<char> name, out MutableJsonElement value)
		{
			this.EnsureValid();
			this.EnsureObject();
			char[] array = null;
			int num = name.Length;
			num += ((this._path.Length > 0) ? (this._path.Length + 1) : 0);
			checked
			{
				Span<char> span;
				if (num <= 1024)
				{
					int num2 = num;
					span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				}
				else
				{
					span = (array = ArrayPool<char>.Shared.Rent(num));
				}
				Span<char> span2 = span;
				bool flag;
				try
				{
					MemoryExtensions.AsSpan(this._path).CopyTo(span2);
					int length = this._path.Length;
					MutableJsonDocument.ChangeTracker.PushProperty(span2, ref length, name);
					MutableJsonChange mutableJsonChange;
					JsonElement jsonElement;
					if (this.Changes.TryGetChange(span2, in this._highWaterMark, out mutableJsonChange))
					{
						if (mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyRemoval)
						{
							value = default(MutableJsonElement);
							flag = false;
						}
						else
						{
							value = new MutableJsonElement(this._root, MutableJsonElement.SerializeToJsonElement(mutableJsonChange.Value, this._root.SerializerOptions), MutableJsonElement.GetString(span2, 0, length), mutableJsonChange.Index);
							flag = true;
						}
					}
					else if (!this._element.TryGetProperty(name, ref jsonElement))
					{
						value = default(MutableJsonElement);
						flag = false;
					}
					else
					{
						value = new MutableJsonElement(this._root, jsonElement, MutableJsonElement.GetString(span2, 0, length), this._highWaterMark);
						flag = true;
					}
				}
				finally
				{
					if (array != null)
					{
						ArrayPool<char>.Shared.Return(array, false);
					}
				}
				return flag;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00012778 File Offset: 0x00010978
		[NullableContext(0)]
		[return: Nullable(1)]
		private static string GetString(ReadOnlySpan<char> value, int start, int end)
		{
			return new string(value.Slice(start, end).ToArray());
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001279C File Offset: 0x0001099C
		public int GetArrayLength()
		{
			this.EnsureValid();
			this.EnsureArray();
			MutableJsonChange mutableJsonChange;
			if (this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return mutableJsonChange.GetArrayLength();
			}
			return this._element.GetArrayLength();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000127E4 File Offset: 0x000109E4
		internal MutableJsonElement GetIndexElement(int index)
		{
			this.EnsureValid();
			this.EnsureArray();
			string text = MutableJsonDocument.ChangeTracker.PushIndex(this._path, index);
			MutableJsonChange mutableJsonChange;
			if (this.Changes.TryGetChange(text, in this._highWaterMark, out mutableJsonChange))
			{
				return new MutableJsonElement(this._root, MutableJsonElement.SerializeToJsonElement(mutableJsonChange.Value, this._root.SerializerOptions), text, mutableJsonChange.Index);
			}
			return new MutableJsonElement(this._root, this._element[index], text, this._highWaterMark);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001286C File Offset: 0x00010A6C
		public bool TryGetDouble(out double value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetDouble(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is double)
			{
				double num = (double)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetDouble(ref value);
			}
			if (value2 != null)
			{
				value = (double)mutableJsonChange.Value;
				return true;
			}
			value = 0.0;
			return false;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00012908 File Offset: 0x00010B08
		public double GetDouble()
		{
			double num;
			if (!this.TryGetDouble(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(double)));
			}
			return num;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001293B File Offset: 0x00010B3B
		private static string GetFormatExceptionText(string path, Type type)
		{
			return string.Format("Element at '{0}' cannot be formatted as type '{1}.", path, type);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001294C File Offset: 0x00010B4C
		public bool TryGetInt32(out int value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetInt32(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is int)
			{
				int num = (int)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetInt32(ref value);
			}
			if (value2 != null)
			{
				value = (int)mutableJsonChange.Value;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000129E0 File Offset: 0x00010BE0
		public int GetInt32()
		{
			int num;
			if (!this.TryGetInt32(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(int)));
			}
			return num;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00012A14 File Offset: 0x00010C14
		public bool TryGetInt64(out long value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetInt64(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is long)
			{
				long num = (long)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetInt64(ref value);
			}
			if (value2 != null)
			{
				value = (long)mutableJsonChange.Value;
				return true;
			}
			value = 0L;
			return false;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public long GetInt64()
		{
			long num;
			if (!this.TryGetInt64(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(long)));
			}
			return num;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00012ADC File Offset: 0x00010CDC
		public bool TryGetSingle(out float value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetSingle(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is float)
			{
				float num = (float)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetSingle(ref value);
			}
			if (value2 != null)
			{
				value = (float)mutableJsonChange.Value;
				return true;
			}
			value = 0f;
			return false;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00012B74 File Offset: 0x00010D74
		public float GetSingle()
		{
			float num;
			if (!this.TryGetSingle(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(float)));
			}
			return num;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00012BA8 File Offset: 0x00010DA8
		[NullableContext(2)]
		public string GetString()
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.GetString();
			}
			mutableJsonChange.EnsureString();
			object value = mutableJsonChange.Value;
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value is JsonElement)
			{
				return ((JsonElement)value).GetString();
			}
			if (value != null)
			{
				throw new InvalidOperationException("Element at '" + this._path + "' is not a string.");
			}
			return null;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00012C38 File Offset: 0x00010E38
		public bool GetBoolean()
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				object value = mutableJsonChange.Value;
				bool flag2;
				if (value is bool)
				{
					bool flag = (bool)value;
					flag2 = flag;
				}
				else
				{
					if (!(value is JsonElement))
					{
						throw new InvalidOperationException("Element at '" + this._path + "' is not a bool.");
					}
					flag2 = ((JsonElement)value).GetBoolean();
				}
				return flag2;
			}
			return this._element.GetBoolean();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00012CCC File Offset: 0x00010ECC
		public bool TryGetByte(out byte value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetByte(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is byte)
			{
				byte b = (byte)value2;
				value = b;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetByte(ref value);
			}
			if (value2 != null)
			{
				value = (byte)mutableJsonChange.Value;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00012D60 File Offset: 0x00010F60
		public byte GetByte()
		{
			byte b;
			if (!this.TryGetByte(out b))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(byte)));
			}
			return b;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00012D94 File Offset: 0x00010F94
		public bool TryGetDateTime(out DateTime value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetDateTime(ref value);
			}
			mutableJsonChange.EnsureString();
			object value2 = mutableJsonChange.Value;
			if (value2 is DateTime)
			{
				DateTime dateTime = (DateTime)value2;
				value = dateTime;
				return true;
			}
			if (value2 is DateTimeOffset || value2 is string)
			{
				return MutableJsonElement.SerializeToJsonElement(mutableJsonChange.Value, this._root.SerializerOptions).TryGetDateTime(ref value);
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetDateTime(ref value);
			}
			if (value2 != null)
			{
				throw new InvalidOperationException(string.Format("Element {0} cannot be converted to DateTime.", mutableJsonChange.Value));
			}
			value = default(DateTime);
			return false;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00012E6C File Offset: 0x0001106C
		public DateTime GetDateTime()
		{
			DateTime dateTime;
			if (!this.TryGetDateTime(out dateTime))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(DateTime)));
			}
			return dateTime;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00012EA0 File Offset: 0x000110A0
		public bool TryGetDateTimeOffset(out DateTimeOffset value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetDateTimeOffset(ref value);
			}
			mutableJsonChange.EnsureString();
			object value2 = mutableJsonChange.Value;
			if (value2 is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value2;
				value = dateTimeOffset;
				return true;
			}
			if (value2 is DateTime || value2 is string)
			{
				return MutableJsonElement.SerializeToJsonElement(mutableJsonChange.Value, this._root.SerializerOptions).TryGetDateTimeOffset(ref value);
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetDateTimeOffset(ref value);
			}
			if (value2 != null)
			{
				throw new InvalidOperationException(string.Format("Element {0} cannot be converted to DateTimeOffset.", mutableJsonChange.Value));
			}
			value = default(DateTimeOffset);
			return false;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00012F78 File Offset: 0x00011178
		public DateTimeOffset GetDateTimeOffset()
		{
			DateTimeOffset dateTimeOffset;
			if (!this.TryGetDateTimeOffset(out dateTimeOffset))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(DateTimeOffset)));
			}
			return dateTimeOffset;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00012FAC File Offset: 0x000111AC
		public bool TryGetDecimal(out decimal value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetDecimal(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is decimal)
			{
				decimal num = (decimal)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetDecimal(ref value);
			}
			if (value2 != null)
			{
				value = (decimal)mutableJsonChange.Value;
				return true;
			}
			value = 0m;
			return false;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001304C File Offset: 0x0001124C
		public decimal GetDecimal()
		{
			decimal num;
			if (!this.TryGetDecimal(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(decimal)));
			}
			return num;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00013080 File Offset: 0x00011280
		public bool TryGetGuid(out Guid value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetGuid(ref value);
			}
			mutableJsonChange.EnsureString();
			object value2 = mutableJsonChange.Value;
			if (value2 is Guid)
			{
				Guid guid = (Guid)value2;
				value = guid;
				return true;
			}
			if (value2 is string)
			{
				return MutableJsonElement.SerializeToJsonElement(mutableJsonChange.Value, this._root.SerializerOptions).TryGetGuid(ref value);
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetGuid(ref value);
			}
			if (value2 != null)
			{
				throw new InvalidOperationException(string.Format("Element {0} cannot be converted to Guid.", mutableJsonChange.Value));
			}
			value = default(Guid);
			return false;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00013150 File Offset: 0x00011350
		public Guid GetGuid()
		{
			Guid guid;
			if (!this.TryGetGuid(out guid))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(Guid)));
			}
			return guid;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00013184 File Offset: 0x00011384
		public bool TryGetInt16(out short value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetInt16(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is short)
			{
				short num = (short)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetInt16(ref value);
			}
			if (value2 != null)
			{
				value = (short)mutableJsonChange.Value;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00013218 File Offset: 0x00011418
		public short GetInt16()
		{
			short num;
			if (!this.TryGetInt16(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(short)));
			}
			return num;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001324C File Offset: 0x0001144C
		public bool TryGetSByte(out sbyte value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetSByte(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is sbyte)
			{
				sbyte b = (sbyte)value2;
				value = b;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetSByte(ref value);
			}
			if (value2 != null)
			{
				value = (sbyte)mutableJsonChange.Value;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000132E0 File Offset: 0x000114E0
		public sbyte GetSByte()
		{
			sbyte b;
			if (!this.TryGetSByte(out b))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(sbyte)));
			}
			return b;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013314 File Offset: 0x00011514
		public bool TryGetUInt16(out ushort value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetUInt16(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is ushort)
			{
				ushort num = (ushort)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetUInt16(ref value);
			}
			if (value2 != null)
			{
				value = (ushort)mutableJsonChange.Value;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000133A8 File Offset: 0x000115A8
		public ushort GetUInt16()
		{
			ushort num;
			if (!this.TryGetUInt16(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(ushort)));
			}
			return num;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000133DC File Offset: 0x000115DC
		public bool TryGetUInt32(out uint value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetUInt32(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is uint)
			{
				uint num = (uint)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetUInt32(ref value);
			}
			if (value2 != null)
			{
				value = (uint)mutableJsonChange.Value;
				return true;
			}
			value = 0U;
			return false;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013470 File Offset: 0x00011670
		public uint GetUInt32()
		{
			uint num;
			if (!this.TryGetUInt32(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(uint)));
			}
			return num;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000134A4 File Offset: 0x000116A4
		public bool TryGetUInt64(out ulong value)
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (!this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return this._element.TryGetUInt64(ref value);
			}
			mutableJsonChange.EnsureNumber();
			object value2 = mutableJsonChange.Value;
			if (value2 is ulong)
			{
				ulong num = (ulong)value2;
				value = num;
				return true;
			}
			if (value2 is JsonElement)
			{
				return ((JsonElement)value2).TryGetUInt64(ref value);
			}
			if (value2 != null)
			{
				value = (ulong)mutableJsonChange.Value;
				return true;
			}
			value = 0UL;
			return false;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013538 File Offset: 0x00011738
		public ulong GetUInt64()
		{
			ulong num;
			if (!this.TryGetUInt64(out num))
			{
				throw new FormatException(MutableJsonElement.GetFormatExceptionText(this._path, typeof(ulong)));
			}
			return num;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001356B File Offset: 0x0001176B
		public MutableJsonElement.ArrayEnumerator EnumerateArray()
		{
			this.EnsureValid();
			this.EnsureArray();
			return new MutableJsonElement.ArrayEnumerator(this);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00013584 File Offset: 0x00011784
		public MutableJsonElement.ObjectEnumerator EnumerateObject()
		{
			this.EnsureValid();
			this.EnsureObject();
			return new MutableJsonElement.ObjectEnumerator(this);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000135A0 File Offset: 0x000117A0
		public void RemoveProperty(string name)
		{
			this.EnsureValid();
			this.EnsureObject();
			JsonElement jsonElement;
			if (!this._element.TryGetProperty(name, ref jsonElement))
			{
				throw new InvalidOperationException("Object does not have property: '" + name + "'.");
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, null, MutableJsonChangeKind.PropertyRemoval, null);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000135FC File Offset: 0x000117FC
		public void Set(double value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00013620 File Offset: 0x00011820
		public MutableJsonElement SetProperty(string name, double value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001366F File Offset: 0x0001186F
		public void Set(int value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00013694 File Offset: 0x00011894
		public MutableJsonElement SetProperty(string name, int value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000136E3 File Offset: 0x000118E3
		public void Set(long value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00013708 File Offset: 0x00011908
		public MutableJsonElement SetProperty(string name, long value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00013757 File Offset: 0x00011957
		public void Set(float value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001377C File Offset: 0x0001197C
		public MutableJsonElement SetProperty(string name, float value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000137CB File Offset: 0x000119CB
		public void Set(string value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000137E8 File Offset: 0x000119E8
		public MutableJsonElement SetProperty(string name, string value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00013832 File Offset: 0x00011A32
		public void SetNull()
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, null, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00013850 File Offset: 0x00011A50
		public MutableJsonElement SetPropertyNull(string name)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.SetNull();
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, null, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00013899 File Offset: 0x00011A99
		public void Set(bool value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000138BC File Offset: 0x00011ABC
		public MutableJsonElement SetProperty(string name, bool value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001390B File Offset: 0x00011B0B
		public void Set(byte value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00013930 File Offset: 0x00011B30
		public MutableJsonElement SetProperty(string name, byte value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001397F File Offset: 0x00011B7F
		public void Set(sbyte value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000139A4 File Offset: 0x00011BA4
		public MutableJsonElement SetProperty(string name, sbyte value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000139F3 File Offset: 0x00011BF3
		public void Set(short value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00013A18 File Offset: 0x00011C18
		public MutableJsonElement SetProperty(string name, short value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00013A67 File Offset: 0x00011C67
		public void Set(ushort value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00013A8C File Offset: 0x00011C8C
		public MutableJsonElement SetProperty(string name, ushort value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00013ADB File Offset: 0x00011CDB
		public void Set(uint value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00013B00 File Offset: 0x00011D00
		public MutableJsonElement SetProperty(string name, uint value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00013B4F File Offset: 0x00011D4F
		public void Set(ulong value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00013B74 File Offset: 0x00011D74
		public MutableJsonElement SetProperty(string name, ulong value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00013BC3 File Offset: 0x00011DC3
		public void Set(decimal value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00013BE8 File Offset: 0x00011DE8
		public MutableJsonElement SetProperty(string name, decimal value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00013C37 File Offset: 0x00011E37
		public void Set(Guid value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00013C5C File Offset: 0x00011E5C
		public MutableJsonElement SetProperty(string name, Guid value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00013CAB File Offset: 0x00011EAB
		public void Set(DateTime value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00013CD0 File Offset: 0x00011ED0
		public MutableJsonElement SetProperty(string name, DateTime value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00013D1F File Offset: 0x00011F1F
		public void Set(DateTimeOffset value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00013D44 File Offset: 0x00011F44
		public MutableJsonElement SetProperty(string name, DateTimeOffset value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00013D93 File Offset: 0x00011F93
		public void Set(JsonElement value)
		{
			this.EnsureValid();
			this.Changes.AddChange(this._path, value, MutableJsonChangeKind.PropertyUpdate, null);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00013DB8 File Offset: 0x00011FB8
		public MutableJsonElement SetProperty(string name, JsonElement value)
		{
			MutableJsonElement mutableJsonElement;
			if (this.TryGetProperty(name, out mutableJsonElement))
			{
				mutableJsonElement.Set(value);
				return this;
			}
			string text = MutableJsonDocument.ChangeTracker.PushProperty(this._path, name);
			this.Changes.AddChange(text, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00013E08 File Offset: 0x00012008
		public override string ToString()
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return mutableJsonChange.AsString();
			}
			if (this.Changes.DescendantChanged(this._path, this._highWaterMark))
			{
				return Encoding.UTF8.GetString(this.GetRawBytes());
			}
			return this._element.ToString() ?? "null";
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00013E82 File Offset: 0x00012082
		[NullableContext(2)]
		[RequiresUnreferencedCode("This method utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		[RequiresDynamicCode("This method utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		internal static JsonElement SerializeToJsonElement(object value, JsonSerializerOptions options = null)
		{
			return MutableJsonElement.ParseFromBytes(JsonSerializer.SerializeToUtf8Bytes<object>(value, options));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00013E90 File Offset: 0x00012090
		private static JsonElement ParseFromBytes(byte[] bytes)
		{
			JsonElement jsonElement;
			using (JsonDocument jsonDocument = JsonDocument.Parse(bytes, default(JsonDocumentOptions)))
			{
				jsonElement = jsonDocument.RootElement;
				jsonElement = jsonElement.Clone();
			}
			return jsonElement;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00013EE0 File Offset: 0x000120E0
		internal JsonElement GetJsonElement()
		{
			this.EnsureValid();
			MutableJsonChange mutableJsonChange;
			if (this.Changes.TryGetChange(this._path, in this._highWaterMark, out mutableJsonChange))
			{
				return MutableJsonElement.SerializeToJsonElement(mutableJsonChange.Value, this._root.SerializerOptions);
			}
			if (this.Changes.DescendantChanged(this._path, this._highWaterMark))
			{
				return MutableJsonElement.ParseFromBytes(this.GetRawBytes());
			}
			return this._element;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00013F54 File Offset: 0x00012154
		private byte[] GetRawBytes()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
				{
					this.WriteTo(utf8JsonWriter);
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00013FBC File Offset: 0x000121BC
		internal static Utf8JsonReader GetReaderForElement(JsonElement element)
		{
			Utf8JsonReader utf8JsonReader;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
				{
					element.WriteTo(utf8JsonWriter);
				}
				utf8JsonReader = new Utf8JsonReader(MemoryExtensions.AsSpan<byte>(memoryStream.GetBuffer()).Slice(0, (int)memoryStream.Position), default(JsonReaderOptions));
			}
			return utf8JsonReader;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00014050 File Offset: 0x00012250
		internal void DisposeRoot()
		{
			this._root.Dispose();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001405D File Offset: 0x0001225D
		private void EnsureObject()
		{
			if (this._element.ValueKind != 1)
			{
				throw new InvalidOperationException(string.Format("Expected an 'Object' type but was '{0}'.", this._element.ValueKind));
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001408D File Offset: 0x0001228D
		private void EnsureArray()
		{
			if (this._element.ValueKind != 2)
			{
				throw new InvalidOperationException(string.Format("Expected an 'Array' type but was '{0}'.", this._element.ValueKind));
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000140BD File Offset: 0x000122BD
		private void EnsureValid()
		{
			if (this.Changes.AncestorChanged(this._path, this._highWaterMark))
			{
				throw new InvalidOperationException("An ancestor node of this element has unapplied changes.  Please re-request this property from the RootElement.");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000140E3 File Offset: 0x000122E3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal string DebuggerDisplay
		{
			get
			{
				return string.Format("ValueKind = {0} : \"{1}\"", this.ValueKind, this.ToString());
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00014106 File Offset: 0x00012306
		internal void WriteTo(Utf8JsonWriter writer, string format)
		{
			if (format == "J")
			{
				this.WriteTo(writer);
				return;
			}
			if (!(format == "P"))
			{
				this._root.AssertInvalidFormat(format);
				return;
			}
			this.WritePatch(writer);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00014140 File Offset: 0x00012340
		internal void WriteTo(Utf8JsonWriter writer)
		{
			this.WriteElement(this._path, this._highWaterMark, this._element, writer);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001415C File Offset: 0x0001235C
		private void WriteElement(string path, int highWaterMark, JsonElement element, Utf8JsonWriter writer)
		{
			MutableJsonChange mutableJsonChange;
			if (this.Changes.TryGetChange(path, in highWaterMark, out mutableJsonChange))
			{
				object value = mutableJsonChange.Value;
				if (!(value is JsonElement))
				{
					this.WritePrimitiveChange(mutableJsonChange, writer);
					return;
				}
				JsonElement jsonElement = (JsonElement)value;
				element = jsonElement;
				highWaterMark = mutableJsonChange.Index;
			}
			if (!this.Changes.DescendantChanged(path, highWaterMark))
			{
				element.WriteTo(writer);
				return;
			}
			JsonValueKind valueKind = element.ValueKind;
			if (valueKind == 1)
			{
				this.WriteObject(path, highWaterMark, element, writer);
				return;
			}
			if (valueKind != 2)
			{
				throw new InvalidOperationException("Element doesn't have descendants.");
			}
			this.WriteArray(path, highWaterMark, element, writer);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000141F8 File Offset: 0x000123F8
		private void WriteObject(string path, int highWaterMark, JsonElement element, Utf8JsonWriter writer)
		{
			writer.WriteStartObject();
			foreach (JsonProperty jsonProperty in element.EnumerateObject())
			{
				string text = MutableJsonDocument.ChangeTracker.PushProperty(path, jsonProperty.Name);
				if (!this.Changes.WasRemoved(text, highWaterMark))
				{
					writer.WritePropertyName(jsonProperty.Name);
					this.WriteElement(text, highWaterMark, jsonProperty.Value, writer);
				}
			}
			foreach (MutableJsonChange mutableJsonChange in this.Changes.GetAddedProperties(path, highWaterMark))
			{
				string addedPropertyName = mutableJsonChange.AddedPropertyName;
				string text2 = MutableJsonDocument.ChangeTracker.PushProperty(path, addedPropertyName);
				writer.WritePropertyName(addedPropertyName);
				object value = mutableJsonChange.Value;
				if (value is JsonElement)
				{
					JsonElement jsonElement = (JsonElement)value;
					this.WriteElement(text2, highWaterMark, jsonElement, writer);
				}
				else
				{
					this.WritePrimitiveChange(mutableJsonChange, writer);
				}
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00014324 File Offset: 0x00012524
		private void WriteArray(string path, int highWaterMark, JsonElement element, Utf8JsonWriter writer)
		{
			writer.WriteStartArray();
			int num = 0;
			foreach (JsonElement jsonElement in element.EnumerateArray())
			{
				string text = MutableJsonDocument.ChangeTracker.PushIndex(path, num++);
				this.WriteElement(text, highWaterMark, jsonElement, writer);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000143A0 File Offset: 0x000125A0
		private void WritePrimitiveChange(MutableJsonChange change, Utf8JsonWriter writer)
		{
			object value = change.Value;
			if (value is bool)
			{
				bool flag = (bool)value;
				writer.WriteBooleanValue(flag);
				return;
			}
			if (value is byte)
			{
				byte b = (byte)value;
				writer.WriteNumberValue((int)b);
				return;
			}
			if (value is sbyte)
			{
				sbyte b2 = (sbyte)value;
				writer.WriteNumberValue((int)b2);
				return;
			}
			if (value is short)
			{
				short num = (short)value;
				writer.WriteNumberValue((int)num);
				return;
			}
			if (value is ushort)
			{
				ushort num2 = (ushort)value;
				writer.WriteNumberValue((int)num2);
				return;
			}
			if (value is int)
			{
				int num3 = (int)value;
				writer.WriteNumberValue(num3);
				return;
			}
			if (value is uint)
			{
				uint num4 = (uint)value;
				writer.WriteNumberValue(num4);
				return;
			}
			if (value is long)
			{
				long num5 = (long)value;
				writer.WriteNumberValue(num5);
				return;
			}
			if (value is ulong)
			{
				ulong num6 = (ulong)value;
				writer.WriteNumberValue(num6);
				return;
			}
			if (value is float)
			{
				float num7 = (float)value;
				writer.WriteNumberValue(num7);
				return;
			}
			if (value is double)
			{
				double num8 = (double)value;
				writer.WriteNumberValue(num8);
				return;
			}
			if (value is decimal)
			{
				decimal num9 = (decimal)value;
				writer.WriteNumberValue(num9);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				writer.WriteStringValue(text);
				return;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				writer.WriteStringValue(dateTime);
				return;
			}
			if (value is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				writer.WriteStringValue(dateTimeOffset);
				return;
			}
			if (value is Guid)
			{
				Guid guid = (Guid)value;
				writer.WriteStringValue(guid);
				return;
			}
			if (value != null)
			{
				throw new InvalidOperationException(string.Format("Unrecognized change type '{0}'.", change.Value.GetType()));
			}
			writer.WriteNullValue();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000145D8 File Offset: 0x000127D8
		internal unsafe void WritePatch(Utf8JsonWriter writer)
		{
			if (!this.Changes.HasChanges)
			{
				return;
			}
			JsonValueKind? valueKind = this.ValueKind;
			JsonValueKind jsonValueKind = 2;
			if ((valueKind.GetValueOrDefault() == jsonValueKind) & (valueKind != null))
			{
				this.WriteTo(writer);
				return;
			}
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(this._path);
			int num;
			MutableJsonChange? mutableJsonChange = this.Changes.GetFirstMergePatchChange(readOnlySpan, out num);
			checked
			{
				Span<char> span;
				if (num <= 1024)
				{
					int num2 = num;
					span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				}
				else
				{
					span = new char[num];
				}
				Span<char> span2 = span;
				int num3 = 0;
				MutableJsonElement.CopyTo(span2, ref num3, readOnlySpan);
				MutableJsonElement mutableJsonElement = this;
				if (num <= 1024)
				{
					int num2 = num;
					span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				}
				else
				{
					span = new char[num];
				}
				Span<char> span3 = span;
				int num4 = 0;
				MutableJsonElement.CopyTo(span3, ref num4, readOnlySpan);
				writer.WriteStartObject();
				while (mutableJsonChange != null)
				{
					ReadOnlySpan<char> readOnlySpan2 = MemoryExtensions.AsSpan(mutableJsonChange.Value.Path);
					num4 = readOnlySpan.Length;
					this.CloseOpenObjects(writer, readOnlySpan2, span2, ref num3, ref mutableJsonElement);
					ReadOnlySpan<char> readOnlySpan3 = readOnlySpan2.Slice(readOnlySpan.Length);
					int num2 = readOnlySpan2.Length;
					this.OpenAncestorObjects(writer, readOnlySpan3, in num2, span3, ref num4, span2, ref num3, ref mutableJsonElement);
					ReadOnlySpan<char> lastSegment = MutableJsonElement.GetLastSegment(span3.Slice(0, num4));
					writer.WritePropertyName(lastSegment);
					this.WritePatchValue(writer, mutableJsonChange.Value, span2.Slice(0, num3), mutableJsonElement);
					MutableJsonDocument.ChangeTracker.PopProperty(span2, ref num3);
					mutableJsonElement = this.GetPropertyFromRoot(span2.Slice(0, num3));
					mutableJsonChange = this.Changes.GetNextMergePatchChange(readOnlySpan, span3.Slice(0, num4));
				}
				this.CloseFinalObjects(writer, span2.Slice(readOnlySpan.Length, num3));
				writer.WriteEndObject();
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000147BC File Offset: 0x000129BC
		[NullableContext(0)]
		private static ReadOnlySpan<char> GetFirstSegment(ReadOnlySpan<char> path)
		{
			int num = MemoryExtensions.IndexOf<char>(path, '\u0001');
			if (num != -1)
			{
				return path.Slice(0, num);
			}
			return path;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000147E0 File Offset: 0x000129E0
		[NullableContext(0)]
		private static ReadOnlySpan<char> GetLastSegment(ReadOnlySpan<char> path)
		{
			int num = MemoryExtensions.LastIndexOf<char>(path, '\u0001');
			if (num != -1)
			{
				return path.Slice(num + 1);
			}
			return path;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00014805 File Offset: 0x00012A05
		[NullableContext(0)]
		private static void CopyTo(Span<char> target, ref int targetLength, ReadOnlySpan<char> value)
		{
			value.CopyTo(target);
			targetLength = value.Length;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00014818 File Offset: 0x00012A18
		[NullableContext(0)]
		private void CloseOpenObjects([Nullable(1)] Utf8JsonWriter writer, ReadOnlySpan<char> changePath, Span<char> patchPath, ref int patchPathLength, ref MutableJsonElement patchElement)
		{
			bool flag = false;
			while (!MutableJsonChange.IsDescendant(patchPath.Slice(0, patchPathLength), changePath))
			{
				writer.WriteEndObject();
				MutableJsonDocument.ChangeTracker.PopProperty(patchPath, ref patchPathLength);
				flag = true;
			}
			if (flag)
			{
				patchElement = this.GetPropertyFromRoot(patchPath.Slice(0, patchPathLength));
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00014874 File Offset: 0x00012A74
		[NullableContext(0)]
		private void OpenAncestorObjects([Nullable(1)] Utf8JsonWriter writer, ReadOnlySpan<char> path, in int changePathLength, Span<char> currentPath, ref int currentPathLength, Span<char> patchPath, ref int patchPathLength, ref MutableJsonElement patchElement)
		{
			foreach (ReadOnlySpan<char> readOnlySpan in MutableJsonDocument.ChangeTracker.Split(path))
			{
				if (readOnlySpan.Length != 0)
				{
					MutableJsonDocument.ChangeTracker.PushProperty(currentPath, ref currentPathLength, readOnlySpan);
					if (!MemoryExtensions.StartsWith<char>(patchPath.Slice(0, patchPathLength), currentPath.Slice(0, currentPathLength)) && MemoryExtensions.StartsWith<char>(currentPath.Slice(0, currentPathLength), patchPath.Slice(0, patchPathLength)))
					{
						MutableJsonDocument.ChangeTracker.PushProperty(patchPath, ref patchPathLength, readOnlySpan);
						if (!patchElement.TryGetProperty(readOnlySpan, out patchElement))
						{
							break;
						}
						JsonValueKind? valueKind = patchElement.ValueKind;
						JsonValueKind jsonValueKind = 2;
						if (((valueKind.GetValueOrDefault() == jsonValueKind) & (valueKind != null)) || changePathLength == currentPathLength)
						{
							break;
						}
						writer.WritePropertyName(readOnlySpan);
						writer.WriteStartObject();
					}
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00014950 File Offset: 0x00012B50
		[NullableContext(0)]
		private void CloseFinalObjects([Nullable(1)] Utf8JsonWriter writer, ReadOnlySpan<char> patchPath)
		{
			foreach (ReadOnlySpan<char> readOnlySpan in MutableJsonDocument.ChangeTracker.Split(patchPath))
			{
				if (readOnlySpan.Length > 0)
				{
					writer.WriteEndObject();
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00014990 File Offset: 0x00012B90
		[NullableContext(0)]
		private void WritePatchValue([Nullable(1)] Utf8JsonWriter writer, MutableJsonChange change, ReadOnlySpan<char> patchPath, MutableJsonElement patchElement)
		{
			switch (change.ChangeKind)
			{
			case MutableJsonChangeKind.PropertyUpdate:
			{
				JsonValueKind? valueKind = patchElement.ValueKind;
				JsonValueKind jsonValueKind = 1;
				if ((valueKind.GetValueOrDefault() == jsonValueKind) & (valueKind != null))
				{
					this.WriteObjectUpdate(writer, patchPath, patchElement);
					return;
				}
				patchElement.WriteTo(writer);
				return;
			}
			case MutableJsonChangeKind.PropertyAddition:
				patchElement.WriteTo(writer);
				return;
			case MutableJsonChangeKind.PropertyRemoval:
				writer.WriteNullValue();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000149FC File Offset: 0x00012BFC
		[NullableContext(0)]
		private void WriteObjectUpdate([Nullable(1)] Utf8JsonWriter writer, ReadOnlySpan<char> path, MutableJsonElement patchElement)
		{
			bool flag = false;
			foreach (JsonProperty jsonProperty in this.GetOriginal(path).EnumerateObject())
			{
				MutableJsonElement mutableJsonElement;
				if (!patchElement.TryGetProperty(jsonProperty.Name, out mutableJsonElement))
				{
					if (!flag)
					{
						writer.WriteStartObject();
						flag = true;
					}
					writer.WritePropertyName(jsonProperty.Name);
					writer.WriteNullValue();
				}
			}
			if (flag)
			{
				foreach (global::System.ValueTuple<string, MutableJsonElement> valueTuple in patchElement.EnumerateObject())
				{
					writer.WritePropertyName(valueTuple.Item1);
					valueTuple.Item2.WriteTo(writer);
				}
				writer.WriteEndObject();
				return;
			}
			patchElement.WriteTo(writer);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00014AF8 File Offset: 0x00012CF8
		[NullableContext(0)]
		private MutableJsonElement GetPropertyFromRoot(ReadOnlySpan<char> path)
		{
			MutableJsonElement mutableJsonElement = this._root.RootElement;
			foreach (ReadOnlySpan<char> readOnlySpan in MutableJsonDocument.ChangeTracker.Split(path))
			{
				if (readOnlySpan.Length > 0)
				{
					mutableJsonElement = mutableJsonElement.GetProperty(readOnlySpan);
				}
			}
			return mutableJsonElement;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00014B48 File Offset: 0x00012D48
		[NullableContext(0)]
		private JsonElement GetOriginal(ReadOnlySpan<char> path)
		{
			JsonElement jsonElement = this._root.RootElement._element;
			foreach (ReadOnlySpan<char> readOnlySpan in MutableJsonDocument.ChangeTracker.Split(path))
			{
				if (readOnlySpan.Length > 0)
				{
					jsonElement = jsonElement.GetProperty(readOnlySpan);
				}
			}
			return jsonElement;
		}

		// Token: 0x0400026C RID: 620
		internal const int MaxStackLimit = 1024;

		// Token: 0x0400026D RID: 621
		private readonly MutableJsonDocument _root;

		// Token: 0x0400026E RID: 622
		private readonly JsonElement _element;

		// Token: 0x0400026F RID: 623
		private readonly string _path;

		// Token: 0x04000270 RID: 624
		private readonly int _highWaterMark;

		// Token: 0x04000271 RID: 625
		internal const string SerializationRequiresUnreferencedCodeConstructor = "This struct utilizes reflection-based JSON serialization which is not compatible with trimming.";

		// Token: 0x04000272 RID: 626
		internal const string SerializationRequiresUnreferencedCodeMethod = "This method utilizes reflection-based JSON serialization which is not compatible with trimming.";

		// Token: 0x02000145 RID: 325
		[NullableContext(0)]
		[DebuggerDisplay("{Current,nq}")]
		public struct ArrayEnumerator : IEnumerable<MutableJsonElement>, IEnumerable, IEnumerator<MutableJsonElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000893 RID: 2195 RVA: 0x00020F7A File Offset: 0x0001F17A
			internal ArrayEnumerator(MutableJsonElement element)
			{
				element.EnsureArray();
				this._element = element;
				this._length = element._element.GetArrayLength();
				this._index = -1;
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000894 RID: 2196 RVA: 0x00020FA4 File Offset: 0x0001F1A4
			public MutableJsonElement Current
			{
				get
				{
					if (this._index < 0)
					{
						return default(MutableJsonElement);
					}
					return this._element.GetIndexElement(this._index);
				}
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x00020FD8 File Offset: 0x0001F1D8
			public MutableJsonElement.ArrayEnumerator GetEnumerator()
			{
				MutableJsonElement.ArrayEnumerator arrayEnumerator = this;
				arrayEnumerator._index = -1;
				return arrayEnumerator;
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x00020FF5 File Offset: 0x0001F1F5
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x00021002 File Offset: 0x0001F202
			[NullableContext(1)]
			IEnumerator<MutableJsonElement> IEnumerable<MutableJsonElement>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x0002100F File Offset: 0x0001F20F
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000899 RID: 2201 RVA: 0x00021018 File Offset: 0x0001F218
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x00021025 File Offset: 0x0001F225
			public bool MoveNext()
			{
				this._index++;
				return this._index < this._length;
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x00021043 File Offset: 0x0001F243
			public void Dispose()
			{
				this._index = this._length;
			}

			// Token: 0x040004EF RID: 1263
			private readonly MutableJsonElement _element;

			// Token: 0x040004F0 RID: 1264
			private readonly int _length;

			// Token: 0x040004F1 RID: 1265
			private int _index;
		}

		// Token: 0x02000146 RID: 326
		[Nullable(0)]
		[RequiresUnreferencedCode("Using MutableJsonElement or MutableJsonElementConverter is not compatible with trimming due to reflection-based serialization.")]
		private class MutableJsonElementConverter : JsonConverter<MutableJsonElement>
		{
			// Token: 0x0600089C RID: 2204 RVA: 0x00021051 File Offset: 0x0001F251
			public override MutableJsonElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return MutableJsonDocument.Parse(ref reader, null).RootElement;
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x0002105F File Offset: 0x0001F25F
			public override void Write(Utf8JsonWriter writer, MutableJsonElement value, JsonSerializerOptions options)
			{
				value.WriteTo(writer);
			}
		}

		// Token: 0x02000147 RID: 327
		[NullableContext(0)]
		[DebuggerDisplay("{Current,nq}")]
		public struct ObjectEnumerator : IEnumerable<global::System.ValueTuple<string, MutableJsonElement>>, IEnumerable, IEnumerator<global::System.ValueTuple<string, MutableJsonElement>>, IDisposable, IEnumerator
		{
			// Token: 0x0600089F RID: 2207 RVA: 0x00021074 File Offset: 0x0001F274
			internal ObjectEnumerator(MutableJsonElement target)
			{
				target.EnsureObject();
				this._target = target;
				this._enumerator = target.GetJsonElement().EnumerateObject();
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x060008A0 RID: 2208 RVA: 0x000210A4 File Offset: 0x0001F2A4
			[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Value" })]
			[Nullable(new byte[] { 0, 1 })]
			public global::System.ValueTuple<string, MutableJsonElement> Current
			{
				[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Value" })]
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					JsonProperty jsonProperty = this._enumerator.Current;
					string name = jsonProperty.Name;
					jsonProperty = this._enumerator.Current;
					return new global::System.ValueTuple<string, MutableJsonElement>(name, this._target.GetProperty(jsonProperty.Name));
				}
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x000210E8 File Offset: 0x0001F2E8
			public MutableJsonElement.ObjectEnumerator GetEnumerator()
			{
				MutableJsonElement.ObjectEnumerator objectEnumerator = this;
				objectEnumerator._enumerator = this._enumerator.GetEnumerator();
				return objectEnumerator;
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x0002110F File Offset: 0x0001F30F
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x0002111C File Offset: 0x0001F31C
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Value" })]
			[return: Nullable(new byte[] { 1, 0, 1 })]
			IEnumerator<global::System.ValueTuple<string, MutableJsonElement>> IEnumerable<global::System.ValueTuple<string, MutableJsonElement>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x00021129 File Offset: 0x0001F329
			public void Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x00021136 File Offset: 0x0001F336
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00021143 File Offset: 0x0001F343
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x00021155 File Offset: 0x0001F355
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x040004F2 RID: 1266
			private readonly MutableJsonElement _target;

			// Token: 0x040004F3 RID: 1267
			private JsonElement.ObjectEnumerator _enumerator;
		}
	}
}
