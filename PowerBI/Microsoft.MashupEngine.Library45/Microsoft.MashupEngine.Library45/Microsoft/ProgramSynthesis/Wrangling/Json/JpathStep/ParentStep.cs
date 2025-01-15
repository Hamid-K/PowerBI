using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000196 RID: 406
	public class ParentStep : JPathStep, IEquatable<ParentStep>
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0001B159 File Offset: 0x00019359
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.Parent;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001B15C File Offset: 0x0001935C
		public override double Score
		{
			get
			{
				return -2.0;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(ParentStep other)
		{
			return true;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001B167 File Offset: 0x00019367
		public override JToken[] Apply(JToken token)
		{
			return new JToken[] { token.Parent };
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001B178 File Offset: 0x00019378
		internal override string Serialize()
		{
			return "..";
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001B178 File Offset: 0x00019378
		public override string ToString()
		{
			return "..";
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001B17F File Offset: 0x0001937F
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ParentStep)obj)));
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001B1AD File Offset: 0x000193AD
		public override int GetHashCode()
		{
			return 17;
		}

		// Token: 0x04000461 RID: 1121
		public const string Symbol = "..";
	}
}
