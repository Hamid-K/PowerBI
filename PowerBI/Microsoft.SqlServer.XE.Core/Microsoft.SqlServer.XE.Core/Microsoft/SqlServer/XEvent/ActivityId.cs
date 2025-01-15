using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class ActivityId
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003D3C File Offset: 0x00003D3C
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003D50 File Offset: 0x00003D50
		public Guid Id { get; internal set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003D64 File Offset: 0x00003D64
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003D78 File Offset: 0x00003D78
		public uint Sequence { get; internal set; }

		// Token: 0x06000074 RID: 116 RVA: 0x00003D8C File Offset: 0x00003D8C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", this.Id, this.Sequence);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DC0 File Offset: 0x00003DC0
		public static bool operator ==(ActivityId x, ActivityId y)
		{
			bool flag = false;
			if (y != null && x != null)
			{
				flag = x.Sequence == y.Sequence && x.Id == y.Id;
			}
			else if (y == null && x == null)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E04 File Offset: 0x00003E04
		public static bool operator !=(ActivityId x, ActivityId y)
		{
			return !(x == y);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E1C File Offset: 0x00003E1C
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.Sequence.GetHashCode();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E4C File Offset: 0x00003E4C
		public override bool Equals(object o)
		{
			bool flag;
			try
			{
				flag = this == (ActivityId)o;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			return flag;
		}
	}
}
