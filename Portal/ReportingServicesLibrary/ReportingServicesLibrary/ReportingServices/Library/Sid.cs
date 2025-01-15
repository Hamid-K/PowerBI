using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000224 RID: 548
	internal sealed class Sid
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x0004A204 File Offset: 0x00048404
		internal Sid(byte[] data)
		{
			if (data == null)
			{
				throw new InternalCatalogException("unexpected use of the class - invalid argument for constructor");
			}
			this.m_data = data;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004A224 File Offset: 0x00048424
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Sid sid = (Sid)obj;
			if (sid.m_data.Length != this.m_data.Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_data.Length; i++)
			{
				if (sid.m_data[i] != this.m_data[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004A27C File Offset: 0x0004847C
		public override int GetHashCode()
		{
			int num = 0;
			foreach (byte b in this.m_data)
			{
				num ^= b.GetHashCode();
			}
			return num;
		}

		// Token: 0x040006F7 RID: 1783
		private byte[] m_data;
	}
}
