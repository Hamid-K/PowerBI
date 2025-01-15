using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000036 RID: 54
	internal class PersistenceBinaryReader : BinaryReader
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x000088CA File Offset: 0x00006ACA
		internal PersistenceBinaryReader(Stream str)
			: base(str)
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000088D3 File Offset: 0x00006AD3
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000088E0 File Offset: 0x00006AE0
		internal long StreamPosition
		{
			get
			{
				return this.BaseStream.Position;
			}
			set
			{
				this.BaseStream.Position = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000088EE File Offset: 0x00006AEE
		internal bool EOS
		{
			get
			{
				return base.BaseStream.Position == base.BaseStream.Length;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008908 File Offset: 0x00006B08
		internal bool ReadReference(out int refID, out ObjectType declaredRefType)
		{
			declaredRefType = this.ReadObjectType();
			if (declaredRefType != ObjectType.Null)
			{
				refID = this.ReadInt32();
				return true;
			}
			refID = -1;
			return false;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008924 File Offset: 0x00006B24
		internal bool ReadListStart(ObjectType objectType, out int listSize)
		{
			if (this.ReadObjectType() == ObjectType.Null)
			{
				listSize = -1;
				return false;
			}
			listSize = base.Read7BitEncodedInt();
			return true;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000893C File Offset: 0x00006B3C
		internal bool ReadDictionaryStart(ObjectType objectType, out int dictionarySize)
		{
			if (this.ReadObjectType() == ObjectType.Null)
			{
				dictionarySize = -1;
				return false;
			}
			dictionarySize = base.Read7BitEncodedInt();
			return true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008954 File Offset: 0x00006B54
		internal bool ReadArrayStart(ObjectType objectType, out int arraySize)
		{
			if (this.ReadObjectType() == ObjectType.Null)
			{
				arraySize = -1;
				return false;
			}
			arraySize = base.Read7BitEncodedInt();
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000896C File Offset: 0x00006B6C
		internal bool Read2DArrayStart(ObjectType objectType, out int arrayXLength, out int arrayYLength)
		{
			if (this.ReadObjectType() == ObjectType.Null)
			{
				arrayXLength = -1;
				arrayYLength = -1;
				return false;
			}
			arrayXLength = base.Read7BitEncodedInt();
			arrayYLength = base.Read7BitEncodedInt();
			return true;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008990 File Offset: 0x00006B90
		internal bool[] ReadBooleanArray()
		{
			bool[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num) && num > 0)
			{
				array = new bool[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadBoolean();
				}
			}
			return array;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000089CC File Offset: 0x00006BCC
		internal byte[] ReadByteArray()
		{
			byte[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num))
			{
				array = this.ReadBytes(num);
			}
			return array;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000089F0 File Offset: 0x00006BF0
		internal float[] ReadFloatArray()
		{
			float[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num) && num > 0)
			{
				array = new float[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadSingle();
				}
			}
			return array;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008A2C File Offset: 0x00006C2C
		internal double[] ReadDoubleArray()
		{
			double[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num) && num > 0)
			{
				array = new double[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadDouble();
				}
			}
			return array;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008A68 File Offset: 0x00006C68
		internal char[] ReadCharArray()
		{
			char[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num) && num > 0)
			{
				array = new char[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadChar();
				}
			}
			return array;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008AA4 File Offset: 0x00006CA4
		internal int[] ReadInt32Array()
		{
			int[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num) && num > 0)
			{
				array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadInt32();
				}
			}
			return array;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008AE0 File Offset: 0x00006CE0
		internal long[] ReadInt64Array()
		{
			long[] array = null;
			int num;
			if (this.ReadArrayStart(ObjectType.PrimitiveTypedArray, out num) && num > 0)
			{
				array = new long[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadInt64();
				}
			}
			return array;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008B1B File Offset: 0x00006D1B
		public override bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008B26 File Offset: 0x00006D26
		internal Guid ReadGuid()
		{
			return new Guid(base.ReadBytes(16));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008B38 File Offset: 0x00006D38
		public override decimal ReadDecimal()
		{
			int[] array = new int[4];
			byte b = base.ReadByte();
			for (int i = 0; i < 3; i++)
			{
				if (((int)b & (1 << i * 2)) != 0)
				{
					if (((int)b & (2 << i * 2)) != 0)
					{
						array[i] = base.Read7BitEncodedInt();
					}
					else
					{
						array[i] = base.ReadInt32();
					}
				}
			}
			if ((b & 64) != 0)
			{
				array[3] = (int)(base.ReadByte() & byte.MaxValue);
				array[3] <<= 16;
			}
			if ((b & 128) != 0)
			{
				array[3] |= int.MinValue;
			}
			return new decimal(array);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008BCE File Offset: 0x00006DCE
		public override string ReadString()
		{
			return this.ReadString(true);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008BD7 File Offset: 0x00006DD7
		internal string ReadString(bool checkforNull)
		{
			if (checkforNull && this.ReadObjectType() == ObjectType.Null)
			{
				return null;
			}
			return base.ReadString();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008BEC File Offset: 0x00006DEC
		internal DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008BFC File Offset: 0x00006DFC
		internal DateTime ReadDateTimeWithKind()
		{
			DateTimeKind dateTimeKind = (DateTimeKind)this.ReadByte();
			return DateTime.SpecifyKind(new DateTime(this.ReadInt64()), dateTimeKind);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008C24 File Offset: 0x00006E24
		internal DateTimeOffset ReadDateTimeOffset()
		{
			DateTime dateTime = this.ReadDateTime();
			TimeSpan timeSpan = this.ReadTimeSpan();
			return new DateTimeOffset(dateTime, timeSpan);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008C44 File Offset: 0x00006E44
		internal TimeSpan ReadTimeSpan()
		{
			return new TimeSpan(this.ReadInt64());
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008C51 File Offset: 0x00006E51
		internal int ReadEnum()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008C59 File Offset: 0x00006E59
		internal Token ReadToken()
		{
			return (Token)this.ReadByte();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008C61 File Offset: 0x00006E61
		internal ObjectType ReadObjectType()
		{
			return (ObjectType)base.Read7BitEncodedInt();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008C69 File Offset: 0x00006E69
		private MemberName ReadMemberName()
		{
			return (MemberName)base.Read7BitEncodedInt();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008C74 File Offset: 0x00006E74
		internal Declaration ReadDeclaration()
		{
			ObjectType objectType = this.ReadObjectType();
			ObjectType objectType2 = this.ReadObjectType();
			int num = base.Read7BitEncodedInt();
			List<MemberInfo> list = new List<MemberInfo>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(new MemberInfo(this.ReadMemberName(), this.ReadObjectType(), this.ReadToken(), this.ReadObjectType()));
			}
			return new Declaration(objectType, objectType2, list);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008CD8 File Offset: 0x00006ED8
		internal void SkipString()
		{
			if (this.ReadObjectType() != ObjectType.Null)
			{
				this.SkipBytes(base.Read7BitEncodedInt());
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008CF0 File Offset: 0x00006EF0
		internal void SkipBytes(int bytesToSkip)
		{
			if (bytesToSkip <= 0)
			{
				return;
			}
			Stream baseStream = this.BaseStream;
			if (baseStream.CanSeek)
			{
				baseStream.Seek((long)bytesToSkip, SeekOrigin.Current);
				return;
			}
			this.ReadBytes(bytesToSkip);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008D24 File Offset: 0x00006F24
		internal void SkipMultiByteInt()
		{
			base.Read7BitEncodedInt();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008D30 File Offset: 0x00006F30
		internal void SkipTypedArray(int elementSize)
		{
			int num = base.Read7BitEncodedInt();
			this.SkipBytes(num * elementSize);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008D50 File Offset: 0x00006F50
		internal void Seek(long newPosition, SeekOrigin seekOrigin)
		{
			Stream baseStream = this.BaseStream;
			if (baseStream.CanSeek)
			{
				baseStream.Seek(newPosition, seekOrigin);
				return;
			}
			Global.Tracer.Assert(false, "Seek not supported for this stream.");
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008D86 File Offset: 0x00006F86
		internal SqlGeography ReadSqlGeography()
		{
			SqlGeography sqlGeography = new SqlGeography();
			sqlGeography.Read(this);
			return sqlGeography;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008D94 File Offset: 0x00006F94
		internal SqlGeometry ReadSqlGeometry()
		{
			SqlGeometry sqlGeometry = new SqlGeometry();
			sqlGeometry.Read(this);
			return sqlGeometry;
		}
	}
}
