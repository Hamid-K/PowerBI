using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	internal sealed class FunctionTotalPages : BaseInternalExpression
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x000301EF File Offset: 0x0002E3EF
		public FunctionTotalPages()
		{
			this._RuntimePageCount = 1;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000301FE File Offset: 0x0002E3FE
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00030202 File Offset: 0x0002E402
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0003020A File Offset: 0x0002E40A
		public int RuntimePageCount
		{
			get
			{
				return this._RuntimePageCount;
			}
			set
			{
				this._RuntimePageCount = value;
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00030213 File Offset: 0x0002E413
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!TotalPages";
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0003021A File Offset: 0x0002E41A
		public override object Evaluate()
		{
			return 1;
		}

		// Token: 0x040006B0 RID: 1712
		private int _RuntimePageCount;
	}
}
