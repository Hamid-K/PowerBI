using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001645 RID: 5701
	internal sealed class TransformedTableValue : OptimizableTableValue
	{
		// Token: 0x06008FB4 RID: 36788 RVA: 0x001DDEAD File Offset: 0x001DC0AD
		private TransformedTableValue(TableValue table, FunctionValue function, Value[] arguments)
		{
			this.table = table;
			this.function = function;
			this.arguments = arguments;
		}

		// Token: 0x06008FB5 RID: 36789 RVA: 0x001DDECA File Offset: 0x001DC0CA
		public static TableValue New(TableValue table, FunctionValue function, Value[] arguments)
		{
			if (table is QueryTableValue)
			{
				return table;
			}
			return new TransformedTableValue(table, function, arguments);
		}

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x06008FB6 RID: 36790 RVA: 0x001DDEDE File Offset: 0x001DC0DE
		public override TypeValue Type
		{
			get
			{
				return this.table.Type;
			}
		}

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x06008FB7 RID: 36791 RVA: 0x001DDEEC File Offset: 0x001DC0EC
		public override IExpression Expression
		{
			get
			{
				IExpression[] array = new IExpression[this.arguments.Length];
				for (int i = 0; i < array.Length; i++)
				{
					IExpression[] array2 = array;
					int num = i;
					IExpression expression2;
					if (!(this.arguments[i] is IOptimizedValue))
					{
						IExpression expression = new ConstantExpressionSyntaxNode(this.arguments[i]);
						expression2 = expression;
					}
					else
					{
						expression2 = this.arguments[i].Expression;
					}
					array2[num] = expression2;
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(this.function), array);
			}
		}

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x06008FB8 RID: 36792 RVA: 0x001DDF56 File Offset: 0x001DC156
		public override RecordValue MetaValue
		{
			get
			{
				return this.table.MetaValue;
			}
		}

		// Token: 0x06008FB9 RID: 36793 RVA: 0x001DDF63 File Offset: 0x001DC163
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x06008FBA RID: 36794 RVA: 0x001DDF70 File Offset: 0x001DC170
		public override TableValue Optimize()
		{
			Value[] array = new Value[this.arguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Library._Value.Optimize.Invoke(this.arguments[i]);
			}
			return new OptimizedTableValue(new TransformedTableValue(this.table, this.function, array));
		}

		// Token: 0x04004D93 RID: 19859
		private readonly TableValue table;

		// Token: 0x04004D94 RID: 19860
		private readonly FunctionValue function;

		// Token: 0x04004D95 RID: 19861
		private readonly Value[] arguments;
	}
}
