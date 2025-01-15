using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A10 RID: 10768
	[GeneratedCode("DomGen", "2.0")]
	internal class TimeNode : OpenXmlLeafElement
	{
		// Token: 0x17006FB9 RID: 28601
		// (get) Token: 0x060158B7 RID: 88247 RVA: 0x003206A4 File Offset: 0x0031E8A4
		public override string LocalName
		{
			get
			{
				return "tn";
			}
		}

		// Token: 0x17006FBA RID: 28602
		// (get) Token: 0x060158B8 RID: 88248 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FBB RID: 28603
		// (get) Token: 0x060158B9 RID: 88249 RVA: 0x003206AB File Offset: 0x0031E8AB
		internal override int ElementTypeId
		{
			get
			{
				return 12196;
			}
		}

		// Token: 0x060158BA RID: 88250 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006FBC RID: 28604
		// (get) Token: 0x060158BB RID: 88251 RVA: 0x003206B2 File Offset: 0x0031E8B2
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeNode.attributeTagNames;
			}
		}

		// Token: 0x17006FBD RID: 28605
		// (get) Token: 0x060158BC RID: 88252 RVA: 0x003206B9 File Offset: 0x0031E8B9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeNode.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FBE RID: 28606
		// (get) Token: 0x060158BD RID: 88253 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060158BE RID: 88254 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt32Value Val
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

		// Token: 0x060158C0 RID: 88256 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060158C1 RID: 88257 RVA: 0x003206C0 File Offset: 0x0031E8C0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TimeNode>(deep);
		}

		// Token: 0x060158C2 RID: 88258 RVA: 0x003206CC File Offset: 0x0031E8CC
		// Note: this type is marked as 'beforefieldinit'.
		static TimeNode()
		{
			byte[] array = new byte[1];
			TimeNode.attributeNamespaceIds = array;
		}

		// Token: 0x040093CE RID: 37838
		private const string tagName = "tn";

		// Token: 0x040093CF RID: 37839
		private const byte tagNsId = 24;

		// Token: 0x040093D0 RID: 37840
		internal const int ElementTypeIdConst = 12196;

		// Token: 0x040093D1 RID: 37841
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040093D2 RID: 37842
		private static byte[] attributeNamespaceIds;
	}
}
