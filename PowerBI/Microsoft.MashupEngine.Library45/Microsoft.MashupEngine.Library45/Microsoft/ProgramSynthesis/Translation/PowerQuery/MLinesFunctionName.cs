using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x0200032C RID: 812
	public readonly struct MLinesFunctionName
	{
		// Token: 0x060011EA RID: 4586 RVA: 0x00034F71 File Offset: 0x00033171
		public MLinesFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00034F7A File Offset: 0x0003317A
		public string Name { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00034F82 File Offset: 0x00033182
		public string QualifiedName
		{
			get
			{
				return "Lines." + this.Name;
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00034F94 File Offset: 0x00033194
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00034F9C File Offset: 0x0003319C
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00034FAC File Offset: 0x000331AC
		public override bool Equals(object obj)
		{
			return obj is MLinesFunctionName && ((MLinesFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008DE RID: 2270
		public static readonly MLinesFunctionName FromBinary = new MLinesFunctionName("FromBinary");

		// Token: 0x040008DF RID: 2271
		public static readonly MLinesFunctionName FromText = new MLinesFunctionName("FromText");
	}
}
