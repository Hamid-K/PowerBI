using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class Extension
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000041A6 File Offset: 0x000023A6
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000041AE File Offset: 0x000023AE
		public string Name { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000041B7 File Offset: 0x000023B7
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000041BF File Offset: 0x000023BF
		public string Class { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000041C8 File Offset: 0x000023C8
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000041D0 File Offset: 0x000023D0
		public string Assembly { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000041D9 File Offset: 0x000023D9
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000041E1 File Offset: 0x000023E1
		public string Configuration { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000041EA File Offset: 0x000023EA
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000041F2 File Offset: 0x000023F2
		public string Type { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000041FB File Offset: 0x000023FB
		public string ClassAndAssembly
		{
			get
			{
				return this.Class + "," + this.Assembly;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004213 File Offset: 0x00002413
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000421B File Offset: 0x0000241B
		public bool Visible { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004224 File Offset: 0x00002424
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000422C File Offset: 0x0000242C
		public bool LogAllExecutionRequests { get; set; }

		// Token: 0x060000CE RID: 206 RVA: 0x00004235 File Offset: 0x00002435
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004240 File Offset: 0x00002440
		public override bool Equals(object extension)
		{
			bool flag = true;
			if (!(extension is Extension))
			{
				return false;
			}
			Extension extension2 = (Extension)extension;
			if (this.Assembly != extension2.Assembly || this.Class != extension2.Class || this.Configuration != extension2.Configuration)
			{
				flag = false;
			}
			return flag;
		}
	}
}
