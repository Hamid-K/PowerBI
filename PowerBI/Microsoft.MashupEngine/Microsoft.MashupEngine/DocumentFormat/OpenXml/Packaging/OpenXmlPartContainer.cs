using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Xml;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002103 RID: 8451
	internal abstract class OpenXmlPartContainer
	{
		// Token: 0x17003254 RID: 12884
		// (get) Token: 0x0600D048 RID: 53320 RVA: 0x00296A1A File Offset: 0x00294C1A
		internal IDictionary<string, OpenXmlPart> ChildrenParts
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.PartDictionary;
			}
		}

		// Token: 0x17003255 RID: 12885
		// (get) Token: 0x0600D049 RID: 53321 RVA: 0x00296A28 File Offset: 0x00294C28
		// (set) Token: 0x0600D04A RID: 53322 RVA: 0x00296A36 File Offset: 0x00294C36
		internal Dictionary<string, OpenXmlPart> PartDictionary
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._childrenPartsDictionary;
			}
			set
			{
				this.ThrowIfObjectDisposed();
				this._childrenPartsDictionary = value;
			}
		}

		// Token: 0x17003256 RID: 12886
		// (get) Token: 0x0600D04B RID: 53323 RVA: 0x00296A45 File Offset: 0x00294C45
		internal LinkedList<ReferenceRelationship> ReferenceRelationshipList
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._referenceRelationships;
			}
		}

		// Token: 0x0600D04C RID: 53324 RVA: 0x00296A54 File Offset: 0x00294C54
		public void DeleteReferenceRelationship(ReferenceRelationship referenceRelationship)
		{
			this.ThrowIfObjectDisposed();
			if (referenceRelationship == null)
			{
				throw new ArgumentNullException("referenceRelationship");
			}
			if (referenceRelationship.Id == null || referenceRelationship.Container != this)
			{
				throw new InvalidOperationException(ExceptionMessages.ReferenceRelationshipIsNotReferenced);
			}
			if (this.ReferenceRelationshipList.Contains(referenceRelationship))
			{
				this.ReferenceRelationshipList.Remove(referenceRelationship);
				this.DeleteRelationship(referenceRelationship.Id);
				referenceRelationship.Container = null;
				return;
			}
			throw new InvalidOperationException(ExceptionMessages.ReferenceRelationshipIsNotReferenced);
		}

		// Token: 0x0600D04D RID: 53325 RVA: 0x00296ACC File Offset: 0x00294CCC
		public void DeleteReferenceRelationship(string id)
		{
			this.ThrowIfObjectDisposed();
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			foreach (ReferenceRelationship referenceRelationship in this.ReferenceRelationshipList)
			{
				if (referenceRelationship.Id == id)
				{
					this.ReferenceRelationshipList.Remove(referenceRelationship);
					this.DeleteRelationship(referenceRelationship.Id);
					referenceRelationship.Container = null;
					return;
				}
			}
			throw new KeyNotFoundException(ExceptionMessages.NoSpecifiedReferenceRelationship);
		}

		// Token: 0x0600D04E RID: 53326 RVA: 0x00296B68 File Offset: 0x00294D68
		public ReferenceRelationship GetReferenceRelationship(string id)
		{
			this.ThrowIfObjectDisposed();
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			foreach (ReferenceRelationship referenceRelationship in this.ReferenceRelationshipList)
			{
				if (referenceRelationship.Id == id)
				{
					return referenceRelationship;
				}
			}
			throw new KeyNotFoundException(ExceptionMessages.NoSpecifiedReferenceRelationship);
		}

		// Token: 0x17003257 RID: 12887
		// (get) Token: 0x0600D04F RID: 53327 RVA: 0x00296BE8 File Offset: 0x00294DE8
		public IEnumerable<ExternalRelationship> ExternalRelationships
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.ReferenceRelationshipList.OfType<ExternalRelationship>();
			}
		}

		// Token: 0x0600D050 RID: 53328 RVA: 0x00296BFC File Offset: 0x00294DFC
		public ExternalRelationship AddExternalRelationship(string relationshipType, Uri externalUri)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (externalUri == null)
			{
				throw new ArgumentNullException("externalUri");
			}
			if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink")
			{
				throw new InvalidOperationException(ExceptionMessages.UseAddHyperlinkRelationship);
			}
			PackageRelationship packageRelationship = this.CreateRelationship(externalUri, TargetMode.External, relationshipType);
			ExternalRelationship externalRelationship = new ExternalRelationship(packageRelationship.TargetUri, packageRelationship.RelationshipType, packageRelationship.Id);
			externalRelationship.Container = this;
			this.ReferenceRelationshipList.AddLast(externalRelationship);
			return externalRelationship;
		}

		// Token: 0x0600D051 RID: 53329 RVA: 0x00296C80 File Offset: 0x00294E80
		public ExternalRelationship AddExternalRelationship(string relationshipType, Uri externalUri, string id)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (externalUri == null)
			{
				throw new ArgumentNullException("externalUri");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink")
			{
				throw new InvalidOperationException(ExceptionMessages.UseAddHyperlinkRelationship);
			}
			PackageRelationship packageRelationship = this.CreateRelationship(externalUri, TargetMode.External, relationshipType, id);
			ExternalRelationship externalRelationship = new ExternalRelationship(packageRelationship.TargetUri, packageRelationship.RelationshipType, packageRelationship.Id);
			externalRelationship.Container = this;
			this.ReferenceRelationshipList.AddLast(externalRelationship);
			return externalRelationship;
		}

		// Token: 0x0600D052 RID: 53330 RVA: 0x00296D14 File Offset: 0x00294F14
		public void DeleteExternalRelationship(ExternalRelationship externalRelationship)
		{
			this.ThrowIfObjectDisposed();
			if (externalRelationship == null)
			{
				throw new ArgumentNullException("externalRelationship");
			}
			if (externalRelationship.Id == null || externalRelationship.Container != this)
			{
				throw new InvalidOperationException(ExceptionMessages.ExternalRelationshipIsNotReferenced);
			}
			if (this.ReferenceRelationshipList.Contains(externalRelationship))
			{
				this.ReferenceRelationshipList.Remove(externalRelationship);
				this.DeleteRelationship(externalRelationship.Id);
				externalRelationship.Container = null;
				return;
			}
			throw new InvalidOperationException(ExceptionMessages.ExternalRelationshipIsNotReferenced);
		}

		// Token: 0x0600D053 RID: 53331 RVA: 0x00296D8C File Offset: 0x00294F8C
		public void DeleteExternalRelationship(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			foreach (ExternalRelationship externalRelationship in this.ReferenceRelationshipList.OfType<ExternalRelationship>())
			{
				if (externalRelationship.Id == id)
				{
					this.ReferenceRelationshipList.Remove(externalRelationship);
					this.DeleteRelationship(externalRelationship.Id);
					externalRelationship.Container = null;
					return;
				}
			}
			throw new KeyNotFoundException(ExceptionMessages.NoSpecifiedExternalRelationship);
		}

		// Token: 0x0600D054 RID: 53332 RVA: 0x00296E20 File Offset: 0x00295020
		public ExternalRelationship GetExternalRelationship(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			foreach (ExternalRelationship externalRelationship in this.ReferenceRelationshipList.OfType<ExternalRelationship>())
			{
				if (externalRelationship.Id == id)
				{
					return externalRelationship;
				}
			}
			throw new KeyNotFoundException(ExceptionMessages.NoSpecifiedExternalRelationship);
		}

		// Token: 0x17003258 RID: 12888
		// (get) Token: 0x0600D055 RID: 53333 RVA: 0x00296E98 File Offset: 0x00295098
		public IEnumerable<HyperlinkRelationship> HyperlinkRelationships
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.ReferenceRelationshipList.OfType<HyperlinkRelationship>();
			}
		}

		// Token: 0x0600D056 RID: 53334 RVA: 0x00296EAC File Offset: 0x002950AC
		public HyperlinkRelationship AddHyperlinkRelationship(Uri hyperlinkUri, bool isExternal)
		{
			this.ThrowIfObjectDisposed();
			if (hyperlinkUri == null)
			{
				throw new ArgumentNullException("hyperlinkUri");
			}
			TargetMode targetMode = (isExternal ? TargetMode.External : TargetMode.Internal);
			PackageRelationship packageRelationship = this.CreateRelationship(hyperlinkUri, targetMode, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink");
			HyperlinkRelationship hyperlinkRelationship = new HyperlinkRelationship(packageRelationship.TargetUri, isExternal, packageRelationship.Id);
			hyperlinkRelationship.Container = this;
			this.ReferenceRelationshipList.AddLast(hyperlinkRelationship);
			return hyperlinkRelationship;
		}

		// Token: 0x0600D057 RID: 53335 RVA: 0x00296F14 File Offset: 0x00295114
		public HyperlinkRelationship AddHyperlinkRelationship(Uri hyperlinkUri, bool isExternal, string id)
		{
			this.ThrowIfObjectDisposed();
			if (hyperlinkUri == null)
			{
				throw new ArgumentNullException("hyperlinkUri");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			PackageRelationship packageRelationship = this.CreateRelationship(hyperlinkUri, TargetMode.External, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink", id);
			HyperlinkRelationship hyperlinkRelationship = new HyperlinkRelationship(packageRelationship.TargetUri, isExternal, packageRelationship.Id);
			hyperlinkRelationship.Container = this;
			this.ReferenceRelationshipList.AddLast(hyperlinkRelationship);
			return hyperlinkRelationship;
		}

		// Token: 0x17003259 RID: 12889
		// (get) Token: 0x0600D058 RID: 53336 RVA: 0x00296F80 File Offset: 0x00295180
		public IEnumerable<DataPartReferenceRelationship> DataPartReferenceRelationships
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.ReferenceRelationshipList.OfType<DataPartReferenceRelationship>();
			}
		}

		// Token: 0x0600D059 RID: 53337 RVA: 0x00296F94 File Offset: 0x00295194
		internal T AddDataPartReferenceRelationship<T>(MediaDataPart mediaDataPart) where T : DataPartReferenceRelationship
		{
			this.ThrowIfObjectDisposed();
			if (mediaDataPart == null)
			{
				throw new ArgumentNullException("mediaDataPart");
			}
			if (mediaDataPart.OpenXmlPackage != this.InternalOpenXmlPackage)
			{
				throw new InvalidOperationException(ExceptionMessages.ForeignMediaDataPart);
			}
			T t = (T)((object)Activator.CreateInstance(typeof(T), true));
			PackageRelationship packageRelationship = this.CreateRelationship(mediaDataPart.Uri, TargetMode.Internal, t.RelationshipType);
			t.Initialize(this, mediaDataPart, t.RelationshipType, packageRelationship.Id);
			this.ReferenceRelationshipList.AddLast(t);
			return t;
		}

		// Token: 0x0600D05A RID: 53338 RVA: 0x00297034 File Offset: 0x00295234
		internal T AddDataPartReferenceRelationship<T>(MediaDataPart mediaDataPart, string id) where T : DataPartReferenceRelationship
		{
			this.ThrowIfObjectDisposed();
			if (mediaDataPart == null)
			{
				throw new ArgumentNullException("mediaDataPart");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (mediaDataPart.OpenXmlPackage != this.InternalOpenXmlPackage)
			{
				throw new InvalidOperationException(ExceptionMessages.ForeignMediaDataPart);
			}
			T t = (T)((object)Activator.CreateInstance(typeof(T), true));
			PackageRelationship packageRelationship = this.CreateRelationship(mediaDataPart.Uri, TargetMode.Internal, t.RelationshipType, id);
			t.Initialize(this, mediaDataPart, t.RelationshipType, packageRelationship.Id);
			this.ReferenceRelationshipList.AddLast(t);
			return t;
		}

		// Token: 0x0600D05B RID: 53339 RVA: 0x002970E4 File Offset: 0x002952E4
		internal DataPartReferenceRelationship AddDataPartReferenceRelationship(DataPartReferenceRelationship dataPartReferenceRelationship)
		{
			if (dataPartReferenceRelationship == null)
			{
				throw new ArgumentNullException("dataPartReferenceRelationship");
			}
			DataPart dataPart = dataPartReferenceRelationship.DataPart;
			this.CreateRelationship(dataPart.Uri, TargetMode.Internal, dataPartReferenceRelationship.RelationshipType, dataPartReferenceRelationship.Id);
			this.ReferenceRelationshipList.AddLast(dataPartReferenceRelationship);
			return dataPartReferenceRelationship;
		}

		// Token: 0x1700325A RID: 12890
		// (get) Token: 0x0600D05C RID: 53340 RVA: 0x00297130 File Offset: 0x00295330
		public IEnumerable<IdPartPair> Parts
		{
			get
			{
				this.ThrowIfObjectDisposed();
				foreach (KeyValuePair<string, OpenXmlPart> item in this.PartDictionary)
				{
					KeyValuePair<string, OpenXmlPart> keyValuePair = item;
					string key = keyValuePair.Key;
					KeyValuePair<string, OpenXmlPart> keyValuePair2 = item;
					yield return new IdPartPair(key, keyValuePair2.Value);
				}
				yield break;
			}
		}

		// Token: 0x0600D05D RID: 53341 RVA: 0x00297150 File Offset: 0x00295350
		public OpenXmlPart GetPartById(string id)
		{
			this.ThrowIfObjectDisposed();
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			OpenXmlPart openXmlPart = null;
			if (this.PartDictionary.TryGetValue(id, out openXmlPart))
			{
				return openXmlPart;
			}
			throw new ArgumentOutOfRangeException("id");
		}

		// Token: 0x0600D05E RID: 53342 RVA: 0x00297190 File Offset: 0x00295390
		public string GetIdOfPart(OpenXmlPart part)
		{
			this.ThrowIfObjectDisposed();
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			if (this.PartDictionary.ContainsValue(part))
			{
				foreach (KeyValuePair<string, OpenXmlPart> keyValuePair in this.PartDictionary)
				{
					if (part == keyValuePair.Value)
					{
						return keyValuePair.Key;
					}
				}
			}
			throw new ArgumentOutOfRangeException("part");
		}

		// Token: 0x0600D05F RID: 53343 RVA: 0x00297220 File Offset: 0x00295420
		public string ChangeIdOfPart(OpenXmlPart part, string newRelationshipId)
		{
			this.ThrowIfObjectDisposed();
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			if (newRelationshipId == null)
			{
				throw new ArgumentNullException("newRelationshipId");
			}
			string text = null;
			foreach (KeyValuePair<string, OpenXmlPart> keyValuePair in this.ChildrenParts)
			{
				if (keyValuePair.Key == newRelationshipId)
				{
					throw new ArgumentException(ExceptionMessages.RelationshipIdConflict, "newRelationshipId");
				}
				if (keyValuePair.Value == part)
				{
					if (text != null)
					{
						throw new InvalidOperationException(ExceptionMessages.MultipleRelationshipsToSamePart);
					}
					text = keyValuePair.Key;
				}
			}
			if (text == null)
			{
				throw new ArgumentOutOfRangeException("part");
			}
			this.CreateRelationship(part.Uri, TargetMode.Internal, part.RelationshipType, newRelationshipId);
			this.ChildrenParts.Add(newRelationshipId, part);
			this.DeleteRelationship(text);
			this.ChildrenParts.Remove(text);
			return text;
		}

		// Token: 0x0600D060 RID: 53344 RVA: 0x00297310 File Offset: 0x00295510
		public virtual T AddPart<T>(T part) where T : OpenXmlPart
		{
			return (T)((object)this.AddPartFrom(part, null));
		}

		// Token: 0x0600D061 RID: 53345 RVA: 0x00297324 File Offset: 0x00295524
		public virtual T AddPart<T>(T part, string id) where T : OpenXmlPart
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			return (T)((object)this.AddPartFrom(part, id));
		}

		// Token: 0x0600D062 RID: 53346 RVA: 0x00297348 File Offset: 0x00295548
		public string CreateRelationshipToPart(OpenXmlPart targetPart)
		{
			if (targetPart == null)
			{
				throw new ArgumentNullException("targetPart");
			}
			if (!this.IsInSamePackage(targetPart))
			{
				throw new InvalidOperationException(ExceptionMessages.PartNotInSamePackage);
			}
			OpenXmlPart openXmlPart = this.AddPart<OpenXmlPart>(targetPart);
			return this.GetIdOfPart(openXmlPart);
		}

		// Token: 0x0600D063 RID: 53347 RVA: 0x00297388 File Offset: 0x00295588
		public string CreateRelationshipToPart(OpenXmlPart targetPart, string id)
		{
			if (targetPart == null)
			{
				throw new ArgumentNullException("targetPart");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (!this.IsInSamePackage(targetPart))
			{
				throw new InvalidOperationException(ExceptionMessages.PartNotInSamePackage);
			}
			this.AddPart<OpenXmlPart>(targetPart, id);
			return id;
		}

		// Token: 0x0600D064 RID: 53348 RVA: 0x002973C4 File Offset: 0x002955C4
		public T AddNewPart<T>() where T : OpenXmlPart, IFixedContentTypePart
		{
			return this.AddNewPartInternal<T>();
		}

		// Token: 0x0600D065 RID: 53349 RVA: 0x002973CC File Offset: 0x002955CC
		public T AddNewPart<T>(string id) where T : OpenXmlPart, IFixedContentTypePart
		{
			return this.AddNewPartInternal<T>(null, id);
		}

		// Token: 0x0600D066 RID: 53350 RVA: 0x002973D6 File Offset: 0x002955D6
		public virtual T AddNewPart<T>(string contentType, string id) where T : OpenXmlPart
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return this.AddNewPartInternal<T>(contentType, id);
		}

		// Token: 0x0600D067 RID: 53351 RVA: 0x002973EE File Offset: 0x002955EE
		public ExtendedPart AddExtendedPart(string relationshipType, string contentType, string targetExt)
		{
			return this.AddExtendedPart(relationshipType, contentType, targetExt, null);
		}

		// Token: 0x0600D068 RID: 53352 RVA: 0x002973FC File Offset: 0x002955FC
		public ExtendedPart AddExtendedPart(string relationshipType, string contentType, string targetExt, string rId)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (targetExt == null)
			{
				throw new ArgumentNullException("targetExt");
			}
			ExtendedPart extendedPart = new ExtendedPart(relationshipType);
			extendedPart.CreateInternal(this.InternalOpenXmlPackage, this.ThisOpenXmlPart, contentType, targetExt);
			string text = this.AttachChild(extendedPart, rId);
			this.ChildrenParts.Add(text, extendedPart);
			return extendedPart;
		}

		// Token: 0x0600D069 RID: 53353 RVA: 0x0029746C File Offset: 0x0029566C
		public bool DeletePart(string id)
		{
			this.ThrowIfObjectDisposed();
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			return this.DeletePartCore(id);
		}

		// Token: 0x0600D06A RID: 53354 RVA: 0x0029748C File Offset: 0x0029568C
		public bool DeletePart(OpenXmlPart part)
		{
			this.ThrowIfObjectDisposed();
			if (part == null)
			{
				return false;
			}
			if (part.OpenXmlPackage != this.InternalOpenXmlPackage || !this.PartDictionary.ContainsValue(part))
			{
				throw new InvalidOperationException(ExceptionMessages.ForeignOpenXmlPart);
			}
			string idOfPart = this.GetIdOfPart(part);
			return this.DeletePart(idOfPart);
		}

		// Token: 0x0600D06B RID: 53355 RVA: 0x002974DC File Offset: 0x002956DC
		public void DeleteParts<T>(IEnumerable<T> partsToBeDeleted) where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			if (partsToBeDeleted == null)
			{
				throw new ArgumentNullException("partsToBeDeleted");
			}
			StringCollection stringCollection = new StringCollection();
			foreach (T t in partsToBeDeleted)
			{
				OpenXmlPart openXmlPart = t;
				string idOfPart = this.GetIdOfPart(openXmlPart);
				stringCollection.Add(idOfPart);
			}
			foreach (string text in stringCollection)
			{
				this.DeletePart(text);
			}
		}

		// Token: 0x0600D06C RID: 53356 RVA: 0x00297598 File Offset: 0x00295798
		public int GetPartsCountOfType<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			int num = 0;
			foreach (KeyValuePair<string, OpenXmlPart> keyValuePair in this.ChildrenParts)
			{
				if (keyValuePair.Value is T)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600D06D RID: 53357 RVA: 0x002975FC File Offset: 0x002957FC
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this._annotations == null)
			{
				this._annotations = ((annotation is object[]) ? new object[] { annotation } : annotation);
				return;
			}
			object[] array = this._annotations as object[];
			if (array == null)
			{
				this._annotations = new object[] { this._annotations, annotation };
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this._annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x0600D06E RID: 53358 RVA: 0x00297698 File Offset: 0x00295898
		public T Annotation<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					return this._annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600D06F RID: 53359 RVA: 0x00297704 File Offset: 0x00295904
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						return this._annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600D070 RID: 53360 RVA: 0x0029776C File Offset: 0x0029596C
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] annotations = this._annotations as object[];
				if (annotations == null)
				{
					if (this._annotations is T)
					{
						yield return (T)((object)this._annotations);
					}
				}
				else
				{
					foreach (object obj in annotations)
					{
						if (obj == null)
						{
							break;
						}
						if (obj is T)
						{
							yield return (T)((object)obj);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600D071 RID: 53361 RVA: 0x0029778C File Offset: 0x0029598C
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] annotations = this._annotations as object[];
				if (annotations == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						yield return this._annotations;
					}
				}
				else
				{
					foreach (object obj in annotations)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							yield return obj;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600D072 RID: 53362 RVA: 0x002977B0 File Offset: 0x002959B0
		public void RemoveAnnotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (this._annotations is T)
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x0600D073 RID: 53363 RVA: 0x0029782C File Offset: 0x00295A2C
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x0600D074 RID: 53364 RVA: 0x002978B8 File Offset: 0x00295AB8
		public IEnumerable<T> GetPartsOfType<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			foreach (OpenXmlPart part in this.PartDictionary.Values)
			{
				if (part is T)
				{
					yield return (T)((object)part);
				}
			}
			yield break;
		}

		// Token: 0x0600D075 RID: 53365 RVA: 0x002978D8 File Offset: 0x00295AD8
		public void GetPartsOfType<T>(ICollection<T> partCollection) where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			if (partCollection == null)
			{
				throw new ArgumentNullException("partCollection");
			}
			partCollection.Clear();
			foreach (T t in this.GetPartsOfType<T>())
			{
				partCollection.Add(t);
			}
		}

		// Token: 0x0600D076 RID: 53366 RVA: 0x00297940 File Offset: 0x00295B40
		internal T AddNewPartInternal<T>() where T : OpenXmlPart, IFixedContentTypePart
		{
			this.ThrowIfObjectDisposed();
			T t = (T)((object)Activator.CreateInstance(typeof(T), true));
			try
			{
				this.InitPart<T>(t, t.ContentType);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new OpenXmlPackageException(ExceptionMessages.AddedPartIsNotAllowed);
			}
			catch (InvalidOperationException)
			{
				throw new OpenXmlPackageException(ExceptionMessages.OnlyOnePartAllowed);
			}
			return t;
		}

		// Token: 0x0600D077 RID: 53367 RVA: 0x002979B4 File Offset: 0x00295BB4
		internal T AddNewPartInternal<T>(string contentType, string id) where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			if (id != null)
			{
				if (id == string.Empty)
				{
					throw new ArgumentException(ExceptionMessages.StringArgumentEmptyException, "id");
				}
				try
				{
					XmlConvert.VerifyNCName(id);
				}
				catch (XmlException)
				{
					throw new ArgumentException(ExceptionMessages.InvalidXmlIDStringException, "id");
				}
				if (this.ChildrenParts.ContainsKey(id))
				{
					throw new ArgumentException(ExceptionMessages.RelationshipIdConflict, "id");
				}
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(ExceptionMessages.StringArgumentEmptyException, "contentType");
			}
			T t = (T)((object)Activator.CreateInstance(typeof(T), true));
			if (t is ExtendedPart)
			{
				throw new ArgumentOutOfRangeException("T", ExceptionMessages.ExtendedPartNotAllowed);
			}
			if (contentType != null && t.IsContentTypeFixed && t.ContentType != contentType)
			{
				throw new ArgumentOutOfRangeException("contentType", ExceptionMessages.ErrorContentType);
			}
			if (contentType == null)
			{
				contentType = t.ContentType;
			}
			try
			{
				this.InitPart<T>(t, contentType, id);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new OpenXmlPackageException(ExceptionMessages.AddedPartIsNotAllowed);
			}
			catch (InvalidOperationException)
			{
				throw new OpenXmlPackageException(ExceptionMessages.OnlyOnePartAllowed);
			}
			return t;
		}

		// Token: 0x0600D078 RID: 53368 RVA: 0x00297B04 File Offset: 0x00295D04
		internal void InitPart<T>(T newPart, string contentType) where T : OpenXmlPart
		{
			this.InitPart<T>(newPart, contentType, null);
		}

		// Token: 0x0600D079 RID: 53369 RVA: 0x00297B10 File Offset: 0x00295D10
		internal virtual void InitPart<T>(T newPart, string contentType, string id) where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(ExceptionMessages.StringArgumentEmptyException);
			}
			PartConstraintRule partConstraintRule;
			if (!this.GetPartConstraint().TryGetValue(newPart.RelationshipType, out partConstraintRule))
			{
				throw new ArgumentOutOfRangeException("newPart");
			}
			if (!partConstraintRule.MaxOccursGreatThanOne && this.GetSubPartOfType<T>() != null)
			{
				throw new OpenXmlPackageException(ExceptionMessages.OnlyOnePartAllowed);
			}
			if (partConstraintRule.PartContentType != null && contentType != partConstraintRule.PartContentType)
			{
				throw new ArgumentOutOfRangeException("newPart");
			}
			newPart.CreateInternal(this.InternalOpenXmlPackage, this.ThisOpenXmlPart, contentType, null);
			string text = this.AttachChild(newPart, id);
			this.ChildrenParts.Add(text, newPart);
		}

		// Token: 0x0600D07A RID: 53370 RVA: 0x00297BEC File Offset: 0x00295DEC
		internal virtual OpenXmlPart AddPartFrom(OpenXmlPart subPart, string rId)
		{
			this.ThrowIfObjectDisposed();
			if (subPart == null)
			{
				throw new ArgumentNullException("subPart");
			}
			PartConstraintRule partConstraintRule;
			if (subPart.OpenXmlPackage == this.InternalOpenXmlPackage && this.IsChildPart(subPart))
			{
				if (rId != null && rId != this.GetIdOfPart(subPart))
				{
					throw new InvalidOperationException(ExceptionMessages.PartExistsWithDifferentRelationshipId);
				}
				return subPart;
			}
			else if (!this.GetPartConstraint().TryGetValue(subPart.RelationshipType, out partConstraintRule))
			{
				if (subPart is ExtendedPart)
				{
					return this.AddSubPart(subPart, rId);
				}
				throw new InvalidOperationException(ExceptionMessages.AddedPartIsNotAllowed);
			}
			else
			{
				if (partConstraintRule.PartContentType != null && subPart.ContentType != partConstraintRule.PartContentType)
				{
					throw new InvalidOperationException(ExceptionMessages.AddedPartIsNotAllowed);
				}
				if (partConstraintRule.MaxOccursGreatThanOne)
				{
					return this.AddSubPart(subPart, rId);
				}
				OpenXmlPart subPart2 = this.GetSubPart(subPart.RelationshipType);
				if (subPart2 != null)
				{
					throw new OpenXmlPackageException(ExceptionMessages.OnlyOnePartAllowed);
				}
				return this.SetSubPart(subPart, rId);
			}
		}

		// Token: 0x0600D07B RID: 53371 RVA: 0x00297CCE File Offset: 0x00295ECE
		internal OpenXmlPart SetSubPart(OpenXmlPart part, string rId)
		{
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			return this.AddSubPart(part, rId);
		}

		// Token: 0x0600D07C RID: 53372 RVA: 0x00297CE8 File Offset: 0x00295EE8
		internal OpenXmlPart AddSubPart(OpenXmlPart part, string rId)
		{
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			if (part.OpenXmlPackage == this.InternalOpenXmlPackage)
			{
				string text = this.AttachChild(part, rId);
				this.ChildrenParts.Add(text, part);
				return part;
			}
			return this.AddSubPartFromOtherPackage(part, false, rId);
		}

		// Token: 0x0600D07D RID: 53373 RVA: 0x00297D34 File Offset: 0x00295F34
		internal OpenXmlPart AddSubPartFromOtherPackage(OpenXmlPart part, bool keepIdAndUri, string rId)
		{
			Dictionary<OpenXmlPart, OpenXmlPart> dictionary = new Dictionary<OpenXmlPart, OpenXmlPart>();
			Dictionary<DataPart, DataPart> dictionary2 = new Dictionary<DataPart, DataPart>();
			return this.AddSubPartFromOtherPackage(part, dictionary, dictionary2, keepIdAndUri, rId);
		}

		// Token: 0x0600D07E RID: 53374 RVA: 0x00297D58 File Offset: 0x00295F58
		private OpenXmlPart AddSubPartFromOtherPackage(OpenXmlPart part, IDictionary<OpenXmlPart, OpenXmlPart> partDictionary, IDictionary<DataPart, DataPart> dataPartsDictionary, bool keepIdAndUri, string rId)
		{
			if (keepIdAndUri && rId == null)
			{
				throw new ArgumentNullException("rId");
			}
			OpenXmlPart openXmlPart;
			if (partDictionary.TryGetValue(part, out openXmlPart))
			{
				string text = this.AttachChild(openXmlPart, rId);
				this.ChildrenParts.Add(text, openXmlPart);
				return openXmlPart;
			}
			openXmlPart = this.CreateOpenXmlPart(part.RelationshipType);
			openXmlPart.CreateInternal2(this.InternalOpenXmlPackage, this.ThisOpenXmlPart, part.ContentType, part.Uri);
			using (Stream stream = part.GetStream())
			{
				openXmlPart.FeedData(stream);
			}
			string text2 = this.AttachChild(openXmlPart, rId);
			this.ChildrenParts.Add(text2, openXmlPart);
			partDictionary.Add(part, openXmlPart);
			foreach (IdPartPair idPartPair in part.Parts)
			{
				openXmlPart.AddSubPartFromOtherPackage(idPartPair.OpenXmlPart, partDictionary, dataPartsDictionary, true, idPartPair.RelationshipId);
			}
			foreach (ExternalRelationship externalRelationship in part.ExternalRelationships)
			{
				openXmlPart.AddExternalRelationship(externalRelationship.RelationshipType, externalRelationship.Uri, externalRelationship.Id);
			}
			foreach (HyperlinkRelationship hyperlinkRelationship in part.HyperlinkRelationships)
			{
				openXmlPart.AddHyperlinkRelationship(hyperlinkRelationship.Uri, hyperlinkRelationship.IsExternal, hyperlinkRelationship.Id);
			}
			foreach (DataPartReferenceRelationship dataPartReferenceRelationship in part.DataPartReferenceRelationships)
			{
				if (!dataPartsDictionary.ContainsKey(dataPartReferenceRelationship.DataPart))
				{
					dataPartsDictionary.Add(dataPartReferenceRelationship.DataPart, null);
				}
			}
			foreach (KeyValuePair<DataPart, DataPart> keyValuePair in dataPartsDictionary)
			{
				if (keyValuePair.Value == null)
				{
					DataPart key = keyValuePair.Key;
					MediaDataPart mediaDataPart = new MediaDataPart();
					mediaDataPart.CreateInternal2(this.InternalOpenXmlPackage, key.ContentType, key.Uri);
					using (Stream stream2 = key.GetStream())
					{
						mediaDataPart.FeedData(stream2);
					}
					this.InternalOpenXmlPackage.AddDataPartToList(mediaDataPart);
					dataPartsDictionary[key] = mediaDataPart;
				}
			}
			foreach (DataPartReferenceRelationship dataPartReferenceRelationship2 in part.DataPartReferenceRelationships)
			{
				MediaDataPart mediaDataPart2 = (MediaDataPart)dataPartsDictionary[dataPartReferenceRelationship2.DataPart];
				DataPartReferenceRelationship dataPartReferenceRelationship3 = DataPartReferenceRelationship.CreateDataPartReferenceRelationship(this, mediaDataPart2, dataPartReferenceRelationship2.RelationshipType, dataPartReferenceRelationship2.Id);
				this.ReferenceRelationshipList.AddLast(dataPartReferenceRelationship3);
			}
			return openXmlPart;
		}

		// Token: 0x0600D07F RID: 53375 RVA: 0x00298090 File Offset: 0x00296290
		internal string AttachChild(OpenXmlPart part)
		{
			return this.AttachChild(part, null);
		}

		// Token: 0x0600D080 RID: 53376 RVA: 0x0029809C File Offset: 0x0029629C
		internal string AttachChild(OpenXmlPart part, string rId)
		{
			if (rId == null)
			{
				PackageRelationship packageRelationship = this.CreateRelationship(part.Uri, TargetMode.Internal, part.RelationshipType);
				return packageRelationship.Id;
			}
			PackageRelationship packageRelationship2 = this.CreateRelationship(part.Uri, TargetMode.Internal, part.RelationshipType, rId);
			return packageRelationship2.Id;
		}

		// Token: 0x0600D081 RID: 53377 RVA: 0x002980E4 File Offset: 0x002962E4
		internal bool DeletePartCore(string id)
		{
			Dictionary<OpenXmlPart, bool> dictionary = new Dictionary<OpenXmlPart, bool>();
			Dictionary<OpenXmlPart, bool> dictionary2 = new Dictionary<OpenXmlPart, bool>();
			OpenXmlPart partById = this.GetPartById(id);
			if (partById == null)
			{
				return false;
			}
			partById.FindAllReachableParts(dictionary2);
			this.ChildrenParts.Remove(id);
			this.InternalOpenXmlPackage.FindAllReachableParts(dictionary);
			Dictionary<OpenXmlPart, bool> dictionary3 = new Dictionary<OpenXmlPart, bool>();
			foreach (OpenXmlPart openXmlPart in dictionary2.Keys)
			{
				if (!dictionary.ContainsKey(openXmlPart))
				{
					dictionary3.Add(openXmlPart, false);
				}
			}
			dictionary2[partById] = true;
			if (dictionary3.Count == 0)
			{
				this.DeleteRelationship(id);
			}
			else
			{
				partById.DeleteAllParts(dictionary2, dictionary3);
				this.DeleteRelationship(id);
				bool flag;
				if (dictionary3.TryGetValue(partById, out flag) && !flag)
				{
					partById.Destroy();
					dictionary3[partById] = true;
				}
			}
			return true;
		}

		// Token: 0x0600D082 RID: 53378 RVA: 0x002981CC File Offset: 0x002963CC
		internal void DeletePartsOfType<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			StringCollection stringCollection = new StringCollection();
			foreach (KeyValuePair<string, OpenXmlPart> keyValuePair in this.ChildrenParts)
			{
				if (keyValuePair.Value is T)
				{
					stringCollection.Add(keyValuePair.Key);
				}
			}
			foreach (string text in stringCollection)
			{
				this.DeletePart(text);
			}
		}

		// Token: 0x0600D083 RID: 53379 RVA: 0x00298280 File Offset: 0x00296480
		internal void DeletePartsRecursivelyOfTypeBase<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			this.DeletePartsOfType<T>();
			foreach (OpenXmlPart openXmlPart in this.ChildrenParts.Values)
			{
				openXmlPart.DeletePartsRecursivelyOfTypeBase<T>();
			}
		}

		// Token: 0x0600D084 RID: 53380 RVA: 0x002982E0 File Offset: 0x002964E0
		internal void DeleteAllParts(Dictionary<OpenXmlPart, bool> processedParts, Dictionary<OpenXmlPart, bool> toBeDeletedParts)
		{
			this.ThrowIfObjectDisposed();
			if (this.PartDictionary.Count > 0)
			{
				Collection<OpenXmlPart> collection = new Collection<OpenXmlPart>();
				foreach (KeyValuePair<string, OpenXmlPart> keyValuePair in this.ChildrenParts)
				{
					if (!processedParts[keyValuePair.Value])
					{
						processedParts[keyValuePair.Value] = true;
						bool flag;
						if (toBeDeletedParts.TryGetValue(keyValuePair.Value, out flag) && !flag)
						{
							keyValuePair.Value.DeleteAllParts(processedParts, toBeDeletedParts);
							collection.Add(keyValuePair.Value);
						}
					}
					this.DeleteRelationship(keyValuePair.Key);
				}
				this.ChildrenParts.Clear();
				foreach (OpenXmlPart openXmlPart in collection)
				{
					openXmlPart.Destroy();
					toBeDeletedParts[openXmlPart] = true;
				}
			}
		}

		// Token: 0x0600D085 RID: 53381 RVA: 0x002983F0 File Offset: 0x002965F0
		internal OpenXmlPart GetSubPart(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			foreach (OpenXmlPart openXmlPart in this.PartDictionary.Values)
			{
				if (openXmlPart.RelationshipType == relationshipType)
				{
					return openXmlPart;
				}
			}
			return null;
		}

		// Token: 0x0600D086 RID: 53382 RVA: 0x0029846C File Offset: 0x0029666C
		internal T GetSubPartOfType<T>() where T : OpenXmlPart
		{
			this.ThrowIfObjectDisposed();
			using (IEnumerator<T> enumerator = this.GetPartsOfType<T>().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return default(T);
		}

		// Token: 0x0600D087 RID: 53383 RVA: 0x002984C8 File Offset: 0x002966C8
		internal bool IsChildPart(OpenXmlPart part)
		{
			this.ThrowIfObjectDisposed();
			if (part == null)
			{
				throw new ArgumentNullException("part");
			}
			if (part.OpenXmlPackage != this.InternalOpenXmlPackage)
			{
				throw new ArgumentOutOfRangeException("part");
			}
			return this.PartDictionary.ContainsValue(part);
		}

		// Token: 0x0600D088 RID: 53384 RVA: 0x00298504 File Offset: 0x00296704
		internal OpenXmlPart CreateOpenXmlPart(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			OpenXmlPart openXmlPart;
			if (this.GetPartConstraint().ContainsKey(relationshipType))
			{
				openXmlPart = this.CreatePartCore(relationshipType);
			}
			else
			{
				openXmlPart = GlobalPartFactory.CreateOpenXmlPart(this.InternalOpenXmlPackage, relationshipType);
			}
			return openXmlPart;
		}

		// Token: 0x0600D089 RID: 53385 RVA: 0x00298550 File Offset: 0x00296750
		internal void LoadReferencedPartsAndRelationships(OpenXmlPackage openXmlPackage, OpenXmlPart sourcePart, PackageRelationshipCollection relationshipCollection, Dictionary<Uri, OpenXmlPart> loadedParts)
		{
			foreach (PackageRelationship packageRelationship in relationshipCollection)
			{
				if (packageRelationship.RelationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink")
				{
					this.ReferenceRelationshipList.AddLast(new HyperlinkRelationship(packageRelationship.TargetUri, packageRelationship.TargetMode == TargetMode.External, packageRelationship.Id)
					{
						Container = this
					});
				}
				else if (packageRelationship.TargetMode == TargetMode.Internal)
				{
					if (!packageRelationship.TargetUri.ToString().Equals("NULL", StringComparison.OrdinalIgnoreCase))
					{
						Uri uri = ((sourcePart == null) ? new Uri("/", UriKind.Relative) : sourcePart.Uri);
						Uri uri2 = PackUriHelper.ResolvePartUri(uri, packageRelationship.TargetUri);
						OpenXmlPart openXmlPart;
						if (loadedParts.TryGetValue(uri2, out openXmlPart))
						{
							if (openXmlPart.RelationshipType != packageRelationship.RelationshipType)
							{
								throw new OpenXmlPackageException(ExceptionMessages.SamePartWithDifferentRelationshipType);
							}
							this.ChildrenParts.Add(packageRelationship.Id, openXmlPart);
						}
						else if (DataPartReferenceRelationship.IsDataPartReferenceRelationship(packageRelationship.RelationshipType))
						{
							DataPart dataPart;
							if ((dataPart = openXmlPackage.FindDataPart(uri2)) == null)
							{
								dataPart = new MediaDataPart();
								PackagePart part = openXmlPackage.Package.GetPart(uri2);
								dataPart.Load(openXmlPackage, part);
								openXmlPackage.AddDataPartToList(dataPart);
							}
							DataPartReferenceRelationship dataPartReferenceRelationship = DataPartReferenceRelationship.CreateDataPartReferenceRelationship(this, dataPart, packageRelationship.RelationshipType, packageRelationship.Id);
							this.ReferenceRelationshipList.AddLast(dataPartReferenceRelationship);
						}
						else
						{
							openXmlPart = this.CreateOpenXmlPart(packageRelationship.RelationshipType);
							openXmlPart.MCSettings = openXmlPackage.MarkupCompatibilityProcessSettings;
							loadedParts.Add(uri2, openXmlPart);
							openXmlPart.Load(openXmlPackage, sourcePart, uri2, packageRelationship.Id, loadedParts);
							this.ChildrenParts.Add(packageRelationship.Id, openXmlPart);
						}
					}
				}
				else
				{
					this.ReferenceRelationshipList.AddLast(new ExternalRelationship(packageRelationship.TargetUri, packageRelationship.RelationshipType, packageRelationship.Id)
					{
						Container = this
					});
				}
			}
		}

		// Token: 0x0600D08A RID: 53386 RVA: 0x00298764 File Offset: 0x00296964
		internal void ValidateInternal(OpenXmlPackageValidationSettings validationSettings, Dictionary<OpenXmlPart, bool> processedParts)
		{
			EventHandler<OpenXmlPackageValidationEventArgs> eventHandler = validationSettings.GetEventHandler();
			this.ValidateDataPartReferenceRelationships(validationSettings);
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (OpenXmlPart openXmlPart in this.ChildrenParts.Values)
			{
				int num = 0;
				dictionary.TryGetValue(openXmlPart.RelationshipType, out num);
				dictionary[openXmlPart.RelationshipType] = num + 1;
				if (!(this is ExtendedPart) && !this.GetPartConstraint().Keys.Contains(openXmlPart.RelationshipType) && openXmlPart.IsInVersion(validationSettings.FileFormat))
				{
					eventHandler(this, new OpenXmlPackageValidationEventArgs
					{
						MessageId = "PartIsNotAllowed",
						PartClassName = openXmlPart.RelationshipType,
						Part = this.ThisOpenXmlPart,
						SubPart = openXmlPart
					});
				}
			}
			foreach (KeyValuePair<string, PartConstraintRule> keyValuePair in this.GetPartConstraint())
			{
				string key = keyValuePair.Key;
				PartConstraintRule value = keyValuePair.Value;
				if (value.MinOccursIsNonZero && value.FileFormat.Includes(validationSettings.FileFormat) && this.GetSubPart(key) == null)
				{
					eventHandler(this, new OpenXmlPackageValidationEventArgs
					{
						MessageId = "RequiredPartDoNotExist",
						PartClassName = value.PartClassName,
						Part = this.ThisOpenXmlPart
					});
				}
				if (!value.MaxOccursGreatThanOne && value.FileFormat.Includes(validationSettings.FileFormat))
				{
					int num2 = 0;
					if (dictionary.TryGetValue(key, out num2) && num2 > 1)
					{
						eventHandler(this, new OpenXmlPackageValidationEventArgs
						{
							MessageId = "OnlyOnePartAllowed",
							PartClassName = value.PartClassName,
							Part = this.ThisOpenXmlPart
						});
					}
				}
			}
			foreach (OpenXmlPart openXmlPart2 in this.ChildrenParts.Values)
			{
				if (!processedParts.ContainsKey(openXmlPart2))
				{
					if (!(openXmlPart2 is ExtendedPart))
					{
						PartConstraintRule partConstraintRule = null;
						if (this.GetPartConstraint().TryGetValue(openXmlPart2.RelationshipType, out partConstraintRule) && partConstraintRule.FileFormat.Includes(validationSettings.FileFormat) && partConstraintRule.PartContentType != null && openXmlPart2.ContentType != partConstraintRule.PartContentType)
						{
							OpenXmlPackageValidationEventArgs openXmlPackageValidationEventArgs = new OpenXmlPackageValidationEventArgs();
							string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.InvalidContentTypePart, new object[] { partConstraintRule.PartContentType });
							openXmlPackageValidationEventArgs.Message = text;
							openXmlPackageValidationEventArgs.MessageId = "InvalidContentTypePart";
							openXmlPackageValidationEventArgs.SubPart = openXmlPart2;
							openXmlPackageValidationEventArgs.Part = this.ThisOpenXmlPart;
							eventHandler(this, openXmlPackageValidationEventArgs);
						}
					}
					processedParts.Add(openXmlPart2, true);
					openXmlPart2.ValidateInternal(validationSettings, processedParts);
				}
			}
		}

		// Token: 0x0600D08B RID: 53387 RVA: 0x00298A94 File Offset: 0x00296C94
		internal void ValidateDataPartReferenceRelationships(OpenXmlPackageValidationSettings validationSettings)
		{
			EventHandler<OpenXmlPackageValidationEventArgs> eventHandler = validationSettings.GetEventHandler();
			foreach (DataPartReferenceRelationship dataPartReferenceRelationship in this.DataPartReferenceRelationships)
			{
				PartConstraintRule partConstraintRule;
				if (!this.GetDataPartReferenceConstraint().TryGetValue(dataPartReferenceRelationship.RelationshipType, out partConstraintRule))
				{
					eventHandler(this, new OpenXmlPackageValidationEventArgs
					{
						MessageId = "DataPartReferenceIsNotAllowed",
						PartClassName = dataPartReferenceRelationship.RelationshipType,
						Part = this.ThisOpenXmlPart,
						SubPart = null,
						DataPartReferenceRelationship = dataPartReferenceRelationship
					});
				}
			}
		}

		// Token: 0x1700325B RID: 12891
		// (get) Token: 0x0600D08C RID: 53388
		internal abstract OpenXmlPackage InternalOpenXmlPackage { get; }

		// Token: 0x1700325C RID: 12892
		// (get) Token: 0x0600D08D RID: 53389
		internal abstract OpenXmlPart ThisOpenXmlPart { get; }

		// Token: 0x0600D08E RID: 53390
		internal abstract IDictionary<string, PartConstraintRule> GetPartConstraint();

		// Token: 0x0600D08F RID: 53391
		internal abstract IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint();

		// Token: 0x0600D090 RID: 53392
		protected abstract void ThrowIfObjectDisposed();

		// Token: 0x0600D091 RID: 53393 RVA: 0x00298B3C File Offset: 0x00296D3C
		internal virtual OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			return new ExtendedPart(relationshipType);
		}

		// Token: 0x0600D092 RID: 53394
		internal abstract OpenXmlPart NewPart(string relationshipType, string contentType);

		// Token: 0x0600D093 RID: 53395
		internal abstract void DeleteRelationship(string id);

		// Token: 0x0600D094 RID: 53396
		internal abstract PackageRelationship CreateRelationship(Uri targetUri, TargetMode targetMode, string relationshipType);

		// Token: 0x0600D095 RID: 53397
		internal abstract PackageRelationship CreateRelationship(Uri targetUri, TargetMode targetMode, string relationshipType, string id);

		// Token: 0x0600D096 RID: 53398
		internal abstract void FindAllReachableParts(IDictionary<OpenXmlPart, bool> reachableParts);

		// Token: 0x0600D097 RID: 53399 RVA: 0x00298B4A File Offset: 0x00296D4A
		private bool IsInSamePackage(OpenXmlPart targetPart)
		{
			return this.InternalOpenXmlPackage != null && targetPart.OpenXmlPackage != null && targetPart.OpenXmlPackage == this.InternalOpenXmlPackage;
		}

		// Token: 0x040068EB RID: 26859
		private Dictionary<string, OpenXmlPart> _childrenPartsDictionary = new Dictionary<string, OpenXmlPart>();

		// Token: 0x040068EC RID: 26860
		private LinkedList<ReferenceRelationship> _referenceRelationships = new LinkedList<ReferenceRelationship>();

		// Token: 0x040068ED RID: 26861
		private object _annotations;
	}
}
