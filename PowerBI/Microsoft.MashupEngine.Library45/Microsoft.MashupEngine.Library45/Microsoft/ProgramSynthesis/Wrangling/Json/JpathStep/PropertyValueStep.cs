using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000198 RID: 408
	public class PropertyValueStep : JPathStep, IEquatable<PropertyValueStep>
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001B224 File Offset: 0x00019424
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.PropertyValue;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001AF59 File Offset: 0x00019159
		public override double Score
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(PropertyValueStep other)
		{
			return true;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001B228 File Offset: 0x00019428
		public override JToken[] Apply(JToken token)
		{
			JProperty jproperty = token as JProperty;
			if (jproperty == null)
			{
				return new JToken[0];
			}
			return new JToken[] { jproperty.Value };
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001B255 File Offset: 0x00019455
		internal override string Serialize()
		{
			return "#";
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001B255 File Offset: 0x00019455
		public override string ToString()
		{
			return "#";
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001B25C File Offset: 0x0001945C
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PropertyValueStep)obj)));
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001B28A File Offset: 0x0001948A
		public override int GetHashCode()
		{
			return 379;
		}

		// Token: 0x04000463 RID: 1123
		public const string Symbol = "#";
	}
}
