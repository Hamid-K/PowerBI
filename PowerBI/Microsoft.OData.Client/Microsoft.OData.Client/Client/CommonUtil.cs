using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Microsoft.OData.Client
{
	// Token: 0x02000012 RID: 18
	internal static class CommonUtil
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003960 File Offset: 0x00001B60
		public static object ParseJsonToPrimitiveValue(string rawValue)
		{
			ODataCollectionValue odataCollectionValue = (ODataCollectionValue)ODataUriUtils.ConvertFromUriLiteral(string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[] { rawValue }), ODataVersion.V4);
			using (IEnumerator<object> enumerator = odataCollectionValue.Items.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000039D4 File Offset: 0x00001BD4
		internal static bool IsCatchableExceptionType(Exception e)
		{
			if (e == null)
			{
				return true;
			}
			Type type = e.GetType();
			return type != CommonUtil.ThreadAbortType && type != CommonUtil.StackOverflowType && type != CommonUtil.OutOfMemoryType;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A14 File Offset: 0x00001C14
		internal static bool IsUnsupportedType(Type type)
		{
			if (type.IsGenericType())
			{
				type = type.GetGenericTypeDefinition();
			}
			return CommonUtil.unsupportedTypes.Any((Type t) => t.IsAssignableFrom(type));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A68 File Offset: 0x00001C68
		internal static string GetCollectionItemTypeName(string typeName, bool isNested)
		{
			if (typeName == null || !typeName.StartsWith("Collection(", StringComparison.Ordinal) || typeName[typeName.Length - 1] != ')' || typeName.Length == "Collection()".Length)
			{
				return null;
			}
			if (isNested)
			{
				throw Error.InvalidOperation(Strings.ClientType_CollectionOfCollectionNotSupported);
			}
			string text = typeName.Substring("Collection(".Length, typeName.Length - "Collection()".Length);
			CommonUtil.GetCollectionItemTypeName(text, true);
			return text;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003AE5 File Offset: 0x00001CE5
		internal static ODataVersion ConvertToODataVersion(ODataProtocolVersion maxProtocolVersion)
		{
			if (maxProtocolVersion == ODataProtocolVersion.V4)
			{
				return ODataVersion.V4;
			}
			if (maxProtocolVersion != ODataProtocolVersion.V401)
			{
				return (ODataVersion)(-1);
			}
			return ODataVersion.V401;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003AF5 File Offset: 0x00001CF5
		internal static ODataVersion ConvertToODataVersion(Version version)
		{
			if (version.Major == 4 && version.Minor == 1)
			{
				return ODataVersion.V401;
			}
			return ODataVersion.V4;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B0C File Offset: 0x00001D0C
		internal static string GetModelTypeName(Type type)
		{
			if (type.IsGenericType())
			{
				Type[] genericArguments = type.GetGenericArguments();
				StringBuilder stringBuilder = new StringBuilder(type.Name.Length * 2 * (1 + genericArguments.Length));
				if (type.IsNested)
				{
					stringBuilder.Append(CommonUtil.GetModelTypeName(type.DeclaringType));
					stringBuilder.Append('_');
				}
				stringBuilder.Append(type.Name);
				stringBuilder.Append('[');
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(' ');
					}
					if (genericArguments[i].IsGenericParameter)
					{
						stringBuilder.Append(genericArguments[i].Name);
					}
					else
					{
						string modelTypeNamespace = CommonUtil.GetModelTypeNamespace(genericArguments[i]);
						if (!string.IsNullOrEmpty(modelTypeNamespace))
						{
							stringBuilder.Append(modelTypeNamespace);
							stringBuilder.Append('.');
						}
						stringBuilder.Append(CommonUtil.GetModelTypeName(genericArguments[i]));
					}
				}
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}
			if (type.IsNested)
			{
				return CommonUtil.GetModelTypeName(type.DeclaringType) + "_" + type.Name;
			}
			return type.Name;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003C1E File Offset: 0x00001E1E
		internal static string GetModelTypeNamespace(Type type)
		{
			return type.Namespace ?? string.Empty;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C30 File Offset: 0x00001E30
		internal static bool TryReadVersion(string text, out KeyValuePair<Version, string> result)
		{
			int num = text.IndexOf(';');
			string text2;
			string text3;
			if (num >= 0)
			{
				text2 = text.Substring(0, num);
				text3 = text.Substring(num + 1).Trim();
			}
			else
			{
				text2 = text;
				text3 = null;
			}
			result = default(KeyValuePair<Version, string>);
			text2 = text2.Trim();
			bool flag = false;
			for (int i = 0; i < text2.Length; i++)
			{
				if (text2[i] == '.')
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
				else if (text2[i] < '0' || text2[i] > '9')
				{
					return false;
				}
			}
			bool flag2;
			try
			{
				result = new KeyValuePair<Version, string>(new Version(text2), text3);
				flag2 = true;
			}
			catch (Exception ex)
			{
				if (!CommonUtil.IsCatchableExceptionType(ex) || (!(ex is FormatException) && !(ex is OverflowException) && !(ex is ArgumentException)))
				{
					throw;
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003D14 File Offset: 0x00001F14
		internal static void SetDefaultMessageQuotas(ODataMessageQuotas messageQuotas)
		{
			messageQuotas.MaxReceivedMessageSize = long.MaxValue;
			messageQuotas.MaxPartsPerBatch = int.MaxValue;
			messageQuotas.MaxOperationsPerChangeset = int.MaxValue;
			messageQuotas.MaxNestingDepth = int.MaxValue;
		}

		// Token: 0x0400002C RID: 44
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400002D RID: 45
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400002E RID: 46
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x0400002F RID: 47
		private static readonly Type[] unsupportedTypes = new Type[]
		{
			typeof(IDynamicMetaObjectProvider),
			typeof(Tuple<>),
			typeof(Tuple<, >),
			typeof(Tuple<, , >),
			typeof(Tuple<, , , >),
			typeof(Tuple<, , , , >),
			typeof(Tuple<, , , , , >),
			typeof(Tuple<, , , , , , >),
			typeof(Tuple<, , , , , , , >)
		};
	}
}
