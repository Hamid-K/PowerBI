using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000E0 RID: 224
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[Designer("Microsoft.AnalysisServices.Design.DatabaseDesigner,Microsoft.AnalysisServices.Design.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", typeof(IRootDesigner))]
	[DesignerCategory("Designer")]
	[DesignerSerializer("Microsoft.DataWarehouse.Serialization.DesignXmlSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.DataWarehouse.Serialization.DesignerComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[DesignerSerializer("Microsoft.AnalysisServices.Project.ComponentModel.DatabaseOnlineSerializer, Microsoft.AnalysisServices.Project.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.DataWarehouse.Serialization.OnlineComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[Guid("335EBEB5-D280-4A95-A133-429AD9E0D33F")]
	public abstract class Database : ProcessableMajorObject, IMajorObject, INamedComponent, IModelComponent, IComponent, IDisposable
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00030F6C File Offset: 0x0002F16C
		Server IMajorObject.ParentServer
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00030F74 File Offset: 0x0002F174
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00030F77 File Offset: 0x0002F177
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteElementString("DatabaseID", base.ID);
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00030F98 File Offset: 0x0002F198
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00030FA0 File Offset: 0x0002F1A0
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00030FA8 File Offset: 0x0002F1A8
		string IMajorObject.Path
		{
			get
			{
				IMajorObject parent = this.Parent;
				return ((parent == null) ? string.Empty : parent.Path) + "<DatabaseID>" + base.ID + "</DatabaseID>";
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00030FE1 File Offset: 0x0002F1E1
		IObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return this.GetObjectReference();
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00030FE9 File Offset: 0x0002F1E9
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && this.Parent == obj;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00030FFC File Offset: 0x0002F1FC
		internal Database()
		{
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00031016 File Offset: 0x0002F216
		internal Database(string name)
			: base(name, name)
		{
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00031032 File Offset: 0x0002F232
		internal Database(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003104E File Offset: 0x0002F24E
		internal Database(ModelType modelType, int compatibilityLevel)
		{
			this.ModelType = modelType;
			this.CompatibilityLevel = compatibilityLevel;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00031076 File Offset: 0x0002F276
		[XmlIgnore]
		[Browsable(false)]
		public new Server Parent
		{
			get
			{
				return this.ParentOrNull;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00031080 File Offset: 0x0002F280
		internal Server ParentOrException
		{
			get
			{
				IModelComponent parent = base.Parent;
				if (parent == null)
				{
					throw Utils.CreateParentMissingException(this, typeof(Server));
				}
				return (Server)parent;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x000310AE File Offset: 0x0002F2AE
		internal Server ParentOrNull
		{
			get
			{
				return (Server)base.Parent;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000310BB File Offset: 0x0002F2BB
		internal override IObjectReference GetObjectReference()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000310C2 File Offset: 0x0002F2C2
		internal override Type GetBaseType()
		{
			return typeof(Database);
		}

		// Token: 0x06000E09 RID: 3593
		internal abstract void SerializeToJsonObject(object jsonObject, object options);

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000310CE File Offset: 0x0002F2CE
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x000310D6 File Offset: 0x0002F2D6
		internal bool IsRemoved { get; set; }

		// Token: 0x06000E0C RID: 3596 RVA: 0x000310E0 File Offset: 0x0002F2E0
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Database database = destination as Database;
			if (database == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			database.LastUpdate = this.LastUpdate;
			database.ImagePath = this.ImagePath;
			database.ImageUniqueID = this.ImageUniqueID;
			database.ImageVersion = this.ImageVersion;
			database.ImageUrl = this.ImageUrl;
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			database.EstimatedSize = this.EstimatedSize;
			database.Visible = this.Visible;
			database.Language = this.Language;
			database.Collation = this.Collation;
			database.StorageEngineUsed = this.StorageEngineUsed;
			database.CompatibilityLevel = this.CompatibilityLevel;
			this.Translations.CopyTo(database.Translations);
			database.ReadWriteMode = this.ReadWriteMode;
			database.DbStorageLocation = this.DbStorageLocation;
			database.Version = this.Version;
			database.IsBlocked = this.IsBlocked;
			if (this.dismissedValidationRules != null)
			{
				this.dismissedValidationRules.Dispose();
				this.dismissedValidationRules = null;
			}
			if (this.dismissedValidationResults != null)
			{
				this.dismissedValidationResults.Dispose();
				this.dismissedValidationResults = null;
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00031224 File Offset: 0x0002F424
		internal override ModelType GetModelType()
		{
			Server parentOrNull = this.ParentOrNull;
			if (parentOrNull != null)
			{
				ServerMode serverMode = parentOrNull.ServerMode;
				if (serverMode == ServerMode.Multidimensional)
				{
					return ModelType.Multidimensional;
				}
				if (serverMode - ServerMode.SharePoint <= 1)
				{
					return ModelType.Tabular;
				}
			}
			return this.ModelType;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00031257 File Offset: 0x0002F457
		internal override int GetCompatibilityLevel()
		{
			if (!base.IsBodyLoadable())
			{
				return 1050;
			}
			return this.GetEffectiveCompatibilityLevel(true);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003126E File Offset: 0x0002F46E
		internal int GetEffectiveCompatibilityLevel(bool includePendingLocalChanges)
		{
			if (includePendingLocalChanges || this.originalCompatibilityLevel == -1)
			{
				return base.GetBody<Database.DatabaseBodyBase>().CompatibilityLevel;
			}
			return this.originalCompatibilityLevel;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003128E File Offset: 0x0002F48E
		[Browsable(false)]
		[XmlIgnore]
		public DismissedValidationRuleCollection DismissedValidationRules
		{
			get
			{
				if (this.dismissedValidationRules == null)
				{
					this.dismissedValidationRules = new DismissedValidationRuleCollection(this.Annotations);
				}
				return this.dismissedValidationRules;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x000312AF File Offset: 0x0002F4AF
		[Browsable(false)]
		[XmlIgnore]
		public DismissedValidationResultCollection DismissedValidationResults
		{
			get
			{
				if (this.dismissedValidationResults == null)
				{
					this.dismissedValidationResults = new DismissedValidationResultCollection(this.Annotations);
				}
				return this.dismissedValidationResults;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000312D0 File Offset: 0x0002F4D0
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x000312D8 File Offset: 0x0002F4D8
		[XmlElement(IsNullable = false)]
		public override string Description
		{
			get
			{
				return base.Description;
			}
			set
			{
				if (string.Compare(base.Description, value, StringComparison.Ordinal) != 0)
				{
					this.OnBodyPropertyChanging();
					base.Description = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000312FC File Offset: 0x0002F4FC
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00031304 File Offset: 0x0002F504
		[ReadOnly(true)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_LastUpdate")]
		public DateTime LastUpdate
		{
			get
			{
				return this.lastUpdate;
			}
			set
			{
				this.lastUpdate = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003130D File Offset: 0x0002F50D
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0003131C File Offset: 0x0002F51C
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().Visible;
			}
			set
			{
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (body.Visible != value)
				{
					this.OnBodyPropertyChanging();
					body.Visible = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003134C File Offset: 0x0002F54C
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x0003135C File Offset: 0x0002F55C
		[DefaultValue(0)]
		[TypeConverter("Microsoft.AnalysisServices.Design.LanguageIdTypeConverter, Microsoft.AnalysisServices.Design.AS")]
		[LocalizedDescription("PropertyDescription_Database_Language")]
		[LocalizedCategory("PropertyCategory_Advanced")]
		public int Language
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().Language;
			}
			set
			{
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (body.Language != value)
				{
					this.OnBodyPropertyChanging();
					body.Language = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0003138C File Offset: 0x0002F58C
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x0003139C File Offset: 0x0002F59C
		[XmlElement(IsNullable = false)]
		[TypeConverter("Microsoft.AnalysisServices.Design.CollationTypeConverter, Microsoft.AnalysisServices.Design.AS")]
		[Editor("Microsoft.AnalysisServices.Design.CollationPropertyTypeEditor, Microsoft.AnalysisServices.Design.AS", typeof(UITypeEditor))]
		[LocalizedDescription("PropertyDescription_Database_Collation")]
		public string Collation
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().Collation;
			}
			set
			{
				value = Utils.Trim(value);
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (string.Compare(body.Collation, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.OnBodyPropertyChanging();
					body.Collation = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000313DA File Offset: 0x0002F5DA
		[XmlArray]
		[Browsable(false)]
		public TranslationCollection Translations
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().Translations;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000313E7 File Offset: 0x0002F5E7
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x000313F4 File Offset: 0x0002F5F4
		[ReadOnly(true)]
		[DefaultValue(ReadWriteMode.ReadWrite)]
		[LocalizedDescription("PropertyDescription_Database_ReadWriteMode")]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2008/engine/100")]
		public ReadWriteMode ReadWriteMode
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().ReadWriteMode;
			}
			set
			{
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (body.ReadWriteMode != value)
				{
					this.OnBodyPropertyChanging();
					body.ReadWriteMode = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00031424 File Offset: 0x0002F624
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00031430 File Offset: 0x0002F630
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2008/engine/100/100")]
		[LocalizedDescription("PropertyDescription_Database_DbStorageLocation")]
		[LocalizedCategory("PropertyCategory_Configurable")]
		[ReadOnly(true)]
		public string DbStorageLocation
		{
			get
			{
				return this.GetDbStorageLocation(true);
			}
			set
			{
				Utils.CheckValidPath(value);
				string text = Utils.Trim(value);
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (!Utils.AreSamePaths(text, body.DbStorageLocation))
				{
					this.OnBodyPropertyChanging();
					base.SetConfigurationValue("DatabaseStorageLocations", new string[] { base.ID }, text);
					body.DbStorageLocation = text;
					this.OnBodyPropertyChange();
					this.OnStorageLocationChange(text);
				}
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00031498 File Offset: 0x0002F698
		internal string GetDbStorageLocation(bool forceHostSettingsCheck)
		{
			if (base.HasID || forceHostSettingsCheck)
			{
				return (string)base.GetConfigurationValue("DatabaseStorageLocations", new string[] { base.ID }, base.GetBody<Database.DatabaseBodyBase>().DbStorageLocation);
			}
			return base.GetBody<Database.DatabaseBodyBase>().DbStorageLocation;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000314E5 File Offset: 0x0002F6E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnStorageLocationChange(string storageLocation)
		{
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000314E7 File Offset: 0x0002F6E7
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x000314EF File Offset: 0x0002F6EF
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200")]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_ImagePath")]
		[ReadOnly(true)]
		public string ImagePath
		{
			get
			{
				return this.imagePath;
			}
			set
			{
				this.imagePath = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000314F8 File Offset: 0x0002F6F8
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00031500 File Offset: 0x0002F700
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200")]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_ImageUniqueID")]
		[ReadOnly(true)]
		public string ImageUniqueID
		{
			get
			{
				return this.imageUniqueId;
			}
			set
			{
				this.imageUniqueId = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00031509 File Offset: 0x0002F709
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x00031511 File Offset: 0x0002F711
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200")]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_ImageVersion")]
		[ReadOnly(true)]
		public string ImageVersion
		{
			get
			{
				return this.imageVersion;
			}
			set
			{
				this.imageVersion = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0003151A File Offset: 0x0002F71A
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x00031522 File Offset: 0x0002F722
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200")]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_ImageUrl")]
		[ReadOnly(true)]
		public string ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0003152B File Offset: 0x0002F72B
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00031538 File Offset: 0x0002F738
		[PropertyOrder(2)]
		[DefaultValue(StorageEngineUsed.Traditional)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_StorageEngineUsed")]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200/200")]
		public StorageEngineUsed StorageEngineUsed
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().StorageEngineUsed;
			}
			set
			{
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (body.StorageEngineUsed != value)
				{
					this.OnBodyPropertyChanging();
					body.StorageEngineUsed = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00031568 File Offset: 0x0002F768
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00031598 File Offset: 0x0002F798
		[Browsable(false)]
		[XmlIgnore]
		public ModelType ModelType
		{
			get
			{
				StorageEngineUsed storageEngineUsed;
				if (!base.IsBodyLoadable())
				{
					storageEngineUsed = StorageEngineUsed.Traditional;
				}
				else
				{
					storageEngineUsed = base.GetBody<Database.DatabaseBodyBase>().StorageEngineUsed;
				}
				if (storageEngineUsed == StorageEngineUsed.InMemory || storageEngineUsed == StorageEngineUsed.TabularMetadata)
				{
					return ModelType.Tabular;
				}
				return ModelType.Multidimensional;
			}
			set
			{
				switch (value)
				{
				case ModelType.Multidimensional:
					this.StorageEngineUsed = StorageEngineUsed.Traditional;
					return;
				case ModelType.Tabular:
					this.StorageEngineUsed = StorageEngineUsed.InMemory;
					return;
				case ModelType.Default:
					this.StorageEngineUsed = StorageEngineUsed.Traditional;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x000315C4 File Offset: 0x0002F7C4
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x000315D4 File Offset: 0x0002F7D4
		[PropertyOrder(1)]
		[DefaultValue(1050)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2010/engine/200")]
		public int CompatibilityLevel
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().CompatibilityLevel;
			}
			set
			{
				int effectiveCompatibilityLevel = this.GetEffectiveCompatibilityLevel(true);
				if (effectiveCompatibilityLevel != value)
				{
					this.OnCompatibilityLevelChange(value, effectiveCompatibilityLevel);
					this.OnBodyPropertyChanging();
					if (this.originalCompatibilityLevel == -1)
					{
						this.originalCompatibilityLevel = effectiveCompatibilityLevel;
					}
					base.GetBody<Database.DatabaseBodyBase>().CompatibilityLevel = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003161D File Offset: 0x0002F81D
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00031639 File Offset: 0x0002F839
		[XmlIgnore]
		public CompatibilityMode CompatibilityMode
		{
			get
			{
				if (this.Parent != null)
				{
					return this.Parent.CompatibilityMode;
				}
				return this.mode;
			}
			set
			{
				if (this.Parent != null)
				{
					throw new InvalidOperationException(SR.InvalidOperation_CannotSetModeOfDBUnderServer);
				}
				if (this.mode != value)
				{
					this.OnCompatibilityModeChange(value, this.mode);
					this.OnBodyPropertyChanging();
					this.mode = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00031678 File Offset: 0x0002F878
		internal override void OnBeforeRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
			base.OnBeforeRefresh(expansion, refreshType);
			if ((refreshType == null || refreshType.Value == RefreshType.LoadedObjectsOnly) && base.IsLoaded && this.StorageEngineUsed == StorageEngineUsed.TabularMetadata && this.originalCompatibilityLevel != -1)
			{
				this.OnCompatibilityLevelRestore(this.originalCompatibilityLevel);
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000316C5 File Offset: 0x0002F8C5
		internal override void OnAfterRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
			base.OnAfterRefresh(expansion, refreshType);
			if (refreshType == null || refreshType.Value == RefreshType.LoadedObjectsOnly)
			{
				this.originalCompatibilityLevel = -1;
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000316E8 File Offset: 0x0002F8E8
		internal override void OnAfterUpdate(UpdateOptions options)
		{
			base.OnAfterUpdate(options);
			this.originalCompatibilityLevel = -1;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000316F8 File Offset: 0x0002F8F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnCompatibilityLevelChange(int newCompatLevel, int currentCompatLevel)
		{
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000316FA File Offset: 0x0002F8FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnCompatibilityLevelRestore(int originalCompatibilityLevel)
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000316FC File Offset: 0x0002F8FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnCompatibilityModeChange(CompatibilityMode newMode, CompatibilityMode currentMode)
		{
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x000316FE File Offset: 0x0002F8FE
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x0003170C File Offset: 0x0002F90C
		[DefaultValue(0L)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[ReadOnly(true)]
		[LocalizedDescription("PropertyDescription_Database_Version")]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2013/engine/500")]
		public long Version
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().Version;
			}
			set
			{
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (body.Version != value)
				{
					this.OnBodyPropertyChanging();
					body.Version = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0003173C File Offset: 0x0002F93C
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x0003174C File Offset: 0x0002F94C
		[DefaultValue(0L)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[ReadOnly(true)]
		[LocalizedDescription("PropertyDescription_Database_EstimatedSize")]
		public long EstimatedSize
		{
			get
			{
				return base.GetBody<Database.DatabaseBodyBase>().EstimatedSize;
			}
			set
			{
				Database.DatabaseBodyBase body = base.GetBody<Database.DatabaseBodyBase>();
				if (body.EstimatedSize != value)
				{
					this.OnBodyPropertyChanging();
					body.EstimatedSize = value;
					this.OnBodyPropertyChange();
				}
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0003177C File Offset: 0x0002F97C
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x00031784 File Offset: 0x0002F984
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2021/engine/921")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_Database_IsBlocked")]
		public bool IsBlocked { get; set; }

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003178D File Offset: 0x0002F98D
		internal virtual void OnBodyPropertyChanging()
		{
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003178F File Offset: 0x0002F98F
		internal virtual void OnBodyPropertyChange()
		{
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00031791 File Offset: 0x0002F991
		public override bool CanProcess(ProcessType processType)
		{
			return processType == ProcessType.ProcessDefault || processType == ProcessType.ProcessFull || processType == ProcessType.ProcessClear || processType == ProcessType.ProcessRecalc || processType == ProcessType.ProcessDefrag;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000317AA File Offset: 0x0002F9AA
		public override Hashtable GetReferences(Hashtable references, bool forMajorChildrenAlso)
		{
			if (references == null)
			{
				references = new Hashtable();
			}
			return references;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000317B8 File Offset: 0x0002F9B8
		protected Server GetConnectedParentServer()
		{
			Server parentOrNull = this.ParentOrNull;
			if (parentOrNull == null || !parentOrNull.Connected)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NoConnectedParentServer);
			}
			return parentOrNull;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000317E3 File Offset: 0x0002F9E3
		public void Backup(string file)
		{
			this.GetConnectedParentServer().Backup(this, file, false, false, null, true, null);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000317F7 File Offset: 0x0002F9F7
		public void Backup(string file, bool allowOverwrite)
		{
			this.GetConnectedParentServer().Backup(this, file, allowOverwrite, false, null, true, null);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003180B File Offset: 0x0002FA0B
		public void Backup(string file, bool allowOverwrite, bool backupRemotePartitions)
		{
			this.GetConnectedParentServer().Backup(this, file, allowOverwrite, backupRemotePartitions, null, true, null);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003181F File Offset: 0x0002FA1F
		public void Backup(string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations)
		{
			this.GetConnectedParentServer().Backup(this, file, allowOverwrite, backupRemotePartitions, locations, true, null);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00031834 File Offset: 0x0002FA34
		public void Backup(string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations, bool applyCompression)
		{
			this.GetConnectedParentServer().Backup(this, file, allowOverwrite, backupRemotePartitions, locations, applyCompression, null);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003184A File Offset: 0x0002FA4A
		public void Backup(string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations, bool applyCompression, string password)
		{
			this.GetConnectedParentServer().Backup(this, file, allowOverwrite, backupRemotePartitions, locations, applyCompression, password);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00031861 File Offset: 0x0002FA61
		public void Backup(BackupInfo backupInfo)
		{
			this.GetConnectedParentServer().Backup(this, backupInfo);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00031870 File Offset: 0x0002FA70
		public void Detach()
		{
			this.GetConnectedParentServer().Detach(this, null);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003187F File Offset: 0x0002FA7F
		public void Detach(string password)
		{
			this.GetConnectedParentServer().Detach(this, password);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00031890 File Offset: 0x0002FA90
		protected internal override void AddToContainer(IContainer container)
		{
			if (container == null)
			{
				return;
			}
			if (this.Site == null)
			{
				container.Add(this);
			}
			if (!base.IsLoaded)
			{
				return;
			}
			foreach (object obj in this.Translations)
			{
				((Translation)obj).AddToContainer(container);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000E4E RID: 3662
		internal abstract string NameOnServer { get; }

		// Token: 0x06000E4F RID: 3663
		internal abstract bool IsModelLoaded();

		// Token: 0x06000E50 RID: 3664
		internal abstract bool IsInTransactionInternal();

		// Token: 0x06000E51 RID: 3665
		internal abstract bool HasObsoleteTransaction();

		// Token: 0x040007B9 RID: 1977
		private DateTime lastUpdate = DateTime.MinValue;

		// Token: 0x040007BA RID: 1978
		private string imagePath;

		// Token: 0x040007BB RID: 1979
		private string imageUniqueId;

		// Token: 0x040007BC RID: 1980
		private string imageVersion;

		// Token: 0x040007BD RID: 1981
		private string imageUrl;

		// Token: 0x040007BE RID: 1982
		private DismissedValidationRuleCollection dismissedValidationRules;

		// Token: 0x040007BF RID: 1983
		private DismissedValidationResultCollection dismissedValidationResults;

		// Token: 0x040007C0 RID: 1984
		private int originalCompatibilityLevel = -1;

		// Token: 0x040007C1 RID: 1985
		private CompatibilityMode mode;

		// Token: 0x020001A8 RID: 424
		internal abstract class DatabaseBodyBase : ProcessableMajorObject.ProcessableMajorObjectBody
		{
			// Token: 0x06001338 RID: 4920 RVA: 0x000435A8 File Offset: 0x000417A8
			private protected DatabaseBodyBase(Database owner)
				: base(owner)
			{
				this.owner = owner;
				this.EstimatedSize = 0L;
				this.Language = 0;
				this.Collation = null;
				this.Visible = true;
				this.Translations = new TranslationCollection(owner);
				this.Translations.CollectionChanging += this.BodyCollectionChanging;
				this.Translations.CollectionChanged += this.BodyCollectionChanged;
				this.ReadWriteMode = ReadWriteMode.ReadWrite;
				this.DbStorageLocation = null;
				this.StorageEngineUsed = StorageEngineUsed.Traditional;
				this.CompatibilityLevel = 1050;
				this.Version = 0L;
			}

			// Token: 0x06001339 RID: 4921 RVA: 0x00043642 File Offset: 0x00041842
			private protected void BodyCollectionChanging(object sender, CollectionChangeEventArgs e)
			{
				this.owner.OnBodyPropertyChanging();
			}

			// Token: 0x0600133A RID: 4922 RVA: 0x0004364F File Offset: 0x0004184F
			private protected void BodyCollectionChanged(object sender, CollectionChangeEventArgs e)
			{
				this.owner.OnBodyPropertyChange();
			}

			// Token: 0x040010C9 RID: 4297
			public long EstimatedSize;

			// Token: 0x040010CA RID: 4298
			public int Language;

			// Token: 0x040010CB RID: 4299
			public string Collation;

			// Token: 0x040010CC RID: 4300
			public bool Visible;

			// Token: 0x040010CD RID: 4301
			public TranslationCollection Translations;

			// Token: 0x040010CE RID: 4302
			public ReadWriteMode ReadWriteMode;

			// Token: 0x040010CF RID: 4303
			public string DbStorageLocation;

			// Token: 0x040010D0 RID: 4304
			public StorageEngineUsed StorageEngineUsed;

			// Token: 0x040010D1 RID: 4305
			public int CompatibilityLevel;

			// Token: 0x040010D2 RID: 4306
			public long Version;

			// Token: 0x040010D3 RID: 4307
			private Database owner;
		}
	}
}
