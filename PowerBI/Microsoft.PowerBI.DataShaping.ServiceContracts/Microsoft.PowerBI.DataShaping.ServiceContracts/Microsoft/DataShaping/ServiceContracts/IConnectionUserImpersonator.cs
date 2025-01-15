using System;
using System.Threading.Tasks;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x0200000F RID: 15
	public interface IConnectionUserImpersonator
	{
		// Token: 0x0600005E RID: 94
		void ExecuteInContext(Action action);

		// Token: 0x0600005F RID: 95
		T ExecuteInContext<T>(Func<T> func);

		// Token: 0x06000060 RID: 96
		Task ExecuteInContextAsync(Func<Task> func);

		// Token: 0x06000061 RID: 97
		Task<T> ExecuteInContextAsync<T>(Func<Task<T>> func);
	}
}
