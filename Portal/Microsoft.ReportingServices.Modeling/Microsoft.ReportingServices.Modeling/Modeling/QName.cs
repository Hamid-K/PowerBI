using System;
using System.Xml;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000052 RID: 82
	public struct QName : IComparable<QName>
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000B43C File Offset: 0x0000963C
		public QName(string name)
		{
			this.m_name = name ?? string.Empty;
			this.m_ns = string.Empty;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B459 File Offset: 0x00009659
		public QName(string name, string ns)
		{
			this.m_name = name ?? string.Empty;
			this.m_ns = ns ?? string.Empty;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B47B File Offset: 0x0000967B
		internal QName(XmlQualifiedName qualifiedName)
		{
			this.m_name = qualifiedName.Name;
			this.m_ns = qualifiedName.Namespace;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000B495 File Offset: 0x00009695
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000B4A6 File Offset: 0x000096A6
		public string Name
		{
			get
			{
				return this.m_name ?? string.Empty;
			}
			set
			{
				this.m_name = value ?? string.Empty;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000B4B8 File Offset: 0x000096B8
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000B4C9 File Offset: 0x000096C9
		public string Namespace
		{
			get
			{
				return this.m_ns ?? string.Empty;
			}
			set
			{
				this.m_ns = value ?? string.Empty;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000B4DC File Offset: 0x000096DC
		public int CompareTo(QName other)
		{
			int num = this.Namespace.CompareTo(other.Namespace);
			if (num == 0)
			{
				return this.Name.CompareTo(other.Name);
			}
			return num;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000B513 File Offset: 0x00009713
		public bool Equals(QName other)
		{
			return this == other;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B521 File Offset: 0x00009721
		public override bool Equals(object obj)
		{
			return obj is QName && this == (QName)obj;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000B53E File Offset: 0x0000973E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B54B File Offset: 0x0000974B
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.m_ns))
			{
				return this.Name;
			}
			return this.Namespace + ":" + this.Name;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000B577 File Offset: 0x00009777
		public static bool operator ==(QName x, QName y)
		{
			return x.Name == y.Name && x.Namespace == y.Namespace;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000B5A3 File Offset: 0x000097A3
		public static bool operator !=(QName x, QName y)
		{
			return !(x == y);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public static QName Parse(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentNullException("s");
			}
			int num = s.LastIndexOf(':');
			if (num >= 0)
			{
				return QName.Verify(new QName(s.Substring(num + 1), s.Substring(0, num)));
			}
			return QName.Verify(new QName(s));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B604 File Offset: 0x00009804
		public static QName Verify(QName qname)
		{
			XmlConvert.VerifyNCName(qname.Name);
			return qname;
		}

		// Token: 0x04000200 RID: 512
		private string m_name;

		// Token: 0x04000201 RID: 513
		private string m_ns;
	}
}
