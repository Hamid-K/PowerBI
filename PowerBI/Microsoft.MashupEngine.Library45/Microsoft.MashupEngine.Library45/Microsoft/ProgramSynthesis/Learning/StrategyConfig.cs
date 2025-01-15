using System;
using System.IO;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006C5 RID: 1733
	public abstract class StrategyConfig
	{
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x00068B5A File Offset: 0x00066D5A
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x00068B62 File Offset: 0x00066D62
		public LogListener LogListener { get; set; }

		// Token: 0x060025A5 RID: 9637 RVA: 0x00068B6B File Offset: 0x00066D6B
		public static StrategyConfig Create(Type configType, string json)
		{
			return JsonConvertUtils.DeserializeObject(json, configType) as StrategyConfig;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00068B7C File Offset: 0x00066D7C
		public void WriteToFile(string filename)
		{
			using (FileStream fileStream = File.Open(filename, FileMode.Create))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					streamWriter.WriteLine(JsonConvert.SerializeObject(this));
				}
			}
		}
	}
}
