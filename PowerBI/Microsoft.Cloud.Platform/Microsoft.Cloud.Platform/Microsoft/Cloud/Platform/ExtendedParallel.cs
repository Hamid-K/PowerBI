using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform
{
	// Token: 0x02000011 RID: 17
	public static class ExtendedParallel
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003200 File Offset: 0x00001400
		public static Task ForEachAsync<T>(IEnumerable<T> source, Func<T, Task> body)
		{
			return Task.WhenAll(source.Select((T item) => Task.Run(() => body(item))));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003234 File Offset: 0x00001434
		public static Task ForEachAsync<T>(IEnumerable<T> source, int degreeOfPartitioning, Func<T, Task> body)
		{
			ExtendedParallel.<>c__DisplayClass1_0<T> CS$<>8__locals1 = new ExtendedParallel.<>c__DisplayClass1_0<T>();
			CS$<>8__locals1.body = body;
			return Task.WhenAll(Partitioner.Create<T>(source).GetPartitions(degreeOfPartitioning).Select(delegate(IEnumerator<T> partition)
			{
				ExtendedParallel.<>c__DisplayClass1_1<T> CS$<>8__locals2 = new ExtendedParallel.<>c__DisplayClass1_1<T>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.partition = partition;
				return Task.Run(delegate
				{
					ExtendedParallel.<>c__DisplayClass1_1<T>.<<ForEachAsync>b__1>d <<ForEachAsync>b__1>d;
					<<ForEachAsync>b__1>d.<>4__this = CS$<>8__locals2;
					<<ForEachAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<ForEachAsync>b__1>d.<>1__state = -1;
					AsyncTaskMethodBuilder <>t__builder = <<ForEachAsync>b__1>d.<>t__builder;
					<>t__builder.Start<ExtendedParallel.<>c__DisplayClass1_1<T>.<<ForEachAsync>b__1>d>(ref <<ForEachAsync>b__1>d);
					return <<ForEachAsync>b__1>d.<>t__builder.Task;
				});
			}));
		}
	}
}
