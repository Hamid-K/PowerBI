using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B6 RID: 1718
	public sealed class DbExpressionBinding
	{
		// Token: 0x06005054 RID: 20564 RVA: 0x00121D02 File Offset: 0x0011FF02
		internal DbExpressionBinding(DbExpression input, DbVariableReferenceExpression varRef)
		{
			this._expr = input;
			this._varRef = varRef;
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005055 RID: 20565 RVA: 0x00121D18 File Offset: 0x0011FF18
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x00121D20 File Offset: 0x0011FF20
		public string VariableName
		{
			get
			{
				return this._varRef.VariableName;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005057 RID: 20567 RVA: 0x00121D2D File Offset: 0x0011FF2D
		public TypeUsage VariableType
		{
			get
			{
				return this._varRef.ResultType;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x00121D3A File Offset: 0x0011FF3A
		public DbVariableReferenceExpression Variable
		{
			get
			{
				return this._varRef;
			}
		}

		// Token: 0x04001D4D RID: 7501
		private readonly DbExpression _expr;

		// Token: 0x04001D4E RID: 7502
		private readonly DbVariableReferenceExpression _varRef;
	}
}
