using System;

namespace System.Web.Http.Services
{
	// Token: 0x020000A5 RID: 165
	public interface IDecorator<out T>
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003F3 RID: 1011
		T Inner { get; }
	}
}
