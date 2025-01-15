using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Apache.Thrift
{
	// Token: 0x02001FF0 RID: 8176
	internal class FastProtocol
	{
		// Token: 0x06011107 RID: 69895 RVA: 0x003ADA86 File Offset: 0x003ABC86
		public FastProtocol(byte[] buffer)
		{
			this.buffer = buffer;
			this.position = 0;
		}

		// Token: 0x06011108 RID: 69896 RVA: 0x003ADAA9 File Offset: 0x003ABCA9
		public void IncrementRecursionDepth()
		{
			this.depth++;
		}

		// Token: 0x06011109 RID: 69897 RVA: 0x003ADAB9 File Offset: 0x003ABCB9
		public void DecrementRecursionDepth()
		{
			this.depth--;
		}

		// Token: 0x0601110A RID: 69898 RVA: 0x003ADAC9 File Offset: 0x003ABCC9
		public void ReadStructBegin()
		{
			this.lastField.Push(this.lastFieldId);
			this.lastFieldId = 0;
		}

		// Token: 0x0601110B RID: 69899 RVA: 0x003ADAE3 File Offset: 0x003ABCE3
		public void ReadStructEnd()
		{
			this.lastFieldId = this.lastField.Pop();
		}

		// Token: 0x0601110C RID: 69900 RVA: 0x003ADAF8 File Offset: 0x003ABCF8
		public TField ReadFieldBegin()
		{
			byte b = this.ReadByte();
			if (b == 0)
			{
				return FastProtocol.StopField;
			}
			short num = (short)((b & 240) >> 4);
			byte b2 = b & 15;
			short num2 = ((num != 0) ? (this.lastFieldId + num) : this.ReadI16());
			TType ttype = FastProtocol.GetTType(b2);
			TField tfield = new TField(string.Empty, ttype, num2);
			if (ttype == TType.Bool)
			{
				this.boolValue = new bool?(b2 == 1);
			}
			else
			{
				this.boolValue = null;
			}
			this.lastFieldId = tfield.ID;
			return tfield;
		}

		// Token: 0x0601110D RID: 69901 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void ReadFieldEnd()
		{
		}

		// Token: 0x0601110E RID: 69902 RVA: 0x003ADB83 File Offset: 0x003ABD83
		public sbyte ReadI8()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x0601110F RID: 69903 RVA: 0x003ADB8C File Offset: 0x003ABD8C
		public short ReadI16()
		{
			return (short)FastProtocol.ZigzagToInt(this.ReadVarInt32());
		}

		// Token: 0x06011110 RID: 69904 RVA: 0x003ADB9A File Offset: 0x003ABD9A
		public int ReadI32()
		{
			return FastProtocol.ZigzagToInt(this.ReadVarInt32());
		}

		// Token: 0x06011111 RID: 69905 RVA: 0x003ADBA7 File Offset: 0x003ABDA7
		public long ReadI64()
		{
			return FastProtocol.ZigzagToLong(this.ReadVarInt64());
		}

		// Token: 0x06011112 RID: 69906 RVA: 0x003ADBB4 File Offset: 0x003ABDB4
		public double ReadDouble()
		{
			return new Converter
			{
				Byte0 = this.ReadByte(),
				Byte1 = this.ReadByte(),
				Byte2 = this.ReadByte(),
				Byte3 = this.ReadByte(),
				Byte4 = this.ReadByte(),
				Byte5 = this.ReadByte(),
				Byte6 = this.ReadByte(),
				Byte7 = this.ReadByte()
			}.Double;
		}

		// Token: 0x06011113 RID: 69907 RVA: 0x003ADC38 File Offset: 0x003ABE38
		public TMap ReadMapBegin()
		{
			int num = (int)this.ReadVarInt32();
			byte b = ((num == 0) ? 0 : this.ReadByte());
			return new TMap(FastProtocol.GetTType((byte)(b >> 4)), FastProtocol.GetTType(b & 15), num);
		}

		// Token: 0x06011114 RID: 69908 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void ReadMapEnd()
		{
		}

		// Token: 0x06011115 RID: 69909 RVA: 0x003ADC74 File Offset: 0x003ABE74
		public TSet ReadSetBegin()
		{
			byte b = this.ReadByte();
			int num = (b & 240) >> 4;
			byte b2 = b & 15;
			if (num == 15)
			{
				num = (int)this.ReadVarInt32();
			}
			return new TSet(FastProtocol.GetTType(b2), num);
		}

		// Token: 0x06011116 RID: 69910 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void ReadSetEnd()
		{
		}

		// Token: 0x06011117 RID: 69911 RVA: 0x003ADCAC File Offset: 0x003ABEAC
		public TList ReadListBegin()
		{
			byte b = this.ReadByte();
			int num = (b & 240) >> 4;
			byte b2 = b & 15;
			if (num == 15)
			{
				num = (int)this.ReadVarInt32();
			}
			return new TList(FastProtocol.GetTType(b2), num);
		}

		// Token: 0x06011118 RID: 69912 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void ReadListEnd()
		{
		}

		// Token: 0x06011119 RID: 69913 RVA: 0x003ADCE4 File Offset: 0x003ABEE4
		public string ReadString()
		{
			int num = (int)this.ReadVarInt32();
			string @string = Encoding.UTF8.GetString(this.buffer, this.position, num);
			this.position += num;
			return @string;
		}

		// Token: 0x0601111A RID: 69914 RVA: 0x003ADD20 File Offset: 0x003ABF20
		public byte[] ReadBytes()
		{
			int num = (int)this.ReadVarInt32();
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.buffer, this.position, array, 0, num);
			this.position += num;
			return array;
		}

		// Token: 0x0601111B RID: 69915 RVA: 0x003ADD5E File Offset: 0x003ABF5E
		public bool ReadBool()
		{
			if (this.boolValue == null)
			{
				throw new InvalidOperationException("bool value not available");
			}
			return this.boolValue.Value;
		}

		// Token: 0x0601111C RID: 69916 RVA: 0x003ADD84 File Offset: 0x003ABF84
		public void Skip(TType type)
		{
			this.IncrementRecursionDepth();
			try
			{
				switch (type)
				{
				case TType.Bool:
					return;
				case TType.Byte:
				case TType.I8:
					this.position++;
					return;
				case TType.Double:
					this.position += 8;
					return;
				case TType.I16:
				case TType.I32:
					this.ReadVarInt32();
					return;
				case TType.I64:
					this.ReadVarInt64();
					return;
				case TType.String:
					this.position += (int)this.ReadVarInt32();
					return;
				case TType.Struct:
					this.SkipStruct();
					return;
				case TType.Map:
					this.SkipMap();
					return;
				case TType.Set:
					this.SkipSet();
					return;
				case TType.List:
					this.SkipList();
					return;
				}
				throw new NotSupportedException();
			}
			finally
			{
				this.DecrementRecursionDepth();
			}
		}

		// Token: 0x0601111D RID: 69917 RVA: 0x003ADE60 File Offset: 0x003AC060
		private void SkipStruct()
		{
			this.ReadStructBegin();
			for (;;)
			{
				TField tfield = this.ReadFieldBegin();
				if (tfield.Type == TType.Stop)
				{
					break;
				}
				this.Skip(tfield.Type);
				this.ReadFieldEnd();
			}
			this.ReadStructEnd();
		}

		// Token: 0x0601111E RID: 69918 RVA: 0x003ADEA0 File Offset: 0x003AC0A0
		private void SkipMap()
		{
			TMap tmap = this.ReadMapBegin();
			for (int i = 0; i < tmap.Count; i++)
			{
				this.Skip(tmap.KeyType);
				this.Skip(tmap.ValueType);
			}
			this.ReadMapEnd();
		}

		// Token: 0x0601111F RID: 69919 RVA: 0x003ADEE8 File Offset: 0x003AC0E8
		private void SkipSet()
		{
			TSet tset = this.ReadSetBegin();
			for (int i = 0; i < tset.Count; i++)
			{
				this.Skip(tset.ElementType);
			}
			this.ReadSetEnd();
		}

		// Token: 0x06011120 RID: 69920 RVA: 0x003ADF24 File Offset: 0x003AC124
		private void SkipList()
		{
			TList tlist = this.ReadListBegin();
			for (int i = 0; i < tlist.Count; i++)
			{
				this.Skip(tlist.ElementType);
			}
			this.ReadListEnd();
		}

		// Token: 0x06011121 RID: 69921 RVA: 0x003ADF60 File Offset: 0x003AC160
		public static TType GetTType(byte compactType)
		{
			switch (compactType)
			{
			case 1:
			case 2:
				return TType.Bool;
			case 3:
				return TType.I8;
			case 4:
				return TType.I16;
			case 5:
				return TType.I32;
			case 6:
				return TType.I64;
			case 7:
				return TType.Double;
			case 8:
				return TType.String;
			case 9:
				return TType.List;
			case 10:
				return TType.Set;
			case 11:
				return TType.Map;
			case 12:
				return TType.Struct;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06011122 RID: 69922 RVA: 0x003ADFC8 File Offset: 0x003AC1C8
		public void EndField(TType type, bool skip)
		{
			if (skip)
			{
				this.Skip(type);
				return;
			}
			this.ReadFieldEnd();
		}

		// Token: 0x06011123 RID: 69923 RVA: 0x003ADFDC File Offset: 0x003AC1DC
		private uint ReadVarInt32()
		{
			uint num = 0U;
			int num2 = 0;
			for (;;)
			{
				byte b = this.ReadByte();
				num |= (uint)((uint)(b & 127) << num2);
				if ((b & 128) != 128)
				{
					break;
				}
				num2 += 7;
			}
			return num;
		}

		// Token: 0x06011124 RID: 69924 RVA: 0x003AE018 File Offset: 0x003AC218
		private ulong ReadVarInt64()
		{
			int num = 0;
			ulong num2 = 0UL;
			for (;;)
			{
				byte b = this.ReadByte();
				num2 |= (ulong)((ulong)((long)(b & 127)) << num);
				if ((b & 128) != 128)
				{
					break;
				}
				num += 7;
			}
			return num2;
		}

		// Token: 0x06011125 RID: 69925 RVA: 0x003AE053 File Offset: 0x003AC253
		private static int ZigzagToInt(uint n)
		{
			return (int)((n >> 1) ^ (0U - (n & 1U)));
		}

		// Token: 0x06011126 RID: 69926 RVA: 0x003AE05E File Offset: 0x003AC25E
		private static long ZigzagToLong(ulong n)
		{
			return (long)((n >> 1) ^ (0UL - (n & 1UL)));
		}

		// Token: 0x06011127 RID: 69927 RVA: 0x003AE06C File Offset: 0x003AC26C
		private byte ReadByte()
		{
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			return array[num];
		}

		// Token: 0x04006733 RID: 26419
		private static readonly TField StopField = new TField
		{
			Type = TType.Stop
		};

		// Token: 0x04006734 RID: 26420
		private readonly byte[] buffer;

		// Token: 0x04006735 RID: 26421
		private readonly Stack<short> lastField = new Stack<short>(15);

		// Token: 0x04006736 RID: 26422
		private short lastFieldId;

		// Token: 0x04006737 RID: 26423
		private bool? boolValue;

		// Token: 0x04006738 RID: 26424
		private int position;

		// Token: 0x04006739 RID: 26425
		private int depth;
	}
}
