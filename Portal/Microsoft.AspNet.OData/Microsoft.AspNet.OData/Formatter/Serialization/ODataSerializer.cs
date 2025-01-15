using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A8 RID: 424
	public abstract class ODataSerializer
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x00037ED3 File Offset: 0x000360D3
		protected ODataSerializer(ODataPayloadKind payloadKind)
		{
			ODataPayloadKindHelper.Validate(payloadKind, "payloadKind");
			this.ODataPayloadKind = payloadKind;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00037EED File Offset: 0x000360ED
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00037EF5 File Offset: 0x000360F5
		public ODataPayloadKind ODataPayloadKind { get; private set; }

		// Token: 0x06000DFC RID: 3580 RVA: 0x00037EFE File Offset: 0x000360FE
		public virtual void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			throw Error.NotSupported(SRResources.WriteObjectNotSupported, new object[] { base.GetType().Name });
		}
	}
}
