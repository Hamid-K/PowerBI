using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000475 RID: 1141
	[Serializable]
	internal class EventDeclarationSetParameter : TSqlFragment
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x00170846 File Offset: 0x0016EA46
		// (set) Token: 0x060032C6 RID: 12998 RVA: 0x0017084E File Offset: 0x0016EA4E
		public Identifier EventField
		{
			get
			{
				return this._eventField;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._eventField = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x0017085E File Offset: 0x0016EA5E
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x00170866 File Offset: 0x0016EA66
		public ScalarExpression EventValue
		{
			get
			{
				return this._eventValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._eventValue = value;
			}
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00170876 File Offset: 0x0016EA76
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x00170882 File Offset: 0x0016EA82
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.EventField != null)
			{
				this.EventField.Accept(visitor);
			}
			if (this.EventValue != null)
			{
				this.EventValue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EC4 RID: 7876
		private Identifier _eventField;

		// Token: 0x04001EC5 RID: 7877
		private ScalarExpression _eventValue;
	}
}
