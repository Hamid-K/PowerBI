using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000080 RID: 128
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class PrepareQueryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0000DC81 File Offset: 0x0000BE81
		internal PrepareQueryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public DataSetDefinition Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSetDefinition)this.results[0];
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000DCA9 File Offset: 0x0000BEA9
		public bool Changed
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000DCBE File Offset: 0x0000BEBE
		public string[] ParameterNames
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[2];
			}
		}

		// Token: 0x04000239 RID: 569
		private object[] results;
	}
}
