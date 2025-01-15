using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.OData.Core
{
	// Token: 0x02000171 RID: 369
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataErrorException : ODataException
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003150B File Offset: 0x0002F70B
		public ODataErrorException()
			: this(Strings.ODataErrorException_GeneralError)
		{
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00031518 File Offset: 0x0002F718
		public ODataErrorException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00031522 File Offset: 0x0002F722
		public ODataErrorException(string message, Exception innerException)
			: this(message, innerException, new ODataError())
		{
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00031531 File Offset: 0x0002F731
		public ODataErrorException(ODataError error)
			: this(Strings.ODataErrorException_GeneralError, null, error)
		{
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00031540 File Offset: 0x0002F740
		public ODataErrorException(string message, ODataError error)
			: this(message, null, error)
		{
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003154B File Offset: 0x0002F74B
		public ODataErrorException(string message, Exception innerException, ODataError error)
			: base(message, innerException)
		{
			this.state.ODataError = error;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00031561 File Offset: 0x0002F761
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		protected ODataErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExceptionUtils.CheckArgumentNotNull<SerializationInfo>(info, "serializationInfo");
			this.state.ODataError = (ODataError)info.GetValue("Error", typeof(ODataError));
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0003159B File Offset: 0x0002F79B
		public ODataError Error
		{
			get
			{
				return this.state.ODataError;
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000315A8 File Offset: 0x0002F7A8
		[SecurityPermission(6, Flags = 128)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExceptionUtils.CheckArgumentNotNull<SerializationInfo>(info, "serializationInfo");
			base.GetObjectData(info, context);
			info.AddValue("Error", this.state.ODataError);
		}

		// Token: 0x040005EC RID: 1516
		[NonSerialized]
		private ODataErrorException.ODataErrorExceptionSafeSerializationState state;

		// Token: 0x02000172 RID: 370
		[Serializable]
		private struct ODataErrorExceptionSafeSerializationState
		{
			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000D9F RID: 3487 RVA: 0x000315D3 File Offset: 0x0002F7D3
			// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x000315DB File Offset: 0x0002F7DB
			public ODataError ODataError { get; set; }
		}
	}
}
