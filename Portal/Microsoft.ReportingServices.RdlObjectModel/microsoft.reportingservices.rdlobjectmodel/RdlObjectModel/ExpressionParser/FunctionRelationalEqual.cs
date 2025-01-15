using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A6 RID: 678
	[Serializable]
	internal sealed class FunctionRelationalEqual : FunctionBinary
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x0003139F File Offset: 0x0002F59F
		public FunctionRelationalEqual(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000313B5 File Offset: 0x0002F5B5
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000313B8 File Offset: 0x0002F5B8
		public override string BinaryOperator()
		{
			return " = ";
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000313C0 File Offset: 0x0002F5C0
		public override object Evaluate()
		{
			object obj = base.Lhs.Evaluate();
			object obj2 = base.Rhs.Evaluate();
			if (obj == null || obj2 == null)
			{
				return false;
			}
			if (obj.GetType() != obj2.GetType())
			{
				try
				{
					obj = Convert.ToDouble(obj, CultureInfo.CurrentCulture);
					obj2 = Convert.ToDouble(obj2, CultureInfo.CurrentCulture);
				}
				catch
				{
					return false;
				}
			}
			return obj.Equals(obj2);
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00031454 File Offset: 0x0002F654
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
