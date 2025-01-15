using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002D2 RID: 722
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class PrepareQueryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001791 RID: 6033 RVA: 0x000231CF File Offset: 0x000213CF
		internal PrepareQueryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x000231E2 File Offset: 0x000213E2
		public DataSetDefinition Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSetDefinition)this.results[0];
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000231F7 File Offset: 0x000213F7
		public bool Changed
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0002320C File Offset: 0x0002140C
		public string[] ParameterNames
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[2];
			}
		}

		// Token: 0x04000707 RID: 1799
		private object[] results;
	}
}
