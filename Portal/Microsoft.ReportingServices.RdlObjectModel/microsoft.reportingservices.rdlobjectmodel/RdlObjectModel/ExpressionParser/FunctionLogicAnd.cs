using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000290 RID: 656
	[Serializable]
	internal sealed class FunctionLogicAnd : FunctionBinary
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x0003038A File Offset: 0x0002E58A
		public FunctionLogicAnd(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000303A0 File Offset: 0x0002E5A0
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000303A3 File Offset: 0x0002E5A3
		public override string BinaryOperator()
		{
			return " And ";
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000303AC File Offset: 0x0002E5AC
		public override object Evaluate()
		{
			object obj = base.Lhs.Evaluate();
			object obj2 = base.Rhs.Evaluate();
			if (obj is bool && obj2 is bool)
			{
				return base.Lhs.EvaluateBoolean() && base.Rhs.EvaluateBoolean();
			}
			if ((obj is int || obj is double) && (obj2 is int || obj2 is double))
			{
				return Convert.ToInt32(base.Lhs.Evaluate(), CultureInfo.CurrentCulture) & Convert.ToInt32(base.Rhs.Evaluate(), CultureInfo.CurrentCulture);
			}
			if (((!(obj is int) && !(obj is double)) || !(obj2 is bool)) && (!(obj is bool) || (!(obj2 is int) && !(obj2 is double))))
			{
				return "#Error";
			}
			if (obj is bool)
			{
				if (base.Lhs.EvaluateBoolean())
				{
					return (int)base.Rhs.EvaluateDouble();
				}
				return 0;
			}
			else
			{
				if (base.Rhs.EvaluateBoolean())
				{
					return (int)base.Lhs.EvaluateDouble();
				}
				return 0;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x000304DC File Offset: 0x0002E6DC
		public override int PriorityCode
		{
			get
			{
				return 11;
			}
		}
	}
}
