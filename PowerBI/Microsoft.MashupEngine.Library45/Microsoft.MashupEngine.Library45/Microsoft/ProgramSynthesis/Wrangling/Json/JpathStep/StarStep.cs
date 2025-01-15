using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x0200019A RID: 410
	public class StarStep : JPathStep, IEquatable<StarStep>
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001B319 File Offset: 0x00019519
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.Star;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001AF59 File Offset: 0x00019159
		public override double Score
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(StarStep other)
		{
			return true;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001B31C File Offset: 0x0001951C
		public override JToken[] Apply(JToken token)
		{
			if (token is JObject)
			{
				return token.Children().ToArray<JToken>();
			}
			JArray jarray = token as JArray;
			return ((jarray != null) ? jarray.Children().ToArray<JToken>() : null) ?? new JToken[0];
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001B368 File Offset: 0x00019568
		internal override string Serialize()
		{
			return "*";
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001B368 File Offset: 0x00019568
		public override string ToString()
		{
			return "*";
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001B36F File Offset: 0x0001956F
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((StarStep)obj)));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001B39D File Offset: 0x0001959D
		public override int GetHashCode()
		{
			return 19;
		}

		// Token: 0x04000465 RID: 1125
		public const string Symbol = "*";
	}
}
