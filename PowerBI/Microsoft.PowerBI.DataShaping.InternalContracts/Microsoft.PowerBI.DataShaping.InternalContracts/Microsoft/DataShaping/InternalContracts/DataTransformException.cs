using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal sealed class DataTransformException : DataShapeEngineException
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002A07 File Offset: 0x00000C07
		internal DataTransformException(string errorCode, string message, string providerMessage, ErrorSource errorSource)
			: base(errorCode, message, errorSource)
		{
			this.ProviderMessage = providerMessage;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A1A File Offset: 0x00000C1A
		internal DataTransformException(string errorCode, string message, Exception innerException, string providerMessage, ErrorSource errorCategory)
			: base(errorCode, message, innerException, errorCategory)
		{
			this.ProviderMessage = providerMessage;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A2F File Offset: 0x00000C2F
		internal DataTransformException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ProviderMessage = (string)info.GetValue("ProviderMessage", typeof(string));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A59 File Offset: 0x00000C59
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ProviderMessage", this.ProviderMessage);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A74 File Offset: 0x00000C74
		internal override string GetErrorDetails()
		{
			return this.ProviderMessage;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A7C File Offset: 0x00000C7C
		public string ProviderMessage { get; }

		// Token: 0x04000038 RID: 56
		private const string ProviderMessageSlotName = "ProviderMessage";
	}
}
