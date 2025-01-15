using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B8 RID: 440
	public class ODataActionPayloadDeserializer : ODataDeserializer
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x0003B8E2 File Offset: 0x00039AE2
		public ODataActionPayloadDeserializer(ODataDeserializerProvider deserializerProvider)
			: base(ODataPayloadKind.Parameter)
		{
			if (deserializerProvider == null)
			{
				throw Error.ArgumentNull("deserializerProvider");
			}
			this.DeserializerProvider = deserializerProvider;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0003B901 File Offset: 0x00039B01
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0003B909 File Offset: 0x00039B09
		public ODataDeserializerProvider DeserializerProvider { get; private set; }

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003B914 File Offset: 0x00039B14
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			IEdmAction action = ODataActionPayloadDeserializer.GetAction(readContext);
			Dictionary<string, object> dictionary;
			if (type == typeof(ODataActionParameters))
			{
				dictionary = new ODataActionParameters();
			}
			else
			{
				dictionary = new ODataUntypedActionParameters(action);
			}
			ODataParameterReader odataParameterReader = messageReader.CreateODataParameterReader(action);
			while (odataParameterReader.Read())
			{
				string parameterName = null;
				switch (odataParameterReader.State)
				{
				case ODataParameterReaderState.Value:
				{
					parameterName = odataParameterReader.Name;
					IEdmOperationParameter edmOperationParameter = action.Parameters.SingleOrDefault((IEdmOperationParameter p) => p.Name == parameterName);
					if (edmOperationParameter.Type.IsPrimitive())
					{
						dictionary[parameterName] = odataParameterReader.Value;
					}
					else
					{
						ODataEdmTypeDeserializer edmTypeDeserializer = this.DeserializerProvider.GetEdmTypeDeserializer(edmOperationParameter.Type);
						dictionary[parameterName] = edmTypeDeserializer.ReadInline(odataParameterReader.Value, edmOperationParameter.Type, readContext);
					}
					break;
				}
				case ODataParameterReaderState.Collection:
				{
					parameterName = odataParameterReader.Name;
					IEdmOperationParameter edmOperationParameter = action.Parameters.SingleOrDefault((IEdmOperationParameter p) => p.Name == parameterName);
					IEdmCollectionTypeReference edmCollectionTypeReference = edmOperationParameter.Type as IEdmCollectionTypeReference;
					ODataCollectionValue odataCollectionValue = ODataCollectionDeserializer.ReadCollection(odataParameterReader.CreateCollectionReader());
					ODataCollectionDeserializer odataCollectionDeserializer = (ODataCollectionDeserializer)this.DeserializerProvider.GetEdmTypeDeserializer(edmCollectionTypeReference);
					dictionary[parameterName] = odataCollectionDeserializer.ReadInline(odataCollectionValue, edmCollectionTypeReference, readContext);
					break;
				}
				case ODataParameterReaderState.Resource:
				{
					parameterName = odataParameterReader.Name;
					IEdmOperationParameter edmOperationParameter = action.Parameters.SingleOrDefault((IEdmOperationParameter p) => p.Name == parameterName);
					object obj = odataParameterReader.CreateResourceReader().ReadResourceOrResourceSet();
					ODataResourceDeserializer odataResourceDeserializer = (ODataResourceDeserializer)this.DeserializerProvider.GetEdmTypeDeserializer(edmOperationParameter.Type);
					dictionary[parameterName] = odataResourceDeserializer.ReadInline(obj, edmOperationParameter.Type, readContext);
					break;
				}
				case ODataParameterReaderState.ResourceSet:
				{
					parameterName = odataParameterReader.Name;
					IEdmOperationParameter edmOperationParameter = action.Parameters.SingleOrDefault((IEdmOperationParameter p) => p.Name == parameterName);
					IEdmCollectionTypeReference edmCollectionTypeReference2 = edmOperationParameter.Type as IEdmCollectionTypeReference;
					object obj2 = odataParameterReader.CreateResourceSetReader().ReadResourceOrResourceSet();
					object obj3 = ((ODataResourceSetDeserializer)this.DeserializerProvider.GetEdmTypeDeserializer(edmCollectionTypeReference2)).ReadInline(obj2, edmCollectionTypeReference2, readContext);
					IEdmTypeReference edmTypeReference = edmCollectionTypeReference2.ElementType();
					IEnumerable enumerable = obj3 as IEnumerable;
					if (enumerable != null)
					{
						if (readContext.IsUntyped)
						{
							dictionary[parameterName] = enumerable.ConvertToEdmObject(edmCollectionTypeReference2);
						}
						else
						{
							Type clrType = EdmLibHelpers.GetClrType(edmTypeReference, readContext.Model);
							IEnumerable enumerable2 = ODataActionPayloadDeserializer._castMethodInfo.MakeGenericMethod(new Type[] { clrType }).Invoke(null, new object[] { obj3 }) as IEnumerable;
							dictionary[parameterName] = enumerable2;
						}
					}
					break;
				}
				}
			}
			return dictionary;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003BBF0 File Offset: 0x00039DF0
		internal static IEdmAction GetAction(ODataDeserializerContext readContext)
		{
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			Microsoft.AspNet.OData.Routing.ODataPath path = readContext.Path;
			if (path == null || path.Segments.Count == 0)
			{
				throw new SerializationException(SRResources.ODataPathMissing);
			}
			IEdmAction edmAction = null;
			if (path.PathTemplate == "~/unboundaction")
			{
				OperationImportSegment operationImportSegment = path.Segments.Last<ODataPathSegment>() as OperationImportSegment;
				if (operationImportSegment != null)
				{
					IEdmActionImport edmActionImport = operationImportSegment.OperationImports.First<IEdmOperationImport>() as IEdmActionImport;
					if (edmActionImport != null)
					{
						edmAction = edmActionImport.Action;
					}
				}
			}
			else
			{
				OperationSegment operationSegment = path.Segments.Last<ODataPathSegment>() as OperationSegment;
				if (operationSegment != null)
				{
					edmAction = operationSegment.Operations.First<IEdmOperation>() as IEdmAction;
				}
			}
			if (edmAction == null)
			{
				throw new SerializationException(Error.Format(SRResources.RequestNotActionInvocation, new object[] { path.ToString() }));
			}
			return edmAction;
		}

		// Token: 0x0400040F RID: 1039
		private static readonly MethodInfo _castMethodInfo = typeof(Enumerable).GetMethod("Cast");
	}
}
