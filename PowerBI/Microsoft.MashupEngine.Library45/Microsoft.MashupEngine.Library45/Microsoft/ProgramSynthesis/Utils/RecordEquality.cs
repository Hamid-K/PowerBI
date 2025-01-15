using System;
using System.Collections;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004F0 RID: 1264
	public class RecordEquality : IEqualityComparer
	{
		// Token: 0x06001C19 RID: 7193 RVA: 0x00002130 File Offset: 0x00000330
		private RecordEquality()
		{
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x000541C6 File Offset: 0x000523C6
		public static RecordEquality Comparer
		{
			get
			{
				return RecordEquality.Lazy.Value;
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000541D4 File Offset: 0x000523D4
		public bool Equals(object x, object y)
		{
			Type[] genericArguments = x.GetType().GetGenericArguments();
			Type[] genericArguments2 = y.GetType().GetGenericArguments();
			if (genericArguments.Length != genericArguments2.Length)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				object recordItem = x.GetRecordItem(i);
				object recordItem2 = y.GetRecordItem(i);
				if (!ValueEquality.Comparer.Equals(recordItem, recordItem2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00054234 File Offset: 0x00052434
		public int GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x04000DB1 RID: 3505
		private static readonly Lazy<RecordEquality> Lazy = new Lazy<RecordEquality>(() => new RecordEquality());
	}
}
