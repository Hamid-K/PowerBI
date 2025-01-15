using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x0200032B RID: 811
	public readonly struct MListFunctionName
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x00034E84 File Offset: 0x00033084
		public MListFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00034E8D File Offset: 0x0003308D
		public string Name { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00034E95 File Offset: 0x00033095
		public string QualifiedName
		{
			get
			{
				return "List." + this.Name;
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00034EA7 File Offset: 0x000330A7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00034EAF File Offset: 0x000330AF
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00034EBC File Offset: 0x000330BC
		public override bool Equals(object obj)
		{
			return obj is MListFunctionName && ((MListFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008D5 RID: 2261
		public static readonly MListFunctionName Accumulate = new MListFunctionName("Accumulate");

		// Token: 0x040008D6 RID: 2262
		public static readonly MListFunctionName Count = new MListFunctionName("Count");

		// Token: 0x040008D7 RID: 2263
		public static readonly MListFunctionName Contains = new MListFunctionName("Contains");

		// Token: 0x040008D8 RID: 2264
		public static readonly MListFunctionName First = new MListFunctionName("First");

		// Token: 0x040008D9 RID: 2265
		public static readonly MListFunctionName FirstN = new MListFunctionName("FirstN");

		// Token: 0x040008DA RID: 2266
		public static readonly MListFunctionName Last = new MListFunctionName("Last");

		// Token: 0x040008DB RID: 2267
		public static readonly MListFunctionName LastN = new MListFunctionName("LastN");

		// Token: 0x040008DC RID: 2268
		public static readonly MListFunctionName Transform = new MListFunctionName("Transform");
	}
}
