using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C5 RID: 453
	internal sealed class ReportExpressionDefaultValueConstantAttribute : DefaultValueAttribute
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002400F File Offset: 0x0002220F
		public ReportExpressionDefaultValueConstantAttribute(string field)
			: base(new ReportExpression(DefaultValueConstantAttribute.GetConstant(field) as string))
		{
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002402C File Offset: 0x0002222C
		public ReportExpressionDefaultValueConstantAttribute(Type type, string field)
			: base(ReportExpressionDefaultValueAttribute.CreateInstance(type, DefaultValueConstantAttribute.GetConstant(field)))
		{
		}
	}
}
