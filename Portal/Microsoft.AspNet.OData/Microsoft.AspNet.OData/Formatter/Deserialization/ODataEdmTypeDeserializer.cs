using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001C0 RID: 448
	public abstract class ODataEdmTypeDeserializer : ODataDeserializer
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003C2CB File Offset: 0x0003A4CB
		protected ODataEdmTypeDeserializer(ODataPayloadKind payloadKind)
			: base(payloadKind)
		{
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003C2D4 File Offset: 0x0003A4D4
		protected ODataEdmTypeDeserializer(ODataPayloadKind payloadKind, ODataDeserializerProvider deserializerProvider)
			: this(payloadKind)
		{
			if (deserializerProvider == null)
			{
				throw Error.ArgumentNull("deserializerProvider");
			}
			this.DeserializerProvider = deserializerProvider;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003C2F2 File Offset: 0x0003A4F2
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x0003C2FA File Offset: 0x0003A4FA
		public ODataDeserializerProvider DeserializerProvider { get; private set; }

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003C303 File Offset: 0x0003A503
		public virtual object ReadInline(object item, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			throw Error.NotSupported(SRResources.DoesNotSupportReadInLine, new object[] { base.GetType().Name });
		}
	}
}
