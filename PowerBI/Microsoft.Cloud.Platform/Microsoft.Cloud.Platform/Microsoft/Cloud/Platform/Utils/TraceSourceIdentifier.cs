using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000162 RID: 354
	[CannotApplyEqualityOperator]
	public class TraceSourceIdentifier : IEquatable<TraceSourceIdentifier>, IIdentifiable
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x0001FE92 File Offset: 0x0001E092
		public TraceSourceIdentifier([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			this.Name = name;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001FEAC File Offset: 0x0001E0AC
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		public string Name { get; private set; }

		// Token: 0x06000942 RID: 2370 RVA: 0x0001FEBD File Offset: 0x0001E0BD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
		public override bool Equals(object obj)
		{
			TraceSourceIdentifier traceSourceIdentifier = obj as TraceSourceIdentifier;
			return this.Equals(traceSourceIdentifier);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001FEE3 File Offset: 0x0001E0E3
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
		public bool Equals(TraceSourceIdentifier other)
		{
			return other != null && this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
