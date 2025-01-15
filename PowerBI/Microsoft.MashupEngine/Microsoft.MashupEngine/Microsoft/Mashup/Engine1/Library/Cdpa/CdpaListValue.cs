using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB8 RID: 3512
	[DataContract]
	internal class CdpaListValue : IEquatable<CdpaListValue>
	{
		// Token: 0x06005F7E RID: 24446 RVA: 0x0014887F File Offset: 0x00146A7F
		public CdpaListValue()
		{
			this.Value = EmptyArray<CdpaListValue>.Instance;
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x00148892 File Offset: 0x00146A92
		// (set) Token: 0x06005F80 RID: 24448 RVA: 0x0014889A File Offset: 0x00146A9A
		[DataMember(Name = "type", IsRequired = true)]
		public string Type { get; set; }

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x001488A3 File Offset: 0x00146AA3
		// (set) Token: 0x06005F82 RID: 24450 RVA: 0x001488AB File Offset: 0x00146AAB
		[DataMember(Name = "value", IsRequired = true)]
		public IList<object> Value { get; set; }

		// Token: 0x06005F83 RID: 24451 RVA: 0x001488B4 File Offset: 0x00146AB4
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaListValue);
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x001488C2 File Offset: 0x00146AC2
		public bool Equals(CdpaListValue other)
		{
			return other != null && this.Value.SetEquals(other.Value);
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x001488DA File Offset: 0x00146ADA
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() * 37 + this.Value.GetHashCode();
		}
	}
}
