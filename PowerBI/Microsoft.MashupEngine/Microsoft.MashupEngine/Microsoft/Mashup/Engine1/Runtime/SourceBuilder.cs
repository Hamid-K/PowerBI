using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001247 RID: 4679
	public class SourceBuilder : ISourceBuilder
	{
		// Token: 0x06007B52 RID: 31570 RVA: 0x001A9B08 File Offset: 0x001A7D08
		private SourceBuilder()
		{
			this.builderStack.Push(new SourceBuilder.RootBuilder());
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x001A9B2B File Offset: 0x001A7D2B
		public static SourceBuilder New()
		{
			return new SourceBuilder();
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06007B54 RID: 31572 RVA: 0x001A9B32 File Offset: 0x001A7D32
		private SourceBuilder.NodeBuilder Builder
		{
			get
			{
				return this.builderStack.Peek();
			}
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x001A9B3F File Offset: 0x001A7D3F
		private void Push(SourceBuilder.NodeBuilder builder)
		{
			this.builderStack.Push(builder);
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x001A9B4D File Offset: 0x001A7D4D
		private SourceBuilder.NodeBuilder Pop(SourceBuilder.BuilderKind kind)
		{
			if (this.Builder.Kind != kind)
			{
				throw new InvalidOperationException();
			}
			return this.builderStack.Pop();
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x001A9B70 File Offset: 0x001A7D70
		public bool IsPrimitive(IValueReference valueRef)
		{
			if (!valueRef.Evaluated)
			{
				return false;
			}
			Value value = valueRef.Value;
			return value.IsDate || value.IsDateTime || value.IsDateTimeZone || value.IsTime || value.IsDuration || value.IsLogical || value.IsNull || value.IsNumber || value.IsText;
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x001A9BD6 File Offset: 0x001A7DD6
		public SourceBuilder Primitive(Value value)
		{
			this.Builder.Insert(ConstantExpressionSyntaxNode.New(value));
			return this;
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x001A9BEA File Offset: 0x001A7DEA
		public SourceBuilder Primitive(string value)
		{
			this.Builder.Insert(ConstantExpressionSyntaxNode.New(value));
			return this;
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x001A9BFE File Offset: 0x001A7DFE
		public SourceBuilder Identifier(string value)
		{
			this.Builder.Insert(new ExclusiveIdentifierExpressionSyntaxNode(value));
			return this;
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x001A9C17 File Offset: 0x001A7E17
		public SourceBuilder BeginList()
		{
			this.Push(new SourceBuilder.ListBuilder());
			return this;
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x001A9C28 File Offset: 0x001A7E28
		public SourceBuilder EndList()
		{
			ISyntaxNode node = this.Pop(SourceBuilder.BuilderKind.List).Node;
			this.Builder.Insert(node);
			return this;
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x001A9C4F File Offset: 0x001A7E4F
		public SourceBuilder BeginRecord()
		{
			this.Push(new SourceBuilder.RecordBuilder());
			return this;
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x001A9C5D File Offset: 0x001A7E5D
		public SourceBuilder AddField(string name)
		{
			this.Builder.AddField(name);
			return this;
		}

		// Token: 0x06007B5F RID: 31583 RVA: 0x001A9C6C File Offset: 0x001A7E6C
		public SourceBuilder EndRecord()
		{
			ISyntaxNode node = this.Pop(SourceBuilder.BuilderKind.Record).Node;
			this.Builder.Insert(node);
			return this;
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x001A9C93 File Offset: 0x001A7E93
		public SourceBuilder BeginInvocation(string functionName)
		{
			this.Push(new SourceBuilder.InvocationBuilder(functionName));
			return this;
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x001A9CA4 File Offset: 0x001A7EA4
		public SourceBuilder EndInvocation()
		{
			ISyntaxNode node = this.Pop(SourceBuilder.BuilderKind.Invocation).Node;
			this.Builder.Insert(node);
			return this;
		}

		// Token: 0x06007B62 RID: 31586 RVA: 0x001A9CCB File Offset: 0x001A7ECB
		public SourceBuilder Insert(SourceBuilder other)
		{
			if (other.Builder.Kind != SourceBuilder.BuilderKind.Root)
			{
				throw new InvalidOperationException();
			}
			this.Builder.Insert(other.Builder.Node);
			return this;
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x001A9CF7 File Offset: 0x001A7EF7
		public string ToSource()
		{
			return this.ToSource(false);
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x001A9D00 File Offset: 0x001A7F00
		public string ToSource(bool prettyPrint)
		{
			if (this.Builder.Kind != SourceBuilder.BuilderKind.Root)
			{
				throw new InvalidOperationException();
			}
			return SourceBuilder.SourceGenerator.Generate(this.Builder.Node, prettyPrint);
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x001A9D26 File Offset: 0x001A7F26
		bool ISourceBuilder.IsPrimitive(IValueReference2 valueRef)
		{
			return this.IsPrimitive((IValueReference)valueRef);
		}

		// Token: 0x06007B66 RID: 31590 RVA: 0x001A9D34 File Offset: 0x001A7F34
		ISourceBuilder ISourceBuilder.Primitive(IValue value)
		{
			return this.Primitive((Value)value);
		}

		// Token: 0x06007B67 RID: 31591 RVA: 0x001A9D42 File Offset: 0x001A7F42
		ISourceBuilder ISourceBuilder.BeginList()
		{
			return this.BeginList();
		}

		// Token: 0x06007B68 RID: 31592 RVA: 0x001A9D4A File Offset: 0x001A7F4A
		ISourceBuilder ISourceBuilder.EndList()
		{
			return this.EndList();
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x001A9D52 File Offset: 0x001A7F52
		ISourceBuilder ISourceBuilder.BeginRecord()
		{
			return this.BeginRecord();
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x001A9D5A File Offset: 0x001A7F5A
		ISourceBuilder ISourceBuilder.AddField(string name)
		{
			return this.AddField(name);
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x001A9D63 File Offset: 0x001A7F63
		ISourceBuilder ISourceBuilder.EndRecord()
		{
			return this.EndRecord();
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x001A9D6B File Offset: 0x001A7F6B
		ISourceBuilder ISourceBuilder.Insert(ISourceBuilder other)
		{
			return this.Insert((SourceBuilder)other);
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x001A9D79 File Offset: 0x001A7F79
		string ISourceBuilder.ToSource()
		{
			return this.ToSource();
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x001A9D81 File Offset: 0x001A7F81
		string ISourceBuilder.ToSource(bool prettyPrint)
		{
			return this.ToSource(prettyPrint);
		}

		// Token: 0x04004472 RID: 17522
		private Stack<SourceBuilder.NodeBuilder> builderStack = new Stack<SourceBuilder.NodeBuilder>();

		// Token: 0x02001248 RID: 4680
		private class SourceGenerator : AstVisitor
		{
			// Token: 0x06007B6F RID: 31599 RVA: 0x001A9D8A File Offset: 0x001A7F8A
			private SourceGenerator(bool prettyPrint)
			{
				this.prettyPrint = prettyPrint;
				this.pendingIndent = true;
			}

			// Token: 0x06007B70 RID: 31600 RVA: 0x001A9DAB File Offset: 0x001A7FAB
			public static string Generate(ISyntaxNode node, bool prettyPrint)
			{
				SourceBuilder.SourceGenerator sourceGenerator = new SourceBuilder.SourceGenerator(prettyPrint);
				sourceGenerator.Visit(node);
				return sourceGenerator.builder.ToString();
			}

			// Token: 0x06007B71 RID: 31601 RVA: 0x001A9DC8 File Offset: 0x001A7FC8
			private void WriteIndent()
			{
				if (this.pendingIndent)
				{
					this.builder.Append("                                        ", 0, this.indent * 4);
				}
				else if (this.pendingSpace)
				{
					this.builder.Append(" ");
				}
				this.pendingIndent = false;
				this.pendingSpace = false;
			}

			// Token: 0x06007B72 RID: 31602 RVA: 0x001A9E20 File Offset: 0x001A8020
			private void WriteSpace()
			{
				if (this.prettyPrint)
				{
					this.pendingSpace = true;
				}
			}

			// Token: 0x06007B73 RID: 31603 RVA: 0x001A9E31 File Offset: 0x001A8031
			private void Write(string text)
			{
				this.WriteIndent();
				this.builder.Append(text);
			}

			// Token: 0x06007B74 RID: 31604 RVA: 0x001A9E46 File Offset: 0x001A8046
			private void WriteLine()
			{
				if (this.prettyPrint)
				{
					this.builder.AppendLine();
					this.pendingIndent = true;
				}
			}

			// Token: 0x06007B75 RID: 31605 RVA: 0x001A9E63 File Offset: 0x001A8063
			private void WriteLine(string text)
			{
				this.Write(text);
				this.WriteLine();
			}

			// Token: 0x06007B76 RID: 31606 RVA: 0x001A9E72 File Offset: 0x001A8072
			private void FreshLine()
			{
				if (!this.pendingIndent)
				{
					this.WriteLine();
				}
			}

			// Token: 0x06007B77 RID: 31607 RVA: 0x001A9E82 File Offset: 0x001A8082
			private void Indent()
			{
				this.indent++;
			}

			// Token: 0x06007B78 RID: 31608 RVA: 0x001A9E92 File Offset: 0x001A8092
			private void Unindent()
			{
				this.indent--;
			}

			// Token: 0x06007B79 RID: 31609 RVA: 0x001A9EA2 File Offset: 0x001A80A2
			private void WriteIdentifier(Identifier identifier)
			{
				this.Write(LanguageServices.Identifier.Escape(identifier));
			}

			// Token: 0x06007B7A RID: 31610 RVA: 0x001A9EB8 File Offset: 0x001A80B8
			protected override IExpression VisitConstant(IConstantExpression constant)
			{
				Value value = constant.Value;
				ValueKind kind = value.Kind;
				if (kind <= ValueKind.Text)
				{
					this.Write(value.ToSource());
					return constant;
				}
				throw new InvalidOperationException();
			}

			// Token: 0x06007B7B RID: 31611 RVA: 0x001A9EEC File Offset: 0x001A80EC
			protected override IExpression VisitList(IListExpression list)
			{
				this.FreshLine();
				this.Write("{");
				this.Indent();
				bool flag = false;
				foreach (IExpression expression in list.Members)
				{
					if (flag)
					{
						this.WriteLine(",");
					}
					else
					{
						this.WriteLine();
					}
					this.VisitExpression(expression);
					flag = true;
				}
				this.Unindent();
				if (flag)
				{
					this.FreshLine();
				}
				else
				{
					this.WriteSpace();
				}
				this.Write("}");
				return list;
			}

			// Token: 0x06007B7C RID: 31612 RVA: 0x001A9F90 File Offset: 0x001A8190
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				this.FreshLine();
				this.Write("[");
				this.Indent();
				bool flag = false;
				foreach (VariableInitializer variableInitializer in record.Members)
				{
					if (flag)
					{
						this.WriteLine(",");
					}
					else
					{
						this.WriteLine();
					}
					this.WriteIdentifier(variableInitializer.Name);
					this.WriteSpace();
					this.Write("=");
					this.WriteSpace();
					this.Indent();
					this.VisitExpression(variableInitializer.Value);
					this.Unindent();
					flag = true;
				}
				this.Unindent();
				if (flag)
				{
					this.FreshLine();
				}
				else
				{
					this.WriteSpace();
				}
				this.Write("]");
				return record;
			}

			// Token: 0x06007B7D RID: 31613 RVA: 0x001AA068 File Offset: 0x001A8268
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				bool flag = false;
				this.VisitExpression(invocation.Function);
				this.Write("(");
				foreach (IExpression expression in invocation.Arguments)
				{
					if (flag)
					{
						this.Write(",");
					}
					this.VisitExpression(expression);
					flag = true;
				}
				this.Write(")");
				return invocation;
			}

			// Token: 0x06007B7E RID: 31614 RVA: 0x001AA0EC File Offset: 0x001A82EC
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				this.WriteIdentifier(identifier.Name);
				return identifier;
			}

			// Token: 0x04004473 RID: 17523
			private const int tabWidth = 4;

			// Token: 0x04004474 RID: 17524
			private const string maxIndent = "                                        ";

			// Token: 0x04004475 RID: 17525
			private StringBuilder builder = new StringBuilder();

			// Token: 0x04004476 RID: 17526
			private bool prettyPrint;

			// Token: 0x04004477 RID: 17527
			private bool pendingIndent;

			// Token: 0x04004478 RID: 17528
			private bool pendingSpace;

			// Token: 0x04004479 RID: 17529
			private int indent;
		}

		// Token: 0x02001249 RID: 4681
		private enum BuilderKind
		{
			// Token: 0x0400447B RID: 17531
			Root,
			// Token: 0x0400447C RID: 17532
			Invocation,
			// Token: 0x0400447D RID: 17533
			List,
			// Token: 0x0400447E RID: 17534
			Record
		}

		// Token: 0x0200124A RID: 4682
		private abstract class NodeBuilder
		{
			// Token: 0x170021B1 RID: 8625
			// (get) Token: 0x06007B7F RID: 31615 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual SourceBuilder.BuilderKind Kind
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170021B2 RID: 8626
			// (get) Token: 0x06007B80 RID: 31616 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual ISyntaxNode Node
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06007B81 RID: 31617 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual void AddField(string name)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06007B82 RID: 31618 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual void Insert(ISyntaxNode node)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200124B RID: 4683
		private sealed class RootBuilder : SourceBuilder.NodeBuilder
		{
			// Token: 0x170021B3 RID: 8627
			// (get) Token: 0x06007B84 RID: 31620 RVA: 0x00002105 File Offset: 0x00000305
			public override SourceBuilder.BuilderKind Kind
			{
				get
				{
					return SourceBuilder.BuilderKind.Root;
				}
			}

			// Token: 0x170021B4 RID: 8628
			// (get) Token: 0x06007B85 RID: 31621 RVA: 0x001AA0FB File Offset: 0x001A82FB
			public override ISyntaxNode Node
			{
				get
				{
					if (this.node == null)
					{
						throw new InvalidOperationException();
					}
					return this.node;
				}
			}

			// Token: 0x06007B86 RID: 31622 RVA: 0x001AA111 File Offset: 0x001A8311
			public override void Insert(ISyntaxNode node)
			{
				if (this.node != null)
				{
					throw new InvalidOperationException();
				}
				this.node = node;
			}

			// Token: 0x0400447F RID: 17535
			private ISyntaxNode node;
		}

		// Token: 0x0200124C RID: 4684
		private sealed class InvocationBuilder : SourceBuilder.NodeBuilder
		{
			// Token: 0x06007B88 RID: 31624 RVA: 0x001AA130 File Offset: 0x001A8330
			public InvocationBuilder(string functionName)
			{
				this.functionName = functionName;
			}

			// Token: 0x170021B5 RID: 8629
			// (get) Token: 0x06007B89 RID: 31625 RVA: 0x00002139 File Offset: 0x00000339
			public override SourceBuilder.BuilderKind Kind
			{
				get
				{
					return SourceBuilder.BuilderKind.Invocation;
				}
			}

			// Token: 0x170021B6 RID: 8630
			// (get) Token: 0x06007B8A RID: 31626 RVA: 0x001AA14C File Offset: 0x001A834C
			public override ISyntaxNode Node
			{
				get
				{
					ExclusiveIdentifierExpressionSyntaxNode exclusiveIdentifierExpressionSyntaxNode = new ExclusiveIdentifierExpressionSyntaxNode(this.functionName);
					switch (this.arguments.Count)
					{
					case 0:
						return new InvocationExpressionSyntaxNode0(exclusiveIdentifierExpressionSyntaxNode);
					case 1:
						return new InvocationExpressionSyntaxNode1(exclusiveIdentifierExpressionSyntaxNode, this.arguments[0]);
					case 2:
						return new InvocationExpressionSyntaxNode2(exclusiveIdentifierExpressionSyntaxNode, this.arguments[0], this.arguments[1]);
					default:
						return new InvocationExpressionSyntaxNodeN(exclusiveIdentifierExpressionSyntaxNode, this.arguments.ToArray());
					}
				}
			}

			// Token: 0x06007B8B RID: 31627 RVA: 0x001AA1D4 File Offset: 0x001A83D4
			public override void Insert(ISyntaxNode node)
			{
				this.arguments.Add((IExpression)node);
			}

			// Token: 0x04004480 RID: 17536
			private readonly string functionName;

			// Token: 0x04004481 RID: 17537
			private readonly List<IExpression> arguments = new List<IExpression>();
		}

		// Token: 0x0200124D RID: 4685
		private sealed class ListBuilder : SourceBuilder.NodeBuilder
		{
			// Token: 0x170021B7 RID: 8631
			// (get) Token: 0x06007B8C RID: 31628 RVA: 0x000023C4 File Offset: 0x000005C4
			public override SourceBuilder.BuilderKind Kind
			{
				get
				{
					return SourceBuilder.BuilderKind.List;
				}
			}

			// Token: 0x170021B8 RID: 8632
			// (get) Token: 0x06007B8D RID: 31629 RVA: 0x001AA1E7 File Offset: 0x001A83E7
			public override ISyntaxNode Node
			{
				get
				{
					return new ListExpressionSyntaxNode(this.items);
				}
			}

			// Token: 0x06007B8E RID: 31630 RVA: 0x001AA1F4 File Offset: 0x001A83F4
			public override void Insert(ISyntaxNode node)
			{
				this.items.Add((IExpression)node);
			}

			// Token: 0x04004482 RID: 17538
			private List<IExpression> items = new List<IExpression>();
		}

		// Token: 0x0200124E RID: 4686
		private sealed class RecordBuilder : SourceBuilder.NodeBuilder
		{
			// Token: 0x170021B9 RID: 8633
			// (get) Token: 0x06007B90 RID: 31632 RVA: 0x0000240C File Offset: 0x0000060C
			public override SourceBuilder.BuilderKind Kind
			{
				get
				{
					return SourceBuilder.BuilderKind.Record;
				}
			}

			// Token: 0x170021BA RID: 8634
			// (get) Token: 0x06007B91 RID: 31633 RVA: 0x001AA21A File Offset: 0x001A841A
			public override ISyntaxNode Node
			{
				get
				{
					if (this.name != null)
					{
						throw new InvalidOperationException();
					}
					return new RecordExpressionSyntaxNode(this.fields.ToArray());
				}
			}

			// Token: 0x06007B92 RID: 31634 RVA: 0x001AA23A File Offset: 0x001A843A
			public override void AddField(string name)
			{
				if (this.name != null)
				{
					throw new InvalidOperationException();
				}
				this.name = name;
			}

			// Token: 0x06007B93 RID: 31635 RVA: 0x001AA251 File Offset: 0x001A8451
			public override void Insert(ISyntaxNode node)
			{
				if (this.name == null)
				{
					throw new InvalidOperationException();
				}
				this.fields.Add(new VariableInitializer(this.name, (IExpression)node));
				this.name = null;
			}

			// Token: 0x04004483 RID: 17539
			private List<VariableInitializer> fields = new List<VariableInitializer>();

			// Token: 0x04004484 RID: 17540
			private string name;
		}
	}
}
