using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.DeltaLake;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001F02 RID: 7938
	internal class DeltaValueVersions : ValueVersions
	{
		// Token: 0x06010B69 RID: 68457 RVA: 0x003998F9 File Offset: 0x00397AF9
		public DeltaValueVersions(DeltaSource source)
		{
			this.source = source;
		}

		// Token: 0x17002C38 RID: 11320
		// (get) Token: 0x06010B6A RID: 68458 RVA: 0x00399908 File Offset: 0x00397B08
		protected override IEngineHost Host
		{
			get
			{
				return this.source.EngineHost;
			}
		}

		// Token: 0x06010B6B RID: 68459 RVA: 0x00399915 File Offset: 0x00397B15
		protected override IEnumerable<ValueVersions.ValueVersion> GetVersions()
		{
			DateTime lastModified = default(DateTime);
			foreach (TableVersion tableVersion in this.source.GetVersions())
			{
				lastModified = tableVersion.Timestamp;
				yield return new DeltaValueVersions.DeltaValueVersion(this.source, tableVersion);
			}
			IEnumerator<TableVersion> enumerator = null;
			foreach (KeyValuePair<string, DateTime> keyValuePair in this.source.GetDraftVersions())
			{
				yield return new DeltaValueVersions.DeltaValueVersion(this.source, keyValuePair.Key, keyValuePair.Value);
			}
			IEnumerator<KeyValuePair<string, DateTime>> enumerator2 = null;
			yield return new DeltaValueVersions.DeltaValueVersion(this.source, null, lastModified);
			yield break;
			yield break;
		}

		// Token: 0x06010B6C RID: 68460 RVA: 0x00399925 File Offset: 0x00397B25
		protected override bool TryCreateVersion(string identity)
		{
			return this.source.TryCreateVersion(identity);
		}

		// Token: 0x06010B6D RID: 68461 RVA: 0x0000CC37 File Offset: 0x0000AE37
		protected override void VerifyActionPermitted()
		{
		}

		// Token: 0x0400641E RID: 25630
		private readonly DeltaSource source;

		// Token: 0x02001F03 RID: 7939
		private class DeltaValueVersion : ValueVersions.ValueVersion
		{
			// Token: 0x06010B6E RID: 68462 RVA: 0x00399934 File Offset: 0x00397B34
			public DeltaValueVersion(DeltaSource source, TableVersion version)
			{
				this.source = source;
				this.version = new long?(version.Version);
				this.identity = version.Version.ToString(CultureInfo.InvariantCulture);
				this.modifiedTime = version.Timestamp;
				this.isDraft = false;
			}

			// Token: 0x06010B6F RID: 68463 RVA: 0x0039998E File Offset: 0x00397B8E
			public DeltaValueVersion(DeltaSource source, string identity, DateTime modifiedTime)
			{
				this.source = source;
				this.version = null;
				this.identity = identity;
				this.modifiedTime = modifiedTime;
				this.isDraft = identity != null;
			}

			// Token: 0x17002C39 RID: 11321
			// (get) Token: 0x06010B70 RID: 68464 RVA: 0x003999C1 File Offset: 0x00397BC1
			public override string Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x06010B71 RID: 68465 RVA: 0x003999C9 File Offset: 0x00397BC9
			public override bool TryCommit()
			{
				this.source.CommitTransaction(this.identity);
				return true;
			}

			// Token: 0x06010B72 RID: 68466 RVA: 0x003999E0 File Offset: 0x00397BE0
			public override bool TryCreateValue(out IValueReference value)
			{
				IValueReference valueReference2;
				if (!this.isDraft)
				{
					IValueReference valueReference = this.source.CreateTable(this.version);
					valueReference2 = valueReference;
				}
				else
				{
					valueReference2 = this.source.CreateDraftTable(this.identity);
				}
				value = valueReference2;
				return true;
			}

			// Token: 0x06010B73 RID: 68467 RVA: 0x00399A20 File Offset: 0x00397C20
			public override RecordValue ToTableRow(IValueReference versionValue)
			{
				string text = this.Identity;
				Value value = ((text != null) ? TextValue.New(text) : Value.Null);
				return RecordValue.New(Library._Value.VersionsFunctionValue.ResultTableType.ItemType, new IValueReference[]
				{
					value,
					LogicalValue.New(!this.isDraft),
					versionValue,
					(this.modifiedTime == default(DateTime)) ? Value.Null : DateTimeValue.New(this.modifiedTime)
				});
			}

			// Token: 0x0400641F RID: 25631
			private readonly DeltaSource source;

			// Token: 0x04006420 RID: 25632
			private readonly long? version;

			// Token: 0x04006421 RID: 25633
			private readonly string identity;

			// Token: 0x04006422 RID: 25634
			private readonly DateTime modifiedTime;

			// Token: 0x04006423 RID: 25635
			private readonly bool isDraft;
		}
	}
}
