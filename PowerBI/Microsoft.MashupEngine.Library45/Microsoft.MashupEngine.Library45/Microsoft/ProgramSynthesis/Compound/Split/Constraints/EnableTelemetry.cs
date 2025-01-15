using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009EF RID: 2543
	public class EnableTelemetry : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D5F RID: 15711 RVA: 0x000C0394 File Offset: 0x000BE594
		public EnableTelemetry(HashSet<string> symbols = null, int lineCount = 10)
		{
			HashSet<string> hashSet = symbols;
			if (symbols == null)
			{
				HashSet<string> hashSet2 = new HashSet<string>();
				hashSet2.Add(",");
				hashSet2.Add("\t");
				hashSet2.Add(" ");
				hashSet2.Add(";");
				hashSet2.Add("|");
				hashSet2.Add("\"");
				hashSet = hashSet2;
				hashSet2.Add("'");
			}
			this.TrackedSymbols = hashSet;
			this.TelemetryLineCount = lineCount;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000C0412 File Offset: 0x000BE612
		public HashSet<string> TrackedSymbols { get; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003D61 RID: 15713 RVA: 0x000C041A File Offset: 0x000BE61A
		public int TelemetryLineCount { get; }

		// Token: 0x06003D62 RID: 15714 RVA: 0x000C0422 File Offset: 0x000BE622
		public void SetOptions(Options options)
		{
			options.EnableTelemetry = true;
			options.TelemetryTrackSymbols.UnionWith(this.TrackedSymbols);
			options.TelemetryLineCount = this.TelemetryLineCount;
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x000C0448 File Offset: 0x000BE648
		protected bool Equals(EnableTelemetry other)
		{
			return this.TrackedSymbols.SetEquals(other.TrackedSymbols) && this.TelemetryLineCount == other.TelemetryLineCount;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000C046D File Offset: 0x000BE66D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((EnableTelemetry)obj)));
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x000C049B File Offset: 0x000BE69B
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return this.Equals(other as EnableTelemetry);
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x000C04AC File Offset: 0x000BE6AC
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			EnableTelemetry enableTelemetry = other as EnableTelemetry;
			return !(enableTelemetry == null) && !object.Equals(this, enableTelemetry);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x000C04D5 File Offset: 0x000BE6D5
		public override int GetHashCode()
		{
			return (((619 * 31541) ^ this.TrackedSymbols.OrderIndependentHashCode<string>()) * 4157) ^ this.TelemetryLineCount;
		}
	}
}
