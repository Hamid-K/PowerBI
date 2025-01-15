using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	internal sealed class DataShapeEngineCanceledException : DataShapeEngineException
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000297F File Offset: 0x00000B7F
		internal DataShapeEngineCanceledException(Exception innerException)
			: base("OperationCanceled", "The query operation was canceled.", innerException, ErrorSource.User)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002993 File Offset: 0x00000B93
		internal DataShapeEngineCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029A0 File Offset: 0x00000BA0
		internal static void ThrowForCancel(Exception e)
		{
			while (e != null)
			{
				if (e is DataShapeEngineCanceledException)
				{
					return;
				}
				DataExtensionException ex = e as DataExtensionException;
				if (ex != null)
				{
					if (ex.ErrorCode == "OperationCanceled" && ex.ErrorSource == ErrorSource.User)
					{
						return;
					}
					throw new DataShapeEngineCanceledException(ex);
				}
				else
				{
					if (e is DataShapeEngineException || e is OperationCanceledException)
					{
						throw new DataShapeEngineCanceledException(e);
					}
					e = e.InnerException;
				}
			}
		}
	}
}
