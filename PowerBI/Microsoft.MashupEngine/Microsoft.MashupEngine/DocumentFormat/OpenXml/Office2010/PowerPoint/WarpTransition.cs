using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A7 RID: 9127
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class WarpTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C21 RID: 19489
		// (get) Token: 0x06010842 RID: 67650 RVA: 0x002E448B File Offset: 0x002E268B
		public override string LocalName
		{
			get
			{
				return "warp";
			}
		}

		// Token: 0x17004C22 RID: 19490
		// (get) Token: 0x06010843 RID: 67651 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C23 RID: 19491
		// (get) Token: 0x06010844 RID: 67652 RVA: 0x002E4492 File Offset: 0x002E2692
		internal override int ElementTypeId
		{
			get
			{
				return 12781;
			}
		}

		// Token: 0x06010845 RID: 67653 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C24 RID: 19492
		// (get) Token: 0x06010846 RID: 67654 RVA: 0x002E4499 File Offset: 0x002E2699
		internal override string[] AttributeTagNames
		{
			get
			{
				return WarpTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C25 RID: 19493
		// (get) Token: 0x06010847 RID: 67655 RVA: 0x002E44A0 File Offset: 0x002E26A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WarpTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C26 RID: 19494
		// (get) Token: 0x06010848 RID: 67656 RVA: 0x002E44A7 File Offset: 0x002E26A7
		// (set) Token: 0x06010849 RID: 67657 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionInOutDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionInOutDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601084B RID: 67659 RVA: 0x002E44B6 File Offset: 0x002E26B6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionInOutDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601084C RID: 67660 RVA: 0x002E44D6 File Offset: 0x002E26D6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WarpTransition>(deep);
		}

		// Token: 0x0601084D RID: 67661 RVA: 0x002E44E0 File Offset: 0x002E26E0
		// Note: this type is marked as 'beforefieldinit'.
		static WarpTransition()
		{
			byte[] array = new byte[1];
			WarpTransition.attributeNamespaceIds = array;
		}

		// Token: 0x04007503 RID: 29955
		private const string tagName = "warp";

		// Token: 0x04007504 RID: 29956
		private const byte tagNsId = 49;

		// Token: 0x04007505 RID: 29957
		internal const int ElementTypeIdConst = 12781;

		// Token: 0x04007506 RID: 29958
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x04007507 RID: 29959
		private static byte[] attributeNamespaceIds;
	}
}
