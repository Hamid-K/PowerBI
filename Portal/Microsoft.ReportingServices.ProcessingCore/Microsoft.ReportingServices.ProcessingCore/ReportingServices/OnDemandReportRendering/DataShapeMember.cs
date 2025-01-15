using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000097 RID: 151
	internal sealed class DataShapeMember : DataRegionMember
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x000269B0 File Offset: 0x00024BB0
		internal DataShapeMember(IReportScope reportScope, IDefinitionPath parentDefinitionPath, DataShape owner, DataShapeMember parent, DataShapeMember rifDataShapeMember, int parentCollectionIndex, DataShapeLimit[] limitsToReset, DataShapeLimit[] limitsToIncrement, DataShapeLimit[] applicableChildLimits)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			if (rifDataShapeMember.IsStatic)
			{
				this.m_reportScope = reportScope;
				DataShapeMember singleDynamicMemberOrNull = rifDataShapeMember.SameLevelMembers.GetSingleDynamicMemberOrNull();
				if (singleDynamicMemberOrNull == null)
				{
					this.m_type = DataShapeMember.MemberType.StaticWithNoPeerDynamic;
				}
				else
				{
					int memberCellIndex = singleDynamicMemberOrNull.MemberCellIndex;
					if (rifDataShapeMember.MemberCellIndex < memberCellIndex)
					{
						this.m_type = DataShapeMember.MemberType.StaticPrecedesDynamicPeer;
						if (singleDynamicMemberOrNull.Grouping.StartPositions != null)
						{
							this.m_owner.m_renderingContext.SegmentationManager.TrySetStartPositionApplied(singleDynamicMemberOrNull);
						}
					}
					else
					{
						this.m_type = DataShapeMember.MemberType.StaticSucceedsDynamicPeer;
					}
				}
			}
			else
			{
				this.m_type = DataShapeMember.MemberType.Dynamic;
				if (rifDataShapeMember.Grouping.StartPositions != null)
				{
					this.m_owner.m_renderingContext.SegmentationManager.ResetStartPositionApplied(rifDataShapeMember);
				}
			}
			this.m_owner = owner;
			this.m_rifDataShapeMember = rifDataShapeMember;
			this.m_limitsToReset = limitsToReset;
			this.m_limitsToIncrement = limitsToIncrement;
			this.m_applicableChildLimits = applicableChildLimits;
			if (this.m_rifDataShapeMember.Grouping != null)
			{
				this.m_group = new Group(this.OwnerDataShape, this.m_rifDataShapeMember, this);
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00026AB1 File Offset: 0x00024CB1
		internal override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.RifDataShapeMemberDefinition;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00026AB9 File Offset: 0x00024CB9
		internal override IDataRegionMemberCollection SubMembers
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00026AC1 File Offset: 0x00024CC1
		internal override bool GetIsColumn()
		{
			return this.IsColumn;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00026AC9 File Offset: 0x00024CC9
		internal override string UniqueName
		{
			get
			{
				return this.m_rifDataShapeMember.UniqueName;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00026AD6 File Offset: 0x00024CD6
		public override string ID
		{
			get
			{
				return this.m_rifDataShapeMember.RenderingModelID;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00026AE3 File Offset: 0x00024CE3
		public override bool IsStatic
		{
			get
			{
				return this.m_rifDataShapeMember.Grouping == null;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00026AF3 File Offset: 0x00024CF3
		public override int MemberCellIndex
		{
			get
			{
				return this.m_rifDataShapeMember.MemberCellIndex;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00026B00 File Offset: 0x00024D00
		internal override IReportScope ReportScope
		{
			get
			{
				if (!this.IsStatic)
				{
					return this;
				}
				return this.m_reportScope;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00026B20 File Offset: 0x00024D20
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				if (!this.IsStatic)
				{
					return this.RifDataShapeMemberDefinition;
				}
				return this.m_reportScope.RIFReportScope;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00026B49 File Offset: 0x00024D49
		internal override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				if (!this.IsStatic)
				{
					return (IReportScopeInstance)this.Instance;
				}
				return this.m_reportScope.ReportScopeInstance;
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00026B6C File Offset: 0x00024D6C
		internal override void SetNewContext(bool fromMoveNext)
		{
			if (!fromMoveNext && this.m_instance != null && !this.IsStatic)
			{
				((IDynamicInstance)this.m_instance).ResetContext();
			}
			base.SetNewContext(fromMoveNext);
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_calculations != null)
			{
				this.m_calculations.SetNewContext();
			}
			if (this.m_dataShapes != null)
			{
				this.m_dataShapes.SetNewContext();
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00026BDC File Offset: 0x00024DDC
		public string ClientID
		{
			get
			{
				return this.m_rifDataShapeMember.Name;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00026BE9 File Offset: 0x00024DE9
		internal DataShapeMember Parent
		{
			get
			{
				return (DataShapeMember)this.m_parent;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00026BF6 File Offset: 0x00024DF6
		internal DataShape OwnerDataShape
		{
			get
			{
				return (DataShape)this.m_owner;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00026C04 File Offset: 0x00024E04
		public DataShapeMemberCollection Children
		{
			get
			{
				DataShapeMemberList subMembers = this.m_rifDataShapeMember.SubMembers;
				if (subMembers == null)
				{
					return null;
				}
				if (this.m_children == null)
				{
					this.m_children = new DataShapeMemberCollection(this, this.OwnerDataShape, this, subMembers);
				}
				return this.m_children;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00026C44 File Offset: 0x00024E44
		public DataShapeCalculationCollection Calculations
		{
			get
			{
				if (this.m_calculations == null)
				{
					this.m_calculations = new DataShapeCalculationCollection(this, this.RifDataShapeMemberDefinition.Calculations, this.m_owner.RenderingContext);
				}
				return this.m_calculations;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00026C76 File Offset: 0x00024E76
		public DataShapeCollection DataShapes
		{
			get
			{
				if (this.m_dataShapes == null)
				{
					this.m_dataShapes = new DataShapeCollection(this.RifDataShapeMemberDefinition.DataShapes, this, this.m_owner.RenderingContext);
				}
				return this.m_dataShapes;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00026CA8 File Offset: 0x00024EA8
		internal bool IsColumn
		{
			get
			{
				return this.m_rifDataShapeMember.IsColumn;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00026CB5 File Offset: 0x00024EB5
		internal DataShapeMember RifDataShapeMemberDefinition
		{
			get
			{
				return this.m_rifDataShapeMember;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00026CC0 File Offset: 0x00024EC0
		internal DataShapeMemberInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new DataShapeMemberInstance(this.OwnerDataShape, this);
					}
					else
					{
						DataShapeDynamicMemberInstance dataShapeDynamicMemberInstance = new DataShapeDynamicMemberInstance(this.OwnerDataShape, this, this.BuildOdpMemberLogic(this.OwnerDataShape.RenderingContext.OdpContext));
						this.m_owner.RenderingContext.AddDynamicInstance(dataShapeDynamicMemberInstance);
						this.m_instance = dataShapeDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00026D32 File Offset: 0x00024F32
		internal DataShapeMember.MemberType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00026D3C File Offset: 0x00024F3C
		internal void ResetWithinLimits()
		{
			if (this.m_limitsToReset != null)
			{
				DataShapeLimit[] limitsToReset = this.m_limitsToReset;
				for (int i = 0; i < limitsToReset.Length; i++)
				{
					limitsToReset[i].ResetCounter();
				}
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00026D70 File Offset: 0x00024F70
		internal bool IncrementTargetLimits()
		{
			bool flag = true;
			if (this.m_limitsToIncrement != null && this.m_limitsToIncrement.Length != 0)
			{
				foreach (DataShapeLimit dataShapeLimit in this.m_limitsToIncrement)
				{
					flag &= dataShapeLimit.Increment();
				}
			}
			return flag;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00026DB4 File Offset: 0x00024FB4
		internal bool VerifyChildLimitsNotExceeded()
		{
			bool flag = true;
			if (this.m_applicableChildLimits != null && this.m_applicableChildLimits.Length != 0)
			{
				foreach (DataShapeLimit dataShapeLimit in this.m_applicableChildLimits)
				{
					if (dataShapeLimit.CurrentCountExhausted)
					{
						dataShapeLimit.SetExceeded();
						flag &= dataShapeLimit.Operator.IgnoreLimitCount;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00026E0A File Offset: 0x0002500A
		internal override InternalDynamicMemberLogic BuildOdpMemberLogic(OnDemandProcessingContext odpContext)
		{
			return new InternalStreamingOdpDataShapeDynamicMemberLogic(this, odpContext);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00026E13 File Offset: 0x00025013
		internal bool HasRestartDefinition()
		{
			return this.OwnerDataShape.RifDataShapeDefinition.RestartDefinitions != null && this.OwnerDataShape.RifDataShapeDefinition.RestartMembers.Contains(this.m_rifDataShapeMember);
		}

		// Token: 0x0400025C RID: 604
		private readonly DataShapeMember m_rifDataShapeMember;

		// Token: 0x0400025D RID: 605
		private readonly IReportScope m_reportScope;

		// Token: 0x0400025E RID: 606
		private DataShapeMemberCollection m_children;

		// Token: 0x0400025F RID: 607
		private DataShapeCalculationCollection m_calculations;

		// Token: 0x04000260 RID: 608
		private DataShapeCollection m_dataShapes;

		// Token: 0x04000261 RID: 609
		private DataShapeMemberInstance m_instance;

		// Token: 0x04000262 RID: 610
		private readonly DataShapeLimit[] m_limitsToReset;

		// Token: 0x04000263 RID: 611
		private readonly DataShapeLimit[] m_limitsToIncrement;

		// Token: 0x04000264 RID: 612
		private readonly DataShapeLimit[] m_applicableChildLimits;

		// Token: 0x04000265 RID: 613
		private readonly DataShapeMember.MemberType m_type;

		// Token: 0x02000923 RID: 2339
		internal enum MemberType
		{
			// Token: 0x04003F76 RID: 16246
			StaticPrecedesDynamicPeer,
			// Token: 0x04003F77 RID: 16247
			StaticSucceedsDynamicPeer,
			// Token: 0x04003F78 RID: 16248
			StaticWithNoPeerDynamic,
			// Token: 0x04003F79 RID: 16249
			Dynamic
		}
	}
}
