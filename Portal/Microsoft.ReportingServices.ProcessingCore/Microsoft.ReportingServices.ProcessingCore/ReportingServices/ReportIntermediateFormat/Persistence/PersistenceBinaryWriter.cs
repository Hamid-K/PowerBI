using System;
using System.IO;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000555 RID: 1365
	internal class PersistenceBinaryWriter : BinaryWriter
	{
		// Token: 0x06004A06 RID: 18950 RVA: 0x00137FC2 File Offset: 0x001361C2
		internal PersistenceBinaryWriter(Stream str)
			: base(str)
		{
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x00137FCB File Offset: 0x001361CB
		internal void WriteNull()
		{
			base.Write(0);
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x00137FD4 File Offset: 0x001361D4
		internal void WriteEnum(int value)
		{
			base.Write7BitEncodedInt(value);
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x00137FDD File Offset: 0x001361DD
		internal void Write(DateTime dateTime, Token token)
		{
			if (token == Token.DateTimeWithKind)
			{
				this.Write((byte)dateTime.Kind);
			}
			this.Write(dateTime.Ticks);
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x00138002 File Offset: 0x00136202
		internal void Write(DateTimeOffset dateTimeOffset)
		{
			this.Write(dateTimeOffset.DateTime, Token.DateTime);
			this.Write(dateTimeOffset.Offset);
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x00138023 File Offset: 0x00136223
		internal void Write(TimeSpan timeSpan)
		{
			this.Write(timeSpan.Ticks);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x00138032 File Offset: 0x00136232
		public override void Write(bool value)
		{
			if (value)
			{
				base.Write(1);
				return;
			}
			base.Write(0);
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x00138046 File Offset: 0x00136246
		internal void Write(Guid guid)
		{
			base.Write(guid.ToByteArray());
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x00138058 File Offset: 0x00136258
		public override void Write(decimal value)
		{
			int[] bits = decimal.GetBits(value);
			int num = (int)((byte)(bits[3] >> 24));
			bits[3] &= int.MaxValue;
			bits[3] >>= 16;
			for (int i = 0; i < 3; i++)
			{
				if (bits[i] != 0)
				{
					if (bits[i] <= PersistenceBinaryWriter.m_MaxEncVal && bits[i] > 0)
					{
						num |= 3 << i * 2;
					}
					else
					{
						num |= 1 << i * 2;
					}
				}
			}
			if (bits[3] != 0)
			{
				num |= 64;
			}
			base.Write((byte)num);
			for (int i = 0; i < 3; i++)
			{
				if (bits[i] != 0)
				{
					if (bits[i] <= PersistenceBinaryWriter.m_MaxEncVal && bits[i] > 0)
					{
						base.Write7BitEncodedInt(bits[i]);
					}
					else
					{
						base.Write(bits[i]);
					}
				}
			}
			if (bits[3] != 0)
			{
				base.Write((byte)bits[3]);
			}
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x00138122 File Offset: 0x00136322
		public override void Write(string value)
		{
			this.Write(value, true);
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0013812C File Offset: 0x0013632C
		internal void Write(string str, bool writeObjType)
		{
			if (str == null)
			{
				this.WriteNull();
				return;
			}
			if (writeObjType)
			{
				this.Write(ObjectType.String);
			}
			base.Write(str);
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0013814D File Offset: 0x0013634D
		internal void WriteDictionaryStart(ObjectType type, int size)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(size);
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0013815D File Offset: 0x0013635D
		internal void WriteListStart(ObjectType type, int size)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(size);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x0013816D File Offset: 0x0013636D
		internal void WriteArrayStart(ObjectType type, int size)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(size);
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x0013817D File Offset: 0x0013637D
		internal void Write2DArrayStart(ObjectType type, int xSize, int ySize)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(xSize);
			base.Write7BitEncodedInt(ySize);
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00138194 File Offset: 0x00136394
		internal void Write(ObjectType type)
		{
			base.Write7BitEncodedInt((int)type);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x0013819D File Offset: 0x0013639D
		internal void Write(MemberName name)
		{
			base.Write7BitEncodedInt((int)name);
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x001381A6 File Offset: 0x001363A6
		internal void Write(Token token)
		{
			this.Write((byte)token);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x001381B0 File Offset: 0x001363B0
		internal void Write(Declaration decl)
		{
			this.Write(decl.ObjectType);
			this.Write(decl.BaseObjectType);
			int count = decl.MemberInfoList.Count;
			base.Write7BitEncodedInt(count);
			for (int i = 0; i < count; i++)
			{
				MemberInfo memberInfo = decl.MemberInfoList[i];
				this.Write(memberInfo);
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00138208 File Offset: 0x00136408
		internal void Write(MemberInfo member)
		{
			this.Write(member.MemberName);
			this.Write(member.ObjectType);
			this.Write(member.Token);
			this.Write(member.ContainedType);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0013823C File Offset: 0x0013643C
		internal void Write(float[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00138274 File Offset: 0x00136474
		internal void Write(int[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x001382AC File Offset: 0x001364AC
		internal void Write(long[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x001382E4 File Offset: 0x001364E4
		internal void Write(double[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x0013831C File Offset: 0x0013651C
		public override void Write(char[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x00138354 File Offset: 0x00136554
		public override void Write(byte[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			this.Write(array, 0, array.Length);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00138378 File Offset: 0x00136578
		public void Write(bool[] array)
		{
			if (array == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteArrayStart(ObjectType.PrimitiveTypedArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x001383B0 File Offset: 0x001365B0
		internal void Write(SqlGeography sqlGeography)
		{
			sqlGeography.Write(this);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x001383B9 File Offset: 0x001365B9
		internal void Write(SqlGeometry sqlGeometry)
		{
			sqlGeometry.Write(this);
		}

		// Token: 0x040020BE RID: 8382
		private static int m_MaxEncVal = 2097152;
	}
}
