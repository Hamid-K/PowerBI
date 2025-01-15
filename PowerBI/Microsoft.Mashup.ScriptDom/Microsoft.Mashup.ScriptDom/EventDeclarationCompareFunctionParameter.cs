using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000477 RID: 1143
	[Serializable]
	internal class EventDeclarationCompareFunctionParameter : BooleanExpression
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x00170904 File Offset: 0x0016EB04
		// (set) Token: 0x060032D2 RID: 13010 RVA: 0x0017090C File Offset: 0x0016EB0C
		public EventSessionObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x0017091C File Offset: 0x0016EB1C
		// (set) Token: 0x060032D4 RID: 13012 RVA: 0x00170924 File Offset: 0x0016EB24
		public SourceDeclaration SourceDeclaration
		{
			get
			{
				return this._sourceDeclaration;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceDeclaration = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x00170934 File Offset: 0x0016EB34
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x0017093C File Offset: 0x0016EB3C
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

		// Token: 0x060032D7 RID: 13015 RVA: 0x0017094C File Offset: 0x0016EB4C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x00170958 File Offset: 0x0016EB58
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.SourceDeclaration != null)
			{
				this.SourceDeclaration.Accept(visitor);
			}
			if (this.EventValue != null)
			{
				this.EventValue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EC7 RID: 7879
		private EventSessionObjectName _name;

		// Token: 0x04001EC8 RID: 7880
		private SourceDeclaration _sourceDeclaration;

		// Token: 0x04001EC9 RID: 7881
		private ScalarExpression _eventValue;
	}
}
