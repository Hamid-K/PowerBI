using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Identity.Extensions;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000023 RID: 35
	public class Storage
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000042E4 File Offset: 0x000024E4
		internal ICacheAccessor CacheAccessor { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000042EC File Offset: 0x000024EC
		internal StorageCreationProperties StorageCreationProperties { get; }

		// Token: 0x0600009F RID: 159 RVA: 0x000042F4 File Offset: 0x000024F4
		public static Storage Create(StorageCreationProperties creationProperties, TraceSource logger = null)
		{
			TraceSourceLogger traceSourceLogger = ((logger == null) ? Storage.s_staticLogger.Value : new TraceSourceLogger(logger));
			ICacheAccessor cacheAccessor;
			if (creationProperties.UseUnencryptedFallback)
			{
				cacheAccessor = new FileAccessor(creationProperties.CacheFilePath, true, traceSourceLogger);
			}
			else if (SharedUtilities.IsWindowsPlatform())
			{
				cacheAccessor = new DpApiEncryptedFileAccessor(creationProperties.CacheFilePath, traceSourceLogger);
			}
			else if (SharedUtilities.IsMacPlatform())
			{
				cacheAccessor = new MacKeychainAccessor(creationProperties.CacheFilePath, creationProperties.MacKeyChainServiceName, creationProperties.MacKeyChainAccountName, traceSourceLogger);
			}
			else
			{
				if (!SharedUtilities.IsLinuxPlatform())
				{
					throw new PlatformNotSupportedException();
				}
				if (creationProperties.UseLinuxUnencryptedFallback)
				{
					cacheAccessor = new FileAccessor(creationProperties.CacheFilePath, true, traceSourceLogger);
				}
				else
				{
					cacheAccessor = new LinuxKeyringAccessor(creationProperties.CacheFilePath, creationProperties.KeyringCollection, creationProperties.KeyringSchemaName, creationProperties.KeyringSecretLabel, creationProperties.KeyringAttribute1.Key, creationProperties.KeyringAttribute1.Value, creationProperties.KeyringAttribute2.Key, creationProperties.KeyringAttribute2.Value, traceSourceLogger);
				}
			}
			return new Storage(creationProperties, cacheAccessor, traceSourceLogger);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000043F3 File Offset: 0x000025F3
		internal Storage(StorageCreationProperties creationProperties, ICacheAccessor cacheAccessor, TraceSourceLogger logger)
		{
			this.StorageCreationProperties = creationProperties;
			this._logger = logger;
			this.CacheAccessor = cacheAccessor;
			this._logger.LogInformation("Initialized 'Storage'");
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004420 File Offset: 0x00002620
		public byte[] ReadData()
		{
			byte[] array2;
			try
			{
				this._logger.LogInformation("Reading Data");
				byte[] array = this.CacheAccessor.Read();
				this._logger.LogInformation(string.Format("Got '{0}' bytes from file storage", (array != null) ? array.Length : 0));
				array2 = array ?? Array.Empty<byte>();
			}
			catch (Exception ex)
			{
				this._logger.LogError(string.Format("An exception was encountered while reading data from the {0} : {1}", "Storage", ex));
				throw;
			}
			return array2;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000044A8 File Offset: 0x000026A8
		public void WriteData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			try
			{
				this._logger.LogInformation(string.Format("Got '{0}' bytes to write to storage", (data != null) ? new int?(data.Length) : null));
				this.CacheAccessor.Write(data);
			}
			catch (Exception ex)
			{
				this._logger.LogError(string.Format("An exception was encountered while writing data to {0} : {1}", "Storage", ex));
				throw;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004530 File Offset: 0x00002730
		public void Clear(bool ignoreExceptions = false)
		{
			try
			{
				this._logger.LogInformation("Clearing the cache file");
				this.CacheAccessor.Clear();
			}
			catch (Exception ex)
			{
				this._logger.LogError(string.Format("An exception was encountered while clearing data from {0} : {1}", "Storage", ex));
				if (!ignoreExceptions)
				{
					throw;
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004590 File Offset: 0x00002790
		public void VerifyPersistence()
		{
			ICacheAccessor cacheAccessor = this.CacheAccessor.CreateForPersistenceValidation();
			try
			{
				this._logger.LogInformation("[Verify Persistence] Writing Data ");
				cacheAccessor.Write(Encoding.UTF8.GetBytes("msal_persistence_test"));
				this._logger.LogInformation("[Verify Persistence] Reading Data ");
				byte[] array = cacheAccessor.Read();
				if (array == null || array.Length == 0)
				{
					throw new MsalCachePersistenceException("Persistence check failed. Data was written but it could not be read. Possible cause: on Linux, LibSecret is installed but D-Bus isn't running because it cannot be started over SSH.");
				}
				string @string = Encoding.UTF8.GetString(array);
				if (!string.Equals("msal_persistence_test", @string, StringComparison.Ordinal))
				{
					throw new MsalCachePersistenceException("Persistence check failed. Data written msal_persistence_test is different from data read " + @string);
				}
			}
			catch (InteropException ex)
			{
				throw new MsalCachePersistenceException(string.Format("Persistence check failed. Reason: {0}. OS error code {1}.", ex.Message, ex.ErrorCode), ex);
			}
			catch (Exception ex2) when (!(ex2 is MsalCachePersistenceException))
			{
				throw new MsalCachePersistenceException("Persistence check failed. Inspect inner exception for details", ex2);
			}
			finally
			{
				try
				{
					this._logger.LogInformation("[Verify Persistence] Clearing data");
					cacheAccessor.Clear();
				}
				catch (Exception ex3)
				{
					TraceSourceLogger logger = this._logger;
					string text = "[Verify Persistence] Could not clear the test data: ";
					Exception ex4 = ex3;
					logger.LogError(text + ((ex4 != null) ? ex4.ToString() : null));
				}
			}
		}

		// Token: 0x04000098 RID: 152
		private readonly TraceSourceLogger _logger;

		// Token: 0x0400009B RID: 155
		internal const string PersistenceValidationDummyData = "msal_persistence_test";

		// Token: 0x0400009C RID: 156
		private static readonly Lazy<TraceSourceLogger> s_staticLogger = new Lazy<TraceSourceLogger>(() => new TraceSourceLogger(EnvUtils.GetNewTraceSource("MsalCacheHelperSingleton")));
	}
}
