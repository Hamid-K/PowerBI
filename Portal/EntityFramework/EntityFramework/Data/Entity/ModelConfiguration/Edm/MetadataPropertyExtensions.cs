using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200016D RID: 365
	internal static class MetadataPropertyExtensions
	{
		// Token: 0x06001692 RID: 5778 RVA: 0x0003B93B File Offset: 0x00039B3B
		public static IList<Attribute> GetClrAttributes(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return (IList<Attribute>)metadataProperties.GetAnnotation("ClrAttributes");
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0003B94D File Offset: 0x00039B4D
		public static void SetClrAttributes(this ICollection<MetadataProperty> metadataProperties, IList<Attribute> attributes)
		{
			metadataProperties.SetAnnotation("ClrAttributes", attributes);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0003B95B File Offset: 0x00039B5B
		public static PropertyInfo GetClrPropertyInfo(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return (PropertyInfo)metadataProperties.GetAnnotation("ClrPropertyInfo");
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0003B96D File Offset: 0x00039B6D
		public static void SetClrPropertyInfo(this ICollection<MetadataProperty> metadataProperties, PropertyInfo propertyInfo)
		{
			metadataProperties.SetAnnotation("ClrPropertyInfo", propertyInfo);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0003B97B File Offset: 0x00039B7B
		public static Type GetClrType(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return (Type)metadataProperties.GetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType");
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0003B98D File Offset: 0x00039B8D
		public static void SetClrType(this ICollection<MetadataProperty> metadataProperties, Type type)
		{
			metadataProperties.SetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType", type);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0003B99B File Offset: 0x00039B9B
		public static object GetConfiguration(this IEnumerable<MetadataProperty> metadataProperties)
		{
			return metadataProperties.GetAnnotation("Configuration");
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0003B9A8 File Offset: 0x00039BA8
		public static void SetConfiguration(this ICollection<MetadataProperty> metadataProperties, object configuration)
		{
			metadataProperties.SetAnnotation("Configuration", configuration);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		public static object GetAnnotation(this IEnumerable<MetadataProperty> metadataProperties, string name)
		{
			foreach (MetadataProperty metadataProperty in metadataProperties)
			{
				if (metadataProperty.Name.Equals(name, StringComparison.Ordinal))
				{
					return metadataProperty.Value;
				}
			}
			return null;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0003BA14 File Offset: 0x00039C14
		public static void SetAnnotation(this ICollection<MetadataProperty> metadataProperties, string name, object value)
		{
			MetadataProperty metadataProperty = metadataProperties.SingleOrDefault((MetadataProperty p) => p.Name.Equals(name, StringComparison.Ordinal));
			if (metadataProperty == null)
			{
				metadataProperty = MetadataProperty.CreateAnnotation(name, value);
				metadataProperties.Add(metadataProperty);
				return;
			}
			metadataProperty.Value = value;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0003BA60 File Offset: 0x00039C60
		public static void RemoveAnnotation(this ICollection<MetadataProperty> metadataProperties, string name)
		{
			MetadataProperty metadataProperty = metadataProperties.SingleOrDefault((MetadataProperty p) => p.Name.Equals(name, StringComparison.Ordinal));
			if (metadataProperty != null)
			{
				metadataProperties.Remove(metadataProperty);
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0003BA98 File Offset: 0x00039C98
		public static void Copy(this ICollection<MetadataProperty> sourceAnnotations, ICollection<MetadataProperty> targetAnnotations)
		{
			foreach (MetadataProperty metadataProperty in sourceAnnotations)
			{
				targetAnnotations.SetAnnotation(metadataProperty.Name, metadataProperty.Value);
			}
		}

		// Token: 0x04000A0D RID: 2573
		private const string ClrPropertyInfoAnnotation = "ClrPropertyInfo";

		// Token: 0x04000A0E RID: 2574
		private const string ClrAttributesAnnotation = "ClrAttributes";

		// Token: 0x04000A0F RID: 2575
		private const string ConfiguationAnnotation = "Configuration";
	}
}
