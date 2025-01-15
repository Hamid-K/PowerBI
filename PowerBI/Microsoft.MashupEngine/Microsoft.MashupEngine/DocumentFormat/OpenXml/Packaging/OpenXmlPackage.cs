using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002119 RID: 8473
	internal abstract class OpenXmlPackage : OpenXmlPartContainer, IDisposable
	{
		// Token: 0x170032A3 RID: 12963
		// (get) Token: 0x0600D177 RID: 53623 RVA: 0x0029AA78 File Offset: 0x00298C78
		// (set) Token: 0x0600D178 RID: 53624 RVA: 0x0029AA80 File Offset: 0x00298C80
		internal OpenSettings OpenSettings { get; set; }

		// Token: 0x0600D17A RID: 53626 RVA: 0x0029AAB2 File Offset: 0x00298CB2
		internal void OpenCore(Package package)
		{
			if (package == null)
			{
				throw new ArgumentNullException("package");
			}
			if (package.FileOpenAccess == FileAccess.Write)
			{
				throw new IOException(ExceptionMessages.PackageMustCanBeRead);
			}
			this._accessMode = package.FileOpenAccess;
			this._metroPackage = package;
			this.Load();
		}

		// Token: 0x0600D17B RID: 53627 RVA: 0x0029AAEF File Offset: 0x00298CEF
		internal void CreateCore(Package package)
		{
			if (package == null)
			{
				throw new ArgumentNullException("package");
			}
			this._accessMode = package.FileOpenAccess;
			this._metroPackage = package;
		}

		// Token: 0x0600D17C RID: 53628 RVA: 0x0029AB14 File Offset: 0x00298D14
		internal void OpenCore(Stream stream, bool readWriteMode)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (readWriteMode)
			{
				this._accessMode = FileAccess.ReadWrite;
			}
			else
			{
				this._accessMode = FileAccess.Read;
			}
			this._metroPackage = Package.Open(stream, (this._accessMode == FileAccess.Read) ? FileMode.Open : FileMode.OpenOrCreate, this._accessMode);
			this.Load();
		}

		// Token: 0x0600D17D RID: 53629 RVA: 0x0029AB67 File Offset: 0x00298D67
		internal void CreateCore(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new OpenXmlPackageException(ExceptionMessages.StreamAccessModeShouldBeWrite);
			}
			this._accessMode = FileAccess.ReadWrite;
			this._metroPackage = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);
		}

		// Token: 0x0600D17E RID: 53630 RVA: 0x0029ABA0 File Offset: 0x00298DA0
		internal void OpenCore(string path, bool readWriteMode)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (readWriteMode)
			{
				this._accessMode = FileAccess.ReadWrite;
			}
			else
			{
				this._accessMode = FileAccess.Read;
			}
			this._metroPackage = Package.Open(path, (this._accessMode == FileAccess.Read) ? FileMode.Open : FileMode.OpenOrCreate, this._accessMode, (this._accessMode == FileAccess.Read) ? FileShare.Read : FileShare.None);
			this.Load();
		}

		// Token: 0x0600D17F RID: 53631 RVA: 0x0029AC00 File Offset: 0x00298E00
		internal void CreateCore(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this._accessMode = FileAccess.ReadWrite;
			this._metroPackage = Package.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x0600D180 RID: 53632 RVA: 0x0029AC28 File Offset: 0x00298E28
		private void Load()
		{
			try
			{
				Dictionary<Uri, OpenXmlPart> dictionary = new Dictionary<Uri, OpenXmlPart>();
				PackageRelationshipCollection relationships = this._metroPackage.GetRelationships();
				bool flag = false;
				foreach (PackageRelationship packageRelationship in relationships)
				{
					if (packageRelationship.RelationshipType == this.MainPartRelationshipType)
					{
						flag = true;
						Uri uri = PackUriHelper.ResolvePartUri(new Uri("/", UriKind.Relative), packageRelationship.TargetUri);
						PackagePart part = this.Package.GetPart(uri);
						if (!this.IsValidMainPartContentType(part.ContentType))
						{
							OpenXmlPackageException ex = new OpenXmlPackageException(ExceptionMessages.InvalidPackageType);
							throw ex;
						}
						this.MainPartContentType = part.ContentType;
						break;
					}
				}
				if (!flag)
				{
					OpenXmlPackageException ex2 = new OpenXmlPackageException(ExceptionMessages.NoMainPart);
					throw ex2;
				}
				base.LoadReferencedPartsAndRelationships(this, null, relationships, dictionary);
			}
			catch (OpenXmlPackageException)
			{
				this.Close();
				throw;
			}
			catch (Exception)
			{
				this.Close();
				throw;
			}
		}

		// Token: 0x170032A4 RID: 12964
		// (get) Token: 0x0600D181 RID: 53633 RVA: 0x0029AD34 File Offset: 0x00298F34
		public Package Package
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._metroPackage;
			}
		}

		// Token: 0x170032A5 RID: 12965
		// (get) Token: 0x0600D182 RID: 53634 RVA: 0x0029AD42 File Offset: 0x00298F42
		public FileAccess FileOpenAccess
		{
			get
			{
				return this._metroPackage.FileOpenAccess;
			}
		}

		// Token: 0x170032A6 RID: 12966
		// (get) Token: 0x0600D183 RID: 53635 RVA: 0x0029AD4F File Offset: 0x00298F4F
		// (set) Token: 0x0600D184 RID: 53636 RVA: 0x0029AD57 File Offset: 0x00298F57
		public CompressionOption CompressionOption
		{
			get
			{
				return this._compressionOption;
			}
			set
			{
				this._compressionOption = value;
			}
		}

		// Token: 0x170032A7 RID: 12967
		// (get) Token: 0x0600D185 RID: 53637 RVA: 0x0029AD60 File Offset: 0x00298F60
		public PackageProperties PackageProperties
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.Package.PackageProperties;
			}
		}

		// Token: 0x170032A8 RID: 12968
		// (get) Token: 0x0600D186 RID: 53638 RVA: 0x0029AD73 File Offset: 0x00298F73
		public PartExtensionProvider PartExtensionProvider
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._partExtensionProvider;
			}
		}

		// Token: 0x170032A9 RID: 12969
		// (get) Token: 0x0600D187 RID: 53639 RVA: 0x0029AD81 File Offset: 0x00298F81
		// (set) Token: 0x0600D188 RID: 53640 RVA: 0x0029AD89 File Offset: 0x00298F89
		public long MaxCharactersInPart { get; internal set; }

		// Token: 0x170032AA RID: 12970
		// (get) Token: 0x0600D189 RID: 53641 RVA: 0x0029AD92 File Offset: 0x00298F92
		public IEnumerable<DataPart> DataParts
		{
			get
			{
				return this._dataPartList;
			}
		}

		// Token: 0x0600D18A RID: 53642 RVA: 0x0029AD9C File Offset: 0x00298F9C
		public override T AddPart<T>(T part)
		{
			this.ThrowIfObjectDisposed();
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			if (part.RelationshipType == this.MainPartRelationshipType && part.ContentType != this.MainPartContentType)
			{
				throw new ArgumentOutOfRangeException(ExceptionMessages.MainPartIsDifferent);
			}
			return (T)((object)this.AddPartFrom(part, null));
		}

		// Token: 0x0600D18B RID: 53643 RVA: 0x0029AE13 File Offset: 0x00299013
		public void DeletePartsRecursivelyOfType<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			base.DeletePartsRecursivelyOfTypeBase<T>();
		}

		// Token: 0x0600D18C RID: 53644 RVA: 0x0029AE21 File Offset: 0x00299021
		public void Close()
		{
			this.ThrowIfObjectDisposed();
			this.Dispose();
		}

		// Token: 0x0600D18D RID: 53645 RVA: 0x0029AE30 File Offset: 0x00299030
		public MediaDataPart CreateMediaDataPart(string contentType)
		{
			this.ThrowIfObjectDisposed();
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			MediaDataPart mediaDataPart = new MediaDataPart();
			mediaDataPart.CreateInternal(this.InternalOpenXmlPackage, contentType, null);
			this._dataPartList.AddLast(mediaDataPart);
			return mediaDataPart;
		}

		// Token: 0x0600D18E RID: 53646 RVA: 0x0029AE74 File Offset: 0x00299074
		public MediaDataPart CreateMediaDataPart(string contentType, string extension)
		{
			this.ThrowIfObjectDisposed();
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			MediaDataPart mediaDataPart = new MediaDataPart();
			mediaDataPart.CreateInternal(this.InternalOpenXmlPackage, contentType, extension);
			this._dataPartList.AddLast(mediaDataPart);
			return mediaDataPart;
		}

		// Token: 0x0600D18F RID: 53647 RVA: 0x0029AEC8 File Offset: 0x002990C8
		public MediaDataPart CreateMediaDataPart(MediaDataPartType mediaDataPartType)
		{
			this.ThrowIfObjectDisposed();
			MediaDataPart mediaDataPart = new MediaDataPart();
			mediaDataPart.CreateInternal(this.InternalOpenXmlPackage, mediaDataPartType);
			this._dataPartList.AddLast(mediaDataPart);
			return mediaDataPart;
		}

		// Token: 0x0600D190 RID: 53648 RVA: 0x0029AEFC File Offset: 0x002990FC
		public bool DeletePart(DataPart dataPart)
		{
			this.ThrowIfObjectDisposed();
			if (dataPart == null)
			{
				throw new ArgumentNullException("dataPart");
			}
			if (dataPart.OpenXmlPackage != this)
			{
				throw new InvalidOperationException(ExceptionMessages.ForeignDataPart);
			}
			if (OpenXmlPackage.IsOrphanDataPart(dataPart))
			{
				dataPart.Destroy();
				return this._dataPartList.Remove(dataPart);
			}
			throw new InvalidOperationException(ExceptionMessages.DataPartIsInUse);
		}

		// Token: 0x0600D191 RID: 53649 RVA: 0x0029AF58 File Offset: 0x00299158
		[Obsolete("This functionality is obsolete and will be removed from future version release. Please see OpenXmlValidator class for supported validation functionality.", false)]
		public void Validate(OpenXmlPackageValidationSettings validationSettings)
		{
			this.ThrowIfObjectDisposed();
			OpenXmlPackageValidationSettings openXmlPackageValidationSettings;
			if (validationSettings != null && validationSettings.GetEventHandler() != null)
			{
				openXmlPackageValidationSettings = validationSettings;
			}
			else
			{
				openXmlPackageValidationSettings = new OpenXmlPackageValidationSettings();
				openXmlPackageValidationSettings.EventHandler += OpenXmlPackage.DefaultValidationEventHandler;
			}
			openXmlPackageValidationSettings.FileFormat = FileFormatVersions.Office2007;
			Dictionary<OpenXmlPart, bool> dictionary = new Dictionary<OpenXmlPart, bool>();
			base.ValidateInternal(openXmlPackageValidationSettings, dictionary);
		}

		// Token: 0x0600D192 RID: 53650 RVA: 0x0029AFA8 File Offset: 0x002991A8
		internal void Validate(OpenXmlPackageValidationSettings validationSettings, FileFormatVersions fileFormatVersion)
		{
			this.ThrowIfObjectDisposed();
			validationSettings.FileFormat = fileFormatVersion;
			Dictionary<OpenXmlPart, bool> dictionary = new Dictionary<OpenXmlPart, bool>();
			base.ValidateInternal(validationSettings, dictionary);
		}

		// Token: 0x0600D193 RID: 53651 RVA: 0x0029AFD0 File Offset: 0x002991D0
		internal void ReserveUri(string contentType, Uri partUri)
		{
			this.ThrowIfObjectDisposed();
			this._partUriHelper.ReserveUri(contentType, partUri);
		}

		// Token: 0x0600D194 RID: 53652 RVA: 0x0029AFE8 File Offset: 0x002991E8
		internal Uri GetUniquePartUri(string contentType, Uri parentUri, string targetPath, string targetName, string targetExt)
		{
			this.ThrowIfObjectDisposed();
			Uri uniquePartUri;
			do
			{
				uniquePartUri = this._partUriHelper.GetUniquePartUri(contentType, parentUri, targetPath, targetName, targetExt);
			}
			while (this._metroPackage.PartExists(uniquePartUri));
			return uniquePartUri;
		}

		// Token: 0x0600D195 RID: 53653 RVA: 0x0029B020 File Offset: 0x00299220
		internal Uri GetUniquePartUri(string contentType, Uri parentUri, Uri targetUri)
		{
			this.ThrowIfObjectDisposed();
			Uri uniquePartUri;
			do
			{
				uniquePartUri = this._partUriHelper.GetUniquePartUri(contentType, parentUri, targetUri);
			}
			while (this._metroPackage.PartExists(uniquePartUri));
			return uniquePartUri;
		}

		// Token: 0x0600D196 RID: 53654 RVA: 0x0029B053 File Offset: 0x00299253
		protected override void ThrowIfObjectDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600D197 RID: 53655 RVA: 0x0029B070 File Offset: 0x00299270
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this.SavePartContents();
					this.DeleteUnusedDataPartOnClose();
					this._metroPackage.Close();
					this._metroPackage = null;
					base.PartDictionary = null;
					base.ReferenceRelationshipList.Clear();
					this._partUriHelper = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x0600D198 RID: 53656 RVA: 0x0029B0C6 File Offset: 0x002992C6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170032AB RID: 12971
		// (get) Token: 0x0600D199 RID: 53657 RVA: 0x0029B0D5 File Offset: 0x002992D5
		public MarkupCompatibilityProcessSettings MarkupCompatibilityProcessSettings
		{
			get
			{
				if (this.OpenSettings.MarkupCompatibilityProcessSettings == null)
				{
					return new MarkupCompatibilityProcessSettings(MarkupCompatibilityProcessMode.NoProcess, FileFormatVersions.Office2007);
				}
				return this.OpenSettings.MarkupCompatibilityProcessSettings;
			}
		}

		// Token: 0x170032AC RID: 12972
		// (get) Token: 0x0600D19A RID: 53658 RVA: 0x0029B0F7 File Offset: 0x002992F7
		public bool AutoSave
		{
			get
			{
				return this.OpenSettings.AutoSave;
			}
		}

		// Token: 0x0600D19B RID: 53659 RVA: 0x0029B104 File Offset: 0x00299304
		private void SavePartContents()
		{
			if (this.FileOpenAccess == FileAccess.Read)
			{
				return;
			}
			if (!this.AutoSave)
			{
				return;
			}
			OpenXmlPackagePartIterator openXmlPackagePartIterator = new OpenXmlPackagePartIterator(this);
			foreach (OpenXmlPart openXmlPart in openXmlPackagePartIterator)
			{
				OpenXmlPackage.TrySavePartContent(openXmlPart);
			}
		}

		// Token: 0x0600D19C RID: 53660 RVA: 0x0029B168 File Offset: 0x00299368
		private static void TrySavePartContent(OpenXmlPart part)
		{
			if (OpenXmlPackage.IsPartContentChanged(part))
			{
				OpenXmlPackage.SavePartContent(part);
			}
		}

		// Token: 0x0600D19D RID: 53661 RVA: 0x0029B178 File Offset: 0x00299378
		private static bool IsPartContentChanged(OpenXmlPart part)
		{
			return (!part.IsRootElementLoaded && part.OpenXmlPackage.MarkupCompatibilityProcessSettings.ProcessMode == MarkupCompatibilityProcessMode.ProcessAllParts && part.PartRootElement != null) || part.IsRootElementLoaded;
		}

		// Token: 0x0600D19E RID: 53662 RVA: 0x0029B1A5 File Offset: 0x002993A5
		private static void SavePartContent(OpenXmlPart part)
		{
			part.PartRootElement.Save();
		}

		// Token: 0x170032AD RID: 12973
		// (get) Token: 0x0600D19F RID: 53663
		internal abstract string MainPartRelationshipType { get; }

		// Token: 0x170032AE RID: 12974
		// (get) Token: 0x0600D1A0 RID: 53664 RVA: 0x0029B1B2 File Offset: 0x002993B2
		// (set) Token: 0x0600D1A1 RID: 53665 RVA: 0x0029B1BA File Offset: 0x002993BA
		internal string MainPartContentType
		{
			get
			{
				return this._mainPartContentType;
			}
			set
			{
				if (this.IsValidMainPartContentType(value))
				{
					this._mainPartContentType = value;
					return;
				}
				throw new ArgumentOutOfRangeException(ExceptionMessages.InvalidMainPartContentType);
			}
		}

		// Token: 0x170032AF RID: 12975
		// (get) Token: 0x0600D1A2 RID: 53666
		internal abstract ICollection<string> ValidMainPartContentTypes { get; }

		// Token: 0x0600D1A3 RID: 53667 RVA: 0x0029B1D7 File Offset: 0x002993D7
		internal bool IsValidMainPartContentType(string contentType)
		{
			return this.ValidMainPartContentTypes.Contains(contentType);
		}

		// Token: 0x0600D1A4 RID: 53668 RVA: 0x0029B1E8 File Offset: 0x002993E8
		internal void ChangeDocumentTypeInternal<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			T subPartOfType = base.GetSubPartOfType<T>();
			MemoryStream memoryStream = null;
			ExtendedPart extendedPart = null;
			Dictionary<string, OpenXmlPart> dictionary = new Dictionary<string, OpenXmlPart>();
			ReferenceRelationship[] array;
			try
			{
				using (Stream stream = subPartOfType.GetStream())
				{
					if (stream.Length > 2147483647L)
					{
						throw new OpenXmlPackageException(ExceptionMessages.DocumentTooBig);
					}
					memoryStream = new MemoryStream(Convert.ToInt32(stream.Length));
					OpenXmlPart.CopyStream(stream, memoryStream);
				}
				extendedPart = base.AddExtendedPart("http://temp", this.MainPartContentType, ".xml");
				foreach (KeyValuePair<string, OpenXmlPart> keyValuePair in subPartOfType.ChildrenParts)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				array = subPartOfType.ReferenceRelationshipList.ToArray<ReferenceRelationship>();
			}
			catch (OpenXmlPackageException ex)
			{
				throw new OpenXmlPackageException(ExceptionMessages.CannotChangeDocumentType, ex);
			}
			catch (SystemException ex2)
			{
				throw new OpenXmlPackageException(ExceptionMessages.CannotChangeDocumentType, ex2);
			}
			try
			{
				Uri uri = subPartOfType.Uri;
				string text = base.GetIdOfPart(subPartOfType);
				base.ChildrenParts.Remove(text);
				this.DeleteRelationship(text);
				subPartOfType.Destroy();
				T t = (T)((object)Activator.CreateInstance(typeof(T), true));
				t.CreateInternal2(this, null, this.MainPartContentType, uri);
				string text2 = base.AttachChild(t, text);
				base.ChildrenParts.Add(text2, t);
				memoryStream.Position = 0L;
				t.FeedData(memoryStream);
				foreach (KeyValuePair<string, OpenXmlPart> keyValuePair2 in dictionary)
				{
					t.AttachChild(keyValuePair2.Value, keyValuePair2.Key);
					t.ChildrenParts.Add(keyValuePair2);
				}
				foreach (ExternalRelationship externalRelationship in array.OfType<ExternalRelationship>())
				{
					t.AddExternalRelationship(externalRelationship.RelationshipType, externalRelationship.Uri, externalRelationship.Id);
				}
				foreach (HyperlinkRelationship hyperlinkRelationship in array.OfType<HyperlinkRelationship>())
				{
					t.AddHyperlinkRelationship(hyperlinkRelationship.Uri, hyperlinkRelationship.IsExternal, hyperlinkRelationship.Id);
				}
				foreach (DataPartReferenceRelationship dataPartReferenceRelationship in array.OfType<DataPartReferenceRelationship>())
				{
					t.AddDataPartReferenceRelationship(dataPartReferenceRelationship);
				}
				text = base.GetIdOfPart(extendedPart);
				base.ChildrenParts.Remove(text);
				this.DeleteRelationship(text);
				extendedPart.Destroy();
			}
			catch (OpenXmlPackageException ex3)
			{
				throw new OpenXmlPackageException(ExceptionMessages.CannotChangeDocumentType, ex3);
			}
			catch (SystemException ex4)
			{
				throw new OpenXmlPackageException(ExceptionMessages.CannotChangeDocumentTypeSerious, ex4);
			}
		}

		// Token: 0x0600D1A5 RID: 53669 RVA: 0x0029B618 File Offset: 0x00299818
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
				throw new InvalidOperationException();
			}
			OpenXmlPart openXmlPart = base.CreateOpenXmlPart(relationshipType);
			openXmlPart.CreateInternal(this, null, contentType, null);
			string text = base.AttachChild(openXmlPart);
			base.ChildrenParts.Add(text, openXmlPart);
			return openXmlPart;
		}

		// Token: 0x170032B0 RID: 12976
		// (get) Token: 0x0600D1A6 RID: 53670 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		internal sealed override OpenXmlPackage InternalOpenXmlPackage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170032B1 RID: 12977
		// (get) Token: 0x0600D1A7 RID: 53671 RVA: 0x000020FA File Offset: 0x000002FA
		internal sealed override OpenXmlPart ThisOpenXmlPart
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600D1A8 RID: 53672 RVA: 0x0029B694 File Offset: 0x00299894
		internal sealed override void FindAllReachableParts(IDictionary<OpenXmlPart, bool> reachableParts)
		{
			this.ThrowIfObjectDisposed();
			if (reachableParts == null)
			{
				throw new ArgumentNullException("reachableParts");
			}
			foreach (OpenXmlPart openXmlPart in base.ChildrenParts.Values)
			{
				if (!reachableParts.ContainsKey(openXmlPart))
				{
					openXmlPart.FindAllReachableParts(reachableParts);
				}
			}
		}

		// Token: 0x0600D1A9 RID: 53673 RVA: 0x0029B704 File Offset: 0x00299904
		internal sealed override void DeleteRelationship(string id)
		{
			this.ThrowIfObjectDisposed();
			this.Package.DeleteRelationship(id);
		}

		// Token: 0x0600D1AA RID: 53674 RVA: 0x0029B718 File Offset: 0x00299918
		internal sealed override PackageRelationship CreateRelationship(Uri targetUri, TargetMode targetMode, string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			return this.Package.CreateRelationship(targetUri, targetMode, relationshipType);
		}

		// Token: 0x0600D1AB RID: 53675 RVA: 0x0029B72E File Offset: 0x0029992E
		internal sealed override PackageRelationship CreateRelationship(Uri targetUri, TargetMode targetMode, string relationshipType, string id)
		{
			this.ThrowIfObjectDisposed();
			return this.Package.CreateRelationship(targetUri, targetMode, relationshipType, id);
		}

		// Token: 0x0600D1AC RID: 53676 RVA: 0x0029B746 File Offset: 0x00299946
		internal PackagePart CreateMetroPart(Uri partUri, string contentType)
		{
			return this.Package.CreatePart(partUri, contentType, this.CompressionOption);
		}

		// Token: 0x0600D1AD RID: 53677 RVA: 0x0029B75C File Offset: 0x0029995C
		private static void DefaultValidationEventHandler(object sender, OpenXmlPackageValidationEventArgs e)
		{
			throw new OpenXmlPackageException(ExceptionMessages.ValidationException)
			{
				Data = { { "OpenXmlPackageValidationEventArgs", e } }
			};
		}

		// Token: 0x0600D1AE RID: 53678 RVA: 0x0029B786 File Offset: 0x00299986
		private static bool IsOrphanDataPart(DataPart dataPart)
		{
			return !dataPart.GetDataPartReferenceRelationships().Any<DataPartReferenceRelationship>();
		}

		// Token: 0x0600D1AF RID: 53679 RVA: 0x0029B798 File Offset: 0x00299998
		private void DeleteUnusedDataPartOnClose()
		{
			if (this._dataPartList.Count > 0)
			{
				HashSet<DataPart> hashSet = new HashSet<DataPart>();
				foreach (DataPart dataPart in this.DataParts)
				{
					hashSet.Add(dataPart);
				}
				foreach (DataPartReferenceRelationship dataPartReferenceRelationship in base.DataPartReferenceRelationships)
				{
					hashSet.Remove(dataPartReferenceRelationship.DataPart);
					if (hashSet.Count == 0)
					{
						return;
					}
				}
				OpenXmlPackagePartIterator openXmlPackagePartIterator = new OpenXmlPackagePartIterator(this);
				foreach (OpenXmlPart openXmlPart in openXmlPackagePartIterator)
				{
					foreach (DataPartReferenceRelationship dataPartReferenceRelationship2 in openXmlPart.DataPartReferenceRelationships)
					{
						hashSet.Remove(dataPartReferenceRelationship2.DataPart);
						if (hashSet.Count == 0)
						{
							return;
						}
					}
				}
				foreach (DataPart dataPart2 in hashSet)
				{
					dataPart2.Destroy();
					this._dataPartList.Remove(dataPart2);
				}
			}
		}

		// Token: 0x0600D1B0 RID: 53680 RVA: 0x0029B934 File Offset: 0x00299B34
		internal DataPart FindDataPart(Uri partUri)
		{
			foreach (DataPart dataPart in this.DataParts)
			{
				if (dataPart.Uri == partUri)
				{
					return dataPart;
				}
			}
			return null;
		}

		// Token: 0x0600D1B1 RID: 53681 RVA: 0x0029B990 File Offset: 0x00299B90
		internal DataPart AddDataPartToList(DataPart dataPart)
		{
			this._dataPartList.AddLast(dataPart);
			return dataPart;
		}

		// Token: 0x04006949 RID: 26953
		private bool _disposed;

		// Token: 0x0400694A RID: 26954
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Package _metroPackage;

		// Token: 0x0400694B RID: 26955
		private FileAccess _accessMode;

		// Token: 0x0400694C RID: 26956
		private string _mainPartContentType;

		// Token: 0x0400694D RID: 26957
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CompressionOption _compressionOption;

		// Token: 0x0400694E RID: 26958
		private OpenXmlPackage.PartUriHelper _partUriHelper = new OpenXmlPackage.PartUriHelper();

		// Token: 0x0400694F RID: 26959
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PartExtensionProvider _partExtensionProvider = new PartExtensionProvider();

		// Token: 0x04006950 RID: 26960
		private LinkedList<DataPart> _dataPartList = new LinkedList<DataPart>();

		// Token: 0x0200211A RID: 8474
		internal class PartUriHelper
		{
			// Token: 0x0600D1B3 RID: 53683 RVA: 0x0029B9C0 File Offset: 0x00299BC0
			private bool IsReservedUri(Uri uri)
			{
				string text = uri.OriginalString.ToUpperInvariant();
				return this._reservedUri.ContainsKey(text);
			}

			// Token: 0x0600D1B4 RID: 53684 RVA: 0x0029B9E8 File Offset: 0x00299BE8
			internal void AddToReserveUri(Uri partUri)
			{
				string text = partUri.OriginalString.ToUpperInvariant();
				this._reservedUri.Add(text, 0);
			}

			// Token: 0x0600D1B5 RID: 53685 RVA: 0x0029BA0E File Offset: 0x00299C0E
			internal void ReserveUri(string contentType, Uri partUri)
			{
				this.GetNextSequenceNumber(contentType);
				this.AddToReserveUri(PackUriHelper.GetNormalizedPartUri(partUri));
			}

			// Token: 0x0600D1B6 RID: 53686 RVA: 0x0029BA24 File Offset: 0x00299C24
			internal Uri GetUniquePartUri(string contentType, Uri parentUri, string targetPath, string targetName, string targetExt)
			{
				Uri uri2;
				do
				{
					string nextSequenceNumber = this.GetNextSequenceNumber(contentType);
					string text = Path.Combine(targetPath, targetName + nextSequenceNumber + targetExt);
					Uri uri = new Uri(text, UriKind.RelativeOrAbsolute);
					uri2 = PackUriHelper.ResolvePartUri(parentUri, uri);
				}
				while (this.IsReservedUri(uri2));
				this.AddToReserveUri(uri2);
				return uri2;
			}

			// Token: 0x0600D1B7 RID: 53687 RVA: 0x0029BA6C File Offset: 0x00299C6C
			internal Uri GetUniquePartUri(string contentType, Uri parentUri, Uri targetUri)
			{
				Uri uri = PackUriHelper.ResolvePartUri(parentUri, targetUri);
				if (this.IsReservedUri(uri))
				{
					string text = ".";
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(targetUri.OriginalString);
					string extension = Path.GetExtension(targetUri.OriginalString);
					uri = this.GetUniquePartUri(contentType, uri, text, fileNameWithoutExtension, extension);
				}
				else
				{
					this.AddToReserveUri(uri);
				}
				return uri;
			}

			// Token: 0x0600D1B8 RID: 53688 RVA: 0x0029BAC0 File Offset: 0x00299CC0
			private string GetNextSequenceNumber(string contentType)
			{
				if (this._sequenceNumbers.ContainsKey(contentType))
				{
					Dictionary<string, int> sequenceNumbers;
					(sequenceNumbers = this._sequenceNumbers)[contentType] = sequenceNumbers[contentType] + 1;
					return Convert.ToString(this._sequenceNumbers[contentType], 16);
				}
				this._sequenceNumbers.Add(contentType, 1);
				return "";
			}

			// Token: 0x04006953 RID: 26963
			private Dictionary<string, int> _sequenceNumbers = new Dictionary<string, int>(20);

			// Token: 0x04006954 RID: 26964
			private Dictionary<string, int> _reservedUri = new Dictionary<string, int>();
		}
	}
}
