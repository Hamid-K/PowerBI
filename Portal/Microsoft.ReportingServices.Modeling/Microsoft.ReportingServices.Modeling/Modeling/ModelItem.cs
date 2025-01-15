using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200008D RID: 141
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ModelItem : ExtensibleModelingObject, IXmlLoadable, IDeserializationCallback, IXmlWriteable, IValidationScope, IPersistable
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x000140D2 File Offset: 0x000122D2
		protected ModelItem()
		{
			this.Reset();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000140E0 File Offset: 0x000122E0
		internal virtual void Reset()
		{
			base.CheckWriteable();
			this.__name = string.Empty;
			this.m_description = string.Empty;
			this.m_hidden = false;
			base.CustomProperties = null;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001410C File Offset: 0x0001230C
		internal ModelItem(ModelItem baseItem, bool markAsHidden)
		{
			if (baseItem == null)
			{
				throw new ArgumentNullException("baseItem");
			}
			if (baseItem.ParentItem == null && !(baseItem is SemanticModel))
			{
				throw new InternalModelingException("baseItem has no ParentItem and is not Model");
			}
			if (!baseItem.IsCompiled)
			{
				throw new InvalidOperationException(DevExceptionMessages.ModelItem_BaseItemMustBeCompiled);
			}
			if (!baseItem.CustomProperties.IsReadOnly)
			{
				throw new InternalModelingException("baseItem.CustomProperties is not read-only");
			}
			this.__id = baseItem.__id;
			this.__name = baseItem.__name;
			this.m_description = baseItem.m_description;
			this.m_hidden = markAsHidden || baseItem.m_hidden;
			base.CustomProperties = baseItem.CustomProperties;
			this.m_baseItem = baseItem;
			base.SetCompiledIndicator();
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000141C2 File Offset: 0x000123C2
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000141D0 File Offset: 0x000123D0
		public QName ID
		{
			get
			{
				this.InitializeID();
				return this.__id;
			}
			set
			{
				if (this.m_parentItem != null && this.__id != value)
				{
					throw new InvalidOperationException();
				}
				this.SetIDCore(value, false);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000141F6 File Offset: 0x000123F6
		internal void ChangeID(QName newID)
		{
			this.SetIDCore(newID, true);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00014200 File Offset: 0x00012400
		private void SetIDCore(QName newID, bool updateModelLink)
		{
			base.CheckWriteable();
			if (updateModelLink && this.Model != null && this.Model != this)
			{
				this.Model.UpdateLinkedItemID(this, newID);
			}
			this.__id = ModelItem.CanonicalizeID(newID, true);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00014236 File Offset: 0x00012436
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001423E File Offset: 0x0001243E
		public virtual string Name
		{
			get
			{
				return this.__name;
			}
			set
			{
				base.CheckWriteable();
				this.__name = value ?? string.Empty;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00014256 File Offset: 0x00012456
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001425E File Offset: 0x0001245E
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				base.CheckWriteable();
				this.m_description = value ?? string.Empty;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00014276 File Offset: 0x00012476
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001427E File Offset: 0x0001247E
		public virtual bool Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				base.CheckWriteable();
				this.m_hidden = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001428D File Offset: 0x0001248D
		public bool IsInvalidRefTarget
		{
			get
			{
				return this.Model == null;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00014298 File Offset: 0x00012498
		public virtual SemanticModel Model
		{
			get
			{
				if (this.m_parentItem != null)
				{
					return this.m_parentItem.Model;
				}
				return null;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000142AF File Offset: 0x000124AF
		public ModelItem ParentItem
		{
			get
			{
				return this.m_parentItem;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000142B7 File Offset: 0x000124B7
		public IOwnedModelItemCollection OwnerCollection
		{
			get
			{
				return this.m_ownerCollection;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x000142BF File Offset: 0x000124BF
		public bool IsLazyClone
		{
			get
			{
				return this.m_baseItem != null;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000659 RID: 1625
		internal abstract string ElementName { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x000142CA File Offset: 0x000124CA
		internal virtual bool IsNamed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000142CD File Offset: 0x000124CD
		internal virtual bool ShouldSerializeName
		{
			get
			{
				return this.IsNamed;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000142D5 File Offset: 0x000124D5
		internal ModelItem BaseItem
		{
			get
			{
				return this.m_baseItem;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000142E0 File Offset: 0x000124E0
		public override string ToString()
		{
			if (this.Name.Length <= 0)
			{
				return this.ID.ToString();
			}
			return this.Name;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00014318 File Offset: 0x00012518
		public bool IsValidParentItem(ModelItem newParentItem)
		{
			ValidationMessage validationMessage;
			return this.IsValidParentItem(newParentItem, out validationMessage);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001432E File Offset: 0x0001252E
		public virtual bool IsValidParentItem(ModelItem newParentItem, out ValidationMessage message)
		{
			message = new ValidationMessage(ModelingErrorCode.None, Severity.Error, this, DevExceptionMessages.ModelItem_InvalidParentItemType(this.ElementName, newParentItem.ElementName));
			return false;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001434C File Offset: 0x0001254C
		public virtual IEnumerable<ModelItem> GetOwnedItems()
		{
			return ModelItem.EmptyArray;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00014353 File Offset: 0x00012553
		public virtual IEnumerable<ModelItem> GetNamespaceItems()
		{
			if (this.m_parentItem != null)
			{
				return this.m_parentItem.GetOwnedItems();
			}
			return ModelItem.EmptyArray;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00014370 File Offset: 0x00012570
		public bool IsNameUnique()
		{
			foreach (ModelItem modelItem in this.GetNamespaceItems())
			{
				if (modelItem != this && ModelItem.CompareNames(modelItem.Name, this.Name) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000143D4 File Offset: 0x000125D4
		public static QName NewID()
		{
			return new QName("G" + Guid.NewGuid().ToString());
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00014403 File Offset: 0x00012603
		public static string IDToString(QName id)
		{
			return id.ToString();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00014412 File Offset: 0x00012612
		public static QName StringToID(string id)
		{
			return QName.Parse(id);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001441A File Offset: 0x0001261A
		public static string CanonicalizeID(string id)
		{
			return ModelItem.IDToString(ModelItem.CanonicalizeID(ModelItem.StringToID(id)));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001442C File Offset: 0x0001262C
		public static QName CanonicalizeID(QName id)
		{
			return ModelItem.CanonicalizeID(id, false);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00014438 File Offset: 0x00012638
		internal static QName CanonicalizeID(QName id, bool skipCheck)
		{
			QName.Verify(id);
			if ("http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling" == id.Namespace || SemanticModelingSchema.PreviousNamespaces.Contains(id.Namespace))
			{
				id.Namespace = string.Empty;
			}
			if (!skipCheck)
			{
				ModelItem.CheckID(id, null);
			}
			return id;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00014489 File Offset: 0x00012689
		internal static XmlQualifiedName GetXmlQualifiedName(QName id)
		{
			if (id.Namespace.Length == 0)
			{
				id.Namespace = "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling";
			}
			return new XmlQualifiedName(id.Name, id.Namespace);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000144B8 File Offset: 0x000126B8
		public static int CompareNames(string nameA, string nameB)
		{
			return string.Compare(nameA, nameB, StringComparison.Ordinal);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000144C4 File Offset: 0x000126C4
		public static void VisitAllItems(ModelItem root, Action<ModelItem> visitor)
		{
			if (root == null)
			{
				throw new InternalModelingException("root is null");
			}
			visitor(root);
			foreach (ModelItem modelItem in root.GetOwnedItems())
			{
				ModelItem.VisitAllItems(modelItem, visitor);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00014524 File Offset: 0x00012724
		internal static Predicate<ModelItem> NameMatch(string name)
		{
			return ModelItem.NameMatch<ModelItem>(name);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001452C File Offset: 0x0001272C
		internal static Predicate<T> NameMatch<T>(string name) where T : ModelItem
		{
			return (T item) => ModelItem.CompareNames(item.Name, name) == 0;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00014545 File Offset: 0x00012745
		internal void LoadFragment(IXPathNavigable fragment)
		{
			if (fragment == null)
			{
				throw new ArgumentNullException("fragment");
			}
			this.LoadFragment(fragment.CreateNavigator().ReadSubtree());
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00014568 File Offset: 0x00012768
		internal void LoadFragment(XmlReader xr)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			base.CheckWriteable();
			DeserializationContext deserializationContext = new DeserializationContext(this.Model, null);
			ModelingXmlReader modelingXmlReader = new ModelingXmlReader(xr, SemanticModelingSchema.Fragment, deserializationContext);
			this.Load(modelingXmlReader, true);
			deserializationContext.Validation.ThrowIfErrors();
			deserializationContext.CompleteLoad();
			deserializationContext.Validation.ThrowIfErrors();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000145C7 File Offset: 0x000127C7
		internal void Load(ModelingXmlReader xr)
		{
			this.Load(xr, false);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000145D4 File Offset: 0x000127D4
		internal void Load(ModelingXmlReader xr, bool fragment)
		{
			base.CheckWriteable();
			xr.Validation.PushScope(this);
			try
			{
				this.LoadCore(xr, fragment);
			}
			finally
			{
				xr.Validation.PopScope();
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001461C File Offset: 0x0001281C
		internal virtual void LoadCore(ModelingXmlReader xr, bool fragment)
		{
			xr.LoadObject(this.ElementName, this);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001462B File Offset: 0x0001282B
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return this.LoadXmlAttribute(xr);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00014634 File Offset: 0x00012834
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			return this.LoadXmlElement(xr);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001463D File Offset: 0x0001283D
		internal virtual bool LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "ID")
			{
				this.ID = xr.ReadValueAsID();
				return true;
			}
			return false;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00014668 File Offset: 0x00012868
		internal virtual bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "Name")
				{
					this.Name = xr.ReadValueAsString();
					return true;
				}
				if (localName == "Description")
				{
					this.m_description = xr.ReadValueAsString();
					return true;
				}
				if (localName == "Hidden")
				{
					this.m_hidden = xr.ReadValueAsBoolean();
					return true;
				}
				if (localName == "CustomProperties")
				{
					base.CustomProperties.Load(xr);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000146F3 File Offset: 0x000128F3
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			return this.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000146FD File Offset: 0x000128FD
		internal virtual bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			return false;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00014700 File Offset: 0x00012900
		internal static T CreateItem<T>(string elementName) where T : ModelItem
		{
			ModelItem modelItem = null;
			if (!(elementName == "EntityFolder"))
			{
				if (!(elementName == "Entity"))
				{
					if (!(elementName == "FieldFolder"))
					{
						if (!(elementName == "Attribute"))
						{
							if (!(elementName == "Role"))
							{
								if (elementName == "Perspective")
								{
									modelItem = new Perspective();
								}
							}
							else
							{
								modelItem = new ModelRole();
							}
						}
						else
						{
							modelItem = new ModelAttribute();
						}
					}
					else
					{
						modelItem = new ModelFieldFolder();
					}
				}
				else
				{
					modelItem = new ModelEntity();
				}
			}
			else
			{
				modelItem = new ModelEntityFolder();
			}
			if (modelItem is T)
			{
				return (T)((object)modelItem);
			}
			return default(T);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000147A5 File Offset: 0x000129A5
		internal static T CreateItem<T>() where T : ModelItem
		{
			return ModelItem.CreateItem<T>(ModelItem.GetConcreteElementNamesArray(typeof(T))[0]);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000147BD File Offset: 0x000129BD
		internal static string GetConcreteElementNames(Type type)
		{
			return string.Join(",", ModelItem.GetConcreteElementNamesArray(type));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000147D0 File Offset: 0x000129D0
		private static string[] GetConcreteElementNamesArray(Type type)
		{
			if (type == typeof(SemanticModel))
			{
				return ModelItem.MakeElementList(new string[] { "SemanticModel" });
			}
			if (type == typeof(ModelEntityFolderItem))
			{
				return ModelItem.MakeElementList(new string[] { "Entity", "EntityFolder" });
			}
			if (type == typeof(ModelEntity))
			{
				return ModelItem.MakeElementList(new string[] { "Entity" });
			}
			if (type == typeof(ModelEntityFolder))
			{
				return ModelItem.MakeElementList(new string[] { "EntityFolder" });
			}
			if (type == typeof(ModelFieldFolderItem))
			{
				return ModelItem.MakeElementList(new string[] { "Attribute", "Role", "FieldFolder" });
			}
			if (type == typeof(ModelField))
			{
				return ModelItem.MakeElementList(new string[] { "Attribute", "Role" });
			}
			if (type == typeof(ModelAttribute))
			{
				return ModelItem.MakeElementList(new string[] { "Attribute" });
			}
			if (type == typeof(ModelRole))
			{
				return ModelItem.MakeElementList(new string[] { "Role" });
			}
			if (type == typeof(ModelFieldFolder))
			{
				return ModelItem.MakeElementList(new string[] { "FieldFolder" });
			}
			throw new InternalModelingException("Unknown type for ModelItem.GetConcreteElementNames '" + ((type != null) ? type.ToString() : null) + "'");
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00014974 File Offset: 0x00012B74
		private static string[] MakeElementList(params string[] elementNames)
		{
			return elementNames;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00014977 File Offset: 0x00012B77
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement(this.ElementName);
			this.WriteXmlAttributes(xw);
			this.WriteXmlElements(xw);
			base.WriteCustomProperties(xw);
			xw.WriteEndElement();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000149A0 File Offset: 0x00012BA0
		internal virtual void WriteXmlAttributes(ModelingXmlWriter xw)
		{
			xw.WriteAttribute("ID", this.ID);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000149B8 File Offset: 0x00012BB8
		internal virtual void WriteXmlElements(ModelingXmlWriter xw)
		{
			if (this.ShouldSerializeName)
			{
				this.WriteName(xw);
			}
			xw.WriteElementIfNonDefault<string>("Description", this.m_description);
			xw.WriteElementIfNonDefault<bool>("Hidden", this.m_hidden);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000149EB File Offset: 0x00012BEB
		internal void WriteName(ModelingXmlWriter xw)
		{
			xw.WriteElement("Name", this.Name);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000149FE File Offset: 0x00012BFE
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00014A08 File Offset: 0x00012C08
		public ValidationMessageCollection Validate(bool throwOnError)
		{
			CompilationContext compilationContext = new CompilationContext(false, false);
			this.Compile(compilationContext);
			if (throwOnError)
			{
				compilationContext.ThrowIfErrors();
			}
			return compilationContext.GetMessages();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00014A34 File Offset: 0x00012C34
		internal virtual void Compile(CompilationContext ctx)
		{
			ctx.PushScope(this);
			try
			{
				this.CompileCore(ctx);
				this.CheckOwnedItemsNameUniqueness(ctx);
				foreach (ModelItem modelItem in this.GetOwnedItems())
				{
					modelItem.Compile(ctx);
				}
			}
			finally
			{
				ctx.PopScope();
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00014AA8 File Offset: 0x00012CA8
		internal virtual void CheckOwnedItemsNameUniqueness(CompilationContext ctx)
		{
			ctx.CheckNameUniqueness<ModelItem>(this.GetOwnedItems(), ModelingErrorCode.DuplicateItemName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateItemName));
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00014AC8 File Offset: 0x00012CC8
		internal virtual void CompileCore(CompilationContext ctx)
		{
			this.InitializeID();
			ModelItem.CheckID(this.ID, ctx);
			base.Compile(ctx.ShouldPersist);
			if (this.IsNamed && this.Name.Length == 0)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingItemName, SRErrors.MissingItemName(ctx.CurrentObjectDescriptor));
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00014B1C File Offset: 0x00012D1C
		private static void CheckID(QName id, CompilationContext ctx)
		{
			if (id.Name.Length > 250)
			{
				if (ctx == null)
				{
					throw new ValidationException(ModelingErrorCode.IDLocalNameLengthExceeded, SRErrors.IDLocalNameLengthExceeded_NoContext(250));
				}
				ctx.AddScopedError(ModelingErrorCode.IDLocalNameLengthExceeded, SRErrors.IDLocalNameLengthExceeded(ctx.CurrentObjectDescriptor, 250));
			}
			if (id.Namespace.Length > 150)
			{
				if (ctx == null)
				{
					throw new ValidationException(ModelingErrorCode.IDNamespaceLengthExceeded, SRErrors.IDNamespaceLengthExceeded_NoContext(150));
				}
				ctx.AddScopedError(ModelingErrorCode.IDNamespaceLengthExceeded, SRErrors.IDNamespaceLengthExceeded(ctx.CurrentObjectDescriptor, 150));
			}
			if (id.Namespace.Length != 0 || ModelItem.m_guidRegex.IsMatch(id.Name))
			{
				return;
			}
			if (ctx != null)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidGuid, SRErrors.InvalidGuid(ctx.CurrentObjectDescriptor));
				return;
			}
			throw new ValidationException(ModelingErrorCode.InvalidGuid, SRErrors.InvalidGuid_NoContext);
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00014BF2 File Offset: 0x00012DF2
		string IValidationScope.ObjectType
		{
			get
			{
				return this.ElementName;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00014BFC File Offset: 0x00012DFC
		string IValidationScope.ObjectID
		{
			get
			{
				return this.ID.ToString();
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00014C20 File Offset: 0x00012E20
		string IValidationScope.ObjectName
		{
			get
			{
				if (!this.IsNamed)
				{
					return null;
				}
				if (this.Name.Length <= 0)
				{
					return this.ID.ToString();
				}
				return this.Name;
			}
		}

		// Token: 0x0600068B RID: 1675
		internal abstract ModelItem CreateLazyClone(bool markAsHidden);

		// Token: 0x0600068C RID: 1676 RVA: 0x00014C60 File Offset: 0x00012E60
		internal virtual bool ResolveRequiredReferences(SemanticModel newModel)
		{
			if (newModel == null)
			{
				throw new InternalModelingException("newModel is null");
			}
			if (this.m_baseItem == null || this.m_baseItem.ParentItem == null)
			{
				throw new InternalModelingException("m_baseItem is null or m_baseItem.ParentItem is null");
			}
			ModelItem modelItem = newModel.LookupItemByID(this.m_baseItem.ParentItem.ID, this);
			if (modelItem == null)
			{
				return false;
			}
			this.SetParentItem(modelItem, true);
			return true;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00014CC4 File Offset: 0x00012EC4
		internal void SetParentItem(ModelItem newParentItem, bool skipWriteableCheck)
		{
			if (!skipWriteableCheck)
			{
				base.CheckWriteable();
			}
			if (newParentItem != null)
			{
				if (this.m_parentItem != null)
				{
					throw new InternalModelingException("Item has existing m_parentItem");
				}
				ValidationMessage validationMessage;
				if (!this.IsValidParentItem(newParentItem, out validationMessage))
				{
					if (validationMessage.Code == ModelingErrorCode.None)
					{
						throw new InvalidOperationException(validationMessage.Message);
					}
					throw new ValidationException(validationMessage);
				}
			}
			this.m_parentItem = newParentItem;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00014D20 File Offset: 0x00012F20
		internal void SetOwnerCollection(IOwnedModelItemCollection newOwnerCollection)
		{
			if (newOwnerCollection != null)
			{
				if (this.m_parentItem != newOwnerCollection.ParentItem)
				{
					throw new InternalModelingException("m_parentItem does not match newOwnerCollection.ParentItem");
				}
				if (!newOwnerCollection.CanContain(this))
				{
					throw new InternalModelingException("item cannot be contained by newOwnerCollection");
				}
				if (this.m_ownerCollection != null)
				{
					throw new InternalModelingException("item already has an OwnerCollection");
				}
			}
			this.m_ownerCollection = newOwnerCollection;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00014D78 File Offset: 0x00012F78
		private void InitializeID()
		{
			if (this.__id.Name.Length == 0)
			{
				if (this.__id.Namespace.Length != 0)
				{
					throw new InternalModelingException("__id.Name is empty but Namespace is non-empty");
				}
				if (base.IsCompiled)
				{
					throw new InternalModelingException("__id is empty on compiled item");
				}
				this.__id = ModelItem.NewID();
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00014DD2 File Offset: 0x00012FD2
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00014DDC File Offset: 0x00012FDC
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelItem.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Description)
					{
						if (memberName != MemberName.Hidden)
						{
							if (!PersistenceHelper.WriteQName(ref writer, this.ID))
							{
								throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
							}
						}
						else
						{
							writer.Write(this.m_hidden);
						}
					}
					else
					{
						writer.Write(this.m_description);
					}
				}
				else
				{
					writer.Write(this.__name);
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00014E90 File Offset: 0x00013090
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00014E9C File Offset: 0x0001309C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ModelItem.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Description)
					{
						if (memberName != MemberName.Hidden)
						{
							if (!PersistenceHelper.ReadQName(ref reader, ref this.__id))
							{
								throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
							}
						}
						else
						{
							this.m_hidden = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_description = reader.ReadString();
					}
				}
				else
				{
					this.__name = reader.ReadString();
				}
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00014F50 File Offset: 0x00013150
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			this.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06000695 RID: 1685
		internal abstract void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		// Token: 0x06000696 RID: 1686 RVA: 0x00014F5A File Offset: 0x0001315A
		ObjectType IPersistable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x06000697 RID: 1687
		internal abstract ObjectType GetObjectType();

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00014F62 File Offset: 0x00013162
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelItem.__declaration, ModelItem.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					PersistenceHelper.DeclareQName(list);
					list.Add(new MemberInfo(MemberName.Name, Token.String));
					list.Add(new MemberInfo(MemberName.Description, Token.String));
					list.Add(new MemberInfo(MemberName.Hidden, Token.Boolean));
					return new Declaration(ObjectType.ModelItem, ObjectType.ExtensibleModelingObject, list);
				});
			}
		}

		// Token: 0x0400032E RID: 814
		private const string IdAttr = "ID";

		// Token: 0x0400032F RID: 815
		internal const string NameElem = "Name";

		// Token: 0x04000330 RID: 816
		private const string DescriptionElem = "Description";

		// Token: 0x04000331 RID: 817
		private const string HiddenElem = "Hidden";

		// Token: 0x04000332 RID: 818
		private const int MaxIDNameLength = 250;

		// Token: 0x04000333 RID: 819
		private const int MaxIDNamespaceLength = 150;

		// Token: 0x04000334 RID: 820
		internal static readonly ModelItem[] EmptyArray = new ModelItem[0];

		// Token: 0x04000335 RID: 821
		private static readonly Regex m_guidRegex = new Regex("^G[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000336 RID: 822
		private QName __id;

		// Token: 0x04000337 RID: 823
		private string __name;

		// Token: 0x04000338 RID: 824
		private string m_description;

		// Token: 0x04000339 RID: 825
		private bool m_hidden;

		// Token: 0x0400033A RID: 826
		private ModelItem m_parentItem;

		// Token: 0x0400033B RID: 827
		private IOwnedModelItemCollection m_ownerCollection;

		// Token: 0x0400033C RID: 828
		private readonly ModelItem m_baseItem;

		// Token: 0x0400033D RID: 829
		private static Declaration __declaration;

		// Token: 0x0400033E RID: 830
		private static readonly object __declarationLock = new object();
	}
}
