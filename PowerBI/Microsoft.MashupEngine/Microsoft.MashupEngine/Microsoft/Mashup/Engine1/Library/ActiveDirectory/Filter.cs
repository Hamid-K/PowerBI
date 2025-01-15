using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FF7 RID: 4087
	internal abstract class Filter
	{
		// Token: 0x06006B36 RID: 27446
		public abstract void Write(StringBuilder builder);

		// Token: 0x06006B37 RID: 27447 RVA: 0x00171528 File Offset: 0x0016F728
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.Write(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06006B38 RID: 27448 RVA: 0x00171548 File Offset: 0x0016F748
		public override bool Equals(object obj)
		{
			return base.GetType().IsInstanceOfType(obj) && obj.ToString() == this.ToString();
		}

		// Token: 0x06006B39 RID: 27449 RVA: 0x0017156B File Offset: 0x0016F76B
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
	}
}
