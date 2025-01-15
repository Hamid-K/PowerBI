using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public sealed class TmdlFormatException : TomException
	{
		// Token: 0x060014D4 RID: 5332 RVA: 0x0008C938 File Offset: 0x0008AB38
		internal TmdlFormatException(ParsingError error, string message, TmdlSourceLocation location, string lineText, Exception innerException)
			: base(TmdlFormatException.BuildMessage(error, message, location, lineText), innerException)
		{
			this.Error = error;
			if (location.IsValid)
			{
				this.HasDocumentSourceInfo = true;
				if (!string.IsNullOrEmpty(location.Document.Path))
				{
					this.Document = location.Document.Path;
				}
				else
				{
					this.Document = "./unknown";
				}
				this.Line = location.LineNumber;
			}
			this.LineText = lineText;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0008C9B4 File Offset: 0x0008ABB4
		internal TmdlFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Error = (ParsingError)((int)info.GetValue("TmdlFormatException.Error", typeof(int)));
			if ((bool)info.GetValue("TmdlFormatException.HasDocumentSourceInfo", typeof(bool)))
			{
				this.HasDocumentSourceInfo = true;
				this.Document = (string)info.GetValue("TmdlFormatException.Document", typeof(string));
				this.Line = (int)info.GetValue("TmdlFormatException.Line", typeof(int));
			}
			this.LineText = (string)info.GetValue("TmdlFormatException.LineText", typeof(string));
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0008CA6C File Offset: 0x0008AC6C
		public bool HasDocumentSourceInfo { get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0008CA74 File Offset: 0x0008AC74
		public string Document { get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0008CA7C File Offset: 0x0008AC7C
		public int Line { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0008CA84 File Offset: 0x0008AC84
		public string LineText { get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0008CA8C File Offset: 0x0008AC8C
		internal ParsingError Error { get; }

		// Token: 0x060014DB RID: 5339 RVA: 0x0008CA94 File Offset: 0x0008AC94
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TmdlFormatException.Error", (int)this.Error);
			if (this.HasDocumentSourceInfo)
			{
				info.AddValue("TmdlFormatException.HasDocumentSourceInfo", true);
				info.AddValue("TmdlFormatException.Document", this.Document, typeof(string));
				info.AddValue("TmdlFormatException.Line", this.Line);
			}
			if (!string.IsNullOrEmpty(this.LineText))
			{
				info.AddValue("TmdlFormatException.LineText", this.LineText);
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0008CB28 File Offset: 0x0008AD28
		private static string BuildMessage(ParsingError error, string message, TmdlSourceLocation location, string lineText)
		{
			StringBuilder stringBuilder = new StringBuilder(TomSR.Exception_TmdlFormatException_Title(error.ToString("G"), Environment.NewLine));
			if (!string.IsNullOrEmpty(message))
			{
				stringBuilder.Append(TomSR.Exception_TmdlFormatException_DetailedError(message, Environment.NewLine));
			}
			if (location.IsValid)
			{
				stringBuilder.Append(TomSR.Exception_TmdlFormatException_SourceInfo(ClientHostingManager.MarkAsRestrictedInformation(location.Document.Path, InfoRestrictionType.CCON), location.LineNumber.ToString("G"), Environment.NewLine));
			}
			if (!string.IsNullOrEmpty(lineText))
			{
				if (lineText.Length <= 80)
				{
					stringBuilder.Append(TomSR.Exception_TmdlFormatException_Line(ClientHostingManager.MarkAsRestrictedInformation(lineText, InfoRestrictionType.CCON), Environment.NewLine));
				}
				else
				{
					stringBuilder.Append(TomSR.Exception_TmdlFormatException_Line(ClientHostingManager.MarkAsRestrictedInformation(lineText.Substring(0, 77), InfoRestrictionType.CCON), Environment.NewLine));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400036E RID: 878
		private const string SerializationInfoKey_Error = "TmdlFormatException.Error";

		// Token: 0x0400036F RID: 879
		private const string SerializationInfoKey_HasSourceInfo = "TmdlFormatException.HasDocumentSourceInfo";

		// Token: 0x04000370 RID: 880
		private const string SerializationInfoKey_Document = "TmdlFormatException.Document";

		// Token: 0x04000371 RID: 881
		private const string SerializationInfoKey_Line = "TmdlFormatException.Line";

		// Token: 0x04000372 RID: 882
		private const string SerializationInfoKey_LineText = "TmdlFormatException.LineText";
	}
}
