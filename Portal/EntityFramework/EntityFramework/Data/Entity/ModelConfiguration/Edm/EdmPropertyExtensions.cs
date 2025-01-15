using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000165 RID: 357
	internal static class EdmPropertyExtensions
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x0003B080 File Offset: 0x00039280
		public static void CopyFrom(this EdmProperty column, EdmProperty other)
		{
			column.IsFixedLength = other.IsFixedLength;
			column.IsMaxLength = other.IsMaxLength;
			column.IsUnicode = other.IsUnicode;
			column.MaxLength = other.MaxLength;
			column.Precision = other.Precision;
			column.Scale = other.Scale;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0003B0D8 File Offset: 0x000392D8
		public static EdmProperty Clone(this EdmProperty tableColumn)
		{
			EdmProperty columnMetadata = new EdmProperty(tableColumn.Name, tableColumn.TypeUsage)
			{
				Nullable = tableColumn.Nullable,
				StoreGeneratedPattern = tableColumn.StoreGeneratedPattern,
				IsFixedLength = tableColumn.IsFixedLength,
				IsMaxLength = tableColumn.IsMaxLength,
				IsUnicode = tableColumn.IsUnicode,
				MaxLength = tableColumn.MaxLength,
				Precision = tableColumn.Precision,
				Scale = tableColumn.Scale
			};
			tableColumn.Annotations.Each(delegate(MetadataProperty a)
			{
				columnMetadata.GetMetadataProperties().Add(a);
			});
			return columnMetadata;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0003B17F File Offset: 0x0003937F
		public static int? GetOrder(this EdmProperty tableColumn)
		{
			return (int?)tableColumn.Annotations.GetAnnotation("Order");
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0003B196 File Offset: 0x00039396
		public static void SetOrder(this EdmProperty tableColumn, int order)
		{
			tableColumn.GetMetadataProperties().SetAnnotation("Order", order);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0003B1AE File Offset: 0x000393AE
		public static string GetPreferredName(this EdmProperty tableColumn)
		{
			return (string)tableColumn.Annotations.GetAnnotation("PreferredName");
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0003B1C5 File Offset: 0x000393C5
		public static void SetPreferredName(this EdmProperty tableColumn, string name)
		{
			tableColumn.GetMetadataProperties().SetAnnotation("PreferredName", name);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0003B1D8 File Offset: 0x000393D8
		public static string GetUnpreferredUniqueName(this EdmProperty tableColumn)
		{
			return (string)tableColumn.Annotations.GetAnnotation("UnpreferredUniqueName");
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0003B1EF File Offset: 0x000393EF
		public static void SetUnpreferredUniqueName(this EdmProperty tableColumn, string name)
		{
			tableColumn.GetMetadataProperties().SetAnnotation("UnpreferredUniqueName", name);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0003B202 File Offset: 0x00039402
		public static void RemoveStoreGeneratedIdentityPattern(this EdmProperty tableColumn)
		{
			if (tableColumn.StoreGeneratedPattern == StoreGeneratedPattern.Identity)
			{
				tableColumn.StoreGeneratedPattern = StoreGeneratedPattern.None;
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0003B214 File Offset: 0x00039414
		public static bool HasStoreGeneratedPattern(this EdmProperty property)
		{
			StoreGeneratedPattern? storeGeneratedPattern = property.GetStoreGeneratedPattern();
			if (storeGeneratedPattern != null)
			{
				StoreGeneratedPattern? storeGeneratedPattern2 = storeGeneratedPattern;
				StoreGeneratedPattern storeGeneratedPattern3 = StoreGeneratedPattern.None;
				return !((storeGeneratedPattern2.GetValueOrDefault() == storeGeneratedPattern3) & (storeGeneratedPattern2 != null));
			}
			return false;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0003B24C File Offset: 0x0003944C
		public static StoreGeneratedPattern? GetStoreGeneratedPattern(this EdmProperty property)
		{
			MetadataProperty metadataProperty;
			if (property.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty))
			{
				return (StoreGeneratedPattern?)Enum.Parse(typeof(StoreGeneratedPattern), (string)metadataProperty.Value);
			}
			return null;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0003B298 File Offset: 0x00039498
		public static void SetStoreGeneratedPattern(this EdmProperty property, StoreGeneratedPattern storeGeneratedPattern)
		{
			MetadataProperty metadataProperty;
			if (!property.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty))
			{
				property.MetadataProperties.Source.Add(new MetadataProperty("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", TypeUsage.Create(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.String)), storeGeneratedPattern.ToString()));
				return;
			}
			metadataProperty.Value = storeGeneratedPattern.ToString();
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0003B306 File Offset: 0x00039506
		public static object GetConfiguration(this EdmProperty property)
		{
			return property.Annotations.GetConfiguration();
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0003B313 File Offset: 0x00039513
		public static void SetConfiguration(this EdmProperty property, object configuration)
		{
			property.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0003B321 File Offset: 0x00039521
		public static List<EdmPropertyPath> ToPropertyPathList(this EdmProperty property)
		{
			return property.ToPropertyPathList(new List<EdmProperty>());
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0003B32E File Offset: 0x0003952E
		public static List<EdmPropertyPath> ToPropertyPathList(this EdmProperty property, List<EdmProperty> currentPath)
		{
			List<EdmPropertyPath> list = new List<EdmPropertyPath>();
			EdmPropertyExtensions.IncludePropertyPath(list, currentPath, property);
			return list;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0003B340 File Offset: 0x00039540
		private static void IncludePropertyPath(List<EdmPropertyPath> propertyPaths, List<EdmProperty> currentPath, EdmProperty property)
		{
			currentPath.Add(property);
			if (property.IsUnderlyingPrimitiveType)
			{
				propertyPaths.Add(new EdmPropertyPath(currentPath));
			}
			else if (property.IsComplexType)
			{
				foreach (EdmProperty edmProperty in property.ComplexType.Properties)
				{
					EdmPropertyExtensions.IncludePropertyPath(propertyPaths, currentPath, edmProperty);
				}
			}
			currentPath.Remove(property);
		}

		// Token: 0x04000A05 RID: 2565
		private const string OrderAnnotation = "Order";

		// Token: 0x04000A06 RID: 2566
		private const string PreferredNameAnnotation = "PreferredName";

		// Token: 0x04000A07 RID: 2567
		private const string UnpreferredUniqueNameAnnotation = "UnpreferredUniqueName";
	}
}
