using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003071 RID: 12401
	[GeneratedCode("DomGen", "2.0")]
	internal class DerivedFrom : OpenXmlLeafElement
	{
		// Token: 0x17009675 RID: 38517
		// (get) Token: 0x0601ADC6 RID: 110022 RVA: 0x00368843 File Offset: 0x00366A43
		public override string LocalName
		{
			get
			{
				return "derived-from";
			}
		}

		// Token: 0x17009676 RID: 38518
		// (get) Token: 0x0601ADC7 RID: 110023 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009677 RID: 38519
		// (get) Token: 0x0601ADC8 RID: 110024 RVA: 0x0036884E File Offset: 0x00366A4E
		internal override int ElementTypeId
		{
			get
			{
				return 12670;
			}
		}

		// Token: 0x0601ADC9 RID: 110025 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009678 RID: 38520
		// (get) Token: 0x0601ADCA RID: 110026 RVA: 0x00368855 File Offset: 0x00366A55
		internal override string[] AttributeTagNames
		{
			get
			{
				return DerivedFrom.attributeTagNames;
			}
		}

		// Token: 0x17009679 RID: 38521
		// (get) Token: 0x0601ADCB RID: 110027 RVA: 0x0036885C File Offset: 0x00366A5C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DerivedFrom.attributeNamespaceIds;
			}
		}

		// Token: 0x1700967A RID: 38522
		// (get) Token: 0x0601ADCC RID: 110028 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601ADCD RID: 110029 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "resource")]
		public StringValue Resource
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

		// Token: 0x1700967B RID: 38523
		// (get) Token: 0x0601ADCE RID: 110030 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601ADCF RID: 110031 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "composite")]
		public BooleanValue Composite
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

		// Token: 0x0601ADD1 RID: 110033 RVA: 0x00368863 File Offset: 0x00366A63
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "resource" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "composite" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601ADD2 RID: 110034 RVA: 0x00368899 File Offset: 0x00366A99
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DerivedFrom>(deep);
		}

		// Token: 0x0601ADD3 RID: 110035 RVA: 0x003688A4 File Offset: 0x00366AA4
		// Note: this type is marked as 'beforefieldinit'.
		static DerivedFrom()
		{
			byte[] array = new byte[2];
			DerivedFrom.attributeNamespaceIds = array;
		}

		// Token: 0x0400B20B RID: 45579
		private const string tagName = "derived-from";

		// Token: 0x0400B20C RID: 45580
		private const byte tagNsId = 44;

		// Token: 0x0400B20D RID: 45581
		internal const int ElementTypeIdConst = 12670;

		// Token: 0x0400B20E RID: 45582
		private static string[] attributeTagNames = new string[] { "resource", "composite" };

		// Token: 0x0400B20F RID: 45583
		private static byte[] attributeNamespaceIds;
	}
}
