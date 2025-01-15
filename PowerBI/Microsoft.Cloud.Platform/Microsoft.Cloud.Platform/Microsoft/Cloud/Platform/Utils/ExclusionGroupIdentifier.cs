using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000153 RID: 339
	[CannotApplyEqualityOperator]
	public class ExclusionGroupIdentifier : IEquatable<ExclusionGroupIdentifier>
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		public ExclusionGroupIdentifier(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001F407 File Offset: 0x0001D607
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0001F40F File Offset: 0x0001D60F
		public string Name { get; private set; }

		// Token: 0x060008DB RID: 2267 RVA: 0x0001F418 File Offset: 0x0001D618
		public bool Equals(ExclusionGroupIdentifier other)
		{
			return other != null && this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001F431 File Offset: 0x0001D631
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExclusionGroupIdentifier);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001F43F File Offset: 0x0001D63F
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001F44C File Offset: 0x0001D64C
		public override string ToString()
		{
			return this.Name;
		}
	}
}
