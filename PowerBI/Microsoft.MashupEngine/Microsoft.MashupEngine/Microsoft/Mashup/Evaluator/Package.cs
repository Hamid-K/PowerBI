using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D04 RID: 7428
	public sealed class Package : IPackage
	{
		// Token: 0x0600B950 RID: 47440 RVA: 0x00258A3F File Offset: 0x00256C3F
		public Package(Dictionary<string, IPackageSection> sections)
		{
			this.sections = sections;
		}

		// Token: 0x17002DDA RID: 11738
		// (get) Token: 0x0600B951 RID: 47441 RVA: 0x00258A4E File Offset: 0x00256C4E
		public IEnumerable<string> SectionNames
		{
			get
			{
				return this.sections.Keys;
			}
		}

		// Token: 0x0600B952 RID: 47442 RVA: 0x00258A5B File Offset: 0x00256C5B
		public IPackageSection GetSection(string sectionName)
		{
			return this.sections[sectionName];
		}

		// Token: 0x0600B953 RID: 47443 RVA: 0x00258A6C File Offset: 0x00256C6C
		public IPackage ApplyEdits(IEnumerable<PackageEdit> edits)
		{
			IDictionary<string, IList<PackageEdit>> sectionEdits = edits.GetSectionEdits();
			Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
			foreach (string text in this.sections.Keys)
			{
				IPackageSection packageSection = this.sections[text];
				IList<PackageEdit> list;
				if (sectionEdits.TryGetValue(text, out list))
				{
					packageSection = new PackageSection(packageSection.Config, packageSection.UniqueID, list.Apply(packageSection.Text));
				}
				dictionary.Add(text, packageSection);
			}
			return new Package(dictionary);
		}

		// Token: 0x04005E50 RID: 24144
		private readonly Dictionary<string, IPackageSection> sections;
	}
}
