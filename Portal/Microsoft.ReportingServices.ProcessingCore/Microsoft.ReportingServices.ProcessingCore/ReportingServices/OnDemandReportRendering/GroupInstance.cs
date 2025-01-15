using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200029E RID: 670
	public sealed class GroupInstance : BaseInstance
	{
		// Token: 0x060019DB RID: 6619 RVA: 0x00068B62 File Offset: 0x00066D62
		internal GroupInstance(Group owner)
			: base(null)
		{
			this.m_owner = owner;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00068B79 File Offset: 0x00066D79
		internal GroupInstance(Group owner, IReportScope reportScope)
			: base(reportScope)
		{
			this.m_owner = owner;
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00068B90 File Offset: 0x00066D90
		public string UniqueName
		{
			get
			{
				if (!this.m_owner.IsOldSnapshot)
				{
					if (this.m_uniqueName == null)
					{
						this.m_uniqueName = InstancePathItem.GenerateUniqueNameString(this.m_owner.MemberDefinition.ID, this.m_owner.MemberDefinition.InstancePath);
					}
					return this.m_uniqueName;
				}
				if (this.m_owner.CurrentRenderGroupIndex < 0)
				{
					return string.Empty;
				}
				if (this.m_owner.IsDetailGroup)
				{
					return this.m_owner.TableDetailMember.DetailInstanceUniqueName;
				}
				return this.m_owner.CurrentShimRenderGroup.UniqueName;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00068C28 File Offset: 0x00066E28
		public string DocumentMapLabel
		{
			get
			{
				if (!this.m_documentMapLabelEvaluated)
				{
					this.m_documentMapLabelEvaluated = true;
					if (this.m_owner.IsOldSnapshot)
					{
						if (!this.m_owner.IsDetailGroup && this.m_owner.CurrentRenderGroupIndex >= 0)
						{
							this.m_documentMapLabel = this.m_owner.CurrentShimRenderGroup.Label;
						}
					}
					else if (this.m_owner.MemberDefinition.Grouping != null && this.m_owner.MemberDefinition.Grouping.GroupLabel != null)
					{
						this.m_documentMapLabel = this.m_owner.MemberDefinition.Grouping.EvaluateGroupingLabelExpression(this.ReportScopeInstance, this.m_owner.OwnerDataRegion.RenderingContext.OdpContext);
					}
				}
				return this.m_documentMapLabel;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x00068CF0 File Offset: 0x00066EF0
		public GroupExpressionValueCollection GroupExpressions
		{
			get
			{
				if (this.m_groupExpressions == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (!this.m_owner.IsDetailGroup && this.m_owner.OwnerDataRegion != null && this.m_owner.CurrentRenderGroupIndex >= 0 && this.m_owner.OwnerDataRegion.DataRegionType == Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Chart)
						{
							ChartHeadingInstanceInfo instanceInfo = ((Microsoft.ReportingServices.ReportRendering.ChartMember)this.m_owner.CurrentShimRenderGroup).InstanceInfo;
							if (instanceInfo != null)
							{
								if (this.m_groupExpressions == null)
								{
									this.m_groupExpressions = new GroupExpressionValueCollection();
								}
								this.m_groupExpressions.UpdateValues(instanceInfo.GroupExpressionValue);
							}
						}
					}
					else if (!this.m_owner.IsDetailGroup && this.m_owner.OwnerDataRegion != null && this.m_owner.MemberDefinition.CurrentMemberIndex >= 0)
					{
						object[] groupInstanceExpressionValues = this.m_owner.MemberDefinition.Grouping.GetGroupInstanceExpressionValues(this.ReportScopeInstance, this.m_owner.OwnerDataRegion.RenderingContext.OdpContext);
						if (this.m_groupExpressions == null)
						{
							this.m_groupExpressions = new GroupExpressionValueCollection();
						}
						this.m_groupExpressions.UpdateValues(groupInstanceExpressionValues);
					}
				}
				return this.m_groupExpressions;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x00068E24 File Offset: 0x00067024
		public int RecursiveLevel
		{
			get
			{
				if (this.m_recursiveLevel < 0 && !this.m_owner.IsOldSnapshot)
				{
					this.m_recursiveLevel = this.m_owner.MemberDefinition.Grouping.GetRecursiveLevel(this.ReportScopeInstance, this.m_owner.OwnerDataRegion.RenderingContext.OdpContext);
				}
				return this.m_recursiveLevel;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00068E84 File Offset: 0x00067084
		public string PageName
		{
			get
			{
				if (!this.m_pageNameEvaluated)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						this.m_pageName = null;
					}
					else
					{
						this.m_pageNameEvaluated = true;
						Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = this.m_owner.MemberDefinition.Grouping;
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo pageName = grouping.PageName;
						if (pageName != null)
						{
							if (pageName.IsExpression)
							{
								this.m_pageName = grouping.EvaluatePageName(this.ReportScopeInstance, this.m_owner.OwnerDataRegion.RenderingContext.OdpContext);
							}
							else
							{
								this.m_pageName = pageName.StringValue;
							}
						}
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00068F14 File Offset: 0x00067114
		protected override void ResetInstanceCache()
		{
			this.m_uniqueName = null;
			this.m_documentMapLabelEvaluated = false;
			this.m_documentMapLabel = null;
			this.m_groupExpressions = null;
			this.m_recursiveLevel = -1;
			this.m_pageNameEvaluated = false;
			this.m_pageName = null;
			if (!this.m_owner.IsOldSnapshot && this.m_owner.MemberDefinition.Grouping != null)
			{
				this.m_owner.MemberDefinition.Grouping.ResetReportItemsWithHideDuplicates();
			}
		}

		// Token: 0x04000CE8 RID: 3304
		private string m_uniqueName;

		// Token: 0x04000CE9 RID: 3305
		private bool m_documentMapLabelEvaluated;

		// Token: 0x04000CEA RID: 3306
		private string m_documentMapLabel;

		// Token: 0x04000CEB RID: 3307
		private GroupExpressionValueCollection m_groupExpressions;

		// Token: 0x04000CEC RID: 3308
		private Group m_owner;

		// Token: 0x04000CED RID: 3309
		private int m_recursiveLevel = -1;

		// Token: 0x04000CEE RID: 3310
		private bool m_pageNameEvaluated;

		// Token: 0x04000CEF RID: 3311
		private string m_pageName;
	}
}
