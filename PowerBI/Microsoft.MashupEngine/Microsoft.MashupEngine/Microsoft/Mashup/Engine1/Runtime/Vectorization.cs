using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016CE RID: 5838
	internal static class Vectorization
	{
		// Token: 0x06009490 RID: 38032 RVA: 0x001EA48C File Offset: 0x001E868C
		public static bool TryVectorize(AddColumnsQuery addColumnsQuery, out IEnumerable<IValueReference> vectorizedEnumerable)
		{
			IList<QueryExpression> queryExpressions = addColumnsQuery.QueryExpressions;
			if (queryExpressions != null)
			{
				bool[] array = new bool[addColumnsQuery.ColumnsConstructor.Names.Length];
				List<string> list = new List<string>();
				KeysBuilder keysBuilder = default(KeysBuilder);
				List<Vectorization.VectorExpression> list2 = new List<Vectorization.VectorExpression>();
				ArrayBuilder<FunctionValue> arrayBuilder = default(ArrayBuilder<FunctionValue>);
				List<IValueReference> list3 = new List<IValueReference>();
				ArrayBuilder<IValueReference> arrayBuilder2 = default(ArrayBuilder<IValueReference>);
				for (int i = 0; i < queryExpressions.Count; i++)
				{
					QueryExpression queryExpression = queryExpressions[i];
					Vectorization.VectorExpression vectorExpression;
					if (Vectorization.VectorExpressionBuilder.TryToVectorExpression(addColumnsQuery.InnerQuery.Columns, queryExpression, out vectorExpression))
					{
						array[i] = true;
						list.Add(addColumnsQuery.ColumnsConstructor.Names[i]);
						list2.Add(vectorExpression);
						list3.Add(addColumnsQuery.ColumnsConstructor.Types[i]);
					}
					else
					{
						keysBuilder.Add(addColumnsQuery.ColumnsConstructor.Names[i]);
						arrayBuilder.Add(QueryExpressionAssembler.Assemble(addColumnsQuery.Columns, queryExpression));
						arrayBuilder2.Add(addColumnsQuery.ColumnsConstructor.Types[i]);
					}
				}
				if (list2.Count > 0)
				{
					TableValue tableValue = new QueryTableValue(addColumnsQuery.InnerQuery);
					if (arrayBuilder.Count > 0)
					{
						tableValue = tableValue.AddColumns(new ColumnsConstructor(keysBuilder.ToKeys(), new TableValue.FunctionsColumnsConstructorFunctionValue(arrayBuilder.ToArray()), arrayBuilder2.ToArray()));
					}
					for (int j = 0; j < list2.Count; j++)
					{
						tableValue = Vectorization.AddVectorExpressionColumnVisitor.Apply(tableValue, list[j], list2[j], list3[j]);
					}
					if (arrayBuilder.Count > 0)
					{
						ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
						columnSelectionBuilder.Add(new ColumnSelection(addColumnsQuery.InnerQuery.Columns));
						int length = addColumnsQuery.InnerQuery.Columns.Length;
						int num = length + arrayBuilder.Count;
						int num2 = 0;
						int num3 = 0;
						for (int k = 0; k < addColumnsQuery.ColumnsConstructor.Names.Length; k++)
						{
							int num4;
							if (array[k])
							{
								num4 = num + num3;
								num3++;
							}
							else
							{
								num4 = length + num2;
								num2++;
							}
							columnSelectionBuilder.Add(addColumnsQuery.ColumnsConstructor.Names[k], num4);
						}
						tableValue = tableValue.SelectColumns(columnSelectionBuilder.ToColumnSelection());
					}
					vectorizedEnumerable = tableValue;
					return true;
				}
			}
			vectorizedEnumerable = null;
			return false;
		}

		// Token: 0x020016CF RID: 5839
		private class VectorExpressionBuilder
		{
			// Token: 0x06009491 RID: 38033 RVA: 0x001EA6F0 File Offset: 0x001E88F0
			public static bool TryToVectorExpression(Keys columns, QueryExpression queryExpression, out Vectorization.VectorExpression vectorExpression)
			{
				Vectorization.VectorExpressionBuilder vectorExpressionBuilder = new Vectorization.VectorExpressionBuilder(columns);
				try
				{
					vectorExpression = vectorExpressionBuilder.VisitExpression(queryExpression);
					if (vectorExpressionBuilder.hasVectorInvocation)
					{
						vectorExpression = Vectorization.NormalizingVectorExpressionVisitor.Normalize(vectorExpression);
						return true;
					}
				}
				catch (NotSupportedException)
				{
				}
				vectorExpression = null;
				return false;
			}

			// Token: 0x06009492 RID: 38034 RVA: 0x001EA740 File Offset: 0x001E8940
			private VectorExpressionBuilder(Keys columns)
			{
				this.columns = columns;
			}

			// Token: 0x06009493 RID: 38035 RVA: 0x001EA74F File Offset: 0x001E894F
			private Vectorization.VectorExpression VisitExpression(QueryExpression expression)
			{
				Vectorization.VectorExpression vectorExpression = this.VisitExpressionCore(expression);
				if (vectorExpression.Kind == Vectorization.VectorExpressionKind.VectorInvocation)
				{
					this.hasVectorInvocation = true;
				}
				return vectorExpression;
			}

			// Token: 0x06009494 RID: 38036 RVA: 0x001EA768 File Offset: 0x001E8968
			private Vectorization.VectorExpression VisitExpressionCore(QueryExpression expression)
			{
				switch (expression.Kind)
				{
				case QueryExpressionKind.Binary:
					return this.VisitBinary((BinaryQueryExpression)expression);
				case QueryExpressionKind.Constant:
					return this.VisitConstant((ConstantQueryExpression)expression);
				case QueryExpressionKind.ColumnAccess:
					return this.VisitColumnAccess((ColumnAccessQueryExpression)expression);
				case QueryExpressionKind.If:
					return this.VisitIf((IfQueryExpression)expression);
				case QueryExpressionKind.Invocation:
					return this.VisitInvocation((InvocationQueryExpression)expression);
				case QueryExpressionKind.Unary:
					return this.VisitUnary((UnaryQueryExpression)expression);
				case QueryExpressionKind.ArgumentAccess:
					return this.VisitArgumentAccess((ArgumentAccessQueryExpression)expression);
				default:
					throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Expression not supported: {0}.", expression.Kind));
				}
			}

			// Token: 0x06009495 RID: 38037 RVA: 0x001EA81C File Offset: 0x001E8A1C
			private Vectorization.VectorExpression VisitBinary(BinaryQueryExpression binary)
			{
				Identifier identifier = Identifier.New();
				Identifier identifier2 = Identifier.New();
				return Vectorization.VectorExpressionBuilder.Transform(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier, identifier2 }), BinaryExpressionSyntaxNode.New(binary.Operator, new InclusiveIdentifierExpressionSyntaxNode(identifier), new InclusiveIdentifierExpressionSyntaxNode(identifier2), TokenRange.Null)), new Vectorization.VectorExpression[]
				{
					this.VisitExpression(binary.Left),
					this.VisitExpression(binary.Right)
				});
			}

			// Token: 0x06009496 RID: 38038 RVA: 0x001EA892 File Offset: 0x001E8A92
			private Vectorization.VectorExpression VisitConstant(ConstantQueryExpression constant)
			{
				return Vectorization.VectorExpressionBuilder.Scalar(constant.Value);
			}

			// Token: 0x06009497 RID: 38039 RVA: 0x001EA8A0 File Offset: 0x001E8AA0
			private Vectorization.VectorExpression VisitArgumentAccess(ArgumentAccessQueryExpression argumentAccess)
			{
				VariableInitializer[] array = new VariableInitializer[this.columns.Length];
				Identifier[] array2 = new Identifier[this.columns.Length];
				Vectorization.VectorExpression[] array3 = new Vectorization.VectorExpression[this.columns.Length];
				for (int i = 0; i < this.columns.Length; i++)
				{
					array2[i] = Identifier.New();
					array3[i] = new Vectorization.ColumnAccessVectorExpression(i);
					array[i] = new VariableInitializer(this.columns[i], new InclusiveIdentifierExpressionSyntaxNode(array2[i]));
				}
				return Vectorization.VectorExpressionBuilder.Transform(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(array2), new RecordExpressionSyntaxNode(Identifier.New(), array, TokenRange.Null)), array3);
			}

			// Token: 0x06009498 RID: 38040 RVA: 0x001EA94D File Offset: 0x001E8B4D
			private Vectorization.VectorExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				return new Vectorization.ColumnAccessVectorExpression(columnAccess.Column);
			}

			// Token: 0x06009499 RID: 38041 RVA: 0x001EA95C File Offset: 0x001E8B5C
			private Vectorization.VectorExpression VisitIf(IfQueryExpression ifExpression)
			{
				Identifier identifier = Identifier.New();
				Identifier identifier2 = Identifier.New();
				Identifier identifier3 = Identifier.New();
				return Vectorization.VectorExpressionBuilder.Transform(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier, identifier2, identifier3 }), new IfExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(identifier), new InclusiveIdentifierExpressionSyntaxNode(identifier2), new InclusiveIdentifierExpressionSyntaxNode(identifier3), TokenRange.Null)), new Vectorization.VectorExpression[]
				{
					this.VisitExpression(ifExpression.Condition),
					this.VisitExpression(ifExpression.TrueCase),
					this.VisitExpression(ifExpression.FalseCase)
				});
			}

			// Token: 0x0600949A RID: 38042 RVA: 0x001EA9EC File Offset: 0x001E8BEC
			private Vectorization.VectorExpression VisitInvocation(InvocationQueryExpression invocation)
			{
				ConstantQueryExpression constantQueryExpression = invocation.Function as ConstantQueryExpression;
				if (constantQueryExpression == null)
				{
					throw new NotSupportedException();
				}
				Vectorization.VectorExpression[] array = new Vectorization.VectorExpression[invocation.Arguments.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.VisitExpression(invocation.Arguments[i]);
				}
				return Vectorization.VectorExpressionBuilder.Transform(new ConstantExpressionSyntaxNode(constantQueryExpression.Value), array);
			}

			// Token: 0x0600949B RID: 38043 RVA: 0x001EAA54 File Offset: 0x001E8C54
			private Vectorization.VectorExpression VisitUnary(UnaryQueryExpression unary)
			{
				Identifier identifier = Identifier.New();
				return Vectorization.VectorExpressionBuilder.Transform(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), UnaryExpressionSyntaxNode.New(unary.Operator, new InclusiveIdentifierExpressionSyntaxNode(identifier), TokenRange.Null)), new Vectorization.VectorExpression[] { this.VisitExpression(unary.Expression) });
			}

			// Token: 0x0600949C RID: 38044 RVA: 0x001EAAAB File Offset: 0x001E8CAB
			private static Vectorization.VectorExpression Scalar(Value value)
			{
				return Vectorization.VectorExpressionBuilder.Transform(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(Array.Empty<Identifier>()), new ConstantExpressionSyntaxNode(value)), Array.Empty<Vectorization.VectorExpression>());
			}

			// Token: 0x0600949D RID: 38045 RVA: 0x001EAACC File Offset: 0x001E8CCC
			private static Vectorization.VectorExpression Transform(IExpression scalarFunction, params Vectorization.VectorExpression[] vectorArguments)
			{
				IConstantExpression constantExpression = scalarFunction as IConstantExpression;
				FunctionValue functionValue;
				if (constantExpression == null || !constantExpression.Value.IsFunction || !constantExpression.Value.AsFunction.TryVectorizeFunction(out functionValue))
				{
					return new Vectorization.ScalarTransformationVectorExpression(scalarFunction, vectorArguments);
				}
				IExpression expression = new ConstantExpressionSyntaxNode(functionValue);
				FunctionTypeValue asFunctionType = constantExpression.Value.Type.AsFunctionType;
				if (vectorArguments.Length < asFunctionType.Min || vectorArguments.Length > asFunctionType.Max)
				{
					throw new NotSupportedException();
				}
				if (vectorArguments.Length < asFunctionType.Max)
				{
					Vectorization.VectorExpression[] array = new Vectorization.VectorExpression[asFunctionType.Max];
					Array.Copy(vectorArguments, array, vectorArguments.Length);
					for (int i = vectorArguments.Length; i < asFunctionType.Max; i++)
					{
						array[i] = Vectorization.VectorExpressionBuilder.NullVectorExpression;
					}
					vectorArguments = array;
				}
				return new Vectorization.VectorInvocationVectorExpression(expression, asFunctionType, vectorArguments);
			}

			// Token: 0x04004EFD RID: 20221
			private static readonly Vectorization.VectorExpression NullVectorExpression = Vectorization.VectorExpressionBuilder.Scalar(Value.Null);

			// Token: 0x04004EFE RID: 20222
			private readonly Keys columns;

			// Token: 0x04004EFF RID: 20223
			private bool hasVectorInvocation;
		}

		// Token: 0x020016D0 RID: 5840
		private class NormalizingVectorExpressionVisitor
		{
			// Token: 0x0600949F RID: 38047 RVA: 0x001EABAA File Offset: 0x001E8DAA
			public static Vectorization.VectorExpression Normalize(Vectorization.VectorExpression expression)
			{
				return Vectorization.NormalizingVectorExpressionVisitor.Instance.Visit(expression);
			}

			// Token: 0x060094A0 RID: 38048 RVA: 0x001EABB8 File Offset: 0x001E8DB8
			private Vectorization.VectorExpression Visit(Vectorization.VectorExpression expression)
			{
				Vectorization.VectorExpressionKind kind = expression.Kind;
				if (kind == Vectorization.VectorExpressionKind.ScalarTransformation)
				{
					return this.VisitScalarTransformation((Vectorization.ScalarTransformationVectorExpression)expression);
				}
				if (kind != Vectorization.VectorExpressionKind.VectorInvocation)
				{
					return expression;
				}
				return this.VisitVectorInvocation((Vectorization.VectorInvocationVectorExpression)expression);
			}

			// Token: 0x060094A1 RID: 38049 RVA: 0x001EABF0 File Offset: 0x001E8DF0
			private Vectorization.VectorExpression VisitScalarTransformation(Vectorization.ScalarTransformationVectorExpression scalarTransformation)
			{
				List<Identifier> list = new List<Identifier>();
				List<Vectorization.VectorExpression> list2 = new List<Vectorization.VectorExpression>();
				IExpression[] array = new IExpression[scalarTransformation.Arguments.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Vectorization.VectorExpression vectorExpression = this.Visit(scalarTransformation.Arguments[i]);
					Vectorization.ScalarTransformationVectorExpression scalarTransformationVectorExpression = vectorExpression as Vectorization.ScalarTransformationVectorExpression;
					if (scalarTransformationVectorExpression != null)
					{
						IExpression[] array2 = new IExpression[scalarTransformationVectorExpression.Arguments.Length];
						for (int j = 0; j < scalarTransformationVectorExpression.Arguments.Length; j++)
						{
							Identifier identifier = Identifier.New();
							list.Add(identifier);
							list2.Add(scalarTransformationVectorExpression.Arguments[j]);
							array2[j] = new InclusiveIdentifierExpressionSyntaxNode(identifier);
						}
						array[i] = new InvocationExpressionSyntaxNodeN(scalarTransformationVectorExpression.ScalarFunction, array2);
					}
					else
					{
						Identifier identifier2 = Identifier.New();
						array[i] = new InclusiveIdentifierExpressionSyntaxNode(identifier2);
						list.Add(identifier2);
						list2.Add(vectorExpression);
					}
				}
				return new Vectorization.ScalarTransformationVectorExpression(NormalizationVisitor.Normalize(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(list.ToArray()), new InvocationExpressionSyntaxNodeN(scalarTransformation.ScalarFunction, array)), true), list2.ToArray());
			}

			// Token: 0x060094A2 RID: 38050 RVA: 0x001EACFC File Offset: 0x001E8EFC
			private Vectorization.VectorExpression VisitVectorInvocation(Vectorization.VectorInvocationVectorExpression vectorInvocation)
			{
				return new Vectorization.VectorInvocationVectorExpression(vectorInvocation.VectorFunction, vectorInvocation.ScalarFunctionType, vectorInvocation.Arguments.Select(new Func<Vectorization.VectorExpression, Vectorization.VectorExpression>(this.Visit)).ToArray<Vectorization.VectorExpression>());
			}

			// Token: 0x04004F00 RID: 20224
			private static readonly Vectorization.NormalizingVectorExpressionVisitor Instance = new Vectorization.NormalizingVectorExpressionVisitor();
		}

		// Token: 0x020016D1 RID: 5841
		private class AddVectorExpressionColumnVisitor
		{
			// Token: 0x060094A5 RID: 38053 RVA: 0x001EAD38 File Offset: 0x001E8F38
			public static TableValue Apply(TableValue table, string name, Vectorization.VectorExpression expression, IValueReference type)
			{
				Vectorization.AddVectorExpressionColumnVisitor addVectorExpressionColumnVisitor = new Vectorization.AddVectorExpressionColumnVisitor(table);
				ColumnSelection columnSelection = new ColumnSelection(table.Columns);
				columnSelection = columnSelection.Add(name, addVectorExpressionColumnVisitor.Visit(expression, type));
				return addVectorExpressionColumnVisitor.current.SelectColumns(columnSelection);
			}

			// Token: 0x060094A6 RID: 38054 RVA: 0x001EAD74 File Offset: 0x001E8F74
			private AddVectorExpressionColumnVisitor(TableValue initial)
			{
				this.current = initial;
			}

			// Token: 0x060094A7 RID: 38055 RVA: 0x001EAD84 File Offset: 0x001E8F84
			private int Visit(Vectorization.VectorExpression expression, IValueReference type = null)
			{
				switch (expression.Kind)
				{
				case Vectorization.VectorExpressionKind.ScalarTransformation:
					return this.VisitScalarTransformation((Vectorization.ScalarTransformationVectorExpression)expression, type);
				case Vectorization.VectorExpressionKind.VectorInvocation:
					return this.VisitVectorInvocation((Vectorization.VectorInvocationVectorExpression)expression, type);
				case Vectorization.VectorExpressionKind.ColumnAccess:
					return this.VisitColumnAccess((Vectorization.ColumnAccessVectorExpression)expression);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060094A8 RID: 38056 RVA: 0x001EADDC File Offset: 0x001E8FDC
			private int VisitScalarTransformation(Vectorization.ScalarTransformationVectorExpression scalarTransformation, IValueReference type = null)
			{
				ColumnSelection columnSelection = new ColumnSelection(this.current.Columns);
				IExpression[] array = new IExpression[scalarTransformation.Arguments.Length];
				for (int i = 0; i < scalarTransformation.Arguments.Length; i++)
				{
					int num = this.Visit(scalarTransformation.Arguments[i], null);
					string text = this.current.Columns[num];
					array[i] = new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), text);
				}
				IExpression expression = new InvocationExpressionSyntaxNodeN(scalarTransformation.ScalarFunction, array);
				expression = NormalizationVisitor.Normalize(expression, true);
				FunctionValue functionValue = new Compiler(CompileOptions.None).ToFunction(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { Identifier.Underscore }), new ListExpressionSyntaxNode(new IExpression[] { expression })));
				string text2 = this.NewKey();
				this.current = this.current.AddColumns(new ColumnsConstructor(Keys.New(text2), functionValue, new IValueReference[] { type ?? TypeValue.Any }));
				this.current = this.current.SelectColumns(columnSelection.Add(text2, this.current.Columns.Length - 1));
				return this.current.Columns.Length - 1;
			}

			// Token: 0x060094A9 RID: 38057 RVA: 0x001EAF20 File Offset: 0x001E9120
			private int VisitVectorInvocation(Vectorization.VectorInvocationVectorExpression vectorInvocation, IValueReference type = null)
			{
				ColumnSelection columnSelection = new ColumnSelection(this.current.Columns);
				ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
				for (int i = 0; i < vectorInvocation.Arguments.Length; i++)
				{
					int num = this.Visit(vectorInvocation.Arguments[i], null);
					columnSelectionBuilder.Add(vectorInvocation.ParameterNames[i], num);
				}
				IExpression expression = new InvocationExpressionSyntaxNodeN(vectorInvocation.VectorFunction, new IExpression[]
				{
					new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore)
				});
				expression = NormalizationVisitor.Normalize(expression, true);
				FunctionValue functionValue = new Compiler(CompileOptions.None).ToFunction(new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { Identifier.Underscore }), expression));
				string text = this.NewKey();
				TypeValue asType = (type ?? TypeValue.Any).Value.AsType;
				RecordTypeValue recordTypeValue = RecordTypeValue.New(this.current.Type.AsTableType.ItemType.Fields.Concatenate(RecordValue.New(new NamedValue[]
				{
					new NamedValue(text, RecordTypeValue.NewField(asType, null))
				})).AsRecord);
				this.current = new ListTableValue(ListValue.New(new VectorizedAddColumnEnumerable(this.current, this.current.Type.AsTableType, recordTypeValue.Fields.Keys, functionValue, columnSelectionBuilder.ToColumnSelection(), vectorInvocation.ScalarFunctionType)), TableTypeValue.New(recordTypeValue));
				this.current = this.current.SelectColumns(columnSelection.Add(text, this.current.Columns.Length - 1));
				return this.current.Columns.Length - 1;
			}

			// Token: 0x060094AA RID: 38058 RVA: 0x001EB0C4 File Offset: 0x001E92C4
			private int VisitColumnAccess(Vectorization.ColumnAccessVectorExpression columnAccess)
			{
				return columnAccess.Column;
			}

			// Token: 0x060094AB RID: 38059 RVA: 0x001EB0CC File Offset: 0x001E92CC
			private string NewKey()
			{
				return JoinQuery.EnsureUniqueKey("Column", this.current.Columns);
			}

			// Token: 0x04004F01 RID: 20225
			private TableValue current;
		}

		// Token: 0x020016D2 RID: 5842
		private abstract class VectorExpression
		{
			// Token: 0x170026FB RID: 9979
			// (get) Token: 0x060094AC RID: 38060
			public abstract Vectorization.VectorExpressionKind Kind { get; }
		}

		// Token: 0x020016D3 RID: 5843
		private class ScalarTransformationVectorExpression : Vectorization.VectorExpression
		{
			// Token: 0x060094AE RID: 38062 RVA: 0x001EB0E3 File Offset: 0x001E92E3
			public ScalarTransformationVectorExpression(IExpression scalarFunction, params Vectorization.VectorExpression[] arguments)
			{
				this.scalarFunction = scalarFunction;
				this.arguments = arguments;
			}

			// Token: 0x170026FC RID: 9980
			// (get) Token: 0x060094AF RID: 38063 RVA: 0x00002105 File Offset: 0x00000305
			public override Vectorization.VectorExpressionKind Kind
			{
				get
				{
					return Vectorization.VectorExpressionKind.ScalarTransformation;
				}
			}

			// Token: 0x170026FD RID: 9981
			// (get) Token: 0x060094B0 RID: 38064 RVA: 0x001EB0F9 File Offset: 0x001E92F9
			public IExpression ScalarFunction
			{
				get
				{
					return this.scalarFunction;
				}
			}

			// Token: 0x170026FE RID: 9982
			// (get) Token: 0x060094B1 RID: 38065 RVA: 0x001EB101 File Offset: 0x001E9301
			public Vectorization.VectorExpression[] Arguments
			{
				get
				{
					return this.arguments;
				}
			}

			// Token: 0x04004F02 RID: 20226
			private readonly IExpression scalarFunction;

			// Token: 0x04004F03 RID: 20227
			private readonly Vectorization.VectorExpression[] arguments;
		}

		// Token: 0x020016D4 RID: 5844
		private class VectorInvocationVectorExpression : Vectorization.VectorExpression
		{
			// Token: 0x060094B2 RID: 38066 RVA: 0x001EB109 File Offset: 0x001E9309
			public VectorInvocationVectorExpression(IExpression vectorFunction, FunctionTypeValue scalarFunctionType, Vectorization.VectorExpression[] arguments)
			{
				this.vectorFunction = vectorFunction;
				this.scalarFunctionType = scalarFunctionType;
				this.arguments = arguments;
			}

			// Token: 0x170026FF RID: 9983
			// (get) Token: 0x060094B3 RID: 38067 RVA: 0x00002139 File Offset: 0x00000339
			public override Vectorization.VectorExpressionKind Kind
			{
				get
				{
					return Vectorization.VectorExpressionKind.VectorInvocation;
				}
			}

			// Token: 0x17002700 RID: 9984
			// (get) Token: 0x060094B4 RID: 38068 RVA: 0x001EB126 File Offset: 0x001E9326
			public IExpression VectorFunction
			{
				get
				{
					return this.vectorFunction;
				}
			}

			// Token: 0x17002701 RID: 9985
			// (get) Token: 0x060094B5 RID: 38069 RVA: 0x001EB12E File Offset: 0x001E932E
			public Keys ParameterNames
			{
				get
				{
					return this.scalarFunctionType.Parameters.Keys;
				}
			}

			// Token: 0x17002702 RID: 9986
			// (get) Token: 0x060094B6 RID: 38070 RVA: 0x001EB140 File Offset: 0x001E9340
			public Vectorization.VectorExpression[] Arguments
			{
				get
				{
					return this.arguments;
				}
			}

			// Token: 0x17002703 RID: 9987
			// (get) Token: 0x060094B7 RID: 38071 RVA: 0x001EB148 File Offset: 0x001E9348
			public FunctionTypeValue ScalarFunctionType
			{
				get
				{
					return this.scalarFunctionType;
				}
			}

			// Token: 0x04004F04 RID: 20228
			private readonly IExpression vectorFunction;

			// Token: 0x04004F05 RID: 20229
			private readonly FunctionTypeValue scalarFunctionType;

			// Token: 0x04004F06 RID: 20230
			private readonly Vectorization.VectorExpression[] arguments;
		}

		// Token: 0x020016D5 RID: 5845
		private class ColumnAccessVectorExpression : Vectorization.VectorExpression
		{
			// Token: 0x060094B8 RID: 38072 RVA: 0x001EB150 File Offset: 0x001E9350
			public ColumnAccessVectorExpression(int column)
			{
				this.column = column;
			}

			// Token: 0x17002704 RID: 9988
			// (get) Token: 0x060094B9 RID: 38073 RVA: 0x000023C4 File Offset: 0x000005C4
			public override Vectorization.VectorExpressionKind Kind
			{
				get
				{
					return Vectorization.VectorExpressionKind.ColumnAccess;
				}
			}

			// Token: 0x17002705 RID: 9989
			// (get) Token: 0x060094BA RID: 38074 RVA: 0x001EB15F File Offset: 0x001E935F
			public int Column
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x04004F07 RID: 20231
			private readonly int column;
		}

		// Token: 0x020016D6 RID: 5846
		private enum VectorExpressionKind
		{
			// Token: 0x04004F09 RID: 20233
			ScalarTransformation,
			// Token: 0x04004F0A RID: 20234
			VectorInvocation,
			// Token: 0x04004F0B RID: 20235
			ColumnAccess
		}
	}
}
