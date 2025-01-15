using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200023E RID: 574
	internal sealed class ODataJsonLightDeltaReader : ODataDeltaReader
	{
		// Token: 0x060018D1 RID: 6353 RVA: 0x00047787 File Offset: 0x00045987
		public ODataJsonLightDeltaReader(ODataJsonLightInputContext jsonLightInputContext, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			this.underlyingReader = new ODataJsonLightReader(jsonLightInputContext, navigationSource, expectedEntityType, true, false, true, null);
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x000477A4 File Offset: 0x000459A4
		public override ODataDeltaReaderState State
		{
			get
			{
				if (this.nestedLevel > 0 || this.underlyingReader.State == ODataReaderState.NestedResourceInfoEnd)
				{
					return ODataDeltaReaderState.NestedResource;
				}
				switch (this.underlyingReader.State)
				{
				case ODataReaderState.Start:
					return ODataDeltaReaderState.Start;
				case ODataReaderState.ResourceStart:
					return ODataDeltaReaderState.DeltaResourceStart;
				case ODataReaderState.ResourceEnd:
					return ODataDeltaReaderState.DeltaResourceEnd;
				case ODataReaderState.Completed:
					return ODataDeltaReaderState.Completed;
				case ODataReaderState.DeltaResourceSetStart:
					return ODataDeltaReaderState.DeltaResourceSetStart;
				case ODataReaderState.DeltaResourceSetEnd:
					return ODataDeltaReaderState.DeltaResourceSetEnd;
				case ODataReaderState.DeletedResourceEnd:
					return ODataDeltaReaderState.DeltaDeletedEntry;
				case ODataReaderState.DeltaLink:
					return ODataDeltaReaderState.DeltaLink;
				case ODataReaderState.DeltaDeletedLink:
					return ODataDeltaReaderState.DeltaDeletedLink;
				}
				return ODataDeltaReaderState.NestedResource;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00047838 File Offset: 0x00045A38
		public override ODataReaderState SubState
		{
			get
			{
				if (this.nestedLevel == 1 && this.underlyingReader.State == ODataReaderState.NestedResourceInfoStart)
				{
					return ODataReaderState.Start;
				}
				if (this.nestedLevel == 0 && this.underlyingReader.State == ODataReaderState.NestedResourceInfoEnd)
				{
					return ODataReaderState.Completed;
				}
				if (this.nestedLevel <= 0)
				{
					return ODataReaderState.Start;
				}
				return this.underlyingReader.State;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00047890 File Offset: 0x00045A90
		public override ODataItem Item
		{
			get
			{
				ODataDeletedResource odataDeletedResource = this.underlyingReader.Item as ODataDeletedResource;
				if (odataDeletedResource != null)
				{
					return ODataDeltaDeletedEntry.GetDeltaDeletedEntry(odataDeletedResource);
				}
				return this.underlyingReader.Item;
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000478C4 File Offset: 0x00045AC4
		public override bool Read()
		{
			bool flag = this.underlyingReader.Read();
			if (this.underlyingReader.State == ODataReaderState.DeletedResourceStart)
			{
				while ((flag = this.underlyingReader.Read()) && this.underlyingReader.State != ODataReaderState.DeletedResourceEnd)
				{
					this.SetNestedLevel();
				}
			}
			this.SetNestedLevel();
			return flag;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00047919 File Offset: 0x00045B19
		public override Task<bool> ReadAsync()
		{
			return this.underlyingReader.ReadAsync().FollowOnSuccessWith(delegate(Task<bool> t)
			{
				if (this.underlyingReader.State == ODataReaderState.DeletedResourceStart)
				{
					this.SkipToDeletedResourceEnd();
				}
				this.SetNestedLevel();
				return t.Result;
			});
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00047938 File Offset: 0x00045B38
		private async void SkipToDeletedResourceEnd()
		{
			if (this.underlyingReader.State != ODataReaderState.DeletedResourceEnd)
			{
				await this.underlyingReader.ReadAsync().FollowOnSuccessWith(delegate(Task<bool> t)
				{
					this.SkipToDeletedResourceEnd();
				});
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00047971 File Offset: 0x00045B71
		private void SetNestedLevel()
		{
			if (this.underlyingReader.State == ODataReaderState.NestedResourceInfoStart)
			{
				this.nestedLevel++;
				return;
			}
			if (this.underlyingReader.State == ODataReaderState.NestedResourceInfoEnd)
			{
				this.nestedLevel--;
			}
		}

		// Token: 0x04000B2B RID: 2859
		private readonly ODataJsonLightReader underlyingReader;

		// Token: 0x04000B2C RID: 2860
		private int nestedLevel;
	}
}
