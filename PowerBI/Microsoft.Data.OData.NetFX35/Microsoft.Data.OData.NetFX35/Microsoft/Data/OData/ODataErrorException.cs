using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023E RID: 574
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataErrorException : ODataException
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x000420AB File Offset: 0x000402AB
		public ODataErrorException()
			: this(Strings.ODataErrorException_GeneralError)
		{
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000420B8 File Offset: 0x000402B8
		public ODataErrorException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000420C2 File Offset: 0x000402C2
		public ODataErrorException(string message, Exception innerException)
			: this(message, innerException, new ODataError())
		{
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000420D1 File Offset: 0x000402D1
		public ODataErrorException(ODataError error)
			: this(Strings.ODataErrorException_GeneralError, null, error)
		{
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000420E0 File Offset: 0x000402E0
		public ODataErrorException(string message, ODataError error)
			: this(message, null, error)
		{
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000420EB File Offset: 0x000402EB
		public ODataErrorException(string message, Exception innerException, ODataError error)
			: base(message, innerException)
		{
			this.state.ODataError = error;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00042101 File Offset: 0x00040301
		protected ODataErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExceptionUtils.CheckArgumentNotNull<SerializationInfo>(info, "serializationInfo");
			this.state.ODataError = (ODataError)info.GetValue("Error", typeof(ODataError));
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004213B File Offset: 0x0004033B
		public ODataError Error
		{
			get
			{
				return this.state.ODataError;
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00042148 File Offset: 0x00040348
		[SecurityPermission(6, Flags = 128)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExceptionUtils.CheckArgumentNotNull<SerializationInfo>(info, "serializationInfo");
			base.GetObjectData(info, context);
			info.AddValue("Error", this.state.ODataError);
		}

		// Token: 0x040006A2 RID: 1698
		private ODataErrorException.ODataErrorExceptionSafeSerializationState state;

		// Token: 0x0200023F RID: 575
		[Serializable]
		private struct ODataErrorExceptionSafeSerializationState
		{
			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06001176 RID: 4470 RVA: 0x00042173 File Offset: 0x00040373
			// (set) Token: 0x06001177 RID: 4471 RVA: 0x0004217B File Offset: 0x0004037B
			public ODataError ODataError { get; set; }
		}
	}
}
