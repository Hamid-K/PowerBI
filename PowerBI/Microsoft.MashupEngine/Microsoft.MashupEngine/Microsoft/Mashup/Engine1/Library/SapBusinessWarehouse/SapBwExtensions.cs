using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004AD RID: 1197
	internal static class SapBwExtensions
	{
		// Token: 0x06002761 RID: 10081 RVA: 0x00073B25 File Offset: 0x00071D25
		public static bool IsIntrinsic(this MdxProperty property)
		{
			return property.Key != null && property.Key.Name == MdxMemberProperties.QuotedMemberUniqueName;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00073B48 File Offset: 0x00071D48
		public static bool IsKey(this MdxProperty property)
		{
			return property.Name.Trim(new char[] { '[', ']' }).StartsWith('2'.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00073B80 File Offset: 0x00071D80
		public static bool IsNavigationProperty(this MdxProperty property, MdxDimension dimension)
		{
			return property.PropertyKind == MdxPropertyKind.UserDefined && SapBwExtensions.IsNavigationPropertyInternal(dimension.MdxIdentifier, property.Name);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00073B9E File Offset: 0x00071D9E
		public static bool IsNavigationProperty(this MdxPropertyMetadata property)
		{
			return property.PropertyKind == MdxPropertyKind.UserDefined && SapBwExtensions.IsNavigationPropertyInternal(property.DimensionUniqueName, property.UniqueName);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00073BBC File Offset: 0x00071DBC
		public static IEnumerable<TSource> TakeLong<TSource>(this IEnumerable<TSource> source, long count)
		{
			if (count > 0L)
			{
				foreach (TSource tsource in source)
				{
					yield return tsource;
					long num = count - 1L;
					count = num;
					if (num == 0L)
					{
						break;
					}
				}
				IEnumerator<TSource> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00073BD3 File Offset: 0x00071DD3
		public static IEnumerable<TSource> SkipLong<TSource>(this IEnumerable<TSource> source, long count)
		{
			foreach (TSource tsource in source)
			{
				long num = count - 1L;
				count = num;
				if (num < 0L)
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00073BEA File Offset: 0x00071DEA
		public static bool TryGetAdornment(this ApplicationCredentialPropertiesAdornment propertiesAdornment, string name, out object value)
		{
			value = null;
			return propertiesAdornment != null && propertiesAdornment.Properties.TryGetValue(name, out value) && value != null;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00073C08 File Offset: 0x00071E08
		public static bool TryGetAdornment(this ConnectionStringPropertiesAdornment propertiesAdornment, string name, out string value)
		{
			value = null;
			return propertiesAdornment != null && propertiesAdornment.Properties.TryGetValue(name, out value) && !string.IsNullOrEmpty(value);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00073C2C File Offset: 0x00071E2C
		private static bool IsNavigationPropertyInternal(string dimensionId, string propertyName)
		{
			string text = dimensionId.Trim(new char[] { '[', ']' });
			return text.Length >= 1 && !propertyName.Contains(text);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00073C68 File Offset: 0x00071E68
		public static void CreateParameter(this IDbCommand command, ParameterDirection direction, string name, object value)
		{
			if (command != null && name != null && value != null)
			{
				IDbDataParameter dbDataParameter = command.CreateParameter();
				dbDataParameter.Direction = direction;
				dbDataParameter.ParameterName = name;
				dbDataParameter.Value = value;
				command.Parameters.Add(dbDataParameter);
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00073CA7 File Offset: 0x00071EA7
		public static string UnquoteIdentifier(string identifier)
		{
			if (identifier != null)
			{
				return identifier.Trim(new char[] { '[', ']' });
			}
			return null;
		}
	}
}
