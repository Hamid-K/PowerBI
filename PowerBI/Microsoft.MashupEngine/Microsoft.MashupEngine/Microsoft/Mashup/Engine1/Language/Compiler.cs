using System;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001723 RID: 5923
	internal class Compiler
	{
		// Token: 0x0600967A RID: 38522 RVA: 0x001F294B File Offset: 0x001F0B4B
		public Compiler(CompileOptions options)
		{
			this.options = options;
			this.argumentEnvironment = new Environment<Instruction>();
			this.memberEnvironment = new Compiler.MemberEnvironment();
		}

		// Token: 0x0600967B RID: 38523 RVA: 0x001F2970 File Offset: 0x001F0B70
		public Module Compile(IExpressionDocument document, RecordValue environment)
		{
			Compiler.Import2[] array;
			Assembly assembly = new Compiler.DummyAssembly(this.Compile(document, environment, out array));
			return new RuntimeModule(this.GetImports(array), Compiler.GetLocations(document, array), assembly);
		}

		// Token: 0x0600967C RID: 38524 RVA: 0x001F29A4 File Offset: 0x001F0BA4
		private FunctionValue Compile(IExpressionDocument document, RecordValue environment, out Compiler.Import2[] imports)
		{
			IFunctionExpression functionExpression = Compiler.FieldBindingVisitor.Rewrite(environment, new Compiler.SectionEnvironment(), document.Expression, out imports);
			this.document = document;
			FunctionValue functionValue = this.ToFunction(functionExpression);
			this.document = null;
			IFunctionExpression functionExpression2 = new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(Compiler.environmentIdentifier, null)
			}, 1), new RecordExpressionSyntaxNode(Compiler.recordIdentifier, new VariableInitializer[]
			{
				new VariableInitializer(Compiler.entry, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(functionValue), new InclusiveIdentifierExpressionSyntaxNode(Compiler.environmentIdentifier))),
				new VariableInitializer(Compiler.shared, ConstantExpressionSyntaxNode.EmptyRecord),
				new VariableInitializer(Compiler.section, ConstantExpressionSyntaxNode.EmptyRecord)
			}, TokenRange.Null));
			return this.ToFunction(functionExpression2);
		}

		// Token: 0x0600967D RID: 38525 RVA: 0x001F2A6C File Offset: 0x001F0C6C
		public Module Compile(ISectionDocument document, RecordValue environment)
		{
			ISection section = document.Section;
			List<string> list = new List<string>();
			List<SourceLocation> list2 = new List<SourceLocation>();
			List<string> list3 = new List<string>();
			List<VariableInitializer> list4 = new List<VariableInitializer>();
			List<VariableInitializer> list5 = new List<VariableInitializer>();
			IList<ISectionMember> members = document.Section.Members;
			for (int i = 0; i < members.Count; i++)
			{
				ISectionMember sectionMember = members[i];
				if (sectionMember.Export)
				{
					Identifier name = sectionMember.Name;
					list.Add(name.Name);
					list2.Add(Compiler.GetLocation(document, sectionMember.Name.Range));
					list4.Add(new VariableInitializer(name, sectionMember.Value));
				}
				list3.Add(sectionMember.Name);
				list5.Add(new VariableInitializer(sectionMember.Name, sectionMember.Value));
			}
			ListExpressionSyntaxNode listExpressionSyntaxNode = new ListExpressionSyntaxNode(new IExpression[]
			{
				new RecordExpressionSyntaxNode(list4.ToArray()),
				new RecordExpressionSyntaxNode(list5.ToArray())
			});
			Compiler.Import2[] array;
			IFunctionExpression functionExpression = Compiler.FieldBindingVisitor.Rewrite(environment, new Compiler.SectionEnvironment(document.Section), listExpressionSyntaxNode, out array);
			this.document = document;
			FunctionValue functionValue = this.ToFunction(functionExpression);
			this.document = null;
			IFunctionExpression functionExpression2 = new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(Compiler.environmentIdentifier, null)
			}, 1), new RecordExpressionSyntaxNode(Compiler.recordIdentifier, new VariableInitializer[]
			{
				new VariableInitializer(Compiler.entry, ConstantExpressionSyntaxNode.Null),
				new VariableInitializer(Compiler.shared, new RequiredElementAccessExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Compiler.recordIdentifier), Compiler.binding), new ConstantExpressionSyntaxNode(NumberValue.New(0)))),
				new VariableInitializer(Compiler.section, new RequiredElementAccessExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Compiler.recordIdentifier), Compiler.binding), new ConstantExpressionSyntaxNode(NumberValue.New(1)))),
				new VariableInitializer(Compiler.binding, new InvocationExpressionSyntaxNode0(new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(functionValue), new InclusiveIdentifierExpressionSyntaxNode(Compiler.environmentIdentifier))))
			}, TokenRange.Null));
			Assembly assembly = new Compiler.DummyAssembly(this.ToFunction(functionExpression2));
			return new RuntimeModule((section.SectionName != null) ? section.SectionName.Name : null, ExpressionAnalysis.GetAttributeValue(section.Attribute) ?? RecordValue.Empty, this.GetImports(array), Compiler.GetLocations(document, array), Keys.New(list.ToArray()), Keys.New(list3.ToArray()), list2.ToArray(), assembly);
		}

		// Token: 0x0600967E RID: 38526 RVA: 0x001F2CFC File Offset: 0x001F0EFC
		public FunctionValue ToFunction(IFunctionExpression expression)
		{
			this.memberEnvironment.EnterScope();
			Instruction instruction = this.Compile(expression);
			if (this.memberEnvironment.ExitScope().Length != 0)
			{
				throw new InvalidOperationException();
			}
			FunctionValue functionValue;
			if (!Compiler.TryGetFunction(instruction, out functionValue))
			{
				throw new InvalidOperationException();
			}
			return functionValue;
		}

		// Token: 0x0600967F RID: 38527 RVA: 0x001F2D40 File Offset: 0x001F0F40
		private Instruction Compile(IExpression expression)
		{
			if (this.currentDepth > 1050)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DocumentReader_ParseDepth, null, null);
			}
			this.currentDepth++;
			Instruction instruction;
			try
			{
				instruction = this._Compile(expression);
			}
			finally
			{
				this.currentDepth--;
			}
			if (this.options == CompileOptions.Debug && Compiler.CanThrow(expression))
			{
				SourceLocation location = Compiler.GetLocation(this.document, expression);
				if (location != null)
				{
					instruction = new DebugInstruction(instruction, location);
				}
			}
			return instruction;
		}

		// Token: 0x06009680 RID: 38528 RVA: 0x001F2DC8 File Offset: 0x001F0FC8
		private Instruction _Compile(IExpression expression)
		{
			switch (expression.Kind)
			{
			case ExpressionKind.Binary:
				return this.CompileBinary((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return this.CompileConstant((IConstantExpression)expression);
			case ExpressionKind.ElementAccess:
				return this.CompileElementAccess((IElementAccessExpression)expression);
			case ExpressionKind.FieldAccess:
				return this.CompileFieldAccess((IFieldAccessExpression)expression);
			case ExpressionKind.Function:
				return this.CompileFunction((IFunctionExpression)expression);
			case ExpressionKind.Identifier:
				return this.CompileIdentifier((IIdentifierExpression)expression);
			case ExpressionKind.If:
				return this.CompileIf((IIfExpression)expression);
			case ExpressionKind.Invocation:
				return this.CompileInvocation((IInvocationExpression)expression);
			case ExpressionKind.List:
				return this.CompileList((IListExpression)expression);
			case ExpressionKind.MultiFieldRecordProjection:
				return this.CompileMultiFieldRecordProjection((IMultiFieldRecordProjectionExpression)expression);
			case ExpressionKind.Record:
				return this.CompileRecord((IRecordExpression)expression);
			case ExpressionKind.Throw:
				return this.CompileThrow((IThrowExpression)expression);
			case ExpressionKind.TryCatch:
				return this.CompileTryCatch((ITryCatchExpression)expression);
			case ExpressionKind.Unary:
				return this.CompileUnary((IUnaryExpression)expression);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06009681 RID: 38529 RVA: 0x001F2EF6 File Offset: 0x001F10F6
		private Instruction CompileConstant(IConstantExpression constant)
		{
			return new ConstantInstruction(constant.Value);
		}

		// Token: 0x06009682 RID: 38530 RVA: 0x001F2F03 File Offset: 0x001F1103
		private Instruction CompileBinary(IBinaryExpression binary)
		{
			if (binary.Operator == BinaryOperator2.Coalesce)
			{
				return this.CompileCoalesce(binary);
			}
			return Compiler.CompileBinary(binary.Operator, this.Compile(binary.Left), this.Compile(binary.Right));
		}

		// Token: 0x06009683 RID: 38531 RVA: 0x001F2F3C File Offset: 0x001F113C
		private Instruction CompileCoalesce(IBinaryExpression binary)
		{
			IIdentifierExpression identifierExpression = new InclusiveIdentifierExpressionSyntaxNode(Identifier.New());
			IIdentifierExpression identifierExpression2 = new InclusiveIdentifierExpressionSyntaxNode("left");
			IExpression expression = new RequiredFieldAccessExpressionSyntaxNode(identifierExpression, identifierExpression2.Name, TokenRange.Null);
			return this.CompileFieldAccess(new RequiredFieldAccessExpressionSyntaxNode(new RecordExpressionSyntaxNode(identifierExpression.Name, new VariableInitializer[]
			{
				new VariableInitializer(identifierExpression2.Name, binary.Left),
				new VariableInitializer(Identifier.Underscore, new IfExpressionSyntaxNode(BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, expression, ConstantExpressionSyntaxNode.Null, TokenRange.Null), binary.Right, expression, TokenRange.Null))
			}, TokenRange.Null), Identifier.Underscore));
		}

		// Token: 0x06009684 RID: 38532 RVA: 0x001F2FE8 File Offset: 0x001F11E8
		private static Instruction CompileBinary(BinaryOperator2 binaryOperator, Instruction left, Instruction right)
		{
			switch (binaryOperator)
			{
			case BinaryOperator2.Add:
				return new AddInstruction(left, right);
			case BinaryOperator2.Subtract:
				return new FunctionInvocationInstruction2(BinaryOperator.Subtract, left, right);
			case BinaryOperator2.Multiply:
				return new FunctionInvocationInstruction2(BinaryOperator.Multiply, left, right);
			case BinaryOperator2.Divide:
				return new FunctionInvocationInstruction2(BinaryOperator.Divide, left, right);
			case BinaryOperator2.GreaterThan:
				return new FunctionInvocationInstruction2(BinaryOperator.GreaterThan, left, right);
			case BinaryOperator2.LessThan:
				return new FunctionInvocationInstruction2(BinaryOperator.LessThan, left, right);
			case BinaryOperator2.GreaterThanOrEquals:
				return new FunctionInvocationInstruction2(BinaryOperator.GreaterThanOrEqual, left, right);
			case BinaryOperator2.LessThanOrEquals:
				return new FunctionInvocationInstruction2(BinaryOperator.LessThanOrEqual, left, right);
			case BinaryOperator2.Equals:
				return Compiler.CompileBinaryEquals(left, right);
			case BinaryOperator2.NotEquals:
				return new NotInstruction(Compiler.CompileBinaryEquals(left, right));
			case BinaryOperator2.And:
				return new AndInstruction(left, right);
			case BinaryOperator2.Or:
				return new OrInstruction(left, right);
			case BinaryOperator2.MetadataAdd:
				return new FunctionInvocationInstruction2(BinaryOperator.AddMeta, left, right);
			case BinaryOperator2.Range:
				return new FunctionInvocationInstruction2(BinaryOperator.Range, left, right);
			case BinaryOperator2.Concatenate:
				return new FunctionInvocationInstruction2(BinaryOperator.Concatenate, left, right);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06009685 RID: 38533 RVA: 0x001F30FC File Offset: 0x001F12FC
		private static Instruction CompileBinaryEquals(Instruction left, Instruction right)
		{
			Value value;
			if (left.TryGetConstant(out value) && value.IsNumber)
			{
				return new NumberEqualsInstruction(right, value.AsNumber);
			}
			if (right.TryGetConstant(out value) && value.IsNumber)
			{
				return new NumberEqualsInstruction(left, value.AsNumber);
			}
			return new EqualsInstruction(left, right);
		}

		// Token: 0x06009686 RID: 38534 RVA: 0x001F314E File Offset: 0x001F134E
		private Instruction CompileUnary(IUnaryExpression unary)
		{
			return Compiler.CompileUnary(unary.Operator, this.Compile(unary.Expression));
		}

		// Token: 0x06009687 RID: 38535 RVA: 0x001F3167 File Offset: 0x001F1367
		private static Instruction CompileUnary(UnaryOperator2 unaryOperator, Instruction instruction)
		{
			switch (unaryOperator)
			{
			case UnaryOperator2.Not:
				return new FunctionInvocationInstruction1(UnaryOperator.Not, instruction);
			case UnaryOperator2.Negative:
				return new FunctionInvocationInstruction1(UnaryOperator.Negate, instruction);
			case UnaryOperator2.Positive:
				return new FunctionInvocationInstruction1(UnaryOperator.Identity, instruction);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009688 RID: 38536 RVA: 0x001F31A6 File Offset: 0x001F13A6
		private Instruction CompileIf(IIfExpression ifExpression)
		{
			return new IfInstruction(this.Compile(ifExpression.Condition), this.Compile(ifExpression.TrueCase), this.Compile(ifExpression.FalseCase));
		}

		// Token: 0x06009689 RID: 38537 RVA: 0x001F31D4 File Offset: 0x001F13D4
		private Instruction CompileList(IListExpression list)
		{
			Instruction[] array = new Instruction[list.Members.Count];
			BitArray bitArray = new BitArray(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				IExpression expression = list.Members[i];
				if (Compiler.CreateElementClosure(expression))
				{
					expression = new FunctionExpressionSyntaxNode(Compiler.zeroParamFunctionType, expression);
					bitArray[i] = true;
				}
				array[i] = this.Compile(expression);
			}
			return new ListInstruction(array, bitArray);
		}

		// Token: 0x0600968A RID: 38538 RVA: 0x001F3245 File Offset: 0x001F1445
		private static bool CreateElementClosure(IExpression expression)
		{
			return Compiler.CanThrow(expression);
		}

		// Token: 0x0600968B RID: 38539 RVA: 0x001F3250 File Offset: 0x001F1450
		private Instruction CompileElementAccess(IElementAccessExpression elementAccess)
		{
			Instruction instruction = this.Compile(elementAccess.Collection);
			Instruction instruction2 = this.Compile(elementAccess.Key);
			if (elementAccess.IsOptional)
			{
				return new FunctionInvocationInstruction2(Library.List.ElementOrNullWithListCheck, instruction, instruction2);
			}
			return new FunctionInvocationInstruction2(Library.List.ElementWithListCheck, instruction, instruction2);
		}

		// Token: 0x0600968C RID: 38540 RVA: 0x001F3298 File Offset: 0x001F1498
		private Instruction CompileRecord(IRecordExpression record)
		{
			KeysBuilder keysBuilder = new KeysBuilder(record.Members.Count);
			for (int i = 0; i < record.Members.Count; i++)
			{
				keysBuilder.Add(record.Members[i].Name.Name);
			}
			Keys keys = keysBuilder.ToKeys();
			Instruction[] array = new Instruction[record.Members.Count];
			BitArray bitArray = new BitArray(record.Members.Count);
			for (int j = 0; j < record.Members.Count; j++)
			{
				IExpression value = record.Members[j].Value;
				if (Compiler.CreateFieldClosure(record, value))
				{
					IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
					{
						new ParameterSyntaxNode(record.Identifier, null)
					}, 1), value);
					array[j] = this.Compile(functionExpression);
					bitArray[j] = true;
				}
				else
				{
					array[j] = this.Compile(value);
				}
			}
			return new RecordInstruction(keys, array, bitArray);
		}

		// Token: 0x0600968D RID: 38541 RVA: 0x001F33AC File Offset: 0x001F15AC
		private static bool CreateFieldClosure(IRecordExpression record, IExpression expression)
		{
			ExpressionKind kind = expression.Kind;
			return kind != ExpressionKind.Constant && (kind != ExpressionKind.Identifier || ((IIdentifierExpression)expression).Name == record.Identifier);
		}

		// Token: 0x0600968E RID: 38542 RVA: 0x001F33EC File Offset: 0x001F15EC
		private Instruction CompileFieldAccess(IFieldAccessExpression fieldAccess)
		{
			Instruction instruction = this.Compile(fieldAccess.Expression);
			if (fieldAccess.IsOptional)
			{
				return new FunctionInvocationInstruction2(Library.Collection.FieldOrNull, instruction, new ConstantInstruction(TextValue.New(fieldAccess.MemberName.Name)));
			}
			return new FieldAccessInstruction(instruction, fieldAccess.MemberName.Name);
		}

		// Token: 0x0600968F RID: 38543 RVA: 0x001F3440 File Offset: 0x001F1640
		private Instruction CompileMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			FunctionValue functionValue = (multiFieldRecordProjection.IsOptional ? Library.Collection.ProjectFieldsIfExistOrNull : Library.Collection.ProjectFields);
			Instruction instruction = this.Compile(multiFieldRecordProjection.Expression);
			Value[] array = new Value[multiFieldRecordProjection.MemberNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TextValue.New(multiFieldRecordProjection.MemberNames[i].Name);
			}
			return new FunctionInvocationInstruction2(functionValue, instruction, new ConstantInstruction(ListValue.New(array)));
		}

		// Token: 0x06009690 RID: 38544 RVA: 0x001F34BC File Offset: 0x001F16BC
		private Instruction CompileFunction(IFunctionExpression function)
		{
			this.argumentEnvironment.EnterScope();
			this.memberEnvironment.EnterScope();
			for (int i = 0; i < function.FunctionType.Parameters.Count; i++)
			{
				this.argumentEnvironment.Add(function.FunctionType.Parameters[i].Identifier, ArgumentAccessInstruction.Argument(i));
			}
			Instruction instruction = this.Compile(function.Expression);
			for (int j = 0; j < function.FunctionType.Parameters.Count; j++)
			{
				this.argumentEnvironment.Remove(function.FunctionType.Parameters[j].Identifier);
			}
			Identifier[] array = this.memberEnvironment.ExitScope();
			this.argumentEnvironment.ExitScope();
			FunctionTypeValue functionTypeValue = Compiler.CompileFunctionType(function.FunctionType);
			if (array.Length == 0)
			{
				return new ConstantInstruction(RuntimeFunctionValue.New(instruction, functionTypeValue, function));
			}
			Instruction[] array2 = new Instruction[array.Length];
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k] = this.CompileIdentifier(array[k]);
			}
			return new FunctionInstruction(instruction, functionTypeValue, function, array2, array.ToArray<Identifier>());
		}

		// Token: 0x06009691 RID: 38545 RVA: 0x001F35E0 File Offset: 0x001F17E0
		private static FunctionTypeValue CompileFunctionType(IFunctionTypeExpression functionType)
		{
			IEnumerable<IParameter> parameters = functionType.Parameters;
			KeysBuilder keysBuilder = default(KeysBuilder);
			foreach (IParameter parameter in parameters)
			{
				keysBuilder.Add(parameter.Identifier.Name);
			}
			return FunctionTypeValue.New(TypeValue.Any, RecordValue.New(keysBuilder.ToKeys(), (int x) => TypeValue.Any), functionType.Min);
		}

		// Token: 0x06009692 RID: 38546 RVA: 0x001F367C File Offset: 0x001F187C
		private Instruction CompileIdentifier(IIdentifierExpression identifier)
		{
			return this.CompileIdentifier(identifier.Name);
		}

		// Token: 0x06009693 RID: 38547 RVA: 0x001F368C File Offset: 0x001F188C
		private Instruction CompileIdentifier(Identifier identifier)
		{
			Instruction value;
			if (!this.argumentEnvironment.TryGetValueAtCurrentDepth(identifier, true, out value))
			{
				value = this.memberEnvironment.GetValue(identifier);
			}
			return value;
		}

		// Token: 0x06009694 RID: 38548 RVA: 0x001F36B8 File Offset: 0x001F18B8
		private Instruction CompileInvocation(IInvocationExpression invocation)
		{
			Instruction instruction = this.Compile(invocation.Function);
			IList<IExpression> arguments = invocation.Arguments;
			switch (arguments.Count)
			{
			case 0:
				return Compiler.CompileInvocation(instruction);
			case 1:
				return Compiler.CompileInvocation(instruction, this.Compile(arguments[0]));
			case 2:
				return Compiler.CompileInvocation(instruction, this.Compile(arguments[0]), this.Compile(arguments[1]));
			default:
			{
				Instruction[] array = new Instruction[arguments.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.Compile(arguments[i]);
				}
				return new InstructionInvocationInstruction(instruction, array);
			}
			}
		}

		// Token: 0x06009695 RID: 38549 RVA: 0x001F3768 File Offset: 0x001F1968
		private static Instruction CompileInvocation(Instruction function)
		{
			FunctionValue functionValue;
			if (Compiler.TryGetFunction(function, out functionValue))
			{
				return new FunctionInvocationInstruction0(functionValue);
			}
			return new InstructionInvocationInstruction0(function);
		}

		// Token: 0x06009696 RID: 38550 RVA: 0x001F378C File Offset: 0x001F198C
		private static Instruction CompileInvocation(Instruction function, Instruction arg0)
		{
			FunctionValue functionValue;
			if (Compiler.TryGetFunction(function, out functionValue))
			{
				return new FunctionInvocationInstruction1(functionValue, arg0);
			}
			return new InstructionInvocationInstruction1(function, arg0);
		}

		// Token: 0x06009697 RID: 38551 RVA: 0x001F37B4 File Offset: 0x001F19B4
		private static Instruction CompileInvocation(Instruction function, Instruction arg0, Instruction arg1)
		{
			FunctionValue functionValue;
			if (Compiler.TryGetFunction(function, out functionValue))
			{
				return new FunctionInvocationInstruction2(functionValue, arg0, arg1);
			}
			return new InstructionInvocationInstruction2(function, arg0, arg1);
		}

		// Token: 0x06009698 RID: 38552 RVA: 0x001F37DC File Offset: 0x001F19DC
		private static bool TryGetFunction(Instruction instruction, out FunctionValue functionValue)
		{
			Value value;
			if (instruction.TryGetConstant(out value) && value.IsFunction)
			{
				functionValue = value.AsFunction;
				return true;
			}
			functionValue = null;
			return false;
		}

		// Token: 0x06009699 RID: 38553 RVA: 0x001F3809 File Offset: 0x001F1A09
		private Instruction CompileThrow(IThrowExpression throwExpression)
		{
			return new ThrowInstruction(this.Compile(throwExpression.Expression));
		}

		// Token: 0x0600969A RID: 38554 RVA: 0x001F381C File Offset: 0x001F1A1C
		private Instruction CompileTryCatch(ITryCatchExpression tryCatch)
		{
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(tryCatch.ExceptionCase.Variable, null)
			}, 1), tryCatch.ExceptionCase.Expression);
			return new TryCatchInstruction(this.Compile(tryCatch.Try), this.Compile(functionExpression));
		}

		// Token: 0x0600969B RID: 38555 RVA: 0x001F387C File Offset: 0x001F1A7C
		private static bool CanThrow(IExpression expression)
		{
			ExpressionKind kind = expression.Kind;
			switch (kind)
			{
			case ExpressionKind.Binary:
				return Compiler.CanThrow((IBinaryExpression)expression);
			case ExpressionKind.Constant:
			case ExpressionKind.Function:
			case ExpressionKind.Identifier:
				break;
			case ExpressionKind.ElementAccess:
			case ExpressionKind.Exports:
			case ExpressionKind.FieldAccess:
				return true;
			default:
				if (kind != ExpressionKind.List && kind != ExpressionKind.Record)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x0600969C RID: 38556 RVA: 0x001F38CC File Offset: 0x001F1ACC
		private static bool CanThrow(IBinaryExpression expression)
		{
			BinaryOperator2 @operator = expression.Operator;
			return @operator - BinaryOperator2.Equals > 1 || Compiler.CanThrow(expression.Left) || Compiler.CanThrow(expression.Right);
		}

		// Token: 0x0600969D RID: 38557 RVA: 0x001F3904 File Offset: 0x001F1B04
		private Import[] GetImports(Compiler.Import2[] imports)
		{
			Import[] array = new Import[imports.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Import((imports[i].Section != null) ? imports[i].Section.Name : null, imports[i].Name.Name);
			}
			return array;
		}

		// Token: 0x0600969E RID: 38558 RVA: 0x001F3970 File Offset: 0x001F1B70
		private static SourceLocation[][] GetLocations(IDocument document, Compiler.Import2[] imports)
		{
			SourceLocation[][] array = new SourceLocation[imports.Length][];
			for (int i = 0; i < imports.Length; i++)
			{
				Compiler.Import2 import = imports[i];
				TokenRange range;
				if (import.Section != null)
				{
					range = new TokenRange(import.Section.Range.Start, import.Name.Range.End);
				}
				else
				{
					range = import.Name.Range;
				}
				SourceLocation location = Compiler.GetLocation(document, range);
				if (location != null)
				{
					array[i] = new SourceLocation[] { location };
				}
				else
				{
					array[i] = new SourceLocation[0];
				}
			}
			return array;
		}

		// Token: 0x0600969F RID: 38559 RVA: 0x001F3A18 File Offset: 0x001F1C18
		private static SourceLocation GetLocation(IDocument document, IExpression expression)
		{
			return Compiler.GetLocation(document, expression.Range);
		}

		// Token: 0x060096A0 RID: 38560 RVA: 0x001F3A28 File Offset: 0x001F1C28
		private static SourceLocation GetLocation(IDocument document, TokenRange range)
		{
			if (document == null)
			{
				return null;
			}
			ITokens tokens = document.Tokens;
			if (tokens == null || range.Start == TokenReference.Null || range.End == TokenReference.Null)
			{
				return null;
			}
			TextRange range2 = tokens.GetRange(range.Start);
			TextRange range3 = tokens.GetRange(range.End);
			TextRange textRange = new TextRange(range2.Start, range3.End);
			return new SourceLocation(document.Host, textRange);
		}

		// Token: 0x04005004 RID: 20484
		private static readonly IFunctionTypeExpression zeroParamFunctionType = new FunctionTypeSyntaxNode(null, new IParameter[0], 0);

		// Token: 0x04005005 RID: 20485
		private static readonly Identifier environmentIdentifier = Identifier.New();

		// Token: 0x04005006 RID: 20486
		private static readonly Identifier importIdentifier = Identifier.New();

		// Token: 0x04005007 RID: 20487
		private static readonly Identifier entry = Identifier.New("Entry");

		// Token: 0x04005008 RID: 20488
		private static readonly Identifier shared = Identifier.New("Shared");

		// Token: 0x04005009 RID: 20489
		private static readonly Identifier section = Identifier.New("Section");

		// Token: 0x0400500A RID: 20490
		private static readonly Identifier binding = Identifier.New("Binding");

		// Token: 0x0400500B RID: 20491
		private static readonly Identifier recordIdentifier = Identifier.New();

		// Token: 0x0400500C RID: 20492
		private CompileOptions options;

		// Token: 0x0400500D RID: 20493
		private Environment<Instruction> argumentEnvironment;

		// Token: 0x0400500E RID: 20494
		private Compiler.MemberEnvironment memberEnvironment;

		// Token: 0x0400500F RID: 20495
		private IDocument document;

		// Token: 0x04005010 RID: 20496
		private int currentDepth;

		// Token: 0x02001724 RID: 5924
		private class MemberEnvironment
		{
			// Token: 0x060096A2 RID: 38562 RVA: 0x001F3B11 File Offset: 0x001F1D11
			public MemberEnvironment()
			{
				this.stack = new Stack<Dictionary<Identifier, Instruction>>();
				this.identifiers = new List<Identifier>();
			}

			// Token: 0x060096A3 RID: 38563 RVA: 0x001F3B2F File Offset: 0x001F1D2F
			public void EnterScope()
			{
				this.stack.Push(this.dictionary);
				this.dictionary = new Dictionary<Identifier, Instruction>();
			}

			// Token: 0x060096A4 RID: 38564 RVA: 0x001F3B50 File Offset: 0x001F1D50
			public Identifier[] ExitScope()
			{
				Identifier[] array = new Identifier[this.dictionary.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.identifiers[this.identifiers.Count - this.dictionary.Count + i];
				}
				for (int j = 0; j < array.Length; j++)
				{
					this.identifiers.RemoveAt(this.identifiers.Count - 1);
				}
				this.dictionary = this.stack.Pop();
				return array;
			}

			// Token: 0x060096A5 RID: 38565 RVA: 0x001F3BDC File Offset: 0x001F1DDC
			public Instruction GetValue(Identifier identifier)
			{
				Instruction instruction;
				if (!this.dictionary.TryGetValue(identifier, out instruction))
				{
					instruction = MemberAccessInstruction.Member(this.dictionary.Count);
					this.dictionary.Add(identifier, instruction);
					this.identifiers.Add(identifier);
				}
				return instruction;
			}

			// Token: 0x04005011 RID: 20497
			private Dictionary<Identifier, Instruction> dictionary;

			// Token: 0x04005012 RID: 20498
			private Stack<Dictionary<Identifier, Instruction>> stack;

			// Token: 0x04005013 RID: 20499
			private List<Identifier> identifiers;
		}

		// Token: 0x02001725 RID: 5925
		private class FieldBindingVisitor : LogicalAstVisitor<IExpression>
		{
			// Token: 0x060096A6 RID: 38566 RVA: 0x001F3C24 File Offset: 0x001F1E24
			public static IFunctionExpression Rewrite(RecordValue environment, Compiler.SectionEnvironment sectionEnvironment, IExpression expression, out Compiler.Import2[] imports)
			{
				Compiler.FieldBindingVisitor fieldBindingVisitor = new Compiler.FieldBindingVisitor(environment, sectionEnvironment);
				IExpression expression2 = fieldBindingVisitor.VisitExpression(expression);
				imports = new Compiler.Import2[fieldBindingVisitor.imports.Count];
				IExpression[] array = new IExpression[fieldBindingVisitor.imports.Count];
				foreach (KeyValuePair<Compiler.Import2, int> keyValuePair in fieldBindingVisitor.imports)
				{
					Compiler.Import2 key = keyValuePair.Key;
					imports[keyValuePair.Value] = keyValuePair.Key;
					array[keyValuePair.Value] = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library.Linker.Bind), new IExpression[]
					{
						Compiler.FieldBindingVisitor.environmentExpression,
						ConstantExpressionSyntaxNode.New(key.Section),
						new ConstantExpressionSyntaxNode(TextValue.New(key.Name))
					});
				}
				return new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
				{
					new ParameterSyntaxNode(Compiler.FieldBindingVisitor.environmentExpression.Name, null)
				}, 1), new InvocationExpressionSyntaxNode1(new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
				{
					new ParameterSyntaxNode(Compiler.FieldBindingVisitor.importsExpression.Name, null)
				}, 1), new FunctionExpressionSyntaxNode(Compiler.zeroParamFunctionType, expression2)), new ListExpressionSyntaxNode(array)));
			}

			// Token: 0x060096A7 RID: 38567 RVA: 0x001F3D70 File Offset: 0x001F1F70
			protected FieldBindingVisitor(RecordValue environment, Compiler.SectionEnvironment sectionEnvironment)
			{
				this.environment = environment;
				this.sectionEnvironment = sectionEnvironment;
				this.imports = new Dictionary<Compiler.Import2, int>();
				this.importExpressions = new List<IExpression>();
			}

			// Token: 0x060096A8 RID: 38568 RVA: 0x001F3D9C File Offset: 0x001F1F9C
			protected override IDocument VisitModuleDocument(ISectionDocument document)
			{
				return base.VisitModuleDocument(document);
			}

			// Token: 0x060096A9 RID: 38569 RVA: 0x001F3DA5 File Offset: 0x001F1FA5
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				return base.VisitFunction(function, new IExpression[function.FunctionType.Parameters.Count]);
			}

			// Token: 0x060096AA RID: 38570 RVA: 0x0000EE09 File Offset: 0x0000D009
			protected override ISection VisitModule(ISection module)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060096AB RID: 38571 RVA: 0x001F3DC4 File Offset: 0x001F1FC4
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				if (record.Identifier == null)
				{
					IIdentifierExpression identifierExpression = new InclusiveIdentifierExpressionSyntaxNode(Identifier.New());
					IExpression[] array = new IExpression[record.Members.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new RequiredFieldAccessExpressionSyntaxNode(identifierExpression, record.Members[i].Name);
					}
					return base.VisitRecord(new RecordExpressionSyntaxNode(identifierExpression.Name, record.Members, record.Range), identifierExpression, array);
				}
				return base.VisitRecord(record, null, new IExpression[record.Members.Count]);
			}

			// Token: 0x060096AC RID: 38572 RVA: 0x001CD562 File Offset: 0x001CB762
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, null);
			}

			// Token: 0x060096AD RID: 38573 RVA: 0x001F3E60 File Offset: 0x001F2060
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				IExpression expression;
				if (base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out expression))
				{
					if (expression == null)
					{
						if (!identifier.IsInclusive)
						{
							expression = new InclusiveIdentifierExpressionSyntaxNode(identifier.Name, identifier.Range);
						}
						else
						{
							expression = identifier;
						}
					}
					return expression;
				}
				if (this.sectionEnvironment.Contains(identifier.Name))
				{
					return this.GetImportExpression(new Compiler.Import2(this.sectionEnvironment.Name, identifier.Name));
				}
				Value value;
				if (this.environment.TryGetValue(identifier.Name, out value))
				{
					return ConstantExpressionSyntaxNode.New(value);
				}
				return this.GetImportExpression(new Compiler.Import2(identifier.Name));
			}

			// Token: 0x060096AE RID: 38574 RVA: 0x001F3F0D File Offset: 0x001F210D
			protected override IExpression VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
			{
				return this.GetImportExpression(new Compiler.Import2(sectionIdentifier.Section, sectionIdentifier.Name));
			}

			// Token: 0x060096AF RID: 38575 RVA: 0x001F3F26 File Offset: 0x001F2126
			protected override IExpression VisitExports(IExportsExpression expression)
			{
				return new RequiredFieldAccessExpressionSyntaxNode(Compiler.FieldBindingVisitor.environmentExpression, expression.Name);
			}

			// Token: 0x060096B0 RID: 38576 RVA: 0x001F3F38 File Offset: 0x001F2138
			private IExpression GetImportExpression(Compiler.Import2 import)
			{
				int count;
				if (!this.imports.TryGetValue(import, out count))
				{
					count = this.importExpressions.Count;
					this.imports.Add(import, count);
					this.importExpressions.Add(new RequiredElementAccessExpressionSyntaxNode(Compiler.FieldBindingVisitor.importsExpression, new ConstantExpressionSyntaxNode(NumberValue.New(count))));
				}
				return this.importExpressions[count];
			}

			// Token: 0x060096B1 RID: 38577 RVA: 0x001F3F9A File Offset: 0x001F219A
			protected override IExpression VisitLet(ILetExpression let)
			{
				return this.VisitExpression(this.TransformLet(let));
			}

			// Token: 0x060096B2 RID: 38578 RVA: 0x001F3FAC File Offset: 0x001F21AC
			private IExpression TransformLet(ILetExpression let)
			{
				if (let.Variables.Count == 0)
				{
					return let.Expression;
				}
				IIdentifierExpression identifierExpression = let.Expression as IIdentifierExpression;
				if (identifierExpression != null && Compiler.FieldBindingVisitor.IsLetIdentifier(let.Variables, identifierExpression.Name))
				{
					return new RequiredFieldAccessExpressionSyntaxNode(new RecordExpressionSyntaxNode(let.Variables), identifierExpression.Name);
				}
				VariableInitializer[] array = new VariableInitializer[let.Variables.Count + 1];
				for (int i = 0; i < let.Variables.Count; i++)
				{
					array[i] = new VariableInitializer(let.Variables[i].Name, let.Variables[i].Value);
				}
				Identifier identifier = Identifier.New();
				array[array.Length - 1] = new VariableInitializer(identifier, let.Expression);
				return new RequiredFieldAccessExpressionSyntaxNode(new RecordExpressionSyntaxNode(array), identifier);
			}

			// Token: 0x060096B3 RID: 38579 RVA: 0x001F4090 File Offset: 0x001F2290
			private static bool IsLetIdentifier(IList<VariableInitializer> initializers, Identifier identifier)
			{
				for (int i = 0; i < initializers.Count; i++)
				{
					if (initializers[i].Name == identifier)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04005014 RID: 20500
			private RecordValue environment;

			// Token: 0x04005015 RID: 20501
			private Compiler.SectionEnvironment sectionEnvironment;

			// Token: 0x04005016 RID: 20502
			private Dictionary<Compiler.Import2, int> imports;

			// Token: 0x04005017 RID: 20503
			private List<IExpression> importExpressions;

			// Token: 0x04005018 RID: 20504
			private static readonly IIdentifierExpression environmentExpression = new InclusiveIdentifierExpressionSyntaxNode(Compiler.environmentIdentifier);

			// Token: 0x04005019 RID: 20505
			private static readonly IIdentifierExpression importsExpression = new InclusiveIdentifierExpressionSyntaxNode(Compiler.importIdentifier);
		}

		// Token: 0x02001726 RID: 5926
		private class SectionEnvironment
		{
			// Token: 0x060096B5 RID: 38581 RVA: 0x001F40E8 File Offset: 0x001F22E8
			public SectionEnvironment()
			{
				this.name = null;
				this.names = null;
			}

			// Token: 0x060096B6 RID: 38582 RVA: 0x001F4100 File Offset: 0x001F2300
			public SectionEnvironment(ISection section)
			{
				this.name = section.SectionName;
				this.names = new HashSet<Identifier>();
				for (int i = 0; i < section.Members.Count; i++)
				{
					this.names.Add(section.Members[i].Name);
				}
			}

			// Token: 0x17002750 RID: 10064
			// (get) Token: 0x060096B7 RID: 38583 RVA: 0x001F415D File Offset: 0x001F235D
			public Identifier Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x060096B8 RID: 38584 RVA: 0x001F4165 File Offset: 0x001F2365
			public bool Contains(Identifier identifier)
			{
				return this.names != null && this.names.Contains(identifier);
			}

			// Token: 0x0400501A RID: 20506
			private Identifier name;

			// Token: 0x0400501B RID: 20507
			private HashSet<Identifier> names;
		}

		// Token: 0x02001727 RID: 5927
		private struct Import2 : IEquatable<Compiler.Import2>
		{
			// Token: 0x060096B9 RID: 38585 RVA: 0x001F4180 File Offset: 0x001F2380
			public Import2(Identifier name)
			{
				this = new Compiler.Import2(null, name);
			}

			// Token: 0x060096BA RID: 38586 RVA: 0x001F418A File Offset: 0x001F238A
			public Import2(Identifier section, Identifier name)
			{
				this.section = section;
				this.name = name;
			}

			// Token: 0x17002751 RID: 10065
			// (get) Token: 0x060096BB RID: 38587 RVA: 0x001F419A File Offset: 0x001F239A
			public Identifier Section
			{
				get
				{
					return this.section;
				}
			}

			// Token: 0x17002752 RID: 10066
			// (get) Token: 0x060096BC RID: 38588 RVA: 0x001F41A2 File Offset: 0x001F23A2
			public Identifier Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x060096BD RID: 38589 RVA: 0x001F41AA File Offset: 0x001F23AA
			public bool Equals(Compiler.Import2 other)
			{
				return this.section == other.section && this.name == other.name;
			}

			// Token: 0x060096BE RID: 38590 RVA: 0x001F41D2 File Offset: 0x001F23D2
			public override int GetHashCode()
			{
				return ((this.section != null) ? this.section.GetHashCode() : 0) + this.name.GetHashCode();
			}

			// Token: 0x0400501C RID: 20508
			private Identifier section;

			// Token: 0x0400501D RID: 20509
			private Identifier name;
		}

		// Token: 0x02001728 RID: 5928
		private class DummyAssembly : Assembly
		{
			// Token: 0x060096BF RID: 38591 RVA: 0x001F41FC File Offset: 0x001F23FC
			public DummyAssembly(FunctionValue function)
			{
				this.function = RuntimeFunctionValue.New(new ConstantInstruction(function), Compiler.DummyAssembly.functionType, null);
			}

			// Token: 0x17002753 RID: 10067
			// (get) Token: 0x060096C0 RID: 38592 RVA: 0x001F421B File Offset: 0x001F241B
			public override FunctionValue Function
			{
				get
				{
					return this.function;
				}
			}

			// Token: 0x17002754 RID: 10068
			// (get) Token: 0x060096C1 RID: 38593 RVA: 0x00019E61 File Offset: 0x00018061
			public override RecordValue Exports
			{
				get
				{
					return RecordValue.Empty;
				}
			}

			// Token: 0x0400501E RID: 20510
			private static readonly FunctionTypeValue functionType = FunctionTypeValue.New(TypeValue.Any, RecordValue.Empty, 0);

			// Token: 0x0400501F RID: 20511
			private FunctionValue function;
		}
	}
}
