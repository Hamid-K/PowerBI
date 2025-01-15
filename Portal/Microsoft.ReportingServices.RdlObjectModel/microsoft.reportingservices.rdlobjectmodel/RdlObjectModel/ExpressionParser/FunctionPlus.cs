using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200029E RID: 670
	[Serializable]
	internal sealed class FunctionPlus : FunctionBinary
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x00030EE0 File Offset: 0x0002F0E0
		public FunctionPlus()
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00030EE8 File Offset: 0x0002F0E8
		public FunctionPlus(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00030EFE File Offset: 0x0002F0FE
		public override string BinaryOperator()
		{
			return " + ";
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00030F08 File Offset: 0x0002F108
		public override object Evaluate()
		{
			object obj = base.Lhs.Evaluate();
			object obj2 = base.Rhs.Evaluate();
			int num = 65536;
			if (obj is int && obj2 is int)
			{
				return (int)obj + (int)obj2;
			}
			double num2 = base.Lhs.EvaluateDouble() + base.Rhs.EvaluateDouble();
			if (Math.Abs(num2) < 1.7976931348623157E+308 / (double)num)
			{
				double num3 = Math.Abs((double)((int)num2 * num) - num2 * (double)num);
				if (num3 < 1.0 && num3 > 0.0)
				{
					num2 = (double)((int)num2 * num / num);
				}
				if (num2 == (double)((int)num2))
				{
					return (int)num2;
				}
			}
			return num2;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00030FD3 File Offset: 0x0002F1D3
		public override int PriorityCode
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00030FD6 File Offset: 0x0002F1D6
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			if (!this.AreTypesCompatibleForAdd(base.Lhs, base.Rhs))
			{
				RDLExceptionHelper.WriteExpectedOperator("'&'", "'+'", this.StartColumn, this.EndColumn);
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00031010 File Offset: 0x0002F210
		private bool AreTypesCompatibleForAdd(IInternalExpression lhs, IInternalExpression rhs)
		{
			TypeCode typeCode = lhs.TypeCode();
			TypeCode typeCode2 = rhs.TypeCode();
			if (lhs is FunctionNothing)
			{
				return typeCode2 != global::System.TypeCode.DBNull;
			}
			if (rhs is FunctionNothing)
			{
				return typeCode != global::System.TypeCode.DBNull;
			}
			DataTypes? dataTypes = RDLUtil.ConvertToDataType(typeCode);
			DataTypes? dataTypes2 = RDLUtil.ConvertToDataType(typeCode2);
			bool flag = dataTypes != null && (dataTypes.Value == DataTypes.Integer || dataTypes.Value == DataTypes.Float || dataTypes.Value == DataTypes.Boolean);
			bool flag2 = dataTypes2 != null && (dataTypes2.Value == DataTypes.Integer || dataTypes2.Value == DataTypes.Float || dataTypes2.Value == DataTypes.Boolean);
			if (flag && flag2)
			{
				return true;
			}
			if ((typeCode == global::System.TypeCode.String && typeCode2 == global::System.TypeCode.DateTime) || (typeCode == global::System.TypeCode.DateTime && typeCode2 == global::System.TypeCode.String))
			{
				return true;
			}
			if (EqualityComparer<DataTypes?>.Default.Equals(dataTypes, dataTypes2))
			{
				return true;
			}
			try
			{
				base.ValidateIntOperandTypes();
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}
