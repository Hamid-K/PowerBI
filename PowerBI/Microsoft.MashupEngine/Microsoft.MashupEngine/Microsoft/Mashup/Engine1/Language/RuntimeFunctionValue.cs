using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001751 RID: 5969
	internal abstract class RuntimeFunctionValue : FunctionValue
	{
		// Token: 0x060097C2 RID: 38850 RVA: 0x001F636C File Offset: 0x001F456C
		public static FunctionValue New(Instruction instruction, FunctionTypeValue type, IExpression ast)
		{
			switch (type.Max)
			{
			case 0:
				return new RuntimeFunctionValue0(instruction, type, ast);
			case 1:
				return new RuntimeFunctionValue1(instruction, type, ast);
			case 2:
				return new RuntimeFunctionValue2(instruction, type, ast);
			default:
				return new RuntimeFunctionValueN(instruction, EmptyArray<Value>.Instance, type, ast, EmptyArray<Identifier>.Instance);
			}
		}

		// Token: 0x060097C3 RID: 38851 RVA: 0x001F63C1 File Offset: 0x001F45C1
		protected RuntimeFunctionValue(Instruction instruction, FunctionTypeValue type, IExpression ast)
		{
			this.instruction = instruction;
			this.type = type;
			this.ast = ast;
		}

		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x060097C4 RID: 38852 RVA: 0x001F63DE File Offset: 0x001F45DE
		public sealed override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x060097C5 RID: 38853 RVA: 0x001F63E6 File Offset: 0x001F45E6
		public sealed override IExpression Expression
		{
			get
			{
				return this.ast;
			}
		}

		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x060097C6 RID: 38854 RVA: 0x001F63EE File Offset: 0x001F45EE
		protected int Min
		{
			get
			{
				return this.type.Min;
			}
		}

		// Token: 0x04005067 RID: 20583
		protected readonly Instruction instruction;

		// Token: 0x04005068 RID: 20584
		private readonly FunctionTypeValue type;

		// Token: 0x04005069 RID: 20585
		private readonly IExpression ast;
	}
}
