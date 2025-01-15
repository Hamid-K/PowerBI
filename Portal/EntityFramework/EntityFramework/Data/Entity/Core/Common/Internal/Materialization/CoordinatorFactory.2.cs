using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.Internal;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000638 RID: 1592
	internal class CoordinatorFactory<TElement> : CoordinatorFactory
	{
		// Token: 0x06004C91 RID: 19601 RVA: 0x0010E84C File Offset: 0x0010CA4C
		internal CoordinatorFactory(int depth, int stateSlot, Expression<Func<Shaper, bool>> hasData, Expression<Func<Shaper, bool>> setKeys, Expression<Func<Shaper, bool>> checkKeys, CoordinatorFactory[] nestedCoordinators, Expression<Func<Shaper, TElement>> element, Expression<Func<Shaper, IEntityWrapper>> wrappedElement, Expression<Func<Shaper, TElement>> elementWithErrorHandling, Expression<Func<Shaper, ICollection<TElement>>> initializeCollection, RecordStateFactory[] recordStateFactories)
			: base(depth, stateSlot, CoordinatorFactory<TElement>.CompilePredicate(hasData), CoordinatorFactory<TElement>.CompilePredicate(setKeys), CoordinatorFactory<TElement>.CompilePredicate(checkKeys), nestedCoordinators, recordStateFactories)
		{
			this.WrappedElement = ((wrappedElement == null) ? null : wrappedElement.Compile());
			this.Element = ((element == null) ? null : element.Compile());
			this.ElementWithErrorHandling = elementWithErrorHandling.Compile();
			Func<Shaper, ICollection<TElement>> func;
			if (initializeCollection != null)
			{
				func = initializeCollection.Compile();
			}
			else
			{
				func = (Shaper s) => new List<TElement>();
			}
			this.InitializeCollection = func;
			this.Description = new StringBuilder().Append("HasData: ").AppendLine(CoordinatorFactory<TElement>.DescribeExpression(hasData)).Append("SetKeys: ")
				.AppendLine(CoordinatorFactory<TElement>.DescribeExpression(setKeys))
				.Append("CheckKeys: ")
				.AppendLine(CoordinatorFactory<TElement>.DescribeExpression(checkKeys))
				.Append("Element: ")
				.AppendLine((element == null) ? CoordinatorFactory<TElement>.DescribeExpression(wrappedElement) : CoordinatorFactory<TElement>.DescribeExpression(element))
				.Append("ElementWithExceptionHandling: ")
				.AppendLine(CoordinatorFactory<TElement>.DescribeExpression(elementWithErrorHandling))
				.Append("InitializeCollection: ")
				.AppendLine(CoordinatorFactory<TElement>.DescribeExpression(initializeCollection))
				.ToString();
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0010E980 File Offset: 0x0010CB80
		public CoordinatorFactory(int depth, int stateSlot, Expression hasData, Expression setKeys, Expression checkKeys, CoordinatorFactory[] nestedCoordinators, Expression element, Expression elementWithErrorHandling, Expression initializeCollection, RecordStateFactory[] recordStateFactories)
			: this(depth, stateSlot, CodeGenEmitter.BuildShaperLambda<bool>(hasData), CodeGenEmitter.BuildShaperLambda<bool>(setKeys), CodeGenEmitter.BuildShaperLambda<bool>(checkKeys), nestedCoordinators, typeof(IEntityWrapper).IsAssignableFrom(element.Type) ? null : CodeGenEmitter.BuildShaperLambda<TElement>(element), typeof(IEntityWrapper).IsAssignableFrom(element.Type) ? CodeGenEmitter.BuildShaperLambda<IEntityWrapper>(element) : null, CodeGenEmitter.BuildShaperLambda<TElement>(typeof(IEntityWrapper).IsAssignableFrom(element.Type) ? CodeGenEmitter.Emit_UnwrapAndEnsureType(elementWithErrorHandling, typeof(TElement)) : elementWithErrorHandling), CodeGenEmitter.BuildShaperLambda<ICollection<TElement>>(initializeCollection), recordStateFactories)
		{
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0010EA2C File Offset: 0x0010CC2C
		private static Func<Shaper, bool> CompilePredicate(Expression<Func<Shaper, bool>> predicate)
		{
			Func<Shaper, bool> func;
			if (predicate == null)
			{
				func = null;
			}
			else
			{
				func = predicate.Compile();
			}
			return func;
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0010EA48 File Offset: 0x0010CC48
		private static string DescribeExpression(Expression expression)
		{
			string text;
			if (expression == null)
			{
				text = "undefined";
			}
			else
			{
				text = expression.ToString();
			}
			return text;
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0010EA68 File Offset: 0x0010CC68
		internal override Coordinator CreateCoordinator(Coordinator parent, Coordinator next)
		{
			return new Coordinator<TElement>(this, parent, next);
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0010EA74 File Offset: 0x0010CC74
		internal RecordState GetDefaultRecordState(Shaper<RecordState> shaper)
		{
			RecordState recordState = null;
			if (this.RecordStateFactories.Count > 0)
			{
				recordState = (RecordState)shaper.State[this.RecordStateFactories[0].StateSlotNumber];
				recordState.ResetToDefaultState();
			}
			return recordState;
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0010EAB6 File Offset: 0x0010CCB6
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x04001B1F RID: 6943
		internal readonly Func<Shaper, IEntityWrapper> WrappedElement;

		// Token: 0x04001B20 RID: 6944
		internal readonly Func<Shaper, TElement> Element;

		// Token: 0x04001B21 RID: 6945
		internal readonly Func<Shaper, TElement> ElementWithErrorHandling;

		// Token: 0x04001B22 RID: 6946
		internal readonly Func<Shaper, ICollection<TElement>> InitializeCollection;

		// Token: 0x04001B23 RID: 6947
		private readonly string Description;
	}
}
