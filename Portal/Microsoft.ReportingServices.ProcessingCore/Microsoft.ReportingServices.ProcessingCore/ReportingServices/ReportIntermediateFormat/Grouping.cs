using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000506 RID: 1286
	[Serializable]
	public sealed class Grouping : IAggregateHolder, ISortFilterScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, IPageBreakOwner
	{
		// Token: 0x0600433D RID: 17213 RVA: 0x0011A678 File Offset: 0x00118878
		internal Grouping(ConstructionPhase phase)
			: this(-1, phase)
		{
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0011A684 File Offset: 0x00118884
		internal Grouping(int id, ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_groupExpressions = new List<ExpressionInfo>();
				this.m_aggregates = new List<DataAggregateInfo>();
				this.m_postSortAggregates = new List<DataAggregateInfo>();
				this.m_recursiveAggregates = new List<DataAggregateInfo>();
			}
			this.m_ID = id;
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x0011A6DB File Offset: 0x001188DB
		// (set) Token: 0x06004340 RID: 17216 RVA: 0x0011A6E3 File Offset: 0x001188E3
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x0011A6EC File Offset: 0x001188EC
		// (set) Token: 0x06004342 RID: 17218 RVA: 0x0011A6F4 File Offset: 0x001188F4
		internal ExpressionInfo GroupLabel
		{
			get
			{
				return this.m_groupLabel;
			}
			set
			{
				this.m_groupLabel = value;
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x0011A6FD File Offset: 0x001188FD
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x0011A705 File Offset: 0x00118905
		internal List<bool> SortDirections
		{
			get
			{
				return this.m_sortDirections;
			}
			set
			{
				this.m_sortDirections = value;
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x0011A70E File Offset: 0x0011890E
		// (set) Token: 0x06004346 RID: 17222 RVA: 0x0011A716 File Offset: 0x00118916
		internal List<ExpressionInfo> GroupExpressions
		{
			get
			{
				return this.m_groupExpressions;
			}
			set
			{
				this.m_groupExpressions = value;
			}
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x0011A71F File Offset: 0x0011891F
		// (set) Token: 0x06004348 RID: 17224 RVA: 0x0011A727 File Offset: 0x00118927
		internal string DomainScope
		{
			get
			{
				return this.m_domainScope;
			}
			set
			{
				this.m_domainScope = value;
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x0011A730 File Offset: 0x00118930
		// (set) Token: 0x0600434A RID: 17226 RVA: 0x0011A752 File Offset: 0x00118952
		internal int ScopeIDForDomainScope
		{
			get
			{
				if (this.m_scopeIDForDomainScope == -1)
				{
					return this.Owner.DataScopeInfo.ScopeID;
				}
				return this.m_scopeIDForDomainScope;
			}
			set
			{
				this.m_scopeIDForDomainScope = value;
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x0011A75B File Offset: 0x0011895B
		internal bool IsDetail
		{
			get
			{
				return this.m_groupExpressions == null || this.m_groupExpressions.Count == 0;
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x0011A775 File Offset: 0x00118975
		internal bool IsClone
		{
			get
			{
				return this.m_isClone;
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x0011A77D File Offset: 0x0011897D
		// (set) Token: 0x0600434E RID: 17230 RVA: 0x0011A785 File Offset: 0x00118985
		internal ExpressionInfo PageName
		{
			get
			{
				return this.m_pageName;
			}
			set
			{
				this.m_pageName = value;
			}
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x0011A78E File Offset: 0x0011898E
		// (set) Token: 0x06004350 RID: 17232 RVA: 0x0011A796 File Offset: 0x00118996
		internal PageBreak PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x0011A79F File Offset: 0x0011899F
		// (set) Token: 0x06004352 RID: 17234 RVA: 0x0011A7A7 File Offset: 0x001189A7
		PageBreak IPageBreakOwner.PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x0011A7B0 File Offset: 0x001189B0
		Microsoft.ReportingServices.ReportProcessing.ObjectType IPageBreakOwner.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x0011A7B4 File Offset: 0x001189B4
		string IPageBreakOwner.ObjectName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x0011A7BC File Offset: 0x001189BC
		IInstancePath IPageBreakOwner.InstancePath
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x0011A7C4 File Offset: 0x001189C4
		// (set) Token: 0x06004357 RID: 17239 RVA: 0x0011A7CC File Offset: 0x001189CC
		internal List<DataAggregateInfo> Aggregates
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

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x0011A7D5 File Offset: 0x001189D5
		// (set) Token: 0x06004359 RID: 17241 RVA: 0x0011A7DD File Offset: 0x001189DD
		internal bool GroupAndSort
		{
			get
			{
				return this.m_groupAndSort;
			}
			set
			{
				this.m_groupAndSort = value;
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x0011A7E6 File Offset: 0x001189E6
		// (set) Token: 0x0600435B RID: 17243 RVA: 0x0011A7EE File Offset: 0x001189EE
		internal List<Filter> Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x0011A7F8 File Offset: 0x001189F8
		internal bool SimpleGroupExpressions
		{
			get
			{
				if (this.m_groupExpressions != null)
				{
					for (int i = 0; i < this.m_groupExpressions.Count; i++)
					{
						ExpressionInfo expressionInfo = this.m_groupExpressions[i];
						Global.Tracer.Assert(expressionInfo != null, "(null != expression)");
						if (expressionInfo.Type != ExpressionInfo.Types.Field && expressionInfo.Type != ExpressionInfo.Types.Constant)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x0011A858 File Offset: 0x00118A58
		// (set) Token: 0x0600435E RID: 17246 RVA: 0x0011A860 File Offset: 0x00118A60
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> ReportItemsWithHideDuplicates
		{
			get
			{
				return this.m_reportItemsWithHideDuplicates;
			}
			set
			{
				this.m_reportItemsWithHideDuplicates = value;
			}
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x0011A869 File Offset: 0x00118A69
		// (set) Token: 0x06004360 RID: 17248 RVA: 0x0011A871 File Offset: 0x00118A71
		internal List<ExpressionInfo> Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x0011A87A File Offset: 0x00118A7A
		internal IndexedExprHost ParentExprHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.ParentExpressionsHost;
			}
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x0011A891 File Offset: 0x00118A91
		internal IndexedExprHost VariableValueHosts
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.VariableValueHosts;
			}
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x0011A8A8 File Offset: 0x00118AA8
		// (set) Token: 0x06004364 RID: 17252 RVA: 0x0011A8B0 File Offset: 0x00118AB0
		internal List<DataAggregateInfo> RecursiveAggregates
		{
			get
			{
				return this.m_recursiveAggregates;
			}
			set
			{
				this.m_recursiveAggregates = value;
			}
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x0011A8B9 File Offset: 0x00118AB9
		// (set) Token: 0x06004366 RID: 17254 RVA: 0x0011A8C1 File Offset: 0x00118AC1
		internal List<DataAggregateInfo> PostSortAggregates
		{
			get
			{
				return this.m_postSortAggregates;
			}
			set
			{
				this.m_postSortAggregates = value;
			}
		}

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x0011A8CA File Offset: 0x00118ACA
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x0011A8D2 File Offset: 0x00118AD2
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x0011A8DB File Offset: 0x00118ADB
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x0011A8E3 File Offset: 0x00118AE3
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x0011A8EC File Offset: 0x00118AEC
		// (set) Token: 0x0600436C RID: 17260 RVA: 0x0011A8F4 File Offset: 0x00118AF4
		internal bool SaveGroupExprValues
		{
			get
			{
				return this.m_saveGroupExprValues;
			}
			set
			{
				this.m_saveGroupExprValues = value;
			}
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x0011A8FD File Offset: 0x00118AFD
		// (set) Token: 0x0600436E RID: 17262 RVA: 0x0011A905 File Offset: 0x00118B05
		internal List<ExpressionInfo> UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x0011A90E File Offset: 0x00118B0E
		// (set) Token: 0x06004370 RID: 17264 RVA: 0x0011A916 File Offset: 0x00118B16
		internal InScopeSortFilterHashtable NonDetailSortFiltersInScope
		{
			get
			{
				return this.m_nonDetailSortFiltersInScope;
			}
			set
			{
				this.m_nonDetailSortFiltersInScope = value;
			}
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x0011A91F File Offset: 0x00118B1F
		// (set) Token: 0x06004372 RID: 17266 RVA: 0x0011A927 File Offset: 0x00118B27
		internal InScopeSortFilterHashtable DetailSortFiltersInScope
		{
			get
			{
				return this.m_detailSortFiltersInScope;
			}
			set
			{
				this.m_detailSortFiltersInScope = value;
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x0011A930 File Offset: 0x00118B30
		// (set) Token: 0x06004374 RID: 17268 RVA: 0x0011A938 File Offset: 0x00118B38
		internal List<int> HideDuplicatesReportItemIDs
		{
			get
			{
				return this.m_hideDuplicatesReportItemIDs;
			}
			set
			{
				this.m_hideDuplicatesReportItemIDs = value;
			}
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x0011A941 File Offset: 0x00118B41
		internal GroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x0011A949 File Offset: 0x00118B49
		// (set) Token: 0x06004377 RID: 17271 RVA: 0x0011A951 File Offset: 0x00118B51
		internal Hashtable ScopeNames
		{
			get
			{
				return this.m_scopeNames;
			}
			set
			{
				this.m_scopeNames = value;
			}
		}

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x0011A95A File Offset: 0x00118B5A
		// (set) Token: 0x06004379 RID: 17273 RVA: 0x0011A962 File Offset: 0x00118B62
		internal int RecursiveLevel
		{
			get
			{
				return this.m_recursiveLevel;
			}
			set
			{
				this.m_recursiveLevel = value;
			}
		}

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x0011A96B File Offset: 0x00118B6B
		// (set) Token: 0x0600437B RID: 17275 RVA: 0x0011A973 File Offset: 0x00118B73
		internal List<object> CurrentGroupExpressionValues
		{
			get
			{
				return this.m_currentGroupExprValues;
			}
			set
			{
				this.m_currentGroupExprValues = value;
			}
		}

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x0011A97C File Offset: 0x00118B7C
		// (set) Token: 0x0600437D RID: 17277 RVA: 0x0011A984 File Offset: 0x00118B84
		internal ReportHierarchyNode Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x0011A98D File Offset: 0x00118B8D
		// (set) Token: 0x0600437F RID: 17279 RVA: 0x0011A995 File Offset: 0x00118B95
		internal List<object>[] SortFilterScopeInfo
		{
			get
			{
				return this.m_sortFilterScopeInfo;
			}
			set
			{
				this.m_sortFilterScopeInfo = value;
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x0011A99E File Offset: 0x00118B9E
		// (set) Token: 0x06004381 RID: 17281 RVA: 0x0011A9A6 File Offset: 0x00118BA6
		internal int[] SortFilterScopeIndex
		{
			get
			{
				return this.m_sortFilterScopeIndex;
			}
			set
			{
				this.m_sortFilterScopeIndex = value;
			}
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x0011A9AF File Offset: 0x00118BAF
		// (set) Token: 0x06004383 RID: 17283 RVA: 0x0011A9B7 File Offset: 0x00118BB7
		internal bool[] NeedScopeInfoForSortFilterExpression
		{
			get
			{
				return this.m_needScopeInfoForSortFilterExpression;
			}
			set
			{
				this.m_needScopeInfoForSortFilterExpression = value;
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x0011A9C0 File Offset: 0x00118BC0
		// (set) Token: 0x06004385 RID: 17285 RVA: 0x0011A9C8 File Offset: 0x00118BC8
		internal bool[] IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x0011A9D1 File Offset: 0x00118BD1
		// (set) Token: 0x06004387 RID: 17287 RVA: 0x0011A9D9 File Offset: 0x00118BD9
		internal bool[] IsSortFilterExpressionScope
		{
			get
			{
				return this.m_isSortFilterExpressionScope;
			}
			set
			{
				this.m_isSortFilterExpressionScope = value;
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x0011A9E2 File Offset: 0x00118BE2
		// (set) Token: 0x06004389 RID: 17289 RVA: 0x0011A9EA File Offset: 0x00118BEA
		internal bool[] SortFilterScopeMatched
		{
			get
			{
				return this.m_sortFilterScopeMatched;
			}
			set
			{
				this.m_sortFilterScopeMatched = value;
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x0011A9F3 File Offset: 0x00118BF3
		int Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable.ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x0011A9FB File Offset: 0x00118BFB
		int ISortFilterScope.ID
		{
			get
			{
				Global.Tracer.Assert(this.m_owner != null);
				return this.m_owner.ID;
			}
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x0011AA1B File Offset: 0x00118C1B
		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x0011AA23 File Offset: 0x00118C23
		// (set) Token: 0x0600438E RID: 17294 RVA: 0x0011AA2B File Offset: 0x00118C2B
		bool[] ISortFilterScope.IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x0011AA34 File Offset: 0x00118C34
		// (set) Token: 0x06004390 RID: 17296 RVA: 0x0011AA3C File Offset: 0x00118C3C
		bool[] ISortFilterScope.IsSortFilterExpressionScope
		{
			get
			{
				return this.m_isSortFilterExpressionScope;
			}
			set
			{
				this.m_isSortFilterExpressionScope = value;
			}
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06004391 RID: 17297 RVA: 0x0011AA45 File Offset: 0x00118C45
		// (set) Token: 0x06004392 RID: 17298 RVA: 0x0011AA4D File Offset: 0x00118C4D
		List<ExpressionInfo> ISortFilterScope.UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x06004393 RID: 17299 RVA: 0x0011AA56 File Offset: 0x00118C56
		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x0011AA6D File Offset: 0x00118C6D
		// (set) Token: 0x06004395 RID: 17301 RVA: 0x0011AA75 File Offset: 0x00118C75
		internal List<Variable> Variables
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

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x0011AA7E File Offset: 0x00118C7E
		// (set) Token: 0x06004397 RID: 17303 RVA: 0x0011AA86 File Offset: 0x00118C86
		internal bool NaturalGroup
		{
			get
			{
				return this.m_naturalGroup;
			}
			set
			{
				this.m_naturalGroup = value;
			}
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x0011AA90 File Offset: 0x00118C90
		internal bool IsAtomic(InitializationContext context)
		{
			return context.EvaluateAtomicityCondition(!this.m_naturalGroup && !this.IsDetail, this.m_owner, AtomicityReason.NonNaturalGroup) || context.EvaluateAtomicityCondition(this.m_domainScope != null, this.m_owner, AtomicityReason.DomainScope) || context.EvaluateAtomicityCondition(this.m_parent != null, this.m_owner, AtomicityReason.RecursiveParent) || context.EvaluateAtomicityCondition(this.HasAggregatesForAtomicityCheck(), this.m_owner, AtomicityReason.Aggregates);
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x0011AB09 File Offset: 0x00118D09
		private bool HasAggregatesForAtomicityCheck()
		{
			return DataScopeInfo.HasNonServerAggregates<DataAggregateInfo>(this.m_aggregates) || DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_postSortAggregates) || DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_recursiveAggregates);
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x0600439A RID: 17306 RVA: 0x0011AB32 File Offset: 0x00118D32
		// (set) Token: 0x0600439B RID: 17307 RVA: 0x0011AB3A File Offset: 0x00118D3A
		internal ScopeIDDefinition ScopeIDDefinition
		{
			get
			{
				return this.m_scopeIdDefinition;
			}
			set
			{
				this.m_scopeIdDefinition = value;
			}
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x0011AB43 File Offset: 0x00118D43
		// (set) Token: 0x0600439D RID: 17309 RVA: 0x0011AB4B File Offset: 0x00118D4B
		internal IList<object> StartPositions
		{
			get
			{
				return this.m_startPositions;
			}
			set
			{
				this.m_startPositions = value;
			}
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x0011AB54 File Offset: 0x00118D54
		public void ResetAggregates(AggregatesImpl reportOmAggregates)
		{
			reportOmAggregates.ResetAll<DataAggregateInfo>(this.m_aggregates);
			reportOmAggregates.ResetAll<DataAggregateInfo>(this.m_postSortAggregates);
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x0011AB70 File Offset: 0x00118D70
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.GroupStart(this.m_name);
			this.m_saveGroupExprValues = context.HasPreviousAggregates;
			this.DataRendererInitialize(context);
			if (this.m_groupExpressions != null)
			{
				for (int i = 0; i < this.m_groupExpressions.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_groupExpressions[i];
					expressionInfo.GroupExpressionInitialize(context);
					context.ExprHostBuilder.GroupExpression(expressionInfo);
				}
			}
			if (this.m_groupLabel != null)
			{
				this.m_groupLabel.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_groupLabel);
			}
			if (this.m_filters != null)
			{
				for (int j = 0; j < this.m_filters.Count; j++)
				{
					this.m_filters[j].Initialize(context);
				}
			}
			if (this.m_parent != null)
			{
				context.ExprHostBuilder.GroupParentExpressionsStart();
				for (int k = 0; k < this.m_parent.Count; k++)
				{
					ExpressionInfo expressionInfo2 = this.m_parent[k];
					expressionInfo2.GroupExpressionInitialize(context);
					context.ExprHostBuilder.GroupParentExpression(expressionInfo2);
				}
				context.ExprHostBuilder.GroupParentExpressionsEnd();
			}
			if (this.m_userSortExpressions != null)
			{
				context.ExprHostBuilder.UserSortExpressionsStart();
				for (int l = 0; l < this.m_userSortExpressions.Count; l++)
				{
					ExpressionInfo expressionInfo3 = this.m_userSortExpressions[l];
					context.ExprHostBuilder.UserSortExpression(expressionInfo3);
				}
				context.ExprHostBuilder.UserSortExpressionsEnd();
			}
			if (this.m_variables != null && this.m_variables.Count != 0)
			{
				context.ExprHostBuilder.VariableValuesStart();
				for (int m = 0; m < this.m_variables.Count; m++)
				{
					Variable variable = this.m_variables[m];
					variable.Initialize(context);
					context.ExprHostBuilder.VariableValueExpression(variable.Value);
				}
				context.ExprHostBuilder.VariableValuesEnd();
			}
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.Initialize(context);
			}
			if (this.m_pageName != null)
			{
				this.m_pageName.Initialize("PageName", context);
				context.ExprHostBuilder.PageName(this.m_pageName);
			}
			if (this.m_scopeIdDefinition != null)
			{
				this.m_scopeIdDefinition.Initialize(this.m_name, context);
			}
			context.ExprHostBuilder.GroupEnd();
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x0011ADC4 File Offset: 0x00118FC4
		List<DataAggregateInfo> IAggregateHolder.GetAggregateList()
		{
			return this.m_aggregates;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x0011ADCC File Offset: 0x00118FCC
		List<DataAggregateInfo> IAggregateHolder.GetPostSortAggregateList()
		{
			return this.m_postSortAggregates;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x0011ADD4 File Offset: 0x00118FD4
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_aggregates != null, "(null != m_aggregates)");
			if (this.m_aggregates.Count == 0)
			{
				this.m_aggregates = null;
			}
			Global.Tracer.Assert(this.m_postSortAggregates != null, "(null != m_postSortAggregates)");
			if (this.m_postSortAggregates.Count == 0)
			{
				this.m_postSortAggregates = null;
			}
			Global.Tracer.Assert(this.m_recursiveAggregates != null, "(null != m_recursiveAggregates)");
			if (this.m_recursiveAggregates.Count == 0)
			{
				this.m_recursiveAggregates = null;
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x0011AE65 File Offset: 0x00119065
		DataScopeInfo IAggregateHolder.DataScopeInfo
		{
			get
			{
				return this.m_owner.DataScopeInfo;
			}
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x0011AE74 File Offset: 0x00119074
		private void DataRendererInitialize(InitializationContext context)
		{
			if (this.m_dataElementOutput == DataElementOutputTypes.Auto || this.m_dataElementOutput == DataElementOutputTypes.ContentsOnly)
			{
				this.m_dataElementOutput = DataElementOutputTypes.Output;
			}
			Microsoft.ReportingServices.ReportPublishing.CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, this.m_name, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x0011AEC6 File Offset: 0x001190C6
		internal void AddReportItemWithHideDuplicates(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem)
		{
			if (this.m_reportItemsWithHideDuplicates == null)
			{
				this.m_reportItemsWithHideDuplicates = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>();
			}
			this.m_reportItemsWithHideDuplicates.Add(reportItem);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x0011AEE8 File Offset: 0x001190E8
		internal void ResetReportItemsWithHideDuplicates()
		{
			if (this.m_reportItemsWithHideDuplicates != null)
			{
				int count = this.m_reportItemsWithHideDuplicates.Count;
				for (int i = 0; i < count; i++)
				{
					(this.m_reportItemsWithHideDuplicates[i] as Microsoft.ReportingServices.ReportIntermediateFormat.TextBox).ResetDuplicates();
				}
			}
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x0011AF2B File Offset: 0x0011912B
		internal bool IsOnPathToSortFilterSource(int index)
		{
			return this.m_sortFilterScopeInfo != null && this.m_sortFilterScopeIndex != null && -1 != this.m_sortFilterScopeIndex[index];
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x0011AF4C File Offset: 0x0011914C
		internal int[] GetGroupExpressionFieldIndices()
		{
			if (this.m_groupExpressionFieldIndices == null && this.m_groupExpressions != null && 0 < this.m_groupExpressions.Count)
			{
				Global.Tracer.Assert(this.m_groupExpressions != null && 0 < this.m_groupExpressions.Count, "(null != m_groupExpressions && 0 < m_groupExpressions.Count)");
				this.m_groupExpressionFieldIndices = new int[this.m_groupExpressions.Count];
				for (int i = 0; i < this.m_groupExpressions.Count; i++)
				{
					this.m_groupExpressionFieldIndices[i] = -2;
					ExpressionInfo expressionInfo = this.m_groupExpressions[i];
					if (expressionInfo.Type == ExpressionInfo.Types.Field)
					{
						this.m_groupExpressionFieldIndices[i] = expressionInfo.IntValue;
					}
					else if (expressionInfo.Type == ExpressionInfo.Types.Constant)
					{
						this.m_groupExpressionFieldIndices[i] = -1;
					}
				}
			}
			return this.m_groupExpressionFieldIndices;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x0011B01C File Offset: 0x0011921C
		internal Grouping CloneForDomainScope(AutomaticSubtotalContext context, ReportHierarchyNode cloneOwner)
		{
			Grouping grouping = new Grouping(ConstructionPhase.Publishing);
			grouping.m_isClone = true;
			grouping.m_ID = context.GenerateID();
			grouping.m_owner = cloneOwner;
			cloneOwner.OriginalScopeID = this.Owner.DataScopeInfo.ScopeID;
			Global.Tracer.Assert(this.m_name != null, "Group Name cannot be null");
			grouping.m_name = context.CreateAndRegisterUniqueGroupName(this.m_name, this.m_isClone, true);
			this.CloneGroupExpressions(context, grouping);
			return grouping;
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x0011B09C File Offset: 0x0011929C
		internal object PublishClone(AutomaticSubtotalContext context, ReportHierarchyNode owner)
		{
			Grouping grouping = (Grouping)base.MemberwiseClone();
			grouping.m_isClone = true;
			grouping.m_ID = context.GenerateID();
			grouping.m_owner = owner;
			if (this.DomainScope != null)
			{
				grouping.DomainScope = context.GetNewScopeName(this.DomainScope);
				if (string.CompareOrdinal(this.DomainScope, grouping.DomainScope) != 0)
				{
					context.DomainScopeGroups.Add(grouping);
				}
				else
				{
					grouping.m_scopeIDForDomainScope = this.Owner.DataScopeInfo.ScopeID;
				}
			}
			context.AddAggregateHolder(grouping);
			Global.Tracer.Assert(this.m_name != null);
			grouping.m_name = context.CreateAndRegisterUniqueGroupName(this.m_name, this.m_isClone);
			context.AddSortTarget(grouping.m_name, grouping);
			this.CloneGroupExpressions(context, grouping);
			if (this.m_groupLabel != null)
			{
				grouping.m_groupLabel = (ExpressionInfo)this.m_groupLabel.PublishClone(context);
			}
			if (this.m_sortDirections != null)
			{
				grouping.m_sortDirections = new List<bool>(this.m_sortDirections.Count);
				foreach (bool flag in this.m_sortDirections)
				{
					grouping.m_sortDirections.Add(flag);
				}
			}
			grouping.m_aggregates = new List<DataAggregateInfo>();
			grouping.m_recursiveAggregates = new List<DataAggregateInfo>();
			grouping.m_postSortAggregates = new List<DataAggregateInfo>();
			if (this.m_filters != null)
			{
				grouping.m_filters = new List<Filter>(this.m_filters.Count);
				foreach (Filter filter in this.m_filters)
				{
					grouping.m_filters.Add((Filter)filter.PublishClone(context));
				}
			}
			if (this.m_parent != null)
			{
				grouping.m_parent = new List<ExpressionInfo>(this.m_parent.Count);
				foreach (ExpressionInfo expressionInfo in this.m_parent)
				{
					grouping.m_parent.Add((ExpressionInfo)expressionInfo.PublishClone(context));
				}
			}
			if (this.m_dataElementName != null)
			{
				grouping.m_dataElementName = (string)this.m_dataElementName.Clone();
			}
			if (this.m_userSortExpressions != null)
			{
				grouping.m_userSortExpressions = new List<ExpressionInfo>(this.m_userSortExpressions.Count);
				foreach (ExpressionInfo expressionInfo2 in this.m_userSortExpressions)
				{
					grouping.m_userSortExpressions.Add((ExpressionInfo)expressionInfo2.PublishClone(context));
				}
			}
			if (this.m_variables != null)
			{
				grouping.m_variables = new List<Variable>(this.m_variables.Count);
				foreach (Variable variable in this.m_variables)
				{
					grouping.m_variables.Add((Variable)variable.PublishClone(context));
				}
			}
			if (this.m_nonDetailSortFiltersInScope != null)
			{
				grouping.m_nonDetailSortFiltersInScope = new InScopeSortFilterHashtable(this.m_nonDetailSortFiltersInScope.Count);
				foreach (object obj in this.m_nonDetailSortFiltersInScope)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					List<int> list = (List<int>)dictionaryEntry.Value;
					List<int> list2 = new List<int>(list.Count);
					foreach (int num in list)
					{
						list2.Add(num);
					}
					grouping.m_nonDetailSortFiltersInScope.Add(dictionaryEntry.Key, list2);
				}
			}
			if (this.m_detailSortFiltersInScope != null)
			{
				grouping.m_detailSortFiltersInScope = new InScopeSortFilterHashtable(this.m_detailSortFiltersInScope.Count);
				foreach (object obj2 in this.m_detailSortFiltersInScope)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					List<int> list3 = (List<int>)dictionaryEntry2.Value;
					List<int> list4 = new List<int>(list3.Count);
					foreach (int num2 in list3)
					{
						list4.Add(num2);
					}
					grouping.m_detailSortFiltersInScope.Add(dictionaryEntry2.Key, list4);
				}
			}
			if (this.m_pageBreak != null)
			{
				grouping.m_pageBreak = (PageBreak)this.m_pageBreak.PublishClone(context);
			}
			if (this.m_pageName != null)
			{
				grouping.m_pageName = (ExpressionInfo)this.m_pageName.PublishClone(context);
			}
			return grouping;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x0011B5EC File Offset: 0x001197EC
		private void CloneGroupExpressions(AutomaticSubtotalContext context, Grouping clone)
		{
			if (this.m_groupExpressions != null)
			{
				clone.m_groupExpressions = new List<ExpressionInfo>(this.m_groupExpressions.Count);
				foreach (ExpressionInfo expressionInfo in this.m_groupExpressions)
				{
					clone.m_groupExpressions.Add((ExpressionInfo)expressionInfo.PublishClone(context));
				}
			}
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x0011B670 File Offset: 0x00119870
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.ID, Token.Int32),
				new MemberInfo(MemberName.GroupExpressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GroupLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SortDirections, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Boolean),
				new ReadOnlyMemberInfo(MemberName.PageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.GroupAndSort, Token.Boolean),
				new MemberInfo(MemberName.Filters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filter),
				new MemberInfo(MemberName.ReportItemsWithHideDuplicates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem),
				new MemberInfo(MemberName.Parent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RecursiveAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.SaveGroupExprValues, Token.Boolean),
				new MemberInfo(MemberName.UserSortExpressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.NonDetailSortFiltersInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32PrimitiveListHashtable),
				new MemberInfo(MemberName.DetailSortFiltersInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32PrimitiveListHashtable),
				new MemberInfo(MemberName.Variables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Variable),
				new MemberInfo(MemberName.PageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageBreak),
				new MemberInfo(MemberName.PageName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DomainScope, Token.String),
				new MemberInfo(MemberName.ScopeIDForDomainScope, Token.Int32),
				new MemberInfo(MemberName.NaturalGroup, Token.Boolean)
			});
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x0011B890 File Offset: 0x00119A90
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Grouping.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementOutput)
				{
					if (memberName <= MemberName.GroupAndSort)
					{
						if (memberName <= MemberName.Variables)
						{
							if (memberName == MemberName.ID)
							{
								writer.Write(this.m_ID);
								continue;
							}
							if (memberName == MemberName.Variables)
							{
								writer.Write<Variable>(this.m_variables);
								continue;
							}
						}
						else
						{
							if (memberName == MemberName.Name)
							{
								writer.Write(this.m_name);
								continue;
							}
							switch (memberName)
							{
							case MemberName.GroupExpressions:
								writer.Write<ExpressionInfo>(this.m_groupExpressions);
								continue;
							case MemberName.GroupLabel:
								writer.Write(this.m_groupLabel);
								continue;
							case MemberName.SortDirections:
								writer.WriteListOfPrimitives<bool>(this.m_sortDirections);
								continue;
							case MemberName.Aggregates:
								writer.Write<DataAggregateInfo>(this.m_aggregates);
								continue;
							case MemberName.GroupAndSort:
								writer.Write(this.m_groupAndSort);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.ReportItemsWithHideDuplicates)
					{
						if (memberName == MemberName.Filters)
						{
							writer.Write<Filter>(this.m_filters);
							continue;
						}
						if (memberName == MemberName.ReportItemsWithHideDuplicates)
						{
							writer.WriteListOfReferences(this.m_reportItemsWithHideDuplicates);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Parent:
							writer.Write<ExpressionInfo>(this.m_parent);
							continue;
						case MemberName.PostSortAggregates:
							writer.Write<DataAggregateInfo>(this.m_postSortAggregates);
							continue;
						case MemberName.RecursiveAggregates:
							writer.Write<DataAggregateInfo>(this.m_recursiveAggregates);
							continue;
						default:
							if (memberName == MemberName.DataElementName)
							{
								writer.Write(this.m_dataElementName);
								continue;
							}
							if (memberName == MemberName.DataElementOutput)
							{
								writer.WriteEnum((int)this.m_dataElementOutput);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.DetailSortFiltersInScope)
				{
					if (memberName <= MemberName.UserSortExpressions)
					{
						if (memberName == MemberName.SaveGroupExprValues)
						{
							writer.Write(this.m_saveGroupExprValues);
							continue;
						}
						if (memberName == MemberName.UserSortExpressions)
						{
							writer.Write<ExpressionInfo>(this.m_userSortExpressions);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.NonDetailSortFiltersInScope)
						{
							writer.WriteInt32PrimitiveListHashtable<int>(this.m_nonDetailSortFiltersInScope);
							continue;
						}
						if (memberName == MemberName.DetailSortFiltersInScope)
						{
							writer.WriteInt32PrimitiveListHashtable<int>(this.m_detailSortFiltersInScope);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.PageName)
				{
					if (memberName == MemberName.PageBreak)
					{
						writer.Write(this.m_pageBreak);
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						writer.Write(this.m_pageName);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DomainScope)
					{
						writer.Write(this.m_domainScope);
						continue;
					}
					if (memberName == MemberName.ScopeIDForDomainScope)
					{
						writer.Write(this.m_scopeIDForDomainScope);
						continue;
					}
					if (memberName == MemberName.NaturalGroup)
					{
						writer.Write(this.m_naturalGroup);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x0011BBA8 File Offset: 0x00119DA8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Grouping.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementName)
				{
					if (memberName <= MemberName.GroupAndSort)
					{
						if (memberName <= MemberName.Variables)
						{
							if (memberName == MemberName.ID)
							{
								this.m_ID = reader.ReadInt32();
								continue;
							}
							if (memberName == MemberName.Variables)
							{
								this.m_variables = reader.ReadGenericListOfRIFObjects<Variable>();
								continue;
							}
						}
						else
						{
							if (memberName == MemberName.Name)
							{
								this.m_name = reader.ReadString();
								continue;
							}
							switch (memberName)
							{
							case MemberName.GroupExpressions:
								this.m_groupExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
								continue;
							case MemberName.GroupLabel:
								this.m_groupLabel = (ExpressionInfo)reader.ReadRIFObject();
								continue;
							case MemberName.SortDirections:
								this.m_sortDirections = reader.ReadListOfPrimitives<bool>();
								continue;
							case MemberName.Aggregates:
								this.m_aggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
								continue;
							case MemberName.GroupAndSort:
								this.m_groupAndSort = reader.ReadBoolean();
								continue;
							}
						}
					}
					else if (memberName <= MemberName.ReportItemsWithHideDuplicates)
					{
						if (memberName == MemberName.Filters)
						{
							this.m_filters = reader.ReadGenericListOfRIFObjects<Filter>();
							continue;
						}
						if (memberName == MemberName.ReportItemsWithHideDuplicates)
						{
							this.m_reportItemsWithHideDuplicates = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>(this);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Parent:
							this.m_parent = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
							continue;
						case MemberName.PostSortAggregates:
							this.m_postSortAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
							continue;
						case MemberName.RecursiveAggregates:
							this.m_recursiveAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
							continue;
						default:
							if (memberName == MemberName.DataElementName)
							{
								this.m_dataElementName = reader.ReadString();
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.PageBreakLocation)
				{
					if (memberName <= MemberName.SaveGroupExprValues)
					{
						if (memberName == MemberName.DataElementOutput)
						{
							this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
							continue;
						}
						if (memberName == MemberName.SaveGroupExprValues)
						{
							this.m_saveGroupExprValues = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.UserSortExpressions)
						{
							this.m_userSortExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
							continue;
						}
						switch (memberName)
						{
						case MemberName.NonDetailSortFiltersInScope:
							this.m_nonDetailSortFiltersInScope = reader.ReadInt32PrimitiveListHashtable<InScopeSortFilterHashtable, int>();
							continue;
						case MemberName.DetailSortFiltersInScope:
							this.m_detailSortFiltersInScope = reader.ReadInt32PrimitiveListHashtable<InScopeSortFilterHashtable, int>();
							continue;
						case MemberName.PageBreakLocation:
							this.m_pageBreak = new PageBreak();
							this.m_pageBreak.BreakLocation = (PageBreakLocation)reader.ReadEnum();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.PageName)
				{
					if (memberName == MemberName.PageBreak)
					{
						this.m_pageBreak = (PageBreak)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						this.m_pageName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DomainScope)
					{
						this.m_domainScope = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.ScopeIDForDomainScope)
					{
						this.m_scopeIDForDomainScope = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.NaturalGroup)
					{
						this.m_naturalGroup = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x0011BEFC File Offset: 0x0011A0FC
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Grouping.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.ReportItemsWithHideDuplicates)
					{
						if (this.m_reportItemsWithHideDuplicates == null)
						{
							this.m_reportItemsWithHideDuplicates = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>();
						}
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem);
						Global.Tracer.Assert(!this.m_reportItemsWithHideDuplicates.Contains((Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)referenceableItems[memberReference.RefID]));
						this.m_reportItemsWithHideDuplicates.Add((Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)referenceableItems[memberReference.RefID]);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x0011C00C File Offset: 0x0011A20C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x0011C014 File Offset: 0x0011A214
		internal void SetExprHost(GroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_exprHost.FilterHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_filters != null, "(m_filters != null)");
				int count = this.m_filters.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_filters[i].SetExprHost(this.m_exprHost.FilterHostsRemotable, reportObjectModel);
				}
			}
			if (this.m_exprHost.ParentExpressionsHost != null)
			{
				this.m_exprHost.ParentExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_exprHost.VariableValueHosts != null)
			{
				this.m_exprHost.VariableValueHosts.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_exprHost.UserSortExpressionsHost != null)
			{
				this.m_exprHost.UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_pageBreak != null && this.m_exprHost.PageBreakExprHost != null)
			{
				this.m_pageBreak.SetExprHost(this.m_exprHost.PageBreakExprHost, reportObjectModel);
			}
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x0011C12D File Offset: 0x0011A32D
		internal string EvaluateGroupingLabelExpression(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_owner, romInstance);
			return context.ReportRuntime.EvaluateGroupingLabelExpression(this, Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix, this.m_name);
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x0011C150 File Offset: 0x0011A350
		internal int GetRecursiveLevel(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_owner, romInstance);
			return this.m_recursiveLevel;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x0011C165 File Offset: 0x0011A365
		internal void SetGroupInstanceExpressionValues(object[] exprValues)
		{
			this.m_groupInstanceExprValues = exprValues;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0011C16E File Offset: 0x0011A36E
		internal object[] GetGroupInstanceExpressionValues(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_owner, romInstance);
			return this.m_groupInstanceExprValues;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x0011C183 File Offset: 0x0011A383
		internal string EvaluatePageName(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_owner, romInstance);
			return context.ReportRuntime.EvaluateGroupingPageNameExpression(this, this.m_pageName, this.m_name);
		}

		// Token: 0x04001EA9 RID: 7849
		private string m_name;

		// Token: 0x04001EAA RID: 7850
		private int m_ID = -1;

		// Token: 0x04001EAB RID: 7851
		private List<ExpressionInfo> m_groupExpressions;

		// Token: 0x04001EAC RID: 7852
		private ExpressionInfo m_groupLabel;

		// Token: 0x04001EAD RID: 7853
		private List<bool> m_sortDirections;

		// Token: 0x04001EAE RID: 7854
		private PageBreak m_pageBreak;

		// Token: 0x04001EAF RID: 7855
		private ExpressionInfo m_pageName;

		// Token: 0x04001EB0 RID: 7856
		private List<DataAggregateInfo> m_aggregates;

		// Token: 0x04001EB1 RID: 7857
		private bool m_groupAndSort;

		// Token: 0x04001EB2 RID: 7858
		private List<Filter> m_filters;

		// Token: 0x04001EB3 RID: 7859
		[Reference]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> m_reportItemsWithHideDuplicates;

		// Token: 0x04001EB4 RID: 7860
		private List<ExpressionInfo> m_parent;

		// Token: 0x04001EB5 RID: 7861
		private List<DataAggregateInfo> m_recursiveAggregates;

		// Token: 0x04001EB6 RID: 7862
		private List<DataAggregateInfo> m_postSortAggregates;

		// Token: 0x04001EB7 RID: 7863
		private string m_dataElementName;

		// Token: 0x04001EB8 RID: 7864
		private DataElementOutputTypes m_dataElementOutput;

		// Token: 0x04001EB9 RID: 7865
		private bool m_saveGroupExprValues;

		// Token: 0x04001EBA RID: 7866
		private List<ExpressionInfo> m_userSortExpressions;

		// Token: 0x04001EBB RID: 7867
		private InScopeSortFilterHashtable m_nonDetailSortFiltersInScope;

		// Token: 0x04001EBC RID: 7868
		private InScopeSortFilterHashtable m_detailSortFiltersInScope;

		// Token: 0x04001EBD RID: 7869
		private List<Variable> m_variables;

		// Token: 0x04001EBE RID: 7870
		private string m_domainScope;

		// Token: 0x04001EBF RID: 7871
		private int m_scopeIDForDomainScope = -1;

		// Token: 0x04001EC0 RID: 7872
		private bool m_naturalGroup;

		// Token: 0x04001EC1 RID: 7873
		[NonSerialized]
		private List<int> m_hideDuplicatesReportItemIDs;

		// Token: 0x04001EC2 RID: 7874
		[NonSerialized]
		private GroupExprHost m_exprHost;

		// Token: 0x04001EC3 RID: 7875
		[NonSerialized]
		private Hashtable m_scopeNames;

		// Token: 0x04001EC4 RID: 7876
		[NonSerialized]
		private int m_recursiveLevel;

		// Token: 0x04001EC5 RID: 7877
		[NonSerialized]
		private int[] m_groupExpressionFieldIndices;

		// Token: 0x04001EC6 RID: 7878
		[NonSerialized]
		private bool m_isClone;

		// Token: 0x04001EC7 RID: 7879
		[NonSerialized]
		private List<object> m_currentGroupExprValues;

		// Token: 0x04001EC8 RID: 7880
		[NonSerialized]
		private object[] m_groupInstanceExprValues;

		// Token: 0x04001EC9 RID: 7881
		[NonSerialized]
		private ReportHierarchyNode m_owner;

		// Token: 0x04001ECA RID: 7882
		[NonSerialized]
		private List<object>[] m_sortFilterScopeInfo;

		// Token: 0x04001ECB RID: 7883
		[NonSerialized]
		private int[] m_sortFilterScopeIndex;

		// Token: 0x04001ECC RID: 7884
		[NonSerialized]
		private bool[] m_needScopeInfoForSortFilterExpression;

		// Token: 0x04001ECD RID: 7885
		[NonSerialized]
		private bool[] m_sortFilterScopeMatched;

		// Token: 0x04001ECE RID: 7886
		[NonSerialized]
		private bool[] m_isSortFilterTarget;

		// Token: 0x04001ECF RID: 7887
		[NonSerialized]
		private bool[] m_isSortFilterExpressionScope;

		// Token: 0x04001ED0 RID: 7888
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Grouping.GetDeclaration();

		// Token: 0x04001ED1 RID: 7889
		[NonSerialized]
		private ScopeIDDefinition m_scopeIdDefinition;

		// Token: 0x04001ED2 RID: 7890
		[NonSerialized]
		private IList<object> m_startPositions;
	}
}
