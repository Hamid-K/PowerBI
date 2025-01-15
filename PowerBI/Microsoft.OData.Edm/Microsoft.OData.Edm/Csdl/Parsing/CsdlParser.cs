using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing
{
	// Token: 0x020001B9 RID: 441
	internal class CsdlParser
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x00023ED8 File Offset: 0x000220D8
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
						goto IL_008D;
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
					goto IL_006A;
					IL_008D:
					num++;
					continue;
				}
				IL_006A:
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

		// Token: 0x06000C80 RID: 3200 RVA: 0x00023FDC File Offset: 0x000221DC
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

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002404E File Offset: 0x0002224E
		public bool GetResult(out CsdlModel model, out IEnumerable<EdmError> errors)
		{
			model = this.result;
			errors = this.errorsList;
			return this.success;
		}

		// Token: 0x04000713 RID: 1811
		private readonly List<EdmError> errorsList = new List<EdmError>();

		// Token: 0x04000714 RID: 1812
		private readonly CsdlModel result = new CsdlModel();

		// Token: 0x04000715 RID: 1813
		private bool success = true;
	}
}
