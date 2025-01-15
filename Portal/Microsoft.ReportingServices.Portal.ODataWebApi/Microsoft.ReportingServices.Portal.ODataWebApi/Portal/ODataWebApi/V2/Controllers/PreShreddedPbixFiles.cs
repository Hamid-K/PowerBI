using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.Configuration.Http;
using Microsoft.BIServer.HostingEnvironment;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000015 RID: 21
	public sealed class PreShreddedPbixFiles : IDisposable
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000051F7 File Offset: 0x000033F7
		[JsonIgnore]
		public string Original
		{
			get
			{
				return this._original.FullPath;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005204 File Offset: 0x00003404
		public string Pbix
		{
			get
			{
				return this._pbix.FullPath;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005211 File Offset: 0x00003411
		public string Model
		{
			get
			{
				if (this._model != null)
				{
					return this._model.FullPath;
				}
				return null;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005228 File Offset: 0x00003428
		public static async Task<PreShreddedPbixFiles> UploadAndShred(BinaryUploader binaryUploader, HttpRequestMessage request)
		{
			PreShreddedPbixFiles preShreddedPbixFiles;
			using (ScopeMeter.Use(new string[] { "PreShredder", "Total" }))
			{
				FileInfo fileInfo = await binaryUploader.Upload(request);
				string text = fileInfo.FullName + "_Original";
				using (ScopeMeter.Use(new string[] { "PreShredder", "CopyOrignal" }))
				{
					fileInfo.CopyTo(text);
				}
				using (ScopeMeter.Use(new string[] { "PreShredder", "ZipSurgery" }))
				{
					using (ZipArchive zipArchive = ZipFile.Open(fileInfo.FullName, ZipArchiveMode.Update))
					{
						ZipArchiveEntry entry = zipArchive.GetEntry("DataModel");
						if (entry == null)
						{
							preShreddedPbixFiles = new PreShreddedPbixFiles(fileInfo.FullName);
						}
						else
						{
							string text2 = fileInfo.FullName + "_Model";
							entry.ExtractToFile(text2);
							File.SetLastWriteTime(text2, DateTime.Now);
							entry.Delete();
							preShreddedPbixFiles = new PreShreddedPbixFiles(text, fileInfo.FullName, text2);
						}
					}
				}
			}
			return preShreddedPbixFiles;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005278 File Offset: 0x00003478
		public PreShreddedPbixFiles(string original, string pbix, string model)
		{
			FileInfo fileInfo = new FileInfo(pbix);
			if (pbix == null || !fileInfo.Exists || fileInfo.Length == 0L)
			{
				throw new ArgumentException("Can't work with missing or empty upload file {0}", fileInfo.FullName);
			}
			FileInfo fileInfo2 = new FileInfo(original);
			if (fileInfo2 == null || !fileInfo2.Exists || fileInfo2.Length == 0L)
			{
				throw new ArgumentException("Can't work with missing or empty upload file {0}", fileInfo.FullName);
			}
			this._original = new ScopedFileDelete(fileInfo2.FullName);
			this._pbix = new ScopedFileDelete(fileInfo.FullName);
			this._model = (string.IsNullOrWhiteSpace(model) ? null : new ScopedFileDelete(model));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000531C File Offset: 0x0000351C
		public PreShreddedPbixFiles(string pbix)
		{
			FileInfo fileInfo = new FileInfo(pbix);
			if (pbix == null || !fileInfo.Exists || fileInfo.Length == 0L)
			{
				throw new ArgumentException("Can't work with missing or empty upload file {0}", fileInfo.FullName);
			}
			this._original = new ScopedFileDelete(fileInfo.FullName);
			this._pbix = this._original;
			this._model = null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000537E File Offset: 0x0000357E
		public static FileStream CreateReadStreamFromPathIfExists(string filePath)
		{
			if (filePath == null || !File.Exists(filePath))
			{
				return null;
			}
			return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005396 File Offset: 0x00003596
		public void Dispose()
		{
			this._original.Dispose();
			this._pbix.Dispose();
			if (this._model != null)
			{
				this._model.Dispose();
			}
		}

		// Token: 0x04000057 RID: 87
		private const string DataModelFileName = "DataModel";

		// Token: 0x04000058 RID: 88
		private const string ModelSuffix = "_Model";

		// Token: 0x04000059 RID: 89
		private const string OriginalSuffix = "_Original";

		// Token: 0x0400005A RID: 90
		[JsonIgnore]
		private readonly ScopedFileDelete _original;

		// Token: 0x0400005B RID: 91
		private readonly ScopedFileDelete _pbix;

		// Token: 0x0400005C RID: 92
		private readonly ScopedFileDelete _model;
	}
}
