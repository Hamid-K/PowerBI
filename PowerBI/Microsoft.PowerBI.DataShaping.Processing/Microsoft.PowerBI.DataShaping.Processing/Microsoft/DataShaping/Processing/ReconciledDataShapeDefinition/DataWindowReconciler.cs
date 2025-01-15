using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Reconciliation;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200003F RID: 63
	internal sealed class DataWindowReconciler
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00005C70 File Offset: 0x00003E70
		internal static void ReconcileWindowsAndMembers(DataWindow legacyWindow, DataWindows windows, IList<DataMember> primaryHierarchy, IList<DataMember> secondaryHierarchy, ScopeLookupTable scopeTable)
		{
			if (windows == null && legacyWindow == null)
			{
				return;
			}
			IReadOnlyList<DataWindow> readOnlyList;
			if (legacyWindow != null)
			{
				readOnlyList = DataWindowReconciler.ReconcileLegacyWindow(legacyWindow, primaryHierarchy);
			}
			else
			{
				readOnlyList = DataWindowReconciler.ReconcileWindows((windows != null) ? windows.Windows : null, scopeTable);
			}
			DataWindowReconciler.AssignWindowIndexesForCollection(secondaryHierarchy, readOnlyList);
			DataWindowReconciler.AssignWindowIndexesForCollection(primaryHierarchy, readOnlyList);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005CB4 File Offset: 0x00003EB4
		private static IReadOnlyList<DataWindow> ReconcileLegacyWindow(DataWindow legacyWindow, IList<DataMember> primaryHierarchy)
		{
			if (legacyWindow == null)
			{
				return null;
			}
			legacyWindow.TargetScopes = new List<Scope>();
			legacyWindow.TargetScopeIds = new List<string>();
			legacyWindow.AppliesToScopeIds = new List<string>();
			legacyWindow.AppliesToScopes = new List<Scope>();
			Stack<DataMember> stack = new Stack<DataMember>(primaryHierarchy);
			while (!stack.IsNullOrEmpty<DataMember>())
			{
				DataMember dataMember = stack.Pop();
				if (dataMember.IsCountedForDataWindow)
				{
					legacyWindow.TargetScopes.Add(dataMember);
					legacyWindow.TargetScopeIds.Add(dataMember.Id);
				}
				legacyWindow.AppliesToScopeIds.Add(dataMember.Id);
				legacyWindow.AppliesToScopes.Add(dataMember);
				if (!dataMember.DataMembers.IsNullOrEmpty<DataMember>())
				{
					foreach (DataMember dataMember2 in dataMember.DataMembers)
					{
						stack.Push(dataMember2);
					}
				}
			}
			return new List<DataWindow> { legacyWindow };
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005DAC File Offset: 0x00003FAC
		private static IReadOnlyList<DataWindow> ReconcileWindows(IReadOnlyList<DataWindow> windows, ScopeLookupTable scopeTable)
		{
			if (windows.IsNullOrEmpty<DataWindow>())
			{
				return null;
			}
			foreach (DataWindow dataWindow in windows)
			{
				dataWindow.TargetScopes = new List<Scope>(dataWindow.TargetScopeIds.Count);
				foreach (string text in dataWindow.TargetScopeIds)
				{
					dataWindow.TargetScopes.Add(scopeTable.Get(text));
				}
				dataWindow.AppliesToScopes = new List<Scope>(dataWindow.AppliesToScopeIds.Count);
				foreach (string text2 in dataWindow.AppliesToScopeIds)
				{
					dataWindow.AppliesToScopes.Add(scopeTable.Get(text2));
				}
			}
			return windows;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005EBC File Offset: 0x000040BC
		internal static void AssignWindowIndexesForCollection(IList<DataMember> members, IReadOnlyList<DataWindow> windows)
		{
			if (members.IsNullOrEmpty<DataMember>())
			{
				return;
			}
			foreach (DataMember dataMember in members)
			{
				DataWindowReconciler.AssignWindowIndexesForMember(dataMember, windows);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005F0C File Offset: 0x0000410C
		private static void AssignWindowIndexesForMember(DataMember member, IReadOnlyList<DataWindow> windows)
		{
			List<int> list = null;
			for (int i = 0; i < windows.Count; i++)
			{
				if (windows[i].AppliesToScopes.Contains(member))
				{
					Util.AddToLazyList<int>(ref list, i);
				}
			}
			if (list != null)
			{
				member.ApplicableWindows = list;
			}
			DataWindowReconciler.AssignWindowIndexesForCollection(member.DataMembers, windows);
		}
	}
}
