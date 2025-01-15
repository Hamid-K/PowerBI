using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000096 RID: 150
	public sealed class SemanticModel : ModelItem
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00016BAC File Offset: 0x00014DAC
		public static SemanticModel LoadFromBinary(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			PersistenceHelper persistenceHelper = new PersistenceHelper();
			IntermediateFormatReader intermediateFormatReader = new IntermediateFormatReader(stream, persistenceHelper, persistenceHelper);
			SemanticModel semanticModel = (SemanticModel)intermediateFormatReader.ReadRIFObject();
			intermediateFormatReader.ResolveReferences();
			return semanticModel;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00016BEC File Offset: 0x00014DEC
		public void WriteToBinary(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, new PersistenceHelper(), PersistenceConstants.CurrentCompatVersion);
			intermediateFormatWriter.Write(this);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00016C24 File Offset: 0x00014E24
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(SemanticModel.Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.Entities:
					((IPersistable)this.Entities).Serialize(writer);
					break;
				case MemberName.Version:
					writer.Write(this.m_version);
					break;
				case MemberName.Culture:
					writer.Write((this.m_culture != null && string.Empty != this.m_culture.Name) ? this.m_culture.ToString() : null);
					break;
				case MemberName.DataSourceView:
					writer.Write(this.m_dsv);
					break;
				case MemberName.Perspectives:
					((IPersistable)this.Perspectives).Serialize(writer);
					break;
				case MemberName.NamespacePrefixes:
					((IPersistable)this.NamespacePrefixes).Serialize(writer);
					break;
				default:
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00016D34 File Offset: 0x00014F34
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(SemanticModel.Declaration);
				while (reader.NextMember())
				{
					switch (reader.CurrentMember.MemberName)
					{
					case MemberName.Entities:
						((IPersistable)this.Entities).Deserialize(reader);
						break;
					case MemberName.Version:
						this.m_version = reader.ReadString();
						break;
					case MemberName.Culture:
					{
						string text = reader.ReadString();
						if (!string.IsNullOrEmpty(text))
						{
							this.m_culture = CultureInfo.GetCultureInfo(text);
						}
						break;
					}
					case MemberName.DataSourceView:
						this.m_dsv = (DataSourceView)reader.ReadRIFObject();
						break;
					case MemberName.Perspectives:
						((IPersistable)this.Perspectives).Deserialize(reader);
						break;
					case MemberName.NamespacePrefixes:
						((IPersistable)this.NamespacePrefixes).Deserialize(reader);
						break;
					default:
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
				}
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00016E50 File Offset: 0x00015050
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00016E5C File Offset: 0x0001505C
		internal override ObjectType GetObjectType()
		{
			return ObjectType.SemanticModel;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00016E60 File Offset: 0x00015060
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref SemanticModel.__declaration, SemanticModel.__declarationLock, () => new Declaration(ObjectType.SemanticModel, ObjectType.ModelItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Version, Token.String),
					new MemberInfo(MemberName.Culture, Token.String),
					new MemberInfo(MemberName.DataSourceView, ObjectType.DataSourceView),
					new MemberInfo(MemberName.Entities, ObjectType.OwnedModelItemCollection),
					new MemberInfo(MemberName.Perspectives, ObjectType.OwnedModelItemCollection),
					new MemberInfo(MemberName.NamespacePrefixes, ObjectType.NamespacePrefixes)
				}));
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00016E90 File Offset: 0x00015090
		public SemanticModel()
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00016EBC File Offset: 0x000150BC
		internal override void Reset()
		{
			base.Reset();
			this.m_version = string.Empty;
			this.m_culture = null;
			this.m_dsv = null;
			this.__entities = new ModelEntityFolderItemCollection(this);
			this.__perspectives = new PerspectiveCollection(this);
			this.m_namespacePrefixes.Clear();
			this.m_namespacePrefixes.AddRange(SemanticModelingSchema.DefaultNamespacePrefixes);
			this.m_allItems.Clear();
			if (this.m_lookupItemStack.Count != 0)
			{
				throw new InternalModelingException("Model.Reset called within LookupBaseModelItem (lookup item stack size != 0)");
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00016F40 File Offset: 0x00015140
		private SemanticModel(SemanticModel baseModel, Predicate<ModelItem> includeItem, Predicate<ModelItem> itemInPerspective)
			: base(baseModel, false)
		{
			if (!baseModel.IsCompiled)
			{
				throw new InternalModelingException("baseModel is not compiled");
			}
			if (includeItem == null)
			{
				throw new InternalModelingException("includeItem is null.");
			}
			this.m_includeItem = includeItem;
			this.m_itemInPerspective = itemInPerspective;
			this.m_version = baseModel.Version;
			this.m_culture = baseModel.Culture;
			this.m_namespacePrefixes = baseModel.NamespacePrefixes;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00016FC9 File Offset: 0x000151C9
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x00016FD0 File Offset: 0x000151D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("The ModelItem.Name property is not applicable for SemanticModel.")]
		public override string Name
		{
			get
			{
				return string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					throw new InvalidOperationException();
				}
				base.CheckWriteable();
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00016FE6 File Offset: 0x000151E6
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00016FE9 File Offset: 0x000151E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("The ModelItem.Hidden property is not applicable for SemanticModel.")]
		public override bool Hidden
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
				{
					throw new InvalidOperationException();
				}
				base.CheckWriteable();
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00016FFA File Offset: 0x000151FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override SemanticModel Model
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00016FFD File Offset: 0x000151FD
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00017005 File Offset: 0x00015205
		public string Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				base.CheckWriteable();
				this.m_version = value ?? string.Empty;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001701D File Offset: 0x0001521D
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00017025 File Offset: 0x00015225
		public CultureInfo Culture
		{
			get
			{
				return this.m_culture;
			}
			set
			{
				base.CheckWriteable();
				this.m_culture = ((value == null) ? null : CultureInfo.ReadOnly(value));
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001703F File Offset: 0x0001523F
		public ModelEntityFolderItemCollection Entities
		{
			get
			{
				if (this.__entities == null)
				{
					this.__entities = this.BaseModel.Entities.CloneFor(this);
				}
				return this.__entities;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00017066 File Offset: 0x00015266
		public PerspectiveCollection Perspectives
		{
			get
			{
				if (this.__perspectives == null)
				{
					this.__perspectives = this.BaseModel.Perspectives.CloneFor(this);
				}
				return this.__perspectives;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001708D File Offset: 0x0001528D
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00017095 File Offset: 0x00015295
		public DataSourceView DataSourceView
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dsv;
			}
			set
			{
				base.CheckWriteable();
				this.m_dsv = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000170A4 File Offset: 0x000152A4
		public XmlNamespacePrefixCollection NamespacePrefixes
		{
			get
			{
				return this.m_namespacePrefixes;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600072D RID: 1837 RVA: 0x000170AC File Offset: 0x000152AC
		// (remove) Token: 0x0600072E RID: 1838 RVA: 0x000170E4 File Offset: 0x000152E4
		public event EventHandler<ModelItemEventArgs> ItemCollectionChanged;

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00017119 File Offset: 0x00015319
		internal SemanticModel BaseModel
		{
			get
			{
				return (SemanticModel)base.BaseItem;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00017126 File Offset: 0x00015326
		internal override string ElementName
		{
			get
			{
				return "SemanticModel";
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001712D File Offset: 0x0001532D
		internal override bool IsNamed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00017130 File Offset: 0x00015330
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00017137 File Offset: 0x00015337
		public static bool SuppressDrillthroughDuringLazyClone
		{
			get
			{
				return SemanticModel.m_suppressDrillthroughDuringLazyClone;
			}
			set
			{
				SemanticModel.m_suppressDrillthroughDuringLazyClone = value;
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001713F File Offset: 0x0001533F
		public override bool IsValidParentItem(ModelItem newParentItem, out ValidationMessage message)
		{
			if (newParentItem == null)
			{
				message = null;
				return true;
			}
			return base.IsValidParentItem(newParentItem, out message);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00017151 File Offset: 0x00015351
		public override IEnumerable<ModelItem> GetOwnedItems()
		{
			foreach (ModelItem modelItem in this.Entities)
			{
				yield return modelItem;
			}
			List<ModelEntityFolderItem>.Enumerator enumerator = default(List<ModelEntityFolderItem>.Enumerator);
			foreach (ModelItem modelItem2 in this.Perspectives)
			{
				yield return modelItem2;
			}
			List<Perspective>.Enumerator enumerator2 = default(List<Perspective>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00017161 File Offset: 0x00015361
		public IEnumerable<ModelEntity> GetAllEntities()
		{
			Queue<ModelEntityFolderItemCollection> folderEntities = null;
			do
			{
				ModelEntityFolderItemCollection modelEntityFolderItemCollection = ((folderEntities == null) ? this.Entities : folderEntities.Dequeue());
				foreach (ModelEntityFolderItem modelEntityFolderItem in modelEntityFolderItemCollection)
				{
					ModelEntity modelEntity = modelEntityFolderItem as ModelEntity;
					ModelEntityFolder modelEntityFolder = modelEntityFolderItem as ModelEntityFolder;
					if (modelEntity != null)
					{
						yield return modelEntity;
					}
					else
					{
						if (modelEntityFolder == null)
						{
							string text = "Unknown ModelEntityFolderItem derived type ";
							Type type = modelEntityFolderItem.GetType();
							throw new InternalModelingException(text + ((type != null) ? type.ToString() : null));
						}
						if (folderEntities == null)
						{
							folderEntities = new Queue<ModelEntityFolderItemCollection>();
						}
						folderEntities.Enqueue(modelEntityFolder.Entities);
					}
				}
				List<ModelEntityFolderItem>.Enumerator enumerator = default(List<ModelEntityFolderItem>.Enumerator);
			}
			while (folderEntities != null && folderEntities.Count > 0);
			yield break;
			yield break;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00017171 File Offset: 0x00015371
		public ModelItem LookupItemByID(QName id)
		{
			return this.LookupItemByID(id, null);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001717B File Offset: 0x0001537B
		internal void NotifyCollectionChanged(IOwnedModelItemCollection collection)
		{
			if (this.ItemCollectionChanged != null)
			{
				this.ItemCollectionChanged(this, new ModelItemEventArgs(new ModelItem[] { collection.ParentItem }));
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000171A8 File Offset: 0x000153A8
		public ValidationMessageCollection Load(XmlReader xr)
		{
			ValidationContext validationContext = new ValidationContext();
			this.Load(xr, validationContext, SemanticModelingSchema.Relaxed);
			return validationContext.GetMessages();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000171D0 File Offset: 0x000153D0
		private void Load(XmlReader xr, ValidationContext validationCtx, SemanticModelingSchema schema)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			base.CheckWriteable();
			DeserializationContext deserializationContext = new DeserializationContext(this, validationCtx);
			ModelingXmlReader mxr = new ModelingXmlReader(xr, schema, deserializationContext);
			this.Reset();
			XmlUtil.WrapXmlExceptions(delegate
			{
				XmlUtil.CheckElement(mxr.Reader, "SemanticModel", "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");
				this.Load(mxr);
			}, ModelingErrorCode.InvalidSemanticModel, new XmlUtil.ErrorMessageWrap(SRErrors.InvalidSemanticModel));
			deserializationContext.Validation.ThrowIfErrors();
			deserializationContext.CompleteLoad();
			deserializationContext.Validation.ThrowIfErrors();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00017252 File Offset: 0x00015452
		internal override bool LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				this.m_namespacePrefixes.AddFromReader(xr);
				return true;
			}
			return base.LoadXmlAttribute(xr);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001727C File Offset: 0x0001547C
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "Version")
				{
					this.m_version = xr.ReadValueAsString();
					return true;
				}
				if (localName == "Culture")
				{
					this.m_culture = xr.ReadValueAsCultureInfo();
					return true;
				}
				if (localName == "Entities")
				{
					this.Entities.Load(xr);
					return true;
				}
				if (localName == "Perspectives")
				{
					this.Perspectives.Load(xr);
					return true;
				}
			}
			if (xr.NamespaceURI == "http://schemas.microsoft.com/analysisservices/2003/engine" && xr.LocalName == "DataSourceView")
			{
				this.m_dsv = new DataSourceView();
				this.m_dsv.Load(xr.Reader);
				return true;
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00017350 File Offset: 0x00015550
		internal T TryGetModelItem<T>(ModelingReference itemRef, ValidationContext ctx) where T : ModelItem
		{
			ModelItem modelItem = this.TryResolveModelItemReference(itemRef, ctx);
			if (modelItem == null)
			{
				modelItem = ModelItem.CreateItem<T>();
				if (itemRef.ReferenceByID != null)
				{
					modelItem.ID = itemRef.ReferenceByID.Value;
				}
				else if (itemRef.ReferenceByName != null)
				{
					modelItem.Name = itemRef.ReferenceByName;
				}
				if (!modelItem.IsInvalidRefTarget)
				{
					throw new InternalModelingException("Invalid ref target has false item.IsInvalidRefTarget value.");
				}
				ctx.SetInvalidRefsFlag();
				string text;
				if (itemRef.MultipleInScope)
				{
					text = SRErrors.ItemNotFound_MultipleProperties(itemRef.PropertyName, ctx.CurrentObjectDescriptor, itemRef.ReferenceString);
				}
				else
				{
					text = SRErrors.ItemNotFound(itemRef.PropertyName, ctx.CurrentObjectDescriptor, itemRef.ReferenceString);
				}
				if (ctx.ShouldCheckInvalidRefsDuringTryGet)
				{
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, text);
				}
				else
				{
					ctx.AddScopedWarning(ModelingErrorCode.ItemNotFound, text);
				}
			}
			if (!(modelItem is T))
			{
				string text2;
				if (itemRef.MultipleInScope)
				{
					text2 = SRErrors.InvalidReferencedItem_MultipleProperties(itemRef.PropertyName, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(modelItem), ModelItem.GetConcreteElementNames(typeof(T)));
				}
				else
				{
					text2 = SRErrors.InvalidReferencedItem(itemRef.PropertyName, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(modelItem), ModelItem.GetConcreteElementNames(typeof(T)));
				}
				throw new ValidationException(ctx.CreateScopedError(ModelingErrorCode.InvalidReferencedItem, text2));
			}
			return (T)((object)modelItem);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000174A4 File Offset: 0x000156A4
		internal ModelItem TryResolveModelItemReference(ModelingReference itemRef, ValidationContext ctx)
		{
			ModelItem modelItem = null;
			if (itemRef.ReferenceByID != null)
			{
				modelItem = this.LookupItemByID(itemRef.ReferenceByID.Value);
			}
			return modelItem;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000174DB File Offset: 0x000156DB
		public void WriteTo(XmlWriter xw)
		{
			this.WriteTo(xw, ModelingSerializationOptions.None);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000174E8 File Offset: 0x000156E8
		public void WriteTo(XmlWriter xw, ModelingSerializationOptions options)
		{
			if (xw == null)
			{
				throw new ArgumentNullException("xw");
			}
			ModelingXmlWriter modelingXmlWriter = new ModelingXmlWriter(xw, SemanticModelingSchema.Relaxed, options);
			base.WriteTo(modelingXmlWriter);
			modelingXmlWriter.Writer.Flush();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00017522 File Offset: 0x00015722
		internal override void WriteXmlAttributes(ModelingXmlWriter xw)
		{
			base.WriteXmlAttributes(xw);
			xw.WriteDefaultNamespace();
			xw.WriteNamespacePrefixes(this.m_namespacePrefixes);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00017540 File Offset: 0x00015740
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteXmlElements(xw);
			xw.WriteElementIfNonDefault<string>("Version", this.m_version);
			if (this.m_culture != null && string.Empty != this.m_culture.Name)
			{
				xw.WriteElementIfNonDefault<CultureInfo>("Culture", this.m_culture);
			}
			this.Entities.WriteTo(xw, "Entities");
			this.Perspectives.WriteTo(xw, "Perspectives");
			if (this.m_dsv != null && xw.ShouldWriteBindings)
			{
				this.m_dsv.WriteTo(xw.Writer);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000175D8 File Offset: 0x000157D8
		public ValidationMessageCollection Compile(XmlReader xr)
		{
			return this.Compile(xr, ModelCompilationOptions.None, null);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000175E3 File Offset: 0x000157E3
		public ValidationMessageCollection Compile(XmlReader xr, ModelCompilationOptions options)
		{
			return this.Compile(xr, options, null);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000175F0 File Offset: 0x000157F0
		public ValidationMessageCollection Compile(XmlReader xr, ModelCompilationOptions options, CultureInfo defaultCulture)
		{
			CompilationContext compilationContext = new CompilationContext(true, options);
			this.Load(xr, compilationContext, SemanticModelingSchema.Strict);
			if (this.m_culture == null)
			{
				this.m_culture = defaultCulture;
			}
			this.Compile(compilationContext);
			compilationContext.ThrowIfErrors();
			return compilationContext.GetMessages();
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00017634 File Offset: 0x00015834
		internal override void Compile(CompilationContext ctx)
		{
			if (base.CustomProperties.Contains(SemanticModel.UdmBindingProperty))
			{
				ctx.SetIgnoreBindings();
			}
			base.Compile(ctx);
			if (ctx.ShouldCheckSecurityFilters)
			{
				this.CheckSecurityFilters(ctx);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00017664 File Offset: 0x00015864
		internal override void CompileCore(CompilationContext ctx)
		{
			base.CompileCore(ctx);
			if (this.m_dsv != null)
			{
				this.m_dsv.Compile(ctx);
			}
			else if (ctx.ShouldCheckBindings)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingDataSourceView, SRErrors.MissingDataSourceView(ctx.CurrentObjectDescriptor));
			}
			this.m_namespacePrefixes.MakeReadOnly();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000176B4 File Offset: 0x000158B4
		private void CheckSecurityFilters(CompilationContext ctx)
		{
			CompilationContext compilationContext = new CompilationContext(true, QueryCompilationOptions.Normalize, null, null, null, (ModelItem modelItem) => true);
			CompilationContext compilationContext2 = new CompilationContext(true, QueryCompilationOptions.Normalize, null, null, null, (ModelItem modelItem) => false);
			foreach (ModelEntity modelEntity in this.GetAllEntities())
			{
				if (modelEntity.SecurityFilters.Count > 0)
				{
					compilationContext.PushScope(modelEntity);
					compilationContext.ClearMessages();
					modelEntity.TryGetSecurityFilterCondition(compilationContext);
					foreach (ValidationMessage validationMessage in compilationContext.GetMessages())
					{
						ctx.AddMessage(validationMessage);
					}
					compilationContext.PopScope();
				}
				if (modelEntity.DefaultSecurityFilter != null || modelEntity.SecurityFilters.Count > 0)
				{
					compilationContext2.PushScope(modelEntity);
					compilationContext2.ClearMessages();
					modelEntity.TryGetSecurityFilterCondition(compilationContext2);
					foreach (ValidationMessage validationMessage2 in compilationContext2.GetMessages())
					{
						ctx.AddMessage(validationMessage2);
					}
					compilationContext2.PopScope();
				}
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001784C File Offset: 0x00015A4C
		internal override void CheckOwnedItemsNameUniqueness(CompilationContext ctx)
		{
			ctx.CheckNameUniqueness<ModelItem>(this.Entities, ModelingErrorCode.DuplicateItemName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateItemName));
			ctx.CheckNameUniqueness<ModelItem>(this.Perspectives, ModelingErrorCode.DuplicateItemName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateItemName));
			ctx.CheckNameUniqueness<ModelEntity>(this.GetAllEntities(), ModelingErrorCode.DuplicateEntityName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateEntityName));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000178AA File Offset: 0x00015AAA
		internal void LinkItem(ModelItem item)
		{
			base.CheckWriteable();
			ModelItem.VisitAllItems(item, new Action<ModelItem>(this.LinkSingleItem));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000178C4 File Offset: 0x00015AC4
		internal void UnlinkItem(ModelItem item)
		{
			base.CheckWriteable();
			ModelItem.VisitAllItems(item, new Action<ModelItem>(this.UnlinkSingleItem));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000178DE File Offset: 0x00015ADE
		internal void UpdateLinkedItemID(ModelItem item, QName newID)
		{
			this.UnlinkSingleItem(item);
			this.LinkSingleItem(item, newID);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000178EF File Offset: 0x00015AEF
		private void LinkSingleItem(ModelItem item)
		{
			if (item == null)
			{
				throw new InternalModelingException("item is null");
			}
			if (item.Model != this)
			{
				throw new InternalModelingException("item.Model mismatch");
			}
			this.LinkSingleItem(item, item.ID);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00017920 File Offset: 0x00015B20
		private void LinkSingleItem(ModelItem item, QName itemID)
		{
			if (this.m_allItems.ContainsKey(itemID) || itemID == base.ID)
			{
				throw new ValidationException(ModelingErrorCode.DuplicateItemID, item, SRErrors.DuplicateItemID(itemID));
			}
			this.m_allItems.Add(itemID, item);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001795C File Offset: 0x00015B5C
		private void UnlinkSingleItem(ModelItem item)
		{
			if (item == null)
			{
				throw new InternalModelingException("item is null");
			}
			if (item.Model != this)
			{
				throw new InternalModelingException("item.Model mismatch");
			}
			if (!this.m_allItems.ContainsKey(item.ID))
			{
				throw new InternalModelingException("item not linked");
			}
			this.m_allItems.Remove(item.ID);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000179BC File Offset: 0x00015BBC
		public SemanticModel CreateLazyClone(Predicate<ModelItem> includeItem)
		{
			return this.CreateLazyCloneCore(includeItem, null);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000179D9 File Offset: 0x00015BD9
		public SemanticModel CreateLazyClone(Predicate<ModelItem> includeItem, QName perspectiveID)
		{
			return this.CreateLazyCloneCore(includeItem, new QName?(perspectiveID));
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000179E8 File Offset: 0x00015BE8
		private SemanticModel CreateLazyCloneCore(Predicate<ModelItem> includeItem, QName? perspectiveID)
		{
			if (includeItem == null)
			{
				throw new ArgumentNullException("includeItem");
			}
			if (!base.IsCompiled)
			{
				throw new InvalidOperationException();
			}
			if (perspectiveID == null)
			{
				return new SemanticModel(this, includeItem, null);
			}
			Bag<ModelItem> perspectiveItems = this.GetCompiledPerspectiveItems(perspectiveID.Value);
			SemanticModel semanticModel = new SemanticModel(this, includeItem, (ModelItem item) => perspectiveItems.Contains(item));
			foreach (ModelItem modelItem in perspectiveItems)
			{
				semanticModel.LookupItemByID(modelItem.ID);
			}
			semanticModel.m_skipLookupBaseModelItem = true;
			return semanticModel;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00017AA0 File Offset: 0x00015CA0
		private Bag<ModelItem> GetCompiledPerspectiveItems(QName perspectiveID)
		{
			ModelItem modelItem = this.LookupItemByID(perspectiveID);
			Perspective perspective = modelItem as Perspective;
			if (modelItem == null)
			{
				throw new ValidationException(ModelingErrorCode.ItemNotFound, SRErrors.SemanticModel_PerspectiveNotFound(perspectiveID));
			}
			if (perspective == null)
			{
				throw new ArgumentException(DevExceptionMessages.SemanticModel_InvalidPerspectiveItem(SRObjectDescriptor.FromScope(modelItem)));
			}
			if (!perspective.IsCompiled)
			{
				throw new InvalidOperationException();
			}
			Bag<ModelItem> bag = new Bag<ModelItem>(perspective.ModelItems, true);
			Bag<ModelItem> bag2 = new Bag<ModelItem>();
			foreach (ModelItem modelItem2 in bag)
			{
				ModelEntity modelEntity = modelItem2 as ModelEntity;
				if (modelEntity != null)
				{
					foreach (AttributeReference attributeReference in modelEntity.IdentifyingAttributes)
					{
						if (!bag2.Contains(attributeReference.Attribute) && !bag.Contains(attributeReference.Attribute))
						{
							bag2.Add(attributeReference.Attribute);
						}
					}
				}
			}
			if (bag2.Count > 0)
			{
				bag.AddRange(bag2);
			}
			return bag;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00017BC0 File Offset: 0x00015DC0
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			throw new InternalModelingException("Model.CreateLazyClone was called");
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00017BCC File Offset: 0x00015DCC
		internal ModelItem LookupItemByID(QName id, ModelItem requestingChild)
		{
			ModelItem modelItem;
			if (this.m_allItems.TryGetValue(id, out modelItem))
			{
				if (modelItem != null && modelItem.ID != id)
				{
					throw new InternalModelingException("LookupItemByID: id does not match item.ID");
				}
				return modelItem;
			}
			else
			{
				if (id == base.ID)
				{
					return this;
				}
				if (this.BaseModel != null && !this.m_skipLookupBaseModelItem)
				{
					return this.LookupBaseModelItem(id, requestingChild);
				}
				return null;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00017C34 File Offset: 0x00015E34
		private ModelItem LookupBaseModelItem(QName id, ModelItem requestingChild)
		{
			if (!base.IsLazyClone)
			{
				throw new InternalModelingException("LookupBaseModelItem called on non-lazy cloned model");
			}
			ModelItem modelItem = this.m_lookupItemStack.FindItem(id);
			if (modelItem != null)
			{
				this.m_lookupItemStack.CurrentFrame.AddDependency(id);
				return modelItem;
			}
			modelItem = this.BaseModel.LookupItemByID(id);
			bool flag = requestingChild != null && modelItem is IFolderItem;
			if (modelItem != null && !flag && !this.m_includeItem(modelItem))
			{
				if (modelItem is IFolderItem && !flag)
				{
					modelItem = this.TryLazyCloneExcludedFolderItem((IFolderItem)modelItem);
					if (modelItem != null)
					{
						return modelItem;
					}
				}
				else
				{
					modelItem = null;
				}
			}
			if (modelItem == null)
			{
				this.m_allItems.Add(id, null);
				return null;
			}
			modelItem = modelItem.CreateLazyClone(this.m_itemInPerspective != null && !this.m_itemInPerspective(modelItem));
			this.m_lookupItemStack.Push(new SemanticModel.LookupItemFrame(modelItem));
			if (flag)
			{
				this.m_lookupItemStack.CurrentFrame.AddDependency(requestingChild.ID);
			}
			SemanticModel.LookupItemFrame lookupItemFrame;
			try
			{
				if (!modelItem.ResolveRequiredReferences(this))
				{
					modelItem = null;
				}
			}
			finally
			{
				lookupItemFrame = this.m_lookupItemStack.Pop();
				if (lookupItemFrame == null)
				{
					throw new InternalModelingException("Popped frame is null (unbalanced stack).");
				}
			}
			if (modelItem != null)
			{
				if (lookupItemFrame.HasDependencies)
				{
					SemanticModel.LookupItemFrame currentFrame = this.m_lookupItemStack.CurrentFrame;
					if (currentFrame == null)
					{
						throw new InternalModelingException("Popped frame on LookupItem stack has dependencies but there are no more frames");
					}
					currentFrame.AddDependenciesFrom(lookupItemFrame);
					currentFrame.AddNewItemsFrom(lookupItemFrame);
					return modelItem;
				}
				else
				{
					using (List<ModelItem>.Enumerator enumerator = lookupItemFrame.GetNewItems().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ModelItem modelItem2 = enumerator.Current;
							this.m_allItems.Add(modelItem2.ID, modelItem2);
						}
						return modelItem;
					}
				}
			}
			this.m_allItems.Add(id, null);
			return modelItem;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00017DF0 File Offset: 0x00015FF0
		private ModelItem TryLazyCloneExcludedFolderItem(IFolderItem item)
		{
			foreach (ModelItem modelItem in item.Items)
			{
				if ((this.m_itemInPerspective == null || this.m_itemInPerspective(modelItem)) && this.LookupItemByID(modelItem.ID) != null)
				{
					ModelItem modelItem2 = this.m_allItems[((ModelItem)item).ID];
					if (modelItem2 == null)
					{
						throw new InternalModelingException("Failed to copy folder model item after one of its children has been successfully copied.");
					}
					return modelItem2;
				}
			}
			return null;
		}

		// Token: 0x0400036D RID: 877
		private static Declaration __declaration;

		// Token: 0x0400036E RID: 878
		private static readonly object __declarationLock = new object();

		// Token: 0x0400036F RID: 879
		internal const string SemanticModelElem = "SemanticModel";

		// Token: 0x04000370 RID: 880
		private const string VersionElem = "Version";

		// Token: 0x04000371 RID: 881
		private const string CultureElem = "Culture";

		// Token: 0x04000372 RID: 882
		internal const string EntitiesElem = "Entities";

		// Token: 0x04000373 RID: 883
		internal const string PerspectivesElem = "Perspectives";

		// Token: 0x04000374 RID: 884
		private static readonly CustomProperty UdmBindingProperty = new CustomProperty(new QName("UdmBinding", "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling/udmbinding"), true);

		// Token: 0x04000375 RID: 885
		private string m_version;

		// Token: 0x04000376 RID: 886
		private CultureInfo m_culture;

		// Token: 0x04000377 RID: 887
		private DataSourceView m_dsv;

		// Token: 0x04000378 RID: 888
		private ModelEntityFolderItemCollection __entities;

		// Token: 0x04000379 RID: 889
		private PerspectiveCollection __perspectives;

		// Token: 0x0400037A RID: 890
		private readonly XmlNamespacePrefixCollection m_namespacePrefixes = new XmlNamespacePrefixCollection();

		// Token: 0x0400037B RID: 891
		private readonly Dictionary<QName, ModelItem> m_allItems = new Dictionary<QName, ModelItem>();

		// Token: 0x0400037C RID: 892
		private readonly Predicate<ModelItem> m_includeItem;

		// Token: 0x0400037D RID: 893
		private readonly Predicate<ModelItem> m_itemInPerspective;

		// Token: 0x0400037E RID: 894
		private bool m_skipLookupBaseModelItem;

		// Token: 0x0400037F RID: 895
		private readonly SemanticModel.LookupItemStack m_lookupItemStack = new SemanticModel.LookupItemStack();

		// Token: 0x04000380 RID: 896
		private static bool m_suppressDrillthroughDuringLazyClone = false;

		// Token: 0x0200017C RID: 380
		private class LookupItemStack : Stack<SemanticModel.LookupItemFrame>
		{
			// Token: 0x06000FE3 RID: 4067 RVA: 0x00031DD4 File Offset: 0x0002FFD4
			internal LookupItemStack()
			{
			}

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00031DDC File Offset: 0x0002FFDC
			public SemanticModel.LookupItemFrame CurrentFrame
			{
				get
				{
					return base.Peek();
				}
			}

			// Token: 0x06000FE5 RID: 4069 RVA: 0x00031DE4 File Offset: 0x0002FFE4
			public ModelItem FindItem(QName id)
			{
				foreach (SemanticModel.LookupItemFrame lookupItemFrame in this)
				{
					ModelItem modelItem = lookupItemFrame.FindNewItem(id);
					if (modelItem != null)
					{
						return modelItem;
					}
				}
				return null;
			}
		}

		// Token: 0x0200017D RID: 381
		private class LookupItemFrame
		{
			// Token: 0x06000FE6 RID: 4070 RVA: 0x00031E3C File Offset: 0x0003003C
			internal LookupItemFrame(ModelItem newItem)
			{
				this.m_newItems = new List<ModelItem>(4);
				this.AddNewItem(newItem);
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00031E57 File Offset: 0x00030057
			public bool HasDependencies
			{
				get
				{
					return this.m_dependencies != null && this.m_dependencies.Count > 0;
				}
			}

			// Token: 0x06000FE8 RID: 4072 RVA: 0x00031E71 File Offset: 0x00030071
			public void AddDependency(QName id)
			{
				if (this.FindNewItem(id) == null)
				{
					if (this.m_dependencies == null)
					{
						this.m_dependencies = new List<QName>(4);
					}
					if (!this.m_dependencies.Contains(id))
					{
						this.m_dependencies.Add(id);
					}
				}
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x00031EAA File Offset: 0x000300AA
			private void AddNewItem(ModelItem newItem)
			{
				this.m_newItems.Add(newItem);
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x00031EB8 File Offset: 0x000300B8
			public void AddDependenciesFrom(SemanticModel.LookupItemFrame frame)
			{
				IList<QName> dependencies = frame.GetDependencies();
				for (int i = 0; i < dependencies.Count; i++)
				{
					this.AddDependency(dependencies[i]);
				}
			}

			// Token: 0x06000FEB RID: 4075 RVA: 0x00031EEC File Offset: 0x000300EC
			public void AddNewItemsFrom(SemanticModel.LookupItemFrame frame)
			{
				IList<ModelItem> newItems = frame.GetNewItems();
				for (int i = 0; i < newItems.Count; i++)
				{
					this.AddNewItem(newItems[i]);
				}
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x00031F20 File Offset: 0x00030120
			private IList<QName> GetDependencies()
			{
				IList<QName> dependencies = this.m_dependencies;
				return dependencies ?? SemanticModel.LookupItemFrame.EmptyQNameArray;
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x00031F3E File Offset: 0x0003013E
			public List<ModelItem> GetNewItems()
			{
				return this.m_newItems;
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x00031F48 File Offset: 0x00030148
			internal ModelItem FindNewItem(QName id)
			{
				for (int i = 0; i < this.m_newItems.Count; i++)
				{
					if (this.m_newItems[i].ID == id)
					{
						return this.m_newItems[i];
					}
				}
				return null;
			}

			// Token: 0x0400068F RID: 1679
			private const int DefaultListCapacity = 4;

			// Token: 0x04000690 RID: 1680
			private static readonly QName[] EmptyQNameArray = new QName[0];

			// Token: 0x04000691 RID: 1681
			private readonly List<ModelItem> m_newItems;

			// Token: 0x04000692 RID: 1682
			private List<QName> m_dependencies;
		}
	}
}
