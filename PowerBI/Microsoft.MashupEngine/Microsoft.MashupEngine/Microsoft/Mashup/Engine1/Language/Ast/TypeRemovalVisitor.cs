using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018B9 RID: 6329
	internal class TypeRemovalVisitor : AstVisitor
	{
		// Token: 0x0600A131 RID: 41265 RVA: 0x00216EC8 File Offset: 0x002150C8
		protected override IExpression VisitListType(IListTypeExpression listType)
		{
			return new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.Type.ForList), new ListExpressionSyntaxNode(new IExpression[] { this.VisitExpression(listType.ItemType) }), listType.Range);
		}

		// Token: 0x0600A132 RID: 41266 RVA: 0x00216EF9 File Offset: 0x002150F9
		protected override IExpression VisitTableType(ITableTypeExpression tableType)
		{
			return new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableModule.Type.ForTable), this.VisitExpression(tableType.RowType), tableType.Range);
		}

		// Token: 0x0600A133 RID: 41267 RVA: 0x00216F1C File Offset: 0x0021511C
		protected override IExpression VisitNullableType(INullableTypeExpression nullableType)
		{
			return new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.Type.ForNullable), new ListExpressionSyntaxNode(new IExpression[] { this.VisitExpression(nullableType.ItemType) }), nullableType.Range);
		}

		// Token: 0x0600A134 RID: 41268 RVA: 0x00216F50 File Offset: 0x00215150
		private IExpression VisitFieldTypes(IList<IFieldType> fields)
		{
			VariableInitializer[] array = new VariableInitializer[fields.Count];
			for (int i = 0; i < fields.Count; i++)
			{
				array[i] = new VariableInitializer(fields[i].Name, new RecordExpressionSyntaxNode(new VariableInitializer[]
				{
					new VariableInitializer(Identifier.New("Type"), this.VisitOptionalExpression(fields[i].Type, TypeValue.Any)),
					new VariableInitializer(Identifier.New("Optional"), ConstantExpressionSyntaxNode.New(fields[i].Optional))
				}));
			}
			return new RecordExpressionSyntaxNode(array);
		}

		// Token: 0x0600A135 RID: 41269 RVA: 0x00216FFC File Offset: 0x002151FC
		private IExpression VisitParameters(IList<IParameter> parameters)
		{
			VariableInitializer[] array = new VariableInitializer[parameters.Count];
			for (int i = 0; i < parameters.Count; i++)
			{
				array[i] = new VariableInitializer(parameters[i].Identifier, this.VisitOptionalExpression(parameters[i].Type, TypeValue.Any));
			}
			return new RecordExpressionSyntaxNode(array);
		}

		// Token: 0x0600A136 RID: 41270 RVA: 0x0021705B File Offset: 0x0021525B
		protected override IExpression VisitRecordType(IRecordTypeExpression recordType)
		{
			return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library.Type.ForRecord), this.VisitFieldTypes(recordType.Fields), ConstantExpressionSyntaxNode.New(recordType.Wildcard), recordType.Range);
		}

		// Token: 0x0600A137 RID: 41271 RVA: 0x00217089 File Offset: 0x00215289
		private IExpression VisitOptionalExpression(IExpression expression, Value value)
		{
			if (expression == null)
			{
				return new ConstantExpressionSyntaxNode(value);
			}
			return this.VisitExpression(expression);
		}

		// Token: 0x0600A138 RID: 41272 RVA: 0x0021709C File Offset: 0x0021529C
		protected override IFunctionTypeExpression VisitSignature(IFunctionTypeExpression signature)
		{
			return (IFunctionTypeExpression)base.VisitFunctionType(signature);
		}

		// Token: 0x0600A139 RID: 41273 RVA: 0x002170AC File Offset: 0x002152AC
		protected override IExpression VisitFunctionType(IFunctionTypeExpression functionType)
		{
			return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library.Type.FunctionFrom), new IExpression[]
			{
				new RecordExpressionSyntaxNode(new VariableInitializer[]
				{
					new VariableInitializer(Identifier.New("ReturnType"), this.VisitOptionalExpression(functionType.ReturnType, TypeValue.Any)),
					new VariableInitializer(Identifier.New("Parameters"), this.VisitParameters(functionType.Parameters))
				}),
				new ConstantExpressionSyntaxNode(NumberValue.New(functionType.Min))
			}, functionType.Range);
		}

		// Token: 0x0600A13A RID: 41274 RVA: 0x0021713E File Offset: 0x0021533E
		private static IExpression CreateAsExpression(IExpression value, IExpression type, TokenRange range)
		{
			return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library._Value.As), value, type, range);
		}

		// Token: 0x0600A13B RID: 41275 RVA: 0x00217154 File Offset: 0x00215354
		private static bool IsTyped(IFunctionTypeExpression function)
		{
			if (function.ReturnType != null)
			{
				return true;
			}
			for (int i = 0; i < function.Parameters.Count; i++)
			{
				if (function.Parameters[i].Type != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A13C RID: 41276 RVA: 0x00217198 File Offset: 0x00215398
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			IFunctionTypeExpression functionType = function.FunctionType;
			if (!TypeRemovalVisitor.IsTyped(functionType))
			{
				return base.VisitFunction(function);
			}
			IParameter[] array = new IParameter[functionType.Parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ParameterSyntaxNode(functionType.Parameters[i].Identifier, null);
			}
			IFunctionTypeExpression functionTypeExpression = new FunctionTypeSyntaxNode(null, array, functionType.Min);
			IExpression[] array2 = new IExpression[array.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				IParameter parameter = functionType.Parameters[j];
				IIdentifierExpression identifierExpression = new InclusiveIdentifierExpressionSyntaxNode(parameter.Identifier);
				if (parameter.Type != null)
				{
					IExpression expression2;
					if (j >= functionType.Min)
					{
						IExpression expression = new NullableTypeSyntaxNode(parameter.Type, parameter.Type.Range);
						expression2 = expression;
					}
					else
					{
						expression2 = parameter.Type;
					}
					IExpression expression3 = expression2;
					array2[j] = this.VisitExpression(new AsBinaryExpressionSyntaxNode(identifierExpression, this.VisitExpression(expression3)));
				}
				else
				{
					array2[j] = identifierExpression;
				}
			}
			IExpression expression4 = new InvocationExpressionSyntaxNodeN(new FunctionExpressionSyntaxNode(functionTypeExpression, function.Expression), array2);
			if (functionType.ReturnType != null)
			{
				expression4 = this.VisitExpression(new AsBinaryExpressionSyntaxNode(expression4, this.VisitExpression(functionType.ReturnType)));
			}
			else
			{
				expression4 = this.VisitExpression(expression4);
			}
			return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library._Value.ReplaceType), new FunctionExpressionSyntaxNode(functionTypeExpression, expression4, function.Range), this.VisitFunctionType(functionType), function.Range);
		}

		// Token: 0x0600A13D RID: 41277 RVA: 0x0021730C File Offset: 0x0021550C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			BinaryOperator2 @operator = binary.Operator;
			if (@operator == BinaryOperator2.As)
			{
				return TypeRemovalVisitor.CreateAsExpression(this.VisitExpression(binary.Left), this.VisitExpression(binary.Right), binary.Range);
			}
			if (@operator != BinaryOperator2.Is)
			{
				return base.VisitBinary(binary);
			}
			return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library._Value.Is), this.VisitExpression(binary.Left), this.VisitExpression(binary.Right), binary.Range);
		}

		// Token: 0x0600A13E RID: 41278 RVA: 0x00217385 File Offset: 0x00215585
		public static IDocument Rewrite(IDocument document)
		{
			return new TypeRemovalVisitor().VisitDocument(document);
		}
	}
}
