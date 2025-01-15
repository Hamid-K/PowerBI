using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D3 RID: 211
	public class Database : Database, ISerializableTabularDatabase, ICloneable, IMajorObject
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x0006CBA9 File Offset: 0x0006ADA9
		public Database()
		{
			this.isNew = true;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		public Database(string name)
			: base(name, name)
		{
			this.isNew = true;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0006CBC9 File Offset: 0x0006ADC9
		public Database(string name, string id)
			: base(name, id)
		{
			this.isNew = true;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0006CBDA File Offset: 0x0006ADDA
		public Database(ModelType modelType, int compatibilityLevel)
			: base(modelType, compatibilityLevel)
		{
			this.isNew = true;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0006CBEB File Offset: 0x0006ADEB
		Server IMajorObject.ParentServer
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0006CBF3 File Offset: 0x0006ADF3
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0006CBF6 File Offset: 0x0006ADF6
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteElementString("DatabaseID", base.ID);
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0006CC17 File Offset: 0x0006AE17
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0006CC1F File Offset: 0x0006AE1F
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0006CC28 File Offset: 0x0006AE28
		string IMajorObject.Path
		{
			get
			{
				IMajorObject parent = this.Parent;
				return ((parent == null) ? string.Empty : parent.Path) + "<DatabaseID>" + base.ID + "</DatabaseID>";
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0006CC61 File Offset: 0x0006AE61
		ObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return (ObjectReference)this.GetObjectReference();
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0006CC6E File Offset: 0x0006AE6E
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && this.Parent == obj;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0006CC81 File Offset: 0x0006AE81
		[XmlIgnore]
		[Browsable(false)]
		public new Server Parent
		{
			get
			{
				return this.ParentOrNull;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0006CC8C File Offset: 0x0006AE8C
		internal new Server ParentOrException
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0006CCBA File Offset: 0x0006AEBA
		internal new Server ParentOrNull
		{
			get
			{
				return (Server)base.Parent;
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0006CCC7 File Offset: 0x0006AEC7
		internal sealed override int GetCompatibilityLevel()
		{
			if (!base.IsBodyLoadable())
			{
				return 1600;
			}
			return base.GetEffectiveCompatibilityLevel(true);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0006CCE0 File Offset: 0x0006AEE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnCompatibilityLevelChange(int newCompatLevel, int currentCompatLevel)
		{
			if (base.StorageEngineUsed == StorageEngineUsed.TabularMetadata && newCompatLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("value", TomSR.Exception_CompatLevelOutOfRange(newCompatLevel.ToString(), 1200.ToString()));
			}
			if (newCompatLevel < currentCompatLevel && this.model != null)
			{
				if (base.CompatibilityMode != CompatibilityMode.Unknown)
				{
					int num;
					string text;
					this.model.GetCompatibilityRequirement(base.CompatibilityMode, out num, out text);
					if (num > newCompatLevel)
					{
						if (base.IsInRefresh() || (this.Parent != null && this.Parent.IsInRefresh()))
						{
							this.ResetModel(true);
							return;
						}
						throw new CompatibilityViolationException(base.CompatibilityMode, newCompatLevel, num, text);
					}
				}
				else
				{
					bool flag = false;
					for (int i = 0; i < 3; i++)
					{
						if (this.model.GetCompatibilityRequirementLevel(CompatibilityRestrictionSet.GetModeByRestrictionIndex(i)) <= newCompatLevel)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						throw new CompatibilityViolationException(this.model.GetFormattedObjectPath());
					}
				}
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0006CDC6 File Offset: 0x0006AFC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnCompatibilityLevelRestore(int originalCompatibilityLevel)
		{
			if (this.model != null && this.model.GetCompatibilityRequirementLevel(base.CompatibilityMode) > originalCompatibilityLevel)
			{
				this.ResetModel(true);
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0006CDEC File Offset: 0x0006AFEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnCompatibilityModeChange(CompatibilityMode newMode, CompatibilityMode currentMode)
		{
			if (newMode != CompatibilityMode.Unknown && this.model != null)
			{
				int num;
				string text;
				this.model.GetCompatibilityRequirement(newMode, out num, out text);
				if (this.GetCompatibilityLevel() < num)
				{
					throw new CompatibilityViolationException(newMode, this.GetCompatibilityLevel(), num, text);
				}
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0006CE2C File Offset: 0x0006B02C
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnStorageLocationChange(string storageLocation)
		{
			if (this.model != null)
			{
				this.model.body.StorageLocation = storageLocation;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0006CE48 File Offset: 0x0006B048
		internal override void OnBodyPropertyChanging()
		{
			if (base.IsInRefresh() || (this.Parent != null && this.Parent.IsInRefresh()))
			{
				return;
			}
			if (this.IsTM && this.Parent != null && this.Parent.IsInTransaction)
			{
				ITransaction currentTransaction = this.Parent.CurrentTransaction;
				if (currentTransaction.ModifiedDatabase != null && currentTransaction.ModifiedDatabase != this)
				{
					throw new InvalidOperationException(TomSR.Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction);
				}
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0006CEB8 File Offset: 0x0006B0B8
		internal override void OnBodyPropertyChange()
		{
			if (base.IsInRefresh() || (this.Parent != null && this.Parent.IsInRefresh()))
			{
				return;
			}
			if (this.IsTM)
			{
				this.hasBodyChanges = true;
				if (this.Parent != null)
				{
					if (this.model != null)
					{
						ObjectChangeTracker.EnsureInCorrectSavepointForLocalChange(this.model, false);
						return;
					}
					if (this.Parent.IsInTransaction)
					{
						this.Parent.CurrentTransaction.ModifiedDatabase = this;
					}
				}
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0006CF2D File Offset: 0x0006B12D
		internal bool HasLocalBodyChanges()
		{
			return this.hasBodyChanges;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0006CF35 File Offset: 0x0006B135
		internal void ResetLocalBodyChangesIndication()
		{
			this.hasBodyChanges = false;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0006CF3E File Offset: 0x0006B13E
		internal override bool HasObsoleteTransaction()
		{
			return this.hasObsoleteTrasaction;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0006CF48 File Offset: 0x0006B148
		internal bool TryRollbackTrasaction()
		{
			Utils.Verify(this.IsTM && (this.model == null || (this.model.TxManager != null && this.model.TxManager.GetBeginTxSavepoint() != null)) && (this.Parent != null && this.Parent.CurrentTransaction != null) && this.Parent.CurrentTransaction.ModifiedDatabase == this, "This method should be called only for a TM DB, trying to rollback the TX for the database associated with the TX!");
			if (this.hasBodyChanges)
			{
				this.hasObsoleteTrasaction = true;
				return false;
			}
			if (this.model != null)
			{
				this.model.RollbackTransaction();
			}
			return true;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0006CFE4 File Offset: 0x0006B1E4
		internal void ResetObsoleteTrasactionOnParentFullRefresh()
		{
			Utils.Verify(this.Parent != null && !this.Parent.IsInTransaction && this.Parent.IsInRefresh(), "This method should be called only during a full refresh after a TX could not be rollbacked locally");
			Utils.Verify(this.hasObsoleteTrasaction, "This method should be called only during a full refresh after a TX could not be rollbacked locally");
			this.ResetModel(true);
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0006D035 File Offset: 0x0006B235
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0006D03D File Offset: 0x0006B23D
		internal bool DisableDiscoveryOptimization { get; set; }

		// Token: 0x06000D6B RID: 3435 RVA: 0x0006D046 File Offset: 0x0006B246
		internal override IObjectReference GetObjectReference()
		{
			return new ObjectReference
			{
				DatabaseID = base.ID
			};
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0006D059 File Offset: 0x0006B259
		internal override Type GetBaseType()
		{
			return typeof(Database);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0006D065 File Offset: 0x0006B265
		private protected override MajorObject.MajorObjectBody CreateBodyImpl()
		{
			return new Database.DatabaseBody(this);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0006D06D File Offset: 0x0006B26D
		internal override void SerializeToJsonObject(object jsonObject, object options)
		{
			this.SerializeToJsonObject((JsonObject)jsonObject, (SerializeOptions)options);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0006D081 File Offset: 0x0006B281
		object ICloneable.Clone()
		{
			return this.CopyTo(new Database());
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0006D090 File Offset: 0x0006B290
		protected internal override MajorObject Clone(bool forceBodyLoading)
		{
			MajorObject majorObject = new Database();
			this.CopyTo(majorObject, forceBodyLoading);
			return majorObject;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0006D0AC File Offset: 0x0006B2AC
		public Database CopyTo(Database obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0006D0B7 File Offset: 0x0006B2B7
		public Database Clone()
		{
			return this.CopyTo(new Database());
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0006D0C4 File Offset: 0x0006B2C4
		[XmlIgnore]
		[Browsable(false)]
		public bool IsInTransaction
		{
			get
			{
				return this.IsInTransactionInternal();
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0006D0CC File Offset: 0x0006B2CC
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0006D134 File Offset: 0x0006B334
		[XmlIgnore]
		[Browsable(false)]
		public Model Model
		{
			get
			{
				base.CheckBody();
				if (this.IsTabularMetadataDatabase(true) && this.Parent != null && this.Parent.Connected && !base.IsRemoved && !this.IsModelLoaded())
				{
					if (this.hasObsoleteTrasaction)
					{
						throw new InvalidOperationException(TomSR.Exception_CannotSyncModelOfDirtyDatabase(base.Name));
					}
					this.LoadModel();
				}
				return this.model;
			}
			set
			{
				if (value != null && value.Database != null)
				{
					throw new InvalidOperationException(TomSR.Exception_ModelAlreadyBelongsToAnotherDatabase);
				}
				if (base.IsRemoved)
				{
					throw new InvalidOperationException(TomSR.Exception_RemovedDatabaseCannotBeAttached);
				}
				if (this.hasObsoleteTrasaction)
				{
					throw new InvalidOperationException(TomSR.Exception_ModifyDirtyDatabase(base.Name));
				}
				base.CheckBody();
				if (this.IsTabularMetadataDatabase(true) && this.Parent != null && this.Parent.Connected && !base.IsRemoved && !this.IsModelLoaded())
				{
					this.LoadModel();
				}
				if (this.model == value)
				{
					return;
				}
				if (this.model != null)
				{
					if (this.model.GetAllDescendants().Any<MetadataObject>())
					{
						throw new InvalidOperationException(TomSR.Exception_NonEmptyModelCannotBeReplaced);
					}
					this.ResetModel(false);
				}
				if (value != null)
				{
					this.VerifyNoOtherModelInTransaction();
					this.SetModel(value, true);
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0006D205 File Offset: 0x0006B405
		internal bool IsTM
		{
			get
			{
				return base.StorageEngineUsed == StorageEngineUsed.TabularMetadata;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0006D210 File Offset: 0x0006B410
		internal override bool IsModelLoaded()
		{
			return this.model != null;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0006D21B File Offset: 0x0006B41B
		internal override bool IsInTransactionInternal()
		{
			return this.Parent != null && this.Parent.IsInTransaction && this.Parent.CurrentTransaction.ModifiedDatabase != null && this.Parent.CurrentTransaction.ModifiedDatabase == this;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0006D25C File Offset: 0x0006B45C
		internal void VerifyNoOtherModelInTransaction()
		{
			if (this.Parent != null && this.Parent.IsInTransaction && this.Parent.CurrentTransaction.ModifiedDatabase != null && this.Parent.CurrentTransaction.ModifiedDatabase != this)
			{
				throw new InvalidOperationException(TomSR.Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction);
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0006D2AE File Offset: 0x0006B4AE
		internal override void OnAfterDrop()
		{
			base.IsRemoved = true;
			if (this.model != null)
			{
				this.model.MarkAsRemoved();
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0006D2CA File Offset: 0x0006B4CA
		internal override void OnBeforeRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
			if (this.hasObsoleteTrasaction && expansion != ObjectExpansion.Full)
			{
				throw new InvalidOperationException(TomSR.Exception_PartialRefreshDirtyDatabase(base.Name));
			}
			base.OnBeforeRefresh(expansion, refreshType);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0006D2F0 File Offset: 0x0006B4F0
		internal override void OnAfterRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
			base.OnAfterRefresh(expansion, refreshType);
			RefreshType? refreshType2;
			RefreshType refreshType3;
			if (expansion != ObjectExpansion.ReferenceOnly)
			{
				refreshType2 = refreshType;
				refreshType3 = RefreshType.UnloadedObjectsOnly;
				if (!((refreshType2.GetValueOrDefault() == refreshType3) & (refreshType2 != null)))
				{
					this.UpdateServerCachedProperties();
					if (!this.Parent.IsInTransaction)
					{
						this.hasBodyChanges = false;
					}
				}
			}
			if (expansion != ObjectExpansion.Full)
			{
				return;
			}
			refreshType2 = refreshType;
			refreshType3 = RefreshType.UnloadedObjectsOnly;
			if (!((refreshType2.GetValueOrDefault() == refreshType3) & (refreshType2 != null)) && !this.IsTabularMetadataDatabase(true))
			{
				if (this.model != null)
				{
					this.ResetModel(true);
				}
				return;
			}
			if (refreshType != null)
			{
				refreshType2 = refreshType;
				refreshType3 = RefreshType.LoadedObjectsOnly;
				if (!((refreshType2.GetValueOrDefault() == refreshType3) & (refreshType2 != null)) || !this.IsModelLoaded())
				{
					refreshType2 = refreshType;
					refreshType3 = RefreshType.UnloadedObjectsOnly;
					if (!((refreshType2.GetValueOrDefault() == refreshType3) & (refreshType2 != null)) || !this.IsTabularMetadataDatabase(true) || this.IsModelLoaded())
					{
						return;
					}
				}
			}
			if (this.hasObsoleteTrasaction)
			{
				Utils.Verify(this.model.TxManager != null && this.model.TxManager.GetBeginTxSavepoint() != null, "This method should be called only after a rollback of a TX, that was not rolled back in the Model level!");
				this.hasObsoleteTrasaction = false;
				if (!this.model.IsNewModel)
				{
					try
					{
						this.model.SyncImpl(false, true, false);
						return;
					}
					catch (Exception)
					{
						this.ResetModel(false);
						throw;
					}
				}
				this.ResetModel(false);
				return;
			}
			if (this.IsModelLoaded())
			{
				if (!this.model.IsNewModel)
				{
					this.model.SyncImpl(true, false, this.DisableDiscoveryOptimization);
					return;
				}
			}
			else
			{
				this.LoadModel();
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0006D478 File Offset: 0x0006B678
		internal override void OnAfterUpdate(UpdateOptions options)
		{
			base.OnAfterUpdate(options);
			if (this.Parent.ConnectionInfo != null && this.Parent.ConnectionInfo.IsPbiPremiumXmlaEp && this.isNew)
			{
				this.Parent.RefreshDatabaseID(this);
			}
			if (!this.Parent.CaptureXml)
			{
				this.UpdateServerCachedProperties();
				if (!this.Parent.IsInTransaction)
				{
					this.hasBodyChanges = false;
				}
			}
			if (this.IsTM && this.Parent.IsInTransaction)
			{
				Database modifiedDatabase = this.Parent.CurrentTransaction.ModifiedDatabase;
				if (modifiedDatabase != null && modifiedDatabase != this)
				{
					throw new InvalidOperationException(TomSR.Exception_CannotSaveChangeAnotherModelInTransaction);
				}
			}
			if (options == UpdateOptions.ExpandFull && this.IsTM && this.IsModelLoaded() && this.model.TxManager.CurrentSavepoint.Name != "Synced")
			{
				this.model.SaveChangesImpl(SaveFlags.Default, 0);
			}
			this.isNew = false;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0006D568 File Offset: 0x0006B768
		private void LoadModel()
		{
			Utils.Verify(this.model == null, "Attempt to LoadModel after model has been initially loaded");
			if (this.Parent == null || !this.Parent.Connected)
			{
				return;
			}
			Model model = null;
			bool captureXml = this.Parent.CaptureXml;
			try
			{
				this.Parent.CaptureXml = false;
				model = DdlUtil.DiscoverModel(this);
			}
			finally
			{
				this.Parent.CaptureXml = captureXml;
			}
			this.SetModel(model, false);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0006D5E8 File Offset: 0x0006B7E8
		private void SetModel(Model model, bool isModelNew)
		{
			if (model == null)
			{
				this.model = null;
				return;
			}
			this.model = model;
			this.model.Database = this;
			if (this.Parent != null)
			{
				if (this.Parent.IsInTransaction)
				{
					if (isModelNew)
					{
						this.Parent.CurrentTransaction.ModifiedDatabase = this;
					}
					this.model.CreateTxManager(this.hasBodyChanges || isModelNew, !isModelNew);
					return;
				}
				this.model.CreateTxManager(false, !isModelNew);
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0006D664 File Offset: 0x0006B864
		private void ResetModel(bool resetObsoleteTxFlag)
		{
			this.model.DetachFromDatabase();
			this.model = null;
			if (resetObsoleteTxFlag)
			{
				this.hasObsoleteTrasaction = false;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0006D682 File Offset: 0x0006B882
		internal override string NameOnServer
		{
			get
			{
				return this.nameOnServer;
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0006D68A File Offset: 0x0006B88A
		internal bool IsTabularMetadataDatabase(bool isServerStateNeeded)
		{
			if (isServerStateNeeded)
			{
				return this.storageEngineOnServer != null && this.storageEngineOnServer.Value == StorageEngineUsed.TabularMetadata;
			}
			return base.StorageEngineUsed == StorageEngineUsed.TabularMetadata;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0006D6B6 File Offset: 0x0006B8B6
		private void UpdateServerCachedProperties()
		{
			if (base.IsLoaded)
			{
				this.nameOnServer = base.Name;
				this.storageEngineOnServer = new StorageEngineUsed?(base.StorageEngineUsed);
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0006D6E0 File Offset: 0x0006B8E0
		internal JsonObject SerializeToNewJsonObject(SerializeOptions options)
		{
			JsonObject jsonObject = new JsonObject();
			this.SerializeToJsonObject(jsonObject, options);
			return jsonObject;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0006D6FC File Offset: 0x0006B8FC
		internal void SerializeToJsonObject(JsonObject result, SerializeOptions options)
		{
			result["name", TomPropCategory.Name, 1, false] = JsonPropertyHelper.ConvertStringToJsonValue(base.Name, SplitMultilineOptions.None);
			if (base.HasID && string.CompareOrdinal(base.Name, base.ID) != 0)
			{
				result["id", TomPropCategory.Regular, 1, false] = JsonPropertyHelper.ConvertStringToJsonValue(base.ID, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				result["description", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly)
			{
				if (base.CompatibilityLevel != 0)
				{
					result["compatibilityLevel", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(base.CompatibilityLevel);
				}
				if (!string.IsNullOrEmpty(base.DbStorageLocation))
				{
					result["storageLocation", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(base.DbStorageLocation, SplitMultilineOptions.None);
				}
				if (base.ReadWriteMode != ReadWriteMode.ReadWrite)
				{
					result["readWriteMode", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ReadWriteMode>(base.ReadWriteMode);
				}
				if (!options.IgnoreTimestamps && !options.IgnoreInferredProperties && base.CreatedTimestamp.CompareTo(DateTime.MinValue) != 0)
				{
					result["createdTimestamp", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(base.CreatedTimestamp);
				}
				if (!options.IgnoreTimestamps && !options.IgnoreInferredProperties && base.LastUpdate.CompareTo(DateTime.MinValue) != 0)
				{
					result["lastUpdate", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(base.LastUpdate);
				}
				if (!options.IgnoreTimestamps && !options.IgnoreInferredProperties && base.LastSchemaUpdate.CompareTo(DateTime.MinValue) != 0)
				{
					result["lastSchemaUpdate", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(base.LastSchemaUpdate);
				}
				if (!options.IgnoreTimestamps && !options.IgnoreInferredProperties && base.LastProcessed.CompareTo(DateTime.MinValue) != 0)
				{
					result["lastProcessed", TomPropCategory.Regular, 9, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(base.LastProcessed);
				}
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && this.Model != null)
			{
				CompatibilityMode compatibilityMode = base.CompatibilityMode;
				int compatibilityLevel = base.CompatibilityLevel;
				if (compatibilityMode == CompatibilityMode.Unknown)
				{
					compatibilityMode = CompatibilityMode.PowerBI;
					int num;
					string text;
					this.model.GetCompatibilityRequirement(compatibilityMode, out num, out text);
					if (compatibilityMode != CompatibilityMode.PowerBI && (num == -2 || num > compatibilityLevel))
					{
						compatibilityMode = CompatibilityMode.PowerBI;
						this.model.GetCompatibilityRequirement(compatibilityMode, out num, out text);
					}
					if (num == -2)
					{
						throw new CompatibilityViolationException(compatibilityMode, text);
					}
					if (num > compatibilityLevel)
					{
						throw new CompatibilityViolationException(compatibilityMode, compatibilityLevel, num, text);
					}
				}
				result["model", TomPropCategory.ChildLink, 10, false] = this.Model.SerializeToNewJsonObject(options, compatibilityMode, compatibilityLevel).ToDictObject();
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0006D99C File Offset: 0x0006BB9C
		internal void DeserializeFromJsonObject(JObject jsonObj, DeserializeOptions options, CompatibilityMode mode)
		{
			foreach (JProperty jproperty in jsonObj.Properties())
			{
				if (!this.ReadPropertyFromJson(jproperty, options, mode))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(jproperty.Name), jproperty, null);
				}
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0006DA00 File Offset: 0x0006BC00
		internal bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 2:
					if (name == "id")
					{
						base.ID = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 4:
					if (name == "name")
					{
						base.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 5:
					if (name == "model")
					{
						if (jsonProp.Value.Type != 10)
						{
							if (jsonProp.Value.Type != 1)
							{
								throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonToken(1.ToString(), jsonProp.Value.Type.ToString()), jsonProp.Value, null);
							}
							Model model = new Model();
							model.DeserializeFromJsonObject((JObject)jsonProp.Value, options, mode, base.CompatibilityLevel);
							this.Model = model;
						}
						return true;
					}
					break;
				case 10:
					if (name == "lastUpdate")
					{
						base.LastUpdate = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				case 11:
					if (name == "description")
					{
						this.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 13:
				{
					char c = name[0];
					if (c != 'l')
					{
						if (c == 'r')
						{
							if (name == "readWriteMode")
							{
								base.ReadWriteMode = JsonPropertyHelper.ConvertJsonValueToEnum<ReadWriteMode>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "lastProcessed")
					{
						base.LastProcessed = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 15:
					if (name == "storageLocation")
					{
						base.DbStorageLocation = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 16:
				{
					char c = name[0];
					if (c != 'c')
					{
						if (c == 'l')
						{
							if (name == "lastSchemaUpdate")
							{
								base.LastSchemaUpdate = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "createdTimestamp")
					{
						base.CreatedTimestamp = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 18:
					if (name == "compatibilityLevel")
					{
						base.CompatibilityLevel = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0006DCD4 File Offset: 0x0006BED4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Database, null, "", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("id", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("id", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("compatibilityLevel", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("compatibilityLevel", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (context.SerializationMode == MetadataSerializationMode.Tmdl && writer.ShouldIncludeProperty("compatibilityMode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<CompatibilityMode>("compatibilityMode", MetadataPropertyNature.RegularProperty, null);
				}
				if (context.SerializationMode == MetadataSerializationMode.Tmdl && writer.ShouldIncludeProperty("language", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("language", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("storageLocation", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("storageLocation", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("readWriteMode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ReadWriteMode>("readWriteMode", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("createdTimestamp", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("createdTimestamp", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("lastUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("lastUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("lastSchemaUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("lastSchemaUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("lastProcessed", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("lastProcessed", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("model", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteSingleChild(context, "model", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable, ObjectType.Model);
				}
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0006DF20 File Offset: 0x0006C120
		internal void LoadMetadata(SerializationActivityContext context, IMetadataReader reader)
		{
			while (reader.IsOnProperty())
			{
				UnexpectedPropertyClassification unexpectedPropertyClassification;
				if (!this.TryReadNextMetadataProperty(context, reader, out unexpectedPropertyClassification))
				{
					throw reader.CreateUnexpectedPropertyException(context, unexpectedPropertyClassification);
				}
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0006DF4C File Offset: 0x0006C14C
		internal void SaveMetadata(SerializationActivityContext context, IMetadataWriter writer)
		{
			if ((!string.IsNullOrEmpty(base.Name) || context.SerializationMode == MetadataSerializationMode.Tmdl) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty, base.Name ?? string.Empty);
			}
			if (base.HasID && string.CompareOrdinal(base.Name, base.ID) != 0 && writer.ShouldIncludeProperty("id", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("id", MetadataPropertyNature.RegularProperty, base.ID);
			}
			if (!string.IsNullOrEmpty(this.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.Description);
			}
			if ((context.SerializationMode == MetadataSerializationMode.Tmdl || base.CompatibilityLevel != 0) && writer.ShouldIncludeProperty("compatibilityLevel", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("compatibilityLevel", MetadataPropertyNature.RegularProperty, base.CompatibilityLevel);
			}
			if (context.SerializationMode == MetadataSerializationMode.Tmdl && base.CompatibilityMode != CompatibilityMode.Unknown && writer.ShouldIncludeProperty("compatibilityMode", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<CompatibilityMode>("compatibilityMode", MetadataPropertyNature.RegularProperty, base.CompatibilityMode);
			}
			if (context.SerializationMode == MetadataSerializationMode.Tmdl && base.Language != 0 && writer.ShouldIncludeProperty("language", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("language", MetadataPropertyNature.RegularProperty, base.Language);
			}
			if (!string.IsNullOrEmpty(base.DbStorageLocation) && writer.ShouldIncludeProperty("storageLocation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("storageLocation", MetadataPropertyNature.RegularProperty, base.DbStorageLocation);
			}
			if (base.ReadWriteMode != ReadWriteMode.ReadWrite && writer.ShouldIncludeProperty("readWriteMode", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<ReadWriteMode>("readWriteMode", MetadataPropertyNature.RegularProperty, base.ReadWriteMode);
			}
			if (base.CreatedTimestamp.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("createdTimestamp", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("createdTimestamp", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, base.CreatedTimestamp);
			}
			if (base.LastUpdate.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("lastUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("lastUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, base.LastUpdate);
			}
			if (base.LastSchemaUpdate.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("lastSchemaUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("lastSchemaUpdate", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, base.LastSchemaUpdate);
			}
			if (base.LastProcessed.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("lastProcessed", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("lastProcessed", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, base.LastProcessed);
			}
			if (this.Model != null && writer.ShouldIncludeProperty("model", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteSingleChild(context, "model", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.Translatable, this.model);
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0006E215 File Offset: 0x0006C415
		void ISerializableTabularDatabase.LoadMetadata(SerializationActivityContext context, IMetadataReader reader)
		{
			this.LoadMetadata(context, reader);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0006E21F File Offset: 0x0006C41F
		void ISerializableTabularDatabase.SaveMetadata(SerializationActivityContext context, IMetadataWriter writer)
		{
			this.SaveMetadata(context, writer);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0006E22C File Offset: 0x0006C42C
		private bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			classification = UnexpectedPropertyClassification.Unclassified;
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				switch (propertyName.Length)
				{
				case 2:
					if (propertyName == "id")
					{
						base.ID = reader.ReadStringProperty();
						return true;
					}
					break;
				case 4:
					if (propertyName == "name")
					{
						base.Name = reader.ReadStringProperty();
						return true;
					}
					break;
				case 5:
					if (propertyName == "model")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							Model model = reader.ReadSingleChildProperty<Model>(context);
							try
							{
								this.Model = model;
							}
							catch (Exception ex)
							{
								throw reader.CreateInvalidChildException(context, model, TomSR.Exception_FailedAddDeserializedNamedObject("Model", (model != null) ? model.Name : null, ex.Message), ex);
							}
						}
						return true;
					}
					break;
				case 8:
					if (propertyName == "language")
					{
						base.Language = reader.ReadInt32Property();
						return true;
					}
					break;
				case 10:
					if (propertyName == "lastUpdate")
					{
						base.LastUpdate = reader.ReadDateTimeProperty();
						return true;
					}
					break;
				case 11:
					if (propertyName == "description")
					{
						this.Description = reader.ReadStringProperty();
						return true;
					}
					break;
				case 13:
				{
					char c = propertyName[0];
					if (c != 'l')
					{
						if (c == 'r')
						{
							if (propertyName == "readWriteMode")
							{
								base.ReadWriteMode = reader.ReadEnumProperty<ReadWriteMode>();
								return true;
							}
						}
					}
					else if (propertyName == "lastProcessed")
					{
						base.LastProcessed = reader.ReadDateTimeProperty();
						return true;
					}
					break;
				}
				case 15:
					if (propertyName == "storageLocation")
					{
						base.DbStorageLocation = reader.ReadStringProperty();
						return true;
					}
					break;
				case 16:
				{
					char c = propertyName[0];
					if (c != 'c')
					{
						if (c == 'l')
						{
							if (propertyName == "lastSchemaUpdate")
							{
								base.LastSchemaUpdate = reader.ReadDateTimeProperty();
								return true;
							}
						}
					}
					else if (propertyName == "createdTimestamp")
					{
						base.CreatedTimestamp = reader.ReadDateTimeProperty();
						return true;
					}
					break;
				}
				case 17:
					if (propertyName == "compatibilityMode")
					{
						if (this.Parent != null)
						{
							if (reader.ReadEnumProperty<CompatibilityMode>() != base.CompatibilityMode)
							{
							}
						}
						else
						{
							base.CompatibilityMode = reader.ReadEnumProperty<CompatibilityMode>();
						}
						return true;
					}
					break;
				case 18:
					if (propertyName == "compatibilityLevel")
					{
						base.CompatibilityLevel = reader.ReadInt32Property();
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0006E524 File Offset: 0x0006C724
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
			if (database.hasObsoleteTrasaction && !database.IsInRefresh() && (database.Parent == null || !database.Parent.IsInRefresh()))
			{
				throw new InvalidOperationException(TomSR.Exception_ModifyDirtyDatabase(database.Name));
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			if (this.IsTabularMetadataDatabase(true) && this.Parent != null && this.Parent.Connected && !base.IsRemoved && !this.IsModelLoaded())
			{
				this.LoadModel();
			}
			if (this.model == null)
			{
				if (!this.IsTM && database.Model != null)
				{
					database.ResetModel(true);
				}
				return;
			}
			if (!database.IsInRefresh() && (database.Parent == null || !database.Parent.IsInRefresh()))
			{
				database.VerifyNoOtherModelInTransaction();
			}
			if (database.model == null)
			{
				database.SetModel(this.model.Clone(), true);
				return;
			}
			this.model.CopyTo(database.model);
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0006E646 File Offset: 0x0006C846
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x0006E64E File Offset: 0x0006C84E
		[XmlIgnoreOnRead]
		[XmlElement(IsNullable = false)]
		private string AggregationPrefix { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0006E657 File Offset: 0x0006C857
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0006E65F File Offset: 0x0006C85F
		[XmlIgnoreOnRead]
		[DefaultValue(DirectQueryMode.InMemory)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300/300")]
		private DirectQueryMode DirectQueryMode { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0006E668 File Offset: 0x0006C868
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0006E670 File Offset: 0x0006C870
		[XmlIgnoreOnRead]
		[XmlElement(IsNullable = false)]
		private string MasterDataSourceID { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0006E679 File Offset: 0x0006C879
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0006E681 File Offset: 0x0006C881
		[XmlIgnoreOnRead]
		[DefaultValue(0)]
		private int ProcessingPriority { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0006E68A File Offset: 0x0006C88A
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x0006E692 File Offset: 0x0006C892
		[XmlIgnoreOnRead]
		[XmlElement(IsNullable = false)]
		private ImpersonationInfo DataSourceImpersonationInfo { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0006E69B File Offset: 0x0006C89B
		[XmlIgnoreOnRead]
		private MajorObjectCollection Accounts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0006E69E File Offset: 0x0006C89E
		[XmlIgnoreOnRead]
		private MajorObjectCollection Assemblies
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0006E6A1 File Offset: 0x0006C8A1
		[XmlIgnoreOnRead]
		private MajorObjectCollection Cubes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0006E6A4 File Offset: 0x0006C8A4
		[XmlIgnoreOnRead]
		private MajorObjectCollection DatabasePermissions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0006E6A7 File Offset: 0x0006C8A7
		[XmlIgnoreOnRead]
		private MajorObjectCollection DataSources
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0006E6AA File Offset: 0x0006C8AA
		[XmlIgnoreOnRead]
		private MajorObjectCollection DataSourceViews
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0006E6AD File Offset: 0x0006C8AD
		[XmlIgnoreOnRead]
		private MajorObjectCollection Dimensions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0006E6B0 File Offset: 0x0006C8B0
		[XmlIgnoreOnRead]
		private MajorObjectCollection MiningStructures
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0006E6B3 File Offset: 0x0006C8B3
		[XmlIgnoreOnRead]
		private MajorObjectCollection Roles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0006E6B6 File Offset: 0x0006C8B6
		[XmlIgnore]
		[Browsable(false)]
		public Server Server
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0006E6BE File Offset: 0x0006C8BE
		internal override bool IsSyntacticallyValidID(string newValue, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(newValue, type, out error);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0006E6C8 File Offset: 0x0006C8C8
		internal override bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error)
		{
			return Utils.IsValidName(newValue, type, modelType, compatibilityLevel, namedComponentCollection, out error);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		internal override void RefreshMajorChildren(RefreshType type)
		{
		}

		// Token: 0x04000191 RID: 401
		private string nameOnServer;

		// Token: 0x04000192 RID: 402
		private StorageEngineUsed? storageEngineOnServer;

		// Token: 0x04000193 RID: 403
		private Model model;

		// Token: 0x04000194 RID: 404
		private bool isNew;

		// Token: 0x04000195 RID: 405
		private bool hasBodyChanges;

		// Token: 0x04000196 RID: 406
		private bool hasObsoleteTrasaction;

		// Token: 0x020002EA RID: 746
		private sealed class DatabaseBody : Database.DatabaseBodyBase
		{
			// Token: 0x060023B1 RID: 9137 RVA: 0x000E2950 File Offset: 0x000E0B50
			internal DatabaseBody(Database owner)
				: base(owner)
			{
				this.StorageEngineUsed = StorageEngineUsed.TabularMetadata;
				this.CompatibilityLevel = 1600;
				this.Roles = new RoleCollection(owner);
				this.Roles.CollectionChanging += base.BodyCollectionChanging;
				this.Roles.CollectionChanged += base.BodyCollectionChanged;
				this.Assemblies = new AssemblyCollection(owner);
				this.Assemblies.CollectionChanging += base.BodyCollectionChanging;
				this.Assemblies.CollectionChanged += base.BodyCollectionChanged;
			}

			// Token: 0x04000ABA RID: 2746
			public RoleCollection Roles;

			// Token: 0x04000ABB RID: 2747
			public AssemblyCollection Assemblies;
		}
	}
}
