using System;
using System.Security.Cryptography;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200011B RID: 283
	internal class HMACSHA256Cng : KeyedHashAlgorithm
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0001A38C File Offset: 0x0001858C
		public HMACSHA256Cng()
			: this(HMACSHA256Cng.GenerateRandomKey())
		{
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001A399 File Offset: 0x00018599
		public HMACSHA256Cng(byte[] key)
		{
			this.hashAlgorithmInner = new SHA256Cng();
			this.hashAlgorithmOuter = new SHA256Cng();
			this.HashSizeValue = 256;
			this.InitializeKey(key);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001A3C9 File Offset: 0x000185C9
		public override void Initialize()
		{
			this.hashAlgorithmInner.Initialize();
			this.hashAlgorithmOuter.Initialize();
			this.hashing = false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001A3E8 File Offset: 0x000185E8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.hashAlgorithmInner != null)
				{
					this.hashAlgorithmInner.Clear();
					this.hashAlgorithmInner = null;
				}
				if (this.hashAlgorithmOuter != null)
				{
					this.hashAlgorithmOuter.Clear();
					this.hashAlgorithmOuter = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001A428 File Offset: 0x00018628
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.hashing)
			{
				this.hashAlgorithmInner.TransformBlock(this.inner, 0, this.inner.Length, this.inner, 0);
				this.hashing = true;
			}
			this.hashAlgorithmInner.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001A478 File Offset: 0x00018678
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hashAlgorithmInner.TransformBlock(this.inner, 0, this.inner.Length, this.inner, 0);
				this.hashing = true;
			}
			this.hashAlgorithmInner.TransformFinalBlock(new byte[0], 0, 0);
			byte[] hash = this.hashAlgorithmInner.Hash;
			this.hashAlgorithmOuter.TransformBlock(this.outer, 0, this.outer.Length, this.outer, 0);
			this.hashAlgorithmOuter.TransformBlock(hash, 0, hash.Length, hash, 0);
			this.hashing = false;
			this.hashAlgorithmOuter.TransformFinalBlock(new byte[0], 0, 0);
			return this.hashAlgorithmOuter.Hash;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001A534 File Offset: 0x00018734
		private static byte[] GenerateRandomKey()
		{
			byte[] array = new byte[64];
			RandomNumberGenerator.Create().GetBytes(array);
			return array;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001A558 File Offset: 0x00018758
		private void InitializeKey(byte[] key)
		{
			this.inner = null;
			this.outer = null;
			if (key.Length > 64)
			{
				this.KeyValue = this.hashAlgorithmInner.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			if (this.inner == null)
			{
				this.inner = new byte[64];
			}
			if (this.outer == null)
			{
				this.outer = new byte[64];
			}
			for (int i = 0; i < 64; i++)
			{
				this.inner[i] = 54;
				this.outer[i] = 92;
			}
			for (int j = 0; j < this.KeyValue.Length; j++)
			{
				this.inner[j] = this.inner[j] ^ this.KeyValue[j];
				this.outer[j] = this.outer[j] ^ this.KeyValue[j];
			}
		}

		// Token: 0x040002B8 RID: 696
		private const int BlockSizeValue = 64;

		// Token: 0x040002B9 RID: 697
		private HashAlgorithm hashAlgorithmInner;

		// Token: 0x040002BA RID: 698
		private HashAlgorithm hashAlgorithmOuter;

		// Token: 0x040002BB RID: 699
		private bool hashing;

		// Token: 0x040002BC RID: 700
		private byte[] inner;

		// Token: 0x040002BD RID: 701
		private byte[] outer;
	}
}
