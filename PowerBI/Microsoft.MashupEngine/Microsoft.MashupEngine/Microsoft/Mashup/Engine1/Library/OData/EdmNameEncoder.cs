using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000722 RID: 1826
	internal static class EdmNameEncoder
	{
		// Token: 0x06003661 RID: 13921 RVA: 0x000AD4D0 File Offset: 0x000AB6D0
		public static string Encode(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			name = XmlConvert.EncodeLocalName(name);
			StringBuilder stringBuilder = null;
			if (name[0] == '℮')
			{
				stringBuilder = new StringBuilder("_x212E_");
			}
			for (int i = 1; i < name.Length; i++)
			{
				char c = name[i];
				if (EdmNameEncoder.followSpecial.Contains(c))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(name, 0, i, name.Length + 6);
					}
					EdmNameEncoder.AppendEncoding(stringBuilder, c);
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder == null)
			{
				return name;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000AD564 File Offset: 0x000AB764
		private static void AppendEncoding(StringBuilder builder, char ch)
		{
			builder.Append(EdmNameEncoder.encodingPrefix);
			int num = (int)ch;
			for (int i = 2; i < 6; i++)
			{
				int num2 = ((num & 61440) >> 12) + 48;
				if (num2 > 57)
				{
					num2 += 7;
				}
				builder.Append((char)num2);
				num <<= 4;
			}
			builder.Append('_');
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000AD5B8 File Offset: 0x000AB7B8
		public static string Decode(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x04001BF4 RID: 7156
		private const char firstSpecial = '℮';

		// Token: 0x04001BF5 RID: 7157
		private const string firstEncoded = "_x212E_";

		// Token: 0x04001BF6 RID: 7158
		private const char encodingSuffix = '_';

		// Token: 0x04001BF7 RID: 7159
		private static readonly HashSet<char> followSpecial = new HashSet<char> { '-', '.', '·', '·', '\u06dd', '۞', '℮' };

		// Token: 0x04001BF8 RID: 7160
		private static readonly char[] encodingPrefix = new char[] { '_', 'x' };
	}
}
