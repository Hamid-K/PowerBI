using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD0 RID: 11216
	[GeneratedCode("DomGen", "2.0")]
	internal class Pane : OpenXmlLeafElement
	{
		// Token: 0x17007D0C RID: 32012
		// (get) Token: 0x06017650 RID: 95824 RVA: 0x00336403 File Offset: 0x00334603
		public override string LocalName
		{
			get
			{
				return "pane";
			}
		}

		// Token: 0x17007D0D RID: 32013
		// (get) Token: 0x06017651 RID: 95825 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D0E RID: 32014
		// (get) Token: 0x06017652 RID: 95826 RVA: 0x0033640A File Offset: 0x0033460A
		internal override int ElementTypeId
		{
			get
			{
				return 11189;
			}
		}

		// Token: 0x06017653 RID: 95827 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D0F RID: 32015
		// (get) Token: 0x06017654 RID: 95828 RVA: 0x00336411 File Offset: 0x00334611
		internal override string[] AttributeTagNames
		{
			get
			{
				return Pane.attributeTagNames;
			}
		}

		// Token: 0x17007D10 RID: 32016
		// (get) Token: 0x06017655 RID: 95829 RVA: 0x00336418 File Offset: 0x00334618
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Pane.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D11 RID: 32017
		// (get) Token: 0x06017656 RID: 95830 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06017657 RID: 95831 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "xSplit")]
		public DoubleValue HorizontalSplit
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007D12 RID: 32018
		// (get) Token: 0x06017658 RID: 95832 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x06017659 RID: 95833 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ySplit")]
		public DoubleValue VerticalSplit
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007D13 RID: 32019
		// (get) Token: 0x0601765A RID: 95834 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601765B RID: 95835 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "topLeftCell")]
		public StringValue TopLeftCell
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

		// Token: 0x17007D14 RID: 32020
		// (get) Token: 0x0601765C RID: 95836 RVA: 0x0033641F File Offset: 0x0033461F
		// (set) Token: 0x0601765D RID: 95837 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "activePane")]
		public EnumValue<PaneValues> ActivePane
		{
			get
			{
				return (EnumValue<PaneValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007D15 RID: 32021
		// (get) Token: 0x0601765E RID: 95838 RVA: 0x0033642E File Offset: 0x0033462E
		// (set) Token: 0x0601765F RID: 95839 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "state")]
		public EnumValue<PaneStateValues> State
		{
			get
			{
				return (EnumValue<PaneStateValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06017661 RID: 95841 RVA: 0x00336440 File Offset: 0x00334640
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "xSplit" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "ySplit" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "topLeftCell" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "activePane" == name)
			{
				return new EnumValue<PaneValues>();
			}
			if (namespaceId == 0 && "state" == name)
			{
				return new EnumValue<PaneStateValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017662 RID: 95842 RVA: 0x003364C3 File Offset: 0x003346C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Pane>(deep);
		}

		// Token: 0x06017663 RID: 95843 RVA: 0x003364CC File Offset: 0x003346CC
		// Note: this type is marked as 'beforefieldinit'.
		static Pane()
		{
			byte[] array = new byte[5];
			Pane.attributeNamespaceIds = array;
		}

		// Token: 0x04009C2F RID: 39983
		private const string tagName = "pane";

		// Token: 0x04009C30 RID: 39984
		private const byte tagNsId = 22;

		// Token: 0x04009C31 RID: 39985
		internal const int ElementTypeIdConst = 11189;

		// Token: 0x04009C32 RID: 39986
		private static string[] attributeTagNames = new string[] { "xSplit", "ySplit", "topLeftCell", "activePane", "state" };

		// Token: 0x04009C33 RID: 39987
		private static byte[] attributeNamespaceIds;
	}
}
