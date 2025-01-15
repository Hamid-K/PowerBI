using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B45 RID: 11077
	[GeneratedCode("DomGen", "2.0")]
	internal class DataBinding : OpenXmlCompositeElement
	{
		// Token: 0x170077CA RID: 30666
		// (get) Token: 0x06016AF4 RID: 92916 RVA: 0x0032DD74 File Offset: 0x0032BF74
		public override string LocalName
		{
			get
			{
				return "DataBinding";
			}
		}

		// Token: 0x170077CB RID: 30667
		// (get) Token: 0x06016AF5 RID: 92917 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077CC RID: 30668
		// (get) Token: 0x06016AF6 RID: 92918 RVA: 0x0032DD7B File Offset: 0x0032BF7B
		internal override int ElementTypeId
		{
			get
			{
				return 11060;
			}
		}

		// Token: 0x06016AF7 RID: 92919 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170077CD RID: 30669
		// (get) Token: 0x06016AF8 RID: 92920 RVA: 0x0032DD82 File Offset: 0x0032BF82
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataBinding.attributeTagNames;
			}
		}

		// Token: 0x170077CE RID: 30670
		// (get) Token: 0x06016AF9 RID: 92921 RVA: 0x0032DD89 File Offset: 0x0032BF89
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataBinding.attributeNamespaceIds;
			}
		}

		// Token: 0x170077CF RID: 30671
		// (get) Token: 0x06016AFA RID: 92922 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016AFB RID: 92923 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "DataBindingName")]
		public StringValue DataBindingName
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

		// Token: 0x170077D0 RID: 30672
		// (get) Token: 0x06016AFC RID: 92924 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016AFD RID: 92925 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "FileBinding")]
		public BooleanValue FileBinding
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170077D1 RID: 30673
		// (get) Token: 0x06016AFE RID: 92926 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06016AFF RID: 92927 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ConnectionID")]
		public UInt32Value ConnectionId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170077D2 RID: 30674
		// (get) Token: 0x06016B00 RID: 92928 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016B01 RID: 92929 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "FileBindingName")]
		public StringValue FileBindingName
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

		// Token: 0x170077D3 RID: 30675
		// (get) Token: 0x06016B02 RID: 92930 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06016B03 RID: 92931 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "DataBindingLoadMode")]
		public UInt32Value DataBindingLoadMode
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06016B04 RID: 92932 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataBinding()
		{
		}

		// Token: 0x06016B05 RID: 92933 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataBinding(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016B06 RID: 92934 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataBinding(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016B07 RID: 92935 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataBinding(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016B08 RID: 92936 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x06016B09 RID: 92937 RVA: 0x0032DD90 File Offset: 0x0032BF90
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "DataBindingName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "FileBinding" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ConnectionID" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "FileBindingName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "DataBindingLoadMode" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016B0A RID: 92938 RVA: 0x0032DE13 File Offset: 0x0032C013
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataBinding>(deep);
		}

		// Token: 0x06016B0B RID: 92939 RVA: 0x0032DE1C File Offset: 0x0032C01C
		// Note: this type is marked as 'beforefieldinit'.
		static DataBinding()
		{
			byte[] array = new byte[5];
			DataBinding.attributeNamespaceIds = array;
		}

		// Token: 0x04009995 RID: 39317
		private const string tagName = "DataBinding";

		// Token: 0x04009996 RID: 39318
		private const byte tagNsId = 22;

		// Token: 0x04009997 RID: 39319
		internal const int ElementTypeIdConst = 11060;

		// Token: 0x04009998 RID: 39320
		private static string[] attributeTagNames = new string[] { "DataBindingName", "FileBinding", "ConnectionID", "FileBindingName", "DataBindingLoadMode" };

		// Token: 0x04009999 RID: 39321
		private static byte[] attributeNamespaceIds;
	}
}
