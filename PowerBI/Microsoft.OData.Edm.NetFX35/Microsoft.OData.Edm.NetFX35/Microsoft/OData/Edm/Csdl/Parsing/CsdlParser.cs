using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing
{
	// Token: 0x020001A2 RID: 418
	internal class CsdlParser
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x00015E78 File Offset: 0x00014078
		public static bool TryParse(IEnumerable<XmlReader> csdlReaders, out CsdlModel entityModel, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<XmlReader>>(csdlReaders, "csdlReaders");
			CsdlParser csdlParser = new CsdlParser();
			int num = 0;
			foreach (XmlReader xmlReader in csdlReaders)
			{
				if (xmlReader != null)
				{
					try
					{
						csdlParser.AddReader(xmlReader, null);
						goto IL_0095;
					}
					catch (XmlException ex)
					{
						entityModel = null;
						errors = new EdmError[]
						{
							new EdmError(new CsdlLocation(ex.LineNumber, ex.LinePosition), EdmErrorCode.XmlError, ex.Message)
						};
						return false;
					}
					goto IL_006D;
					IL_0095:
					num++;
					continue;
				}
				IL_006D:
				entityModel = null;
				errors = new EdmError[]
				{
					new EdmError(null, EdmErrorCode.NullXmlReader, Strings.CsdlParser_NullXmlReader)
				};
				return false;
			}
			if (num == 0)
			{
				entityModel = null;
				errors = new EdmError[]
				{
					new EdmError(null, EdmErrorCode.NoReadersProvided, Strings.CsdlParser_NoReadersProvided)
				};
				return false;
			}
			bool flag = csdlParser.GetResult(out entityModel, out errors);
			if (!flag)
			{
				entityModel = null;
			}
			return flag;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00015F94 File Offset: 0x00014194
		public bool AddReader(XmlReader csdlReader, string source = null)
		{
			string text = source ?? csdlReader.BaseURI;
			CsdlDocumentParser csdlDocumentParser = new CsdlDocumentParser(text, csdlReader);
			csdlDocumentParser.ParseDocumentElement();
			this.success &= !csdlDocumentParser.HasErrors;
			this.errorsList.AddRange(csdlDocumentParser.Errors);
			if (csdlDocumentParser.Result != null)
			{
				this.result.AddSchema(csdlDocumentParser.Result.Value);
			}
			return this.success;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00016006 File Offset: 0x00014206
		public bool GetResult(out CsdlModel model, out IEnumerable<EdmError> errors)
		{
			model = this.result;
			errors = this.errorsList;
			return this.success;
		}

		// Token: 0x0400042A RID: 1066
		private readonly List<EdmError> errorsList = new List<EdmError>();

		// Token: 0x0400042B RID: 1067
		private readonly CsdlModel result = new CsdlModel();

		// Token: 0x0400042C RID: 1068
		private bool success = true;
	}
}
