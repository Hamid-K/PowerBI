using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	internal class MethodSpecifier : TSqlFragment
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0015FAE6 File Offset: 0x0015DCE6
		// (set) Token: 0x060022A8 RID: 8872 RVA: 0x0015FAEE File Offset: 0x0015DCEE
		public Identifier AssemblyName
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0015FAFE File Offset: 0x0015DCFE
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x0015FB06 File Offset: 0x0015DD06
		public Identifier ClassName
		{
			get
			{
				return this._className;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._className = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0015FB16 File Offset: 0x0015DD16
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x0015FB1E File Offset: 0x0015DD1E
		public Identifier MethodName
		{
			get
			{
				return this._methodName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._methodName = value;
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0015FB2E File Offset: 0x0015DD2E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0015FB3C File Offset: 0x0015DD3C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.AssemblyName != null)
			{
				this.AssemblyName.Accept(visitor);
			}
			if (this.ClassName != null)
			{
				this.ClassName.Accept(visitor);
			}
			if (this.MethodName != null)
			{
				this.MethodName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A38 RID: 6712
		private Identifier _assemblyName;

		// Token: 0x04001A39 RID: 6713
		private Identifier _className;

		// Token: 0x04001A3A RID: 6714
		private Identifier _methodName;
	}
}
