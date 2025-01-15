using System;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F6 RID: 1526
	internal abstract class InternalBase
	{
		// Token: 0x06004A91 RID: 19089
		internal abstract void ToCompactString(StringBuilder builder);

		// Token: 0x06004A92 RID: 19090 RVA: 0x001085D8 File Offset: 0x001067D8
		internal virtual void ToFullString(StringBuilder builder)
		{
			this.ToCompactString(builder);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x001085E4 File Offset: 0x001067E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x00108604 File Offset: 0x00106804
		internal virtual string ToFullString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToFullString(stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
