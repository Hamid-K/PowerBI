using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	internal abstract class Constant : BaseInternalExpression
	{
		// Token: 0x060013CC RID: 5068 RVA: 0x0002F4D5 File Offset: 0x0002D6D5
		protected Constant(object value)
		{
			this.valueConst = value;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0002F4E4 File Offset: 0x0002D6E4
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
		public override bool IsConstant()
		{
			return true;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0002F4EB File Offset: 0x0002D6EB
		public override string WriteSource(NameChanges nameChanges)
		{
			return Convert.ToString(this.Evaluate(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0002F4FD File Offset: 0x0002D6FD
		public override object Evaluate()
		{
			return this.valueConst;
		}

		// Token: 0x04000697 RID: 1687
		protected object valueConst;
	}
}
