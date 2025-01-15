using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public class FuzzyLookupBuilderStatistics : IDeserializationCallback
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00009D8F File Offset: 0x00007F8F
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.CreateDomainsTime = new Stopwatch();
			this.EditTransformationPrecomputationTime = new Stopwatch();
			this.TuneFuzzyLookupTime = new Stopwatch();
			this.PopulateFuzzyLookupTime = new Stopwatch();
			this.PopulateFrequencyOracleTime = new Stopwatch();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public void Reset()
		{
			this.CreateDomainsTime.Reset();
			this.EditTransformationPrecomputationTime.Reset();
			this.TuneFuzzyLookupTime.Reset();
			this.PopulateFuzzyLookupTime.Reset();
			this.PopulateFrequencyOracleTime.Reset();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009E04 File Offset: 0x00008004
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("CreateDomainsTime: {0:F3} ms", this.CreateDomainsTime.Elapsed.TotalMilliseconds));
			stringBuilder.AppendLine(string.Format("EditTransformationPrecomputationTime: {0:F3} ms", this.EditTransformationPrecomputationTime.Elapsed.TotalMilliseconds));
			stringBuilder.AppendLine(string.Format("TuneFuzzyLookupTime: {0:F3} ms", this.TuneFuzzyLookupTime.Elapsed.TotalMilliseconds));
			stringBuilder.AppendLine(string.Format("PopulateFuzzyLookupTime: {0:F3} ms", this.PopulateFuzzyLookupTime.Elapsed.TotalMilliseconds));
			stringBuilder.AppendLine(string.Format("PopulateFrequencyOracleTime: {0:F3} ms", this.PopulateFrequencyOracleTime.Elapsed.TotalMilliseconds));
			return stringBuilder.ToString();
		}

		// Token: 0x04000098 RID: 152
		[NonSerialized]
		public Stopwatch CreateDomainsTime = new Stopwatch();

		// Token: 0x04000099 RID: 153
		[NonSerialized]
		public Stopwatch EditTransformationPrecomputationTime = new Stopwatch();

		// Token: 0x0400009A RID: 154
		[NonSerialized]
		public Stopwatch TuneFuzzyLookupTime = new Stopwatch();

		// Token: 0x0400009B RID: 155
		[NonSerialized]
		public Stopwatch PopulateFuzzyLookupTime = new Stopwatch();

		// Token: 0x0400009C RID: 156
		[NonSerialized]
		public Stopwatch PopulateFrequencyOracleTime = new Stopwatch();
	}
}
