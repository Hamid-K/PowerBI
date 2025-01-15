using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000213 RID: 531
	internal interface IInternalExpression
	{
		// Token: 0x06001206 RID: 4614
		TypeCode TypeCode();

		// Token: 0x06001207 RID: 4615
		bool IsConstant();

		// Token: 0x06001208 RID: 4616
		string WriteSource();

		// Token: 0x06001209 RID: 4617
		string WriteSource(NameChanges nameChanges);

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600120A RID: 4618
		// (set) Token: 0x0600120B RID: 4619
		bool IsArray { get; set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600120C RID: 4620
		// (set) Token: 0x0600120D RID: 4621
		bool Bracketed { get; set; }

		// Token: 0x0600120E RID: 4622
		object Evaluate();

		// Token: 0x0600120F RID: 4623
		string EvaluateString();

		// Token: 0x06001210 RID: 4624
		double EvaluateDouble();

		// Token: 0x06001211 RID: 4625
		decimal EvaluateDecimal();

		// Token: 0x06001212 RID: 4626
		DateTime EvaluateDateTime();

		// Token: 0x06001213 RID: 4627
		bool EvaluateBoolean();

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001214 RID: 4628
		int PriorityCode { get; }

		// Token: 0x06001215 RID: 4629
		void Traverse(ProcessInternalExpressionHandler callback);

		// Token: 0x06001216 RID: 4630
		void Validate(ExpressionValidationContext context);
	}
}
