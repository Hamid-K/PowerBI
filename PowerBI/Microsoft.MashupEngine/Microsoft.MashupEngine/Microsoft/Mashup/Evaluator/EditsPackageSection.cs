using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C94 RID: 7316
	internal sealed class EditsPackageSection : IPackageSection, IDocumentHost, ICacheableDocumentHost
	{
		// Token: 0x0600B5F7 RID: 46583 RVA: 0x0024F2E0 File Offset: 0x0024D4E0
		public EditsPackageSection(IPackageSection section, IEnumerable<PackageEdit> sectionEdits)
		{
			this.section = section;
			this.orderedSectionEdits = sectionEdits.OrderBy((PackageEdit e) => e.Offset).ToArray<PackageEdit>();
		}

		// Token: 0x17002D6C RID: 11628
		// (get) Token: 0x0600B5F8 RID: 46584 RVA: 0x0024F31F File Offset: 0x0024D51F
		public string UniqueID
		{
			get
			{
				return (this.section.UniqueID ?? "(null)") + "/edits";
			}
		}

		// Token: 0x17002D6D RID: 11629
		// (get) Token: 0x0600B5F9 RID: 46585 RVA: 0x0024F33F File Offset: 0x0024D53F
		public object CacheIdentity
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x17002D6E RID: 11630
		// (get) Token: 0x0600B5FA RID: 46586 RVA: 0x0024F34C File Offset: 0x0024D54C
		public IPackageSectionConfig Config
		{
			get
			{
				return this.section.Config;
			}
		}

		// Token: 0x17002D6F RID: 11631
		// (get) Token: 0x0600B5FB RID: 46587 RVA: 0x0024F35C File Offset: 0x0024D55C
		public SegmentedString Text
		{
			get
			{
				if (this.text.Equals(default(SegmentedString)))
				{
					this.text = PackageEditsHelper.ApplyOrderedEdits(this.section.Text, this.orderedSectionEdits);
				}
				return this.text;
			}
		}

		// Token: 0x04005CF4 RID: 23796
		private const string nullToken = "(null)";

		// Token: 0x04005CF5 RID: 23797
		private readonly IPackageSection section;

		// Token: 0x04005CF6 RID: 23798
		private readonly PackageEdit[] orderedSectionEdits;

		// Token: 0x04005CF7 RID: 23799
		private SegmentedString text;
	}
}
