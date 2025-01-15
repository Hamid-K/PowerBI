using System;
using System.Buffers;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200012B RID: 299
	public class DeflateCompressionProvider : ICompressionProvider
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003AAD0 File Offset: 0x00038CD0
		public DeflateCompressionProvider()
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003AAE3 File Offset: 0x00038CE3
		public DeflateCompressionProvider(CompressionLevel compressionLevel)
		{
			this.CompressionLevel = compressionLevel;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003AAFD File Offset: 0x00038CFD
		public string Algorithm
		{
			get
			{
				return "DEF";
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0003AB04 File Offset: 0x00038D04
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x0003AB0C File Offset: 0x00038D0C
		public CompressionLevel CompressionLevel { get; private set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0003AB15 File Offset: 0x00038D15
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0003AB1D File Offset: 0x00038D1D
		public int MaximumDeflateSize
		{
			get
			{
				return this._maximumTokenSizeInBytes;
			}
			set
			{
				if (value >= 1)
				{
					this._maximumTokenSizeInBytes = value;
					return;
				}
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10101: MaximumTokenSizeInBytes must be greater than zero. value: '{0}'", new object[] { LogHelper.MarkAsNonPII(value) })));
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public byte[] Decompress(byte[] value)
		{
			if (value == null)
			{
				throw LogHelper.LogArgumentNullException("value");
			}
			char[] array = null;
			byte[] bytes;
			try
			{
				array = ArrayPool<char>.Shared.Rent(this.MaximumDeflateSize);
				using (MemoryStream memoryStream = new MemoryStream(value))
				{
					using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
					{
						using (StreamReader streamReader = new StreamReader(deflateStream, Encoding.UTF8))
						{
							int num = streamReader.Read(array, 0, this.MaximumDeflateSize);
							if (streamReader.Peek() != -1)
							{
								throw LogHelper.LogExceptionMessage(new SecurityTokenDecompressionFailedException(LogHelper.FormatInvariant("IDX10816: Decompressing would result in a token with a size greater than allowed. Maximum size allowed: '{0}'.", new object[] { LogHelper.MarkAsNonPII(this.MaximumDeflateSize) })));
							}
							bytes = Encoding.UTF8.GetBytes(array, 0, num);
						}
					}
				}
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<char>.Shared.Return(array, false);
				}
			}
			return bytes;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003AC64 File Offset: 0x00038E64
		public byte[] Compress(byte[] value)
		{
			if (value == null)
			{
				throw LogHelper.LogArgumentNullException("value");
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (DeflateStream deflateStream = new DeflateStream(memoryStream, this.CompressionLevel))
				{
					using (StreamWriter streamWriter = new StreamWriter(deflateStream, Encoding.UTF8))
					{
						streamWriter.Write(Encoding.UTF8.GetString(value));
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003AD04 File Offset: 0x00038F04
		public bool IsSupportedAlgorithm(string algorithm)
		{
			return this.Algorithm.Equals(algorithm);
		}

		// Token: 0x040004B2 RID: 1202
		private int _maximumTokenSizeInBytes = 256000;
	}
}
