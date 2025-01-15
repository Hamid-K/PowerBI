﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000D1 RID: 209
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002EF5D File Offset: 0x0002D15D
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0002EF65 File Offset: 0x0002D165
		public int? Start { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002EF6E File Offset: 0x0002D16E
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0002EF76 File Offset: 0x0002D176
		public int? End { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002EF7F File Offset: 0x0002D17F
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0002EF87 File Offset: 0x0002D187
		public int? Step { get; set; }

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002EF90 File Offset: 0x0002D190
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			int? num = this.Step;
			int num2 = 0;
			if ((num.GetValueOrDefault() == num2) & (num != null))
			{
				throw new JsonException("Step cannot be zero.");
			}
			foreach (JToken jtoken in current)
			{
				JArray a = jtoken as JArray;
				if (a != null)
				{
					int stepCount = this.Step.GetValueOrDefault(1);
					int num3 = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
					int stopIndex = this.End ?? ((stepCount > 0) ? a.Count : (-1));
					num = this.Start;
					num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						num3 = a.Count + num3;
					}
					num = this.End;
					num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						stopIndex = a.Count + stopIndex;
					}
					num3 = Math.Max(num3, (stepCount > 0) ? 0 : int.MinValue);
					num3 = Math.Min(num3, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = Math.Max(stopIndex, -1);
					stopIndex = Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (this.IsValid(num3, stopIndex, positiveStep))
					{
						int i = num3;
						while (this.IsValid(i, stopIndex, positiveStep))
						{
							yield return a[i];
							i += stepCount;
						}
					}
					else if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
				a = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002EFAE File Offset: 0x0002D1AE
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
