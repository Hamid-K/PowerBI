using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Parsing;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000154 RID: 340
	public class CsdlReader
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x00016D6C File Offset: 0x00014F6C
		private CsdlReader(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			this.reader = reader;
			this.getReferencedModelReaderFunc = getReferencedModelReaderFunc;
			this.errors = new List<EdmError>();
			this.edmReferences = new List<IEdmReference>();
			this.csdlParser = new CsdlParser();
			this.edmxParserLookup = new Dictionary<string, Action>
			{
				{
					"DataServices",
					new Action(this.ParseDataServicesElement)
				},
				{
					"Reference",
					new Action(this.ParseReferenceElement)
				},
				{
					"Runtime",
					new Action(this.ParseRuntimeElement)
				}
			};
			this.dataServicesParserLookup = new Dictionary<string, Action> { 
			{
				"Schema",
				new Action(this.ParseSchemaElement)
			} };
			this.runtimeParserLookup = new Dictionary<string, Action> { 
			{
				"ConceptualModels",
				new Action(this.ParseConceptualModelsElement)
			} };
			this.conceptualModelsParserLookup = new Dictionary<string, Action> { 
			{
				"Schema",
				new Action(this.ParseSchemaElement)
			} };
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00016E64 File Offset: 0x00015064
		public static bool TryParse(XmlReader reader, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlReader csdlReader = new CsdlReader(reader, null);
			return csdlReader.TryParse(Enumerable.Empty<IEdmModel>(), true, out model, out errors);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00016E88 File Offset: 0x00015088
		public static bool TryParse(XmlReader reader, bool ignoreUnexpectedAttributesAndElements, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return new CsdlReader(reader, null)
			{
				ignoreUnexpectedAttributesAndElements = ignoreUnexpectedAttributesAndElements
			}.TryParse(Enumerable.Empty<IEdmModel>(), true, out model, out errors);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00016EB4 File Offset: 0x000150B4
		public static IEdmModel Parse(XmlReader reader)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!CsdlReader.TryParse(reader, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00016ED8 File Offset: 0x000150D8
		public static bool TryParse(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlReader csdlReader = new CsdlReader(reader, getReferencedModelReaderFunc);
			return csdlReader.TryParse(Enumerable.Empty<IEdmModel>(), true, out model, out errors);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00016EFB File Offset: 0x000150FB
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return CsdlReader.TryParse(reader, references, true, out model, out errors);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00016F08 File Offset: 0x00015108
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, bool includeDefaultVocabularies, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(references, "references");
			CsdlReader csdlReader = new CsdlReader(reader, null);
			return csdlReader.TryParse(references, includeDefaultVocabularies, out model, out errors);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00016F34 File Offset: 0x00015134
		public static bool TryParse(XmlReader reader, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlReader csdlReader = new CsdlReader(reader, null);
			return csdlReader.TryParse(new IEdmModel[] { reference }, true, out model, out errors);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00016F5C File Offset: 0x0001515C
		public static IEdmModel Parse(XmlReader reader, IEnumerable<IEdmModel> referencedModels)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!CsdlReader.TryParse(reader, referencedModels, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00016F80 File Offset: 0x00015180
		public static IEdmModel Parse(XmlReader reader, IEdmModel referencedModel)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!CsdlReader.TryParse(reader, referencedModel, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00016FA4 File Offset: 0x000151A4
		public static IEdmModel Parse(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!CsdlReader.TryParse(reader, getReferencedModelReaderFunc, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00016FC8 File Offset: 0x000151C8
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, CsdlReaderSettings settings, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(references, "references");
			if (settings == null)
			{
				return CsdlReader.TryParse(reader, references, out model, out errors);
			}
			CsdlReader csdlReader = new CsdlReader(reader, settings.GetReferencedModelReaderFunc)
			{
				ignoreUnexpectedAttributesAndElements = settings.IgnoreUnexpectedAttributesAndElements
			};
			return csdlReader.TryParse(references, true, out model, out errors);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00017014 File Offset: 0x00015214
		private static bool TryParseVersion(string input, out Version version)
		{
			version = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			input = input.Trim();
			string[] array = input.Split(new char[] { '.' });
			if (array.Length != 2)
			{
				return false;
			}
			int num;
			int num2;
			if (!int.TryParse(array[0], out num) || !int.TryParse(array[1], out num2))
			{
				return false;
			}
			version = new Version(num, num2);
			return true;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00017074 File Offset: 0x00015274
		private bool TryParse(IEnumerable<IEdmModel> referencedModels, bool includeDefaultVocabularies, out IEdmModel model, out IEnumerable<EdmError> parsingErrors)
		{
			Version version;
			CsdlModel csdlModel;
			this.TryParseCsdlFileToCsdlModel(out version, out csdlModel);
			if (!this.HasIntolerableError())
			{
				List<CsdlModel> list = this.LoadAndParseReferencedCsdlFiles(version);
				IEnumerable<EdmError> enumerable;
				this.csdlParser.GetResult(out csdlModel, out enumerable);
				if (enumerable != null)
				{
					this.errors.AddRange(enumerable.Except(this.errors));
				}
				if (!this.HasIntolerableError())
				{
					CsdlSemanticsModel csdlSemanticsModel = new CsdlSemanticsModel(csdlModel, new CsdlSemanticsDirectValueAnnotationsManager(), list, includeDefaultVocabularies);
					csdlSemanticsModel.AddToReferencedModels(referencedModels);
					model = csdlSemanticsModel;
					model.SetEdmxVersion(version);
					Version version2;
					if (CsdlConstants.EdmxToEdmVersions.TryGetValue(version, out version2))
					{
						model.SetEdmVersion(version2);
					}
				}
				else
				{
					model = null;
				}
			}
			else
			{
				model = null;
			}
			parsingErrors = this.errors;
			return !this.HasIntolerableError();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00017128 File Offset: 0x00015328
		private List<CsdlModel> LoadAndParseReferencedCsdlFiles(Version mainCsdlVersion)
		{
			List<CsdlModel> list = new List<CsdlModel>();
			if (this.getReferencedModelReaderFunc == null)
			{
				return list;
			}
			foreach (IEdmReference edmReference in this.edmReferences)
			{
				if (!edmReference.Includes.Any<IEdmInclude>() && !edmReference.IncludeAnnotations.Any<IEdmIncludeAnnotations>())
				{
					this.RaiseError(EdmErrorCode.ReferenceElementMustContainAtLeastOneIncludeOrIncludeAnnotationsElement, Strings.EdmxParser_InvalidReferenceIncorrectNumberOfIncludes);
				}
				else if (!(edmReference.Uri != null) || (!edmReference.Uri.OriginalString.EndsWith("/Org.OData.Core.V1.xml", StringComparison.Ordinal) && !edmReference.Uri.OriginalString.EndsWith("/Org.OData.Capabilities.V1.xml", StringComparison.Ordinal) && !edmReference.Uri.OriginalString.EndsWith("/Org.OData.Authorization.V1.xml", StringComparison.Ordinal) && !edmReference.Uri.OriginalString.EndsWith("/Org.OData.Validation.V1.xml", StringComparison.Ordinal) && !edmReference.Uri.OriginalString.EndsWith("/Org.OData.Community.V1.xml", StringComparison.Ordinal) && !edmReference.Uri.OriginalString.EndsWith("/OData.Community.Keys.V1.xml", StringComparison.Ordinal)))
				{
					XmlReader xmlReader = this.getReferencedModelReaderFunc(edmReference.Uri);
					if (xmlReader == null)
					{
						this.RaiseError(EdmErrorCode.UnresolvedReferenceUriInEdmxReference, Strings.EdmxParser_UnresolvedReferenceUriInEdmxReference);
					}
					else
					{
						CsdlReader csdlReader = new CsdlReader(xmlReader, null);
						csdlReader.source = ((edmReference.Uri != null) ? edmReference.Uri.OriginalString : null);
						csdlReader.ignoreUnexpectedAttributesAndElements = this.ignoreUnexpectedAttributesAndElements;
						Version version;
						CsdlModel csdlModel;
						if (csdlReader.TryParseCsdlFileToCsdlModel(out version, out csdlModel))
						{
							if (!mainCsdlVersion.Equals(version))
							{
								this.errors.Add(null);
							}
							csdlModel.AddParentModelReferences(edmReference);
							list.Add(csdlModel);
						}
						this.errors.AddRange(csdlReader.errors);
					}
				}
			}
			return list;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00017320 File Offset: 0x00015520
		private bool TryParseCsdlFileToCsdlModel(out Version csdlVersion, out CsdlModel csdlModel)
		{
			csdlVersion = null;
			csdlModel = null;
			try
			{
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					while (this.reader.Read() && this.reader.NodeType != XmlNodeType.Element)
					{
					}
				}
				if (this.reader.EOF)
				{
					this.RaiseEmptyFile();
					return false;
				}
				if (this.reader.LocalName != "Edmx" || !CsdlConstants.SupportedEdmxNamespaces.TryGetValue(this.reader.NamespaceURI, out csdlVersion))
				{
					this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(this.reader.Name, "Edmx"));
					return false;
				}
				csdlVersion = this.ParseEdmxElement(csdlVersion);
				IEnumerable<EdmError> enumerable;
				if (!this.csdlParser.GetResult(out csdlModel, out enumerable))
				{
					this.errors.AddRange(enumerable);
					if (this.HasIntolerableError())
					{
						return false;
					}
				}
			}
			catch (XmlException ex)
			{
				this.errors.Add(new EdmError(new CsdlLocation(this.source, ex.LineNumber, ex.LinePosition), EdmErrorCode.XmlError, ex.Message));
				return false;
			}
			csdlModel.AddCurrentModelReferences(this.edmReferences);
			return true;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00017450 File Offset: 0x00015650
		private bool HasIntolerableError()
		{
			if (this.ignoreUnexpectedAttributesAndElements)
			{
				return this.errors.Any((EdmError error) => error.ErrorCode != EdmErrorCode.UnexpectedXmlElement && error.ErrorCode != EdmErrorCode.UnexpectedXmlAttribute);
			}
			return this.errors.Any<EdmError>();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00017490 File Offset: 0x00015690
		private void ParseElement(string elementName, Dictionary<string, Action> elementParsers)
		{
			if (this.reader.IsEmptyElement)
			{
				this.reader.Read();
				return;
			}
			this.reader.Read();
			while (this.reader.NodeType != XmlNodeType.EndElement)
			{
				if (this.reader.NodeType == XmlNodeType.Element)
				{
					if (elementParsers.ContainsKey(this.reader.LocalName))
					{
						elementParsers[this.reader.LocalName]();
					}
					else
					{
						this.ParseElement(this.reader.LocalName, CsdlReader.EmptyParserLookup);
					}
				}
				else if (!this.reader.Read())
				{
					break;
				}
			}
			this.reader.Read();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00017540 File Offset: 0x00015740
		private Version ParseEdmxElement(Version edmxVersion)
		{
			string attributeValue = this.GetAttributeValue(null, "Version");
			Version version = null;
			if (attributeValue != null && (!CsdlReader.TryParseVersion(attributeValue, out version) || version.Major != edmxVersion.Major))
			{
				this.RaiseError(EdmErrorCode.InvalidVersionNumber, Strings.EdmxParser_EdmxVersionMismatch);
			}
			this.ParseElement("Edmx", this.edmxParserLookup);
			return version ?? edmxVersion;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001759C File Offset: 0x0001579C
		private string GetAttributeValue(string namespaceUri, string localName)
		{
			string namespaceURI = this.reader.NamespaceURI;
			string text = null;
			bool flag = this.reader.MoveToFirstAttribute();
			while (flag)
			{
				if (((namespaceUri != null && this.reader.NamespaceURI == namespaceUri) || string.IsNullOrEmpty(this.reader.NamespaceURI) || this.reader.NamespaceURI == namespaceURI) && this.reader.LocalName == localName)
				{
					text = this.reader.Value;
					break;
				}
				flag = this.reader.MoveToNextAttribute();
			}
			this.reader.MoveToElement();
			return text;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001763D File Offset: 0x0001583D
		private void ParseRuntimeElement()
		{
			this.ParseTargetElement("Runtime", this.runtimeParserLookup);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00017650 File Offset: 0x00015850
		private void ParseDataServicesElement()
		{
			this.ParseTargetElement("DataServices", this.dataServicesParserLookup);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00017663 File Offset: 0x00015863
		private void ParseTargetElement(string elementName, Dictionary<string, Action> elementParsers)
		{
			if (!this.targetParsed)
			{
				this.targetParsed = true;
			}
			else
			{
				this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.EdmxParser_BodyElement("DataServices"));
				elementParsers = CsdlReader.EmptyParserLookup;
			}
			this.ParseElement(elementName, elementParsers);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00017697 File Offset: 0x00015897
		private void ParseConceptualModelsElement()
		{
			this.ParseElement("ConceptualModels", this.conceptualModelsParserLookup);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000176AC File Offset: 0x000158AC
		private void ParseReferenceElement()
		{
			EdmReference edmReference = new EdmReference(new Uri(this.GetAttributeValue(null, "Uri"), UriKind.RelativeOrAbsolute));
			if (this.reader.IsEmptyElement)
			{
				this.reader.Read();
				this.edmReferences.Add(edmReference);
				return;
			}
			this.reader.Read();
			while (this.reader.NodeType != XmlNodeType.EndElement)
			{
				while (this.reader.NodeType == XmlNodeType.Whitespace && this.reader.Read())
				{
				}
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					break;
				}
				if (this.reader.LocalName == "Include")
				{
					IEdmInclude edmInclude = new EdmInclude(this.GetAttributeValue(null, "Alias"), this.GetAttributeValue(null, "Namespace"));
					edmReference.AddInclude(edmInclude);
				}
				else if (this.reader.LocalName == "IncludeAnnotations")
				{
					IEdmIncludeAnnotations edmIncludeAnnotations = new EdmIncludeAnnotations(this.GetAttributeValue(null, "TermNamespace"), this.GetAttributeValue(null, "Qualifier"), this.GetAttributeValue(null, "TargetNamespace"));
					edmReference.AddIncludeAnnotations(edmIncludeAnnotations);
				}
				else
				{
					if (this.reader.LocalName == "Annotation")
					{
						this.reader.Skip();
						this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedElement(this.reader.LocalName));
						continue;
					}
					this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedElement(this.reader.LocalName));
				}
				if (!this.reader.IsEmptyElement)
				{
					this.reader.Read();
					while (this.reader.NodeType == XmlNodeType.Whitespace && this.reader.Read())
					{
					}
				}
				this.reader.Read();
			}
			this.reader.Read();
			this.edmReferences.Add(edmReference);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00017880 File Offset: 0x00015A80
		private void ParseSchemaElement()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				xmlReaderSettings.LineNumberOffset = xmlLineInfo.LineNumber - 1;
				xmlReaderSettings.LinePositionOffset = xmlLineInfo.LinePosition - 2;
			}
			using (StringReader stringReader = new StringReader(this.reader.ReadOuterXml()))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					this.csdlParser.AddReader(xmlReader, this.source);
				}
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00017924 File Offset: 0x00015B24
		private void RaiseEmptyFile()
		{
			this.RaiseError(EdmErrorCode.EmptyFile, Strings.XmlParser_EmptySchemaTextReader);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00017934 File Offset: 0x00015B34
		private CsdlLocation Location()
		{
			int num = 0;
			int num2 = 0;
			IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				num = xmlLineInfo.LineNumber;
				num2 = xmlLineInfo.LinePosition;
			}
			return new CsdlLocation(this.source, num, num2);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00017977 File Offset: 0x00015B77
		private void RaiseError(EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(this.Location(), errorCode, errorMessage));
		}

		// Token: 0x040004EE RID: 1262
		private static readonly Dictionary<string, Action> EmptyParserLookup = new Dictionary<string, Action>();

		// Token: 0x040004EF RID: 1263
		private readonly Dictionary<string, Action> edmxParserLookup;

		// Token: 0x040004F0 RID: 1264
		private readonly Dictionary<string, Action> runtimeParserLookup;

		// Token: 0x040004F1 RID: 1265
		private readonly Dictionary<string, Action> conceptualModelsParserLookup;

		// Token: 0x040004F2 RID: 1266
		private readonly Dictionary<string, Action> dataServicesParserLookup;

		// Token: 0x040004F3 RID: 1267
		private readonly XmlReader reader;

		// Token: 0x040004F4 RID: 1268
		private readonly List<EdmError> errors;

		// Token: 0x040004F5 RID: 1269
		private readonly List<IEdmReference> edmReferences;

		// Token: 0x040004F6 RID: 1270
		private readonly CsdlParser csdlParser;

		// Token: 0x040004F7 RID: 1271
		private readonly Func<Uri, XmlReader> getReferencedModelReaderFunc;

		// Token: 0x040004F8 RID: 1272
		private bool targetParsed;

		// Token: 0x040004F9 RID: 1273
		private bool ignoreUnexpectedAttributesAndElements;

		// Token: 0x040004FA RID: 1274
		private string source;
	}
}
