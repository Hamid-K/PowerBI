using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Mashup.Security.Cryptography;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F2 RID: 6642
	public sealed class KeyHasher
	{
		// Token: 0x17002ACA RID: 10954
		// (get) Token: 0x0600A800 RID: 43008 RVA: 0x0022BA92 File Offset: 0x00229C92
		private static Func<HashAlgorithm> HashAlgorithmFactory
		{
			get
			{
				if (KeyHasher.hashAlgorithmFactory == null)
				{
					KeyHasher.hashAlgorithmFactory = CryptoConfig2.CreateFactoryFromName("SHA256Cng");
				}
				return () => (HashAlgorithm)KeyHasher.hashAlgorithmFactory();
			}
		}

		// Token: 0x0600A801 RID: 43009 RVA: 0x0022BAC9 File Offset: 0x00229CC9
		public KeyHasher()
		{
			this.hashAlgorithms = new Stack<HashAlgorithm>();
		}

		// Token: 0x0600A802 RID: 43010 RVA: 0x0022BADC File Offset: 0x00229CDC
		public string HashKey(string cacheKey)
		{
			HashAlgorithm hashAlgorithm = this.TakeHashAlgorithm();
			string text = Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(cacheKey)));
			this.ReturnHashAlgorithm(hashAlgorithm);
			return text;
		}

		// Token: 0x0600A803 RID: 43011 RVA: 0x0022BB10 File Offset: 0x00229D10
		private HashAlgorithm TakeHashAlgorithm()
		{
			HashAlgorithm hashAlgorithm = null;
			Stack<HashAlgorithm> stack = this.hashAlgorithms;
			lock (stack)
			{
				if (this.hashAlgorithms.Count > 0)
				{
					hashAlgorithm = this.hashAlgorithms.Pop();
				}
			}
			if (hashAlgorithm == null)
			{
				hashAlgorithm = KeyHasher.HashAlgorithmFactory();
			}
			return hashAlgorithm;
		}

		// Token: 0x0600A804 RID: 43012 RVA: 0x0022BB78 File Offset: 0x00229D78
		private void ReturnHashAlgorithm(HashAlgorithm hashAlgorithm)
		{
			Stack<HashAlgorithm> stack = this.hashAlgorithms;
			lock (stack)
			{
				if (this.hashAlgorithms.Count < 64)
				{
					this.hashAlgorithms.Push(hashAlgorithm);
					hashAlgorithm = null;
				}
			}
			if (hashAlgorithm != null)
			{
				((IDisposable)hashAlgorithm).Dispose();
			}
		}

		// Token: 0x04005777 RID: 22391
		private const int maxCacheSize = 64;

		// Token: 0x04005778 RID: 22392
		private readonly Stack<HashAlgorithm> hashAlgorithms;

		// Token: 0x04005779 RID: 22393
		private static Func<object> hashAlgorithmFactory;
	}
}
