using System;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000271 RID: 625
	internal abstract class FunctionBinary : BaseInternalExpression
	{
		// Token: 0x060013F0 RID: 5104 RVA: 0x0002F6D5 File Offset: 0x0002D8D5
		public FunctionBinary()
		{
			this._lhs = null;
			this._rhs = null;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0002F6EB File Offset: 0x0002D8EB
		public FunctionBinary(IInternalExpression l, IInternalExpression r)
		{
			this._lhs = l;
			this._rhs = r;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0002F701 File Offset: 0x0002D901
		public override bool IsConstant()
		{
			return this._lhs.IsConstant() && this._rhs.IsConstant();
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0002F71D File Offset: 0x0002D91D
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0002F725 File Offset: 0x0002D925
		public IInternalExpression Lhs
		{
			get
			{
				return this._lhs;
			}
			protected set
			{
				this._lhs = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0002F72E File Offset: 0x0002D92E
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0002F736 File Offset: 0x0002D936
		public IInternalExpression Rhs
		{
			get
			{
				return this._rhs;
			}
			protected set
			{
				this._rhs = value;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0002F740 File Offset: 0x0002D940
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = this.Lhs.WriteSource(nameChanges);
			string text2 = this.Rhs.WriteSource(nameChanges);
			if (base.Bracketed)
			{
				return string.Concat(new string[]
				{
					"(",
					text,
					this.BinaryOperator(),
					text2,
					")"
				});
			}
			return text + this.BinaryOperator() + text2;
		}

		// Token: 0x060013F8 RID: 5112
		public abstract string BinaryOperator();

		// Token: 0x060013F9 RID: 5113 RVA: 0x0002F7A9 File Offset: 0x0002D9A9
		public override TypeCode TypeCode()
		{
			return this.GetGreatestRangeTypeCode();
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0002F7B1 File Offset: 0x0002D9B1
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			this.Lhs.Traverse(callback);
			this.Rhs.Traverse(callback);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0002F7CC File Offset: 0x0002D9CC
		protected TypeCode GetGreatestRangeTypeCode()
		{
			if (this.Lhs != null && this.Rhs != null)
			{
				TypeCode typeCode = this.Lhs.TypeCode();
				TypeCode typeCode2 = this.Rhs.TypeCode();
				if (RDLUtil.IsNumericType(typeCode) && RDLUtil.IsNumericType(typeCode2))
				{
					if (typeCode <= typeCode2)
					{
						return typeCode2;
					}
					return typeCode;
				}
			}
			return global::System.TypeCode.Double;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0002F81C File Offset: 0x0002DA1C
		protected void ValidateIntOperandTypes()
		{
			double num;
			if (!base.IsBoolIntOrDouble(this.Lhs) && !double.TryParse(this.Lhs.EvaluateString(), out num))
			{
				RDLExceptionHelper.WriteOperandTypesInvalid(this.Lhs.WriteSource(), this.BinaryOperator(), this.StartColumn, this.EndColumn);
			}
			if (!base.IsBoolIntOrDouble(this.Rhs) && !double.TryParse(this.Rhs.EvaluateString(), out num))
			{
				RDLExceptionHelper.WriteOperandTypesInvalid(this.Rhs.WriteSource(), this.BinaryOperator(), this.StartColumn, this.EndColumn);
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0002F8B4 File Offset: 0x0002DAB4
		protected void ArrayCheck()
		{
			if (this.Lhs.IsArray)
			{
				RDLExceptionHelper.WriteArrayOperand(this.BinaryOperator(), this.Lhs.WriteSource(), this.StartColumn, this.EndColumn);
				return;
			}
			if (this.Rhs.IsArray)
			{
				RDLExceptionHelper.WriteArrayOperand(this.BinaryOperator(), this.Rhs.WriteSource(), this.StartColumn, this.EndColumn);
			}
		}

		// Token: 0x04000698 RID: 1688
		private IInternalExpression _lhs;

		// Token: 0x04000699 RID: 1689
		private IInternalExpression _rhs;

		// Token: 0x0400069A RID: 1690
		protected static int TwoToThePowerOf16 = 65536;
	}
}
