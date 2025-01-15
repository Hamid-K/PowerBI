using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Security;
using Microsoft.Mashup.Security.Cryptography;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019CE RID: 6606
	public abstract class EncryptedPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x17002A96 RID: 10902
		// (get) Token: 0x0600A73F RID: 42815 RVA: 0x00229B30 File Offset: 0x00227D30
		private static Func<HMAC> HMACFactory
		{
			get
			{
				if (EncryptedPersistentCache.hashAlgorithmFactory == null)
				{
					EncryptedPersistentCache.hashAlgorithmFactory = CryptoConfig2.CreateFactoryFromName("HMACSHA256Cng");
				}
				return () => (HMAC)EncryptedPersistentCache.hashAlgorithmFactory();
			}
		}

		// Token: 0x0600A740 RID: 42816 RVA: 0x00229B68 File Offset: 0x00227D68
		protected EncryptedPersistentCache(PersistentCache cache, SymmetricAlgorithm algorithm, string encryptionKeyPathName)
			: base(cache)
		{
			this.algorithm = algorithm;
			this.encryptionKeyPathName = encryptionKeyPathName;
			using (SHA256 sha = (SHA256)CryptoConfig2.CreateFactoryFromName("SHA256Cng")())
			{
				string text = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(encryptionKeyPathName)));
				text = text.Replace(Path.DirectorySeparatorChar, '_');
				this.mutex = MutexFactory.Create(false, "DataExplorerEncryptionKeyMutex_" + text);
			}
			this.hmacLock = new object();
			this.maxEntryLength = -1L;
		}

		// Token: 0x17002A97 RID: 10903
		// (get) Token: 0x0600A741 RID: 42817 RVA: 0x00229C18 File Offset: 0x00227E18
		private RandomNumberGenerator Random
		{
			get
			{
				if (this.random == null)
				{
					this.random = RNGCryptoProvider.Create();
				}
				return this.random;
			}
		}

		// Token: 0x17002A98 RID: 10904
		// (get) Token: 0x0600A742 RID: 42818 RVA: 0x00229C33 File Offset: 0x00227E33
		private byte[] EncryptionKey
		{
			get
			{
				if (this.encryptionKey == null)
				{
					this.encryptionKey = this.GetEncryptionKey();
				}
				return this.encryptionKey;
			}
		}

		// Token: 0x17002A99 RID: 10905
		// (get) Token: 0x0600A743 RID: 42819 RVA: 0x00229C4F File Offset: 0x00227E4F
		public override long MaxEntryLength
		{
			get
			{
				if (this.maxEntryLength == -1L)
				{
					this.maxEntryLength = this.algorithm.GetDecryptedLength(65536, base.MaxEntryLength);
				}
				return this.maxEntryLength;
			}
		}

		// Token: 0x0600A744 RID: 42820 RVA: 0x00229C7D File Offset: 0x00227E7D
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			if (!base.TryGetStorage(this.EncryptCacheKey(key), maxStaleness, minVersion, out storage))
			{
				storage = null;
				return false;
			}
			storage = new EncryptedPersistentCache.EncryptedStorage(this.algorithm, this.Random, this.EncryptionKey, storage);
			return true;
		}

		// Token: 0x0600A745 RID: 42821 RVA: 0x00229CB6 File Offset: 0x00227EB6
		public override IStorage CreateStorage()
		{
			return new EncryptedPersistentCache.EncryptedStorage(this.algorithm, this.Random, this.EncryptionKey, base.CreateStorage());
		}

		// Token: 0x0600A746 RID: 42822 RVA: 0x00229CD5 File Offset: 0x00227ED5
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			base.CommitStorage(this.EncryptCacheKey(key), maxVersion, ((EncryptedPersistentCache.EncryptedStorage)storage).Storage);
		}

		// Token: 0x0600A747 RID: 42823 RVA: 0x00229CF0 File Offset: 0x00227EF0
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			int num = (int)this.algorithm.GetEncryptedLength(65536, (long)pageSize);
			int num2 = (int)this.algorithm.GetDecryptedLength(65536, (long)num);
			IPagedStorage pagedStorage = this.Cache.OpenStorage(this.EncryptCacheKey(key), maxStaleness, minVersion, num, maxPageCount);
			pagedStorage = new EncryptedPersistentCache.EncryptedPagedStorage(this.algorithm, this.Random, this.EncryptionKey, num2, pagedStorage);
			if (pageSize != num2)
			{
				pagedStorage = new PageSizePagedStorage(pagedStorage, pageSize);
			}
			return pagedStorage;
		}

		// Token: 0x0600A748 RID: 42824 RVA: 0x00229D68 File Offset: 0x00227F68
		private byte[] GetRandomEncryptionKey()
		{
			byte[] array = new byte[this.algorithm.KeySize / 8];
			this.Random.GetBytes(array);
			return array;
		}

		// Token: 0x0600A749 RID: 42825 RVA: 0x00229D98 File Offset: 0x00227F98
		private KeyedHashAlgorithm TakeHMac()
		{
			object obj = this.hmacLock;
			KeyedHashAlgorithm keyedHashAlgorithm2;
			lock (obj)
			{
				KeyedHashAlgorithm keyedHashAlgorithm = this.hmac;
				this.hmac = null;
				if (keyedHashAlgorithm == null)
				{
					keyedHashAlgorithm = EncryptedPersistentCache.HMACFactory();
					keyedHashAlgorithm.Key = this.EncryptionKey;
				}
				keyedHashAlgorithm2 = keyedHashAlgorithm;
			}
			return keyedHashAlgorithm2;
		}

		// Token: 0x0600A74A RID: 42826 RVA: 0x00229E00 File Offset: 0x00228000
		private void ReturnHMac(KeyedHashAlgorithm hmac)
		{
			object obj = this.hmacLock;
			lock (obj)
			{
				if (this.hmac == null)
				{
					this.hmac = hmac;
				}
				else
				{
					((IDisposable)hmac).Dispose();
				}
			}
		}

		// Token: 0x0600A74B RID: 42827 RVA: 0x00229E54 File Offset: 0x00228054
		private string EncryptCacheKey(string cacheKey)
		{
			KeyedHashAlgorithm keyedHashAlgorithm = this.TakeHMac();
			string text = Convert.ToBase64String(keyedHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(cacheKey)));
			this.ReturnHMac(keyedHashAlgorithm);
			return text;
		}

		// Token: 0x0600A74C RID: 42828 RVA: 0x00229E88 File Offset: 0x00228088
		private byte[] GetEncryptionKey()
		{
			try
			{
				this.mutex.WaitOne();
			}
			catch (AbandonedMutexException)
			{
			}
			byte[] array3;
			try
			{
				using (Stream stream = new FileStream(this.encryptionKeyPathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						if (binaryReader.ReadInt32() != 3)
						{
							throw new IOException();
						}
						int num = binaryReader.ReadInt32();
						if (num < 0 || num > 1024)
						{
							throw new IOException();
						}
						byte[] array = binaryReader.ReadBytes(num);
						if (stream.ReadByte() != -1)
						{
							throw new IOException();
						}
						byte[] array2 = this.UnencryptEncryptionKey(array);
						if (!this.algorithm.ValidKeySize(array2.Length * 8))
						{
							throw new IOException();
						}
						array3 = array2;
					}
				}
			}
			catch (IOException)
			{
				array3 = this.CreateEncryptionKey();
			}
			catch (CryptographicException)
			{
				array3 = this.CreateEncryptionKey();
			}
			finally
			{
				this.mutex.ReleaseMutex();
			}
			return array3;
		}

		// Token: 0x0600A74D RID: 42829 RVA: 0x00229FA8 File Offset: 0x002281A8
		private byte[] CreateEncryptionKey()
		{
			byte[] randomEncryptionKey = this.GetRandomEncryptionKey();
			byte[] array = this.EncryptEncryptionKey(randomEncryptionKey);
			using (Stream stream = EncryptedPersistentCache.CreateStream(this.encryptionKeyPathName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(stream))
				{
					binaryWriter.Write(3);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array, 0, array.Length);
				}
			}
			return randomEncryptionKey;
		}

		// Token: 0x0600A74E RID: 42830 RVA: 0x0022A028 File Offset: 0x00228228
		private static Stream CreateStream(string pathName, FileMode mode, FileAccess access, FileShare share)
		{
			Stream stream;
			try
			{
				stream = new FileStream(pathName, mode, access, share);
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(pathName));
				stream = new FileStream(pathName, mode, access, share);
			}
			return stream;
		}

		// Token: 0x0600A74F RID: 42831
		protected abstract byte[] UnencryptEncryptionKey(byte[] encryptedEncryptionKey);

		// Token: 0x0600A750 RID: 42832
		protected abstract byte[] EncryptEncryptionKey(byte[] encryptionKey);

		// Token: 0x0400570D RID: 22285
		public const int EncryptionPageSize = 65536;

		// Token: 0x0400570E RID: 22286
		private const int version = 3;

		// Token: 0x0400570F RID: 22287
		private readonly SymmetricAlgorithm algorithm;

		// Token: 0x04005710 RID: 22288
		private string encryptionKeyPathName;

		// Token: 0x04005711 RID: 22289
		private RandomNumberGenerator random;

		// Token: 0x04005712 RID: 22290
		private readonly Mutex mutex;

		// Token: 0x04005713 RID: 22291
		private readonly object hmacLock = new object();

		// Token: 0x04005714 RID: 22292
		private KeyedHashAlgorithm hmac;

		// Token: 0x04005715 RID: 22293
		private byte[] encryptionKey;

		// Token: 0x04005716 RID: 22294
		private long maxEntryLength;

		// Token: 0x04005717 RID: 22295
		private static Func<object> hashAlgorithmFactory;

		// Token: 0x020019CF RID: 6607
		private class EncryptedStorage : IStorage, IDisposable
		{
			// Token: 0x0600A751 RID: 42833 RVA: 0x0022A06C File Offset: 0x0022826C
			public EncryptedStorage(SymmetricAlgorithm algorithm, RandomNumberGenerator random, byte[] encryptionKey, IStorage storage)
			{
				this.algorithm = algorithm;
				this.random = random;
				this.encryptionKey = encryptionKey;
				this.storage = storage;
			}

			// Token: 0x17002A9A RID: 10906
			// (get) Token: 0x0600A752 RID: 42834 RVA: 0x0022A091 File Offset: 0x00228291
			public IStorage Storage
			{
				get
				{
					return this.storage;
				}
			}

			// Token: 0x17002A9B RID: 10907
			// (get) Token: 0x0600A753 RID: 42835 RVA: 0x0022A099 File Offset: 0x00228299
			public IEnumerable<int> StreamIds
			{
				get
				{
					return this.storage.StreamIds;
				}
			}

			// Token: 0x0600A754 RID: 42836 RVA: 0x0022A0A6 File Offset: 0x002282A6
			public Stream OpenStream(int id)
			{
				return new EncryptedPersistentCache.StreamEncryptor(this.algorithm, this.random, this.encryptionKey).EncryptDecryptStream(this.storage.OpenStream(id));
			}

			// Token: 0x0600A755 RID: 42837 RVA: 0x0022A0D0 File Offset: 0x002282D0
			public Stream CreateStream()
			{
				return new EncryptedPersistentCache.StreamEncryptor(this.algorithm, this.random, this.encryptionKey).EncryptDecryptStream(this.storage.CreateStream());
			}

			// Token: 0x0600A756 RID: 42838 RVA: 0x0022A0FC File Offset: 0x002282FC
			public Stream CommitStream(int id, Stream stream)
			{
				TransformingPagedStream transformingPagedStream = (TransformingPagedStream)stream;
				transformingPagedStream.Flush();
				return new EncryptedPersistentCache.StreamEncryptor(this.algorithm, this.random, this.encryptionKey).EncryptDecryptStream(this.storage.CommitStream(id, transformingPagedStream.InputStream));
			}

			// Token: 0x0600A757 RID: 42839 RVA: 0x0022A144 File Offset: 0x00228344
			public void Close()
			{
				this.storage.Close();
			}

			// Token: 0x0600A758 RID: 42840 RVA: 0x0022A151 File Offset: 0x00228351
			public void Dispose()
			{
				this.Close();
			}

			// Token: 0x04005718 RID: 22296
			private readonly SymmetricAlgorithm algorithm;

			// Token: 0x04005719 RID: 22297
			private readonly RandomNumberGenerator random;

			// Token: 0x0400571A RID: 22298
			private readonly byte[] encryptionKey;

			// Token: 0x0400571B RID: 22299
			private readonly IStorage storage;
		}

		// Token: 0x020019D0 RID: 6608
		private class EncryptedPagedStorage : IPagedStorage, IDisposable
		{
			// Token: 0x0600A759 RID: 42841 RVA: 0x0022A159 File Offset: 0x00228359
			public EncryptedPagedStorage(SymmetricAlgorithm algorithm, RandomNumberGenerator random, byte[] encryptionKey, int outerPageSize, IPagedStorage innerPagedStorage)
			{
				this.algorithm = algorithm;
				this.random = random;
				this.encryptionKey = encryptionKey;
				this.outerPageSize = outerPageSize;
				this.innerPagedStorage = innerPagedStorage;
			}

			// Token: 0x17002A9C RID: 10908
			// (get) Token: 0x0600A75A RID: 42842 RVA: 0x0022A186 File Offset: 0x00228386
			public int PageSize
			{
				get
				{
					return this.outerPageSize;
				}
			}

			// Token: 0x17002A9D RID: 10909
			// (get) Token: 0x0600A75B RID: 42843 RVA: 0x0022A18E File Offset: 0x0022838E
			public int MaxPageCount
			{
				get
				{
					return this.innerPagedStorage.MaxPageCount;
				}
			}

			// Token: 0x0600A75C RID: 42844 RVA: 0x0022A19C File Offset: 0x0022839C
			public Stream OpenPage(int pageIndex, out bool created)
			{
				EncryptedPersistentCache.StreamEncryptor streamEncryptor = new EncryptedPersistentCache.StreamEncryptor(this.algorithm, this.random, this.encryptionKey);
				Stream stream = this.innerPagedStorage.OpenPage(pageIndex, out created);
				return new EncryptedPersistentCache.EncryptedPagedStorage.EncryptedPagedStorageStream(streamEncryptor.EncryptDecryptStream(stream).Take((long)this.PageSize), stream);
			}

			// Token: 0x0600A75D RID: 42845 RVA: 0x0022A1E8 File Offset: 0x002283E8
			public void CommitPage(Stream stream)
			{
				EncryptedPersistentCache.EncryptedPagedStorage.EncryptedPagedStorageStream encryptedPagedStorageStream = (EncryptedPersistentCache.EncryptedPagedStorage.EncryptedPagedStorageStream)stream;
				if (encryptedPagedStorageStream.Length < (long)this.PageSize)
				{
					encryptedPagedStorageStream.Position = encryptedPagedStorageStream.Length;
					encryptedPagedStorageStream.WriteZeros((long)this.PageSize - encryptedPagedStorageStream.Position);
				}
				encryptedPagedStorageStream.Flush();
				this.innerPagedStorage.CommitPage(encryptedPagedStorageStream.InnerStream);
			}

			// Token: 0x0600A75E RID: 42846 RVA: 0x0022A242 File Offset: 0x00228442
			public void Dispose()
			{
				this.innerPagedStorage.Dispose();
			}

			// Token: 0x0400571C RID: 22300
			private readonly SymmetricAlgorithm algorithm;

			// Token: 0x0400571D RID: 22301
			private readonly RandomNumberGenerator random;

			// Token: 0x0400571E RID: 22302
			private readonly byte[] encryptionKey;

			// Token: 0x0400571F RID: 22303
			private readonly int outerPageSize;

			// Token: 0x04005720 RID: 22304
			private readonly IPagedStorage innerPagedStorage;

			// Token: 0x020019D1 RID: 6609
			private sealed class EncryptedPagedStorageStream : DelegatingStream
			{
				// Token: 0x0600A75F RID: 42847 RVA: 0x0022A24F File Offset: 0x0022844F
				public EncryptedPagedStorageStream(Stream outerStream, Stream innerStream)
					: base(outerStream)
				{
					this.innerStream = innerStream;
				}

				// Token: 0x17002A9E RID: 10910
				// (get) Token: 0x0600A760 RID: 42848 RVA: 0x0022A25F File Offset: 0x0022845F
				public Stream InnerStream
				{
					get
					{
						return this.innerStream;
					}
				}

				// Token: 0x04005721 RID: 22305
				private readonly Stream innerStream;
			}
		}

		// Token: 0x020019D2 RID: 6610
		private class StreamEncryptor
		{
			// Token: 0x0600A761 RID: 42849 RVA: 0x0022A267 File Offset: 0x00228467
			public StreamEncryptor(SymmetricAlgorithm algorithm, RandomNumberGenerator random, byte[] encryptionKey)
			{
				this.algorithm = algorithm;
				this.blockSizeInBytes = this.algorithm.GetBlockSizeInBytes();
				this.random = random;
				this.encryptionKey = encryptionKey;
				this.tempIV = new byte[this.blockSizeInBytes];
			}

			// Token: 0x17002A9F RID: 10911
			// (get) Token: 0x0600A762 RID: 42850 RVA: 0x0022A2A6 File Offset: 0x002284A6
			private int DecryptedPageSize
			{
				get
				{
					return (int)PageHelpers.Align(65536L, (long)this.blockSizeInBytes);
				}
			}

			// Token: 0x17002AA0 RID: 10912
			// (get) Token: 0x0600A763 RID: 42851 RVA: 0x0022A2BB File Offset: 0x002284BB
			private int EncryptedPageSize
			{
				get
				{
					return this.algorithm.GetEncryptedPageSize(65536);
				}
			}

			// Token: 0x0600A764 RID: 42852 RVA: 0x0022A2CD File Offset: 0x002284CD
			public Stream EncryptDecryptStream(Stream stream)
			{
				return new TransformingPagedStream(stream, this.EncryptedPageSize, this.DecryptedPageSize, new Func<byte[], int, byte[], int>(this.DecryptPage), new Func<byte[], int, byte[], int>(this.EncryptPage));
			}

			// Token: 0x0600A765 RID: 42853 RVA: 0x0022A2FC File Offset: 0x002284FC
			private int DecryptPage(byte[] input, int count, byte[] output)
			{
				Buffer.BlockCopy(input, 0, this.tempIV, 0, this.blockSizeInBytes);
				int num;
				using (ICryptoTransform cryptoTransform = this.algorithm.CreateDecryptor(this.encryptionKey, this.tempIV))
				{
					num = this.TransformBlocks(cryptoTransform, input, this.blockSizeInBytes, count - this.blockSizeInBytes, output, 0);
				}
				return num;
			}

			// Token: 0x0600A766 RID: 42854 RVA: 0x0022A36C File Offset: 0x0022856C
			private int EncryptPage(byte[] input, int count, byte[] output)
			{
				this.random.GetBytes(this.tempIV);
				Buffer.BlockCopy(this.tempIV, 0, output, 0, this.blockSizeInBytes);
				int num;
				using (ICryptoTransform cryptoTransform = this.algorithm.CreateEncryptor(this.encryptionKey, this.tempIV))
				{
					num = this.TransformBlocks(cryptoTransform, input, 0, count, output, this.blockSizeInBytes) + this.blockSizeInBytes;
				}
				return num;
			}

			// Token: 0x0600A767 RID: 42855 RVA: 0x0022A3EC File Offset: 0x002285EC
			private int TransformBlocks(ICryptoTransform transform, byte[] input, int inputOffset, int inputCount, byte[] output, int outputOffset)
			{
				int num = 0;
				for (;;)
				{
					int num2 = inputCount / this.blockSizeInBytes * this.blockSizeInBytes;
					if (inputCount - num2 == 0)
					{
						num2 -= this.blockSizeInBytes;
					}
					if (num2 < this.blockSizeInBytes)
					{
						break;
					}
					int num3 = transform.TransformBlock(input, inputOffset, num2, output, outputOffset);
					num += num3;
					outputOffset += num3;
					inputOffset += num2;
					inputCount -= num2;
				}
				byte[] array = transform.TransformFinalBlock(input, inputOffset, inputCount);
				Buffer.BlockCopy(array, 0, output, outputOffset, array.Length);
				return num + array.Length;
			}

			// Token: 0x04005722 RID: 22306
			private readonly SymmetricAlgorithm algorithm;

			// Token: 0x04005723 RID: 22307
			private readonly int blockSizeInBytes;

			// Token: 0x04005724 RID: 22308
			private readonly RandomNumberGenerator random;

			// Token: 0x04005725 RID: 22309
			private readonly byte[] encryptionKey;

			// Token: 0x04005726 RID: 22310
			private readonly byte[] tempIV;
		}
	}
}
