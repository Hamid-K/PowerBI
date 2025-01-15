using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x0200000A RID: 10
	internal class JwtTokenDecryptionParameters
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000068F0 File Offset: 0x00004AF0
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000068F8 File Offset: 0x00004AF8
		public byte[] CipherTextBytes { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00006901 File Offset: 0x00004B01
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00006909 File Offset: 0x00004B09
		public byte[] HeaderAsciiBytes { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00006912 File Offset: 0x00004B12
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000691A File Offset: 0x00004B1A
		public byte[] InitializationVectorBytes { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00006923 File Offset: 0x00004B23
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000692B File Offset: 0x00004B2B
		public byte[] AuthenticationTagBytes { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00006934 File Offset: 0x00004B34
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000693C File Offset: 0x00004B3C
		public string Alg { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00006945 File Offset: 0x00004B45
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000694D File Offset: 0x00004B4D
		public string AuthenticationTag { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00006956 File Offset: 0x00004B56
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000695E File Offset: 0x00004B5E
		public string Ciphertext { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00006967 File Offset: 0x00004B67
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000696F File Offset: 0x00004B6F
		public Func<byte[], string, int, string> DecompressionFunction { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00006978 File Offset: 0x00004B78
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00006980 File Offset: 0x00004B80
		public string Enc { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00006989 File Offset: 0x00004B89
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00006991 File Offset: 0x00004B91
		public string EncodedHeader { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000699A File Offset: 0x00004B9A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000069A2 File Offset: 0x00004BA2
		public string EncodedToken { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000069AB File Offset: 0x00004BAB
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000069B3 File Offset: 0x00004BB3
		public string InitializationVector { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000069BC File Offset: 0x00004BBC
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000069C4 File Offset: 0x00004BC4
		public IEnumerable<SecurityKey> Keys { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000069CD File Offset: 0x00004BCD
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000069D5 File Offset: 0x00004BD5
		public int MaximumDeflateSize { get; set; } = 256000;

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000069DE File Offset: 0x00004BDE
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000069E6 File Offset: 0x00004BE6
		public string Zip { get; set; }
	}
}
