using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.Client
{
	// Token: 0x0200004E RID: 78
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "No longer relevant after .NET 4 introduction of SerializeObjectState event and ISafeSerializationData interface.")]
	[Serializable]
	public class DataServiceTransportException : InvalidOperationException
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000968D File Offset: 0x0000788D
		public DataServiceTransportException(IODataResponseMessage response, Exception innerException)
			: base(innerException.Message, innerException)
		{
			Util.CheckArgumentNull<Exception>(innerException, "innerException");
			this.state.ResponseMessage = response;
			base.SerializeObjectState += delegate(object sender, SafeSerializationEventArgs e)
			{
				e.AddSerializedState(this.state);
			};
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000096C6 File Offset: 0x000078C6
		public IODataResponseMessage Response
		{
			get
			{
				return this.state.ResponseMessage;
			}
		}

		// Token: 0x040000D5 RID: 213
		[NonSerialized]
		private DataServiceTransportException.DataServiceWebExceptionSerializationState state;

		// Token: 0x02000163 RID: 355
		[Serializable]
		private struct DataServiceWebExceptionSerializationState : ISafeSerializationData
		{
			// Token: 0x17000349 RID: 841
			// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002E921 File Offset: 0x0002CB21
			// (set) Token: 0x06000D55 RID: 3413 RVA: 0x0002E929 File Offset: 0x0002CB29
			public IODataResponseMessage ResponseMessage { get; set; }

			// Token: 0x06000D56 RID: 3414 RVA: 0x0002E934 File Offset: 0x0002CB34
			void ISafeSerializationData.CompleteDeserialization(object deserialized)
			{
				DataServiceTransportException ex = (DataServiceTransportException)deserialized;
				ex.state = this;
			}
		}
	}
}
