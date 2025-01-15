using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000554 RID: 1364
	internal class PersistenceBinaryReader : BinaryReader
	{
		// Token: 0x060049E1 RID: 18913 RVA: 0x00137AEA File Offset: 0x00135CEA
		internal PersistenceBinaryReader(Stream str)
			: base(str)
		{
		}

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00137AF3 File Offset: 0x00135CF3
		// (set) Token: 0x060049E3 RID: 18915 RVA: 0x00137B00 File Offset: 0x00135D00
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

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x00137B0E File Offset: 0x00135D0E
		internal bool EOS
		{
			get
			{
				return base.BaseStream.Position == base.BaseStream.Length;
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x00137B28 File Offset: 0x00135D28
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

		// Token: 0x060049E6 RID: 18918 RVA: 0x00137B44 File Offset: 0x00135D44
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

		// Token: 0x060049E7 RID: 18919 RVA: 0x00137B5C File Offset: 0x00135D5C
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

		// Token: 0x060049E8 RID: 18920 RVA: 0x00137B74 File Offset: 0x00135D74
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

		// Token: 0x060049E9 RID: 18921 RVA: 0x00137B8C File Offset: 0x00135D8C
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

		// Token: 0x060049EA RID: 18922 RVA: 0x00137BB0 File Offset: 0x00135DB0
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

		// Token: 0x060049EB RID: 18923 RVA: 0x00137BEC File Offset: 0x00135DEC
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

		// Token: 0x060049EC RID: 18924 RVA: 0x00137C10 File Offset: 0x00135E10
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

		// Token: 0x060049ED RID: 18925 RVA: 0x00137C4C File Offset: 0x00135E4C
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

		// Token: 0x060049EE RID: 18926 RVA: 0x00137C88 File Offset: 0x00135E88
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

		// Token: 0x060049EF RID: 18927 RVA: 0x00137CC4 File Offset: 0x00135EC4
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

		// Token: 0x060049F0 RID: 18928 RVA: 0x00137D00 File Offset: 0x00135F00
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

		// Token: 0x060049F1 RID: 18929 RVA: 0x00137D3B File Offset: 0x00135F3B
		public override bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x00137D46 File Offset: 0x00135F46
		internal Guid ReadGuid()
		{
			return new Guid(base.ReadBytes(16));
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x00137D58 File Offset: 0x00135F58
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

		// Token: 0x060049F4 RID: 18932 RVA: 0x00137DEE File Offset: 0x00135FEE
		public override string ReadString()
		{
			return this.ReadString(true);
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x00137DF7 File Offset: 0x00135FF7
		internal string ReadString(bool checkforNull)
		{
			if (checkforNull && this.ReadObjectType() == ObjectType.Null)
			{
				return null;
			}
			return base.ReadString();
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x00137E0C File Offset: 0x0013600C
		internal DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x00137E1C File Offset: 0x0013601C
		internal DateTime ReadDateTimeWithKind()
		{
			DateTimeKind dateTimeKind = (DateTimeKind)this.ReadByte();
			return DateTime.SpecifyKind(new DateTime(this.ReadInt64()), dateTimeKind);
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x00137E44 File Offset: 0x00136044
		internal DateTimeOffset ReadDateTimeOffset()
		{
			DateTime dateTime = this.ReadDateTime();
			TimeSpan timeSpan = this.ReadTimeSpan();
			return new DateTimeOffset(dateTime, timeSpan);
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x00137E64 File Offset: 0x00136064
		internal TimeSpan ReadTimeSpan()
		{
			return new TimeSpan(this.ReadInt64());
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x00137E71 File Offset: 0x00136071
		internal int ReadEnum()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x00137E79 File Offset: 0x00136079
		internal Token ReadToken()
		{
			return (Token)this.ReadByte();
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x00137E81 File Offset: 0x00136081
		internal ObjectType ReadObjectType()
		{
			return (ObjectType)base.Read7BitEncodedInt();
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x00137E89 File Offset: 0x00136089
		private MemberName ReadMemberName()
		{
			return (MemberName)base.Read7BitEncodedInt();
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x00137E94 File Offset: 0x00136094
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

		// Token: 0x060049FF RID: 18943 RVA: 0x00137EF8 File Offset: 0x001360F8
		internal void SkipString()
		{
			if (this.ReadObjectType() != ObjectType.Null)
			{
				this.SkipBytes(base.Read7BitEncodedInt());
			}
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x00137F10 File Offset: 0x00136110
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

		// Token: 0x06004A01 RID: 18945 RVA: 0x00137F44 File Offset: 0x00136144
		internal void SkipMultiByteInt()
		{
			base.Read7BitEncodedInt();
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x00137F50 File Offset: 0x00136150
		internal void SkipTypedArray(int elementSize)
		{
			int num = base.Read7BitEncodedInt();
			this.SkipBytes(num * elementSize);
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x00137F70 File Offset: 0x00136170
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

		// Token: 0x06004A04 RID: 18948 RVA: 0x00137FA6 File Offset: 0x001361A6
		internal SqlGeography ReadSqlGeography()
		{
			SqlGeography sqlGeography = new SqlGeography();
			sqlGeography.Read(this);
			return sqlGeography;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x00137FB4 File Offset: 0x001361B4
		internal SqlGeometry ReadSqlGeometry()
		{
			SqlGeometry sqlGeometry = new SqlGeometry();
			sqlGeometry.Read(this);
			return sqlGeometry;
		}
	}
}
