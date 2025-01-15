using System;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001ED RID: 493
	internal class ConsolePlatformLogger : IPlatformLogger
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x000468F8 File Offset: 0x00044AF8
		public void Always(string message)
		{
			Console.WriteLine(message);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00046900 File Offset: 0x00044B00
		public void Error(string message)
		{
			Console.WriteLine(message);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00046908 File Offset: 0x00044B08
		public void Warning(string message)
		{
			Console.WriteLine(message);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00046910 File Offset: 0x00044B10
		public void Verbose(string message)
		{
			Console.WriteLine(message);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00046918 File Offset: 0x00044B18
		public void Information(string message)
		{
			Console.WriteLine(message);
		}
	}
}
