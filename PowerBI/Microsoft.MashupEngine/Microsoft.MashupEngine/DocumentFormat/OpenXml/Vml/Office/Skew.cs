using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002206 RID: 8710
	[GeneratedCode("DomGen", "2.0")]
	internal class Skew : OpenXmlLeafElement
	{
		// Token: 0x1700385C RID: 14428
		// (get) Token: 0x0600DE4F RID: 56911 RVA: 0x002BE056 File Offset: 0x002BC256
		public override string LocalName
		{
			get
			{
				return "skew";
			}
		}

		// Token: 0x1700385D RID: 14429
		// (get) Token: 0x0600DE50 RID: 56912 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700385E RID: 14430
		// (get) Token: 0x0600DE51 RID: 56913 RVA: 0x002BE05D File Offset: 0x002BC25D
		internal override int ElementTypeId
		{
			get
			{
				return 12404;
			}
		}

		// Token: 0x0600DE52 RID: 56914 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700385F RID: 14431
		// (get) Token: 0x0600DE53 RID: 56915 RVA: 0x002BE064 File Offset: 0x002BC264
		internal override string[] AttributeTagNames
		{
			get
			{
				return Skew.attributeTagNames;
			}
		}

		// Token: 0x17003860 RID: 14432
		// (get) Token: 0x0600DE54 RID: 56916 RVA: 0x002BE06B File Offset: 0x002BC26B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Skew.attributeNamespaceIds;
			}
		}

		// Token: 0x17003861 RID: 14433
		// (get) Token: 0x0600DE55 RID: 56917 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DE56 RID: 56918 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003862 RID: 14434
		// (get) Token: 0x0600DE57 RID: 56919 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600DE58 RID: 56920 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003863 RID: 14435
		// (get) Token: 0x0600DE59 RID: 56921 RVA: 0x002BDE2B File Offset: 0x002BC02B
		// (set) Token: 0x0600DE5A RID: 56922 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
		{
			get
			{
				return (TrueFalseValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003864 RID: 14436
		// (get) Token: 0x0600DE5B RID: 56923 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600DE5C RID: 56924 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "offset")]
		public StringValue Offset
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003865 RID: 14437
		// (get) Token: 0x0600DE5D RID: 56925 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600DE5E RID: 56926 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "origin")]
		public StringValue Origin
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003866 RID: 14438
		// (get) Token: 0x0600DE5F RID: 56927 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600DE60 RID: 56928 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "matrix")]
		public StringValue Matrix
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0600DE62 RID: 56930 RVA: 0x002BE090 File Offset: 0x002BC290
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "offset" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "origin" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "matrix" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DE63 RID: 56931 RVA: 0x002BE12B File Offset: 0x002BC32B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Skew>(deep);
		}

		// Token: 0x0600DE64 RID: 56932 RVA: 0x002BE134 File Offset: 0x002BC334
		// Note: this type is marked as 'beforefieldinit'.
		static Skew()
		{
			byte[] array = new byte[6];
			array[0] = 26;
			Skew.attributeNamespaceIds = array;
		}

		// Token: 0x04006D6D RID: 28013
		private const string tagName = "skew";

		// Token: 0x04006D6E RID: 28014
		private const byte tagNsId = 27;

		// Token: 0x04006D6F RID: 28015
		internal const int ElementTypeIdConst = 12404;

		// Token: 0x04006D70 RID: 28016
		private static string[] attributeTagNames = new string[] { "ext", "id", "on", "offset", "origin", "matrix" };

		// Token: 0x04006D71 RID: 28017
		private static byte[] attributeNamespaceIds;
	}
}
