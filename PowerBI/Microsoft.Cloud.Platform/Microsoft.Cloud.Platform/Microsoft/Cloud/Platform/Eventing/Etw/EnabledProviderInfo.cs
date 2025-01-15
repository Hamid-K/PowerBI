using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003DD RID: 989
	public class EnabledProviderInfo : IEquatable<EnabledProviderInfo>
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x00072A77 File Offset: 0x00070C77
		public EnabledProviderInfo(Guid id, EtwTraceLevel level, int flags)
		{
			this.Id = id;
			this.Level = level;
			this.Flags = flags;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00072A94 File Offset: 0x00070C94
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x00072A9C File Offset: 0x00070C9C
		public Guid Id { get; private set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00072AA5 File Offset: 0x00070CA5
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x00072AAD File Offset: 0x00070CAD
		public EtwTraceLevel Level { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00072AB6 File Offset: 0x00070CB6
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x00072ABE File Offset: 0x00070CBE
		public int Flags { get; private set; }

		// Token: 0x06001E79 RID: 7801 RVA: 0x00072AC8 File Offset: 0x00070CC8
		public bool Equals(EnabledProviderInfo other)
		{
			return other != null && (this.Id.Equals(other.Id) && this.Level == other.Level) && this.Flags == other.Flags;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00072B0E File Offset: 0x00070D0E
		public override bool Equals(object other)
		{
			return this.Equals(other as EnabledProviderInfo);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00072B1C File Offset: 0x00070D1C
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ (int)this.Level ^ this.Flags;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00072B4C File Offset: 0x00070D4C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "<Id: {0} Level: {1} Flags: {2}>", new object[] { this.Id, this.Level, this.Flags });
		}
	}
}
