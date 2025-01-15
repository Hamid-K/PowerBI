using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000010 RID: 16
	public class SendFileMiddleware
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002993 File Offset: 0x00000B93
		public SendFileMiddleware(Func<IDictionary<string, object>, Task> next)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			this._next = next;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029B0 File Offset: 0x00000BB0
		public Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			if (!(context.Get<object>("sendfile.SendAsync") is Func<string, long, long?, CancellationToken, Task>))
			{
				context.Set<Func<string, long, long?, CancellationToken, Task>>("sendfile.SendAsync", new Func<string, long, long?, CancellationToken, Task>(new SendFileMiddleware.SendFileWrapper(context.Response.Body).SendAsync));
			}
			return this._next(environment);
		}

		// Token: 0x04000025 RID: 37
		private readonly Func<IDictionary<string, object>, Task> _next;

		// Token: 0x0200001D RID: 29
		private class SendFileWrapper
		{
			// Token: 0x0600008A RID: 138 RVA: 0x00005591 File Offset: 0x00003791
			internal SendFileWrapper(Stream output)
			{
				this._output = output;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000055A0 File Offset: 0x000037A0
			internal Task SendAsync(string fileName, long offset, long? length, CancellationToken cancel)
			{
				cancel.ThrowIfCancellationRequested();
				if (string.IsNullOrWhiteSpace(fileName))
				{
					throw new ArgumentNullException("fileName");
				}
				if (!File.Exists(fileName))
				{
					throw new FileNotFoundException(string.Empty, fileName);
				}
				FileInfo fileInfo = new FileInfo(fileName);
				if (offset < 0L || offset > fileInfo.Length)
				{
					throw new ArgumentOutOfRangeException("offset", offset, string.Empty);
				}
				if (length != null && (length.Value < 0L || length.Value > fileInfo.Length - offset))
				{
					throw new ArgumentOutOfRangeException("length", length, string.Empty);
				}
				Stream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536, FileOptions.Asynchronous | FileOptions.SequentialScan);
				Task task;
				try
				{
					fileStream.Seek(offset, SeekOrigin.Begin);
					StreamCopyOperation copyOperation = new StreamCopyOperation(fileStream, this._output, length, cancel);
					task = copyOperation.Start().ContinueWith(delegate(Task resultTask)
					{
						fileStream.Close();
						resultTask.Wait();
					}, TaskContinuationOptions.ExecuteSynchronously);
				}
				catch (Exception)
				{
					fileStream.Close();
					throw;
				}
				return task;
			}

			// Token: 0x04000051 RID: 81
			private readonly Stream _output;
		}
	}
}
