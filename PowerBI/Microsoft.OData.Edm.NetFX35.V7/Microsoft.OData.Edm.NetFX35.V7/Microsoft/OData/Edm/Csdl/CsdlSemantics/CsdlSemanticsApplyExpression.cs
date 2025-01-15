using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015D RID: 349
	internal class CsdlSemanticsApplyExpression : CsdlSemanticsExpression, IEdmApplyExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x000195D1 File Offset: 0x000177D1
		public CsdlSemanticsApplyExpression(CsdlApplyExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
			this.schema = schema;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00019606 File Offset: 0x00017806
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001398E File Offset: 0x00011B8E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionApplication;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001960E File Offset: 0x0001780E
		public IEdmFunction AppliedFunction
		{
			get
			{
				return this.appliedFunctionCache.GetValue(this, CsdlSemanticsApplyExpression.ComputeAppliedFunctionFunc, null);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00019622 File Offset: 0x00017822
		public IEnumerable<IEdmExpression> Arguments
		{
			get
			{
				return this.argumentsCache.GetValue(this, CsdlSemanticsApplyExpression.ComputeArgumentsFunc, null);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00019636 File Offset: 0x00017836
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

		// Token: 0x06000915 RID: 2325 RVA: 0x00019658 File Offset: 0x00017858
		private IEdmFunction ComputeAppliedFunction()
		{
			if (this.expression.Function == null)
			{
				return null;
			}
			IEnumerable<IEdmFunction> enumerable = Enumerable.OfType<IEdmFunction>(this.schema.FindOperations(this.expression.Function));
			if (Enumerable.Count<IEdmFunction>(enumerable) == 0)
			{
				return new UnresolvedFunction(this.expression.Function, Strings.Bad_UnresolvedOperation(this.expression.Function), base.Location);
			}
			enumerable = Enumerable.Where<IEdmFunction>(enumerable, new Func<IEdmFunction, bool>(this.IsMatchingFunction));
			int num = Enumerable.Count<IEdmFunction>(enumerable);
			if (num > 1)
			{
				enumerable = Enumerable.Where<IEdmFunction>(enumerable, new Func<IEdmFunction, bool>(this.IsExactMatch));
				num = Enumerable.Count<IEdmFunction>(enumerable);
				if (num != 1)
				{
					return new UnresolvedFunction(this.expression.Function, Strings.Bad_AmbiguousOperation(this.expression.Function), base.Location);
				}
				return Enumerable.Single<IEdmFunction>(enumerable);
			}
			else
			{
				if (num == 0)
				{
					return new UnresolvedFunction(this.expression.Function, Strings.Bad_OperationParametersDontMatch(this.expression.Function), base.Location);
				}
				return Enumerable.Single<IEdmFunction>(enumerable);
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001975C File Offset: 0x0001795C
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

		// Token: 0x06000917 RID: 2327 RVA: 0x000197DC File Offset: 0x000179DC
		private bool IsMatchingFunction(IEdmOperation operation)
		{
			if (Enumerable.Count<IEdmOperationParameter>(operation.Parameters) != Enumerable.Count<IEdmExpression>(this.Arguments))
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

		// Token: 0x06000918 RID: 2328 RVA: 0x00019874 File Offset: 0x00017A74
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

		// Token: 0x04000575 RID: 1397
		private readonly CsdlApplyExpression expression;

		// Token: 0x04000576 RID: 1398
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000577 RID: 1399
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000578 RID: 1400
		private readonly Cache<CsdlSemanticsApplyExpression, IEdmFunction> appliedFunctionCache = new Cache<CsdlSemanticsApplyExpression, IEdmFunction>();

		// Token: 0x04000579 RID: 1401
		private static readonly Func<CsdlSemanticsApplyExpression, IEdmFunction> ComputeAppliedFunctionFunc = (CsdlSemanticsApplyExpression me) => me.ComputeAppliedFunction();

		// Token: 0x0400057A RID: 1402
		private readonly Cache<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>> argumentsCache = new Cache<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x0400057B RID: 1403
		private static readonly Func<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>> ComputeArgumentsFunc = (CsdlSemanticsApplyExpression me) => me.ComputeArguments();
	}
}
