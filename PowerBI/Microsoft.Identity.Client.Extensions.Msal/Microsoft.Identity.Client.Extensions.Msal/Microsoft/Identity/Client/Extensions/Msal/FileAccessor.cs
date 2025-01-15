using System;
using System.IO;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000011 RID: 17
	internal class FileAccessor : ICacheAccessor
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000024CE File Offset: 0x000006CE
		internal FileAccessor(string cacheFilePath, bool setOwnerOnlyPermissions, TraceSourceLogger logger)
		{
			this._cacheFilePath = cacheFilePath;
			this._setOwnerOnlyPermission = setOwnerOnlyPermissions;
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000024FA File Offset: 0x000006FA
		public void Clear()
		{
			this._logger.LogInformation("Deleting cache file");
			FileIOWithRetries.DeleteCacheFile(this._cacheFilePath, this._logger);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000251D File Offset: 0x0000071D
		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new FileAccessor(this._cacheFilePath + ".test", this._setOwnerOnlyPermission, this._logger);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002540 File Offset: 0x00000740
		public byte[] Read()
		{
			this._logger.LogInformation("Reading from file");
			byte[] fileData = null;
			bool flag = File.Exists(this._cacheFilePath);
			this._logger.LogInformation(string.Format("Cache file exists? '{0}'", flag));
			if (flag)
			{
				FileIOWithRetries.TryProcessFile(delegate
				{
					fileData = File.ReadAllBytes(this._cacheFilePath);
					this._logger.LogInformation(string.Format("Read '{0}' bytes from the file", fileData.Length));
				}, this._logger);
			}
			return fileData;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025B8 File Offset: 0x000007B8
		public void Write(byte[] data)
		{
			FileIOWithRetries.CreateAndWriteToFile(this._cacheFilePath, data, this._setOwnerOnlyPermission, this._logger);
		}

		// Token: 0x04000045 RID: 69
		private readonly string _cacheFilePath;

		// Token: 0x04000046 RID: 70
		private readonly TraceSourceLogger _logger;

		// Token: 0x04000047 RID: 71
		private readonly bool _setOwnerOnlyPermission;
	}
}
