using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200018F RID: 399
	[ServiceObjectDefinition("Folder")]
	public class Folder : ServiceObject
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x0003318A File Offset: 0x0003218A
		public Folder(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00033193 File Offset: 0x00032193
		public static Folder Bind(ExchangeService service, FolderId id, PropertySet propertySet)
		{
			return service.BindToFolder<Folder>(id, propertySet);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003319D File Offset: 0x0003219D
		public static Folder Bind(ExchangeService service, FolderId id)
		{
			return Folder.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000331AB File Offset: 0x000321AB
		public static Folder Bind(ExchangeService service, WellKnownFolderName name, PropertySet propertySet)
		{
			return Folder.Bind(service, new FolderId(name), propertySet);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000331BA File Offset: 0x000321BA
		public static Folder Bind(ExchangeService service, WellKnownFolderName name)
		{
			return Folder.Bind(service, new FolderId(name), PropertySet.FirstClassProperties);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000331CD File Offset: 0x000321CD
		internal override void Validate()
		{
			base.Validate();
			if (base.PropertyBag.Contains(FolderSchema.Permissions))
			{
				this.Permissions.Validate();
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000331F2 File Offset: 0x000321F2
		internal override ServiceObjectSchema GetSchema()
		{
			return FolderSchema.Instance;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000331F9 File Offset: 0x000321F9
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000331FC File Offset: 0x000321FC
		internal override string GetChangeXmlElementName()
		{
			return "FolderChange";
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00033203 File Offset: 0x00032203
		internal override string GetSetFieldXmlElementName()
		{
			return "SetFolderField";
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003320A File Offset: 0x0003220A
		internal override string GetDeleteFieldXmlElementName()
		{
			return "DeleteFolderField";
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00033211 File Offset: 0x00032211
		internal override void InternalLoad(PropertySet propertySet)
		{
			base.ThrowIfThisIsNew();
			base.Service.LoadPropertiesForFolder(this, propertySet);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00033226 File Offset: 0x00032226
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			base.ThrowIfThisIsNew();
			base.Service.DeleteFolder(this.Id, deleteMode);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00033240 File Offset: 0x00032240
		public void Delete(DeleteMode deleteMode)
		{
			this.InternalDelete(deleteMode, default(SendCancellationsMode?), default(AffectedTaskOccurrence?));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00033266 File Offset: 0x00032266
		public void Empty(DeleteMode deleteMode, bool deleteSubFolders)
		{
			base.ThrowIfThisIsNew();
			base.Service.EmptyFolder(this.Id, deleteMode, deleteSubFolders);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00033281 File Offset: 0x00032281
		public void MarkAllItemsAsRead(bool suppressReadReceipts)
		{
			base.ThrowIfThisIsNew();
			base.Service.MarkAllItemsAsRead(this.Id, true, suppressReadReceipts);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003329C File Offset: 0x0003229C
		public void MarkAllItemsAsUnread(bool suppressReadReceipts)
		{
			base.ThrowIfThisIsNew();
			base.Service.MarkAllItemsAsRead(this.Id, false, suppressReadReceipts);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000332B7 File Offset: 0x000322B7
		public void Save(FolderId parentFolderId)
		{
			base.ThrowIfThisIsNotNew();
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			if (base.IsDirty)
			{
				base.Service.CreateFolder(this, parentFolderId);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000332DF File Offset: 0x000322DF
		public void Save(WellKnownFolderName parentFolderName)
		{
			this.Save(new FolderId(parentFolderName));
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000332ED File Offset: 0x000322ED
		public void Update()
		{
			if (base.IsDirty && base.PropertyBag.GetIsUpdateCallNecessary())
			{
				base.Service.UpdateFolder(this);
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00033310 File Offset: 0x00032310
		public Folder Copy(FolderId destinationFolderId)
		{
			base.ThrowIfThisIsNew();
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return base.Service.CopyFolder(this.Id, destinationFolderId);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00033335 File Offset: 0x00032335
		public Folder Copy(WellKnownFolderName destinationFolderName)
		{
			return this.Copy(new FolderId(destinationFolderName));
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00033343 File Offset: 0x00032343
		public Folder Move(FolderId destinationFolderId)
		{
			base.ThrowIfThisIsNew();
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return base.Service.MoveFolder(this.Id, destinationFolderId);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00033368 File Offset: 0x00032368
		public Folder Move(WellKnownFolderName destinationFolderName)
		{
			return this.Move(new FolderId(destinationFolderName));
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00033378 File Offset: 0x00032378
		internal ServiceResponseCollection<FindItemResponse<TItem>> InternalFindItems<TItem>(string queryString, ViewBase view, Grouping groupBy) where TItem : Item
		{
			base.ThrowIfThisIsNew();
			return base.Service.FindItems<TItem>(new FolderId[] { this.Id }, null, queryString, view, groupBy, ServiceErrorHandling.ThrowOnError);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000333AC File Offset: 0x000323AC
		internal ServiceResponseCollection<FindItemResponse<TItem>> InternalFindItems<TItem>(SearchFilter searchFilter, ViewBase view, Grouping groupBy) where TItem : Item
		{
			base.ThrowIfThisIsNew();
			return base.Service.FindItems<TItem>(new FolderId[] { this.Id }, searchFilter, null, view, groupBy, ServiceErrorHandling.ThrowOnError);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000333E0 File Offset: 0x000323E0
		public FindItemsResults<Item> FindItems(SearchFilter searchFilter, ItemView view)
		{
			EwsUtilities.ValidateParamAllowNull(searchFilter, "searchFilter");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.InternalFindItems<Item>(searchFilter, view, null);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00033410 File Offset: 0x00032410
		public FindItemsResults<Item> FindItems(string queryString, ItemView view)
		{
			EwsUtilities.ValidateParamAllowNull(queryString, "queryString");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.InternalFindItems<Item>(queryString, view, null);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00033440 File Offset: 0x00032440
		public FindItemsResults<Item> FindItems(ItemView view)
		{
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.InternalFindItems<Item>(null, view, null);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00033464 File Offset: 0x00032464
		public GroupedFindItemsResults<Item> FindItems(SearchFilter searchFilter, ItemView view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			EwsUtilities.ValidateParamAllowNull(searchFilter, "searchFilter");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.InternalFindItems<Item>(searchFilter, view, groupBy);
			return serviceResponseCollection[0].GroupedFindResults;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000334A0 File Offset: 0x000324A0
		public GroupedFindItemsResults<Item> FindItems(string queryString, ItemView view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			ServiceResponseCollection<FindItemResponse<Item>> serviceResponseCollection = this.InternalFindItems<Item>(queryString, view, groupBy);
			return serviceResponseCollection[0].GroupedFindResults;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000334CE File Offset: 0x000324CE
		public FindFoldersResults FindFolders(FolderView view)
		{
			base.ThrowIfThisIsNew();
			return base.Service.FindFolders(this.Id, view);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000334E8 File Offset: 0x000324E8
		public FindFoldersResults FindFolders(SearchFilter searchFilter, FolderView view)
		{
			base.ThrowIfThisIsNew();
			return base.Service.FindFolders(this.Id, searchFilter, view);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00033503 File Offset: 0x00032503
		public GroupedFindItemsResults<Item> FindItems(ItemView view, Grouping groupBy)
		{
			EwsUtilities.ValidateParam(groupBy, "groupBy");
			return this.FindItems(null, view, groupBy);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00033519 File Offset: 0x00032519
		internal override PropertyDefinition GetIdPropertyDefinition()
		{
			return FolderSchema.Id;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00033520 File Offset: 0x00032520
		public void SetExtendedProperty(ExtendedPropertyDefinition extendedPropertyDefinition, object value)
		{
			this.ExtendedProperties.SetExtendedProperty(extendedPropertyDefinition, value);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003352F File Offset: 0x0003252F
		public bool RemoveExtendedProperty(ExtendedPropertyDefinition extendedPropertyDefinition)
		{
			return this.ExtendedProperties.RemoveExtendedProperty(extendedPropertyDefinition);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003353D File Offset: 0x0003253D
		internal override ExtendedPropertyCollection GetExtendedProperties()
		{
			return this.ExtendedProperties;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00033545 File Offset: 0x00032545
		public FolderId Id
		{
			get
			{
				return (FolderId)base.PropertyBag[this.GetIdPropertyDefinition()];
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0003355D File Offset: 0x0003255D
		public FolderId ParentFolderId
		{
			get
			{
				return (FolderId)base.PropertyBag[FolderSchema.ParentFolderId];
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00033574 File Offset: 0x00032574
		public int ChildFolderCount
		{
			get
			{
				return (int)base.PropertyBag[FolderSchema.ChildFolderCount];
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0003358B File Offset: 0x0003258B
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x000335A2 File Offset: 0x000325A2
		public string DisplayName
		{
			get
			{
				return (string)base.PropertyBag[FolderSchema.DisplayName];
			}
			set
			{
				base.PropertyBag[FolderSchema.DisplayName] = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x000335B5 File Offset: 0x000325B5
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x000335CC File Offset: 0x000325CC
		public string FolderClass
		{
			get
			{
				return (string)base.PropertyBag[FolderSchema.FolderClass];
			}
			set
			{
				base.PropertyBag[FolderSchema.FolderClass] = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x000335DF File Offset: 0x000325DF
		public int TotalCount
		{
			get
			{
				return (int)base.PropertyBag[FolderSchema.TotalCount];
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x000335F6 File Offset: 0x000325F6
		public ExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return (ExtendedPropertyCollection)base.PropertyBag[ServiceObjectSchema.ExtendedProperties];
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0003360D File Offset: 0x0003260D
		public ManagedFolderInformation ManagedFolderInformation
		{
			get
			{
				return (ManagedFolderInformation)base.PropertyBag[FolderSchema.ManagedFolderInformation];
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00033624 File Offset: 0x00032624
		public EffectiveRights EffectiveRights
		{
			get
			{
				return (EffectiveRights)base.PropertyBag[FolderSchema.EffectiveRights];
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0003363B File Offset: 0x0003263B
		public FolderPermissionCollection Permissions
		{
			get
			{
				return (FolderPermissionCollection)base.PropertyBag[FolderSchema.Permissions];
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00033652 File Offset: 0x00032652
		public int UnreadCount
		{
			get
			{
				return (int)base.PropertyBag[FolderSchema.UnreadCount];
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00033669 File Offset: 0x00032669
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x00033680 File Offset: 0x00032680
		public PolicyTag PolicyTag
		{
			get
			{
				return (PolicyTag)base.PropertyBag[FolderSchema.PolicyTag];
			}
			set
			{
				base.PropertyBag[FolderSchema.PolicyTag] = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00033693 File Offset: 0x00032693
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x000336AA File Offset: 0x000326AA
		public ArchiveTag ArchiveTag
		{
			get
			{
				return (ArchiveTag)base.PropertyBag[FolderSchema.ArchiveTag];
			}
			set
			{
				base.PropertyBag[FolderSchema.ArchiveTag] = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x000336BD File Offset: 0x000326BD
		public WellKnownFolderName? WellKnownFolderName
		{
			get
			{
				return (WellKnownFolderName?)base.PropertyBag[FolderSchema.WellKnownFolderName];
			}
		}
	}
}
