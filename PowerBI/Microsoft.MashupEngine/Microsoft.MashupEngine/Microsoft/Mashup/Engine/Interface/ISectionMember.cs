using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000DC RID: 220
	public interface ISectionMember : ISyntaxNode
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000345 RID: 837
		Identifier Name { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000346 RID: 838
		IRecordExpression Attribute { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000347 RID: 839
		bool Export { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000348 RID: 840
		IExpression Value { get; }
	}
}
