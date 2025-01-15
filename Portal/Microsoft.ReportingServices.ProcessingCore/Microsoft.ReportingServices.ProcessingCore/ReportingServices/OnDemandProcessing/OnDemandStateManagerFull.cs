using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000822 RID: 2082
	internal sealed class OnDemandStateManagerFull : OnDemandStateManager
	{
		// Token: 0x06007476 RID: 29814 RVA: 0x001E2019 File Offset: 0x001E0219
		public OnDemandStateManagerFull(OnDemandProcessingContext odpContext)
			: base(odpContext)
		{
		}

		// Token: 0x1700276D RID: 10093
		// (get) Token: 0x06007477 RID: 29815 RVA: 0x001E202D File Offset: 0x001E022D
		internal override IReportScopeInstance LastROMInstance
		{
			get
			{
				return this.m_lastROMInstance;
			}
		}

		// Token: 0x1700276E RID: 10094
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x001E2035 File Offset: 0x001E0235
		// (set) Token: 0x06007479 RID: 29817 RVA: 0x001E203D File Offset: 0x001E023D
		internal override IInstancePath LastRIFObject
		{
			get
			{
				return this.m_lastRIFObject;
			}
			set
			{
				this.m_lastRIFObject = value;
			}
		}

		// Token: 0x1700276F RID: 10095
		// (get) Token: 0x0600747A RID: 29818 RVA: 0x001E2046 File Offset: 0x001E0246
		// (set) Token: 0x0600747B RID: 29819 RVA: 0x001E204E File Offset: 0x001E024E
		internal override IRIFReportScope LastTablixProcessingReportScope
		{
			get
			{
				return this.m_lastTablixProcessingReportScope;
			}
			set
			{
				this.m_lastTablixProcessingReportScope = value;
			}
		}

		// Token: 0x17002770 RID: 10096
		// (get) Token: 0x0600747C RID: 29820 RVA: 0x001E2057 File Offset: 0x001E0257
		internal override QueryRestartInfo QueryRestartInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002771 RID: 10097
		// (get) Token: 0x0600747D RID: 29821 RVA: 0x001E205A File Offset: 0x001E025A
		internal override ExecutedQueryCache ExecutedQueryCache
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600747E RID: 29822 RVA: 0x001E205D File Offset: 0x001E025D
		internal override ExecutedQueryCache SetupExecutedQueryCache()
		{
			return this.ExecutedQueryCache;
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x001E2068 File Offset: 0x001E0268
		internal override Dictionary<string, object> GetCurrentSpecialGroupingValues()
		{
			int num = ((this.m_lastInstancePath == null) ? 0 : this.m_lastInstancePath.Count);
			Dictionary<string, object> dictionary = new Dictionary<string, object>(num, StringComparer.Ordinal);
			for (int i = 0; i < num; i++)
			{
				PairObj<string, object> pairObj = this.m_specialLastGroupingValues[i];
				if (pairObj != null && !dictionary.ContainsKey(pairObj.First))
				{
					dictionary.Add(pairObj.First, pairObj.Second);
				}
			}
			return dictionary;
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x001E20D8 File Offset: 0x001E02D8
		internal override bool CalculateAggregate(string aggregateName)
		{
			OnDemandProcessingContext odpWorkerContextForTablixProcessing = base.GetOdpWorkerContextForTablixProcessing();
			Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo;
			odpWorkerContextForTablixProcessing.ReportAggregates.TryGetValue(aggregateName, out dataAggregateInfo);
			if (dataAggregateInfo == null)
			{
				return false;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_odpContext.ReportDefinition.MappingDataSetIndexToDataSet[dataAggregateInfo.DataSetIndexInCollection];
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance = odpWorkerContextForTablixProcessing.GetDataSetInstance(dataSet);
			if (dataSetInstance != null)
			{
				bool flag = odpWorkerContextForTablixProcessing.IsTablixProcessingComplete(dataSet.IndexInCollection);
				if (!flag)
				{
					if (odpWorkerContextForTablixProcessing.IsTablixProcessingMode)
					{
						return false;
					}
					((OnDemandStateManagerFull)odpWorkerContextForTablixProcessing.StateManager).PerformOnDemandTablixProcessingWithContextRestore(dataSet);
				}
				if (flag || this.m_odpContext.IsPageHeaderFooter)
				{
					dataSetInstance.SetupDataSetLevelAggregates(this.m_odpContext);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06007481 RID: 29825 RVA: 0x001E2174 File Offset: 0x001E0374
		internal override bool CalculateLookup(LookupInfo lookup)
		{
			OnDemandProcessingContext odpWorkerContextForTablixProcessing = base.GetOdpWorkerContextForTablixProcessing();
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_odpContext.ReportDefinition.MappingDataSetIndexToDataSet[lookup.DataSetIndexInCollection];
			if (odpWorkerContextForTablixProcessing.GetDataSetInstance(dataSet) != null)
			{
				if (!odpWorkerContextForTablixProcessing.IsTablixProcessingComplete(dataSet.IndexInCollection))
				{
					if (odpWorkerContextForTablixProcessing.IsTablixProcessingMode)
					{
						return false;
					}
					((OnDemandStateManagerFull)odpWorkerContextForTablixProcessing.StateManager).PerformOnDemandTablixProcessingWithContextRestore(dataSet);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06007482 RID: 29826 RVA: 0x001E21DC File Offset: 0x001E03DC
		internal override bool PrepareFieldsCollectionForDirectFields()
		{
			if (this.m_odpContext.IsPageHeaderFooter && this.m_odpContext.ReportDefinition.DataSetsNotOnlyUsedInParameters == 1)
			{
				OnDemandProcessingContext parentContext = this.m_odpContext.ParentContext;
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet firstDataSet = this.m_odpContext.ReportDefinition.FirstDataSet;
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance = parentContext.GetDataSetInstance(firstDataSet);
				if (dataSetInstance != null)
				{
					if (!parentContext.IsTablixProcessingComplete(firstDataSet.IndexInCollection))
					{
						((OnDemandStateManagerFull)parentContext.StateManager).PerformOnDemandTablixProcessing(firstDataSet);
					}
					if (!dataSetInstance.NoRows)
					{
						dataSetInstance.SetupEnvironment(this.m_odpContext, false);
						parentContext.GetDataChunkReader(firstDataSet.IndexInCollection).ResetCachedStreamOffset();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007483 RID: 29827 RVA: 0x001E227C File Offset: 0x001E047C
		internal override void EvaluateScopedFieldReference(string scopeName, int fieldIndex, ref Microsoft.ReportingServices.RdlExpressions.VariantResult result)
		{
			Global.Tracer.Assert(false, "Scoped field references are not supported in Full ODP mode.");
			throw new NotImplementedException();
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x001E2294 File Offset: 0x001E0494
		internal override int RecursiveLevel(string scopeName)
		{
			if (this.m_odpContext.IsTablixProcessingMode)
			{
				return this.m_odpContext.ReportRuntime.RecursiveLevel(scopeName);
			}
			if (scopeName == null)
			{
				if (this.m_inRecursiveRowHierarchy && this.m_inRecursiveColumnHierarchy)
				{
					return 0;
				}
				return this.m_lastRecursiveLevel;
			}
			else
			{
				this.m_lastRecursiveLevel = 0;
				this.SetupObjectModels(OnDemandMode.InScope, false, -1, scopeName);
				if (this.m_lastInScopeResult)
				{
					return this.m_lastRecursiveLevel;
				}
				return 0;
			}
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x001E2300 File Offset: 0x001E0500
		internal override bool InScope(string scopeName)
		{
			this.m_lastInScopeResult = false;
			if (this.m_odpContext.IsTablixProcessingMode)
			{
				return this.m_odpContext.ReportRuntime.CurrentScope != null && this.m_odpContext.ReportRuntime.CurrentScope.InScope(scopeName);
			}
			if (this.m_lastInstancePath != null && scopeName != null)
			{
				this.SetupObjectModels(OnDemandMode.InScope, false, -1, scopeName);
			}
			return this.m_lastInScopeResult;
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x001E2367 File Offset: 0x001E0567
		internal override void ResetOnDemandState()
		{
			this.m_lastInstancePath = null;
			this.m_lastRecursiveLevel = 0;
			this.m_lastInScopeResult = false;
			this.m_lastTablixProcessingReportScope = null;
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x001E2385 File Offset: 0x001E0585
		private bool InScopeCompare(string scope1, string scope2)
		{
			this.m_lastInScopeResult = string.CompareOrdinal(scope1, scope2) == 0;
			return this.m_lastInScopeResult;
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x001E239D File Offset: 0x001E059D
		internal override void RestoreContext(IInstancePath originalObject)
		{
			if (originalObject == null || !this.m_odpContext.ReportRuntime.ContextUpdated)
			{
				return;
			}
			if (!InstancePathItem.IsSameScopePath(originalObject, this.m_lastRIFObject))
			{
				this.SetupContext(originalObject, null, -1);
			}
		}

		// Token: 0x06007489 RID: 29833 RVA: 0x001E23CC File Offset: 0x001E05CC
		internal override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance)
		{
			this.SetupContext(rifObject, romInstance, -1);
		}

		// Token: 0x0600748A RID: 29834 RVA: 0x001E23D8 File Offset: 0x001E05D8
		internal override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			bool flag = false;
			bool flag2 = false;
			if (romInstance == null)
			{
				flag = true;
				this.m_lastRIFObject = rifObject;
				flag2 = true;
			}
			else if (romInstance.IsNewContext || this.m_lastROMInstance == null || this.m_lastRIFObject == null || 0 <= moveNextInstanceIndex)
			{
				flag = true;
				romInstance.IsNewContext = false;
				this.m_lastROMInstance = romInstance;
				this.m_lastRIFObject = rifObject;
				flag2 = true;
			}
			else if (this.m_lastROMInstance.Equals(romInstance))
			{
				if (!this.m_lastRIFObject.Equals(rifObject) && (this.m_lastRIFObject.InstancePathItem.Type == InstancePathItemType.SubReport || rifObject.InstancePathItem.Type == InstancePathItemType.SubReport))
				{
					flag = true;
				}
				this.m_lastRIFObject = rifObject;
			}
			else if (this.m_lastRIFObject.Equals(rifObject))
			{
				this.m_lastROMInstance = romInstance;
			}
			else if (InstancePathItem.IsSamePath(this.m_lastInstancePath, rifObject.InstancePath))
			{
				this.m_lastROMInstance = romInstance;
				this.m_lastRIFObject = rifObject;
			}
			else
			{
				flag = true;
				this.m_lastROMInstance = romInstance;
				this.m_lastRIFObject = rifObject;
				flag2 = true;
			}
			if (flag)
			{
				this.SetupObjectModels(OnDemandMode.FullSetup, flag2, moveNextInstanceIndex, null);
				this.m_odpContext.ReportRuntime.ContextUpdated = true;
			}
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x001E24E8 File Offset: 0x001E06E8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlThread)]
		private static void UpdateThreadCultureWithAssert(CultureInfo newCulture)
		{
			Thread.CurrentThread.CurrentCulture = newCulture;
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x001E24F8 File Offset: 0x001E06F8
		private void SetupObjectModels(OnDemandMode mode, bool needDeepCopyPath, int moveNextInstanceIndex, string scopeName)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegionInstance dataRegionInstance = null;
			IMemberHierarchy memberHierarchy = null;
			int num = -1;
			ScopeInstance scopeInstance = this.m_odpContext.CurrentReportInstance;
			List<InstancePathItem> lastInstancePath = this.m_lastInstancePath;
			List<InstancePathItem> list = null;
			int num2 = 0;
			Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDefinition = this.m_odpContext.ReportDefinition;
			ObjectModelImpl reportObjectModel = this.m_odpContext.ReportObjectModel;
			bool flag = false;
			bool flag2 = false;
			int i = 0;
			try
			{
				if (this.m_lastRIFObject.InstancePath != null)
				{
					list = this.m_lastRIFObject.InstancePath;
					num2 = list.Count;
				}
				if (mode != OnDemandMode.InScope)
				{
					this.m_odpContext.EnsureCultureIsSetOnCurrentThread();
				}
				if (mode != OnDemandMode.InScope || 1 != reportDefinition.DataSetsNotOnlyUsedInParameters || !this.InScopeCompare(reportDefinition.FirstDataSet.Name, scopeName))
				{
					int num3 = 0;
					if (this.m_odpContext.InSubreport)
					{
						num3 = InstancePathItem.GetParentReportIndex(this.m_lastRIFObject.InstancePath, this.m_lastRIFObject.InstancePathItem.Type == InstancePathItemType.SubReport);
					}
					bool flag3;
					int sharedPathIndex = InstancePathItem.GetSharedPathIndex(num3, lastInstancePath, list, reportObjectModel.AllFieldsCleared, out flag3);
					for (int j = this.m_specialLastGroupingValues.Count; j < num3; j++)
					{
						this.m_specialLastGroupingValues.Add(null);
					}
					int k = num3;
					while (k < num2)
					{
						InstancePathItem instancePathItem = list[k];
						bool flag4 = false;
						if (mode != OnDemandMode.InScope)
						{
							flag4 = k <= sharedPathIndex;
						}
						if (!flag4 && mode == OnDemandMode.FullSetup)
						{
							if (this.m_specialLastGroupingValues.Count < num2)
							{
								this.m_specialLastGroupingValues.Add(null);
							}
							else
							{
								this.m_specialLastGroupingValues[k] = null;
							}
						}
						switch (instancePathItem.Type)
						{
						case InstancePathItemType.None:
							IL_070F:
							k++;
							continue;
						case InstancePathItemType.DataRegion:
							goto IL_0250;
						case InstancePathItemType.SubReport:
							if (scopeInstance.SubreportInstances != null && instancePathItem.IndexInCollection < scopeInstance.SubreportInstances.Count)
							{
								IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> reference = scopeInstance.SubreportInstances[instancePathItem.IndexInCollection];
								using (reference.PinValue())
								{
									Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance = reference.Value();
									subReportInstance.SubReportDef.CurrentSubReportInstance = reference;
									if (mode != OnDemandMode.InScope && !subReportInstance.Initialized)
									{
										if (this.m_odpContext.IsTablixProcessingMode || this.m_odpContext.IsTopLevelSubReportProcessing)
										{
											return;
										}
										SubReportInitializer.InitializeSubReport(subReportInstance.SubReportDef);
										reference.PinValue();
									}
									Global.Tracer.Assert(k == num2 - 1, "SubReport not last in instance path.");
									break;
								}
								goto IL_0250;
							}
							break;
						case InstancePathItemType.Cell:
						{
							if (-1 == num)
							{
								num = 0;
							}
							IList<Microsoft.ReportingServices.ReportIntermediateFormat.DataCellInstance> cellInstances = memberHierarchy.GetCellInstances(num);
							if (cellInstances == null)
							{
								if (flag2 && flag)
								{
									reportObjectModel.ResetFieldValues();
								}
							}
							else if (cellInstances.Count > instancePathItem.IndexInCollection)
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.DataCellInstance dataCellInstance = cellInstances[instancePathItem.IndexInCollection];
								if (dataCellInstance != null)
								{
									scopeInstance = dataCellInstance;
									if (!flag4)
									{
										dataCellInstance.SetupEnvironment(this.m_odpContext, this.m_odpContext.CurrentDataSetIndex);
										i = 0;
									}
									else
									{
										i = k + 1;
									}
								}
							}
							break;
						}
						case InstancePathItemType.ColumnMemberInstanceIndexTopMost:
							scopeInstance = dataRegionInstance;
							break;
						}
						IL_0486:
						if (!instancePathItem.IsDynamicMember)
						{
							goto IL_070F;
						}
						IList<DataRegionMemberInstance> childMemberInstances = ((IMemberHierarchy)scopeInstance).GetChildMemberInstances(instancePathItem.Type == InstancePathItemType.RowMemberInstanceIndex, instancePathItem.IndexInCollection);
						if (childMemberInstances == null)
						{
							reportObjectModel.ResetFieldValues();
							return;
						}
						int num4;
						if (k == num2 - 1 && moveNextInstanceIndex >= 0 && moveNextInstanceIndex < childMemberInstances.Count)
						{
							num4 = moveNextInstanceIndex;
						}
						else
						{
							num4 = ((instancePathItem.InstanceIndex < 0) ? 0 : instancePathItem.InstanceIndex);
						}
						if (num4 >= childMemberInstances.Count)
						{
							instancePathItem.ResetContext();
							num4 = 0;
						}
						DataRegionMemberInstance dataRegionMemberInstance = childMemberInstances[num4];
						if (mode == OnDemandMode.FullSetup)
						{
							dataRegionMemberInstance.MemberDef.InstanceCount = childMemberInstances.Count;
							dataRegionMemberInstance.MemberDef.CurrentMemberIndex = num4;
						}
						scopeInstance = dataRegionMemberInstance;
						this.m_lastRecursiveLevel = dataRegionMemberInstance.RecursiveLevel;
						Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef = dataRegionMemberInstance.MemberDef;
						if (mode == OnDemandMode.InScope && this.InScopeCompare(memberDef.Grouping.Name, scopeName))
						{
							return;
						}
						if (instancePathItem.Type == InstancePathItemType.RowMemberInstanceIndex)
						{
							memberHierarchy = dataRegionMemberInstance;
							flag2 = true;
						}
						else
						{
							num = dataRegionMemberInstance.MemberInstanceIndexWithinScopeLevel;
							flag = true;
						}
						if (mode != OnDemandMode.FullSetup || flag4)
						{
							i = k + 1;
							goto IL_070F;
						}
						dataRegionMemberInstance.SetupEnvironment(this.m_odpContext, this.m_odpContext.CurrentDataSetIndex);
						i = 0;
						Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = memberDef.Grouping;
						if (grouping.Parent != null)
						{
							if (memberDef.IsColumn)
							{
								this.m_inRecursiveColumnHierarchy = true;
							}
							else
							{
								this.m_inRecursiveRowHierarchy = true;
							}
							if (memberDef.IsTablixMember)
							{
								memberDef.SetMemberInstances(childMemberInstances);
								memberDef.SetRecursiveParentIndex(dataRegionMemberInstance.RecursiveParentIndex);
								memberDef.SetInstanceHasRecursiveChildren(dataRegionMemberInstance.HasRecursiveChildren);
							}
						}
						else if (memberDef.IsColumn)
						{
							this.m_inRecursiveColumnHierarchy = false;
						}
						else
						{
							this.m_inRecursiveRowHierarchy = false;
						}
						grouping.RecursiveLevel = this.m_lastRecursiveLevel;
						grouping.SetGroupInstanceExpressionValues(dataRegionMemberInstance.GroupExprValues);
						if (mode != OnDemandMode.FullSetup || grouping == null || grouping.GroupExpressions == null || grouping.GroupExpressions.Count <= 0)
						{
							goto IL_070F;
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = grouping.GroupExpressions[0];
						if (expressionInfo.Type != Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field)
						{
							goto IL_070F;
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.Field field = memberDef.DataRegionDef.GetDataSet(reportDefinition).Fields[expressionInfo.IntValue];
						if (field.DataField == null)
						{
							goto IL_070F;
						}
						string dataField = field.DataField;
						object obj = dataRegionMemberInstance.GroupExprValues[0];
						PairObj<string, object> pairObj = this.m_specialLastGroupingValues[k];
						if (pairObj == null)
						{
							pairObj = new PairObj<string, object>(dataField, obj);
							this.m_specialLastGroupingValues[k] = pairObj;
							goto IL_070F;
						}
						pairObj.First = dataField;
						pairObj.Second = obj;
						goto IL_070F;
						IL_0250:
						if (scopeInstance is Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance && (scopeInstance.DataRegionInstances == null || scopeInstance.DataRegionInstances.Count <= instancePathItem.IndexInCollection || scopeInstance.DataRegionInstances[instancePathItem.IndexInCollection] == null || scopeInstance.DataRegionInstances[instancePathItem.IndexInCollection].Value() == null))
						{
							Global.Tracer.Assert(instancePathItem.IndexInCollection < reportDefinition.TopLevelDataRegions.Count, "(newItem.IndexInCollection < m_reportDefinition.TopLevelDataRegions.Count)");
							Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = reportDefinition.TopLevelDataRegions[instancePathItem.IndexInCollection].GetDataSet(reportDefinition);
							if (mode == OnDemandMode.InScope && this.InScopeCompare(dataSet.Name, scopeName))
							{
								return;
							}
							this.PerformOnDemandTablixProcessing(dataSet);
						}
						scopeInstance = scopeInstance.DataRegionInstances[instancePathItem.IndexInCollection].Value();
						flag = (this.m_inRecursiveColumnHierarchy = false);
						flag2 = (this.m_inRecursiveRowHierarchy = false);
						num = -1;
						dataRegionInstance = scopeInstance as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegionInstance;
						memberHierarchy = dataRegionInstance;
						if (mode == OnDemandMode.InScope && this.InScopeCompare(dataRegionInstance.DataRegionDef.Name, scopeName))
						{
							return;
						}
						if (dataRegionInstance.DataSetIndexInCollection >= 0 && this.m_odpContext.CurrentDataSetIndex != dataRegionInstance.DataSetIndexInCollection && mode != OnDemandMode.InScope)
						{
							if (!flag4)
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance = this.m_odpContext.CurrentReportInstance.GetDataSetInstance(dataRegionInstance.DataSetIndexInCollection, this.m_odpContext);
								if (dataSetInstance != null)
								{
									dataSetInstance.SetupEnvironment(this.m_odpContext, true);
									i = 0;
								}
							}
							else
							{
								i = k + 1;
							}
						}
						if (mode == OnDemandMode.InScope)
						{
							goto IL_0486;
						}
						if (flag4)
						{
							i = k + 1;
							goto IL_0486;
						}
						dataRegionInstance.SetupEnvironment(this.m_odpContext);
						i = 0;
						if (!dataRegionInstance.NoRows)
						{
							dataRegionInstance.DataRegionDef.NoRows = false;
							goto IL_0486;
						}
						dataRegionInstance.DataRegionDef.NoRows = true;
						dataRegionInstance.DataRegionDef.ResetTopLevelDynamicMemberInstanceCount();
						return;
					}
					if (mode == OnDemandMode.FullSetup && !flag3 && scopeInstance != null && i > 0)
					{
						while (i < this.m_lastInstancePath.Count)
						{
							if (this.m_lastInstancePath[i].IsScope)
							{
								scopeInstance.SetupFields(this.m_odpContext, this.m_odpContext.CurrentDataSetIndex);
								break;
							}
							i++;
						}
					}
					if (mode == OnDemandMode.FullSetup && !this.m_odpContext.IsTablixProcessingMode && this.m_odpContext.CurrentReportInstance != null && dataRegionInstance == null && reportDefinition.DataSetsNotOnlyUsedInParameters == 1)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.DataSet firstDataSet = reportDefinition.FirstDataSet;
						Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance2 = this.m_odpContext.CurrentReportInstance.GetDataSetInstance(firstDataSet, this.m_odpContext);
						if (dataSetInstance2 != null)
						{
							bool flag5 = true;
							if (!this.m_odpContext.IsTablixProcessingComplete(firstDataSet.IndexInCollection))
							{
								this.PerformOnDemandTablixProcessing(firstDataSet);
								flag5 = false;
							}
							if (this.m_odpContext.CurrentOdpDataSetInstance == dataSetInstance2)
							{
								flag5 = false;
							}
							if (flag5)
							{
								dataSetInstance2.SetupEnvironment(this.m_odpContext, true);
							}
							else if (!dataSetInstance2.NoRows)
							{
								dataSetInstance2.SetupFields(this.m_odpContext, dataSetInstance2);
							}
						}
					}
				}
			}
			finally
			{
				if (needDeepCopyPath)
				{
					InstancePathItem.DeepCopyPath(list, ref this.m_lastInstancePath);
				}
			}
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x001E2D80 File Offset: 0x001E0F80
		private void PerformOnDemandTablixProcessing(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			Merge.TablixDataProcessing(this.m_odpContext, dataSet);
			this.m_odpContext.ReportObjectModel.ResetFieldValues();
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x001E2DA0 File Offset: 0x001E0FA0
		private void PerformOnDemandTablixProcessingWithContextRestore(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			Global.Tracer.Assert(!this.m_odpContext.IsTablixProcessingMode, "Nested calls of tablix data processing are not supported");
			IInstancePath lastRIFObject = this.m_lastRIFObject;
			this.PerformOnDemandTablixProcessing(dataSet);
			this.RestoreContext(lastRIFObject);
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x001E2DE0 File Offset: 0x001E0FE0
		internal override IRecordRowReader CreateSequentialDataReader(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance currentReportInstance = this.m_odpContext.CurrentReportInstance;
			dataSetInstance = currentReportInstance.GetDataSetInstance(dataSet, this.m_odpContext);
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader dataChunkReader = null;
			if (!dataSetInstance.NoRows)
			{
				dataChunkReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader(dataSetInstance, this.m_odpContext, dataSetInstance.DataChunkName);
				base.RegisterDisposableDataReaderOrIdcDataManager(dataChunkReader);
			}
			return dataChunkReader;
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x001E2E30 File Offset: 0x001E1030
		internal override void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			Global.Tracer.Assert(false, "This method is not valid for this StateManager type.");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x001E2E4C File Offset: 0x001E104C
		internal override bool ShouldStopPipelineAdvance(bool rowAccepted)
		{
			return true;
		}

		// Token: 0x06007492 RID: 29842 RVA: 0x001E2E4F File Offset: 0x001E104F
		internal override void CreatedScopeInstance(IRIFReportDataScope scope)
		{
		}

		// Token: 0x06007493 RID: 29843 RVA: 0x001E2E51 File Offset: 0x001E1051
		internal override bool ProcessOneRow(IRIFReportDataScope scope)
		{
			Global.Tracer.Assert(false, "This method is not valid for this StateManager type.");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x06007494 RID: 29844 RVA: 0x001E2E6D File Offset: 0x001E106D
		internal override bool CheckForPrematureServerAggregate(string aggregateName)
		{
			return false;
		}

		// Token: 0x04003B4B RID: 15179
		private IReportScopeInstance m_lastROMInstance;

		// Token: 0x04003B4C RID: 15180
		private IInstancePath m_lastRIFObject;

		// Token: 0x04003B4D RID: 15181
		private IRIFReportScope m_lastTablixProcessingReportScope;

		// Token: 0x04003B4E RID: 15182
		private List<InstancePathItem> m_lastInstancePath;

		// Token: 0x04003B4F RID: 15183
		private readonly List<PairObj<string, object>> m_specialLastGroupingValues = new List<PairObj<string, object>>();

		// Token: 0x04003B50 RID: 15184
		private bool m_lastInScopeResult;

		// Token: 0x04003B51 RID: 15185
		private int m_lastRecursiveLevel;

		// Token: 0x04003B52 RID: 15186
		private bool m_inRecursiveRowHierarchy;

		// Token: 0x04003B53 RID: 15187
		private bool m_inRecursiveColumnHierarchy;
	}
}
