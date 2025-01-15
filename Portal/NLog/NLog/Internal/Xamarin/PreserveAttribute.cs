using System;

namespace NLog.Internal.Xamarin
{
	// Token: 0x02000151 RID: 337
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PreserveAttribute : Attribute
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00029B32 File Offset: 0x00027D32
		public bool AllMembers { get; } = true;

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00029B3A File Offset: 0x00027D3A
		public bool Conditional { get; }
	}
}
