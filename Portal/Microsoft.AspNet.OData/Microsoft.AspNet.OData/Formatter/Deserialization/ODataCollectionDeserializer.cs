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
	// Token: 0x020001B9 RID: 441
	public class ODataCollectionDeserializer : ODataEdmTypeDeserializer
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		public ODataCollectionDeserializer(ODataDeserializerProvider deserializerProvider)
			: base(ODataPayloadKind.Collection, deserializerProvider)
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003BCE4 File Offset: 0x00039EE4
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			IEdmTypeReference edmType = readContext.GetEdmType(type);
			if (!edmType.IsCollection())
			{
				throw Error.Argument("type", SRResources.ArgumentMustBeOfType, new object[] { EdmTypeKind.Collection });
			}
			IEdmTypeReference edmTypeReference = edmType.AsCollection().ElementType();
			ODataCollectionReader odataCollectionReader = messageReader.CreateODataCollectionReader(edmTypeReference);
			return this.ReadInline(ODataCollectionDeserializer.ReadCollection(odataCollectionReader), edmType, readContext);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003BD50 File Offset: 0x00039F50
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
			if (!edmType.IsCollection())
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { edmType.ToTraceString() }));
			}
			IEdmCollectionTypeReference edmCollectionTypeReference = edmType.AsCollection();
			IEdmTypeReference edmTypeReference = edmCollectionTypeReference.ElementType();
			ODataCollectionValue odataCollectionValue = item as ODataCollectionValue;
			if (odataCollectionValue == null)
			{
				throw Error.Argument("item", SRResources.ArgumentMustBeOfType, new object[] { typeof(ODataCollectionValue).Name });
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			IEnumerable enumerable = this.ReadCollectionValue(odataCollectionValue, edmTypeReference, readContext);
			if (enumerable == null)
			{
				return null;
			}
			if (readContext.IsUntyped && edmTypeReference.IsEnum())
			{
				return enumerable.ConvertToEdmObject(edmCollectionTypeReference);
			}
			Type clrType = EdmLibHelpers.GetClrType(edmTypeReference, readContext.Model);
			return ODataCollectionDeserializer._castMethodInfo.MakeGenericMethod(new Type[] { clrType }).Invoke(null, new object[] { enumerable }) as IEnumerable;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003BE3B File Offset: 0x0003A03B
		public virtual IEnumerable ReadCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference elementType, ODataDeserializerContext readContext)
		{
			if (collectionValue == null)
			{
				throw Error.ArgumentNull("collectionValue");
			}
			if (elementType == null)
			{
				throw Error.ArgumentNull("elementType");
			}
			ODataEdmTypeDeserializer deserializer = base.DeserializerProvider.GetEdmTypeDeserializer(elementType);
			if (deserializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { elementType.FullName() }));
			}
			foreach (object obj in collectionValue.Items)
			{
				if (elementType.IsPrimitive())
				{
					yield return obj;
				}
				else
				{
					yield return deserializer.ReadInline(obj, elementType, readContext);
				}
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003BE60 File Offset: 0x0003A060
		internal static ODataCollectionValue ReadCollection(ODataCollectionReader reader)
		{
			ArrayList arrayList = new ArrayList();
			string text = null;
			while (reader.Read())
			{
				if (ODataCollectionReaderState.Value == reader.State)
				{
					arrayList.Add(reader.Item);
				}
				else if (ODataCollectionReaderState.CollectionStart == reader.State)
				{
					text = reader.Item.ToString();
				}
			}
			return new ODataCollectionValue
			{
				Items = arrayList.Cast<object>(),
				TypeName = text
			};
		}

		// Token: 0x04000411 RID: 1041
		private static readonly MethodInfo _castMethodInfo = typeof(Enumerable).GetMethod("Cast");
	}
}
