using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class Extension
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000067E5 File Offset: 0x000049E5
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000067ED File Offset: 0x000049ED
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000067F6 File Offset: 0x000049F6
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000067FE File Offset: 0x000049FE
		public string Class
		{
			get
			{
				return this.m_class;
			}
			set
			{
				this.m_class = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006807 File Offset: 0x00004A07
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000680F File Offset: 0x00004A0F
		public string Assembly
		{
			get
			{
				return this.m_assembly;
			}
			set
			{
				this.m_assembly = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006818 File Offset: 0x00004A18
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00006820 File Offset: 0x00004A20
		public string Configuration
		{
			get
			{
				return this.m_configuration;
			}
			set
			{
				this.m_configuration = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006829 File Offset: 0x00004A29
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00006831 File Offset: 0x00004A31
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000683A File Offset: 0x00004A3A
		public string ClassAndAssembly
		{
			get
			{
				return this.Class + "," + this.Assembly;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006852 File Offset: 0x00004A52
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000685A File Offset: 0x00004A5A
		public bool Visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006863 File Offset: 0x00004A63
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000686B File Offset: 0x00004A6B
		public bool LogAllExecutionRequests
		{
			get
			{
				return this.m_logAllExecutionRequests;
			}
			set
			{
				this.m_logAllExecutionRequests = value;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006874 File Offset: 0x00004A74
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000687C File Offset: 0x00004A7C
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

		// Token: 0x040000C3 RID: 195
		private string m_name = "";

		// Token: 0x040000C4 RID: 196
		private string m_class = "";

		// Token: 0x040000C5 RID: 197
		private string m_assembly = "";

		// Token: 0x040000C6 RID: 198
		private string m_configuration = "";

		// Token: 0x040000C7 RID: 199
		private string m_type = "";

		// Token: 0x040000C8 RID: 200
		private bool m_visible = true;

		// Token: 0x040000C9 RID: 201
		private bool m_logAllExecutionRequests = true;
	}
}
