using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.Client
{
	// Token: 0x020000CC RID: 204
	[DebuggerDisplay("{Message}")]
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "No longer relevant after .NET 4 introduction of SerializeObjectState event and ISafeSerializationData interface.")]
	[Serializable]
	public sealed class DataServiceClientException : InvalidOperationException
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0001C365 File Offset: 0x0001A565
		public DataServiceClientException()
			: this(Strings.DataServiceException_GeneralError)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001C372 File Offset: 0x0001A572
		public DataServiceClientException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001C37C File Offset: 0x0001A57C
		public DataServiceClientException(string message, Exception innerException)
			: this(message, innerException, 500)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001C38B File Offset: 0x0001A58B
		public DataServiceClientException(string message, int statusCode)
			: this(message, null, statusCode)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001C396 File Offset: 0x0001A596
		public DataServiceClientException(string message, Exception innerException, int statusCode)
			: base(message, innerException)
		{
			this.state.StatusCode = statusCode;
			base.SerializeObjectState += delegate(object sender, SafeSerializationEventArgs e)
			{
				e.AddSerializedState(this.state);
			};
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001C3BE File Offset: 0x0001A5BE
		public int StatusCode
		{
			get
			{
				return this.state.StatusCode;
			}
		}

		// Token: 0x040002EB RID: 747
		[NonSerialized]
		private DataServiceClientException.DataServiceClientExceptionSerializationState state;

		// Token: 0x020001A2 RID: 418
		[Serializable]
		private struct DataServiceClientExceptionSerializationState : ISafeSerializationData
		{
			// Token: 0x17000384 RID: 900
			// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x000318C9 File Offset: 0x0002FAC9
			// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x000318D1 File Offset: 0x0002FAD1
			public int StatusCode { get; set; }

			// Token: 0x06000EA3 RID: 3747 RVA: 0x000318DC File Offset: 0x0002FADC
			void ISafeSerializationData.CompleteDeserialization(object deserialized)
			{
				DataServiceClientException ex = (DataServiceClientException)deserialized;
				ex.state = this;
			}
		}
	}
}
