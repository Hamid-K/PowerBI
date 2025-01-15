using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200245E RID: 9310
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SingleDataSourceRecord))]
	internal class MailMergeRecipients : OpenXmlPartRootElement
	{
		// Token: 0x170050A9 RID: 20649
		// (get) Token: 0x06011270 RID: 70256 RVA: 0x002A6C9C File Offset: 0x002A4E9C
		public override string LocalName
		{
			get
			{
				return "recipients";
			}
		}

		// Token: 0x170050AA RID: 20650
		// (get) Token: 0x06011271 RID: 70257 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050AB RID: 20651
		// (get) Token: 0x06011272 RID: 70258 RVA: 0x002EB15D File Offset: 0x002E935D
		internal override int ElementTypeId
		{
			get
			{
				return 12540;
			}
		}

		// Token: 0x06011273 RID: 70259 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011274 RID: 70260 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public MailMergeRecipients()
		{
		}

		// Token: 0x06011275 RID: 70261 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public MailMergeRecipients(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011276 RID: 70262 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public MailMergeRecipients(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011277 RID: 70263 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public MailMergeRecipients(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011278 RID: 70264 RVA: 0x002EB164 File Offset: 0x002E9364
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "recipientData" == name)
			{
				return new SingleDataSourceRecord();
			}
			return null;
		}

		// Token: 0x06011279 RID: 70265 RVA: 0x002EB17F File Offset: 0x002E937F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MailMergeRecipients>(deep);
		}

		// Token: 0x0601127A RID: 70266 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal MailMergeRecipients(MailMergeRecipientDataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601127B RID: 70267 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(MailMergeRecipientDataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x0601127C RID: 70268 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(MailMergeRecipientDataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x04007864 RID: 30820
		private const string tagName = "recipients";

		// Token: 0x04007865 RID: 30821
		private const byte tagNsId = 33;

		// Token: 0x04007866 RID: 30822
		internal const int ElementTypeIdConst = 12540;
	}
}
