using System;
using System.Globalization;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C1 RID: 1217
	internal class CommunicationFrameworkParameterInspector : IParameterInspector
	{
		// Token: 0x06002525 RID: 9509 RVA: 0x00084205 File Offset: 0x00082405
		public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "{0}", new object[] { CommunicationFrameworkParameterInspector.GetCallOutputParameters(operationName, returnValue) });
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00084227 File Offset: 0x00082427
		public object BeforeCall(string operationName, object[] inputs)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "{0}", new object[] { CommunicationFrameworkParameterInspector.GetCallInputParameters(operationName, inputs) });
			return null;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0008424A File Offset: 0x0008244A
		private static string GetCallOutputParameters(string operationName, object returnValue)
		{
			return string.Format(CultureInfo.CurrentCulture, "Output for ECF operation '{0}': '{1}'", new object[]
			{
				operationName,
				(returnValue == null) ? "Null" : returnValue.ToString()
			});
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00084278 File Offset: 0x00082478
		private static string GetCallInputParameters(string operationName, object[] objects)
		{
			string[] array = new string[objects.Length];
			for (int i = 0; i < objects.Length; i++)
			{
				array[i] = ((objects[i] == null) ? "Null" : objects[i].ToString());
			}
			return string.Format(CultureInfo.CurrentCulture, "Input for ECF operation '{0}': '{1}'", new object[]
			{
				operationName,
				string.Join(", ", array)
			});
		}
	}
}
