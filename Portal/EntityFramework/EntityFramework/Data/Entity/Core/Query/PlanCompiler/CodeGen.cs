using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000333 RID: 819
	internal class CodeGen
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x000712E7 File Offset: 0x0006F4E7
		internal static void Process(PlanCompiler compilerState, out List<ProviderCommandInfo> childCommands, out ColumnMap resultColumnMap, out int columnCount)
		{
			new CodeGen(compilerState).Process(out childCommands, out resultColumnMap, out columnCount);
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000712F7 File Offset: 0x0006F4F7
		private CodeGen(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x00071308 File Offset: 0x0006F508
		private void Process(out List<ProviderCommandInfo> childCommands, out ColumnMap resultColumnMap, out int columnCount)
		{
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)this.Command.Root.Op;
			this.m_subCommands = new List<Node>(new Node[] { this.Command.Root });
			childCommands = new List<ProviderCommandInfo>(new ProviderCommandInfo[] { ProviderCommandInfoUtils.Create(this.Command, this.Command.Root) });
			resultColumnMap = this.BuildResultColumnMap(physicalProjectOp);
			columnCount = physicalProjectOp.Outputs.Count;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00071388 File Offset: 0x0006F588
		private ColumnMap BuildResultColumnMap(PhysicalProjectOp projectOp)
		{
			Dictionary<Var, KeyValuePair<int, int>> dictionary = this.BuildVarMap();
			return ColumnMapTranslator.Translate(projectOp.ColumnMap, dictionary);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000713A8 File Offset: 0x0006F5A8
		private Dictionary<Var, KeyValuePair<int, int>> BuildVarMap()
		{
			Dictionary<Var, KeyValuePair<int, int>> dictionary = new Dictionary<Var, KeyValuePair<int, int>>();
			int num = 0;
			foreach (Node node in this.m_subCommands)
			{
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)node.Op;
				int num2 = 0;
				foreach (Var var in physicalProjectOp.Outputs)
				{
					KeyValuePair<int, int> keyValuePair = new KeyValuePair<int, int>(num, num2);
					dictionary[var] = keyValuePair;
					num2++;
				}
				num++;
			}
			return dictionary;
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x00071460 File Offset: 0x0006F660
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x04000DA1 RID: 3489
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04000DA2 RID: 3490
		private List<Node> m_subCommands;
	}
}
