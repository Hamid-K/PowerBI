using System;
using System.Collections;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x020003A4 RID: 932
	internal class StructMapping : TypeMapping
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x0007D5CA File Offset: 0x0007B7CA
		public StructMapping(Type objectType)
		{
			this.m_objectType = objectType;
			this.elements = new NameTable();
			this.attributes = new NameTable();
			this.members = new ArrayList();
			this.m_parentXmlElements = new NameTable();
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0007D605 File Offset: 0x0007B805
		public Type ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0007D60D File Offset: 0x0007B80D
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0007D615 File Offset: 0x0007B815
		public string UseTypeName
		{
			get
			{
				return this.m_useTypeName;
			}
			set
			{
				this.m_useTypeName = value;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0007D61E File Offset: 0x0007B81E
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0007D626 File Offset: 0x0007B826
		public string UseTypeNameSpace
		{
			get
			{
				return this.m_useTypeNameSpace;
			}
			set
			{
				this.m_useTypeNameSpace = value;
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0007D62F File Offset: 0x0007B82F
		public MemberMapping GetElement(string name, string ns)
		{
			return (MemberMapping)this.elements[name, ns];
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0007D643 File Offset: 0x0007B843
		public MemberMapping GetTypeNameElement()
		{
			return (MemberMapping)this.elements[this.m_useTypeName, this.m_useTypeNameSpace];
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x0007D661 File Offset: 0x0007B861
		public ArrayList Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0007D66C File Offset: 0x0007B86C
		public void AddParentXmlElement(string name, string ns)
		{
			string text = string.Empty;
			if (ns != null)
			{
				text = ns;
			}
			this.m_parentXmlElements[name, text] = false;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0007D698 File Offset: 0x0007B898
		public void SetParentXmlElement(string name, string ns)
		{
			string text = string.Empty;
			if (ns != null)
			{
				text = ns;
			}
			this.m_parentXmlElements[name, text] = true;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0007D6C3 File Offset: 0x0007B8C3
		public object GetParentXmlElement(string name, string ns)
		{
			return this.m_parentXmlElements[name, ns];
		}

		// Token: 0x04000CEF RID: 3311
		internal NameTable elements;

		// Token: 0x04000CF0 RID: 3312
		internal NameTable attributes;

		// Token: 0x04000CF1 RID: 3313
		internal ArrayList members;

		// Token: 0x04000CF2 RID: 3314
		private NameTable m_parentXmlElements;

		// Token: 0x04000CF3 RID: 3315
		public Type m_objectType;

		// Token: 0x04000CF4 RID: 3316
		private string m_useTypeName;

		// Token: 0x04000CF5 RID: 3317
		private string m_useTypeNameSpace;
	}
}
