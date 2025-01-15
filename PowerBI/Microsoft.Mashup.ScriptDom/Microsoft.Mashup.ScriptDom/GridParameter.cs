using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	internal class GridParameter : TSqlFragment
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x00170F59 File Offset: 0x0016F159
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x00170F61 File Offset: 0x0016F161
		public GridParameterType Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x00170F6A File Offset: 0x0016F16A
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x00170F72 File Offset: 0x0016F172
		public ImportanceParameterType Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x00170F7B File Offset: 0x0016F17B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x00170F87 File Offset: 0x0016F187
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE4 RID: 7908
		private GridParameterType _parameter;

		// Token: 0x04001EE5 RID: 7909
		private ImportanceParameterType _value;
	}
}
