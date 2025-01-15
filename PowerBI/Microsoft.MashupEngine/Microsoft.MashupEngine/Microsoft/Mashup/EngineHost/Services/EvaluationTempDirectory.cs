using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019D9 RID: 6617
	internal class EvaluationTempDirectory : ITempDirectoryService
	{
		// Token: 0x0600A788 RID: 42888 RVA: 0x0022AA38 File Offset: 0x00228C38
		public EvaluationTempDirectory(ITempDirectoryConfig tempDirectoryConfig, IEvaluationConstants evaluationConstants)
		{
			this.tempDirectoryPath = Path.Combine(tempDirectoryConfig.TempDirectoryPath, "ContainerData");
			this.maxSize = tempDirectoryConfig.TempDirectoryMaxSize;
			this.evaluationConstants = evaluationConstants;
			this.available = 0L;
		}

		// Token: 0x0600A789 RID: 42889 RVA: 0x0022AA87 File Offset: 0x00228C87
		public Stream CreateFile(string extension, FileAccess fileAccess, out string path)
		{
			return this.CreateFile(extension, fileAccess, FileOptions.None, out path);
		}

		// Token: 0x0600A78A RID: 42890 RVA: 0x0022AA94 File Offset: 0x00228C94
		public Stream CreateFile()
		{
			string text;
			return this.CreateFile(null, FileAccess.ReadWrite, FileOptions.DeleteOnClose, out text);
		}

		// Token: 0x0600A78B RID: 42891 RVA: 0x0022AAB0 File Offset: 0x00228CB0
		private Stream CreateFile(string extension, FileAccess fileAccess, FileOptions fileOptions, out string path)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (!this.created)
				{
					FileSystemAccessHelper.CreateDirectory(this.tempDirectoryPath, this.evaluationConstants);
					this.created = true;
				}
			}
			path = Path.Combine(this.tempDirectoryPath, Guid.NewGuid().ToString());
			if (!string.IsNullOrEmpty(extension))
			{
				path = Path.ChangeExtension(path, extension);
			}
			Stream stream2;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("EvaluationTempDirectory/CreateFile", this.evaluationConstants, TraceEventType.Information, null))
			{
				hostTrace.Add("TempFilePath", path, true);
				Stream stream = new FileStream(path, FileMode.Create, fileAccess, FileShare.None, 4096, fileOptions);
				if (this.maxSize != -1L)
				{
					stream = new EvaluationTempDirectory.LengthCheckingStream(stream, new Action<long>(this.CheckLength));
				}
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x0600A78C RID: 42892 RVA: 0x0022ABB4 File Offset: 0x00228DB4
		private void CheckLength(long count)
		{
			if (Interlocked.Add(ref this.available, -count) < 0L)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.available < 0L)
					{
						long num = DirectorySize.Compute(this.tempDirectoryPath, "*.*");
						Interlocked.Add(ref this.available, (this.maxSize - num) / 3L);
						if (this.available < 0L)
						{
							throw new HostingException(Strings.OutOfTempStorage, "OutOfTempStorage");
						}
					}
				}
			}
		}

		// Token: 0x0400573D RID: 22333
		private const long ExpectedMaxParallelSubevaluations = 3L;

		// Token: 0x0400573E RID: 22334
		private const string FileSpec = "*.*";

		// Token: 0x0400573F RID: 22335
		private readonly object syncRoot = new object();

		// Token: 0x04005740 RID: 22336
		private readonly string tempDirectoryPath;

		// Token: 0x04005741 RID: 22337
		private readonly long maxSize;

		// Token: 0x04005742 RID: 22338
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04005743 RID: 22339
		private long available;

		// Token: 0x04005744 RID: 22340
		private bool created;

		// Token: 0x020019DA RID: 6618
		private class LengthCheckingStream : Stream
		{
			// Token: 0x0600A78D RID: 42893 RVA: 0x0022AC4C File Offset: 0x00228E4C
			public LengthCheckingStream(Stream stream, Action<long> lengthChecker)
			{
				this.stream = stream;
				this.lengthChecker = lengthChecker;
			}

			// Token: 0x17002AA6 RID: 10918
			// (get) Token: 0x0600A78E RID: 42894 RVA: 0x0022AC62 File Offset: 0x00228E62
			public override bool CanRead
			{
				get
				{
					return this.stream.CanRead;
				}
			}

			// Token: 0x17002AA7 RID: 10919
			// (get) Token: 0x0600A78F RID: 42895 RVA: 0x0022AC6F File Offset: 0x00228E6F
			public override bool CanSeek
			{
				get
				{
					return this.stream.CanSeek;
				}
			}

			// Token: 0x17002AA8 RID: 10920
			// (get) Token: 0x0600A790 RID: 42896 RVA: 0x0022AC7C File Offset: 0x00228E7C
			public override bool CanWrite
			{
				get
				{
					return this.stream.CanWrite;
				}
			}

			// Token: 0x0600A791 RID: 42897 RVA: 0x0022AC89 File Offset: 0x00228E89
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x17002AA9 RID: 10921
			// (get) Token: 0x0600A792 RID: 42898 RVA: 0x0022AC96 File Offset: 0x00228E96
			public override long Length
			{
				get
				{
					return this.stream.Length;
				}
			}

			// Token: 0x17002AAA RID: 10922
			// (get) Token: 0x0600A793 RID: 42899 RVA: 0x0022ACA3 File Offset: 0x00228EA3
			// (set) Token: 0x0600A794 RID: 42900 RVA: 0x0022ACB0 File Offset: 0x00228EB0
			public override long Position
			{
				get
				{
					return this.stream.Position;
				}
				set
				{
					this.stream.Position = value;
				}
			}

			// Token: 0x0600A795 RID: 42901 RVA: 0x0022ACBE File Offset: 0x00228EBE
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.stream.Read(buffer, offset, count);
			}

			// Token: 0x0600A796 RID: 42902 RVA: 0x0022ACCE File Offset: 0x00228ECE
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.stream.Seek(offset, origin);
			}

			// Token: 0x0600A797 RID: 42903 RVA: 0x0022ACDD File Offset: 0x00228EDD
			public override void SetLength(long value)
			{
				this.stream.SetLength(value);
			}

			// Token: 0x0600A798 RID: 42904 RVA: 0x0022ACEB File Offset: 0x00228EEB
			public override void WriteByte(byte value)
			{
				this.lengthChecker(1L);
				this.stream.WriteByte(value);
			}

			// Token: 0x0600A799 RID: 42905 RVA: 0x0022AD06 File Offset: 0x00228F06
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.lengthChecker((long)count);
				this.stream.Write(buffer, offset, count);
			}

			// Token: 0x0600A79A RID: 42906 RVA: 0x0022AD23 File Offset: 0x00228F23
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.stream != null)
				{
					this.stream.Close();
					this.stream.Dispose();
					this.stream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600A79B RID: 42907 RVA: 0x0022AD54 File Offset: 0x00228F54
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x04005745 RID: 22341
			private readonly Action<long> lengthChecker;

			// Token: 0x04005746 RID: 22342
			private Stream stream;
		}
	}
}
