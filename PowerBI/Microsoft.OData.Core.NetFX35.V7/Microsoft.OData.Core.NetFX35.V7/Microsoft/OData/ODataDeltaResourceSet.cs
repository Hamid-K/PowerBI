using System;

namespace Microsoft.OData
{
	// Token: 0x0200004F RID: 79
	public sealed class ODataDeltaResourceSet : ODataResourceSetBase
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009B06 File Offset: 0x00007D06
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00009B0E File Offset: 0x00007D0E
		internal ODataDeltaResourceSetSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataDeltaResourceSetSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000175 RID: 373
		private ODataDeltaResourceSetSerializationInfo serializationInfo;
	}
}
