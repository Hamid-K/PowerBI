using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E2 RID: 994
	internal abstract class SetOp : RelOp
	{
		// Token: 0x06002EFD RID: 12029 RVA: 0x0009537E File Offset: 0x0009357E
		internal SetOp(OpType opType, VarVec outputs, VarMap left, VarMap right)
			: this(opType)
		{
			this.m_varMap = new VarMap[2];
			this.m_varMap[0] = left;
			this.m_varMap[1] = right;
			this.m_outputVars = outputs;
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000953AD File Offset: 0x000935AD
		protected SetOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000953B6 File Offset: 0x000935B6
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000953B9 File Offset: 0x000935B9
		internal VarMap[] VarMap
		{
			get
			{
				return this.m_varMap;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x000953C1 File Offset: 0x000935C1
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputVars;
			}
		}

		// Token: 0x04000FD6 RID: 4054
		private readonly VarMap[] m_varMap;

		// Token: 0x04000FD7 RID: 4055
		private readonly VarVec m_outputVars;
	}
}
