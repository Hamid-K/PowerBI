using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x0200230C RID: 8972
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageFastCommandButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageTab), FileFormatVersions.Office2010)]
	internal class Backstage : OpenXmlCompositeElement
	{
		// Token: 0x1700481C RID: 18460
		// (get) Token: 0x0600FF78 RID: 65400 RVA: 0x002DDFC6 File Offset: 0x002DC1C6
		public override string LocalName
		{
			get
			{
				return "backstage";
			}
		}

		// Token: 0x1700481D RID: 18461
		// (get) Token: 0x0600FF79 RID: 65401 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700481E RID: 18462
		// (get) Token: 0x0600FF7A RID: 65402 RVA: 0x002DDFCD File Offset: 0x002DC1CD
		internal override int ElementTypeId
		{
			get
			{
				return 13114;
			}
		}

		// Token: 0x0600FF7B RID: 65403 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700481F RID: 18463
		// (get) Token: 0x0600FF7C RID: 65404 RVA: 0x002DDFD4 File Offset: 0x002DC1D4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Backstage.attributeTagNames;
			}
		}

		// Token: 0x17004820 RID: 18464
		// (get) Token: 0x0600FF7D RID: 65405 RVA: 0x002DDFDB File Offset: 0x002DC1DB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Backstage.attributeNamespaceIds;
			}
		}

		// Token: 0x17004821 RID: 18465
		// (get) Token: 0x0600FF7E RID: 65406 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FF7F RID: 65407 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onShow")]
		public StringValue OnShow
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004822 RID: 18466
		// (get) Token: 0x0600FF80 RID: 65408 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FF81 RID: 65409 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "onHide")]
		public StringValue OnHide
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0600FF82 RID: 65410 RVA: 0x00293ECF File Offset: 0x002920CF
		public Backstage()
		{
		}

		// Token: 0x0600FF83 RID: 65411 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Backstage(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF84 RID: 65412 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Backstage(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF85 RID: 65413 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Backstage(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FF86 RID: 65414 RVA: 0x002DDFE2 File Offset: 0x002DC1E2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "tab" == name)
			{
				return new BackstageTab();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new BackstageFastCommandButton();
			}
			return null;
		}

		// Token: 0x0600FF87 RID: 65415 RVA: 0x002DE015 File Offset: 0x002DC215
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "onShow" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onHide" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FF88 RID: 65416 RVA: 0x002DE04B File Offset: 0x002DC24B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Backstage>(deep);
		}

		// Token: 0x0600FF89 RID: 65417 RVA: 0x002DE054 File Offset: 0x002DC254
		// Note: this type is marked as 'beforefieldinit'.
		static Backstage()
		{
			byte[] array = new byte[2];
			Backstage.attributeNamespaceIds = array;
		}

		// Token: 0x0400724C RID: 29260
		private const string tagName = "backstage";

		// Token: 0x0400724D RID: 29261
		private const byte tagNsId = 57;

		// Token: 0x0400724E RID: 29262
		internal const int ElementTypeIdConst = 13114;

		// Token: 0x0400724F RID: 29263
		private static string[] attributeTagNames = new string[] { "onShow", "onHide" };

		// Token: 0x04007250 RID: 29264
		private static byte[] attributeNamespaceIds;
	}
}
