using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FFB RID: 4091
	internal class NotFilter : Filter
	{
		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x06006B41 RID: 27457 RVA: 0x00171681 File Offset: 0x0016F881
		public Filter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x00171689 File Offset: 0x0016F889
		public NotFilter(Filter filter)
		{
			this.filter = filter;
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x00171698 File Offset: 0x0016F898
		public override void Write(StringBuilder builder)
		{
			builder.Append("(!");
			this.Filter.Write(builder);
			builder.Append(")");
		}

		// Token: 0x04003BA7 RID: 15271
		private readonly Filter filter;
	}
}
