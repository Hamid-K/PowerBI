using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200047F RID: 1151
	[Serializable]
	internal class AlterEventSessionStatement : EventSessionStatement
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x00170B86 File Offset: 0x0016ED86
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x00170B8E File Offset: 0x0016ED8E
		public AlterEventSessionStatementType StatementType
		{
			get
			{
				return this._statementType;
			}
			set
			{
				this._statementType = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x00170B97 File Offset: 0x0016ED97
		public IList<EventSessionObjectName> DropEventDeclarations
		{
			get
			{
				return this._dropEventDeclarations;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x00170B9F File Offset: 0x0016ED9F
		public IList<EventSessionObjectName> DropTargetDeclarations
		{
			get
			{
				return this._dropTargetDeclarations;
			}
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x00170BA7 File Offset: 0x0016EDA7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x00170BB4 File Offset: 0x0016EDB4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.DropEventDeclarations.Count;
			while (i < count)
			{
				this.DropEventDeclarations[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.DropTargetDeclarations.Count;
			while (j < count2)
			{
				this.DropTargetDeclarations[j].Accept(visitor);
				j++;
			}
		}

		// Token: 0x04001ED4 RID: 7892
		private AlterEventSessionStatementType _statementType;

		// Token: 0x04001ED5 RID: 7893
		private List<EventSessionObjectName> _dropEventDeclarations = new List<EventSessionObjectName>();

		// Token: 0x04001ED6 RID: 7894
		private List<EventSessionObjectName> _dropTargetDeclarations = new List<EventSessionObjectName>();
	}
}
