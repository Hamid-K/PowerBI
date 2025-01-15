using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F10 RID: 12048
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RecipientData))]
	internal class Recipients : OpenXmlPartRootElement
	{
		// Token: 0x17008E3A RID: 36410
		// (get) Token: 0x06019BCC RID: 105420 RVA: 0x002A6C9C File Offset: 0x002A4E9C
		public override string LocalName
		{
			get
			{
				return "recipients";
			}
		}

		// Token: 0x17008E3B RID: 36411
		// (get) Token: 0x06019BCD RID: 105421 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E3C RID: 36412
		// (get) Token: 0x06019BCE RID: 105422 RVA: 0x003547D0 File Offset: 0x003529D0
		internal override int ElementTypeId
		{
			get
			{
				return 11690;
			}
		}

		// Token: 0x06019BCF RID: 105423 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019BD0 RID: 105424 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Recipients()
		{
		}

		// Token: 0x06019BD1 RID: 105425 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Recipients(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BD2 RID: 105426 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Recipients(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BD3 RID: 105427 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Recipients(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019BD4 RID: 105428 RVA: 0x003547D7 File Offset: 0x003529D7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "recipientData" == name)
			{
				return new RecipientData();
			}
			return null;
		}

		// Token: 0x06019BD5 RID: 105429 RVA: 0x003547F2 File Offset: 0x003529F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Recipients>(deep);
		}

		// Token: 0x06019BD6 RID: 105430 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Recipients(MailMergeRecipientDataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019BD7 RID: 105431 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(MailMergeRecipientDataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x06019BD8 RID: 105432 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(MailMergeRecipientDataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0400AA5C RID: 43612
		private const string tagName = "recipients";

		// Token: 0x0400AA5D RID: 43613
		private const byte tagNsId = 23;

		// Token: 0x0400AA5E RID: 43614
		internal const int ElementTypeIdConst = 11690;
	}
}
