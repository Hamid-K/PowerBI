using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000199 RID: 409
	public class SinglePropertyStep : JPathStep, IEquatable<SinglePropertyStep>
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001B291 File Offset: 0x00019491
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.SingleProperty;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001AF59 File Offset: 0x00019159
		public override double Score
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(SinglePropertyStep other)
		{
			return true;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001B294 File Offset: 0x00019494
		public override JToken[] Apply(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null || jobject.Count != 1)
			{
				return new JToken[0];
			}
			JProperty jproperty = jobject.First as JProperty;
			if (jproperty == null)
			{
				return new JToken[0];
			}
			return new JToken[] { jproperty.Value };
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001B2E0 File Offset: 0x000194E0
		internal override string Serialize()
		{
			return "0";
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001B2E0 File Offset: 0x000194E0
		public override string ToString()
		{
			return "0";
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001B2E7 File Offset: 0x000194E7
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SinglePropertyStep)obj)));
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001B315 File Offset: 0x00019515
		public override int GetHashCode()
		{
			return 37;
		}

		// Token: 0x04000464 RID: 1124
		public const string Symbol = "0";
	}
}
