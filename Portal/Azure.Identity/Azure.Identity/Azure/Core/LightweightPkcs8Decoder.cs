using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Azure.Core
{
	// Token: 0x02000017 RID: 23
	internal static class LightweightPkcs8Decoder
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002D54 File Offset: 0x00000F54
		internal unsafe static byte[] ReadBitString(byte[] data, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			if (data[num] != 3)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num2 = LightweightPkcs8Decoder.ReadLength(data, ref offset);
			if (num2 == 0)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			num = offset;
			offset = num + 1;
			int num3 = (int)data[num];
			if (num3 > 7)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			Span<byte> span = MemoryExtensions.AsSpan<byte>(data, offset, num2 - 1);
			int num4 = -1 << num3;
			byte b = (byte)((int)(*span[span.Length - 1]) & num4);
			byte[] array = new byte[span.Length];
			Buffer.BlockCopy(data, offset, array, 0, span.Length);
			array[span.Length - 1] = b;
			offset += span.Length;
			return array;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E18 File Offset: 0x00001018
		internal static string ReadObjectIdentifier(byte[] data, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			if (data[num] != 6)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num2 = LightweightPkcs8Decoder.ReadLength(data, ref offset);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = offset; i < offset + num2; i++)
			{
				byte b = data[i];
				if (i == offset)
				{
					byte b2;
					if (b < 40)
					{
						b2 = 0;
					}
					else
					{
						if (b >= 80)
						{
							throw new InvalidDataException("Unsupported PKCS#8 Data");
						}
						b2 = 1;
						b -= 40;
					}
					stringBuilder.Append(b2).Append('.').Append(b);
				}
				else if (b < 128)
				{
					stringBuilder.Append('.').Append(b);
				}
				else
				{
					stringBuilder.Append('.');
					if (b == 128)
					{
						throw new InvalidDataException("Invalid PKCS#8 Data");
					}
					int num3 = -1;
					for (int j = i; j < offset + num2; j++)
					{
						if ((data[j] & 128) == 0)
						{
							num3 = j;
							break;
						}
					}
					if (num3 < 0)
					{
						throw new InvalidDataException("Invalid PKCS#8 Data");
					}
					int num4 = num3 + 1;
					if (num4 > i + 4)
					{
						throw new InvalidDataException("Unsupported PKCS#8 Data");
					}
					int num5 = 0;
					for (int j = i; j < num4; j++)
					{
						b = data[j];
						num5 <<= 7;
						num5 |= (int)(b & 127);
					}
					stringBuilder.Append(num5);
					i = num3;
				}
			}
			offset += num2;
			return stringBuilder.ToString();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F84 File Offset: 0x00001184
		internal static byte[] ReadOctetString(byte[] data, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			if (data[num] != 4)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num2 = LightweightPkcs8Decoder.ReadLength(data, ref offset);
			byte[] array = new byte[num2];
			Buffer.BlockCopy(data, offset, array, 0, num2);
			offset += num2;
			return array;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FCC File Offset: 0x000011CC
		private static int ReadLength(byte[] data, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			byte b = data[num];
			if (b < 128)
			{
				return (int)b;
			}
			int num2 = (int)(b & 127);
			int num3 = 0;
			for (int i = 0; i < num2; i++)
			{
				num3 <<= 8;
				int num4 = num3;
				num = offset;
				offset = num + 1;
				num3 = num4 | (int)data[num];
				if (num3 > 65535)
				{
					throw new InvalidDataException("Invalid PKCS#8 Data");
				}
			}
			return num3;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000302C File Offset: 0x0000122C
		private static byte[] ReadUnsignedInteger(byte[] data, ref int offset, int targetSize = 0)
		{
			int num = offset;
			offset = num + 1;
			if (data[num] != 2)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num2 = LightweightPkcs8Decoder.ReadLength(data, ref offset);
			if (num2 < 1 || data[offset] >= 128)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			byte[] array;
			if (num2 == 1)
			{
				array = new byte[num2];
				byte[] array2 = array;
				int num3 = 0;
				num = offset;
				offset = num + 1;
				array2[num3] = data[num];
				return array;
			}
			if (data[offset] == 0)
			{
				offset++;
				num2--;
			}
			if (targetSize != 0)
			{
				if (num2 > targetSize)
				{
					throw new InvalidDataException("Invalid PKCS#8 Data");
				}
				array = new byte[targetSize];
			}
			else
			{
				array = new byte[num2];
			}
			Buffer.BlockCopy(data, offset, array, array.Length - num2, num2);
			offset += num2;
			return array;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030D8 File Offset: 0x000012D8
		private static int ReadPayloadTagLength(byte[] data, ref int offset, byte tagValue)
		{
			int num = offset;
			offset = num + 1;
			if (data[num] != tagValue)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			return LightweightPkcs8Decoder.ReadLength(data, ref offset);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003108 File Offset: 0x00001308
		private static void ConsumeFullPayloadTag(byte[] data, ref int offset, byte tagValue)
		{
			int num = offset;
			offset = num + 1;
			if (data[num] != tagValue)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num2 = LightweightPkcs8Decoder.ReadLength(data, ref offset);
			if (data.Length - offset != num2)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000314A File Offset: 0x0000134A
		private static void ConsumeMatch(byte[] data, ref int offset, byte[] toMatch)
		{
			if (data.Length - offset > toMatch.Length && data.Skip(offset).Take(toMatch.Length).SequenceEqual(toMatch))
			{
				offset += toMatch.Length;
				return;
			}
			throw new InvalidDataException("Invalid PKCS#8 Data");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003184 File Offset: 0x00001384
		public static RSA DecodeRSAPkcs8(byte[] pkcs8Bytes)
		{
			int num = 0;
			LightweightPkcs8Decoder.ConsumeFullPayloadTag(pkcs8Bytes, ref num, 48);
			LightweightPkcs8Decoder.ConsumeMatch(pkcs8Bytes, ref num, LightweightPkcs8Decoder.s_derIntegerZero);
			LightweightPkcs8Decoder.ConsumeMatch(pkcs8Bytes, ref num, LightweightPkcs8Decoder.s_rsaAlgorithmId);
			LightweightPkcs8Decoder.ConsumeFullPayloadTag(pkcs8Bytes, ref num, 4);
			LightweightPkcs8Decoder.ConsumeFullPayloadTag(pkcs8Bytes, ref num, 48);
			LightweightPkcs8Decoder.ConsumeMatch(pkcs8Bytes, ref num, LightweightPkcs8Decoder.s_derIntegerZero);
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Modulus = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, 0);
			rsaparameters.Exponent = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, 0);
			rsaparameters.D = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, rsaparameters.Modulus.Length);
			int num2 = (rsaparameters.Modulus.Length + 1) / 2;
			rsaparameters.P = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, num2);
			rsaparameters.Q = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, num2);
			rsaparameters.DP = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, num2);
			rsaparameters.DQ = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, num2);
			rsaparameters.InverseQ = LightweightPkcs8Decoder.ReadUnsignedInteger(pkcs8Bytes, ref num, num2);
			if (num != pkcs8Bytes.Length)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			RSA rsa = RSA.Create();
			rsa.ImportParameters(rsaparameters);
			return rsa;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003290 File Offset: 0x00001490
		public static string DecodePrivateKeyOid(byte[] pkcs8Bytes)
		{
			int num = 0;
			LightweightPkcs8Decoder.ConsumeFullPayloadTag(pkcs8Bytes, ref num, 48);
			LightweightPkcs8Decoder.ConsumeMatch(pkcs8Bytes, ref num, LightweightPkcs8Decoder.s_derIntegerZero);
			LightweightPkcs8Decoder.ReadPayloadTagLength(pkcs8Bytes, ref num, 48);
			return LightweightPkcs8Decoder.ReadObjectIdentifier(pkcs8Bytes, ref num);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032C9 File Offset: 0x000014C9
		// Note: this type is marked as 'beforefieldinit'.
		static LightweightPkcs8Decoder()
		{
			byte[] array = new byte[3];
			array[0] = 2;
			array[1] = 1;
			LightweightPkcs8Decoder.s_derIntegerZero = array;
			LightweightPkcs8Decoder.s_rsaAlgorithmId = new byte[]
			{
				48, 13, 6, 9, 42, 134, 72, 134, 247, 13,
				1, 1, 1, 5, 0
			};
		}

		// Token: 0x04000039 RID: 57
		private static readonly byte[] s_derIntegerZero;

		// Token: 0x0400003A RID: 58
		private static readonly byte[] s_rsaAlgorithmId;
	}
}
