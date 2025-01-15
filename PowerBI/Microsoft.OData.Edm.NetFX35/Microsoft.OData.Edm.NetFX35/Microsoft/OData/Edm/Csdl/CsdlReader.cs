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
	// Token: 0x020001E5 RID: 485
	public static class CsdlReader
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x00020AB8 File Offset: 0x0001ECB8
		public static bool TryParse(IEnumerable<XmlReader> readers, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return CsdlReader.TryParse(readers, Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public static bool TryParse(IEnumerable<XmlReader> readers, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return CsdlReader.TryParse(readers, new IEdmModel[] { reference }, out model, out errors);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00020AEC File Offset: 0x0001ECEC
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
