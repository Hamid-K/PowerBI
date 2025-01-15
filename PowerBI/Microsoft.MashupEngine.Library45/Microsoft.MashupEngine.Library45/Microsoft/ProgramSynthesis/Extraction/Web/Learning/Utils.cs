using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x02001168 RID: 4456
	public static class Utils
	{
		// Token: 0x06008467 RID: 33895 RVA: 0x001BE700 File Offset: 0x001BC900
		public static void GetSubsets(ProgramNode[] superSet, int k, int idx, List<ProgramNode> current, List<ProgramNode[]> solution)
		{
			if (current.Count == k)
			{
				solution.Add(current.ToArray());
				return;
			}
			if (idx == superSet.Length)
			{
				return;
			}
			ProgramNode programNode = superSet[idx];
			current.Add(programNode);
			Utils.GetSubsets(superSet, k, idx + 1, current, solution);
			current.RemoveAt(current.Count - 1);
			Utils.GetSubsets(superSet, k, idx + 1, current, solution);
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x001BE760 File Offset: 0x001BC960
		public static List<ProgramNode[]> GetSubsets(ProgramNode[] superSet, int k)
		{
			List<ProgramNode[]> list = new List<ProgramNode[]>();
			Utils.GetSubsets(superSet, k, 0, new List<ProgramNode>(), list);
			return list;
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x001BE782 File Offset: 0x001BC982
		public static int GetProgramSize<T>(T program) where T : IProgramNodeBuilder
		{
			return Utils.GetProgramSize(program.Node);
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x001BE798 File Offset: 0x001BC998
		public static int GetProgramSize(ProgramNode program)
		{
			int num = 1;
			foreach (ProgramNode programNode in program.Children)
			{
				num += Utils.GetProgramSize(programNode);
			}
			return num;
		}
	}
}
