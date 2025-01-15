using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x0200013C RID: 316
	internal class CapabilitiesVocabularyModel
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x000140F8 File Offset: 0x000122F8
		static CapabilitiesVocabularyModel()
		{
			Assembly assembly = typeof(CapabilitiesVocabularyModel).GetAssembly();
			string[] manifestResourceNames = assembly.GetManifestResourceNames();
			string text = Enumerable.FirstOrDefault<string>(Enumerable.Where<string>(manifestResourceNames, (string x) => x.Contains("CapabilitiesVocabularies.xml")));
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(text))
			{
				IEnumerable<EdmError> enumerable;
				CsdlReader.TryParse(XmlReader.Create(manifestResourceStream), out CapabilitiesVocabularyModel.Instance, out enumerable);
			}
			CapabilitiesVocabularyModel.ChangeTrackingTerm = CapabilitiesVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Capabilities.V1.ChangeTracking");
		}

		// Token: 0x04000457 RID: 1111
		public static readonly IEdmModel Instance;

		// Token: 0x04000458 RID: 1112
		public static readonly IEdmTerm ChangeTrackingTerm;
	}
}
