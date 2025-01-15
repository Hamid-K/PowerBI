using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200073B RID: 1851
	public interface IErrorBlockProcessor
	{
		// Token: 0x060039F6 RID: 14838
		void PopulateErrorBlockStructure(ref object theErrorBlock, object[] theParameterArray, object runtimeCallContext, string className, string classGUID, string methodName);

		// Token: 0x060039F7 RID: 14839
		int ExtractErrorInformation(ref object theErrorBlock, object[] theParameterArray, object runtimeCallContext, out bool returnErrorToClient, out string errorMessage, out bool readyToCommit);
	}
}
