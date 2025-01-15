using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200013B RID: 315
	internal sealed class ModelSnapshot : ServerSnapshot
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x0002E6FA File Offset: 0x0002C8FA
		private ModelSnapshot(Guid snapshotDataID)
			: base(snapshotDataID, true)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ModelSnapshot({0}) constructor of existing snapshot.", new object[] { snapshotDataID });
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002E72F File Offset: 0x0002C92F
		private ModelSnapshot(ModelSnapshot snapshotDataToCopy)
			: base(snapshotDataToCopy)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "### ModelSnapshot(copy {0})", new object[] { base.SnapshotDataID });
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002E768 File Offset: 0x0002C968
		public static ModelSnapshot Create()
		{
			return ModelSnapshot.Create(Guid.NewGuid());
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002E774 File Offset: 0x0002C974
		public static ModelSnapshot Create(Guid snapshotDataID)
		{
			return new ModelSnapshot(snapshotDataID);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002E77C File Offset: 0x0002C97C
		public Stream GetChunk(string name)
		{
			string text;
			return base.GetChunk(name, ReportProcessing.ReportChunkTypes.Main, out text);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002E793 File Offset: 0x0002C993
		public Stream CreateChunk(string name)
		{
			return base.CreateChunk(name, ReportProcessing.ReportChunkTypes.Main, null);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00005BF2 File Offset: 0x00003DF2
		[Obsolete("Use PrepareExecutionSnasphot", true)]
		public override void CopyImageChunksTo(SnapshotBase target)
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void PrepareExecutionSnapshot(SnapshotBase target, string compiledDefinitionChunkName)
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002E79E File Offset: 0x0002C99E
		public override SnapshotBase Duplicate()
		{
			return new ModelSnapshot(this);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002E7A6 File Offset: 0x0002C9A6
		public override void WriteNewSnapshotToDB(ParameterInfoCollection parameters, DateTime createdDate, string description)
		{
			base.WriteNewSnapshotToDBImpl(parameters, createdDate, description, 0);
		}
	}
}
