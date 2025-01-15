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
	// Token: 0x02000158 RID: 344
	public static class SchemaReader
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00017F34 File Offset: 0x00016134
		public static bool TryParse(IEnumerable<XmlReader> readers, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return SchemaReader.TryParse(readers, Enumerable.Empty<IEdmModel>(), out model, out errors);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00017F43 File Offset: 0x00016143
		public static bool TryParse(IEnumerable<XmlReader> readers, IEdmModel reference, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return SchemaReader.TryParse(readers, new IEdmModel[] { reference }, out model, out errors);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00017F57 File Offset: 0x00016157
		public static bool TryParse(IEnumerable<XmlReader> readers, IEnumerable<IEdmModel> references, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			return SchemaReader.TryParse(readers, references, true, out model, out errors);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00017F64 File Offset: 0x00016164
		public static bool TryParse(IEnumerable<XmlReader> readers, IEnumerable<IEdmModel> references, bool includeDefaultVocabularies, out IEdmModel model, out IEnumerable<EdmError> errors)
		{
			CsdlModel csdlModel;
			if (CsdlParser.TryParse(readers, out csdlModel, out errors))
			{
				CsdlSemanticsModel csdlSemanticsModel = new CsdlSemanticsModel(csdlModel, new CsdlSemanticsDirectValueAnnotationsManager(), references, includeDefaultVocabularies);
				model = csdlSemanticsModel;
				return true;
			}
			model = null;
			return false;
		}
	}
}
