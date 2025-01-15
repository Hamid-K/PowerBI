using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000248 RID: 584
	internal sealed class ODataJsonLightParameterDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060019E1 RID: 6625 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		internal ODataJsonLightParameterDeserializer(ODataJsonLightParameterReader parameterReader, ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.parameterReader = parameterReader;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		internal bool ReadNextParameter(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			bool parameterRead = false;
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				bool foundCustomInstanceAnnotation = false;
				base.ProcessProperty(propertyAndAnnotationCollector, ODataJsonLightParameterDeserializer.propertyAnnotationValueReader, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string parameterName)
				{
					if (this.JsonReader.NodeType == JsonNodeType.Property)
					{
						this.JsonReader.Read();
					}
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						IEdmTypeReference parameterTypeReference = this.parameterReader.GetParameterTypeReference(parameterName);
						object obj;
						ODataParameterReaderState odataParameterReaderState;
						switch (parameterTypeReference.TypeKind())
						{
						case EdmTypeKind.Primitive:
						{
							IEdmPrimitiveTypeReference edmPrimitiveTypeReference = parameterTypeReference.AsPrimitive();
							if (edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Stream)
							{
								throw new ODataException(Strings.ODataJsonLightParameterDeserializer_UnsupportedPrimitiveParameterType(parameterName, edmPrimitiveTypeReference.PrimitiveKind()));
							}
							obj = this.ReadNonEntityValue(null, edmPrimitiveTypeReference, null, null, true, false, false, parameterName, null);
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_021A;
						}
						case EdmTypeKind.Entity:
						case EdmTypeKind.Complex:
							obj = null;
							odataParameterReaderState = ODataParameterReaderState.Resource;
							goto IL_021A;
						case EdmTypeKind.Collection:
							obj = null;
							if (this.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
							{
								obj = this.JsonReader.ReadPrimitiveValue();
								if (obj != null)
								{
									throw new ODataException(Strings.ODataJsonLightParameterDeserializer_NullCollectionExpected(JsonNodeType.PrimitiveValue, obj));
								}
								odataParameterReaderState = ODataParameterReaderState.Value;
								goto IL_021A;
							}
							else
							{
								if (((IEdmCollectionType)parameterTypeReference.Definition).ElementType.IsStructured())
								{
									odataParameterReaderState = ODataParameterReaderState.ResourceSet;
									goto IL_021A;
								}
								odataParameterReaderState = ODataParameterReaderState.Collection;
								goto IL_021A;
							}
							break;
						case EdmTypeKind.Enum:
						{
							IEdmEnumTypeReference edmEnumTypeReference = parameterTypeReference.AsEnum();
							obj = this.ReadNonEntityValue(null, edmEnumTypeReference, null, null, true, false, false, parameterName, null);
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_021A;
						}
						case EdmTypeKind.TypeDefinition:
						{
							IEdmTypeDefinitionReference edmTypeDefinitionReference = parameterTypeReference.AsTypeDefinition();
							obj = this.ReadNonEntityValue(null, edmTypeDefinitionReference, null, null, true, false, false, parameterName, null);
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_021A;
						}
						}
						throw new ODataException(Strings.ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(parameterName, parameterTypeReference.TypeKind()));
						IL_021A:
						parameterRead = true;
						this.parameterReader.EnterScope(odataParameterReaderState, parameterName, obj);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightParameterDeserializer_PropertyAnnotationWithoutPropertyForParameters(parameterName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(parameterName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						foundCustomInstanceAnnotation = true;
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(parameterName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightParameterDeserializer_ReadNextParameter));
					}
				});
				if (foundCustomInstanceAnnotation)
				{
					return this.ReadNextParameter(propertyAndAnnotationCollector);
				}
			}
			if (!parameterRead && base.JsonReader.NodeType == JsonNodeType.EndObject)
			{
				base.JsonReader.ReadEndObject();
				base.ReadPayloadEnd(false);
				if (this.parameterReader.State != ODataParameterReaderState.Start)
				{
					this.parameterReader.PopScope(this.parameterReader.State);
				}
				this.parameterReader.PopScope(ODataParameterReaderState.Start);
				this.parameterReader.EnterScope(ODataParameterReaderState.Completed, null, null);
			}
			return parameterRead;
		}

		// Token: 0x04000B46 RID: 2886
		private static readonly Func<string, object> propertyAnnotationValueReader = delegate(string annotationName)
		{
			throw new ODataException(Strings.ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters);
		};

		// Token: 0x04000B47 RID: 2887
		private readonly ODataJsonLightParameterReader parameterReader;
	}
}
