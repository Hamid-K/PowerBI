using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000DF RID: 223
	public interface IExpressionDocument : IDocument, ISyntaxNode
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600034D RID: 845
		IExpression Expression { get; }
	}
}
