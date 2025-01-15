using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000103 RID: 259
	internal class FeedAndEntryMaterializerAdapter
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x00029224 File Offset: 0x00027424
		internal FeedAndEntryMaterializerAdapter(ODataMessageReader messageReader, ODataReaderWrapper reader, ClientEdmModel model, MergeOption mergeOption)
			: this(ODataUtils.GetReadFormat(messageReader), reader, model, mergeOption)
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00029236 File Offset: 0x00027436
		internal FeedAndEntryMaterializerAdapter(ODataFormat odataFormat, ODataReaderWrapper reader, ClientEdmModel model, MergeOption mergeOption)
		{
			this.readODataFormat = odataFormat;
			this.clientEdmModel = model;
			this.mergeOption = mergeOption;
			this.reader = reader;
			this.currentEntry = null;
			this.currentFeed = null;
			this.feedEntries = null;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00029270 File Offset: 0x00027470
		public ODataResourceSet CurrentFeed
		{
			get
			{
				return this.currentFeed;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00029278 File Offset: 0x00027478
		public ODataResource CurrentEntry
		{
			get
			{
				return this.currentEntry;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00029280 File Offset: 0x00027480
		public bool IsEndOfStream
		{
			get
			{
				return this.reader.State == ODataReaderState.Completed;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00029294 File Offset: 0x00027494
		public long GetCountValue(bool readIfNoFeed)
		{
			if (this.currentFeed == null && this.currentEntry == null && readIfNoFeed && this.TryReadFeed(true, out this.currentFeed))
			{
				this.feedEntries = MaterializerFeed.GetFeed(this.currentFeed).Entries.GetEnumerator();
			}
			if (this.currentFeed != null && this.currentFeed.Count != null)
			{
				return this.currentFeed.Count.Value;
			}
			throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00029324 File Offset: 0x00027524
		public bool Read()
		{
			if (this.feedEntries != null)
			{
				if (this.feedEntries.MoveNext())
				{
					this.currentEntry = this.feedEntries.Current;
					return true;
				}
				this.feedEntries = null;
				this.currentEntry = null;
			}
			ODataReaderState state = this.reader.State;
			switch (state)
			{
			case ODataReaderState.Start:
			{
				ODataResourceSet odataResourceSet;
				MaterializerEntry materializerEntry;
				if (this.TryReadFeedOrEntry(true, out odataResourceSet, out materializerEntry))
				{
					this.currentEntry = ((materializerEntry != null) ? materializerEntry.Entry : null);
					this.currentFeed = odataResourceSet;
					if (this.currentFeed != null)
					{
						this.feedEntries = MaterializerFeed.GetFeed(this.currentFeed).Entries.GetEnumerator();
						if (!this.feedEntries.MoveNext())
						{
							this.feedEntries = null;
							this.currentEntry = null;
							return false;
						}
						this.currentEntry = this.feedEntries.Current;
					}
					return true;
				}
				throw new NotImplementedException();
			}
			case ODataReaderState.ResourceSetStart:
			case ODataReaderState.ResourceStart:
				break;
			case ODataReaderState.ResourceSetEnd:
			case ODataReaderState.ResourceEnd:
				if (this.TryRead() || this.reader.State != ODataReaderState.Completed)
				{
					throw Error.InternalError(InternalError.UnexpectedReadState);
				}
				this.currentEntry = null;
				return false;
			default:
				if (state == ODataReaderState.Completed)
				{
					this.currentEntry = null;
					return false;
				}
				break;
			}
			throw Error.InternalError(InternalError.UnexpectedReadState);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002944D File Offset: 0x0002764D
		public void Dispose()
		{
			if (this.feedEntries != null)
			{
				this.feedEntries.Dispose();
				this.feedEntries = null;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002946C File Offset: 0x0002766C
		private bool TryReadFeedOrEntry(bool lazy, out ODataResourceSet feed, out MaterializerEntry entry)
		{
			if (this.TryStartReadFeedOrEntry())
			{
				if (this.reader.State == ODataReaderState.ResourceStart)
				{
					entry = this.ReadEntryCore();
					feed = null;
				}
				else
				{
					entry = null;
					feed = this.ReadFeedCore(lazy);
				}
			}
			else
			{
				feed = null;
				entry = null;
			}
			return feed != null || entry != null;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000294BB File Offset: 0x000276BB
		private bool TryStartReadFeedOrEntry()
		{
			return this.TryRead() && (this.reader.State == ODataReaderState.ResourceSetStart || this.reader.State == ODataReaderState.ResourceStart);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000294E5 File Offset: 0x000276E5
		private bool TryReadFeed(bool lazy, out ODataResourceSet feed)
		{
			if (this.TryStartReadFeedOrEntry())
			{
				this.ExpectState(ODataReaderState.ResourceSetStart);
				feed = this.ReadFeedCore(lazy);
			}
			else
			{
				feed = null;
			}
			return feed != null;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002950C File Offset: 0x0002770C
		private ODataResourceSet ReadFeedCore(bool lazy)
		{
			this.ExpectState(ODataReaderState.ResourceSetStart);
			ODataResourceSet odataResourceSet = (ODataResourceSet)this.reader.Item;
			IEnumerable<ODataResource> enumerable = this.LazyReadEntries();
			if (lazy)
			{
				MaterializerFeed.CreateFeed(odataResourceSet, enumerable);
			}
			else
			{
				MaterializerFeed.CreateFeed(odataResourceSet, new List<ODataResource>(enumerable));
			}
			return odataResourceSet;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00029553 File Offset: 0x00027753
		private IEnumerable<ODataResource> LazyReadEntries()
		{
			MaterializerEntry entryAndState;
			while (this.TryReadEntry(out entryAndState))
			{
				yield return entryAndState.Entry;
			}
			yield break;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00029563 File Offset: 0x00027763
		private bool TryReadEntry(out MaterializerEntry entry)
		{
			if (this.TryStartReadFeedOrEntry())
			{
				this.ExpectState(ODataReaderState.ResourceStart);
				entry = this.ReadEntryCore();
				return true;
			}
			entry = null;
			return false;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00029584 File Offset: 0x00027784
		private MaterializerEntry ReadEntryCore()
		{
			this.ExpectState(ODataReaderState.ResourceStart);
			ODataResource odataResource = (ODataResource)this.reader.Item;
			List<ODataNestedResourceInfo> list = new List<ODataNestedResourceInfo>();
			MaterializerEntry materializerEntry;
			if (odataResource != null)
			{
				materializerEntry = MaterializerEntry.CreateEntry(odataResource, this.readODataFormat, this.mergeOption != MergeOption.NoTracking, this.clientEdmModel);
				for (;;)
				{
					this.AssertRead();
					ODataReaderState state = this.reader.State;
					if (state != ODataReaderState.ResourceEnd)
					{
						if (state != ODataReaderState.NestedResourceInfoStart)
						{
							break;
						}
						list.Add(this.ReadNestedResourceInfo());
					}
					if (this.reader.State == ODataReaderState.ResourceEnd)
					{
						goto Block_4;
					}
				}
				throw Error.InternalError(InternalError.UnexpectedReadState);
				Block_4:
				if (!materializerEntry.Entry.IsTransient)
				{
					materializerEntry.UpdateEntityDescriptor();
				}
			}
			else
			{
				materializerEntry = MaterializerEntry.CreateEmpty();
				this.ReadAndExpectState(ODataReaderState.ResourceEnd);
			}
			foreach (ODataNestedResourceInfo odataNestedResourceInfo in list)
			{
				materializerEntry.AddNestedResourceInfo(odataNestedResourceInfo);
			}
			return materializerEntry;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00029678 File Offset: 0x00027878
		private ODataNestedResourceInfo ReadNestedResourceInfo()
		{
			ODataNestedResourceInfo odataNestedResourceInfo = (ODataNestedResourceInfo)this.reader.Item;
			ODataResourceSet odataResourceSet;
			MaterializerEntry materializerEntry;
			if (this.TryReadFeedOrEntry(false, out odataResourceSet, out materializerEntry))
			{
				if (odataResourceSet != null)
				{
					MaterializerNavigationLink.CreateLink(odataNestedResourceInfo, odataResourceSet);
				}
				else
				{
					MaterializerNavigationLink.CreateLink(odataNestedResourceInfo, materializerEntry);
				}
				this.ReadAndExpectState(ODataReaderState.NestedResourceInfoEnd);
			}
			this.ExpectState(ODataReaderState.NestedResourceInfoEnd);
			return odataNestedResourceInfo;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000296C8 File Offset: 0x000278C8
		private bool TryRead()
		{
			bool flag;
			try
			{
				flag = this.reader.Read();
			}
			catch (ODataErrorException ex)
			{
				throw new DataServiceClientException(Strings.Deserialize_ServerException(ex.Error.Message), ex);
			}
			catch (ODataException ex2)
			{
				throw new InvalidOperationException(ex2.Message, ex2);
			}
			return flag;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00029728 File Offset: 0x00027928
		private void ReadAndExpectState(ODataReaderState expectedState)
		{
			this.AssertRead();
			this.ExpectState(expectedState);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00029737 File Offset: 0x00027937
		private void AssertRead()
		{
			if (!this.TryRead())
			{
				throw Error.InternalError(InternalError.UnexpectedReadState);
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00029748 File Offset: 0x00027948
		private void ExpectState(ODataReaderState expectedState)
		{
			if (this.reader.State != expectedState)
			{
				throw Error.InternalError(InternalError.UnexpectedReadState);
			}
		}

		// Token: 0x04000621 RID: 1569
		private readonly ODataFormat readODataFormat;

		// Token: 0x04000622 RID: 1570
		private readonly ODataReaderWrapper reader;

		// Token: 0x04000623 RID: 1571
		private readonly ClientEdmModel clientEdmModel;

		// Token: 0x04000624 RID: 1572
		private readonly MergeOption mergeOption;

		// Token: 0x04000625 RID: 1573
		private IEnumerator<ODataResource> feedEntries;

		// Token: 0x04000626 RID: 1574
		private ODataResourceSet currentFeed;

		// Token: 0x04000627 RID: 1575
		private ODataResource currentEntry;
	}
}
