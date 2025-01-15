using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	internal class CreateAggregateStatement : TSqlStatement
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x0016ADD3 File Offset: 0x00168FD3
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x0016ADDB File Offset: 0x00168FDB
		public SchemaObjectName Name
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

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0016ADEB File Offset: 0x00168FEB
		// (set) Token: 0x06002D18 RID: 11544 RVA: 0x0016ADF3 File Offset: 0x00168FF3
		public AssemblyName AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._assemblyName = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x0016AE03 File Offset: 0x00169003
		public IList<ProcedureParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x0016AE0B File Offset: 0x0016900B
		// (set) Token: 0x06002D1B RID: 11547 RVA: 0x0016AE13 File Offset: 0x00169013
		public DataTypeReference ReturnType
		{
			get
			{
				return this._returnType;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._returnType = value;
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0016AE23 File Offset: 0x00169023
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0016AE30 File Offset: 0x00169030
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.AssemblyName != null)
			{
				this.AssemblyName.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
			if (this.ReturnType != null)
			{
				this.ReturnType.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D38 RID: 7480
		private SchemaObjectName _name;

		// Token: 0x04001D39 RID: 7481
		private AssemblyName _assemblyName;

		// Token: 0x04001D3A RID: 7482
		private List<ProcedureParameter> _parameters = new List<ProcedureParameter>();

		// Token: 0x04001D3B RID: 7483
		private DataTypeReference _returnType;
	}
}
