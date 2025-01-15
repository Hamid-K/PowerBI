using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Parsing;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x020000D2 RID: 210
	public class EdmxReader
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000089EC File Offset: 0x00006BEC
		private EdmxReader(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc)
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
			dictionary2.Add("Schema", new Action(this.ParseCsdlSchemaElement));
			this.dataServicesParserLookup = dictionary2;
			Dictionary<string, Action> dictionary3 = new Dictionary<string, Action>();
			dictionary3.Add("ConceptualModels", new Action(this.ParseConceptualModelsElement));
			this.runtimeParserLookup = dictionary3;
			Dictionary<string, Action> dictionary4 = new Dictionary<string, Action>();
			dictionary4.Add("Schema", new Action(this.ParseCsdlSchemaElement));
			this.conceptualModelsParserLookup = dictionary4;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00008AEC File Offset: 0x00006CEC
		public static bool TryParse(XmlReader reader, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmxReader edmxReader = new EdmxReader(reader, null);
			return edmxReader.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00008B10 File Offset: 0x00006D10
		public static bool TryParse(XmlReader reader, bool ignoreUnexpectedAttributesAndElements, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return new EdmxReader(reader, null)
			{
				ignoreUnexpectedAttributesAndElements = ignoreUnexpectedAttributesAndElements
			}.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00008B3C File Offset: 0x00006D3C
		public static IEdmModel Parse(XmlReader reader)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(reader, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00008B60 File Offset: 0x00006D60
		public static bool TryParse(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmxReader edmxReader = new EdmxReader(reader, getReferencedModelReaderFunc);
			return edmxReader.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00008B84 File Offset: 0x00006D84
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(references, "references");
			EdmxReader edmxReader = new EdmxReader(reader, null);
			return edmxReader.TryParse(references, out model, out errors);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public static bool TryParse(XmlReader reader, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmxReader edmxReader = new EdmxReader(reader, null);
			return edmxReader.TryParse(new IEdmModel[] { reference }, out model, out errors);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00008BDC File Offset: 0x00006DDC
		public static IEdmModel Parse(XmlReader reader, IEnumerable<IEdmModel> referencedModels)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(reader, referencedModels, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00008C00 File Offset: 0x00006E00
		public static IEdmModel Parse(XmlReader reader, IEdmModel referencedModel)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(reader, referencedModel, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00008C24 File Offset: 0x00006E24
		public static IEdmModel Parse(XmlReader reader, Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(reader, getReferencedModelReaderFunc, out edmModel, out enumerable))
			{
				throw new EdmParseException(enumerable);
			}
			return edmModel;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00008C48 File Offset: 0x00006E48
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, EdmxReaderSettings settings, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(references, "references");
			if (settings == null)
			{
				return EdmxReader.TryParse(reader, references, out model, out errors);
			}
			EdmxReader edmxReader = new EdmxReader(reader, settings.GetReferencedModelReaderFunc)
			{
				ignoreUnexpectedAttributesAndElements = settings.IgnoreUnexpectedAttributesAndElements
			};
			return edmxReader.TryParse(references, out model, out errors);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00008C94 File Offset: 0x00006E94
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

		// Token: 0x060003AF RID: 943 RVA: 0x00008CF8 File Offset: 0x00006EF8
		private bool TryParse(IEnumerable<IEdmModel> referencedModels, out IEdmModel model, out IEnumerable<EdmError> parsingErrors)
		{
			Version version;
			CsdlModel csdlModel;
			this.TryParseEdmxFileToCsdlModel(out version, out csdlModel);
			if (!this.HasIntolerableError())
			{
				List<CsdlModel> list = this.LoadAndParseReferencedEdmxFiles(version);
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

		// Token: 0x060003B0 RID: 944 RVA: 0x00008D90 File Offset: 0x00006F90
		private List<CsdlModel> LoadAndParseReferencedEdmxFiles(Version mainEdmxVersion)
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
				else if (edmReference.Uri == null || (!edmReference.Uri.EndsWith("/Org.OData.Core.V1.xml", 4) && !edmReference.Uri.EndsWith("/Org.OData.Capabilities.V1.xml", 4) && !edmReference.Uri.EndsWith("/OData.Community.Keys.V1.xml", 4)))
				{
					XmlReader xmlReader = this.getReferencedModelReaderFunc.Invoke(new Uri(edmReference.Uri, 0));
					if (xmlReader == null)
					{
						this.RaiseError(EdmErrorCode.UnresolvedReferenceUriInEdmxReference, Strings.EdmxParser_UnresolvedReferenceUriInEdmxReference);
					}
					else
					{
						EdmxReader edmxReader = new EdmxReader(xmlReader, null);
						edmxReader.source = edmReference.Uri;
						edmxReader.ignoreUnexpectedAttributesAndElements = this.ignoreUnexpectedAttributesAndElements;
						Version version;
						CsdlModel csdlModel;
						if (edmxReader.TryParseEdmxFileToCsdlModel(out version, out csdlModel))
						{
							if (!mainEdmxVersion.Equals(version))
							{
								this.errors.Add(null);
							}
							csdlModel.AddParentModelReferences(edmReference);
							list.Add(csdlModel);
						}
						this.errors.AddRange(edmxReader.errors);
					}
				}
			}
			return list;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00008F0C File Offset: 0x0000710C
		private bool TryParseEdmxFileToCsdlModel(out Version edmxVersion, out CsdlModel csdlModel)
		{
			edmxVersion = null;
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
				if (this.reader.LocalName != "Edmx" || !CsdlConstants.SupportedEdmxNamespaces.TryGetValue(this.reader.NamespaceURI, ref edmxVersion))
				{
					this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(this.reader.Name, "Edmx"));
					return false;
				}
				this.ParseEdmxElement(edmxVersion);
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

		// Token: 0x060003B2 RID: 946 RVA: 0x00009053 File Offset: 0x00007253
		private bool HasIntolerableError()
		{
			if (this.ignoreUnexpectedAttributesAndElements)
			{
				return Enumerable.Any<EdmError>(this.errors, (EdmError error) => error.ErrorCode != EdmErrorCode.UnexpectedXmlElement && error.ErrorCode != EdmErrorCode.UnexpectedXmlAttribute);
			}
			return Enumerable.Any<EdmError>(this.errors);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00009094 File Offset: 0x00007294
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
						this.ParseElement(this.reader.LocalName, EdmxReader.EmptyParserLookup);
					}
				}
				else if (!this.reader.Read())
				{
					break;
				}
			}
			this.reader.Read();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009144 File Offset: 0x00007344
		private void ParseEdmxElement(Version edmxVersion)
		{
			string attributeValue = this.GetAttributeValue(null, "Version");
			Version version;
			if (attributeValue != null && (!EdmxReader.TryParseVersion(attributeValue, out version) || version != edmxVersion))
			{
				this.RaiseError(EdmErrorCode.InvalidVersionNumber, Strings.EdmxParser_EdmxVersionMismatch);
			}
			this.ParseElement("Edmx", this.edmxParserLookup);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009194 File Offset: 0x00007394
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

		// Token: 0x060003B6 RID: 950 RVA: 0x00009235 File Offset: 0x00007435
		private void ParseRuntimeElement()
		{
			this.ParseTargetElement("Runtime", this.runtimeParserLookup);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009248 File Offset: 0x00007448
		private void ParseDataServicesElement()
		{
			this.ParseTargetElement("DataServices", this.dataServicesParserLookup);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000925B File Offset: 0x0000745B
		private void ParseTargetElement(string elementName, Dictionary<string, Action> elementParsers)
		{
			if (!this.targetParsed)
			{
				this.targetParsed = true;
			}
			else
			{
				this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.EdmxParser_BodyElement("DataServices"));
				elementParsers = EdmxReader.EmptyParserLookup;
			}
			this.ParseElement(elementName, elementParsers);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000928F File Offset: 0x0000748F
		private void ParseConceptualModelsElement()
		{
			this.ParseElement("ConceptualModels", this.conceptualModelsParserLookup);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000092A4 File Offset: 0x000074A4
		private void ParseReferenceElement()
		{
			EdmReference edmReference = new EdmReference(this.GetAttributeValue(null, "Uri"));
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

		// Token: 0x060003BB RID: 955 RVA: 0x00009474 File Offset: 0x00007674
		private void ParseCsdlSchemaElement()
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

		// Token: 0x060003BC RID: 956 RVA: 0x0000951C File Offset: 0x0000771C
		private void RaiseEmptyFile()
		{
			this.RaiseError(EdmErrorCode.EmptyFile, Strings.XmlParser_EmptySchemaTextReader);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000952C File Offset: 0x0000772C
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

		// Token: 0x060003BE RID: 958 RVA: 0x0000956F File Offset: 0x0000776F
		private void RaiseError(EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(this.Location(), errorCode, errorMessage));
		}

		// Token: 0x0400018D RID: 397
		private static readonly Dictionary<string, Action> EmptyParserLookup = new Dictionary<string, Action>();

		// Token: 0x0400018E RID: 398
		private readonly Dictionary<string, Action> edmxParserLookup;

		// Token: 0x0400018F RID: 399
		private readonly Dictionary<string, Action> runtimeParserLookup;

		// Token: 0x04000190 RID: 400
		private readonly Dictionary<string, Action> conceptualModelsParserLookup;

		// Token: 0x04000191 RID: 401
		private readonly Dictionary<string, Action> dataServicesParserLookup;

		// Token: 0x04000192 RID: 402
		private readonly XmlReader reader;

		// Token: 0x04000193 RID: 403
		private readonly List<EdmError> errors;

		// Token: 0x04000194 RID: 404
		private readonly List<IEdmReference> edmReferences;

		// Token: 0x04000195 RID: 405
		private readonly CsdlParser csdlParser;

		// Token: 0x04000196 RID: 406
		private readonly Func<Uri, XmlReader> getReferencedModelReaderFunc;

		// Token: 0x04000197 RID: 407
		private bool targetParsed;

		// Token: 0x04000198 RID: 408
		private bool ignoreUnexpectedAttributesAndElements;

		// Token: 0x04000199 RID: 409
		private string source;
	}
}
