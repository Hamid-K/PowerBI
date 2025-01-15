using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x02000329 RID: 809
	public readonly struct MTextFunctionName
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x00034C15 File Offset: 0x00032E15
		public MTextFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00034C1E File Offset: 0x00032E1E
		public string Name { get; }

		// Token: 0x060011D5 RID: 4565 RVA: 0x00034C26 File Offset: 0x00032E26
		public string Invoke(IReadOnlyList<string> args)
		{
			return this.QualifiedName + "(" + string.Join(", ", args) + ")";
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00034C48 File Offset: 0x00032E48
		public string Invoke(params string[] args)
		{
			return this.Invoke(args);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00034C51 File Offset: 0x00032E51
		public string QualifiedName
		{
			get
			{
				return "Text." + this.Name;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00034C63 File Offset: 0x00032E63
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00034C6B File Offset: 0x00032E6B
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00034C78 File Offset: 0x00032E78
		public override bool Equals(object obj)
		{
			return obj is MTextFunctionName && ((MTextFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008BC RID: 2236
		public static readonly MTextFunctionName AfterDelimiter = new MTextFunctionName("AfterDelimiter");

		// Token: 0x040008BD RID: 2237
		public static readonly MTextFunctionName BeforeDelimiter = new MTextFunctionName("BeforeDelimiter");

		// Token: 0x040008BE RID: 2238
		public static readonly MTextFunctionName BetweenDelimiters = new MTextFunctionName("BetweenDelimiters");

		// Token: 0x040008BF RID: 2239
		public static readonly MTextFunctionName Combine = new MTextFunctionName("Combine");

		// Token: 0x040008C0 RID: 2240
		public static readonly MTextFunctionName End = new MTextFunctionName("End");

		// Token: 0x040008C1 RID: 2241
		public static readonly MTextFunctionName EndsWith = new MTextFunctionName("EndsWith");

		// Token: 0x040008C2 RID: 2242
		public static readonly MTextFunctionName Length = new MTextFunctionName("Length");

		// Token: 0x040008C3 RID: 2243
		public static readonly MTextFunctionName Lower = new MTextFunctionName("Lower");

		// Token: 0x040008C4 RID: 2244
		public static readonly MTextFunctionName Middle = new MTextFunctionName("Middle");

		// Token: 0x040008C5 RID: 2245
		public static readonly MTextFunctionName PositionOfAny = new MTextFunctionName("PositionOfAny");

		// Token: 0x040008C6 RID: 2246
		public static readonly MTextFunctionName Proper = new MTextFunctionName("Proper");

		// Token: 0x040008C7 RID: 2247
		public static readonly MTextFunctionName Range = new MTextFunctionName("Range");

		// Token: 0x040008C8 RID: 2248
		public static readonly MTextFunctionName Replace = new MTextFunctionName("Replace");

		// Token: 0x040008C9 RID: 2249
		public static readonly MTextFunctionName Split = new MTextFunctionName("Split");

		// Token: 0x040008CA RID: 2250
		public static readonly MTextFunctionName Start = new MTextFunctionName("Start");

		// Token: 0x040008CB RID: 2251
		public static readonly MTextFunctionName StartsWith = new MTextFunctionName("StartsWith");

		// Token: 0x040008CC RID: 2252
		public static readonly MTextFunctionName Trim = new MTextFunctionName("Trim");

		// Token: 0x040008CD RID: 2253
		public static readonly MTextFunctionName Upper = new MTextFunctionName("Upper");
	}
}
