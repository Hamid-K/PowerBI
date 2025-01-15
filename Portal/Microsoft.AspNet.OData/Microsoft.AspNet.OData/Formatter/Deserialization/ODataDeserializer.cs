using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001BF RID: 447
	public abstract class ODataDeserializer
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003C28B File Offset: 0x0003A48B
		protected ODataDeserializer(ODataPayloadKind payloadKind)
		{
			this.ODataPayloadKind = payloadKind;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0003C29A File Offset: 0x0003A49A
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x0003C2A2 File Offset: 0x0003A4A2
		public ODataPayloadKind ODataPayloadKind { get; private set; }

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003C2AB File Offset: 0x0003A4AB
		public virtual object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			throw Error.NotSupported(SRResources.DeserializerDoesNotSupportRead, new object[] { base.GetType().Name });
		}
	}
}
