using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB6 RID: 3510
	[DataContract]
	internal class CdpaEvent : IEquatable<CdpaEvent>
	{
		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x06005F6E RID: 24430 RVA: 0x00148794 File Offset: 0x00146994
		// (set) Token: 0x06005F6F RID: 24431 RVA: 0x0014879C File Offset: 0x0014699C
		[DataMember(Name = "tenant", IsRequired = true)]
		public CdpaTenant Tenant { get; set; }

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x06005F70 RID: 24432 RVA: 0x001487A5 File Offset: 0x001469A5
		// (set) Token: 0x06005F71 RID: 24433 RVA: 0x001487AD File Offset: 0x001469AD
		[DataMember(Name = "eventName", IsRequired = true)]
		public string EventName { get; set; }

		// Token: 0x06005F72 RID: 24434 RVA: 0x001487B6 File Offset: 0x001469B6
		public override int GetHashCode()
		{
			return this.Tenant.GetHashCode() + this.EventName.GetHashCode();
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x001487CF File Offset: 0x001469CF
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaEvent);
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x001487DD File Offset: 0x001469DD
		public bool Equals(CdpaEvent other)
		{
			return other != null && this.Tenant.Equals(other.Tenant) && this.EventName == other.EventName;
		}
	}
}
