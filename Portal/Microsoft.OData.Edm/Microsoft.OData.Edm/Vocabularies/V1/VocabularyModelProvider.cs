using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x0200013C RID: 316
	internal static class VocabularyModelProvider
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x00012748 File Offset: 0x00010948
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static VocabularyModelProvider()
		{
			Assembly assembly = typeof(VocabularyModelProvider).GetAssembly();
			string[] manifestResourceNames = assembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string x) => x.Contains("CoreVocabularies.xml"));
			VocabularyModelProvider.CoreModel = VocabularyModelProvider.LoadSchemaEdmModel(assembly, text, new IEdmModel[0]);
			string text2 = manifestResourceNames.FirstOrDefault((string x) => x.Contains("AuthorizationVocabularies.xml"));
			VocabularyModelProvider.AuthorizationModel = VocabularyModelProvider.LoadCsdlEdmModel(assembly, text2, new IEdmModel[] { VocabularyModelProvider.CoreModel });
			string text3 = manifestResourceNames.Where((string x) => x.Contains("ValidationVocabularies.xml")).FirstOrDefault<string>();
			VocabularyModelProvider.ValidationModel = VocabularyModelProvider.LoadSchemaEdmModel(assembly, text3, new IEdmModel[] { VocabularyModelProvider.CoreModel });
			string text4 = manifestResourceNames.FirstOrDefault((string x) => x.Contains("CapabilitiesVocabularies.xml"));
			VocabularyModelProvider.CapabilitesModel = VocabularyModelProvider.LoadCsdlEdmModel(assembly, text4, new IEdmModel[]
			{
				VocabularyModelProvider.CoreModel,
				VocabularyModelProvider.AuthorizationModel,
				VocabularyModelProvider.ValidationModel
			});
			string text5 = manifestResourceNames.Where((string x) => x.Contains("AlternateKeysVocabularies.xml")).FirstOrDefault<string>();
			VocabularyModelProvider.AlternateKeyModel = VocabularyModelProvider.LoadSchemaEdmModel(assembly, text5, new IEdmModel[] { VocabularyModelProvider.CoreModel });
			string text6 = manifestResourceNames.Where((string x) => x.Contains("CommunityVocabularies.xml")).FirstOrDefault<string>();
			VocabularyModelProvider.CommunityModel = VocabularyModelProvider.LoadCsdlEdmModel(assembly, text6, new IEdmModel[] { VocabularyModelProvider.CoreModel });
			VocabularyModelProvider.VocabularyModels = new List<IEdmModel>
			{
				VocabularyModelProvider.CoreModel,
				VocabularyModelProvider.CapabilitesModel,
				VocabularyModelProvider.AlternateKeyModel,
				VocabularyModelProvider.CommunityModel,
				VocabularyModelProvider.ValidationModel,
				VocabularyModelProvider.AuthorizationModel
			};
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00012900 File Offset: 0x00010B00
		private static IEdmModel LoadCsdlEdmModel(Assembly assembly, string vocabularyName, IEnumerable<IEdmModel> referenceModels)
		{
			IEdmModel edmModel2;
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(vocabularyName))
			{
				IEdmModel edmModel;
				IEnumerable<EdmError> enumerable;
				CsdlReader.TryParse(XmlReader.Create(manifestResourceStream), referenceModels, false, out edmModel, out enumerable);
				edmModel2 = edmModel;
			}
			return edmModel2;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00012948 File Offset: 0x00010B48
		private static IEdmModel LoadSchemaEdmModel(Assembly assembly, string vocabularyName, IEnumerable<IEdmModel> referenceModels)
		{
			IEdmModel edmModel2;
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(vocabularyName))
			{
				IEdmModel edmModel;
				IEnumerable<EdmError> enumerable;
				SchemaReader.TryParse(new XmlReader[] { XmlReader.Create(manifestResourceStream) }, referenceModels, false, out edmModel, out enumerable);
				edmModel2 = edmModel;
			}
			return edmModel2;
		}

		// Token: 0x04000376 RID: 886
		public static readonly IEdmModel CoreModel;

		// Token: 0x04000377 RID: 887
		public static readonly IEdmModel CapabilitesModel;

		// Token: 0x04000378 RID: 888
		public static readonly IEdmModel AlternateKeyModel;

		// Token: 0x04000379 RID: 889
		public static readonly IEdmModel CommunityModel;

		// Token: 0x0400037A RID: 890
		public static readonly IEdmModel ValidationModel;

		// Token: 0x0400037B RID: 891
		public static readonly IEdmModel AuthorizationModel;

		// Token: 0x0400037C RID: 892
		public static readonly IEnumerable<IEdmModel> VocabularyModels;
	}
}
