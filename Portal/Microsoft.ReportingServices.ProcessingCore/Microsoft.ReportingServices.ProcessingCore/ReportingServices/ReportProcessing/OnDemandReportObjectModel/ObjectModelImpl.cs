using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007AC RID: 1964
	public class ObjectModelImpl : OnDemandObjectModel, IConvertible, IStaticReferenceable
	{
		// Token: 0x06006F11 RID: 28433 RVA: 0x001D0324 File Offset: 0x001CE524
		internal ObjectModelImpl(OnDemandProcessingContext odpContext)
		{
			this.m_currentFields = null;
			this.m_parameters = null;
			this.m_globals = null;
			this.m_user = null;
			this.m_reportItems = null;
			this.m_aggregates = null;
			this.m_lookups = null;
			this.m_dataSets = null;
			this.m_dataSources = null;
			this.m_odpContext = odpContext;
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x001D0388 File Offset: 0x001CE588
		internal ObjectModelImpl(ObjectModelImpl copy, OnDemandProcessingContext odpContext)
		{
			this.m_odpContext = odpContext;
			this.m_currentFields = new FieldsContext(this);
			this.m_parameters = copy.m_parameters;
			this.m_globals = new GlobalsImpl(odpContext);
			this.m_user = new UserImpl(copy.m_user, odpContext);
			this.m_dataSets = copy.m_dataSets;
			this.m_dataSources = copy.m_dataSources;
			this.m_reportItems = null;
			this.m_aggregates = null;
			this.m_lookups = null;
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x001D0410 File Offset: 0x001CE610
		internal void Initialize(DataSetDefinition dataSetDefinition)
		{
			int num = 0;
			if (dataSetDefinition.DataSetCore != null && dataSetDefinition.DataSetCore.Query != null && dataSetDefinition.DataSetCore.Query.Parameters != null)
			{
				num = dataSetDefinition.DataSetCore.Query.Parameters.Count;
			}
			this.m_parameters = new ParametersImpl(num);
			this.InitializeGlobalAndUserCollections();
			this.m_currentFields = new FieldsContext(this, dataSetDefinition.DataSetCore);
			this.m_dataSources = new DataSourcesImpl(0);
			this.m_dataSets = new DataSetsImpl(0);
			this.m_variables = new VariablesImpl(false);
			this.m_aggregates = new AggregatesImpl(false, this.m_odpContext);
			this.m_reportItems = new ReportItemsImpl(false);
			this.m_lookups = new LookupsImpl();
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x001D04D0 File Offset: 0x001CE6D0
		internal void Initialize(Report report, ReportInstance reportInstance)
		{
			int num = 0;
			if (report.Parameters != null)
			{
				num = report.Parameters.Count;
			}
			this.m_parameters = new ParametersImpl(num);
			this.InitializeGlobalAndUserCollections();
			this.m_currentFields = new FieldsContext(this);
			this.m_dataSources = new DataSourcesImpl(report.DataSourceCount);
			this.m_dataSets = new DataSetsImpl(report.DataSetCount);
			this.InitOrUpdateDataSetCollection(report, reportInstance, true);
			this.m_variables = new VariablesImpl(false);
			this.m_aggregates = new AggregatesImpl(false, this.m_odpContext);
			this.m_reportItems = new ReportItemsImpl(false);
			this.m_lookups = new LookupsImpl();
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x001D0574 File Offset: 0x001CE774
		private void InitializeGlobalAndUserCollections()
		{
			this.m_globals = new GlobalsImpl(this.m_odpContext);
			this.m_user = new UserImpl(this.m_odpContext.RequestUserName, this.m_odpContext.UserLanguage.Name, this.m_odpContext.AllowUserProfileState, this.m_odpContext);
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x001D05CC File Offset: 0x001CE7CC
		private int InitOrUpdateDataSetCollection(Report report, ReportInstance reportInstance, bool initialize)
		{
			int num = 0;
			for (int i = 0; i < report.DataSourceCount; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource = report.DataSources[i];
				if (initialize && !dataSource.IsArtificialForSharedDataSets)
				{
					this.m_dataSources.Add(dataSource);
				}
				if (dataSource.DataSets != null)
				{
					for (int j = 0; j < dataSource.DataSets.Count; j++)
					{
						this.InitDataSet(reportInstance, dataSource.DataSets[j], ref num);
					}
				}
			}
			return num;
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x001D0644 File Offset: 0x001CE844
		private void InitDataSet(ReportInstance reportInstance, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, ref int dataSetCount)
		{
			DataSetInstance dataSetInstance = null;
			if (reportInstance != null)
			{
				dataSetInstance = reportInstance.GetDataSetInstance(dataSet, this.m_odpContext);
			}
			this.m_dataSets.AddOrUpdate(dataSet, dataSetInstance, this.m_odpContext.ExecutionTime);
			if (!dataSet.UsedOnlyInParameters)
			{
				dataSetCount++;
			}
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x001D068C File Offset: 0x001CE88C
		internal void Initialize(ParameterInfoCollection parameters)
		{
			this.m_parameters = new ParametersImpl(parameters.Count);
			if (parameters != null && parameters.Count > 0)
			{
				for (int i = 0; i < parameters.Count; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					this.m_parameters.Add(parameterInfo.Name, new ParameterImpl(parameterInfo));
				}
			}
		}

		// Token: 0x06006F19 RID: 28441 RVA: 0x001D06E8 File Offset: 0x001CE8E8
		internal void SetForNewSubReportContext(ParametersImpl parameters)
		{
			this.m_parameters = parameters;
			if (this.m_variables != null)
			{
				this.m_variables.ResetAll();
			}
			if (this.m_reportItems != null)
			{
				this.m_reportItems.ResetAll();
			}
			if (this.m_aggregates != null)
			{
				this.m_aggregates.ClearAll();
			}
			if (this.m_currentFields != null && this.m_currentFields.Fields != null)
			{
				this.ResetFieldValues();
			}
			this.InitOrUpdateDataSetCollection(this.m_odpContext.ReportDefinition, this.m_odpContext.CurrentReportInstance, false);
		}

		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x06006F1A RID: 28442 RVA: 0x001D076E File Offset: 0x001CE96E
		internal virtual bool UseDataSetFieldsCache
		{
			get
			{
				return !this.m_odpContext.InSubreport && !this.m_odpContext.IsPageHeaderFooter;
			}
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x001D078D File Offset: 0x001CE98D
		internal void SetupEmptyTopLevelFields()
		{
			this.m_currentFields = new FieldsContext(this);
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x001D079B File Offset: 0x001CE99B
		internal void SetupPageSectionDataSetFields(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataset)
		{
			this.m_currentFields = new FieldsContext(this, dataset.DataSetCore, false, true);
			this.m_currentFields.Fields.NeedsInlineSetup = true;
		}

		// Token: 0x06006F1D RID: 28445 RVA: 0x001D07C2 File Offset: 0x001CE9C2
		internal void SetupFieldsForNewDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataset, bool addRowIndex, bool noRows, bool forceNewFieldsContext)
		{
			this.m_currentFields.ResetFieldFlags();
			this.SetupFieldsForNewDataSetWithoutResettingOldFieldFlags(dataset, addRowIndex, noRows, forceNewFieldsContext);
		}

		// Token: 0x06006F1E RID: 28446 RVA: 0x001D07DC File Offset: 0x001CE9DC
		private void SetupFieldsForNewDataSetWithoutResettingOldFieldFlags(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataset, bool addRowIndex, bool noRows, bool forceNewFieldsContext)
		{
			this.m_currentFields = (this.UseDataSetFieldsCache ? dataset.DataSetCore.FieldsContext : null);
			if (this.m_currentFields == null || !this.m_currentFields.Fields.IsCollectionInitialized || this.m_currentFields.Fields.NeedsInlineSetup || forceNewFieldsContext)
			{
				this.m_currentFields = new FieldsContext(this, dataset.DataSetCore, addRowIndex, noRows);
			}
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x001D084B File Offset: 0x001CEA4B
		internal void CreateNoRows()
		{
			this.m_currentFields.CreateNoRows();
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x001D0858 File Offset: 0x001CEA58
		internal void ResetFieldValues()
		{
			if (this.m_odpContext.IsTablixProcessingMode || this.m_odpContext.StreamingMode)
			{
				this.m_currentFields.CreateNoRows();
				return;
			}
			this.m_currentFields.CreateNullFieldValues();
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x001D088B File Offset: 0x001CEA8B
		internal void PerformPendingFieldValueUpdate()
		{
			this.m_currentFields.PerformPendingFieldValueUpdate(this, this.UseDataSetFieldsCache);
		}

		// Token: 0x06006F22 RID: 28450 RVA: 0x001D089F File Offset: 0x001CEA9F
		internal void RegisterOnDemandFieldValueUpdate(long firstRowOffsetInScope, DataSetInstance dataSetInstance, ChunkManager.DataChunkReader dataReader)
		{
			this.m_currentFields.RegisterOnDemandFieldValueUpdate(firstRowOffsetInScope, dataSetInstance, dataReader);
		}

		// Token: 0x06006F23 RID: 28451 RVA: 0x001D08AF File Offset: 0x001CEAAF
		internal void UpdateFieldValues(long firstRowOffsetInScope)
		{
			this.m_currentFields.UpdateFieldValues(this, this.UseDataSetFieldsCache, firstRowOffsetInScope);
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x001D08C4 File Offset: 0x001CEAC4
		internal void UpdateFieldValues(bool reuseFieldObjects, RecordRow row, DataSetInstance dataSetInstance, bool readerExtensionsSupported)
		{
			this.m_currentFields.UpdateFieldValues(this, this.UseDataSetFieldsCache, reuseFieldObjects, row, dataSetInstance, readerExtensionsSupported);
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x001D08DD File Offset: 0x001CEADD
		internal void ResetFieldsUsedInExpression()
		{
			this.FieldsImpl.ResetFieldsUsedInExpression();
			this.AggregatesImpl.ResetFieldsUsedInExpression();
		}

		// Token: 0x06006F26 RID: 28454 RVA: 0x001D08F5 File Offset: 0x001CEAF5
		internal void AddFieldsUsedInExpression(List<string> fieldsUsedInExpression)
		{
			this.FieldsImpl.AddFieldsUsedInExpression(fieldsUsedInExpression);
			this.AggregatesImpl.AddFieldsUsedInExpression(this.m_odpContext, fieldsUsedInExpression);
		}

		// Token: 0x06006F27 RID: 28455 RVA: 0x001D0918 File Offset: 0x001CEB18
		internal ObjectModelImpl.SecondaryFieldsCollectionWithAutomaticRestore SetupNewFieldsWithBackup(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataset, DataSetInstance dataSetInstance, ChunkManager.DataChunkReader dataChunkReader)
		{
			ObjectModelImpl.SecondaryFieldsCollectionWithAutomaticRestore secondaryFieldsCollectionWithAutomaticRestore = new ObjectModelImpl.SecondaryFieldsCollectionWithAutomaticRestore(this, this.m_currentFields);
			bool flag = this.m_currentFields.Fields.Count != this.m_currentFields.Fields.CountWithRowIndex;
			this.SetupFieldsForNewDataSetWithoutResettingOldFieldFlags(dataset, flag, dataSetInstance.NoRows, true);
			this.m_currentFields.UpdateDataSetInfo(dataSetInstance, dataChunkReader);
			return secondaryFieldsCollectionWithAutomaticRestore;
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x001D0973 File Offset: 0x001CEB73
		internal void RestoreFields(FieldsContext fieldsContext)
		{
			this.m_currentFields = fieldsContext;
			this.m_currentFields.AttachToDataSetCache(this);
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x06006F29 RID: 28457 RVA: 0x001D0988 File Offset: 0x001CEB88
		public override Fields Fields
		{
			get
			{
				return this.FieldsImpl;
			}
		}

		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x06006F2A RID: 28458 RVA: 0x001D0990 File Offset: 0x001CEB90
		public override Parameters Parameters
		{
			get
			{
				return this.ParametersImpl;
			}
		}

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x06006F2B RID: 28459 RVA: 0x001D0998 File Offset: 0x001CEB98
		public override Globals Globals
		{
			get
			{
				return this.GlobalsImpl;
			}
		}

		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x06006F2C RID: 28460 RVA: 0x001D09A0 File Offset: 0x001CEBA0
		public override User User
		{
			get
			{
				return this.UserImpl;
			}
		}

		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x06006F2D RID: 28461 RVA: 0x001D09A8 File Offset: 0x001CEBA8
		public override ReportItems ReportItems
		{
			get
			{
				return this.ReportItemsImpl;
			}
		}

		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x06006F2E RID: 28462 RVA: 0x001D09B0 File Offset: 0x001CEBB0
		public override Aggregates Aggregates
		{
			get
			{
				return this.AggregatesImpl;
			}
		}

		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x06006F2F RID: 28463 RVA: 0x001D09B8 File Offset: 0x001CEBB8
		public override Lookups Lookups
		{
			get
			{
				return this.LookupsImpl;
			}
		}

		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x06006F30 RID: 28464 RVA: 0x001D09C0 File Offset: 0x001CEBC0
		public override DataSets DataSets
		{
			get
			{
				return this.DataSetsImpl;
			}
		}

		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x06006F31 RID: 28465 RVA: 0x001D09C8 File Offset: 0x001CEBC8
		public override DataSources DataSources
		{
			get
			{
				return this.DataSourcesImpl;
			}
		}

		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x06006F32 RID: 28466 RVA: 0x001D09D0 File Offset: 0x001CEBD0
		public override Variables Variables
		{
			get
			{
				return this.VariablesImpl;
			}
		}

		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x06006F33 RID: 28467 RVA: 0x001D09D8 File Offset: 0x001CEBD8
		internal FieldsContext CurrentFields
		{
			get
			{
				return this.m_currentFields;
			}
		}

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x06006F34 RID: 28468 RVA: 0x001D09E0 File Offset: 0x001CEBE0
		internal FieldsImpl FieldsImpl
		{
			get
			{
				return this.m_currentFields.Fields;
			}
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x001D09F0 File Offset: 0x001CEBF0
		internal FieldsImpl GetFieldsImplForUpdate(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet currentDataSet)
		{
			if (currentDataSet.DataSetCore != this.m_currentFields.DataSet)
			{
				if (currentDataSet.DataSetCore.FieldsContext != null && this.UseDataSetFieldsCache)
				{
					this.m_currentFields = currentDataSet.DataSetCore.FieldsContext;
				}
				else
				{
					Global.Tracer.Assert(false, "Fields collection is not setup correctly. Actual: " + this.m_currentFields.DataSet.Name + " Expected: " + currentDataSet.DataSetCore.Name);
				}
			}
			return this.m_currentFields.Fields;
		}

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x06006F36 RID: 28470 RVA: 0x001D0A78 File Offset: 0x001CEC78
		// (set) Token: 0x06006F37 RID: 28471 RVA: 0x001D0A80 File Offset: 0x001CEC80
		internal ParametersImpl ParametersImpl
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x06006F38 RID: 28472 RVA: 0x001D0A89 File Offset: 0x001CEC89
		// (set) Token: 0x06006F39 RID: 28473 RVA: 0x001D0A91 File Offset: 0x001CEC91
		internal GlobalsImpl GlobalsImpl
		{
			get
			{
				return this.m_globals;
			}
			set
			{
				this.m_globals = value;
			}
		}

		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x06006F3A RID: 28474 RVA: 0x001D0A9A File Offset: 0x001CEC9A
		// (set) Token: 0x06006F3B RID: 28475 RVA: 0x001D0AA2 File Offset: 0x001CECA2
		internal UserImpl UserImpl
		{
			get
			{
				return this.m_user;
			}
			set
			{
				this.m_user = value;
			}
		}

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x06006F3C RID: 28476 RVA: 0x001D0AAB File Offset: 0x001CECAB
		// (set) Token: 0x06006F3D RID: 28477 RVA: 0x001D0AB3 File Offset: 0x001CECB3
		internal ReportItemsImpl ReportItemsImpl
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x06006F3E RID: 28478 RVA: 0x001D0ABC File Offset: 0x001CECBC
		// (set) Token: 0x06006F3F RID: 28479 RVA: 0x001D0AC4 File Offset: 0x001CECC4
		internal AggregatesImpl AggregatesImpl
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x06006F40 RID: 28480 RVA: 0x001D0ACD File Offset: 0x001CECCD
		// (set) Token: 0x06006F41 RID: 28481 RVA: 0x001D0AD5 File Offset: 0x001CECD5
		internal LookupsImpl LookupsImpl
		{
			get
			{
				return this.m_lookups;
			}
			set
			{
				this.m_lookups = value;
			}
		}

		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x06006F42 RID: 28482 RVA: 0x001D0ADE File Offset: 0x001CECDE
		// (set) Token: 0x06006F43 RID: 28483 RVA: 0x001D0AE6 File Offset: 0x001CECE6
		internal DataSetsImpl DataSetsImpl
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x06006F44 RID: 28484 RVA: 0x001D0AEF File Offset: 0x001CECEF
		// (set) Token: 0x06006F45 RID: 28485 RVA: 0x001D0AF7 File Offset: 0x001CECF7
		internal DataSourcesImpl DataSourcesImpl
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x06006F46 RID: 28486 RVA: 0x001D0B00 File Offset: 0x001CED00
		// (set) Token: 0x06006F47 RID: 28487 RVA: 0x001D0B08 File Offset: 0x001CED08
		internal VariablesImpl VariablesImpl
		{
			get
			{
				return this.m_variables;
			}
			set
			{
				this.m_variables = value;
			}
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x06006F48 RID: 28488 RVA: 0x001D0B11 File Offset: 0x001CED11
		internal OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x06006F49 RID: 28489 RVA: 0x001D0B19 File Offset: 0x001CED19
		internal bool AllFieldsCleared
		{
			get
			{
				return this.m_currentFields.AllFieldsCleared;
			}
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x001D0B26 File Offset: 0x001CED26
		public override bool InScope(string scope)
		{
			return this.m_odpContext.InScope(scope);
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x001D0B34 File Offset: 0x001CED34
		public override int RecursiveLevel(string scope)
		{
			return this.m_odpContext.RecursiveLevel(scope);
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x001D0B42 File Offset: 0x001CED42
		public override string CreateDrillthroughContext()
		{
			return this.m_odpContext.ReportRuntime.CreateDrillthroughContext();
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x001D0B54 File Offset: 0x001CED54
		public override object MinValue(params object[] arguments)
		{
			return this.m_odpContext.ReportRuntime.MinValue(arguments);
		}

		// Token: 0x06006F4E RID: 28494 RVA: 0x001D0B67 File Offset: 0x001CED67
		public override object MaxValue(params object[] arguments)
		{
			return this.m_odpContext.ReportRuntime.MaxValue(arguments);
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x06006F4F RID: 28495 RVA: 0x001D0B7A File Offset: 0x001CED7A
		int IStaticReferenceable.ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x06006F50 RID: 28496 RVA: 0x001D0B82 File Offset: 0x001CED82
		void IStaticReferenceable.SetID(int id)
		{
			this.m_id = id;
		}

		// Token: 0x06006F51 RID: 28497 RVA: 0x001D0B8B File Offset: 0x001CED8B
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IStaticReferenceable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ObjectModelImpl;
		}

		// Token: 0x06006F52 RID: 28498 RVA: 0x001D0B92 File Offset: 0x001CED92
		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		// Token: 0x06006F53 RID: 28499 RVA: 0x001D0B95 File Offset: 0x001CED95
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F54 RID: 28500 RVA: 0x001D0B9C File Offset: 0x001CED9C
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F55 RID: 28501 RVA: 0x001D0BA3 File Offset: 0x001CEDA3
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x001D0BAA File Offset: 0x001CEDAA
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x001D0BB1 File Offset: 0x001CEDB1
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F58 RID: 28504 RVA: 0x001D0BB8 File Offset: 0x001CEDB8
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F59 RID: 28505 RVA: 0x001D0BBF File Offset: 0x001CEDBF
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x001D0BC6 File Offset: 0x001CEDC6
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x001D0BCD File Offset: 0x001CEDCD
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F5C RID: 28508 RVA: 0x001D0BD4 File Offset: 0x001CEDD4
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x001D0BDB File Offset: 0x001CEDDB
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x001D0BE2 File Offset: 0x001CEDE2
		string IConvertible.ToString(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F5F RID: 28511 RVA: 0x001D0BE9 File Offset: 0x001CEDE9
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType == typeof(ObjectModel))
			{
				return this;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006F60 RID: 28512 RVA: 0x001D0C04 File Offset: 0x001CEE04
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F61 RID: 28513 RVA: 0x001D0C0B File Offset: 0x001CEE0B
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006F62 RID: 28514 RVA: 0x001D0C12 File Offset: 0x001CEE12
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003992 RID: 14738
		private FieldsContext m_currentFields;

		// Token: 0x04003993 RID: 14739
		private ParametersImpl m_parameters;

		// Token: 0x04003994 RID: 14740
		private GlobalsImpl m_globals;

		// Token: 0x04003995 RID: 14741
		private UserImpl m_user;

		// Token: 0x04003996 RID: 14742
		private ReportItemsImpl m_reportItems;

		// Token: 0x04003997 RID: 14743
		private AggregatesImpl m_aggregates;

		// Token: 0x04003998 RID: 14744
		private LookupsImpl m_lookups;

		// Token: 0x04003999 RID: 14745
		private DataSetsImpl m_dataSets;

		// Token: 0x0400399A RID: 14746
		private DataSourcesImpl m_dataSources;

		// Token: 0x0400399B RID: 14747
		private VariablesImpl m_variables;

		// Token: 0x0400399C RID: 14748
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x0400399D RID: 14749
		private int m_id = int.MinValue;

		// Token: 0x0400399E RID: 14750
		internal const string NamespacePrefix = "Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.";

		// Token: 0x02000CF1 RID: 3313
		internal sealed class SecondaryFieldsCollectionWithAutomaticRestore : IDisposable
		{
			// Token: 0x06008E0D RID: 36365 RVA: 0x00244080 File Offset: 0x00242280
			internal SecondaryFieldsCollectionWithAutomaticRestore(ObjectModelImpl reportOM, FieldsContext fieldsContext)
			{
				this.m_reportOM = reportOM;
				this.m_fieldsContext = fieldsContext;
			}

			// Token: 0x06008E0E RID: 36366 RVA: 0x00244096 File Offset: 0x00242296
			public void Dispose()
			{
				this.m_reportOM.RestoreFields(this.m_fieldsContext);
			}

			// Token: 0x04004FB9 RID: 20409
			private ObjectModelImpl m_reportOM;

			// Token: 0x04004FBA RID: 20410
			private FieldsContext m_fieldsContext;
		}
	}
}
