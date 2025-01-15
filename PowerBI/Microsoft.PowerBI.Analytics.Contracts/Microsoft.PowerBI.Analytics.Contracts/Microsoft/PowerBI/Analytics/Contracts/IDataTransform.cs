using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000010 RID: 16
	public interface IDataTransform
	{
		// Token: 0x0600002A RID: 42
		Task<DataTransformResult> ExecuteAsync(DataTransformExecutionContext context);
	}
}
