using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000384 RID: 900
	public struct AutomaticSubtotalContext
	{
		// Token: 0x0600225E RID: 8798 RVA: 0x00084090 File Offset: 0x00082290
		internal AutomaticSubtotalContext(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, List<ICreateSubtotals> createSubtotals, List<Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> domainScopeGroups, NameValidator reportItemNameValidator, NameValidator scopeNameValidator, NameValidator variableNameValidator, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope> reportScopes, List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection> reportItemCollections, List<Microsoft.ReportingServices.ReportIntermediateFormat.IAggregateHolder> aggregateHolders, List<Microsoft.ReportingServices.ReportIntermediateFormat.IRunningValueHolder> runningValueHolders, Holder<int> variableSequenceIdCounter, Holder<int> textboxSequenceIdCounter, ScopeTree scopeTree)
		{
			this.m_createSubtotals = createSubtotals;
			this.m_domainScopeGroups = domainScopeGroups;
			this.m_reportItemNameValidator = reportItemNameValidator;
			this.m_scopeNameValidator = scopeNameValidator;
			this.m_variableNameValidator = variableNameValidator;
			this.m_report = report;
			this.m_variableSequenceIdCounter = variableSequenceIdCounter;
			this.m_textboxSequenceIdCounter = textboxSequenceIdCounter;
			this.m_dynamicWithStaticPeerEncountered = false;
			this.m_location = LocationFlags.None;
			this.m_objectName = null;
			this.m_objectType = ObjectType.Tablix;
			this.m_currentDataRegion = null;
			this.m_cellLists = null;
			this.m_tablixColumns = null;
			this.m_rows = null;
			this.m_scopeNameMap = new Dictionary<string, string>(StringComparer.Ordinal);
			this.m_reportItemNameMap = new Dictionary<string, string>(StringComparer.Ordinal);
			this.m_aggregateMap = new Dictionary<string, string>(StringComparer.Ordinal);
			this.m_lookupMap = new Dictionary<string, string>(StringComparer.Ordinal);
			this.m_variableNameMap = new Dictionary<string, string>(StringComparer.Ordinal);
			this.m_currentScope = null;
			this.m_currentScopeBeingCloned = null;
			this.m_startIndex = new Holder<int>();
			this.m_currentIndex = new Holder<int>();
			this.m_headerLevel = 0;
			this.m_originalColumnCount = 0;
			this.m_originalRowCount = 0;
			this.m_reportScopes = reportScopes;
			this.m_reportItemCollections = reportItemCollections;
			this.m_aggregateHolders = aggregateHolders;
			this.m_runningValueHolders = runningValueHolders;
			this.m_expressionsWithReportItemReferences = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo>();
			this.m_visibilitiesWithToggleToUpdate = new List<Microsoft.ReportingServices.ReportIntermediateFormat.Visibility>();
			this.m_reportItemsWithRepeatWithToUpdate = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>();
			this.m_endUserSortWithTarget = new List<Microsoft.ReportingServices.ReportIntermediateFormat.EndUserSort>();
			this.m_scopeNamesToClone = new Dictionary<string, IRIFDataScope>(StringComparer.Ordinal);
			this.m_headerLevelHasStaticArray = null;
			this.m_currentDataRegionClone = null;
			this.m_currentMapClone = null;
			this.m_outerAggregate = null;
			this.m_scopeTree = scopeTree;
			this.m_currentDataScope = null;
			this.m_currentMapVectorLayerClone = null;
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x0008422A File Offset: 0x0008242A
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x00084232 File Offset: 0x00082432
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

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x0008423B File Offset: 0x0008243B
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x00084243 File Offset: 0x00082443
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

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x0008424C File Offset: 0x0008244C
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x00084254 File Offset: 0x00082454
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

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x0008425D File Offset: 0x0008245D
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x00084265 File Offset: 0x00082465
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion CurrentDataRegion
		{
			get
			{
				return this.m_currentDataRegion;
			}
			set
			{
				this.m_currentDataRegion = value;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x0008426E File Offset: 0x0008246E
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x00084276 File Offset: 0x00082476
		internal IRIFDataScope CurrentDataScope
		{
			get
			{
				return this.m_currentDataScope;
			}
			set
			{
				this.m_currentDataScope = value;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0008427F File Offset: 0x0008247F
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x00084287 File Offset: 0x00082487
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion CurrentDataRegionClone
		{
			get
			{
				return this.m_currentDataRegionClone;
			}
			set
			{
				this.m_currentDataRegionClone = value;
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x00084290 File Offset: 0x00082490
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x00084298 File Offset: 0x00082498
		internal Map CurrentMapClone
		{
			get
			{
				return this.m_currentMapClone;
			}
			set
			{
				this.m_currentMapClone = value;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x000842A1 File Offset: 0x000824A1
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x000842A9 File Offset: 0x000824A9
		internal MapVectorLayer CurrentMapVectorLayerClone
		{
			get
			{
				return this.m_currentMapVectorLayerClone;
			}
			set
			{
				this.m_currentMapVectorLayerClone = value;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000842B2 File Offset: 0x000824B2
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x000842BA File Offset: 0x000824BA
		internal string CurrentScope
		{
			get
			{
				return this.m_currentScope;
			}
			set
			{
				this.m_currentScope = value;
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000842C3 File Offset: 0x000824C3
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x000842CB File Offset: 0x000824CB
		internal string CurrentScopeBeingCloned
		{
			get
			{
				return this.m_currentScopeBeingCloned;
			}
			set
			{
				this.m_currentScopeBeingCloned = value;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000842D4 File Offset: 0x000824D4
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x000842DC File Offset: 0x000824DC
		internal bool[] HeaderLevelHasStaticArray
		{
			get
			{
				return this.m_headerLevelHasStaticArray;
			}
			set
			{
				this.m_headerLevelHasStaticArray = value;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000842E5 File Offset: 0x000824E5
		internal List<ICreateSubtotals> CreateSubtotalsDefinitions
		{
			get
			{
				return this.m_createSubtotals;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000842ED File Offset: 0x000824ED
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> DomainScopeGroups
		{
			get
			{
				return this.m_domainScopeGroups;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000842F5 File Offset: 0x000824F5
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x00084302 File Offset: 0x00082502
		internal int StartIndex
		{
			get
			{
				return this.m_startIndex.Value;
			}
			set
			{
				this.m_startIndex.Value = value;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00084310 File Offset: 0x00082510
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x0008431D File Offset: 0x0008251D
		internal int CurrentIndex
		{
			get
			{
				return this.m_currentIndex.Value;
			}
			set
			{
				this.m_currentIndex.Value = value;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0008432B File Offset: 0x0008252B
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x00084333 File Offset: 0x00082533
		internal List<CellList> CellLists
		{
			get
			{
				return this.m_cellLists;
			}
			set
			{
				this.m_cellLists = value;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x0008433C File Offset: 0x0008253C
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x00084344 File Offset: 0x00082544
		internal List<TablixColumn> TablixColumns
		{
			get
			{
				return this.m_tablixColumns;
			}
			set
			{
				this.m_tablixColumns = value;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x0008434D File Offset: 0x0008254D
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x00084355 File Offset: 0x00082555
		internal RowList Rows
		{
			get
			{
				return this.m_rows;
			}
			set
			{
				this.m_rows = value;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x0008435E File Offset: 0x0008255E
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x00084366 File Offset: 0x00082566
		internal bool DynamicWithStaticPeerEncountered
		{
			get
			{
				return this.m_dynamicWithStaticPeerEncountered;
			}
			set
			{
				this.m_dynamicWithStaticPeerEncountered = value;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0008436F File Offset: 0x0008256F
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x00084377 File Offset: 0x00082577
		internal int HeaderLevel
		{
			get
			{
				return this.m_headerLevel;
			}
			set
			{
				this.m_headerLevel = value;
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x00084380 File Offset: 0x00082580
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x00084388 File Offset: 0x00082588
		internal int OriginalColumnCount
		{
			get
			{
				return this.m_originalColumnCount;
			}
			set
			{
				this.m_originalColumnCount = value;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x00084391 File Offset: 0x00082591
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x00084399 File Offset: 0x00082599
		internal int OriginalRowCount
		{
			get
			{
				return this.m_originalRowCount;
			}
			set
			{
				this.m_originalRowCount = value;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000843A2 File Offset: 0x000825A2
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x000843AA File Offset: 0x000825AA
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo OuterAggregate
		{
			get
			{
				return this.m_outerAggregate;
			}
			set
			{
				this.m_outerAggregate = value;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000843B3 File Offset: 0x000825B3
		internal Dictionary<string, IRIFDataScope> ScopeNamesToClone
		{
			get
			{
				return this.m_scopeNamesToClone;
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000843BC File Offset: 0x000825BC
		internal int GenerateVariableSequenceID()
		{
			Holder<int> variableSequenceIdCounter = this.m_variableSequenceIdCounter;
			int value = variableSequenceIdCounter.Value;
			variableSequenceIdCounter.Value = value + 1;
			return value;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000843E0 File Offset: 0x000825E0
		internal int GenerateTextboxSequenceID()
		{
			Holder<int> textboxSequenceIdCounter = this.m_textboxSequenceIdCounter;
			int value = textboxSequenceIdCounter.Value;
			textboxSequenceIdCounter.Value = value + 1;
			return value;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00084404 File Offset: 0x00082604
		internal bool HasStaticPeerWithHeader(TablixMember member, out int spanDifference)
		{
			spanDifference = 0;
			if (member.HeaderLevel == -1)
			{
				return false;
			}
			int num = member.HeaderLevel + (member.IsColumn ? member.RowSpan : member.ColSpan);
			Global.Tracer.Assert(num <= this.m_headerLevelHasStaticArray.Length, "(count <= m_headerLevelHasStaticArray.Length)");
			for (int i = member.HeaderLevel; i < num; i++)
			{
				if (this.m_headerLevelHasStaticArray[i])
				{
					if (member.Grouping != null)
					{
						this.m_dynamicWithStaticPeerEncountered = true;
						spanDifference = i - member.HeaderLevel;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00084491 File Offset: 0x00082691
		internal void AddReportItemCollection(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection collection)
		{
			this.m_reportItemCollections.Add(collection);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0008449F File Offset: 0x0008269F
		internal void AddAggregateHolder(Microsoft.ReportingServices.ReportIntermediateFormat.IAggregateHolder aggregateHolder)
		{
			this.m_aggregateHolders.Add(aggregateHolder);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000844AD File Offset: 0x000826AD
		internal void AddRunningValueHolder(Microsoft.ReportingServices.ReportIntermediateFormat.IRunningValueHolder runningValueHolder)
		{
			this.m_runningValueHolders.Add(runningValueHolder);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000844BB File Offset: 0x000826BB
		internal string CreateUniqueReportItemName(string oldName, bool isClone)
		{
			return this.CreateUniqueReportItemName(oldName, false, isClone);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000844C8 File Offset: 0x000826C8
		internal string CreateUniqueReportItemName(string oldName, bool emptyRectangle, bool isClone)
		{
			string text = null;
			if (!emptyRectangle && this.m_reportItemNameMap.TryGetValue(oldName, out text))
			{
				return text;
			}
			int num = 1;
			if (isClone)
			{
				num = 2;
			}
			do
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(oldName);
				if (emptyRectangle && !isClone)
				{
					stringBuilder.Append("_AsRectangle");
				}
				else if (isClone)
				{
					stringBuilder.Append("_");
				}
				else
				{
					stringBuilder.Append("_InAutoSubtotal");
				}
				stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture.NumberFormat));
				text = stringBuilder.ToString();
				num++;
			}
			while (!this.m_reportItemNameValidator.Validate(text));
			if (!emptyRectangle)
			{
				if (this.m_reportItemNameMap.ContainsKey(oldName))
				{
					this.m_reportItemNameMap[oldName] = text;
				}
				else
				{
					this.m_reportItemNameMap.Add(oldName, text);
				}
			}
			return text;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00084594 File Offset: 0x00082794
		internal string GetNewReportItemName(string oldName)
		{
			string text;
			if (this.m_reportItemNameMap.TryGetValue(oldName, out text))
			{
				return text;
			}
			return oldName;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000845B4 File Offset: 0x000827B4
		internal string CreateAndRegisterUniqueGroupName(string oldName, bool isClone)
		{
			return this.CreateAndRegisterUniqueGroupName(oldName, isClone, false);
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000845C0 File Offset: 0x000827C0
		internal string CreateAndRegisterUniqueGroupName(string oldName, bool isClone, bool isDomainScope)
		{
			string text = null;
			if (this.m_scopeNameMap.TryGetValue(oldName, out text))
			{
				return text;
			}
			int num = 1;
			if (isClone)
			{
				num = 2;
			}
			do
			{
				StringBuilder stringBuilder;
				if (isDomainScope)
				{
					stringBuilder = new StringBuilder(oldName.Length + 16);
				}
				else if (isClone)
				{
					stringBuilder = new StringBuilder(oldName.Length + 4);
				}
				else
				{
					stringBuilder = new StringBuilder(oldName.Length + 19);
				}
				stringBuilder.Append(oldName);
				if (isDomainScope)
				{
					stringBuilder.Append("_DomainScope");
				}
				else if (isClone)
				{
					stringBuilder.Append("_");
				}
				else
				{
					stringBuilder.Append("_InAutoSubtotal");
				}
				stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture.NumberFormat));
				text = stringBuilder.ToString();
				num++;
			}
			while (!this.m_scopeNameValidator.Validate(text));
			this.RegisterClonedScopeName(oldName, text);
			return text;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00084694 File Offset: 0x00082894
		internal string CreateUniqueVariableName(string oldName, bool isClone)
		{
			int num = 1;
			if (isClone)
			{
				num = 2;
			}
			string text;
			do
			{
				StringBuilder stringBuilder;
				if (isClone)
				{
					stringBuilder = new StringBuilder(oldName.Length + 4);
				}
				else
				{
					stringBuilder = new StringBuilder(oldName.Length + 19);
				}
				stringBuilder.Append(oldName);
				if (isClone)
				{
					stringBuilder.Append("_");
				}
				else
				{
					stringBuilder.Append("_InAutoSubtotal");
				}
				stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture.NumberFormat));
				text = stringBuilder.ToString();
				num++;
			}
			while (!this.m_variableNameValidator.Validate(text));
			Global.Tracer.Assert(!this.m_variableNameMap.ContainsKey(oldName), "(!m_variableNameMap.ContainsKey(oldName))");
			this.m_variableNameMap.Add(oldName, text);
			return text;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00084750 File Offset: 0x00082950
		internal string GetNewVariableName(string oldVariableName)
		{
			string text;
			if (oldVariableName != null && oldVariableName.Length > 0 && this.m_variableNameMap.TryGetValue(oldVariableName, out text))
			{
				return text;
			}
			return oldVariableName;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0008477C File Offset: 0x0008297C
		internal string GetNewScopeName(string oldScopeName)
		{
			string text;
			if (oldScopeName != null && oldScopeName.Length > 0 && this.m_scopeNameMap.TryGetValue(oldScopeName, out text))
			{
				return text;
			}
			return oldScopeName;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000847A8 File Offset: 0x000829A8
		internal string GetNewScopeNameForInnerOrOuterAggregate(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo originalAggregate)
		{
			string scope = originalAggregate.PublishingInfo.Scope;
			IRIFDataScope irifdataScope;
			if (this.m_scopeNamesToClone.TryGetValue(scope, out irifdataScope))
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = irifdataScope as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion;
				if (dataRegion != null)
				{
					return this.CreateUniqueReportItemName(scope, dataRegion.IsClone);
				}
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode reportHierarchyNode = irifdataScope as Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode;
				if (reportHierarchyNode != null)
				{
					return this.CreateAndRegisterUniqueGroupName(scope, reportHierarchyNode.IsClone);
				}
				Global.Tracer.Assert(false, "Unknown object type in GetNewScopeNameForNestedAggregate: {0}", new object[] { irifdataScope });
				return scope;
			}
			else
			{
				IRIFDataScope scopeByName = this.m_scopeTree.GetScopeByName(this.m_currentScope);
				int num;
				if (scopeByName != null && this.NeedsSubtotalScopeLift(originalAggregate, scopeByName))
				{
					num = this.m_scopeTree.MeasureScopeDistance(this.m_currentScopeBeingCloned, this.m_currentScope);
				}
				else
				{
					num = -1;
				}
				if (num <= 0)
				{
					return scope;
				}
				string text = this.m_scopeTree.FindAncestorScopeName(scope, num);
				if (text == null)
				{
					return scope;
				}
				if (this.m_outerAggregate != null && !string.IsNullOrEmpty(this.m_outerAggregate.PublishingInfo.Scope))
				{
					IRIFDataScope scopeByName2 = this.m_scopeTree.GetScopeByName(this.m_outerAggregate.PublishingInfo.Scope);
					IRIFDataScope scopeByName3 = this.m_scopeTree.GetScopeByName(text);
					if (scopeByName2 != null && scopeByName3 != null && this.m_scopeTree.IsParentScope(scopeByName3, scopeByName2))
					{
						text = this.m_outerAggregate.PublishingInfo.Scope;
					}
				}
				return text;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000848F4 File Offset: 0x00082AF4
		private bool NeedsSubtotalScopeLift(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo aggregate, IRIFDataScope displayScope)
		{
			if (aggregate.PublishingInfo.HasScope)
			{
				IRIFDataScope scopeByName = this.m_scopeTree.GetScopeByName(aggregate.PublishingInfo.Scope);
				return scopeByName != null && this.m_scopeTree.IsParentScope(displayScope, scopeByName);
			}
			return true;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0008493E File Offset: 0x00082B3E
		internal void RegisterScopeName(string name)
		{
			Global.Tracer.Assert(!this.m_scopeNameMap.ContainsKey(name), "(!m_scopeNameMap.ContainsKey(name))");
			this.m_scopeNameMap.Add(name, this.m_currentScope);
			this.m_currentScopeBeingCloned = name;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00084977 File Offset: 0x00082B77
		internal void RegisterClonedScopeName(string oldName, string newName)
		{
			Global.Tracer.Assert(!this.m_scopeNameMap.ContainsKey(oldName), "(!m_scopeNameMap.ContainsKey(oldName))");
			this.m_scopeNameMap.Add(oldName, newName);
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000849A4 File Offset: 0x00082BA4
		internal string CreateAggregateID(string oldID)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report = this.m_report;
			int lastAggregateID = report.LastAggregateID;
			report.LastAggregateID = lastAggregateID + 1;
			string text = "Aggregate" + this.m_report.LastAggregateID.ToString();
			Global.Tracer.Assert(!this.m_aggregateMap.ContainsKey(oldID), "(!m_aggregateMap.ContainsKey(oldID))");
			this.m_aggregateMap.Add(oldID, text);
			return text;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x00084A10 File Offset: 0x00082C10
		internal string CreateLookupID(string oldID)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report = this.m_report;
			int lastLookupID = report.LastLookupID;
			report.LastLookupID = lastLookupID + 1;
			string text = "Lookup" + this.m_report.LastLookupID.ToString();
			Global.Tracer.Assert(!this.m_lookupMap.ContainsKey(oldID), "(!m_lookupMap.ContainsKey(oldID))");
			this.m_lookupMap.Add(oldID, text);
			return text;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00084A7C File Offset: 0x00082C7C
		internal string GetNewAggregateID(string oldID)
		{
			string text;
			if (this.m_aggregateMap.TryGetValue(oldID, out text))
			{
				return text;
			}
			return oldID;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00084A9C File Offset: 0x00082C9C
		internal string GetNewLookupID(string oldID)
		{
			string text;
			if (this.m_lookupMap.TryGetValue(oldID, out text))
			{
				return text;
			}
			return oldID;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00084ABC File Offset: 0x00082CBC
		internal int GenerateID()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report = this.m_report;
			int lastID = report.LastID;
			report.LastID = lastID + 1;
			return this.m_report.LastID;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00084AEC File Offset: 0x00082CEC
		internal void AdjustReferences()
		{
			if (this.m_expressionsWithReportItemReferences.Count > 0)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo in this.m_expressionsWithReportItemReferences)
				{
					expressionInfo.UpdateReportItemReferences(this);
				}
			}
			if (this.m_visibilitiesWithToggleToUpdate.Count > 0)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility in this.m_visibilitiesWithToggleToUpdate)
				{
					visibility.UpdateToggleItemReference(this);
				}
			}
			if (this.m_reportItemsWithRepeatWithToUpdate.Count > 0)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem in this.m_reportItemsWithRepeatWithToUpdate)
				{
					reportItem.UpdateRepeatWithReference(this);
				}
			}
			if (this.m_endUserSortWithTarget.Count > 0)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.EndUserSort endUserSort in this.m_endUserSortWithTarget)
				{
					endUserSort.UpdateSortScopeAndTargetReference(this);
				}
			}
			this.m_lookupMap.Clear();
			this.m_aggregateMap.Clear();
			this.m_reportItemNameMap.Clear();
			this.m_variableNameMap.Clear();
			this.m_visibilitiesWithToggleToUpdate.Clear();
			this.m_reportItemsWithRepeatWithToUpdate.Clear();
			this.m_expressionsWithReportItemReferences.Clear();
			this.m_endUserSortWithTarget.Clear();
			this.m_scopeNameMap.Clear();
			this.m_scopeNamesToClone.Clear();
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00084CB8 File Offset: 0x00082EB8
		internal void AddSortTarget(string scopeName, Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope target)
		{
			Global.Tracer.Assert(!this.m_reportScopes.ContainsKey(scopeName), "(!m_reportScopes.ContainsKey(scopeName))");
			this.m_reportScopes.Add(scopeName, target);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x00084CE5 File Offset: 0x00082EE5
		internal bool TryGetNewSortTarget(string scopeName, out Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope target)
		{
			target = null;
			return this.m_reportScopes.TryGetValue(scopeName, out target);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00084CF7 File Offset: 0x00082EF7
		internal void AddExpressionThatReferencesReportItems(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.m_expressionsWithReportItemReferences.Add(expression);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x00084D05 File Offset: 0x00082F05
		internal void AddVisibilityWithToggleToUpdate(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			this.m_visibilitiesWithToggleToUpdate.Add(visibility);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x00084D13 File Offset: 0x00082F13
		internal void AddReportItemWithRepeatWithToUpdate(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem)
		{
			this.m_reportItemsWithRepeatWithToUpdate.Add(reportItem);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00084D21 File Offset: 0x00082F21
		internal void AddEndUserSort(Microsoft.ReportingServices.ReportIntermediateFormat.EndUserSort endUserSort)
		{
			this.m_endUserSortWithTarget.Add(endUserSort);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00084D2F File Offset: 0x00082F2F
		internal void AddSubReport(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport)
		{
			this.m_report.SubReports.Add(subReport);
		}

		// Token: 0x040010F9 RID: 4345
		private string m_objectName;

		// Token: 0x040010FA RID: 4346
		private ObjectType m_objectType;

		// Token: 0x040010FB RID: 4347
		private LocationFlags m_location;

		// Token: 0x040010FC RID: 4348
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion m_currentDataRegion;

		// Token: 0x040010FD RID: 4349
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion m_currentDataRegionClone;

		// Token: 0x040010FE RID: 4350
		private Map m_currentMapClone;

		// Token: 0x040010FF RID: 4351
		private MapVectorLayer m_currentMapVectorLayerClone;

		// Token: 0x04001100 RID: 4352
		private string m_currentScope;

		// Token: 0x04001101 RID: 4353
		private string m_currentScopeBeingCloned;

		// Token: 0x04001102 RID: 4354
		private List<ICreateSubtotals> m_createSubtotals;

		// Token: 0x04001103 RID: 4355
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> m_domainScopeGroups;

		// Token: 0x04001104 RID: 4356
		private Holder<int> m_startIndex;

		// Token: 0x04001105 RID: 4357
		private Holder<int> m_currentIndex;

		// Token: 0x04001106 RID: 4358
		private List<CellList> m_cellLists;

		// Token: 0x04001107 RID: 4359
		private List<TablixColumn> m_tablixColumns;

		// Token: 0x04001108 RID: 4360
		private RowList m_rows;

		// Token: 0x04001109 RID: 4361
		private ScopeTree m_scopeTree;

		// Token: 0x0400110A RID: 4362
		private Dictionary<string, string> m_scopeNameMap;

		// Token: 0x0400110B RID: 4363
		private Dictionary<string, string> m_reportItemNameMap;

		// Token: 0x0400110C RID: 4364
		private Dictionary<string, string> m_aggregateMap;

		// Token: 0x0400110D RID: 4365
		private Dictionary<string, string> m_lookupMap;

		// Token: 0x0400110E RID: 4366
		private Dictionary<string, string> m_variableNameMap;

		// Token: 0x0400110F RID: 4367
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> m_expressionsWithReportItemReferences;

		// Token: 0x04001110 RID: 4368
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.Visibility> m_visibilitiesWithToggleToUpdate;

		// Token: 0x04001111 RID: 4369
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> m_reportItemsWithRepeatWithToUpdate;

		// Token: 0x04001112 RID: 4370
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.EndUserSort> m_endUserSortWithTarget;

		// Token: 0x04001113 RID: 4371
		private Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope> m_reportScopes;

		// Token: 0x04001114 RID: 4372
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection> m_reportItemCollections;

		// Token: 0x04001115 RID: 4373
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.IAggregateHolder> m_aggregateHolders;

		// Token: 0x04001116 RID: 4374
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.IRunningValueHolder> m_runningValueHolders;

		// Token: 0x04001117 RID: 4375
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo m_outerAggregate;

		// Token: 0x04001118 RID: 4376
		private Dictionary<string, IRIFDataScope> m_scopeNamesToClone;

		// Token: 0x04001119 RID: 4377
		private IRIFDataScope m_currentDataScope;

		// Token: 0x0400111A RID: 4378
		private NameValidator m_reportItemNameValidator;

		// Token: 0x0400111B RID: 4379
		private NameValidator m_scopeNameValidator;

		// Token: 0x0400111C RID: 4380
		private NameValidator m_variableNameValidator;

		// Token: 0x0400111D RID: 4381
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x0400111E RID: 4382
		private bool m_dynamicWithStaticPeerEncountered;

		// Token: 0x0400111F RID: 4383
		private int m_headerLevel;

		// Token: 0x04001120 RID: 4384
		private int m_originalColumnCount;

		// Token: 0x04001121 RID: 4385
		private int m_originalRowCount;

		// Token: 0x04001122 RID: 4386
		private bool[] m_headerLevelHasStaticArray;

		// Token: 0x04001123 RID: 4387
		private Holder<int> m_variableSequenceIdCounter;

		// Token: 0x04001124 RID: 4388
		private Holder<int> m_textboxSequenceIdCounter;
	}
}
