using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000052 RID: 82
	public interface IUserContext
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E8 RID: 232
		string UserName { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E9 RID: 233
		object Token { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EA RID: 234
		AuthenticationType AuthenticationType { get; }
	}
}
