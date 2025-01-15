using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C2 RID: 450
	internal class CommandLineParser : IApplicationSwitchesProvider
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x00028301 File Offset: 0x00026501
		internal CommandLineParser(string[] commandLine)
		{
			this.m_args = commandLine;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00028310 File Offset: 0x00026510
		internal CommandLineParser(string commandLine)
		{
			this.m_args = this.CommandLineSplitter(commandLine);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00028328 File Offset: 0x00026528
		public bool GetBoolSwitch(string name, out bool specified)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (string text in this.m_args)
			{
				Match match = CommandLineParser.sm_regex.Match(text);
				string value = match.Groups["arg"].Value;
				string value2 = match.Groups["value"].Value;
				string value3 = match.Groups["error"].Value;
				if (!string.IsNullOrEmpty(value3))
				{
					string text2 = "Syntax error: Failed to match the following argument (is it in -NAME:VALUE format?): " + value3;
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Syntax error: {0}", new object[] { text2 });
					throw new SyntaxErrorException("Syntax error: " + text2);
				}
				if (!string.IsNullOrEmpty(value))
				{
					if (flag2)
					{
						specified = true;
						return true;
					}
					if (value.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						flag2 = true;
					}
					flag = true;
				}
				if (!string.IsNullOrEmpty(value2))
				{
					if (!flag)
					{
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Syntax error: Value '{0}' is not assigned to an argument.", new object[] { value2 });
						throw new SyntaxErrorException("Value '" + value2 + "' is not assigned to an argument.");
					}
					if (flag2)
					{
						bool flag3;
						if (bool.TryParse(value2, out flag3))
						{
							specified = true;
							return flag3;
						}
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Argument '{0}' is a boolean switch, but its value '{1}' could not be parsed.", new object[] { name, value2 });
						throw new SyntaxErrorException(string.Concat(new string[] { "Argument '", name, "' is a boolean switch, but its value '", value2, "' could not be parsed" }));
					}
					else
					{
						flag = false;
					}
				}
			}
			if (flag2)
			{
				specified = true;
				return true;
			}
			specified = false;
			return false;
		}

		// Token: 0x170001B4 RID: 436
		[CanBeNull]
		public string this[string name]
		{
			get
			{
				bool flag = false;
				bool flag2 = false;
				foreach (string text in this.m_args)
				{
					Match match = CommandLineParser.sm_regex.Match(text);
					string value = match.Groups["arg"].Value;
					string value2 = match.Groups["value"].Value;
					string value3 = match.Groups["error"].Value;
					if (!string.IsNullOrEmpty(value3))
					{
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Syntax error: {0}", new object[] { value3 });
						throw new SyntaxErrorException("Syntax error: " + value3);
					}
					if (!string.IsNullOrEmpty(value))
					{
						if (flag)
						{
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Argument '{0}' requires a value.", new object[] { name });
							throw new SyntaxErrorException("Argument '" + name + "' requires a value.");
						}
						if (value.Equals(name, StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
						else
						{
							flag2 = true;
						}
					}
					if (!string.IsNullOrEmpty(value2))
					{
						if (flag)
						{
							return this.RemoveQuotes(value2);
						}
						if (!flag2)
						{
							TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Syntax error: Value '{0}' is not assigned to an argument.", new object[] { value2 });
							throw new SyntaxErrorException("Value '" + value2 + "' is not assigned to an argument.");
						}
						flag2 = false;
					}
				}
				if (flag)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Argument '{0}' requires a value.", new object[] { name });
					throw new SyntaxErrorException("Argument '" + name + "' requires a value.");
				}
				return null;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00028648 File Offset: 0x00026848
		public string Name
		{
			get
			{
				return "Command Line";
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002864F File Offset: 0x0002684F
		private string RemoveQuotes(string source)
		{
			return new Regex("^['\"]?(.*?)['\"]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, "$1");
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00028668 File Offset: 0x00026868
		private string[] CommandLineSplitter(string cmdLine)
		{
			bool flag = false;
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder(cmdLine.Length);
			for (int i = 0; i < cmdLine.Length; i++)
			{
				if (cmdLine[i] == '"' || cmdLine[i] == '\'')
				{
					flag = !flag;
				}
				if (!flag && cmdLine[i] == ' ')
				{
					list.Add(stringBuilder.ToString());
					stringBuilder.Remove(0, stringBuilder.Length);
				}
				else
				{
					stringBuilder.Append(cmdLine[i]);
				}
			}
			list.Add(stringBuilder.ToString());
			list.RemoveAt(0);
			return list.ToArray();
		}

		// Token: 0x04000482 RID: 1154
		private string[] m_args;

		// Token: 0x04000483 RID: 1155
		private const string c_regexArgument = "(?<arg>[A-Za-z_?][A-Za-z0-9_?]*)";

		// Token: 0x04000484 RID: 1156
		private const string c_regexValue = "(?<value>.*)";

		// Token: 0x04000485 RID: 1157
		private const string c_regexPrefix = "^(?<type>-{1,2}|/)";

		// Token: 0x04000486 RID: 1158
		private const string c_regexError = "^(?<error>(-{1,2}|/).*)$";

		// Token: 0x04000487 RID: 1159
		private static Regex sm_regex = new Regex(string.Format(CultureInfo.CurrentCulture, "({2}{0}(=|:){1}$)|{2}{0}$|{3}|{1}", new object[] { "(?<arg>[A-Za-z_?][A-Za-z0-9_?]*)", "(?<value>.*)", "^(?<type>-{1,2}|/)", "^(?<error>(-{1,2}|/).*)$" }));
	}
}
