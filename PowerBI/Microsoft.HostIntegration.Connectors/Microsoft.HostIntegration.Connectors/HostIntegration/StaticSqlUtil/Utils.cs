using System;
using System.Globalization;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A7F RID: 2687
	public class Utils
	{
		// Token: 0x0600538A RID: 21386 RVA: 0x00154FD4 File Offset: 0x001531D4
		public static BNDOPTCodePoint GetCodepointByName(string codepointName)
		{
			Array values = Enum.GetValues(typeof(BNDOPTCodePoint));
			for (int i = 0; i < values.Length; i++)
			{
				if (Enum.GetName(typeof(BNDOPTCodePoint), values.GetValue(i)).Equals(codepointName, StringComparison.InvariantCultureIgnoreCase))
				{
					return (BNDOPTCodePoint)values.GetValue(i);
				}
			}
			return BNDOPTCodePoint.UNKNOWN;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x00155030 File Offset: 0x00153230
		public static BNDOPTCodePoint GetCodepoint(object key)
		{
			if (key is BNDOPTCodePoint)
			{
				return (BNDOPTCodePoint)key;
			}
			if (key is string)
			{
				BNDOPTCodePoint bndoptcodePoint = Utils.GetCodepointByName((string)key);
				if (bndoptcodePoint == BNDOPTCodePoint.UNKNOWN)
				{
					bndoptcodePoint = Utils.GetCodepointByDisplayName((string)key);
				}
				return bndoptcodePoint;
			}
			throw new Exception("Key can only be a Codepoint or a string");
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0015507C File Offset: 0x0015327C
		private static BNDOPTCodePoint GetCodepointByDisplayName(string displayName)
		{
			foreach (BNDOPTCodePoint bndoptcodePoint in StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS.Keys)
			{
				if (StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[bndoptcodePoint].Item1.Equals(displayName, StringComparison.CurrentCultureIgnoreCase))
				{
					return bndoptcodePoint;
				}
			}
			return BNDOPTCodePoint.UNKNOWN;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x001550EC File Offset: 0x001532EC
		public static object GetBindOptionValue(BNDOPTCodePoint optCodepoint, object value)
		{
			if (value is int)
			{
				return value;
			}
			if (value is string)
			{
				int num = -1;
				if (int.TryParse((string)value, out num))
				{
					return num;
				}
				if (int.TryParse((string)value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
				{
					return num;
				}
				BNDOPTCodePoint codepointByName = Utils.GetCodepointByName((string)value);
				if (codepointByName == BNDOPTCodePoint.UNKNOWN)
				{
					return Utils.GetOptionValueByXmlName(optCodepoint, (string)value);
				}
				return codepointByName;
			}
			else
			{
				if (value is BNDOPTCodePoint)
				{
					return value;
				}
				throw new Exception("Invalid bind option value.");
			}
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0015517C File Offset: 0x0015337C
		private static object GetOptionValueByXmlName(BNDOPTCodePoint optCodepoint, string xmlValue)
		{
			object[] array = (object[])StaticSqlConstants.BNDOPT_CHOICES[optCodepoint];
			if (array == null || array.Length == 0)
			{
				return xmlValue;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is CustomOptionValue)
				{
					if (((CustomOptionValue)array[i]).XmlName.Equals(xmlValue, StringComparison.CurrentCultureIgnoreCase))
					{
						return ((CustomOptionValue)array[i]).Value;
					}
				}
				else if (StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[(BNDOPTCodePoint)array[i]].Item1.Equals(xmlValue, StringComparison.CurrentCultureIgnoreCase))
				{
					return array[i];
				}
			}
			throw new Exception("Invalid option value");
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00155218 File Offset: 0x00153418
		internal static string GetCodepointDisplayName(BNDOPTCodePoint cp)
		{
			Tuple<string, string> tuple = StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[cp];
			if (tuple != null)
			{
				return tuple.Item1;
			}
			return "";
		}
	}
}
