using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Parsing;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014B RID: 331
	public static class SchemaReader
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x000160C5 File Offset: 0x000142C5
		public static bool TryParse(IEnumerable<XmlReader> readers, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return SchemaReader.TryParse(readers, Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000160D4 File Offset: 0x000142D4
		public static bool TryParse(IEnumerable<XmlReader> readers, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return SchemaReader.TryParse(readers, new IEdmModel[] { reference }, out model, out errors);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000160E8 File Offset: 0x000142E8
		public static bool TryParse(IEnumerable<XmlReader> readers, IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlModel csdlModel;
			if (CsdlParser.TryParse(readers, out csdlModel, out errors))
			{
				model = new CsdlSemanticsModel(csdlModel, new CsdlSemanticsDirectValueAnnotationsManager(), references);
				return true;
			}
			model = null;
			return false;
		}
	}
}
