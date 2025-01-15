using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Vocabularies.Community.V1
{
	// Token: 0x02000156 RID: 342
	public static class AlternateKeysVocabularyModel
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0000ED94 File Offset: 0x0000CF94
		static AlternateKeysVocabularyModel()
		{
			Assembly assembly = typeof(AlternateKeysVocabularyModel).GetAssembly();
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream("AlternateKeysVocabularies.xml"))
			{
				IEnumerable<EdmError> enumerable;
				CsdlReader.TryParse(new XmlReader[] { XmlReader.Create(manifestResourceStream) }, out AlternateKeysVocabularyModel.Instance, out enumerable);
			}
			AlternateKeysVocabularyModel.AlternateKeysTerm = AlternateKeysVocabularyModel.Instance.FindDeclaredValueTerm("OData.Community.Keys.V1.AlternateKeys");
			AlternateKeysVocabularyModel.AlternateKeyType = AlternateKeysVocabularyModel.Instance.FindDeclaredType("OData.Community.Keys.V1.AlternateKey") as IEdmComplexType;
			AlternateKeysVocabularyModel.PropertyRefType = AlternateKeysVocabularyModel.Instance.FindDeclaredType("OData.Community.Keys.V1.PropertyRef") as IEdmComplexType;
		}

		// Token: 0x0400028F RID: 655
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmModel Instance;

		// Token: 0x04000290 RID: 656
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm AlternateKeysTerm;

		// Token: 0x04000291 RID: 657
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		internal static readonly IEdmComplexType AlternateKeyType;

		// Token: 0x04000292 RID: 658
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		internal static readonly IEdmComplexType PropertyRefType;
	}
}
