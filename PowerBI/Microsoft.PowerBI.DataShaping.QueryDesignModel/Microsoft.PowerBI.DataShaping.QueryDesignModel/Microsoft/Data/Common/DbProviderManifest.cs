using System;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Data.Common
{
	// Token: 0x02000063 RID: 99
	public abstract class DbProviderManifest
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060008FB RID: 2299
		public abstract string NamespaceName { get; }

		// Token: 0x060008FC RID: 2300
		public abstract ReadOnlyCollection<PrimitiveType> GetStoreTypes();

		// Token: 0x060008FD RID: 2301
		public abstract ReadOnlyCollection<EdmFunction> GetStoreFunctions();

		// Token: 0x060008FE RID: 2302
		public abstract ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType edmType);

		// Token: 0x060008FF RID: 2303
		public abstract TypeUsage GetEdmType(TypeUsage storeType);

		// Token: 0x06000900 RID: 2304
		public abstract TypeUsage GetStoreType(TypeUsage edmType);

		// Token: 0x06000901 RID: 2305
		protected abstract XmlReader GetDbInformation(string informationType);

		// Token: 0x06000902 RID: 2306 RVA: 0x000143B6 File Offset: 0x000125B6
		public virtual bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '\0';
			return false;
		}

		// Token: 0x040006FC RID: 1788
		public static readonly string StoreSchemaDefinition = "StoreSchemaDefinition";

		// Token: 0x040006FD RID: 1789
		public static readonly string StoreSchemaMapping = "StoreSchemaMapping";

		// Token: 0x040006FE RID: 1790
		public static readonly string ConceptualSchemaDefinition = "ConceptualSchemaDefinition";

		// Token: 0x040006FF RID: 1791
		internal const string MaxLengthFacetName = "MaxLength";

		// Token: 0x04000700 RID: 1792
		internal const string UnicodeFacetName = "Unicode";

		// Token: 0x04000701 RID: 1793
		internal const string FixedLengthFacetName = "FixedLength";

		// Token: 0x04000702 RID: 1794
		internal const string PrecisionFacetName = "Precision";

		// Token: 0x04000703 RID: 1795
		internal const string ScaleFacetName = "Scale";

		// Token: 0x04000704 RID: 1796
		internal const string NullableFacetName = "Nullable";

		// Token: 0x04000705 RID: 1797
		internal const string DefaultValueFacetName = "DefaultValue";

		// Token: 0x04000706 RID: 1798
		internal const string CollationFacetName = "Collation";
	}
}
