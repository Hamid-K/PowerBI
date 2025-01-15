using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	internal sealed class TmdlParserException : TomException
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x0008E5D4 File Offset: 0x0008C7D4
		public TmdlParserException(ParsingError error, string message)
			: base(message)
		{
			this.Error = error;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0008E5E4 File Offset: 0x0008C7E4
		public TmdlParserException(ParsingError error, string message, Exception innerException)
			: base(message, innerException)
		{
			this.Error = error;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0008E5F8 File Offset: 0x0008C7F8
		internal TmdlParserException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			object value = info.GetValue("TmdlParserException.Error", typeof(ParsingError));
			if (value != null)
			{
				this.Error = (ParsingError)value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0008E632 File Offset: 0x0008C832
		public ParsingError Error { get; }

		// Token: 0x06001521 RID: 5409 RVA: 0x0008E63A File Offset: 0x0008C83A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TmdlParserException.Error", this.Error);
		}

		// Token: 0x04000393 RID: 915
		private const string SerializationInfoKey_Error = "TmdlParserException.Error";
	}
}
