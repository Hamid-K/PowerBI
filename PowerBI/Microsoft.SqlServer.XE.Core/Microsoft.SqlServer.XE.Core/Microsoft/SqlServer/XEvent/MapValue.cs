using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000024 RID: 36
	public class MapValue
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003EA0 File Offset: 0x00003EA0
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003EB4 File Offset: 0x00003EB4
		public uint Key
		{
			get
			{
				return this.m_key;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003EC8 File Offset: 0x00003EC8
		public override string ToString()
		{
			return this.m_value;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003EDC File Offset: 0x00003EDC
		public override int GetHashCode()
		{
			return this.m_key.GetHashCode();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003EF4 File Offset: 0x00003EF4
		public static bool operator ==(MapValue x, MapValue y)
		{
			bool flag = false;
			if (y != null && x != null)
			{
				flag = x.m_key == y.m_key && x.m_value.Equals(y.m_value);
			}
			else if (y == null && x == null)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F38 File Offset: 0x00003F38
		public static bool operator !=(MapValue x, MapValue y)
		{
			return !(x == y);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003F50 File Offset: 0x00003F50
		public override bool Equals(object o)
		{
			bool flag;
			try
			{
				flag = this == (MapValue)o;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F90 File Offset: 0x00003F90
		public MapValue(uint key, string value)
		{
			this.m_key = key;
			this.m_value = value;
		}

		// Token: 0x04000052 RID: 82
		private uint m_key;

		// Token: 0x04000053 RID: 83
		private string m_value;
	}
}
