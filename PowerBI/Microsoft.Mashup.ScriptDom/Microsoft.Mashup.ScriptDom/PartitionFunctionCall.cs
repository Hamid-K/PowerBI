using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	internal class PartitionFunctionCall : PrimaryExpression
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x001621DD File Offset: 0x001603DD
		// (set) Token: 0x060024C0 RID: 9408 RVA: 0x001621E5 File Offset: 0x001603E5
		public Identifier DatabaseName
		{
			get
			{
				return this._databaseName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._databaseName = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x001621F5 File Offset: 0x001603F5
		// (set) Token: 0x060024C2 RID: 9410 RVA: 0x001621FD File Offset: 0x001603FD
		public Identifier FunctionName
		{
			get
			{
				return this._functionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._functionName = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x0016220D File Offset: 0x0016040D
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00162215 File Offset: 0x00160415
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00162224 File Offset: 0x00160424
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			if (this.FunctionName != null)
			{
				this.FunctionName.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001ADA RID: 6874
		private Identifier _databaseName;

		// Token: 0x04001ADB RID: 6875
		private Identifier _functionName;

		// Token: 0x04001ADC RID: 6876
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
