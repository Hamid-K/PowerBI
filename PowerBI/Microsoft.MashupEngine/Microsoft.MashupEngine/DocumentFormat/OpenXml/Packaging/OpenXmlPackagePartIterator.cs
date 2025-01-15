using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002121 RID: 8481
	internal class OpenXmlPackagePartIterator : IEnumerable<OpenXmlPart>, IEnumerable
	{
		// Token: 0x0600D1DD RID: 53725 RVA: 0x0029BCA3 File Offset: 0x00299EA3
		public OpenXmlPackagePartIterator(OpenXmlPackage package)
		{
			this._package = package;
		}

		// Token: 0x0600D1DE RID: 53726 RVA: 0x0029BCB2 File Offset: 0x00299EB2
		public IEnumerator<OpenXmlPart> GetEnumerator()
		{
			return this.GetPartsByBreadthFirstTraversal();
		}

		// Token: 0x0600D1DF RID: 53727 RVA: 0x0029BCBC File Offset: 0x00299EBC
		private IEnumerator<OpenXmlPart> GetPartsByBreadthFirstTraversal()
		{
			List<OpenXmlPart> list = new List<OpenXmlPart>();
			Queue<OpenXmlPart> queue = new Queue<OpenXmlPart>();
			using (IEnumerator<IdPartPair> enumerator = this._package.Parts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IdPartPair idPartPair = enumerator.Current;
					queue.Enqueue(idPartPair.OpenXmlPart);
				}
				goto IL_00B3;
			}
			IL_004B:
			OpenXmlPart openXmlPart = queue.Dequeue();
			list.Add(openXmlPart);
			foreach (IdPartPair idPartPair2 in openXmlPart.Parts)
			{
				if (!queue.Contains(idPartPair2.OpenXmlPart) && !list.Contains(idPartPair2.OpenXmlPart))
				{
					queue.Enqueue(idPartPair2.OpenXmlPart);
				}
			}
			IL_00B3:
			if (queue.Count <= 0)
			{
				return list.GetEnumerator();
			}
			goto IL_004B;
		}

		// Token: 0x0600D1E0 RID: 53728 RVA: 0x0029BDAC File Offset: 0x00299FAC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04006966 RID: 26982
		private OpenXmlPackage _package;
	}
}
