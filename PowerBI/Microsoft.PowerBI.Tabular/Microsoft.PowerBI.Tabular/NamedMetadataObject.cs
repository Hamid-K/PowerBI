using System;
using System.Diagnostics;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000104 RID: 260
	[DebuggerDisplay("{Name}")]
	public abstract class NamedMetadataObject : MetadataObject
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x0007B7C2 File Offset: 0x000799C2
		private protected NamedMetadataObject()
		{
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001122 RID: 4386
		// (set) Token: 0x06001123 RID: 4387
		public abstract string Name { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0007B7CA File Offset: 0x000799CA
		internal bool RenameRequestedThroughAPI
		{
			get
			{
				return ((INamedMetadataObjectBody)this.Body).RenameRequestedThroughAPI;
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0007B7DC File Offset: 0x000799DC
		internal bool WasObjectRenamed(out string originalName)
		{
			INamedMetadataObjectBody namedMetadataObjectBody = (INamedMetadataObjectBody)this.Body;
			if (namedMetadataObjectBody.CreatedFrom == null || namedMetadataObjectBody.CreatedFrom.Savepoint == null)
			{
				originalName = null;
				return false;
			}
			string objectName = namedMetadataObjectBody.GetObjectName();
			originalName = ((INamedMetadataObjectBody)namedMetadataObjectBody.CreatedFrom).GetObjectName();
			return !PropertyHelper.AreValuesIdentical(originalName, objectName);
		}
	}
}
