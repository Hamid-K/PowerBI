using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002092 RID: 8338
	public static class StorageExceptions
	{
		// Token: 0x0600CC12 RID: 52242 RVA: 0x00289A62 File Offset: 0x00287C62
		public static Exception StorageException(string message, Exception innerException = null)
		{
			return StorageExceptions.StorageExceptionConstructor(message, innerException);
		}

		// Token: 0x0600CC13 RID: 52243 RVA: 0x00289A70 File Offset: 0x00287C70
		public static Exception CredentialValidationException(string message, Exception innerException = null)
		{
			return StorageExceptions.CredentialValidationExceptionConstructor(message, innerException);
		}

		// Token: 0x0600CC14 RID: 52244 RVA: 0x00289A7E File Offset: 0x00287C7E
		private static Exception DefaultStorageExceptionConstructor(string message, Exception innerException)
		{
			return new StorageException(message, innerException);
		}

		// Token: 0x0600CC15 RID: 52245 RVA: 0x00289A87 File Offset: 0x00287C87
		private static Exception DefaultCredentialValidationExceptionConstructor(string message, Exception innerException)
		{
			return new CredentialValidationException(message, innerException);
		}

		// Token: 0x04006775 RID: 26485
		public static Func<string, Exception, Exception> StorageExceptionConstructor = new Func<string, Exception, Exception>(StorageExceptions.DefaultStorageExceptionConstructor);

		// Token: 0x04006776 RID: 26486
		public static Func<string, Exception, Exception> CredentialValidationExceptionConstructor = new Func<string, Exception, Exception>(StorageExceptions.DefaultCredentialValidationExceptionConstructor);
	}
}
