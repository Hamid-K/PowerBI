using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200084F RID: 2127
	public sealed class DiffieHellmanAlgorithm : AsymmetricAlgorithm
	{
		// Token: 0x060043BC RID: 17340 RVA: 0x000E3FEC File Offset: 0x000E21EC
		public DiffieHellmanAlgorithm(byte[] p, byte[] g, int l)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (l < 0)
			{
				throw new ArgumentException();
			}
			this.Initialize(new BigInteger(p), new BigInteger(g), l, true);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x000E4020 File Offset: 0x000E2220
		private void Initialize(BigInteger p, BigInteger g, int secretLen, bool checkInput)
		{
			if (g <= 0L || g >= p)
			{
				throw new CryptographicException();
			}
			if (secretLen == 0)
			{
				this.keySize = p.ToByteArray().Length;
			}
			else
			{
				this.keySize = secretLen / 8;
			}
			this.m_P = p;
			this.m_G = g;
			BigInteger bigInteger = this.m_P - 1;
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] array = new byte[this.keySize + 1];
			do
			{
				rngcryptoServiceProvider.GetBytes(array);
				array[this.keySize] = 0;
				this.m_X = new BigInteger(array);
			}
			while (!(this.m_X >= bigInteger) && !(this.m_X == 0L));
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x000E40D0 File Offset: 0x000E22D0
		public byte[] CreateKeyExchange()
		{
			byte[] array = BigInteger.ModPow(this.m_G, this.m_X, this.m_P).ToByteArray();
			Array.Resize<byte>(ref array, this.keySize);
			Array.Reverse(array);
			return array;
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x000E4114 File Offset: 0x000E2314
		public byte[] DecryptKeyExchange(byte[] keyEx)
		{
			byte[] array = new byte[this.keySize];
			Array.Copy(keyEx, array, this.keySize);
			Array.Reverse(array);
			Array.Resize<byte>(ref array, this.keySize + 1);
			byte[] array2 = BigInteger.ModPow(new BigInteger(array), this.m_X, this.m_P).ToByteArray();
			Array.Resize<byte>(ref array2, this.keySize);
			Array.Reverse(array2);
			return array2;
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060043C0 RID: 17344 RVA: 0x000E4183 File Offset: 0x000E2383
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "1.2.840.113549.1.3";
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000189CC File Offset: 0x00016BCC
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x000E418A File Offset: 0x000E238A
		protected override void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				this.m_P = BigInteger.MinusOne;
				this.m_G = BigInteger.MinusOne;
				this.m_X = BigInteger.MinusOne;
			}
			this.m_Disposed = true;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000E41BC File Offset: 0x000E23BC
		~DiffieHellmanAlgorithm()
		{
			this.Dispose(false);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void FromXmlString(string xmlString)
		{
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000189CC File Offset: 0x00016BCC
		public override string ToXmlString(bool includePrivateParameters)
		{
			return null;
		}

		// Token: 0x04002FA5 RID: 12197
		private int keySize;

		// Token: 0x04002FA6 RID: 12198
		private BigInteger m_P;

		// Token: 0x04002FA7 RID: 12199
		private BigInteger m_G;

		// Token: 0x04002FA8 RID: 12200
		private BigInteger m_X;

		// Token: 0x04002FA9 RID: 12201
		private bool m_Disposed;
	}
}
