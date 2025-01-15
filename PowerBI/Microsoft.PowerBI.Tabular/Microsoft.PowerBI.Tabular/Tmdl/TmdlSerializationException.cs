using System;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	public sealed class TmdlSerializationException : TomException
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x0008F1E1 File Offset: 0x0008D3E1
		internal TmdlSerializationException(TmdlSourceLocation location)
		{
			this.Initialize(location);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0008F1F0 File Offset: 0x0008D3F0
		internal TmdlSerializationException(string message, TmdlSourceLocation location)
			: base(message)
		{
			this.Initialize(location);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0008F200 File Offset: 0x0008D400
		internal TmdlSerializationException(string message, TmdlSourceLocation location, Exception innerException)
			: base(message, innerException)
		{
			this.Initialize(location);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0008F214 File Offset: 0x0008D414
		internal TmdlSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if ((bool)info.GetValue("TmdlSerializationException.HasDocumentSourceInfo", typeof(bool)))
			{
				this.HasDocumentSourceInfo = true;
				this.Document = (string)info.GetValue("TmdlSerializationException.Document", typeof(string));
				this.Line = (int)info.GetValue("TmdlSerializationException.Line", typeof(int));
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0008F28C File Offset: 0x0008D48C
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x0008F294 File Offset: 0x0008D494
		public bool HasDocumentSourceInfo { get; private set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0008F29D File Offset: 0x0008D49D
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0008F2A5 File Offset: 0x0008D4A5
		public string Document { get; private set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0008F2AE File Offset: 0x0008D4AE
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x0008F2B6 File Offset: 0x0008D4B6
		public int Line { get; private set; }

		// Token: 0x06001542 RID: 5442 RVA: 0x0008F2C0 File Offset: 0x0008D4C0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			if (this.HasDocumentSourceInfo)
			{
				info.AddValue("TmdlSerializationException.HasDocumentSourceInfo", true);
				info.AddValue("TmdlSerializationException.Document", this.Document, typeof(string));
				info.AddValue("TmdlSerializationException.Line", this.Line);
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0008F324 File Offset: 0x0008D524
		internal static TmdlSerializationException CreateAmbiguousSourceException(string error, TmdlObject first, TmdlObject second)
		{
			string newLine = Environment.NewLine;
			string text = first.ObjectType.ToString("G");
			string text2 = ClientHostingManager.MarkAsRestrictedInformation(first.Name, InfoRestrictionType.CCON);
			TmdlDocument document = first.SourceLocation.Document;
			string text3 = ClientHostingManager.MarkAsRestrictedInformation((document != null) ? document.Path : null, InfoRestrictionType.CCON);
			string text4 = second.ObjectType.ToString("G");
			string text5 = ClientHostingManager.MarkAsRestrictedInformation(second.Name, InfoRestrictionType.CCON);
			TmdlDocument document2 = second.SourceLocation.Document;
			return new TmdlSerializationException(TomSR.Exception_TmdlAmbiguousSource(error, newLine, text, text2, text3, text4, text5, ClientHostingManager.MarkAsRestrictedInformation((document2 != null) ? document2.Path : null, InfoRestrictionType.CCON)), second.SourceLocation);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0008F3D0 File Offset: 0x0008D5D0
		internal static TmdlSerializationException CreateAmbiguousSourceException(string error, TmdlTranslationElement first, TmdlTranslationElement second)
		{
			string newLine = Environment.NewLine;
			string text = first.ObjectType.ToString("G");
			string text2 = ClientHostingManager.MarkAsRestrictedInformation(first.Name, InfoRestrictionType.CCON);
			TmdlDocument document = first.SourceLocation.Document;
			string text3 = ClientHostingManager.MarkAsRestrictedInformation((document != null) ? document.Path : null, InfoRestrictionType.CCON);
			string text4 = second.ObjectType.ToString("G");
			string text5 = ClientHostingManager.MarkAsRestrictedInformation(second.Name, InfoRestrictionType.CCON);
			TmdlDocument document2 = second.SourceLocation.Document;
			return new TmdlSerializationException(TomSR.Exception_TmdlAmbiguousSource2(error, newLine, text, text2, text3, text4, text5, ClientHostingManager.MarkAsRestrictedInformation((document2 != null) ? document2.Path : null, InfoRestrictionType.CCON)), second.SourceLocation);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0008F47C File Offset: 0x0008D67C
		internal static TmdlSerializationException CreateAmbiguousSourceException(string error, TmdlObject @object, TmdlProperty property)
		{
			string newLine = Environment.NewLine;
			string text = @object.ObjectType.ToString("G");
			string text2 = ClientHostingManager.MarkAsRestrictedInformation(@object.Name, InfoRestrictionType.CCON);
			TmdlDocument document = @object.SourceLocation.Document;
			string text3 = ClientHostingManager.MarkAsRestrictedInformation((document != null) ? document.Path : null, InfoRestrictionType.CCON);
			string name = property.Name;
			TmdlDocument document2 = property.SourceLocation.Document;
			return new TmdlSerializationException(TomSR.Exception_TmdlAmbiguousSource3(error, newLine, text, text2, text3, name, ClientHostingManager.MarkAsRestrictedInformation((document2 != null) ? document2.Path : null, InfoRestrictionType.CCON)), @object.SourceLocation);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0008F508 File Offset: 0x0008D708
		private void Initialize(TmdlSourceLocation location)
		{
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
		}

		// Token: 0x040003A1 RID: 929
		private const string SerializationInfoKey_HasSourceInfo = "TmdlSerializationException.HasDocumentSourceInfo";

		// Token: 0x040003A2 RID: 930
		private const string SerializationInfoKey_Document = "TmdlSerializationException.Document";

		// Token: 0x040003A3 RID: 931
		private const string SerializationInfoKey_Line = "TmdlSerializationException.Line";
	}
}
