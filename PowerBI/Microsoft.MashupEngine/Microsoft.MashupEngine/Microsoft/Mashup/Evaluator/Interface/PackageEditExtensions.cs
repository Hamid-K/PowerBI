using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E1C RID: 7708
	public static class PackageEditExtensions
	{
		// Token: 0x0600BE02 RID: 48642 RVA: 0x002673A8 File Offset: 0x002655A8
		public static IDictionary<string, IList<PackageEdit>> GetSectionEdits(this IEnumerable<PackageEdit> edits)
		{
			Dictionary<string, IList<PackageEdit>> dictionary = new Dictionary<string, IList<PackageEdit>>();
			foreach (PackageEdit packageEdit in edits)
			{
				IList<PackageEdit> list;
				if (!dictionary.TryGetValue(packageEdit.Section, out list))
				{
					list = new List<PackageEdit>();
					dictionary.Add(packageEdit.Section, list);
				}
				list.Add(packageEdit);
			}
			return dictionary;
		}

		// Token: 0x0600BE03 RID: 48643 RVA: 0x0026741C File Offset: 0x0026561C
		public static SegmentedString Apply(this PackageEdit edit, SegmentedString source)
		{
			return PackageEditsHelper.ApplyOrderedEdits(source, new PackageEdit[] { edit });
		}

		// Token: 0x0600BE04 RID: 48644 RVA: 0x00267432 File Offset: 0x00265632
		public static SegmentedString Apply(this IEnumerable<PackageEdit> edits, SegmentedString source)
		{
			return PackageEditsHelper.ApplyOrderedEdits(source, edits.OrderBy((PackageEdit e) => e.Offset));
		}
	}
}
