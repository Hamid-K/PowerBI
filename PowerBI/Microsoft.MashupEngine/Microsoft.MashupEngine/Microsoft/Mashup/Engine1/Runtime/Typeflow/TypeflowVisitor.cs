using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime.Typeflow
{
	// Token: 0x020016EA RID: 5866
	internal abstract class TypeflowVisitor : LogicalAstVisitor<TypeValue>, ITypeflowEnvironment
	{
		// Token: 0x06009519 RID: 38169 RVA: 0x001ED3D0 File Offset: 0x001EB5D0
		protected TypeflowVisitor.TypeScope NewTypeScope()
		{
			return new TypeflowVisitor.TypeScope(this);
		}

		// Token: 0x0600951A RID: 38170 RVA: 0x001ED3D8 File Offset: 0x001EB5D8
		protected IExpression SetType(IExpression expression, TypeValue type)
		{
			this.typeNode.expression = expression;
			this.typeNode.type = type;
			return expression;
		}

		// Token: 0x0600951B RID: 38171 RVA: 0x001ED3F3 File Offset: 0x001EB5F3
		protected TypeValue GetType(IExpression expression)
		{
			return this.GetType(expression, this.typeNode) ?? TypeValue.Any;
		}

		// Token: 0x0600951C RID: 38172 RVA: 0x001ED40C File Offset: 0x001EB60C
		private TypeValue GetType(IExpression expression, TypeflowVisitor.TypeNode typeNode)
		{
			TypeValue typeValue = null;
			while (typeValue == null && typeNode != null)
			{
				if (expression == typeNode.expression)
				{
					typeValue = typeNode.type;
				}
				else
				{
					typeValue = this.GetType(expression, typeNode.firstChild);
				}
				typeNode = typeNode.nextPeer;
			}
			return typeValue;
		}

		// Token: 0x0600951D RID: 38173 RVA: 0x001ED44C File Offset: 0x001EB64C
		protected override IExpression VisitExpression(IExpression expression)
		{
			IExpression expression2;
			using (this.NewTypeScope())
			{
				expression2 = base.VisitExpression(expression);
			}
			return expression2;
		}

		// Token: 0x0600951E RID: 38174 RVA: 0x001ED48C File Offset: 0x001EB68C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			binary = (IBinaryExpression)base.VisitBinary(binary);
			TypeValue typeValue = OperatorTypeflowModels.Binary(binary.Operator, this.GetType(binary.Left), this.GetType(binary.Right));
			return this.SetType(binary, typeValue);
		}

		// Token: 0x0600951F RID: 38175 RVA: 0x001ED4D3 File Offset: 0x001EB6D3
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			return this.SetType(constant, constant.Value.Type);
		}

		// Token: 0x06009520 RID: 38176 RVA: 0x001ED4E8 File Offset: 0x001EB6E8
		protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			fieldAccess = (IFieldAccessExpression)base.VisitFieldAccess(fieldAccess);
			TypeValue type = this.GetType(fieldAccess.Expression);
			TypeValue nullable;
			bool flag;
			if (type.IsRecordType && type.AsRecordType.TryGetFieldType(fieldAccess.MemberName, out nullable, out flag))
			{
				if (flag)
				{
					nullable = nullable.Nullable;
				}
				this.SetType(fieldAccess, nullable);
			}
			return fieldAccess;
		}

		// Token: 0x06009521 RID: 38177 RVA: 0x001ED548 File Offset: 0x001EB748
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			TypeValue[] array = new TypeValue[function.FunctionType.Parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Value value;
				if (function.FunctionType.Parameters[i].Type.TryGetConstant(out value) && value.IsType)
				{
					array[i] = value.AsType;
				}
			}
			return base.VisitFunction(function, array);
		}

		// Token: 0x06009522 RID: 38178 RVA: 0x001ED5B4 File Offset: 0x001EB7B4
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			TypeValue typeValue;
			if (base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out typeValue) && typeValue != null && typeValue.TypeKind != ValueKind.Any)
			{
				this.SetType(identifier, typeValue);
			}
			return identifier;
		}

		// Token: 0x06009523 RID: 38179 RVA: 0x001ED5F2 File Offset: 0x001EB7F2
		protected override IExpression VisitIf(IIfExpression @if)
		{
			@if = (IIfExpression)base.VisitIf(@if);
			return this.SetType(@if, TypeAlgebra.Union(this.GetType(@if.TrueCase), this.GetType(@if.FalseCase)));
		}

		// Token: 0x06009524 RID: 38180 RVA: 0x001ED628 File Offset: 0x001EB828
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			invocation = (IInvocationExpression)base.VisitInvocation(invocation);
			Value value;
			TypeValue typeValue;
			if (invocation.Function.TryGetConstant(out value) && value.IsFunction)
			{
				typeValue = this.GetReturnType(value.AsFunction, invocation.Arguments);
			}
			else
			{
				typeValue = TypeValue.Any;
				TypeValue type = this.GetType(invocation.Function);
				if (type != null && type.IsFunctionType)
				{
					FunctionTypeValue asFunctionType = type.AsFunctionType;
					if (!asFunctionType.Abstract)
					{
						typeValue = asFunctionType.ReturnType;
					}
				}
			}
			return this.SetType(invocation, typeValue);
		}

		// Token: 0x06009525 RID: 38181 RVA: 0x001ED6AB File Offset: 0x001EB8AB
		protected override IExpression VisitLet(ILetExpression let)
		{
			return base.VisitLet(let, new TypeValue[let.Variables.Count]);
		}

		// Token: 0x06009526 RID: 38182 RVA: 0x001ED6C4 File Offset: 0x001EB8C4
		protected override ISection VisitModule(ISection module)
		{
			return base.VisitModule(module, null);
		}

		// Token: 0x06009527 RID: 38183 RVA: 0x001ED6CE File Offset: 0x001EB8CE
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			return base.VisitRecord(record, null, new TypeValue[record.Members.Count]);
		}

		// Token: 0x06009528 RID: 38184 RVA: 0x001ED6E8 File Offset: 0x001EB8E8
		protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, null);
		}

		// Token: 0x06009529 RID: 38185 RVA: 0x001ED6F2 File Offset: 0x001EB8F2
		TypeValue ITypeflowEnvironment.GetType(IExpression expression)
		{
			return this.GetType(expression);
		}

		// Token: 0x0600952A RID: 38186 RVA: 0x001ED6FB File Offset: 0x001EB8FB
		private void TrimTypeNode(TypeflowVisitor.TypeNode typeNode, int typeDepth)
		{
			while (typeNode != null)
			{
				if (typeDepth <= 1)
				{
					typeNode.firstChild = null;
				}
				if (typeNode.firstChild != null)
				{
					this.TrimTypeNode(typeNode.firstChild, typeDepth - 1);
				}
				typeNode = typeNode.nextPeer;
			}
		}

		// Token: 0x04004F45 RID: 20293
		private const int maxTypeDepth = 3;

		// Token: 0x04004F46 RID: 20294
		private TypeflowVisitor.TypeNode typeNode;

		// Token: 0x020016EB RID: 5867
		private class TypeNode
		{
			// Token: 0x04004F47 RID: 20295
			public IExpression expression;

			// Token: 0x04004F48 RID: 20296
			public TypeValue type;

			// Token: 0x04004F49 RID: 20297
			public TypeflowVisitor.TypeNode nextPeer;

			// Token: 0x04004F4A RID: 20298
			public TypeflowVisitor.TypeNode firstChild;
		}

		// Token: 0x020016EC RID: 5868
		protected struct TypeScope : IDisposable
		{
			// Token: 0x0600952C RID: 38188 RVA: 0x001ED72C File Offset: 0x001EB92C
			public TypeScope(TypeflowVisitor visitor)
			{
				this.visitor = visitor;
				this.lastTypeNode = this.visitor.typeNode;
				this.visitor.typeNode = new TypeflowVisitor.TypeNode();
			}

			// Token: 0x0600952D RID: 38189 RVA: 0x001ED758 File Offset: 0x001EB958
			public void Dispose()
			{
				this.visitor.TrimTypeNode(this.visitor.typeNode, 2);
				if (this.lastTypeNode != null)
				{
					if (this.lastTypeNode.firstChild == null)
					{
						this.lastTypeNode.firstChild = this.visitor.typeNode;
					}
					else
					{
						TypeflowVisitor.TypeNode typeNode = this.lastTypeNode.firstChild;
						while (typeNode.nextPeer != null)
						{
							typeNode = typeNode.nextPeer;
						}
						typeNode.nextPeer = this.visitor.typeNode;
					}
				}
				this.visitor.typeNode = this.lastTypeNode;
			}

			// Token: 0x04004F4B RID: 20299
			private readonly TypeflowVisitor visitor;

			// Token: 0x04004F4C RID: 20300
			private TypeflowVisitor.TypeNode lastTypeNode;
		}
	}
}
