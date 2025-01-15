using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB5 RID: 3509
	[DataContract]
	internal class CdpaTenant : IEquatable<CdpaTenant>
	{
		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x00148704 File Offset: 0x00146904
		// (set) Token: 0x06005F67 RID: 24423 RVA: 0x0014870C File Offset: 0x0014690C
		[DataMember(Name = "targetDocumentId", IsRequired = true)]
		public string TargetDocumentId { get; set; }

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x00148715 File Offset: 0x00146915
		[DataMember(Name = "linkType", IsRequired = true)]
		public string LinkType
		{
			get
			{
				return "reference";
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x06005F69 RID: 24425 RVA: 0x0014871C File Offset: 0x0014691C
		[DataMember(Name = "targetDocumentType", IsRequired = true)]
		public string TargetDocumentType
		{
			get
			{
				return "tenant";
			}
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x00148723 File Offset: 0x00146923
		public override int GetHashCode()
		{
			return this.TargetDocumentId.GetHashCode() + this.LinkType.GetHashCode() + this.TargetDocumentType.GetHashCode();
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x00148748 File Offset: 0x00146948
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaTenant);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x00148756 File Offset: 0x00146956
		public bool Equals(CdpaTenant other)
		{
			return other != null && this.TargetDocumentId == other.TargetDocumentId && this.LinkType == other.LinkType && this.TargetDocumentType == other.TargetDocumentType;
		}
	}
}
