using System;
using System.Globalization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200005A RID: 90
	internal sealed class DataShapeIdGenerator
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0000E188 File Offset: 0x0000C388
		internal string CreateIntersectionId()
		{
			string text = "I";
			int numIntersections = this._numIntersections;
			this._numIntersections = numIntersections + 1;
			return DataShapeIdGenerator.CreateId(text, numIntersections);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		internal string CreateMemberId()
		{
			string text = "DM";
			int numMembers = this._numMembers;
			this._numMembers = numMembers + 1;
			return DataShapeIdGenerator.CreateId(text, numMembers);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		internal string CreateGroupId()
		{
			string text = "G";
			int numGroups = this._numGroups;
			this._numGroups = numGroups + 1;
			return DataShapeIdGenerator.CreateId(text, numGroups);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000E200 File Offset: 0x0000C400
		internal string CreateKeyId()
		{
			string text = "K";
			int numKeys = this._numKeys;
			this._numKeys = numKeys + 1;
			return DataShapeIdGenerator.CreateId(text, numKeys);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000E228 File Offset: 0x0000C428
		internal string CreateMeasureId()
		{
			string text = "M";
			int numMeasures = this._numMeasures;
			this._numMeasures = numMeasures + 1;
			return DataShapeIdGenerator.CreateId(text, numMeasures);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000E250 File Offset: 0x0000C450
		internal string CreateAggregateId()
		{
			string text = "A";
			int numAggregates = this._numAggregates;
			this._numAggregates = numAggregates + 1;
			return DataShapeIdGenerator.CreateId(text, numAggregates);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000E278 File Offset: 0x0000C478
		internal string CreateLimitId()
		{
			string text = "L";
			int numLimits = this._numLimits;
			this._numLimits = numLimits + 1;
			return DataShapeIdGenerator.CreateId(text, numLimits);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		internal string CreateHighlightId()
		{
			string text = "H";
			int numHighlights = this._numHighlights;
			this._numHighlights = numHighlights + 1;
			return DataShapeIdGenerator.CreateId(text, numHighlights);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		internal string CreateTransformId()
		{
			string text = "T";
			int numTransforms = this._numTransforms;
			this._numTransforms = numTransforms + 1;
			return DataShapeIdGenerator.CreateId(text, numTransforms);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		internal string CreateSubqueryDataShapeId()
		{
			string text = "SQDS";
			int numSubqueryDataShapes = this._numSubqueryDataShapes;
			this._numSubqueryDataShapes = numSubqueryDataShapes + 1;
			return DataShapeIdGenerator.CreateId(text, numSubqueryDataShapes);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000E318 File Offset: 0x0000C518
		internal string CreateGroupSynchronizationDataShapeId()
		{
			string text = "SyncDS";
			int numGroupSynchronizationDataShapes = this._numGroupSynchronizationDataShapes;
			this._numGroupSynchronizationDataShapes = numGroupSynchronizationDataShapes + 1;
			return DataShapeIdGenerator.CreateId(text, numGroupSynchronizationDataShapes);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000E340 File Offset: 0x0000C540
		internal string CreateSynchronizationIndexId()
		{
			string text = "SyncI";
			int numSynchronizationIndex = this._numSynchronizationIndex;
			this._numSynchronizationIndex = numSynchronizationIndex + 1;
			return DataShapeIdGenerator.CreateId(text, numSynchronizationIndex);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000E368 File Offset: 0x0000C568
		internal static string CreateTransformColumnId(int columnIndex)
		{
			return DataShapeIdGenerator.CreateId("C", columnIndex);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000E375 File Offset: 0x0000C575
		internal static string CreateId(string prefix, int num)
		{
			return prefix + num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0400022F RID: 559
		private const string DataMemberPrefix = "DM";

		// Token: 0x04000230 RID: 560
		private const string GroupIdPrefix = "G";

		// Token: 0x04000231 RID: 561
		private const string KeyPrefix = "K";

		// Token: 0x04000232 RID: 562
		private const string IntersectionIdPrefix = "I";

		// Token: 0x04000233 RID: 563
		private const string MeasurePrefix = "M";

		// Token: 0x04000234 RID: 564
		private const string AggregatePrefix = "A";

		// Token: 0x04000235 RID: 565
		private const string LimitPrefix = "L";

		// Token: 0x04000236 RID: 566
		private const string HighlightPrefix = "H";

		// Token: 0x04000237 RID: 567
		private const string TransformPrefix = "T";

		// Token: 0x04000238 RID: 568
		private const string TransformColumnPrefix = "C";

		// Token: 0x04000239 RID: 569
		private const string SubqueryDataShapePrefix = "SQDS";

		// Token: 0x0400023A RID: 570
		private const string GroupSynchronizationDataShapePrefix = "SyncDS";

		// Token: 0x0400023B RID: 571
		private const string SynchronizationIndexIdPrefix = "SyncI";

		// Token: 0x0400023C RID: 572
		private int _numMembers;

		// Token: 0x0400023D RID: 573
		private int _numGroups;

		// Token: 0x0400023E RID: 574
		private int _numKeys;

		// Token: 0x0400023F RID: 575
		private int _numIntersections;

		// Token: 0x04000240 RID: 576
		private int _numMeasures;

		// Token: 0x04000241 RID: 577
		private int _numAggregates;

		// Token: 0x04000242 RID: 578
		private int _numLimits;

		// Token: 0x04000243 RID: 579
		private int _numHighlights;

		// Token: 0x04000244 RID: 580
		private int _numTransforms;

		// Token: 0x04000245 RID: 581
		private int _numSubqueryDataShapes;

		// Token: 0x04000246 RID: 582
		private int _numGroupSynchronizationDataShapes;

		// Token: 0x04000247 RID: 583
		private int _numSynchronizationIndex;
	}
}
