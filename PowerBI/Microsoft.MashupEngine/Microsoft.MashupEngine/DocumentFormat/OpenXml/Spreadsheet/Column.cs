using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BCD RID: 11213
	[GeneratedCode("DomGen", "2.0")]
	internal class Column : OpenXmlLeafElement
	{
		// Token: 0x17007CED RID: 31981
		// (get) Token: 0x06017612 RID: 95762 RVA: 0x002E35A2 File Offset: 0x002E17A2
		public override string LocalName
		{
			get
			{
				return "col";
			}
		}

		// Token: 0x17007CEE RID: 31982
		// (get) Token: 0x06017613 RID: 95763 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CEF RID: 31983
		// (get) Token: 0x06017614 RID: 95764 RVA: 0x00336107 File Offset: 0x00334307
		internal override int ElementTypeId
		{
			get
			{
				return 11185;
			}
		}

		// Token: 0x06017615 RID: 95765 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007CF0 RID: 31984
		// (get) Token: 0x06017616 RID: 95766 RVA: 0x0033610E File Offset: 0x0033430E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Column.attributeTagNames;
			}
		}

		// Token: 0x17007CF1 RID: 31985
		// (get) Token: 0x06017617 RID: 95767 RVA: 0x00336115 File Offset: 0x00334315
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Column.attributeNamespaceIds;
			}
		}

		// Token: 0x17007CF2 RID: 31986
		// (get) Token: 0x06017618 RID: 95768 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017619 RID: 95769 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "min")]
		public UInt32Value Min
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007CF3 RID: 31987
		// (get) Token: 0x0601761A RID: 95770 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601761B RID: 95771 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "max")]
		public UInt32Value Max
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007CF4 RID: 31988
		// (get) Token: 0x0601761C RID: 95772 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x0601761D RID: 95773 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "width")]
		public DoubleValue Width
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007CF5 RID: 31989
		// (get) Token: 0x0601761E RID: 95774 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601761F RID: 95775 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "style")]
		public UInt32Value Style
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007CF6 RID: 31990
		// (get) Token: 0x06017620 RID: 95776 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017621 RID: 95777 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007CF7 RID: 31991
		// (get) Token: 0x06017622 RID: 95778 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017623 RID: 95779 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "bestFit")]
		public BooleanValue BestFit
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007CF8 RID: 31992
		// (get) Token: 0x06017624 RID: 95780 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017625 RID: 95781 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "customWidth")]
		public BooleanValue CustomWidth
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007CF9 RID: 31993
		// (get) Token: 0x06017626 RID: 95782 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017627 RID: 95783 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "phonetic")]
		public BooleanValue Phonetic
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007CFA RID: 31994
		// (get) Token: 0x06017628 RID: 95784 RVA: 0x00334AF1 File Offset: 0x00332CF1
		// (set) Token: 0x06017629 RID: 95785 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "outlineLevel")]
		public ByteValue OutlineLevel
		{
			get
			{
				return (ByteValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007CFB RID: 31995
		// (get) Token: 0x0601762A RID: 95786 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601762B RID: 95787 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "collapsed")]
		public BooleanValue Collapsed
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x0601762D RID: 95789 RVA: 0x0033611C File Offset: 0x0033431C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "min" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "max" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "width" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "bestFit" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "customWidth" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "phonetic" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outlineLevel" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "collapsed" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601762E RID: 95790 RVA: 0x0033620D File Offset: 0x0033440D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Column>(deep);
		}

		// Token: 0x0601762F RID: 95791 RVA: 0x00336218 File Offset: 0x00334418
		// Note: this type is marked as 'beforefieldinit'.
		static Column()
		{
			byte[] array = new byte[10];
			Column.attributeNamespaceIds = array;
		}

		// Token: 0x04009C20 RID: 39968
		private const string tagName = "col";

		// Token: 0x04009C21 RID: 39969
		private const byte tagNsId = 22;

		// Token: 0x04009C22 RID: 39970
		internal const int ElementTypeIdConst = 11185;

		// Token: 0x04009C23 RID: 39971
		private static string[] attributeTagNames = new string[] { "min", "max", "width", "style", "hidden", "bestFit", "customWidth", "phonetic", "outlineLevel", "collapsed" };

		// Token: 0x04009C24 RID: 39972
		private static byte[] attributeNamespaceIds;
	}
}
