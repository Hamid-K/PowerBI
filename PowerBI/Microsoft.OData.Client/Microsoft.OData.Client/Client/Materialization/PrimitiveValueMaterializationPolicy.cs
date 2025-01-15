using System;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000FF RID: 255
	internal class PrimitiveValueMaterializationPolicy
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x00028D60 File Offset: 0x00026F60
		internal PrimitiveValueMaterializationPolicy(IODataMaterializerContext context, SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter)
		{
			this.context = context;
			this.lazyPrimitivePropertyConverter = lazyPrimitivePropertyConverter;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00028D76 File Offset: 0x00026F76
		private PrimitivePropertyConverter PrimitivePropertyConverter
		{
			get
			{
				return this.lazyPrimitivePropertyConverter.Value;
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00028D84 File Offset: 0x00026F84
		public object MaterializePrimitiveDataValue(Type collectionItemType, string wireTypeName, object item)
		{
			object obj = null;
			this.MaterializePrimitiveDataValue(collectionItemType, wireTypeName, item, () => "TODO: Is this reachable?", out obj);
			return obj;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00028DC0 File Offset: 0x00026FC0
		public object MaterializePrimitiveDataValueCollectionElement(Type collectionItemType, string wireTypeName, object item)
		{
			object obj = null;
			this.MaterializePrimitiveDataValue(collectionItemType, wireTypeName, item, () => Strings.Collection_NullCollectionItemsNotSupported, out obj);
			return obj;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00028DFC File Offset: 0x00026FFC
		private void MaterializePrimitiveDataValue(Type type, string wireTypeName, object value, Func<string> throwOnNullMessage, out object materializedValue)
		{
			Type type2 = Nullable.GetUnderlyingType(type) ?? type;
			PrimitiveType primitiveType;
			bool flag = PrimitiveType.TryGetPrimitiveType(type2, out primitiveType);
			if (!flag)
			{
				ClientTypeAnnotation clientTypeAnnotation = this.context.ResolveTypeForMaterialization(type, wireTypeName);
				flag = PrimitiveType.TryGetPrimitiveType(clientTypeAnnotation.ElementType, out primitiveType);
			}
			if (!flag)
			{
				materializedValue = null;
				return;
			}
			if (value != null)
			{
				ODataUntypedValue odataUntypedValue = value as ODataUntypedValue;
				if (odataUntypedValue != null && this.context.UndeclaredPropertyBehavior == UndeclaredPropertyBehavior.Support)
				{
					value = CommonUtil.ParseJsonToPrimitiveValue(odataUntypedValue.RawValue);
				}
				materializedValue = this.PrimitivePropertyConverter.ConvertPrimitiveValue(value, type2);
				return;
			}
			if (!ClientTypeUtil.CanAssignNull(type))
			{
				throw new InvalidOperationException(throwOnNullMessage());
			}
			materializedValue = null;
		}

		// Token: 0x0400061A RID: 1562
		private readonly IODataMaterializerContext context;

		// Token: 0x0400061B RID: 1563
		private readonly SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter;
	}
}
