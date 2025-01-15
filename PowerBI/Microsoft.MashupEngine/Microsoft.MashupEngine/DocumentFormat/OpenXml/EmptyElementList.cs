using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002113 RID: 8467
	internal sealed class EmptyElementList : OpenXmlElementList
	{
		// Token: 0x0600D12F RID: 53551 RVA: 0x0029A264 File Offset: 0x00298464
		private EmptyElementList()
		{
		}

		// Token: 0x1700328B RID: 12939
		// (get) Token: 0x0600D130 RID: 53552 RVA: 0x0029A26C File Offset: 0x0029846C
		internal static EmptyElementList EmptyElementListSingleton
		{
			get
			{
				return EmptyElementList._EmptyElementList;
			}
		}

		// Token: 0x0600D131 RID: 53553 RVA: 0x001BAF19 File Offset: 0x001B9119
		public override OpenXmlElement GetItem(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x1700328C RID: 12940
		// (get) Token: 0x0600D132 RID: 53554 RVA: 0x00002105 File Offset: 0x00000305
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600D133 RID: 53555 RVA: 0x0029A273 File Offset: 0x00298473
		public override IEnumerator<OpenXmlElement> GetEnumerator()
		{
			return EmptyEnumerator<OpenXmlElement>.EmptyEnumeratorSingleton;
		}

		// Token: 0x0400693C RID: 26940
		private static readonly EmptyElementList _EmptyElementList = new EmptyElementList();
	}
}
