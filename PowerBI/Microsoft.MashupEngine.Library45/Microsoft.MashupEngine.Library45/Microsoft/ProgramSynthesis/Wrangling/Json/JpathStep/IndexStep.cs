using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000193 RID: 403
	public class IndexStep : JPathStep, IEquatable<IndexStep>
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0001AFB1 File Offset: 0x000191B1
		public IndexStep(int k)
		{
			if (k < 0)
			{
				throw new InvalidOperationException("Expect non-negative index!");
			}
			this.K = k;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001AFCF File Offset: 0x000191CF
		public int K { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0001AFD7 File Offset: 0x000191D7
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.Index;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001AFDA File Offset: 0x000191DA
		public override double Score
		{
			get
			{
				return (double)(-(double)this.K);
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001AFE4 File Offset: 0x000191E4
		public bool Equals(IndexStep other)
		{
			return other != null && (this == other || this.K == other.K);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001AFFF File Offset: 0x000191FF
		internal override string Serialize()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { this.K }));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001B024 File Offset: 0x00019224
		public override JToken[] Apply(JToken token)
		{
			JArray jarray = token as JArray;
			if (jarray == null || this.K >= jarray.Count)
			{
				return new JToken[0];
			}
			return new JToken[] { jarray[this.K] };
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001AFFF File Offset: 0x000191FF
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { this.K }));
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001B065 File Offset: 0x00019265
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((IndexStep)obj)));
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001B093 File Offset: 0x00019293
		public override int GetHashCode()
		{
			return this.K;
		}
	}
}
