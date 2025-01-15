using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Xml;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005EA RID: 1514
	public abstract class DbProviderManifest
	{
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060049E8 RID: 18920
		public abstract string NamespaceName { get; }

		// Token: 0x060049E9 RID: 18921
		public abstract ReadOnlyCollection<PrimitiveType> GetStoreTypes();

		// Token: 0x060049EA RID: 18922
		public abstract ReadOnlyCollection<EdmFunction> GetStoreFunctions();

		// Token: 0x060049EB RID: 18923
		public abstract ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType edmType);

		// Token: 0x060049EC RID: 18924
		public abstract TypeUsage GetEdmType(TypeUsage storeType);

		// Token: 0x060049ED RID: 18925
		public abstract TypeUsage GetStoreType(TypeUsage edmType);

		// Token: 0x060049EE RID: 18926
		protected abstract XmlReader GetDbInformation(string informationType);

		// Token: 0x060049EF RID: 18927 RVA: 0x00106494 File Offset: 0x00104694
		public XmlReader GetInformation(string informationType)
		{
			XmlReader xmlReader = null;
			try
			{
				xmlReader = this.GetDbInformation(informationType);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.EntityClient_FailedToGetInformation(informationType), ex);
				}
				throw;
			}
			if (xmlReader != null)
			{
				return xmlReader;
			}
			if (informationType == "ConceptualSchemaDefinitionVersion3" || informationType == "ConceptualSchemaDefinition")
			{
				return DbProviderServices.GetConceptualSchemaDefinition(informationType);
			}
			throw new ProviderIncompatibleException(Strings.ProviderReturnedNullForGetDbInformation(informationType));
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x00106508 File Offset: 0x00104708
		public virtual bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '\0';
			return false;
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0010650E File Offset: 0x0010470E
		public virtual bool SupportsParameterOptimizationInSchemaQueries()
		{
			return false;
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x00106511 File Offset: 0x00104711
		public virtual string EscapeLikeArgument(string argument)
		{
			Check.NotNull<string>(argument, "argument");
			throw new ProviderIncompatibleException(Strings.ProviderShouldOverrideEscapeLikeArgument);
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x00106529 File Offset: 0x00104729
		public virtual bool SupportsInExpression()
		{
			return false;
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0010652C File Offset: 0x0010472C
		public virtual bool SupportsIntersectAndUnionAllFlattening()
		{
			return false;
		}

		// Token: 0x04001A10 RID: 6672
		public const string StoreSchemaDefinition = "StoreSchemaDefinition";

		// Token: 0x04001A11 RID: 6673
		public const string StoreSchemaMapping = "StoreSchemaMapping";

		// Token: 0x04001A12 RID: 6674
		public const string ConceptualSchemaDefinition = "ConceptualSchemaDefinition";

		// Token: 0x04001A13 RID: 6675
		public const string StoreSchemaDefinitionVersion3 = "StoreSchemaDefinitionVersion3";

		// Token: 0x04001A14 RID: 6676
		public const string StoreSchemaMappingVersion3 = "StoreSchemaMappingVersion3";

		// Token: 0x04001A15 RID: 6677
		public const string ConceptualSchemaDefinitionVersion3 = "ConceptualSchemaDefinitionVersion3";

		// Token: 0x04001A16 RID: 6678
		public const string MaxLengthFacetName = "MaxLength";

		// Token: 0x04001A17 RID: 6679
		public const string UnicodeFacetName = "Unicode";

		// Token: 0x04001A18 RID: 6680
		public const string FixedLengthFacetName = "FixedLength";

		// Token: 0x04001A19 RID: 6681
		public const string PrecisionFacetName = "Precision";

		// Token: 0x04001A1A RID: 6682
		public const string ScaleFacetName = "Scale";

		// Token: 0x04001A1B RID: 6683
		public const string NullableFacetName = "Nullable";

		// Token: 0x04001A1C RID: 6684
		public const string DefaultValueFacetName = "DefaultValue";

		// Token: 0x04001A1D RID: 6685
		public const string CollationFacetName = "Collation";

		// Token: 0x04001A1E RID: 6686
		public const string SridFacetName = "SRID";

		// Token: 0x04001A1F RID: 6687
		public const string IsStrictFacetName = "IsStrict";
	}
}
