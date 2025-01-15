using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200032C RID: 812
	[Serializable]
	internal class RecoveryDatabaseOption : DatabaseOption
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x0016892C File Offset: 0x00166B2C
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x00168934 File Offset: 0x00166B34
		public RecoveryDatabaseOptionKind Value
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

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0016893D File Offset: 0x00166B3D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00168949 File Offset: 0x00166B49
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C8C RID: 7308
		private RecoveryDatabaseOptionKind _value;
	}
}
