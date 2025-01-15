using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FAC RID: 12204
	[GeneratedCode("DomGen", "2.0")]
	internal class LatentStyleExceptionInfo : OpenXmlLeafElement
	{
		// Token: 0x1700933E RID: 37694
		// (get) Token: 0x0601A6D9 RID: 108249 RVA: 0x00362213 File Offset: 0x00360413
		public override string LocalName
		{
			get
			{
				return "lsdException";
			}
		}

		// Token: 0x1700933F RID: 37695
		// (get) Token: 0x0601A6DA RID: 108250 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009340 RID: 37696
		// (get) Token: 0x0601A6DB RID: 108251 RVA: 0x0036221A File Offset: 0x0036041A
		internal override int ElementTypeId
		{
			get
			{
				return 11911;
			}
		}

		// Token: 0x0601A6DC RID: 108252 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009341 RID: 37697
		// (get) Token: 0x0601A6DD RID: 108253 RVA: 0x00362221 File Offset: 0x00360421
		internal override string[] AttributeTagNames
		{
			get
			{
				return LatentStyleExceptionInfo.attributeTagNames;
			}
		}

		// Token: 0x17009342 RID: 37698
		// (get) Token: 0x0601A6DE RID: 108254 RVA: 0x00362228 File Offset: 0x00360428
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LatentStyleExceptionInfo.attributeNamespaceIds;
			}
		}

		// Token: 0x17009343 RID: 37699
		// (get) Token: 0x0601A6DF RID: 108255 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A6E0 RID: 108256 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17009344 RID: 37700
		// (get) Token: 0x0601A6E1 RID: 108257 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601A6E2 RID: 108258 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "locked")]
		public OnOffValue Locked
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009345 RID: 37701
		// (get) Token: 0x0601A6E3 RID: 108259 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601A6E4 RID: 108260 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "uiPriority")]
		public Int32Value UiPriority
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17009346 RID: 37702
		// (get) Token: 0x0601A6E5 RID: 108261 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601A6E6 RID: 108262 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "semiHidden")]
		public OnOffValue SemiHidden
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17009347 RID: 37703
		// (get) Token: 0x0601A6E7 RID: 108263 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x0601A6E8 RID: 108264 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "unhideWhenUsed")]
		public OnOffValue UnhideWhenUsed
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17009348 RID: 37704
		// (get) Token: 0x0601A6E9 RID: 108265 RVA: 0x003461FC File Offset: 0x003443FC
		// (set) Token: 0x0601A6EA RID: 108266 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "qFormat")]
		public OnOffValue PrimaryStyle
		{
			get
			{
				return (OnOffValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0601A6EC RID: 108268 RVA: 0x00362230 File Offset: 0x00360430
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "locked" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "uiPriority" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "semiHidden" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "unhideWhenUsed" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "qFormat" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A6ED RID: 108269 RVA: 0x003622D5 File Offset: 0x003604D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LatentStyleExceptionInfo>(deep);
		}

		// Token: 0x0400ACEF RID: 44271
		private const string tagName = "lsdException";

		// Token: 0x0400ACF0 RID: 44272
		private const byte tagNsId = 23;

		// Token: 0x0400ACF1 RID: 44273
		internal const int ElementTypeIdConst = 11911;

		// Token: 0x0400ACF2 RID: 44274
		private static string[] attributeTagNames = new string[] { "name", "locked", "uiPriority", "semiHidden", "unhideWhenUsed", "qFormat" };

		// Token: 0x0400ACF3 RID: 44275
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
