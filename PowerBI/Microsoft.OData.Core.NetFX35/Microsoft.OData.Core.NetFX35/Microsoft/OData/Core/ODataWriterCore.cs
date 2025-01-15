using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000061 RID: 97
	internal abstract class ODataWriterCore : ODataWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000EB98 File Offset: 0x0000CD98
		protected ODataWriterCore(ODataOutputContext outputContext, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool writingFeed, bool writingDelta = false, IODataReaderWriterListener listener = null)
		{
			this.outputContext = outputContext;
			this.writingFeed = writingFeed;
			this.writingDelta = writingDelta;
			this.WriterValidator = outputContext.WriterValidator;
			if (this.writingFeed && this.outputContext.Model.IsUserModel())
			{
				this.feedValidator = new FeedWithoutExpectedTypeValidator();
			}
			if (navigationSource != null && entityType == null)
			{
				entityType = this.outputContext.EdmTypeResolver.GetElementType(navigationSource);
			}
			ODataUri odataUri = outputContext.MessageWriterSettings.ODataUri.Clone();
			if (!writingFeed && odataUri != null && odataUri.Path != null)
			{
				odataUri.Path = odataUri.Path.TrimEndingKeySegment();
			}
			this.listener = listener;
			this.scopes.Push(new ODataWriterCore.Scope(ODataWriterCore.WriterState.Start, null, navigationSource, entityType, false, outputContext.MessageWriterSettings.SelectedProperties, odataUri));
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000EC71 File Offset: 0x0000CE71
		protected ODataWriterCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		protected ODataWriterCore.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000EC8B File Offset: 0x0000CE8B
		protected bool SkipWriting
		{
			get
			{
				return this.CurrentScope.SkipWriting;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000EC98 File Offset: 0x0000CE98
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count == 2;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		protected ODataNavigationLink ParentNavigationLink
		{
			get
			{
				ODataWriterCore.Scope parentOrNull = this.scopes.ParentOrNull;
				if (parentOrNull != null)
				{
					return parentOrNull.Item as ODataNavigationLink;
				}
				return null;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		protected IEdmEntityType ParentEntryEntityType
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopes.Parent;
				return parent.EntityType;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		protected IEdmNavigationSource ParentEntryNavigationSource
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopes.Parent;
				return parent.NavigationSource;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000ED13 File Offset: 0x0000CF13
		protected int FeedScopeEntryCount
		{
			get
			{
				return ((ODataWriterCore.FeedScope)this.CurrentScope).EntryCount;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000ED28 File Offset: 0x0000CF28
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				ODataWriterCore.EntryScope entryScope;
				switch (this.State)
				{
				case ODataWriterCore.WriterState.Entry:
					entryScope = (ODataWriterCore.EntryScope)this.CurrentScope;
					goto IL_0053;
				case ODataWriterCore.WriterState.NavigationLink:
				case ODataWriterCore.WriterState.NavigationLinkWithContent:
					entryScope = (ODataWriterCore.EntryScope)this.scopes.Parent;
					goto IL_0053;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_DuplicatePropertyNamesChecker));
				IL_0053:
				return entryScope.DuplicatePropertyNamesChecker;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		protected IEdmEntityType EntryEntityType
		{
			get
			{
				return this.CurrentScope.EntityType;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000ED9C File Offset: 0x0000CF9C
		protected ODataWriterCore.NavigationLinkScope ParentNavigationLinkScope
		{
			get
			{
				ODataWriterCore.Scope scope = this.scopes.Parent;
				if (scope.State == ODataWriterCore.WriterState.Start)
				{
					return null;
				}
				if (scope.State == ODataWriterCore.WriterState.Feed)
				{
					scope = this.scopes.ParentOfParent;
					if (scope.State == ODataWriterCore.WriterState.Start)
					{
						return null;
					}
				}
				if (scope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
				{
					return (ODataWriterCore.NavigationLinkScope)scope;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ParentNavigationLinkScope));
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000EDFE File Offset: 0x0000CFFE
		private FeedWithoutExpectedTypeValidator CurrentFeedValidator
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

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EE18 File Offset: 0x0000D018
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.FlushSynchronously();
			}
			catch
			{
				this.EnterScope(ODataWriterCore.WriterState.Error, null);
				throw;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EE50 File Offset: 0x0000D050
		public sealed override void WriteStart(ODataFeed feed)
		{
			this.VerifyCanWriteStartFeed(true, feed);
			this.WriteStartFeedImplementation(feed);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000EE61 File Offset: 0x0000D061
		public sealed override void WriteStart(ODataEntry entry)
		{
			this.VerifyCanWriteStartEntry(true, entry);
			this.WriteStartEntryImplementation(entry);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000EE72 File Offset: 0x0000D072
		public sealed override void WriteStart(ODataNavigationLink navigationLink)
		{
			this.VerifyCanWriteStartNavigationLink(true, navigationLink);
			this.WriteStartNavigationLinkImplementation(navigationLink);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000EE83 File Offset: 0x0000D083
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
		public sealed override void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, true);
			this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.VerifyNotDisposed();
			if (this.State == ODataWriterCore.WriterState.Completed)
			{
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), ODataWriterCore.WriterState.Error.ToString()));
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataWriterCore.WriterState.Error, this.CurrentScope.Item);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000EF14 File Offset: 0x0000D114
		protected ODataWriterCore.EntryScope GetParentEntryScope()
		{
			ODataWriterCore.ScopeStack scopeStack = new ODataWriterCore.ScopeStack();
			ODataWriterCore.Scope scope = null;
			if (this.scopes.Count > 0)
			{
				scopeStack.Push(this.scopes.Pop());
			}
			while (this.scopes.Count > 0)
			{
				ODataWriterCore.Scope scope2 = this.scopes.Pop();
				scopeStack.Push(scope2);
				if (scope2 is ODataWriterCore.EntryScope)
				{
					scope = scope2;
					IL_006B:
					while (scopeStack.Count > 0)
					{
						ODataWriterCore.Scope scope3 = scopeStack.Pop();
						this.scopes.Push(scope3);
					}
					return scope as ODataWriterCore.EntryScope;
				}
			}
			goto IL_006B;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000EF9B File Offset: 0x0000D19B
		protected static bool IsErrorState(ODataWriterCore.WriterState state)
		{
			return state == ODataWriterCore.WriterState.Error;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		protected static ProjectedPropertiesAnnotation GetProjectedPropertiesAnnotation(ODataWriterCore.Scope currentScope)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataWriterCore.Scope>(currentScope, "currentScope");
			ODataItem item = currentScope.Item;
			if (item != null)
			{
				return item.GetAnnotation<ProjectedPropertiesAnnotation>();
			}
			return null;
		}

		// Token: 0x060003FE RID: 1022
		protected abstract void VerifyNotDisposed();

		// Token: 0x060003FF RID: 1023
		protected abstract void FlushSynchronously();

		// Token: 0x06000400 RID: 1024
		protected abstract void StartPayload();

		// Token: 0x06000401 RID: 1025
		protected abstract void StartEntry(ODataEntry entry);

		// Token: 0x06000402 RID: 1026
		protected abstract void EndEntry(ODataEntry entry);

		// Token: 0x06000403 RID: 1027
		protected abstract void StartFeed(ODataFeed feed);

		// Token: 0x06000404 RID: 1028
		protected abstract void EndPayload();

		// Token: 0x06000405 RID: 1029
		protected abstract void EndFeed(ODataFeed feed);

		// Token: 0x06000406 RID: 1030
		protected abstract void WriteDeferredNavigationLink(ODataNavigationLink navigationLink);

		// Token: 0x06000407 RID: 1031
		protected abstract void StartNavigationLinkWithContent(ODataNavigationLink navigationLink);

		// Token: 0x06000408 RID: 1032
		protected abstract void EndNavigationLinkWithContent(ODataNavigationLink navigationLink);

		// Token: 0x06000409 RID: 1033
		protected abstract void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x0600040A RID: 1034
		protected abstract ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri);

		// Token: 0x0600040B RID: 1035
		protected abstract ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri);

		// Token: 0x0600040C RID: 1036 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		protected ODataFeedAndEntrySerializationInfo GetEntrySerializationInfo(ODataEntry entry)
		{
			ODataFeedAndEntrySerializationInfo odataFeedAndEntrySerializationInfo = ((entry == null) ? null : entry.SerializationInfo);
			if (odataFeedAndEntrySerializationInfo != null)
			{
				return odataFeedAndEntrySerializationInfo;
			}
			ODataWriterCore.FeedScope feedScope = this.CurrentScope as ODataWriterCore.FeedScope;
			if (feedScope != null)
			{
				ODataFeed odataFeed = (ODataFeed)feedScope.Item;
				return odataFeed.SerializationInfo;
			}
			return null;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F012 File Offset: 0x0000D212
		protected virtual ODataWriterCore.NavigationLinkScope CreateNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataWriterCore.NavigationLinkScope(writerState, navLink, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F024 File Offset: 0x0000D224
		protected virtual void PrepareEntryForWriteStart(ODataEntry entry, ODataFeedAndEntryTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F028 File Offset: 0x0000D228
		protected virtual void ValidateEntryMediaResource(ODataEntry entry, IEdmEntityType entityType)
		{
			bool flag = this.outputContext.UseDefaultFormatBehavior || this.outputContext.UseServerFormatBehavior;
			ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.outputContext.Model, flag);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F064 File Offset: 0x0000D264
		protected IEdmEntityType ValidateEntryType(ODataEntry entry)
		{
			if (entry.TypeName == null && this.CurrentScope.EntityType != null)
			{
				return this.CurrentScope.EntityType;
			}
			return (IEdmEntityType)TypeNameOracle.ResolveAndValidateTypeName(this.outputContext.Model, entry.TypeName, EdmTypeKind.Entity, this.WriterValidator);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "An instance field is used in a debug assert.")]
		protected void ValidateNoDeltaLinkForExpandedFeed(ODataFeed feed)
		{
			if (feed.DeltaLink != null)
			{
				throw new ODataException(Strings.ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F0CF File Offset: 0x0000D2CF
		private void VerifyCanWriteStartFeed(bool synchronousCall, ODataFeed feed)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F154 File Offset: 0x0000D354
		private void WriteStartFeedImplementation(ODataFeed feed)
		{
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.Feed);
			this.EnterScope(ODataWriterCore.WriterState.Feed, feed);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					if (feed.Count != null && !this.outputContext.WritingResponse)
					{
						this.ThrowODataException(Strings.ODataWriterCore_QueryCountInRequest, feed);
					}
					this.StartFeed(feed);
				});
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F1AA File Offset: 0x0000D3AA
		private void VerifyCanWriteStartEntry(bool synchronousCall, ODataEntry entry)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataWriterCore.WriterState.NavigationLink)
			{
				ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		private void WriteStartEntryImplementation(ODataEntry entry)
		{
			this.StartPayloadInStartState();
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.Entry);
			this.EnterScope(ODataWriterCore.WriterState.Entry, entry);
			if (!this.SkipWriting)
			{
				this.IncreaseEntryDepth();
				this.InterceptException(delegate
				{
					if (entry != null)
					{
						ODataWriterCore.EntryScope entryScope = (ODataWriterCore.EntryScope)this.CurrentScope;
						IEdmEntityType edmEntityType = this.ValidateEntryType(entry);
						entryScope.EntityTypeFromMetadata = entryScope.EntityType;
						ODataWriterCore.NavigationLinkScope parentNavigationLinkScope = this.ParentNavigationLinkScope;
						if (parentNavigationLinkScope != null)
						{
							this.WriterValidator.ValidateEntryInExpandedLink(edmEntityType, parentNavigationLinkScope.EntityType);
							entryScope.EntityTypeFromMetadata = parentNavigationLinkScope.EntityType;
						}
						else if (this.CurrentFeedValidator != null)
						{
							this.CurrentFeedValidator.ValidateEntry(edmEntityType);
						}
						entryScope.EntityType = edmEntityType;
						this.PrepareEntryForWriteStart(entry, entryScope.GetOrCreateTypeContext(this.outputContext.Model, this.outputContext.WritingResponse), entryScope.SelectedProperties);
						this.ValidateEntryMediaResource(entry, edmEntityType);
					}
					this.StartEntry(entry);
				});
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000F33A File Offset: 0x0000D53A
		private void VerifyCanWriteStartNavigationLink(bool synchronousCall, ODataNavigationLink navigationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNavigationLink>(navigationLink, "navigationLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000F354 File Offset: 0x0000D554
		private void WriteStartNavigationLinkImplementation(ODataNavigationLink navigationLink)
		{
			this.EnterScope(ODataWriterCore.WriterState.NavigationLink, navigationLink);
			ODataEntry odataEntry = (ODataEntry)this.scopes.Parent.Item;
			if (odataEntry.MetadataBuilder != null)
			{
				navigationLink.MetadataBuilder = odataEntry.MetadataBuilder;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000F393 File Offset: 0x0000D593
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000F4FA File Offset: 0x0000D6FA
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				ODataWriterCore.Scope currentScope = this.CurrentScope;
				switch (currentScope.State)
				{
				case ODataWriterCore.WriterState.Start:
				case ODataWriterCore.WriterState.Completed:
				case ODataWriterCore.WriterState.Error:
					throw new ODataException(Strings.ODataWriterCore_WriteEndCalledInInvalidState(currentScope.State.ToString()));
				case ODataWriterCore.WriterState.Entry:
					if (!this.SkipWriting)
					{
						ODataEntry odataEntry = (ODataEntry)currentScope.Item;
						this.EndEntry(odataEntry);
						this.DecreaseEntryDepth();
					}
					break;
				case ODataWriterCore.WriterState.Feed:
					if (!this.SkipWriting)
					{
						ODataFeed odataFeed = (ODataFeed)currentScope.Item;
						this.WriterValidator.ValidateFeedAtEnd(odataFeed, !this.outputContext.WritingResponse);
						this.EndFeed(odataFeed);
					}
					break;
				case ODataWriterCore.WriterState.NavigationLink:
					if (!this.outputContext.WritingResponse)
					{
						throw new ODataException(Strings.ODataWriterCore_DeferredLinkInRequest);
					}
					if (!this.SkipWriting)
					{
						ODataNavigationLink odataNavigationLink = (ODataNavigationLink)currentScope.Item;
						this.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataNavigationLink, false, odataNavigationLink.IsCollection);
						this.WriteDeferredNavigationLink(odataNavigationLink);
						this.MarkNavigationLinkAsProcessed(odataNavigationLink);
					}
					break;
				case ODataWriterCore.WriterState.NavigationLinkWithContent:
					if (!this.SkipWriting)
					{
						ODataNavigationLink odataNavigationLink2 = (ODataNavigationLink)currentScope.Item;
						this.EndNavigationLinkWithContent(odataNavigationLink2);
						this.MarkNavigationLinkAsProcessed(odataNavigationLink2);
					}
					break;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_WriteEnd_UnreachableCodePath));
				}
				this.LeaveScope();
			});
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000F510 File Offset: 0x0000D710
		private void MarkNavigationLinkAsProcessed(ODataNavigationLink link)
		{
			ODataEntry odataEntry = (ODataEntry)this.scopes.Parent.Item;
			odataEntry.MetadataBuilder.MarkNavigationLinkProcessed(link.Name);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000F544 File Offset: 0x0000D744
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool synchronousCall)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			if (this.outputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataWriterCore_EntityReferenceLinkInResponse, null);
			}
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.EntityReferenceLink);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					this.WriterValidator.ValidateEntityReferenceLink(entityReferenceLink);
					this.WriteEntityReferenceInNavigationLinkContent((ODataNavigationLink)this.CurrentScope.Item, entityReferenceLink);
				});
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000F606 File Offset: 0x0000D806
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000F615 File Offset: 0x0000D815
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000F632 File Offset: 0x0000D832
		private void ThrowODataException(string errorMessage, ODataItem item)
		{
			this.EnterScope(ODataWriterCore.WriterState.Error, item);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000F642 File Offset: 0x0000D842
		private void StartPayloadInStartState()
		{
			if (this.State == ODataWriterCore.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000F770 File Offset: 0x0000D970
		private void CheckForNavigationLinkWithContent(ODataPayloadKind contentPayloadKind)
		{
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			if (currentScope.State == ODataWriterCore.WriterState.NavigationLink || currentScope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
			{
				ODataNavigationLink currentNavigationLink = (ODataNavigationLink)currentScope.Item;
				this.InterceptException(delegate
				{
					IEdmNavigationProperty edmNavigationProperty = this.WriterValidator.ValidateNavigationLink(currentNavigationLink, this.ParentEntryEntityType, new ODataPayloadKind?(contentPayloadKind));
					if (edmNavigationProperty != null)
					{
						this.CurrentScope.EntityType = edmNavigationProperty.ToEntityType();
						IEdmNavigationSource parentEntryNavigationSource = this.ParentEntryNavigationSource;
						this.CurrentScope.NavigationSource = ((parentEntryNavigationSource == null) ? null : parentEntryNavigationSource.FindNavigationTarget(edmNavigationProperty));
					}
				});
				if (currentScope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
				{
					if (this.outputContext.WritingResponse || currentNavigationLink.IsCollection != true)
					{
						this.ThrowODataException(Strings.ODataWriterCore_MultipleItemsInNavigationLinkContent, currentNavigationLink);
						return;
					}
				}
				else
				{
					this.PromoteNavigationLinkScope();
					if (!this.SkipWriting)
					{
						this.InterceptException(delegate
						{
							this.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(currentNavigationLink, contentPayloadKind != ODataPayloadKind.EntityReferenceLink, new bool?(contentPayloadKind == ODataPayloadKind.Feed));
							this.StartNavigationLinkWithContent(currentNavigationLink);
						});
						return;
					}
				}
			}
			else if (contentPayloadKind == ODataPayloadKind.EntityReferenceLink)
			{
				this.ThrowODataException(Strings.ODataWriterCore_EntityReferenceLinkWithoutNavigationLink, null);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000F868 File Offset: 0x0000DA68
		private void InterceptException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch
			{
				if (!ODataWriterCore.IsErrorState(this.State))
				{
					this.EnterScope(ODataWriterCore.WriterState.Error, this.CurrentScope.Item);
				}
				throw;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		private void IncreaseEntryDepth()
		{
			this.currentEntryDepth++;
			if (this.currentEntryDepth > this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth)
			{
				this.ThrowODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth), null);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000F90E File Offset: 0x0000DB0E
		private void DecreaseEntryDepth()
		{
			this.currentEntryDepth--;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000F91E File Offset: 0x0000DB1E
		private void NotifyListener(ODataWriterCore.WriterState newState)
		{
			if (this.listener != null)
			{
				if (ODataWriterCore.IsErrorState(newState))
				{
					this.listener.OnException();
					return;
				}
				if (newState == ODataWriterCore.WriterState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000F968 File Offset: 0x0000DB68
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug only cast.")]
		private void EnterScope(ODataWriterCore.WriterState newState, ODataItem item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			bool flag = this.SkipWriting;
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			IEdmNavigationSource edmNavigationSource = null;
			IEdmEntityType edmEntityType = null;
			SelectedPropertiesNode selectedPropertiesNode = currentScope.SelectedProperties;
			ODataUri odataUri = currentScope.ODataUri;
			if (newState == ODataWriterCore.WriterState.Entry || newState == ODataWriterCore.WriterState.Feed)
			{
				edmNavigationSource = currentScope.NavigationSource;
				edmEntityType = currentScope.EntityType;
			}
			ODataWriterCore.WriterState state = currentScope.State;
			if (this.writingDelta)
			{
				flag = state == ODataWriterCore.WriterState.Start && newState == ODataWriterCore.WriterState.Entry;
			}
			if (state == ODataWriterCore.WriterState.Entry && newState == ODataWriterCore.WriterState.NavigationLink)
			{
				ODataNavigationLink odataNavigationLink = (ODataNavigationLink)item;
				if (!flag)
				{
					ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentScope);
					flag = projectedPropertiesAnnotation.ShouldSkipProperty(odataNavigationLink.Name);
					selectedPropertiesNode = currentScope.SelectedProperties.GetSelectedPropertiesForNavigationProperty(currentScope.EntityType, odataNavigationLink.Name);
					if (this.outputContext.WritingResponse)
					{
						odataUri = currentScope.ODataUri.Clone();
						IEdmEntityType entityType = currentScope.EntityType;
						IEdmNavigationProperty edmNavigationProperty = this.WriterValidator.ValidateNavigationLink(odataNavigationLink, entityType, default(ODataPayloadKind?));
						if (edmNavigationProperty != null)
						{
							edmEntityType = edmNavigationProperty.ToEntityType();
							IEdmNavigationSource navigationSource = currentScope.NavigationSource;
							edmNavigationSource = ((navigationSource == null) ? null : navigationSource.FindNavigationTarget(edmNavigationProperty));
							SelectExpandClause selectAndExpand = odataUri.SelectAndExpand;
							TypeSegment typeSegment = null;
							if (selectAndExpand != null)
							{
								SelectExpandClause selectExpandClause;
								selectAndExpand.GetSubSelectExpandClause(odataNavigationLink.Name, out selectExpandClause, out typeSegment);
								odataUri.SelectAndExpand = selectExpandClause;
							}
							ODataPath odataPath;
							switch (edmNavigationSource.NavigationSourceKind())
							{
							case EdmNavigationSourceKind.EntitySet:
								odataPath = new ODataPath(new ODataPathSegment[]
								{
									new EntitySetSegment(edmNavigationSource as IEdmEntitySet)
								});
								break;
							case EdmNavigationSourceKind.Singleton:
								odataPath = new ODataPath(new ODataPathSegment[]
								{
									new SingletonSegment(edmNavigationSource as IEdmSingleton)
								});
								break;
							case EdmNavigationSourceKind.ContainedEntitySet:
							{
								if (odataUri.Path == null)
								{
									throw new ODataException(Strings.ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement);
								}
								odataPath = odataUri.Path;
								if (ODataWriterCore.ShouldAppendKey(navigationSource))
								{
									ODataItem item2 = this.CurrentScope.Item;
									ODataEntry odataEntry = (ODataEntry)item2;
									KeyValuePair<string, object>[] keyProperties = ODataEntryMetadataContext.GetKeyProperties(odataEntry, this.GetEntrySerializationInfo(odataEntry), entityType);
									odataPath = odataPath.AppendKeySegment(keyProperties, entityType, navigationSource);
								}
								if (odataPath != null && typeSegment != null)
								{
									odataPath.Add(typeSegment);
								}
								IEdmContainedEntitySet edmContainedEntitySet = (IEdmContainedEntitySet)edmNavigationSource;
								odataPath = odataPath.AppendNavigationPropertySegment(edmContainedEntitySet.NavigationProperty, edmContainedEntitySet);
								break;
							}
							default:
								odataPath = null;
								break;
							}
							odataUri.Path = odataPath;
						}
					}
				}
			}
			else if (newState == ODataWriterCore.WriterState.Entry && state == ODataWriterCore.WriterState.Feed)
			{
				((ODataWriterCore.FeedScope)currentScope).EntryCount++;
			}
			this.PushScope(newState, item, edmNavigationSource, edmEntityType, flag, selectedPropertiesNode, odataUri);
			this.NotifyListener(newState);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000FC30 File Offset: 0x0000DE30
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				ODataWriterCore.Scope scope = this.scopes.Pop();
				this.PushScope(ODataWriterCore.WriterState.Completed, null, scope.NavigationSource, scope.EntityType, false, scope.SelectedProperties, scope.ODataUri);
				this.InterceptException(new Action(this.EndPayload));
				this.NotifyListener(ODataWriterCore.WriterState.Completed);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Second cast only in debug.")]
		private void PromoteNavigationLinkScope()
		{
			this.ValidateTransition(ODataWriterCore.WriterState.NavigationLinkWithContent);
			ODataWriterCore.NavigationLinkScope navigationLinkScope = (ODataWriterCore.NavigationLinkScope)this.scopes.Pop();
			ODataWriterCore.NavigationLinkScope navigationLinkScope2 = navigationLinkScope.Clone(ODataWriterCore.WriterState.NavigationLinkWithContent);
			this.scopes.Push(navigationLinkScope2);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "All the transition checks are encapsulated in this method.")]
		private void ValidateTransition(ODataWriterCore.WriterState newState)
		{
			if (!ODataWriterCore.IsErrorState(this.State) && ODataWriterCore.IsErrorState(newState))
			{
				return;
			}
			switch (this.State)
			{
			case ODataWriterCore.WriterState.Start:
				if (newState != ODataWriterCore.WriterState.Feed && newState != ODataWriterCore.WriterState.Entry)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				if (newState == ODataWriterCore.WriterState.Feed && !this.writingFeed)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelFeedWithEntryWriter);
				}
				if (newState == ODataWriterCore.WriterState.Entry && this.writingFeed)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelEntryWithFeedWriter);
				}
				break;
			case ODataWriterCore.WriterState.Entry:
				if (this.CurrentScope.Item == null)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromNullEntry(this.State.ToString(), newState.ToString()));
				}
				if (newState != ODataWriterCore.WriterState.NavigationLink)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromEntry(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.Feed:
				if (newState != ODataWriterCore.WriterState.Entry)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromFeed(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.NavigationLink:
				if (newState != ODataWriterCore.WriterState.NavigationLinkWithContent)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidStateTransition(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.NavigationLinkWithContent:
				if (newState != ODataWriterCore.WriterState.Feed && newState != ODataWriterCore.WriterState.Entry)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromExpandedLink(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataWriterCore.WriterState.Error:
				if (newState != ODataWriterCore.WriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
		private void PushScope(ODataWriterCore.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			ODataWriterCore.Scope scope;
			switch (state)
			{
			case ODataWriterCore.WriterState.Start:
			case ODataWriterCore.WriterState.Completed:
			case ODataWriterCore.WriterState.Error:
				scope = new ODataWriterCore.Scope(state, item, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.Entry:
				scope = this.CreateEntryScope((ODataEntry)item, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.Feed:
				scope = this.CreateFeedScope((ODataFeed)item, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.NavigationLink:
			case ODataWriterCore.WriterState.NavigationLinkWithContent:
				scope = this.CreateNavigationLinkScope(state, (ODataNavigationLink)item, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			this.scopes.Push(scope);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000FF90 File Offset: 0x0000E190
		private static bool ShouldAppendKey(IEdmNavigationSource currentNavigationSource)
		{
			if (currentNavigationSource is IEdmEntitySet)
			{
				return true;
			}
			IEdmContainedEntitySet edmContainedEntitySet = currentNavigationSource as IEdmContainedEntitySet;
			return edmContainedEntitySet != null && edmContainedEntitySet.NavigationProperty.Type.TypeKind() == EdmTypeKind.Collection;
		}

		// Token: 0x040001DB RID: 475
		protected readonly IWriterValidator WriterValidator;

		// Token: 0x040001DC RID: 476
		private readonly ODataOutputContext outputContext;

		// Token: 0x040001DD RID: 477
		private readonly bool writingFeed;

		// Token: 0x040001DE RID: 478
		private readonly bool writingDelta;

		// Token: 0x040001DF RID: 479
		private readonly IODataReaderWriterListener listener;

		// Token: 0x040001E0 RID: 480
		private readonly ODataWriterCore.ScopeStack scopes = new ODataWriterCore.ScopeStack();

		// Token: 0x040001E1 RID: 481
		private readonly FeedWithoutExpectedTypeValidator feedValidator;

		// Token: 0x040001E2 RID: 482
		private int currentEntryDepth;

		// Token: 0x02000062 RID: 98
		internal enum WriterState
		{
			// Token: 0x040001E4 RID: 484
			Start,
			// Token: 0x040001E5 RID: 485
			Entry,
			// Token: 0x040001E6 RID: 486
			Feed,
			// Token: 0x040001E7 RID: 487
			NavigationLink,
			// Token: 0x040001E8 RID: 488
			NavigationLinkWithContent,
			// Token: 0x040001E9 RID: 489
			Completed,
			// Token: 0x040001EA RID: 490
			Error
		}

		// Token: 0x02000063 RID: 99
		internal sealed class ScopeStack
		{
			// Token: 0x0600042D RID: 1069 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
			internal ScopeStack()
			{
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000FFDA File Offset: 0x0000E1DA
			internal int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
			internal ODataWriterCore.Scope Parent
			{
				get
				{
					ODataWriterCore.Scope scope = this.scopes.Pop();
					ODataWriterCore.Scope scope2 = this.scopes.Peek();
					this.scopes.Push(scope);
					return scope2;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001001C File Offset: 0x0000E21C
			internal ODataWriterCore.Scope ParentOfParent
			{
				get
				{
					ODataWriterCore.Scope scope = this.scopes.Pop();
					ODataWriterCore.Scope scope2 = this.scopes.Pop();
					ODataWriterCore.Scope scope3 = this.scopes.Peek();
					this.scopes.Push(scope2);
					this.scopes.Push(scope);
					return scope3;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x00010066 File Offset: 0x0000E266
			internal ODataWriterCore.Scope ParentOrNull
			{
				get
				{
					if (this.Count != 0)
					{
						return this.Parent;
					}
					return null;
				}
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x00010078 File Offset: 0x0000E278
			internal void Push(ODataWriterCore.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x00010086 File Offset: 0x0000E286
			internal ODataWriterCore.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00010093 File Offset: 0x0000E293
			internal ODataWriterCore.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x040001EB RID: 491
			private readonly Stack<ODataWriterCore.Scope> scopes = new Stack<ODataWriterCore.Scope>();
		}

		// Token: 0x02000064 RID: 100
		internal class Scope
		{
			// Token: 0x06000435 RID: 1077 RVA: 0x000100A0 File Offset: 0x0000E2A0
			internal Scope(ODataWriterCore.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.entityType = entityType;
				this.navigationSource = navigationSource;
				this.skipWriting = skipWriting;
				this.selectedProperties = selectedProperties;
				this.odataUri = odataUri;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x000100DD File Offset: 0x0000E2DD
			// (set) Token: 0x06000437 RID: 1079 RVA: 0x000100E5 File Offset: 0x0000E2E5
			public IEdmEntityType EntityType
			{
				get
				{
					return this.entityType;
				}
				set
				{
					this.entityType = value;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x000100EE File Offset: 0x0000E2EE
			internal ODataWriterCore.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x000100F6 File Offset: 0x0000E2F6
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x000100FE File Offset: 0x0000E2FE
			// (set) Token: 0x0600043B RID: 1083 RVA: 0x00010106 File Offset: 0x0000E306
			internal IEdmNavigationSource NavigationSource
			{
				get
				{
					return this.navigationSource;
				}
				set
				{
					this.navigationSource = value;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001010F File Offset: 0x0000E30F
			internal SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600043D RID: 1085 RVA: 0x00010117 File Offset: 0x0000E317
			internal ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001011F File Offset: 0x0000E31F
			internal bool SkipWriting
			{
				get
				{
					return this.skipWriting;
				}
			}

			// Token: 0x040001EC RID: 492
			private readonly ODataWriterCore.WriterState state;

			// Token: 0x040001ED RID: 493
			private readonly ODataItem item;

			// Token: 0x040001EE RID: 494
			private readonly bool skipWriting;

			// Token: 0x040001EF RID: 495
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x040001F0 RID: 496
			private IEdmNavigationSource navigationSource;

			// Token: 0x040001F1 RID: 497
			private IEdmEntityType entityType;

			// Token: 0x040001F2 RID: 498
			private ODataUri odataUri;
		}

		// Token: 0x02000065 RID: 101
		internal abstract class FeedScope : ODataWriterCore.Scope
		{
			// Token: 0x0600043F RID: 1087 RVA: 0x00010127 File Offset: 0x0000E327
			internal FeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.Feed, feed, navigationSource, entityType, skipWriting, selectedProperties, odataUri)
			{
				this.serializationInfo = feed.SerializationInfo;
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x00010145 File Offset: 0x0000E345
			// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001014D File Offset: 0x0000E34D
			internal int EntryCount
			{
				get
				{
					return this.entryCount;
				}
				set
				{
					this.entryCount = value;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00010156 File Offset: 0x0000E356
			internal InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
			{
				get
				{
					if (this.instanceAnnotationWriteTracker == null)
					{
						this.instanceAnnotationWriteTracker = new InstanceAnnotationWriteTracker();
					}
					return this.instanceAnnotationWriteTracker;
				}
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x00010171 File Offset: 0x0000E371
			internal ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), base.EntityType, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x040001F3 RID: 499
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x040001F4 RID: 500
			private int entryCount;

			// Token: 0x040001F5 RID: 501
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x040001F6 RID: 502
			private ODataFeedAndEntryTypeContext typeContext;
		}

		// Token: 0x02000066 RID: 102
		internal class EntryScope : ODataWriterCore.Scope
		{
			// Token: 0x06000444 RID: 1092 RVA: 0x000101B0 File Offset: 0x0000E3B0
			internal EntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool enableValidation = true)
				: base(ODataWriterCore.WriterState.Entry, entry, navigationSource, entityType, skipWriting, selectedProperties, odataUri)
			{
				if (entry != null)
				{
					this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(writerBehavior.AllowDuplicatePropertyNames, writingResponse, !enableValidation);
				}
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x000101E6 File Offset: 0x0000E3E6
			// (set) Token: 0x06000446 RID: 1094 RVA: 0x000101EE File Offset: 0x0000E3EE
			public IEdmEntityType EntityTypeFromMetadata
			{
				get
				{
					return this.entityTypeFromMetadata;
				}
				internal set
				{
					this.entityTypeFromMetadata = value;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x000101F7 File Offset: 0x0000E3F7
			public ODataFeedAndEntrySerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x000101FF File Offset: 0x0000E3FF
			internal DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
			{
				get
				{
					return this.duplicatePropertyNamesChecker;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000449 RID: 1097 RVA: 0x00010207 File Offset: 0x0000E407
			internal InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
			{
				get
				{
					if (this.instanceAnnotationWriteTracker == null)
					{
						this.instanceAnnotationWriteTracker = new InstanceAnnotationWriteTracker();
					}
					return this.instanceAnnotationWriteTracker;
				}
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00010222 File Offset: 0x0000E422
			public ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), this.EntityTypeFromMetadata, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x040001F7 RID: 503
			private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

			// Token: 0x040001F8 RID: 504
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x040001F9 RID: 505
			private IEdmEntityType entityTypeFromMetadata;

			// Token: 0x040001FA RID: 506
			private ODataFeedAndEntryTypeContext typeContext;

			// Token: 0x040001FB RID: 507
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x02000067 RID: 103
		internal class NavigationLinkScope : ODataWriterCore.Scope
		{
			// Token: 0x0600044B RID: 1099 RVA: 0x00010261 File Offset: 0x0000E461
			internal NavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, navLink, navigationSource, entityType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00010274 File Offset: 0x0000E474
			internal virtual ODataWriterCore.NavigationLinkScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataWriterCore.NavigationLinkScope(newWriterState, (ODataNavigationLink)base.Item, base.NavigationSource, base.EntityType, base.SkipWriting, base.SelectedProperties, base.ODataUri);
			}
		}
	}
}
