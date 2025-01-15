using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C1 RID: 1729
	public sealed class DbGroupExpressionBinding
	{
		// Token: 0x060050D0 RID: 20688 RVA: 0x00121FB8 File Offset: 0x001201B8
		internal DbGroupExpressionBinding(DbExpression input, DbVariableReferenceExpression inputRef, DbVariableReferenceExpression groupRef)
		{
			this._expr = input;
			this._varRef = inputRef;
			this._groupVarRef = groupRef;
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x060050D1 RID: 20689 RVA: 0x00121FD5 File Offset: 0x001201D5
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x060050D2 RID: 20690 RVA: 0x00121FDD File Offset: 0x001201DD
		public string VariableName
		{
			get
			{
				return this._varRef.VariableName;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x060050D3 RID: 20691 RVA: 0x00121FEA File Offset: 0x001201EA
		public TypeUsage VariableType
		{
			get
			{
				return this._varRef.ResultType;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x00121FF7 File Offset: 0x001201F7
		public DbVariableReferenceExpression Variable
		{
			get
			{
				return this._varRef;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x00121FFF File Offset: 0x001201FF
		public string GroupVariableName
		{
			get
			{
				return this._groupVarRef.VariableName;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x0012200C File Offset: 0x0012020C
		public TypeUsage GroupVariableType
		{
			get
			{
				return this._groupVarRef.ResultType;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x00122019 File Offset: 0x00120219
		public DbVariableReferenceExpression GroupVariable
		{
			get
			{
				return this._groupVarRef;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060050D8 RID: 20696 RVA: 0x00122021 File Offset: 0x00120221
		public DbGroupAggregate GroupAggregate
		{
			get
			{
				if (this._groupAggregate == null)
				{
					this._groupAggregate = DbExpressionBuilder.GroupAggregate(this.GroupVariable);
				}
				return this._groupAggregate;
			}
		}

		// Token: 0x04001D99 RID: 7577
		private readonly DbExpression _expr;

		// Token: 0x04001D9A RID: 7578
		private readonly DbVariableReferenceExpression _varRef;

		// Token: 0x04001D9B RID: 7579
		private readonly DbVariableReferenceExpression _groupVarRef;

		// Token: 0x04001D9C RID: 7580
		private DbGroupAggregate _groupAggregate;
	}
}
