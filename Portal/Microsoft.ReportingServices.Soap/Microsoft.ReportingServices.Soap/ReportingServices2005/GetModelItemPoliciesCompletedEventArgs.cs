using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200025D RID: 605
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelItemPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015F4 RID: 5620 RVA: 0x00022930 File Offset: 0x00020B30
		internal GetModelItemPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00022943 File Offset: 0x00020B43
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00022958 File Offset: 0x00020B58
		public bool InheritParent
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x040006DE RID: 1758
		private object[] results;
	}
}
