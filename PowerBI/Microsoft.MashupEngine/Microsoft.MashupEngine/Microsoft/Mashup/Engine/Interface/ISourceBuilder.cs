using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000061 RID: 97
	public interface ISourceBuilder
	{
		// Token: 0x06000193 RID: 403
		bool IsPrimitive(IValueReference2 valueRef);

		// Token: 0x06000194 RID: 404
		ISourceBuilder Primitive(IValue value);

		// Token: 0x06000195 RID: 405
		ISourceBuilder BeginList();

		// Token: 0x06000196 RID: 406
		ISourceBuilder EndList();

		// Token: 0x06000197 RID: 407
		ISourceBuilder BeginRecord();

		// Token: 0x06000198 RID: 408
		ISourceBuilder AddField(string name);

		// Token: 0x06000199 RID: 409
		ISourceBuilder EndRecord();

		// Token: 0x0600019A RID: 410
		ISourceBuilder Insert(ISourceBuilder other);

		// Token: 0x0600019B RID: 411
		string ToSource();

		// Token: 0x0600019C RID: 412
		string ToSource(bool prettyPrint);
	}
}
