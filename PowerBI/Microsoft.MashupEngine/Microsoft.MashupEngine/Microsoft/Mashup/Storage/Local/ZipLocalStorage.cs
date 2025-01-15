using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Storage.Local
{
	// Token: 0x020020AC RID: 8364
	public class ZipLocalStorage : LocalStorage
	{
		// Token: 0x0600CCC6 RID: 52422 RVA: 0x0028B1B4 File Offset: 0x002893B4
		public ZipLocalStorage(Stream stream, bool isReadonly = false)
		{
			this.objectLock = new object();
			this.stream = stream;
			FileMode fileMode = (isReadonly ? FileMode.Open : FileMode.OpenOrCreate);
			FileAccess fileAccess = (isReadonly ? FileAccess.Read : FileAccess.ReadWrite);
			if (stream.Length == 0L && isReadonly)
			{
				this.package = ZipPackage.Open(new MemoryStream(), FileMode.OpenOrCreate, FileAccess.ReadWrite);
			}
			else
			{
				this.package = ZipPackage.Open(this.stream, fileMode, fileAccess);
			}
			this.isReadonly = isReadonly;
			if (!isReadonly)
			{
				this.package.Flush();
			}
		}

		// Token: 0x1700314A RID: 12618
		// (get) Token: 0x0600CCC7 RID: 52423 RVA: 0x0028B233 File Offset: 0x00289433
		public override object ObjectLock
		{
			get
			{
				return this.objectLock;
			}
		}

		// Token: 0x1700314B RID: 12619
		// (get) Token: 0x0600CCC8 RID: 52424 RVA: 0x000033E7 File Offset: 0x000015E7
		public override LocalStorageCache Cache
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600CCC9 RID: 52425 RVA: 0x0028B23C File Offset: 0x0028943C
		public override void SetPart(string path, byte[] content, string contentType, bool cache)
		{
			if (this.isReadonly)
			{
				throw new InvalidOperationException();
			}
			try
			{
				Uri partUri = this.GetPartUri(path);
				ZipPackagePart zipPackagePart;
				if (this.package.PartExists(partUri))
				{
					zipPackagePart = this.package.GetPart(partUri);
					if (string.CompareOrdinal(zipPackagePart.ContentType, contentType) != 0)
					{
						this.package.DeletePart(partUri);
						zipPackagePart = null;
					}
				}
				else
				{
					zipPackagePart = null;
				}
				if (zipPackagePart == null)
				{
					zipPackagePart = this.package.CreatePart(partUri, contentType, false);
				}
				using (Stream stream = zipPackagePart.GetStream(FileMode.Create, FileAccess.Write))
				{
					stream.Write(content, 0, content.Length);
				}
				this.package.Flush();
			}
			catch (IOException ex)
			{
				throw StorageExceptions.StorageException(Strings.Local_Storage_Unable_To_Save_File, ex);
			}
		}

		// Token: 0x0600CCCA RID: 52426 RVA: 0x0028B304 File Offset: 0x00289504
		public override void ClearPart(string path)
		{
			if (this.isReadonly)
			{
				throw new InvalidOperationException();
			}
			try
			{
				Uri partUri = this.GetPartUri(path);
				if (this.package.PartExists(partUri))
				{
					this.package.DeletePart(partUri);
					this.package.Flush();
				}
			}
			catch (IOException ex)
			{
				throw StorageExceptions.StorageException(Strings.Local_Storage_Unable_To_Remove_File, ex);
			}
		}

		// Token: 0x0600CCCB RID: 52427 RVA: 0x0028B36C File Offset: 0x0028956C
		public override bool TryGetPart(string path, out byte[] content)
		{
			bool flag;
			try
			{
				Uri partUri = this.GetPartUri(path);
				if (this.package.PartExists(partUri))
				{
					using (Stream stream = this.package.GetPart(partUri).GetStream(FileMode.Open, FileAccess.Read))
					{
						int num = (int)stream.Length;
						content = new byte[num];
						stream.Read(content, 0, num);
					}
					flag = true;
				}
				else
				{
					content = null;
					flag = false;
				}
			}
			catch (IOException ex)
			{
				throw StorageExceptions.StorageException(Strings.Local_Storage_Unable_To_File_Storage, ex);
			}
			return flag;
		}

		// Token: 0x0600CCCC RID: 52428 RVA: 0x0028B404 File Offset: 0x00289604
		public override string[] GetPartPaths(string path)
		{
			List<string> list = new List<string>();
			foreach (ZipPackagePart zipPackagePart in this.package.GetParts())
			{
				string text = zipPackagePart.Uri.ToString();
				if (text.StartsWith(path, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600CCCD RID: 52429 RVA: 0x0028B478 File Offset: 0x00289678
		private Uri GetPartUri(string path)
		{
			return new Uri(path, UriKind.Relative);
		}

		// Token: 0x0600CCCE RID: 52430 RVA: 0x0028B481 File Offset: 0x00289681
		public override void Dispose()
		{
			if (this.package != null)
			{
				this.package.Close();
				this.package = null;
				this.stream.Dispose();
				this.stream = null;
			}
		}

		// Token: 0x040067AA RID: 26538
		private const bool enableCompression = false;

		// Token: 0x040067AB RID: 26539
		private ZipPackage package;

		// Token: 0x040067AC RID: 26540
		private Stream stream;

		// Token: 0x040067AD RID: 26541
		private object objectLock;

		// Token: 0x040067AE RID: 26542
		private bool isReadonly;
	}
}
