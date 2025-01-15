using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x02000027 RID: 39
	internal sealed class ClientEdmStructuredValue : IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600014E RID: 334 RVA: 0x000079C4 File Offset: 0x00005BC4
		public ClientEdmStructuredValue(object structuredValue, ClientEdmModel model, ClientTypeAnnotation clientTypeAnnotation)
		{
			if (clientTypeAnnotation.EdmType.TypeKind == EdmTypeKind.Complex)
			{
				this.Type = new EdmComplexTypeReference((IEdmComplexType)clientTypeAnnotation.EdmType, true);
			}
			else
			{
				this.Type = new EdmEntityTypeReference((IEdmEntityType)clientTypeAnnotation.EdmType, true);
			}
			this.structuredValue = structuredValue;
			this.typeAnnotation = clientTypeAnnotation;
			this.model = model;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007A2A File Offset: 0x00005C2A
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00007A32 File Offset: 0x00005C32
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007A3B File Offset: 0x00005C3B
		public EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007A3F File Offset: 0x00005C3F
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				return this.typeAnnotation.Properties().Select(new Func<ClientPropertyAnnotation, IEdmPropertyValue>(this.BuildEdmPropertyValue));
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007A60 File Offset: 0x00005C60
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			ClientPropertyAnnotation property = this.typeAnnotation.GetProperty(propertyName, UndeclaredPropertyBehavior.Support);
			if (property == null)
			{
				return null;
			}
			return this.BuildEdmPropertyValue(property);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007A88 File Offset: 0x00005C88
		private IEdmPropertyValue BuildEdmPropertyValue(ClientPropertyAnnotation propertyAnnotation)
		{
			object value = propertyAnnotation.GetValue(this.structuredValue);
			IEdmValue edmValue = this.ConvertToEdmValue(value, propertyAnnotation.EdmProperty.Type);
			return new EdmPropertyValue(propertyAnnotation.EdmProperty.Name, edmValue);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007AC8 File Offset: 0x00005CC8
		private IEdmValue ConvertToEdmValue(object propertyValue, IEdmTypeReference edmPropertyType)
		{
			if (propertyValue == null)
			{
				return EdmNullExpression.Instance;
			}
			if (edmPropertyType.IsStructured())
			{
				ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(propertyValue.GetType());
				if (clientTypeAnnotation != null && clientTypeAnnotation.EdmTypeReference.Definition.IsOrInheritsFrom(edmPropertyType.Definition))
				{
					return new ClientEdmStructuredValue(propertyValue, this.model, clientTypeAnnotation);
				}
				return new ClientEdmStructuredValue(propertyValue, this.model, this.model.GetClientTypeAnnotation(edmPropertyType.Definition));
			}
			else
			{
				if (edmPropertyType.IsCollection())
				{
					IEdmCollectionTypeReference collectionType = edmPropertyType as IEdmCollectionTypeReference;
					IEnumerable<IEdmValue> enumerable = from object v in (IEnumerable)propertyValue
						select this.ConvertToEdmValue(v, collectionType.ElementType());
					return new ClientEdmCollectionValue(collectionType, enumerable);
				}
				if (edmPropertyType.IsEnum())
				{
					return new EdmEnumValue(edmPropertyType as IEdmEnumTypeReference, new EdmEnumMemberValue(Convert.ToInt64(propertyValue, CultureInfo.InvariantCulture)));
				}
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = edmPropertyType as IEdmPrimitiveTypeReference;
				return EdmValueUtils.ConvertPrimitiveValue(propertyValue, edmPrimitiveTypeReference).Value;
			}
		}

		// Token: 0x0400006B RID: 107
		private readonly object structuredValue;

		// Token: 0x0400006C RID: 108
		private readonly ClientTypeAnnotation typeAnnotation;

		// Token: 0x0400006D RID: 109
		private readonly ClientEdmModel model;
	}
}
