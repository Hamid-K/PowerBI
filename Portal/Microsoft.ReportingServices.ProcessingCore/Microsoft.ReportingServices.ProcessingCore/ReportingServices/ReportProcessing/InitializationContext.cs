using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006CB RID: 1739
	public struct InitializationContext
	{
		// Token: 0x06005D0F RID: 23823 RVA: 0x0017A68C File Offset: 0x0017888C
		internal InitializationContext(ICatalogItemContext reportContext, bool hasFilters, StringDictionary dataSources, DataSetList dataSets, ArrayList dynamicParameters, Hashtable dataSetQueryInfo, ErrorContext errorContext, ExprHostBuilder exprHostBuilder, Report report, CultureInfo reportLanguage, Hashtable reportScopes, bool hasUserSortPeerScopes, int dataRegionCount)
		{
			Global.Tracer.Assert(dataSets != null, "(null != dataSets)");
			Global.Tracer.Assert(errorContext != null, "(null != errorContext)");
			this.m_reportContext = reportContext;
			this.m_location = LocationFlags.None;
			this.m_objectType = ObjectType.Report;
			this.m_objectName = null;
			this.m_detailObjectType = ObjectType.Report;
			this.m_matrixName = null;
			this.m_embeddedImages = report.EmbeddedImages;
			this.m_imageStreamNames = report.ImageStreamNames;
			this.m_errorContext = errorContext;
			this.m_parameters = null;
			this.m_dynamicParameters = dynamicParameters;
			this.m_dataSetQueryInfo = dataSetQueryInfo;
			this.m_registerReceiver = true;
			this.m_exprHostBuilder = exprHostBuilder;
			this.m_dataSources = dataSources;
			this.m_report = report;
			this.m_aggregateEscalateScopes = null;
			this.m_aggregateRewriteScopes = null;
			this.m_aggregateRewriteMap = null;
			this.m_reportLanguage = reportLanguage;
			this.m_outerGroupName = null;
			this.m_currentGroupName = null;
			this.m_currentDataregionName = null;
			this.m_runningValues = null;
			this.m_groupingScopesForRunningValues = new Hashtable();
			this.m_groupingScopesForRunningValuesInTablix = null;
			this.m_dataregionScopesForRunningValues = new Hashtable();
			this.m_hasFilters = hasFilters;
			this.m_currentScope = null;
			this.m_outermostDataregionScope = null;
			this.m_groupingScopes = new Hashtable();
			this.m_dataregionScopes = new Hashtable();
			this.m_datasetScopes = new Hashtable();
			for (int i = 0; i < dataSets.Count; i++)
			{
				this.m_datasetScopes[dataSets[i].Name] = new InitializationContext.ScopeInfo(true, dataSets[i].Aggregates, dataSets[i].PostSortAggregates, dataSets[i]);
			}
			this.m_numberOfDataSets = dataSets.Count;
			this.m_oneDataSetName = ((1 == dataSets.Count) ? dataSets[0].Name : null);
			this.m_currentDataSetName = null;
			this.m_fieldNameMap = new Hashtable();
			this.m_dataSetNameToDataRegionsMap = new Hashtable();
			bool flag = false;
			if (this.m_dynamicParameters != null && this.m_dynamicParameters.Count > 0)
			{
				flag = true;
			}
			for (int j = 0; j < dataSets.Count; j++)
			{
				DataSet dataSet = dataSets[j];
				Global.Tracer.Assert(dataSet != null, "(null != dataSet)");
				Global.Tracer.Assert(dataSet.Query != null, "(null != dataSet.Query)");
				bool flag2 = false;
				if (report.DataSources != null)
				{
					for (int k = 0; k < report.DataSources.Count; k++)
					{
						if (dataSet.Query.DataSourceName == report.DataSources[k].Name)
						{
							flag2 = true;
							if (report.DataSources[k].DataSets == null)
							{
								report.DataSources[k].DataSets = new DataSetList();
							}
							if (flag)
							{
								YukonDataSetInfo yukonDataSetInfo = (YukonDataSetInfo)dataSetQueryInfo[dataSet.Name];
								Global.Tracer.Assert(yukonDataSetInfo != null, "(null != dataSetInfo)");
								yukonDataSetInfo.DataSourceIndex = k;
								yukonDataSetInfo.DataSetIndex = report.DataSources[k].DataSets.Count;
								yukonDataSetInfo.MergeFlagsFromDataSource(report.DataSources[k].IsComplex, report.DataSources[k].ParameterNames);
							}
							report.DataSources[k].DataSets.Add(dataSet);
							break;
						}
					}
				}
				if (!flag2)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDataSourceReference, Severity.Error, dataSet.ObjectType, dataSet.Name, "DataSourceName", new string[] { dataSet.Query.DataSourceName });
				}
				Hashtable hashtable = new Hashtable();
				if (dataSet.Fields != null)
				{
					for (int l = 0; l < dataSet.Fields.Count; l++)
					{
						hashtable[dataSet.Fields[l].Name] = l;
					}
				}
				this.m_fieldNameMap[dataSet.Name] = hashtable;
				this.m_dataSetNameToDataRegionsMap[dataSet.Name] = dataSet.DataRegions;
			}
			if (report.Parameters != null)
			{
				this.m_parameters = new Hashtable();
				for (int m = 0; m < report.Parameters.Count; m++)
				{
					ParameterDef parameterDef = report.Parameters[m];
					if (parameterDef != null)
					{
						try
						{
							this.m_parameters.Add(parameterDef.Name, parameterDef);
						}
						catch
						{
						}
					}
				}
			}
			this.m_reportItemsInScope = new Hashtable();
			this.m_toggleItemInfos = new Hashtable();
			this.m_reportDataElementStyleAttribute = true;
			this.m_tableColumnVisible = true;
			this.m_hasUserSortPeerScopes = hasUserSortPeerScopes;
			this.m_userSortExpressionScopes = new Hashtable();
			this.m_userSortTextboxes = new Hashtable();
			this.m_peerScopes = (hasUserSortPeerScopes ? new Hashtable() : null);
			this.m_lastPeerScopeId = 0;
			this.m_reportScopes = reportScopes;
			this.m_reportScopeDatasetIDs = new Hashtable();
			this.m_groupingList = new GroupingList();
			this.m_scopesInMatrixCells = new Hashtable();
			this.m_parentMatrixList = new StringList();
			this.m_reportSortFilterTextboxes = new TextBoxList();
			this.m_detailSortExpressionScopeTextboxes = new TextBoxList();
			this.m_reportGroupingLists = new Hashtable();
			this.m_reportGroupingLists.Add("0_ReportScope", this.m_groupingList.Clone());
			this.m_dataRegionCount = dataRegionCount;
		}

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x0017ABE0 File Offset: 0x00178DE0
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x06005D11 RID: 23825 RVA: 0x0017ABE8 File Offset: 0x00178DE8
		// (set) Token: 0x06005D12 RID: 23826 RVA: 0x0017ABF0 File Offset: 0x00178DF0
		internal LocationFlags Location
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x0017ABF9 File Offset: 0x00178DF9
		// (set) Token: 0x06005D14 RID: 23828 RVA: 0x0017AC01 File Offset: 0x00178E01
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
			set
			{
				this.m_objectType = value;
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x06005D15 RID: 23829 RVA: 0x0017AC0A File Offset: 0x00178E0A
		// (set) Token: 0x06005D16 RID: 23830 RVA: 0x0017AC12 File Offset: 0x00178E12
		internal string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			set
			{
				this.m_objectName = value;
			}
		}

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x06005D17 RID: 23831 RVA: 0x0017AC1B File Offset: 0x00178E1B
		// (set) Token: 0x06005D18 RID: 23832 RVA: 0x0017AC23 File Offset: 0x00178E23
		internal bool ReportDataElementStyleAttribute
		{
			get
			{
				return this.m_reportDataElementStyleAttribute;
			}
			set
			{
				this.m_reportDataElementStyleAttribute = value;
			}
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x0017AC2C File Offset: 0x00178E2C
		// (set) Token: 0x06005D1A RID: 23834 RVA: 0x0017AC34 File Offset: 0x00178E34
		internal bool TableColumnVisible
		{
			get
			{
				return this.m_tableColumnVisible;
			}
			set
			{
				this.m_tableColumnVisible = value;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (set) Token: 0x06005D1B RID: 23835 RVA: 0x0017AC3D File Offset: 0x00178E3D
		internal ObjectType DetailObjectType
		{
			set
			{
				this.m_detailObjectType = value;
			}
		}

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x06005D1C RID: 23836 RVA: 0x0017AC46 File Offset: 0x00178E46
		// (set) Token: 0x06005D1D RID: 23837 RVA: 0x0017AC4E File Offset: 0x00178E4E
		internal string MatrixName
		{
			get
			{
				return this.m_matrixName;
			}
			set
			{
				this.m_matrixName = value;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x06005D1E RID: 23838 RVA: 0x0017AC57 File Offset: 0x00178E57
		internal EmbeddedImageHashtable EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x06005D1F RID: 23839 RVA: 0x0017AC5F File Offset: 0x00178E5F
		internal ImageStreamNames ImageStreamNames
		{
			get
			{
				return this.m_imageStreamNames;
			}
		}

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x06005D20 RID: 23840 RVA: 0x0017AC67 File Offset: 0x00178E67
		internal ErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x06005D21 RID: 23841 RVA: 0x0017AC6F File Offset: 0x00178E6F
		// (set) Token: 0x06005D22 RID: 23842 RVA: 0x0017AC77 File Offset: 0x00178E77
		internal bool RegisterHiddenReceiver
		{
			get
			{
				return this.m_registerReceiver;
			}
			set
			{
				this.m_registerReceiver = value;
			}
		}

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x0017AC80 File Offset: 0x00178E80
		internal ExprHostBuilder ExprHostBuilder
		{
			get
			{
				return this.m_exprHostBuilder;
			}
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x0017AC88 File Offset: 0x00178E88
		internal bool MergeOnePass
		{
			get
			{
				return this.m_report.MergeOnePass;
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x06005D25 RID: 23845 RVA: 0x0017AC95 File Offset: 0x00178E95
		internal int DataRegionCount
		{
			get
			{
				return this.m_dataRegionCount;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x06005D26 RID: 23846 RVA: 0x0017AC9D File Offset: 0x00178E9D
		internal CultureInfo ReportLanguage
		{
			get
			{
				return this.m_reportLanguage;
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x06005D27 RID: 23847 RVA: 0x0017ACA5 File Offset: 0x00178EA5
		// (set) Token: 0x06005D28 RID: 23848 RVA: 0x0017ACAD File Offset: 0x00178EAD
		internal StringList AggregateEscalateScopes
		{
			get
			{
				return this.m_aggregateEscalateScopes;
			}
			set
			{
				this.m_aggregateEscalateScopes = value;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x06005D29 RID: 23849 RVA: 0x0017ACB6 File Offset: 0x00178EB6
		// (set) Token: 0x06005D2A RID: 23850 RVA: 0x0017ACBE File Offset: 0x00178EBE
		internal Hashtable AggregateRewriteScopes
		{
			get
			{
				return this.m_aggregateRewriteScopes;
			}
			set
			{
				this.m_aggregateRewriteScopes = value;
			}
		}

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x06005D2B RID: 23851 RVA: 0x0017ACC7 File Offset: 0x00178EC7
		// (set) Token: 0x06005D2C RID: 23852 RVA: 0x0017ACCF File Offset: 0x00178ECF
		internal Hashtable AggregateRewriteMap
		{
			get
			{
				return this.m_aggregateRewriteMap;
			}
			set
			{
				this.m_aggregateRewriteMap = value;
			}
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x0017ACD8 File Offset: 0x00178ED8
		private void RegisterDataSetScope(string scopeName, DataAggregateInfoList scopeAggregates, DataAggregateInfoList scopePostSortAggregates)
		{
			Global.Tracer.Assert(scopeName != null, "(null != scopeName)");
			Global.Tracer.Assert(scopeAggregates != null, "(null != scopeAggregates)");
			Global.Tracer.Assert(scopePostSortAggregates != null, "(null != scopePostSortAggregates)");
			this.m_currentScope = new InitializationContext.ScopeInfo(true, scopeAggregates, scopePostSortAggregates);
			if (!this.m_reportGroupingLists.ContainsKey(scopeName))
			{
				this.m_reportGroupingLists.Add(scopeName, this.GetGroupingList());
				this.m_reportScopeDatasetIDs.Add(scopeName, this.GetDataSetID());
			}
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x0017AD63 File Offset: 0x00178F63
		private void UnRegisterDataSetScope(string scopeName)
		{
			Global.Tracer.Assert(scopeName != null, "(null != scopeName)");
			this.m_currentScope = null;
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x0017AD80 File Offset: 0x00178F80
		private void RegisterDataRegionScope(DataRegion dataRegion)
		{
			Global.Tracer.Assert(dataRegion.Name != null, "(null != dataRegion.Name)");
			Global.Tracer.Assert(dataRegion.Aggregates != null, "(null != dataRegion.Aggregates)");
			Global.Tracer.Assert(dataRegion.PostSortAggregates != null, "(null != dataRegion.PostSortAggregates)");
			this.m_currentDataregionName = dataRegion.Name;
			this.m_dataregionScopesForRunningValues[dataRegion.Name] = this.m_currentGroupName;
			InitializationContext.ScopeInfo scopeInfo = new InitializationContext.ScopeInfo(this.m_currentScope == null || this.m_currentScope.AllowCustomAggregates, dataRegion.Aggregates, dataRegion.PostSortAggregates, dataRegion);
			this.m_currentScope = scopeInfo;
			if ((this.m_location & LocationFlags.InDataRegion) == (LocationFlags)0)
			{
				this.m_outermostDataregionScope = scopeInfo;
			}
			this.m_dataregionScopes[dataRegion.Name] = scopeInfo;
			if (dataRegion is Matrix)
			{
				this.m_parentMatrixList.Add(dataRegion.Name);
			}
			if (!this.m_reportGroupingLists.ContainsKey(dataRegion.Name))
			{
				this.m_reportGroupingLists.Add(dataRegion.Name, this.GetGroupingList());
				this.m_reportScopeDatasetIDs.Add(dataRegion.Name, this.GetDataSetID());
			}
			if ((LocationFlags)0 < (this.m_location & LocationFlags.InMatrixCell))
			{
				this.RegisterScopeInMatrixCell(this.m_matrixName, dataRegion.Name, false);
			}
			this.ProcessUserSortInnerScope(dataRegion.Name, false, false);
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x0017AED8 File Offset: 0x001790D8
		private void UnRegisterDataRegionScope(string scopeName)
		{
			Global.Tracer.Assert(scopeName != null, "(null != scopeName)");
			this.m_currentDataregionName = null;
			this.m_dataregionScopesForRunningValues.Remove(scopeName);
			this.m_currentScope = null;
			if ((this.m_location & LocationFlags.InDataRegion) == (LocationFlags)0)
			{
				this.m_outermostDataregionScope = null;
			}
			this.m_dataregionScopes.Remove(scopeName);
			int count = this.m_parentMatrixList.Count;
			if (0 < count && ReportProcessing.CompareWithInvariantCulture(this.m_parentMatrixList[count - 1], scopeName, false) == 0)
			{
				this.m_parentMatrixList.RemoveAt(count - 1);
			}
			this.ValidateUserSortInnerScope(scopeName);
			this.TextboxesWithDetailSortExpressionInitialize();
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x0017AF71 File Offset: 0x00179171
		internal void RegisterGroupingScope(string scopeName, bool simpleGroupExpressions, DataAggregateInfoList scopeAggregates, DataAggregateInfoList scopePostSortAggregates, DataAggregateInfoList scopeRecursiveAggregates, Grouping groupingScope)
		{
			this.RegisterGroupingScope(scopeName, simpleGroupExpressions, scopeAggregates, scopePostSortAggregates, scopeRecursiveAggregates, groupingScope, false);
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x0017AF84 File Offset: 0x00179184
		internal void RegisterGroupingScope(string scopeName, bool simpleGroupExpressions, DataAggregateInfoList scopeAggregates, DataAggregateInfoList scopePostSortAggregates, DataAggregateInfoList scopeRecursiveAggregates, Grouping groupingScope, bool isMatrixGrouping)
		{
			Global.Tracer.Assert(scopeName != null);
			Global.Tracer.Assert(scopeAggregates != null);
			Global.Tracer.Assert(scopePostSortAggregates != null);
			Global.Tracer.Assert(scopeRecursiveAggregates != null);
			this.m_outerGroupName = this.m_currentGroupName;
			this.m_currentGroupName = scopeName;
			this.m_groupingScopesForRunningValues[scopeName] = null;
			InitializationContext.ScopeInfo scopeInfo = new InitializationContext.ScopeInfo((this.m_currentScope == null) ? simpleGroupExpressions : (simpleGroupExpressions && this.m_currentScope.AllowCustomAggregates), scopeAggregates, scopePostSortAggregates, scopeRecursiveAggregates, groupingScope);
			this.m_currentScope = scopeInfo;
			this.m_groupingScopes[scopeName] = scopeInfo;
			this.m_groupingList.Add(groupingScope);
			if (!this.m_reportGroupingLists.ContainsKey(scopeName))
			{
				this.m_reportGroupingLists.Add(scopeName, this.GetGroupingList());
				this.m_reportScopeDatasetIDs.Add(scopeName, this.GetDataSetID());
				if ((LocationFlags)0 < (this.m_location & LocationFlags.InMatrixCell))
				{
					this.RegisterScopeInMatrixCell(this.m_matrixName, scopeName, false);
				}
			}
			if (!isMatrixGrouping)
			{
				this.ProcessUserSortInnerScope(scopeName, false, false);
			}
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0017B094 File Offset: 0x00179294
		internal void UnRegisterGroupingScope(string scopeName)
		{
			this.UnRegisterGroupingScope(scopeName, false);
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x0017B0A0 File Offset: 0x001792A0
		internal void UnRegisterGroupingScope(string scopeName, bool isMatrixGrouping)
		{
			Global.Tracer.Assert(scopeName != null);
			this.m_outerGroupName = null;
			this.m_currentGroupName = null;
			this.m_groupingScopesForRunningValues.Remove(scopeName);
			this.m_currentScope = null;
			this.m_groupingScopes.Remove(scopeName);
			Global.Tracer.Assert(0 < this.m_groupingList.Count, "(0 < m_groupingList.Count)");
			this.m_groupingList.RemoveAt(this.m_groupingList.Count - 1);
			if (!isMatrixGrouping)
			{
				this.ValidateUserSortInnerScope(scopeName);
				this.TextboxesWithDetailSortExpressionInitialize();
			}
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x0017B130 File Offset: 0x00179330
		internal void ValidateHideDuplicateScope(string hideDuplicateScope, ReportItem reportItem)
		{
			if (hideDuplicateScope == null)
			{
				return;
			}
			bool flag = true;
			InitializationContext.ScopeInfo scopeInfo = null;
			if ((this.m_location & LocationFlags.InDetail) == (LocationFlags)0 && hideDuplicateScope.Equals(this.m_currentGroupName))
			{
				flag = false;
			}
			else if (this.m_groupingScopes.Contains(hideDuplicateScope))
			{
				scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[hideDuplicateScope];
			}
			else if (!this.m_datasetScopes.ContainsKey(hideDuplicateScope))
			{
				flag = false;
			}
			if (flag)
			{
				if (scopeInfo != null)
				{
					Global.Tracer.Assert(scopeInfo.GroupingScope != null, "(null != scope.GroupingScope)");
					scopeInfo.GroupingScope.AddReportItemWithHideDuplicates(reportItem);
					return;
				}
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidHideDuplicateScope, Severity.Error, this.m_objectType, this.m_objectName, "HideDuplicates", new string[] { hideDuplicateScope });
			}
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x0017B1EC File Offset: 0x001793EC
		internal void RegisterGroupingScopeForTablixCell(string scopeName, bool column, bool simpleGroupExpressions, DataAggregateInfoList scopeAggregates, DataAggregateInfoList scopePostSortAggregates, DataAggregateInfoList scopeRecursiveAggregates, Grouping groupingScope)
		{
			Global.Tracer.Assert(scopeName != null);
			Global.Tracer.Assert(scopeAggregates != null);
			Global.Tracer.Assert(scopePostSortAggregates != null);
			Global.Tracer.Assert(scopeRecursiveAggregates != null);
			if (column)
			{
				this.m_groupingScopesForRunningValuesInTablix.RegisterColumnGrouping(scopeName);
			}
			else
			{
				this.m_groupingScopesForRunningValuesInTablix.RegisterRowGrouping(scopeName);
			}
			InitializationContext.ScopeInfo scopeInfo = new InitializationContext.ScopeInfo((this.m_currentScope == null) ? simpleGroupExpressions : (simpleGroupExpressions && this.m_currentScope.AllowCustomAggregates), scopeAggregates, scopePostSortAggregates, scopeRecursiveAggregates, groupingScope);
			this.m_groupingScopes[scopeName] = scopeInfo;
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x0017B288 File Offset: 0x00179488
		internal void UnRegisterGroupingScopeForTablixCell(string scopeName, bool column)
		{
			Global.Tracer.Assert(scopeName != null);
			if (column)
			{
				this.m_groupingScopesForRunningValuesInTablix.UnRegisterColumnGrouping(scopeName);
			}
			else
			{
				this.m_groupingScopesForRunningValuesInTablix.UnRegisterRowGrouping(scopeName);
			}
			this.m_groupingScopes.Remove(scopeName);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x0017B2C4 File Offset: 0x001794C4
		internal void RegisterTablixCellScope(bool forceRows, DataAggregateInfoList scopeAggregates, DataAggregateInfoList scopePostSortAggregates)
		{
			Global.Tracer.Assert(scopeAggregates != null);
			this.m_groupingScopesForRunningValues = new Hashtable();
			this.m_groupingScopesForRunningValuesInTablix = new InitializationContext.GroupingScopesForTablix(forceRows, this.m_objectType, this.m_objectName);
			this.m_dataregionScopesForRunningValues = new Hashtable();
			this.m_currentScope = new InitializationContext.ScopeInfo(this.m_currentScope == null || this.m_currentScope.AllowCustomAggregates, scopeAggregates, scopePostSortAggregates);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x0017B330 File Offset: 0x00179530
		internal void UnRegisterTablixCellScope()
		{
			this.m_groupingScopesForRunningValues = null;
			this.m_groupingScopesForRunningValuesInTablix = null;
			this.m_dataregionScopesForRunningValues = null;
			this.m_currentScope = null;
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0017B34E File Offset: 0x0017954E
		internal void RegisterPageSectionScope(DataAggregateInfoList scopeAggregates)
		{
			Global.Tracer.Assert(scopeAggregates != null);
			this.m_currentScope = new InitializationContext.ScopeInfo(false, scopeAggregates);
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0017B36B File Offset: 0x0017956B
		internal void UnRegisterPageSectionScope()
		{
			this.m_currentScope = null;
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x0017B374 File Offset: 0x00179574
		internal void RegisterRunningValues(RunningValueInfoList runningValues)
		{
			Global.Tracer.Assert(runningValues != null);
			this.m_runningValues = runningValues;
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x0017B38B File Offset: 0x0017958B
		internal void UnRegisterRunningValues(RunningValueInfoList runningValues)
		{
			Global.Tracer.Assert(runningValues != null);
			Global.Tracer.Assert(this.m_runningValues != null);
			Global.Tracer.Assert(this.m_runningValues == runningValues);
			this.m_runningValues = null;
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x0017B3C8 File Offset: 0x001795C8
		internal void TransferGroupExpressionRowNumbers(RunningValueInfoList rowNumbers)
		{
			if (rowNumbers == null)
			{
				return;
			}
			for (int i = rowNumbers.Count - 1; i >= 0; i--)
			{
				Global.Tracer.Assert((this.m_location & LocationFlags.InGrouping) > (LocationFlags)0);
				RunningValueInfo runningValueInfo = rowNumbers[i];
				Global.Tracer.Assert(runningValueInfo != null);
				string scope = runningValueInfo.Scope;
				bool flag = true;
				InitializationContext.ScopeInfo scopeInfo = null;
				if ((this.m_location & LocationFlags.InMatrixCell) != (LocationFlags)0)
				{
					flag = false;
				}
				else if (scope == null)
				{
					if (this.m_outerGroupName != null)
					{
						flag = false;
					}
					else
					{
						scopeInfo = this.m_outermostDataregionScope;
					}
				}
				else if (this.m_outerGroupName == scope)
				{
					Global.Tracer.Assert(this.m_outerGroupName != null, "(null != m_outerGroupName)");
					scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_outerGroupName];
				}
				else if (this.m_currentDataregionName == scope)
				{
					Global.Tracer.Assert(this.m_currentDataregionName != null, "(null != m_currentDataregionName)");
					scopeInfo = (InitializationContext.ScopeInfo)this.m_dataregionScopes[this.m_currentDataregionName];
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidGroupExpressionScope, Severity.Error, this.m_objectType, this.m_objectName, "GroupExpression", Array.Empty<string>());
				}
				else if (scopeInfo != null)
				{
					Global.Tracer.Assert(scopeInfo.Aggregates != null, "(null != destinationScope.Aggregates)");
					scopeInfo.Aggregates.Add(runningValueInfo);
				}
				rowNumbers.RemoveAt(i);
			}
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x0017B538 File Offset: 0x00179738
		internal bool IsRunningValueDirectionColumn()
		{
			return this.m_groupingScopesForRunningValuesInTablix.IsRunningValueDirectionColumn();
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x0017B545 File Offset: 0x00179745
		internal void TransferRunningValues(RunningValueInfoList runningValues, string propertyName)
		{
			this.TransferRunningValues(runningValues, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x0017B55C File Offset: 0x0017975C
		internal void TransferRunningValues(RunningValueInfoList runningValues, ObjectType objectType, string objectName, string propertyName)
		{
			if (runningValues == null)
			{
				return;
			}
			if ((this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return;
			}
			for (int i = runningValues.Count - 1; i >= 0; i--)
			{
				RunningValueInfo runningValueInfo = runningValues[i];
				Global.Tracer.Assert(runningValueInfo != null, "(null != runningValue)");
				string scope = runningValueInfo.Scope;
				bool flag = true;
				string text = null;
				DataAggregateInfoList dataAggregateInfoList = null;
				RunningValueInfoList runningValueInfoList = null;
				if (scope == null)
				{
					if ((this.m_location & LocationFlags.InDataRegion) == (LocationFlags)0)
					{
						flag = false;
					}
					else if ((this.m_location & LocationFlags.InMatrixCell) != (LocationFlags)0)
					{
						flag = false;
					}
					else if ((this.m_location & LocationFlags.InGrouping) != (LocationFlags)0 || (this.m_location & LocationFlags.InDetail) != (LocationFlags)0)
					{
						text = this.GetDataSetName();
						runningValueInfoList = this.m_runningValues;
					}
					else
					{
						text = this.GetDataSetName();
						if (text != null)
						{
							InitializationContext.ScopeInfo scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text];
							Global.Tracer.Assert(scopeInfo != null, "(null != destinationScope)");
							dataAggregateInfoList = scopeInfo.Aggregates;
						}
					}
				}
				else if (this.m_groupingScopesForRunningValuesInTablix != null && this.m_groupingScopesForRunningValuesInTablix.ContainsScope(scope, this.m_errorContext, true))
				{
					Global.Tracer.Assert((this.m_location & LocationFlags.InMatrixCell) > (LocationFlags)0);
					text = this.GetDataSetName();
					runningValueInfoList = this.m_runningValues;
				}
				else if (this.m_groupingScopesForRunningValues.ContainsKey(scope))
				{
					Global.Tracer.Assert((this.m_location & LocationFlags.InGrouping) > (LocationFlags)0, "(0 != (m_location & LocationFlags.InGrouping))");
					text = this.GetDataSetName();
					runningValueInfoList = this.m_runningValues;
				}
				else if (this.m_dataregionScopesForRunningValues.ContainsKey(scope))
				{
					Global.Tracer.Assert((this.m_location & LocationFlags.InDataRegion) > (LocationFlags)0, "(0 != (m_location & LocationFlags.InDataRegion))");
					runningValueInfo.Scope = (string)this.m_dataregionScopesForRunningValues[scope];
					if ((this.m_location & LocationFlags.InGrouping) != (LocationFlags)0 || (this.m_location & LocationFlags.InDetail) != (LocationFlags)0)
					{
						text = this.GetDataSetName();
						runningValueInfoList = this.m_runningValues;
					}
					else
					{
						text = this.GetDataSetName();
						if (text != null)
						{
							InitializationContext.ScopeInfo scopeInfo2 = (InitializationContext.ScopeInfo)this.m_datasetScopes[text];
							Global.Tracer.Assert(scopeInfo2 != null, "(null != destinationScope)");
							dataAggregateInfoList = scopeInfo2.Aggregates;
						}
					}
				}
				else if (this.m_datasetScopes.ContainsKey(scope))
				{
					if (((this.m_location & LocationFlags.InGrouping) != (LocationFlags)0 || (this.m_location & LocationFlags.InDetail) != (LocationFlags)0) && scope == this.GetDataSetName())
					{
						if ((this.m_location & LocationFlags.InMatrixCell) != (LocationFlags)0)
						{
							flag = false;
						}
						else
						{
							text = scope;
							runningValueInfo.Scope = null;
							runningValueInfoList = this.m_runningValues;
						}
					}
					else
					{
						text = scope;
						InitializationContext.ScopeInfo scopeInfo3 = (InitializationContext.ScopeInfo)this.m_datasetScopes[scope];
						Global.Tracer.Assert(scopeInfo3 != null, "(null != destinationScope)");
						dataAggregateInfoList = scopeInfo3.Aggregates;
					}
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					if (!runningValueInfo.SuppressExceptions)
					{
						if ((this.m_location & LocationFlags.InMatrixCell) != (LocationFlags)0)
						{
							if (DataAggregateInfo.AggregateTypes.Previous == runningValueInfo.AggregateType)
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsInvalidPreviousAggregateInMatrixCell, Severity.Error, objectType, objectName, propertyName, new string[] { this.m_matrixName });
							}
							else
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsInvalidScopeInMatrix, Severity.Error, objectType, objectName, propertyName, new string[] { this.m_matrixName });
							}
						}
						else
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
						}
					}
				}
				else
				{
					if (runningValueInfo.Expressions != null)
					{
						for (int j = 0; j < runningValueInfo.Expressions.Length; j++)
						{
							Global.Tracer.Assert(runningValueInfo.Expressions[j] != null, "(null != runningValue.Expressions[j])");
							runningValueInfo.Expressions[j].AggregateInitialize(text, objectType, objectName, propertyName, this);
						}
					}
					if (dataAggregateInfoList != null)
					{
						dataAggregateInfoList.Add(runningValueInfo);
					}
					else if (runningValueInfoList != null)
					{
						Global.Tracer.Assert(runningValues != runningValueInfoList);
						runningValueInfoList.Add(runningValueInfo);
					}
				}
				runningValues.RemoveAt(i);
			}
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x0017B938 File Offset: 0x00179B38
		internal void SpecialTransferRunningValues(RunningValueInfoList runningValues)
		{
			if (runningValues == null)
			{
				return;
			}
			for (int i = runningValues.Count - 1; i >= 0; i--)
			{
				Global.Tracer.Assert(this.m_runningValues != null, "(null != m_runningValues)");
				Global.Tracer.Assert(runningValues != this.m_runningValues);
				this.m_runningValues.Add(runningValues[i]);
				runningValues.RemoveAt(i);
			}
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x0017B9A4 File Offset: 0x00179BA4
		internal void CopyRunningValues(RunningValueInfoList runningValues, DataAggregateInfoList tablixAggregates)
		{
			Global.Tracer.Assert(runningValues != null);
			Global.Tracer.Assert((this.m_location & LocationFlags.InMatrixCell) > (LocationFlags)0);
			Global.Tracer.Assert(tablixAggregates != null);
			Global.Tracer.Assert(this.m_groupingScopesForRunningValuesInTablix != null);
			for (int i = 0; i < runningValues.Count; i++)
			{
				RunningValueInfo runningValueInfo = runningValues[i];
				if (runningValueInfo.Scope != null && this.m_groupingScopesForRunningValuesInTablix.ContainsScope(runningValueInfo.Scope, this.m_errorContext, false))
				{
					tablixAggregates.Add(runningValueInfo);
				}
			}
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x0017BA39 File Offset: 0x00179C39
		internal void TransferAggregates(DataAggregateInfoList aggregates, string propertyName)
		{
			this.TransferAggregates(aggregates, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x0017BA50 File Offset: 0x00179C50
		internal void TransferAggregates(DataAggregateInfoList aggregates, ObjectType objectType, string objectName, string propertyName)
		{
			if (aggregates == null)
			{
				return;
			}
			for (int i = aggregates.Count - 1; i >= 0; i--)
			{
				DataAggregateInfo dataAggregateInfo = aggregates[i];
				Global.Tracer.Assert(dataAggregateInfo != null, "(null != aggregate)");
				if (this.m_hasFilters && DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType && !dataAggregateInfo.SuppressExceptions)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsCustomAggregateAndFilter, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				}
				string text;
				bool scope = dataAggregateInfo.GetScope(out text);
				bool flag = true;
				string text2 = null;
				InitializationContext.ScopeInfo scopeInfo = null;
				if ((this.m_location & LocationFlags.InPageSection) > (LocationFlags)0 && scope)
				{
					flag = false;
					if (!dataAggregateInfo.SuppressExceptions)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsScopeInPageSectionExpression, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
				}
				else if ((this.m_location & LocationFlags.InPageSection) == (LocationFlags)0 && this.m_numberOfDataSets == 0)
				{
					flag = false;
					if (!dataAggregateInfo.SuppressExceptions)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
				}
				else if (!scope)
				{
					text2 = this.GetDataSetName();
					if (LocationFlags.None == this.m_location)
					{
						if (1 != this.m_numberOfDataSets)
						{
							flag = false;
							if (!dataAggregateInfo.SuppressExceptions)
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsMissingAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
							}
						}
						else if (text2 != null)
						{
							scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text2];
						}
					}
					else
					{
						Global.Tracer.Assert((this.m_location & LocationFlags.InDataSet) != (LocationFlags)0 || (this.m_location & LocationFlags.InPageSection) > (LocationFlags)0);
						scopeInfo = this.m_currentScope;
					}
					if (scopeInfo != null && scopeInfo.DataSetScope != null)
					{
						scopeInfo.DataSetScope.UsedInAggregates = true;
					}
				}
				else if (text == null)
				{
					flag = false;
				}
				else if (this.m_groupingScopes.ContainsKey(text))
				{
					Global.Tracer.Assert((this.m_location & LocationFlags.InGrouping) > (LocationFlags)0, "(0 != (m_location & LocationFlags.InGrouping))");
					text2 = this.GetDataSetName();
					scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[text];
				}
				else if (this.m_dataregionScopes.ContainsKey(text))
				{
					Global.Tracer.Assert((this.m_location & LocationFlags.InDataRegion) > (LocationFlags)0, "(0 != (m_location & LocationFlags.InDataRegion))");
					text2 = this.GetDataSetName();
					scopeInfo = (InitializationContext.ScopeInfo)this.m_dataregionScopes[text];
				}
				else if (this.m_datasetScopes.ContainsKey(text))
				{
					text2 = text;
					scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text];
					scopeInfo.DataSetScope.UsedInAggregates = true;
				}
				else
				{
					flag = false;
					if (!dataAggregateInfo.SuppressExceptions)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
				}
				if (flag && scopeInfo != null)
				{
					if (DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType && !scopeInfo.AllowCustomAggregates && !dataAggregateInfo.SuppressExceptions)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidCustomAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
					if (dataAggregateInfo.Expressions != null)
					{
						for (int j = 0; j < dataAggregateInfo.Expressions.Length; j++)
						{
							Global.Tracer.Assert(dataAggregateInfo.Expressions[j] != null, "(null != aggregate.Expressions[j])");
							dataAggregateInfo.Expressions[j].AggregateInitialize(text2, objectType, objectName, propertyName, this);
						}
					}
					if (DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType)
					{
						DataSet dataSet = this.m_reportScopes[text2] as DataSet;
						if (dataSet != null && dataSet.InterpretSubtotalsAsDetailsIsAuto)
						{
							dataSet.InterpretSubtotalsAsDetails = false;
						}
					}
					DataAggregateInfoList dataAggregateInfoList;
					if (dataAggregateInfo.Recursive)
					{
						if (scopeInfo.GroupingScope == null || scopeInfo.GroupingScope.Parent == null)
						{
							dataAggregateInfoList = scopeInfo.Aggregates;
						}
						else
						{
							dataAggregateInfoList = scopeInfo.RecursiveAggregates;
						}
					}
					else if (scopeInfo.PostSortAggregates != null && dataAggregateInfo.IsPostSortAggregate())
					{
						dataAggregateInfoList = scopeInfo.PostSortAggregates;
					}
					else
					{
						dataAggregateInfoList = scopeInfo.Aggregates;
					}
					Global.Tracer.Assert(dataAggregateInfoList != null, "(null != destinationAggregates)");
					Global.Tracer.Assert(aggregates != dataAggregateInfoList);
					dataAggregateInfoList.Add(dataAggregateInfo);
				}
				aggregates.RemoveAt(i);
			}
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x0017BE50 File Offset: 0x0017A050
		internal string EscalateScope(string oldScope)
		{
			if (this.m_aggregateRewriteScopes != null && this.m_aggregateRewriteScopes.ContainsKey(oldScope))
			{
				Global.Tracer.Assert(this.m_aggregateEscalateScopes != null && 1 <= this.m_aggregateEscalateScopes.Count);
				return this.m_aggregateEscalateScopes[this.m_aggregateEscalateScopes.Count - 1];
			}
			return oldScope;
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x0017BEB4 File Offset: 0x0017A0B4
		internal void InitializeParameters(ParameterDefList parameters, DataSetList dataSetList)
		{
			if (this.m_dynamicParameters == null || this.m_dynamicParameters.Count == 0)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			int i = 0;
			for (int j = 0; j < this.m_dynamicParameters.Count; j++)
			{
				DynamicParameter dynamicParameter = (DynamicParameter)this.m_dynamicParameters[j];
				while (i < dynamicParameter.Index)
				{
					hashtable.Add(parameters[i].Name, i);
					i++;
				}
				this.InitializeParameter(parameters[dynamicParameter.Index], dynamicParameter, hashtable, dataSetList);
			}
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x0017BF48 File Offset: 0x0017A148
		private void InitializeParameter(ParameterDef parameter, DynamicParameter dynamicParameter, Hashtable dependencies, DataSetList dataSetList)
		{
			Global.Tracer.Assert(dynamicParameter != null, "(null != dynamicParameter)");
			bool isComplex = dynamicParameter.IsComplex;
			DataSetReference dataSetReference = dynamicParameter.ValidValueDataSet;
			if (dataSetReference != null)
			{
				this.InitializeParameterDataSource(parameter, dataSetReference, false, dependencies, ref isComplex, dataSetList);
			}
			dataSetReference = dynamicParameter.DefaultDataSet;
			if (dataSetReference != null)
			{
				this.InitializeParameterDataSource(parameter, dataSetReference, true, dependencies, ref isComplex, dataSetList);
			}
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x0017BFA4 File Offset: 0x0017A1A4
		private void InitializeParameterDataSource(ParameterDef parameter, DataSetReference dataSetRef, bool isDefault, Hashtable dependencies, ref bool isComplex, DataSetList dataSetList)
		{
			ParameterDataSource parameterDataSource = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			YukonDataSetInfo yukonDataSetInfo = (YukonDataSetInfo)this.m_dataSetQueryInfo[dataSetRef.DataSet];
			if (yukonDataSetInfo == null)
			{
				if (isDefault)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDefaultValueDataSetReference, Severity.Error, ObjectType.ReportParameter, parameter.Name, "DataSetReference", new string[] { dataSetRef.DataSet });
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidValidValuesDataSetReference, Severity.Error, ObjectType.ReportParameter, parameter.Name, "DataSetReference", new string[] { dataSetRef.DataSet });
				}
			}
			else
			{
				DataSet dataSet = dataSetList[yukonDataSetInfo.DataSetDefIndex];
				if (!dataSet.UsedInAggregates)
				{
					DataRegionList dataRegionList = (DataRegionList)this.m_dataSetNameToDataRegionsMap[dataSetRef.DataSet];
					if (dataRegionList == null || dataRegionList.Count == 0)
					{
						dataSet.UsedOnlyInParameters = true;
					}
				}
				parameterDataSource = new ParameterDataSource(yukonDataSetInfo.DataSourceIndex, yukonDataSetInfo.DataSetIndex);
				Hashtable hashtable = (Hashtable)this.m_fieldNameMap[dataSetRef.DataSet];
				if (hashtable != null)
				{
					if (hashtable.ContainsKey(dataSetRef.ValueAlias))
					{
						parameterDataSource.ValueFieldIndex = (int)hashtable[dataSetRef.ValueAlias];
						if (parameterDataSource.ValueFieldIndex >= yukonDataSetInfo.CalculatedFieldIndex)
						{
							flag3 = dataSet.Fields == null || parameterDataSource.ValueFieldIndex > dataSet.Fields.Count || !(dataSet.Fields[parameterDataSource.ValueFieldIndex].Value is ExpressionInfoExtended) || !((ExpressionInfoExtended)dataSet.Fields[parameterDataSource.ValueFieldIndex].Value).IsExtendedSimpleFieldReference;
						}
						flag = true;
					}
					if (dataSetRef.LabelAlias != null)
					{
						if (hashtable.ContainsKey(dataSetRef.LabelAlias))
						{
							parameterDataSource.LabelFieldIndex = (int)hashtable[dataSetRef.LabelAlias];
							if (parameterDataSource.LabelFieldIndex >= yukonDataSetInfo.CalculatedFieldIndex)
							{
								flag3 = dataSet.Fields == null || parameterDataSource.LabelFieldIndex > dataSet.Fields.Count || !(dataSet.Fields[parameterDataSource.LabelFieldIndex].Value is ExpressionInfoExtended) || !((ExpressionInfoExtended)dataSet.Fields[parameterDataSource.LabelFieldIndex].Value).IsExtendedSimpleFieldReference;
							}
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else if (dataSetRef.LabelAlias == null)
				{
					flag2 = true;
				}
				if (!flag)
				{
					this.ErrorContext.Register(ProcessingErrorCode.rsInvalidDataSetReferenceField, Severity.Error, ObjectType.ReportParameter, parameter.Name, "Field", new string[] { dataSetRef.ValueAlias, dataSetRef.DataSet });
				}
				if (!flag2)
				{
					this.ErrorContext.Register(ProcessingErrorCode.rsInvalidDataSetReferenceField, Severity.Error, ObjectType.ReportParameter, parameter.Name, "Field", new string[] { dataSetRef.LabelAlias, dataSetRef.DataSet });
				}
				if (!isComplex)
				{
					if (yukonDataSetInfo.IsComplex || flag3)
					{
						isComplex = true;
						parameter.Dependencies = (Hashtable)dependencies.Clone();
					}
					else if (yukonDataSetInfo.ParameterNames != null && yukonDataSetInfo.ParameterNames.Count != 0)
					{
						Hashtable hashtable2 = parameter.Dependencies;
						if (hashtable2 == null)
						{
							hashtable2 = new Hashtable();
							parameter.Dependencies = hashtable2;
						}
						for (int i = 0; i < yukonDataSetInfo.ParameterNames.Count; i++)
						{
							string text = yukonDataSetInfo.ParameterNames[i];
							if (dependencies.ContainsKey(text))
							{
								if (!hashtable2.ContainsKey(text))
								{
									hashtable2.Add(text, dependencies[text]);
								}
							}
							else
							{
								this.ErrorContext.Register(ProcessingErrorCode.rsInvalidReportParameterDependency, Severity.Error, ObjectType.ReportParameter, parameter.Name, "DataSetReference", new string[] { text });
							}
						}
					}
				}
			}
			if (isDefault)
			{
				parameter.DefaultDataSource = parameterDataSource;
				return;
			}
			parameter.ValidValuesDataSource = parameterDataSource;
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x0017C370 File Offset: 0x0017A570
		internal void MergeFieldPropertiesIntoDataset(ExpressionInfo expressionInfo)
		{
			if (expressionInfo.ReferencedFieldProperties == null && !expressionInfo.DynamicFieldReferences)
			{
				return;
			}
			string dataSetName = this.GetDataSetName();
			if (dataSetName == null)
			{
				return;
			}
			DataSet dataSet = this.m_reportScopes[dataSetName] as DataSet;
			if (dataSet == null)
			{
				return;
			}
			dataSet.MergeFieldProperties(expressionInfo);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x0017C3B8 File Offset: 0x0017A5B8
		internal void RegisterDataRegion(DataRegion dataRegion)
		{
			if (this.m_numberOfDataSets == 0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionWithoutDataSet, Severity.Error, this.m_objectType, this.m_objectName, null, Array.Empty<string>());
			}
			if ((this.m_location & LocationFlags.InDetail) != (LocationFlags)0 && ObjectType.List == this.m_detailObjectType)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInDetailList, Severity.Error, this.m_objectType, this.m_objectName, null, Array.Empty<string>());
			}
			if ((this.m_location & LocationFlags.InDataRegion) == (LocationFlags)0)
			{
				this.ValidateDataSetNameForTopLevelDataRegion(dataRegion.DataSetName);
				string dataSetName = this.GetDataSetName();
				if (dataSetName != null)
				{
					DataRegionList dataRegionList = (DataRegionList)this.m_dataSetNameToDataRegionsMap[dataSetName];
					Global.Tracer.Assert(dataRegionList != null, "(null != dataRegions)");
					dataRegionList.Add(dataRegion);
					InitializationContext.ScopeInfo scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[dataSetName];
					Global.Tracer.Assert(scopeInfo != null, "(null != dataSetScope)");
					this.RegisterDataSetScope(dataSetName, scopeInfo.Aggregates, scopeInfo.PostSortAggregates);
				}
			}
			this.RegisterDataRegionScope(dataRegion);
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x0017C4B0 File Offset: 0x0017A6B0
		internal void UnRegisterDataRegion(DataRegion dataRegion)
		{
			if ((this.m_location & LocationFlags.InDataRegion) == (LocationFlags)0)
			{
				string dataSetName = this.GetDataSetName();
				if (dataSetName != null)
				{
					this.UnRegisterDataSetScope(dataSetName);
				}
			}
			this.UnRegisterDataRegionScope(dataRegion.Name);
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x0017C4E4 File Offset: 0x0017A6E4
		internal void RegisterDataSet(DataSet dataSet)
		{
			this.m_currentDataSetName = dataSet.Name;
			this.RegisterDataSetScope(dataSet.Name, dataSet.Aggregates, dataSet.PostSortAggregates);
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x0017C50A File Offset: 0x0017A70A
		internal void UnRegisterDataSet(DataSet dataSet)
		{
			this.m_currentDataSetName = null;
			this.UnRegisterDataSetScope(dataSet.Name);
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x0017C520 File Offset: 0x0017A720
		private string GetDataSetName()
		{
			if (this.m_numberOfDataSets == 0)
			{
				return null;
			}
			if (1 == this.m_numberOfDataSets)
			{
				Global.Tracer.Assert(this.m_oneDataSetName != null);
				return this.m_oneDataSetName;
			}
			Global.Tracer.Assert(1 < this.m_numberOfDataSets);
			return this.m_currentDataSetName;
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x0017C574 File Offset: 0x0017A774
		private int GetDataSetID()
		{
			string dataSetName = this.GetDataSetName();
			if (dataSetName == null)
			{
				return -1;
			}
			ISortFilterScope sortFilterScope = this.m_reportScopes[dataSetName] as ISortFilterScope;
			if (sortFilterScope == null)
			{
				return -1;
			}
			return sortFilterScope.ID;
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0017C5AC File Offset: 0x0017A7AC
		private void ValidateDataSetNameForTopLevelDataRegion(string dataSetName)
		{
			bool flag = true;
			if (this.m_numberOfDataSets == 0)
			{
				flag = dataSetName == null;
			}
			else if (1 == this.m_numberOfDataSets)
			{
				if (dataSetName == null)
				{
					dataSetName = this.m_oneDataSetName;
					flag = true;
				}
				else
				{
					flag = this.m_fieldNameMap.ContainsKey(dataSetName);
				}
			}
			else
			{
				Global.Tracer.Assert(1 < this.m_numberOfDataSets);
				if (dataSetName == null)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsMissingDataSetName, Severity.Error, this.m_objectType, this.m_objectName, "DataSetName", Array.Empty<string>());
				}
				else
				{
					flag = this.m_fieldNameMap.ContainsKey(dataSetName);
				}
			}
			if (!flag)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDataSetName, Severity.Error, this.m_objectType, this.m_objectName, "DataSetName", new string[] { dataSetName });
				return;
			}
			this.m_currentDataSetName = dataSetName;
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x0017C673 File Offset: 0x0017A873
		internal void CheckFieldReferences(StringList fieldNames, string propertyName)
		{
			this.InternalCheckFieldReference(fieldNames, this.GetDataSetName(), this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0017C68F File Offset: 0x0017A88F
		internal void AggregateCheckFieldReferences(StringList fieldNames, string dataSetName, ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckFieldReference(fieldNames, dataSetName, objectType, objectName, propertyName);
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x0017C6A0 File Offset: 0x0017A8A0
		private void InternalCheckFieldReference(StringList fieldNames, string dataSetName, ObjectType objectType, string objectName, string propertyName)
		{
			if (fieldNames == null)
			{
				return;
			}
			if ((this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return;
			}
			Hashtable hashtable = null;
			if (dataSetName != null)
			{
				hashtable = (Hashtable)this.m_fieldNameMap[dataSetName];
			}
			for (int i = 0; i < fieldNames.Count; i++)
			{
				string text = fieldNames[i];
				if (this.m_numberOfDataSets == 0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldReference, Severity.Error, objectType, objectName, propertyName, new string[] { text });
				}
				else
				{
					Global.Tracer.Assert(1 <= this.m_numberOfDataSets);
					if (hashtable != null && !hashtable.ContainsKey(text))
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsFieldReference, Severity.Error, objectType, objectName, propertyName, new string[] { text });
					}
				}
			}
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x0017C75E File Offset: 0x0017A95E
		internal void FillInFieldIndex(ExpressionInfo exprInfo)
		{
			this.InternalFillInFieldIndex(exprInfo, this.GetDataSetName());
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x0017C76D File Offset: 0x0017A96D
		internal void FillInFieldIndex(ExpressionInfo exprInfo, string dataSetName)
		{
			this.InternalFillInFieldIndex(exprInfo, dataSetName);
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x0017C778 File Offset: 0x0017A978
		private void InternalFillInFieldIndex(ExpressionInfo exprInfo, string dataSetName)
		{
			if (exprInfo == null || exprInfo.Type != ExpressionInfo.Types.Field)
			{
				return;
			}
			if ((this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return;
			}
			if (dataSetName == null)
			{
				return;
			}
			Hashtable hashtable = (Hashtable)this.m_fieldNameMap[dataSetName];
			if (hashtable != null && hashtable.ContainsKey(exprInfo.Value))
			{
				exprInfo.IntValue = (int)hashtable[exprInfo.Value];
			}
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x0017C7DC File Offset: 0x0017A9DC
		internal void FillInTokenIndex(ExpressionInfo exprInfo)
		{
			if (exprInfo == null || exprInfo.Type != ExpressionInfo.Types.Token)
			{
				return;
			}
			string value = exprInfo.Value;
			if (value == null)
			{
				return;
			}
			DataSet dataSet = this.m_reportScopes[value] as DataSet;
			if (dataSet != null)
			{
				exprInfo.IntValue = dataSet.ID;
			}
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x0017C822 File Offset: 0x0017AA22
		internal void CheckDataSetReference(StringList referencedDataSets, string propertyName)
		{
			this.InternalCheckDataSetReference(referencedDataSets, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x0017C838 File Offset: 0x0017AA38
		internal void AggregateCheckDataSetReference(StringList referencedDataSets, ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckDataSetReference(referencedDataSets, objectType, objectName, propertyName);
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x0017C848 File Offset: 0x0017AA48
		private void InternalCheckDataSetReference(StringList dataSetNames, ObjectType objectType, string objectName, string propertyName)
		{
			if (dataSetNames == null)
			{
				return;
			}
			if ((this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return;
			}
			for (int i = 0; i < dataSetNames.Count; i++)
			{
				if (!this.m_dataSetNameToDataRegionsMap.ContainsKey(dataSetNames[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetReference, Severity.Error, objectType, objectName, propertyName, new string[] { dataSetNames[i] });
				}
			}
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0017C8B0 File Offset: 0x0017AAB0
		internal void CheckDataSourceReference(StringList referencedDataSources, string propertyName)
		{
			this.InternalCheckDataSourceReference(referencedDataSources, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0017C8C6 File Offset: 0x0017AAC6
		internal void AggregateCheckDataSourceReference(StringList referencedDataSources, ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckDataSetReference(referencedDataSources, objectType, objectName, propertyName);
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0017C8D4 File Offset: 0x0017AAD4
		private void InternalCheckDataSourceReference(StringList dataSourceNames, ObjectType objectType, string objectName, string propertyName)
		{
			if (dataSourceNames == null)
			{
				return;
			}
			if ((this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return;
			}
			for (int i = 0; i < dataSourceNames.Count; i++)
			{
				if (!this.m_dataSources.ContainsKey(dataSourceNames[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceReference, Severity.Error, objectType, objectName, propertyName, new string[] { dataSourceNames[i] });
				}
			}
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0017C93C File Offset: 0x0017AB3C
		internal int GenerateSubtotalID()
		{
			Global.Tracer.Assert(this.m_report != null);
			Report report = this.m_report;
			int lastID = report.LastID;
			report.LastID = lastID + 1;
			return this.m_report.LastID;
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0017C97C File Offset: 0x0017AB7C
		internal string GenerateAggregateID(string oldAggregateID)
		{
			Global.Tracer.Assert(this.m_report != null);
			Report report = this.m_report;
			int lastAggregateID = report.LastAggregateID;
			report.LastAggregateID = lastAggregateID + 1;
			string text = "Aggregate" + this.m_report.LastAggregateID.ToString();
			if (this.m_aggregateRewriteMap == null)
			{
				this.m_aggregateRewriteMap = new Hashtable();
			}
			this.m_aggregateRewriteMap.Add(oldAggregateID, text);
			return text;
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0017C9F0 File Offset: 0x0017ABF0
		internal void RegisterReportItems(ReportItemCollection reportItems)
		{
			Global.Tracer.Assert(reportItems != null, "(null != reportItems)");
			for (int i = 0; i < reportItems.Count; i++)
			{
				ReportItem reportItem = reportItems[i];
				if (reportItem != null)
				{
					this.m_reportItemsInScope[reportItem.Name] = reportItem;
					if (reportItem is Rectangle)
					{
						this.RegisterReportItems(((Rectangle)reportItem).ReportItems);
					}
					if (reportItem is Table)
					{
						((Table)reportItem).RegisterHeaderAndFooter(this);
					}
					if (reportItem is Matrix)
					{
						this.RegisterReportItems(((Matrix)reportItem).CornerReportItems);
					}
				}
			}
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x0017CA8C File Offset: 0x0017AC8C
		internal void UnRegisterReportItems(ReportItemCollection reportItems)
		{
			Global.Tracer.Assert(reportItems != null, "(null != reportItems)");
			for (int i = 0; i < reportItems.Count; i++)
			{
				ReportItem reportItem = reportItems[i];
				if (reportItem != null)
				{
					this.m_reportItemsInScope.Remove(reportItem.Name);
					if (reportItem is Rectangle)
					{
						this.UnRegisterReportItems(((Rectangle)reportItem).ReportItems);
					}
					if (reportItem is Table)
					{
						((Table)reportItem).UnRegisterHeaderAndFooter(this);
					}
					if (reportItem is Matrix)
					{
						this.UnRegisterReportItems(((Matrix)reportItem).CornerReportItems);
					}
				}
			}
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x0017CB24 File Offset: 0x0017AD24
		internal void CheckReportItemReferences(StringList referencedReportItems, string propertyName)
		{
			this.InternalCheckReportItemReferences(referencedReportItems, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x0017CB3A File Offset: 0x0017AD3A
		internal void AggregateCheckReportItemReferences(StringList referencedReportItems, ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckReportItemReferences(referencedReportItems, objectType, objectName, propertyName);
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0017CB48 File Offset: 0x0017AD48
		private void InternalCheckReportItemReferences(StringList referencedReportItems, ObjectType objectType, string objectName, string propertyName)
		{
			if (referencedReportItems == null || (this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return;
			}
			for (int i = 0; i < referencedReportItems.Count; i++)
			{
				if (!this.m_reportItemsInScope.ContainsKey(referencedReportItems[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemReference, Severity.Error, objectType, objectName, propertyName, new string[] { referencedReportItems[i] });
				}
			}
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0017CBAF File Offset: 0x0017ADAF
		internal void CheckReportParameterReferences(StringList referencedParameters, string propertyName)
		{
			this.InternalCheckReportParameterReferences(referencedParameters, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0017CBC8 File Offset: 0x0017ADC8
		private void InternalCheckReportParameterReferences(StringList referencedParameters, ObjectType objectType, string objectName, string propertyName)
		{
			if (referencedParameters == null)
			{
				return;
			}
			for (int i = 0; i < referencedParameters.Count; i++)
			{
				if (this.m_parameters == null || !this.m_parameters.ContainsKey(referencedParameters[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsParameterReference, Severity.Error, objectType, objectName, propertyName, new string[] { referencedParameters[i] });
				}
			}
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x0017CC2C File Offset: 0x0017AE2C
		internal ToggleItemInfo RegisterReceiver(string senderName, Visibility visibility, bool isContainer)
		{
			if (senderName == null)
			{
				return null;
			}
			if ((this.m_location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				return null;
			}
			ReportItem reportItem = (ReportItem)this.m_reportItemsInScope[senderName];
			if (!(reportItem is TextBox))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidToggleItem, Severity.Error, this.m_objectType, this.m_objectName, "Item", new string[] { senderName });
			}
			else
			{
				((TextBox)reportItem).IsToggle = true;
				do
				{
					reportItem.Computed = true;
					reportItem = reportItem.Parent;
				}
				while (reportItem is Rectangle);
				if (isContainer)
				{
					ToggleItemInfoList toggleItemInfoList = (ToggleItemInfoList)this.m_toggleItemInfos[senderName];
					if (toggleItemInfoList == null)
					{
						toggleItemInfoList = new ToggleItemInfoList();
						this.m_toggleItemInfos[senderName] = toggleItemInfoList;
					}
					ToggleItemInfo toggleItemInfo = new ToggleItemInfo();
					toggleItemInfo.ObjectName = this.m_objectName;
					toggleItemInfo.ObjectType = this.m_objectType;
					toggleItemInfo.Visibility = visibility;
					toggleItemInfo.GroupName = this.m_currentGroupName;
					toggleItemInfoList.Add(toggleItemInfo);
					return toggleItemInfo;
				}
			}
			return null;
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0017CD20 File Offset: 0x0017AF20
		internal void UnRegisterReceiver(string senderName, ToggleItemInfo toggleItemInfo)
		{
			Global.Tracer.Assert(toggleItemInfo != null, "(null != toggleItemInfo)");
			ToggleItemInfoList toggleItemInfoList = (ToggleItemInfoList)this.m_toggleItemInfos[senderName];
			if (toggleItemInfoList != null)
			{
				toggleItemInfoList.Remove(toggleItemInfo);
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x0017CD5C File Offset: 0x0017AF5C
		internal void RegisterSender(TextBox textbox)
		{
			Global.Tracer.Assert(textbox != null);
			ToggleItemInfoList toggleItemInfoList = (ToggleItemInfoList)this.m_toggleItemInfos[textbox.Name];
			if (toggleItemInfoList != null && 0 < toggleItemInfoList.Count)
			{
				bool flag = false;
				if (this.m_currentGroupName != null)
				{
					InitializationContext.ScopeInfo scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_currentGroupName];
					Global.Tracer.Assert(scopeInfo != null && scopeInfo.GroupingScope != null);
					if (scopeInfo.GroupingScope.Parent != null)
					{
						flag = true;
					}
				}
				for (int i = 0; i < toggleItemInfoList.Count; i++)
				{
					ToggleItemInfo toggleItemInfo = toggleItemInfoList[i];
					if (flag && toggleItemInfo.GroupName == this.m_currentGroupName)
					{
						textbox.RecursiveSender = true;
						toggleItemInfo.Visibility.RecursiveReceiver = true;
					}
					else
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidToggleItem, Severity.Error, toggleItemInfo.ObjectType, toggleItemInfo.ObjectName, "Item", new string[] { textbox.Name });
					}
				}
				this.m_toggleItemInfos.Remove(textbox.Name);
			}
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x0017CE78 File Offset: 0x0017B078
		internal double ValidateSize(string size, string propertyName)
		{
			double num;
			string text;
			PublishingValidator.ValidateSize(size, this.m_objectType, this.m_objectName, propertyName, true, this.m_errorContext, out num, out text);
			return num;
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x0017CEA5 File Offset: 0x0017B0A5
		internal double ValidateSize(ref string size, string propertyName)
		{
			return this.ValidateSize(ref size, true, propertyName);
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x0017CEB0 File Offset: 0x0017B0B0
		internal double ValidateSize(ref string size, bool restrictMaxValue, string propertyName)
		{
			double num;
			string text;
			PublishingValidator.ValidateSize(size, this.m_objectType, this.m_objectName, propertyName, restrictMaxValue, this.m_errorContext, out num, out text);
			size = text;
			return num;
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x0017CEE4 File Offset: 0x0017B0E4
		internal void CheckInternationalSettings(StyleAttributeHashtable styleAttributes)
		{
			if (styleAttributes == null || styleAttributes.Count == 0)
			{
				return;
			}
			CultureInfo cultureInfo = null;
			AttributeInfo attributeInfo = styleAttributes["Language"];
			if (attributeInfo == null)
			{
				cultureInfo = this.m_reportLanguage;
			}
			else if (!attributeInfo.IsExpression)
			{
				PublishingValidator.ValidateLanguage(attributeInfo.Value, this.ObjectType, this.ObjectName, "Language", this.ErrorContext, out cultureInfo);
			}
			if (cultureInfo != null)
			{
				AttributeInfo attributeInfo2 = styleAttributes["Calendar"];
				if (attributeInfo2 != null && !attributeInfo2.IsExpression)
				{
					PublishingValidator.ValidateCalendar(cultureInfo, attributeInfo2.Value, this.ObjectType, this.ObjectName, "Calendar", this.ErrorContext);
				}
			}
			attributeInfo = styleAttributes["NumeralLanguage"];
			if (attributeInfo != null)
			{
				if (attributeInfo.IsExpression)
				{
					cultureInfo = null;
				}
				else
				{
					PublishingValidator.ValidateLanguage(attributeInfo.Value, this.ObjectType, this.ObjectName, "NumeralLanguage", this.ErrorContext, out cultureInfo);
				}
			}
			if (cultureInfo != null)
			{
				AttributeInfo attributeInfo3 = styleAttributes["NumeralVariant"];
				if (attributeInfo3 != null && !attributeInfo3.IsExpression)
				{
					PublishingValidator.ValidateNumeralVariant(cultureInfo, attributeInfo3.IntValue, this.ObjectType, this.ObjectName, "NumeralVariant", this.ErrorContext);
				}
			}
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0017D000 File Offset: 0x0017B200
		internal string GetCurrentScope()
		{
			if (this.m_currentScope == null)
			{
				return "0_ReportScope";
			}
			if (this.m_currentScope.GroupingScope != null)
			{
				return this.m_currentScope.GroupingScope.Name;
			}
			Global.Tracer.Assert(this.m_currentDataregionName != null, "(null != m_currentDataregionName)");
			return this.m_currentDataregionName;
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x0017D057 File Offset: 0x0017B257
		internal bool IsScope(string scope)
		{
			return scope != null && this.m_reportScopes.ContainsKey(scope);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0017D06C File Offset: 0x0017B26C
		internal bool IsAncestorScope(string targetScope, bool inMatrixGrouping, bool checkAllGroupingScopes)
		{
			string dataSetName = this.GetDataSetName();
			return (dataSetName != null && ReportProcessing.CompareWithInvariantCulture(dataSetName, targetScope, false) == 0) || (this.m_dataregionScopes != null && this.m_dataregionScopes.ContainsKey(targetScope)) || ((checkAllGroupingScopes || inMatrixGrouping) && this.m_groupingScopesForRunningValuesInTablix != null && this.m_groupingScopesForRunningValuesInTablix.ContainsScope(targetScope, null, false)) || ((checkAllGroupingScopes || !inMatrixGrouping) && this.m_groupingScopesForRunningValues != null && this.m_groupingScopesForRunningValues.ContainsKey(targetScope));
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x0017D0E8 File Offset: 0x0017B2E8
		internal bool IsCurrentScope(string targetScope)
		{
			if (this.m_currentScope != null)
			{
				if (this.m_currentScope.GroupingScope == null && ReportProcessing.CompareWithInvariantCulture(targetScope, this.m_currentDataregionName, false) == 0)
				{
					return true;
				}
				if (this.m_currentScope.GroupingScope != null && ReportProcessing.CompareWithInvariantCulture(targetScope, this.m_currentScope.GroupingScope.Name, false) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x0017D144 File Offset: 0x0017B344
		internal bool IsPeerScope(string targetScope)
		{
			if (!this.m_hasUserSortPeerScopes)
			{
				return false;
			}
			string currentScope = this.GetCurrentScope();
			Global.Tracer.Assert(currentScope != null && this.m_peerScopes != null, "(null != currentScope && null != m_peerScopes)");
			object obj = this.m_peerScopes[currentScope];
			if (obj == null)
			{
				return false;
			}
			int num = (int)obj;
			obj = this.m_peerScopes[targetScope];
			if (obj == null)
			{
				return false;
			}
			int num2 = (int)obj;
			return num == num2;
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x0017D1BA File Offset: 0x0017B3BA
		internal bool IsReportTopLevelScope()
		{
			return this.m_currentScope == null;
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x0017D1C5 File Offset: 0x0017B3C5
		internal ISortFilterScope GetSortFilterScope()
		{
			return this.GetSortFilterScope(this.GetCurrentScope());
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0017D1D3 File Offset: 0x0017B3D3
		internal ISortFilterScope GetSortFilterScope(string scopeName)
		{
			Global.Tracer.Assert(scopeName != null && "0_ReportScope" != scopeName && this.m_reportScopes.ContainsKey(scopeName));
			return this.m_reportScopes[scopeName] as ISortFilterScope;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x0017D20F File Offset: 0x0017B40F
		internal GroupingList GetGroupingList()
		{
			Global.Tracer.Assert(this.m_groupingList != null);
			return this.m_groupingList.Clone();
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x0017D230 File Offset: 0x0017B430
		internal void RegisterScopeInMatrixCell(string matrixName, string scope, bool registerMatrixCellScope)
		{
			Global.Tracer.Assert(matrixName != null && this.m_scopesInMatrixCells != null);
			StringList stringList = this.m_scopesInMatrixCells[matrixName] as StringList;
			if (stringList != null)
			{
				if (!stringList.Contains(scope))
				{
					stringList.Add(scope);
				}
			}
			else
			{
				stringList = new StringList();
				stringList.Add(scope);
				this.m_scopesInMatrixCells.Add(matrixName, stringList);
			}
			if (registerMatrixCellScope && !this.m_reportGroupingLists.ContainsKey(scope))
			{
				this.m_reportGroupingLists.Add(scope, this.GetGroupingList());
			}
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x0017D2BC File Offset: 0x0017B4BC
		internal void UpdateScopesInMatrixCells(string matrixName, GroupingList matrixGroups)
		{
			int count = this.m_groupingList.Count;
			int count2 = this.m_parentMatrixList.Count;
			Global.Tracer.Assert(1 <= count2 && ReportProcessing.CompareWithInvariantCulture(this.m_parentMatrixList[count2 - 1], matrixName, false) == 0);
			string text = null;
			if (1 < count2)
			{
				text = this.m_parentMatrixList[count2 - 2];
			}
			StringList stringList = this.m_scopesInMatrixCells[matrixName] as StringList;
			Global.Tracer.Assert(stringList != null);
			int count3 = stringList.Count;
			for (int i = 0; i < count3; i++)
			{
				string text2 = stringList[i];
				Global.Tracer.Assert(text2 != null);
				if (matrixGroups != null)
				{
					GroupingList groupingList = this.m_reportGroupingLists[text2] as GroupingList;
					Global.Tracer.Assert(groupingList != null && count <= groupingList.Count);
					groupingList.InsertRange(count, matrixGroups);
				}
				if (text != null)
				{
					this.RegisterScopeInMatrixCell(text, text2, false);
				}
			}
			this.m_scopesInMatrixCells.Remove(matrixName);
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x0017D3C8 File Offset: 0x0017B5C8
		internal void RegisterPeerScopes(ReportItemCollection reportItems)
		{
			int num = this.m_lastPeerScopeId + 1;
			this.m_lastPeerScopeId = num;
			this.RegisterPeerScopes(reportItems, num, true);
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x0017D3F0 File Offset: 0x0017B5F0
		private void RegisterMatrixPeerScopes(MatrixHeading headings, int scopeID)
		{
			if (headings == null)
			{
				return;
			}
			while (headings != null)
			{
				if (headings.Grouping == null)
				{
					this.RegisterPeerScopes(headings.ReportItems, scopeID, false);
					headings = headings.SubHeading;
				}
				else
				{
					if (headings.Subtotal != null)
					{
						this.RegisterPeerScopes(headings.Subtotal.ReportItems, scopeID, false);
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x0017D440 File Offset: 0x0017B640
		private void RegisterPeerScopes(ReportItemCollection reportItems, int scopeID, bool traverse)
		{
			if (reportItems == null || !this.m_hasUserSortPeerScopes)
			{
				return;
			}
			string currentScope = this.GetCurrentScope();
			if (this.m_peerScopes.ContainsKey(currentScope))
			{
				return;
			}
			int count = reportItems.Count;
			for (int i = 0; i < count; i++)
			{
				ReportItem reportItem = reportItems[i];
				if (reportItem is Rectangle)
				{
					this.RegisterPeerScopes(((Rectangle)reportItem).ReportItems, scopeID, traverse);
				}
				else if (reportItem is DataRegion && !this.m_peerScopes.ContainsKey(reportItem.Name))
				{
					this.m_peerScopes.Add(reportItem.Name, scopeID);
				}
				if (reportItem is CustomReportItem)
				{
					this.RegisterPeerScopes(((CustomReportItem)reportItem).AltReportItem, scopeID, traverse);
				}
				else if (traverse)
				{
					if (reportItem is Matrix)
					{
						this.RegisterPeerScopes(((Matrix)reportItem).CornerReportItems, scopeID, false);
						this.RegisterMatrixPeerScopes(((Matrix)reportItem).Columns, scopeID);
						this.RegisterMatrixPeerScopes(((Matrix)reportItem).Rows, scopeID);
					}
					else if (reportItem is Table)
					{
						Table table = reportItem as Table;
						if (table.HeaderRows != null)
						{
							int num = table.HeaderRows.Count;
							for (int j = 0; j < num; j++)
							{
								this.RegisterPeerScopes(table.HeaderRows[j].ReportItems, scopeID, false);
							}
						}
						if (table.FooterRows != null)
						{
							int num = table.FooterRows.Count;
							for (int k = 0; k < num; k++)
							{
								this.RegisterPeerScopes(table.FooterRows[k].ReportItems, scopeID, false);
							}
						}
					}
				}
			}
			if (!this.m_peerScopes.ContainsKey(currentScope))
			{
				this.m_peerScopes.Add(currentScope, scopeID);
			}
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x0017D604 File Offset: 0x0017B804
		internal void RegisterUserSortInnerScope(TextBox textbox)
		{
			Global.Tracer.Assert(textbox.UserSort != null && textbox.UserSort.SortExpressionScopeString != null && this.m_userSortExpressionScopes != null && this.m_userSortTextboxes != null);
			string currentScope = this.GetCurrentScope();
			TextBoxList textBoxList = this.m_userSortExpressionScopes[textbox.UserSort.SortExpressionScopeString] as TextBoxList;
			if (textBoxList != null)
			{
				if (!textBoxList.Contains(textbox))
				{
					textBoxList.Add(textbox);
				}
			}
			else
			{
				textBoxList = new TextBoxList();
				textBoxList.Add(textbox);
				this.m_userSortExpressionScopes.Add(textbox.UserSort.SortExpressionScopeString, textBoxList);
			}
			textBoxList = this.m_userSortTextboxes[currentScope] as TextBoxList;
			if (textBoxList != null)
			{
				if (!textBoxList.Contains(textbox))
				{
					textBoxList.Add(textbox);
					return;
				}
			}
			else
			{
				textBoxList = new TextBoxList();
				textBoxList.Add(textbox);
				this.m_userSortTextboxes.Add(currentScope, textBoxList);
			}
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x0017D6E4 File Offset: 0x0017B8E4
		internal void ProcessUserSortInnerScope(string scopeName, bool isMatrixGroup, bool isMatrixColumnGroup)
		{
			TextBoxList textBoxList = this.m_userSortExpressionScopes[scopeName] as TextBoxList;
			if (textBoxList != null)
			{
				int count = textBoxList.Count;
				for (int i = 0; i < count; i++)
				{
					TextBox textBox = textBoxList[i];
					Global.Tracer.Assert(textBox.UserSort != null, "(null != textbox.UserSort)");
					if (isMatrixGroup && this.m_groupingScopesForRunningValuesInTablix != null)
					{
						string text = ((textBox.UserSort.SortTarget != null) ? textBox.UserSort.SortTarget.ScopeName : null);
						textBox.UserSort.FoundSortExpressionScope = !this.m_groupingScopesForRunningValuesInTablix.HasRowColScopeConflict(textBox.TextBoxScope, text, isMatrixColumnGroup);
					}
					else
					{
						textBox.UserSort.FoundSortExpressionScope = true;
					}
					textBox.InitializeSortExpression(this, false);
				}
			}
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x0017D7AC File Offset: 0x0017B9AC
		internal void ValidateUserSortInnerScope(string scopeName)
		{
			TextBoxList textBoxList = this.m_userSortTextboxes[scopeName] as TextBoxList;
			if (textBoxList != null)
			{
				int count = textBoxList.Count;
				for (int i = 0; i < count; i++)
				{
					TextBox textBox = textBoxList[i];
					Global.Tracer.Assert(textBox.UserSort != null, "(null != textbox.UserSort)");
					if (!textBox.UserSort.FoundSortExpressionScope)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionScope, Severity.Error, textBox.ObjectType, textBox.Name, "SortExpressionScope", new string[] { textBox.UserSort.SortExpressionScopeString });
					}
					else
					{
						textBox.UserSort.SortExpressionScope = this.GetSortFilterScope(textBox.UserSort.SortExpressionScopeString);
					}
				}
				this.m_userSortTextboxes.Remove(scopeName);
			}
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x0017D875 File Offset: 0x0017BA75
		internal void RegisterSortFilterTextbox(TextBox textbox)
		{
			this.m_reportSortFilterTextboxes.Add(textbox);
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x0017D884 File Offset: 0x0017BA84
		internal void SetDataSetDetailUserSortFilter()
		{
			string dataSetName = this.GetDataSetName();
			Global.Tracer.Assert(dataSetName != null, "(null != currentDataset)");
			DataSet dataSet = this.m_reportScopes[dataSetName] as DataSet;
			Global.Tracer.Assert(dataSet != null, "(null != dataset)");
			dataSet.HasDetailUserSortFilter = true;
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x0017D8D8 File Offset: 0x0017BAD8
		internal void CalculateSortFilterGroupingLists()
		{
			int num = this.m_reportSortFilterTextboxes.Count;
			for (int i = 0; i < num; i++)
			{
				TextBox textBox = this.m_reportSortFilterTextboxes[i];
				Global.Tracer.Assert(textBox != null && textBox.TextBoxScope != null);
				if (textBox.IsMatrixCellScope)
				{
					textBox.ContainingScopes = this.m_reportGroupingLists["0_CellScope" + textBox.TextBoxScope] as GroupingList;
				}
				if (!textBox.IsMatrixCellScope || textBox.ContainingScopes == null)
				{
					textBox.ContainingScopes = this.m_reportGroupingLists[textBox.TextBoxScope] as GroupingList;
				}
				Global.Tracer.Assert(textBox.ContainingScopes != null, "(null != textbox.ContainingScopes)");
				for (int j = 0; j < textBox.ContainingScopes.Count; j++)
				{
					textBox.ContainingScopes[j].SaveGroupExprValues = true;
				}
				if (textBox.IsDetailScope)
				{
					textBox.ContainingScopes = textBox.ContainingScopes.Clone();
					textBox.ContainingScopes.Add(null);
				}
				if (textBox.UserSort != null && textBox.UserSort.SortTarget != null)
				{
					string scopeName = textBox.UserSort.SortTarget.ScopeName;
					int num2 = (int)this.m_reportScopeDatasetIDs[scopeName];
					textBox.UserSort.DataSetID = num2;
					if (textBox.UserSort.SortExpressionScope != null)
					{
						string scopeName2 = textBox.UserSort.SortExpressionScope.ScopeName;
						Global.Tracer.Assert(scopeName2 != null && scopeName != null, "(null != esScope && null != stScope)");
						int num3 = (int)this.m_reportScopeDatasetIDs[scopeName2];
						Global.Tracer.Assert(0 <= num3 && 0 <= num2);
						if (num3 == num2)
						{
							textBox.UserSort.GroupsInSortTarget = this.CalculateGroupingDifference(this.m_reportGroupingLists[scopeName2] as GroupingList, this.m_reportGroupingLists[scopeName] as GroupingList);
						}
						else
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionScopeDataSet, Severity.Error, textBox.ObjectType, textBox.Name, "SortExpressionScope", new string[]
							{
								textBox.UserSort.SortExpressionScopeString,
								"SortTarget"
							});
						}
					}
					if (!this.m_errorContext.HasError)
					{
						textBox.AddToScopeSortFilterList();
					}
				}
			}
			num = ((this.m_report.SubReports == null) ? 0 : this.m_report.SubReports.Count);
			for (int k = 0; k < num; k++)
			{
				SubReport subReport = this.m_report.SubReports[k];
				if (subReport.SubReportScope != null)
				{
					if (subReport.IsMatrixCellScope)
					{
						subReport.ContainingScopes = this.m_reportGroupingLists["0_CellScope" + subReport.SubReportScope] as GroupingList;
					}
					else
					{
						subReport.ContainingScopes = this.m_reportGroupingLists[subReport.SubReportScope] as GroupingList;
					}
					Global.Tracer.Assert(subReport.ContainingScopes != null, "(null != subReport.ContainingScopes)");
					for (int l = 0; l < subReport.ContainingScopes.Count; l++)
					{
						subReport.ContainingScopes[l].SaveGroupExprValues = true;
					}
				}
				if (subReport.IsDetailScope)
				{
					subReport.ContainingScopes = subReport.ContainingScopes.Clone();
					subReport.ContainingScopes.Add(null);
				}
			}
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x0017DC48 File Offset: 0x0017BE48
		private GroupingList CalculateGroupingDifference(GroupingList expressionScope, GroupingList targetScope)
		{
			if (expressionScope == null || expressionScope.Count == 0)
			{
				return null;
			}
			if (targetScope == null || targetScope.Count == 0)
			{
				return expressionScope;
			}
			if (expressionScope.Count < targetScope.Count)
			{
				return null;
			}
			GroupingList groupingList = expressionScope.Clone();
			int count = targetScope.Count;
			int num = expressionScope.IndexOf(targetScope[0]);
			if (num < 0)
			{
				return groupingList;
			}
			Global.Tracer.Assert(num + count <= expressionScope.Count, "(startIndex + count <= expressionScope.Count)");
			groupingList.RemoveRange(0, num + 1);
			for (int i = 1; i < count; i++)
			{
				if (expressionScope[num + i] != targetScope[i])
				{
					return groupingList;
				}
				groupingList.RemoveAt(0);
			}
			return groupingList;
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x0017DCF2 File Offset: 0x0017BEF2
		internal void TextboxWithDetailSortExpressionAdd(TextBox textbox)
		{
			Global.Tracer.Assert(this.m_detailSortExpressionScopeTextboxes != null, "(null != m_detailSortExpressionScopeTextboxes)");
			this.m_detailSortExpressionScopeTextboxes.Add(textbox);
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x0017DD1C File Offset: 0x0017BF1C
		internal void TextboxesWithDetailSortExpressionInitialize()
		{
			Global.Tracer.Assert(this.m_detailSortExpressionScopeTextboxes != null, "(null != m_detailSortExpressionScopeTextboxes)");
			int count = this.m_detailSortExpressionScopeTextboxes.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.m_detailSortExpressionScopeTextboxes[i].InitializeSortExpression(this, true);
			}
			this.m_detailSortExpressionScopeTextboxes.RemoveRange(0, count);
		}

		// Token: 0x04002FA0 RID: 12192
		private ICatalogItemContext m_reportContext;

		// Token: 0x04002FA1 RID: 12193
		private LocationFlags m_location;

		// Token: 0x04002FA2 RID: 12194
		private ObjectType m_objectType;

		// Token: 0x04002FA3 RID: 12195
		private string m_objectName;

		// Token: 0x04002FA4 RID: 12196
		private ObjectType m_detailObjectType;

		// Token: 0x04002FA5 RID: 12197
		private string m_matrixName;

		// Token: 0x04002FA6 RID: 12198
		private EmbeddedImageHashtable m_embeddedImages;

		// Token: 0x04002FA7 RID: 12199
		private ImageStreamNames m_imageStreamNames;

		// Token: 0x04002FA8 RID: 12200
		private ErrorContext m_errorContext;

		// Token: 0x04002FA9 RID: 12201
		private Hashtable m_parameters;

		// Token: 0x04002FAA RID: 12202
		private ArrayList m_dynamicParameters;

		// Token: 0x04002FAB RID: 12203
		private Hashtable m_dataSetQueryInfo;

		// Token: 0x04002FAC RID: 12204
		private ExprHostBuilder m_exprHostBuilder;

		// Token: 0x04002FAD RID: 12205
		private Report m_report;

		// Token: 0x04002FAE RID: 12206
		private StringList m_aggregateEscalateScopes;

		// Token: 0x04002FAF RID: 12207
		private Hashtable m_aggregateRewriteScopes;

		// Token: 0x04002FB0 RID: 12208
		private Hashtable m_aggregateRewriteMap;

		// Token: 0x04002FB1 RID: 12209
		private int m_dataRegionCount;

		// Token: 0x04002FB2 RID: 12210
		private string m_outerGroupName;

		// Token: 0x04002FB3 RID: 12211
		private string m_currentGroupName;

		// Token: 0x04002FB4 RID: 12212
		private string m_currentDataregionName;

		// Token: 0x04002FB5 RID: 12213
		private RunningValueInfoList m_runningValues;

		// Token: 0x04002FB6 RID: 12214
		private Hashtable m_groupingScopesForRunningValues;

		// Token: 0x04002FB7 RID: 12215
		private InitializationContext.GroupingScopesForTablix m_groupingScopesForRunningValuesInTablix;

		// Token: 0x04002FB8 RID: 12216
		private Hashtable m_dataregionScopesForRunningValues;

		// Token: 0x04002FB9 RID: 12217
		private bool m_hasFilters;

		// Token: 0x04002FBA RID: 12218
		private InitializationContext.ScopeInfo m_currentScope;

		// Token: 0x04002FBB RID: 12219
		private InitializationContext.ScopeInfo m_outermostDataregionScope;

		// Token: 0x04002FBC RID: 12220
		private Hashtable m_groupingScopes;

		// Token: 0x04002FBD RID: 12221
		private Hashtable m_dataregionScopes;

		// Token: 0x04002FBE RID: 12222
		private Hashtable m_datasetScopes;

		// Token: 0x04002FBF RID: 12223
		private int m_numberOfDataSets;

		// Token: 0x04002FC0 RID: 12224
		private string m_oneDataSetName;

		// Token: 0x04002FC1 RID: 12225
		private string m_currentDataSetName;

		// Token: 0x04002FC2 RID: 12226
		private Hashtable m_fieldNameMap;

		// Token: 0x04002FC3 RID: 12227
		private Hashtable m_dataSetNameToDataRegionsMap;

		// Token: 0x04002FC4 RID: 12228
		private StringDictionary m_dataSources;

		// Token: 0x04002FC5 RID: 12229
		private Hashtable m_reportItemsInScope;

		// Token: 0x04002FC6 RID: 12230
		private Hashtable m_toggleItemInfos;

		// Token: 0x04002FC7 RID: 12231
		private bool m_registerReceiver;

		// Token: 0x04002FC8 RID: 12232
		private CultureInfo m_reportLanguage;

		// Token: 0x04002FC9 RID: 12233
		private bool m_reportDataElementStyleAttribute;

		// Token: 0x04002FCA RID: 12234
		private bool m_tableColumnVisible;

		// Token: 0x04002FCB RID: 12235
		private bool m_hasUserSortPeerScopes;

		// Token: 0x04002FCC RID: 12236
		private Hashtable m_userSortExpressionScopes;

		// Token: 0x04002FCD RID: 12237
		private Hashtable m_userSortTextboxes;

		// Token: 0x04002FCE RID: 12238
		private Hashtable m_peerScopes;

		// Token: 0x04002FCF RID: 12239
		private int m_lastPeerScopeId;

		// Token: 0x04002FD0 RID: 12240
		private Hashtable m_reportScopes;

		// Token: 0x04002FD1 RID: 12241
		private Hashtable m_reportScopeDatasetIDs;

		// Token: 0x04002FD2 RID: 12242
		private GroupingList m_groupingList;

		// Token: 0x04002FD3 RID: 12243
		private Hashtable m_reportGroupingLists;

		// Token: 0x04002FD4 RID: 12244
		private Hashtable m_scopesInMatrixCells;

		// Token: 0x04002FD5 RID: 12245
		private StringList m_parentMatrixList;

		// Token: 0x04002FD6 RID: 12246
		private TextBoxList m_reportSortFilterTextboxes;

		// Token: 0x04002FD7 RID: 12247
		private TextBoxList m_detailSortExpressionScopeTextboxes;

		// Token: 0x02000CB9 RID: 3257
		private enum GroupingType
		{
			// Token: 0x04004E54 RID: 20052
			Normal,
			// Token: 0x04004E55 RID: 20053
			MatrixRow,
			// Token: 0x04004E56 RID: 20054
			MatrixColumn
		}

		// Token: 0x02000CBA RID: 3258
		private sealed class ScopeInfo
		{
			// Token: 0x06008CE3 RID: 36067 RVA: 0x0023D298 File Offset: 0x0023B498
			internal ScopeInfo(bool allowCustomAggregates, DataAggregateInfoList aggregates)
			{
				this.m_allowCustomAggregates = allowCustomAggregates;
				this.m_aggregates = aggregates;
			}

			// Token: 0x06008CE4 RID: 36068 RVA: 0x0023D2AE File Offset: 0x0023B4AE
			internal ScopeInfo(bool allowCustomAggregates, DataAggregateInfoList aggregates, DataAggregateInfoList postSortAggregates)
			{
				this.m_allowCustomAggregates = allowCustomAggregates;
				this.m_aggregates = aggregates;
				this.m_postSortAggregates = postSortAggregates;
			}

			// Token: 0x06008CE5 RID: 36069 RVA: 0x0023D2CB File Offset: 0x0023B4CB
			internal ScopeInfo(bool allowCustomAggregates, DataAggregateInfoList aggregates, DataAggregateInfoList postSortAggregates, DataRegion dataRegion)
			{
				this.m_allowCustomAggregates = allowCustomAggregates;
				this.m_aggregates = aggregates;
				this.m_postSortAggregates = postSortAggregates;
				this.m_dataRegionScope = dataRegion;
			}

			// Token: 0x06008CE6 RID: 36070 RVA: 0x0023D2F0 File Offset: 0x0023B4F0
			internal ScopeInfo(bool allowCustomAggregates, DataAggregateInfoList aggregates, DataAggregateInfoList postSortAggregates, DataSet dataset)
			{
				this.m_allowCustomAggregates = allowCustomAggregates;
				this.m_aggregates = aggregates;
				this.m_postSortAggregates = postSortAggregates;
				this.m_dataSetScope = dataset;
			}

			// Token: 0x06008CE7 RID: 36071 RVA: 0x0023D315 File Offset: 0x0023B515
			internal ScopeInfo(bool allowCustomAggregates, DataAggregateInfoList aggregates, DataAggregateInfoList postSortAggregates, DataAggregateInfoList recursiveAggregates, Grouping groupingScope)
			{
				this.m_allowCustomAggregates = allowCustomAggregates;
				this.m_aggregates = aggregates;
				this.m_postSortAggregates = postSortAggregates;
				this.m_recursiveAggregates = recursiveAggregates;
				this.m_groupingScope = groupingScope;
			}

			// Token: 0x17002B4B RID: 11083
			// (get) Token: 0x06008CE8 RID: 36072 RVA: 0x0023D342 File Offset: 0x0023B542
			internal bool AllowCustomAggregates
			{
				get
				{
					return this.m_allowCustomAggregates;
				}
			}

			// Token: 0x17002B4C RID: 11084
			// (get) Token: 0x06008CE9 RID: 36073 RVA: 0x0023D34A File Offset: 0x0023B54A
			internal DataAggregateInfoList Aggregates
			{
				get
				{
					return this.m_aggregates;
				}
			}

			// Token: 0x17002B4D RID: 11085
			// (get) Token: 0x06008CEA RID: 36074 RVA: 0x0023D352 File Offset: 0x0023B552
			internal DataAggregateInfoList PostSortAggregates
			{
				get
				{
					return this.m_postSortAggregates;
				}
			}

			// Token: 0x17002B4E RID: 11086
			// (get) Token: 0x06008CEB RID: 36075 RVA: 0x0023D35A File Offset: 0x0023B55A
			internal DataAggregateInfoList RecursiveAggregates
			{
				get
				{
					return this.m_recursiveAggregates;
				}
			}

			// Token: 0x17002B4F RID: 11087
			// (get) Token: 0x06008CEC RID: 36076 RVA: 0x0023D362 File Offset: 0x0023B562
			internal Grouping GroupingScope
			{
				get
				{
					return this.m_groupingScope;
				}
			}

			// Token: 0x17002B50 RID: 11088
			// (get) Token: 0x06008CED RID: 36077 RVA: 0x0023D36A File Offset: 0x0023B56A
			internal DataSet DataSetScope
			{
				get
				{
					return this.m_dataSetScope;
				}
			}

			// Token: 0x04004E57 RID: 20055
			private bool m_allowCustomAggregates;

			// Token: 0x04004E58 RID: 20056
			private DataAggregateInfoList m_aggregates;

			// Token: 0x04004E59 RID: 20057
			private DataAggregateInfoList m_postSortAggregates;

			// Token: 0x04004E5A RID: 20058
			private DataAggregateInfoList m_recursiveAggregates;

			// Token: 0x04004E5B RID: 20059
			private Grouping m_groupingScope;

			// Token: 0x04004E5C RID: 20060
			private DataRegion m_dataRegionScope;

			// Token: 0x04004E5D RID: 20061
			private DataSet m_dataSetScope;
		}

		// Token: 0x02000CBB RID: 3259
		private sealed class GroupingScopesForTablix
		{
			// Token: 0x06008CEE RID: 36078 RVA: 0x0023D372 File Offset: 0x0023B572
			internal GroupingScopesForTablix(bool forceRows, ObjectType containerType, string containerName)
			{
				this.m_rowScopeFound = forceRows;
				this.m_columnScopeFound = false;
				this.m_containerType = containerType;
				this.m_containerName = containerName;
				this.m_rowScopes = new Hashtable();
				this.m_columnScopes = new Hashtable();
			}

			// Token: 0x06008CEF RID: 36079 RVA: 0x0023D3AC File Offset: 0x0023B5AC
			internal void RegisterRowGrouping(string groupName)
			{
				Global.Tracer.Assert(groupName != null);
				this.m_rowScopes[groupName] = null;
			}

			// Token: 0x06008CF0 RID: 36080 RVA: 0x0023D3C9 File Offset: 0x0023B5C9
			internal void UnRegisterRowGrouping(string groupName)
			{
				Global.Tracer.Assert(groupName != null);
				this.m_rowScopes.Remove(groupName);
			}

			// Token: 0x06008CF1 RID: 36081 RVA: 0x0023D3E5 File Offset: 0x0023B5E5
			internal void RegisterColumnGrouping(string groupName)
			{
				Global.Tracer.Assert(groupName != null);
				this.m_columnScopes[groupName] = null;
			}

			// Token: 0x06008CF2 RID: 36082 RVA: 0x0023D402 File Offset: 0x0023B602
			internal void UnRegisterColumnGrouping(string groupName)
			{
				Global.Tracer.Assert(groupName != null);
				this.m_columnScopes.Remove(groupName);
			}

			// Token: 0x06008CF3 RID: 36083 RVA: 0x0023D420 File Offset: 0x0023B620
			private ProcessingErrorCode getErrorCode()
			{
				ObjectType containerType = this.m_containerType;
				if (containerType == ObjectType.Matrix)
				{
					return ProcessingErrorCode.rsConflictingRunningValueScopesInMatrix;
				}
				if (containerType == ObjectType.Chart)
				{
					return ProcessingErrorCode.rsConflictingRunningValueScopesInTablix;
				}
				if (containerType != ObjectType.CustomReportItem)
				{
					Global.Tracer.Assert(false, string.Empty);
					return ProcessingErrorCode.rsConflictingRunningValueScopesInMatrix;
				}
				return ProcessingErrorCode.rsConflictingRunningValueScopesInTablix;
			}

			// Token: 0x06008CF4 RID: 36084 RVA: 0x0023D460 File Offset: 0x0023B660
			internal bool HasRowColScopeConflict(string textboxSortActionScope, string sortTargetScope, bool sortExpressionScopeIsColumnScope)
			{
				return this.HasRowColScopeConflict(textboxSortActionScope, sortExpressionScopeIsColumnScope) || (sortTargetScope != null && this.HasRowColScopeConflict(sortTargetScope, sortExpressionScopeIsColumnScope));
			}

			// Token: 0x06008CF5 RID: 36085 RVA: 0x0023D47B File Offset: 0x0023B67B
			private bool HasRowColScopeConflict(string scope, bool sortExpressionScopeIsColumnScope)
			{
				return (!sortExpressionScopeIsColumnScope && this.m_columnScopes.ContainsKey(scope)) || (sortExpressionScopeIsColumnScope && this.m_rowScopes.ContainsKey(scope));
			}

			// Token: 0x06008CF6 RID: 36086 RVA: 0x0023D4A4 File Offset: 0x0023B6A4
			internal bool ContainsScope(string scope, ErrorContext errorContext, bool checkConflictingScope)
			{
				Global.Tracer.Assert(scope != null, "(null != scope)");
				if (this.m_rowScopes.ContainsKey(scope))
				{
					if (checkConflictingScope)
					{
						if (this.m_columnScopeFound)
						{
							errorContext.Register(this.getErrorCode(), Severity.Error, this.m_containerType, this.m_containerName, null, Array.Empty<string>());
						}
						this.m_rowScopeFound = true;
					}
					return true;
				}
				if (this.m_columnScopes.ContainsKey(scope))
				{
					if (checkConflictingScope)
					{
						if (this.m_rowScopeFound)
						{
							errorContext.Register(this.getErrorCode(), Severity.Error, this.m_containerType, this.m_containerName, null, Array.Empty<string>());
						}
						this.m_columnScopeFound = true;
					}
					return true;
				}
				return false;
			}

			// Token: 0x06008CF7 RID: 36087 RVA: 0x0023D549 File Offset: 0x0023B749
			internal bool IsRunningValueDirectionColumn()
			{
				return this.m_columnScopeFound;
			}

			// Token: 0x04004E5E RID: 20062
			private bool m_rowScopeFound;

			// Token: 0x04004E5F RID: 20063
			private bool m_columnScopeFound;

			// Token: 0x04004E60 RID: 20064
			private ObjectType m_containerType;

			// Token: 0x04004E61 RID: 20065
			private string m_containerName;

			// Token: 0x04004E62 RID: 20066
			private Hashtable m_rowScopes;

			// Token: 0x04004E63 RID: 20067
			private Hashtable m_columnScopes;
		}
	}
}
