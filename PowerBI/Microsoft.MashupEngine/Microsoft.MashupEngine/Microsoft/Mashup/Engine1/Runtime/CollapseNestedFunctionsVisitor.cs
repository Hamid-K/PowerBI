using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200129E RID: 4766
	internal sealed class CollapseNestedFunctionsVisitor : LogicalAstVisitor<CollapseNestedFunctionsVisitor.Declaration>
	{
		// Token: 0x06007D25 RID: 32037 RVA: 0x001AD329 File Offset: 0x001AB529
		private CollapseNestedFunctionsVisitor()
		{
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x06007D26 RID: 32038 RVA: 0x001AD331 File Offset: 0x001AB531
		private int Depth
		{
			get
			{
				return this.references.Count;
			}
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x001AD33E File Offset: 0x001AB53E
		public IExpression CollapseNestedFunctions(IExpression node)
		{
			this.references = new Stack<Dictionary<Identifier, int>>();
			return this.VisitExpression(node);
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x001AD352 File Offset: 0x001AB552
		protected override IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression node)
		{
			return this.VisitIdentifier(node);
		}

		// Token: 0x06007D29 RID: 32041 RVA: 0x001AD35C File Offset: 0x001AB55C
		protected override IExpression VisitIdentifier(IIdentifierExpression node)
		{
			CollapseNestedFunctionsVisitor.Declaration declaration;
			if (base.Environment.TryGetValue(node.Name, node.IsInclusive, out declaration))
			{
				this.references.Peek()[node.Name] = declaration.depth;
			}
			return node;
		}

		// Token: 0x06007D2A RID: 32042 RVA: 0x001AD3A4 File Offset: 0x001AB5A4
		private Identifier[] PopDeclarations()
		{
			Dictionary<Identifier, int> dictionary = this.references.Pop();
			List<Identifier> list = new List<Identifier>();
			foreach (KeyValuePair<Identifier, int> keyValuePair in dictionary)
			{
				if (keyValuePair.Value <= this.references.Count)
				{
					list.Add(keyValuePair.Key);
					this.references.Peek()[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return list.ToArray();
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x001AD440 File Offset: 0x001AB640
		private CollapseNestedFunctionsVisitor.Declaration[] PushDeclarations(int count)
		{
			this.references.Push(new Dictionary<Identifier, int>());
			CollapseNestedFunctionsVisitor.Declaration[] array = new CollapseNestedFunctionsVisitor.Declaration[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new CollapseNestedFunctionsVisitor.Declaration
				{
					depth = this.Depth
				};
			}
			return array;
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x001AD490 File Offset: 0x001AB690
		protected override IExpression VisitFunction(IFunctionExpression node)
		{
			CollapseNestedFunctionsVisitor.Declaration[] array = this.PushDeclarations(node.FunctionType.Parameters.Count);
			node = base.VisitFunction(node, array);
			Identifier[] array2 = this.PopDeclarations();
			if (this.Depth > 0)
			{
				if (array2.Length != 0)
				{
					node = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(array2), node);
				}
				IExpression expression = new ConstantExpressionSyntaxNode(new Compiler(CompileOptions.None).ToFunction(node));
				if (array2.Length != 0)
				{
					IExpression expression2 = expression;
					IExpression[] array3 = array2.Select((Identifier r) => new InclusiveIdentifierExpressionSyntaxNode(r)).ToArray<InclusiveIdentifierExpressionSyntaxNode>();
					expression = new InvocationExpressionSyntaxNodeN(expression2, array3);
				}
				return expression;
			}
			return node;
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x001AD52C File Offset: 0x001AB72C
		protected override IExpression VisitLet(ILetExpression node)
		{
			CollapseNestedFunctionsVisitor.Declaration[] array = this.PushDeclarations(node.Variables.Count);
			node = (ILetExpression)base.VisitLet(node, array);
			this.PopDeclarations();
			return node;
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x001AD564 File Offset: 0x001AB764
		protected override IExpression VisitRecord(IRecordExpression node)
		{
			CollapseNestedFunctionsVisitor.Declaration[] array = this.PushDeclarations(node.Members.Count);
			node = base.VisitRecord(node, new CollapseNestedFunctionsVisitor.Declaration
			{
				depth = this.Depth
			}, array);
			this.PopDeclarations();
			return node;
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x001AD5AC File Offset: 0x001AB7AC
		protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
		{
			CollapseNestedFunctionsVisitor.Declaration[] array = this.PushDeclarations(1);
			tryCatchExceptionCase = base.VisitTryCatchExceptionCase(tryCatchExceptionCase, array[0]);
			this.PopDeclarations();
			return tryCatchExceptionCase;
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x001AD5DC File Offset: 0x001AB7DC
		protected override ISection VisitModule(ISection module)
		{
			CollapseNestedFunctionsVisitor.Declaration[] array = this.PushDeclarations(module.Members.Count);
			module = base.VisitModule(module, array);
			this.PopDeclarations();
			return module;
		}

		// Token: 0x040044F7 RID: 17655
		public static readonly CollapseNestedFunctionsVisitor Instance = new CollapseNestedFunctionsVisitor();

		// Token: 0x040044F8 RID: 17656
		private Stack<Dictionary<Identifier, int>> references;

		// Token: 0x0200129F RID: 4767
		public struct Declaration
		{
			// Token: 0x040044F9 RID: 17657
			public int depth;
		}
	}
}
