using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x0200002F RID: 47
	public sealed class MetricIdentifier : IEquatable<MetricIdentifier>
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009369 File Offset: 0x00007569
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009370 File Offset: 0x00007570
		public static string DefaultMetricNamespace
		{
			get
			{
				return MetricIdentifier.defaultMetricNamespace;
			}
			set
			{
				MetricIdentifier.ValidateLiteral(value, "value", true);
				MetricIdentifier.defaultMetricNamespace = value.Trim();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000938C File Offset: 0x0000758C
		private static void ValidateLiteral(string partValue, string partName, bool allowEmpty)
		{
			if (partValue == null)
			{
				throw new ArgumentNullException(partName);
			}
			if (allowEmpty)
			{
				if (partValue.Length > 0 && string.IsNullOrWhiteSpace(partValue))
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} may not be non-empty, but whitespace-only.", new object[] { partName })));
				}
			}
			else if (string.IsNullOrWhiteSpace(partValue))
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} may not be empty or whitespace-only.", new object[] { partName })));
			}
			int num = partName.IndexOfAny(MetricIdentifier.InvalidMetricChars);
			if (num >= 0)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} (\"{1}\") contains a disallowed character at position {2}.", new object[] { partName, partValue, num })));
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000943C File Offset: 0x0000763C
		public MetricIdentifier(string metricId)
			: this(null, metricId, null, null, null, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000945C File Offset: 0x0000765C
		public MetricIdentifier(string metricNamespace, string metricId)
			: this(metricNamespace, metricId, null, null, null, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000947C File Offset: 0x0000767C
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name)
			: this(metricNamespace, metricId, dimension1Name, null, null, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000949C File Offset: 0x0000769C
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, null, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000094BC File Offset: 0x000076BC
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000094E0 File Offset: 0x000076E0
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name, null, null, null, null, null, null)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009504 File Offset: 0x00007704
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, string dimension5Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name, dimension5Name, null, null, null, null, null)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009528 File Offset: 0x00007728
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, string dimension5Name, string dimension6Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name, dimension5Name, dimension6Name, null, null, null, null)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000954C File Offset: 0x0000774C
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, string dimension5Name, string dimension6Name, string dimension7Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name, dimension5Name, dimension6Name, dimension7Name, null, null, null)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009574 File Offset: 0x00007774
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, string dimension5Name, string dimension6Name, string dimension7Name, string dimension8Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name, dimension5Name, dimension6Name, dimension7Name, dimension8Name, null, null)
		{
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000959C File Offset: 0x0000779C
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, string dimension5Name, string dimension6Name, string dimension7Name, string dimension8Name, string dimension9Name)
			: this(metricNamespace, metricId, dimension1Name, dimension2Name, dimension3Name, dimension4Name, dimension5Name, dimension6Name, dimension7Name, dimension8Name, dimension9Name, null)
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000095C4 File Offset: 0x000077C4
		public MetricIdentifier(string metricNamespace, string metricId, string dimension1Name, string dimension2Name, string dimension3Name, string dimension4Name, string dimension5Name, string dimension6Name, string dimension7Name, string dimension8Name, string dimension9Name, string dimension10Name)
		{
			if (metricNamespace == null)
			{
				metricNamespace = MetricIdentifier.DefaultMetricNamespace;
			}
			else
			{
				MetricIdentifier.ValidateLiteral(metricNamespace, "metricNamespace", true);
				metricNamespace = metricNamespace.Trim();
			}
			MetricIdentifier.ValidateLiteral(metricId, "metricId", false);
			metricId = metricId.Trim();
			int num;
			MetricIdentifier.EnsureDimensionNamesValid(out num, ref dimension1Name, ref dimension2Name, ref dimension3Name, ref dimension4Name, ref dimension5Name, ref dimension6Name, ref dimension7Name, ref dimension8Name, ref dimension9Name, ref dimension10Name);
			this.MetricNamespace = metricNamespace;
			this.MetricId = metricId;
			this.DimensionsCount = num;
			this.dimension1Name = dimension1Name;
			this.dimension2Name = dimension2Name;
			this.dimension3Name = dimension3Name;
			this.dimension4Name = dimension4Name;
			this.dimension5Name = dimension5Name;
			this.dimension6Name = dimension6Name;
			this.dimension7Name = dimension7Name;
			this.dimension8Name = dimension8Name;
			this.dimension9Name = dimension9Name;
			this.dimension10Name = dimension10Name;
			this.identifierString = this.GetIdentifierString();
			this.hashCode = this.identifierString.GetHashCode();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000096A8 File Offset: 0x000078A8
		public MetricIdentifier(string metricNamespace, string metricId, IList<string> dimensionNames)
			: this(metricNamespace, metricId, (dimensionNames != null && dimensionNames.Count > 0) ? dimensionNames[0] : null, (dimensionNames != null && dimensionNames.Count > 1) ? dimensionNames[1] : null, (dimensionNames != null && dimensionNames.Count > 2) ? dimensionNames[2] : null, (dimensionNames != null && dimensionNames.Count > 3) ? dimensionNames[3] : null, (dimensionNames != null && dimensionNames.Count > 4) ? dimensionNames[4] : null, (dimensionNames != null && dimensionNames.Count > 5) ? dimensionNames[5] : null, (dimensionNames != null && dimensionNames.Count > 6) ? dimensionNames[6] : null, (dimensionNames != null && dimensionNames.Count > 7) ? dimensionNames[7] : null, (dimensionNames != null && dimensionNames.Count > 8) ? dimensionNames[8] : null, (dimensionNames != null && dimensionNames.Count > 9) ? dimensionNames[9] : null)
		{
			if (dimensionNames != null && dimensionNames.Count > 10)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("May not have more than {0} dimensions, but {1} has {2} elements.", new object[] { 10, "dimensionNames", dimensionNames.Count })));
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000097E3 File Offset: 0x000079E3
		public string MetricNamespace { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000097EB File Offset: 0x000079EB
		public string MetricId { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000097F3 File Offset: 0x000079F3
		public int DimensionsCount { get; }

		// Token: 0x060001AA RID: 426 RVA: 0x000097FB File Offset: 0x000079FB
		public IEnumerable<string> GetDimensionNames()
		{
			int num;
			for (int d = 1; d <= this.DimensionsCount; d = num + 1)
			{
				yield return this.GetDimensionName(d);
				num = d;
			}
			yield break;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000980C File Offset: 0x00007A0C
		[SuppressMessage("Microsoft.Usage", "CA2233", Justification = "dimensionNumber is validated.")]
		public string GetDimensionName(int dimensionNumber)
		{
			this.ValidateDimensionNumberForGetter(dimensionNumber);
			switch (dimensionNumber)
			{
			case 1:
				return this.dimension1Name;
			case 2:
				return this.dimension2Name;
			case 3:
				return this.dimension3Name;
			case 4:
				return this.dimension4Name;
			case 5:
				return this.dimension5Name;
			case 6:
				return this.dimension6Name;
			case 7:
				return this.dimension7Name;
			case 8:
				return this.dimension8Name;
			case 9:
				return this.dimension9Name;
			case 10:
				return this.dimension10Name;
			default:
				throw new ArgumentOutOfRangeException("dimensionNumber");
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000098A2 File Offset: 0x00007AA2
		public override string ToString()
		{
			return this.identifierString;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000098AA File Offset: 0x00007AAA
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000098B4 File Offset: 0x00007AB4
		public override bool Equals(object otherObj)
		{
			MetricIdentifier metricIdentifier = otherObj as MetricIdentifier;
			if (metricIdentifier != null)
			{
				return this.Equals(metricIdentifier);
			}
			return base.Equals(otherObj);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000098DA File Offset: 0x00007ADA
		public bool Equals(MetricIdentifier otherMetricIdentifier)
		{
			return otherMetricIdentifier != null && this.hashCode == otherMetricIdentifier.hashCode && this.identifierString.Equals(otherMetricIdentifier.identifierString, StringComparison.Ordinal);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009904 File Offset: 0x00007B04
		internal static void ValidateDimensionNumberForGetter(int dimensionNumber, int thisDimensionsCount)
		{
			if (dimensionNumber < 1)
			{
				throw new ArgumentOutOfRangeException("dimensionNumber", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} is an invalid {1}. Note that {2} is a 1-based index.", new object[] { dimensionNumber, "dimensionNumber", "dimensionNumber" })));
			}
			if (dimensionNumber > 10)
			{
				throw new ArgumentOutOfRangeException("dimensionNumber", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} is an invalid {1}.", new object[] { dimensionNumber, "dimensionNumber" })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" Only {0} = 1, 2, ..., {1} are supported.", new object[] { "dimensionNumber", 10 })));
			}
			if (thisDimensionsCount < 1)
			{
				throw new ArgumentOutOfRangeException("dimensionNumber", "Cannot access dimension because this metric has no dimensions.");
			}
			if (dimensionNumber > thisDimensionsCount)
			{
				throw new ArgumentOutOfRangeException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cannot access dimension for {0}={1}", new object[] { "dimensionNumber", dimensionNumber })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" because this metric only has {0} dimensions.", new object[] { thisDimensionsCount })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" Note that {0} is a 1-based index.", new object[] { "dimensionNumber" })));
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009A34 File Offset: 0x00007C34
		internal void ValidateDimensionNumberForGetter(int dimensionNumber)
		{
			MetricIdentifier.ValidateDimensionNumberForGetter(dimensionNumber, this.DimensionsCount);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009A44 File Offset: 0x00007C44
		private static void EnsureDimensionNamesValid(out int dimensionCount, ref string dimension1Name, ref string dimension2Name, ref string dimension3Name, ref string dimension4Name, ref string dimension5Name, ref string dimension6Name, ref string dimension7Name, ref string dimension8Name, ref string dimension9Name, ref string dimension10Name)
		{
			dimensionCount = 0;
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension10Name, 10);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension9Name, 9);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension8Name, 8);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension7Name, 7);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension6Name, 6);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension5Name, 5);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension4Name, 4);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension3Name, 3);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension2Name, 2);
			MetricIdentifier.EnsureDimensionNameValid(ref dimensionCount, ref dimension1Name, 1);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009AB0 File Offset: 0x00007CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void EnsureDimensionNameValid(ref int dimensionCount, ref string dimensionName, int thisDimensionNumber)
		{
			if (dimensionName == null)
			{
				if (dimensionCount != 0)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Name for dimension number {0} may not be omitted,", new object[] { thisDimensionNumber })) + " or may not be null if higher dimensions are present.");
				}
				return;
			}
			else
			{
				dimensionCount = Math.Max(dimensionCount, thisDimensionNumber);
				dimensionName = dimensionName.Trim();
				if (dimensionName.Length == 0)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Name for dimension number {0} may not be empty (or whitespace only).", new object[] { thisDimensionNumber })) + " Dimension names may be 'null' to indicate the absence of a dimension, but if present, they must contain at least 1 printable character.");
				}
				int num = dimensionName.IndexOfAny(MetricIdentifier.InvalidMetricChars);
				if (num >= 0)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Name for dimension number {0} (\"{1}\")", new object[] { thisDimensionNumber, dimensionName })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" contains a disallowed character at position {0}.", new object[] { num })));
				}
				return;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009B9C File Offset: 0x00007D9C
		private string GetIdentifierString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.MetricNamespace.Length > 0)
			{
				stringBuilder.Append(this.MetricNamespace);
			}
			else
			{
				stringBuilder.Append("<NoNamespace>");
			}
			stringBuilder.Append("+");
			stringBuilder.Append(this.MetricId);
			stringBuilder.Append("[");
			stringBuilder.Append(this.DimensionsCount);
			stringBuilder.Append("]");
			stringBuilder.Append("(");
			for (int i = 1; i <= this.DimensionsCount; i++)
			{
				if (i > 1)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append('"');
				stringBuilder.Append(this.GetDimensionName(i));
				stringBuilder.Append('"');
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x040000BB RID: 187
		public const int MaxDimensionsCount = 10;

		// Token: 0x040000BC RID: 188
		private const string NoNamespaceIdentifierStringComponent = "<NoNamespace>";

		// Token: 0x040000BD RID: 189
		private static readonly char[] InvalidMetricChars = new char[]
		{
			'\0', '"', '\'', '(', ')', '[', ']', '{', '}', '<',
			'>', '=', ',', '`', '~', '!', '@', '#', '$', '%',
			'^', '&', '*', '+', '?'
		};

		// Token: 0x040000BE RID: 190
		private static string defaultMetricNamespace = string.Empty;

		// Token: 0x040000BF RID: 191
		private readonly string dimension1Name;

		// Token: 0x040000C0 RID: 192
		private readonly string dimension2Name;

		// Token: 0x040000C1 RID: 193
		private readonly string dimension3Name;

		// Token: 0x040000C2 RID: 194
		private readonly string dimension4Name;

		// Token: 0x040000C3 RID: 195
		private readonly string dimension5Name;

		// Token: 0x040000C4 RID: 196
		private readonly string dimension6Name;

		// Token: 0x040000C5 RID: 197
		private readonly string dimension7Name;

		// Token: 0x040000C6 RID: 198
		private readonly string dimension8Name;

		// Token: 0x040000C7 RID: 199
		private readonly string dimension9Name;

		// Token: 0x040000C8 RID: 200
		private readonly string dimension10Name;

		// Token: 0x040000C9 RID: 201
		private readonly string identifierString;

		// Token: 0x040000CA RID: 202
		private readonly int hashCode;
	}
}
