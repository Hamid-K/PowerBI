using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F3 RID: 243
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class FrameTree
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00008A16 File Offset: 0x00006C16
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00008A1E File Offset: 0x00006C1E
		[DataMember(IsRequired = true, Order = 10)]
		public ParseFrame RootFrame { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00008A27 File Offset: 0x00006C27
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00008A2F File Offset: 0x00006C2F
		[DataMember(IsRequired = false, Order = 20)]
		public string AlteredUtterance { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00008A38 File Offset: 0x00006C38
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00008A40 File Offset: 0x00006C40
		public string OriginalUtterance { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00008A49 File Offset: 0x00006C49
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00008A51 File Offset: 0x00006C51
		public object SyntaxFrame { get; set; }

		// Token: 0x060004B3 RID: 1203 RVA: 0x00008A5C File Offset: 0x00006C5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("Frame: ");
			stringBuilder.AppendLine(this.RootFrame.ToString(string.IsNullOrEmpty(this.AlteredUtterance) ? this.OriginalUtterance : this.AlteredUtterance));
			if (!string.IsNullOrEmpty(this.AlteredUtterance))
			{
				stringBuilder.Append("AlternateUtterance: ");
				stringBuilder.AppendLine(this.AlteredUtterance);
			}
			return stringBuilder.ToString();
		}
	}
}
