using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Json
{
	// Token: 0x02000189 RID: 393
	public class ParsedJson
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0001A19E File Offset: 0x0001839E
		internal ParsedJson(IReadOnlyList<JsonRegion> regions, string startDelimiter, string endDelimiter, JsonErrors errors)
		{
			this.Regions = regions;
			this.IsDelimitedJson = true;
			this.StartDelimiter = startDelimiter;
			this.EndDelimiter = endDelimiter;
			this.Errors = errors;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001A1CA File Offset: 0x000183CA
		internal ParsedJson(JsonRegion region, string startDelimiter, string endDelimiter, JsonErrors errors = JsonErrors.None)
			: this(new JsonRegion[] { region }, startDelimiter, endDelimiter, errors)
		{
			this.IsDelimitedJson = false;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001A1E7 File Offset: 0x000183E7
		public IReadOnlyList<JsonRegion> Regions { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001A1EF File Offset: 0x000183EF
		public JsonErrors Errors { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001A1F7 File Offset: 0x000183F7
		public bool IsDelimitedJson { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001A1FF File Offset: 0x000183FF
		public string EndDelimiter { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001A207 File Offset: 0x00018407
		public string StartDelimiter { get; }

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001A210 File Offset: 0x00018410
		protected bool Equals(ParsedJson other)
		{
			return this.Regions.SequenceEqual(other.Regions) && this.IsDelimitedJson == other.IsDelimitedJson && string.Equals(this.EndDelimiter, other.EndDelimiter) && string.Equals(this.StartDelimiter, other.StartDelimiter) && this.Errors == other.Errors;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001A274 File Offset: 0x00018474
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ParsedJson)obj)));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001A2A4 File Offset: 0x000184A4
		public override int GetHashCode()
		{
			int num = ((this.Regions.OrderDependentHashCode<JsonRegion>() * 233) ^ this.IsDelimitedJson.GetHashCode()) * 29;
			string endDelimiter = this.EndDelimiter;
			int num2 = (num ^ ((endDelimiter != null) ? endDelimiter.GetHashCode() : 0)) * 191;
			string startDelimiter = this.StartDelimiter;
			return ((num2 ^ ((startDelimiter != null) ? startDelimiter.GetHashCode() : 0)) * 167) ^ this.Errors.GetHashCode();
		}
	}
}
