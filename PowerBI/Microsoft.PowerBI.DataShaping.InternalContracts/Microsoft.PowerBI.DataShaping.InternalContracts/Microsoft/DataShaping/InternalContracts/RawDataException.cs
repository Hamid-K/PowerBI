using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	internal sealed class RawDataException : DataShapeEngineException
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000306B File Offset: 0x0000126B
		internal RawDataException(string message, ErrorSource errorSource)
			: base("RawDataProcessingError", message, errorSource)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000307A File Offset: 0x0000127A
		internal RawDataException(string message, Exception innerException, ErrorSource errorSource, string providerMessage)
			: base("RawDataProcessingError", message, innerException, errorSource)
		{
			this._providerMessage = providerMessage;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003092 File Offset: 0x00001292
		internal RawDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._providerMessage = (string)info.GetValue("ProviderMessage", typeof(string));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030BC File Offset: 0x000012BC
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ProviderMessage", this._providerMessage);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000030D7 File Offset: 0x000012D7
		internal string ProviderMessage
		{
			get
			{
				return this._providerMessage;
			}
		}

		// Token: 0x0400004F RID: 79
		private const string ProviderMessageSlotName = "ProviderMessage";

		// Token: 0x04000050 RID: 80
		private string _providerMessage;
	}
}
