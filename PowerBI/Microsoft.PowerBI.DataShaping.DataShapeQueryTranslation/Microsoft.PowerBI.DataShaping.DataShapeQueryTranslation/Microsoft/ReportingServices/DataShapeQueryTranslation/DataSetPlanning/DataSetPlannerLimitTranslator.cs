using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F9 RID: 249
	internal sealed class DataSetPlannerLimitTranslator
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x00025ED1 File Offset: 0x000240D1
		private DataSetPlannerLimitTranslator(ScopeTree scopeTree, DataShapeAnnotations annotations)
		{
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00025EE8 File Offset: 0x000240E8
		public static List<DataSetPlanInfo> Translate(List<DataSetPlanInfo> dataSetPlanInfos, ScopeTree scopeTree, DataShapeAnnotations annotations)
		{
			DataSetPlannerLimitTranslator dataSetPlannerLimitTranslator = new DataSetPlannerLimitTranslator(scopeTree, annotations);
			int count = dataSetPlanInfos.Count;
			List<DataSetPlanInfo> list = new List<DataSetPlanInfo>(count);
			for (int i = 0; i < count; i++)
			{
				DataSetPlanInfo dataSetPlanInfo = dataSetPlanInfos[i];
				DataSetPlanInfo dataSetPlanInfo2 = dataSetPlannerLimitTranslator.TranslateLimits(dataSetPlanInfo);
				list.Add(dataSetPlanInfo2);
			}
			return list;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00025F34 File Offset: 0x00024134
		private DataSetPlanInfo TranslateLimits(DataSetPlanInfo planInfo)
		{
			List<ContextElement> elements = planInfo.Elements;
			List<ContextElement> list = new List<ContextElement>(elements);
			for (int i = 0; i < elements.Count; i++)
			{
				ContextElement contextElement = elements[i];
				if (contextElement.Content.ObjectType == ObjectType.DataMember)
				{
					Limit limit = contextElement.Limit;
					if (limit != null && limit.Operator.ObjectType == ObjectType.LastLimitOperator)
					{
						Limit limit2 = DataSetPlannerLimitTranslator.RewriteLastLimit(limit);
						list[i] = contextElement.AttachLimit(limit2);
						this.ReverseDataMemberSortDirections(list, (DataMember)contextElement.Content, i);
					}
				}
			}
			return planInfo.ReplaceElements(list);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00025FC8 File Offset: 0x000241C8
		internal static Limit RewriteLastLimit(Limit limit)
		{
			LastLimitOperator lastLimitOperator = (LastLimitOperator)limit.Operator;
			return new Limit
			{
				Id = limit.Id,
				Within = limit.Within,
				Targets = limit.Targets,
				Operator = new FirstLimitOperator
				{
					Count = lastLimitOperator.Count
				}
			};
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00026024 File Offset: 0x00024224
		private void ReverseDataMemberSortDirections(List<ContextElement> elements, DataMember member, int index)
		{
			DataShape containingDataShape = this.m_annotations.GetContainingDataShape(member);
			List<IScope> list = this.m_scopeTree.GetContinuousLinearScopeNodesBetween(member, containingDataShape).ToList<IScope>();
			int num = 0;
			foreach (IScope scope in list)
			{
				if (scope.ObjectType == ObjectType.DataMember)
				{
					ContextElement contextElement = elements[num];
					while (contextElement.Content.ObjectType != ObjectType.DataMember || contextElement.Content != scope)
					{
						num++;
						contextElement = elements[num];
					}
					elements[num] = contextElement.ReverseSortDirection();
					num++;
				}
			}
		}

		// Token: 0x040004BC RID: 1212
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040004BD RID: 1213
		private readonly DataShapeAnnotations m_annotations;
	}
}
