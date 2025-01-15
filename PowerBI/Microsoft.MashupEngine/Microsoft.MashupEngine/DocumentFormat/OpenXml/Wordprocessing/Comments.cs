using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F12 RID: 12050
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Comment))]
	internal class Comments : OpenXmlPartRootElement
	{
		// Token: 0x17008E40 RID: 36416
		// (get) Token: 0x06019BE3 RID: 105443 RVA: 0x002A4733 File Offset: 0x002A2933
		public override string LocalName
		{
			get
			{
				return "comments";
			}
		}

		// Token: 0x17008E41 RID: 36417
		// (get) Token: 0x06019BE4 RID: 105444 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E42 RID: 36418
		// (get) Token: 0x06019BE5 RID: 105445 RVA: 0x00354B6B File Offset: 0x00352D6B
		internal override int ElementTypeId
		{
			get
			{
				return 11692;
			}
		}

		// Token: 0x06019BE6 RID: 105446 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019BE7 RID: 105447 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Comments(WordprocessingCommentsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019BE8 RID: 105448 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WordprocessingCommentsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E43 RID: 36419
		// (get) Token: 0x06019BE9 RID: 105449 RVA: 0x00354B72 File Offset: 0x00352D72
		// (set) Token: 0x06019BEA RID: 105450 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WordprocessingCommentsPart WordprocessingCommentsPart
		{
			get
			{
				return base.OpenXmlPart as WordprocessingCommentsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019BEB RID: 105451 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Comments(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BEC RID: 105452 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Comments(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BED RID: 105453 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Comments(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019BEE RID: 105454 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Comments()
		{
		}

		// Token: 0x06019BEF RID: 105455 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WordprocessingCommentsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019BF0 RID: 105456 RVA: 0x00354B7F File Offset: 0x00352D7F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "comment" == name)
			{
				return new Comment();
			}
			return null;
		}

		// Token: 0x06019BF1 RID: 105457 RVA: 0x00354B9A File Offset: 0x00352D9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Comments>(deep);
		}

		// Token: 0x0400AA62 RID: 43618
		private const string tagName = "comments";

		// Token: 0x0400AA63 RID: 43619
		private const byte tagNsId = 23;

		// Token: 0x0400AA64 RID: 43620
		internal const int ElementTypeIdConst = 11692;
	}
}
