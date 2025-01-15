using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	internal class ResultSetsExecuteOption : ExecuteOption
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0015EDD7 File Offset: 0x0015CFD7
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x0015EDDF File Offset: 0x0015CFDF
		public ResultSetsOptionKind ResultSetsOptionKind
		{
			get
			{
				return this._resultSetsOptionKind;
			}
			set
			{
				this._resultSetsOptionKind = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0015EDE8 File Offset: 0x0015CFE8
		public IList<ResultSetDefinition> Definitions
		{
			get
			{
				return this._definitions;
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0015EDF0 File Offset: 0x0015CFF0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0015EDFC File Offset: 0x0015CFFC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Definitions.Count;
			while (i < count)
			{
				this.Definitions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A03 RID: 6659
		private ResultSetsOptionKind _resultSetsOptionKind;

		// Token: 0x04001A04 RID: 6660
		private List<ResultSetDefinition> _definitions = new List<ResultSetDefinition>();
	}
}
