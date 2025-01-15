using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x0200019C RID: 412
	public class ODataEnumSerializer : ODataEdmTypeSerializer
	{
		// Token: 0x06000DA8 RID: 3496 RVA: 0x00036E78 File Offset: 0x00035078
		public ODataEnumSerializer(ODataSerializerProvider serializerProvider)
			: base(ODataPayloadKind.Property, serializerProvider)
		{
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00036E84 File Offset: 0x00035084
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (writeContext.RootElementName == null)
			{
				throw Error.Argument("writeContext", SRResources.RootElementNameMissing, new object[] { typeof(ODataSerializerContext).Name });
			}
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, type);
			messageWriter.WriteProperty(this.CreateProperty(graph, edmType, writeContext.RootElementName, writeContext));
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00036F00 File Offset: 0x00035100
		public sealed override ODataValue CreateODataValue(object graph, IEdmTypeReference expectedType, ODataSerializerContext writeContext)
		{
			if (!expectedType.IsEnum())
			{
				throw Error.InvalidOperation(SRResources.CannotWriteType, new object[]
				{
					typeof(ODataEnumSerializer).Name,
					expectedType.FullName()
				});
			}
			ODataEnumValue odataEnumValue = this.CreateODataEnumValue(graph, expectedType.AsEnum(), writeContext);
			if (odataEnumValue == null)
			{
				return new ODataNullValue();
			}
			return odataEnumValue;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00036F5C File Offset: 0x0003515C
		public virtual ODataEnumValue CreateODataEnumValue(object graph, IEdmEnumTypeReference enumType, ODataSerializerContext writeContext)
		{
			if (graph == null)
			{
				return null;
			}
			string text = null;
			if (TypeHelper.IsEnum(graph.GetType()))
			{
				text = graph.ToString();
			}
			else if (graph.GetType() == typeof(EdmEnumObject))
			{
				text = ((EdmEnumObject)graph).Value;
			}
			ClrEnumMemberAnnotation clrEnumMemberAnnotation = writeContext.Model.GetClrEnumMemberAnnotation(enumType.EnumDefinition());
			if (clrEnumMemberAnnotation != null)
			{
				IEdmEnumMember edmEnumMember = clrEnumMemberAnnotation.GetEdmEnumMember((Enum)graph);
				if (edmEnumMember != null)
				{
					text = edmEnumMember.Name;
				}
			}
			ODataEnumValue odataEnumValue = new ODataEnumValue(text, enumType.FullName());
			ODataMetadataLevel odataMetadataLevel = ((writeContext != null) ? writeContext.MetadataLevel : ODataMetadataLevel.MinimalMetadata);
			ODataEnumSerializer.AddTypeNameAnnotationAsNeeded(odataEnumValue, enumType, odataMetadataLevel);
			return odataEnumValue;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00036FF8 File Offset: 0x000351F8
		internal static void AddTypeNameAnnotationAsNeeded(ODataEnumValue enumValue, IEdmEnumTypeReference enumType, ODataMetadataLevel metadataLevel)
		{
			if (ODataEnumSerializer.ShouldAddTypeNameAnnotation(metadataLevel))
			{
				string text;
				if (ODataEnumSerializer.ShouldSuppressTypeNameSerialization(metadataLevel))
				{
					text = null;
				}
				else
				{
					text = enumType.FullName();
				}
				enumValue.TypeAnnotation = new ODataTypeAnnotation(text);
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003702C File Offset: 0x0003522C
		private static bool ShouldAddTypeNameAnnotation(ODataMetadataLevel metadataLevel)
		{
			if (metadataLevel != ODataMetadataLevel.MinimalMetadata)
			{
				if (metadataLevel - ODataMetadataLevel.FullMetadata > 1)
				{
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003703C File Offset: 0x0003523C
		private static bool ShouldSuppressTypeNameSerialization(ODataMetadataLevel metadataLevel)
		{
			return metadataLevel != ODataMetadataLevel.FullMetadata && metadataLevel == ODataMetadataLevel.NoMetadata;
		}
	}
}
