using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal
{
	// Token: 0x020001D5 RID: 469
	public static class EdmItemExtensions
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x0003E77A File Offset: 0x0003C97A
		public static string GetStringMetadataProperty(this EdmItem edmItem, string propertyId, string defaultValue)
		{
			return edmItem.InternalEdmItem.GetStringMetadataProperty(propertyId, defaultValue);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0003E789 File Offset: 0x0003C989
		public static bool GetBooleanMetadataProperty(this EdmItem edmItem, string propertyId, bool defaultValue)
		{
			return edmItem.InternalEdmItem.GetBooleanMetadataProperty(propertyId, defaultValue);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0003E798 File Offset: 0x0003C998
		public static T GetEnumMetadataProperty<T>(this EdmItem edmItem, string propertyId, T defaultValue) where T : struct
		{
			return edmItem.InternalEdmItem.GetEnumMetadataProperty(propertyId, defaultValue);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0003E7A7 File Offset: 0x0003C9A7
		public static T? GetNullableEnumMetadataProperty<T>(this EdmItem edmItem, string propertyId) where T : struct
		{
			return edmItem.InternalEdmItem.GetNullableEnumMetadataProperty(propertyId);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0003E7B5 File Offset: 0x0003C9B5
		public static XElement GetXElementMetadataProperty(this EdmItem edmItem, string propertyId)
		{
			return edmItem.InternalEdmItem.GetXElementMetadataProperty(propertyId);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0003E7C3 File Offset: 0x0003C9C3
		internal static string GetStringMetadataProperty(this MetadataItem edmItem, string propertyId, string defaultValue)
		{
			return EdmItemExtensions.GetMetadataProperty<string>(edmItem, propertyId, null, defaultValue);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0003E7CE File Offset: 0x0003C9CE
		internal static bool GetBooleanMetadataProperty(this MetadataItem edmItem, string propertyId, bool defaultValue)
		{
			Func<string, bool> func;
			if ((func = EdmItemExtensions.<>O.<0>__ToBoolean) == null)
			{
				func = (EdmItemExtensions.<>O.<0>__ToBoolean = new Func<string, bool>(XmlConvert.ToBoolean));
			}
			return EdmItemExtensions.GetMetadataProperty<bool>(edmItem, propertyId, func, defaultValue);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0003E7F3 File Offset: 0x0003C9F3
		internal static T GetEnumMetadataProperty<T>(this MetadataItem edmItem, string propertyId, T defaultValue) where T : struct
		{
			return EdmItemExtensions.GetMetadataProperty<T>(edmItem, propertyId, (string str) => (T)((object)Enum.Parse(typeof(T), str, false)), defaultValue);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0003E81C File Offset: 0x0003CA1C
		internal static T? GetNullableEnumMetadataProperty<T>(this MetadataItem edmItem, string propertyId) where T : struct
		{
			return EdmItemExtensions.GetMetadataProperty<T?>(edmItem, propertyId, delegate(string str)
			{
				T t;
				if (Enum.TryParse<T>(str, false, out t))
				{
					return new T?(t);
				}
				return null;
			}, null);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0003E858 File Offset: 0x0003CA58
		internal static XElement GetXElementMetadataProperty(this MetadataItem edmItem, string propertyId)
		{
			return EdmItemExtensions.GetMetadataProperty<XElement>(edmItem, propertyId, null, null);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0003E864 File Offset: 0x0003CA64
		private static T GetMetadataProperty<T>(MetadataItem edmItem, string propertyId, Func<string, T> convert, T defaultValue)
		{
			MetadataProperty metadataProperty;
			if (edmItem.MetadataProperties.TryGetValue(propertyId, false, out metadataProperty))
			{
				if (metadataProperty.Value is T)
				{
					return (T)((object)metadataProperty.Value);
				}
				if (convert != null)
				{
					string text = metadataProperty.Value as string;
					if (!string.IsNullOrEmpty(text))
					{
						try
						{
							return convert(text);
						}
						catch (Exception)
						{
						}
						return defaultValue;
					}
				}
			}
			return defaultValue;
		}

		// Token: 0x020003B7 RID: 951
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400137C RID: 4988
			public static Func<string, bool> <0>__ToBoolean;
		}
	}
}
