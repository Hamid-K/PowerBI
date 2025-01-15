using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing
{
	// Token: 0x020001AC RID: 428
	internal class CsdlParser
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x00021D24 File Offset: 0x0001FF24
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

		// Token: 0x06000BCE RID: 3022 RVA: 0x00021E28 File Offset: 0x00020028
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

		// Token: 0x06000BCF RID: 3023 RVA: 0x00021E9A File Offset: 0x0002009A
		public bool GetResult(out CsdlModel model, out IEnumerable<EdmError> errors)
		{
			model = this.result;
			errors = this.errorsList;
			return this.success;
		}

		// Token: 0x0400069A RID: 1690
		private readonly List<EdmError> errorsList = new List<EdmError>();

		// Token: 0x0400069B RID: 1691
		private readonly CsdlModel result = new CsdlModel();

		// Token: 0x0400069C RID: 1692
		private bool success = true;
	}
}
