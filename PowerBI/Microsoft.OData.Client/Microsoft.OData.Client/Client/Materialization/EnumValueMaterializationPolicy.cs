using System;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000F6 RID: 246
	internal class EnumValueMaterializationPolicy
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x00027094 File Offset: 0x00025294
		internal EnumValueMaterializationPolicy(IODataMaterializerContext context)
		{
			this.context = context;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000270A4 File Offset: 0x000252A4
		public object MaterializeEnumTypeProperty(Type valueType, ODataProperty property)
		{
			object obj = null;
			ODataEnumValue odataEnumValue = property.Value as ODataEnumValue;
			this.MaterializeODataEnumValue(valueType, odataEnumValue.TypeName, odataEnumValue.Value, () => "TODO: Is this reachable?", out obj);
			if (!property.HasMaterializedValue())
			{
				property.SetMaterializedValue(obj);
			}
			return obj;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00027104 File Offset: 0x00025304
		public object MaterializeEnumDataValueCollectionElement(Type collectionItemType, string wireTypeName, string item)
		{
			object obj = null;
			this.MaterializeODataEnumValue(collectionItemType, wireTypeName, item, () => Strings.Collection_NullCollectionItemsNotSupported, out obj);
			return obj;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00027140 File Offset: 0x00025340
		internal static object MaterializeODataEnumValue(Type enumType, ODataEnumValue enumValue)
		{
			object obj;
			if (enumValue == null)
			{
				obj = null;
			}
			else
			{
				string text = enumValue.Value.Trim();
				Type type = Nullable.GetUnderlyingType(enumType) ?? enumType;
				if (!Enum.IsDefined(type, text))
				{
					obj = Enum.Parse(type, ClientTypeUtil.GetClientFieldName(type, text), false);
				}
				else
				{
					obj = Enum.Parse(type, text, false);
				}
			}
			return obj;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00027190 File Offset: 0x00025390
		private void MaterializeODataEnumValue(Type type, string wireTypeName, string enumValueStr, Func<string> throwOnNullMessage, out object materializedValue)
		{
			Type type2 = Nullable.GetUnderlyingType(type) ?? type;
			ClientTypeAnnotation clientTypeAnnotation = this.context.ResolveTypeForMaterialization(type2, wireTypeName);
			Type elementType = clientTypeAnnotation.ElementType;
			enumValueStr = enumValueStr.Trim();
			if (!Enum.IsDefined(elementType, enumValueStr))
			{
				materializedValue = Enum.Parse(elementType, ClientTypeUtil.GetClientFieldName(elementType, enumValueStr), false);
				return;
			}
			materializedValue = Enum.Parse(elementType, enumValueStr, false);
		}

		// Token: 0x04000605 RID: 1541
		private readonly IODataMaterializerContext context;
	}
}
