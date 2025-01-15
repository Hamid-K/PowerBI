using System;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200009B RID: 155
	public sealed class EntityKey
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x000188F6 File Offset: 0x00016AF6
		private EntityKey(byte[] value)
		{
			if (value == null || value.Length == 0)
			{
				throw new ArgumentNullException("value");
			}
			this.m_value = value;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00018918 File Offset: 0x00016B18
		public override bool Equals(object obj)
		{
			EntityKey entityKey = obj as EntityKey;
			return entityKey != null && CollectionUtil.ElementsEqual<byte>(this.m_value, entityKey.m_value);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00018944 File Offset: 0x00016B44
		public override int GetHashCode()
		{
			int num = 0;
			int num2 = 0;
			while (num2 < this.m_value.Length && num2 < 10)
			{
				num ^= (int)this.m_value[num2] << ((num2 & 3) << 3);
				num2++;
			}
			return num;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00018980 File Offset: 0x00016B80
		public string ToBase64String()
		{
			return Convert.ToBase64String(this.m_value);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001898D File Offset: 0x00016B8D
		public static EntityKey FromBase64String(string base64Value)
		{
			if (!string.IsNullOrEmpty(base64Value))
			{
				base64Value = base64Value.Replace(".", "");
			}
			return new EntityKey(Convert.FromBase64String(base64Value));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000189B4 File Offset: 0x00016BB4
		public object[] ToKeyParts(Type[] keyPartTypes, IQueryEntity entity)
		{
			object[] array2;
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.m_value)))
			{
				EntityKey.KeyPartReader keyPartReader = new EntityKey.KeyPartReader(binaryReader);
				object[] array = new object[keyPartTypes.Length];
				EntityKey.KeyHeader keyHeader = EntityKey.KeyHeader.Load(binaryReader, keyPartTypes.Length);
				if (keyHeader == null)
				{
					array = null;
				}
				else
				{
					for (int i = 0; i < keyPartTypes.Length; i++)
					{
						if (keyHeader.Nulls != null && keyHeader.Nulls[i])
						{
							array[i] = null;
						}
						else
						{
							array[i] = keyPartReader.Read(keyPartTypes[i]);
							if (array[i] == null)
							{
								array = null;
								break;
							}
						}
					}
					if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
					{
						array = null;
					}
				}
				if (array == null)
				{
					throw new ValidationException(ModelingErrorCode.InvalidEntityKeyValue, SRErrors.InvalidEntityKeyValue(this.ToBase64String(), SRObjectDescriptor.FromScope(entity)));
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x04000390 RID: 912
		private readonly byte[] m_value;

		// Token: 0x04000391 RID: 913
		private static readonly object UnknownTypeObject = new object();

		// Token: 0x0200018E RID: 398
		internal sealed class KeyHeader
		{
			// Token: 0x0600103F RID: 4159 RVA: 0x00033697 File Offset: 0x00031897
			internal KeyHeader()
			{
			}

			// Token: 0x06001040 RID: 4160 RVA: 0x000336A0 File Offset: 0x000318A0
			internal void Init(object[] keyParts)
			{
				if (this.Nulls == null || this.Nulls.Length != keyParts.Length)
				{
					this.Nulls = new bool[keyParts.Length];
				}
				for (int i = 0; i < keyParts.Length; i++)
				{
					this.Nulls[i] = keyParts[i] == null;
				}
			}

			// Token: 0x06001041 RID: 4161 RVA: 0x000336EC File Offset: 0x000318EC
			internal static EntityKey.KeyHeader Load(BinaryReader reader, int keyPartCount)
			{
				int num = reader.BaseStream.ReadByte();
				if (num < 0)
				{
					return null;
				}
				bool flag = false;
				if ((num & 128) != 0)
				{
					flag = true;
				}
				else if (num != 0)
				{
					return null;
				}
				EntityKey.KeyHeader keyHeader = new EntityKey.KeyHeader();
				if (flag)
				{
					keyHeader.Nulls = new bool[keyPartCount];
					int num2 = 0;
					for (int i = 0; i < keyPartCount; i++)
					{
						int num3 = i % 8;
						if (num3 == 0)
						{
							num2 = reader.BaseStream.ReadByte();
							if (num2 < 0)
							{
								return null;
							}
						}
						keyHeader.Nulls[i] = (num2 & (int)EntityKey.KeyHeader.BitTests[num3]) != 0;
					}
				}
				return keyHeader;
			}

			// Token: 0x06001042 RID: 4162 RVA: 0x0003377C File Offset: 0x0003197C
			internal void WriteTo(BinaryWriter writer)
			{
				byte b = 0;
				if (this.Nulls != null)
				{
					for (int i = 0; i < this.Nulls.Length; i++)
					{
						if (this.Nulls[i])
						{
							b |= 128;
							break;
						}
					}
				}
				writer.Write(b);
				if ((b & 128) != 0)
				{
					byte b2 = 0;
					for (int j = 0; j < this.Nulls.Length; j++)
					{
						int num = j % 8;
						if (this.Nulls[j])
						{
							b2 |= EntityKey.KeyHeader.BitTests[num];
						}
						if (num == 7)
						{
							writer.Write(b2);
							b2 = 0;
						}
					}
					if (this.Nulls.Length % 8 != 0)
					{
						writer.Write(b2);
					}
				}
			}

			// Token: 0x06001043 RID: 4163 RVA: 0x0003381C File Offset: 0x00031A1C
			private static int GetNullsLength(int keyPartCount)
			{
				return keyPartCount / 8 + ((keyPartCount % 8 > 0) ? 1 : 0);
			}

			// Token: 0x040006C2 RID: 1730
			internal bool[] Nulls;

			// Token: 0x040006C3 RID: 1731
			private const byte NullsBit = 128;

			// Token: 0x040006C4 RID: 1732
			private static readonly byte[] BitTests = new byte[] { 128, 64, 32, 16, 8, 4, 2, 1 };
		}

		// Token: 0x0200018F RID: 399
		private sealed class KeyPartReader : IMappedClrTypeAction<object>
		{
			// Token: 0x06001045 RID: 4165 RVA: 0x00033840 File Offset: 0x00031A40
			public KeyPartReader(BinaryReader reader)
			{
				this.m_reader = reader;
			}

			// Token: 0x06001046 RID: 4166 RVA: 0x00033850 File Offset: 0x00031A50
			public object Read(Type keyPartType)
			{
				object obj;
				try
				{
					obj = DataTypeMapper.PerformAction<object>(keyPartType, this);
				}
				catch
				{
					obj = null;
				}
				if (obj == EntityKey.UnknownTypeObject)
				{
					throw new ArgumentOutOfRangeException("keyPartType", DevExceptionMessages.EntityKey_InvalidKeyPartType(keyPartType));
				}
				return obj;
			}

			// Token: 0x06001047 RID: 4167 RVA: 0x00033898 File Offset: 0x00031A98
			object IMappedClrTypeAction<object>.ForString()
			{
				return this.m_reader.ReadString();
			}

			// Token: 0x06001048 RID: 4168 RVA: 0x000338A5 File Offset: 0x00031AA5
			object IMappedClrTypeAction<object>.ForChar()
			{
				return this.m_reader.ReadChar();
			}

			// Token: 0x06001049 RID: 4169 RVA: 0x000338B7 File Offset: 0x00031AB7
			object IMappedClrTypeAction<object>.ForInt32()
			{
				return this.m_reader.ReadInt32();
			}

			// Token: 0x0600104A RID: 4170 RVA: 0x000338C9 File Offset: 0x00031AC9
			object IMappedClrTypeAction<object>.ForInt16()
			{
				return this.m_reader.ReadInt16();
			}

			// Token: 0x0600104B RID: 4171 RVA: 0x000338DB File Offset: 0x00031ADB
			object IMappedClrTypeAction<object>.ForUInt16()
			{
				return this.m_reader.ReadUInt16();
			}

			// Token: 0x0600104C RID: 4172 RVA: 0x000338ED File Offset: 0x00031AED
			object IMappedClrTypeAction<object>.ForByte()
			{
				return this.m_reader.ReadByte();
			}

			// Token: 0x0600104D RID: 4173 RVA: 0x000338FF File Offset: 0x00031AFF
			object IMappedClrTypeAction<object>.ForSByte()
			{
				return this.m_reader.ReadSByte();
			}

			// Token: 0x0600104E RID: 4174 RVA: 0x00033911 File Offset: 0x00031B11
			object IMappedClrTypeAction<object>.ForDecimal()
			{
				return this.m_reader.ReadDecimal();
			}

			// Token: 0x0600104F RID: 4175 RVA: 0x00033923 File Offset: 0x00031B23
			object IMappedClrTypeAction<object>.ForInt64()
			{
				return this.m_reader.ReadInt64();
			}

			// Token: 0x06001050 RID: 4176 RVA: 0x00033935 File Offset: 0x00031B35
			object IMappedClrTypeAction<object>.ForUInt64()
			{
				return this.m_reader.ReadUInt64();
			}

			// Token: 0x06001051 RID: 4177 RVA: 0x00033947 File Offset: 0x00031B47
			object IMappedClrTypeAction<object>.ForUInt32()
			{
				return this.m_reader.ReadUInt32();
			}

			// Token: 0x06001052 RID: 4178 RVA: 0x00033959 File Offset: 0x00031B59
			object IMappedClrTypeAction<object>.ForDouble()
			{
				return this.m_reader.ReadDouble();
			}

			// Token: 0x06001053 RID: 4179 RVA: 0x0003396B File Offset: 0x00031B6B
			object IMappedClrTypeAction<object>.ForSingle()
			{
				return this.m_reader.ReadSingle();
			}

			// Token: 0x06001054 RID: 4180 RVA: 0x0003397D File Offset: 0x00031B7D
			object IMappedClrTypeAction<object>.ForDateTime()
			{
				return DateTime.FromBinary(this.m_reader.ReadInt64());
			}

			// Token: 0x06001055 RID: 4181 RVA: 0x00033994 File Offset: 0x00031B94
			object IMappedClrTypeAction<object>.ForDateTimeOffset()
			{
				return DateTime.FromBinary(this.m_reader.ReadInt64());
			}

			// Token: 0x06001056 RID: 4182 RVA: 0x000339AB File Offset: 0x00031BAB
			object IMappedClrTypeAction<object>.ForTimeSpan()
			{
				return TimeSpan.FromTicks(this.m_reader.ReadInt64());
			}

			// Token: 0x06001057 RID: 4183 RVA: 0x000339C2 File Offset: 0x00031BC2
			object IMappedClrTypeAction<object>.ForBoolean()
			{
				return this.m_reader.ReadBoolean();
			}

			// Token: 0x06001058 RID: 4184 RVA: 0x000339D4 File Offset: 0x00031BD4
			object IMappedClrTypeAction<object>.ForGuid()
			{
				return new Guid(this.m_reader.ReadBytes(16));
			}

			// Token: 0x06001059 RID: 4185 RVA: 0x000339F0 File Offset: 0x00031BF0
			object IMappedClrTypeAction<object>.ForByteArray()
			{
				int num = this.m_reader.ReadInt32();
				return this.m_reader.ReadBytes(num);
			}

			// Token: 0x0600105A RID: 4186 RVA: 0x00033A15 File Offset: 0x00031C15
			object IMappedClrTypeAction<object>.ForEntityKey()
			{
				return EntityKey.UnknownTypeObject;
			}

			// Token: 0x0600105B RID: 4187 RVA: 0x00033A1C File Offset: 0x00031C1C
			object IMappedClrTypeAction<object>.ForUnknown()
			{
				return EntityKey.UnknownTypeObject;
			}

			// Token: 0x040006C5 RID: 1733
			private readonly BinaryReader m_reader;
		}

		// Token: 0x02000190 RID: 400
		internal sealed class KeyPartWriter : IMappedClrTypeAction<object>
		{
			// Token: 0x0600105C RID: 4188 RVA: 0x00033A23 File Offset: 0x00031C23
			public KeyPartWriter(BinaryWriter writer)
			{
				this.m_writer = writer;
			}

			// Token: 0x0600105D RID: 4189 RVA: 0x00033A34 File Offset: 0x00031C34
			public void Write(object keyPart, Type keyPartType)
			{
				if (keyPart == null)
				{
					throw new ValidationException(ModelingErrorCode.InvalidEntityKeyPart, SRErrors.InvalidEntityKeyPart((keyPart == null) ? "[NULL]" : keyPart, keyPartType));
				}
				try
				{
					keyPart = Convert.ChangeType(keyPart, keyPartType, CultureInfo.InvariantCulture);
				}
				catch
				{
					throw new ValidationException(ModelingErrorCode.InvalidEntityKeyPart, SRErrors.InvalidEntityKeyPart(keyPart, keyPartType));
				}
				this.m_keyPart = keyPart;
				if (DataTypeMapper.PerformAction<object>(keyPartType, this) == EntityKey.UnknownTypeObject)
				{
					throw new ArgumentOutOfRangeException("keyPartType", DevExceptionMessages.EntityKey_InvalidKeyPartType(keyPartType));
				}
			}

			// Token: 0x0600105E RID: 4190 RVA: 0x00033AB4 File Offset: 0x00031CB4
			object IMappedClrTypeAction<object>.ForString()
			{
				this.m_writer.Write((string)this.m_keyPart);
				return null;
			}

			// Token: 0x0600105F RID: 4191 RVA: 0x00033ACD File Offset: 0x00031CCD
			object IMappedClrTypeAction<object>.ForChar()
			{
				this.m_writer.Write((char)this.m_keyPart);
				return null;
			}

			// Token: 0x06001060 RID: 4192 RVA: 0x00033AE6 File Offset: 0x00031CE6
			object IMappedClrTypeAction<object>.ForInt32()
			{
				this.m_writer.Write((int)this.m_keyPart);
				return null;
			}

			// Token: 0x06001061 RID: 4193 RVA: 0x00033AFF File Offset: 0x00031CFF
			object IMappedClrTypeAction<object>.ForInt16()
			{
				this.m_writer.Write((short)this.m_keyPart);
				return null;
			}

			// Token: 0x06001062 RID: 4194 RVA: 0x00033B18 File Offset: 0x00031D18
			object IMappedClrTypeAction<object>.ForUInt16()
			{
				this.m_writer.Write((ushort)this.m_keyPart);
				return null;
			}

			// Token: 0x06001063 RID: 4195 RVA: 0x00033B31 File Offset: 0x00031D31
			object IMappedClrTypeAction<object>.ForByte()
			{
				this.m_writer.Write((byte)this.m_keyPart);
				return null;
			}

			// Token: 0x06001064 RID: 4196 RVA: 0x00033B4A File Offset: 0x00031D4A
			object IMappedClrTypeAction<object>.ForSByte()
			{
				this.m_writer.Write((sbyte)this.m_keyPart);
				return null;
			}

			// Token: 0x06001065 RID: 4197 RVA: 0x00033B63 File Offset: 0x00031D63
			object IMappedClrTypeAction<object>.ForDecimal()
			{
				this.m_writer.Write((decimal)this.m_keyPart);
				return null;
			}

			// Token: 0x06001066 RID: 4198 RVA: 0x00033B7C File Offset: 0x00031D7C
			object IMappedClrTypeAction<object>.ForInt64()
			{
				this.m_writer.Write((long)this.m_keyPart);
				return null;
			}

			// Token: 0x06001067 RID: 4199 RVA: 0x00033B95 File Offset: 0x00031D95
			object IMappedClrTypeAction<object>.ForUInt64()
			{
				this.m_writer.Write((ulong)this.m_keyPart);
				return null;
			}

			// Token: 0x06001068 RID: 4200 RVA: 0x00033BAE File Offset: 0x00031DAE
			object IMappedClrTypeAction<object>.ForUInt32()
			{
				this.m_writer.Write((uint)this.m_keyPart);
				return null;
			}

			// Token: 0x06001069 RID: 4201 RVA: 0x00033BC7 File Offset: 0x00031DC7
			object IMappedClrTypeAction<object>.ForDouble()
			{
				this.m_writer.Write((double)this.m_keyPart);
				return null;
			}

			// Token: 0x0600106A RID: 4202 RVA: 0x00033BE0 File Offset: 0x00031DE0
			object IMappedClrTypeAction<object>.ForSingle()
			{
				this.m_writer.Write((float)this.m_keyPart);
				return null;
			}

			// Token: 0x0600106B RID: 4203 RVA: 0x00033BFC File Offset: 0x00031DFC
			object IMappedClrTypeAction<object>.ForDateTime()
			{
				this.m_writer.Write(((DateTime)this.m_keyPart).ToBinary());
				return null;
			}

			// Token: 0x0600106C RID: 4204 RVA: 0x00033C28 File Offset: 0x00031E28
			object IMappedClrTypeAction<object>.ForDateTimeOffset()
			{
				this.m_writer.Write(((DateTimeOffset)this.m_keyPart).UtcDateTime.ToBinary());
				return null;
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x00033C5C File Offset: 0x00031E5C
			object IMappedClrTypeAction<object>.ForTimeSpan()
			{
				this.m_writer.Write(((TimeSpan)this.m_keyPart).Ticks);
				return null;
			}

			// Token: 0x0600106E RID: 4206 RVA: 0x00033C88 File Offset: 0x00031E88
			object IMappedClrTypeAction<object>.ForBoolean()
			{
				this.m_writer.Write((bool)this.m_keyPart);
				return null;
			}

			// Token: 0x0600106F RID: 4207 RVA: 0x00033CA4 File Offset: 0x00031EA4
			object IMappedClrTypeAction<object>.ForGuid()
			{
				this.m_writer.Write(((Guid)this.m_keyPart).ToByteArray());
				return null;
			}

			// Token: 0x06001070 RID: 4208 RVA: 0x00033CD0 File Offset: 0x00031ED0
			object IMappedClrTypeAction<object>.ForByteArray()
			{
				byte[] array = (byte[])this.m_keyPart;
				int num = array.Length;
				this.m_writer.Write(num);
				this.m_writer.Write(array);
				return null;
			}

			// Token: 0x06001071 RID: 4209 RVA: 0x00033D06 File Offset: 0x00031F06
			object IMappedClrTypeAction<object>.ForEntityKey()
			{
				return EntityKey.UnknownTypeObject;
			}

			// Token: 0x06001072 RID: 4210 RVA: 0x00033D0D File Offset: 0x00031F0D
			object IMappedClrTypeAction<object>.ForUnknown()
			{
				return EntityKey.UnknownTypeObject;
			}

			// Token: 0x040006C6 RID: 1734
			private readonly BinaryWriter m_writer;

			// Token: 0x040006C7 RID: 1735
			private object m_keyPart;
		}
	}
}
