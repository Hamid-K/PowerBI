using System;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B3 RID: 435
	public class ODataEnumDeserializer : ODataEdmTypeDeserializer
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x0003AD25 File Offset: 0x00038F25
		public ODataEnumDeserializer()
			: base(ODataPayloadKind.Property)
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003AD30 File Offset: 0x00038F30
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			IEdmTypeReference edmType = readContext.GetEdmType(type);
			ODataProperty odataProperty = messageReader.ReadProperty(edmType);
			return this.ReadInline(odataProperty, edmType, readContext);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003AD88 File Offset: 0x00038F88
		public override object ReadInline(object item, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			if (item == null)
			{
				return null;
			}
			ODataProperty odataProperty = item as ODataProperty;
			if (odataProperty != null)
			{
				item = odataProperty.Value;
			}
			IEdmEnumTypeReference edmEnumTypeReference = edmType.AsEnum();
			ODataEnumValue enumValue = item as ODataEnumValue;
			if (readContext.IsUntyped)
			{
				return new EdmEnumObject(edmEnumTypeReference, enumValue.Value);
			}
			IEdmEnumType edmEnumType = edmEnumTypeReference.EnumDefinition();
			ClrEnumMemberAnnotation clrEnumMemberAnnotation = readContext.Model.GetClrEnumMemberAnnotation(edmEnumType);
			if (clrEnumMemberAnnotation != null && enumValue != null)
			{
				IEdmEnumMember edmEnumMember = edmEnumType.Members.FirstOrDefault((IEdmEnumMember m) => m.Name == enumValue.Value);
				if (edmEnumMember != null)
				{
					Enum clrEnumMember = clrEnumMemberAnnotation.GetClrEnumMember(edmEnumMember);
					if (clrEnumMember != null)
					{
						return clrEnumMember;
					}
				}
			}
			Type clrType = EdmLibHelpers.GetClrType(edmType, readContext.Model);
			return EnumDeserializationHelpers.ConvertEnumValue(item, clrType);
		}
	}
}
