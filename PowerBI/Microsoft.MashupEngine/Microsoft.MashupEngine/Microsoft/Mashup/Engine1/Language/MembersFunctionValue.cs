using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001755 RID: 5973
	internal abstract class MembersFunctionValue : FunctionValue
	{
		// Token: 0x060097D0 RID: 38864 RVA: 0x001F64D0 File Offset: 0x001F46D0
		public static FunctionValue New(Instruction instruction, FunctionTypeValue type, IExpression ast, Value[] members, Identifier[] memberNames)
		{
			switch (type.Max)
			{
			case 0:
				return new MembersFunctionValue0(instruction, type, ast, members, memberNames);
			case 1:
				return new MembersFunctionValue1(instruction, type, ast, members, memberNames);
			case 2:
				return new MembersFunctionValue2(instruction, type, ast, members, memberNames);
			default:
				return new RuntimeFunctionValueN(instruction, members, type, ast, memberNames);
			}
		}

		// Token: 0x060097D1 RID: 38865 RVA: 0x001F6527 File Offset: 0x001F4727
		public MembersFunctionValue(Instruction instruction, FunctionTypeValue type, IExpression ast, Value[] members, Identifier[] memberNames)
		{
			this.instruction = instruction;
			this.type = type;
			this.members = members;
			this.memberNames = memberNames;
			this.ast = ast;
		}

		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x060097D2 RID: 38866 RVA: 0x001F6554 File Offset: 0x001F4754
		public sealed override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x060097D3 RID: 38867 RVA: 0x001F655C File Offset: 0x001F475C
		public sealed override IExpression Expression
		{
			get
			{
				if (this.ast != null && this.memberNames != null)
				{
					this.ast = new MembersFunctionValue.SubstitutingVisitor(this.memberNames, this.members).Substitute(this.ast);
					this.memberNames = null;
				}
				return this.ast;
			}
		}

		// Token: 0x060097D4 RID: 38868 RVA: 0x001F65A8 File Offset: 0x001F47A8
		public Value Member(int index)
		{
			return this.members[index];
		}

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x060097D5 RID: 38869 RVA: 0x001F65B2 File Offset: 0x001F47B2
		protected int Min
		{
			get
			{
				return this.type.Min;
			}
		}

		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x060097D6 RID: 38870 RVA: 0x001F65BF File Offset: 0x001F47BF
		protected int Max
		{
			get
			{
				return this.type.Max;
			}
		}

		// Token: 0x0400506A RID: 20586
		protected readonly Instruction instruction;

		// Token: 0x0400506B RID: 20587
		private readonly FunctionTypeValue type;

		// Token: 0x0400506C RID: 20588
		private readonly Value[] members;

		// Token: 0x0400506D RID: 20589
		private Identifier[] memberNames;

		// Token: 0x0400506E RID: 20590
		private IExpression ast;

		// Token: 0x02001756 RID: 5974
		private sealed class SubstitutingVisitor : AstVisitor
		{
			// Token: 0x060097D7 RID: 38871 RVA: 0x001F65CC File Offset: 0x001F47CC
			public SubstitutingVisitor(Identifier[] memberNames, Value[] members)
			{
				this.dictionary = new Dictionary<Identifier, int>();
				for (int i = 0; i < memberNames.Length; i++)
				{
					this.dictionary.Add(memberNames[i], i);
				}
				this.members = members;
			}

			// Token: 0x060097D8 RID: 38872 RVA: 0x00146DFF File Offset: 0x00144FFF
			public IExpression Substitute(IExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x060097D9 RID: 38873 RVA: 0x001F6610 File Offset: 0x001F4810
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				int num;
				if (this.dictionary.TryGetValue(identifier.Name, out num))
				{
					return new ConstantExpressionSyntaxNode(this.members[num]);
				}
				return identifier;
			}

			// Token: 0x0400506F RID: 20591
			private readonly Dictionary<Identifier, int> dictionary;

			// Token: 0x04005070 RID: 20592
			private readonly Value[] members;
		}
	}
}
