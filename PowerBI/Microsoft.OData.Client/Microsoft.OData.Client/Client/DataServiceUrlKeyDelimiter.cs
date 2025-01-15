using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E5 RID: 229
	public sealed class DataServiceUrlKeyDelimiter
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x000206DE File Offset: 0x0001E8DE
		private DataServiceUrlKeyDelimiter(bool enableKeyAsSegment)
		{
			this.keySerializer = KeySerializer.Create(enableKeyAsSegment);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000206F2 File Offset: 0x0001E8F2
		public static DataServiceUrlKeyDelimiter Parentheses
		{
			get
			{
				return DataServiceUrlKeyDelimiter.parenthesesDelimiter;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000206F9 File Offset: 0x0001E8F9
		public static DataServiceUrlKeyDelimiter Slash
		{
			get
			{
				return DataServiceUrlKeyDelimiter.slashDelimiter;
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00020700 File Offset: 0x0001E900
		internal void AppendKeyExpression(IEdmStructuredValue entity, StringBuilder builder)
		{
			IEdmEntityTypeReference edmEntityTypeReference = entity.Type as IEdmEntityTypeReference;
			if (edmEntityTypeReference == null || !edmEntityTypeReference.Key().Any<IEdmStructuralProperty>())
			{
				throw Error.Argument(Strings.Content_EntityWithoutKey, "entity");
			}
			this.AppendKeyExpression<IEdmStructuralProperty>(edmEntityTypeReference.Key().ToList<IEdmStructuralProperty>(), (IEdmStructuralProperty p) => p.Name, (IEdmStructuralProperty p) => DataServiceUrlKeyDelimiter.GetPropertyValue(entity.FindPropertyValue(p.Name), entity.Type), builder);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00020788 File Offset: 0x0001E988
		internal void AppendKeyExpression<T>(ICollection<T> keyProperties, Func<T, string> getPropertyName, Func<T, object> getValueForProperty, StringBuilder builder)
		{
			Func<T, object> func = delegate(T p)
			{
				object obj = getValueForProperty(p);
				if (obj == null)
				{
					throw Error.InvalidOperation(Strings.Context_NullKeysAreNotSupported(getPropertyName(p)));
				}
				return obj;
			};
			this.keySerializer.AppendKeyExpression<T>(builder, keyProperties, getPropertyName, func);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000207CC File Offset: 0x0001E9CC
		private static object GetPropertyValue(IEdmPropertyValue property, IEdmTypeReference type)
		{
			IEdmValue value = property.Value;
			if (value.ValueKind == EdmValueKind.Null)
			{
				throw Error.InvalidOperation(Strings.Context_NullKeysAreNotSupported(property.Name));
			}
			IEdmPrimitiveValue edmPrimitiveValue = value as IEdmPrimitiveValue;
			if (edmPrimitiveValue == null)
			{
				throw Error.InvalidOperation(Strings.ClientType_KeysMustBeSimpleTypes(property.Name, type.FullName(), value.Type.FullName()));
			}
			return edmPrimitiveValue.ToClrValue();
		}

		// Token: 0x0400036C RID: 876
		private static readonly DataServiceUrlKeyDelimiter parenthesesDelimiter = new DataServiceUrlKeyDelimiter(false);

		// Token: 0x0400036D RID: 877
		private static readonly DataServiceUrlKeyDelimiter slashDelimiter = new DataServiceUrlKeyDelimiter(true);

		// Token: 0x0400036E RID: 878
		private readonly KeySerializer keySerializer;
	}
}
