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
	// Token: 0x0200013E RID: 318
	public static class CoreVocabularyModel
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00014184 File Offset: 0x00012384
		static CoreVocabularyModel()
		{
			Assembly assembly = typeof(CoreVocabularyModel).GetAssembly();
			string[] manifestResourceNames = assembly.GetManifestResourceNames();
			string text = Enumerable.FirstOrDefault<string>(Enumerable.Where<string>(manifestResourceNames, (string x) => x.Contains("CoreVocabularies.xml")));
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(text))
			{
				IEnumerable<EdmError> enumerable;
				SchemaReader.TryParse(new XmlReader[] { XmlReader.Create(manifestResourceStream) }, out CoreVocabularyModel.Instance, out enumerable);
				CoreVocabularyModel.IsInitializing = false;
			}
			CoreVocabularyModel.AcceptableMediaTypesTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.AcceptableMediaTypes");
			CoreVocabularyModel.ComputedTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.Computed");
			CoreVocabularyModel.ConcurrencyTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.OptimisticConcurrency");
			CoreVocabularyModel.ConventionalIDsTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.ConventionalIDs");
			CoreVocabularyModel.DereferenceableIDsTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.DereferenceableIDs");
			CoreVocabularyModel.DescriptionTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.Description");
			CoreVocabularyModel.ImmutableTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.Immutable");
			CoreVocabularyModel.IsLanguageDependentTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.IsLanguageDependent");
			CoreVocabularyModel.IsMediaTypeTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.IsMediaType");
			CoreVocabularyModel.IsURLTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.IsURL");
			CoreVocabularyModel.LongDescriptionTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.LongDescription");
			CoreVocabularyModel.MediaTypeTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.MediaType");
			CoreVocabularyModel.OptionalParameterTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.OptionalParameter");
			CoreVocabularyModel.RequiresTypeTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.RequiresType");
			CoreVocabularyModel.ResourcePathTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.ResourcePath");
			CoreVocabularyModel.PermissionsTerm = CoreVocabularyModel.Instance.FindDeclaredTerm("Org.OData.Core.V1.Permissions");
		}

		// Token: 0x0400046A RID: 1130
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmModel is immutable")]
		public static readonly IEdmModel Instance;

		// Token: 0x0400046B RID: 1131
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ConcurrencyTerm;

		// Token: 0x0400046C RID: 1132
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm DescriptionTerm;

		// Token: 0x0400046D RID: 1133
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm LongDescriptionTerm;

		// Token: 0x0400046E RID: 1134
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm IsLanguageDependentTerm;

		// Token: 0x0400046F RID: 1135
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm RequiresTypeTerm;

		// Token: 0x04000470 RID: 1136
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ResourcePathTerm;

		// Token: 0x04000471 RID: 1137
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm DereferenceableIDsTerm;

		// Token: 0x04000472 RID: 1138
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ConventionalIDsTerm;

		// Token: 0x04000473 RID: 1139
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ImmutableTerm;

		// Token: 0x04000474 RID: 1140
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm ComputedTerm;

		// Token: 0x04000475 RID: 1141
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmTerm OptionalParameterTerm;

		// Token: 0x04000476 RID: 1142
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm IsURLTerm;

		// Token: 0x04000477 RID: 1143
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm AcceptableMediaTypesTerm;

		// Token: 0x04000478 RID: 1144
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm MediaTypeTerm;

		// Token: 0x04000479 RID: 1145
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm IsMediaTypeTerm;

		// Token: 0x0400047A RID: 1146
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "EdmTerm is immutable")]
		public static readonly IEdmTerm PermissionsTerm;

		// Token: 0x0400047B RID: 1147
		internal static bool IsInitializing = true;
	}
}
