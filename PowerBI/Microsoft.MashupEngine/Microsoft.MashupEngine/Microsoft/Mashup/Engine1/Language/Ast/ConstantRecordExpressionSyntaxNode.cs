using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018B0 RID: 6320
	internal sealed class ConstantRecordExpressionSyntaxNode : RecordExpressionSyntaxNode, IConstantValue, IConstantValue2
	{
		// Token: 0x0600A0FB RID: 41211 RVA: 0x00215ED0 File Offset: 0x002140D0
		public ConstantRecordExpressionSyntaxNode(Value value, params VariableInitializer[] initializers)
			: this(value, initializers, TokenRange.Null)
		{
		}

		// Token: 0x0600A0FC RID: 41212 RVA: 0x00215ED0 File Offset: 0x002140D0
		public ConstantRecordExpressionSyntaxNode(Value value, IList<VariableInitializer> initializers)
			: this(value, initializers, TokenRange.Null)
		{
		}

		// Token: 0x0600A0FD RID: 41213 RVA: 0x00215EDF File Offset: 0x002140DF
		public ConstantRecordExpressionSyntaxNode(Value value, IList<VariableInitializer> initializers, TokenRange range)
			: base(null, initializers, range)
		{
			this.value = value;
		}

		// Token: 0x17002937 RID: 10551
		// (get) Token: 0x0600A0FE RID: 41214 RVA: 0x00215EF1 File Offset: 0x002140F1
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17002938 RID: 10552
		// (get) Token: 0x0600A0FF RID: 41215 RVA: 0x00215EF1 File Offset: 0x002140F1
		IValue IConstantValue2.Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400546B RID: 21611
		private Value value;
	}
}
