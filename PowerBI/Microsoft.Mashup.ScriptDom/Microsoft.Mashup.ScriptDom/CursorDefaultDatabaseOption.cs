using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200032B RID: 811
	[Serializable]
	internal class CursorDefaultDatabaseOption : DatabaseOption
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x001688FE File Offset: 0x00166AFE
		// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x00168906 File Offset: 0x00166B06
		public bool IsLocal
		{
			get
			{
				return this._isLocal;
			}
			set
			{
				this._isLocal = value;
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0016890F File Offset: 0x00166B0F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0016891B File Offset: 0x00166B1B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C8B RID: 7307
		private bool _isLocal;
	}
}
