using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC6 RID: 3526
	[DataContract]
	internal abstract class UnaryCdpaPropertyFilter : CdpaPropertyFilter
	{
		// Token: 0x06005FAA RID: 24490 RVA: 0x00148A4E File Offset: 0x00146C4E
		public override bool Equals(CdpaPropertyFilter other)
		{
			return this.Equals(other as UnaryCdpaPropertyFilter);
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x00148A5C File Offset: 0x00146C5C
		public bool Equals(UnaryCdpaPropertyFilter other)
		{
			return other != null && this.Operator == other.Operator && base.PropertyName == other.PropertyName;
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x00148A87 File Offset: 0x00146C87
		public override int GetHashCode()
		{
			return this.Operator.GetHashCode() * 37 + base.PropertyName.GetHashCode();
		}
	}
}
