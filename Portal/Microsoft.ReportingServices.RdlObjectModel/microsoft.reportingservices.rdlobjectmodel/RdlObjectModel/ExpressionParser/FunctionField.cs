using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200027E RID: 638
	[Serializable]
	internal class FunctionField : BaseInternalExpression, IInternalNamedExpression
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x0002FBA9 File Offset: 0x0002DDA9
		public FunctionField(Field field, string property)
		{
			this._Field = field;
			this._Property = (string.IsNullOrEmpty(property) ? "Value" : property);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0002FBCE File Offset: 0x0002DDCE
		public FunctionField(string name, string property)
		{
			this._Name = name;
			this._Property = (string.IsNullOrEmpty(property) ? "Value" : property);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0002FBF3 File Offset: 0x0002DDF3
		public FunctionField(IInternalExpression name)
		{
			this._NameExpr = name;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0002FC02 File Offset: 0x0002DE02
		public FunctionField(IInternalExpression name, IInternalExpression property)
		{
			this._NameExpr = name;
			this._PropertyExpr = property;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0002FC18 File Offset: 0x0002DE18
		public string Name
		{
			get
			{
				if (this._Field != null)
				{
					return this._Field.Name;
				}
				return this._Name;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0002FC34 File Offset: 0x0002DE34
		public string DisplayName
		{
			get
			{
				if (this._Field != null)
				{
					return this._Field.Name;
				}
				return this._Name;
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0002FC50 File Offset: 0x0002DE50
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0002FC54 File Offset: 0x0002DE54
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public virtual Field Fld
		{
			get
			{
				return this._Field;
			}
			internal set
			{
				this._Field = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0002FC65 File Offset: 0x0002DE65
		public string PropertyName
		{
			get
			{
				return this._Property;
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0002FC70 File Offset: 0x0002DE70
		public override string WriteSource(NameChanges nameChanges)
		{
			string text;
			if (this._NameExpr != null)
			{
				if (this._NameExpr is ConstantString)
				{
					text = "!" + this._NameExpr.EvaluateString();
				}
				else
				{
					text = "(" + this._NameExpr.WriteSource(nameChanges) + ")";
				}
			}
			else
			{
				text = "!" + this.Name;
			}
			string text2 = "Value";
			if (this._PropertyExpr != null)
			{
				if (this._PropertyExpr is ConstantString)
				{
					text2 = this._PropertyExpr.EvaluateString();
				}
				else
				{
					text2 = this._PropertyExpr.WriteSource(nameChanges);
				}
			}
			else if (this._Property != null)
			{
				text2 = this._Property;
			}
			return "Fields" + text + "." + text2;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0002FD33 File Offset: 0x0002DF33
		public override object Evaluate()
		{
			return null;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0002FD36 File Offset: 0x0002DF36
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this._NameExpr != null)
			{
				this._NameExpr.Traverse(callback);
			}
			if (this._PropertyExpr != null)
			{
				this._PropertyExpr.Traverse(callback);
			}
		}

		// Token: 0x040006A1 RID: 1697
		private Field _Field;

		// Token: 0x040006A2 RID: 1698
		private readonly string _Name;

		// Token: 0x040006A3 RID: 1699
		private readonly IInternalExpression _NameExpr;

		// Token: 0x040006A4 RID: 1700
		private readonly IInternalExpression _PropertyExpr;

		// Token: 0x040006A5 RID: 1701
		private readonly string _Property;
	}
}
