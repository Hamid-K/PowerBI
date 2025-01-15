using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000264 RID: 612
	internal abstract class BaseInternalExpression : IInternalExpression
	{
		// Token: 0x060013B2 RID: 5042
		public abstract TypeCode TypeCode();

		// Token: 0x060013B3 RID: 5043 RVA: 0x0002F31A File Offset: 0x0002D51A
		public virtual string WriteSource(NameChanges nameChanges)
		{
			return "";
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0002F321 File Offset: 0x0002D521
		public virtual string WriteSource()
		{
			return this.WriteSource(null);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0002F32A File Offset: 0x0002D52A
		public virtual int PriorityCode
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0002F32D File Offset: 0x0002D52D
		public virtual bool IsConstant()
		{
			return false;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0002F330 File Offset: 0x0002D530
		public virtual int StartColumn
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0002F333 File Offset: 0x0002D533
		public virtual int EndColumn
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0002F336 File Offset: 0x0002D536
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0002F33E File Offset: 0x0002D53E
		public bool IsArray
		{
			get
			{
				return this._ReturnTypeIsArray;
			}
			set
			{
				this._ReturnTypeIsArray = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0002F347 File Offset: 0x0002D547
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x0002F34F File Offset: 0x0002D54F
		public bool Bracketed
		{
			get
			{
				return this._bracketed;
			}
			set
			{
				this._bracketed = value;
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0002F358 File Offset: 0x0002D558
		public virtual object Evaluate()
		{
			return null;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0002F35B File Offset: 0x0002D55B
		public string EvaluateString()
		{
			return Convert.ToString(this.Evaluate(), CultureInfo.CurrentCulture);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0002F36D File Offset: 0x0002D56D
		public string EvaluateString(bool useUserCulture)
		{
			return RDLUtil.ObjectToString(this.Evaluate(), useUserCulture);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0002F37B File Offset: 0x0002D57B
		public double EvaluateDouble()
		{
			return RDLUtil.ConvertToDouble(this.Evaluate());
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0002F388 File Offset: 0x0002D588
		public decimal EvaluateDecimal()
		{
			return RDLUtil.ConvertToDecimal(this.Evaluate());
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0002F395 File Offset: 0x0002D595
		public DateTime EvaluateDateTime()
		{
			return RDLUtil.ConvertToDateTime(this.Evaluate());
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0002F3A2 File Offset: 0x0002D5A2
		public bool EvaluateBoolean()
		{
			return RDLUtil.ConvertToBoolean(this.Evaluate());
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0002F3AF File Offset: 0x0002D5AF
		public override bool Equals(object obj)
		{
			return this == obj || (obj is BaseInternalExpression && base.GetType() == obj.GetType() && this.WriteSource() == ((BaseInternalExpression)obj).WriteSource());
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0002F3EA File Offset: 0x0002D5EA
		public override int GetHashCode()
		{
			return this.WriteSource().GetHashCode();
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0002F3F7 File Offset: 0x0002D5F7
		public void Traverse(ProcessInternalExpressionHandler callback)
		{
			callback(this);
			this.TraverseChildren(callback);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0002F407 File Offset: 0x0002D607
		protected virtual void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0002F409 File Offset: 0x0002D609
		public virtual void Validate(ExpressionValidationContext context)
		{
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0002F40C File Offset: 0x0002D60C
		protected bool IsBoolIntOrDouble(IInternalExpression exp)
		{
			return exp.TypeCode() == global::System.TypeCode.Boolean || RDLUtil.IsNumericType(exp.TypeCode()) || (exp.TypeCode() == global::System.TypeCode.Object && (exp.Evaluate() is bool || exp.Evaluate() is int || exp.Evaluate() is decimal || exp.Evaluate() is float || exp.Evaluate() is double));
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0002F480 File Offset: 0x0002D680
		protected bool IsBoolOrInt(IInternalExpression exp)
		{
			return exp.TypeCode() == global::System.TypeCode.Boolean || RDLUtil.IsIntegerType(exp.TypeCode()) || (exp.TypeCode() == global::System.TypeCode.Object && (exp.Evaluate() is bool || exp.Evaluate() is int));
		}

		// Token: 0x04000695 RID: 1685
		private bool _ReturnTypeIsArray;

		// Token: 0x04000696 RID: 1686
		private bool _bracketed;
	}
}
