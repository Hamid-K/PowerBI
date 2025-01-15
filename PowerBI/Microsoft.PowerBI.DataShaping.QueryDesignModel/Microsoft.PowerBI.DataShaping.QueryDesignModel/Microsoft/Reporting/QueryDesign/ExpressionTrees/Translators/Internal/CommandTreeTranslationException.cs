using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	internal class CommandTreeTranslationException : NotSupportedException
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x0002C769 File Offset: 0x0002A969
		internal CommandTreeTranslationException()
		{
			this._errorCode = CommandTreeTranslationErrorCode.Unexpected;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002C778 File Offset: 0x0002A978
		internal CommandTreeTranslationException(string message)
			: base(message)
		{
			this._errorCode = CommandTreeTranslationErrorCode.Unexpected;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002C788 File Offset: 0x0002A988
		internal CommandTreeTranslationException(string message, CommandTreeTranslationErrorCode errorCode)
			: base(message)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002C798 File Offset: 0x0002A998
		internal CommandTreeTranslationException(string message, Exception innerException)
			: base(message, innerException)
		{
			this._errorCode = CommandTreeTranslationErrorCode.Unexpected;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002C7A9 File Offset: 0x0002A9A9
		protected CommandTreeTranslationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorCode = (CommandTreeTranslationErrorCode)info.GetValue("ErrorCode", typeof(CommandTreeTranslationErrorCode));
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002C7D3 File Offset: 0x0002A9D3
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorCode", this.ErrorCode);
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0002C7F3 File Offset: 0x0002A9F3
		public CommandTreeTranslationErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x04000A70 RID: 2672
		private const string ErrorCodeSlotName = "ErrorCode";

		// Token: 0x04000A71 RID: 2673
		private readonly CommandTreeTranslationErrorCode _errorCode;
	}
}
