using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D07 RID: 7431
	internal static class PackageEditsHelper
	{
		// Token: 0x0600B95F RID: 47455 RVA: 0x00258BB0 File Offset: 0x00256DB0
		public static SegmentedString ApplyOrderedEdits(SegmentedString source, IEnumerable<PackageEdit> orderedEdits)
		{
			SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
			int num = 0;
			foreach (PackageEdit packageEdit in orderedEdits)
			{
				int offset = packageEdit.Offset;
				segmentedStringBuilder.Append(source, num, offset - num);
				segmentedStringBuilder.Append(packageEdit.Text);
				num = packageEdit.Offset + packageEdit.Length;
			}
			segmentedStringBuilder.Append(source, num, source.Length - num);
			return segmentedStringBuilder.ToSegmentedString();
		}
	}
}
