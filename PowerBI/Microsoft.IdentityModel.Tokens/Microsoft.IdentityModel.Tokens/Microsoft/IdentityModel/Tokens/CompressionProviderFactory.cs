using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000126 RID: 294
	public class CompressionProviderFactory
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x000399A9 File Offset: 0x00037BA9
		public CompressionProviderFactory()
		{
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000399B1 File Offset: 0x00037BB1
		public CompressionProviderFactory(CompressionProviderFactory other)
		{
			if (other == null)
			{
				throw LogHelper.LogArgumentNullException("other");
			}
			this.CustomCompressionProvider = other.CustomCompressionProvider;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000399D3 File Offset: 0x00037BD3
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000399DA File Offset: 0x00037BDA
		public static CompressionProviderFactory Default
		{
			get
			{
				return CompressionProviderFactory._default;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("Default");
				}
				CompressionProviderFactory._default = value;
			}
		} = new CompressionProviderFactory();

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000399F1 File Offset: 0x00037BF1
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000399F9 File Offset: 0x00037BF9
		public ICompressionProvider CustomCompressionProvider { get; set; }

		// Token: 0x06000E7D RID: 3709 RVA: 0x00039A02 File Offset: 0x00037C02
		public virtual bool IsSupportedAlgorithm(string algorithm)
		{
			return (this.CustomCompressionProvider != null && this.CustomCompressionProvider.IsSupportedAlgorithm(algorithm)) || CompressionProviderFactory.IsSupportedCompressionAlgorithm(algorithm);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00039A22 File Offset: 0x00037C22
		private static bool IsSupportedCompressionAlgorithm(string algorithm)
		{
			return "DEF".Equals(algorithm);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00039A2F File Offset: 0x00037C2F
		public ICompressionProvider CreateCompressionProvider(string algorithm)
		{
			return this.CreateCompressionProvider(algorithm, 256000);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00039A40 File Offset: 0x00037C40
		public ICompressionProvider CreateCompressionProvider(string algorithm, int maximumDeflateSize)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (this.CustomCompressionProvider != null && this.CustomCompressionProvider.IsSupportedAlgorithm(algorithm))
			{
				return this.CustomCompressionProvider;
			}
			if (algorithm.Equals("DEF"))
			{
				return new DeflateCompressionProvider
				{
					MaximumDeflateSize = maximumDeflateSize
				};
			}
			throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
		}

		// Token: 0x040004A4 RID: 1188
		private static CompressionProviderFactory _default;
	}
}
