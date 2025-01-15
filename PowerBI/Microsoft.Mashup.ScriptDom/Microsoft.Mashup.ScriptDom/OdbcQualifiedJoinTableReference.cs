using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C3 RID: 963
	[Serializable]
	internal class OdbcQualifiedJoinTableReference : TableReference
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x0016CC2C File Offset: 0x0016AE2C
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x0016CC34 File Offset: 0x0016AE34
		public TableReference TableReference
		{
			get
			{
				return this._tableReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableReference = value;
			}
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x0016CC44 File Offset: 0x0016AE44
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x0016CC50 File Offset: 0x0016AE50
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.TableReference != null)
			{
				this.TableReference.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DBF RID: 7615
		private TableReference _tableReference;
	}
}
