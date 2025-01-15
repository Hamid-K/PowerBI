using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D4 RID: 468
	[Serializable]
	internal abstract class ParameterizedDataTypeReference : DataTypeReference
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x0016007C File Offset: 0x0015E27C
		public IList<Literal> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00160084 File Offset: 0x0015E284
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A4E RID: 6734
		private List<Literal> _parameters = new List<Literal>();
	}
}
