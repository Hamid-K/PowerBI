using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000650 RID: 1616
	public sealed class FunctionDefinition
	{
		// Token: 0x06004DD3 RID: 19923 RVA: 0x00117EF3 File Offset: 0x001160F3
		internal FunctionDefinition(string name, DbLambda lambda, int startPosition, int endPosition)
		{
			this._name = name;
			this._lambda = lambda;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x00117F18 File Offset: 0x00116118
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06004DD5 RID: 19925 RVA: 0x00117F20 File Offset: 0x00116120
		public DbLambda Lambda
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x00117F28 File Offset: 0x00116128
		public int StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x00117F30 File Offset: 0x00116130
		public int EndPosition
		{
			get
			{
				return this._endPosition;
			}
		}

		// Token: 0x04001C2C RID: 7212
		private readonly string _name;

		// Token: 0x04001C2D RID: 7213
		private readonly DbLambda _lambda;

		// Token: 0x04001C2E RID: 7214
		private readonly int _startPosition;

		// Token: 0x04001C2F RID: 7215
		private readonly int _endPosition;
	}
}
