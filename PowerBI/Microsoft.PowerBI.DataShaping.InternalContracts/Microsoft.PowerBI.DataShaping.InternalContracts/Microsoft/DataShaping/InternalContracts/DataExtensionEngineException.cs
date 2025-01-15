using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal sealed class DataExtensionEngineException : DataShapeEngineException
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000293E File Offset: 0x00000B3E
		internal DataExtensionEngineException(string errorCode, string message, DataExtensionException dataExtensionException, IReadOnlyList<AdditionalMessage> additionalMessages)
			: base(errorCode, message, dataExtensionException, dataExtensionException.ErrorSource, additionalMessages)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002951 File Offset: 0x00000B51
		internal DataExtensionEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000295B File Offset: 0x00000B5B
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002965 File Offset: 0x00000B65
		internal override string GetErrorDetails()
		{
			return this.ExtensionException.GetErrorDetails();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002972 File Offset: 0x00000B72
		internal DataExtensionException ExtensionException
		{
			get
			{
				return base.InnerException as DataExtensionException;
			}
		}
	}
}
