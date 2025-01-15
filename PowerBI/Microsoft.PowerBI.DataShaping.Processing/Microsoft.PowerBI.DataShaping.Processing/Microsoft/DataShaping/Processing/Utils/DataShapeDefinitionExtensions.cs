using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Utils
{
	// Token: 0x02000017 RID: 23
	internal static class DataShapeDefinitionExtensions
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00002F9C File Offset: 0x0000119C
		internal static bool TargetsCoverWholeHierarchy(this IList<Scope> windowTargets, IList<DataMember> hierarchyMembers)
		{
			List<DataMember> list = new List<DataMember>();
			Stack<DataMember> stack = new Stack<DataMember>(hierarchyMembers);
			while (stack.Count != 0)
			{
				DataMember dataMember = stack.Pop();
				if (dataMember.IsCountedForDataWindow)
				{
					list.Add(dataMember);
				}
				if (!dataMember.DataMembers.IsNullOrEmpty<DataMember>())
				{
					foreach (DataMember dataMember2 in dataMember.DataMembers)
					{
						stack.Push(dataMember2);
					}
				}
			}
			return windowTargets.Count == list.Count && windowTargets.All(new Func<Scope, bool>(list.Contains<Scope>));
		}
	}
}
