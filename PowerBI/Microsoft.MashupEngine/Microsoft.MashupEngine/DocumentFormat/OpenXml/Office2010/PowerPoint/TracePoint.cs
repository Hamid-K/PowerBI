using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C5 RID: 9157
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TracePoint : OpenXmlLeafElement
	{
		// Token: 0x17004CEE RID: 19694
		// (get) Token: 0x060109FE RID: 68094 RVA: 0x002E56FB File Offset: 0x002E38FB
		public override string LocalName
		{
			get
			{
				return "tracePt";
			}
		}

		// Token: 0x17004CEF RID: 19695
		// (get) Token: 0x060109FF RID: 68095 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CF0 RID: 19696
		// (get) Token: 0x06010A00 RID: 68096 RVA: 0x002E5702 File Offset: 0x002E3902
		internal override int ElementTypeId
		{
			get
			{
				return 12811;
			}
		}

		// Token: 0x06010A01 RID: 68097 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CF1 RID: 19697
		// (get) Token: 0x06010A02 RID: 68098 RVA: 0x002E5709 File Offset: 0x002E3909
		internal override string[] AttributeTagNames
		{
			get
			{
				return TracePoint.attributeTagNames;
			}
		}

		// Token: 0x17004CF2 RID: 19698
		// (get) Token: 0x06010A03 RID: 68099 RVA: 0x002E5710 File Offset: 0x002E3910
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TracePoint.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CF3 RID: 19699
		// (get) Token: 0x06010A04 RID: 68100 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010A05 RID: 68101 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public StringValue Time
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

		// Token: 0x17004CF4 RID: 19700
		// (get) Token: 0x06010A06 RID: 68102 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06010A07 RID: 68103 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "x")]
		public Int64Value XCoordinate
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004CF5 RID: 19701
		// (get) Token: 0x06010A08 RID: 68104 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x06010A09 RID: 68105 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "y")]
		public Int64Value YCoordinate
		{
			get
			{
				return (Int64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06010A0B RID: 68107 RVA: 0x002E5718 File Offset: 0x002E3918
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "x" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010A0C RID: 68108 RVA: 0x002E576F File Offset: 0x002E396F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TracePoint>(deep);
		}

		// Token: 0x06010A0D RID: 68109 RVA: 0x002E5778 File Offset: 0x002E3978
		// Note: this type is marked as 'beforefieldinit'.
		static TracePoint()
		{
			byte[] array = new byte[3];
			TracePoint.attributeNamespaceIds = array;
		}

		// Token: 0x04007590 RID: 30096
		private const string tagName = "tracePt";

		// Token: 0x04007591 RID: 30097
		private const byte tagNsId = 49;

		// Token: 0x04007592 RID: 30098
		internal const int ElementTypeIdConst = 12811;

		// Token: 0x04007593 RID: 30099
		private static string[] attributeTagNames = new string[] { "t", "x", "y" };

		// Token: 0x04007594 RID: 30100
		private static byte[] attributeNamespaceIds;
	}
}
