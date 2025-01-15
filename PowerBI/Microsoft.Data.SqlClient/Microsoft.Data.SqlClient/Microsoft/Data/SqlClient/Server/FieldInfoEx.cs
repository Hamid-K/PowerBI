using System;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000132 RID: 306
	internal sealed class FieldInfoEx : IComparable
	{
		// Token: 0x06001842 RID: 6210 RVA: 0x000656F0 File Offset: 0x000638F0
		internal FieldInfoEx(FieldInfo fi, int offset, Normalizer normalizer)
		{
			this._offset = offset;
			this.FieldInfo = fi;
			this.Normalizer = normalizer;
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0006570D File Offset: 0x0006390D
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x00065715 File Offset: 0x00063915
		internal FieldInfo FieldInfo { get; private set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0006571E File Offset: 0x0006391E
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x00065726 File Offset: 0x00063926
		internal Normalizer Normalizer { get; private set; }

		// Token: 0x06001847 RID: 6215 RVA: 0x00065730 File Offset: 0x00063930
		public int CompareTo(object other)
		{
			FieldInfoEx fieldInfoEx = other as FieldInfoEx;
			if (fieldInfoEx == null)
			{
				return -1;
			}
			return this._offset.CompareTo(fieldInfoEx._offset);
		}

		// Token: 0x040009AF RID: 2479
		private readonly int _offset;
	}
}
