using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x0200001F RID: 31
	internal static class ReconciliationUtils
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000490B File Offset: 0x00002B0B
		internal static ReadOnlyCollection<TRDSD> Reconcile<TDSD, TRDSD>(this IList<TDSD> dsdItems, Func<TDSD, TRDSD> reconcileFunc)
		{
			if (dsdItems == null)
			{
				return null;
			}
			return dsdItems.ReconcileWritable(reconcileFunc).AsReadOnly();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004920 File Offset: 0x00002B20
		internal static List<TRDSD> ReconcileWritable<TDSD, TRDSD>(this IList<TDSD> dsdItems, Func<TDSD, TRDSD> reconcileFunc)
		{
			if (dsdItems == null)
			{
				return null;
			}
			List<TRDSD> list = new List<TRDSD>(dsdItems.Count);
			foreach (TDSD tdsd in dsdItems)
			{
				TRDSD trdsd = reconcileFunc(tdsd);
				list.Add(trdsd);
			}
			return list;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004984 File Offset: 0x00002B84
		internal static ReadOnlyCollection<IList<TRDSD>> Reconcile<TDSD, TRDSD>(this IList<IList<TDSD>> dsdItems, Func<TDSD, TRDSD> reconcileFunc)
		{
			if (dsdItems == null)
			{
				return null;
			}
			List<IList<TRDSD>> list = new List<IList<TRDSD>>(dsdItems.Count);
			foreach (IList<TDSD> list2 in dsdItems)
			{
				ReadOnlyCollection<TRDSD> readOnlyCollection = list2.Reconcile(reconcileFunc);
				list.Add(readOnlyCollection);
			}
			return list.AsReadOnly();
		}
	}
}
