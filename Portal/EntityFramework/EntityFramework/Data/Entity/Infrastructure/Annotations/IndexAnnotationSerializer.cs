using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x020002C6 RID: 710
	public class IndexAnnotationSerializer : IMetadataAnnotationSerializer
	{
		// Token: 0x06002233 RID: 8755 RVA: 0x000602FC File Offset: 0x0005E4FC
		public virtual string Serialize(string name, object value)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<object>(value, "value");
			IndexAnnotation indexAnnotation = value as IndexAnnotation;
			if (indexAnnotation == null)
			{
				throw new ArgumentException(Strings.AnnotationSerializeWrongType(value.GetType().Name, typeof(IndexAnnotationSerializer).Name, typeof(IndexAnnotation).Name));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IndexAttribute indexAttribute in indexAnnotation.Indexes)
			{
				stringBuilder.Append(IndexAnnotationSerializer.SerializeIndexAttribute(indexAttribute));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000603B0 File Offset: 0x0005E5B0
		internal static string SerializeIndexAttribute(IndexAttribute indexAttribute)
		{
			StringBuilder stringBuilder = new StringBuilder("{ ");
			if (!string.IsNullOrWhiteSpace(indexAttribute.Name))
			{
				stringBuilder.Append("Name: ").Append(indexAttribute.Name.Replace(",", "\\,").Replace("{", "\\{"));
			}
			if (indexAttribute.Order != -1)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Order: ").Append(indexAttribute.Order);
			}
			if (indexAttribute.IsClusteredConfigured)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("IsClustered: ").Append(indexAttribute.IsClustered);
			}
			if (indexAttribute.IsUniqueConfigured)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("IsUnique: ").Append(indexAttribute.IsUnique);
			}
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000604D0 File Offset: 0x0005E6D0
		public virtual object Deserialize(string name, string value)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(value, "value");
			value = value.Trim();
			if (!value.StartsWith("{", StringComparison.Ordinal) || !value.EndsWith("}", StringComparison.Ordinal))
			{
				throw IndexAnnotationSerializer.BuildFormatException(value);
			}
			List<IndexAttribute> list = new List<IndexAttribute>();
			List<string> list2 = (from s in IndexAnnotationSerializer._indexesSplitter.Split(value)
				select s.Trim()).ToList<string>();
			list2[0] = list2[0].Substring(1);
			int num = list2.Count - 1;
			list2[num] = list2[num].Substring(0, list2[num].Length - 1);
			foreach (string text in list2)
			{
				IndexAttribute indexAttribute = new IndexAttribute();
				if (!string.IsNullOrWhiteSpace(text))
				{
					foreach (string text2 in from s in IndexAnnotationSerializer._indexPartsSplitter.Split(text)
						select s.Trim())
					{
						if (text2.StartsWith("Name:", StringComparison.Ordinal))
						{
							string text3 = text2.Substring(5).Trim();
							if (string.IsNullOrWhiteSpace(text3) || !string.IsNullOrWhiteSpace(indexAttribute.Name))
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.Name = text3.Replace("\\,", ",").Replace("\\{", "{");
						}
						else if (text2.StartsWith("Order:", StringComparison.Ordinal))
						{
							int num2;
							if (!int.TryParse(text2.Substring(6).Trim(), out num2) || indexAttribute.Order != -1)
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.Order = num2;
						}
						else if (text2.StartsWith("IsClustered:", StringComparison.Ordinal))
						{
							bool flag;
							if (!bool.TryParse(text2.Substring(12).Trim(), out flag) || indexAttribute.IsClusteredConfigured)
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.IsClustered = flag;
						}
						else
						{
							if (!text2.StartsWith("IsUnique:", StringComparison.Ordinal))
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							bool flag2;
							if (!bool.TryParse(text2.Substring(9).Trim(), out flag2) || indexAttribute.IsUniqueConfigured)
							{
								throw IndexAnnotationSerializer.BuildFormatException(value);
							}
							indexAttribute.IsUnique = flag2;
						}
					}
				}
				list.Add(indexAttribute);
			}
			return new IndexAnnotation(list);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000607B4 File Offset: 0x0005E9B4
		private static FormatException BuildFormatException(string value)
		{
			return new FormatException(Strings.AnnotationSerializeBadFormat(value, typeof(IndexAnnotationSerializer).Name, "{ Name: MyIndex, Order: 7, IsClustered: True, IsUnique: False } { } { Name: MyOtherIndex }"));
		}

		// Token: 0x04000BE4 RID: 3044
		internal const string FormatExample = "{ Name: MyIndex, Order: 7, IsClustered: True, IsUnique: False } { } { Name: MyOtherIndex }";

		// Token: 0x04000BE5 RID: 3045
		private static readonly Regex _indexesSplitter = new Regex("(?<!\\\\)}\\s*{", RegexOptions.Compiled);

		// Token: 0x04000BE6 RID: 3046
		private static readonly Regex _indexPartsSplitter = new Regex("(?<!\\\\),", RegexOptions.Compiled);
	}
}
