using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.Utils;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A2 RID: 674
	internal abstract class InternalStreamingOdpDynamicMemberLogicBase : InternalDynamicMemberLogic
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x000691EC File Offset: 0x000673EC
		protected InternalStreamingOdpDynamicMemberLogicBase(DataRegionMember memberDef, OnDemandProcessingContext odpContext)
		{
			this.m_memberDef = memberDef;
			this.m_sorting = this.m_memberDef.DataRegionMemberDefinition.Sorting;
			this.m_grouping = this.m_memberDef.DataRegionMemberDefinition.Grouping;
			this.m_memberGroupAndSortExpressionFlag = this.m_memberDef.DataRegionMemberDefinition.MemberGroupAndSortExpressionFlag;
			this.m_odpContext = odpContext;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00069250 File Offset: 0x00067450
		public override void ResetContext()
		{
			this.m_isNewContext = true;
			this.m_currentContext = -1;
			this.m_scopeID = null;
			this.m_memberDef.DataRegionMemberDefinition.InstanceCount = -1;
			this.m_memberDef.DataRegionMemberDefinition.InstancePathItem.ResetContext();
			((IRIFReportDataScope)this.m_memberDef.ReportScope.RIFReportScope).ClearStreamingScopeInstanceBinding();
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000692B4 File Offset: 0x000674B4
		protected bool MoveNextCore(global::System.Action actionOnNextInstance)
		{
			IRIFReportDataScope irifreportDataScope = (IRIFReportDataScope)this.m_memberDef.ReportScope.RIFReportScope;
			if (irifreportDataScope.IsBoundToStreamingScopeInstance)
			{
				this.m_odpContext.BindNextMemberInstance(this.m_memberDef.DataRegionMemberDefinition, this.m_memberDef.ReportScopeInstance, this.m_currentContext + 1);
			}
			else
			{
				this.m_odpContext.SetupContext(this.m_memberDef.DataRegionMemberDefinition, this.m_memberDef.ReportScopeInstance, -1);
			}
			if (irifreportDataScope.CurrentStreamingScopeInstance.Value().IsNoRows)
			{
				return false;
			}
			if (actionOnNextInstance != null)
			{
				actionOnNextInstance();
			}
			this.m_isNewContext = true;
			this.m_currentContext++;
			this.m_memberDef.DataRegionMemberDefinition.InstancePathItem.MoveNext();
			this.m_memberDef.SetNewContext(true);
			return true;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00069380 File Offset: 0x00067580
		public override bool SetInstanceIndex(int index)
		{
			this.ResetContext();
			if (index < 0)
			{
				return true;
			}
			int num = -1;
			while (num < index && this.MoveNext())
			{
				num++;
			}
			return num == index;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000693B4 File Offset: 0x000675B4
		internal override ScopeID GetScopeID()
		{
			DataShapeMember dataShapeMember = this.m_memberDef as DataShapeMember;
			bool flag = dataShapeMember != null && dataShapeMember.HasRestartDefinition();
			bool flag2 = this.m_grouping.ScopeIDDefinition != null && this.m_grouping.ScopeIDDefinition.OmitScopeIdFromDataShapeResult;
			if (flag2 && !flag)
			{
				return null;
			}
			bool flag3 = !flag;
			if (this.m_grouping.IsDetail)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsDetailGroupsNotSupportedInStreamingMode, new object[] { "GetScopeID" });
			}
			if (this.m_scopeID != null)
			{
				return this.m_scopeID;
			}
			this.m_odpContext.SetupContext(this.m_memberDef.DataRegionMemberDefinition, this.m_memberDef.ReportScopeInstance);
			IRIFReportDataScope irifreportDataScope = (IRIFReportDataScope)this.m_memberDef.ReportScope.RIFReportScope;
			if (irifreportDataScope.IsBoundToStreamingScopeInstance && !irifreportDataScope.CurrentStreamingScopeInstance.Value().IsNoRows)
			{
				IOnDemandMemberInstance onDemandMemberInstance = (IOnDemandMemberInstance)irifreportDataScope.CurrentStreamingScopeInstance.Value();
				List<ScopeValue> list;
				if (this.m_grouping.ScopeIDDefinition != null)
				{
					list = this.EvaluateScopeValues(this.m_grouping.ScopeIDDefinition, flag3);
				}
				else
				{
					list = this.EvaluateSortAndGroupExpressionValues(onDemandMemberInstance.GroupExprValues);
				}
				this.m_scopeID = new ScopeID((list == null) ? null : list.ToArray());
			}
			if (flag)
			{
				this.m_lastScopeID = this.m_scopeID;
			}
			if (flag2)
			{
				return null;
			}
			return this.m_scopeID;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00069512 File Offset: 0x00067712
		internal override ScopeID GetLastScopeID()
		{
			return this.m_lastScopeID;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0006951A File Offset: 0x0006771A
		private List<ScopeValue> EvaluateSortAndGroupExpressionValues(List<object> groupExpressionValues)
		{
			if (this.m_memberGroupAndSortExpressionFlag == null)
			{
				return null;
			}
			return this.AddSortAndGroupExpressionValues(groupExpressionValues);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00069530 File Offset: 0x00067730
		private List<ScopeValue> EvaluateScopeValues(ScopeIDDefinition scopeIDDefinition, bool computeStringKeys)
		{
			if (scopeIDDefinition == null)
			{
				return null;
			}
			List<ScopeValueDefinition> scopeValues = scopeIDDefinition.ScopeValues;
			if (scopeValues == null)
			{
				return null;
			}
			StringKeyGenerator stringKeyGenerator = this.m_odpContext.StringKeyGenerator;
			List<ScopeValue> list = new List<ScopeValue>(scopeValues.Count);
			for (int i = 0; i < scopeValues.Count; i++)
			{
				ScopeValue scopeValue = this.CreateScopeValueFromDefinition(scopeValues[i], stringKeyGenerator, computeStringKeys);
				list.Add(scopeValue);
			}
			return list;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00069594 File Offset: 0x00067794
		private ScopeValue CreateScopeValueFromDefinition(ScopeValueDefinition scopeValueDefinition, StringKeyGenerator keyGenerator, bool computeStringKey)
		{
			object obj = this.m_odpContext.ReportRuntime.EvaluateScopeValueDefinition(scopeValueDefinition, this.m_memberDef.DataRegionMemberDefinition.Grouping.Name);
			string text = null;
			if (computeStringKey)
			{
				text = keyGenerator.GetKey(obj);
			}
			return new ScopeValue(obj, ScopeIDType.None, text);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000695E0 File Offset: 0x000677E0
		private List<ScopeValue> AddSortAndGroupExpressionValues(List<object> groupExpValues)
		{
			List<ScopeValue> list = new List<ScopeValue>(this.m_memberGroupAndSortExpressionFlag.Count);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.m_memberGroupAndSortExpressionFlag.Count; i++)
			{
				ScopeValue scopeValue = null;
				switch (this.m_memberGroupAndSortExpressionFlag[i])
				{
				case ScopeIDType.SortValues:
					scopeValue = this.CreateScopeValueFromSortExpression(num, this.m_memberGroupAndSortExpressionFlag[i]);
					num++;
					break;
				case ScopeIDType.GroupValues:
					scopeValue = new ScopeValue(this.NormalizeValue(groupExpValues[num2]), this.m_memberGroupAndSortExpressionFlag[i]);
					num2++;
					break;
				case ScopeIDType.SortGroup:
					if (groupExpValues.Count == 1)
					{
						scopeValue = new ScopeValue(this.NormalizeValue(groupExpValues[num2]), this.m_memberGroupAndSortExpressionFlag[i]);
					}
					else
					{
						scopeValue = this.CreateScopeValueFromSortExpression(num, this.m_memberGroupAndSortExpressionFlag[i]);
					}
					num2++;
					num++;
					break;
				}
				if (scopeValue != null)
				{
					list.Add(scopeValue);
				}
			}
			return list;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000696E8 File Offset: 0x000678E8
		private ScopeValue CreateScopeValueFromSortExpression(int sortCursor, ScopeIDType scopeIdType)
		{
			RuntimeExpressionInfo runtimeExpressionInfo = new RuntimeExpressionInfo(this.m_sorting.SortExpressions, this.m_sorting.ExprHost, this.m_sorting.SortDirections, sortCursor);
			object obj = this.m_odpContext.ReportRuntime.EvaluateRuntimeExpression(runtimeExpressionInfo, ObjectType.Grouping, this.m_memberDef.DataRegionMemberDefinition.Grouping.Name, "Sort");
			return new ScopeValue(this.NormalizeValue(obj), scopeIdType);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00069758 File Offset: 0x00067958
		private object NormalizeValue(object value)
		{
			if (value is DBNull)
			{
				return null;
			}
			return value;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00069765 File Offset: 0x00067965
		protected void ResetScopeID()
		{
			this.m_scopeID = null;
		}

		// Token: 0x04000CF5 RID: 3317
		protected readonly DataRegionMember m_memberDef;

		// Token: 0x04000CF6 RID: 3318
		protected readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04000CF7 RID: 3319
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.Grouping m_grouping;

		// Token: 0x04000CF8 RID: 3320
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.Sorting m_sorting;

		// Token: 0x04000CF9 RID: 3321
		private readonly List<ScopeIDType> m_memberGroupAndSortExpressionFlag;

		// Token: 0x04000CFA RID: 3322
		private ScopeID m_scopeID;

		// Token: 0x04000CFB RID: 3323
		private ScopeID m_lastScopeID;
	}
}
