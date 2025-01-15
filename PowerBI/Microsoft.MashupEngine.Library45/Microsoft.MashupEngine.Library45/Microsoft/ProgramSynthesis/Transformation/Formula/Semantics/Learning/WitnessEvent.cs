using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001614 RID: 5652
	public class WitnessEvent
	{
		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x0600BC5C RID: 48220 RVA: 0x002881A9 File Offset: 0x002863A9
		// (set) Token: 0x0600BC5D RID: 48221 RVA: 0x002881B1 File Offset: 0x002863B1
		public IEnumerable<object> ArgumentInputs { get; set; }

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x0600BC5E RID: 48222 RVA: 0x002881BA File Offset: 0x002863BA
		// (set) Token: 0x0600BC5F RID: 48223 RVA: 0x002881C2 File Offset: 0x002863C2
		public object DependentArg1 { get; set; }

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x0600BC60 RID: 48224 RVA: 0x002881CB File Offset: 0x002863CB
		// (set) Token: 0x0600BC61 RID: 48225 RVA: 0x002881D3 File Offset: 0x002863D3
		public object DependentArg2 { get; set; }

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x0600BC62 RID: 48226 RVA: 0x002881DC File Offset: 0x002863DC
		// (set) Token: 0x0600BC63 RID: 48227 RVA: 0x002881E4 File Offset: 0x002863E4
		public object DependentArg3 { get; set; }

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x0600BC64 RID: 48228 RVA: 0x002881ED File Offset: 0x002863ED
		// (set) Token: 0x0600BC65 RID: 48229 RVA: 0x002881F5 File Offset: 0x002863F5
		public IRow InputRow { get; set; }

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x0600BC66 RID: 48230 RVA: 0x002881FE File Offset: 0x002863FE
		// (set) Token: 0x0600BC67 RID: 48231 RVA: 0x00288206 File Offset: 0x00286406
		public int MethodOrder { get; set; }

		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x0600BC68 RID: 48232 RVA: 0x0028820F File Offset: 0x0028640F
		// (set) Token: 0x0600BC69 RID: 48233 RVA: 0x00288217 File Offset: 0x00286417
		public object OperatorOutput { get; set; }

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x0600BC6A RID: 48234 RVA: 0x00288220 File Offset: 0x00286420
		// (set) Token: 0x0600BC6B RID: 48235 RVA: 0x00288228 File Offset: 0x00286428
		public int Order { get; set; }

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x0600BC6C RID: 48236 RVA: 0x00288231 File Offset: 0x00286431
		// (set) Token: 0x0600BC6D RID: 48237 RVA: 0x00288239 File Offset: 0x00286439
		public int? Tier { get; set; }

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x0600BC6E RID: 48238 RVA: 0x00288242 File Offset: 0x00286442
		// (set) Token: 0x0600BC6F RID: 48239 RVA: 0x0028824A File Offset: 0x0028644A
		public string WitnessName { get; set; }

		// Token: 0x0600BC70 RID: 48240 RVA: 0x00288254 File Offset: 0x00286454
		public override string ToString()
		{
			IEnumerable<object> argumentInputs = this.ArgumentInputs;
			List<object> list = ((argumentInputs != null) ? argumentInputs.ToList<object>() : null);
			string text;
			if (list == null)
			{
				text = "null";
			}
			else if (!list.Any<object>())
			{
				text = "[]";
			}
			else
			{
				object obj = list.First<object>();
				string text2;
				if (!(obj is Record<int, int>))
				{
					if (!(obj is Record<string, int>))
					{
						text2 = WitnessEvent.<ToString>g__Format|40_1(list);
					}
					else
					{
						text2 = WitnessEvent.<ToString>g__FormatRecords|40_0<string, int>(list.Cast<Record<string, int>>());
					}
				}
				else
				{
					text2 = WitnessEvent.<ToString>g__FormatRecords|40_0<int, int>(list.Cast<Record<int, int>>());
				}
				text = text2;
			}
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = new string(' ', 23);
			if (this.DependentArg1 != null)
			{
				text3 = Environment.NewLine + text6 + "Dep1: " + this.DependentArg1.ToLiteral(null);
			}
			if (this.DependentArg2 != null)
			{
				text4 = Environment.NewLine + text6 + "Dep2: " + this.DependentArg2.ToLiteral(null);
			}
			if (this.DependentArg3 != null)
			{
				text5 = Environment.NewLine + text6 + "Dep3: " + this.DependentArg3.ToLiteral(null);
			}
			return string.Concat(new string[]
			{
				this.OperatorOutput.ToLiteral(null),
				" -> ",
				text,
				text3,
				text4,
				text5
			});
		}

		// Token: 0x0600BC72 RID: 48242 RVA: 0x0028838C File Offset: 0x0028658C
		[CompilerGenerated]
		internal static string <ToString>g__FormatRecords|40_0<T1, T2>(IEnumerable<Record<T1, T2>> records)
		{
			return "[" + string.Join(",", records.Select((Record<T1, T2> i) => i.ToLiteral(null))) + "]";
		}

		// Token: 0x0600BC73 RID: 48243 RVA: 0x002883C0 File Offset: 0x002865C0
		[CompilerGenerated]
		internal static string <ToString>g__Format|40_1(IReadOnlyCollection<object> items)
		{
			if (!items.Any<object>())
			{
				return "[]";
			}
			string text = string.Join(",", items.Select((object i) => i.ToLiteral(null)));
			if (items.Count != 1)
			{
				return "[" + text + "]";
			}
			return text;
		}
	}
}
