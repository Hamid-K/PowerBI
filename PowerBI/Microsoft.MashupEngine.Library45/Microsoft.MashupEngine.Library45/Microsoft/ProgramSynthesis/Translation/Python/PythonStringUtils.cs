using System;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000318 RID: 792
	public static class PythonStringUtils
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x00033437 File Offset: 0x00031637
		public static string ToPythonLiteral(this char c)
		{
			return c.ToString().ToPythonLiteral();
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00033448 File Offset: 0x00031648
		public static string ToPythonLiteral(this string str)
		{
			if (str == null)
			{
				return "None";
			}
			string text = PythonStringUtils.PythonQuotes.FirstOrDefault((string quote) => !str.Contains(quote) && (quote.Length == 1 || str[str.Length - 1] != quote[0]));
			bool flag;
			if (text != null && !str.EndsWith("\\") && str.Contains("\\"))
			{
				flag = str.All(delegate(char c)
				{
					string text2;
					return c == '\\' || !CSharpUtils.ObjectDisplay.TryReplaceChar(c, out text2);
				});
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				return "r" + text + str + text;
			}
			if (text != null)
			{
				return text + CSharpUtils.ObjectDisplay.FormatLiteral(str, false) + text;
			}
			return CSharpUtils.ObjectDisplay.FormatLiteral(str);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00019834 File Offset: 0x00017A34
		public static string ToPythonLiteral(this bool b)
		{
			if (!b)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00033514 File Offset: 0x00031714
		public static string ToPythonLiteral(this DateTime d)
		{
			if (d.Hour == 0 && d.Minute == 0 && d.Second == 0 && d.Millisecond == 0)
			{
				return string.Format("datetime.strptime('{0:yyyy-MM-dd}', '%Y-%m-%d')", d);
			}
			if (d.Second == 0 && d.Millisecond == 0)
			{
				return string.Format("datetime.strptime('{0:yyyy-MM-ddTHH:mm}', '%Y-%m-%dT%H:%M')", d);
			}
			if (d.Millisecond != 0)
			{
				return string.Format("datetime.strptime('{0:s}.{1:000}', '%Y-%m-%dT%H:%M:%S.%f')", d, d.Millisecond);
			}
			return string.Format("datetime.strptime('{0:s}', '%Y-%m-%dT%H:%M:%S')", d);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000335AF File Offset: 0x000317AF
		public static string ToPythonLiteral(this decimal d)
		{
			return "Decimal(\"" + d.ToString(CultureInfo.InvariantCulture) + "\")";
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000335CC File Offset: 0x000317CC
		public static string ToPythonLiteral(this int i)
		{
			return i.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000335DA File Offset: 0x000317DA
		public static string ToPythonLiteral(this long l)
		{
			return l.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000335E8 File Offset: 0x000317E8
		public static string ToPythonLiteral(this uint u)
		{
			return u.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000335F6 File Offset: 0x000317F6
		public static string ToPythonLiteral(this ulong ul)
		{
			return ul.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00033604 File Offset: 0x00031804
		public static string ToPythonLiteral(this object obj)
		{
			string text;
			if (obj is decimal)
			{
				decimal num = (decimal)obj;
				text = num.ToPythonLiteral();
			}
			else if (obj is int)
			{
				int num2 = (int)obj;
				text = num2.ToPythonLiteral();
			}
			else if (obj is long)
			{
				long num3 = (long)obj;
				text = num3.ToPythonLiteral();
			}
			else if (obj is uint)
			{
				uint num4 = (uint)obj;
				text = num4.ToPythonLiteral();
			}
			else if (obj is ulong)
			{
				ulong num5 = (ulong)obj;
				text = num5.ToPythonLiteral();
			}
			else if (obj is bool)
			{
				bool flag = (bool)obj;
				text = flag.ToPythonLiteral();
			}
			else
			{
				string text2 = obj as string;
				if (text2 == null)
				{
					if (obj is DateTime)
					{
						DateTime dateTime = (DateTime)obj;
						text = dateTime.ToPythonLiteral();
					}
					else if (obj != null)
					{
						text = obj.ToLiteral(null);
					}
					else
					{
						text = "None";
					}
				}
				else
				{
					text = text2.ToPythonLiteral();
				}
			}
			return text;
		}

		// Token: 0x04000881 RID: 2177
		private static readonly string[] PythonQuotes = new string[] { "\"", "'", "\"\"\"", "'''" };
	}
}
