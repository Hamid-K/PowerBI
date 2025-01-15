using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x0200231A RID: 8986
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Style : OpenXmlLeafElement
	{
		// Token: 0x17004840 RID: 18496
		// (get) Token: 0x0600FFCE RID: 65486 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17004841 RID: 18497
		// (get) Token: 0x0600FFCF RID: 65487 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004842 RID: 18498
		// (get) Token: 0x0600FFD0 RID: 65488 RVA: 0x002DE373 File Offset: 0x002DC573
		internal override int ElementTypeId
		{
			get
			{
				return 12694;
			}
		}

		// Token: 0x0600FFD1 RID: 65489 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004843 RID: 18499
		// (get) Token: 0x0600FFD2 RID: 65490 RVA: 0x002DE37A File Offset: 0x002DC57A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Style.attributeTagNames;
			}
		}

		// Token: 0x17004844 RID: 18500
		// (get) Token: 0x0600FFD3 RID: 65491 RVA: 0x002DE381 File Offset: 0x002DC581
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Style.attributeNamespaceIds;
			}
		}

		// Token: 0x17004845 RID: 18501
		// (get) Token: 0x0600FFD4 RID: 65492 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x0600FFD5 RID: 65493 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public ByteValue Val
		{
			get
			{
				return (ByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600FFD7 RID: 65495 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FFD8 RID: 65496 RVA: 0x002DE3B7 File Offset: 0x002DC5B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Style>(deep);
		}

		// Token: 0x0600FFD9 RID: 65497 RVA: 0x002DE3C0 File Offset: 0x002DC5C0
		// Note: this type is marked as 'beforefieldinit'.
		static Style()
		{
			byte[] array = new byte[1];
			Style.attributeNamespaceIds = array;
		}

		// Token: 0x0400728B RID: 29323
		private const string tagName = "style";

		// Token: 0x0400728C RID: 29324
		private const byte tagNsId = 46;

		// Token: 0x0400728D RID: 29325
		internal const int ElementTypeIdConst = 12694;

		// Token: 0x0400728E RID: 29326
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400728F RID: 29327
		private static byte[] attributeNamespaceIds;
	}
}
