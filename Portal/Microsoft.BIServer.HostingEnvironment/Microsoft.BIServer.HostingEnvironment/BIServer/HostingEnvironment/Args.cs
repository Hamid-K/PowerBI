using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000005 RID: 5
	public sealed class Args
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000218F File Offset: 0x0000038F
		public Args(string[] args)
		{
			this.SetArgs(args);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public Args(Dictionary<string, string> args)
		{
			this._switches = args;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D9 File Offset: 0x000003D9
		private Args()
			: this(Environment.GetCommandLineArgs().Skip(1).ToArray<string>())
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FC File Offset: 0x000003FC
		public Args(string[] args, string[] defaultArgs)
		{
			this.SetArgs(defaultArgs);
			this.SetArgs(args);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002228 File Offset: 0x00000428
		public IReadOnlyDictionary<string, string> Switches
		{
			get
			{
				return this._switches;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002230 File Offset: 0x00000430
		private void SetArgs(string[] args)
		{
			foreach (string text in args)
			{
				if (text.StartsWith("-") || text.StartsWith("/"))
				{
					string text2 = text.Substring(1);
					int num = text2.IndexOf('=');
					if (num == 0)
					{
						throw new FormatException(string.Format("Switch argument {0} cannot begin with '='", text2));
					}
					if (num > 0)
					{
						string text3 = text2.Substring(0, num);
						int num2 = num + 1;
						string text4 = text2.Substring(num2, text2.Length - num2);
						this._switches[text3] = text4;
					}
					else
					{
						this._switches[text2] = null;
					}
				}
				else
				{
					this._nonSwitches.Add(text);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E6 File Offset: 0x000004E6
		public bool TryGetSwitch(string key, out string value)
		{
			return this._switches.TryGetValue(key, out value);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F5 File Offset: 0x000004F5
		public string GetSwtich(string key)
		{
			return this.GetSwitchOrDefault(key, null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002300 File Offset: 0x00000500
		public string GetSwitchOrDefault(string key, string defaultValue)
		{
			string text;
			if (this._switches.TryGetValue(key, out text))
			{
				return text;
			}
			return defaultValue;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002320 File Offset: 0x00000520
		public string GetSwitchOrException(string key)
		{
			string text;
			if (this._switches.TryGetValue(key, out text))
			{
				return text;
			}
			throw new KeyNotFoundException(string.Format("Missing required command line parameter [{0}]", key));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000234F File Offset: 0x0000054F
		public bool ContainsSwitch(string key)
		{
			return this._switches.ContainsKey(key);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002228 File Offset: 0x00000428
		public IReadOnlyDictionary<string, string> GetSwitches()
		{
			return this._switches;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000235D File Offset: 0x0000055D
		public IReadOnlyList<string> GetNonSwitches()
		{
			return this._nonSwitches;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002365 File Offset: 0x00000565
		public void PauseIfDebugSwitch()
		{
			if (this.ContainsSwitch("debug"))
			{
				if (Logger.ConsoleAvailable())
				{
					Console.WriteLine("Paused for debugger attach. Hit any key to continue...");
					Console.ReadKey();
					Console.WriteLine();
					return;
				}
				Debugger.Launch();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002398 File Offset: 0x00000598
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\nSwitches:");
			foreach (KeyValuePair<string, string> keyValuePair in this._switches)
			{
				if (!keyValuePair.Key.Contains("pass") && !keyValuePair.Key.Contains("pwd"))
				{
					stringBuilder.Append("\n    [").Append(keyValuePair.Key).Append("] = [")
						.Append(keyValuePair.Value)
						.Append("]");
				}
				else
				{
					stringBuilder.Append("\n    [").Append(keyValuePair.Key).Append("] = [********]");
				}
			}
			stringBuilder.Append("\nNon-Switches:");
			foreach (string text in this._nonSwitches)
			{
				stringBuilder.Append("\n    [").Append(text).Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400002F RID: 47
		public static readonly Args CommandLine = new Args();

		// Token: 0x04000030 RID: 48
		private readonly Dictionary<string, string> _switches = new Dictionary<string, string>();

		// Token: 0x04000031 RID: 49
		private readonly List<string> _nonSwitches = new List<string>();
	}
}
