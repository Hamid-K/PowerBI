using System;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000487 RID: 1159
	public interface ISapBwMicrosoftConnection
	{
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600268F RID: 9871
		// (set) Token: 0x06002690 RID: 9872
		Func<string, IDisposable> ImpersonationWrapper { get; set; }
	}
}
