using System;

namespace Microsoft.OData
{
	// Token: 0x0200007F RID: 127
	public sealed class ODataResource : ODataResourceBase
	{
		// Token: 0x170000D1 RID: 209
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000BB7D File Offset: 0x00009D7D
		internal override ODataResourceSerializationInfo SerializationInfo
		{
			set
			{
				base.SerializationInfo = ODataResourceSerializationInfo.Validate(value);
			}
		}
	}
}
