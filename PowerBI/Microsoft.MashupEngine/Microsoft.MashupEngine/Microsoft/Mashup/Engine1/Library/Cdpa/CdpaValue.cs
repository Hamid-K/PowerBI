using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB7 RID: 3511
	[DataContract]
	internal class CdpaValue : IEquatable<CdpaValue>
	{
		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06005F76 RID: 24438 RVA: 0x00148808 File Offset: 0x00146A08
		// (set) Token: 0x06005F77 RID: 24439 RVA: 0x00148810 File Offset: 0x00146A10
		[DataMember(Name = "type", IsRequired = true)]
		public string Type { get; set; }

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x00148819 File Offset: 0x00146A19
		// (set) Token: 0x06005F79 RID: 24441 RVA: 0x00148821 File Offset: 0x00146A21
		[DataMember(Name = "value", IsRequired = true)]
		public object Value { get; set; }

		// Token: 0x06005F7A RID: 24442 RVA: 0x0014882A File Offset: 0x00146A2A
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaValue);
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x00148838 File Offset: 0x00146A38
		public bool Equals(CdpaValue other)
		{
			return other != null && this.Type == other.Type && this.Value.NullableEquals(other.Value);
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x00148863 File Offset: 0x00146A63
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() * 37 + this.Value.NullableGetHashCode<object>();
		}
	}
}
