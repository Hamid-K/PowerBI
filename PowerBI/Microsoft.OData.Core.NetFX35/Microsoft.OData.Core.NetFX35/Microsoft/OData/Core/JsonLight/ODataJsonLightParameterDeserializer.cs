using System;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000E6 RID: 230
	internal sealed class ODataJsonLightParameterDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x000202A3 File Offset: 0x0001E4A3
		internal ODataJsonLightParameterDeserializer(ODataJsonLightParameterReader parameterReader, ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.parameterReader = parameterReader;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002052C File Offset: 0x0001E72C
		internal bool ReadNextParameter(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			bool parameterRead = false;
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				bool foundCustomInstanceAnnotation = false;
				base.ProcessProperty(duplicatePropertyNamesChecker, ODataJsonLightParameterDeserializer.propertyAnnotationValueReader, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string parameterName)
				{
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
							obj = this.ReadNonEntityValue(null, edmPrimitiveTypeReference, null, null, true, false, false, parameterName, default(bool?));
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_0222;
						}
						case EdmTypeKind.Entity:
							obj = null;
							odataParameterReaderState = ODataParameterReaderState.Entry;
							goto IL_0222;
						case EdmTypeKind.Complex:
							obj = this.ReadNonEntityValue(null, parameterTypeReference, null, null, true, false, false, parameterName, default(bool?));
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_0222;
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
								goto IL_0222;
							}
							else
							{
								if (((IEdmCollectionType)parameterTypeReference.Definition).ElementType.TypeKind() == EdmTypeKind.Entity)
								{
									odataParameterReaderState = ODataParameterReaderState.Feed;
									goto IL_0222;
								}
								odataParameterReaderState = ODataParameterReaderState.Collection;
								goto IL_0222;
							}
							break;
						case EdmTypeKind.Enum:
						{
							IEdmEnumTypeReference edmEnumTypeReference = parameterTypeReference.AsEnum();
							obj = this.ReadNonEntityValue(null, edmEnumTypeReference, null, null, true, false, false, parameterName, default(bool?));
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_0222;
						}
						case EdmTypeKind.TypeDefinition:
						{
							IEdmTypeDefinitionReference edmTypeDefinitionReference = parameterTypeReference.AsTypeDefinition();
							obj = this.ReadNonEntityValue(null, edmTypeDefinitionReference, null, null, true, false, false, parameterName, default(bool?));
							odataParameterReaderState = ODataParameterReaderState.Value;
							goto IL_0222;
						}
						}
						throw new ODataException(Strings.ODataJsonLightParameterDeserializer_UnsupportedParameterTypeKind(parameterName, parameterTypeReference.TypeKind()));
						IL_0222:
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
					return this.ReadNextParameter(duplicatePropertyNamesChecker);
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

		// Token: 0x04000392 RID: 914
		private static readonly Func<string, object> propertyAnnotationValueReader = delegate(string annotationName)
		{
			throw new ODataException(Strings.ODataJsonLightParameterDeserializer_PropertyAnnotationForParameters);
		};

		// Token: 0x04000393 RID: 915
		private readonly ODataJsonLightParameterReader parameterReader;
	}
}
