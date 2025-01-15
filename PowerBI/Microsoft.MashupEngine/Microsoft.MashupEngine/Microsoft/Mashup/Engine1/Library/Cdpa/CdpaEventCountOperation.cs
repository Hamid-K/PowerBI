using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE5 RID: 3557
	[DataContract]
	internal class CdpaEventCountOperation : CdpaOperation, IEquatable<CdpaEventCountOperation>
	{
		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x0600601C RID: 24604 RVA: 0x001494FF File Offset: 0x001476FF
		[DataMember(Name = "name")]
		public override string Name
		{
			get
			{
				return "eventCount";
			}
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x00149506 File Offset: 0x00147706
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x00149513 File Offset: 0x00147713
		public override bool Equals(CdpaOperation other)
		{
			return this.Equals(other as CdpaEventCountOperation);
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x00149521 File Offset: 0x00147721
		public bool Equals(CdpaEventCountOperation other)
		{
			return other != null && this.Name == other.Name;
		}
	}
}
