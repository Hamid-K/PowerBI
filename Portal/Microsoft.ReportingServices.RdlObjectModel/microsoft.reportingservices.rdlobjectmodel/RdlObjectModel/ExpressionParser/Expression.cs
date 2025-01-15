using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000212 RID: 530
	[DebuggerDisplay("Expression Source : {Source}")]
	[Serializable]
	internal class Expression
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x000286AB File Offset: 0x000268AB
		internal Expression()
		{
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000286BE File Offset: 0x000268BE
		protected Expression(Report r, IReportLink p)
		{
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000286D1 File Offset: 0x000268D1
		internal Expression(Report r, IReportLink p, string tag, XmlNode xNode)
		{
			this.Source = xNode.InnerText;
			this.SetTag(tag);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000286F8 File Offset: 0x000268F8
		public Expression(Report r, IReportLink p, string tag, string expression, ExpressionType expectedType)
		{
			this.Source = expression;
			this.SetTag(tag);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0002871A File Offset: 0x0002691A
		public Expression(Report r, IReportLink p, string tag, string expression)
			: this(r, p, tag, expression, ExpressionType.Variant)
		{
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00028728 File Offset: 0x00026928
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x00028730 File Offset: 0x00026930
		public string Source
		{
			get
			{
				return this._ExpressionSource;
			}
			set
			{
				if (this._ExpressionSource == value)
				{
					return;
				}
				string expressionSource = this._ExpressionSource;
				this._ExpressionSource = value;
				this.Validate(true);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00028756 File Offset: 0x00026956
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0002875E File Offset: 0x0002695E
		public string SourceNoValidate
		{
			get
			{
				return this._ExpressionSource;
			}
			set
			{
				if (this._ExpressionSource == value)
				{
					return;
				}
				string expressionSource = this._ExpressionSource;
				this._ExpressionSource = value;
				this.InternalExpression = null;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00028784 File Offset: 0x00026984
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0002878C File Offset: 0x0002698C
		public ExpressionSubType SubType
		{
			get
			{
				return this._SubType;
			}
			set
			{
				if (this._SubType == value)
				{
					return;
				}
				ExpressionSubType subType = this._SubType;
				this._SubType = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x000287A6 File Offset: 0x000269A6
		public IDictionary FieldScope
		{
			get
			{
				return this.GetFieldScope();
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x000287AE File Offset: 0x000269AE
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x000287B6 File Offset: 0x000269B6
		public IInternalExpression InternalExpression
		{
			get
			{
				return this._Expression;
			}
			private set
			{
				if (this._Expression == value || (this._Expression != null && this._Expression.Equals(value)))
				{
					return;
				}
				IInternalExpression expression = this._Expression;
				this._Expression = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000287E6 File Offset: 0x000269E6
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x000287EE File Offset: 0x000269EE
		public List<IInternalExpression> ObjectDependencyList
		{
			get
			{
				return this._ObjectDependencyList;
			}
			protected set
			{
				this._ObjectDependencyList = value;
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000287F7 File Offset: 0x000269F7
		public void Validate()
		{
			this.Validate(true);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00028800 File Offset: 0x00026A00
		internal void Validate(bool suppressExceptions)
		{
			this._ObjectDependencyList.Clear();
			if (this._ExpressionSource == null)
			{
				this.InternalExpression = null;
				return;
			}
			if (this._ExpressionSource.Trim() == "" || this.IsConstantString())
			{
				this.InternalExpression = new ConstantNonExpression(this._ExpressionSource);
				this._IsConstant = true;
				if (this._SubType != ExpressionSubType.Calculation)
				{
					this.SubType = ExpressionSubType.Text;
				}
				return;
			}
			ExpressionParser expressionParser = new ExpressionParser(EnvironmentContext.DefaultEnvironment);
			try
			{
				this.InternalExpression = expressionParser.Parse(this._ExpressionSource);
				this._ObjectDependencyList = expressionParser.ObjectDependencyList;
			}
			catch (ExpressionParserException)
			{
				this.InternalExpression = null;
				if (!suppressExceptions)
				{
					throw;
				}
				if (!this.IsConstantString())
				{
					this.SubType = ExpressionSubType.Calculation;
				}
				else
				{
					this.SubType = ExpressionSubType.Text;
				}
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000288D4 File Offset: 0x00026AD4
		internal static void IterateExpressionTree(Action<IInternalExpression> visitExpression, IInternalExpression internExp)
		{
			visitExpression(internExp);
			if (internExp is FunctionUnary)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionUnary)internExp).Rhs);
				return;
			}
			if (internExp is FunctionBinary)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionBinary)internExp).Lhs);
				Expression.IterateExpressionTree(visitExpression, ((FunctionBinary)internExp).Rhs);
				return;
			}
			if (internExp is FunctionRelationalTypeOf)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionRelationalTypeOf)internExp).Lhs);
				Expression.IterateExpressionTree(visitExpression, ((FunctionRelationalTypeOf)internExp).TypeNameExpr);
				return;
			}
			if (internExp is FunctionMultiArgument)
			{
				foreach (IInternalExpression internalExpression in ((FunctionMultiArgument)internExp).Arguments)
				{
					Expression.IterateExpressionTree(visitExpression, internalExpression);
				}
				return;
			}
			if (internExp is FunctionMethodOrProperty)
			{
				foreach (IInternalExpression internalExpression2 in ((FunctionMethodOrProperty)internExp).Args)
				{
					Expression.IterateExpressionTree(visitExpression, internalExpression2);
				}
				Expression.IterateExpressionTree(visitExpression, ((FunctionMethodOrProperty)internExp).CallTarget);
				return;
			}
			if (internExp is FunctionDefaultPropertyOrIndexer)
			{
				foreach (IInternalExpression internalExpression3 in ((FunctionDefaultPropertyOrIndexer)internExp).Args)
				{
					Expression.IterateExpressionTree(visitExpression, internalExpression3);
				}
				Expression.IterateExpressionTree(visitExpression, ((FunctionDefaultPropertyOrIndexer)internExp).CallTarget);
				return;
			}
			if (internExp is FunctionLateBoundAccessor)
			{
				foreach (IInternalExpression internalExpression4 in ((FunctionLateBoundAccessor)internExp).Args)
				{
					Expression.IterateExpressionTree(visitExpression, internalExpression4);
				}
				Expression.IterateExpressionTree(visitExpression, ((FunctionLateBoundAccessor)internExp).CallTarget);
				return;
			}
			if (internExp is FunctionNewArray)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionNewArray)internExp).TypeExpr);
				Expression.IterateExpressionTree(visitExpression, ((FunctionNewArray)internExp).InitExpr);
				return;
			}
			if (internExp is FunctionNewObject)
			{
				foreach (IInternalExpression internalExpression5 in ((FunctionNewObject)internExp).Args)
				{
					Expression.IterateExpressionTree(visitExpression, internalExpression5);
				}
				Expression.IterateExpressionTree(visitExpression, ((FunctionNewObject)internExp).TypeExpr);
				return;
			}
			if (internExp is FunctionMemberField)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionMemberField)internExp).CallTarget);
				return;
			}
			if (internExp is FunctionDictionaryAccessor)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionDictionaryAccessor)internExp).CallTarget);
				return;
			}
			if (internExp is FunctionArrayInit)
			{
				using (List<IInternalExpression>.Enumerator enumerator = ((FunctionArrayInit)internExp).Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IInternalExpression internalExpression6 = enumerator.Current;
						Expression.IterateExpressionTree(visitExpression, internalExpression6);
					}
					return;
				}
			}
			if (internExp is FunctionArrayType)
			{
				Expression.IterateExpressionTree(visitExpression, ((FunctionArrayType)internExp).TypeExpr);
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00028BE0 File Offset: 0x00026DE0
		public IDictionary GetFieldScope()
		{
			return new Hashtable();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00028BE8 File Offset: 0x00026DE8
		public string TypeCode()
		{
			if (this._Expression != null)
			{
				return "System." + this._Expression.TypeCode().ToString();
			}
			return "System.String";
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00028C26 File Offset: 0x00026E26
		public bool IsConstant()
		{
			return this._IsConstant;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00028C2E File Offset: 0x00026E2E
		public bool IsConstantString()
		{
			return Expression.IsConstantString(this.Source);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00028C3B File Offset: 0x00026E3B
		public static bool IsConstantString(string expressionSource)
		{
			return expressionSource != null && !expressionSource.Trim().StartsWith("=", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00028C56 File Offset: 0x00026E56
		public bool IsSourceEmpty()
		{
			return string.IsNullOrEmpty(this.Source);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00028C63 File Offset: 0x00026E63
		public override string ToString()
		{
			return this._ExpressionSource;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00028C6B File Offset: 0x00026E6B
		public Dictionary<string, IIdentifiable> ReportItemsDict
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00028C6E File Offset: 0x00026E6E
		public string GetTag()
		{
			return this._Tag;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00028C76 File Offset: 0x00026E76
		protected void SetTag(string tag)
		{
			this._Tag = tag;
		}

		// Token: 0x040005B9 RID: 1465
		private List<IInternalExpression> _ObjectDependencyList = new List<IInternalExpression>();

		// Token: 0x040005BA RID: 1466
		private IInternalExpression _Expression;

		// Token: 0x040005BB RID: 1467
		private string _ExpressionSource;

		// Token: 0x040005BC RID: 1468
		private bool _IsConstant;

		// Token: 0x040005BD RID: 1469
		private ExpressionSubType _SubType;

		// Token: 0x040005BE RID: 1470
		private string _Tag;
	}
}
