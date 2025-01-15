using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FFB RID: 12283
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTagType : OpenXmlLeafElement
	{
		// Token: 0x170095C7 RID: 38343
		// (get) Token: 0x0601AC48 RID: 109640 RVA: 0x0033D218 File Offset: 0x0033B418
		public override string LocalName
		{
			get
			{
				return "smartTagType";
			}
		}

		// Token: 0x170095C8 RID: 38344
		// (get) Token: 0x0601AC49 RID: 109641 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095C9 RID: 38345
		// (get) Token: 0x0601AC4A RID: 109642 RVA: 0x00367528 File Offset: 0x00365728
		internal override int ElementTypeId
		{
			get
			{
				return 12050;
			}
		}

		// Token: 0x0601AC4B RID: 109643 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170095CA RID: 38346
		// (get) Token: 0x0601AC4C RID: 109644 RVA: 0x0036752F File Offset: 0x0036572F
		internal override string[] AttributeTagNames
		{
			get
			{
				return SmartTagType.attributeTagNames;
			}
		}

		// Token: 0x170095CB RID: 38347
		// (get) Token: 0x0601AC4D RID: 109645 RVA: 0x00367536 File Offset: 0x00365736
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SmartTagType.attributeNamespaceIds;
			}
		}

		// Token: 0x170095CC RID: 38348
		// (get) Token: 0x0601AC4E RID: 109646 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AC4F RID: 109647 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "namespaceuri")]
		public StringValue SmartTagNamespace
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

		// Token: 0x170095CD RID: 38349
		// (get) Token: 0x0601AC50 RID: 109648 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AC51 RID: 109649 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x170095CE RID: 38350
		// (get) Token: 0x0601AC52 RID: 109650 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AC53 RID: 109651 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "url")]
		public StringValue Url
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601AC55 RID: 109653 RVA: 0x00367540 File Offset: 0x00365740
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "namespaceuri" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "url" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AC56 RID: 109654 RVA: 0x0036759D File Offset: 0x0036579D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagType>(deep);
		}

		// Token: 0x0400AE3B RID: 44603
		private const string tagName = "smartTagType";

		// Token: 0x0400AE3C RID: 44604
		private const byte tagNsId = 23;

		// Token: 0x0400AE3D RID: 44605
		internal const int ElementTypeIdConst = 12050;

		// Token: 0x0400AE3E RID: 44606
		private static string[] attributeTagNames = new string[] { "namespaceuri", "name", "url" };

		// Token: 0x0400AE3F RID: 44607
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
