using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002097 RID: 8343
	public class ZipPackage : IDisposable
	{
		// Token: 0x0600CC2E RID: 52270 RVA: 0x00289E86 File Offset: 0x00288086
		private ZipPackage(Stream stream, FileMode fileMode, FileAccess fileAccess)
		{
			this.streamStartPosition = stream.Position;
			this.zipPackage = Package.Open(stream, fileMode, fileAccess);
			this.stream = stream;
			this.fileMode = fileMode;
		}

		// Token: 0x0600CC2F RID: 52271 RVA: 0x00289EB6 File Offset: 0x002880B6
		public static ZipPackage Open(Stream stream, FileMode fileMode)
		{
			return new ZipPackage(stream, fileMode, FileAccess.ReadWrite);
		}

		// Token: 0x0600CC30 RID: 52272 RVA: 0x00289EC0 File Offset: 0x002880C0
		public static ZipPackage Open(Stream stream, FileMode fileMode, FileAccess fileAccess)
		{
			return new ZipPackage(stream, fileMode, fileAccess);
		}

		// Token: 0x0600CC31 RID: 52273 RVA: 0x00289ECA File Offset: 0x002880CA
		public void Dispose()
		{
			((IDisposable)this.zipPackage).Dispose();
		}

		// Token: 0x0600CC32 RID: 52274 RVA: 0x00289ED7 File Offset: 0x002880D7
		public void Close()
		{
			this.zipPackage.Close();
		}

		// Token: 0x0600CC33 RID: 52275 RVA: 0x00289EE4 File Offset: 0x002880E4
		public void Flush()
		{
			((IDisposable)this.zipPackage).Dispose();
			this.stream.Seek(this.streamStartPosition, SeekOrigin.Begin);
			if (this.fileMode == FileMode.Create || this.fileMode == FileMode.CreateNew || this.fileMode == FileMode.Truncate)
			{
				this.fileMode = FileMode.Open;
			}
			this.zipPackage = Package.Open(this.stream, this.fileMode);
		}

		// Token: 0x0600CC34 RID: 52276 RVA: 0x00289F48 File Offset: 0x00288148
		public bool PartExists(Uri uri)
		{
			return this.zipPackage.PartExists(uri);
		}

		// Token: 0x0600CC35 RID: 52277 RVA: 0x00289F56 File Offset: 0x00288156
		public ZipPackagePart GetPart(Uri uri)
		{
			return new ZipPackagePart(this.zipPackage.GetPart(uri));
		}

		// Token: 0x0600CC36 RID: 52278 RVA: 0x00289F69 File Offset: 0x00288169
		public ZipPackagePart CreatePart(Uri uri, string contentType, bool enableCompression)
		{
			return new ZipPackagePart(this.zipPackage.CreatePart(uri, contentType, enableCompression ? CompressionOption.Maximum : CompressionOption.NotCompressed));
		}

		// Token: 0x0600CC37 RID: 52279 RVA: 0x00289F84 File Offset: 0x00288184
		public void DeletePart(Uri uri)
		{
			this.zipPackage.DeletePart(uri);
		}

		// Token: 0x0600CC38 RID: 52280 RVA: 0x00289F92 File Offset: 0x00288192
		public IEnumerable<ZipPackagePart> GetParts()
		{
			return from p in this.zipPackage.GetParts()
				select new ZipPackagePart(p);
		}

		// Token: 0x0400677F RID: 26495
		private const FileAccess defaultFileAccess = FileAccess.ReadWrite;

		// Token: 0x04006780 RID: 26496
		private Package zipPackage;

		// Token: 0x04006781 RID: 26497
		private Stream stream;

		// Token: 0x04006782 RID: 26498
		private FileMode fileMode;

		// Token: 0x04006783 RID: 26499
		private long streamStartPosition;
	}
}
