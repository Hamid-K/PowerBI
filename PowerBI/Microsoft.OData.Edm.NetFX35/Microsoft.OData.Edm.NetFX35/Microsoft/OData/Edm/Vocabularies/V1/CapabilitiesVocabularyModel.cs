using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x02000152 RID: 338
	internal class CapabilitiesVocabularyModel
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0000EB84 File Offset: 0x0000CD84
		static CapabilitiesVocabularyModel()
		{
			Assembly assembly = typeof(CapabilitiesVocabularyModel).GetAssembly();
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream("CapabilitiesVocabularies.xml"))
			{
				IEnumerable<EdmError> enumerable;
				EdmxReader.TryParse(XmlReader.Create(manifestResourceStream), out CapabilitiesVocabularyModel.Instance, out enumerable);
			}
			CapabilitiesVocabularyModel.ChangeTrackingTerm = CapabilitiesVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Capabilities.V1.ChangeTracking");
		}

		// Token: 0x04000265 RID: 613
		public static readonly IEdmModel Instance;

		// Token: 0x04000266 RID: 614
		public static readonly IEdmValueTerm ChangeTrackingTerm;
	}
}
