using System;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x020000DA RID: 218
	internal class BufferLocator
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0001CBFC File Offset: 0x0001CBFC
		public static bool operator ==(BufferLocator x, BufferLocator y)
		{
			bool flag = false;
			if (y != null && x != null)
			{
				flag = x.m_fileId == y.m_fileId && x.m_bufferNum == y.m_bufferNum;
			}
			else if (y == null && x == null)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001CC3C File Offset: 0x0001CC3C
		public static bool operator !=(BufferLocator x, BufferLocator y)
		{
			return !(x == y);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001CC54 File Offset: 0x0001CC54
		public static bool operator <(BufferLocator x, BufferLocator y)
		{
			return x.m_fileId < y.m_fileId || (x.m_fileId == y.m_fileId && x.m_bufferNum < y.m_bufferNum);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001CC90 File Offset: 0x0001CC90
		public static bool operator >(BufferLocator x, BufferLocator y)
		{
			return x.m_fileId > y.m_fileId || (x.m_fileId == y.m_fileId && x.m_bufferNum > y.m_bufferNum);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001CCCC File Offset: 0x0001CCCC
		public static bool operator <=(BufferLocator x, BufferLocator y)
		{
			return x < y || x == y;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001CCEC File Offset: 0x0001CCEC
		public static bool operator >=(BufferLocator x, BufferLocator y)
		{
			return x > y || x == y;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001CD0C File Offset: 0x0001CD0C
		public override int GetHashCode()
		{
			return (int)((ulong)((ulong)this.m_fileId << 16) | this.m_bufferNum);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001CD2C File Offset: 0x0001CD2C
		public override bool Equals(object o)
		{
			bool flag;
			try
			{
				flag = this == (BufferLocator)o;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001CD6C File Offset: 0x0001CD6C
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.m_fileId, this.m_bufferNum);
		}

		// Token: 0x040002B1 RID: 689
		public ushort m_fileId;

		// Token: 0x040002B2 RID: 690
		public ulong m_bufferNum;
	}
}
