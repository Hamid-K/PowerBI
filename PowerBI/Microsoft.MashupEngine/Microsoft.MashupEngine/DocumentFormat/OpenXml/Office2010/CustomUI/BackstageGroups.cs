using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002306 RID: 8966
	[ChildElementInfo(typeof(TaskFormGroup), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TaskGroup), FileFormatVersions.Office2010)]
	internal class BackstageGroups : OpenXmlCompositeElement
	{
		// Token: 0x170047CC RID: 18380
		// (get) Token: 0x0600FECA RID: 65226 RVA: 0x002DD701 File Offset: 0x002DB901
		public override string LocalName
		{
			get
			{
				return "firstColumn";
			}
		}

		// Token: 0x170047CD RID: 18381
		// (get) Token: 0x0600FECB RID: 65227 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170047CE RID: 18382
		// (get) Token: 0x0600FECC RID: 65228 RVA: 0x002DD708 File Offset: 0x002DB908
		internal override int ElementTypeId
		{
			get
			{
				return 13108;
			}
		}

		// Token: 0x0600FECD RID: 65229 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FECE RID: 65230 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageGroups()
		{
		}

		// Token: 0x0600FECF RID: 65231 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageGroups(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FED0 RID: 65232 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageGroups(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FED1 RID: 65233 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageGroups(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FED2 RID: 65234 RVA: 0x002DD710 File Offset: 0x002DB910
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "taskFormGroup" == name)
			{
				return new TaskFormGroup();
			}
			if (57 == namespaceId && "group" == name)
			{
				return new BackstageGroup();
			}
			if (57 == namespaceId && "taskGroup" == name)
			{
				return new TaskGroup();
			}
			return null;
		}

		// Token: 0x0600FED3 RID: 65235 RVA: 0x002DD766 File Offset: 0x002DB966
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageGroups>(deep);
		}

		// Token: 0x04007230 RID: 29232
		private const string tagName = "firstColumn";

		// Token: 0x04007231 RID: 29233
		private const byte tagNsId = 57;

		// Token: 0x04007232 RID: 29234
		internal const int ElementTypeIdConst = 13108;
	}
}
