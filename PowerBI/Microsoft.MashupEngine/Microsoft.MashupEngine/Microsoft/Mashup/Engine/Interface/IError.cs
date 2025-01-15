using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000075 RID: 117
	public interface IError
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001BD RID: 445
		ErrorKind Kind { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BE RID: 446
		SourceLocation Location { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BF RID: 447
		ErrorRange ErrorRange { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C0 RID: 448
		string Message { get; }
	}
}
