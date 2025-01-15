using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200006C RID: 108
	internal class CsdlSemanticsApplyExpression : CsdlSemanticsExpression, IEdmApplyExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00004310 File Offset: 0x00002510
		public CsdlSemanticsApplyExpression(CsdlApplyExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
			this.schema = schema;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00004345 File Offset: 0x00002545
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000434D File Offset: 0x0000254D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.OperationApplication;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00004351 File Offset: 0x00002551
		public IEdmExpression AppliedOperation
		{
			get
			{
				return this.appliedOperationCache.GetValue(this, CsdlSemanticsApplyExpression.ComputeAppliedOperationFunc, null);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00004365 File Offset: 0x00002565
		public IEnumerable<IEdmExpression> Arguments
		{
			get
			{
				return this.argumentsCache.GetValue(this, CsdlSemanticsApplyExpression.ComputeArgumentsFunc, null);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004379 File Offset: 0x00002579
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.AppliedOperation is IUnresolvedElement)
				{
					return this.AppliedOperation.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000439C File Offset: 0x0000259C
		private IEdmExpression ComputeAppliedOperation()
		{
			if (this.expression.Function == null)
			{
				return CsdlSemanticsModel.WrapExpression(Enumerable.FirstOrDefault<CsdlExpressionBase>(this.expression.Arguments, null), this.bindingContext, this.schema);
			}
			IEnumerable<IEdmOperation> enumerable = this.schema.FindOperations(this.expression.Function);
			IEdmOperation edmOperation;
			if (Enumerable.Count<IEdmOperation>(enumerable) == 0)
			{
				edmOperation = new UnresolvedOperation(this.expression.Function, Strings.Bad_UnresolvedOperation(this.expression.Function), base.Location);
			}
			else
			{
				enumerable = Enumerable.Where<IEdmOperation>(enumerable, new Func<IEdmOperation, bool>(this.IsMatchingFunction));
				int num = Enumerable.Count<IEdmOperation>(enumerable);
				if (num > 1)
				{
					enumerable = Enumerable.Where<IEdmOperation>(enumerable, new Func<IEdmOperation, bool>(this.IsExactMatch));
					num = Enumerable.Count<IEdmOperation>(enumerable);
					if (num != 1)
					{
						edmOperation = new UnresolvedOperation(this.expression.Function, Strings.Bad_AmbiguousOperation(this.expression.Function), base.Location);
					}
					else
					{
						edmOperation = Enumerable.Single<IEdmOperation>(enumerable);
					}
				}
				else if (num == 0)
				{
					edmOperation = new UnresolvedOperation(this.expression.Function, Strings.Bad_OperationParametersDontMatch(this.expression.Function), base.Location);
				}
				else
				{
					edmOperation = Enumerable.Single<IEdmOperation>(enumerable);
				}
			}
			return new EdmOperationReferenceExpression(edmOperation);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000044D0 File Offset: 0x000026D0
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

		// Token: 0x060001A5 RID: 421 RVA: 0x00004550 File Offset: 0x00002750
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

		// Token: 0x060001A6 RID: 422 RVA: 0x000045EC File Offset: 0x000027EC
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

		// Token: 0x04000089 RID: 137
		private readonly CsdlApplyExpression expression;

		// Token: 0x0400008A RID: 138
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x0400008B RID: 139
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400008C RID: 140
		private readonly Cache<CsdlSemanticsApplyExpression, IEdmExpression> appliedOperationCache = new Cache<CsdlSemanticsApplyExpression, IEdmExpression>();

		// Token: 0x0400008D RID: 141
		private static readonly Func<CsdlSemanticsApplyExpression, IEdmExpression> ComputeAppliedOperationFunc = (CsdlSemanticsApplyExpression me) => me.ComputeAppliedOperation();

		// Token: 0x0400008E RID: 142
		private readonly Cache<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>> argumentsCache = new Cache<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>>();

		// Token: 0x0400008F RID: 143
		private static readonly Func<CsdlSemanticsApplyExpression, IEnumerable<IEdmExpression>> ComputeArgumentsFunc = (CsdlSemanticsApplyExpression me) => me.ComputeArguments();
	}
}
