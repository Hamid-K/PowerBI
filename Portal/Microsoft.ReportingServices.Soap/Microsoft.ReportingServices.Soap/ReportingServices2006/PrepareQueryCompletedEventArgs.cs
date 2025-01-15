using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001CD RID: 461
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class PrepareQueryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x000180C8 File Offset: 0x000162C8
		internal PrepareQueryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x000180DB File Offset: 0x000162DB
		public DataSetDefinition Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSetDefinition)this.results[0];
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000180F0 File Offset: 0x000162F0
		public bool Changed
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00018105 File Offset: 0x00016305
		public string[] ParameterNames
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[2];
			}
		}

		// Token: 0x0400049F RID: 1183
		private object[] results;
	}
}
