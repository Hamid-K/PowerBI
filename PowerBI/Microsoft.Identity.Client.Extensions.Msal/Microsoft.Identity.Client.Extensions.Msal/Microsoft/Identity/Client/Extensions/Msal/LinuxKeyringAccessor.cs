using System;
using System.Runtime.InteropServices;
using Microsoft.Identity.Extensions;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000013 RID: 19
	internal class LinuxKeyringAccessor : ICacheAccessor
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000025D4 File Offset: 0x000007D4
		public LinuxKeyringAccessor(string cacheFilePath, string keyringCollection, string keyringSchemaName, string keyringSecretLabel, string attributeKey1, string attributeValue1, string attributeKey2, string attributeValue2, TraceSourceLogger logger)
		{
			if (string.IsNullOrWhiteSpace(cacheFilePath))
			{
				throw new ArgumentNullException("cacheFilePath");
			}
			if (string.IsNullOrWhiteSpace(attributeKey1))
			{
				throw new ArgumentNullException("attributeKey1");
			}
			if (string.IsNullOrWhiteSpace(attributeValue1))
			{
				throw new ArgumentNullException("attributeValue1");
			}
			if (string.IsNullOrWhiteSpace(attributeKey2))
			{
				throw new ArgumentNullException("attributeKey2");
			}
			if (string.IsNullOrWhiteSpace(attributeValue2))
			{
				throw new ArgumentNullException("attributeValue2");
			}
			this._cacheFilePath = cacheFilePath;
			this._keyringCollection = keyringCollection;
			this._keyringSchemaName = keyringSchemaName;
			this._keyringSecretLabel = keyringSecretLabel;
			this._attributeKey1 = attributeKey1;
			this._attributeValue1 = attributeValue1;
			this._attributeKey2 = attributeKey2;
			this._attributeValue2 = attributeValue2;
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000026AC File Offset: 0x000008AC
		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new LinuxKeyringAccessor(this._cacheFilePath + ".test", this._keyringCollection, this._keyringSchemaName, "MSAL Persistence Test", this._attributeKey1, "test", this._attributeKey2, "test", this._logger);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026FC File Offset: 0x000008FC
		public void Clear()
		{
			this._logger.LogInformation("Clearing cache");
			FileIOWithRetries.DeleteCacheFile(this._cacheFilePath, this._logger);
			this._logger.LogInformation("Before deleting secret from Linux keyring");
			IntPtr intPtr;
			Libsecret.secret_password_clear_sync(this.GetLibsecretSchema(), IntPtr.Zero, out intPtr, this._attributeKey1, this._attributeValue1, this._attributeKey2, this._attributeValue2, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					GError gerror = (GError)Marshal.PtrToStructure(intPtr, typeof(GError));
					throw new InteropException(string.Format("An error was encountered while clearing secret from keyring in the {0} domain:'{1}' code:'{2}' message:'{3}'", new object[] { "Storage", gerror.Domain, gerror.Code, gerror.Message }), gerror.Code);
				}
				catch (Exception ex)
				{
					throw new InteropException(string.Format("An exception was encountered while processing libsecret error information during clearing secret in the {0} ex:'{1}'", "Storage", ex), 0, ex);
				}
			}
			this._logger.LogInformation("After deleting secret from linux keyring");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002810 File Offset: 0x00000A10
		public byte[] Read()
		{
			this._logger.LogInformation("ReadDataCore, Before reading from linux keyring");
			byte[] array = null;
			IntPtr intPtr;
			string text = Libsecret.secret_password_lookup_sync(this.GetLibsecretSchema(), IntPtr.Zero, out intPtr, this._attributeKey1, this._attributeValue1, this._attributeKey2, this._attributeValue2, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					GError gerror = (GError)Marshal.PtrToStructure(intPtr, typeof(GError));
					throw new InteropException(string.Format("An error was encountered while reading secret from keyring in the {0} domain:'{1}' code:'{2}' message:'{3}'", new object[] { "Storage", gerror.Domain, gerror.Code, gerror.Message }), gerror.Code);
				}
				catch (Exception ex)
				{
					throw new InteropException(string.Format("An exception was encountered while processing libsecret error information during reading in the {0} ex:'{1}'", "Storage", ex), 0, ex);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				this._logger.LogWarning("No matching secret found in the keyring");
			}
			else
			{
				this._logger.LogInformation("Base64 decoding the secret string");
				array = Convert.FromBase64String(text);
				this._logger.LogInformation(string.Format("ReadDataCore, read '{0}' bytes from the keyring", (array != null) ? new int?(array.Length) : null));
			}
			return array;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000295C File Offset: 0x00000B5C
		public void Write(byte[] data)
		{
			this._logger.LogInformation("Before saving to linux keyring");
			IntPtr intPtr;
			Libsecret.secret_password_store_sync(this.GetLibsecretSchema(), this._keyringCollection, this._keyringSecretLabel, Convert.ToBase64String(data), IntPtr.Zero, out intPtr, this._attributeKey1, this._attributeValue1, this._attributeKey2, this._attributeValue2, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					GError gerror = (GError)Marshal.PtrToStructure(intPtr, typeof(GError));
					throw new InteropException(string.Format("An error was encountered while saving secret to keyring in the {0} domain:'{1}' code:'{2}' message:'{3}'", new object[] { "Storage", gerror.Domain, gerror.Code, gerror.Message }), gerror.Code);
				}
				catch (Exception ex)
				{
					throw new InteropException("An exception was encountered while processing libsecret error information during saving in the Storage", 0, ex);
				}
			}
			this._logger.LogInformation("After saving to linux keyring");
			FileIOWithRetries.TouchFile(this._cacheFilePath, this._logger);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A68 File Offset: 0x00000C68
		private IntPtr GetLibsecretSchema()
		{
			if (this._libsecretSchema == IntPtr.Zero)
			{
				this._logger.LogInformation("Before creating libsecret schema");
				this._libsecretSchema = Libsecret.secret_schema_new(this._keyringSchemaName, 2, this._attributeKey1, 0, this._attributeKey2, 0, IntPtr.Zero);
				if (this._libsecretSchema == IntPtr.Zero)
				{
					throw new InteropException("Failed to create libsecret schema from the {nameof(Storage)}", 0);
				}
				this._logger.LogInformation("After creating libsecret schema");
			}
			return this._libsecretSchema;
		}

		// Token: 0x04000048 RID: 72
		private readonly TraceSourceLogger _logger;

		// Token: 0x04000049 RID: 73
		private IntPtr _libsecretSchema = IntPtr.Zero;

		// Token: 0x0400004A RID: 74
		private readonly string _cacheFilePath;

		// Token: 0x0400004B RID: 75
		private readonly string _keyringCollection;

		// Token: 0x0400004C RID: 76
		private readonly string _keyringSchemaName;

		// Token: 0x0400004D RID: 77
		private readonly string _keyringSecretLabel;

		// Token: 0x0400004E RID: 78
		private readonly string _attributeKey1;

		// Token: 0x0400004F RID: 79
		private readonly string _attributeValue1;

		// Token: 0x04000050 RID: 80
		private readonly string _attributeKey2;

		// Token: 0x04000051 RID: 81
		private readonly string _attributeValue2;
	}
}
