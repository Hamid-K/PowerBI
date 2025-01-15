using System;
using System.Text;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020003BF RID: 959
	internal static class TextSaverUtils
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x00076E60 File Offset: 0x00075060
		internal static void MapText(ref DvText src, ref StringBuilder sb, char sep)
		{
			if (sb == null)
			{
				sb = new StringBuilder();
			}
			else
			{
				sb.Clear();
			}
			if (src.IsEmpty)
			{
				sb.Append("\"\"");
				return;
			}
			if (!src.IsNA)
			{
				int num;
				int num2;
				string rawUnderlyingBufferInfo = src.GetRawUnderlyingBufferInfo(ref num, ref num2);
				int i = num;
				int num3 = i;
				bool flag = false;
				if (rawUnderlyingBufferInfo[i] == ' ')
				{
					flag = true;
					sb.Append('"');
				}
				while (i < num2)
				{
					char c = rawUnderlyingBufferInfo[i];
					if (c == '"' || c == sep || c == ':')
					{
						if (!flag)
						{
							sb.Append('"');
							flag = true;
						}
						if (c == '"')
						{
							if (num3 < i)
							{
								sb.Append(rawUnderlyingBufferInfo, num3, i - num3);
							}
							sb.Append("\"\"");
							num3 = i + 1;
						}
					}
					i++;
				}
				if (num3 < i)
				{
					sb.Append(rawUnderlyingBufferInfo, num3, i - num3);
				}
				if (flag)
				{
					sb.Append('"');
				}
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00076F54 File Offset: 0x00075154
		internal static void MapTimeSpan(ref DvTimeSpan src, ref StringBuilder sb)
		{
			if (sb == null)
			{
				sb = new StringBuilder();
			}
			else
			{
				sb.Clear();
			}
			if (!src.IsNA)
			{
				sb.AppendFormat("\"{0:c}\"", ((TimeSpan?)src).Value);
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00076FA4 File Offset: 0x000751A4
		internal static void MapDateTime(ref DvDateTime src, ref StringBuilder sb)
		{
			if (sb == null)
			{
				sb = new StringBuilder();
			}
			else
			{
				sb.Clear();
			}
			if (!src.IsNA)
			{
				sb.AppendFormat("\"{0:o}\"", ((DateTime?)src).Value);
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00076FF4 File Offset: 0x000751F4
		internal static void MapDateTimeZone(ref DvDateTimeZone src, ref StringBuilder sb)
		{
			if (sb == null)
			{
				sb = new StringBuilder();
			}
			else
			{
				sb.Clear();
			}
			if (!src.IsNA)
			{
				sb.AppendFormat("\"{0:o}\"", ((DateTimeOffset?)src).Value);
			}
		}
	}
}
