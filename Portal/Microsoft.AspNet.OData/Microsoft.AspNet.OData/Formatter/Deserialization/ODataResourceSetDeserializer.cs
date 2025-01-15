using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001BC RID: 444
	public class ODataResourceSetDeserializer : ODataEdmTypeDeserializer
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x0003BF43 File Offset: 0x0003A143
		public ODataResourceSetDeserializer(ODataDeserializerProvider deserializerProvider)
			: base(ODataPayloadKind.ResourceSet, deserializerProvider)
		{
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003BF50 File Offset: 0x0003A150
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			IEdmTypeReference edmType = readContext.GetEdmType(type);
			if (!edmType.IsCollection() || !edmType.AsCollection().ElementType().IsStructured())
			{
				throw Error.Argument("edmType", SRResources.ArgumentMustBeOfType, new object[] { EdmTypeKind.Complex.ToString() + " or " + EdmTypeKind.Entity.ToString() });
			}
			object obj = messageReader.CreateODataResourceSetReader().ReadResourceOrResourceSet();
			return this.ReadInline(obj, edmType, readContext);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003BFE4 File Offset: 0x0003A1E4
		public sealed override object ReadInline(object item, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			if (item == null)
			{
				return null;
			}
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (!edmType.IsCollection() || !edmType.AsCollection().ElementType().IsStructured())
			{
				throw Error.Argument("edmType", SRResources.TypeMustBeResourceSet, new object[] { edmType.ToTraceString() });
			}
			ODataResourceSetWrapper odataResourceSetWrapper = item as ODataResourceSetWrapper;
			if (odataResourceSetWrapper == null)
			{
				throw Error.Argument("item", SRResources.ArgumentMustBeOfType, new object[] { typeof(ODataResourceSetWrapper).Name });
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			IEdmStructuredTypeReference edmStructuredTypeReference = edmType.AsCollection().ElementType().AsStructured();
			IEnumerable enumerable = this.ReadResourceSet(odataResourceSetWrapper, edmStructuredTypeReference, readContext);
			if (enumerable == null || !edmStructuredTypeReference.IsComplex())
			{
				return enumerable;
			}
			if (readContext.IsUntyped)
			{
				EdmComplexObjectCollection edmComplexObjectCollection = new EdmComplexObjectCollection(edmType.AsCollection());
				foreach (object obj in enumerable)
				{
					EdmComplexObject edmComplexObject = (EdmComplexObject)obj;
					edmComplexObjectCollection.Add(edmComplexObject);
				}
				return edmComplexObjectCollection;
			}
			Type clrType = EdmLibHelpers.GetClrType(edmStructuredTypeReference, readContext.Model);
			return ODataResourceSetDeserializer.CastMethodInfo.MakeGenericMethod(new Type[] { clrType }).Invoke(null, new object[] { enumerable }) as IEnumerable;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003C140 File Offset: 0x0003A340
		public virtual IEnumerable ReadResourceSet(ODataResourceSetWrapper resourceSet, IEdmStructuredTypeReference elementType, ODataDeserializerContext readContext)
		{
			ODataEdmTypeDeserializer deserializer = base.DeserializerProvider.GetEdmTypeDeserializer(elementType);
			if (deserializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { elementType.FullName() }));
			}
			foreach (ODataResourceWrapper odataResourceWrapper in resourceSet.Resources)
			{
				yield return deserializer.ReadInline(odataResourceWrapper, elementType, readContext);
			}
			IEnumerator<ODataResourceWrapper> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000414 RID: 1044
		private static readonly MethodInfo CastMethodInfo = typeof(Enumerable).GetMethod("Cast");
	}
}
