using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200030C RID: 780
	[Serializable]
	internal class SetCommandStatement : TSqlStatement
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x00167CD0 File Offset: 0x00165ED0
		public IList<SetCommand> Commands
		{
			get
			{
				return this._commands;
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00167CD8 File Offset: 0x00165ED8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00167CE4 File Offset: 0x00165EE4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Commands.Count;
			while (i < count)
			{
				this.Commands[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C50 RID: 7248
		private List<SetCommand> _commands = new List<SetCommand>();
	}
}
