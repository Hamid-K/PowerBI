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
	// Token: 0x02000147 RID: 327
	public class CsdlReader
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x000151E0 File Offset: 0x000133E0
		private CsdlReader(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			this.reader = reader;
			this.getReferencedModelReaderFunc = getReferencedModelReaderFunc;
			this.errors = new List<EdmError>();
			this.edmReferences = new List<IEdmReference>();
			this.csdlParser = new CsdlParser();
			Dictionary<string, Action> dictionary = new Dictionary<string, Action>();
			dictionary.Add("DataServices", new Action(this.ParseDataServicesElement));
			dictionary.Add("Reference", new Action(this.ParseReferenceElement));
			dictionary.Add("Runtime", new Action(this.ParseRuntimeElement));
			this.edmxParserLookup = dictionary;
			Dictionary<string, Action> dictionary2 = new Dictionary<string, Action>();
			dictionary2.Add("Schema", new Action(this.ParseSchemaElement));
			this.dataServicesParserLookup = dictionary2;
			Dictionary<string, Action> dictionary3 = new Dictionary<string, Action>();
			dictionary3.Add("ConceptualModels", new Action(this.ParseConceptualModelsElement));
			this.runtimeParserLookup = dictionary3;
			Dictionary<string, Action> dictionary4 = new Dictionary<string, Action>();
			dictionary4.Add("Schema", new Action(this.ParseSchemaElement));
			this.conceptualModelsParserLookup = dictionary4;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000152D8 File Offset: 0x000134D8
		public static bool TryParse(XmlReader reader, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlReader csdlReader = new CsdlReader(reader, null);
			return csdlReader.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000152FC File Offset: 0x000134FC
		public static bool TryParse(XmlReader reader, bool ignoreUnexpectedAttributesAndElements, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return new CsdlReader(reader, null)
			{
				ignoreUnexpectedAttributesAndElements = ignoreUnexpectedAttributesAndElements
			}.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00015328 File Offset: 0x00013528
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

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001534C File Offset: 0x0001354C
		public static bool TryParse(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlReader csdlReader = new CsdlReader(reader, getReferencedModelReaderFunc);
			return csdlReader.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00015370 File Offset: 0x00013570
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(references, "references");
			CsdlReader csdlReader = new CsdlReader(reader, null);
			return csdlReader.TryParse(references, out model, out errors);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001539C File Offset: 0x0001359C
		public static bool TryParse(XmlReader reader, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlReader csdlReader = new CsdlReader(reader, null);
			return csdlReader.TryParse(new IEdmModel[] { reference }, out model, out errors);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000153C4 File Offset: 0x000135C4
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

		// Token: 0x060007FC RID: 2044 RVA: 0x000153E8 File Offset: 0x000135E8
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

		// Token: 0x060007FD RID: 2045 RVA: 0x0001540C File Offset: 0x0001360C
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

		// Token: 0x060007FE RID: 2046 RVA: 0x00015430 File Offset: 0x00013630
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
			return csdlReader.TryParse(references, out model, out errors);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001547C File Offset: 0x0001367C
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
			if (!int.TryParse(array[0], ref num) || !int.TryParse(array[1], ref num2))
			{
				return false;
			}
			version = new Version(num, num2);
			return true;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000154DC File Offset: 0x000136DC
		private bool TryParse(IEnumerable<IEdmModel> referencedModels, out IEdmModel model, out IEnumerable<EdmError> parsingErrors)
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
					this.errors.AddRange(Enumerable.Except<EdmError>(enumerable, this.errors));
				}
				if (!this.HasIntolerableError())
				{
					CsdlSemanticsModel csdlSemanticsModel = new CsdlSemanticsModel(csdlModel, new CsdlSemanticsDirectValueAnnotationsManager(), list);
					csdlSemanticsModel.AddToReferencedModels(referencedModels);
					model = csdlSemanticsModel;
					model.SetEdmxVersion(version);
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

		// Token: 0x06000801 RID: 2049 RVA: 0x00015574 File Offset: 0x00013774
		private List<CsdlModel> LoadAndParseReferencedCsdlFiles(Version mainCsdlVersion)
		{
			List<CsdlModel> list = new List<CsdlModel>();
			if (this.getReferencedModelReaderFunc == null)
			{
				return list;
			}
			foreach (IEdmReference edmReference in this.edmReferences)
			{
				if (!Enumerable.Any<IEdmInclude>(edmReference.Includes) && !Enumerable.Any<IEdmIncludeAnnotations>(edmReference.IncludeAnnotations))
				{
					this.RaiseError(EdmErrorCode.ReferenceElementMustContainAtLeastOneIncludeOrIncludeAnnotationsElement, Strings.EdmxParser_InvalidReferenceIncorrectNumberOfIncludes);
				}
				else if (!(edmReference.Uri != null) || (!edmReference.Uri.OriginalString.EndsWith("/Org.OData.Core.V1.xml", 4) && !edmReference.Uri.OriginalString.EndsWith("/Org.OData.Capabilities.V1.xml", 4) && !edmReference.Uri.OriginalString.EndsWith("/OData.Community.Keys.V1.xml", 4)))
				{
					XmlReader xmlReader = this.getReferencedModelReaderFunc.Invoke(edmReference.Uri);
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

		// Token: 0x06000802 RID: 2050 RVA: 0x00015718 File Offset: 0x00013918
		private bool TryParseCsdlFileToCsdlModel(out Version csdlVersion, out CsdlModel csdlModel)
		{
			csdlVersion = null;
			csdlModel = null;
			try
			{
				if (this.reader.NodeType != 1)
				{
					while (this.reader.Read() && this.reader.NodeType != 1)
					{
					}
				}
				if (this.reader.EOF)
				{
					this.RaiseEmptyFile();
					return false;
				}
				if (this.reader.LocalName != "Edmx" || !CsdlConstants.SupportedEdmxNamespaces.TryGetValue(this.reader.NamespaceURI, ref csdlVersion))
				{
					this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(this.reader.Name, "Edmx"));
					return false;
				}
				this.ParseEdmxElement(csdlVersion);
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

		// Token: 0x06000803 RID: 2051 RVA: 0x00015844 File Offset: 0x00013A44
		private bool HasIntolerableError()
		{
			if (this.ignoreUnexpectedAttributesAndElements)
			{
				return Enumerable.Any<EdmError>(this.errors, (EdmError error) => error.ErrorCode != EdmErrorCode.UnexpectedXmlElement && error.ErrorCode != EdmErrorCode.UnexpectedXmlAttribute);
			}
			return Enumerable.Any<EdmError>(this.errors);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00015884 File Offset: 0x00013A84
		private void ParseElement(string elementName, Dictionary<string, Action> elementParsers)
		{
			if (this.reader.IsEmptyElement)
			{
				this.reader.Read();
				return;
			}
			this.reader.Read();
			while (this.reader.NodeType != 15)
			{
				if (this.reader.NodeType == 1)
				{
					if (elementParsers.ContainsKey(this.reader.LocalName))
					{
						elementParsers[this.reader.LocalName].Invoke();
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

		// Token: 0x06000805 RID: 2053 RVA: 0x00015934 File Offset: 0x00013B34
		private void ParseEdmxElement(Version edmxVersion)
		{
			string attributeValue = this.GetAttributeValue(null, "Version");
			Version version;
			if (attributeValue != null && (!CsdlReader.TryParseVersion(attributeValue, out version) || version != edmxVersion))
			{
				this.RaiseError(EdmErrorCode.InvalidVersionNumber, Strings.EdmxParser_EdmxVersionMismatch);
			}
			this.ParseElement("Edmx", this.edmxParserLookup);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00015984 File Offset: 0x00013B84
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

		// Token: 0x06000807 RID: 2055 RVA: 0x00015A25 File Offset: 0x00013C25
		private void ParseRuntimeElement()
		{
			this.ParseTargetElement("Runtime", this.runtimeParserLookup);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00015A38 File Offset: 0x00013C38
		private void ParseDataServicesElement()
		{
			this.ParseTargetElement("DataServices", this.dataServicesParserLookup);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00015A4B File Offset: 0x00013C4B
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

		// Token: 0x0600080A RID: 2058 RVA: 0x00015A7F File Offset: 0x00013C7F
		private void ParseConceptualModelsElement()
		{
			this.ParseElement("ConceptualModels", this.conceptualModelsParserLookup);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00015A94 File Offset: 0x00013C94
		private void ParseReferenceElement()
		{
			EdmReference edmReference = new EdmReference(new Uri(this.GetAttributeValue(null, "Uri"), 0));
			if (this.reader.IsEmptyElement)
			{
				this.reader.Read();
				this.edmReferences.Add(edmReference);
				return;
			}
			this.reader.Read();
			while (this.reader.NodeType != 15)
			{
				while (this.reader.NodeType == 13 && this.reader.Read())
				{
				}
				if (this.reader.NodeType != 1)
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
					while (this.reader.NodeType == 13 && this.reader.Read())
					{
					}
				}
				this.reader.Read();
			}
			this.reader.Read();
			this.edmReferences.Add(edmReference);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00015C68 File Offset: 0x00013E68
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

		// Token: 0x0600080D RID: 2061 RVA: 0x00015D0C File Offset: 0x00013F0C
		private void RaiseEmptyFile()
		{
			this.RaiseError(EdmErrorCode.EmptyFile, Strings.XmlParser_EmptySchemaTextReader);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00015D1C File Offset: 0x00013F1C
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

		// Token: 0x0600080F RID: 2063 RVA: 0x00015D5F File Offset: 0x00013F5F
		private void RaiseError(EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(this.Location(), errorCode, errorMessage));
		}

		// Token: 0x04000535 RID: 1333
		private static readonly Dictionary<string, Action> EmptyParserLookup = new Dictionary<string, Action>();

		// Token: 0x04000536 RID: 1334
		private readonly Dictionary<string, Action> edmxParserLookup;

		// Token: 0x04000537 RID: 1335
		private readonly Dictionary<string, Action> runtimeParserLookup;

		// Token: 0x04000538 RID: 1336
		private readonly Dictionary<string, Action> conceptualModelsParserLookup;

		// Token: 0x04000539 RID: 1337
		private readonly Dictionary<string, Action> dataServicesParserLookup;

		// Token: 0x0400053A RID: 1338
		private readonly XmlReader reader;

		// Token: 0x0400053B RID: 1339
		private readonly List<EdmError> errors;

		// Token: 0x0400053C RID: 1340
		private readonly List<IEdmReference> edmReferences;

		// Token: 0x0400053D RID: 1341
		private readonly CsdlParser csdlParser;

		// Token: 0x0400053E RID: 1342
		private readonly Func<Uri, XmlReader> getReferencedModelReaderFunc;

		// Token: 0x0400053F RID: 1343
		private bool targetParsed;

		// Token: 0x04000540 RID: 1344
		private bool ignoreUnexpectedAttributesAndElements;

		// Token: 0x04000541 RID: 1345
		private string source;
	}
}
