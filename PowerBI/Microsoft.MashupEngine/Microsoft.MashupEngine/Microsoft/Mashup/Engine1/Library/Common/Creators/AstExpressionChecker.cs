using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x02001176 RID: 4470
	internal abstract class AstExpressionChecker<TBinding> : EnvironmentAstVisitor<TBinding>
	{
		// Token: 0x0600753A RID: 30010 RVA: 0x00191B08 File Offset: 0x0018FD08
		protected AstExpressionChecker(IExpression rootExpression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(rootExpression, cursor, externalEnvironment)
		{
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x0600753B RID: 30011 RVA: 0x00191B13 File Offset: 0x0018FD13
		protected CheckerContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x0600753C RID: 30012 RVA: 0x00191B1B File Offset: 0x0018FD1B
		protected void Check()
		{
			this.Check(CheckerContext.New(ContextLabel.None, new Action<CheckerContext>(this.SetCurrentContext)));
		}

		// Token: 0x0600753D RID: 30013 RVA: 0x00191B35 File Offset: 0x0018FD35
		protected void CheckStatement()
		{
			this.CheckStatement(CheckerContext.New(ContextLabel.None, new Action<CheckerContext>(this.SetCurrentContext)));
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x00191B50 File Offset: 0x0018FD50
		protected void Check(CheckerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
			using (this.Context.Enter(ContextLabel.Root, base.FoldingTracingService))
			{
				this.CheckInternal();
			}
		}

		// Token: 0x0600753F RID: 30015 RVA: 0x00191BA8 File Offset: 0x0018FDA8
		protected void CheckStatement(CheckerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
			using (this.Context.Enter(ContextLabel.Root, base.FoldingTracingService))
			{
				try
				{
					this.CheckStatementInternal();
				}
				catch (FoldingFailureException ex)
				{
					ValueException ex2 = ex.InnerException as ValueException;
					if (ex2 != null)
					{
						throw ex2;
					}
					throw;
				}
			}
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x00191C20 File Offset: 0x0018FE20
		protected virtual void CheckInternal()
		{
			base.Visit(base.RootExpression);
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x00191C30 File Offset: 0x0018FE30
		protected virtual void CheckStatementInternal()
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.CheckStatementInternal");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007542 RID: 30018 RVA: 0x00191C84 File Offset: 0x0018FE84
		protected void CheckListSelectInvocation(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckListSelectInvocation"))
			{
				using (this.Context.Enter(ContextLabel.Select, base.FoldingTracingService))
				{
					IExpression expression = invocation.Arguments[0];
					this.VisitExpression(expression);
					using (this.Context.Enter(ContextLabel.SelectBody, base.FoldingTracingService))
					{
						this.VisitListFunctionArgumentAsLambda(invocation, 1, new Func<IExpression, IExpression>(this.VisitExpression));
					}
				}
			}
		}

		// Token: 0x06007543 RID: 30019 RVA: 0x00191D3C File Offset: 0x0018FF3C
		protected void CheckListSortInvocation(IInvocationExpression invocation)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckListSortInvocation"))
			{
				using (this.Context.Enter(ContextLabel.Sort, base.FoldingTracingService))
				{
					IExpression expression = invocation.Arguments[0];
					this.VisitExpression(expression);
					using (this.Context.Enter(ContextLabel.SortBody, base.FoldingTracingService))
					{
						RecordTypeValue itemType = base.GetType(invocation.Arguments[0]).AsTableType.ItemType;
						if (invocation.Arguments.Count < 2)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						IListExpression listExpression = EnvironmentAstVisitor<TBinding>.Reduce(invocation.Arguments[1]) as IListExpression;
						if (listExpression == null)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						if (listExpression.Members.Count == 0)
						{
							throw base.FoldingTracingService.NewFoldingFailureException(null);
						}
						for (int i = 0; i < listExpression.Members.Count; i++)
						{
							IRecordExpression recordExpression = (IRecordExpression)EnvironmentAstVisitor<TBinding>.Reduce(listExpression.Members[i]);
							base.Cursor.Push(listExpression, i);
							if (recordExpression.Members.FirstOrDefault((VariableInitializer m) => m.Name.Equals("KeyComparer")).Value != null)
							{
								throw base.FoldingTracingService.NewFoldingFailureException(null);
							}
							IFunctionExpression functionExpression = EnvironmentAstVisitor<TBinding>.Reduce(recordExpression.Members.FirstOrDefault((VariableInitializer m) => m.Name.Equals("KeySelector")).Value) as IFunctionExpression;
							if (functionExpression == null)
							{
								throw base.FoldingTracingService.NewFoldingFailureException(null);
							}
							base.Cursor.Push(recordExpression, "KeySelector");
							FunctionTypeValue asFunctionType = base.GetType(functionExpression).AsFunctionType;
							IFunctionExpression functionExpression2 = functionExpression;
							FunctionTypeValue functionTypeValue = asFunctionType;
							TypeValue[] array = new RecordTypeValue[] { itemType };
							TypeValue typeValue = base.VisitLambda(functionExpression2, functionTypeValue, array, new Func<IExpression, IExpression>(this.VisitExpression), this.CreateDefaultBindingsFromParameterTypes(asFunctionType));
							this.CheckRequiredScalarType(typeValue);
							base.Cursor.Pop();
							base.Cursor.Pop();
						}
					}
				}
			}
		}

		// Token: 0x06007544 RID: 30020 RVA: 0x00191FD4 File Offset: 0x001901D4
		protected TypeValue CheckListTransformInvocation(IInvocationExpression invocation)
		{
			TypeValue typeValue;
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckListTransformInvocation"))
			{
				using (this.Context.Enter(ContextLabel.Transform, base.FoldingTracingService))
				{
					IExpression expression = invocation.Arguments[0];
					this.VisitExpression(expression);
					using (this.Context.Enter(ContextLabel.TransformBody, base.FoldingTracingService))
					{
						typeValue = this.VisitListFunctionArgumentAsLambda(invocation, 1, new Func<IExpression, IExpression>(this.VisitExpression));
					}
				}
			}
			return typeValue;
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x00192090 File Offset: 0x00190290
		protected void CheckQueryResultValueHasConsistentEnvironment(IQueryResultValue value, IConstantExpression errorTerm)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckQueryResultValueHasConsistentEnvironment"))
			{
				if (!base.ExternalEnvironment.OtherCanFoldToThis(value.Environment))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x001920EC File Offset: 0x001902EC
		protected List<string> CheckRelationalAlgebraFunctionArgumentAsColumns(IListExpression columns)
		{
			List<string> list2;
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckRelationalAlgebraFunctionArgumentAsColumns"))
			{
				List<string> list = new List<string>();
				for (int i = 0; i < columns.Members.Count; i++)
				{
					Value value;
					if (!columns.Members[i].TryGetConstant(out value) || !value.IsText)
					{
						throw base.FoldingTracingService.NewFoldingFailureException(null);
					}
					list.Add(value.AsString);
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x00192180 File Offset: 0x00190380
		protected void CheckRequiredScalarType(TypeValue type)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.CheckRequiredScalarType"))
			{
				if (!TypeServices.IsScalar(type))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x06007548 RID: 30024
		protected abstract TBinding[] CreateDefaultBindingsFromParameterTypes(FunctionTypeValue functionType);

		// Token: 0x06007549 RID: 30025 RVA: 0x001921D0 File Offset: 0x001903D0
		protected void SetCurrentContext(CheckerContext context)
		{
			this.context = context;
		}

		// Token: 0x0600754A RID: 30026 RVA: 0x001921DC File Offset: 0x001903DC
		protected bool ValidateExpressionTypeIsValid(IExpression expression)
		{
			bool flag;
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.ValidateExpressionTypeIsValid"))
			{
				if (!TypeServices.IsValid(base.GetType(expression)))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600754B RID: 30027 RVA: 0x00192234 File Offset: 0x00190434
		protected void ValidateRequireComparableType(IExpression node)
		{
			TypeValue type = base.GetType(node);
			this.ValidateRequireComparableType(type);
		}

		// Token: 0x0600754C RID: 30028 RVA: 0x00192250 File Offset: 0x00190450
		protected void ValidateRequireComparableType(TypeValue type)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.ValidateRequireComparableType"))
			{
				if (!DbTypeServices.IsComparable(type))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
			}
		}

		// Token: 0x0600754D RID: 30029 RVA: 0x001922A0 File Offset: 0x001904A0
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			IExpression expression;
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.VisitIdentifier"))
			{
				TBinding tbinding;
				if (!base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out tbinding))
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				expression = base.VisitIdentifier(identifier);
			}
			return expression;
		}

		// Token: 0x0600754E RID: 30030 RVA: 0x0019230C File Offset: 0x0019050C
		protected override IExpression VisitElementAccess(IElementAccessExpression elementAccess)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitElementAccess");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600754F RID: 30031 RVA: 0x00192360 File Offset: 0x00190560
		protected override IExpression VisitExports(IExportsExpression exports)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitExports");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x001923B4 File Offset: 0x001905B4
		protected override IExpression VisitExpression(IExpression expression)
		{
			if (!this.ValidateExpressionTypeIsValid(expression))
			{
				return expression;
			}
			return base.VisitExpression(expression);
		}

		// Token: 0x06007551 RID: 30033 RVA: 0x001923C8 File Offset: 0x001905C8
		protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			if (this.Context.Milestone == ContextLabel.SortBody || this.Context.Parent.Milestone == ContextLabel.SortBody)
			{
				this.ValidateRequireComparableType(fieldAccess);
			}
			return base.VisitFieldAccess(fieldAccess);
		}

		// Token: 0x06007552 RID: 30034 RVA: 0x001923FC File Offset: 0x001905FC
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitFunction");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x00192450 File Offset: 0x00190650
		protected override IExpression VisitFunctionType(IFunctionTypeExpression functionType)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitFunctionType");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007554 RID: 30036 RVA: 0x001924A4 File Offset: 0x001906A4
		protected override IExpression VisitLet(ILetExpression let)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitLet");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007555 RID: 30037 RVA: 0x001924F8 File Offset: 0x001906F8
		protected override IExpression VisitList(IListExpression list)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitList");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007556 RID: 30038 RVA: 0x0019254C File Offset: 0x0019074C
		protected TypeValue VisitListFunctionArgumentAsLambda(IInvocationExpression invocation, int argumentIndex, Func<IExpression, IExpression> visit)
		{
			return this.VisitListFunctionArgumentAsLambda(invocation, argumentIndex, visit, this.CreateDefaultBindingsFromParameterTypes(base.GetType(invocation.Arguments[argumentIndex]).AsFunctionType));
		}

		// Token: 0x06007557 RID: 30039 RVA: 0x00192574 File Offset: 0x00190774
		protected TypeValue VisitListFunctionArgumentAsLambda(IInvocationExpression invocation, int argumentIndex, Func<IExpression, IExpression> visit, TBinding[] bindings)
		{
			TypeValue typeValue;
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.VisitListFunctionArgumentAsLambda"))
			{
				IFunctionExpression functionExpression = EnvironmentAstVisitor<TBinding>.Reduce(invocation.Arguments[argumentIndex]) as IFunctionExpression;
				if (functionExpression == null)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				FunctionTypeValue asFunctionType = base.Cursor.GetParameterResults(invocation)[argumentIndex].AsFunctionType;
				typeValue = base.VisitLambda(functionExpression, asFunctionType, EnvironmentAstVisitor<TBinding>.GetParameterTypes(asFunctionType), visit, bindings);
			}
			return typeValue;
		}

		// Token: 0x06007558 RID: 30040 RVA: 0x001925FC File Offset: 0x001907FC
		protected override IExpression VisitListType(IListTypeExpression listType)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitListType");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x00192650 File Offset: 0x00190850
		protected override IExpression VisitTableType(ITableTypeExpression tableType)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitTableType");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600755A RID: 30042 RVA: 0x001926A4 File Offset: 0x001908A4
		protected override IExpression VisitNullableType(INullableTypeExpression nullableType)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitNullableType");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600755B RID: 30043 RVA: 0x001926F8 File Offset: 0x001908F8
		protected IExpression VisitRecord(IRecordExpression record, TBinding identifier, List<TBinding> bindings)
		{
			using (base.FoldingTracingService.NewScope("AstExpressionChecker.VisitRecord"))
			{
				if (record.Members.Count == 0)
				{
					throw base.FoldingTracingService.NewFoldingFailureException(null);
				}
				IRecordExpression currentRecord = this.Context.CurrentRecord;
				this.Context.CurrentRecord = record;
				base.VisitRecord(record, identifier, bindings);
				this.Context.CurrentRecord = currentRecord;
			}
			return record;
		}

		// Token: 0x0600755C RID: 30044 RVA: 0x00192780 File Offset: 0x00190980
		protected override IExpression VisitRecordType(IRecordTypeExpression recordType)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitRecordType");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600755D RID: 30045 RVA: 0x001927D4 File Offset: 0x001909D4
		protected override IExpression VisitThrow(IThrowExpression @throw)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitThrow");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600755E RID: 30046 RVA: 0x00192828 File Offset: 0x00190A28
		protected override IExpression VisitTryCatch(ITryCatchExpression tryCatch)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitTryCatch");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x0019287C File Offset: 0x00190A7C
		protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.VisitTryCatchExceptionCase");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException("Not implemented");
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_002B;
				}
				goto IL_002B;
				IL_002B:;
			}
		}

		// Token: 0x06007560 RID: 30048 RVA: 0x001928D0 File Offset: 0x00190AD0
		protected override void TypeLookupFailed()
		{
			IDisposable disposable = base.FoldingTracingService.NewScope("AstExpressionChecker.TypeLookupFailed");
			try
			{
				throw base.FoldingTracingService.NewFoldingFailureException(null);
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
					goto IL_0027;
				}
				goto IL_0027;
				IL_0027:;
			}
		}

		// Token: 0x04004061 RID: 16481
		private CheckerContext context;
	}
}
