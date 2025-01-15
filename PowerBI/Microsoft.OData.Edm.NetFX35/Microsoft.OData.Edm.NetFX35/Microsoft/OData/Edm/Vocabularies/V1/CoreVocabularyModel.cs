using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Vocabularies.V1
{
	// Token: 0x02000155 RID: 341
	public static class CoreVocabularyModel
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		static CoreVocabularyModel()
		{
			Assembly assembly = typeof(CoreVocabularyModel).GetAssembly();
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream("CoreVocabularies.xml"))
			{
				IEnumerable<EdmError> enumerable;
				CsdlReader.TryParse(new XmlReader[] { XmlReader.Create(manifestResourceStream) }, out CoreVocabularyModel.Instance, out enumerable);
				CoreVocabularyModel.IsInitializing = false;
			}
			CoreVocabularyModel.AcceptableMediaTypesTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.AcceptableMediaTypes");
			CoreVocabularyModel.ComputedTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.Computed");
			CoreVocabularyModel.ConcurrencyControlTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.OptimisticConcurrencyControl");
			CoreVocabularyModel.ConcurrencyTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.OptimisticConcurrency");
			CoreVocabularyModel.ConventionalIDsTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.ConventionalIDs");
			CoreVocabularyModel.DereferenceableIDsTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.DereferenceableIDs");
			CoreVocabularyModel.DescriptionTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.Description");
			CoreVocabularyModel.ImmutableTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.Immutable");
			CoreVocabularyModel.IsLanguageDependentTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.IsLanguageDependent");
			CoreVocabularyModel.IsMediaTypeTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.IsMediaType");
			CoreVocabularyModel.IsURLTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.IsURL");
			CoreVocabularyModel.LongDescriptionTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.LongDescription");
			CoreVocabularyModel.MediaTypeTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.MediaType");
			CoreVocabularyModel.RequiresTypeTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.RequiresType");
			CoreVocabularyModel.ResourcePathTerm = CoreVocabularyModel.Instance.FindDeclaredValueTerm("Org.OData.Core.V1.ResourcePath");
		}

		// Token: 0x0400027E RID: 638
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmModel Instance;

		// Token: 0x0400027F RID: 639
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm ConcurrencyTerm;

		// Token: 0x04000280 RID: 640
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm ConcurrencyControlTerm;

		// Token: 0x04000281 RID: 641
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm DescriptionTerm;

		// Token: 0x04000282 RID: 642
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm LongDescriptionTerm;

		// Token: 0x04000283 RID: 643
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm IsLanguageDependentTerm;

		// Token: 0x04000284 RID: 644
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm RequiresTypeTerm;

		// Token: 0x04000285 RID: 645
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm ResourcePathTerm;

		// Token: 0x04000286 RID: 646
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm DereferenceableIDsTerm;

		// Token: 0x04000287 RID: 647
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm ConventionalIDsTerm;

		// Token: 0x04000288 RID: 648
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm ImmutableTerm;

		// Token: 0x04000289 RID: 649
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm ComputedTerm;

		// Token: 0x0400028A RID: 650
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm IsURLTerm;

		// Token: 0x0400028B RID: 651
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm AcceptableMediaTypesTerm;

		// Token: 0x0400028C RID: 652
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm MediaTypeTerm;

		// Token: 0x0400028D RID: 653
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Resolver is immutable")]
		public static readonly IEdmValueTerm IsMediaTypeTerm;

		// Token: 0x0400028E RID: 654
		internal static bool IsInitializing = true;
	}
}
