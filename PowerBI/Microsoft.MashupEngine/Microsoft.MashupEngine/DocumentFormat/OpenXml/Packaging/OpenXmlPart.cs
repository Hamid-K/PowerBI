using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Xml;
using System.Xml.Schema;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002108 RID: 8456
	internal abstract class OpenXmlPart : OpenXmlPartContainer
	{
		// Token: 0x0600D0BA RID: 53434 RVA: 0x00299206 File Offset: 0x00297406
		protected internal OpenXmlPart()
		{
		}

		// Token: 0x0600D0BB RID: 53435 RVA: 0x00299210 File Offset: 0x00297410
		internal void Load(OpenXmlPackage openXmlPackage, OpenXmlPart parent, Uri uriTarget, string id, Dictionary<Uri, OpenXmlPart> loadedParts)
		{
			if (uriTarget == null)
			{
				throw new ArgumentNullException("uriTarget");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (openXmlPackage == null && parent == null)
			{
				throw new ArgumentNullException(ExceptionMessages.PackageRelatedArgumentNullException);
			}
			if (parent != null && openXmlPackage != null && parent.OpenXmlPackage != openXmlPackage)
			{
				throw new ArgumentOutOfRangeException("parent");
			}
			if (parent != null && openXmlPackage == null)
			{
				openXmlPackage = parent.OpenXmlPackage;
			}
			this._openXmlPackage = openXmlPackage;
			this._uri = uriTarget;
			PackagePart part = this.OpenXmlPackage.Package.GetPart(uriTarget);
			if (this.IsContentTypeFixed && part.ContentType != this.ContentType)
			{
				string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.InvalidPartContentType, new object[]
				{
					part.Uri.OriginalString,
					part.ContentType,
					this.ContentType
				});
				OpenXmlPackageException ex = new OpenXmlPackageException(text);
				throw ex;
			}
			this._metroPart = part;
			this.OpenXmlPackage.ReserveUri(this.ContentType, this.Uri);
			PackageRelationshipCollection relationships = this.PackagePart.GetRelationships();
			base.LoadReferencedPartsAndRelationships(openXmlPackage, this, relationships, loadedParts);
		}

		// Token: 0x0600D0BC RID: 53436 RVA: 0x00299330 File Offset: 0x00297530
		internal sealed override OpenXmlPart NewPart(string relationshipType, string contentType)
		{
			this.ThrowIfObjectDisposed();
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			PartConstraintRule partConstraintRule;
			if (!this.GetPartConstraint().TryGetValue(relationshipType, out partConstraintRule))
			{
				throw new ArgumentOutOfRangeException("relationshipType");
			}
			if (!partConstraintRule.MaxOccursGreatThanOne && base.GetSubPart(relationshipType) != null)
			{
				throw new InvalidOperationException(ExceptionMessages.OnlyOnePartAllowed);
			}
			OpenXmlPart openXmlPart = base.CreateOpenXmlPart(relationshipType);
			openXmlPart.CreateInternal(this.OpenXmlPackage, this, contentType, null);
			string text = base.AttachChild(openXmlPart);
			base.ChildrenParts.Add(text, openXmlPart);
			return openXmlPart;
		}

		// Token: 0x0600D0BD RID: 53437 RVA: 0x002993B8 File Offset: 0x002975B8
		internal void CreateInternal(OpenXmlPackage openXmlPackage, OpenXmlPart parent, string contentType, string targetExt)
		{
			if (openXmlPackage == null && parent == null)
			{
				throw new ArgumentNullException(ExceptionMessages.PackageRelatedArgumentNullException);
			}
			if (parent != null && openXmlPackage != null && parent.OpenXmlPackage != openXmlPackage)
			{
				throw new ArgumentOutOfRangeException("parent");
			}
			if (parent != null && openXmlPackage == null)
			{
				openXmlPackage = parent.OpenXmlPackage;
			}
			if (this._metroPart != null)
			{
				throw new InvalidOperationException();
			}
			this._openXmlPackage = openXmlPackage;
			Uri uri;
			if (parent != null)
			{
				uri = parent.Uri;
			}
			else
			{
				uri = new Uri("/", UriKind.Relative);
			}
			string text = this.TargetPath;
			if (text == null)
			{
				text = ".";
			}
			string text2 = targetExt;
			if (!this.IsContentTypeFixed && !this._openXmlPackage.PartExtensionProvider.TryGetValue(contentType, out text2))
			{
				text2 = targetExt;
			}
			if (text2 == null)
			{
				text2 = this.TargetFileExtension;
			}
			this._uri = this._openXmlPackage.GetUniquePartUri(contentType, uri, text, this.TargetName, text2);
			this._metroPart = this._openXmlPackage.CreateMetroPart(this._uri, contentType);
		}

		// Token: 0x0600D0BE RID: 53438 RVA: 0x0029949C File Offset: 0x0029769C
		internal void CreateInternal2(OpenXmlPackage openXmlPackage, OpenXmlPart parent, string contentType, Uri partUri)
		{
			if (openXmlPackage == null && parent == null)
			{
				throw new ArgumentNullException(ExceptionMessages.PackageRelatedArgumentNullException);
			}
			if (parent != null && openXmlPackage != null && parent.OpenXmlPackage != openXmlPackage)
			{
				throw new ArgumentOutOfRangeException("parent");
			}
			if (parent != null && openXmlPackage == null)
			{
				openXmlPackage = parent.OpenXmlPackage;
			}
			if (this._metroPart != null)
			{
				throw new InvalidOperationException();
			}
			this._openXmlPackage = openXmlPackage;
			Uri uri;
			if (parent != null)
			{
				uri = parent.Uri;
			}
			else
			{
				uri = new Uri("/", UriKind.Relative);
			}
			this._uri = this._openXmlPackage.GetUniquePartUri(contentType, uri, partUri);
			this._metroPart = this._openXmlPackage.CreateMetroPart(this._uri, contentType);
		}

		// Token: 0x17003265 RID: 12901
		// (get) Token: 0x0600D0BF RID: 53439 RVA: 0x0029953C File Offset: 0x0029773C
		public OpenXmlPackage OpenXmlPackage
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._openXmlPackage;
			}
		}

		// Token: 0x17003266 RID: 12902
		// (get) Token: 0x0600D0C0 RID: 53440 RVA: 0x0029954A File Offset: 0x0029774A
		public Uri Uri
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._uri;
			}
		}

		// Token: 0x0600D0C1 RID: 53441 RVA: 0x00299558 File Offset: 0x00297758
		public IEnumerable<OpenXmlPart> GetParentParts()
		{
			this.ThrowIfObjectDisposed();
			Dictionary<OpenXmlPart, bool> liveParts = new Dictionary<OpenXmlPart, bool>();
			this.OpenXmlPackage.FindAllReachableParts(liveParts);
			foreach (OpenXmlPart part in liveParts.Keys)
			{
				if (part.IsChildPart(this))
				{
					yield return part;
				}
			}
			yield break;
		}

		// Token: 0x0600D0C2 RID: 53442 RVA: 0x00299575 File Offset: 0x00297775
		public Stream GetStream()
		{
			this.ThrowIfObjectDisposed();
			return this.PackagePart.GetStream();
		}

		// Token: 0x0600D0C3 RID: 53443 RVA: 0x00299588 File Offset: 0x00297788
		public Stream GetStream(FileMode mode)
		{
			this.ThrowIfObjectDisposed();
			return this.PackagePart.GetStream(mode);
		}

		// Token: 0x0600D0C4 RID: 53444 RVA: 0x0029959C File Offset: 0x0029779C
		public Stream GetStream(FileMode mode, FileAccess access)
		{
			this.ThrowIfObjectDisposed();
			return this.PackagePart.GetStream(mode, access);
		}

		// Token: 0x0600D0C5 RID: 53445 RVA: 0x002995B4 File Offset: 0x002977B4
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

		// Token: 0x17003267 RID: 12903
		// (get) Token: 0x0600D0C6 RID: 53446 RVA: 0x00299600 File Offset: 0x00297800
		public virtual string ContentType
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.PackagePart.ContentType;
			}
		}

		// Token: 0x17003268 RID: 12904
		// (get) Token: 0x0600D0C7 RID: 53447
		public abstract string RelationshipType { get; }

		// Token: 0x0600D0C8 RID: 53448 RVA: 0x00299614 File Offset: 0x00297814
		[Obsolete("This functionality is obsolete and will be removed from future version release. Please see OpenXmlValidator class for supported validation functionality.", false)]
		public void ValidateXml(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			this.ThrowIfObjectDisposed();
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.MaxCharactersInDocument = this.MaxCharactersInPart;
			using (Stream stream = this.GetStream())
			{
				xmlReaderSettings.Schemas = schemas;
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				if (validationEventHandler != null)
				{
					xmlReaderSettings.ValidationEventHandler += validationEventHandler;
				}
				XmlReader xmlReader2;
				XmlReader xmlReader = (xmlReader2 = XmlReader.Create(stream, xmlReaderSettings));
				try
				{
					while (xmlReader.Read())
					{
					}
				}
				finally
				{
					if (xmlReader2 != null)
					{
						((IDisposable)xmlReader2).Dispose();
					}
				}
			}
		}

		// Token: 0x0600D0C9 RID: 53449 RVA: 0x002996B0 File Offset: 0x002978B0
		[Obsolete("This functionality is obsolete and will be removed from future version release. Please see OpenXmlValidator class for supported validation functionality.", false)]
		public void ValidateXml(string schemaFile, ValidationEventHandler validationEventHandler)
		{
			this.ThrowIfObjectDisposed();
			if (schemaFile == null)
			{
				throw new ArgumentNullException("schemaFile");
			}
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.Add(null, schemaFile);
			this.ValidateXml(xmlSchemaSet, validationEventHandler);
		}

		// Token: 0x17003269 RID: 12905
		// (get) Token: 0x0600D0CA RID: 53450 RVA: 0x002996E8 File Offset: 0x002978E8
		public OpenXmlPartRootElement RootElement
		{
			get
			{
				return this.PartRootElement;
			}
		}

		// Token: 0x1700326A RID: 12906
		// (get) Token: 0x0600D0CB RID: 53451 RVA: 0x002996F0 File Offset: 0x002978F0
		internal PackagePart PackagePart
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._metroPart;
			}
		}

		// Token: 0x1700326B RID: 12907
		// (get) Token: 0x0600D0CC RID: 53452 RVA: 0x002996FE File Offset: 0x002978FE
		internal long MaxCharactersInPart
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.OpenXmlPackage.MaxCharactersInPart;
			}
		}

		// Token: 0x1700326C RID: 12908
		// (get) Token: 0x0600D0CD RID: 53453 RVA: 0x00299711 File Offset: 0x00297911
		internal virtual bool IsContentTypeFixed
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return false;
			}
		}

		// Token: 0x0600D0CE RID: 53454 RVA: 0x0029971C File Offset: 0x0029791C
		internal sealed override void FindAllReachableParts(IDictionary<OpenXmlPart, bool> reachableParts)
		{
			this.ThrowIfObjectDisposed();
			if (reachableParts == null)
			{
				throw new ArgumentNullException("reachableParts");
			}
			reachableParts.Add(this, false);
			foreach (OpenXmlPart openXmlPart in base.ChildrenParts.Values)
			{
				if (!reachableParts.ContainsKey(openXmlPart))
				{
					openXmlPart.FindAllReachableParts(reachableParts);
				}
			}
		}

		// Token: 0x1700326D RID: 12909
		// (get) Token: 0x0600D0CF RID: 53455
		internal abstract string TargetPath { get; }

		// Token: 0x1700326E RID: 12910
		// (get) Token: 0x0600D0D0 RID: 53456
		internal abstract string TargetName { get; }

		// Token: 0x1700326F RID: 12911
		// (get) Token: 0x0600D0D1 RID: 53457 RVA: 0x00299794 File Offset: 0x00297994
		internal virtual string TargetFileExtension
		{
			get
			{
				return ".xml";
			}
		}

		// Token: 0x17003270 RID: 12912
		// (get) Token: 0x0600D0D2 RID: 53458 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x0600D0D3 RID: 53459 RVA: 0x0000EE09 File Offset: 0x0000D009
		internal virtual OpenXmlPartRootElement _rootElement
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17003271 RID: 12913
		// (get) Token: 0x0600D0D4 RID: 53460 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600D0D5 RID: 53461 RVA: 0x00002139 File Offset: 0x00000339
		internal virtual bool IsInVersion(FileFormatVersions version)
		{
			return true;
		}

		// Token: 0x17003272 RID: 12914
		// (get) Token: 0x0600D0D6 RID: 53462 RVA: 0x0029979B File Offset: 0x0029799B
		internal bool IsRootElementLoaded
		{
			get
			{
				return this._rootElement != null;
			}
		}

		// Token: 0x0600D0D7 RID: 53463 RVA: 0x002997AC File Offset: 0x002979AC
		internal OpenXmlPartRootElement SetPartRootElementToNull()
		{
			OpenXmlPartRootElement rootElement = this._rootElement;
			if (this._rootElement != null)
			{
				this._rootElement = null;
			}
			return rootElement;
		}

		// Token: 0x0600D0D8 RID: 53464 RVA: 0x002997D0 File Offset: 0x002979D0
		internal void LoadDomTree<T>() where T : OpenXmlPartRootElement, new()
		{
			bool flag = true;
			using (Stream stream = this.GetStream(FileMode.OpenOrCreate, FileAccess.Read))
			{
				if (stream.Length > 0L)
				{
					flag = false;
				}
				if (!flag)
				{
					T t = new T();
					try
					{
						if (t.LoadFromPart(this, stream))
						{
							t.OpenXmlPart = this;
							this._rootElement = t;
						}
					}
					catch (InvalidDataException ex)
					{
						throw new InvalidDataException(ExceptionMessages.CannotLoadRootElement, ex);
					}
				}
			}
		}

		// Token: 0x0600D0D9 RID: 53465 RVA: 0x00299860 File Offset: 0x00297A60
		internal void SetDomTree(OpenXmlPartRootElement partRootElement)
		{
			if (partRootElement.OpenXmlPart != null)
			{
				throw new ArgumentException(ExceptionMessages.PartRootAlreadyHasAssociation, "partRootElement");
			}
			partRootElement.OpenXmlPart = this;
			if (this._rootElement != null)
			{
				this._rootElement.OpenXmlPart = null;
			}
			this._rootElement = partRootElement;
		}

		// Token: 0x0600D0DA RID: 53466 RVA: 0x0029989C File Offset: 0x00297A9C
		internal void Destroy()
		{
			this.OpenXmlPackage.Package.DeletePart(this.Uri);
			base.PartDictionary = null;
			base.ReferenceRelationshipList.Clear();
			this._openXmlPackage = null;
			this._metroPart = null;
			this._uri = null;
			if (this._rootElement != null)
			{
				this._rootElement.OpenXmlPart = null;
				this._rootElement = null;
			}
		}

		// Token: 0x0600D0DB RID: 53467 RVA: 0x00299901 File Offset: 0x00297B01
		protected sealed override void ThrowIfObjectDisposed()
		{
			if (this._openXmlPackage == null)
			{
				throw new InvalidOperationException(ExceptionMessages.PartIsDestroyed);
			}
		}

		// Token: 0x17003273 RID: 12915
		// (get) Token: 0x0600D0DC RID: 53468 RVA: 0x00299916 File Offset: 0x00297B16
		internal sealed override OpenXmlPackage InternalOpenXmlPackage
		{
			get
			{
				return this._openXmlPackage;
			}
		}

		// Token: 0x17003274 RID: 12916
		// (get) Token: 0x0600D0DD RID: 53469 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		internal sealed override OpenXmlPart ThisOpenXmlPart
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600D0DE RID: 53470 RVA: 0x0029991E File Offset: 0x00297B1E
		internal sealed override void DeleteRelationship(string id)
		{
			this.ThrowIfObjectDisposed();
			this.PackagePart.DeleteRelationship(id);
		}

		// Token: 0x0600D0DF RID: 53471 RVA: 0x00299932 File Offset: 0x00297B32
		internal sealed override PackageRelationship CreateRelationship(Uri targetUri, TargetMode targetMode, string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			return this._metroPart.CreateRelationship(targetUri, targetMode, relationshipType);
		}

		// Token: 0x0600D0E0 RID: 53472 RVA: 0x00299948 File Offset: 0x00297B48
		internal sealed override PackageRelationship CreateRelationship(Uri targetUri, TargetMode targetMode, string relationshipType, string id)
		{
			this.ThrowIfObjectDisposed();
			return this._metroPart.CreateRelationship(targetUri, targetMode, relationshipType, id);
		}

		// Token: 0x0600D0E1 RID: 53473 RVA: 0x00299960 File Offset: 0x00297B60
		internal static void CopyStream(Stream sourceStream, Stream targetStream)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("sourceStream");
			}
			using (BinaryReader binaryReader = new BinaryReader(sourceStream))
			{
				byte[] array = new byte[4096];
				int num;
				do
				{
					num = binaryReader.Read(array, 0, 4096);
					if (num > 0)
					{
						targetStream.Write(array, 0, num);
					}
				}
				while (num > 0);
			}
		}

		// Token: 0x0400690A RID: 26890
		private const string DefaultTargetExt = ".xml";

		// Token: 0x0400690B RID: 26891
		private OpenXmlPackage _openXmlPackage;

		// Token: 0x0400690C RID: 26892
		private PackagePart _metroPart;

		// Token: 0x0400690D RID: 26893
		private Uri _uri;

		// Token: 0x0400690E RID: 26894
		internal MarkupCompatibilityProcessSettings MCSettings;
	}
}
