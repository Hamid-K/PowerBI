using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x02001764 RID: 5988
	public abstract class OutputToken : IEquatable<OutputToken>
	{
		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x0600C6A5 RID: 50853 RVA: 0x002ABB93 File Offset: 0x002A9D93
		// (set) Token: 0x0600C6A6 RID: 50854 RVA: 0x002ABB9B File Offset: 0x002A9D9B
		public int FirstIndex { get; set; }

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x0600C6A7 RID: 50855 RVA: 0x002ABBA4 File Offset: 0x002A9DA4
		public int LastIndex
		{
			get
			{
				return this.FirstIndex + this.Output.Length - 1;
			}
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x0600C6A8 RID: 50856 RVA: 0x002ABBBA File Offset: 0x002A9DBA
		// (set) Token: 0x0600C6A9 RID: 50857 RVA: 0x002ABBC2 File Offset: 0x002A9DC2
		public string Output { get; set; }

		// Token: 0x0600C6AA RID: 50858 RVA: 0x002ABBCB File Offset: 0x002A9DCB
		public bool Equals(OutputToken other)
		{
			return other != null && (this == other || (this.FirstIndex == other.FirstIndex && this.Output == other.Output));
		}

		// Token: 0x0600C6AB RID: 50859 RVA: 0x002ABBF9 File Offset: 0x002A9DF9
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj.GetType() == base.GetType() && this.Equals((OutputToken)obj)));
		}

		// Token: 0x0600C6AC RID: 50860 RVA: 0x002ABC27 File Offset: 0x002A9E27
		public override int GetHashCode()
		{
			return (this.FirstIndex * 397) ^ ((this.Output != null) ? this.Output.GetHashCode() : 0);
		}

		// Token: 0x0600C6AD RID: 50861
		public abstract bool IsCompatible(OutputToken otherToken);

		// Token: 0x0600C6AE RID: 50862 RVA: 0x002ABC4C File Offset: 0x002A9E4C
		public override string ToString()
		{
			return string.Format("[{0}..{1}] {2}", this.FirstIndex, this.LastIndex, base.GetType().Name.Replace("OutputToken", string.Empty));
		}
	}
}
