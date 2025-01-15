using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B8 RID: 952
	public class SinkIdentifier : IEquatable<SinkIdentifier>
	{
		// Token: 0x06001D6B RID: 7531 RVA: 0x00070180 File Offset: 0x0006E380
		public SinkIdentifier([NotNull] string creatorType, [NotNull] string assembly, [NotNull] string sinkType, [NotNull] SinkParametersCollection parameters)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(creatorType, "creatorType");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(assembly, "assembly");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(sinkType, "sinkType");
			ExtendedDiagnostics.EnsureArgumentNotNull<SinkParametersCollection>(parameters, "parameters");
			this.CreatorType = creatorType;
			this.Assembly = assembly;
			this.SinkType = sinkType;
			this.Parameters = parameters;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x000701DD File Offset: 0x0006E3DD
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x000701E5 File Offset: 0x0006E3E5
		public string CreatorType { get; private set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000701EE File Offset: 0x0006E3EE
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x000701F6 File Offset: 0x0006E3F6
		public string Assembly { get; private set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000701FF File Offset: 0x0006E3FF
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00070207 File Offset: 0x0006E407
		public string SinkType { get; private set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00070210 File Offset: 0x0006E410
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x00070218 File Offset: 0x0006E418
		public SinkParametersCollection Parameters { get; private set; }

		// Token: 0x06001D74 RID: 7540 RVA: 0x00070224 File Offset: 0x0006E424
		public bool Equals(SinkIdentifier other)
		{
			return other != null && (this.CreatorType.Equals(other.CreatorType, StringComparison.Ordinal) && this.Assembly.Equals(other.Assembly, StringComparison.Ordinal) && this.SinkType.Equals(other.SinkType, StringComparison.Ordinal)) && this.Parameters.Equals(other.Parameters);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00070285 File Offset: 0x0006E485
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SinkIdentifier);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00070293 File Offset: 0x0006E493
		public override int GetHashCode()
		{
			return this.CreatorType.GetHashCode() ^ this.Assembly.GetHashCode() ^ this.SinkType.GetHashCode() ^ this.Parameters.GetHashCode();
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000702C4 File Offset: 0x0006E4C4
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<creator: {0}, assembly: {1}, type: {2}, params: ({3})>", new object[]
			{
				this.CreatorType,
				this.Assembly,
				this.SinkType,
				this.Parameters.ToString()
			});
		}
	}
}
