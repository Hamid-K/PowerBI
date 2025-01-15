using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000055 RID: 85
	internal abstract class ODataReaderCore : ODataReader
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		protected ODataReaderCore(ODataInputContext inputContext, bool readingFeed, bool readingDelta, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.readingFeed = readingFeed;
			this.readingDelta = readingDelta;
			this.listener = listener;
			this.currentEntryDepth = 0;
			if (this.readingFeed && this.inputContext.Model.IsUserModel())
			{
				this.feedValidator = new FeedWithoutExpectedTypeValidator();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000CA83 File Offset: 0x0000AC83
		public sealed override ODataReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		public sealed override ODataItem Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000CABD File Offset: 0x0000ACBD
		protected ODataEntry CurrentEntry
		{
			get
			{
				return (ODataEntry)this.Item;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000CACA File Offset: 0x0000ACCA
		protected ODataFeed CurrentFeed
		{
			get
			{
				return (ODataFeed)this.Item;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000CAD7 File Offset: 0x0000ACD7
		protected int CurrentEntryDepth
		{
			get
			{
				return this.currentEntryDepth;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000CADF File Offset: 0x0000ACDF
		protected ODataNavigationLink CurrentNavigationLink
		{
			get
			{
				return (ODataNavigationLink)this.Item;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		protected ODataEntityReferenceLink CurrentEntityReferenceLink
		{
			get
			{
				return (ODataEntityReferenceLink)this.Item;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000CB1B File Offset: 0x0000AD1B
		protected IEdmEntityType CurrentEntityType
		{
			get
			{
				return this.scopes.Peek().EntityType;
			}
			set
			{
				this.scopes.Peek().EntityType = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000CB30 File Offset: 0x0000AD30
		protected IEdmNavigationSource CurrentNavigationSource
		{
			get
			{
				return this.scopes.Peek().NavigationSource;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		protected ODataReaderCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		protected ODataReaderCore.Scope LinkParentEntityScope
		{
			get
			{
				return Enumerable.First<ODataReaderCore.Scope>(Enumerable.Skip<ODataReaderCore.Scope>(this.scopes, 1));
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CB84 File Offset: 0x0000AD84
		protected ODataReaderCore.Scope ExpandedLinkContentParentScope
		{
			get
			{
				if (this.scopes.Count > 1)
				{
					ODataReaderCore.Scope scope = Enumerable.First<ODataReaderCore.Scope>(Enumerable.Skip<ODataReaderCore.Scope>(this.scopes, 1));
					if (scope.State == ODataReaderState.NavigationLinkStart)
					{
						return scope;
					}
				}
				return null;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000CBBD File Offset: 0x0000ADBD
		protected bool IsExpandedLinkContent
		{
			get
			{
				return this.ExpandedLinkContentParentScope != null;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		protected bool ReadingFeed
		{
			get
			{
				return this.readingFeed;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CBD3 File Offset: 0x0000ADD3
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.readingDelta || this.listener != null;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		protected FeedWithoutExpectedTypeValidator CurrentFeedValidator
		{
			get
			{
				if (this.scopes.Count != 3)
				{
					return null;
				}
				return this.feedValidator;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CC03 File Offset: 0x0000AE03
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000366 RID: 870
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x06000367 RID: 871
		protected abstract bool ReadAtFeedStartImplementation();

		// Token: 0x06000368 RID: 872
		protected abstract bool ReadAtFeedEndImplementation();

		// Token: 0x06000369 RID: 873
		protected abstract bool ReadAtEntryStartImplementation();

		// Token: 0x0600036A RID: 874
		protected abstract bool ReadAtEntryEndImplementation();

		// Token: 0x0600036B RID: 875
		protected abstract bool ReadAtNavigationLinkStartImplementation();

		// Token: 0x0600036C RID: 876
		protected abstract bool ReadAtNavigationLinkEndImplementation();

		// Token: 0x0600036D RID: 877
		protected abstract bool ReadAtEntityReferenceLink();

		// Token: 0x0600036E RID: 878 RVA: 0x0000CC1E File Offset: 0x0000AE1E
		protected void EnterScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Push(scope);
			if (this.listener != null)
			{
				if (scope.State == ODataReaderState.Exception)
				{
					this.listener.OnException();
					return;
				}
				if (scope.State == ODataReaderState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000CC5E File Offset: 0x0000AE5E
		protected void ReplaceScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000CC73 File Offset: 0x0000AE73
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected void PopScope(ODataReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000CC81 File Offset: 0x0000AE81
		protected void EndEntry(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		protected void ApplyEntityTypeNameFromPayload(string entityTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			SerializationTypeNameAnnotation serializationTypeNameAnnotation;
			IEdmEntityTypeReference edmEntityTypeReference = (IEdmEntityTypeReference)ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.Entity, null, this.CurrentEntityType.ToTypeReference(), entityTypeNameFromPayload, this.inputContext.Model, this.inputContext.MessageReaderSettings, () => EdmTypeKind.Entity, out edmTypeKind, out serializationTypeNameAnnotation);
			IEdmEntityType edmEntityType = null;
			ODataEntry currentEntry = this.CurrentEntry;
			if (edmEntityTypeReference != null)
			{
				edmEntityType = edmEntityTypeReference.EntityDefinition();
				currentEntry.TypeName = edmEntityType.FullTypeName();
				if (serializationTypeNameAnnotation != null)
				{
					currentEntry.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
				}
			}
			else if (entityTypeNameFromPayload != null)
			{
				currentEntry.TypeName = entityTypeNameFromPayload;
			}
			this.CurrentEntityType = edmEntityType;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CD39 File Offset: 0x0000AF39
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000CD44 File Offset: 0x0000AF44
		protected void IncreaseEntryDepth()
		{
			this.currentEntryDepth++;
			if (this.currentEntryDepth > this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth)
			{
				throw new ODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth));
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
		protected void DecreaseEntryDepth()
		{
			this.currentEntryDepth--;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		private bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataReaderState.FeedStart:
				flag = this.ReadAtFeedStartImplementation();
				break;
			case ODataReaderState.FeedEnd:
				flag = this.ReadAtFeedEndImplementation();
				break;
			case ODataReaderState.EntryStart:
				this.IncreaseEntryDepth();
				flag = this.ReadAtEntryStartImplementation();
				break;
			case ODataReaderState.EntryEnd:
				this.DecreaseEntryDepth();
				flag = this.ReadAtEntryEndImplementation();
				break;
			case ODataReaderState.NavigationLinkStart:
				flag = this.ReadAtNavigationLinkStartImplementation();
				break;
			case ODataReaderState.NavigationLinkEnd:
				flag = this.ReadAtNavigationLinkEndImplementation();
				break;
			case ODataReaderState.EntityReferenceLink:
				flag = this.ReadAtEntityReferenceLink();
				break;
			case ODataReaderState.Exception:
			case ODataReaderState.Completed:
				throw new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCore_ReadImplementation));
			}
			if ((this.State == ODataReaderState.EntryStart || this.State == ODataReaderState.EntryEnd) && this.Item != null)
			{
				ReaderValidationUtils.ValidateEntry(this.CurrentEntry);
			}
			return flag;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CE9C File Offset: 0x0000B09C
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action.Invoke();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Exception, null, null, null, null));
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataReaderState.Exception || this.State == ODataReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CF1D File Offset: 0x0000B11D
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x0400019C RID: 412
		private readonly ODataInputContext inputContext;

		// Token: 0x0400019D RID: 413
		private readonly bool readingFeed;

		// Token: 0x0400019E RID: 414
		private readonly bool readingDelta;

		// Token: 0x0400019F RID: 415
		private readonly Stack<ODataReaderCore.Scope> scopes = new Stack<ODataReaderCore.Scope>();

		// Token: 0x040001A0 RID: 416
		private readonly IODataReaderWriterListener listener;

		// Token: 0x040001A1 RID: 417
		private readonly FeedWithoutExpectedTypeValidator feedValidator;

		// Token: 0x040001A2 RID: 418
		private int currentEntryDepth;

		// Token: 0x02000056 RID: 86
		protected internal class Scope
		{
			// Token: 0x0600037B RID: 891 RVA: 0x0000CF3A File Offset: 0x0000B13A
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			internal Scope(ODataReaderState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.EntityType = expectedEntityType;
				this.NavigationSource = navigationSource;
				this.odataUri = odataUri;
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600037C RID: 892 RVA: 0x0000CF67 File Offset: 0x0000B167
			internal ODataReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600037D RID: 893 RVA: 0x0000CF6F File Offset: 0x0000B16F
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600037E RID: 894 RVA: 0x0000CF77 File Offset: 0x0000B177
			internal ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CF7F File Offset: 0x0000B17F
			// (set) Token: 0x06000380 RID: 896 RVA: 0x0000CF87 File Offset: 0x0000B187
			internal IEdmNavigationSource NavigationSource { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CF90 File Offset: 0x0000B190
			// (set) Token: 0x06000382 RID: 898 RVA: 0x0000CF98 File Offset: 0x0000B198
			internal IEdmEntityType EntityType { get; set; }

			// Token: 0x040001A4 RID: 420
			private readonly ODataReaderState state;

			// Token: 0x040001A5 RID: 421
			private readonly ODataItem item;

			// Token: 0x040001A6 RID: 422
			private readonly ODataUri odataUri;
		}
	}
}
