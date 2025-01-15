using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web.Caching;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D4 RID: 212
	[CatalogItemType(ItemType.Model)]
	internal sealed class ModelCatalogItem : CatalogItem
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x00024E67 File Offset: 0x00023067
		internal ModelCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00024E7C File Offset: 0x0002307C
		internal void LoadModel(bool getModelDefinition)
		{
			base.LoadProperties();
			new ModelCatalogItem.ModelStorage(base.Service).LoadModel(this, getModelDefinition, base.Service);
			RSService.EnsureItemType(base.ThisItemType, this.m_itemContext.OriginalItemPath.Value, new ItemType[] { ItemType.Model });
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00024ECC File Offset: 0x000230CC
		internal Microsoft.ReportingServices.Modeling.ModelItem LoadModelAndGetModelItem(string originalModelItemID)
		{
			this.LoadModel(false);
			return this.GetModelItem(this.Model, originalModelItemID);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00024EE2 File Offset: 0x000230E2
		internal SemanticModel LoadUserModel(string perspectiveID)
		{
			this.LoadModel(false);
			this.ModelItemPolicies.CacheInherited = true;
			return this.GetUserModel(perspectiveID);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00024F00 File Offset: 0x00023100
		internal Microsoft.ReportingServices.Modeling.ModelItem LoadUserModelAndGetModelItem(string originalModelItemID)
		{
			SemanticModel semanticModel = this.LoadUserModel(null);
			return this.GetModelItem(semanticModel, originalModelItemID);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00024F1D File Offset: 0x0002311D
		internal ModelEntity LoadUserModelAndGetEntity(string originalEntityId)
		{
			ModelEntity modelEntity = this.LoadUserModelAndGetModelItem(originalEntityId) as ModelEntity;
			if (modelEntity == null)
			{
				throw new ModelItemNotFoundException(this.m_itemContext.OriginalItemPath.Value, originalEntityId);
			}
			return modelEntity;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00024F45 File Offset: 0x00023145
		protected override void ContentLoadSecurityCheck()
		{
			this.ThrowIfNoAccess(ModelOperation.ReadContent);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00024F50 File Offset: 0x00023150
		private Microsoft.ReportingServices.Modeling.ModelItem GetModelItem(SemanticModel model, string originalModelItemID)
		{
			Microsoft.ReportingServices.Modeling.ModelItem modelItem;
			if (originalModelItemID == null)
			{
				modelItem = model;
			}
			else
			{
				modelItem = model.LookupItemByID(Microsoft.ReportingServices.Modeling.ModelItem.StringToID(Microsoft.ReportingServices.Modeling.ModelItem.CanonicalizeID(originalModelItemID)));
				if (modelItem == null || modelItem is Perspective || modelItem is SemanticModel)
				{
					throw new ModelItemNotFoundException(base.ItemContext.OriginalItemPath.Value, originalModelItemID);
				}
			}
			return modelItem;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00024FA1 File Offset: 0x000231A1
		internal void RemoveFromCache()
		{
			RSLocalCacheManager.Current.RemoveCachedModel(ModelCatalogItem.ModelStorage.CachedModel.BuildKey(this.m_itemID, Globals.ParsePublicDateTimeFormat(base.Properties.ModifiedDate)));
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00024FC9 File Offset: 0x000231C9
		internal Guid CatalogItemID
		{
			get
			{
				return base.ItemID;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00024FD1 File Offset: 0x000231D1
		private Guid BinarySnapshotID
		{
			get
			{
				return this.m_binarySnapshotID;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00004FE5 File Offset: 0x000031E5
		internal override byte[] Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00024FD9 File Offset: 0x000231D9
		internal ServerSnapshot CompiledDefinition
		{
			set
			{
				this.m_compiledDefinition = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00024FE2 File Offset: 0x000231E2
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00024FEA File Offset: 0x000231EA
		internal SemanticModel Model
		{
			get
			{
				return this.m_model;
			}
			set
			{
				this.m_model = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00024FF3 File Offset: 0x000231F3
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x000246E4 File Offset: 0x000228E4
		internal DataSourceInfoCollection DataSources
		{
			get
			{
				if (this.m_dataSources == null)
				{
					this.m_dataSources = base.GetSyncedItemDataSources(base.ItemID);
				}
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00025015 File Offset: 0x00023215
		internal static Stream CreateModelChunk(ModelSnapshot binarySnapshot)
		{
			return binarySnapshot.CreateChunk("model+dsv");
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00025022 File Offset: 0x00023222
		internal static Stream GetModelChunk(ModelSnapshot binarySnapshot)
		{
			return binarySnapshot.GetChunk("model+dsv");
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00025030 File Offset: 0x00023230
		private SemanticModel GetUserModel(string perspectiveID)
		{
			if (!Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DynamicDrillthrough))
			{
				SemanticModel.SuppressDrillthroughDuringLazyClone = true;
			}
			if (perspectiveID == null)
			{
				return this.Model.CreateLazyClone(new Predicate<Microsoft.ReportingServices.Modeling.ModelItem>(this.CanUserSeeItem));
			}
			return this.Model.CreateLazyClone(new Predicate<Microsoft.ReportingServices.Modeling.ModelItem>(this.CanUserSeeItem), Microsoft.ReportingServices.Modeling.ModelItem.StringToID(perspectiveID));
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00025090 File Offset: 0x00023290
		private bool CanUserSeeItem(Microsoft.ReportingServices.Modeling.ModelItem modelItem)
		{
			if (!this.m_userCanReadContentChecked)
			{
				this.m_userCanReadContent = base.Service.SecMgr.CheckAccess(ItemType.Model, base.SecurityDescriptor, ModelOperation.ReadContent, base.ItemContext.ItemPath);
				this.m_userCanReadContentChecked = true;
			}
			return this.m_userCanReadContent || this.ModelItemPolicies.GetPolicy(modelItem).Rights.CheckAccess(base.Service);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000250FB File Offset: 0x000232FB
		internal bool RootPolicyExists()
		{
			return !this.ModelItemPolicies.GetPolicy(this.Model).Inherited;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00025116 File Offset: 0x00023316
		internal bool CanSetModelItemPolicy(Microsoft.ReportingServices.Modeling.ModelItem modelItem)
		{
			return modelItem.ParentItem == null || this.RootPolicyExists();
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00025128 File Offset: 0x00023328
		internal ModelItemPolicyCollection ModelItemPolicies
		{
			get
			{
				if (this.m_modelItemPolicies == null)
				{
					this.m_modelItemPolicies = new ModelItemPolicyCollection();
				}
				return this.m_modelItemPolicies;
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00025143 File Offset: 0x00023343
		internal void SaveDataSources()
		{
			base.Service.Storage.DeleteDataSources(base.ItemID);
			base.Service.AddDataSources(base.ItemID, this.DataSources, base.ItemContext.ItemPath.EditSessionID);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00025182 File Offset: 0x00023382
		protected override void Update()
		{
			base.Service.Storage.SetObjectContent(this.m_itemContext.CatalogItemPath, base.ThisItemType, this.m_content, Guid.Empty, null, Guid.Empty, null);
			base.SaveProperties();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000251C0 File Offset: 0x000233C0
		protected override void FinalizeCreation()
		{
			if (string.Equals(base.Properties.IsAutoGenerated, bool.TrueString, StringComparison.OrdinalIgnoreCase))
			{
				this.AddModelDataSource();
				new ModelCatalogItem.ModelStorage(base.Service).AddModelPerspectives(this.m_itemID, this.m_model);
				return;
			}
			this.AddEmptyModelDataSource(this.m_model);
			new ModelCatalogItem.ModelStorage(base.Service).AddModelPerspectives(this.m_itemID, this.m_model);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00025230 File Offset: 0x00023430
		internal void AddEmptyModelDataSource(SemanticModel model)
		{
			DataSourceInfo dataSourceInfo = new DataSourceInfo(ModelCatalogItem.GetDataSourceName(model), true);
			if (!dataSourceInfo.IsModel)
			{
				throw new InternalCatalogException("AddEmptyModelDataSource: dsi.IsModel must be true.");
			}
			Guid guid;
			ItemType itemType;
			byte[] array;
			base.Service.Storage.AddDataSource(this.m_itemID, Guid.Empty, dataSourceInfo, base.Service, this.m_itemContext.ItemPath.EditSessionID, out guid, out itemType, out array);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00025298 File Offset: 0x00023498
		private void AddModelDataSource()
		{
			DataSourceInfo theOnlyDataSource = this.m_dataSources.GetTheOnlyDataSource();
			Guid guid;
			ItemType itemType;
			byte[] array;
			base.Service.Storage.AddDataSource(this.m_itemID, Guid.Empty, theOnlyDataSource, base.Service, this.m_itemContext.ItemPath.EditSessionID, out guid, out itemType, out array);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000252E9 File Offset: 0x000234E9
		internal void ThrowIfNoAccess(ModelOperation operation)
		{
			if (!base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, operation, base.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00025328 File Offset: 0x00023528
		internal static string GetDataSourceName(SemanticModel model)
		{
			if (model.DataSourceView != null && model.DataSourceView.DataSourceID != null && model.DataSourceView.DataSourceID.Length > 0)
			{
				return model.DataSourceView.DataSourceID;
			}
			return "ModelDataSource";
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00025364 File Offset: 0x00023564
		internal static SemanticModel CompileModelDefinition(byte[] modelDefinition, bool verifyModel, string modelPathForTrace, out ValidationMessageCollection warnings)
		{
			if (RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Compiling model \"{0}\".", new object[] { modelPathForTrace });
			}
			SemanticModel semanticModel2;
			using (MemoryStream memoryStream = new MemoryStream(modelDefinition))
			{
				using (XmlReader xmlReader = Microsoft.ReportingServices.Common.XmlRWFactory.CreateReader(memoryStream))
				{
					SemanticModel semanticModel = new SemanticModel();
					warnings = semanticModel.Compile(xmlReader, verifyModel ? ModelCompilationOptions.None : (ModelCompilationOptions.IgnoreBindings | ModelCompilationOptions.IgnoreSecurityFilters), Localization.DefaultReportServerCulture);
					if (RSTrace.CatalogTrace.TraceVerbose)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Compilation of the model \"{0}\" is complete.", new object[] { modelPathForTrace });
					}
					semanticModel2 = semanticModel;
				}
			}
			return semanticModel2;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002541C File Offset: 0x0002361C
		internal static bool SameDataSourceName(SemanticModel model1, SemanticModel model2)
		{
			string dataSourceName = ModelCatalogItem.GetDataSourceName(model1);
			string dataSourceName2 = ModelCatalogItem.GetDataSourceName(model2);
			return dataSourceName == dataSourceName2;
		}

		// Token: 0x0400045C RID: 1116
		private Guid m_binarySnapshotID = Guid.Empty;

		// Token: 0x0400045D RID: 1117
		private SemanticModel m_model;

		// Token: 0x0400045E RID: 1118
		private const string ModelChunkName = "model+dsv";

		// Token: 0x0400045F RID: 1119
		private bool m_userCanReadContent;

		// Token: 0x04000460 RID: 1120
		private bool m_userCanReadContentChecked;

		// Token: 0x04000461 RID: 1121
		private ModelItemPolicyCollection m_modelItemPolicies;

		// Token: 0x02000463 RID: 1123
		internal sealed class ModelStorage : DBInterface
		{
			// Token: 0x06002351 RID: 9041 RVA: 0x00083F8E File Offset: 0x0008218E
			internal ModelStorage(RSService service)
			{
				service.ConnectStorage(this);
			}

			// Token: 0x06002352 RID: 9042 RVA: 0x00083F9D File Offset: 0x0008219D
			private ModelStorage(ConnectionManager connectionManager)
			{
				this.ConnectionManager = connectionManager;
			}

			// Token: 0x06002353 RID: 9043 RVA: 0x00083FAC File Offset: 0x000821AC
			internal void LoadModel(ModelCatalogItem model, bool getModelDefinition, RSService service)
			{
				ModelCatalogItem.ModelStorage.PrepareModelInfo(model, this, service);
				if (getModelDefinition && model.Content == null)
				{
					this.GetModelDefinition(model);
				}
				string text = ModelCatalogItem.ModelStorage.CachedModel.BuildKey(model.ItemID, Globals.ParsePublicDateTimeFormat(model.Properties.ModifiedDate));
				ModelCatalogItem.ModelStorage.CachedModel cachedModel = RSLocalCacheManager.Current.GetCachedModel(text);
				if (cachedModel != null)
				{
					this.SetCompiledModelFromCache(model, cachedModel);
					return;
				}
				ModelCatalogItem.ModelStorage.ModelDeserializationLock modelDeserializationLock = ModelCatalogItem.ModelStorage.AcquireModelDeserializationLock(text);
				ModelCatalogItem.ModelStorage.ModelDeserializationLock modelDeserializationLock2 = modelDeserializationLock;
				lock (modelDeserializationLock2)
				{
					try
					{
						cachedModel = RSLocalCacheManager.Current.GetCachedModel(text);
						if (cachedModel != null)
						{
							this.SetCompiledModelFromCache(model, cachedModel);
						}
						else
						{
							long num = this.DeserializeFromBinarySnapshot(model) * 4L;
							cachedModel = new ModelCatalogItem.ModelStorage.CachedModel(model.Model, model.ItemID, Globals.ParsePublicDateTimeFormat(model.Properties.ModifiedDate), num);
							RSLocalCacheManager.Current.CacheModel(cachedModel);
						}
					}
					finally
					{
						this.ReleaseModelDeserializationLock(text, modelDeserializationLock);
					}
				}
			}

			// Token: 0x06002354 RID: 9044 RVA: 0x000840A4 File Offset: 0x000822A4
			private static ModelCatalogItem.ModelStorage.ModelDeserializationLock AcquireModelDeserializationLock(string modelKey)
			{
				Dictionary<string, ModelCatalogItem.ModelStorage.ModelDeserializationLock> deserializingModels = ModelCatalogItem.ModelStorage.m_deserializingModels;
				ModelCatalogItem.ModelStorage.ModelDeserializationLock modelDeserializationLock;
				lock (deserializingModels)
				{
					if (ModelCatalogItem.ModelStorage.m_deserializingModels.ContainsKey(modelKey))
					{
						modelDeserializationLock = ModelCatalogItem.ModelStorage.m_deserializingModels[modelKey];
					}
					else
					{
						modelDeserializationLock = new ModelCatalogItem.ModelStorage.ModelDeserializationLock();
						ModelCatalogItem.ModelStorage.m_deserializingModels.Add(modelKey, modelDeserializationLock);
					}
					modelDeserializationLock.Count++;
				}
				return modelDeserializationLock;
			}

			// Token: 0x06002355 RID: 9045 RVA: 0x0008411C File Offset: 0x0008231C
			private void ReleaseModelDeserializationLock(string modelKey, ModelCatalogItem.ModelStorage.ModelDeserializationLock modelDeserializationLock)
			{
				Dictionary<string, ModelCatalogItem.ModelStorage.ModelDeserializationLock> deserializingModels = ModelCatalogItem.ModelStorage.m_deserializingModels;
				lock (deserializingModels)
				{
					int num = modelDeserializationLock.Count - 1;
					modelDeserializationLock.Count = num;
					if (num == 0)
					{
						ModelCatalogItem.ModelStorage.m_deserializingModels.Remove(modelKey);
					}
				}
			}

			// Token: 0x06002356 RID: 9046 RVA: 0x00084174 File Offset: 0x00082374
			private static void PrepareModelInfo(ModelCatalogItem model, ModelCatalogItem.ModelStorage currentModelStorage, RSService service)
			{
				ModelCatalogItem.ModelStorage modelStorage = currentModelStorage;
				bool flag = false;
				try
				{
					if (!currentModelStorage.ConnectionManager.IsBatchScoped && (currentModelStorage.Transaction == null || currentModelStorage.Transaction.IsolationLevel < IsolationLevel.RepeatableRead))
					{
						ConnectionManager connectionManager = new ConnectionManager();
						connectionManager.ConnectionTransactionType = ConnectionTransactionType.Explicit;
						connectionManager.WillDisconnectStorage();
						modelStorage = new ModelCatalogItem.ModelStorage(connectionManager);
						flag = true;
					}
					modelStorage.GetModelItemInfo(model, flag);
					if (model.BinarySnapshotID == Guid.Empty)
					{
						modelStorage.CompileModelDefinitionAndSaveBinarySnapshot(model, service);
					}
					if (flag)
					{
						modelStorage.Commit();
					}
				}
				finally
				{
					if (flag && modelStorage != null)
					{
						modelStorage.Disconnect();
					}
				}
			}

			// Token: 0x06002357 RID: 9047 RVA: 0x00084210 File Offset: 0x00082410
			private void SetCompiledModelFromCache(ModelCatalogItem model, ModelCatalogItem.ModelStorage.CachedModel cachedModel)
			{
				if (cachedModel.Model == null)
				{
					throw new InternalCatalogException("LoadModel: found cached model entry with null model.");
				}
				model.m_model = cachedModel.Model;
			}

			// Token: 0x06002358 RID: 9048 RVA: 0x00084234 File Offset: 0x00082434
			private long DeserializeFromBinarySnapshot(ModelCatalogItem model)
			{
				if (model.BinarySnapshotID == Guid.Empty)
				{
					throw new InternalCatalogException("DeserializeFromBinarySnapshot: found model without binary snapshot. Probably this model failed to upgrade and needs to be recreated.");
				}
				ModelSnapshot modelSnapshot = ModelSnapshot.Create(model.BinarySnapshotID);
				long num = 0L;
				using (ISnapshotTransaction snapshotTransaction = modelSnapshot.EnterTransactionContext())
				{
					using (Stream modelChunk = ModelCatalogItem.GetModelChunk(modelSnapshot))
					{
						num = modelChunk.Length;
						model.m_model = SemanticModel.LoadFromBinary(modelChunk);
					}
					snapshotTransaction.Commit();
				}
				return num;
			}

			// Token: 0x06002359 RID: 9049 RVA: 0x000842CC File Offset: 0x000824CC
			private void CompileModelDefinitionAndSaveBinarySnapshot(ModelCatalogItem model, RSService service)
			{
				if (model.Content == null)
				{
					this.GetModelDefinition(model);
				}
				ValidationMessageCollection validationMessageCollection;
				model.Model = ModelCatalogItem.CompileModelDefinition(model.Content, false, model.ItemContext.ItemPath.Value, out validationMessageCollection);
				SnapshotBase snapshotBase = CreateModelAction.CreateBinarySnapshot(model.m_model, service);
				base.UpdateCompiledDefinition(model.ItemContext.CatalogItemPath, model.BinarySnapshotID, snapshotBase.SnapshotDataID);
				model.m_binarySnapshotID = snapshotBase.SnapshotDataID;
			}

			// Token: 0x0600235A RID: 9050 RVA: 0x00084344 File Offset: 0x00082544
			internal void DeleteModelPerspectives(Guid modelCatalogItemID)
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteModelPerspectives", null))
				{
					instrumentedSqlCommand.AddParameter("@ModelID", SqlDbType.UniqueIdentifier, modelCatalogItemID);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}

			// Token: 0x0600235B RID: 9051 RVA: 0x00084398 File Offset: 0x00082598
			internal void AddModelPerspectives(Guid modelCatalogItemID, SemanticModel model)
			{
				foreach (Perspective perspective in model.Perspectives)
				{
					this.AddModelPerspective(modelCatalogItemID, perspective);
				}
			}

			// Token: 0x0600235C RID: 9052 RVA: 0x000843EC File Offset: 0x000825EC
			internal ModelCatalogItem ListModelPerspectives(CatalogItemContext itemContext, RSService service)
			{
				ModelCatalogItem modelCatalogItem = null;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetModelPerspectives", null))
				{
					instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, itemContext.CatalogItemPath.Value);
					instrumentedSqlCommand.AddParameter("@AuthType", SqlDbType.Int, (int)service.UserContext.AuthenticationType);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							throw new ItemNotFoundException(itemContext.OriginalItemPath.Value);
						}
						ItemType @int = (ItemType)dataReader.GetInt32(0);
						byte[] array = null;
						if (!dataReader.IsDBNull(1))
						{
							array = DataReaderHelper.ReadAllBytes(dataReader, 1);
						}
						string text = null;
						if (!dataReader.IsDBNull(2))
						{
							text = dataReader.GetString(2);
						}
						if (dataReader.Read())
						{
							throw new InternalCatalogException("ModelStorage.ListModelPerspectives: Read several models");
						}
						RSService.EnsureItemType(@int, itemContext.OriginalItemPath.Value, new ItemType[] { ItemType.Model });
						if (!service.SecMgr.CheckAccess(@int, array, ModelOperation.ReadProperties, itemContext.ItemPath))
						{
							throw new AccessDeniedException(service.UserName, ErrorCode.rsAccessDenied);
						}
						modelCatalogItem = new ModelCatalogItem();
						modelCatalogItem.Model = itemContext.ItemPath.Value;
						modelCatalogItem.Description = text;
						if (!dataReader.NextResult())
						{
							throw new InternalCatalogException("ModelStorage.ListModelPerspectives: no next result for existing model");
						}
						List<ModelPerspective> list = new List<ModelPerspective>();
						while (dataReader.Read())
						{
							string @string = dataReader.GetString(0);
							string text2 = null;
							if (!dataReader.IsDBNull(1))
							{
								text2 = dataReader.GetString(1);
							}
							string text3 = null;
							if (!dataReader.IsDBNull(2))
							{
								text3 = dataReader.GetString(2);
							}
							list.Add(new ModelPerspective
							{
								ID = @string,
								Name = text2,
								Description = text3
							});
						}
						modelCatalogItem.Perspectives = list.ToArray();
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("ModelStorage.ListModelPerspectives: third result found");
						}
					}
				}
				return modelCatalogItem;
			}

			// Token: 0x0600235D RID: 9053 RVA: 0x000845F0 File Offset: 0x000827F0
			internal ModelCatalogItem[] ListAllModelsAndPerspectives(ExternalItemPath sitePath, Hashtable filterModelIDs, RSService service)
			{
				ModelAndPerspectiveListBuilder modelAndPerspectiveListBuilder = new ModelAndPerspectiveListBuilder(service.SecMgr);
				if (filterModelIDs != null && filterModelIDs.Count == 0)
				{
					return modelAndPerspectiveListBuilder.GetModelAndPerspectiveList();
				}
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetModelsAndPerspectives", null))
				{
					instrumentedSqlCommand.AddParameter("@AuthType", SqlDbType.Int, (int)service.UserContext.AuthenticationType);
					if (sitePath != null)
					{
						instrumentedSqlCommand.AddParameter("@SitePathPrefix", SqlDbType.NVarChar, Storage.EncodeForLike(service.ExternalToCatalog(sitePath.Value)) + "/%");
					}
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						while (dataReader.Read())
						{
							Guid guid = dataReader.GetGuid(0);
							byte[] array = null;
							if (!dataReader.IsDBNull(1))
							{
								array = DataReaderHelper.ReadAllBytes(dataReader, 1);
							}
							Guid guid2 = dataReader.GetGuid(2);
							if (filterModelIDs == null || filterModelIDs.ContainsKey(guid2))
							{
								CatalogItemPath catalogItemPath = new CatalogItemPath(dataReader.GetString(3));
								string text = null;
								if (!dataReader.IsDBNull(4))
								{
									text = dataReader.GetString(4);
								}
								string text2 = null;
								if (!dataReader.IsDBNull(5))
								{
									text2 = dataReader.GetString(5);
								}
								string text3 = null;
								if (!dataReader.IsDBNull(6))
								{
									text3 = dataReader.GetString(6);
								}
								string text4 = null;
								if (!dataReader.IsDBNull(7))
								{
									text4 = dataReader.GetString(7);
								}
								modelAndPerspectiveListBuilder.AddModelPerspective(guid, array, guid2, service.CatalogToExternal(catalogItemPath), text, text2, text3, text4);
							}
						}
					}
				}
				return modelAndPerspectiveListBuilder.GetModelAndPerspectiveList();
			}

			// Token: 0x0600235E RID: 9054 RVA: 0x00084790 File Offset: 0x00082990
			private void AddModelPerspective(Guid modelCatalogItemID, Perspective perspective)
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddModelPerspective", null))
				{
					instrumentedSqlCommand.AddParameter("@ModelID", SqlDbType.UniqueIdentifier, modelCatalogItemID);
					instrumentedSqlCommand.AddParameter("@PerspectiveID", SqlDbType.NVarChar, Microsoft.ReportingServices.Modeling.ModelItem.IDToString(perspective.ID));
					if (perspective.Name != null)
					{
						instrumentedSqlCommand.AddParameter("@PerspectiveName", SqlDbType.NText, perspective.Name);
					}
					if (perspective.Description != null)
					{
						instrumentedSqlCommand.AddParameter("@PerspectiveDescription", SqlDbType.NText, perspective.Description);
					}
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}

			// Token: 0x0600235F RID: 9055 RVA: 0x00084834 File Offset: 0x00082A34
			private void GetModelItemInfo(ModelCatalogItem model, bool useUpdateLock)
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetModelItemInfo", null))
				{
					instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, model.ItemContext.CatalogItemPath.Value);
					instrumentedSqlCommand.AddParameter("@UseUpdateLock", SqlDbType.Bit, useUpdateLock);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							throw new ItemNotFoundException(model.ItemContext.OriginalItemPath.Value);
						}
						if (!dataReader.IsDBNull(0))
						{
							model.m_binarySnapshotID = dataReader.GetGuid(0);
						}
						if (dataReader.Read())
						{
							throw new InternalCatalogException("GetModelInfo: More than one model found.");
						}
						if (!dataReader.NextResult())
						{
							throw new InternalCatalogException("GetModelItemInfo: No model item security records returned.");
						}
						while (dataReader.Read())
						{
							string @string = dataReader.GetString(0);
							byte[] array = null;
							if (!dataReader.IsDBNull(1))
							{
								array = DataReaderHelper.ReadAllBytes(dataReader, 1);
							}
							string text;
							if (!dataReader.IsDBNull(2))
							{
								text = dataReader.GetString(2);
							}
							else
							{
								text = Security.ProduceEmptyPolicy();
							}
							model.ModelItemPolicies.AddPolicyRoot(@string, array, text, model.ItemContext.ItemPath);
						}
						if (dataReader.NextResult())
						{
							throw new InternalCatalogException("GetModelItemInfo: Found third result set.");
						}
					}
				}
			}

			// Token: 0x06002360 RID: 9056 RVA: 0x00084998 File Offset: 0x00082B98
			private void GetModelDefinition(ModelCatalogItem model)
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetModelDefinition", null))
				{
					instrumentedSqlCommand.AddParameter("@CatalogItemID", SqlDbType.UniqueIdentifier, model.m_itemID);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader(CommandBehavior.SequentialAccess))
					{
						if (!dataReader.Read())
						{
							throw new ItemNotFoundException(model.ItemContext.OriginalItemPath.Value);
						}
						if (!dataReader.IsDBNull(0))
						{
							try
							{
								model.m_content = new byte[dataReader.GetBytes(0, 0L, null, 0, 0)];
								dataReader.GetBytes(0, 0L, model.m_content, 0, model.m_content.Length);
							}
							catch (Exception ex)
							{
								throw new InternalCatalogException("GetModelDefintion: failed to load model definition from catalog: " + ex.ToString());
							}
						}
						if (dataReader.Read())
						{
							throw new InternalCatalogException("GetModelDefintion: More than one model found by name on model loading!");
						}
					}
				}
			}

			// Token: 0x04000FB5 RID: 4021
			private static readonly Dictionary<string, ModelCatalogItem.ModelStorage.ModelDeserializationLock> m_deserializingModels = new Dictionary<string, ModelCatalogItem.ModelStorage.ModelDeserializationLock>();

			// Token: 0x02000539 RID: 1337
			private sealed class ModelDeserializationLock
			{
				// Token: 0x04001377 RID: 4983
				internal int Count;
			}

			// Token: 0x0200053A RID: 1338
			internal sealed class CachedModel : CachedItemBase
			{
				// Token: 0x06002556 RID: 9558 RVA: 0x00088002 File Offset: 0x00086202
				internal CachedModel(SemanticModel model, Guid modelCatalogItemID, DateTime modifiedDate, long sizeEstimate)
					: base(ModelCatalogItem.ModelStorage.CachedModel.BuildKey(modelCatalogItemID, modifiedDate), Cache.NoAbsoluteExpiration)
				{
					this.m_model = model;
					this.m_sizeEstimateKb = sizeEstimate / 1024L;
				}

				// Token: 0x06002557 RID: 9559 RVA: 0x0008802C File Offset: 0x0008622C
				internal static string BuildKey(Guid modelCatalogItemID, DateTime modifiedDate)
				{
					return string.Format(CultureInfo.InvariantCulture, "SemanticModel:CatalogItemID={0},ModifiedDateTicks={1}({2})", modelCatalogItemID, modifiedDate.Ticks, modifiedDate);
				}

				// Token: 0x17000AC0 RID: 2752
				// (get) Token: 0x06002558 RID: 9560 RVA: 0x00088055 File Offset: 0x00086255
				internal SemanticModel Model
				{
					get
					{
						return this.m_model;
					}
				}

				// Token: 0x17000AC1 RID: 2753
				// (get) Token: 0x06002559 RID: 9561 RVA: 0x0008805D File Offset: 0x0008625D
				public override long SizeEstimateKb
				{
					get
					{
						return this.m_sizeEstimateKb;
					}
				}

				// Token: 0x04001378 RID: 4984
				private readonly long m_sizeEstimateKb;

				// Token: 0x04001379 RID: 4985
				private readonly SemanticModel m_model;
			}
		}
	}
}
