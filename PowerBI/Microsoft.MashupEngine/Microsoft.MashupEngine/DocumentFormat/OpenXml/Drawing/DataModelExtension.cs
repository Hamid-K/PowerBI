using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Drawing;
using DocumentFormat.OpenXml.Office2010.Drawing.Diagram;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200282C RID: 10284
	[ChildElementInfo(typeof(RecolorImages), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DataModelExtensionBlock), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataModelExtension : OpenXmlCompositeElement
	{
		// Token: 0x170065E9 RID: 26089
		// (get) Token: 0x06014243 RID: 82499 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170065EA RID: 26090
		// (get) Token: 0x06014244 RID: 82500 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065EB RID: 26091
		// (get) Token: 0x06014245 RID: 82501 RVA: 0x0030FB58 File Offset: 0x0030DD58
		internal override int ElementTypeId
		{
			get
			{
				return 10317;
			}
		}

		// Token: 0x06014246 RID: 82502 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065EC RID: 26092
		// (get) Token: 0x06014247 RID: 82503 RVA: 0x0030FB5F File Offset: 0x0030DD5F
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataModelExtension.attributeTagNames;
			}
		}

		// Token: 0x170065ED RID: 26093
		// (get) Token: 0x06014248 RID: 82504 RVA: 0x0030FB66 File Offset: 0x0030DD66
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataModelExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170065EE RID: 26094
		// (get) Token: 0x06014249 RID: 82505 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601424A RID: 82506 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x0601424B RID: 82507 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataModelExtension()
		{
		}

		// Token: 0x0601424C RID: 82508 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataModelExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601424D RID: 82509 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataModelExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601424E RID: 82510 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataModelExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601424F RID: 82511 RVA: 0x0030FB6D File Offset: 0x0030DD6D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (56 == namespaceId && "dataModelExt" == name)
			{
				return new DataModelExtensionBlock();
			}
			if (58 == namespaceId && "recolorImg" == name)
			{
				return new RecolorImages();
			}
			return null;
		}

		// Token: 0x06014250 RID: 82512 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014251 RID: 82513 RVA: 0x0030FBA0 File Offset: 0x0030DDA0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataModelExtension>(deep);
		}

		// Token: 0x06014252 RID: 82514 RVA: 0x0030FBAC File Offset: 0x0030DDAC
		// Note: this type is marked as 'beforefieldinit'.
		static DataModelExtension()
		{
			byte[] array = new byte[1];
			DataModelExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04008936 RID: 35126
		private const string tagName = "ext";

		// Token: 0x04008937 RID: 35127
		private const byte tagNsId = 10;

		// Token: 0x04008938 RID: 35128
		internal const int ElementTypeIdConst = 10317;

		// Token: 0x04008939 RID: 35129
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x0400893A RID: 35130
		private static byte[] attributeNamespaceIds;
	}
}
