using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000197 RID: 407
	public class PropertyKeyStep : JPathStep, IEquatable<PropertyKeyStep>
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001B1B1 File Offset: 0x000193B1
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.PropertyKey;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0001AF59 File Offset: 0x00019159
		public override double Score
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(PropertyKeyStep other)
		{
			return true;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001B1B4 File Offset: 0x000193B4
		public override JToken[] Apply(JToken token)
		{
			JProperty jproperty = token as JProperty;
			if (jproperty == null)
			{
				return new JToken[0];
			}
			return new JValue[]
			{
				new JValue(jproperty.Name)
			};
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001B1E8 File Offset: 0x000193E8
		internal override string Serialize()
		{
			return "@";
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001B1E8 File Offset: 0x000193E8
		public override string ToString()
		{
			return "@";
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001B1EF File Offset: 0x000193EF
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PropertyKeyStep)obj)));
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001B21D File Offset: 0x0001941D
		public override int GetHashCode()
		{
			return 967;
		}

		// Token: 0x04000462 RID: 1122
		public const string Symbol = "@";
	}
}
