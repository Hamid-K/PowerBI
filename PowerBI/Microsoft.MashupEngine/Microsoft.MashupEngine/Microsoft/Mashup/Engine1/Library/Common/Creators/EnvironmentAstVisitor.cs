using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x02001198 RID: 4504
	internal abstract class EnvironmentAstVisitor<TBinding> : LogicalAstVisitor<TBinding>
	{
		// Token: 0x060076F4 RID: 30452 RVA: 0x0019D164 File Offset: 0x0019B364
		protected EnvironmentAstVisitor(IExpression rootExpression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
		{
			this.cursor = cursor;
			this.externalEnvironment = externalEnvironment;
			this.rootExpression = rootExpression;
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x060076F5 RID: 30453 RVA: 0x0019D181 File Offset: 0x0019B381
		protected LogicalAstToCachedTypeflowResultCursor Cursor
		{
			get
			{
				return this.cursor;
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x060076F6 RID: 30454 RVA: 0x0019D189 File Offset: 0x0019B389
		protected EnvironmentBase ExternalEnvironment
		{
			get
			{
				return this.externalEnvironment;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x060076F7 RID: 30455 RVA: 0x0019D191 File Offset: 0x0019B391
		protected IExpression RootExpression
		{
			get
			{
				return this.rootExpression;
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x060076F8 RID: 30456 RVA: 0x0019D199 File Offset: 0x0019B399
		protected FoldingTracingService FoldingTracingService
		{
			get
			{
				return this.ExternalEnvironment.FoldingTracingService;
			}
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x0019D1A6 File Offset: 0x0019B3A6
		private void EnterFunctionScope(IFunctionExpression function, FunctionTypeValue functionType, TypeValue[] types, TBinding[] bindings)
		{
			base.EnterScope(function, bindings);
			this.cursor.Push(function, types);
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x0019D1BE File Offset: 0x0019B3BE
		private void ExitFunctionScope(IFunctionExpression function)
		{
			this.cursor.Pop();
			base.ExitScope(function);
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x0019D1D4 File Offset: 0x0019B3D4
		protected static TypeValue[] GetParameterTypes(FunctionTypeValue functionType)
		{
			TypeValue[] array = new TypeValue[functionType.ParameterCount];
			for (int i = 0; i < functionType.ParameterCount; i++)
			{
				array[i] = functionType.ParameterType(i);
			}
			return array;
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x0019D20C File Offset: 0x0019B40C
		protected TypeValue GetType(IExpression expression)
		{
			TypeValue typeValue;
			if (!this.cursor.TryGetResult(expression, out typeValue))
			{
				this.TypeLookupFailed();
			}
			return typeValue;
		}

		// Token: 0x060076FD RID: 30461 RVA: 0x0000EE09 File Offset: 0x0000D009
		protected virtual void TypeLookupFailed()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060076FE RID: 30462 RVA: 0x0019D230 File Offset: 0x0019B430
		protected bool MultifieldProjectionIsNoop(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			TypeValue typeValue = TypeServices.StripNullableAndMetadata(this.GetType(multiFieldRecordProjection.Expression));
			TypeValue typeValue2 = TypeServices.StripNullableAndMetadata(this.GetType(multiFieldRecordProjection));
			if (typeValue.Equals(typeValue2))
			{
				return true;
			}
			if (typeValue.TypeKind != ValueKind.Table || typeValue2.TypeKind != ValueKind.Table)
			{
				return false;
			}
			RecordTypeValue asRecordType = typeValue.AsTableType.ItemType.AsRecordType;
			RecordTypeValue asRecordType2 = typeValue2.AsTableType.ItemType.AsRecordType;
			return asRecordType.Fields.Keys.Equals(asRecordType2.Fields.Keys);
		}

		// Token: 0x060076FF RID: 30463 RVA: 0x0019D2B8 File Offset: 0x0019B4B8
		protected static IExpression Reduce(IExpression expression)
		{
			if (expression != null && expression.Kind == ExpressionKind.Binary)
			{
				IBinaryExpression binaryExpression = (IBinaryExpression)expression;
				if (binaryExpression.Operator == BinaryOperator2.As)
				{
					return EnvironmentAstVisitor<TBinding>.Reduce(binaryExpression.Left);
				}
			}
			return expression;
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x0019D2EE File Offset: 0x0019B4EE
		protected TypeValue VisitLambda(IFunctionExpression function, FunctionTypeValue functionType, TypeValue[] parameterTypes, Func<IExpression, IExpression> visit, TBinding[] bindings)
		{
			this.EnterFunctionScope(function, functionType, parameterTypes, bindings);
			visit(function.Expression);
			TypeValue type = this.GetType(function.Expression);
			this.ExitFunctionScope(function);
			return type;
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			return tryCatchExceptionCase;
		}

		// Token: 0x06007702 RID: 30466 RVA: 0x000091AE File Offset: 0x000073AE
		protected override ISection VisitModule(ISection module)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040040CB RID: 16587
		protected static readonly Identifier PlaceholderIdentifier = Identifier.Underscore;

		// Token: 0x040040CC RID: 16588
		private readonly LogicalAstToCachedTypeflowResultCursor cursor;

		// Token: 0x040040CD RID: 16589
		private readonly EnvironmentBase externalEnvironment;

		// Token: 0x040040CE RID: 16590
		private readonly IExpression rootExpression;
	}
}
