using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics;
using Microsoft.Data.Edm.Csdl.Internal.Parsing;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl
{
	// Token: 0x020000AF RID: 175
	public class EdmxReader
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00007D54 File Offset: 0x00005F54
		private EdmxReader(XmlReader reader)
		{
			this.reader = reader;
			this.errors = new List<EdmError>();
			this.csdlParser = new CsdlParser();
			Dictionary<string, Action> dictionary = new Dictionary<string, Action>();
			dictionary.Add("DataServices", new Action(this.ParseDataServicesElement));
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

		// Token: 0x0600030D RID: 781 RVA: 0x00007E2C File Offset: 0x0000602C
		public static bool TryParse(XmlReader reader, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmxReader edmxReader = new EdmxReader(reader);
			return edmxReader.TryParse(Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00007E50 File Offset: 0x00006050
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

		// Token: 0x0600030F RID: 783 RVA: 0x00007E74 File Offset: 0x00006074
		public static bool TryParse(XmlReader reader, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmxReader edmxReader = new EdmxReader(reader);
			return edmxReader.TryParse(new IEdmModel[] { reference }, out model, out errors);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007E9C File Offset: 0x0000609C
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

		// Token: 0x06000311 RID: 785 RVA: 0x00007EC0 File Offset: 0x000060C0
		public static bool TryParse(XmlReader reader, IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			EdmxReader edmxReader = new EdmxReader(reader);
			return edmxReader.TryParse(references, out model, out errors);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00007EE0 File Offset: 0x000060E0
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

		// Token: 0x06000313 RID: 787 RVA: 0x00007F04 File Offset: 0x00006104
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

		// Token: 0x06000314 RID: 788 RVA: 0x00007F68 File Offset: 0x00006168
		private bool TryParse(IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> parsingErrors)
		{
			Version version;
			try
			{
				this.ParseEdmxFile(out version);
			}
			catch (XmlException ex)
			{
				model = null;
				parsingErrors = new EdmError[]
				{
					new EdmError(new CsdlLocation(ex.LineNumber, ex.LinePosition), EdmErrorCode.XmlError, ex.Message)
				};
				return false;
			}
			if (this.errors.Count == 0)
			{
				CsdlModel csdlModel;
				IEnumerable<EdmError> enumerable;
				if (this.csdlParser.GetResult(out csdlModel, out enumerable))
				{
					model = new CsdlSemanticsModel(csdlModel, new CsdlSemanticsDirectValueAnnotationsManager(), references);
					model.SetEdmxVersion(version);
					if (this.dataServiceVersion != null)
					{
						model.SetDataServiceVersion(this.dataServiceVersion);
					}
					if (this.maxDataServiceVersion != null)
					{
						model.SetMaxDataServiceVersion(this.maxDataServiceVersion);
					}
				}
				else
				{
					this.errors.AddRange(enumerable);
					model = null;
				}
			}
			else
			{
				model = null;
			}
			parsingErrors = this.errors;
			return this.errors.Count == 0;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008064 File Offset: 0x00006264
		private void ParseEdmxFile(out Version edmxVersion)
		{
			edmxVersion = null;
			if (this.reader.NodeType != 1)
			{
				while (this.reader.Read() && this.reader.NodeType != 1)
				{
				}
			}
			if (this.reader.EOF)
			{
				this.RaiseEmptyFile();
				return;
			}
			if (this.reader.LocalName != "Edmx" || !CsdlConstants.SupportedEdmxNamespaces.TryGetValue(this.reader.NamespaceURI, ref edmxVersion))
			{
				this.RaiseError(EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(this.reader.Name, "Edmx"));
				return;
			}
			this.ParseEdmxElement(edmxVersion);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008108 File Offset: 0x00006308
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

		// Token: 0x06000317 RID: 791 RVA: 0x000081B8 File Offset: 0x000063B8
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

		// Token: 0x06000318 RID: 792 RVA: 0x00008208 File Offset: 0x00006408
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

		// Token: 0x06000319 RID: 793 RVA: 0x000082A9 File Offset: 0x000064A9
		private void ParseRuntimeElement()
		{
			this.ParseTargetElement("Runtime", this.runtimeParserLookup);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000082BC File Offset: 0x000064BC
		private void ParseDataServicesElement()
		{
			string attributeValue = this.GetAttributeValue("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "DataServiceVersion");
			if (attributeValue != null && !EdmxReader.TryParseVersion(attributeValue, out this.dataServiceVersion))
			{
				this.RaiseError(EdmErrorCode.InvalidVersionNumber, Strings.EdmxParser_EdmxDataServiceVersionInvalid);
			}
			string attributeValue2 = this.GetAttributeValue("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "MaxDataServiceVersion");
			if (attributeValue2 != null && !EdmxReader.TryParseVersion(attributeValue2, out this.maxDataServiceVersion))
			{
				this.RaiseError(EdmErrorCode.InvalidVersionNumber, Strings.EdmxParser_EdmxMaxDataServiceVersionInvalid);
			}
			this.ParseTargetElement("DataServices", this.dataServicesParserLookup);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008338 File Offset: 0x00006538
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

		// Token: 0x0600031C RID: 796 RVA: 0x0000836C File Offset: 0x0000656C
		private void ParseConceptualModelsElement()
		{
			this.ParseElement("ConceptualModels", this.conceptualModelsParserLookup);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008380 File Offset: 0x00006580
		private void ParseCsdlSchemaElement()
		{
			using (StringReader stringReader = new StringReader(this.reader.ReadOuterXml()))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					this.csdlParser.AddReader(xmlReader);
				}
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000083E8 File Offset: 0x000065E8
		private void RaiseEmptyFile()
		{
			this.RaiseError(EdmErrorCode.EmptyFile, Strings.XmlParser_EmptySchemaTextReader);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000083F8 File Offset: 0x000065F8
		private CsdlLocation Location()
		{
			IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				return new CsdlLocation(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			return new CsdlLocation(0, 0);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008435 File Offset: 0x00006635
		private void RaiseError(EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(this.Location(), errorCode, errorMessage));
		}

		// Token: 0x04000158 RID: 344
		private static readonly Dictionary<string, Action> EmptyParserLookup = new Dictionary<string, Action>();

		// Token: 0x04000159 RID: 345
		private readonly Dictionary<string, Action> edmxParserLookup;

		// Token: 0x0400015A RID: 346
		private readonly Dictionary<string, Action> runtimeParserLookup;

		// Token: 0x0400015B RID: 347
		private readonly Dictionary<string, Action> conceptualModelsParserLookup;

		// Token: 0x0400015C RID: 348
		private readonly Dictionary<string, Action> dataServicesParserLookup;

		// Token: 0x0400015D RID: 349
		private readonly XmlReader reader;

		// Token: 0x0400015E RID: 350
		private readonly List<EdmError> errors;

		// Token: 0x0400015F RID: 351
		private readonly CsdlParser csdlParser;

		// Token: 0x04000160 RID: 352
		private Version dataServiceVersion;

		// Token: 0x04000161 RID: 353
		private Version maxDataServiceVersion;

		// Token: 0x04000162 RID: 354
		private bool targetParsed;
	}
}
