using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000008 RID: 8
	public class StaticConfig
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002671 File Offset: 0x00000871
		private StaticConfig()
		{
			this._args = Args.CommandLine;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002684 File Offset: 0x00000884
		public StaticConfig(string[] args)
		{
			this._args = new Args(args);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002698 File Offset: 0x00000898
		public StaticConfig(Args args)
		{
			this._args = args;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026A7 File Offset: 0x000008A7
		public bool IsDeveloperMode()
		{
			return this._args.ContainsSwitch("dev");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026B9 File Offset: 0x000008B9
		public bool TryGet(string key, out string value)
		{
			if (!this._args.TryGetSwitch(key, out value))
			{
				value = Environment.GetEnvironmentVariable(key);
				if (value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026DC File Offset: 0x000008DC
		public string Get(string key)
		{
			string environmentVariable;
			if (!this._args.TryGetSwitch(key, out environmentVariable))
			{
				environmentVariable = Environment.GetEnvironmentVariable(key);
			}
			return environmentVariable;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002701 File Offset: 0x00000901
		public string GetOrException(string key)
		{
			string text = this.Get(key);
			if (text == null)
			{
				throw new KeyNotFoundException(string.Format("Missing Configuration [{0}]", key));
			}
			return text;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002720 File Offset: 0x00000920
		public bool GetOrDefault(string key, bool defaultValue)
		{
			bool flag = defaultValue;
			string text;
			if (this.TryGet(key, out text))
			{
				bool.TryParse(text, out flag);
			}
			return flag;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002744 File Offset: 0x00000944
		public string GetOrDefault(string key, string defaultValue)
		{
			string text;
			if (!this._args.TryGetSwitch(key, out text))
			{
				text = Environment.GetEnvironmentVariable(key);
				if (text == null)
				{
					text = defaultValue;
				}
			}
			return text;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002770 File Offset: 0x00000970
		public int GetIntOrException(string key)
		{
			string orException = this.GetOrException(key);
			int num;
			if (int.TryParse(orException, out num))
			{
				return num;
			}
			throw new ArgumentException(string.Format("The value {0} for the parameter is not a valid integer", orException), key);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027A4 File Offset: 0x000009A4
		public int GetIntOrDefault(string key, int defaultValue)
		{
			string environmentVariable;
			if (!this._args.TryGetSwitch(key, out environmentVariable))
			{
				environmentVariable = Environment.GetEnvironmentVariable(key);
				if (environmentVariable == null)
				{
					return defaultValue;
				}
			}
			return int.Parse(environmentVariable);
		}

		// Token: 0x0400003C RID: 60
		public static readonly StaticConfig Current = new StaticConfig();

		// Token: 0x0400003D RID: 61
		private readonly Args _args;
	}
}
