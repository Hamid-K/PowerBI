using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000295 RID: 661
	[Serializable]
	internal sealed class FunctionLogicXor : FunctionBinary
	{
		// Token: 0x060014AC RID: 5292 RVA: 0x00030680 File Offset: 0x0002E880
		public FunctionLogicXor(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00030696 File Offset: 0x0002E896
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00030699 File Offset: 0x0002E899
		public override string BinaryOperator()
		{
			return " Xor ";
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000306A0 File Offset: 0x0002E8A0
		public override object Evaluate()
		{
			object obj = base.Lhs.Evaluate();
			object obj2 = base.Rhs.Evaluate();
			if (obj is bool && obj2 is bool)
			{
				return Convert.ToInt32(base.Lhs.EvaluateBoolean()) + Convert.ToInt32(base.Rhs.EvaluateBoolean()) == 1;
			}
			if ((obj is int || obj is double) && (obj2 is int || obj2 is double))
			{
				return Convert.ToInt32(base.Lhs.EvaluateDouble()) ^ Convert.ToInt32(base.Rhs.EvaluateDouble());
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

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x000307CF File Offset: 0x0002E9CF
		public override int PriorityCode
		{
			get
			{
				return 13;
			}
		}
	}
}
