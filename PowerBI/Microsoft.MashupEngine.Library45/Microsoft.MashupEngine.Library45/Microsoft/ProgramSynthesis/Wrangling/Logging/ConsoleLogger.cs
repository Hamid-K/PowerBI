using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x02000174 RID: 372
	public class ConsoleLogger : ILogger
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0001972E File Offset: 0x0001792E
		public void Debug(string area, string message, string userData = null)
		{
			this.Output("DEBUG", area, message);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001973D File Offset: 0x0001793D
		public void Info(string area, string message, string userData = null)
		{
			this.Output("INFO ", area, message);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001974C File Offset: 0x0001794C
		public void Warn(string area, string message, string userData = null)
		{
			this.Output("WARN ", area, message);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001975B File Offset: 0x0001795B
		public void Error(string area, string message, string userData = null)
		{
			this.Output("ERROR", area, message);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001976A File Offset: 0x0001796A
		public void TrackException(Exception exception)
		{
			this.Output("ERROR", "Exception", exception.ToString());
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00019782 File Offset: 0x00017982
		public void TrackEvent(string eventName, IReadOnlyCollection<KeyValuePair<string, double>> metrics = null, IReadOnlyCollection<KeyValuePair<string, string>> properties = null, IReadOnlyCollection<KeyValuePair<string, string>> userDataProperties = null)
		{
			this.Output("EVENT", "", string.Format("\n\tMETRICS: {0}\n\tPROPS : {1}", metrics, properties));
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void Flush(bool wait)
		{
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000197A0 File Offset: 0x000179A0
		private void Output(string level, string area, string message)
		{
			Console.Error.WriteLine(string.Concat(new string[]
			{
				DateTime.Now.ToString("HH:mm:ss"),
				" ",
				level,
				" ",
				area,
				" - ",
				message
			}));
		}

		// Token: 0x04000389 RID: 905
		public static ConsoleLogger Instance = new ConsoleLogger();
	}
}
