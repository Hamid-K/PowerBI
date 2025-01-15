using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015A RID: 346
	internal abstract class ODataReaderCore : ODataReader
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x0001CA48 File Offset: 0x0001AC48
		protected ODataReaderCore(ODataInputContext inputContext, bool readingFeed, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.readingFeed = readingFeed;
			this.listener = listener;
			this.currentEntryDepth = 0;
			if (this.readingFeed && this.inputContext.Model.IsUserModel())
			{
				this.feedValidator = new FeedWithoutExpectedTypeValidator();
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001CAA7 File Offset: 0x0001ACA7
		public sealed override ODataReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		public sealed override ODataItem Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001CAE1 File Offset: 0x0001ACE1
		protected ODataEntry CurrentEntry
		{
			get
			{
				return (ODataEntry)this.Item;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001CAEE File Offset: 0x0001ACEE
		protected ODataFeed CurrentFeed
		{
			get
			{
				return (ODataFeed)this.Item;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001CAFB File Offset: 0x0001ACFB
		protected ODataNavigationLink CurrentNavigationLink
		{
			get
			{
				return (ODataNavigationLink)this.Item;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001CB08 File Offset: 0x0001AD08
		protected ODataEntityReferenceLink CurrentEntityReferenceLink
		{
			get
			{
				return (ODataEntityReferenceLink)this.Item;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001CB18 File Offset: 0x0001AD18
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0001CB37 File Offset: 0x0001AD37
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		protected IEdmEntitySet CurrentEntitySet
		{
			get
			{
				return this.scopes.Peek().EntitySet;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001CB6B File Offset: 0x0001AD6B
		protected ODataReaderCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001CB78 File Offset: 0x0001AD78
		protected ODataReaderCore.Scope LinkParentEntityScope
		{
			get
			{
				return Enumerable.First<ODataReaderCore.Scope>(Enumerable.Skip<ODataReaderCore.Scope>(this.scopes, 1));
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001CB8B File Offset: 0x0001AD8B
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		protected ODataReaderCore.Scope ExpandedLinkContentParentScope
		{
			get
			{
				ODataReaderCore.Scope scope = Enumerable.First<ODataReaderCore.Scope>(Enumerable.Skip<ODataReaderCore.Scope>(this.scopes, 1));
				if (scope.State == ODataReaderState.NavigationLinkStart)
				{
					return scope;
				}
				return null;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001CBCB File Offset: 0x0001ADCB
		protected bool IsExpandedLinkContent
		{
			get
			{
				return this.ExpandedLinkContentParentScope != null;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001CBD9 File Offset: 0x0001ADD9
		protected bool ReadingFeed
		{
			get
			{
				return this.readingFeed;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001CBE1 File Offset: 0x0001ADE1
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001CBEF File Offset: 0x0001ADEF
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

		// Token: 0x0600092D RID: 2349 RVA: 0x0001CC07 File Offset: 0x0001AE07
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x0600092E RID: 2350
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x0600092F RID: 2351
		protected abstract bool ReadAtFeedStartImplementation();

		// Token: 0x06000930 RID: 2352
		protected abstract bool ReadAtFeedEndImplementation();

		// Token: 0x06000931 RID: 2353
		protected abstract bool ReadAtEntryStartImplementation();

		// Token: 0x06000932 RID: 2354
		protected abstract bool ReadAtEntryEndImplementation();

		// Token: 0x06000933 RID: 2355
		protected abstract bool ReadAtNavigationLinkStartImplementation();

		// Token: 0x06000934 RID: 2356
		protected abstract bool ReadAtNavigationLinkEndImplementation();

		// Token: 0x06000935 RID: 2357
		protected abstract bool ReadAtEntityReferenceLink();

		// Token: 0x06000936 RID: 2358 RVA: 0x0001CC22 File Offset: 0x0001AE22
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

		// Token: 0x06000937 RID: 2359 RVA: 0x0001CC62 File Offset: 0x0001AE62
		protected void ReplaceScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001CC77 File Offset: 0x0001AE77
		protected void PopScope(ODataReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001CC85 File Offset: 0x0001AE85
		protected void EndEntry(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		protected void ApplyEntityTypeNameFromPayload(string entityTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			SerializationTypeNameAnnotation serializationTypeNameAnnotation;
			IEdmEntityTypeReference edmEntityTypeReference = (IEdmEntityTypeReference)ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.Entity, null, this.CurrentEntityType.ToTypeReference(), entityTypeNameFromPayload, this.inputContext.Model, this.inputContext.MessageReaderSettings, this.inputContext.Version, () => EdmTypeKind.Entity, out edmTypeKind, out serializationTypeNameAnnotation);
			IEdmEntityType edmEntityType = null;
			ODataEntry currentEntry = this.CurrentEntry;
			if (edmEntityTypeReference != null)
			{
				edmEntityType = edmEntityTypeReference.EntityDefinition();
				currentEntry.TypeName = edmEntityType.ODataFullName();
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

		// Token: 0x0600093B RID: 2363 RVA: 0x0001CD48 File Offset: 0x0001AF48
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001CD50 File Offset: 0x0001AF50
		protected void IncreaseEntryDepth()
		{
			this.currentEntryDepth++;
			if (this.currentEntryDepth > this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth)
			{
				throw new ODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth));
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001CDAD File Offset: 0x0001AFAD
		protected void DecreaseEntryDepth()
		{
			this.currentEntryDepth--;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
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

		// Token: 0x0600093F RID: 2367 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
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
					this.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Exception, null, null, null));
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001CEEC File Offset: 0x0001B0EC
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataReaderState.Exception || this.State == ODataReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001CF29 File Offset: 0x0001B129
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x04000373 RID: 883
		private readonly ODataInputContext inputContext;

		// Token: 0x04000374 RID: 884
		private readonly bool readingFeed;

		// Token: 0x04000375 RID: 885
		private readonly Stack<ODataReaderCore.Scope> scopes = new Stack<ODataReaderCore.Scope>();

		// Token: 0x04000376 RID: 886
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000377 RID: 887
		private readonly FeedWithoutExpectedTypeValidator feedValidator;

		// Token: 0x04000378 RID: 888
		private int currentEntryDepth;

		// Token: 0x0200015B RID: 347
		protected internal class Scope
		{
			// Token: 0x06000943 RID: 2371 RVA: 0x0001CF46 File Offset: 0x0001B146
			internal Scope(ODataReaderState state, ODataItem item, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
			{
				this.state = state;
				this.item = item;
				this.EntityType = expectedEntityType;
				this.EntitySet = entitySet;
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001CF6B File Offset: 0x0001B16B
			internal ODataReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001CF73 File Offset: 0x0001B173
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001CF7B File Offset: 0x0001B17B
			// (set) Token: 0x06000947 RID: 2375 RVA: 0x0001CF83 File Offset: 0x0001B183
			internal IEdmEntitySet EntitySet { get; set; }

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001CF8C File Offset: 0x0001B18C
			// (set) Token: 0x06000949 RID: 2377 RVA: 0x0001CF94 File Offset: 0x0001B194
			internal IEdmEntityType EntityType { get; set; }

			// Token: 0x0400037A RID: 890
			private readonly ODataReaderState state;

			// Token: 0x0400037B RID: 891
			private readonly ODataItem item;
		}
	}
}
