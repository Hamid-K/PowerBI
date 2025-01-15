using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001611 RID: 5649
	internal static class SimpleActionBinding
	{
		// Token: 0x06008DFA RID: 36346 RVA: 0x001DA961 File Offset: 0x001D8B61
		public static bool TryGetSimpleActionBinding(FunctionValue binding, out FunctionValue simpleBinding, out FunctionValue tailBinding)
		{
			return new SimpleActionBinding.SimpleActionBindingVisitor().TryGetSimpleBinding(binding, out simpleBinding, out tailBinding);
		}

		// Token: 0x04004D38 RID: 19768
		public static readonly FunctionValue ReturnNull = ReturnNullFunctionValue.Instance;

		// Token: 0x04004D39 RID: 19769
		public static readonly FunctionValue ReturnRowCount = ReturnRowCountFunctionValue.Instance;

		// Token: 0x04004D3A RID: 19770
		public static readonly FunctionValue ReturnBinaryLength = ReturnBinaryLengthFunctionValue.Instance;

		// Token: 0x04004D3B RID: 19771
		public static readonly FunctionValue ReturnResult = ReturnResultFunctionValue.Instance;

		// Token: 0x02001612 RID: 5650
		private sealed class SimpleActionBindingVisitor : LogicalAstVisitor<bool>
		{
			// Token: 0x06008DFC RID: 36348 RVA: 0x001DA99C File Offset: 0x001D8B9C
			public bool TryGetSimpleBinding(FunctionValue binding, out FunctionValue simpleBinding, out FunctionValue tailBinding)
			{
				IFunctionExpression functionExpression = binding.Expression as IFunctionExpression;
				if (functionExpression != null && functionExpression.FunctionType.Parameters.Count == 1 && functionExpression.FunctionType.Min == 1)
				{
					functionExpression = (IFunctionExpression)NormalizationVisitor.Normalize(functionExpression, false);
					this.resultUse = SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.None;
					base.VisitFunction(functionExpression, new bool[] { true });
					switch (this.resultUse)
					{
					case SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.None:
						simpleBinding = SimpleActionBinding.ReturnNull;
						tailBinding = binding;
						return true;
					case SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.RowCount:
						simpleBinding = SimpleActionBinding.ReturnRowCount;
						tailBinding = new SimpleActionBinding.ApplyTableFromCountFunctionValue(binding);
						return true;
					case SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.TableGroup:
					{
						Value value;
						this.invocation.Arguments[2].TryGetConstant(out value);
						simpleBinding = new ReturnTableGroupFunctionValue(value);
						tailBinding = new SimpleActionBinding.ApplyTableFromGroupFunctionValue(binding, value);
						return true;
					}
					case SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.BinaryLength:
						simpleBinding = SimpleActionBinding.ReturnBinaryLength;
						tailBinding = new SimpleActionBinding.ApplyDeltaTableFromBinaryLengthFunctionValue(binding);
						return true;
					}
				}
				simpleBinding = null;
				tailBinding = null;
				return false;
			}

			// Token: 0x06008DFD RID: 36349 RVA: 0x001DAA88 File Offset: 0x001D8C88
			protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				IExpression expression;
				IIdentifierExpression identifierExpression;
				if (!fieldAccess.IsOptional && fieldAccess.MemberName.Name == "Value" && fieldAccess.Expression.TryGetFirstItemAccess(out expression) && expression.TryGetIdentifier(out identifierExpression) && base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive))
				{
					this.RecordResultUse(SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.BinaryLength, null);
					return fieldAccess;
				}
				return base.VisitFieldAccess(fieldAccess);
			}

			// Token: 0x06008DFE RID: 36350 RVA: 0x00147007 File Offset: 0x00145207
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				return base.VisitFunction(function, new bool[function.FunctionType.Parameters.Count]);
			}

			// Token: 0x06008DFF RID: 36351 RVA: 0x001DAAF7 File Offset: 0x001D8CF7
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				if (base.Environment.GetValue(identifier.Name, identifier.IsInclusive))
				{
					this.RecordResultUse(SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.Unknown, null);
				}
				return base.VisitIdentifier(identifier);
			}

			// Token: 0x06008E00 RID: 36352 RVA: 0x001DAB24 File Offset: 0x001D8D24
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				IList<IExpression> list;
				IIdentifierExpression identifierExpression;
				if (invocation.TryGetInvocation(TableModule.Table.RowCount, 1, out list) && list[0].TryGetIdentifier(out identifierExpression) && base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive))
				{
					this.RecordResultUse(SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.RowCount, null);
					return invocation;
				}
				Value value;
				if (invocation.TryGetInvocation(TableModule.Table.Group, 3, out list) && list[1].TryGetConstant(out value) && value.IsList && value.AsList.IsEmpty && list[0].TryGetIdentifier(out identifierExpression) && base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive) && list[2].TryGetConstant(out value) && value.IsList)
				{
					this.RecordResultUse(SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.TableGroup, invocation);
					return invocation;
				}
				return base.VisitInvocation(invocation);
			}

			// Token: 0x06008E01 RID: 36353 RVA: 0x00147025 File Offset: 0x00145225
			protected override IExpression VisitLet(ILetExpression let)
			{
				return base.VisitLet(let, new bool[let.Variables.Count]);
			}

			// Token: 0x06008E02 RID: 36354 RVA: 0x0014703E File Offset: 0x0014523E
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				return base.VisitRecord(record, false, new bool[record.Members.Count]);
			}

			// Token: 0x06008E03 RID: 36355 RVA: 0x00147058 File Offset: 0x00145258
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, false);
			}

			// Token: 0x06008E04 RID: 36356 RVA: 0x00147062 File Offset: 0x00145262
			protected override ISection VisitModule(ISection module)
			{
				return base.VisitModule(module, new bool[module.Members.Count]);
			}

			// Token: 0x06008E05 RID: 36357 RVA: 0x001DABFB File Offset: 0x001D8DFB
			private void RecordResultUse(SimpleActionBinding.SimpleActionBindingVisitor.ResultUse newResultUse, IInvocationExpression invocation = null)
			{
				if (this.resultUse == SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.None || this.resultUse == newResultUse)
				{
					this.resultUse = newResultUse;
					this.invocation = invocation;
					return;
				}
				this.resultUse = SimpleActionBinding.SimpleActionBindingVisitor.ResultUse.Unknown;
			}

			// Token: 0x04004D3C RID: 19772
			private SimpleActionBinding.SimpleActionBindingVisitor.ResultUse resultUse;

			// Token: 0x04004D3D RID: 19773
			private IInvocationExpression invocation;

			// Token: 0x02001613 RID: 5651
			private enum ResultUse
			{
				// Token: 0x04004D3F RID: 19775
				None,
				// Token: 0x04004D40 RID: 19776
				RowCount,
				// Token: 0x04004D41 RID: 19777
				TableGroup,
				// Token: 0x04004D42 RID: 19778
				BinaryLength,
				// Token: 0x04004D43 RID: 19779
				Unknown
			}
		}

		// Token: 0x02001614 RID: 5652
		private sealed class ApplyTableFromCountFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06008E07 RID: 36359 RVA: 0x001DAC2C File Offset: 0x001D8E2C
			public ApplyTableFromCountFunctionValue(FunctionValue function)
				: base("count")
			{
				this.function = function;
			}

			// Token: 0x17002546 RID: 9542
			// (get) Token: 0x06008E08 RID: 36360 RVA: 0x001DAC40 File Offset: 0x001D8E40
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						Identifier identifier = Identifier.New();
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(this.function), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableFromCountFunctionValue.Instance), new InclusiveIdentifierExpressionSyntaxNode(identifier))));
					}
					return this.expression;
				}
			}

			// Token: 0x06008E09 RID: 36361 RVA: 0x001DACA0 File Offset: 0x001D8EA0
			public override Value Invoke(Value count)
			{
				return this.function.Invoke(TableFromCountFunctionValue.Instance.Invoke(count));
			}

			// Token: 0x04004D44 RID: 19780
			private readonly FunctionValue function;

			// Token: 0x04004D45 RID: 19781
			private IExpression expression;
		}

		// Token: 0x02001615 RID: 5653
		private sealed class ApplyTableFromGroupFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06008E0A RID: 36362 RVA: 0x001DACB8 File Offset: 0x001D8EB8
			public ApplyTableFromGroupFunctionValue(FunctionValue function, Value aggregatedColumns)
				: base("aggregates")
			{
				this.function = function;
				this.fromGroupFunction = new TableFromGroupFunctionValue(aggregatedColumns);
			}

			// Token: 0x17002547 RID: 9543
			// (get) Token: 0x06008E0B RID: 36363 RVA: 0x001DACD8 File Offset: 0x001D8ED8
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						Identifier identifier = Identifier.New();
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(this.function), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(this.fromGroupFunction), new InclusiveIdentifierExpressionSyntaxNode(identifier))));
					}
					return this.expression;
				}
			}

			// Token: 0x06008E0C RID: 36364 RVA: 0x001DAD39 File Offset: 0x001D8F39
			public override Value Invoke(Value aggregatedRows)
			{
				return this.function.Invoke(this.fromGroupFunction.Invoke(aggregatedRows));
			}

			// Token: 0x04004D46 RID: 19782
			private readonly FunctionValue function;

			// Token: 0x04004D47 RID: 19783
			private readonly TableFromGroupFunctionValue fromGroupFunction;

			// Token: 0x04004D48 RID: 19784
			private IExpression expression;
		}

		// Token: 0x02001616 RID: 5654
		private sealed class ApplyDeltaTableFromBinaryLengthFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06008E0D RID: 36365 RVA: 0x001DAD52 File Offset: 0x001D8F52
			public ApplyDeltaTableFromBinaryLengthFunctionValue(FunctionValue function)
				: base("length")
			{
				this.function = function;
			}

			// Token: 0x17002548 RID: 9544
			// (get) Token: 0x06008E0E RID: 36366 RVA: 0x001DAD68 File Offset: 0x001D8F68
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						Identifier identifier = Identifier.New();
						this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(this.function), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.FromRows), new ListExpressionSyntaxNode(new IExpression[]
						{
							new ListExpressionSyntaxNode(new IExpression[]
							{
								new ConstantExpressionSyntaxNode(ListValue.Empty),
								new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(BinaryFromLengthFunctionValue.Instance), new InclusiveIdentifierExpressionSyntaxNode(identifier))
							})
						}), new ConstantExpressionSyntaxNode(SimpleActionBinding.ApplyDeltaTableFromBinaryLengthFunctionValue.deltaTableColumns))));
					}
					return this.expression;
				}
			}

			// Token: 0x06008E0F RID: 36367 RVA: 0x001DAE10 File Offset: 0x001D9010
			public override Value Invoke(Value length)
			{
				Value value = ListValue.New(new Value[]
				{
					ListValue.Empty,
					BinaryFromLengthFunctionValue.Instance.Invoke(length)
				});
				Value value2 = TableModule.Table.FromRows.Invoke(ListValue.New(new Value[] { value }), SimpleActionBinding.ApplyDeltaTableFromBinaryLengthFunctionValue.deltaTableColumns);
				return this.function.Invoke(value2);
			}

			// Token: 0x04004D49 RID: 19785
			private static readonly Value deltaTableColumns = ListValue.New(new Value[]
			{
				TextValue.New("Path"),
				TextValue.New("Value")
			});

			// Token: 0x04004D4A RID: 19786
			private readonly FunctionValue function;

			// Token: 0x04004D4B RID: 19787
			private IExpression expression;
		}
	}
}
