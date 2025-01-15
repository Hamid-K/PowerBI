using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B6 RID: 182
	[DataContract(Name = "CachePartitionId", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class CachePartitionId : IComparable<CachePartitionId>
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00014EF6 File Offset: 0x000130F6
		public CachePartitionId(string serviceNamespace, int lowKey, int highKey)
		{
			this.ServiceNamespace = serviceNamespace;
			this.LowKey = lowKey;
			this.HighKey = highKey;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00014F13 File Offset: 0x00013113
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00014F1B File Offset: 0x0001311B
		[DataMember]
		public string ServiceNamespace
		{
			get
			{
				return this.m_serviceNamespace;
			}
			private set
			{
				this.m_serviceNamespace = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00014F24 File Offset: 0x00013124
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00014F2C File Offset: 0x0001312C
		[DataMember]
		public int LowKey
		{
			get
			{
				return this.m_lowKey;
			}
			private set
			{
				this.m_lowKey = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00014F35 File Offset: 0x00013135
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00014F3D File Offset: 0x0001313D
		[DataMember]
		public int HighKey
		{
			get
			{
				return this.m_highKey;
			}
			private set
			{
				this.m_highKey = value;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00014F24 File Offset: 0x00013124
		public override int GetHashCode()
		{
			return this.m_lowKey;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00014F48 File Offset: 0x00013148
		public override bool Equals(object obj)
		{
			CachePartitionId cachePartitionId = obj as CachePartitionId;
			return cachePartitionId != null && this.m_lowKey == cachePartitionId.m_lowKey && this.m_highKey == cachePartitionId.m_highKey && this.m_serviceNamespace == cachePartitionId.m_serviceNamespace;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014F94 File Offset: 0x00013194
		public int CompareTo(CachePartitionId other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return 1;
			}
			int num = string.Compare(this.m_serviceNamespace, other.m_serviceNamespace, StringComparison.Ordinal);
			if (num == 0)
			{
				num = this.m_lowKey.CompareTo(other.m_lowKey);
			}
			if (num == 0)
			{
				num = this.m_highKey.CompareTo(other.m_highKey);
			}
			return num;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014FEA File Offset: 0x000131EA
		public static bool operator ==(CachePartitionId id1, CachePartitionId id2)
		{
			return object.ReferenceEquals(id1, id2) || (id1 != null && id2 != null && id1.CompareTo(id2) == 0);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00015009 File Offset: 0x00013209
		public static bool operator !=(CachePartitionId id1, CachePartitionId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015015 File Offset: 0x00013215
		public static bool operator <=(CachePartitionId id1, CachePartitionId id2)
		{
			return id1.CompareTo(id2) <= 0;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015024 File Offset: 0x00013224
		public static bool operator >(CachePartitionId id1, CachePartitionId id2)
		{
			return !(id1 <= id2);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015030 File Offset: 0x00013230
		public static bool operator >=(CachePartitionId id1, CachePartitionId id2)
		{
			return id1.CompareTo(id2) >= 0;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001503F File Offset: 0x0001323F
		public static bool operator <(CachePartitionId id1, CachePartitionId id2)
		{
			return !(id1 >= id2);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001504B File Offset: 0x0001324B
		internal string TraceString
		{
			get
			{
				return this.m_lowKey.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00015060 File Offset: 0x00013260
		internal static CachePartitionId Parse(string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			string[] array = data.Split(new char[] { '/' });
			if (array.Length != 3)
			{
				throw new FormatException("Invalid format for partition id");
			}
			return new CachePartitionId(array[0], int.Parse(array[1], CultureInfo.InvariantCulture), int.Parse(array[2], CultureInfo.InvariantCulture));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000150C4 File Offset: 0x000132C4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", new object[] { this.m_serviceNamespace, this.m_lowKey, this.m_highKey });
		}

		// Token: 0x04000341 RID: 833
		private string m_serviceNamespace;

		// Token: 0x04000342 RID: 834
		private int m_lowKey;

		// Token: 0x04000343 RID: 835
		private int m_highKey;
	}
}
