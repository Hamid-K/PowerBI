using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004AE RID: 1198
	public static class PredictionUtil
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x0008D098 File Offset: 0x0008B298
		public static void ParseArguments(object args, string settings, string name = null)
		{
			if (string.IsNullOrWhiteSpace(settings))
			{
				return;
			}
			string text = null;
			try
			{
				string err = null;
				string text2;
				if (!CmdParser.ParseArguments(settings, args, delegate(string e)
				{
					err = err ?? e;
				}, ref text2))
				{
					text = err + ((!string.IsNullOrWhiteSpace(name)) ? ("\nUSAGE FOR '" + name + "':\n") : "\nUSAGE:\n") + text2;
				}
			}
			catch (Exception ex)
			{
				text = "Unexpected exception thrown while parsing: " + ex.Message;
			}
			if (text != null)
			{
				throw Contracts.Except(text);
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0008D130 File Offset: 0x0008B330
		public static string CombineSettings(string[] settings, string[] extraSettings = null)
		{
			if (Utils.Size<string>(extraSettings) == 0)
			{
				return CmdParser.CombineSettings(settings);
			}
			if (Utils.Size<string>(settings) == 0)
			{
				return CmdParser.CombineSettings(PredictionUtil.SplitOnSemis(extraSettings));
			}
			return CmdParser.CombineSettings(settings) + " " + CmdParser.CombineSettings(PredictionUtil.SplitOnSemis(extraSettings));
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0008D170 File Offset: 0x0008B370
		public static string[] SplitOnSemis(string[] args)
		{
			if (Utils.Size<string>(args) == 0)
			{
				return null;
			}
			List<string> list = null;
			for (int i = 0; i < args.Length; i++)
			{
				string text = args[i];
				if (!text.Contains(';') || text.IndexOfAny(PredictionUtil._dontSplitChars) >= 0)
				{
					if (list != null)
					{
						list.Add(text);
					}
				}
				else
				{
					if (list == null)
					{
						list = new List<string>(args.Take(i));
					}
					list.AddRange(text.Split(new char[] { ';' }));
				}
			}
			if (list != null)
			{
				return list.ToArray();
			}
			return args;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0008D1F4 File Offset: 0x0008B3F4
		public static string Array2String(float[] a, string sep)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (a.Length == 0)
			{
				return "";
			}
			stringBuilder.Append(a[0].ToString());
			for (int i = 1; i < a.Length; i++)
			{
				stringBuilder.Append(sep + a[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0008D250 File Offset: 0x0008B450
		public static char[] SeparatorFromString(string sep)
		{
			if (string.IsNullOrEmpty(sep))
			{
				return null;
			}
			if (sep.Length == 1)
			{
				return new char[] { sep[0] };
			}
			List<char> list = new List<char>();
			foreach (string text in sep.Split(new char[] { ',' }))
			{
				char c = PredictionUtil.SepCharFromString(text);
				if (c != '\0')
				{
					list.Add(c);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0008D2DC File Offset: 0x0008B4DC
		public static char SepCharFromString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return '\0';
			}
			string text;
			switch (text = s.ToLower())
			{
			case "space":
				return ' ';
			case "tab":
				return '\t';
			case "comma":
				return ',';
			case "colon":
				return ':';
			case "semicolon":
				return ';';
			case "bar":
				return '|';
			}
			if (s.Length == 1)
			{
				return s[0];
			}
			return '\0';
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
		public static string SepCharToString(char separator)
		{
			char c = separator;
			if (c == '\t')
			{
				return "tab";
			}
			if (c == ' ')
			{
				return "space";
			}
			return separator.ToString();
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0008D3E0 File Offset: 0x0008B5E0
		public static void DummyTraceWriteLine(string format, params object[] arg)
		{
		}

		// Token: 0x04000EEB RID: 3819
		private static char[] _dontSplitChars = new char[] { ' ', '=', '{', '}', '\t' };
	}
}
