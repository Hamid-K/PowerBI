using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x0200314C RID: 12620
	[DebuggerDisplay("Description={Description}")]
	internal class ValidationErrorInfo
	{
		// Token: 0x0601B5BA RID: 112058 RVA: 0x0000336E File Offset: 0x0000156E
		[Conditional("DEBUG")]
		public void SetDebugField(string attributeQualifiedName, string validationErrorCategory)
		{
		}

		// Token: 0x170099AD RID: 39341
		// (get) Token: 0x0601B5BB RID: 112059 RVA: 0x00376B42 File Offset: 0x00374D42
		// (set) Token: 0x0601B5BC RID: 112060 RVA: 0x00376B4A File Offset: 0x00374D4A
		public string Id { get; internal set; }

		// Token: 0x170099AE RID: 39342
		// (get) Token: 0x0601B5BD RID: 112061 RVA: 0x00376B53 File Offset: 0x00374D53
		// (set) Token: 0x0601B5BE RID: 112062 RVA: 0x00376B5B File Offset: 0x00374D5B
		public ValidationErrorType ErrorType { get; internal set; }

		// Token: 0x170099AF RID: 39343
		// (get) Token: 0x0601B5BF RID: 112063 RVA: 0x00376B64 File Offset: 0x00374D64
		// (set) Token: 0x0601B5C0 RID: 112064 RVA: 0x00376B6C File Offset: 0x00374D6C
		public string Description { get; internal set; }

		// Token: 0x170099B0 RID: 39344
		// (get) Token: 0x0601B5C1 RID: 112065 RVA: 0x00376B75 File Offset: 0x00374D75
		// (set) Token: 0x0601B5C2 RID: 112066 RVA: 0x00376B7D File Offset: 0x00374D7D
		public XmlPath Path { get; internal set; }

		// Token: 0x170099B1 RID: 39345
		// (get) Token: 0x0601B5C3 RID: 112067 RVA: 0x00376B86 File Offset: 0x00374D86
		// (set) Token: 0x0601B5C4 RID: 112068 RVA: 0x00376B8E File Offset: 0x00374D8E
		public OpenXmlElement Node
		{
			get
			{
				return this._element;
			}
			internal set
			{
				this._element = value;
				this.Path = XmlPath.GetXPath(value);
				if (this.Part == null)
				{
					this.Part = this._element.GetPart();
				}
			}
		}

		// Token: 0x170099B2 RID: 39346
		// (get) Token: 0x0601B5C5 RID: 112069 RVA: 0x00376BBC File Offset: 0x00374DBC
		// (set) Token: 0x0601B5C6 RID: 112070 RVA: 0x00376BC4 File Offset: 0x00374DC4
		public OpenXmlPart Part { get; internal set; }

		// Token: 0x170099B3 RID: 39347
		// (get) Token: 0x0601B5C7 RID: 112071 RVA: 0x00376BCD File Offset: 0x00374DCD
		// (set) Token: 0x0601B5C8 RID: 112072 RVA: 0x00376BD5 File Offset: 0x00374DD5
		public OpenXmlElement RelatedNode { get; internal set; }

		// Token: 0x170099B4 RID: 39348
		// (get) Token: 0x0601B5C9 RID: 112073 RVA: 0x00376BDE File Offset: 0x00374DDE
		// (set) Token: 0x0601B5CA RID: 112074 RVA: 0x00376BE6 File Offset: 0x00374DE6
		public OpenXmlPart RelatedPart { get; internal set; }

		// Token: 0x0400B54D RID: 46413
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private OpenXmlElement _element;
	}
}
