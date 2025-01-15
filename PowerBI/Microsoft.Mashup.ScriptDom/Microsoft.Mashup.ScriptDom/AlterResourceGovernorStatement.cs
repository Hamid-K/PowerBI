using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000481 RID: 1153
	[Serializable]
	internal class AlterResourceGovernorStatement : TSqlStatement
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x00170C57 File Offset: 0x0016EE57
		// (set) Token: 0x0600330C RID: 13068 RVA: 0x00170C5F File Offset: 0x0016EE5F
		public AlterResourceGovernorCommandType Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x00170C68 File Offset: 0x0016EE68
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x00170C70 File Offset: 0x0016EE70
		public SchemaObjectName ClassifierFunction
		{
			get
			{
				return this._classifierFunction;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._classifierFunction = value;
			}
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x00170C80 File Offset: 0x0016EE80
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x00170C8C File Offset: 0x0016EE8C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ClassifierFunction != null)
			{
				this.ClassifierFunction.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ED7 RID: 7895
		private AlterResourceGovernorCommandType _command;

		// Token: 0x04001ED8 RID: 7896
		private SchemaObjectName _classifierFunction;
	}
}
