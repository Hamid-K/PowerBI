using System;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x0200039F RID: 927
	internal abstract class MemberMapping : Mapping
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0007D46F File Offset: 0x0007B66F
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0007D477 File Offset: 0x0007B677
		public string ParentElementName
		{
			get
			{
				return this.m_parentElementName;
			}
			set
			{
				this.m_parentElementName = value;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0007D480 File Offset: 0x0007B680
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0007D488 File Offset: 0x0007B688
		public string ParentElementNameSpace
		{
			get
			{
				return this.m_parentElementNameSpace;
			}
			set
			{
				if (value == null)
				{
					this.m_parentElementNameSpace = string.Empty;
					return;
				}
				this.m_parentElementNameSpace = value;
			}
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0007D4A0 File Offset: 0x0007B6A0
		public MemberMapping(Type type, string name, string ns, bool isReadOnly)
		{
			this.Name = name;
			this.Namespace = ns;
			this.MemberType = type;
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x06001E83 RID: 7811
		public abstract void SetValue(object obj, object value);

		// Token: 0x06001E84 RID: 7812
		public abstract object GetValue(object obj);

		// Token: 0x04000CE6 RID: 3302
		public bool IsReadOnly;

		// Token: 0x04000CE7 RID: 3303
		public Type MemberType;

		// Token: 0x04000CE8 RID: 3304
		public string Name;

		// Token: 0x04000CE9 RID: 3305
		public string Namespace;

		// Token: 0x04000CEA RID: 3306
		public XmlAttributes XmlAttributes;

		// Token: 0x04000CEB RID: 3307
		private string m_parentElementName;

		// Token: 0x04000CEC RID: 3308
		private string m_parentElementNameSpace;
	}
}
