using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200051D RID: 1309
	[CannotApplyEqualityOperator]
	[Serializable]
	public class ElementType : IEquatable<ElementType>
	{
		// Token: 0x06002882 RID: 10370 RVA: 0x000920BA File Offset: 0x000902BA
		public ElementType([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(name, "name");
			this.Name = name;
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x000920D4 File Offset: 0x000902D4
		// (set) Token: 0x06002884 RID: 10372 RVA: 0x000920DC File Offset: 0x000902DC
		public string Name { get; private set; }

		// Token: 0x06002885 RID: 10373 RVA: 0x000920E5 File Offset: 0x000902E5
		public bool Equals(ElementType other)
		{
			return other != null && this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000920FE File Offset: 0x000902FE
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0009210B File Offset: 0x0009030B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ElementType);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x00092119 File Offset: 0x00090319
		public override string ToString()
		{
			return this.Name;
		}
	}
}
