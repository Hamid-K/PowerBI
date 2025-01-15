using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016C RID: 364
	internal class CsdlSemanticsApplyExpression : CsdlSemanticsExpression, IEdmApplyExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x060009CA RID: 2506 RVA: 0x0001B6D1 File Offset: 0x000198D1
		public CsdlSemanticsApplyExpression(CsdlApplyExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
			this.schema = schema;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001B706 File Offset: 0x00019906
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00011E72 File Offset: 0x00010072
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionApplication;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0001B70E File Offset: 0x0001990E
		public IEdmFunction AppliedFunction
		{
			get
			{
				return this.appliedFunctionCache.GetValue(this, CsdlSemanticsApplyExpression.ComputeAppliedFunctionFunc, null);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001B722 File Offset: 0x00019922
		public IEnumerable<IEdmExpression> Arguments
		{
			get
			{
				return this.argumentsCache.GetValue(this, CsdlSemanticsApplyExpression.ComputeArgumentsFunc, null);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0001B736 File Offset: 0x00019936
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.AppliedFunction is IUnresolvedElement)
				{
					return this.AppliedFunction.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001B758 File Offset: 0x00019958
		private IEdmFunction ComputeAppliedFunction()
		{
			if (this.expression.Function == null)
			{
				return null;
			}
			IEnumerable<IEdmFunction> enumerable = this.schema.FindOperations(this.expression.Function).OfType<IEdmFunction>();
			if (enumerable.Count<IEdmFunction>() == 0)
			{
				return new UnresolvedFunction(this.expression.Function, Strings.Bad_UnresolvedOperation(this.expression.Function), base.Location);
			}
			enumerable = enumerable.Where(new Func<IEdmFunction, bool>(this.IsMatchingFunction));
			int num = enumerable.Count<IEdmFunction>();
			if (num > 1)
			{
				enumerable = enumerable.Where(new Func<IEdmFunction, bool>(this.IsExactMatch));
				num = enumerable.Count<IEdmFunction>();
				if (num != 1)
				{
					return new UnresolvedFunction(this.expression.Function, Strings.Bad_AmbiguousOperation(this.expression.Function), base.Location);
				}
				return enumerable.Single<IEdmFunction>();
			}
			else
			{
				if (num == 0)
				{
					return new UnresolvedFunction(this.expression.Function, Strings.Bad_OperationParametersDontMatch(this.expression.Function), base.Location);
				}
				return enumerable.Single<IEdmFunction>();
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001B85C File Offset: 0x00019A5C
		private IEnumerable<IEdmExpression> ComputeArguments()
		{
			bool flag = this.expression.Function == null;
			List<IEdmExpression> list = new List<IEdmExpression>();
			foreach (CsdlExpressionBase csdlExpressionBase in this.expression.Arguments)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					list.Add(CsdlSemanticsModel.WrapExpression(csdlExpressionBase, this.bindingContext, this.schema));
				}
			}
			return list;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001B8DC File Offset: 0x00019ADC
		private bool IsMatchingFunction(IEdmOperation operation)
		{
			if (operation.Parameters.Count<IEdmOperationParameter>() != this.Arguments.Count<IEdmExpression>())
			{
				return false;
			}
			IEnumerator<IEdmExpression> enumerator = this.Arguments.GetEnumerator();
			foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters)
			{
				enumerator.MoveNext();
				IEnumerable<EdmError> enumerable;
				if (!enumerator.Current.TryCast(edmOperationParameter.Type, this.bindingContext, false, out enumerable))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001B974 File Offset: 0x00019B74
		private bool IsExactMatch(IEdmOperation operation)
		{
			IEnumerator<IEdmExpression> enumerator = this.Arguments.GetEnumerator();
			foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters)
			{
				enumerator.MoveNext();
				IEnumerable<EdmError> enumerable;
				if (!enumerator.Current.TryCast(edmOperationParameter.Type, this.bindingContext, true, out enumerable))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040005F0 RID: 1520
		private readonly CsdlApplyExpression expression;

		// Token: 0x040005F1 RID: 1521
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040005F2 RID: 1522
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005F3 RID: 1523
		private readonly Cache<CsdlSemanticsApplyExpression, IEdmFunction> appliedFunctionCache = new Cache<CsdlSemanticsApplyExpression, IEdmFunction>();

		// Token: 0x040005F4 RID: 1524
		private static readonly Func<CsdlSemanticsApplyExpression, IEdmFunction> ComputeAppliedFunctionFunc = (CsdlSemanticsApplyExpression me) => me.ComputeAppliedFunction();

		// Token: 0x040005F5 RID: 1525
		private readonly Cache<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>> argumentsCache = new Cache<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x040005F6 RID: 1526
		private static readonly Func<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>> ComputeArgumentsFunc = (CsdlSemanticsApplyExpression me) => me.ComputeArguments();
	}
}
