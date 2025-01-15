using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration.Http
{
	// Token: 0x02000038 RID: 56
	public sealed class BinaryUploader
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00007F31 File Offset: 0x00006131
		public BinaryUploader(string localDir, string localPrefix, long maxUploadSize, TimeSpan tempFileCleanupTime)
		{
			this._uploadPath = localDir;
			this._uploadFilePrefix = localPrefix;
			this._uploadFileSelector = this._uploadFilePrefix + "*";
			this._maxUploadSize = maxUploadSize;
			this.DeleteUnusedUploadsOlderThan(tempFileCleanupTime);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007F6C File Offset: 0x0000616C
		public static long ApproximateUploadSize(HttpRequestMessage request)
		{
			if (request.Content.Headers.ContentLength == null)
			{
				return 0L;
			}
			return request.Content.Headers.ContentLength.Value;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007FB0 File Offset: 0x000061B0
		public async Task<FileInfo> Upload(HttpRequestMessage request)
		{
			FileInfo uploadedFile;
			using (ScopeMeter.Use(new string[] { "BinaryUploader", "Upload" }))
			{
				if (!request.Content.IsMimeMultipartContent())
				{
					Logger.Debug("Request must be MIME multipart/form-data for file upload, but was {0}", new object[] { request.Content.GetType().ToString() });
					throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
				}
				long num = BinaryUploader.ApproximateUploadSize(request);
				if (num > this._maxUploadSize)
				{
					string text = string.Format("Upload size of {0} exceeds max allowable {1}", num, this._maxUploadSize);
					Logger.Debug(text, Array.Empty<object>());
					throw new Exception(text);
				}
				uploadedFile = (await request.Content.ReadAsMultipartAsync(new LocalFileStreamProvider(this._uploadPath, this._uploadFilePrefix))).GetUploadedFile();
			}
			return uploadedFile;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008000 File Offset: 0x00006200
		public void DeleteUnusedUploadsOlderThan(TimeSpan deleteOlderThan)
		{
			using (ScopeMeter.Use(new string[] { "BinaryUploader", "Cleanup" }))
			{
				try
				{
					DateTime deleteThreshold = DateTime.Now - deleteOlderThan;
					Parallel.ForEach<FileInfo>(from fileInfo in new DirectoryInfo(this._uploadPath).GetFiles(this._uploadFileSelector)
						where fileInfo.CreationTime < deleteThreshold
						select fileInfo, delegate(FileInfo fileToDelete)
					{
						File.Delete(fileToDelete.FullName);
					});
				}
				catch (Exception ex)
				{
					Logger.Debug(ex, "Binary Uploader Cleanup: attempting to continue after unexpected exception", Array.Empty<object>());
				}
			}
		}

		// Token: 0x04000198 RID: 408
		private readonly string _uploadFilePrefix;

		// Token: 0x04000199 RID: 409
		private readonly string _uploadFileSelector;

		// Token: 0x0400019A RID: 410
		private readonly long _maxUploadSize;

		// Token: 0x0400019B RID: 411
		private readonly string _uploadPath;
	}
}
