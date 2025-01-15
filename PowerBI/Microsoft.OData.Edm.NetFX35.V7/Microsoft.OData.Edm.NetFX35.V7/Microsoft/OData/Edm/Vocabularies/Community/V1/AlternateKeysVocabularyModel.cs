using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Vocabularies.Community.V1
{
	// Token: 0x0200013A RID: 314
	public static class AlternateKeysVocabularyModel
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x00014030 File Offset: 0x00012230
		static AlternateKeysVocabularyModel()
		{
			Assembly assembly = typeof(AlternateKeysVocabularyModel).GetAssembly();
			string[] manifestResourceNames = assembly.GetManifestResourceNames();
			string text = Enumerable.FirstOrDefault<string>(Enumerable.Where<string>(manifestResourceNames, (string x) => x.Contains("AlternateKeysVocabularies.xml")));
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(text))
			{
				IEnumerable<EdmError> enumerable;
				SchemaReader.TryParse(new XmlReader[] { XmlReader.Create(manifestResourceStream) }, out AlternateKeysVocabularyModel.Instance, out enumerable);
			}
			AlternateKeysVocabularyModel.AlternateKeysTerm = AlternateKeysVocabularyModel.Instance.FindDeclaredTerm("OData.Community.Keys.V1.AlternateKeys");
			AlternateKeysVocabularyModel.AlternateKeyType = AlternateKeysVocabularyModel.Instance.FindDeclaredType("OData.Community.Keys.V1.AlternateKey") as IEdmComplexType;
			AlternateKeysVocabularyModel.PropertyRefType = AlternateKeysVocabularyModel.Instance.FindDeclaredType("OData.Community.Keys.V1.PropertyRef") as IEdmComplexType;
		}

		// Token: 0x0400044E RID: 1102
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmModel is immutable")]
		public static readonly IEdmModel Instance;

		// Token: 0x0400044F RID: 1103
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm AlternateKeysTerm;

		// Token: 0x04000450 RID: 1104
		internal static readonly IEdmComplexType AlternateKeyType;

		// Token: 0x04000451 RID: 1105
		internal static readonly IEdmComplexType PropertyRefType;
	}
}
