using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C0F RID: 3087
	internal class ExcelFile
	{
		// Token: 0x06005434 RID: 21556 RVA: 0x00120DCC File Offset: 0x0011EFCC
		public ExcelFile(IEngineHost engineHost, BinaryValue content)
		{
			this.engineHost = engineHost;
			this.persistentCache = this.engineHost.GetPersistentCache();
			this.content = content;
			this.actualContent = content;
			if (!DataSource.TryGetFileExtensionAndResource(content, out this.fileExtension, out this.resource))
			{
				this.fileExtension = ".xlsx";
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x00120E24 File Offset: 0x0011F024
		public BinaryValue Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06005436 RID: 21558 RVA: 0x00120E2C File Offset: 0x0011F02C
		public BinaryValue ActualContent
		{
			get
			{
				return this.actualContent;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x00120E34 File Offset: 0x0011F034
		public string FileExtension
		{
			get
			{
				return this.fileExtension;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06005438 RID: 21560 RVA: 0x00120E3C File Offset: 0x0011F03C
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x00120E44 File Offset: 0x0011F044
		public string CacheKey
		{
			get
			{
				if (this.cacheKey == null)
				{
					string text;
					string sourceKeyOrDigest = DataSource.GetSourceKeyOrDigest(this.content, out text);
					string text2 = (this.decrypted ? "true" : "false");
					this.cacheKey = PersistentCacheKey.ExcelWorkbook.Qualify("Stream/2", sourceKeyOrDigest, text, text2);
				}
				return this.cacheKey;
			}
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00120EA0 File Offset: 0x0011F0A0
		public IDisposable Lock()
		{
			string text;
			Func<IDisposable> func;
			if (DataSource.TryGetSourceFilePath(this.content, false, out text, out func))
			{
				return new AceMutex(AceMutex.GetMutexName(text), this.engineHost);
			}
			return null;
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x00120ED2 File Offset: 0x0011F0D2
		public Stream Open()
		{
			return this.EnsureNotHtmlResponse(() => this.GetSeekableContent().Open());
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x00120EE8 File Offset: 0x0011F0E8
		public Stream Open(out string contentKey)
		{
			string tmp = string.Empty;
			Stream stream = this.EnsureNotHtmlResponse(() => this.GetSeekableContent().Open(out tmp));
			contentKey = tmp;
			return stream;
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00120F27 File Offset: 0x0011F127
		public void Decrypt(Func<Stream, Stream> decryptionFunction)
		{
			this.decrypted = true;
			this.actualContent = new ExcelFile.EncryptedBinaryValue(this, this.GetSeekableContent(), decryptionFunction);
			this.seekableContent = null;
			this.cacheKey = null;
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x00120F51 File Offset: 0x0011F151
		private SeekableBinaryValue GetSeekableContent()
		{
			if (this.seekableContent == null)
			{
				this.seekableContent = new SeekableBinaryValue(this.persistentCache, this.CacheKey, this.actualContent);
			}
			return this.seekableContent;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x00120F80 File Offset: 0x0011F180
		private Stream EnsureNotHtmlResponse(Func<Stream> streamCtor)
		{
			Stream stream = null;
			Stream stream3;
			try
			{
				stream = streamCtor();
				DataSource.EnsureNotHtmlResponse(this.content);
				Stream stream2 = stream;
				stream = null;
				stream3 = stream2;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			return stream3;
		}

		// Token: 0x04002E89 RID: 11913
		private readonly IEngineHost engineHost;

		// Token: 0x04002E8A RID: 11914
		private readonly IPersistentCache persistentCache;

		// Token: 0x04002E8B RID: 11915
		private readonly BinaryValue content;

		// Token: 0x04002E8C RID: 11916
		private readonly IResource resource;

		// Token: 0x04002E8D RID: 11917
		private readonly string fileExtension;

		// Token: 0x04002E8E RID: 11918
		private SeekableBinaryValue seekableContent;

		// Token: 0x04002E8F RID: 11919
		private BinaryValue actualContent;

		// Token: 0x04002E90 RID: 11920
		private string cacheKey;

		// Token: 0x04002E91 RID: 11921
		private bool decrypted;

		// Token: 0x02000C10 RID: 3088
		private class EncryptedBinaryValue : StreamedBinaryValue
		{
			// Token: 0x06005441 RID: 21569 RVA: 0x00120FD1 File Offset: 0x0011F1D1
			public EncryptedBinaryValue(ExcelFile file, SeekableBinaryValue seekableContent, Func<Stream, Stream> decryptionFunction)
			{
				this.file = file;
				this.seekableContent = seekableContent;
				this.decryptionFunction = decryptionFunction;
			}

			// Token: 0x06005442 RID: 21570 RVA: 0x00120FF0 File Offset: 0x0011F1F0
			public override Stream Open()
			{
				IDisposable fileLock = this.file.Lock();
				Stream baseStream = this.seekableContent.Open();
				Action action = delegate
				{
					if (baseStream != null)
					{
						baseStream.Dispose();
						baseStream = null;
					}
					if (fileLock != null)
					{
						fileLock.Dispose();
						fileLock = null;
					}
				};
				Stream stream2;
				try
				{
					Stream stream = this.decryptionFunction(baseStream);
					if (stream == null)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.EncryptedWorkbook_NotSupported, this.file.Content, null);
					}
					stream2 = stream.AfterDispose(action);
				}
				catch (Exception)
				{
					action();
					throw;
				}
				return stream2;
			}

			// Token: 0x04002E92 RID: 11922
			private readonly ExcelFile file;

			// Token: 0x04002E93 RID: 11923
			private readonly SeekableBinaryValue seekableContent;

			// Token: 0x04002E94 RID: 11924
			private readonly Func<Stream, Stream> decryptionFunction;
		}
	}
}
