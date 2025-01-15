using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000192 RID: 402
	public class CurrentStep : JPathStep, IEquatable<CurrentStep>
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.Current;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001AF59 File Offset: 0x00019159
		public override double Score
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001AF64 File Offset: 0x00019164
		internal override string Serialize()
		{
			return ".";
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001AF6B File Offset: 0x0001916B
		public override JToken[] Apply(JToken token)
		{
			return new JToken[] { token };
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(CurrentStep other)
		{
			return true;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001AF64 File Offset: 0x00019164
		public override string ToString()
		{
			return ".";
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001AF77 File Offset: 0x00019177
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((CurrentStep)obj)));
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001AFA5 File Offset: 0x000191A5
		public override int GetHashCode()
		{
			return 47;
		}

		// Token: 0x04000456 RID: 1110
		public const string Symbol = ".";
	}
}
