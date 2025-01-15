using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004DC RID: 1244
	public static class TypeUtils
	{
		// Token: 0x0600196E RID: 6510 RVA: 0x0008FAEC File Offset: 0x0008DCEC
		public static string PrettyName(Type type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			TypeUtils.BuildPrettyName(type, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0008FB0C File Offset: 0x0008DD0C
		private static void BuildPrettyName(Type type, StringBuilder sb)
		{
			if (type.IsArray)
			{
				Type type2 = type;
				while (type.IsArray)
				{
					type = type.GetElementType();
				}
				TypeUtils.BuildPrettyName(type, sb);
				type = type2;
				while (type.IsArray)
				{
					sb.Append('[');
					for (int i = 1; i < type.GetArrayRank(); i++)
					{
						sb.Append(',');
					}
					sb.Append(']');
					type = type.GetElementType();
				}
				return;
			}
			if (type.IsGenericParameter)
			{
				sb.Append(type.Name);
				return;
			}
			if (type.FullName == null)
			{
				string name = type.Name;
			}
			Match match = Regex.Match(type.FullName, "^(?:\\w+\\.)*([^\\[]+)");
			string[] array = match.Groups[1].Value.Split(new char[] { '+' });
			Type[] array2 = (PlatformUtils.IsGenericEx(type) ? type.GetGenericArguments() : null);
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (j > 0)
				{
					sb.Append('.');
				}
				string text = array[j];
				if (!text.Contains('`'))
				{
					sb.Append(text);
				}
				else
				{
					string[] array3 = text.Split(new char[] { '`' });
					sb.Append(array3[0]);
					sb.Append('<');
					int num2 = int.Parse(array3[1]);
					while (num2-- > 0)
					{
						Type type3 = array2[num++];
						if (!type3.IsGenericParameter)
						{
							TypeUtils.BuildPrettyName(type3, sb);
						}
						if (num2 > 0)
						{
							sb.Append(',');
						}
					}
					sb.Append('>');
				}
			}
		}
	}
}
