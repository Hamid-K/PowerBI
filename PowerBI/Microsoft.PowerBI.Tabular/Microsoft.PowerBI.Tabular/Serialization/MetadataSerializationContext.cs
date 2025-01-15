using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000167 RID: 359
	public abstract class MetadataSerializationContext : MetadataSerializationManagerBase, IEnumerable<MetadataDocument>, IEnumerable
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x000947EF File Offset: 0x000929EF
		private protected MetadataSerializationContext(MetadataSerializationStyle style)
		{
			this.Style = style;
			this.documents = new Dictionary<string, MetadataDocument>(StringComparer.InvariantCultureIgnoreCase);
			this.keys = new MetadataSerializationContext.DocumentKeys(this.documents);
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0009481F File Offset: 0x00092A1F
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x00094827 File Offset: 0x00092A27
		public IMetadataSerializationHost Host { get; set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00094830 File Offset: 0x00092A30
		public MetadataSerializationStyle Style { get; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00094838 File Offset: 0x00092A38
		public IReadOnlyCollection<string> Documents
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x170005DE RID: 1502
		public MetadataDocument this[string logicalPath]
		{
			get
			{
				if (string.IsNullOrWhiteSpace(logicalPath))
				{
					throw new ArgumentNullException("logicalPath");
				}
				MetadataDocument metadataDocument;
				if (!this.documents.TryGetValue(logicalPath, out metadataDocument))
				{
					throw new ArgumentException(TomSR.Exception_MissingDocInContext(logicalPath), "logicalPath");
				}
				return metadataDocument;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00094882 File Offset: 0x00092A82
		public static MetadataSerializationContext Create(MetadataSerializationStyle style)
		{
			if (style <= MetadataSerializationStyle.Tmdl)
			{
				return new MetadataSerializationContext.TmdlSerializationContext();
			}
			if (style != MetadataSerializationStyle.Json)
			{
				throw new ArgumentException(TomSR.Exception_InvalidContentStyle(style.ToString("G")), "style");
			}
			throw new NotSupportedException(TomSR.Exception_JsonSerializationSupport);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000948BE File Offset: 0x00092ABE
		public static MetadataSerializationContext Create(MetadataSerializationStyle style, Model model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			MetadataSerializationContext metadataSerializationContext = MetadataSerializationContext.Create(style);
			metadataSerializationContext.LoadModel(null, model, null);
			return metadataSerializationContext;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000948DD File Offset: 0x00092ADD
		public static MetadataSerializationContext Create(MetadataSerializationStyle style, Model model, MetadataSerializationOptions options)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			MetadataSerializationContext metadataSerializationContext = MetadataSerializationContext.Create(style);
			metadataSerializationContext.LoadModel(options, model, null);
			return metadataSerializationContext;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0009490A File Offset: 0x00092B0A
		public static MetadataSerializationContext Create(MetadataSerializationStyle style, Database db)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			MetadataSerializationContext metadataSerializationContext = MetadataSerializationContext.Create(style);
			metadataSerializationContext.LoadDatabase(null, db, null);
			return metadataSerializationContext;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00094929 File Offset: 0x00092B29
		public static MetadataSerializationContext Create(MetadataSerializationStyle style, Database db, MetadataSerializationOptions options)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			MetadataSerializationContext metadataSerializationContext = MetadataSerializationContext.Create(style);
			metadataSerializationContext.LoadDatabase(options, db, null);
			return metadataSerializationContext;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00094956 File Offset: 0x00092B56
		public MetadataDocument ReadFromDocument(Stream document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (!document.CanRead)
			{
				throw new ArgumentException(TomSR.Exception_InvalidStreamNoRead, "document");
			}
			return this.ReadFromDocumentImpl(this.GetNewLogicalPath(), document);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0009498C File Offset: 0x00092B8C
		public MetadataDocument ReadFromDocument(string logicalPath, Stream document)
		{
			if (string.IsNullOrWhiteSpace(logicalPath))
			{
				throw new ArgumentNullException("logicalPath");
			}
			if (this.documents.ContainsKey(logicalPath))
			{
				throw new ArgumentException(TomSR.Exception_DuplicateDocInContext(logicalPath), "logicalPath");
			}
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (!document.CanRead)
			{
				throw new ArgumentException(TomSR.Exception_InvalidStreamNoRead, "document");
			}
			return this.ReadFromDocumentImpl(logicalPath, document);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000949F9 File Offset: 0x00092BF9
		public MetadataDocument ReadFromDocument(TextReader reader, Encoding encoding = null)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return this.ReadFromDocumentImpl(this.GetNewLogicalPath(), reader, encoding);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00094A18 File Offset: 0x00092C18
		public MetadataDocument ReadFromDocument(string logicalPath, TextReader reader, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(logicalPath))
			{
				throw new ArgumentNullException("logicalPath");
			}
			if (this.documents.ContainsKey(logicalPath))
			{
				throw new ArgumentException(TomSR.Exception_DuplicateDocInContext(logicalPath), "logicalPath");
			}
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return this.ReadFromDocumentImpl(logicalPath, reader, encoding);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00094A70 File Offset: 0x00092C70
		public void WriteToDocument(string logicalPath, Stream document)
		{
			if (string.IsNullOrWhiteSpace(logicalPath))
			{
				throw new ArgumentNullException("logicalPath");
			}
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (!document.CanWrite)
			{
				throw new ArgumentException(TomSR.Exception_InvalidStreamNoWrite, "document");
			}
			MetadataDocument metadataDocument;
			if (!this.documents.TryGetValue(logicalPath, out metadataDocument))
			{
				throw new ArgumentException(TomSR.Exception_MissingDocInContext(logicalPath), "logicalPath");
			}
			metadataDocument.SaveImpl(document);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00094AE0 File Offset: 0x00092CE0
		public void WriteToDocument(string logicalPath, TextWriter writer, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(logicalPath))
			{
				throw new ArgumentNullException("logicalPath");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			MetadataDocument metadataDocument;
			if (!this.documents.TryGetValue(logicalPath, out metadataDocument))
			{
				throw new ArgumentException(TomSR.Exception_MissingDocInContext(logicalPath), "logicalPath");
			}
			metadataDocument.SaveImpl(writer, encoding);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00094B37 File Offset: 0x00092D37
		public bool RemoveDocument(string logicalPath)
		{
			if (string.IsNullOrWhiteSpace(logicalPath))
			{
				throw new ArgumentNullException("logicalPath");
			}
			return this.documents.Remove(logicalPath);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00094B58 File Offset: 0x00092D58
		public void Clear()
		{
			this.documents.Clear();
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00094B65 File Offset: 0x00092D65
		public void LoadFromModel(Model model, object context = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.documents.Clear();
			this.LoadModel(null, model, context);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00094B89 File Offset: 0x00092D89
		public void LoadFromModel(Model model, MetadataSerializationOptions options, object context = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.documents.Clear();
			this.LoadModel(options, model, context);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00094BBB File Offset: 0x00092DBB
		public void LoadFromDatabase(Database db, object context = null)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			this.documents.Clear();
			this.LoadDatabase(null, db, context);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00094BDF File Offset: 0x00092DDF
		public void LoadFromDatabase(Database db, MetadataSerializationOptions options, object context = null)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.documents.Clear();
			this.LoadDatabase(options, db, context);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00094C11 File Offset: 0x00092E11
		public void UpdateModel(Model model, object context = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (this.documents.Count == 0)
			{
				throw new InvalidOperationException(TomSR.Exception_NoDocsInContext);
			}
			this.UpdateModelImpl(null, model, context);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00094C42 File Offset: 0x00092E42
		public void UpdateModel(Model model, MetadataDeserializationOptions options, object context = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (this.documents.Count == 0)
			{
				throw new InvalidOperationException(TomSR.Exception_NoDocsInContext);
			}
			this.UpdateModelImpl(options, model, context);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00094C81 File Offset: 0x00092E81
		public Model ToModel(object context = null)
		{
			if (this.documents.Count == 0)
			{
				throw new InvalidOperationException(TomSR.Exception_NoDocsInContext);
			}
			return this.CreateModel(null, context);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00094CA3 File Offset: 0x00092EA3
		public Model ToModel(MetadataDeserializationOptions options, object context = null)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (this.documents.Count == 0)
			{
				throw new InvalidOperationException(TomSR.Exception_NoDocsInContext);
			}
			return this.CreateModel(options, context);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00094CD3 File Offset: 0x00092ED3
		public Database ToDatabase(object context = null)
		{
			if (this.documents.Count == 0)
			{
				throw new InvalidOperationException(TomSR.Exception_NoDocsInContext);
			}
			return this.CreateDatabase(null, context);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00094CF5 File Offset: 0x00092EF5
		public Database ToDatabase(MetadataDeserializationOptions options, object context = null)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (this.documents.Count == 0)
			{
				throw new InvalidOperationException(TomSR.Exception_NoDocsInContext);
			}
			return this.CreateDatabase(options, context);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00094D25 File Offset: 0x00092F25
		public IEnumerator<MetadataDocument> GetEnumerator()
		{
			return this.documents.Values.GetEnumerator();
		}

		// Token: 0x06001675 RID: 5749
		private protected abstract void LoadModel(MetadataSerializationOptions options, Model model, object context);

		// Token: 0x06001676 RID: 5750
		private protected abstract void LoadDatabase(MetadataSerializationOptions options, Database db, object context);

		// Token: 0x06001677 RID: 5751
		private protected abstract IEnumerable<KeyValuePair<ObjectPath, MetadataObject>> ParseDocuments(SerializationActivityContext activityContext, object context);

		// Token: 0x06001678 RID: 5752
		private protected abstract Exception CreateModelUpdateByObjectException(SerializationActivityContext context, MetadataObject @object, string error, Exception e = null);

		// Token: 0x06001679 RID: 5753
		private protected abstract Model CreateModel(MetadataDeserializationOptions options, object context);

		// Token: 0x0600167A RID: 5754
		private protected abstract Database CreateDatabase(MetadataDeserializationOptions options, object context);

		// Token: 0x0600167B RID: 5755 RVA: 0x00094D3C File Offset: 0x00092F3C
		private protected sealed override void OperationStartNotification(bool isSerializing, object context, IReadOnlyCollection<string> logicalPaths)
		{
			if (this.Host != null)
			{
				this.Host.OperationStartNotification(isSerializing, context, logicalPaths);
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00094D54 File Offset: 0x00092F54
		private protected sealed override void DocumentStartNotification(bool isSerializing, object context, string logicalPath)
		{
			if (this.Host != null)
			{
				this.Host.DocumentStartNotification(isSerializing, context, logicalPath);
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00094D6C File Offset: 0x00092F6C
		private protected sealed override void DocumentEndNotification(bool isSerializing, object context, string logicalPath, bool isSuccessful)
		{
			if (this.Host != null)
			{
				this.Host.DocumentEndNotification(isSerializing, context, logicalPath, isSuccessful);
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00094D86 File Offset: 0x00092F86
		private protected sealed override void OperationEndNotification(bool isSerializing, object context, bool isSuccessful)
		{
			if (this.Host != null)
			{
				this.Host.OperationEndNotification(isSerializing, context, isSuccessful);
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00094D9E File Offset: 0x00092F9E
		private protected sealed override void ErrorNotification(bool isSerializing, object context, Exception e)
		{
			if (this.Host != null)
			{
				this.Host.ErrorNotification(isSerializing, context, e);
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00094DB6 File Offset: 0x00092FB6
		private protected sealed override void OnSerializationStart(object userContext, IReadOnlyCollection<string> logicalPaths, out object operationContext)
		{
			operationContext = logicalPaths;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00094DBB File Offset: 0x00092FBB
		private protected sealed override void OnDocumentSerializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			documentContext = null;
			document = new MemoryStream();
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00094DCC File Offset: 0x00092FCC
		private protected sealed override void OnDocumentSerializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulSerialization)
		{
			try
			{
				if (isSuccessfulSerialization)
				{
					this.documents.Add(logicalPath, new MetadataDocument(this.Style, logicalPath, (MemoryStream)document));
				}
			}
			finally
			{
				document.Close();
			}
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00094E18 File Offset: 0x00093018
		private protected sealed override void OnSerializationEnd(object userContext, object operationContext, bool isSuccessfulSerialization)
		{
			if (isSuccessfulSerialization)
			{
				IReadOnlyCollection<string> readOnlyCollection = (IReadOnlyCollection<string>)operationContext;
				Utils.Verify(this.documents.Count == readOnlyCollection.Count, "Some of the documents are missing after the Serialization");
				foreach (string text in readOnlyCollection)
				{
					Utils.Verify(this.documents.ContainsKey(text), "The document for '{0}' is missing after the serialization", new KeyValuePair<InfoRestrictionType, object>[]
					{
						new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.CCON, text)
					});
				}
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00094EAC File Offset: 0x000930AC
		private protected sealed override IReadOnlyCollection<string> GetDocumentsForDeserialization(object userContext)
		{
			return this.keys;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00094EB4 File Offset: 0x000930B4
		private protected sealed override void OnDocumentDeserializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document)
		{
			documentContext = null;
			document = this.documents[logicalPath].GetContent();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00094ECE File Offset: 0x000930CE
		private protected sealed override void OnDocumentDeserializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulDeserialization)
		{
			document.Close();
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00094ED7 File Offset: 0x000930D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.documents.Values.GetEnumerator();
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00094EF0 File Offset: 0x000930F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetObjectPathElement(MetadataObject @object)
		{
			NamedMetadataObject namedMetadataObject = @object as NamedMetadataObject;
			if (namedMetadataObject != null)
			{
				return namedMetadataObject.Name;
			}
			IKeyedMetadataObject keyedMetadataObject = @object as IKeyedMetadataObject;
			if (keyedMetadataObject != null)
			{
				return keyedMetadataObject.LogicalPathElement;
			}
			return string.Empty;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00094F24 File Offset: 0x00093124
		private MetadataDocument ReadFromDocumentImpl(string logicalPath, Stream document)
		{
			MetadataDocument metadataDocument;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				document.CopyTo(memoryStream);
				metadataDocument = new MetadataDocument(this.Style, logicalPath, memoryStream);
			}
			this.documents.Add(logicalPath, metadataDocument);
			return metadataDocument;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00094F78 File Offset: 0x00093178
		private MetadataDocument ReadFromDocumentImpl(string logicalPath, TextReader reader, Encoding encoding)
		{
			MetadataDocument metadataDocument;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (TextWriter textWriter = new StreamWriter(memoryStream, encoding ?? MetadataFormattingOptions.GetEffectiveEncoding(), 1024, true))
				{
					reader.CopyTo(textWriter, true);
				}
				metadataDocument = new MetadataDocument(this.Style, logicalPath, memoryStream);
			}
			this.documents.Add(logicalPath, metadataDocument);
			return metadataDocument;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00094FFC File Offset: 0x000931FC
		private string GetNewLogicalPath()
		{
			if (!this.documents.ContainsKey("./unknown"))
			{
				return "./unknown";
			}
			int num = 0;
			string text;
			do
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", "./unknown", num);
			}
			while (this.documents.ContainsKey(text));
			return text;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00095050 File Offset: 0x00093250
		private void UpdateModelImpl(MetadataDeserializationOptions options, Model model, object context)
		{
			bool flag = true;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessDeserializationOptions(options, ref flag, out metadataCompatibilityOptions);
			SerializationActivityContext serializationActivityContext = TmdlSerializationHelper.CreateContextBasedOnDeserializationOptions(false, metadataCompatibilityOptions);
			using (new SerializationActivityInfoScope(serializationActivityContext, "SerializationActivity::ModelUpdateByObject"))
			{
				foreach (KeyValuePair<ObjectPath, MetadataObject> keyValuePair in this.ParseDocuments(serializationActivityContext, context))
				{
					MetadataObject metadataObject = ObjectTreeHelper.LocateObjectByPath(keyValuePair.Key, model);
					if (metadataObject != null)
					{
						try
						{
							metadataObject.CopyFrom(keyValuePair.Value, CopyFlags.UserCopy);
							continue;
						}
						catch (Exception ex)
						{
							throw this.CreateModelUpdateByObjectException(serializationActivityContext, keyValuePair.Value, TomSR.Exception_FailureInModelUpdateFromContext(keyValuePair.Value.ObjectType.ToString("G"), ClientHostingManager.MarkAsRestrictedInformation(keyValuePair.Key[keyValuePair.Key.Count - 1].Value, InfoRestrictionType.CCON)), ex);
						}
					}
					KeyValuePair<ObjectType, string> keyValuePair2 = keyValuePair.Key[keyValuePair.Key.Count - 1];
					ObjectPath key = keyValuePair.Key;
					key.Pop();
					if (key.IsEmpty)
					{
						this.AddDeserializedObjectToModel(serializationActivityContext, model, keyValuePair.Value);
					}
					else
					{
						MetadataObject metadataObject2 = ObjectTreeHelper.LocateObjectByPath(key, model);
						if (metadataObject2 == null)
						{
							throw this.CreateModelUpdateByObjectException(serializationActivityContext, keyValuePair.Value, TomSR.Exception_FailureInModelUpdateFromContext2(key[key.Count - 1].Key.ToString("G"), ClientHostingManager.MarkAsRestrictedInformation(key[key.Count - 1].Value, InfoRestrictionType.CCON), keyValuePair2.Key.ToString("G"), keyValuePair2.Value), null);
						}
						this.AddDeserializedObjectToModel(serializationActivityContext, metadataObject2, keyValuePair.Value);
					}
				}
			}
			serializationActivityContext.TryReconstructMasterReferenceCrossLinkForRegistreredObjects(!flag);
			List<string> list = new List<string>();
			if (!model.TryResolveAllCrossLinksInTreeByObjectPath(list) && flag)
			{
				throw this.CreateModelUpdateByObjectException(serializationActivityContext, null, TomSR.Exception_CannotDeserializeObjectResolvePathsFailedWithList(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Model), ClientHostingManager.MarkAsRestrictedInformation(string.Join(", ", list), InfoRestrictionType.CCON)), null);
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000952BC File Offset: 0x000934BC
		private void AddDeserializedObjectToModel(SerializationActivityContext context, MetadataObject parentObject, MetadataObject @object)
		{
			string objectPathElement = MetadataSerializationContext.GetObjectPathElement(@object);
			try
			{
				if (string.IsNullOrEmpty(objectPathElement))
				{
					parentObject.SetDirectChild(@object);
				}
				else
				{
					IMetadataObjectCollection metadataObjectCollection = (from c in parentObject.GetChildrenCollections(true)
						where c.ItemType == @object.ObjectType
						select c).SingleOrDefault<IMetadataObjectCollection>();
					if (metadataObjectCollection == null)
					{
						throw new InvalidOperationException(TomSR.Exception_InvalidChildCollection(parentObject.ObjectType.ToString("G"), @object.ObjectType.ToString("G")));
					}
					metadataObjectCollection.Add(@object);
				}
			}
			catch (Exception ex)
			{
				throw this.CreateModelUpdateByObjectException(context, @object, TomSR.Exception_FailureInModelUpdateFromContext3(parentObject.ObjectType.ToString("G"), ClientHostingManager.MarkAsRestrictedInformation(MetadataSerializationContext.GetObjectPathElement(parentObject), InfoRestrictionType.CCON), @object.ObjectType.ToString("G"), objectPathElement), ex);
			}
		}

		// Token: 0x0400041B RID: 1051
		private Dictionary<string, MetadataDocument> documents;

		// Token: 0x0400041C RID: 1052
		private IReadOnlyCollection<string> keys;

		// Token: 0x02000348 RID: 840
		private sealed class DocumentKeys : IReadOnlyCollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x0600258C RID: 9612 RVA: 0x000E8749 File Offset: 0x000E6949
			public DocumentKeys(IDictionary<string, MetadataDocument> documents)
			{
				this.documents = documents;
			}

			// Token: 0x17000793 RID: 1939
			// (get) Token: 0x0600258D RID: 9613 RVA: 0x000E8758 File Offset: 0x000E6958
			public int Count
			{
				get
				{
					return this.documents.Count;
				}
			}

			// Token: 0x0600258E RID: 9614 RVA: 0x000E8765 File Offset: 0x000E6965
			public IEnumerator<string> GetEnumerator()
			{
				return this.documents.Keys.GetEnumerator();
			}

			// Token: 0x0600258F RID: 9615 RVA: 0x000E8777 File Offset: 0x000E6977
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.documents.Keys.GetEnumerator();
			}

			// Token: 0x04000E55 RID: 3669
			private IDictionary<string, MetadataDocument> documents;
		}

		// Token: 0x02000349 RID: 841
		private sealed class TmdlSerializationContext : MetadataSerializationContext
		{
			// Token: 0x06002590 RID: 9616 RVA: 0x000E8789 File Offset: 0x000E6989
			public TmdlSerializationContext()
				: base(MetadataSerializationStyle.Tmdl)
			{
			}

			// Token: 0x06002591 RID: 9617 RVA: 0x000E8792 File Offset: 0x000E6992
			private protected override void LoadModel(MetadataSerializationOptions options, Model model, object context)
			{
				TmdlSerializer.SerializeModelImpl(options, model, this, context);
			}

			// Token: 0x06002592 RID: 9618 RVA: 0x000E879D File Offset: 0x000E699D
			private protected override void LoadDatabase(MetadataSerializationOptions options, Database db, object context)
			{
				TmdlSerializer.SerializeDatabaseImpl(options, db, this, context);
			}

			// Token: 0x06002593 RID: 9619 RVA: 0x000E87A8 File Offset: 0x000E69A8
			private protected override IEnumerable<KeyValuePair<ObjectPath, MetadataObject>> ParseDocuments(SerializationActivityContext activityContext, object context)
			{
				TmdlProject tmdlProject = TmdlParser.ParseProject(TmdlContentSource.InMemory, MetadataObjectConfiguration.Default.Schema, this, context);
				List<TmdlObject> list = new List<TmdlObject>();
				foreach (TmdlDocument tmdlDocument in tmdlProject.Documents)
				{
					foreach (TmdlObject tmdlObject in tmdlDocument.Objects)
					{
						ObjectType objectType = tmdlObject.ObjectType;
						if (objectType == ObjectType.Null)
						{
							throw TomInternalException.Create("The schema that is used by the parser should not include a pseudo-object as a root object!", Array.Empty<object>());
						}
						if (objectType == ObjectType.Model)
						{
							throw new TmdlSerializationException(TomSR.Exception_ModelObjectInModelUpdateFromContext, tmdlObject.SourceLocation);
						}
						if (objectType == ObjectType.Database)
						{
							throw TomInternalException.Create("The schema that is used by the parser should not include the DB!", Array.Empty<object>());
						}
						list.Add(tmdlObject);
					}
				}
				IList<KeyValuePair<ObjectType, IList<TmdlObject>>> list2 = TmdlSerializationHelper.MergeAndGroupChildObject(list, default(TmdlSourceLocation));
				foreach (IList<TmdlObject> list3 in list2.Select((KeyValuePair<ObjectType, IList<TmdlObject>> kvp) => kvp.Value))
				{
					foreach (KeyValuePair<ObjectPath, MetadataObject> keyValuePair in MetadataSerializationContext.TmdlSerializationContext.ConvertTmdlObjects(activityContext, null, list3))
					{
						yield return keyValuePair;
					}
					IEnumerator<KeyValuePair<ObjectPath, MetadataObject>> enumerator4 = null;
				}
				IEnumerator<IList<TmdlObject>> enumerator3 = null;
				yield break;
				yield break;
			}

			// Token: 0x06002594 RID: 9620 RVA: 0x000E87C8 File Offset: 0x000E69C8
			private protected override Exception CreateModelUpdateByObjectException(SerializationActivityContext context, MetadataObject @object, string error, Exception e = null)
			{
				TmdlSourceLocation tmdlSourceLocation = default(TmdlSourceLocation);
				TmdlObject tmdlObject;
				if (context.TryGetActivityInfo<TmdlObject>("SerializationActivity::ModelUpdateByObject", out tmdlObject))
				{
					tmdlSourceLocation = tmdlObject.SourceLocation;
				}
				if (e != null)
				{
					return new TmdlSerializationException(error, tmdlSourceLocation, e);
				}
				return new TmdlSerializationException(error, tmdlSourceLocation);
			}

			// Token: 0x06002595 RID: 9621 RVA: 0x000E8808 File Offset: 0x000E6A08
			private protected override Model CreateModel(MetadataDeserializationOptions options, object context)
			{
				return TmdlSerializer.DeserializeModelImpl(options, TmdlContentSource.InMemory, this, context);
			}

			// Token: 0x06002596 RID: 9622 RVA: 0x000E8813 File Offset: 0x000E6A13
			private protected override Database CreateDatabase(MetadataDeserializationOptions options, object context)
			{
				return TmdlSerializer.DeserializeDatabaseImpl(options, TmdlContentSource.InMemory, this, context);
			}

			// Token: 0x06002597 RID: 9623 RVA: 0x000E881E File Offset: 0x000E6A1E
			private static IEnumerable<KeyValuePair<ObjectPath, MetadataObject>> ConvertTmdlObjects(SerializationActivityContext context, ObjectPath basePath, IList<TmdlObject> tmdlObjects)
			{
				foreach (TmdlObject tmdlObject in tmdlObjects)
				{
					if (tmdlObject.IsReference)
					{
						if (tmdlObject.HasAnyChild(false))
						{
							ObjectPath path = MetadataSerializationContext.TmdlSerializationContext.BuildObjectPath(basePath, tmdlObject);
							IList<KeyValuePair<ObjectType, IList<TmdlObject>>> list = TmdlSerializationHelper.MergeAndGroupChildObject(tmdlObject.Children.Where((TmdlObject c) => c.ObjectType > ObjectType.Null), tmdlObject.SourceLocation);
							foreach (IList<TmdlObject> list2 in list.Select((KeyValuePair<ObjectType, IList<TmdlObject>> kvp) => kvp.Value))
							{
								foreach (KeyValuePair<ObjectPath, MetadataObject> keyValuePair in MetadataSerializationContext.TmdlSerializationContext.ConvertTmdlObjects(context, path, list2))
								{
									yield return keyValuePair;
								}
								IEnumerator<KeyValuePair<ObjectPath, MetadataObject>> enumerator3 = null;
							}
							IEnumerator<IList<TmdlObject>> enumerator2 = null;
							path = null;
						}
					}
					else
					{
						TmdlObjectReader tmdlObjectReader = new TmdlObjectReader(tmdlObject);
						MetadataObject metadataObject = MetadataObject.CreateFromMetadataStream<MetadataObject>(context, tmdlObject.ObjectType, tmdlObjectReader);
						context.ActivityInfo["SerializationActivity::ModelUpdateByObject"] = tmdlObject;
						yield return new KeyValuePair<ObjectPath, MetadataObject>(MetadataSerializationContext.TmdlSerializationContext.BuildObjectPath(basePath, metadataObject), metadataObject);
					}
				}
				IEnumerator<TmdlObject> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002598 RID: 9624 RVA: 0x000E883C File Offset: 0x000E6A3C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static ObjectPath BuildObjectPath(ObjectPath basePath, MetadataObject @object)
			{
				return MetadataSerializationContext.TmdlSerializationContext.BuildObjectPathImpl(basePath, @object.ObjectType, MetadataSerializationContext.GetObjectPathElement(@object));
			}

			// Token: 0x06002599 RID: 9625 RVA: 0x000E8850 File Offset: 0x000E6A50
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static ObjectPath BuildObjectPath(ObjectPath basePath, TmdlObject @object)
			{
				string text;
				if (ObjectTreeHelper.IsNamedObject(@object.ObjectType) || ObjectTreeHelper.IsKeyedObject(@object.ObjectType))
				{
					text = @object.Name.Name;
				}
				else
				{
					text = string.Empty;
				}
				return MetadataSerializationContext.TmdlSerializationContext.BuildObjectPathImpl(basePath, @object.ObjectType, text);
			}

			// Token: 0x0600259A RID: 9626 RVA: 0x000E889B File Offset: 0x000E6A9B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static ObjectPath BuildObjectPathImpl(ObjectPath basePath, ObjectType objectType, string pathElement)
			{
				if (basePath == null)
				{
					return new ObjectPath(objectType, pathElement);
				}
				ObjectPath objectPath = basePath.Clone();
				objectPath.Push(objectType, pathElement);
				return objectPath;
			}
		}
	}
}
