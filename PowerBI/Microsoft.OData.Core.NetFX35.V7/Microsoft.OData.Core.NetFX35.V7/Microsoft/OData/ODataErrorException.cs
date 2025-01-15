using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.OData
{
	// Token: 0x0200005F RID: 95
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataErrorException : ODataException
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00009FAD File Offset: 0x000081AD
		public ODataErrorException()
			: this(Strings.ODataErrorException_GeneralError)
		{
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009FBA File Offset: 0x000081BA
		public ODataErrorException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009FC4 File Offset: 0x000081C4
		public ODataErrorException(string message, Exception innerException)
			: this(message, innerException, new ODataError())
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009FD3 File Offset: 0x000081D3
		public ODataErrorException(ODataError error)
			: this(Strings.ODataErrorException_GeneralError, null, error)
		{
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009FE2 File Offset: 0x000081E2
		public ODataErrorException(string message, ODataError error)
			: this(message, null, error)
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009FED File Offset: 0x000081ED
		public ODataErrorException(string message, Exception innerException, ODataError error)
			: base(message, innerException)
		{
			this.state.ODataError = error;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A003 File Offset: 0x00008203
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		protected ODataErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExceptionUtils.CheckArgumentNotNull<SerializationInfo>(info, "serializationInfo");
			this.state.ODataError = (ODataError)info.GetValue("Error", typeof(ODataError));
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000A03E File Offset: 0x0000823E
		public ODataError Error
		{
			get
			{
				return this.state.ODataError;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A04B File Offset: 0x0000824B
		[SecurityPermission(6, Flags = 128)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExceptionUtils.CheckArgumentNotNull<SerializationInfo>(info, "serializationInfo");
			base.GetObjectData(info, context);
			info.AddValue("Error", this.state.ODataError);
		}

		// Token: 0x040001AE RID: 430
		[NonSerialized]
		private ODataErrorException.ODataErrorExceptionSafeSerializationState state;

		// Token: 0x0200025E RID: 606
		[Serializable]
		private struct ODataErrorExceptionSafeSerializationState
		{
			// Token: 0x17000543 RID: 1347
			// (get) Token: 0x0600177B RID: 6011 RVA: 0x0004757E File Offset: 0x0004577E
			// (set) Token: 0x0600177C RID: 6012 RVA: 0x00047586 File Offset: 0x00045786
			public ODataError ODataError { get; set; }
		}
	}
}
