using System;
using AngleSharp.Extensions;

namespace AngleSharp.Dom
{
	// Token: 0x02000150 RID: 336
	public sealed class DomException : Exception, IDomException
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x00042C0B File Offset: 0x00040E0B
		public DomException(DomError code)
			: base(code.GetMessage<DomError>())
		{
			this.Code = (int)code;
			this.Name = code.ToString();
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00042C33 File Offset: 0x00040E33
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00042C3B File Offset: 0x00040E3B
		public string Name { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00042C44 File Offset: 0x00040E44
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00042C4C File Offset: 0x00040E4C
		public int Code { get; private set; }
	}
}
