using System;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C0E RID: 3086
	public class AceSourceFile : IDisposable
	{
		// Token: 0x0600542D RID: 21549 RVA: 0x00120C50 File Offset: 0x0011EE50
		public AceSourceFile(IEngineHost host, BinaryValue source, string extension, bool supportsImpersonation)
		{
			this.host = host;
			this.path = DataSource.GetSourceFilePath(host, source, extension, supportsImpersonation, out this.isTempFile, out this.impersonate);
			if (this.isTempFile)
			{
				DataSource.EnsureNotHtmlResponse(source);
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x0600542E RID: 21550 RVA: 0x00120C89 File Offset: 0x0011EE89
		public bool IsTempFile
		{
			get
			{
				return this.isTempFile;
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x00120C91 File Offset: 0x0011EE91
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06005430 RID: 21552 RVA: 0x00120C99 File Offset: 0x0011EE99
		public Func<IDisposable> Impersonate
		{
			get
			{
				return this.impersonate;
			}
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x00120CA1 File Offset: 0x0011EEA1
		public void Dispose()
		{
			if (this.isTempFile)
			{
				FileOperations.TryDelete(this.host, this.path, null);
			}
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x00120CC0 File Offset: 0x0011EEC0
		public static void ThrowIfProviderMissing(Exception e, string dataSourceName, string fileName, bool isTempFile)
		{
			if (e.Message.Contains(AccessDatabaseEngine.ProviderName) && !e.Message.Contains("https://go.microsoft.com/fwlink/?LinkID=285987"))
			{
				int num = (X64Helper.Is64BitProcess ? 64 : 32);
				string text = (isTempFile ? Strings.AccessDatabaseEngine2010Missing(e.Message, num, "https://go.microsoft.com/fwlink/?LinkID=285987").ToString() : Strings.AccessDatabaseEngine2010Missing_WithFileName(e.Message, num, fileName, "https://go.microsoft.com/fwlink/?LinkID=285987"));
				throw ValueException.NewDataSourceNotFound<Message2>(DataSourceException.DataSourceMessage(dataSourceName, text), Value.Null, e);
			}
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00120D58 File Offset: 0x0011EF58
		public static void ThrowProviderBitnessMismatch(Exception e, string dataSourceName, string fileName, bool isTempFile)
		{
			int num = (X64Helper.Is64BitProcess ? 64 : 32);
			string text = (isTempFile ? Strings.AccessDatabaseEngine2010BitnessMismatch(e.Message, num, "https://go.microsoft.com/fwlink/?LinkID=285987").ToString() : Strings.AccessDatabaseEngine2010BitnessMismatch_WithFileName(e.Message, num, fileName, "https://go.microsoft.com/fwlink/?LinkID=285987"));
			throw ValueException.NewDataSourceNotFound<Message2>(DataSourceException.DataSourceMessage(dataSourceName, text), Value.Null, e);
		}

		// Token: 0x04002E84 RID: 11908
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkID=285987";

		// Token: 0x04002E85 RID: 11909
		private readonly IEngineHost host;

		// Token: 0x04002E86 RID: 11910
		private readonly string path;

		// Token: 0x04002E87 RID: 11911
		private readonly bool isTempFile;

		// Token: 0x04002E88 RID: 11912
		private readonly Func<IDisposable> impersonate;
	}
}
