using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Learning
{
	// Token: 0x02001EB3 RID: 7859
	public abstract class TTreeExample<T, U> : IEquatable<TTreeExample<T, U>>
	{
		// Token: 0x17002BFF RID: 11263
		// (get) Token: 0x0601096D RID: 67949 RVA: 0x0039074D File Offset: 0x0038E94D
		public T Input { get; }

		// Token: 0x17002C00 RID: 11264
		// (get) Token: 0x0601096E RID: 67950 RVA: 0x00390755 File Offset: 0x0038E955
		public U Output { get; }

		// Token: 0x17002C01 RID: 11265
		// (get) Token: 0x0601096F RID: 67951 RVA: 0x0039075D File Offset: 0x0038E95D
		public bool IsPositive { get; }

		// Token: 0x06010970 RID: 67952 RVA: 0x00390768 File Offset: 0x0038E968
		public bool Equals(TTreeExample<T, U> other)
		{
			return EqualityComparer<T>.Default.Equals(this.Input, other.Input) && EqualityComparer<U>.Default.Equals(this.Output, other.Output) && this.IsPositive == other.IsPositive;
		}

		// Token: 0x06010971 RID: 67953 RVA: 0x003907B5 File Offset: 0x0038E9B5
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((TTreeExample<T, U>)obj)));
		}

		// Token: 0x06010972 RID: 67954 RVA: 0x003907E4 File Offset: 0x0038E9E4
		public override int GetHashCode()
		{
			return (((EqualityComparer<T>.Default.GetHashCode(this.Input) * 397) ^ EqualityComparer<U>.Default.GetHashCode(this.Output)) * 397) ^ this.IsPositive.GetHashCode();
		}

		// Token: 0x06010973 RID: 67955 RVA: 0x0039082D File Offset: 0x0038EA2D
		public TTreeExample(T input, U output, bool isPositive = true)
		{
			this.Input = input;
			this.Output = output;
			this.IsPositive = isPositive;
		}

		// Token: 0x04006322 RID: 25378
		protected const string IsPositiveAttributeName = "IsPositive";
	}
}
