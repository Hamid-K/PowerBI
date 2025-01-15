using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000140 RID: 320
	internal sealed class JsonLightAnnotationGroupDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0001B297 File Offset: 0x00019497
		internal JsonLightAnnotationGroupDeserializer(ODataJsonLightInputContext inputContext)
			: base(inputContext)
		{
			this.annotationGroups = new Dictionary<string, ODataJsonLightAnnotationGroup>(EqualityComparer<string>.Default);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001B2B0 File Offset: 0x000194B0
		internal ODataJsonLightAnnotationGroup ReadAnnotationGroup(Func<string, object> readPropertyAnnotationValue, Func<string, DuplicatePropertyNamesChecker, object> readInstanceAnnotationValue)
		{
			string propertyName = base.JsonReader.GetPropertyName();
			if (string.CompareOrdinal(propertyName, "odata.annotationGroup") == 0)
			{
				base.JsonReader.ReadPropertyName();
				return this.ReadAnnotationGroupDeclaration(readPropertyAnnotationValue, readInstanceAnnotationValue);
			}
			if (string.CompareOrdinal(propertyName, "odata.annotationGroupReference") == 0)
			{
				base.JsonReader.ReadPropertyName();
				return this.ReadAnnotationGroupReference();
			}
			return null;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001B30C File Offset: 0x0001950C
		internal void AddAnnotationGroup(ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (this.annotationGroups.ContainsKey(annotationGroup.Name))
			{
				throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_MultipleAnnotationGroupsWithSameName(annotationGroup.Name));
			}
			this.annotationGroups.Add(annotationGroup.Name, annotationGroup);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001B344 File Offset: 0x00019544
		private static void VerifyAnnotationGroupNameNotYetFound(ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (!string.IsNullOrEmpty(annotationGroup.Name))
			{
				throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_EncounteredMultipleNameProperties);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001B35E File Offset: 0x0001955E
		private static bool IsAnnotationGroupName(string propertyName)
		{
			return string.CompareOrdinal(propertyName, "name") == 0;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B36E File Offset: 0x0001956E
		private static void VerifyAnnotationGroupNameFound(ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (string.IsNullOrEmpty(annotationGroup.Name))
			{
				throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_AnnotationGroupDeclarationWithoutName);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001B388 File Offset: 0x00019588
		private static void VerifyDataPropertyIsAnnotationName(string propertyName, ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (!JsonLightAnnotationGroupDeserializer.IsAnnotationGroupName(propertyName))
			{
				throw JsonLightAnnotationGroupDeserializer.CreateExceptionForInvalidAnnotationInsideAnnotationGroup(propertyName, annotationGroup);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001B39A File Offset: 0x0001959A
		private static ODataException CreateExceptionForInvalidAnnotationInsideAnnotationGroup(string propertyName, ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (string.IsNullOrEmpty(annotationGroup.Name))
			{
				return new ODataException(Strings.JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideAnnotationGroup(propertyName));
			}
			return new ODataException(Strings.JsonLightAnnotationGroupDeserializer_InvalidAnnotationFoundInsideNamedAnnotationGroup(annotationGroup.Name, propertyName));
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001B3C8 File Offset: 0x000195C8
		private ODataJsonLightAnnotationGroup ReadAnnotationGroupReference()
		{
			string text = base.JsonReader.ReadStringValue("odata.annotationGroupReference");
			ODataJsonLightAnnotationGroup odataJsonLightAnnotationGroup;
			if (this.annotationGroups.TryGetValue(text, ref odataJsonLightAnnotationGroup))
			{
				return odataJsonLightAnnotationGroup;
			}
			throw new ODataException(Strings.JsonLightAnnotationGroupDeserializer_UndefinedAnnotationGroupReference(text));
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001B540 File Offset: 0x00019740
		private ODataJsonLightAnnotationGroup ReadAnnotationGroupDeclaration(Func<string, object> readPropertyAnnotationValue, Func<string, DuplicatePropertyNamesChecker, object> readInstanceAnnotationValue)
		{
			ODataJsonLightAnnotationGroup annotationGroup = new ODataJsonLightAnnotationGroup
			{
				Annotations = new Dictionary<string, object>(EqualityComparer<string>.Default)
			};
			base.JsonReader.ReadStartObject();
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, readPropertyAnnotationValue, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						JsonLightAnnotationGroupDeserializer.VerifyDataPropertyIsAnnotationName(propertyName, annotationGroup);
						JsonLightAnnotationGroupDeserializer.VerifyAnnotationGroupNameNotYetFound(annotationGroup);
						annotationGroup.Name = this.JsonReader.ReadStringValue(propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
					{
						Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
						if (odataPropertyAnnotations == null)
						{
							return;
						}
						using (Dictionary<string, object>.Enumerator enumerator = odataPropertyAnnotations.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<string, object> keyValuePair = enumerator.Current;
								annotationGroup.Annotations.Add(propertyName + '@' + keyValuePair.Key, keyValuePair.Value);
							}
							return;
						}
						break;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw JsonLightAnnotationGroupDeserializer.CreateExceptionForInvalidAnnotationInsideAnnotationGroup(propertyName, annotationGroup);
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightAnnotationGroupDeserializer_ReadAnnotationGroupDeclaration));
					}
					annotationGroup.Annotations.Add(propertyName, readInstanceAnnotationValue.Invoke(propertyName, duplicatePropertyNamesChecker));
				});
			}
			JsonLightAnnotationGroupDeserializer.VerifyAnnotationGroupNameFound(annotationGroup);
			base.JsonReader.ReadEndObject();
			this.AddAnnotationGroup(annotationGroup);
			return annotationGroup;
		}

		// Token: 0x0400034B RID: 843
		private readonly Dictionary<string, ODataJsonLightAnnotationGroup> annotationGroups;
	}
}
