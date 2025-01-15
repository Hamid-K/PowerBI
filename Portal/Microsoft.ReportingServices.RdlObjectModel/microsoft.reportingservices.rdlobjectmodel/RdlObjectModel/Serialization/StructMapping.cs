using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E6 RID: 742
	public class StructMapping : TypeMapping
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x00035E96 File Offset: 0x00034096
		public StructMapping(Type type)
			: base(type)
		{
			this.Elements = new NameTable<MemberMapping>();
			this.Attributes = new NameTable<MemberMapping>();
			this.m_members = new List<MemberMapping>();
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00035EC0 File Offset: 0x000340C0
		public MemberMapping GetAttribute(string name, string ns)
		{
			return this.Attributes[name, ns];
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00035ECF File Offset: 0x000340CF
		public MemberMapping GetElement(string name, string ns)
		{
			return this.Elements[name, ns];
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00035EE0 File Offset: 0x000340E0
		public void AddUseTypeInfo(string name, string ns)
		{
			StructMapping.UseTypeInfo useTypeInfo = default(StructMapping.UseTypeInfo);
			useTypeInfo.Name = name;
			useTypeInfo.Namespace = ns;
			if (this.m_useTypes == null)
			{
				this.m_useTypes = new List<StructMapping.UseTypeInfo>();
			}
			this.m_useTypes.Add(useTypeInfo);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00035F24 File Offset: 0x00034124
		public List<MemberMapping> GetTypeNameElements()
		{
			if (this.m_useTypes != null)
			{
				List<MemberMapping> list = new List<MemberMapping>();
				foreach (StructMapping.UseTypeInfo useTypeInfo in this.m_useTypes)
				{
					list.Add(this.Elements[useTypeInfo.Name, useTypeInfo.Namespace]);
				}
				return list;
			}
			return null;
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00035FA0 File Offset: 0x000341A0
		public List<MemberMapping> Members
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00035FA8 File Offset: 0x000341A8
		internal void ResolveChildAttributes()
		{
			if (this.ChildAttributes == null)
			{
				return;
			}
			for (int i = 0; i < this.ChildAttributes.Count; i++)
			{
				MemberMapping memberMapping = this.ChildAttributes[i];
				string elementName = ((XmlChildAttributeAttribute)memberMapping.XmlAttributes.XmlAttribute).ElementName;
				this.GetElement(elementName, "").AddChildAttribute(memberMapping);
			}
			this.ChildAttributes = null;
		}

		// Token: 0x0400070C RID: 1804
		public NameTable<MemberMapping> Elements;

		// Token: 0x0400070D RID: 1805
		public NameTable<MemberMapping> Attributes;

		// Token: 0x0400070E RID: 1806
		private readonly List<MemberMapping> m_members;

		// Token: 0x0400070F RID: 1807
		private List<StructMapping.UseTypeInfo> m_useTypes;

		// Token: 0x0200041A RID: 1050
		private struct UseTypeInfo
		{
			// Token: 0x040007EF RID: 2031
			public string Name;

			// Token: 0x040007F0 RID: 2032
			public string Namespace;
		}
	}
}
