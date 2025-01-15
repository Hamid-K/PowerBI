using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017C6 RID: 6086
	internal static class AggregationToAccumulationRewriter
	{
		// Token: 0x060099FB RID: 39419 RVA: 0x001FE301 File Offset: 0x001FC501
		public static bool TryRewrite(RecordTypeValue rowType, FunctionValue constructor, out IAccumulable accumulable, out FunctionValue resultExtractor)
		{
			return AggregationToAccumulationRewriter.AccumulableBuilder.TryRewrite(rowType, constructor, out accumulable, out resultExtractor);
		}

		// Token: 0x060099FC RID: 39420 RVA: 0x001FE30C File Offset: 0x001FC50C
		private static bool TryNormalizeInvocationArguments(InvocationQueryExpression invocation, FunctionTypeValue functionType, out InvocationQueryExpression normalizedInvocation)
		{
			int count = invocation.Arguments.Count;
			int max = functionType.Max;
			if (count < functionType.Min || count > max)
			{
				normalizedInvocation = null;
				return false;
			}
			if (count == max)
			{
				normalizedInvocation = invocation;
				return true;
			}
			IList<QueryExpression> list = new QueryExpression[max];
			for (int i = 0; i < count; i++)
			{
				list[i] = invocation.Arguments[i];
			}
			for (int j = count; j < max; j++)
			{
				list[j] = new ConstantQueryExpression(Value.Null);
			}
			normalizedInvocation = new InvocationQueryExpression(invocation.Function, list);
			return true;
		}

		// Token: 0x060099FD RID: 39421 RVA: 0x001FE3A0 File Offset: 0x001FC5A0
		private static bool TryGetArguments(InvocationQueryExpression invocation, RecordTypeValue rowType, string enumerableParameter, out QueryExpression enumeratingArgument, out RecordValue arguments)
		{
			enumeratingArgument = null;
			arguments = null;
			Value value;
			if (!invocation.Function.TryGetConstant(out value) || !value.IsFunction)
			{
				return false;
			}
			FunctionValue asFunction = value.AsFunction;
			if (!AggregationToAccumulationRewriter.TryNormalizeInvocationArguments(invocation, asFunction.Type.AsFunctionType, out invocation))
			{
				return false;
			}
			RecordBuilder recordBuilder = new RecordBuilder(invocation.Arguments.Count - 1);
			for (int i = 0; i < invocation.Arguments.Count; i++)
			{
				QueryExpression queryExpression = invocation.Arguments[i];
				string text = asFunction.Type.AsFunctionType.ParameterName(i);
				Value value2;
				if (queryExpression.TryGetConstant(out value2) && text != enumerableParameter)
				{
					recordBuilder.Add(text, value2, value2.Type);
				}
				else
				{
					if (!(text == enumerableParameter))
					{
						return false;
					}
					enumeratingArgument = queryExpression;
				}
			}
			if (enumeratingArgument == null)
			{
				return false;
			}
			Value value3;
			if (AggregationToAccumulationRewriter.TryCreateSymbolicValue(enumeratingArgument, rowType, out value3))
			{
				recordBuilder.Add(enumerableParameter, value3, TypeValue.Any);
			}
			arguments = recordBuilder.ToRecord();
			return true;
		}

		// Token: 0x060099FE RID: 39422 RVA: 0x001FE4A4 File Offset: 0x001FC6A4
		private static bool TryCreateSymbolicValue(QueryExpression expression, RecordTypeValue rowType, out Value symbolicValue)
		{
			try
			{
				TableValue tableValue = ListValue.New(1, delegate(int i)
				{
					throw new NotSupportedException();
				}).ToTable(TableTypeValue.New(rowType));
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(rowType.FieldKeys, expression);
				symbolicValue = functionValue.Invoke(tableValue);
				return true;
			}
			catch (ValueException)
			{
			}
			catch (NotSupportedException)
			{
			}
			symbolicValue = null;
			return false;
		}

		// Token: 0x020017C7 RID: 6087
		private sealed class AccumulableBuilder : QueryExpressionVisitor
		{
			// Token: 0x060099FF RID: 39423 RVA: 0x001FE524 File Offset: 0x001FC724
			public static bool TryRewrite(RecordTypeValue rowType, FunctionValue constructor, out IAccumulable accumulable, out FunctionValue resultExtractor)
			{
				AggregationToAccumulationRewriter.AccumulableBuilder accumulableBuilder = new AggregationToAccumulationRewriter.AccumulableBuilder(rowType);
				bool flag;
				try
				{
					QueryExpression queryExpression = accumulableBuilder.Visit(QueryExpressionBuilder.ToQueryExpression(TableTypeValue.New(rowType), constructor));
					Keys keys = accumulableBuilder.accumulableKeysBuilder.ToKeys();
					List<IAccumulable> list = accumulableBuilder.accumulables;
					accumulable = new RecordAccumulable(keys, list);
					resultExtractor = QueryExpressionAssembler.Assemble(keys, queryExpression);
					flag = true;
				}
				catch (NotSupportedException)
				{
					accumulable = null;
					resultExtractor = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x06009A00 RID: 39424 RVA: 0x001FE594 File Offset: 0x001FC794
			private AccumulableBuilder(RecordTypeValue rowType)
			{
				this.rowType = rowType;
				this.accumulableKeysBuilder = default(KeysBuilder);
				this.accumulables = new List<IAccumulable>();
			}

			// Token: 0x06009A01 RID: 39425 RVA: 0x001FE5BC File Offset: 0x001FC7BC
			protected override QueryExpression VisitInvocation(InvocationQueryExpression invocation)
			{
				QueryExpression queryExpression;
				if (this.TryRewrite(invocation, out queryExpression))
				{
					return queryExpression;
				}
				return base.VisitInvocation(invocation);
			}

			// Token: 0x06009A02 RID: 39426 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009A03 RID: 39427 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override QueryExpression VisitArgumentAccess(ArgumentAccessQueryExpression argument)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009A04 RID: 39428 RVA: 0x001FE5E0 File Offset: 0x001FC7E0
			private bool TryRewrite(InvocationQueryExpression invocation, out QueryExpression resultExtractor)
			{
				resultExtractor = null;
				Value value;
				if (!invocation.Function.TryGetConstant(out value) || !value.IsFunction)
				{
					return false;
				}
				IAccumulableFunction accumulableFunction;
				if (!value.AsFunction.TryGetAccumulableFunction(out accumulableFunction))
				{
					return false;
				}
				QueryExpression queryExpression;
				RecordValue recordValue;
				if (!AggregationToAccumulationRewriter.TryGetArguments(invocation, this.rowType, accumulableFunction.EnumerableParameter, out queryExpression, out recordValue))
				{
					return false;
				}
				IAccumulable accumulable;
				try
				{
					accumulable = accumulableFunction.CreateAccumulable(recordValue);
				}
				catch (ValueException)
				{
					return false;
				}
				if (!AggregationToAccumulationRewriter.AccumulatorChainBuilder.TryRewrite(this.rowType, queryExpression, accumulable, out accumulable))
				{
					return false;
				}
				Identifier identifier = Identifier.New();
				resultExtractor = new ColumnAccessQueryExpression(this.accumulables.Count);
				this.accumulableKeysBuilder.Add(identifier);
				this.accumulables.Add(new AggregationToAccumulationRewriter.ExceptionHandlingAccumulable(accumulable));
				return true;
			}

			// Token: 0x04005164 RID: 20836
			private readonly RecordTypeValue rowType;

			// Token: 0x04005165 RID: 20837
			private readonly List<IAccumulable> accumulables;

			// Token: 0x04005166 RID: 20838
			private KeysBuilder accumulableKeysBuilder;
		}

		// Token: 0x020017C8 RID: 6088
		private sealed class AccumulatorChainBuilder
		{
			// Token: 0x06009A05 RID: 39429 RVA: 0x001FE6AC File Offset: 0x001FC8AC
			public static bool TryRewrite(RecordTypeValue rowType, QueryExpression tableExpression, IAccumulable accumulable, out IAccumulable chained)
			{
				bool flag;
				try
				{
					AggregationToAccumulationRewriter.AccumulatorChainBuilder accumulatorChainBuilder = new AggregationToAccumulationRewriter.AccumulatorChainBuilder(rowType);
					chained = accumulatorChainBuilder.Visit(tableExpression, accumulable);
					flag = true;
				}
				catch (NotSupportedException)
				{
					chained = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x06009A06 RID: 39430 RVA: 0x001FE6E8 File Offset: 0x001FC8E8
			private AccumulatorChainBuilder(RecordTypeValue rowType)
			{
				this.rowType = rowType;
			}

			// Token: 0x06009A07 RID: 39431 RVA: 0x001FE6F8 File Offset: 0x001FC8F8
			private IAccumulable Visit(QueryExpression expression, IAccumulable accumulable)
			{
				switch (expression.Kind)
				{
				case QueryExpressionKind.ColumnAccess:
					return this.VisitColumnAccess((ColumnAccessQueryExpression)expression, accumulable);
				case QueryExpressionKind.Invocation:
					return this.VisitInvocation((InvocationQueryExpression)expression, accumulable);
				case QueryExpressionKind.ArgumentAccess:
					return this.VisitArgumentAccess((ArgumentAccessQueryExpression)expression, accumulable);
				}
				throw new NotSupportedException();
			}

			// Token: 0x06009A08 RID: 39432 RVA: 0x001FE759 File Offset: 0x001FC959
			private IAccumulable VisitColumnAccess(ColumnAccessQueryExpression columnAccess, IAccumulable accumulable)
			{
				return new AggregationToAccumulationRewriter.AccumulatorChainBuilder.ColumnAccessAccumulable(accumulable, this.rowType.FieldKeys[columnAccess.Column]);
			}

			// Token: 0x06009A09 RID: 39433 RVA: 0x000020F7 File Offset: 0x000002F7
			private IAccumulable VisitArgumentAccess(ArgumentAccessQueryExpression argument, IAccumulable accumulable)
			{
				return accumulable;
			}

			// Token: 0x06009A0A RID: 39434 RVA: 0x001FE778 File Offset: 0x001FC978
			private IAccumulable VisitInvocation(InvocationQueryExpression invocation, IAccumulable accumulable)
			{
				Value value;
				if (!invocation.Function.TryGetConstant(out value) || !value.IsFunction)
				{
					throw new NotSupportedException();
				}
				IAccumulableChainingFunction accumulableChainingFunction;
				if (!value.AsFunction.TryGetAccumulableChainingFunction(out accumulableChainingFunction))
				{
					throw new NotSupportedException();
				}
				QueryExpression queryExpression;
				RecordValue recordValue;
				if (!AggregationToAccumulationRewriter.TryGetArguments(invocation, this.rowType, accumulableChainingFunction.EnumerableParameter, out queryExpression, out recordValue))
				{
					throw new NotSupportedException();
				}
				IAccumulable accumulable2;
				try
				{
					accumulable2 = accumulableChainingFunction.CreateAccumulable(recordValue, accumulable);
				}
				catch (ValueException)
				{
					throw new NotSupportedException();
				}
				return this.Visit(queryExpression, accumulable2);
			}

			// Token: 0x04005167 RID: 20839
			private readonly RecordTypeValue rowType;

			// Token: 0x020017C9 RID: 6089
			private sealed class ColumnAccessAccumulable : IAccumulable
			{
				// Token: 0x06009A0B RID: 39435 RVA: 0x001FE804 File Offset: 0x001FCA04
				public ColumnAccessAccumulable(IAccumulable accumulable, string field)
				{
					this.accumulable = accumulable;
					this.field = field;
				}

				// Token: 0x06009A0C RID: 39436 RVA: 0x001FE81A File Offset: 0x001FCA1A
				public IAccumulator CreateAccumulator()
				{
					return new AggregationToAccumulationRewriter.AccumulatorChainBuilder.ColumnAccessAccumulable.ColumnAccessAccumulator(this);
				}

				// Token: 0x04005168 RID: 20840
				private readonly IAccumulable accumulable;

				// Token: 0x04005169 RID: 20841
				private readonly string field;

				// Token: 0x020017CA RID: 6090
				private sealed class ColumnAccessAccumulator : TransformingAccumulator
				{
					// Token: 0x06009A0D RID: 39437 RVA: 0x001FE822 File Offset: 0x001FCA22
					public ColumnAccessAccumulator(AggregationToAccumulationRewriter.AccumulatorChainBuilder.ColumnAccessAccumulable accumulable)
						: base(accumulable.accumulable.CreateAccumulator())
					{
						this.field = accumulable.field;
					}

					// Token: 0x06009A0E RID: 39438 RVA: 0x001FE841 File Offset: 0x001FCA41
					protected override IValueReference Transform(IValueReference valueReference)
					{
						return new RequiredFieldAccessValueReference(valueReference, this.field);
					}

					// Token: 0x0400516A RID: 20842
					private readonly string field;
				}
			}
		}

		// Token: 0x020017CB RID: 6091
		private sealed class ExceptionHandlingAccumulable : IAccumulable
		{
			// Token: 0x06009A0F RID: 39439 RVA: 0x001FE84F File Offset: 0x001FCA4F
			public ExceptionHandlingAccumulable(IAccumulable accumulable)
			{
				this.accumulable = accumulable;
			}

			// Token: 0x06009A10 RID: 39440 RVA: 0x001FE85E File Offset: 0x001FCA5E
			public IAccumulator CreateAccumulator()
			{
				return new AggregationToAccumulationRewriter.ExceptionHandlingAccumulable.ExceptionHandlingAccumulator(this.accumulable.CreateAccumulator());
			}

			// Token: 0x0400516B RID: 20843
			private readonly IAccumulable accumulable;

			// Token: 0x020017CC RID: 6092
			private sealed class ExceptionHandlingAccumulator : IAccumulator
			{
				// Token: 0x06009A11 RID: 39441 RVA: 0x001FE870 File Offset: 0x001FCA70
				public ExceptionHandlingAccumulator(IAccumulator accumulator)
				{
					this.accumulator = accumulator;
				}

				// Token: 0x170027B4 RID: 10164
				// (get) Token: 0x06009A12 RID: 39442 RVA: 0x001FE880 File Offset: 0x001FCA80
				public IValueReference Current
				{
					get
					{
						ValueException ex = this.exception;
						if (ex == null)
						{
							try
							{
								return this.accumulator.Current;
							}
							catch (ValueException ex)
							{
							}
						}
						return new ExceptionValueReference(ex);
					}
				}

				// Token: 0x06009A13 RID: 39443 RVA: 0x001FE8C0 File Offset: 0x001FCAC0
				public void AccumulateNext(IValueReference next)
				{
					if (this.exception == null)
					{
						try
						{
							this.accumulator.AccumulateNext(next);
						}
						catch (ValueException ex)
						{
							this.exception = ex;
						}
					}
				}

				// Token: 0x0400516C RID: 20844
				private readonly IAccumulator accumulator;

				// Token: 0x0400516D RID: 20845
				private ValueException exception;
			}
		}
	}
}
