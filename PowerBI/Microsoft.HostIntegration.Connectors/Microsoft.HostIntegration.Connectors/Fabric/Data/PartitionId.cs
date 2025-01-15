using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C5 RID: 965
	[DataContract(Name = "PartitionId", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class PartitionId : IComparable<PartitionId>
	{
		// Token: 0x060021F3 RID: 8691 RVA: 0x00068970 File Offset: 0x00066B70
		public PartitionId(string serviceNamespace, int lowKey, int highKey)
		{
			this.ServiceNamespace = serviceNamespace;
			this.LowKey = lowKey;
			this.HighKey = highKey;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0006898D File Offset: 0x00066B8D
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x00068995 File Offset: 0x00066B95
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

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0006899E File Offset: 0x00066B9E
		// (set) Token: 0x060021F7 RID: 8695 RVA: 0x000689A6 File Offset: 0x00066BA6
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

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x000689AF File Offset: 0x00066BAF
		// (set) Token: 0x060021F9 RID: 8697 RVA: 0x000689B7 File Offset: 0x00066BB7
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

		// Token: 0x060021FA RID: 8698 RVA: 0x0006899E File Offset: 0x00066B9E
		public override int GetHashCode()
		{
			return this.m_lowKey;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000689C0 File Offset: 0x00066BC0
		public override bool Equals(object obj)
		{
			PartitionId partitionId = obj as PartitionId;
			return partitionId != null && this.m_lowKey == partitionId.m_lowKey && this.m_highKey == partitionId.m_highKey && this.m_serviceNamespace == partitionId.m_serviceNamespace;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00068A0C File Offset: 0x00066C0C
		public int CompareTo(PartitionId other)
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

		// Token: 0x060021FD RID: 8701 RVA: 0x00068A62 File Offset: 0x00066C62
		public static bool operator ==(PartitionId id1, PartitionId id2)
		{
			if (object.ReferenceEquals(id1, null))
			{
				return object.ReferenceEquals(id2, null);
			}
			return id1.CompareTo(id2) == 0;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00068A84 File Offset: 0x00066C84
		public static bool operator !=(PartitionId id1, PartitionId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00068A90 File Offset: 0x00066C90
		public static bool operator <=(PartitionId id1, PartitionId id2)
		{
			if (object.ReferenceEquals(id1, null))
			{
				throw new ArgumentNullException("id1");
			}
			if (object.ReferenceEquals(id2, null))
			{
				throw new ArgumentNullException("id2");
			}
			return id1.CompareTo(id2) <= 0;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00068AC7 File Offset: 0x00066CC7
		public static bool operator >(PartitionId id1, PartitionId id2)
		{
			return !(id1 <= id2);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00068AD3 File Offset: 0x00066CD3
		public static bool operator >=(PartitionId id1, PartitionId id2)
		{
			if (object.ReferenceEquals(id1, null))
			{
				throw new ArgumentNullException("id1");
			}
			if (object.ReferenceEquals(id2, null))
			{
				throw new ArgumentNullException("id2");
			}
			return id1.CompareTo(id2) >= 0;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00068B0A File Offset: 0x00066D0A
		public static bool operator <(PartitionId id1, PartitionId id2)
		{
			return !(id1 >= id2);
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00068B16 File Offset: 0x00066D16
		internal string TraceString
		{
			get
			{
				return this.m_lowKey.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00068B28 File Offset: 0x00066D28
		internal static PartitionId Parse(string data)
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
			return new PartitionId(array[0], int.Parse(array[1], CultureInfo.InvariantCulture), int.Parse(array[2], CultureInfo.InvariantCulture));
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00068B8C File Offset: 0x00066D8C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", new object[] { this.m_serviceNamespace, this.m_lowKey, this.m_highKey });
		}

		// Token: 0x04001591 RID: 5521
		private string m_serviceNamespace;

		// Token: 0x04001592 RID: 5522
		private int m_lowKey;

		// Token: 0x04001593 RID: 5523
		private int m_highKey;
	}
}
