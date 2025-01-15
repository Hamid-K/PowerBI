using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000472 RID: 1138
	[Serializable]
	internal class EventSessionStatement : TSqlStatement
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x0017061A File Offset: 0x0016E81A
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x00170622 File Offset: 0x0016E822
		public Identifier Name
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

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x00170632 File Offset: 0x0016E832
		public IList<EventDeclaration> EventDeclarations
		{
			get
			{
				return this._eventDeclarations;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x0017063A File Offset: 0x0016E83A
		public IList<TargetDeclaration> TargetDeclarations
		{
			get
			{
				return this._targetDeclarations;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x00170642 File Offset: 0x0016E842
		public IList<SessionOption> SessionOptions
		{
			get
			{
				return this._sessionOptions;
			}
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x0017064A File Offset: 0x0016E84A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x00170658 File Offset: 0x0016E858
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.EventDeclarations.Count;
			while (i < count)
			{
				this.EventDeclarations[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.TargetDeclarations.Count;
			while (j < count2)
			{
				this.TargetDeclarations[j].Accept(visitor);
				j++;
			}
			int k = 0;
			int count3 = this.SessionOptions.Count;
			while (k < count3)
			{
				this.SessionOptions[k].Accept(visitor);
				k++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EBC RID: 7868
		private Identifier _name;

		// Token: 0x04001EBD RID: 7869
		private List<EventDeclaration> _eventDeclarations = new List<EventDeclaration>();

		// Token: 0x04001EBE RID: 7870
		private List<TargetDeclaration> _targetDeclarations = new List<TargetDeclaration>();

		// Token: 0x04001EBF RID: 7871
		private List<SessionOption> _sessionOptions = new List<SessionOption>();
	}
}
