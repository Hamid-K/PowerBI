using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200209A RID: 8346
	public class ZipPackagePartStorage : PackagePartStorage
	{
		// Token: 0x0600CC40 RID: 52288 RVA: 0x0028A00F File Offset: 0x0028820F
		public ZipPackagePartStorage(Stream stream, bool allowEdits, bool enableCompression, ContentStorage contentStorage)
			: this(stream, allowEdits, enableCompression, contentStorage, new object())
		{
		}

		// Token: 0x0600CC41 RID: 52289 RVA: 0x0028A024 File Offset: 0x00288224
		public ZipPackagePartStorage(Stream stream, bool allowEdits, bool enableCompression, ContentStorage contentStorage, object objectLock)
		{
			this.allowEdits = allowEdits;
			this.contentStorage = contentStorage;
			this.objectLock = objectLock;
			this.enableCompression = enableCompression;
			try
			{
				this.package = ZipPackage.Open(stream, FileMode.OpenOrCreate);
				this.package.Flush();
				this.stream = stream;
			}
			catch (IOException ex)
			{
				throw StorageExceptions.StorageException(Strings.Package_Unable_To_Access, ex);
			}
			catch (FileFormatException ex2)
			{
				throw StorageExceptions.StorageException(Strings.Package_Unrecognized_File_Format, ex2);
			}
			catch (NotSupportedException ex3)
			{
				throw StorageExceptions.StorageException(Strings.Package_Unrecognized_File_Format, ex3);
			}
		}

		// Token: 0x17003123 RID: 12579
		// (get) Token: 0x0600CC42 RID: 52290 RVA: 0x0028A0C8 File Offset: 0x002882C8
		public object ObjectLock
		{
			get
			{
				return this.objectLock;
			}
		}

		// Token: 0x17003124 RID: 12580
		// (get) Token: 0x0600CC43 RID: 52291 RVA: 0x0028A0D0 File Offset: 0x002882D0
		// (set) Token: 0x0600CC44 RID: 52292 RVA: 0x0028A114 File Offset: 0x00288314
		public bool IsModified
		{
			get
			{
				object obj = this.objectLock;
				bool flag2;
				lock (obj)
				{
					flag2 = this.isModified;
				}
				return flag2;
			}
			set
			{
				object obj = this.objectLock;
				lock (obj)
				{
					this.isModified = value;
				}
			}
		}

		// Token: 0x17003125 RID: 12581
		// (get) Token: 0x0600CC45 RID: 52293 RVA: 0x0028A158 File Offset: 0x00288358
		protected ZipPackage Package
		{
			get
			{
				return this.package;
			}
		}

		// Token: 0x0600CC46 RID: 52294 RVA: 0x0028A160 File Offset: 0x00288360
		public override bool TryAddPart(PackagePartType partType, string name, string contentType, byte[] content)
		{
			object obj = this.objectLock;
			bool flag2;
			lock (obj)
			{
				try
				{
					Uri partUri = this.GetPartUri(partType, name);
					if (!this.package.PartExists(partUri))
					{
						ZipPackagePart zipPackagePart = this.package.CreatePart(partUri, contentType, this.enableCompression);
						this.SetPartContent(partType, zipPackagePart, content);
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToAddPart, ex);
				}
			}
			return flag2;
		}

		// Token: 0x0600CC47 RID: 52295 RVA: 0x0028A1F4 File Offset: 0x002883F4
		private Uri GetPartUri(PackagePartType partType, string name)
		{
			string text;
			switch (partType)
			{
			case PackagePartType.Formulas:
				text = "/Formulas/";
				break;
			case PackagePartType.Content:
				text = "/Content/";
				break;
			case PackagePartType.Config:
				text = "/Config/";
				break;
			default:
				throw new InvalidOperationException();
			}
			string text2 = UriMethods.EscapeDataString(name, true);
			return new Uri(text + text2, UriKind.Relative);
		}

		// Token: 0x0600CC48 RID: 52296 RVA: 0x0028A248 File Offset: 0x00288448
		public override bool TryRemovePart(PackagePartType partType, string name)
		{
			object obj = this.objectLock;
			bool flag2;
			lock (obj)
			{
				try
				{
					Uri partUri = this.GetPartUri(partType, name);
					if (this.package.PartExists(partUri))
					{
						this.package.DeletePart(partUri);
						this.package.Flush();
						this.OnEdit();
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToRemovePart, ex);
				}
			}
			return flag2;
		}

		// Token: 0x0600CC49 RID: 52297 RVA: 0x0028A2D8 File Offset: 0x002884D8
		private bool TryGetPart(PackagePartType partType, string name, out ZipPackagePart part)
		{
			bool flag;
			try
			{
				Uri partUri = this.GetPartUri(partType, name);
				if (this.package.PartExists(partUri))
				{
					part = this.package.GetPart(partUri);
					flag = true;
				}
				else
				{
					part = null;
					flag = false;
				}
			}
			catch (IOException ex)
			{
				throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToAccessPart, ex);
			}
			return flag;
		}

		// Token: 0x0600CC4A RID: 52298 RVA: 0x0028A334 File Offset: 0x00288534
		public override bool TryGetPartContent(PackagePartType partType, string name, out string contentType, out byte[] content)
		{
			object obj = this.objectLock;
			bool flag2;
			lock (obj)
			{
				try
				{
					ZipPackagePart zipPackagePart;
					if (this.TryGetPart(partType, name, out zipPackagePart))
					{
						if (this.IsPartStoredSeparately(partType))
						{
							Guid guid = new Guid(this.GetPartContentCore(zipPackagePart));
							content = this.contentStorage.GetContent(guid);
						}
						else
						{
							content = this.GetPartContentCore(zipPackagePart);
						}
						contentType = zipPackagePart.ContentType;
						flag2 = true;
					}
					else
					{
						content = null;
						contentType = null;
						flag2 = false;
					}
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToAccessPartContents, ex);
				}
			}
			return flag2;
		}

		// Token: 0x0600CC4B RID: 52299 RVA: 0x0028A3E0 File Offset: 0x002885E0
		private byte[] GetPartContentCore(ZipPackagePart part)
		{
			byte[] array;
			using (Stream stream = part.GetStream(FileMode.Open, FileAccess.Read))
			{
				int num = (int)stream.Length;
				if ((long)num > 10485760L)
				{
					throw StorageExceptions.StorageException(Strings.MashupTooLarge(10L), null);
				}
				array = new byte[num];
				stream.Read(array, 0, num);
			}
			return array;
		}

		// Token: 0x0600CC4C RID: 52300 RVA: 0x0028A44C File Offset: 0x0028864C
		public override bool TrySetPartContent(PackagePartType partType, string name, byte[] content)
		{
			object obj = this.objectLock;
			bool flag2;
			lock (obj)
			{
				try
				{
					ZipPackagePart zipPackagePart;
					if (this.TryGetPart(partType, name, out zipPackagePart))
					{
						this.SetPartContent(partType, zipPackagePart, content);
						this.OnEdit();
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToSavePartContents, ex);
				}
			}
			return flag2;
		}

		// Token: 0x0600CC4D RID: 52301 RVA: 0x0028A4C4 File Offset: 0x002886C4
		private bool IsPartStoredSeparately(PackagePartType partType)
		{
			return this.contentStorage != null && partType == PackagePartType.Content;
		}

		// Token: 0x0600CC4E RID: 52302 RVA: 0x0028A4D4 File Offset: 0x002886D4
		private void SetPartContent(PackagePartType partType, ZipPackagePart part, byte[] content)
		{
			if (this.IsPartStoredSeparately(partType))
			{
				Guid guid = Guid.NewGuid();
				this.contentStorage.SetContent(guid, content);
				this.SetPartContentCore(part, guid.ToByteArray());
				return;
			}
			this.SetPartContentCore(part, content);
		}

		// Token: 0x0600CC4F RID: 52303 RVA: 0x0028A514 File Offset: 0x00288714
		private void SetPartContentCore(ZipPackagePart part, byte[] content)
		{
			try
			{
				if ((long)content.Length > 10485760L)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_EmbeddedPartTooLarge(10L), null);
				}
				using (Stream stream = part.GetStream(FileMode.Create, FileAccess.Write))
				{
					stream.Write(content, 0, content.Length);
				}
				this.package.Flush();
				this.OnEdit();
			}
			catch (IOException ex)
			{
				throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToSavePartContents, ex);
			}
		}

		// Token: 0x0600CC50 RID: 52304 RVA: 0x0028A5A0 File Offset: 0x002887A0
		public override bool TryGetPartContentType(PackagePartType partType, string name, out string contentType)
		{
			object obj = this.objectLock;
			bool flag2;
			lock (obj)
			{
				try
				{
					ZipPackagePart zipPackagePart;
					if (this.TryGetPart(partType, name, out zipPackagePart))
					{
						contentType = zipPackagePart.ContentType;
						flag2 = true;
					}
					else
					{
						contentType = null;
						flag2 = false;
					}
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToAccessPartContentType, ex);
				}
			}
			return flag2;
		}

		// Token: 0x0600CC51 RID: 52305 RVA: 0x0028A614 File Offset: 0x00288814
		public override string[] GetPartNames(PackagePartType partType)
		{
			object obj = this.objectLock;
			string[] array;
			lock (obj)
			{
				try
				{
					string text = this.GetPartUri(partType, "").ToString();
					List<string> list = new List<string>();
					foreach (ZipPackagePart zipPackagePart in this.package.GetParts())
					{
						string text2 = Uri.UnescapeDataString(zipPackagePart.Uri.ToString());
						if (text2.StartsWith(text, StringComparison.Ordinal))
						{
							list.Add(text2.Substring(text.Length));
						}
					}
					array = list.ToArray();
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToAccessPartNames, ex);
				}
			}
			return array;
		}

		// Token: 0x0600CC52 RID: 52306 RVA: 0x0028A6F8 File Offset: 0x002888F8
		public override bool HasPart(PackagePartType partType, string name)
		{
			object obj = this.objectLock;
			bool flag2;
			lock (obj)
			{
				try
				{
					Uri partUri = this.GetPartUri(partType, name);
					flag2 = this.package.PartExists(partUri);
				}
				catch (IOException ex)
				{
					throw StorageExceptions.StorageException(Strings.PackagePartStorage_UnableToAccessPart, ex);
				}
			}
			return flag2;
		}

		// Token: 0x0600CC53 RID: 52307 RVA: 0x0028A764 File Offset: 0x00288964
		public override void Dispose()
		{
			object obj = this.objectLock;
			lock (obj)
			{
				if (this.package != null)
				{
					this.package.Close();
					this.package = null;
					this.stream.Dispose();
					this.stream = null;
				}
			}
		}

		// Token: 0x0600CC54 RID: 52308 RVA: 0x0028A7CC File Offset: 0x002889CC
		protected void OnEdit()
		{
			if (!this.allowEdits)
			{
				throw new InvalidOperationException();
			}
			this.isModified = true;
		}

		// Token: 0x04006787 RID: 26503
		public const long MaxPartSizeInMB = 10L;

		// Token: 0x04006788 RID: 26504
		public const long MaxPartSize = 10485760L;

		// Token: 0x04006789 RID: 26505
		private ZipPackage package;

		// Token: 0x0400678A RID: 26506
		private Stream stream;

		// Token: 0x0400678B RID: 26507
		private bool enableCompression;

		// Token: 0x0400678C RID: 26508
		private bool isModified;

		// Token: 0x0400678D RID: 26509
		private bool allowEdits;

		// Token: 0x0400678E RID: 26510
		private ContentStorage contentStorage;

		// Token: 0x0400678F RID: 26511
		private object objectLock;
	}
}
