using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012F7 RID: 4855
	internal abstract class FieldAccessValueReference : IValueReference
	{
		// Token: 0x0600806C RID: 32876 RVA: 0x001B6833 File Offset: 0x001B4A33
		public FieldAccessValueReference(IValueReference reference, string field)
		{
			this.reference = reference;
			this.field = field;
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x0600806D RID: 32877 RVA: 0x001B6849 File Offset: 0x001B4A49
		public bool Evaluated
		{
			get
			{
				return this.field == null;
			}
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x0600806E RID: 32878 RVA: 0x001B6854 File Offset: 0x001B4A54
		public Value Value
		{
			get
			{
				if (this.field != null)
				{
					this.reference = this.GetValue();
					this.field = null;
				}
				return this.reference.Value;
			}
		}

		// Token: 0x0600806F RID: 32879
		protected abstract Value GetValue();

		// Token: 0x040045F2 RID: 17906
		protected IValueReference reference;

		// Token: 0x040045F3 RID: 17907
		protected string field;
	}
}
