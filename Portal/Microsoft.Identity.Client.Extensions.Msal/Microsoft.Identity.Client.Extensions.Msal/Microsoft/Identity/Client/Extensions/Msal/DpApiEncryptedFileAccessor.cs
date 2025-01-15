using System;
using System.Security.Cryptography;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000010 RID: 16
	internal class DpApiEncryptedFileAccessor : ICacheAccessor
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000023D0 File Offset: 0x000005D0
		public DpApiEncryptedFileAccessor(string cacheFilePath, TraceSourceLogger logger)
		{
			if (string.IsNullOrEmpty(cacheFilePath))
			{
				throw new ArgumentNullException("cacheFilePath");
			}
			this._cacheFilePath = cacheFilePath;
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._unencryptedFileAccessor = new FileAccessor(this._cacheFilePath, false, this._logger);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000242B File Offset: 0x0000062B
		public void Clear()
		{
			this._logger.LogInformation("Clearing cache");
			this._unencryptedFileAccessor.Clear();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002448 File Offset: 0x00000648
		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new DpApiEncryptedFileAccessor(this._cacheFilePath + ".test", this._logger);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002468 File Offset: 0x00000668
		public byte[] Read()
		{
			byte[] array = this._unencryptedFileAccessor.Read();
			if (array != null && array.Length != 0)
			{
				this._logger.LogInformation("Unprotecting the data");
				array = ProtectedData.Unprotect(array, null, 0);
			}
			return array;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000024A2 File Offset: 0x000006A2
		public void Write(byte[] data)
		{
			if (data.Length != 0)
			{
				this._logger.LogInformation("Protecting the data");
				data = ProtectedData.Protect(data, null, 0);
			}
			this._unencryptedFileAccessor.Write(data);
		}

		// Token: 0x04000042 RID: 66
		private readonly string _cacheFilePath;

		// Token: 0x04000043 RID: 67
		private readonly TraceSourceLogger _logger;

		// Token: 0x04000044 RID: 68
		private readonly ICacheAccessor _unencryptedFileAccessor;
	}
}
