using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200018F RID: 399
	internal abstract class SerializationStrategy : ISerializationStrategy
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000A3C20 File Offset: 0x000A1E20
		public static SerializationStrategy Default
		{
			get
			{
				return SerializationStrategy.@default;
			}
		}

		// Token: 0x06001866 RID: 6246
		public abstract string GetObjectLogicalPath(ObjectType objectType, string objectName, out bool isSerializedInParentScope);

		// Token: 0x06001867 RID: 6247 RVA: 0x000A3C28 File Offset: 0x000A1E28
		private static IReadOnlyDictionary<char, string> GetReservedCharactersUrlEncoding()
		{
			Dictionary<char, string> dictionary = new Dictionary<char, string>();
			char[] array = "\"<>|:*?/\\".ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				dictionary.Add(array[i], string.Format(CultureInfo.InvariantCulture, "%{0:X}", (int)array[i]));
			}
			return dictionary;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000A3C78 File Offset: 0x000A1E78
		private static string SanitizeName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in name)
			{
				string text;
				if (SerializationStrategy.reservedCharacters.TryGetValue(c, out text))
				{
					stringBuilder.Append(text);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004A1 RID: 1185
		internal const string DbRootPath = "./database";

		// Token: 0x040004A2 RID: 1186
		internal const string ModelRootPath = "./model";

		// Token: 0x040004A3 RID: 1187
		internal const string UnknownPath = "./unknown";

		// Token: 0x040004A4 RID: 1188
		private const string RESERVED_CHARACTERS = "\"<>|:*?/\\";

		// Token: 0x040004A5 RID: 1189
		private static readonly IReadOnlyDictionary<char, string> reservedCharacters = SerializationStrategy.GetReservedCharactersUrlEncoding();

		// Token: 0x040004A6 RID: 1190
		private static readonly SerializationStrategy @default = new SerializationStrategy.DefaultSerializationStrategy();

		// Token: 0x020003B2 RID: 946
		private sealed class DefaultSerializationStrategy : SerializationStrategy
		{
			// Token: 0x060026D7 RID: 9943 RVA: 0x000EBE78 File Offset: 0x000EA078
			public override string GetObjectLogicalPath(ObjectType objectType, string objectName, out bool isSerializedInParentScope)
			{
				if (objectType <= ObjectType.Culture)
				{
					if (objectType <= ObjectType.Table)
					{
						if (objectType == ObjectType.DataSource)
						{
							isSerializedInParentScope = true;
							return "./dataSources";
						}
						if (objectType == ObjectType.Table)
						{
							isSerializedInParentScope = false;
							return string.Format(CultureInfo.InvariantCulture, "./tables/{0}", SerializationStrategy.SanitizeName(objectName));
						}
					}
					else
					{
						if (objectType == ObjectType.Relationship)
						{
							isSerializedInParentScope = true;
							return "./relationships";
						}
						if (objectType == ObjectType.Culture)
						{
							isSerializedInParentScope = false;
							return string.Format(CultureInfo.InvariantCulture, "./cultures/{0}", SerializationStrategy.SanitizeName(objectName));
						}
					}
				}
				else if (objectType <= ObjectType.Role)
				{
					if (objectType == ObjectType.Perspective)
					{
						isSerializedInParentScope = false;
						return string.Format(CultureInfo.InvariantCulture, "./perspectives/{0}", SerializationStrategy.SanitizeName(objectName));
					}
					if (objectType == ObjectType.Role)
					{
						isSerializedInParentScope = false;
						return string.Format(CultureInfo.InvariantCulture, "./roles/{0}", SerializationStrategy.SanitizeName(objectName));
					}
				}
				else
				{
					if (objectType == ObjectType.Expression)
					{
						isSerializedInParentScope = true;
						return "./expressions";
					}
					if (objectType == ObjectType.Database)
					{
						isSerializedInParentScope = false;
						return "./database";
					}
				}
				isSerializedInParentScope = objectType != ObjectType.Model;
				return "./model";
			}
		}
	}
}
