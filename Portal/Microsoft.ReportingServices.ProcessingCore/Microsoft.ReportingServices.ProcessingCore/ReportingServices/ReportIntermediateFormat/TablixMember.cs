using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200051D RID: 1309
	[Serializable]
	internal class TablixMember : ReportHierarchyNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IVisibilityOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable
	{
		// Token: 0x06004632 RID: 17970 RVA: 0x00127008 File Offset: 0x00125208
		internal TablixMember()
		{
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x0012701E File Offset: 0x0012521E
		internal TablixMember(int id, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablixDef)
			: base(id, tablixDef)
		{
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x00127036 File Offset: 0x00125236
		internal override string RdlElementName
		{
			get
			{
				return "TablixMember";
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x0012703D File Offset: 0x0012523D
		internal override HierarchyNodeList InnerHierarchy
		{
			get
			{
				return this.m_tablixMembers;
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x00127045 File Offset: 0x00125245
		// (set) Token: 0x06004637 RID: 17975 RVA: 0x0012704D File Offset: 0x0012524D
		internal TablixMemberList SubMembers
		{
			get
			{
				return this.m_tablixMembers;
			}
			set
			{
				this.m_tablixMembers = value;
			}
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x00127056 File Offset: 0x00125256
		// (set) Token: 0x06004639 RID: 17977 RVA: 0x0012705E File Offset: 0x0012525E
		public Microsoft.ReportingServices.ReportIntermediateFormat.Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x00127067 File Offset: 0x00125267
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x0012706F File Offset: 0x0012526F
		internal TablixMember ParentMember
		{
			get
			{
				return this.m_parentMember;
			}
			set
			{
				this.m_parentMember = value;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x00127078 File Offset: 0x00125278
		// (set) Token: 0x0600463D RID: 17981 RVA: 0x00127080 File Offset: 0x00125280
		internal bool HasStaticPeerWithHeader
		{
			get
			{
				return this.m_hasStaticPeerWithHeader;
			}
			set
			{
				this.m_hasStaticPeerWithHeader = value;
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x00127089 File Offset: 0x00125289
		// (set) Token: 0x0600463F RID: 17983 RVA: 0x00127091 File Offset: 0x00125291
		internal PageBreakLocation PropagatedPageBreakLocation
		{
			get
			{
				return this.m_propagatedPageBreakLocation;
			}
			set
			{
				this.m_propagatedPageBreakLocation = value;
			}
		}

		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x0012709A File Offset: 0x0012529A
		// (set) Token: 0x06004641 RID: 17985 RVA: 0x001270A2 File Offset: 0x001252A2
		internal TablixHeader TablixHeader
		{
			get
			{
				return this.m_tablixHeader;
			}
			set
			{
				this.m_tablixHeader = value;
			}
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x001270AB File Offset: 0x001252AB
		// (set) Token: 0x06004643 RID: 17987 RVA: 0x001270B3 File Offset: 0x001252B3
		internal bool FixedData
		{
			get
			{
				return this.m_fixedData;
			}
			set
			{
				this.m_fixedData = value;
			}
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x001270BC File Offset: 0x001252BC
		// (set) Token: 0x06004645 RID: 17989 RVA: 0x001270C4 File Offset: 0x001252C4
		internal bool RepeatOnNewPage
		{
			get
			{
				return this.m_repeatOnNewPage;
			}
			set
			{
				this.m_repeatOnNewPage = value;
			}
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x001270CD File Offset: 0x001252CD
		// (set) Token: 0x06004647 RID: 17991 RVA: 0x001270D5 File Offset: 0x001252D5
		internal KeepWithGroup KeepWithGroup
		{
			get
			{
				return this.m_keepWithGroup;
			}
			set
			{
				this.m_keepWithGroup = value;
			}
		}

		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x001270DE File Offset: 0x001252DE
		// (set) Token: 0x06004649 RID: 17993 RVA: 0x001270E6 File Offset: 0x001252E6
		internal bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x001270EF File Offset: 0x001252EF
		// (set) Token: 0x0600464B RID: 17995 RVA: 0x001270F7 File Offset: 0x001252F7
		internal bool KeepTogetherSpecified
		{
			get
			{
				return this.m_keepTogetherSpecified;
			}
			set
			{
				this.m_keepTogetherSpecified = value;
			}
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x00127100 File Offset: 0x00125300
		// (set) Token: 0x0600464D RID: 17997 RVA: 0x00127108 File Offset: 0x00125308
		internal bool HideIfNoRows
		{
			get
			{
				return this.m_hideIfNoRows;
			}
			set
			{
				this.m_hideIfNoRows = value;
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x00127111 File Offset: 0x00125311
		// (set) Token: 0x0600464F RID: 17999 RVA: 0x00127119 File Offset: 0x00125319
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

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x00127122 File Offset: 0x00125322
		// (set) Token: 0x06004651 RID: 18001 RVA: 0x0012712A File Offset: 0x0012532A
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

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x00127134 File Offset: 0x00125334
		internal override bool IsNonToggleableHiddenMember
		{
			get
			{
				return this.m_visibility != null && this.m_visibility.Toggle == null && this.m_visibility.Hidden != null && this.m_visibility.Hidden.Type == ExpressionInfo.Types.Constant && this.m_visibility.Hidden.BoolValue;
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x00127188 File Offset: 0x00125388
		private bool WasResized
		{
			get
			{
				return this.m_resizedForLevel > 0;
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x00127193 File Offset: 0x00125393
		// (set) Token: 0x06004655 RID: 18005 RVA: 0x0012719B File Offset: 0x0012539B
		internal bool CanHaveSpanDecreased
		{
			get
			{
				return this.m_canHaveSpanDecreased;
			}
			set
			{
				this.m_canHaveSpanDecreased = value;
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x001271A4 File Offset: 0x001253A4
		internal bool HasToggleableVisibility
		{
			get
			{
				return this.m_visibility != null && this.m_visibility.Toggle != null;
			}
		}

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x001271BE File Offset: 0x001253BE
		internal bool HasConditionalOrToggleableVisibility
		{
			get
			{
				return this.m_visibility != null && (this.m_visibility.Toggle != null || (this.m_visibility.Hidden != null && this.m_visibility.Hidden.Type != ExpressionInfo.Types.Constant));
			}
		}

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x001271FE File Offset: 0x001253FE
		// (set) Token: 0x06004659 RID: 18009 RVA: 0x00127206 File Offset: 0x00125406
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

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x0600465A RID: 18010 RVA: 0x0012720F File Offset: 0x0012540F
		// (set) Token: 0x0600465B RID: 18011 RVA: 0x00127217 File Offset: 0x00125417
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

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x00127220 File Offset: 0x00125420
		// (set) Token: 0x0600465D RID: 18013 RVA: 0x00127228 File Offset: 0x00125428
		internal bool IsInnerMostMemberWithHeader
		{
			get
			{
				return this.m_isInnerMostMemberWithHeader;
			}
			set
			{
				this.m_isInnerMostMemberWithHeader = value;
			}
		}

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x0600465E RID: 18014 RVA: 0x00127231 File Offset: 0x00125431
		internal override bool IsTablixMember
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x00127234 File Offset: 0x00125434
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> InScopeTextBoxes
		{
			get
			{
				return this.m_inScopeTextBoxes;
			}
		}

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x0012723C File Offset: 0x0012543C
		// (set) Token: 0x06004661 RID: 18017 RVA: 0x00127244 File Offset: 0x00125444
		public IReportScopeInstance ROMScopeInstance
		{
			get
			{
				return this.m_romScopeInstance;
			}
			set
			{
				this.m_romScopeInstance = value;
			}
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x0012724D File Offset: 0x0012544D
		// (set) Token: 0x06004663 RID: 18019 RVA: 0x00127255 File Offset: 0x00125455
		public IVisibilityOwner ContainingDynamicVisibility
		{
			get
			{
				return this.m_containingDynamicVisibility;
			}
			set
			{
				this.m_containingDynamicVisibility = value;
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06004664 RID: 18020 RVA: 0x0012725E File Offset: 0x0012545E
		// (set) Token: 0x06004665 RID: 18021 RVA: 0x00127266 File Offset: 0x00125466
		public IVisibilityOwner ContainingDynamicColumnVisibility
		{
			get
			{
				return this.m_containingDynamicColumnVisibility;
			}
			set
			{
				this.m_containingDynamicColumnVisibility = value;
			}
		}

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x0012726F File Offset: 0x0012546F
		// (set) Token: 0x06004667 RID: 18023 RVA: 0x00127277 File Offset: 0x00125477
		public IVisibilityOwner ContainingDynamicRowVisibility
		{
			get
			{
				return this.m_containingDynamicRowVisibility;
			}
			set
			{
				this.m_containingDynamicRowVisibility = value;
			}
		}

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06004668 RID: 18024 RVA: 0x00127280 File Offset: 0x00125480
		public string SenderUniqueName
		{
			get
			{
				if (this.m_senderUniqueName == null && this.m_visibility != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.TextBox toggleSender = this.m_visibility.ToggleSender;
					if (toggleSender != null)
					{
						if (toggleSender.RecursiveSender && this.m_visibility.RecursiveReceiver)
						{
							int value = this.GetRecursiveParentIndex().Value;
							if (value >= 0)
							{
								this.m_senderUniqueName = toggleSender.GetRecursiveUniqueName(value);
							}
						}
						else
						{
							this.m_senderUniqueName = toggleSender.UniqueName;
						}
					}
				}
				return this.m_senderUniqueName;
			}
		}

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x001272F5 File Offset: 0x001254F5
		// (set) Token: 0x0600466A RID: 18026 RVA: 0x001272FD File Offset: 0x001254FD
		internal int ConsecutiveZeroHeightDescendentCount
		{
			get
			{
				return this.m_consecutiveZeroHeightDescendentCount;
			}
			set
			{
				this.m_consecutiveZeroHeightDescendentCount = value;
			}
		}

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x00127306 File Offset: 0x00125506
		// (set) Token: 0x0600466C RID: 18028 RVA: 0x0012730E File Offset: 0x0012550E
		internal int ConsecutiveZeroHeightAncestorCount
		{
			get
			{
				return this.m_consecutiveZeroHeightAncestorCount;
			}
			set
			{
				this.m_consecutiveZeroHeightAncestorCount = value;
			}
		}

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x00127317 File Offset: 0x00125517
		internal bool InstanceHasRecursiveChildren
		{
			get
			{
				return this.m_instanceHasRecursiveChildren == null || this.m_instanceHasRecursiveChildren.Value;
			}
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x00127334 File Offset: 0x00125534
		internal override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> MemberContentCollection
		{
			get
			{
				List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> list = null;
				if (this.m_tablixHeader != null && this.m_tablixHeader.CellContents != null)
				{
					list = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>((this.m_tablixHeader.AltCellContents == null) ? 1 : 2);
					list.Add(this.m_tablixHeader.CellContents);
					if (this.m_tablixHeader.AltCellContents != null)
					{
						list.Add(this.m_tablixHeader.AltCellContents);
					}
				}
				return list;
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x001273A0 File Offset: 0x001255A0
		internal override void TraverseMemberScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_tablixHeader != null)
			{
				if (this.m_tablixHeader.CellContents != null)
				{
					this.m_tablixHeader.CellContents.TraverseScopes(visitor);
				}
				if (this.m_tablixHeader.AltCellContents != null)
				{
					this.m_tablixHeader.AltCellContents.TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x001273F4 File Offset: 0x001255F4
		public bool ComputeHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ToggleCascadeDirection direction)
		{
			TablixMember.VisibilityState cachedVisibilityState = this.GetCachedVisibilityState(renderingContext.OdpContext);
			if (!cachedVisibilityState.HasCachedHidden)
			{
				cachedVisibilityState.HasCachedHidden = true;
				bool flag = false;
				int? num = this.SetupParentRecursiveIndex(renderingContext.OdpContext);
				if (num != null)
				{
					int? num2 = num;
					int num3 = 0;
					if (((num2.GetValueOrDefault() < num3) & (num2 != null)) && this.IsRecursiveToggleReceiver())
					{
						flag = true;
						cachedVisibilityState.CachedHiddenValue = false;
						goto IL_0071;
					}
				}
				cachedVisibilityState.CachedHiddenValue = Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.ComputeHidden(this, renderingContext, direction, out flag);
				IL_0071:
				if (flag)
				{
					cachedVisibilityState.HasCachedDeepHidden = true;
					cachedVisibilityState.CachedDeepHiddenValue = cachedVisibilityState.CachedHiddenValue;
				}
			}
			return cachedVisibilityState.CachedHiddenValue;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x00127490 File Offset: 0x00125690
		public bool ComputeDeepHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ToggleCascadeDirection direction)
		{
			TablixMember.VisibilityState cachedVisibilityState = this.GetCachedVisibilityState(renderingContext.OdpContext);
			if (!cachedVisibilityState.HasCachedDeepHidden)
			{
				direction = this.ToggleCascadeDirection;
				bool flag;
				if (cachedVisibilityState.HasCachedHidden)
				{
					flag = cachedVisibilityState.CachedHiddenValue;
				}
				else
				{
					flag = this.ComputeHidden(renderingContext, direction);
				}
				if (!cachedVisibilityState.HasCachedDeepHidden)
				{
					cachedVisibilityState.HasCachedDeepHidden = true;
					cachedVisibilityState.CachedDeepHiddenValue = Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.ComputeDeepHidden(flag, this, direction, renderingContext);
				}
			}
			return cachedVisibilityState.CachedDeepHiddenValue;
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x001274FC File Offset: 0x001256FC
		public bool ComputeToggleSenderDeepHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			bool flag = false;
			ToggleCascadeDirection toggleCascadeDirection = this.ToggleCascadeDirection;
			TablixMember recursiveMember = this.GetRecursiveMember();
			if (recursiveMember != null)
			{
				int? num = this.SetupParentRecursiveIndex(renderingContext.OdpContext);
				if (num.Value >= 0)
				{
					DataRegionMemberInstance dataRegionMemberInstance = recursiveMember.m_memberInstances[num.Value];
					int? parentInstanceIndex = recursiveMember.m_parentInstanceIndex;
					IList<DataRegionMemberInstance> memberInstances = recursiveMember.m_memberInstances;
					bool? instanceHasRecursiveChildren = recursiveMember.m_instanceHasRecursiveChildren;
					int instanceIndex = recursiveMember.InstancePathItem.InstanceIndex;
					recursiveMember.InstancePathItem.SetContext(num.Value);
					this.m_romScopeInstance.IsNewContext = true;
					recursiveMember.ResetVisibilityComputationCache();
					if (this != recursiveMember)
					{
						this.ResetVisibilityComputationCache();
					}
					flag |= this.m_visibility.ToggleSender.ComputeDeepHidden(renderingContext, toggleCascadeDirection);
					recursiveMember.InstancePathItem.SetContext(instanceIndex);
					this.m_romScopeInstance.IsNewContext = true;
					recursiveMember.m_parentInstanceIndex = parentInstanceIndex;
					recursiveMember.m_memberInstances = memberInstances;
					recursiveMember.m_instanceHasRecursiveChildren = instanceHasRecursiveChildren;
				}
			}
			return flag;
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x001275E8 File Offset: 0x001257E8
		private ToggleCascadeDirection ToggleCascadeDirection
		{
			get
			{
				ToggleCascadeDirection toggleCascadeDirection;
				if (base.IsColumn)
				{
					toggleCascadeDirection = ToggleCascadeDirection.Column;
				}
				else
				{
					toggleCascadeDirection = ToggleCascadeDirection.Row;
				}
				return toggleCascadeDirection;
			}
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00127604 File Offset: 0x00125804
		public bool ComputeStartHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			TablixMember.VisibilityState cachedVisibilityState = this.GetCachedVisibilityState(renderingContext.OdpContext);
			if (!cachedVisibilityState.HasCachedStartHidden)
			{
				cachedVisibilityState.HasCachedStartHidden = true;
				if (this.m_visibility == null || this.m_visibility.Hidden == null)
				{
					cachedVisibilityState.CachedStartHiddenValue = false;
				}
				else if (!this.m_visibility.Hidden.IsExpression)
				{
					cachedVisibilityState.CachedStartHiddenValue = this.m_visibility.Hidden.BoolValue;
				}
				else
				{
					cachedVisibilityState.CachedStartHiddenValue = this.EvaluateStartHidden(this.m_romScopeInstance, renderingContext.OdpContext);
				}
			}
			return cachedVisibilityState.CachedStartHiddenValue;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00127693 File Offset: 0x00125893
		internal void ResetVisibilityComputationCache()
		{
			if (this.m_nonRecursiveVisibilityCache != null)
			{
				this.m_nonRecursiveVisibilityCache.Reset();
			}
			this.m_parentInstanceIndex = null;
			this.m_senderUniqueName = null;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x001276BB File Offset: 0x001258BB
		protected override void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			builder.DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Tablix, this.m_isColumn);
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x001276CA File Offset: 0x001258CA
		protected override int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			return builder.DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Tablix, this.m_isColumn);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x001276DC File Offset: 0x001258DC
		internal override object PublishClone(AutomaticSubtotalContext context, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion newContainingRegion)
		{
			TablixMember tablixMember = (TablixMember)base.PublishClone(context, newContainingRegion);
			if (this.m_tablixHeader != null)
			{
				tablixMember.m_tablixHeader = (TablixHeader)this.m_tablixHeader.PublishClone(context, false);
			}
			if (this.m_tablixMembers != null)
			{
				tablixMember.m_tablixMembers = new TablixMemberList(this.m_tablixMembers.Count);
				foreach (object obj in this.m_tablixMembers)
				{
					TablixMember tablixMember2 = (TablixMember)((TablixMember)obj).PublishClone(context, newContainingRegion);
					tablixMember2.ParentMember = tablixMember;
					tablixMember.m_tablixMembers.Add(tablixMember2);
				}
			}
			if (this.m_visibility != null)
			{
				tablixMember.m_visibility = (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility)this.m_visibility.PublishClone(context, false);
			}
			return tablixMember;
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x001277BC File Offset: 0x001259BC
		internal TablixMember CreateAutomaticSubtotalClone(AutomaticSubtotalContext context, TablixMember parent, bool isDynamicTarget, out int aDynamicsRemoved, ref bool aAllWereDynamic)
		{
			TablixMember tablixMember = null;
			TablixMember tablixMember2 = null;
			aDynamicsRemoved = 0;
			int num = -1;
			if (this.m_grouping != null)
			{
				context.RegisterScopeName(this.m_grouping.Name);
			}
			if (isDynamicTarget || this.m_grouping == null || context.DynamicWithStaticPeerEncountered || (((this.m_isColumn && context.OriginalColumnCount > 1) || (!this.m_isColumn && context.OriginalRowCount > 1)) && (context.HasStaticPeerWithHeader(this, out num) || (this.m_parentMember.m_tablixMembers.Count > 1 && this.m_isInnerMostMemberWithHeader && !this.HasStaticAncestorWithOneMemberGenerations(this)) || (!this.m_isInnerMostMemberWithHeader && this.m_tablixMembers == null && !this.HasInnermostHeaderAncestorWithOneMemberGenerations(this)))))
			{
				tablixMember = (TablixMember)base.PublishClone(context, null, true);
				tablixMember2 = tablixMember;
				tablixMember2.DataElementOutput = DataElementOutputTypes.NoOutput;
				tablixMember2.ParentMember = parent;
				if (parent != null)
				{
					tablixMember2.m_level = parent.m_level + 1;
				}
				else
				{
					Global.Tracer.Assert(tablixMember2.m_level == 0, "(currentClone.m_level == 0)");
				}
				if (this.m_tablixHeader != null)
				{
					tablixMember2.m_headerLevel = context.HeaderLevel;
					int num2 = context.HeaderLevel;
					context.HeaderLevel = num2 + 1;
					tablixMember2.m_tablixHeader = (TablixHeader)this.m_tablixHeader.PublishClone(context, this.m_grouping != null);
				}
				if (this.m_grouping != null)
				{
					if (num > 0 || (context.HasStaticPeerWithHeader(this, out num) && num > 0))
					{
						if (this.m_isColumn)
						{
							tablixMember2.RowSpan -= num;
						}
						else
						{
							tablixMember2.ColSpan -= num;
						}
						tablixMember2.m_resizedForLevel = 1;
					}
					if (!context.DynamicWithStaticPeerEncountered)
					{
						tablixMember2.m_canHaveSpanDecreased = true;
					}
				}
				if (this.m_visibility != null)
				{
					tablixMember2.m_visibility = (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility)this.m_visibility.PublishClone(context, isDynamicTarget);
				}
				if (this.m_tablixMembers != null)
				{
					tablixMember2.m_tablixMembers = new TablixMemberList(this.m_tablixMembers.Count);
				}
			}
			else
			{
				if (this.m_tablixMembers != null)
				{
					tablixMember2 = parent;
					if (this.m_isColumn)
					{
						aDynamicsRemoved = base.RowSpan;
					}
					else
					{
						aDynamicsRemoved = base.ColSpan;
					}
				}
				if (this.m_tablixHeader != null)
				{
					int num2 = context.HeaderLevel;
					context.HeaderLevel = num2 + 1;
				}
				if (this.m_tablixHeader != null)
				{
					TablixMember tablixMember3 = parent;
					while (tablixMember3.m_tablixHeader == null)
					{
						if (tablixMember3.m_parentMember != null)
						{
							tablixMember3 = tablixMember3.m_parentMember;
						}
					}
					if (tablixMember3.m_tablixHeader != null && tablixMember3.m_resizedForLevel < this.m_headerLevel)
					{
						if (this.m_isColumn)
						{
							tablixMember3.RowSpan += base.RowSpan;
							Global.Tracer.Assert(this.m_headerLevel > 0, "(this.m_headerLevel > 0)");
							tablixMember3.m_resizedForLevel = this.m_headerLevel + base.RowSpan - 1;
						}
						else
						{
							tablixMember3.ColSpan += base.ColSpan;
							Global.Tracer.Assert(this.m_headerLevel > 0, "(this.m_headerLevel > 0)");
							tablixMember3.m_resizedForLevel = this.m_headerLevel + base.ColSpan - 1;
						}
					}
				}
			}
			if (this.m_tablixMembers != null)
			{
				int num3 = int.MaxValue;
				bool flag = true;
				int count = tablixMember2.m_tablixMembers.Count;
				foreach (object obj in this.m_tablixMembers)
				{
					TablixMember tablixMember4 = (TablixMember)obj;
					int num4 = 0;
					TablixMember tablixMember5 = tablixMember4.CreateAutomaticSubtotalClone(context, tablixMember2, false, out num4, ref flag);
					if (tablixMember5 != null)
					{
						if (tablixMember4.m_grouping == null)
						{
							flag = false;
							num3 = 0;
						}
						tablixMember2.m_tablixMembers.Add(tablixMember5);
					}
					num3 = Math.Min(num3, num4);
				}
				aDynamicsRemoved += num3;
				aAllWereDynamic = aAllWereDynamic && flag;
				if (tablixMember != null && (this.m_grouping == null || isDynamicTarget))
				{
					for (int i = count; i < tablixMember2.m_tablixMembers.Count; i++)
					{
						TablixMember tablixMember6 = tablixMember2.m_tablixMembers[i];
						if (tablixMember6.m_canHaveSpanDecreased)
						{
							if (flag)
							{
								if (this.m_isColumn)
								{
									tablixMember6.RowSpan = 1;
								}
								else
								{
									tablixMember6.ColSpan = 1;
								}
							}
							else if (this.m_isColumn)
							{
								if (tablixMember6.RowSpan > 1)
								{
									tablixMember6.RowSpan -= num3;
								}
							}
							else if (tablixMember6.ColSpan > 1)
							{
								tablixMember6.ColSpan -= num3;
							}
							tablixMember6.m_canHaveSpanDecreased = false;
							tablixMember6.m_resizedForLevel = 1;
						}
					}
				}
			}
			else
			{
				RowList rows = context.CurrentDataRegion.Rows;
				if (this.m_isColumn)
				{
					for (int j = 0; j < rows.Count; j++)
					{
						Cell cell = (Cell)rows[j].Cells[context.CurrentIndex].PublishClone(context);
						context.CellLists[j].Add(cell);
					}
					TablixColumn tablixColumn = (TablixColumn)((Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)context.CurrentDataRegion).TablixColumns[context.CurrentIndex].PublishClone(context);
					tablixColumn.ForAutoSubtotal = true;
					context.TablixColumns.Add(tablixColumn);
				}
				else
				{
					TablixRow tablixRow = (TablixRow)rows[context.CurrentIndex].PublishClone(context);
					tablixRow.ForAutoSubtotal = true;
					context.Rows.Add(tablixRow);
				}
				int num2 = context.CurrentIndex;
				context.CurrentIndex = num2 + 1;
			}
			if (tablixMember != null && tablixMember.m_tablixMembers != null && tablixMember.m_tablixMembers.Count == 0)
			{
				tablixMember.m_tablixMembers = null;
			}
			return tablixMember;
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00127D5C File Offset: 0x00125F5C
		private bool HasStaticAncestorWithOneMemberGenerations(TablixMember member)
		{
			if (member.ParentMember != null)
			{
				if (member.ParentMember.Grouping == null)
				{
					if (member.ParentMember.SubMembers.Count == 1)
					{
						return true;
					}
				}
				else if (member.ParentMember.SubMembers.Count == 1)
				{
					return this.HasStaticAncestorWithOneMemberGenerations(member.ParentMember);
				}
			}
			return false;
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x00127DB4 File Offset: 0x00125FB4
		private bool HasInnermostHeaderAncestorWithOneMemberGenerations(TablixMember member)
		{
			if (member.ParentMember != null)
			{
				if (member.ParentMember.m_isInnerMostMemberWithHeader)
				{
					if (member.ParentMember.SubMembers.Count == 1)
					{
						return true;
					}
				}
				else if (member.ParentMember.SubMembers.Count == 1)
				{
					return this.HasInnermostHeaderAncestorWithOneMemberGenerations(member.ParentMember);
				}
			}
			return false;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00127E0C File Offset: 0x0012600C
		internal override bool InnerInitialize(InitializationContext context, bool restrictive)
		{
			context.RegisterMemberReportItems(this, true, restrictive);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false);
			}
			bool flag = base.InnerInitialize(context, restrictive);
			if (this.m_tablixHeader != null)
			{
				this.m_tablixHeader.Initialize(context, this.m_isColumn, this.WasResized);
			}
			context.UnRegisterMemberReportItems(this, true, restrictive);
			return flag;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00127E6C File Offset: 0x0012606C
		internal override bool Initialize(InitializationContext context, bool restrictive)
		{
			if (this.m_visibility != null)
			{
				string text = null;
				Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix;
				if (this.m_grouping != null)
				{
					text = context.ObjectName;
					objectType = context.ObjectType;
					context.ObjectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping;
					context.ObjectName = this.m_grouping.Name;
				}
				VisibilityToggleInfo visibilityToggleInfo = this.m_visibility.RegisterVisibilityToggle(context);
				if (visibilityToggleInfo != null)
				{
					visibilityToggleInfo.IsTablixMember = true;
				}
				if (this.m_grouping != null)
				{
					if (visibilityToggleInfo != null)
					{
						visibilityToggleInfo.GroupName = this.m_grouping.Name;
					}
					context.ObjectName = text;
					context.ObjectType = objectType;
				}
			}
			bool flag = context.RegisterVisibility(this.m_visibility, this);
			if (!this.m_hideIfNoRows && this.m_grouping == null)
			{
				((Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)context.GetCurrentDataRegion()).HideStaticsIfNoRows = false;
			}
			if (!this.m_isColumn)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.ValidateKeepWithGroup(this.m_tablixMembers, context);
			}
			bool flag2 = base.Initialize(context, restrictive);
			if (this.m_keepWithGroup != KeepWithGroup.None && flag2)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidKeepWithGroupOnDynamicTablixMember, Severity.Error, context.ObjectType, context.ObjectName, "TablixMember", new string[]
				{
					"KeepWithGroup",
					KeepWithGroup.None.ToString()
				});
				this.m_keepWithGroup = KeepWithGroup.None;
			}
			this.DataRendererInitialize(context);
			if (flag)
			{
				context.UnRegisterVisibility(this.m_visibility, this);
			}
			return flag2;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x00127FC4 File Offset: 0x001261C4
		internal void DataRendererInitialize(InitializationContext context)
		{
			if (this.m_dataElementOutput == DataElementOutputTypes.Auto)
			{
				if (this.m_grouping != null || (this.m_tablixHeader != null && this.m_tablixHeader.CellContents != null))
				{
					this.m_dataElementOutput = DataElementOutputTypes.Output;
				}
				else
				{
					this.m_dataElementOutput = DataElementOutputTypes.ContentsOnly;
				}
			}
			string text = string.Empty;
			if (this.m_grouping != null)
			{
				text = this.m_grouping.Name + "_Collection";
			}
			else if (this.m_tablixHeader != null && this.m_tablixHeader.CellContents != null)
			{
				text = this.m_tablixHeader.CellContents.DataElementName;
			}
			Microsoft.ReportingServices.ReportPublishing.CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, text, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x0012807C File Offset: 0x0012627C
		internal override bool PreInitializeDataMember(InitializationContext context)
		{
			if (this.m_tablixHeader != null)
			{
				if (this.WasResized)
				{
					double headerSize = context.GetHeaderSize(this.m_isColumn, this.m_headerLevel, this.m_isColumn ? this.m_rowSpan : this.m_colSpan);
					this.m_tablixHeader.SizeValue = Math.Round(headerSize, 10);
					this.m_tablixHeader.Size = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertSize(headerSize);
				}
				else if (this.m_headerLevel > -1)
				{
					context.ValidateHeaderSize(this.m_tablixHeader.SizeValue, this.m_headerLevel, this.m_isColumn ? this.m_rowSpan : this.m_colSpan, this.m_isColumn, this.m_memberCellIndex);
				}
			}
			bool flag = context.RegisterVisibility(this.m_visibility, this);
			context.RegisterMemberReportItems(this, false);
			return flag;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00128146 File Offset: 0x00126346
		internal override void PostInitializeDataMember(InitializationContext context, bool registeredVisibility)
		{
			context.UnRegisterMemberReportItems(this, false);
			if (registeredVisibility)
			{
				context.UnRegisterVisibility(this.m_visibility, this);
			}
			base.PostInitializeDataMember(context, registeredVisibility);
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x0012816C File Offset: 0x0012636C
		internal override void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			if (this.m_tablixHeader != null)
			{
				if (this.m_tablixHeader.CellContents != null)
				{
					this.m_tablixHeader.CellContents.InitializeRVDirectionDependentItems(context);
				}
				if (this.m_tablixHeader.AltCellContents != null)
				{
					this.m_tablixHeader.AltCellContents.InitializeRVDirectionDependentItems(context);
				}
			}
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x001281C0 File Offset: 0x001263C0
		internal override void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			if (this.m_tablixHeader != null)
			{
				if (this.m_tablixHeader.CellContents != null)
				{
					this.m_tablixHeader.CellContents.DetermineGroupingExprValueCount(context, groupingExprCount);
				}
				if (this.m_tablixHeader.AltCellContents != null)
				{
					this.m_tablixHeader.AltCellContents.DetermineGroupingExprValueCount(context, groupingExprCount);
				}
			}
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00128214 File Offset: 0x00126414
		internal void ValidateTablixMemberForBanding(PublishingContextStruct context, out bool isdynamic)
		{
			isdynamic = false;
			this.SetIgnoredPropertiesForBandingToDefault(context);
			if (!base.IsStatic)
			{
				isdynamic = true;
				if (this.Visibility != null && this.Visibility.IsToggleReceiver)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidBandShouldNotBeTogglable, Severity.Error, context.ObjectType, context.ObjectName, base.Grouping.Name, Array.Empty<string>());
				}
				if (base.Grouping.PageBreak != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidBandPageBreakIsSet, Severity.Error, context.ObjectType, context.ObjectName, base.Grouping.Name, Array.Empty<string>());
				}
			}
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x001282C0 File Offset: 0x001264C0
		internal void SetIgnoredPropertiesForBandingToDefault(PublishingContextStruct context)
		{
			if (base.CustomProperties != null)
			{
				base.CustomProperties = null;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "CustomProperties", Array.Empty<string>());
			}
			if (this.FixedData)
			{
				this.FixedData = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "FixedData", Array.Empty<string>());
			}
			if (this.HideIfNoRows)
			{
				this.HideIfNoRows = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "HideIfNoRows", Array.Empty<string>());
			}
			if (this.KeepWithGroup != KeepWithGroup.None)
			{
				this.KeepWithGroup = KeepWithGroup.None;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "KeepWithGroup", Array.Empty<string>());
			}
			if (this.RepeatOnNewPage)
			{
				this.RepeatOnNewPage = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "RepeatOnNewPage", Array.Empty<string>());
			}
			if (!this.KeepTogether)
			{
				this.KeepTogether = true;
				if (this.KeepTogetherSpecified)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsBandKeepTogetherIgnored, Severity.Warning, context.ObjectType, context.ObjectName, "KeepTogether", Array.Empty<string>());
				}
			}
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00128434 File Offset: 0x00126634
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, new List<MemberInfo>
			{
				new MemberInfo(MemberName.TablixHeader, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixHeader),
				new MemberInfo(MemberName.TablixMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember),
				new MemberInfo(MemberName.Visibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Visibility),
				new MemberInfo(MemberName.PropagatedPageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.FixedData, Token.Boolean),
				new MemberInfo(MemberName.KeepWithGroup, Token.Enum),
				new MemberInfo(MemberName.RepeatOnNewPage, Token.Boolean),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.HideIfNoRows, Token.Boolean),
				new MemberInfo(MemberName.KeepTogether, Token.Boolean),
				new MemberInfo(MemberName.InScopeTextBoxes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox),
				new MemberInfo(MemberName.ContainingDynamicVisibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IVisibilityOwner, Token.Reference),
				new MemberInfo(MemberName.ContainingDynamicRowVisibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IVisibilityOwner, Token.Reference),
				new MemberInfo(MemberName.ContainingDynamicColumnVisibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IVisibilityOwner, Token.Reference)
			});
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x0012858C File Offset: 0x0012678C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixMember.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.TablixHeader)
				{
					if (memberName <= MemberName.DataElementName)
					{
						if (memberName == MemberName.Visibility)
						{
							writer.Write(this.m_visibility);
							continue;
						}
						if (memberName == MemberName.KeepTogether)
						{
							writer.Write(this.m_keepTogether);
							continue;
						}
						if (memberName == MemberName.DataElementName)
						{
							writer.Write(this.m_dataElementName);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.DataElementOutput)
						{
							writer.WriteEnum((int)this.m_dataElementOutput);
							continue;
						}
						if (memberName == MemberName.PropagatedPageBreakLocation)
						{
							writer.WriteEnum((int)this.m_propagatedPageBreakLocation);
							continue;
						}
						if (memberName == MemberName.TablixHeader)
						{
							writer.Write(this.m_tablixHeader);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.FixedData)
				{
					if (memberName == MemberName.TablixMembers)
					{
						writer.Write(this.m_tablixMembers);
						continue;
					}
					if (memberName == MemberName.KeepWithGroup)
					{
						writer.WriteEnum((int)this.m_keepWithGroup);
						continue;
					}
					if (memberName == MemberName.FixedData)
					{
						writer.Write(this.m_fixedData);
						continue;
					}
				}
				else if (memberName <= MemberName.InScopeTextBoxes)
				{
					if (memberName == MemberName.HideIfNoRows)
					{
						writer.Write(this.m_hideIfNoRows);
						continue;
					}
					if (memberName == MemberName.InScopeTextBoxes)
					{
						writer.WriteListOfReferences(this.m_inScopeTextBoxes);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RepeatOnNewPage)
					{
						writer.Write(this.m_repeatOnNewPage);
						continue;
					}
					switch (memberName)
					{
					case MemberName.ContainingDynamicVisibility:
						writer.WriteReference(this.m_containingDynamicVisibility);
						continue;
					case MemberName.ContainingDynamicRowVisibility:
						writer.WriteReference(this.m_containingDynamicRowVisibility);
						continue;
					case MemberName.ContainingDynamicColumnVisibility:
						writer.WriteReference(this.m_containingDynamicColumnVisibility);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x0012879C File Offset: 0x0012699C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixMember.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.TablixHeader)
				{
					if (memberName <= MemberName.DataElementName)
					{
						if (memberName == MemberName.Visibility)
						{
							this.m_visibility = (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.KeepTogether)
						{
							this.m_keepTogether = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.DataElementName)
						{
							this.m_dataElementName = reader.ReadString();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.DataElementOutput)
						{
							this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
							continue;
						}
						if (memberName == MemberName.PropagatedPageBreakLocation)
						{
							this.m_propagatedPageBreakLocation = (PageBreakLocation)reader.ReadEnum();
							continue;
						}
						if (memberName == MemberName.TablixHeader)
						{
							this.m_tablixHeader = (TablixHeader)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.FixedData)
				{
					if (memberName == MemberName.TablixMembers)
					{
						this.m_tablixMembers = reader.ReadListOfRIFObjects<TablixMemberList>();
						continue;
					}
					if (memberName == MemberName.KeepWithGroup)
					{
						this.m_keepWithGroup = (KeepWithGroup)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.FixedData)
					{
						this.m_fixedData = reader.ReadBoolean();
						continue;
					}
				}
				else if (memberName <= MemberName.InScopeTextBoxes)
				{
					if (memberName == MemberName.HideIfNoRows)
					{
						this.m_hideIfNoRows = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.InScopeTextBoxes)
					{
						this.m_inScopeTextBoxes = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RepeatOnNewPage)
					{
						this.m_repeatOnNewPage = reader.ReadBoolean();
						continue;
					}
					switch (memberName)
					{
					case MemberName.ContainingDynamicVisibility:
						this.m_containingDynamicVisibility = reader.ReadReference<IVisibilityOwner>(this);
						continue;
					case MemberName.ContainingDynamicRowVisibility:
						this.m_containingDynamicRowVisibility = reader.ReadReference<IVisibilityOwner>(this);
						continue;
					case MemberName.ContainingDynamicColumnVisibility:
						this.m_containingDynamicColumnVisibility = reader.ReadReference<IVisibilityOwner>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001289B8 File Offset: 0x00126BB8
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(TablixMember.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.InScopeTextBoxes)
					{
						switch (memberName)
						{
						case MemberName.ContainingDynamicVisibility:
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
							if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable))
							{
								this.m_containingDynamicVisibility = referenceable as IVisibilityOwner;
							}
							break;
						}
						case MemberName.ContainingDynamicRowVisibility:
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable2;
							if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable2))
							{
								this.m_containingDynamicRowVisibility = referenceable2 as IVisibilityOwner;
							}
							break;
						}
						case MemberName.ContainingDynamicColumnVisibility:
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable3;
							if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable3))
							{
								this.m_containingDynamicColumnVisibility = referenceable3 as IVisibilityOwner;
							}
							break;
						}
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						if (this.m_inScopeTextBoxes == null)
						{
							this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
						}
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Microsoft.ReportingServices.ReportIntermediateFormat.TextBox);
						this.m_inScopeTextBoxes.Add((Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceableItems[memberReference.RefID]);
					}
				}
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x00128B2C File Offset: 0x00126D2C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember;
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00128B34 File Offset: 0x00126D34
		internal override void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(memberExprHost != null && reportObjectModel != null);
				this.m_exprHost = (TablixMemberExprHost)memberExprHost;
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				base.MemberNodeSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00128B84 File Offset: 0x00126D84
		internal override void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			if (this.m_tablixHeader != null && this.m_tablixHeader.CellContents != null)
			{
				reportObjectModel.OdpContext.RuntimeInitializeReportItemObjs(this.m_tablixHeader.CellContents, traverseDataRegions);
				if (this.m_tablixHeader.AltCellContents != null)
				{
					reportObjectModel.OdpContext.RuntimeInitializeReportItemObjs(this.m_tablixHeader.AltCellContents, traverseDataRegions);
				}
			}
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00128BE1 File Offset: 0x00126DE1
		internal override void MoveNextForUserSort(OnDemandProcessingContext odpContext)
		{
			base.MoveNextForUserSort(odpContext);
			this.ResetTextBoxImpls(odpContext);
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00128BF1 File Offset: 0x00126DF1
		protected override void AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
			if (this.m_inScopeTextBoxes == null)
			{
				this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
			}
			this.m_inScopeTextBoxes.Add(textbox);
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x00128C14 File Offset: 0x00126E14
		internal override void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			if (this.m_inScopeTextBoxes != null)
			{
				for (int i = 0; i < this.m_inScopeTextBoxes.Count; i++)
				{
					this.m_inScopeTextBoxes[i].ResetTextBoxImpl(context);
				}
			}
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00128C54 File Offset: 0x00126E54
		internal bool EvaluateStartHidden(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			string text = (base.IsStatic ? this.m_dataRegionDef.Name : this.m_grouping.Name);
			return context.ReportRuntime.EvaluateStartHiddenExpression(this.m_visibility, this.m_exprHost, Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix, text);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x00128CA4 File Offset: 0x00126EA4
		internal override void SetMemberInstances(IList<DataRegionMemberInstance> memberInstances)
		{
			this.m_memberInstances = memberInstances;
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00128CB0 File Offset: 0x00126EB0
		internal override void SetRecursiveParentIndex(int parentInstanceIndex)
		{
			if (this.m_parentInstanceIndex != null)
			{
				int? parentInstanceIndex2 = this.m_parentInstanceIndex;
				if ((parentInstanceIndex == parentInstanceIndex2.GetValueOrDefault()) & (parentInstanceIndex2 != null))
				{
					return;
				}
			}
			this.m_senderUniqueName = null;
			this.m_parentInstanceIndex = new int?(parentInstanceIndex);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00128CF8 File Offset: 0x00126EF8
		internal override void SetInstanceHasRecursiveChildren(bool? hasRecursiveChildren)
		{
			this.m_instanceHasRecursiveChildren = hasRecursiveChildren;
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00128D04 File Offset: 0x00126F04
		private int? SetupParentRecursiveIndex(OnDemandProcessingContext odpContext)
		{
			if (this.IsRecursive())
			{
				if (this.m_parentInstanceIndex == null)
				{
					odpContext.SetupContext(this, this.m_romScopeInstance);
				}
			}
			else if (this.IsToggleableChildOfRecursive())
			{
				this.m_parentInstanceIndex = this.m_visibility.RecursiveMember.SetupParentRecursiveIndex(odpContext);
			}
			return this.m_parentInstanceIndex;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x00128D5A File Offset: 0x00126F5A
		private TablixMember.VisibilityState GetCachedVisibilityState(OnDemandProcessingContext odpContext)
		{
			return this.GetCachedVisibilityState(odpContext, int.MinValue);
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00128D68 File Offset: 0x00126F68
		private TablixMember.VisibilityState GetCachedVisibilityState(OnDemandProcessingContext odpContext, int memberIndex)
		{
			TablixMember recursiveMember = this.GetRecursiveMember();
			if (recursiveMember != null && this.m_visibility != null && this.m_visibility.RecursiveReceiver)
			{
				if (memberIndex == -2147483648)
				{
					if (recursiveMember.m_parentInstanceIndex == null)
					{
						recursiveMember.SetupParentRecursiveIndex(odpContext);
					}
					memberIndex = recursiveMember.CurrentMemberIndex;
				}
				DataRegionMemberInstance dataRegionMemberInstance = recursiveMember.m_memberInstances[memberIndex];
				int recursiveLevel = dataRegionMemberInstance.RecursiveLevel;
				if (this.m_recursiveVisibilityCache == null)
				{
					this.m_recursiveVisibilityCache = new List<TablixMember.VisibilityState>();
				}
				TablixMember.VisibilityState visibilityState = null;
				if (recursiveLevel >= this.m_recursiveVisibilityCache.Count)
				{
					for (int i = this.m_recursiveVisibilityCache.Count; i <= recursiveLevel; i++)
					{
						visibilityState = new TablixMember.VisibilityState();
						this.m_recursiveVisibilityCache.Add(visibilityState);
					}
				}
				else
				{
					visibilityState = this.m_recursiveVisibilityCache[recursiveLevel];
				}
				if (visibilityState.MemberInstance != dataRegionMemberInstance)
				{
					visibilityState.Reset();
					visibilityState.MemberInstance = dataRegionMemberInstance;
				}
				return visibilityState;
			}
			if (this.m_nonRecursiveVisibilityCache == null)
			{
				this.m_nonRecursiveVisibilityCache = new TablixMember.VisibilityState();
			}
			else if (this.m_romScopeInstance != null && this.m_romScopeInstance.IsNewContext)
			{
				this.m_nonRecursiveVisibilityCache.Reset();
			}
			return this.m_nonRecursiveVisibilityCache;
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00128E88 File Offset: 0x00127088
		private TablixMember GetRecursiveMember()
		{
			TablixMember tablixMember = null;
			if (this.IsRecursive())
			{
				tablixMember = this;
			}
			else if (this.IsToggleableChildOfRecursive())
			{
				tablixMember = this.m_visibility.RecursiveMember;
			}
			return tablixMember;
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x00128EB8 File Offset: 0x001270B8
		private int? GetRecursiveParentIndex()
		{
			return this.m_parentInstanceIndex;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x00128EC0 File Offset: 0x001270C0
		private bool IsRecursive()
		{
			return this.m_grouping != null && this.m_grouping.Parent != null;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x00128EDA File Offset: 0x001270DA
		private bool IsToggleableChildOfRecursive()
		{
			return this.m_visibility != null && this.m_visibility.RecursiveMember != null;
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x00128EF4 File Offset: 0x001270F4
		private bool IsRecursiveToggleReceiver()
		{
			return this.m_visibility != null && this.m_visibility.Toggle != null && this.m_visibility.RecursiveReceiver;
		}

		// Token: 0x04001F87 RID: 8071
		private TablixHeader m_tablixHeader;

		// Token: 0x04001F88 RID: 8072
		private TablixMemberList m_tablixMembers;

		// Token: 0x04001F89 RID: 8073
		private Microsoft.ReportingServices.ReportIntermediateFormat.Visibility m_visibility;

		// Token: 0x04001F8A RID: 8074
		private PageBreakLocation m_propagatedPageBreakLocation;

		// Token: 0x04001F8B RID: 8075
		private bool m_keepTogether;

		// Token: 0x04001F8C RID: 8076
		private bool m_fixedData;

		// Token: 0x04001F8D RID: 8077
		private KeepWithGroup m_keepWithGroup;

		// Token: 0x04001F8E RID: 8078
		private bool m_repeatOnNewPage;

		// Token: 0x04001F8F RID: 8079
		private string m_dataElementName;

		// Token: 0x04001F90 RID: 8080
		private DataElementOutputTypes m_dataElementOutput = DataElementOutputTypes.Auto;

		// Token: 0x04001F91 RID: 8081
		private bool m_hideIfNoRows;

		// Token: 0x04001F92 RID: 8082
		[Reference]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> m_inScopeTextBoxes;

		// Token: 0x04001F93 RID: 8083
		[Reference]
		private IVisibilityOwner m_containingDynamicVisibility;

		// Token: 0x04001F94 RID: 8084
		[Reference]
		private IVisibilityOwner m_containingDynamicRowVisibility;

		// Token: 0x04001F95 RID: 8085
		[Reference]
		private IVisibilityOwner m_containingDynamicColumnVisibility;

		// Token: 0x04001F96 RID: 8086
		[NonSerialized]
		private bool m_keepTogetherSpecified;

		// Token: 0x04001F97 RID: 8087
		[NonSerialized]
		private TablixMember m_parentMember;

		// Token: 0x04001F98 RID: 8088
		[NonSerialized]
		private bool[] m_headerLevelHasStaticArray;

		// Token: 0x04001F99 RID: 8089
		[NonSerialized]
		private int m_headerLevel = -1;

		// Token: 0x04001F9A RID: 8090
		[NonSerialized]
		private bool m_isInnerMostMemberWithHeader;

		// Token: 0x04001F9B RID: 8091
		[NonSerialized]
		private bool m_hasStaticPeerWithHeader;

		// Token: 0x04001F9C RID: 8092
		[NonSerialized]
		private int m_resizedForLevel;

		// Token: 0x04001F9D RID: 8093
		[NonSerialized]
		private bool m_canHaveSpanDecreased;

		// Token: 0x04001F9E RID: 8094
		[NonSerialized]
		private int m_consecutiveZeroHeightDescendentCount;

		// Token: 0x04001F9F RID: 8095
		[NonSerialized]
		private int m_consecutiveZeroHeightAncestorCount;

		// Token: 0x04001FA0 RID: 8096
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixMember.GetDeclaration();

		// Token: 0x04001FA1 RID: 8097
		[NonSerialized]
		private string m_senderUniqueName;

		// Token: 0x04001FA2 RID: 8098
		[NonSerialized]
		private int? m_parentInstanceIndex;

		// Token: 0x04001FA3 RID: 8099
		[NonSerialized]
		private bool? m_instanceHasRecursiveChildren;

		// Token: 0x04001FA4 RID: 8100
		[NonSerialized]
		private IList<DataRegionMemberInstance> m_memberInstances;

		// Token: 0x04001FA5 RID: 8101
		[NonSerialized]
		private TablixMemberExprHost m_exprHost;

		// Token: 0x04001FA6 RID: 8102
		[NonSerialized]
		private IReportScopeInstance m_romScopeInstance;

		// Token: 0x04001FA7 RID: 8103
		[NonSerialized]
		private List<TablixMember.VisibilityState> m_recursiveVisibilityCache;

		// Token: 0x04001FA8 RID: 8104
		[NonSerialized]
		private TablixMember.VisibilityState m_nonRecursiveVisibilityCache;

		// Token: 0x02000988 RID: 2440
		private class VisibilityState
		{
			// Token: 0x0600808D RID: 32909 RVA: 0x0021186A File Offset: 0x0020FA6A
			internal void Reset()
			{
				this.CachedHiddenValue = false;
				this.HasCachedHidden = false;
				this.CachedDeepHiddenValue = false;
				this.HasCachedDeepHidden = false;
				this.CachedStartHiddenValue = false;
				this.HasCachedStartHidden = false;
				this.MemberInstance = null;
			}

			// Token: 0x0400415D RID: 16733
			internal DataRegionMemberInstance MemberInstance;

			// Token: 0x0400415E RID: 16734
			internal bool CachedHiddenValue;

			// Token: 0x0400415F RID: 16735
			internal bool HasCachedHidden;

			// Token: 0x04004160 RID: 16736
			internal bool CachedDeepHiddenValue;

			// Token: 0x04004161 RID: 16737
			internal bool HasCachedDeepHidden;

			// Token: 0x04004162 RID: 16738
			internal bool CachedStartHiddenValue;

			// Token: 0x04004163 RID: 16739
			internal bool HasCachedStartHidden;
		}
	}
}
