using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B70 RID: 7024
	public class ExpressionToMVisitor : AstVisitor2
	{
		// Token: 0x0600AFF6 RID: 45046 RVA: 0x002405B0 File Offset: 0x0023E7B0
		public ExpressionToMVisitor(ITinyEngine engine, IRecordValue library = null)
		{
			this.engine = engine;
			this.library = library;
			this.symbolicNames = new Dictionary<IValue, string>();
			if (library != null)
			{
				for (int i = 0; i < library.Keys.Length; i++)
				{
					IValue value = library[i];
					if (value.IsFunction)
					{
						this.symbolicNames[value] = library.Keys[i];
					}
				}
			}
			this.Register(TypeHandle.Byte, "Byte.Type");
			this.Register(TypeHandle.Int8, "Int8.Type");
			this.Register(TypeHandle.Int16, "Int16.Type");
			this.Register(TypeHandle.Int32, "Int32.Type");
			this.Register(TypeHandle.Int64, "Int64.Type");
			this.Register(TypeHandle.Decimal, "Decimal.Type");
			this.Register(TypeHandle.Currency, "Currency.Type");
			this.Register(TypeHandle.Percentage, "Percentage.Type");
			this.Register(TypeHandle.Single, "Single.Type");
			this.Register(TypeHandle.Double, "Double.Type");
			this.Register(TypeHandle.Character, "Character.Type");
			this.Register(TypeHandle.Guid, "Guid.Type");
			this.Register(TypeHandle.Uri, "Uri.Type");
			this.Register(TypeHandle.Password, "Password.Type");
		}

		// Token: 0x0600AFF7 RID: 45047 RVA: 0x002406D0 File Offset: 0x0023E8D0
		private void Register(TypeHandle handle, string name)
		{
			ITypeValue typeValue = this.engine.Type(handle);
			if (typeValue != null)
			{
				this.symbolicNames.Add(typeValue, name);
			}
		}

		// Token: 0x0600AFF8 RID: 45048 RVA: 0x002406FC File Offset: 0x0023E8FC
		public string Visit(IExpression expression)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter indentedTextWriter = new IndentedTextWriter(stringWriter, "    "))
				{
					this.writer = indentedTextWriter;
					this.VisitExpression(expression);
					this.writer = null;
					indentedTextWriter.Flush();
					text = stringWriter.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600AFF9 RID: 45049 RVA: 0x00240778 File Offset: 0x0023E978
		public string Visit(ISection section)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter indentedTextWriter = new IndentedTextWriter(stringWriter, "    "))
				{
					this.writer = indentedTextWriter;
					this.VisitModule(section);
					this.writer = null;
					indentedTextWriter.Flush();
					text = stringWriter.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600AFFA RID: 45050 RVA: 0x002407F4 File Offset: 0x0023E9F4
		public string Visit(IDocument document)
		{
			if (document.Kind == DocumentKind.Expression)
			{
				IExpression expression = ((IExpressionDocument)document).Expression;
				return this.Visit(expression);
			}
			ISection section = ((ISectionDocument)document).Section;
			return this.Visit(section);
		}

		// Token: 0x0600AFFB RID: 45051 RVA: 0x00240834 File Offset: 0x0023EA34
		protected override ISection VisitModule(ISection module)
		{
			if (module.Attribute != null)
			{
				this.VisitExpression(module.Attribute);
				this.writer.WriteLine();
			}
			this.writer.Write("section ");
			if (module.SectionName != null)
			{
				this.WriteIdentifier(module.SectionName);
				this.writer.WriteLine(";");
			}
			for (int i = 0; i < module.Members.Count; i++)
			{
				this.writer.WriteLine();
				this.VisitModuleMember(module.Members[i]);
				this.writer.WriteLine(";");
			}
			return module;
		}

		// Token: 0x0600AFFC RID: 45052 RVA: 0x002408E0 File Offset: 0x0023EAE0
		protected override ISectionMember VisitModuleMember(ISectionMember moduleMember)
		{
			if (moduleMember.Attribute != null)
			{
				this.VisitExpression(moduleMember.Attribute);
				this.writer.WriteLine();
			}
			if (moduleMember.Export)
			{
				this.writer.Write("shared ");
			}
			this.WriteIdentifier(moduleMember.Name);
			this.writer.Write(" = ");
			IndentedTextWriter indentedTextWriter = this.writer;
			int num = indentedTextWriter.Indent;
			indentedTextWriter.Indent = num + 1;
			this.VisitExpression(moduleMember.Value);
			IndentedTextWriter indentedTextWriter2 = this.writer;
			num = indentedTextWriter2.Indent;
			indentedTextWriter2.Indent = num - 1;
			return moduleMember;
		}

		// Token: 0x0600AFFD RID: 45053 RVA: 0x0024097C File Offset: 0x0023EB7C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			this.VisitOperand(ExpressionToMVisitor.GetPrecedence(binary.Operator), binary.Left);
			this.writer.Write(" ");
			this.writer.Write(ExpressionToMVisitor.GetOperator(binary.Operator));
			this.writer.Write(" ");
			this.VisitOperand(ExpressionToMVisitor.GetPrecedence(binary.Operator) + 1, binary.Right);
			return binary;
		}

		// Token: 0x0600AFFE RID: 45054 RVA: 0x002409F0 File Offset: 0x0023EBF0
		protected override IExpression VisitConstant(IConstantExpression2 constant)
		{
			this.WriteValue(constant.Value);
			return constant;
		}

		// Token: 0x0600AFFF RID: 45055 RVA: 0x00240A00 File Offset: 0x0023EC00
		protected override IExpression VisitElementAccess(IElementAccessExpression elementAccess)
		{
			this.VisitPrimaryExpression(elementAccess.Collection);
			this.writer.Write("{");
			this.VisitExpression(elementAccess.Key);
			this.writer.Write("}");
			if (elementAccess.IsOptional)
			{
				this.writer.Write("?");
			}
			return elementAccess;
		}

		// Token: 0x0600B000 RID: 45056 RVA: 0x00240A60 File Offset: 0x0023EC60
		protected override IExpression VisitExports(IExportsExpression expression)
		{
			string name = expression.Name.Name;
			if (!(name == "Shared"))
			{
				if (!(name == "Sections"))
				{
					throw new InvalidOperationException(expression.Name);
				}
				this.writer.Write("#sections");
			}
			else
			{
				this.writer.Write("#shared");
			}
			return expression;
		}

		// Token: 0x0600B001 RID: 45057 RVA: 0x00240ACC File Offset: 0x0023ECCC
		protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			IIdentifierExpression identifierExpression = fieldAccess.Expression as IIdentifierExpression;
			if (identifierExpression == null || !identifierExpression.Name.Equals(Identifier.Underscore))
			{
				this.VisitPrimaryExpression(fieldAccess.Expression);
			}
			this.writer.Write("[");
			this.WriteFieldIdentifier(fieldAccess.MemberName);
			this.writer.Write("]");
			if (fieldAccess.IsOptional)
			{
				this.writer.Write("?");
			}
			return fieldAccess;
		}

		// Token: 0x0600B002 RID: 45058 RVA: 0x00240B4C File Offset: 0x0023ED4C
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			if (ExpressionToMVisitor.IsEachType(function.FunctionType))
			{
				this.writer.Write("each ");
			}
			else
			{
				this.WriteSignature(function.FunctionType, false);
				this.writer.Write(" => ");
			}
			this.VisitExpression(function.Expression);
			return function;
		}

		// Token: 0x0600B003 RID: 45059 RVA: 0x00240BA3 File Offset: 0x0023EDA3
		protected override IExpression VisitFunctionType(IFunctionTypeExpression functionType)
		{
			this.writer.Write("function ");
			this.WriteSignature(functionType, true);
			return functionType;
		}

		// Token: 0x0600B004 RID: 45060 RVA: 0x00240BBE File Offset: 0x0023EDBE
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			if (identifier.IsInclusive)
			{
				this.writer.Write("@");
			}
			this.WriteIdentifier(identifier.Name);
			return identifier;
		}

		// Token: 0x0600B005 RID: 45061 RVA: 0x00240BE8 File Offset: 0x0023EDE8
		protected override IExpression VisitIf(IIfExpression @if)
		{
			this.writer.Write("if ");
			this.VisitExpression(@if.Condition);
			this.writer.Write(" then ");
			this.VisitExpression(@if.TrueCase);
			this.writer.Write(" else ");
			this.VisitExpression(@if.FalseCase);
			return @if;
		}

		// Token: 0x0600B006 RID: 45062 RVA: 0x001AD352 File Offset: 0x001AB552
		protected override IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
		{
			return this.VisitIdentifier(implicitIdentifier);
		}

		// Token: 0x0600B007 RID: 45063 RVA: 0x00240C4D File Offset: 0x0023EE4D
		protected override VariableInitializer VisitInitializer(VariableInitializer member)
		{
			this.WriteIdentifier(member.Name);
			this.writer.Write(" = ");
			this.VisitExpression(member.Value);
			return member;
		}

		// Token: 0x0600B008 RID: 45064 RVA: 0x00240C7C File Offset: 0x0023EE7C
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			this.VisitPrimaryExpression(invocation.Function);
			this.writer.Write("(");
			for (int i = 0; i < invocation.Arguments.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.VisitExpression(invocation.Arguments[i]);
			}
			this.writer.Write(")");
			return invocation;
		}

		// Token: 0x0600B009 RID: 45065 RVA: 0x00240CF4 File Offset: 0x0023EEF4
		protected override IExpression VisitLet(ILetExpression let)
		{
			if (this.IsTry(let))
			{
				this.writer.Write("try ");
				this.VisitExpression(((IInvocationExpression)((ITryCatchExpression)let.Expression).Try).Arguments[0]);
				return let;
			}
			this.writer.WriteLine("let");
			IndentedTextWriter indentedTextWriter = this.writer;
			int num = indentedTextWriter.Indent;
			indentedTextWriter.Indent = num + 1;
			for (int i = 0; i < let.Variables.Count; i++)
			{
				if (i != 0)
				{
					this.writer.WriteLine(",");
				}
				this.VisitInitializer(let.Variables[i]);
			}
			IndentedTextWriter indentedTextWriter2 = this.writer;
			num = indentedTextWriter2.Indent;
			indentedTextWriter2.Indent = num - 1;
			if (let.Variables.Count > 0)
			{
				this.writer.WriteLine();
			}
			this.writer.WriteLine("in");
			IndentedTextWriter indentedTextWriter3 = this.writer;
			num = indentedTextWriter3.Indent;
			indentedTextWriter3.Indent = num + 1;
			this.VisitExpression(let.Expression);
			IndentedTextWriter indentedTextWriter4 = this.writer;
			num = indentedTextWriter4.Indent;
			indentedTextWriter4.Indent = num - 1;
			return let;
		}

		// Token: 0x0600B00A RID: 45066 RVA: 0x00240E1C File Offset: 0x0023F01C
		protected override IExpression VisitList(IListExpression list)
		{
			this.writer.Write("{");
			for (int i = 0; i < list.Members.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.VisitExpression(list.Members[i]);
			}
			this.writer.Write("}");
			return list;
		}

		// Token: 0x0600B00B RID: 45067 RVA: 0x00240E86 File Offset: 0x0023F086
		protected override IExpression VisitListType(IListTypeExpression listType)
		{
			this.writer.Write("{");
			this.VisitExpression(listType.ItemType);
			this.writer.Write("}");
			return listType;
		}

		// Token: 0x0600B00C RID: 45068 RVA: 0x00240EB8 File Offset: 0x0023F0B8
		protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			IIdentifierExpression identifierExpression = multiFieldRecordProjection.Expression as IIdentifierExpression;
			if (identifierExpression == null || !identifierExpression.Name.Equals(Identifier.Underscore))
			{
				this.VisitPrimaryExpression(multiFieldRecordProjection.Expression);
			}
			this.writer.Write("[");
			for (int i = 0; i < multiFieldRecordProjection.MemberNames.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(",");
				}
				this.writer.Write("[");
				this.WriteIdentifier(multiFieldRecordProjection.MemberNames[i]);
				this.writer.Write("]");
			}
			this.writer.Write("]");
			if (multiFieldRecordProjection.IsOptional)
			{
				this.writer.Write("?");
			}
			return multiFieldRecordProjection;
		}

		// Token: 0x0600B00D RID: 45069 RVA: 0x00240F88 File Offset: 0x0023F188
		protected override IFieldType VisitFieldType(IFieldType fieldType)
		{
			if (fieldType.Optional)
			{
				this.writer.Write("optional ");
			}
			this.WriteFieldIdentifier(fieldType.Name);
			if (fieldType.Type != null)
			{
				this.writer.Write(" = ");
				this.VisitExpression(fieldType.Type);
			}
			return fieldType;
		}

		// Token: 0x0600B00E RID: 45070 RVA: 0x00240FDF File Offset: 0x0023F1DF
		protected override IExpression VisitNotImplemented(INotImplementedExpression notImplemented)
		{
			this.writer.Write("...");
			return notImplemented;
		}

		// Token: 0x0600B00F RID: 45071 RVA: 0x00240FF2 File Offset: 0x0023F1F2
		protected override IExpression VisitNullableType(INullableTypeExpression nullableType)
		{
			this.writer.Write("nullable ");
			this.VisitExpression(nullableType.ItemType);
			return nullableType;
		}

		// Token: 0x0600B010 RID: 45072 RVA: 0x00241012 File Offset: 0x0023F212
		protected override IParameter VisitParameter(IParameter parameter)
		{
			this.WriteParameter(parameter, true);
			return parameter;
		}

		// Token: 0x0600B011 RID: 45073 RVA: 0x00241020 File Offset: 0x0023F220
		protected override IExpression VisitParentheses(IParenthesesExpression parentheses)
		{
			this.writer.Write("(");
			using (ExpressionToMVisitor.TypeScope.ExitScope(this))
			{
				this.VisitExpression(parentheses.Expression);
			}
			this.writer.Write(")");
			return parentheses;
		}

		// Token: 0x0600B012 RID: 45074 RVA: 0x00241084 File Offset: 0x0023F284
		protected override IExpression VisitRangeList(IRangeListExpression rangeList)
		{
			this.writer.Write("{");
			for (int i = 0; i < rangeList.Members.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.VisitRangeExpression(rangeList.Members[i]);
			}
			this.writer.Write("}");
			return rangeList;
		}

		// Token: 0x0600B013 RID: 45075 RVA: 0x002410EE File Offset: 0x0023F2EE
		protected override IRangeExpression VisitRangeExpression(IRangeExpression range)
		{
			this.VisitExpression(range.Lower);
			if (range.Lower != range.Upper)
			{
				this.writer.Write("..");
				this.VisitExpression(range.Upper);
			}
			return range;
		}

		// Token: 0x0600B014 RID: 45076 RVA: 0x0024112C File Offset: 0x0023F32C
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			this.writer.Write("[");
			for (int i = 0; i < record.Members.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.VisitInitializer(record.Members[i]);
			}
			this.writer.Write("]");
			return record;
		}

		// Token: 0x0600B015 RID: 45077 RVA: 0x00241198 File Offset: 0x0023F398
		protected override IExpression VisitRecordType(IRecordTypeExpression recordType)
		{
			this.writer.Write("[");
			for (int i = 0; i < recordType.Fields.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.VisitFieldType(recordType.Fields[i]);
			}
			if (recordType.Wildcard)
			{
				if (recordType.Fields.Count > 0)
				{
					this.writer.Write(", ");
				}
				this.writer.Write("...");
			}
			this.writer.Write("]");
			return recordType;
		}

		// Token: 0x0600B016 RID: 45078 RVA: 0x00241238 File Offset: 0x0023F438
		protected override IExpression VisitSectionIdentifier(ISectionIdentifierExpression identifier)
		{
			this.WriteSectionIdentifier(identifier);
			return identifier;
		}

		// Token: 0x0600B017 RID: 45079 RVA: 0x00241242 File Offset: 0x0023F442
		protected override IFunctionTypeExpression VisitSignature(IFunctionTypeExpression signature)
		{
			this.WriteSignature(signature, true);
			return signature;
		}

		// Token: 0x0600B018 RID: 45080 RVA: 0x0024124D File Offset: 0x0023F44D
		protected override IExpression VisitTableType(ITableTypeExpression tableType)
		{
			this.writer.Write("table ");
			this.VisitExpression(tableType.RowType);
			return tableType;
		}

		// Token: 0x0600B019 RID: 45081 RVA: 0x0024126D File Offset: 0x0023F46D
		protected override IExpression VisitThrow(IThrowExpression @throw)
		{
			this.writer.Write("error ");
			this.VisitExpression(@throw.Expression);
			return @throw;
		}

		// Token: 0x0600B01A RID: 45082 RVA: 0x0024128D File Offset: 0x0023F48D
		protected override IExpression VisitTryCatch(ITryCatchExpression tryCatch)
		{
			this.writer.Write("try ");
			this.VisitExpression(tryCatch.Try);
			this.VisitTryCatchExceptionCase(tryCatch.ExceptionCase);
			return tryCatch;
		}

		// Token: 0x0600B01B RID: 45083 RVA: 0x002412BA File Offset: 0x0023F4BA
		protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			this.writer.Write(" otherwise ");
			this.VisitExpression(tryCatchExceptionCase.Expression);
			return tryCatchExceptionCase;
		}

		// Token: 0x0600B01C RID: 45084 RVA: 0x002412DC File Offset: 0x0023F4DC
		protected override IExpression VisitType(ITypeExpression type)
		{
			IExpression expression;
			using (ExpressionToMVisitor.TypeScope.ExplicitScope(this))
			{
				expression = this.VisitExpression(type.Expression);
			}
			return expression;
		}

		// Token: 0x0600B01D RID: 45085 RVA: 0x00241320 File Offset: 0x0023F520
		protected override IExpression VisitUnary(IUnaryExpression unary)
		{
			this.writer.Write(ExpressionToMVisitor.GetOperator(unary.Operator));
			this.writer.Write(" ");
			this.VisitPrimaryExpression(unary.Expression);
			return unary;
		}

		// Token: 0x0600B01E RID: 45086 RVA: 0x00241355 File Offset: 0x0023F555
		protected override IExpression VisitVerbatim(IVerbatimExpression verbatim)
		{
			this.writer.Write("#!");
			this.VisitExpression(verbatim.Text);
			return verbatim;
		}

		// Token: 0x0600B01F RID: 45087 RVA: 0x00241378 File Offset: 0x0023F578
		protected virtual void WriteTable(ITableValue table)
		{
			IRecordValue fields = table.Type.AsTableType.ItemType.Fields;
			IndentedTextWriter indentedTextWriter = this.writer;
			int num = indentedTextWriter.Indent;
			indentedTextWriter.Indent = num + 1;
			this.writer.Write("#table(");
			using (ExpressionToMVisitor.TypeScope.ExplicitScope(this))
			{
				this.writer.WriteLine("table [");
				for (int i = 0; i < fields.Keys.Length; i++)
				{
					if (i != 0)
					{
						this.writer.WriteLine(", ");
					}
					this.WriteFieldIdentifier(fields.Keys[i]);
					this.writer.Write(" = ");
					this.WriteValue(fields[i].AsRecord["Type"]);
				}
			}
			IndentedTextWriter indentedTextWriter2 = this.writer;
			num = indentedTextWriter2.Indent;
			indentedTextWriter2.Indent = num - 1;
			this.writer.WriteLine();
			this.writer.WriteLine("], {");
			IndentedTextWriter indentedTextWriter3 = this.writer;
			num = indentedTextWriter3.Indent;
			indentedTextWriter3.Indent = num + 1;
			bool flag = true;
			foreach (IValueReference2 valueReference in table)
			{
				if (!flag)
				{
					this.writer.WriteLine(", ");
				}
				flag = false;
				IRecordValue asRecord = valueReference.Value.AsRecord;
				this.writer.Write("{");
				for (int j = 0; j < asRecord.Keys.Length; j++)
				{
					if (j != 0)
					{
						this.writer.Write(", ");
					}
					this.WriteValue(asRecord[j]);
				}
				this.writer.Write("}");
			}
			IndentedTextWriter indentedTextWriter4 = this.writer;
			num = indentedTextWriter4.Indent;
			indentedTextWriter4.Indent = num - 1;
			this.writer.WriteLine();
			this.writer.Write("})");
		}

		// Token: 0x0600B020 RID: 45088 RVA: 0x0024159C File Offset: 0x0023F79C
		private void WriteParameter(IParameter parameter, bool writeAnyTypes)
		{
			this.WriteIdentifier(parameter.Identifier);
			if (parameter.Type != null || writeAnyTypes)
			{
				this.writer.Write(" as ");
				if (parameter.Type != null)
				{
					this.VisitExpression(parameter.Type);
					return;
				}
				this.writer.Write("any");
			}
		}

		// Token: 0x0600B021 RID: 45089 RVA: 0x002415F8 File Offset: 0x0023F7F8
		private void WriteSectionIdentifier(ISectionIdentifierExpression identifier)
		{
			this.WriteIdentifier(identifier.Section);
			this.writer.Write("!");
			this.WriteIdentifier(identifier.Name);
		}

		// Token: 0x0600B022 RID: 45090 RVA: 0x00241624 File Offset: 0x0023F824
		private void WriteSignature(IFunctionTypeExpression signature, bool writeAnyTypes)
		{
			using (ExpressionToMVisitor.TypeScope.ImplicitScope(this))
			{
				this.writer.Write("(");
				for (int i = 0; i < signature.Parameters.Count; i++)
				{
					if (i != 0)
					{
						this.writer.Write(", ");
					}
					if (i >= signature.Min)
					{
						this.writer.Write("optional ");
					}
					this.WriteParameter(signature.Parameters[i], writeAnyTypes);
				}
				this.writer.Write(")");
				if (signature.ReturnType != null || writeAnyTypes)
				{
					this.writer.Write(" as ");
					if (signature.ReturnType != null)
					{
						this.VisitExpression(signature.ReturnType);
					}
					else
					{
						this.writer.Write("any");
					}
				}
			}
		}

		// Token: 0x0600B023 RID: 45091 RVA: 0x00241714 File Offset: 0x0023F914
		private static bool IsEachType(IFunctionTypeExpression functionType)
		{
			return functionType.Parameters.Count == 1 && functionType.Min == 1 && functionType.Parameters[0].Identifier.Equals(Identifier.Underscore) && functionType.Parameters[0].Type == null && functionType.ReturnType == null;
		}

		// Token: 0x0600B024 RID: 45092 RVA: 0x00241774 File Offset: 0x0023F974
		private void VisitOperand(int contextPrecedence, IExpression expression)
		{
			if (expression.Kind != ExpressionKind.Binary)
			{
				this.VisitPrimaryExpression(expression);
				return;
			}
			if (contextPrecedence > ExpressionToMVisitor.GetPrecedence(((IBinaryExpression)expression).Operator))
			{
				this.writer.Write("(");
				this.VisitExpression(expression);
				this.writer.Write(")");
				return;
			}
			this.VisitExpression(expression);
		}

		// Token: 0x0600B025 RID: 45093 RVA: 0x002417D8 File Offset: 0x0023F9D8
		private static int GetPrecedence(BinaryOperator2 op)
		{
			switch (op)
			{
			case BinaryOperator2.Add:
			case BinaryOperator2.Subtract:
			case BinaryOperator2.Concatenate:
				return 7;
			case BinaryOperator2.Multiply:
			case BinaryOperator2.Divide:
				return 8;
			case BinaryOperator2.GreaterThan:
			case BinaryOperator2.LessThan:
			case BinaryOperator2.GreaterThanOrEquals:
			case BinaryOperator2.LessThanOrEquals:
				return 6;
			case BinaryOperator2.Equals:
			case BinaryOperator2.NotEquals:
				return 5;
			case BinaryOperator2.And:
				return 2;
			case BinaryOperator2.Or:
				return 1;
			case BinaryOperator2.MetadataAdd:
				return 9;
			case BinaryOperator2.As:
				return 4;
			case BinaryOperator2.Is:
				return 3;
			case BinaryOperator2.Coalesce:
				return 0;
			}
			throw new InvalidOperationException(op.ToString());
		}

		// Token: 0x0600B026 RID: 45094 RVA: 0x0024185C File Offset: 0x0023FA5C
		private static string GetOperator(UnaryOperator2 op)
		{
			switch (op)
			{
			case UnaryOperator2.Not:
				return "not";
			case UnaryOperator2.Negative:
				return "-";
			case UnaryOperator2.Positive:
				return "+";
			default:
				throw new InvalidOperationException(op.ToString());
			}
		}

		// Token: 0x0600B027 RID: 45095 RVA: 0x00241898 File Offset: 0x0023FA98
		private static string GetOperator(BinaryOperator2 op)
		{
			switch (op)
			{
			case BinaryOperator2.Add:
				return "+";
			case BinaryOperator2.Subtract:
				return "-";
			case BinaryOperator2.Multiply:
				return "*";
			case BinaryOperator2.Divide:
				return "/";
			case BinaryOperator2.GreaterThan:
				return ">";
			case BinaryOperator2.LessThan:
				return "<";
			case BinaryOperator2.GreaterThanOrEquals:
				return ">=";
			case BinaryOperator2.LessThanOrEquals:
				return "<=";
			case BinaryOperator2.Equals:
				return "=";
			case BinaryOperator2.NotEquals:
				return "<>";
			case BinaryOperator2.And:
				return "and";
			case BinaryOperator2.Or:
				return "or";
			case BinaryOperator2.MetadataAdd:
				return "meta";
			case BinaryOperator2.Concatenate:
				return "&";
			case BinaryOperator2.As:
				return "as";
			case BinaryOperator2.Is:
				return "is";
			case BinaryOperator2.Coalesce:
				return "??";
			}
			throw new InvalidOperationException(op.ToString());
		}

		// Token: 0x0600B028 RID: 45096 RVA: 0x0024196D File Offset: 0x0023FB6D
		private void VisitPrimaryExpression(IExpression expression)
		{
			if (ExpressionToMVisitor.IsPrimaryExpression(expression))
			{
				this.VisitExpression(expression);
				return;
			}
			this.writer.Write("(");
			this.VisitExpression(expression);
			this.writer.Write(")");
		}

		// Token: 0x0600B029 RID: 45097 RVA: 0x002419A8 File Offset: 0x0023FBA8
		private static bool IsPrimaryExpression(IExpression expression)
		{
			switch (expression.Kind)
			{
			case ExpressionKind.Constant:
			case ExpressionKind.ElementAccess:
			case ExpressionKind.FieldAccess:
			case ExpressionKind.Identifier:
			case ExpressionKind.Invocation:
			case ExpressionKind.List:
			case ExpressionKind.NotImplemented:
			case ExpressionKind.Parentheses:
			case ExpressionKind.Record:
			case ExpressionKind.SectionIdentifier:
			case ExpressionKind.ImplicitIdentifier:
				return true;
			}
			return false;
		}

		// Token: 0x0600B02A RID: 45098 RVA: 0x00241A20 File Offset: 0x0023FC20
		private void WriteValue(IValue value)
		{
			base.IncrementDepth();
			IValue value2;
			IValue value3;
			if (this.library != null && value.TryGetMetaField("Documentation.Name", out value2) && value2.IsText && this.library.TryGetValue(value2.AsString, out value3) && value3.Equals(value))
			{
				this.WriteIdentifier(value2.AsString);
				return;
			}
			if (value.IsRecord)
			{
				this.WriteRecord(value.AsRecord);
			}
			else if (value.IsList)
			{
				this.WriteList(value.AsList);
			}
			else if (value.IsTable)
			{
				this.WriteTable(value.AsTable);
			}
			else
			{
				if (value.IsType)
				{
					string text;
					if (this.symbolicNames.TryGetValue(value.AsType.NonNullable, out text))
					{
						if (value.AsType.IsNullable)
						{
							using (ExpressionToMVisitor.TypeScope.ExplicitScope(this))
							{
								this.writer.Write("nullable ");
							}
						}
						this.WriteIdentifier(text);
						goto IL_017D;
					}
					using (ExpressionToMVisitor.TypeScope.ExplicitScope(this))
					{
						this.writer.Write(value.ToSource());
						goto IL_017D;
					}
				}
				if (value.IsBinary)
				{
					this.WriteBinary(value.AsBinary);
				}
				else
				{
					string text;
					if (value.IsFunction && this.symbolicNames.TryGetValue(value, out text))
					{
						this.WriteIdentifier(text);
						return;
					}
					this.writer.Write(value.ToSource());
				}
			}
			IL_017D:
			IRecordValue metaValue = value.MetaValue;
			if (metaValue.Keys.Length > 0)
			{
				this.writer.Write(" meta ");
				this.WriteRecord(metaValue);
			}
			base.DecrementDepth();
		}

		// Token: 0x0600B02B RID: 45099 RVA: 0x00241BF8 File Offset: 0x0023FDF8
		private void WriteRecord(IRecordValue record)
		{
			this.writer.Write("[");
			for (int i = 0; i < record.Keys.Length; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.WriteFieldIdentifier(record.Keys[i]);
				this.writer.Write(" = ");
				this.WriteValue(record[i]);
			}
			this.writer.Write("]");
		}

		// Token: 0x0600B02C RID: 45100 RVA: 0x00241C84 File Offset: 0x0023FE84
		private void WriteList(IListValue list)
		{
			this.writer.Write("{");
			for (int i = 0; i < list.Count; i++)
			{
				if (i != 0)
				{
					this.writer.Write(", ");
				}
				this.WriteValue(list[i]);
			}
			this.writer.Write("}");
		}

		// Token: 0x0600B02D RID: 45101 RVA: 0x00241CE4 File Offset: 0x0023FEE4
		private void WriteBinary(IBinaryValue binary)
		{
			this.writer.Write("Binary.FromText(");
			using (Stream stream = binary.Open())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					this.writer.Write(this.engine.EscapeString(Convert.ToBase64String(memoryStream.ToArray())));
				}
			}
			this.writer.Write(")");
		}

		// Token: 0x0600B02E RID: 45102 RVA: 0x00241D78 File Offset: 0x0023FF78
		private void WriteIdentifier(Identifier identifier)
		{
			this.writer.Write(this.engine.EscapeIdentifier(identifier));
		}

		// Token: 0x0600B02F RID: 45103 RVA: 0x00241D96 File Offset: 0x0023FF96
		private void WriteFieldIdentifier(Identifier identifier)
		{
			this.writer.Write(this.engine.EscapeFieldIdentifier(identifier));
		}

		// Token: 0x0600B030 RID: 45104 RVA: 0x00241DB4 File Offset: 0x0023FFB4
		private bool IsTry(ILetExpression let)
		{
			ITryCatchExpression tryCatchExpression;
			IRecordExpression recordExpression;
			Identifier identifier;
			return this.TryGetTryCatch(let.Expression, out tryCatchExpression) && this.TryGetRecord(tryCatchExpression.ExceptionCase.Expression, out recordExpression) && recordExpression.Count == 2 && this.TryGetIdentifier(recordExpression.Members[1].Value, out identifier) && identifier == tryCatchExpression.ExceptionCase.Variable;
		}

		// Token: 0x0600B031 RID: 45105 RVA: 0x00241E2D File Offset: 0x0024002D
		private bool TryGetTryCatch(IExpression expression, out ITryCatchExpression tryCatchExpression)
		{
			tryCatchExpression = expression as ITryCatchExpression;
			return tryCatchExpression != null;
		}

		// Token: 0x0600B032 RID: 45106 RVA: 0x00241E3C File Offset: 0x0024003C
		private bool TryGetRecord(IExpression expression, out IRecordExpression recordExpression)
		{
			recordExpression = expression as IRecordExpression;
			return recordExpression != null;
		}

		// Token: 0x0600B033 RID: 45107 RVA: 0x00241E4C File Offset: 0x0024004C
		private bool TryGetIdentifier(IExpression expression, out Identifier identifier)
		{
			IIdentifierExpression identifierExpression = expression as IIdentifierExpression;
			if (identifierExpression != null)
			{
				identifier = identifierExpression.Name;
				return true;
			}
			identifier = null;
			return false;
		}

		// Token: 0x04005A8F RID: 23183
		private readonly ITinyEngine engine;

		// Token: 0x04005A90 RID: 23184
		private readonly IRecordValue library;

		// Token: 0x04005A91 RID: 23185
		private readonly Dictionary<IValue, string> symbolicNames;

		// Token: 0x04005A92 RID: 23186
		private bool writingType;

		// Token: 0x04005A93 RID: 23187
		protected IndentedTextWriter writer;

		// Token: 0x02001B71 RID: 7025
		private struct TypeScope : IDisposable
		{
			// Token: 0x0600B034 RID: 45108 RVA: 0x00241E74 File Offset: 0x00240074
			private TypeScope(ExpressionToMVisitor visitor, bool emitTypeKeyword = true, bool exitScope = false)
			{
				this.visitor = visitor;
				this.oldWritingType = this.visitor.writingType;
				if (emitTypeKeyword && !this.visitor.writingType)
				{
					this.visitor.writer.Write("type ");
				}
				this.visitor.writingType = !exitScope;
			}

			// Token: 0x0600B035 RID: 45109 RVA: 0x00241ECD File Offset: 0x002400CD
			public static ExpressionToMVisitor.TypeScope ExplicitScope(ExpressionToMVisitor visitor)
			{
				return new ExpressionToMVisitor.TypeScope(visitor, true, false);
			}

			// Token: 0x0600B036 RID: 45110 RVA: 0x00241ED7 File Offset: 0x002400D7
			public static ExpressionToMVisitor.TypeScope ImplicitScope(ExpressionToMVisitor visitor)
			{
				return new ExpressionToMVisitor.TypeScope(visitor, false, false);
			}

			// Token: 0x0600B037 RID: 45111 RVA: 0x00241EE1 File Offset: 0x002400E1
			public static ExpressionToMVisitor.TypeScope ExitScope(ExpressionToMVisitor visitor)
			{
				return new ExpressionToMVisitor.TypeScope(visitor, false, true);
			}

			// Token: 0x0600B038 RID: 45112 RVA: 0x00241EEB File Offset: 0x002400EB
			public void Dispose()
			{
				this.visitor.writingType = this.oldWritingType;
			}

			// Token: 0x04005A94 RID: 23188
			private readonly ExpressionToMVisitor visitor;

			// Token: 0x04005A95 RID: 23189
			private readonly bool oldWritingType;
		}
	}
}
