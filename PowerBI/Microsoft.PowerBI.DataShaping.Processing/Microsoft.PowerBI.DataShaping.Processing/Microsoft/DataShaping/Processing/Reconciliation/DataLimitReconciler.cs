using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x0200001C RID: 28
	internal sealed class DataLimitReconciler
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x0000378C File Offset: 0x0000198C
		internal static void ReconcileLimitsAndMembers(DataLimits limits, IList<DataMember> primaryHierarchy, IList<DataMember> secondaryHierarchy, ScopeLookupTable scopeTable)
		{
			if (limits == null || limits.Limits.IsNullOrEmpty<DataLimit>())
			{
				return;
			}
			IList<DataLimit> limits2 = limits.Limits;
			foreach (DataLimit dataLimit in limits2)
			{
				dataLimit.TargetScopes = new List<Scope>(dataLimit.TargetScopeIds.Count);
				foreach (string text in dataLimit.TargetScopeIds)
				{
					dataLimit.TargetScopes.Add(scopeTable.Get(text));
				}
				dataLimit.WithinScope = scopeTable.Get(dataLimit.WithinScopeId);
				dataLimit.AppliesToScopes = new List<Scope>(dataLimit.AppliesToScopeIds.Count);
				foreach (string text2 in dataLimit.AppliesToScopeIds)
				{
					dataLimit.AppliesToScopes.Add(scopeTable.Get(text2));
				}
			}
			DataLimitReconciler.AssignLimitIndexesForCollection(secondaryHierarchy, limits2, LimitTelemetryRole.Secondary);
			DataLimitReconciler.AssignLimitIndexesForCollection(primaryHierarchy, limits2, LimitTelemetryRole.Primary);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000038CC File Offset: 0x00001ACC
		private static void AssignLimitIndexesForMember(DataMember member, IList<DataLimit> limits, LimitTelemetryRole role)
		{
			if (member == null || limits.IsNullOrEmpty<DataLimit>())
			{
				return;
			}
			List<int> list = null;
			List<int> list2 = null;
			for (int i = 0; i < limits.Count; i++)
			{
				DataLimit dataLimit = limits[i];
				DataLimitReconciler.ReconcileLimit(member, ref list, ref list2, i, dataLimit, role);
			}
			if (list != null)
			{
				member.ApplicableLimits = list;
			}
			if (list2 != null)
			{
				member.WithinLimits = list2;
			}
			DataLimitReconciler.AssignLimitIndexesForCollection(member.DataMembers, limits, role);
			DataLimitReconciler.AssignLimitIndexesForCollection(member.Intersections, limits);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000393C File Offset: 0x00001B3C
		private static void ReconcileLimit(DataMember member, ref List<int> applicableLimitIndexes, ref List<int> withinLimitIndexes, int index, DataLimit limit, LimitTelemetryRole category)
		{
			if (limit.AppliesToScopes.Contains(member))
			{
				Util.AddToLazyList<int>(ref applicableLimitIndexes, index);
				DataLimitReconciler.AssignRole(limit, category);
				return;
			}
			if (limit.WithinScope == member)
			{
				Util.AddToLazyList<int>(ref withinLimitIndexes, index);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003970 File Offset: 0x00001B70
		internal static void AssignLimitIndexesForCollection(IEnumerable<DataMember> members, IList<DataLimit> limits, LimitTelemetryRole role)
		{
			if (members.IsNullOrEmpty<DataMember>() || limits.IsNullOrEmpty<DataLimit>())
			{
				return;
			}
			foreach (DataMember dataMember in members)
			{
				DataLimitReconciler.AssignLimitIndexesForMember(dataMember, limits, role);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000039C8 File Offset: 0x00001BC8
		private static void AssignLimitIndexesForCollection(IEnumerable<DataIntersection> intersections, IList<DataLimit> limits)
		{
			if (intersections.IsNullOrEmpty<DataIntersection>())
			{
				return;
			}
			foreach (DataIntersection dataIntersection in intersections)
			{
				DataLimitReconciler.AssignLimitIndexesForIntersection(dataIntersection, limits);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003A18 File Offset: 0x00001C18
		internal static void AssignLimitIndexesForIntersection(DataIntersection intersection, IList<DataLimit> limits)
		{
			if (intersection == null)
			{
				return;
			}
			List<int> list = null;
			for (int i = 0; i < limits.Count; i++)
			{
				DataLimit dataLimit = limits[i];
				if (dataLimit.AppliesToScopes.Contains(intersection))
				{
					if (list == null)
					{
						list = new List<int>();
						intersection.ApplicableLimits = list;
					}
					list.Add(i);
					DataLimitReconciler.AssignRole(dataLimit, LimitTelemetryRole.Intersection);
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003A70 File Offset: 0x00001C70
		private static void AssignRole(DataLimit limit, LimitTelemetryRole role)
		{
			if (limit.Role == LimitTelemetryRole.None)
			{
				limit.Role = role;
			}
		}
	}
}
