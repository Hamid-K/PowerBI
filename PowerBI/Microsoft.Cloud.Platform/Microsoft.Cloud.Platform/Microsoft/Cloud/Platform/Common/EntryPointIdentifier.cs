using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000511 RID: 1297
	[CannotApplyEqualityOperator]
	public class EntryPointIdentifier : IEquatable<EntryPointIdentifier>
	{
		// Token: 0x06002856 RID: 10326 RVA: 0x000919E4 File Offset: 0x0008FBE4
		public EntryPointIdentifier([NotNull] string entryPoint)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(entryPoint, "entryPoint");
			this.m_identifier = entryPoint;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000919FE File Offset: 0x0008FBFE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EntryPointIdentifier);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00091A0C File Offset: 0x0008FC0C
		public override int GetHashCode()
		{
			return this.m_identifier.GetHashCode();
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00091A19 File Offset: 0x0008FC19
		public override string ToString()
		{
			return this.m_identifier;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00091A21 File Offset: 0x0008FC21
		public bool Equals(EntryPointIdentifier other)
		{
			return other != null && other.m_identifier.Equals(this.m_identifier, StringComparison.Ordinal);
		}

		// Token: 0x04000DF2 RID: 3570
		private string m_identifier;
	}
}
