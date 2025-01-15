using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003F1 RID: 1009
	public class AsyncProcessRunner
	{
		// Token: 0x060016EE RID: 5870 RVA: 0x000461CC File Offset: 0x000443CC
		public static AsyncProcessRunner.ProcessResult RunProcesses(IEnumerable<Process> processes, Action<string> logger = null, Action<Process> afterProcessStart = null, Action<Process> afterProcessCompletion = null)
		{
			int count = processes.Count<Process>();
			Stopwatch stopwatch = Stopwatch.StartNew();
			Record<bool, double> record = processes.AsParallel<Process>().Select(delegate(Process process, int i)
			{
				Stopwatch stopwatch2 = Stopwatch.StartNew();
				Action<string> logger3 = logger;
				if (logger3 != null)
				{
					logger3(string.Format("Starting process {0} of {1}: {2} {3}", new object[]
					{
						i,
						count,
						process.StartInfo.FileName,
						process.StartInfo.Arguments
					}));
				}
				bool flag = process.Start();
				Action<Process> afterProcessStart2 = afterProcessStart;
				if (afterProcessStart2 != null)
				{
					afterProcessStart2(process);
				}
				process.WaitForExit();
				Action<Process> afterProcessCompletion2 = afterProcessCompletion;
				if (afterProcessCompletion2 != null)
				{
					afterProcessCompletion2(process);
				}
				double totalSeconds = stopwatch2.Elapsed.TotalSeconds;
				Action<string> logger4 = logger;
				if (logger4 != null)
				{
					logger4(string.Format("Finished process {0} of {1} in {2} seconds: {3} {4}", new object[]
					{
						i,
						count,
						totalSeconds,
						process.StartInfo.FileName,
						process.StartInfo.Arguments
					}));
				}
				return Record.Create<bool, double>(flag, totalSeconds);
			}).AsSequential<Record<bool, double>>()
				.Aggregate(Record.Create<bool, double>(true, 0.0), (Record<bool, double> r, Record<bool, double> a) => Record.Create<bool, double>(r.Item1 & a.Item1, r.Item2 + a.Item2));
			AsyncProcessRunner.ProcessResult processResult = new AsyncProcessRunner.ProcessResult
			{
				Result = record.Item1,
				AggregateChildrenRunTimeSeconds = record.Item2,
				RunTimeSeconds = stopwatch.Elapsed.TotalSeconds
			};
			Action<string> logger2 = logger;
			if (logger2 != null)
			{
				logger2(string.Format("Finished {0} processes in {1} seconds.", count, Math.Round(processResult.RunTimeSeconds, 2)) + string.Format("Aggregate sum of each process's time: {0} seconds.", Math.Round(processResult.AggregateChildrenRunTimeSeconds, 2)));
			}
			return processResult;
		}

		// Token: 0x020003F2 RID: 1010
		public struct ProcessResult
		{
			// Token: 0x04000B02 RID: 2818
			public bool Result;

			// Token: 0x04000B03 RID: 2819
			public double AggregateChildrenRunTimeSeconds;

			// Token: 0x04000B04 RID: 2820
			public double RunTimeSeconds;
		}
	}
}
