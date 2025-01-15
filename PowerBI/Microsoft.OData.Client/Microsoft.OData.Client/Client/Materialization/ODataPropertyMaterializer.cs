using System;
using System.Collections.Generic;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x0200010C RID: 268
	internal sealed class ODataPropertyMaterializer : ODataMessageReaderMaterializer
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x0002975F File Offset: 0x0002795F
		public ODataPropertyMaterializer(ODataMessageReader reader, IODataMaterializerContext materializerContext, Type expectedType, bool? singleResult)
			: base(reader, materializerContext, expectedType, singleResult)
		{
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002B338 File Offset: 0x00029538
		internal override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002B340 File Offset: 0x00029540
		protected override void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType)
		{
			ODataProperty odataProperty = this.messageReader.ReadProperty(expectedReaderType);
			Type type = Nullable.GetUnderlyingType(base.ExpectedType) ?? base.ExpectedType;
			if (expectedClientType.IsCollection())
			{
				Type type2 = base.ExpectedType;
				Type type3 = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>));
				object obj;
				if (type3 != null)
				{
					type2 = type3.GetGenericArguments()[0];
					obj = base.CollectionValueMaterializationPolicy.CreateCollectionPropertyInstance(odataProperty, type);
				}
				else
				{
					type3 = typeof(ICollection<>).MakeGenericType(new Type[] { type2 });
					obj = base.CollectionValueMaterializationPolicy.CreateCollectionPropertyInstance(odataProperty, type3);
				}
				bool isNullable = expectedClientType.AsCollection().ElementType().IsNullable;
				base.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(odataProperty, obj, type2, ClientTypeUtil.GetAddToCollectionDelegate(type3), isNullable);
				this.currentValue = obj;
				return;
			}
			if (expectedClientType.IsEnum())
			{
				this.currentValue = base.EnumValueMaterializationPolicy.MaterializeEnumTypeProperty(type, odataProperty);
				return;
			}
			object obj2 = odataProperty.Value;
			ODataUntypedValue odataUntypedValue = obj2 as ODataUntypedValue;
			if (odataUntypedValue != null && base.MaterializerContext.UndeclaredPropertyBehavior == UndeclaredPropertyBehavior.Support)
			{
				obj2 = CommonUtil.ParseJsonToPrimitiveValue(odataUntypedValue.RawValue);
			}
			this.currentValue = base.PrimitivePropertyConverter.ConvertPrimitiveValue(obj2, base.ExpectedType);
		}

		// Token: 0x0400063F RID: 1599
		private object currentValue;
	}
}
