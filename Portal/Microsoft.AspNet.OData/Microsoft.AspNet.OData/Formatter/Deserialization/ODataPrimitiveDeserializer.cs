using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001BE RID: 446
	public class ODataPrimitiveDeserializer : ODataEdmTypeDeserializer
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003AD25 File Offset: 0x00038F25
		public ODataPrimitiveDeserializer()
			: base(ODataPayloadKind.Property)
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003C1B4 File Offset: 0x0003A3B4
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			IEdmTypeReference edmType = readContext.GetEdmType(type);
			ODataProperty odataProperty = messageReader.ReadProperty(edmType);
			return this.ReadInline(odataProperty, edmType, readContext);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003C1E8 File Offset: 0x0003A3E8
		public sealed override object ReadInline(object item, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			if (item == null)
			{
				return null;
			}
			ODataProperty odataProperty = item as ODataProperty;
			if (odataProperty == null)
			{
				throw Error.Argument("item", SRResources.ArgumentMustBeOfType, new object[] { typeof(ODataProperty).Name });
			}
			return this.ReadPrimitive(odataProperty, readContext);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003C234 File Offset: 0x0003A434
		public virtual object ReadPrimitive(ODataProperty primitiveProperty, ODataDeserializerContext readContext)
		{
			if (primitiveProperty == null)
			{
				throw Error.ArgumentNull("primitiveProperty");
			}
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			if (readContext.ResourceType != null && primitiveProperty.Value != null)
			{
				return EdmPrimitiveHelpers.ConvertPrimitiveValue(primitiveProperty.Value, readContext.ResourceType);
			}
			return primitiveProperty.Value;
		}
	}
}
