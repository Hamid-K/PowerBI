using System;
using System.IO;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000037 RID: 55
	internal class PersistenceBinaryWriter : BinaryWriter
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00008DA2 File Offset: 0x00006FA2
		internal PersistenceBinaryWriter(Stream str)
			: base(str)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008DAB File Offset: 0x00006FAB
		internal void WriteNull()
		{
			base.Write(0);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008DB4 File Offset: 0x00006FB4
		internal void WriteEnum(int value)
		{
			base.Write7BitEncodedInt(value);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008DBD File Offset: 0x00006FBD
		internal void Write(DateTime dateTime, Token token)
		{
			if (token == Token.DateTimeWithKind)
			{
				this.Write((byte)dateTime.Kind);
			}
			this.Write(dateTime.Ticks);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008DE2 File Offset: 0x00006FE2
		internal void Write(DateTimeOffset dateTimeOffset)
		{
			this.Write(dateTimeOffset.DateTime, Token.DateTime);
			this.Write(dateTimeOffset.Offset);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008E03 File Offset: 0x00007003
		internal void Write(TimeSpan timeSpan)
		{
			this.Write(timeSpan.Ticks);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008E12 File Offset: 0x00007012
		public override void Write(bool value)
		{
			if (value)
			{
				base.Write(1);
				return;
			}
			base.Write(0);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008E26 File Offset: 0x00007026
		internal void Write(Guid guid)
		{
			base.Write(guid.ToByteArray());
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008E38 File Offset: 0x00007038
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

		// Token: 0x06000223 RID: 547 RVA: 0x00008F02 File Offset: 0x00007102
		public override void Write(string value)
		{
			this.Write(value, true);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008F0C File Offset: 0x0000710C
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

		// Token: 0x06000225 RID: 549 RVA: 0x00008F2A File Offset: 0x0000712A
		internal void WriteDictionaryStart(ObjectType type, int size)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(size);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008F3A File Offset: 0x0000713A
		internal void WriteListStart(ObjectType type, int size)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(size);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008F4A File Offset: 0x0000714A
		internal void WriteArrayStart(ObjectType type, int size)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(size);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008F5A File Offset: 0x0000715A
		internal void Write2DArrayStart(ObjectType type, int xSize, int ySize)
		{
			base.Write7BitEncodedInt((int)type);
			base.Write7BitEncodedInt(xSize);
			base.Write7BitEncodedInt(ySize);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008F71 File Offset: 0x00007171
		internal void Write(ObjectType type)
		{
			base.Write7BitEncodedInt((int)type);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008F7A File Offset: 0x0000717A
		internal void Write(MemberName name)
		{
			base.Write7BitEncodedInt((int)name);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008F83 File Offset: 0x00007183
		internal void Write(Token token)
		{
			this.Write((byte)token);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008F8C File Offset: 0x0000718C
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

		// Token: 0x0600022D RID: 557 RVA: 0x00008FE4 File Offset: 0x000071E4
		internal void Write(MemberInfo member)
		{
			this.Write(member.MemberName);
			this.Write(member.ObjectType);
			this.Write(member.Token);
			this.Write(member.ContainedType);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009018 File Offset: 0x00007218
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

		// Token: 0x0600022F RID: 559 RVA: 0x00009050 File Offset: 0x00007250
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

		// Token: 0x06000230 RID: 560 RVA: 0x00009088 File Offset: 0x00007288
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

		// Token: 0x06000231 RID: 561 RVA: 0x000090C0 File Offset: 0x000072C0
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

		// Token: 0x06000232 RID: 562 RVA: 0x000090F8 File Offset: 0x000072F8
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

		// Token: 0x06000233 RID: 563 RVA: 0x00009130 File Offset: 0x00007330
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

		// Token: 0x06000234 RID: 564 RVA: 0x00009154 File Offset: 0x00007354
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

		// Token: 0x06000235 RID: 565 RVA: 0x0000918C File Offset: 0x0000738C
		internal void Write(SqlGeography sqlGeography)
		{
			sqlGeography.Write(this);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009195 File Offset: 0x00007395
		internal void Write(SqlGeometry sqlGeometry)
		{
			sqlGeometry.Write(this);
		}

		// Token: 0x0400013E RID: 318
		private static int m_MaxEncVal = 2097152;
	}
}
