using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004BA RID: 1210
	[Serializable]
	public sealed class DataSet : IDOwner, IAggregateHolder, ISortFilterScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner, IRIFDataScope
	{
		// Token: 0x06003CB7 RID: 15543 RVA: 0x00105528 File Offset: 0x00103728
		internal DataSet(int id, int indexCounter)
			: base(id)
		{
			this.m_indexInCollection = indexCounter;
			this.m_dataRegions = new List<DataRegion>();
			this.m_aggregates = new List<DataAggregateInfo>();
			this.m_postSortAggregates = new List<DataAggregateInfo>();
			this.m_dataSetCore = new DataSetCore();
			this.m_dataSetCore.Fields = new List<Field>();
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x0010558D File Offset: 0x0010378D
		internal DataSet()
		{
			this.m_dataSetCore = new DataSetCore();
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x001055AE File Offset: 0x001037AE
		internal DataSet(DataSetCore dataSetCore)
		{
			this.m_dataSetCore = dataSetCore;
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x001055CB File Offset: 0x001037CB
		internal Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet;
			}
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x001055CF File Offset: 0x001037CF
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x001055D7 File Offset: 0x001037D7
		internal DataSetCore DataSetCore
		{
			get
			{
				return this.m_dataSetCore;
			}
			set
			{
				this.m_dataSetCore = value;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x001055E0 File Offset: 0x001037E0
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x001055ED File Offset: 0x001037ED
		public string Name
		{
			get
			{
				return this.m_dataSetCore.Name;
			}
			set
			{
				this.m_dataSetCore.Name = value;
			}
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x001055FB File Offset: 0x001037FB
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x00105608 File Offset: 0x00103808
		internal List<Field> Fields
		{
			get
			{
				return this.m_dataSetCore.Fields;
			}
			set
			{
				this.m_dataSetCore.Fields = value;
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x00105616 File Offset: 0x00103816
		internal bool HasAggregateIndicatorFields
		{
			get
			{
				return this.m_dataSetCore.HasAggregateIndicatorFields;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x00105623 File Offset: 0x00103823
		// (set) Token: 0x06003CC3 RID: 15555 RVA: 0x00105630 File Offset: 0x00103830
		internal ReportQuery Query
		{
			get
			{
				return this.m_dataSetCore.Query;
			}
			set
			{
				this.m_dataSetCore.Query = value;
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x06003CC4 RID: 15556 RVA: 0x0010563E File Offset: 0x0010383E
		// (set) Token: 0x06003CC5 RID: 15557 RVA: 0x0010564B File Offset: 0x0010384B
		internal SharedDataSetQuery SharedDataSetQuery
		{
			get
			{
				return this.m_dataSetCore.SharedDataSetQuery;
			}
			set
			{
				this.m_dataSetCore.SharedDataSetQuery = value;
			}
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x00105659 File Offset: 0x00103859
		internal bool IsReferenceToSharedDataSet
		{
			get
			{
				return this.m_dataSetCore.SharedDataSetQuery != null;
			}
		}

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x00105669 File Offset: 0x00103869
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x00105676 File Offset: 0x00103876
		internal DataSet.TriState CaseSensitivity
		{
			get
			{
				return this.m_dataSetCore.CaseSensitivity;
			}
			set
			{
				this.m_dataSetCore.CaseSensitivity = value;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x00105684 File Offset: 0x00103884
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x00105691 File Offset: 0x00103891
		internal string Collation
		{
			get
			{
				return this.m_dataSetCore.Collation;
			}
			set
			{
				this.m_dataSetCore.Collation = value;
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x0010569F File Offset: 0x0010389F
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x001056AC File Offset: 0x001038AC
		internal string CollationCulture
		{
			get
			{
				return this.m_dataSetCore.CollationCulture;
			}
			set
			{
				this.m_dataSetCore.CollationCulture = value;
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x001056BA File Offset: 0x001038BA
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x001056C7 File Offset: 0x001038C7
		internal DataSet.TriState AccentSensitivity
		{
			get
			{
				return this.m_dataSetCore.AccentSensitivity;
			}
			set
			{
				this.m_dataSetCore.AccentSensitivity = value;
			}
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x001056D5 File Offset: 0x001038D5
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x001056E2 File Offset: 0x001038E2
		internal DataSet.TriState KanatypeSensitivity
		{
			get
			{
				return this.m_dataSetCore.KanatypeSensitivity;
			}
			set
			{
				this.m_dataSetCore.KanatypeSensitivity = value;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x001056F0 File Offset: 0x001038F0
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x001056FD File Offset: 0x001038FD
		internal DataSet.TriState WidthSensitivity
		{
			get
			{
				return this.m_dataSetCore.WidthSensitivity;
			}
			set
			{
				this.m_dataSetCore.WidthSensitivity = value;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x0010570B File Offset: 0x0010390B
		// (set) Token: 0x06003CD4 RID: 15572 RVA: 0x00105718 File Offset: 0x00103918
		internal bool NullsAsBlanks
		{
			get
			{
				return this.m_dataSetCore.NullsAsBlanks;
			}
			set
			{
				this.m_dataSetCore.NullsAsBlanks = value;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x00105726 File Offset: 0x00103926
		// (set) Token: 0x06003CD6 RID: 15574 RVA: 0x00105733 File Offset: 0x00103933
		internal bool UseOrdinalStringKeyGeneration
		{
			get
			{
				return this.m_dataSetCore.UseOrdinalStringKeyGeneration;
			}
			set
			{
				this.m_dataSetCore.UseOrdinalStringKeyGeneration = value;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x00105741 File Offset: 0x00103941
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x0010574E File Offset: 0x0010394E
		internal List<Filter> Filters
		{
			get
			{
				return this.m_dataSetCore.Filters;
			}
			set
			{
				this.m_dataSetCore.Filters = value;
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x0010575C File Offset: 0x0010395C
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x00105764 File Offset: 0x00103964
		internal List<DataRegion> DataRegions
		{
			get
			{
				return this.m_dataRegions;
			}
			set
			{
				this.m_dataRegions = value;
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x0010576D File Offset: 0x0010396D
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x00105775 File Offset: 0x00103975
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

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x0010577E File Offset: 0x0010397E
		// (set) Token: 0x06003CDE RID: 15582 RVA: 0x00105786 File Offset: 0x00103986
		internal List<LookupInfo> Lookups
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

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x0010578F File Offset: 0x0010398F
		// (set) Token: 0x06003CE0 RID: 15584 RVA: 0x00105797 File Offset: 0x00103997
		internal List<LookupDestinationInfo> LookupDestinationInfos
		{
			get
			{
				return this.m_lookupDestinationInfos;
			}
			set
			{
				this.m_lookupDestinationInfos = value;
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x001057A0 File Offset: 0x001039A0
		internal bool HasLookups
		{
			get
			{
				return this.m_lookupDestinationInfos != null;
			}
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x001057AC File Offset: 0x001039AC
		internal bool HasSameDataSetLookups
		{
			get
			{
				if (this.m_hasSameDataSetLookups == null)
				{
					this.m_hasSameDataSetLookups = new bool?(false);
					if (this.m_lookupDestinationInfos != null)
					{
						for (int i = 0; i < this.m_lookupDestinationInfos.Count; i++)
						{
							if (this.m_lookupDestinationInfos[i].UsedInSameDataSetTablixProcessing)
							{
								this.m_hasSameDataSetLookups = new bool?(true);
								break;
							}
						}
					}
				}
				return this.m_hasSameDataSetLookups.Value;
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x0010581C File Offset: 0x00103A1C
		internal bool UsedOnlyInParametersSet
		{
			get
			{
				return this.m_usedOnlyInParametersSet;
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x00105824 File Offset: 0x00103A24
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x0010582C File Offset: 0x00103A2C
		internal bool UsedOnlyInParameters
		{
			get
			{
				return this.m_usedOnlyInParameters;
			}
			set
			{
				if (!this.m_usedOnlyInParametersSet)
				{
					this.m_usedOnlyInParameters = value;
					this.m_usedOnlyInParametersSet = true;
				}
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x00105844 File Offset: 0x00103A44
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x00105851 File Offset: 0x00103A51
		internal int NonCalculatedFieldCount
		{
			get
			{
				return this.m_dataSetCore.NonCalculatedFieldCount;
			}
			set
			{
				this.m_dataSetCore.NonCalculatedFieldCount = value;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x0010585F File Offset: 0x00103A5F
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x00105867 File Offset: 0x00103A67
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

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x00105870 File Offset: 0x00103A70
		// (set) Token: 0x06003CEB RID: 15595 RVA: 0x0010587D File Offset: 0x00103A7D
		internal uint LCID
		{
			get
			{
				return this.m_dataSetCore.LCID;
			}
			set
			{
				this.m_dataSetCore.LCID = value;
			}
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x0010588B File Offset: 0x00103A8B
		internal CultureInfo CreateCultureInfoFromLcid()
		{
			return this.m_dataSetCore.CreateCultureInfoFromLcid();
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x00105898 File Offset: 0x00103A98
		// (set) Token: 0x06003CEE RID: 15598 RVA: 0x001058A0 File Offset: 0x00103AA0
		internal bool HasDetailUserSortFilter
		{
			get
			{
				return this.m_hasDetailUserSortFilter;
			}
			set
			{
				this.m_hasDetailUserSortFilter = value;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x001058A9 File Offset: 0x00103AA9
		// (set) Token: 0x06003CF0 RID: 15600 RVA: 0x001058B1 File Offset: 0x00103AB1
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

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x001058BA File Offset: 0x00103ABA
		internal DataSetExprHost ExprHost
		{
			get
			{
				return this.m_dataSetCore.ExprHost;
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x001058C7 File Offset: 0x00103AC7
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x001058CF File Offset: 0x00103ACF
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

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x001058D8 File Offset: 0x00103AD8
		int ISortFilterScope.ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x001058E0 File Offset: 0x00103AE0
		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_dataSetCore.Name;
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x001058ED File Offset: 0x00103AED
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x001058F5 File Offset: 0x00103AF5
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

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x001058FE File Offset: 0x00103AFE
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x00105901 File Offset: 0x00103B01
		bool[] ISortFilterScope.IsSortFilterExpressionScope
		{
			get
			{
				return null;
			}
			set
			{
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x00105913 File Offset: 0x00103B13
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x0010591B File Offset: 0x00103B1B
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

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x00105924 File Offset: 0x00103B24
		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				if (this.m_dataSetCore.ExprHost == null)
				{
					return null;
				}
				return this.m_dataSetCore.ExprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x00105945 File Offset: 0x00103B45
		// (set) Token: 0x06003CFE RID: 15614 RVA: 0x0010594D File Offset: 0x00103B4D
		internal bool UsedInAggregates
		{
			get
			{
				return this.m_usedInAggregates;
			}
			set
			{
				this.m_usedInAggregates = value;
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x00105956 File Offset: 0x00103B56
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x0010595E File Offset: 0x00103B5E
		internal bool HasScopeWithCustomAggregates
		{
			get
			{
				return this.m_hasScopeWithCustomAggregates;
			}
			set
			{
				this.m_hasScopeWithCustomAggregates = value;
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x00105967 File Offset: 0x00103B67
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x00105974 File Offset: 0x00103B74
		internal DataSet.TriState InterpretSubtotalsAsDetails
		{
			get
			{
				return this.m_dataSetCore.InterpretSubtotalsAsDetails;
			}
			set
			{
				this.m_dataSetCore.InterpretSubtotalsAsDetails = value;
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x00105982 File Offset: 0x00103B82
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x0010598A File Offset: 0x00103B8A
		internal bool HasSubReports
		{
			get
			{
				return this.m_hasSubReports;
			}
			set
			{
				this.m_hasSubReports = value;
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x00105993 File Offset: 0x00103B93
		internal int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x0010599B File Offset: 0x00103B9B
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x001059A3 File Offset: 0x00103BA3
		internal DataSource DataSource
		{
			get
			{
				return this.m_dataSource;
			}
			set
			{
				this.m_dataSource = value;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x001059AC File Offset: 0x00103BAC
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x001059B4 File Offset: 0x00103BB4
		internal List<DefaultRelationship> DefaultRelationships
		{
			get
			{
				return this.m_defaultRelationships;
			}
			set
			{
				this.m_defaultRelationships = value;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x001059BD File Offset: 0x00103BBD
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x001059C5 File Offset: 0x00103BC5
		internal bool AllowIncrementalProcessing
		{
			get
			{
				return this.m_allowIncrementalProcessing;
			}
			set
			{
				this.m_allowIncrementalProcessing = value;
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x001059CE File Offset: 0x00103BCE
		public DataScopeInfo DataScopeInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x001059D1 File Offset: 0x00103BD1
		Microsoft.ReportingServices.ReportProcessing.ObjectType IRIFDataScope.DataScopeObjectType
		{
			get
			{
				return this.ObjectType;
			}
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x001059D9 File Offset: 0x00103BD9
		internal void Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_dataSetCore.Name;
			context.RegisterDataSet(this);
			this.InternalInitialize(context);
			context.UnRegisterDataSet(this);
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x00105A14 File Offset: 0x00103C14
		private void InternalInitialize(InitializationContext context)
		{
			context.ExprHostBuilder.DataSetStart(this.m_dataSetCore.Name);
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet;
			this.m_dataSetCore.Initialize(context);
			if (this.m_defaultRelationships != null)
			{
				foreach (DefaultRelationship defaultRelationship in this.m_defaultRelationships)
				{
					defaultRelationship.Initialize(this, context);
				}
			}
			if (this.m_userSortExpressions != null)
			{
				context.ExprHostBuilder.UserSortExpressionsStart();
				foreach (ExpressionInfo expressionInfo in this.m_userSortExpressions)
				{
					context.ExprHostBuilder.UserSortExpression(expressionInfo);
				}
				context.ExprHostBuilder.UserSortExpressionsEnd();
			}
			this.m_dataSetCore.ExprHostID = context.ExprHostBuilder.DataSetEnd();
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x00105B20 File Offset: 0x00103D20
		internal void BindAndValidateDefaultRelationships(ErrorContext errorContext, Report report)
		{
			if (this.m_defaultRelationships != null)
			{
				List<string> list = new List<string>(this.m_defaultRelationships.Count);
				foreach (DefaultRelationship defaultRelationship in this.m_defaultRelationships)
				{
					defaultRelationship.BindAndValidate(this, errorContext, report);
					if (list.Contains(defaultRelationship.RelatedDataSetName))
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidDefaultRelationshipDuplicateRelatedDataset, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.Name, "DefaultRelationship", new string[] { "RelatedDataSet", defaultRelationship.RelatedDataSetName });
					}
					else
					{
						list.Add(defaultRelationship.RelatedDataSetName);
					}
				}
			}
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x00105BE0 File Offset: 0x00103DE0
		internal void CheckCircularDefaultRelationshipReference(InitializationContext context)
		{
			HashSet<int> hashSet = new HashSet<int>();
			this.CheckCircularDefaultRelationshipReference(context, this, hashSet);
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x00105BFC File Offset: 0x00103DFC
		private void CheckCircularDefaultRelationshipReference(InitializationContext context, DataSet dataSet, HashSet<int> visitedDataSetIds)
		{
			visitedDataSetIds.Add(base.ID);
			if (this.m_defaultRelationships != null)
			{
				foreach (DefaultRelationship defaultRelationship in this.m_defaultRelationships)
				{
					if (defaultRelationship.RelatedDataSet != null && !defaultRelationship.IsCrossJoin)
					{
						if (visitedDataSetIds.Contains(defaultRelationship.RelatedDataSet.ID))
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsInvalidDefaultRelationshipCircularReference, Severity.Error, dataSet.ObjectType, dataSet.Name, "DefaultRelationship", new string[] { this.Name });
						}
						else
						{
							defaultRelationship.RelatedDataSet.CheckCircularDefaultRelationshipReference(context, dataSet, visitedDataSetIds);
						}
					}
				}
			}
			visitedDataSetIds.Remove(base.ID);
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x00105CD4 File Offset: 0x00103ED4
		internal bool HasDefaultRelationship(DataSet parentDataSet)
		{
			return this.GetDefaultRelationship(parentDataSet) != null;
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x00105CE0 File Offset: 0x00103EE0
		internal DefaultRelationship GetDefaultRelationship(DataSet parentDataSet)
		{
			return JoinInfo.FindActiveRelationship<DefaultRelationship>(this.m_defaultRelationships, parentDataSet);
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x00105CF0 File Offset: 0x00103EF0
		internal void DetermineDecomposability(InitializationContext context)
		{
			if (context.EvaluateAtomicityCondition(this.m_dataSetCore.Filters != null, this, AtomicityReason.Filters) || context.EvaluateAtomicityCondition(this.HasAggregatesForAtomicityCheck(), this, AtomicityReason.Aggregates) || context.EvaluateAtomicityCondition(this.HasLookups, this, AtomicityReason.Lookups) || context.EvaluateAtomicityCondition(this.m_dataRegions.Count > 1, this, AtomicityReason.PeerChildScopes))
			{
				this.m_allowIncrementalProcessing = false;
			}
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x00105D59 File Offset: 0x00103F59
		public static bool AreEqualById(DataSet dataSet1, DataSet dataSet2)
		{
			return (dataSet1 == null && dataSet2 == null) || (dataSet1 != null && dataSet2 != null && dataSet1.ID == dataSet2.ID);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x00105D79 File Offset: 0x00103F79
		private bool HasAggregatesForAtomicityCheck()
		{
			return DataScopeInfo.HasNonServerAggregates<DataAggregateInfo>(this.m_aggregates) || DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_postSortAggregates);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x00105D95 File Offset: 0x00103F95
		List<DataAggregateInfo> IAggregateHolder.GetAggregateList()
		{
			return this.m_aggregates;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x00105D9D File Offset: 0x00103F9D
		List<DataAggregateInfo> IAggregateHolder.GetPostSortAggregateList()
		{
			return this.m_postSortAggregates;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x00105DA8 File Offset: 0x00103FA8
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
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x00105E10 File Offset: 0x00104010
		internal void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			this.m_dataSetCore.SetExprHost(reportExprHost, reportObjectModel);
			if (this.m_lookups != null && reportExprHost.LookupExprHostsRemotable != null)
			{
				for (int i = 0; i < this.m_lookups.Count; i++)
				{
					this.m_lookups[i].SetExprHost(reportExprHost, reportObjectModel);
				}
			}
			if (this.m_lookupDestinationInfos != null && reportExprHost.LookupDestExprHostsRemotable != null)
			{
				for (int j = 0; j < this.m_lookupDestinationInfos.Count; j++)
				{
					this.m_lookupDestinationInfos[j].SetExprHost(reportExprHost, reportObjectModel);
				}
			}
			if (this.m_defaultRelationships != null && this.ExprHost != null)
			{
				foreach (DefaultRelationship defaultRelationship in this.m_defaultRelationships)
				{
					defaultRelationship.SetExprHost(this.ExprHost.JoinConditionExprHostsRemotable, reportObjectModel);
				}
			}
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x00105EFC File Offset: 0x001040FC
		internal void SetFilterExprHost(ObjectModelImpl reportObjectModel)
		{
			this.m_dataSetCore.SetFilterExprHost(reportObjectModel);
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x00105F0A File Offset: 0x0010410A
		internal void SetupRuntimeEnvironment(OnDemandProcessingContext odpContext)
		{
			odpContext.SetComparisonInformation(this.m_dataSetCore);
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x00105F18 File Offset: 0x00104118
		internal bool NeedAutoDetectCollation()
		{
			return this.m_dataSetCore.NeedAutoDetectCollation();
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x00105F25 File Offset: 0x00104125
		internal void MergeCollationSettings(ErrorContext errorContext, string dataSourceType, string cultureName, bool caseSensitive, bool accentSensitive, bool kanatypeSensitive, bool widthSensitive)
		{
			this.m_dataSetCore.MergeCollationSettings(errorContext, dataSourceType, cultureName, caseSensitive, accentSensitive, kanatypeSensitive, widthSensitive);
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x00105F40 File Offset: 0x00104140
		internal void MarkDataRegionsAsNoRows()
		{
			if (this.m_dataRegions == null)
			{
				return;
			}
			foreach (DataRegion dataRegion in this.m_dataRegions)
			{
				dataRegion.NoRows = true;
			}
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x00105F9C File Offset: 0x0010419C
		internal CompareOptions GetCLRCompareOptions()
		{
			return this.m_dataSetCore.GetCLRCompareOptions();
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x00105FAC File Offset: 0x001041AC
		internal void ClearDataRegionStreamingScopeInstances()
		{
			if (this.m_dataRegions != null)
			{
				foreach (DataRegion dataRegion in this.m_dataRegions)
				{
					dataRegion.ClearStreamingScopeInstanceBinding();
				}
			}
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x00106004 File Offset: 0x00104204
		internal void RestrictDataSetAggregates(PublishingErrorContext m_errorContext)
		{
			if (this.m_usedOnlyInParameters || !this.m_usedInAggregates)
			{
				return;
			}
			if (this.m_dataRegions != null && this.m_dataRegions.Count != 0)
			{
				return;
			}
			if (this.m_defaultRelationships != null)
			{
				m_errorContext.Register(ProcessingErrorCode.rsDefaultRelationshipIgnored, Severity.Warning, this.ObjectType, this.Name, "DefaultRelationship", Array.Empty<string>());
			}
			if (this.m_aggregates != null)
			{
				foreach (DataAggregateInfo dataAggregateInfo in this.m_aggregates)
				{
					if (dataAggregateInfo.AggregateType != DataAggregateInfo.AggregateTypes.First)
					{
						m_errorContext.Register(ProcessingErrorCode.rsInvalidDataSetScopedAggregate, Severity.Error, this.ObjectType, this.Name, dataAggregateInfo.AggregateType.ToString(), Array.Empty<string>());
					}
				}
			}
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x001060E4 File Offset: 0x001042E4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new ReadOnlyMemberInfo(MemberName.Name, Token.String),
				new ReadOnlyMemberInfo(MemberName.Fields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Field),
				new ReadOnlyMemberInfo(MemberName.Query, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportQuery),
				new ReadOnlyMemberInfo(MemberName.CaseSensitivity, Token.Enum),
				new ReadOnlyMemberInfo(MemberName.Collation, Token.String),
				new ReadOnlyMemberInfo(MemberName.AccentSensitivity, Token.Enum),
				new ReadOnlyMemberInfo(MemberName.KanatypeSensitivity, Token.Enum),
				new ReadOnlyMemberInfo(MemberName.WidthSensitivity, Token.Enum),
				new MemberInfo(MemberName.DataRegions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion),
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new ReadOnlyMemberInfo(MemberName.Filters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filter),
				new MemberInfo(MemberName.UsedOnlyInParameters, Token.Boolean),
				new ReadOnlyMemberInfo(MemberName.NonCalculatedFieldCount, Token.Int32),
				new ReadOnlyMemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new ReadOnlyMemberInfo(MemberName.LCID, Token.UInt32),
				new MemberInfo(MemberName.HasDetailUserSortFilter, Token.Boolean),
				new MemberInfo(MemberName.UserSortExpressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new ReadOnlyMemberInfo(MemberName.InterpretSubtotalsAsDetails, Token.Enum),
				new MemberInfo(MemberName.HasSubReports, Token.Boolean),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.DataSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSource, Token.Reference),
				new MemberInfo(MemberName.Lookups, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupInfo),
				new MemberInfo(MemberName.LookupDestinations, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupDestinationInfo),
				new MemberInfo(MemberName.DataSetCore, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetCore, Token.Reference),
				new MemberInfo(MemberName.AllowIncrementalProcessing, Token.Boolean),
				new MemberInfo(MemberName.DefaultRelationships, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DefaultRelationship),
				new MemberInfo(MemberName.HasScopeWithCustomAggregates, Token.Boolean)
			});
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x00106334 File Offset: 0x00104534
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataSet.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.HasSubReports)
				{
					if (memberName <= MemberName.UsedOnlyInParameters)
					{
						if (memberName == MemberName.Aggregates)
						{
							writer.Write<DataAggregateInfo>(this.m_aggregates);
							continue;
						}
						if (memberName == MemberName.DataRegions)
						{
							writer.WriteListOfReferences(this.m_dataRegions);
							continue;
						}
						if (memberName == MemberName.UsedOnlyInParameters)
						{
							writer.Write(this.m_usedOnlyInParameters);
							continue;
						}
					}
					else if (memberName <= MemberName.HasDetailUserSortFilter)
					{
						if (memberName == MemberName.PostSortAggregates)
						{
							writer.Write<DataAggregateInfo>(this.m_postSortAggregates);
							continue;
						}
						if (memberName == MemberName.HasDetailUserSortFilter)
						{
							writer.Write(this.m_hasDetailUserSortFilter);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.UserSortExpressions)
						{
							writer.Write<ExpressionInfo>(this.m_userSortExpressions);
							continue;
						}
						if (memberName == MemberName.HasSubReports)
						{
							writer.Write(this.m_hasSubReports);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.LookupDestinations)
				{
					if (memberName <= MemberName.DataSource)
					{
						if (memberName == MemberName.IndexInCollection)
						{
							writer.Write(this.m_indexInCollection);
							continue;
						}
						if (memberName == MemberName.DataSource)
						{
							writer.WriteReference(this.m_dataSource);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Lookups)
						{
							writer.Write<LookupInfo>(this.m_lookups);
							continue;
						}
						if (memberName == MemberName.LookupDestinations)
						{
							writer.Write<LookupDestinationInfo>(this.m_lookupDestinationInfos);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.AllowIncrementalProcessing)
				{
					if (memberName == MemberName.DataSetCore)
					{
						writer.Write(this.m_dataSetCore);
						continue;
					}
					if (memberName == MemberName.AllowIncrementalProcessing)
					{
						writer.Write(this.m_allowIncrementalProcessing);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DefaultRelationships)
					{
						writer.Write<DefaultRelationship>(this.m_defaultRelationships);
						continue;
					}
					if (memberName == MemberName.HasScopeWithCustomAggregates)
					{
						writer.Write(this.m_hasScopeWithCustomAggregates);
						continue;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x00106570 File Offset: 0x00104770
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataSet.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.HasDetailUserSortFilter)
				{
					if (memberName <= MemberName.DataRegions)
					{
						if (memberName <= MemberName.Name)
						{
							switch (memberName)
							{
							case MemberName.Fields:
								this.m_dataSetCore.Fields = reader.ReadGenericListOfRIFObjects<Field>();
								continue;
							case MemberName.CaseSensitivity:
								this.m_dataSetCore.CaseSensitivity = (DataSet.TriState)reader.ReadEnum();
								continue;
							case MemberName.Collation:
								this.m_dataSetCore.Collation = reader.ReadString();
								continue;
							case MemberName.AccentSensitivity:
								this.m_dataSetCore.AccentSensitivity = (DataSet.TriState)reader.ReadEnum();
								continue;
							case MemberName.KanatypeSensitivity:
								this.m_dataSetCore.KanatypeSensitivity = (DataSet.TriState)reader.ReadEnum();
								continue;
							case MemberName.WidthSensitivity:
								this.m_dataSetCore.WidthSensitivity = (DataSet.TriState)reader.ReadEnum();
								continue;
							case MemberName.LCID:
								this.m_dataSetCore.LCID = reader.ReadUInt32();
								continue;
							default:
								if (memberName == MemberName.Name)
								{
									this.m_dataSetCore.Name = reader.ReadString();
									continue;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.Aggregates)
							{
								this.m_aggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
								continue;
							}
							if (memberName == MemberName.Query)
							{
								this.m_dataSetCore.Query = (ReportQuery)reader.ReadRIFObject();
								continue;
							}
							if (memberName == MemberName.DataRegions)
							{
								this.m_dataRegions = reader.ReadGenericListOfReferences<DataRegion>(this);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.NonCalculatedFieldCount)
					{
						if (memberName == MemberName.Filters)
						{
							this.m_dataSetCore.Filters = reader.ReadGenericListOfRIFObjects<Filter>();
							continue;
						}
						if (memberName == MemberName.UsedOnlyInParameters)
						{
							this.m_usedOnlyInParameters = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.NonCalculatedFieldCount)
						{
							this.m_dataSetCore.NonCalculatedFieldCount = reader.ReadInt32();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ExprHostID)
						{
							this.m_dataSetCore.ExprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.PostSortAggregates)
						{
							this.m_postSortAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
							continue;
						}
						if (memberName == MemberName.HasDetailUserSortFilter)
						{
							this.m_hasDetailUserSortFilter = reader.ReadBoolean();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DataSource)
				{
					if (memberName <= MemberName.InterpretSubtotalsAsDetails)
					{
						if (memberName == MemberName.UserSortExpressions)
						{
							this.m_userSortExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
							continue;
						}
						if (memberName == MemberName.InterpretSubtotalsAsDetails)
						{
							this.m_dataSetCore.InterpretSubtotalsAsDetails = (DataSet.TriState)reader.ReadEnum();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.HasSubReports)
						{
							this.m_hasSubReports = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.IndexInCollection)
						{
							this.m_indexInCollection = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.DataSource)
						{
							this.m_dataSource = reader.ReadReference<DataSource>(this);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DataSetCore)
				{
					if (memberName == MemberName.Lookups)
					{
						this.m_lookups = reader.ReadGenericListOfRIFObjects<LookupInfo>();
						continue;
					}
					if (memberName == MemberName.LookupDestinations)
					{
						this.m_lookupDestinationInfos = reader.ReadGenericListOfRIFObjects<LookupDestinationInfo>();
						continue;
					}
					if (memberName == MemberName.DataSetCore)
					{
						this.m_dataSetCore = (DataSetCore)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.AllowIncrementalProcessing)
					{
						this.m_allowIncrementalProcessing = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.DefaultRelationships)
					{
						this.m_defaultRelationships = reader.ReadGenericListOfRIFObjects<DefaultRelationship>();
						continue;
					}
					if (memberName == MemberName.HasScopeWithCustomAggregates)
					{
						this.m_hasScopeWithCustomAggregates = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x00106954 File Offset: 0x00104B54
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(DataSet.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.DataRegions)
					{
						if (memberName != MemberName.DataSource)
						{
							Global.Tracer.Assert(false, string.Empty);
						}
						else
						{
							Global.Tracer.Assert(this.m_dataSource == null);
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSource);
							this.m_dataSource = (DataSource)referenceableItems[memberReference.RefID];
						}
					}
					else
					{
						if (this.m_dataRegions == null)
						{
							this.m_dataRegions = new List<DataRegion>();
						}
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(((ReportItem)referenceableItems[memberReference.RefID]).IsDataRegion);
						Global.Tracer.Assert(!this.m_dataRegions.Contains((DataRegion)referenceableItems[memberReference.RefID]));
						this.m_dataRegions.Add((DataRegion)referenceableItems[memberReference.RefID]);
					}
				}
			}
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x00106AE4 File Offset: 0x00104CE4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet;
		}

		// Token: 0x04001C82 RID: 7298
		internal const uint CompareFlag_Default = 0U;

		// Token: 0x04001C83 RID: 7299
		internal const uint CompareFlag_IgnoreCase = 1U;

		// Token: 0x04001C84 RID: 7300
		internal const uint CompareFlag_IgnoreNonSpace = 2U;

		// Token: 0x04001C85 RID: 7301
		internal const uint CompareFlag_IgnoreKanatype = 65536U;

		// Token: 0x04001C86 RID: 7302
		internal const uint CompareFlag_IgnoreWidth = 131072U;

		// Token: 0x04001C87 RID: 7303
		private DataSetCore m_dataSetCore;

		// Token: 0x04001C88 RID: 7304
		[Reference]
		private List<DataRegion> m_dataRegions;

		// Token: 0x04001C89 RID: 7305
		private List<DataAggregateInfo> m_aggregates;

		// Token: 0x04001C8A RID: 7306
		private List<LookupInfo> m_lookups;

		// Token: 0x04001C8B RID: 7307
		private List<LookupDestinationInfo> m_lookupDestinationInfos;

		// Token: 0x04001C8C RID: 7308
		private bool m_usedOnlyInParameters;

		// Token: 0x04001C8D RID: 7309
		private List<DataAggregateInfo> m_postSortAggregates;

		// Token: 0x04001C8E RID: 7310
		private bool m_hasDetailUserSortFilter;

		// Token: 0x04001C8F RID: 7311
		private List<ExpressionInfo> m_userSortExpressions;

		// Token: 0x04001C90 RID: 7312
		private bool m_hasSubReports;

		// Token: 0x04001C91 RID: 7313
		private int m_indexInCollection = -1;

		// Token: 0x04001C92 RID: 7314
		private bool m_hasScopeWithCustomAggregates;

		// Token: 0x04001C93 RID: 7315
		[Reference]
		private DataSource m_dataSource;

		// Token: 0x04001C94 RID: 7316
		private bool m_allowIncrementalProcessing = true;

		// Token: 0x04001C95 RID: 7317
		private List<DefaultRelationship> m_defaultRelationships;

		// Token: 0x04001C96 RID: 7318
		[NonSerialized]
		private bool m_usedOnlyInParametersSet;

		// Token: 0x04001C97 RID: 7319
		[NonSerialized]
		private bool[] m_isSortFilterTarget;

		// Token: 0x04001C98 RID: 7320
		[NonSerialized]
		private bool m_usedInAggregates;

		// Token: 0x04001C99 RID: 7321
		[NonSerialized]
		private bool? m_hasSameDataSetLookups;

		// Token: 0x04001C9A RID: 7322
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataSet.GetDeclaration();

		// Token: 0x02000978 RID: 2424
		internal enum TriState
		{
			// Token: 0x040040DD RID: 16605
			Auto,
			// Token: 0x040040DE RID: 16606
			True,
			// Token: 0x040040DF RID: 16607
			False
		}
	}
}
