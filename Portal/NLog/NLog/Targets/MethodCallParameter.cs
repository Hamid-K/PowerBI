using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000047 RID: 71
	[NLogConfigurationItem]
	public class MethodCallParameter
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x00011C75 File Offset: 0x0000FE75
		public MethodCallParameter()
		{
			this.ParameterType = typeof(string);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00011C8D File Offset: 0x0000FE8D
		public MethodCallParameter(Layout layout)
		{
			this.ParameterType = typeof(string);
			this.Layout = layout;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public MethodCallParameter(string parameterName, Layout layout)
		{
			this.ParameterType = typeof(string);
			this.Name = parameterName;
			this.Layout = layout;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00011CD2 File Offset: 0x0000FED2
		public MethodCallParameter(string name, Layout layout, Type type)
		{
			this.ParameterType = type;
			this.Name = name;
			this.Layout = layout;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00011CEF File Offset: 0x0000FEEF
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		public string Name { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00011D00 File Offset: 0x0000FF00
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00011D08 File Offset: 0x0000FF08
		[Obsolete("Use property ParameterType instead. Marked obsolete on NLog 4.6")]
		public Type Type
		{
			get
			{
				return this.ParameterType;
			}
			set
			{
				this.ParameterType = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00011D11 File Offset: 0x0000FF11
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x00011D19 File Offset: 0x0000FF19
		[DefaultValue(typeof(string))]
		public Type ParameterType { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00011D22 File Offset: 0x0000FF22
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00011D2A File Offset: 0x0000FF2A
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
