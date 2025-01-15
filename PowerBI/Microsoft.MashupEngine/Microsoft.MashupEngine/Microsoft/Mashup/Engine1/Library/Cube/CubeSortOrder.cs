using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE6 RID: 3302
	internal sealed class CubeSortOrder
	{
		// Token: 0x06005998 RID: 22936 RVA: 0x001398F1 File Offset: 0x00137AF1
		public CubeSortOrder(CubeExpression expression, bool ascending)
		{
			this.expression = expression;
			this.ascending = ascending;
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x00139907 File Offset: 0x00137B07
		public CubeExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x0600599A RID: 22938 RVA: 0x0013990F File Offset: 0x00137B0F
		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x00139917 File Offset: 0x00137B17
		public bool Equals(CubeSortOrder other)
		{
			return other != null && this.expression.Equals(other.expression) && this.ascending == other.ascending;
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x0013993F File Offset: 0x00137B3F
		public override bool Equals(object other)
		{
			return this.Equals(other as CubeSortOrder);
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x00139950 File Offset: 0x00137B50
		public override int GetHashCode()
		{
			return this.expression.GetHashCode() + 37 * this.ascending.GetHashCode();
		}

		// Token: 0x04003229 RID: 12841
		private readonly CubeExpression expression;

		// Token: 0x0400322A RID: 12842
		private readonly bool ascending;
	}
}
