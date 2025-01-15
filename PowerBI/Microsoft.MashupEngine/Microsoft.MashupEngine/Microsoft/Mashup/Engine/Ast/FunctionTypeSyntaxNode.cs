using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BCA RID: 7114
	public sealed class FunctionTypeSyntaxNode : RangeSyntaxNode, IFunctionTypeExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B18C RID: 45452 RVA: 0x00243C93 File Offset: 0x00241E93
		public FunctionTypeSyntaxNode(IExpression returnType, IList<IParameter> parameters, int min)
			: this(returnType, parameters, min, TokenRange.Null)
		{
		}

		// Token: 0x0600B18D RID: 45453 RVA: 0x00243CA3 File Offset: 0x00241EA3
		public FunctionTypeSyntaxNode(IExpression returnType, IList<IParameter> parameters, int min, TokenRange range)
			: base(range)
		{
			this.returnType = returnType;
			this.parameters = parameters;
			this.min = min;
		}

		// Token: 0x17002C94 RID: 11412
		// (get) Token: 0x0600B18E RID: 45454 RVA: 0x0012AF09 File Offset: 0x00129109
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.FunctionType;
			}
		}

		// Token: 0x17002C95 RID: 11413
		// (get) Token: 0x0600B18F RID: 45455 RVA: 0x00243CC2 File Offset: 0x00241EC2
		public IExpression ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17002C96 RID: 11414
		// (get) Token: 0x0600B190 RID: 45456 RVA: 0x00243CCA File Offset: 0x00241ECA
		public IList<IParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002C97 RID: 11415
		// (get) Token: 0x0600B191 RID: 45457 RVA: 0x00243CD2 File Offset: 0x00241ED2
		public int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x04005B02 RID: 23298
		private IExpression returnType;

		// Token: 0x04005B03 RID: 23299
		private IList<IParameter> parameters;

		// Token: 0x04005B04 RID: 23300
		private int min;
	}
}
