using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200050B RID: 1291
	[Serializable]
	public abstract class ReportHierarchyNode : IDOwner, IRunningValueHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ICustomPropertiesHolder, IIndexedInCollection, IRIFReportScope, IInstancePath, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner, IStaticReferenceable, IRIFDataScope, IRIFReportDataScope
	{
		// Token: 0x06004403 RID: 17411 RVA: 0x0011D0A4 File Offset: 0x0011B2A4
		internal ReportHierarchyNode()
		{
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x0011D104 File Offset: 0x0011B304
		internal ReportHierarchyNode(int id, DataRegion dataRegionDef)
			: base(id)
		{
			this.m_dataRegionDef = dataRegionDef;
			this.m_runningValues = new List<RunningValueInfo>();
			this.m_dataScopeInfo = new DataScopeInfo(id);
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x0011D180 File Offset: 0x0011B380
		string IRIFDataScope.Name
		{
			get
			{
				if (this.m_grouping != null)
				{
					return this.m_grouping.Name;
				}
				return null;
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x0011D197 File Offset: 0x0011B397
		Microsoft.ReportingServices.ReportProcessing.ObjectType IRIFDataScope.DataScopeObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping;
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x0011D19B File Offset: 0x0011B39B
		public DataScopeInfo DataScopeInfo
		{
			get
			{
				return this.m_dataScopeInfo;
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x06004408 RID: 17416
		internal abstract string RdlElementName { get; }

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x06004409 RID: 17417
		internal abstract HierarchyNodeList InnerHierarchy { get; }

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0011D1A3 File Offset: 0x0011B3A3
		// (set) Token: 0x0600440B RID: 17419 RVA: 0x0011D1AB File Offset: 0x0011B3AB
		internal bool IsColumn
		{
			get
			{
				return this.m_isColumn;
			}
			set
			{
				this.m_isColumn = value;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0011D1B4 File Offset: 0x0011B3B4
		internal bool IsDomainScope
		{
			get
			{
				return this.m_originalScopeID != -1;
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x0011D1C2 File Offset: 0x0011B3C2
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x0011D1CA File Offset: 0x0011B3CA
		internal int OriginalScopeID
		{
			get
			{
				return this.m_originalScopeID;
			}
			set
			{
				this.m_originalScopeID = value;
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x0011D1D3 File Offset: 0x0011B3D3
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x0011D1DB File Offset: 0x0011B3DB
		internal int Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x0011D1E4 File Offset: 0x0011B3E4
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x0011D1EC File Offset: 0x0011B3EC
		internal Grouping Grouping
		{
			get
			{
				return this.m_grouping;
			}
			set
			{
				this.m_grouping = value;
				if (this.m_grouping != null)
				{
					this.m_grouping.Owner = this;
				}
			}
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x0011D209 File Offset: 0x0011B409
		// (set) Token: 0x06004414 RID: 17428 RVA: 0x0011D211 File Offset: 0x0011B411
		internal Sorting Sorting
		{
			get
			{
				return this.m_sorting;
			}
			set
			{
				this.m_sorting = value;
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x0011D21A File Offset: 0x0011B41A
		internal List<ScopeIDType> MemberGroupAndSortExpressionFlag
		{
			get
			{
				return this.m_memberGroupAndSortExpressionFlag;
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x0011D222 File Offset: 0x0011B422
		// (set) Token: 0x06004417 RID: 17431 RVA: 0x0011D22A File Offset: 0x0011B42A
		internal int MemberCellIndex
		{
			get
			{
				return this.m_memberCellIndex;
			}
			set
			{
				this.m_memberCellIndex = value;
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x0011D233 File Offset: 0x0011B433
		// (set) Token: 0x06004419 RID: 17433 RVA: 0x0011D23B File Offset: 0x0011B43B
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x0011D244 File Offset: 0x0011B444
		// (set) Token: 0x0600441B RID: 17435 RVA: 0x0011D24C File Offset: 0x0011B44C
		internal int RowSpan
		{
			get
			{
				return this.m_rowSpan;
			}
			set
			{
				this.m_rowSpan = value;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x0011D255 File Offset: 0x0011B455
		// (set) Token: 0x0600441D RID: 17437 RVA: 0x0011D25D File Offset: 0x0011B45D
		internal int ColSpan
		{
			get
			{
				return this.m_colSpan;
			}
			set
			{
				this.m_colSpan = value;
			}
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x0011D266 File Offset: 0x0011B466
		// (set) Token: 0x0600441F RID: 17439 RVA: 0x0011D26E File Offset: 0x0011B46E
		internal bool IsAutoSubtotal
		{
			get
			{
				return this.m_isAutoSubtotal;
			}
			set
			{
				this.m_isAutoSubtotal = value;
			}
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x0011D277 File Offset: 0x0011B477
		DataValueList ICustomPropertiesHolder.CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x0011D27F File Offset: 0x0011B47F
		IInstancePath ICustomPropertiesHolder.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x0011D282 File Offset: 0x0011B482
		// (set) Token: 0x06004423 RID: 17443 RVA: 0x0011D28A File Offset: 0x0011B48A
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06004424 RID: 17444 RVA: 0x0011D293 File Offset: 0x0011B493
		// (set) Token: 0x06004425 RID: 17445 RVA: 0x0011D29B File Offset: 0x0011B49B
		internal DataRegion DataRegionDef
		{
			get
			{
				return this.m_dataRegionDef;
			}
			set
			{
				this.m_dataRegionDef = value;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x0011D2A4 File Offset: 0x0011B4A4
		// (set) Token: 0x06004427 RID: 17447 RVA: 0x0011D2AC File Offset: 0x0011B4AC
		internal List<RunningValueInfo> RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x0011D2B5 File Offset: 0x0011B4B5
		internal bool IsStatic
		{
			get
			{
				return this.m_grouping == null;
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06004429 RID: 17449 RVA: 0x0011D2C0 File Offset: 0x0011B4C0
		internal bool IsLeaf
		{
			get
			{
				return this.InnerHierarchy == null || this.InnerHierarchy.Count == 0;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x0011D2DA File Offset: 0x0011B4DA
		internal bool IsInnermostDynamicMember
		{
			get
			{
				return this.InnerHierarchy == null || this.InnerHierarchy.DynamicMembersAtScope.Count == 0;
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x0011D2F9 File Offset: 0x0011B4F9
		internal virtual bool IsTablixMember
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x0011D2FC File Offset: 0x0011B4FC
		// (set) Token: 0x0600442D RID: 17453 RVA: 0x0011D304 File Offset: 0x0011B504
		internal int CurrentMemberIndex
		{
			get
			{
				return this.m_currentMemberIndex;
			}
			set
			{
				this.m_currentMemberIndex = value;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x0011D30D File Offset: 0x0011B50D
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x0011D31F File Offset: 0x0011B51F
		internal int InstanceCount
		{
			get
			{
				if (this.IsStatic)
				{
					return 1;
				}
				return this.m_currentDynamicInstanceCount;
			}
			set
			{
				Global.Tracer.Assert(!this.IsStatic, "Cannot set instance count on static tablix member");
				this.m_currentDynamicInstanceCount = value;
			}
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x0011D340 File Offset: 0x0011B540
		// (set) Token: 0x06004431 RID: 17457 RVA: 0x0011D348 File Offset: 0x0011B548
		public int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
			set
			{
				this.m_indexInCollection = value;
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x0011D351 File Offset: 0x0011B551
		public IndexedInCollectionType IndexedInCollectionType
		{
			get
			{
				return IndexedInCollectionType.Member;
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x0011D354 File Offset: 0x0011B554
		internal bool HasInnerDynamic
		{
			get
			{
				return this.InnerHierarchy != null && this.InnerHierarchy.DynamicMembersAtScope.Count != 0;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x0011D373 File Offset: 0x0011B573
		internal HierarchyNodeList InnerDynamicMembers
		{
			get
			{
				if (this.InnerHierarchy == null)
				{
					return null;
				}
				return this.InnerHierarchy.DynamicMembersAtScope;
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x0011D38A File Offset: 0x0011B58A
		internal HierarchyNodeList InnerStaticMembersInSameScope
		{
			get
			{
				if (this.InnerHierarchy == null)
				{
					return null;
				}
				return this.InnerHierarchy.StaticMembersInSameScope;
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x0011D3A1 File Offset: 0x0011B5A1
		internal int CellStartIndex
		{
			get
			{
				if (this.m_cellStartIndex < 0)
				{
					this.CalculateDependencies();
				}
				return this.m_cellStartIndex;
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x0011D3B8 File Offset: 0x0011B5B8
		internal int CellEndIndex
		{
			get
			{
				if (this.m_cellEndIndex < 0)
				{
					this.CalculateDependencies();
				}
				return this.m_cellEndIndex;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x0011D3CF File Offset: 0x0011B5CF
		internal bool HasFilters
		{
			get
			{
				return this.m_grouping != null && this.m_grouping.Filters != null;
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x0011D3E9 File Offset: 0x0011B5E9
		internal bool HasVariables
		{
			get
			{
				return this.m_grouping != null && this.m_grouping.Variables != null;
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x0011D404 File Offset: 0x0011B604
		// (set) Token: 0x0600443B RID: 17467 RVA: 0x0011D4A7 File Offset: 0x0011B6A7
		internal bool HasInnerFilters
		{
			get
			{
				if (this.m_hasInnerFilters == null)
				{
					this.m_hasInnerFilters = new bool?(false);
					if (this.InnerHierarchy != null)
					{
						int count = this.InnerHierarchy.Count;
						int num = 0;
						while (!this.m_hasInnerFilters.Value && num < count)
						{
							this.m_hasInnerFilters = new bool?(this.InnerHierarchy[num].HasFilters);
							if (!this.m_hasInnerFilters.Value)
							{
								this.m_hasInnerFilters = new bool?(this.InnerHierarchy[num].HasInnerFilters);
							}
							num++;
						}
					}
				}
				return this.m_hasInnerFilters.Value;
			}
			set
			{
				this.m_hasInnerFilters = new bool?(value);
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x0011D4B5 File Offset: 0x0011B6B5
		// (set) Token: 0x0600443D RID: 17469 RVA: 0x0011D4BD File Offset: 0x0011B6BD
		internal AggregatesImpl OutermostStaticCellRVCol
		{
			get
			{
				return this.m_outermostStaticCellRVCol;
			}
			set
			{
				this.m_outermostStaticCellRVCol = value;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x0011D4C6 File Offset: 0x0011B6C6
		// (set) Token: 0x0600443F RID: 17471 RVA: 0x0011D4CE File Offset: 0x0011B6CE
		internal AggregatesImpl[] OutermostStaticCellScopedRVCollections
		{
			get
			{
				return this.m_outermostStaticCellScopedRVCollections;
			}
			set
			{
				this.m_outermostStaticCellScopedRVCollections = value;
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x0011D4D7 File Offset: 0x0011B6D7
		// (set) Token: 0x06004441 RID: 17473 RVA: 0x0011D4DF File Offset: 0x0011B6DF
		internal AggregatesImpl CellRVCol
		{
			get
			{
				return this.m_cellRVCol;
			}
			set
			{
				this.m_cellRVCol = value;
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x0011D4E8 File Offset: 0x0011B6E8
		// (set) Token: 0x06004443 RID: 17475 RVA: 0x0011D4F0 File Offset: 0x0011B6F0
		internal AggregatesImpl[] CellScopedRVCollections
		{
			get
			{
				return this.m_cellScopedRVCollections;
			}
			set
			{
				this.m_cellScopedRVCollections = value;
			}
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x0011D4F9 File Offset: 0x0011B6F9
		internal List<IInScopeEventSource> InScopeEventSources
		{
			get
			{
				return this.m_inScopeEventSources;
			}
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x0011D501 File Offset: 0x0011B701
		bool IRIFReportScope.VariableInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_variablesInScope, sequenceIndex, true);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x0011D510 File Offset: 0x0011B710
		bool IRIFReportScope.TextboxInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_textboxesInScope, sequenceIndex, true);
		}

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x0011D51F File Offset: 0x0011B71F
		public IRIFReportDataScope ParentReportScope
		{
			get
			{
				if (this.m_parentReportScope == null)
				{
					this.m_parentReportScope = IDOwner.FindReportDataScope(base.ParentInstancePath);
				}
				return this.m_parentReportScope;
			}
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x0011D540 File Offset: 0x0011B740
		public bool IsDataIntersectionScope
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x0011D543 File Offset: 0x0011B743
		public bool IsScope
		{
			get
			{
				return this.IsGroup;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x0011D54B File Offset: 0x0011B74B
		public bool IsGroup
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x0011D556 File Offset: 0x0011B756
		public bool IsSameScope(IRIFReportDataScope candidateScope)
		{
			return this.DataScopeInfo.IsSameScope(candidateScope.DataScopeInfo);
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x0011D569 File Offset: 0x0011B769
		public bool IsSameOrChildScopeOf(IRIFReportDataScope candidateScope)
		{
			return DataScopeInfo.IsSameOrChildScope(this, candidateScope);
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x0011D572 File Offset: 0x0011B772
		public bool IsChildScopeOf(IRIFReportDataScope candidateScope)
		{
			return DataScopeInfo.IsChildScopeOf(this, candidateScope);
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x0011D57B File Offset: 0x0011B77B
		public IReference<IOnDemandScopeInstance> CurrentStreamingScopeInstance
		{
			get
			{
				return this.m_currentStreamingScopeInstance;
			}
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x0011D583 File Offset: 0x0011B783
		public void ResetAggregates(AggregatesImpl reportOmAggregates)
		{
			if (this.m_grouping == null)
			{
				return;
			}
			this.m_grouping.ResetAggregates(reportOmAggregates);
			reportOmAggregates.ResetAll<RunningValueInfo>(this.m_runningValues);
			if (this.m_dataScopeInfo != null)
			{
				this.m_dataScopeInfo.ResetAggregates(reportOmAggregates);
			}
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0011D5BA File Offset: 0x0011B7BA
		public bool HasServerAggregate(string aggregateName)
		{
			return DataScopeInfo.ContainsServerAggregate<DataAggregateInfo>(this.m_grouping.Aggregates, aggregateName);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0011D5CD File Offset: 0x0011B7CD
		public void BindToStreamingScopeInstance(IReference<IOnDemandScopeInstance> scopeInstance)
		{
			this.m_currentStreamingScopeInstance = scopeInstance;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0011D5D8 File Offset: 0x0011B7D8
		public void BindToNoRowsScopeInstance(OnDemandProcessingContext odpContext)
		{
			if (this.m_cachedNoRowsStreamingScopeInstance == null)
			{
				StreamingNoRowsMemberInstance streamingNoRowsMemberInstance = new StreamingNoRowsMemberInstance(odpContext, this);
				this.m_cachedNoRowsStreamingScopeInstance = new SyntheticOnDemandMemberInstanceReference(streamingNoRowsMemberInstance);
			}
			this.m_currentStreamingScopeInstance = this.m_cachedNoRowsStreamingScopeInstance;
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x0011D60D File Offset: 0x0011B80D
		public void ClearStreamingScopeInstanceBinding()
		{
			this.m_currentStreamingScopeInstance = null;
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x0011D616 File Offset: 0x0011B816
		public bool IsBoundToStreamingScopeInstance
		{
			get
			{
				return this.m_currentStreamingScopeInstance != null;
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0011D621 File Offset: 0x0011B821
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x0011D629 File Offset: 0x0011B829
		internal int HierarchyDynamicIndex
		{
			get
			{
				return this.m_hierarchyDynamicIndex;
			}
			set
			{
				this.m_hierarchyDynamicIndex = value;
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x0011D632 File Offset: 0x0011B832
		// (set) Token: 0x06004458 RID: 17496 RVA: 0x0011D63A File Offset: 0x0011B83A
		internal int HierarchyPathIndex
		{
			get
			{
				return this.m_hierarchyPathIndex;
			}
			set
			{
				this.m_hierarchyPathIndex = value;
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x0011D643 File Offset: 0x0011B843
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x0011D64B File Offset: 0x0011B84B
		internal Dictionary<string, Grouping>[] CellScopes
		{
			get
			{
				return this.m_cellScopes;
			}
			set
			{
				this.m_cellScopes = value;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x0011D654 File Offset: 0x0011B854
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x0011D65C File Offset: 0x0011B85C
		internal GroupingList HierarchyParentGroups
		{
			get
			{
				return this.m_hierarchyParentGroups;
			}
			set
			{
				this.m_hierarchyParentGroups = ((value != null && value.Count == 0) ? null : value);
			}
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x0011D674 File Offset: 0x0011B874
		internal Dictionary<string, Grouping> GetScopeNames()
		{
			Dictionary<string, Grouping> dictionary = new Dictionary<string, Grouping>();
			int num = ((this.m_hierarchyParentGroups != null) ? this.m_hierarchyParentGroups.Count : 0);
			for (int i = 0; i < num; i++)
			{
				dictionary.Add(this.m_hierarchyParentGroups[i].Name, this.m_hierarchyParentGroups[i]);
			}
			if (!this.IsStatic)
			{
				dictionary.Add(this.m_grouping.Name, this.m_grouping);
			}
			return dictionary;
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x0011D6F0 File Offset: 0x0011B8F0
		internal int InnerDomainScopeCount
		{
			get
			{
				if (this.m_innerDomainScopeCount == null)
				{
					if (this.InnerHierarchy == null)
					{
						this.m_innerDomainScopeCount = new int?(0);
					}
					else
					{
						this.m_innerDomainScopeCount = new int?(this.InnerHierarchy.Count - this.InnerHierarchy.OriginalNodeCount);
					}
				}
				return this.m_innerDomainScopeCount.Value;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x0011D74D File Offset: 0x0011B94D
		internal List<ReportItem> GroupScopedContentsForProcessing
		{
			get
			{
				if (this.m_groupScopedContentsForProcessing == null)
				{
					this.m_groupScopedContentsForProcessing = this.ComputeMemberScopedItems();
				}
				return this.m_groupScopedContentsForProcessing;
			}
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x0011D76C File Offset: 0x0011B96C
		protected virtual List<ReportItem> ComputeMemberScopedItems()
		{
			List<ReportItem> list = null;
			RuntimeRICollection.MergeDataProcessingItems(this.MemberContentCollection, ref list);
			HierarchyNodeList innerStaticMembersInSameScope = this.InnerStaticMembersInSameScope;
			if (innerStaticMembersInSameScope != null)
			{
				foreach (object obj in innerStaticMembersInSameScope)
				{
					RuntimeRICollection.MergeDataProcessingItems(((ReportHierarchyNode)obj).MemberContentCollection, ref list);
				}
			}
			return list;
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x0011D7E0 File Offset: 0x0011B9E0
		internal virtual List<ReportItem> MemberContentCollection
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x0011D7E3 File Offset: 0x0011B9E3
		// (set) Token: 0x06004463 RID: 17507 RVA: 0x0011D7EB File Offset: 0x0011B9EB
		bool IRIFReportScope.NeedToCacheDataRows
		{
			get
			{
				return this.m_needToCacheDataRows;
			}
			set
			{
				if (!this.m_needToCacheDataRows)
				{
					this.m_needToCacheDataRows = value;
				}
			}
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x0011D7FC File Offset: 0x0011B9FC
		void IRIFReportScope.AddInScopeTextBox(TextBox textbox)
		{
			this.AddInScopeTextBox(textbox);
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x0011D805 File Offset: 0x0011BA05
		protected virtual void AddInScopeTextBox(TextBox textbox)
		{
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x0011D807 File Offset: 0x0011BA07
		void IRIFReportScope.ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			this.ResetTextBoxImpls(context);
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x0011D810 File Offset: 0x0011BA10
		internal virtual void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x0011D812 File Offset: 0x0011BA12
		void IRIFReportScope.AddInScopeEventSource(IInScopeEventSource eventSource)
		{
			if (this.m_inScopeEventSources == null)
			{
				this.m_inScopeEventSources = new List<IInScopeEventSource>();
			}
			this.m_inScopeEventSources.Add(eventSource);
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x0011D833 File Offset: 0x0011BA33
		internal virtual void TraverseMemberScopes(IRIFScopeVisitor visitor)
		{
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x0011D838 File Offset: 0x0011BA38
		internal virtual bool InnerInitialize(InitializationContext context, bool restrictive)
		{
			bool flag = false;
			if (this.InnerHierarchy != null)
			{
				bool handledCellContents = context.HandledCellContents;
				context.HandledCellContents = false;
				foreach (object obj in this.InnerHierarchy)
				{
					ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
					flag |= reportHierarchyNode.Initialize(context, false);
				}
				context.HandledCellContents = handledCellContents;
			}
			else
			{
				int memberCellIndex = context.MemberCellIndex;
				context.MemberCellIndex = memberCellIndex + 1;
			}
			return flag;
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x0011D8D0 File Offset: 0x0011BAD0
		internal virtual bool Initialize(InitializationContext context)
		{
			return this.Initialize(context, true);
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x0011D8DC File Offset: 0x0011BADC
		internal virtual bool Initialize(InitializationContext context, bool restrictive)
		{
			bool suspendErrors = context.ErrorContext.SuspendErrors;
			context.ErrorContext.SuspendErrors |= this.m_isAutoSubtotal;
			bool flag = false;
			this.DataGroupStart(context.ExprHostBuilder);
			bool flag2 = false;
			if (this.m_grouping != null)
			{
				context.SetIndexInCollection(this);
				if (this.m_grouping.Variables != null)
				{
					context.RegisterGroupWithVariables(this);
					context.RegisterVariables(this.m_grouping.Variables);
				}
				this.m_variablesInScope = context.GetCurrentReferencableVariables();
				flag = true;
				if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidDetailDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "Grouping", Array.Empty<string>());
				}
				else
				{
					context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping;
					if (this.m_grouping.IsDetail)
					{
						context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail;
					}
					context.RegisterGroupingScope(this);
					flag2 = true;
					this.m_dataScopeInfo.ValidateScopeRulesForIdc(context, this);
					if (this.m_grouping.DomainScope != null && !context.IsAncestor(this, this.m_grouping.DomainScope))
					{
						if (this.m_grouping.IsClone)
						{
							if (Global.Tracer.TraceVerbose)
							{
								Global.Tracer.Trace(TraceLevel.Verbose, "The grouping '{3}' in the {0} '{1}' has invalid {2} '{4}'. Domain Scope is allowed only if it is an ancestor scope.", new object[]
								{
									this.m_dataRegionDef.ObjectType,
									this.m_dataRegionDef.Name,
									"DomainScope",
									this.m_grouping.Name,
									this.m_grouping.DomainScope
								});
							}
							this.m_grouping.DomainScope = null;
							this.m_grouping.ScopeIDForDomainScope = -1;
						}
						else
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsInvalidGroupingDomainScopeNotAncestor, Severity.Error, this.m_dataRegionDef.ObjectType, this.m_dataRegionDef.Name, "DomainScope", new string[]
							{
								this.m_grouping.Name,
								this.m_grouping.DomainScope
							});
						}
					}
					Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = context.ObjectType;
					string objectName = context.ObjectName;
					context.ObjectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping;
					context.ObjectName = this.m_grouping.Name;
					context.ValidateScopeRulesForNaturalGroup(this);
					context.ValidateScopeRulesForNaturalSort(this);
					if (this.HasNaturalGroupAndNaturalSort && !ListUtils.IsSubset<ExpressionInfo>(this.m_sorting.SortExpressions, this.m_grouping.GroupExpressions, RdlExpressionComparer.Instance))
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsIncompatibleNaturalSortAndNaturalGroup, Severity.Error, this.m_dataRegionDef.ObjectType, this.m_dataRegionDef.Name, "Group", new string[] { this.m_grouping.Name });
					}
				}
				this.m_grouping.Initialize(context);
				if (this.m_sorting != null)
				{
					this.m_sorting.Initialize(context);
				}
				if (this.m_grouping.ScopeIDDefinition == null)
				{
					this.InitializeMemberGroupAndSortExpressionFlags();
				}
			}
			if (this.m_dataScopeInfo != null)
			{
				this.m_dataScopeInfo.Initialize(context, this);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, context);
			}
			this.m_memberCellIndex = context.MemberCellIndex;
			flag |= this.InnerInitialize(context, restrictive);
			if (this.m_grouping != null)
			{
				if (flag2)
				{
					context.UnRegisterGroupingScope(this);
				}
				if (this.m_grouping.Variables != null)
				{
					context.UnregisterVariables(this.m_grouping.Variables);
				}
			}
			this.m_exprHostID = this.DataGroupEnd(context.ExprHostBuilder);
			context.ErrorContext.SuspendErrors = suspendErrors;
			return flag;
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x0011DC60 File Offset: 0x0011BE60
		private void InitializeMemberGroupAndSortExpressionFlags()
		{
			int num = ((this.m_sorting == null) ? this.m_grouping.GroupExpressions.Count : this.m_sorting.SortExpressions.Count);
			this.m_memberGroupAndSortExpressionFlag = new List<ScopeIDType>(num);
			if (this.m_sorting != null)
			{
				for (int i = 0; i < this.m_sorting.SortExpressions.Count; i++)
				{
					if (ListUtils.Contains<ExpressionInfo>(this.m_grouping.GroupExpressions, this.m_sorting.SortExpressions[i], RdlExpressionComparer.Instance))
					{
						this.m_memberGroupAndSortExpressionFlag.Add(ScopeIDType.SortGroup);
					}
					else
					{
						this.m_memberGroupAndSortExpressionFlag.Add(ScopeIDType.SortValues);
					}
				}
			}
			for (int j = 0; j < this.m_grouping.GroupExpressions.Count; j++)
			{
				if (this.m_sorting == null || !ListUtils.Contains<ExpressionInfo>(this.m_sorting.SortExpressions, this.m_grouping.GroupExpressions[j], RdlExpressionComparer.Instance))
				{
					this.m_memberGroupAndSortExpressionFlag.Add(ScopeIDType.GroupValues);
				}
			}
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x0011DD60 File Offset: 0x0011BF60
		private bool HasNaturalGroupAndNaturalSort
		{
			get
			{
				return this.m_grouping != null && this.m_sorting != null && this.m_grouping.NaturalGroup && this.m_sorting.NaturalSort;
			}
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x0011DD8C File Offset: 0x0011BF8C
		internal virtual bool PreInitializeDataMember(InitializationContext context)
		{
			return false;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x0011DD90 File Offset: 0x0011BF90
		internal virtual void PostInitializeDataMember(InitializationContext context, bool registeredVisibility)
		{
			if (this.m_grouping != null)
			{
				if (this.m_grouping.IsAtomic(context) || context.EvaluateAtomicityCondition(this.m_dataScopeInfo.HasAggregatesOrRunningValues, this, AtomicityReason.Aggregates) || context.EvaluateAtomicityCondition(this.HasFilters, this, AtomicityReason.Filters) || context.EvaluateAtomicityCondition(this.m_sorting != null && !this.m_sorting.NaturalSort, this, AtomicityReason.NonNaturalSorts))
				{
					context.FoundAtomicScope(this);
					return;
				}
				if (context.EvaluateAtomicityCondition(context.HasMultiplePeerChildScopes(this), this, AtomicityReason.PeerChildScopes))
				{
					this.m_dataScopeInfo.IsDecomposable = true;
					context.FoundAtomicScope(this);
					return;
				}
				this.m_dataScopeInfo.IsDecomposable = true;
			}
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x0011DE3F File Offset: 0x0011C03F
		internal void CaptureReferencableTextboxes(InitializationContext context)
		{
			this.m_textboxesInScope = context.GetCurrentReferencableTextboxes();
		}

		// Token: 0x06004472 RID: 17522
		protected abstract void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder);

		// Token: 0x06004473 RID: 17523
		protected abstract int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder);

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x0011DE4E File Offset: 0x0011C04E
		internal virtual bool IsNonToggleableHiddenMember
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x0011DE51 File Offset: 0x0011C051
		List<RunningValueInfo> IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x0011DE59 File Offset: 0x0011C059
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null, "(null != m_runningValues)");
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues.Clear();
			}
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x0011DE8B File Offset: 0x0011C08B
		internal virtual void InitializeRVDirectionDependentItems(InitializationContext context)
		{
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x0011DE8D File Offset: 0x0011C08D
		internal virtual void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x0011DE8F File Offset: 0x0011C08F
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			return this.PublishClone(context, null, false);
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x0011DE9A File Offset: 0x0011C09A
		internal virtual object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion)
		{
			return this.PublishClone(context, newContainingRegion, false);
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x0011DEA8 File Offset: 0x0011C0A8
		internal virtual object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion, bool isSubtotal)
		{
			ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)base.PublishClone(context);
			reportHierarchyNode.m_dataScopeInfo = this.m_dataScopeInfo.PublishClone(context, reportHierarchyNode.ID);
			context.AddRunningValueHolder(reportHierarchyNode);
			if (isSubtotal)
			{
				reportHierarchyNode.m_grouping = null;
				reportHierarchyNode.m_sorting = null;
			}
			else
			{
				if (this.m_grouping != null)
				{
					reportHierarchyNode.m_grouping = (Grouping)this.m_grouping.PublishClone(context, reportHierarchyNode);
				}
				if (this.m_sorting != null)
				{
					reportHierarchyNode.m_sorting = (Sorting)this.m_sorting.PublishClone(context);
				}
			}
			if (this.m_customProperties != null)
			{
				reportHierarchyNode.m_customProperties = new DataValueList(this.m_customProperties.Count);
				foreach (object obj in this.m_customProperties)
				{
					DataValue dataValue = (DataValue)obj;
					reportHierarchyNode.m_customProperties.Add(dataValue.PublishClone(context));
				}
			}
			if (newContainingRegion != null)
			{
				reportHierarchyNode.m_dataRegionDef = newContainingRegion;
			}
			return reportHierarchyNode;
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x0011DFB8 File Offset: 0x0011C1B8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.IsColumn, Token.Boolean),
				new MemberInfo(MemberName.Level, Token.Int32),
				new MemberInfo(MemberName.Grouping, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping),
				new MemberInfo(MemberName.Sorting, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sorting),
				new MemberInfo(MemberName.MemberCellIndex, Token.Int32),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.RowSpan, Token.Int32),
				new MemberInfo(MemberName.ColSpan, Token.Int32),
				new MemberInfo(MemberName.AutoSubtotal, Token.Boolean),
				new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RunningValueInfo),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.DataRegionDef, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, Token.Reference),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.NeedToCacheDataRows, Token.Boolean),
				new MemberInfo(MemberName.InScopeEventSources, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IInScopeEventSource),
				new MemberInfo(MemberName.HierarchyDynamicIndex, Token.Int32),
				new MemberInfo(MemberName.HierarchyPathIndex, Token.Int32),
				new MemberInfo(MemberName.HierarchyParentGroups, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Grouping),
				new MemberInfo(MemberName.TextboxesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.VariablesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.DataScopeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataScopeInfo),
				new MemberInfo(MemberName.OriginalScopeID, Token.Int32),
				new MemberInfo(MemberName.InnerDomainScopeCount, Token.Int32),
				new MemberInfo(MemberName.MemberGroupAndSortExpressionFlag, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList)
			});
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x0011E1D8 File Offset: 0x0011C3D8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ReportHierarchyNode.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.InScopeEventSources)
				{
					if (memberName <= MemberName.CustomProperties)
					{
						if (memberName <= MemberName.RunningValues)
						{
							switch (memberName)
							{
							case MemberName.Grouping:
								writer.Write(this.m_grouping);
								continue;
							case MemberName.Sorting:
								writer.Write(this.m_sorting);
								continue;
							case MemberName.DataRegionDef:
								writer.WriteReference(this.m_dataRegionDef);
								continue;
							default:
								switch (memberName)
								{
								case MemberName.Level:
									writer.Write(this.m_level);
									continue;
								case MemberName.IsColumn:
									writer.Write(this.m_isColumn);
									continue;
								case MemberName.RunningValues:
									writer.Write<RunningValueInfo>(this.m_runningValues);
									continue;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.ExprHostID)
							{
								writer.Write(this.m_exprHostID);
								continue;
							}
							if (memberName == MemberName.CustomProperties)
							{
								writer.Write(this.m_customProperties);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.IndexInCollection)
					{
						switch (memberName)
						{
						case MemberName.ColSpan:
							writer.Write(this.m_colSpan);
							continue;
						case MemberName.RowSpan:
							writer.Write(this.m_rowSpan);
							continue;
						case MemberName.AutoSubtotal:
							writer.Write(this.m_isAutoSubtotal);
							continue;
						case MemberName.MemberCellIndex:
							writer.Write(this.m_memberCellIndex);
							continue;
						default:
							if (memberName == MemberName.IndexInCollection)
							{
								writer.Write(this.m_indexInCollection);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.NeedToCacheDataRows)
						{
							writer.Write(this.m_needToCacheDataRows);
							continue;
						}
						if (memberName == MemberName.InScopeEventSources)
						{
							writer.WriteListOfReferences(this.m_inScopeEventSources);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.VariablesInScope)
				{
					if (memberName <= MemberName.HierarchyPathIndex)
					{
						if (memberName == MemberName.HierarchyDynamicIndex)
						{
							writer.Write(this.m_hierarchyDynamicIndex);
							continue;
						}
						if (memberName == MemberName.HierarchyPathIndex)
						{
							writer.Write(this.m_hierarchyPathIndex);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.HierarchyParentGroups)
						{
							writer.WriteListOfReferences(this.m_hierarchyParentGroups);
							continue;
						}
						if (memberName == MemberName.VariablesInScope)
						{
							writer.Write(this.m_variablesInScope);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DataScopeInfo)
				{
					if (memberName == MemberName.TextboxesInScope)
					{
						writer.Write(this.m_textboxesInScope);
						continue;
					}
					if (memberName == MemberName.DataScopeInfo)
					{
						writer.Write(this.m_dataScopeInfo);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.InnerDomainScopeCount)
					{
						writer.Write(this.InnerDomainScopeCount);
						continue;
					}
					if (memberName == MemberName.OriginalScopeID)
					{
						writer.Write(this.m_originalScopeID);
						continue;
					}
					if (memberName == MemberName.MemberGroupAndSortExpressionFlag)
					{
						writer.WriteListOfPrimitives<ScopeIDType>(this.m_memberGroupAndSortExpressionFlag);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x0011E504 File Offset: 0x0011C704
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ReportHierarchyNode.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.InScopeEventSources)
				{
					if (memberName <= MemberName.CustomProperties)
					{
						if (memberName <= MemberName.RunningValues)
						{
							switch (memberName)
							{
							case MemberName.Grouping:
								this.Grouping = (Grouping)reader.ReadRIFObject();
								continue;
							case MemberName.Sorting:
								this.m_sorting = (Sorting)reader.ReadRIFObject();
								continue;
							case MemberName.DataRegionDef:
								this.m_dataRegionDef = reader.ReadReference<DataRegion>(this);
								continue;
							default:
								switch (memberName)
								{
								case MemberName.Level:
									this.m_level = reader.ReadInt32();
									continue;
								case MemberName.IsColumn:
									this.m_isColumn = reader.ReadBoolean();
									continue;
								case MemberName.RunningValues:
									this.m_runningValues = reader.ReadGenericListOfRIFObjects<RunningValueInfo>();
									continue;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.ExprHostID)
							{
								this.m_exprHostID = reader.ReadInt32();
								continue;
							}
							if (memberName == MemberName.CustomProperties)
							{
								this.m_customProperties = reader.ReadListOfRIFObjects<DataValueList>();
								continue;
							}
						}
					}
					else if (memberName <= MemberName.IndexInCollection)
					{
						switch (memberName)
						{
						case MemberName.ColSpan:
							this.m_colSpan = reader.ReadInt32();
							continue;
						case MemberName.RowSpan:
							this.m_rowSpan = reader.ReadInt32();
							continue;
						case MemberName.AutoSubtotal:
							this.m_isAutoSubtotal = reader.ReadBoolean();
							continue;
						case MemberName.MemberCellIndex:
							this.m_memberCellIndex = reader.ReadInt32();
							continue;
						default:
							if (memberName == MemberName.IndexInCollection)
							{
								this.m_indexInCollection = reader.ReadInt32();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.NeedToCacheDataRows)
						{
							this.m_needToCacheDataRows = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.InScopeEventSources)
						{
							this.m_inScopeEventSources = reader.ReadGenericListOfReferences<IInScopeEventSource>(this);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.VariablesInScope)
				{
					if (memberName <= MemberName.HierarchyPathIndex)
					{
						if (memberName == MemberName.HierarchyDynamicIndex)
						{
							this.m_hierarchyDynamicIndex = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.HierarchyPathIndex)
						{
							this.m_hierarchyPathIndex = reader.ReadInt32();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.HierarchyParentGroups)
						{
							this.m_hierarchyParentGroups = reader.ReadListOfReferences<GroupingList, Grouping>(this);
							continue;
						}
						if (memberName == MemberName.VariablesInScope)
						{
							this.m_variablesInScope = reader.ReadByteArray();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DataScopeInfo)
				{
					if (memberName == MemberName.TextboxesInScope)
					{
						this.m_textboxesInScope = reader.ReadByteArray();
						continue;
					}
					if (memberName == MemberName.DataScopeInfo)
					{
						this.m_dataScopeInfo = (DataScopeInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.InnerDomainScopeCount)
					{
						this.m_innerDomainScopeCount = new int?(reader.ReadInt32());
						continue;
					}
					if (memberName == MemberName.OriginalScopeID)
					{
						this.m_originalScopeID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.MemberGroupAndSortExpressionFlag)
					{
						this.m_memberGroupAndSortExpressionFlag = reader.ReadListOfPrimitives<ScopeIDType>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x0011E848 File Offset: 0x0011CA48
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ReportHierarchyNode.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.DataRegionDef)
					{
						if (memberName != MemberName.InScopeEventSources)
						{
							if (memberName != MemberName.HierarchyParentGroups)
							{
								Global.Tracer.Assert(false);
							}
							else
							{
								if (this.m_hierarchyParentGroups == null)
								{
									this.m_hierarchyParentGroups = new GroupingList();
								}
								if (memberReference.RefID != -2)
								{
									Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
									Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Grouping);
									Global.Tracer.Assert(!this.m_hierarchyParentGroups.Contains((Grouping)referenceableItems[memberReference.RefID]));
									this.m_hierarchyParentGroups.Add((Grouping)referenceableItems[memberReference.RefID]);
								}
							}
						}
						else
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
							IInScopeEventSource inScopeEventSource = (IInScopeEventSource)referenceable;
							if (this.m_inScopeEventSources == null)
							{
								this.m_inScopeEventSources = new List<IInScopeEventSource>();
							}
							this.m_inScopeEventSources.Add(inScopeEventSource);
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(((ReportItem)referenceableItems[memberReference.RefID]).IsDataRegion);
						Global.Tracer.Assert(this.m_dataRegionDef != (DataRegion)referenceableItems[memberReference.RefID]);
						this.m_dataRegionDef = (DataRegion)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x0011EA40 File Offset: 0x0011CC40
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode;
		}

		// Token: 0x06004481 RID: 17537
		internal abstract void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel);

		// Token: 0x06004482 RID: 17538 RVA: 0x0011EA48 File Offset: 0x0011CC48
		protected void MemberNodeSetExprHost(IMemberNode exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null, "(null != exprHost)");
			this.m_exprHost = exprHost;
			if (this.m_exprHost.GroupHost != null)
			{
				Global.Tracer.Assert(this.m_grouping != null, "(null != m_grouping)");
				this.m_grouping.SetExprHost(this.m_exprHost.GroupHost, reportObjectModel);
			}
			if (this.m_exprHost.SortHost != null)
			{
				Global.Tracer.Assert(this.m_sorting != null, "(null != m_sorting)");
				this.m_sorting.SetExprHost(this.m_exprHost.SortHost, reportObjectModel);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_customProperties != null, "(null != m_customProperties)");
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
			if (this.m_dataScopeInfo != null && this.m_dataScopeInfo.JoinInfo != null && this.m_exprHost.JoinConditionExprHostsRemotable != null)
			{
				this.m_dataScopeInfo.JoinInfo.SetJoinConditionExprHost(this.m_exprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06004483 RID: 17539
		internal abstract void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions);

		// Token: 0x06004484 RID: 17540 RVA: 0x0011EB64 File Offset: 0x0011CD64
		private void CalculateDependencies()
		{
			this.m_cellStartIndex = (this.m_cellEndIndex = this.m_memberCellIndex);
			if (this.InnerHierarchy != null)
			{
				ReportHierarchyNode.GetCellIndexes(this.InnerStaticMembersInSameScope, ref this.m_cellStartIndex, ref this.m_cellEndIndex);
			}
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x0011EBA8 File Offset: 0x0011CDA8
		internal List<int> GetCellIndexes()
		{
			if (this.m_cellIndexes == null)
			{
				if (this.InnerStaticMembersInSameScope != null && this.InnerStaticMembersInSameScope.Count != 0 && this.InnerStaticMembersInSameScope.LeafCellIndexes != null)
				{
					this.m_cellIndexes = this.InnerStaticMembersInSameScope.LeafCellIndexes;
				}
				else
				{
					this.m_cellIndexes = new List<int>(1) { this.m_memberCellIndex };
				}
			}
			return this.m_cellIndexes;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x0011EC14 File Offset: 0x0011CE14
		private static void GetCellIndexes(HierarchyNodeList innerStaticMemberList, ref int cellStartIndex, ref int cellEndIndex)
		{
			if (innerStaticMemberList == null)
			{
				return;
			}
			foreach (object obj in innerStaticMemberList)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				if (reportHierarchyNode.InnerHierarchy == null)
				{
					cellStartIndex = Math.Min(cellStartIndex, reportHierarchyNode.MemberCellIndex);
					cellEndIndex = Math.Max(cellEndIndex, reportHierarchyNode.MemberCellIndex);
				}
			}
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0011EC8C File Offset: 0x0011CE8C
		internal void ResetInstancePathCascade()
		{
			if (this.m_grouping == null)
			{
				return;
			}
			base.InstancePathItem.ResetContext();
			HierarchyNodeList innerDynamicMembers = this.InnerDynamicMembers;
			if (innerDynamicMembers != null)
			{
				for (int i = 0; i < innerDynamicMembers.Count; i++)
				{
					innerDynamicMembers[i].InstancePathItem.ResetContext();
				}
			}
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0011ECD9 File Offset: 0x0011CED9
		internal virtual void MoveNextForUserSort(OnDemandProcessingContext odpContext)
		{
			if (this.m_grouping == null)
			{
				return;
			}
			base.InstancePathItem.MoveNext();
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x0011ECF0 File Offset: 0x0011CEF0
		internal void SetUserSortDetailRowIndex(OnDemandProcessingContext odpContext)
		{
			if (this.m_grouping != null && this.m_grouping.IsDetail)
			{
				int rowIndex = odpContext.ReportObjectModel.FieldsImpl.GetRowIndex();
				if (this.m_isColumn)
				{
					this.m_dataRegionDef.CurrentColDetailIndex = rowIndex;
					return;
				}
				this.m_dataRegionDef.CurrentRowDetailIndex = rowIndex;
			}
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x0011ED44 File Offset: 0x0011CF44
		internal virtual void SetMemberInstances(IList<DataRegionMemberInstance> memberInstances)
		{
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x0011ED46 File Offset: 0x0011CF46
		internal virtual void SetRecursiveParentIndex(int parentInstanceIndex)
		{
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x0011ED48 File Offset: 0x0011CF48
		internal virtual void SetInstanceHasRecursiveChildren(bool? hasRecursiveChildren)
		{
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x0011ED4A File Offset: 0x0011CF4A
		protected override InstancePathItem CreateInstancePathItem()
		{
			if (this.IsStatic)
			{
				return new InstancePathItem();
			}
			if (this.IsColumn)
			{
				return new InstancePathItem(InstancePathItemType.ColumnMemberInstanceIndex, this.IndexInCollection);
			}
			return new InstancePathItem(InstancePathItemType.RowMemberInstanceIndex, this.IndexInCollection);
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x0011ED7B File Offset: 0x0011CF7B
		int IStaticReferenceable.ID
		{
			get
			{
				return this.m_staticRefId;
			}
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x0011ED83 File Offset: 0x0011CF83
		void IStaticReferenceable.SetID(int id)
		{
			this.m_staticRefId = id;
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x0011ED8C File Offset: 0x0011CF8C
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IStaticReferenceable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x04001EEC RID: 7916
		protected bool m_isColumn;

		// Token: 0x04001EED RID: 7917
		protected int m_originalScopeID = -1;

		// Token: 0x04001EEE RID: 7918
		protected int m_level;

		// Token: 0x04001EEF RID: 7919
		protected Grouping m_grouping;

		// Token: 0x04001EF0 RID: 7920
		protected Sorting m_sorting;

		// Token: 0x04001EF1 RID: 7921
		protected List<ScopeIDType> m_memberGroupAndSortExpressionFlag;

		// Token: 0x04001EF2 RID: 7922
		protected int m_memberCellIndex;

		// Token: 0x04001EF3 RID: 7923
		protected int m_exprHostID = -1;

		// Token: 0x04001EF4 RID: 7924
		protected int m_rowSpan;

		// Token: 0x04001EF5 RID: 7925
		protected int m_colSpan;

		// Token: 0x04001EF6 RID: 7926
		protected bool m_isAutoSubtotal;

		// Token: 0x04001EF7 RID: 7927
		protected List<RunningValueInfo> m_runningValues;

		// Token: 0x04001EF8 RID: 7928
		protected DataValueList m_customProperties;

		// Token: 0x04001EF9 RID: 7929
		[Reference]
		protected DataRegion m_dataRegionDef;

		// Token: 0x04001EFA RID: 7930
		private int m_indexInCollection = -1;

		// Token: 0x04001EFB RID: 7931
		private bool m_needToCacheDataRows;

		// Token: 0x04001EFC RID: 7932
		private List<IInScopeEventSource> m_inScopeEventSources;

		// Token: 0x04001EFD RID: 7933
		private byte[] m_textboxesInScope;

		// Token: 0x04001EFE RID: 7934
		private byte[] m_variablesInScope;

		// Token: 0x04001EFF RID: 7935
		private int m_hierarchyDynamicIndex = -1;

		// Token: 0x04001F00 RID: 7936
		private int m_hierarchyPathIndex = -1;

		// Token: 0x04001F01 RID: 7937
		private GroupingList m_hierarchyParentGroups;

		// Token: 0x04001F02 RID: 7938
		private DataScopeInfo m_dataScopeInfo;

		// Token: 0x04001F03 RID: 7939
		private int? m_innerDomainScopeCount;

		// Token: 0x04001F04 RID: 7940
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportHierarchyNode.GetDeclaration();

		// Token: 0x04001F05 RID: 7941
		[NonSerialized]
		private IMemberNode m_exprHost;

		// Token: 0x04001F06 RID: 7942
		[NonSerialized]
		private bool? m_hasInnerFilters;

		// Token: 0x04001F07 RID: 7943
		[NonSerialized]
		protected int m_cellStartIndex = -1;

		// Token: 0x04001F08 RID: 7944
		[NonSerialized]
		protected int m_cellEndIndex = -1;

		// Token: 0x04001F09 RID: 7945
		[NonSerialized]
		private List<int> m_cellIndexes;

		// Token: 0x04001F0A RID: 7946
		[NonSerialized]
		private Dictionary<string, Grouping>[] m_cellScopes;

		// Token: 0x04001F0B RID: 7947
		[NonSerialized]
		protected AggregatesImpl m_outermostStaticCellRVCol;

		// Token: 0x04001F0C RID: 7948
		[NonSerialized]
		protected AggregatesImpl[] m_outermostStaticCellScopedRVCollections;

		// Token: 0x04001F0D RID: 7949
		[NonSerialized]
		protected AggregatesImpl m_cellRVCol;

		// Token: 0x04001F0E RID: 7950
		[NonSerialized]
		protected AggregatesImpl[] m_cellScopedRVCollections;

		// Token: 0x04001F0F RID: 7951
		[NonSerialized]
		protected int m_staticRefId = int.MinValue;

		// Token: 0x04001F10 RID: 7952
		[NonSerialized]
		private int m_currentMemberIndex = -1;

		// Token: 0x04001F11 RID: 7953
		[NonSerialized]
		private int m_currentDynamicInstanceCount = -1;

		// Token: 0x04001F12 RID: 7954
		[NonSerialized]
		private IRIFReportDataScope m_parentReportScope;

		// Token: 0x04001F13 RID: 7955
		[NonSerialized]
		private IReference<IOnDemandScopeInstance> m_currentStreamingScopeInstance;

		// Token: 0x04001F14 RID: 7956
		[NonSerialized]
		private IReference<IOnDemandScopeInstance> m_cachedNoRowsStreamingScopeInstance;

		// Token: 0x04001F15 RID: 7957
		[NonSerialized]
		private List<ReportItem> m_groupScopedContentsForProcessing;
	}
}
