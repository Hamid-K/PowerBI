using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	internal sealed class Recursive : BaseInternalExpression
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x0002F2EA File Offset: 0x0002D4EA
		public Recursive(RecursiveOption v)
		{
			this._Value = v;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0002F2F9 File Offset: 0x0002D4F9
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0002F2FC File Offset: 0x0002D4FC
		public RecursiveOption Value
		{
			get
			{
				return this._Value;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0002F304 File Offset: 0x0002D504
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this._Value != RecursiveOption.Simple)
			{
				return "Recursive";
			}
			return "Simple";
		}

		// Token: 0x04000694 RID: 1684
		private readonly RecursiveOption _Value;
	}
}
