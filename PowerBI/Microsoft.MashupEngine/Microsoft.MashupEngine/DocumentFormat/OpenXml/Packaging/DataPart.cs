using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020FC RID: 8444
	internal class DataPart
	{
		// Token: 0x0600D00A RID: 53258 RVA: 0x000020FD File Offset: 0x000002FD
		internal DataPart()
		{
		}

		// Token: 0x17003242 RID: 12866
		// (get) Token: 0x0600D00B RID: 53259 RVA: 0x002957FC File Offset: 0x002939FC
		public OpenXmlPackage OpenXmlPackage
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._openXmlPackage;
			}
		}

		// Token: 0x17003243 RID: 12867
		// (get) Token: 0x0600D00C RID: 53260 RVA: 0x0029580A File Offset: 0x00293A0A
		public Uri Uri
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._uri;
			}
		}

		// Token: 0x0600D00D RID: 53261 RVA: 0x00295818 File Offset: 0x00293A18
		public IEnumerable<DataPartReferenceRelationship> GetDataPartReferenceRelationships()
		{
			this.ThrowIfObjectDisposed();
			foreach (DataPartReferenceRelationship dataPartReferenceRelationship in this.OpenXmlPackage.DataPartReferenceRelationships)
			{
				if (dataPartReferenceRelationship.DataPart == this)
				{
					yield return dataPartReferenceRelationship;
				}
			}
			OpenXmlPackagePartIterator partIterator = new OpenXmlPackagePartIterator(this.OpenXmlPackage);
			foreach (OpenXmlPart openXmlPart in partIterator)
			{
				foreach (DataPartReferenceRelationship dataPartReferenceRelationship2 in openXmlPart.DataPartReferenceRelationships)
				{
					if (dataPartReferenceRelationship2.DataPart == this)
					{
						yield return dataPartReferenceRelationship2;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600D00E RID: 53262 RVA: 0x00295835 File Offset: 0x00293A35
		public Stream GetStream()
		{
			this.ThrowIfObjectDisposed();
			return this.PackagePart.GetStream();
		}

		// Token: 0x0600D00F RID: 53263 RVA: 0x00295848 File Offset: 0x00293A48
		public Stream GetStream(FileMode mode)
		{
			this.ThrowIfObjectDisposed();
			return this.PackagePart.GetStream(mode);
		}

		// Token: 0x0600D010 RID: 53264 RVA: 0x0029585C File Offset: 0x00293A5C
		public Stream GetStream(FileMode mode, FileAccess access)
		{
			this.ThrowIfObjectDisposed();
			return this.PackagePart.GetStream(mode, access);
		}

		// Token: 0x0600D011 RID: 53265 RVA: 0x00295874 File Offset: 0x00293A74
		public void FeedData(Stream sourceStream)
		{
			this.ThrowIfObjectDisposed();
			if (sourceStream == null)
			{
				throw new ArgumentNullException("sourceStream");
			}
			using (Stream stream = this.GetStream(FileMode.Create))
			{
				OpenXmlPart.CopyStream(sourceStream, stream);
			}
		}

		// Token: 0x17003244 RID: 12868
		// (get) Token: 0x0600D012 RID: 53266 RVA: 0x002958C0 File Offset: 0x00293AC0
		public virtual string ContentType
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.PackagePart.ContentType;
			}
		}

		// Token: 0x17003245 RID: 12869
		// (get) Token: 0x0600D013 RID: 53267 RVA: 0x002958D3 File Offset: 0x00293AD3
		internal PackagePart PackagePart
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._metroPart;
			}
		}

		// Token: 0x17003246 RID: 12870
		// (get) Token: 0x0600D014 RID: 53268 RVA: 0x002958E1 File Offset: 0x00293AE1
		internal virtual string TargetPath
		{
			get
			{
				return "data";
			}
		}

		// Token: 0x17003247 RID: 12871
		// (get) Token: 0x0600D015 RID: 53269 RVA: 0x002958E1 File Offset: 0x00293AE1
		internal virtual string TargetName
		{
			get
			{
				return "data";
			}
		}

		// Token: 0x17003248 RID: 12872
		// (get) Token: 0x0600D016 RID: 53270 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal virtual string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x0600D017 RID: 53271 RVA: 0x002958EF File Offset: 0x00293AEF
		internal void Load(OpenXmlPackage openXmlPackage, PackagePart packagePart)
		{
			this._openXmlPackage = openXmlPackage;
			this._metroPart = packagePart;
			this._uri = packagePart.Uri;
			if (this._metroPart.GetRelationships().Any<PackageRelationship>())
			{
				throw new OpenXmlPackageException(ExceptionMessages.MediaDataPartShouldNotReferenceOtherParts);
			}
		}

		// Token: 0x0600D018 RID: 53272 RVA: 0x00295928 File Offset: 0x00293B28
		internal void CreateInternal(OpenXmlPackage openXmlPackage, string contentType, string extension)
		{
			this._openXmlPackage = openXmlPackage;
			this._uri = this.NewPartUri(openXmlPackage, contentType, extension);
			this._metroPart = this._openXmlPackage.CreateMetroPart(this._uri, contentType);
		}

		// Token: 0x0600D019 RID: 53273 RVA: 0x00295958 File Offset: 0x00293B58
		internal void CreateInternal(OpenXmlPackage openXmlPackage, MediaDataPartType mediaDataPartType)
		{
			this._openXmlPackage = openXmlPackage;
			string contentType = mediaDataPartType.GetContentType();
			string targetExtension = mediaDataPartType.GetTargetExtension();
			this.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			this._uri = this.NewPartUri(openXmlPackage, contentType, null);
			this._metroPart = this._openXmlPackage.CreateMetroPart(this._uri, contentType);
		}

		// Token: 0x0600D01A RID: 53274 RVA: 0x002959B4 File Offset: 0x00293BB4
		internal void CreateInternal2(OpenXmlPackage openXmlPackage, string contentType, Uri partUri)
		{
			if (openXmlPackage == null)
			{
				throw new ArgumentNullException(ExceptionMessages.PackageRelatedArgumentNullException);
			}
			if (this._metroPart != null)
			{
				throw new InvalidOperationException();
			}
			this._openXmlPackage = openXmlPackage;
			Uri uri = new Uri("/", UriKind.Relative);
			this._uri = this._openXmlPackage.GetUniquePartUri(contentType, uri, partUri);
			this._metroPart = this._openXmlPackage.CreateMetroPart(this._uri, contentType);
		}

		// Token: 0x0600D01B RID: 53275 RVA: 0x00295A1C File Offset: 0x00293C1C
		internal Uri NewPartUri(OpenXmlPackage openXmlPackage, string contentType, string extension)
		{
			string text;
			if (extension == null)
			{
				if (!this._openXmlPackage.PartExtensionProvider.TryGetValue(contentType, out text))
				{
					text = this.TargetFileExtension;
				}
			}
			else
			{
				text = extension;
			}
			return openXmlPackage.GetUniquePartUri(contentType, new Uri("/", UriKind.Relative), this.TargetPath, this.TargetName, text);
		}

		// Token: 0x0600D01C RID: 53276 RVA: 0x00295A6B File Offset: 0x00293C6B
		internal void Destroy()
		{
			this.OpenXmlPackage.Package.DeletePart(this.Uri);
			this._openXmlPackage = null;
		}

		// Token: 0x0600D01D RID: 53277 RVA: 0x00295A8A File Offset: 0x00293C8A
		protected void ThrowIfObjectDisposed()
		{
			if (this._openXmlPackage == null)
			{
				throw new InvalidOperationException(ExceptionMessages.PartIsDestroyed);
			}
		}

		// Token: 0x040068BC RID: 26812
		private const string DefaultTargetPart = "data";

		// Token: 0x040068BD RID: 26813
		private const string DefaultTargetName = "data";

		// Token: 0x040068BE RID: 26814
		private const string DefaultTargetExt = ".bin";

		// Token: 0x040068BF RID: 26815
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private OpenXmlPackage _openXmlPackage;

		// Token: 0x040068C0 RID: 26816
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PackagePart _metroPart;

		// Token: 0x040068C1 RID: 26817
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Uri _uri;
	}
}
