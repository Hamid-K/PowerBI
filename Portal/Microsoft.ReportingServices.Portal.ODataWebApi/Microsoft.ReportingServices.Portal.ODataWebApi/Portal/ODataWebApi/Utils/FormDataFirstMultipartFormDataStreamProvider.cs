using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x02000035 RID: 53
	public class FormDataFirstMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000B138 File Offset: 0x00009338
		public FormDataFirstMultipartFormDataStreamProvider(string rootPath)
			: base(rootPath)
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B141 File Offset: 0x00009341
		public FormDataFirstMultipartFormDataStreamProvider(string rootPath, int bufferSize)
			: base(rootPath, bufferSize)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002A5 RID: 677 RVA: 0x0000B14C File Offset: 0x0000934C
		// (remove) Token: 0x060002A6 RID: 678 RVA: 0x0000B184 File Offset: 0x00009384
		public event FormDataFirstMultipartFormDataStreamProvider.FormDataAvailableHandler FormDataAvailable;

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B1BC File Offset: 0x000093BC
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (headers.ContentDisposition == null)
			{
				return null;
			}
			this.SaveValueToResult(base.FormData);
			this.SaveNameToResult(headers, base.FormData);
			if (!(this._lastFormKey == "File"))
			{
				return this.GetStream();
			}
			base.FormData["Size"] = this.GetFileSize(parent, headers.ContentType).ToString();
			if (this.FormDataAvailable != null && !this.FormDataAvailable())
			{
				return null;
			}
			this._localFileName = string.Concat(new object[]
			{
				"PortalUpload_",
				Guid.NewGuid(),
				"_",
				base.FormData["Name"]
			});
			headers.ContentDisposition.FileName = headers.ContentDisposition.FileName ?? this._localFileName;
			return base.GetStream(parent, headers);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public override string GetLocalFileName(HttpContentHeaders headers)
		{
			return this._localFileName;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B2B8 File Offset: 0x000094B8
		private Stream GetStream()
		{
			this._formValueMemoryStream = new MemoryStream();
			return this._formValueMemoryStream;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B2CB File Offset: 0x000094CB
		private void SaveNameToResult(HttpContentHeaders headers, NameValueCollection formData)
		{
			this._lastFormKey = ((headers.ContentDisposition.Name != null) ? headers.ContentDisposition.Name.Replace("\"", "") : null);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B300 File Offset: 0x00009500
		private void SaveValueToResult(NameValueCollection formData)
		{
			if (this._lastFormKey != null)
			{
				string text = new StreamReader(this._formValueMemoryStream).ReadToEnd();
				text = HttpUtility.HtmlDecode(text);
				text = text.Replace("\"", "");
				formData.Set(this._lastFormKey, text);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B34C File Offset: 0x0000954C
		private long GetFileSize(HttpContent content, MediaTypeHeaderValue mediaType)
		{
			long? contentLength = content.Headers.ContentLength;
			if (contentLength != null)
			{
				NameValueHeaderValue nameValueHeaderValue = content.Headers.ContentType.Parameters.FirstOrDefault<NameValueHeaderValue>();
				if (nameValueHeaderValue != null)
				{
					string value = nameValueHeaderValue.Value;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text in base.FormData.AllKeys)
					{
						string text2 = base.FormData[text];
						stringBuilder.AppendFormat("--{0}", value);
						stringBuilder.AppendLine();
						stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"", text);
						stringBuilder.AppendLine();
						stringBuilder.AppendLine();
						stringBuilder.AppendLine(text2);
					}
					stringBuilder.AppendFormat("--{0}", value);
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"File\", filename=\"{0}\"", base.FormData["Name"]);
					stringBuilder.AppendLine();
					if (mediaType != null)
					{
						stringBuilder.AppendFormat("Content-Type: {0}", mediaType.MediaType);
						stringBuilder.AppendLine();
					}
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("--{0}--", value);
					stringBuilder.AppendLine();
					return contentLength.Value - (long)stringBuilder.Length;
				}
			}
			return 0L;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B490 File Offset: 0x00009690
		public string GetTempFilename()
		{
			return Path.Combine(base.RootPath, this.GetLocalFileName(null));
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B4A4 File Offset: 0x000096A4
		public void DeleteTemporaryFile()
		{
			try
			{
				string tempFilename = this.GetTempFilename();
				if (File.Exists(tempFilename))
				{
					File.Delete(tempFilename);
				}
				this.DeleteOldUploadedFiles();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B4E4 File Offset: 0x000096E4
		private void DeleteOldUploadedFiles()
		{
			DateTime deleteThreshold = DateTime.Now - TimeSpan.FromDays(10.0);
			foreach (FileInfo fileInfo2 in from fileInfo in new DirectoryInfo(base.RootPath).GetFiles("PortalUpload_*")
				where fileInfo.CreationTime < deleteThreshold
				select fileInfo)
			{
				File.Delete(fileInfo2.FullName);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B578 File Offset: 0x00009778
		public FileStream GetFileStream()
		{
			return new FileStream(this.GetTempFilename(), FileMode.Open);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B586 File Offset: 0x00009786
		public byte[] GetFileContent()
		{
			return File.ReadAllBytes(this.GetTempFilename());
		}

		// Token: 0x04000097 RID: 151
		private string _localFileName;

		// Token: 0x04000098 RID: 152
		private MemoryStream _formValueMemoryStream;

		// Token: 0x04000099 RID: 153
		private string _lastFormKey;

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x0600039C RID: 924
		public delegate bool FormDataAvailableHandler();
	}
}
