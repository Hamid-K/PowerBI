using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020001A5 RID: 421
	internal abstract class ODataWriterCore : ODataWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0002AA3C File Offset: 0x00028C3C
		protected ODataWriterCore(ODataOutputContext outputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
		{
			this.outputContext = outputContext;
			this.writingFeed = writingFeed;
			if (this.writingFeed && this.outputContext.Model.IsUserModel())
			{
				this.feedValidator = new FeedWithoutExpectedTypeValidator();
			}
			if (entitySet != null && entityType == null)
			{
				entityType = this.outputContext.EdmTypeResolver.GetElementType(entitySet);
			}
			this.scopes.Push(new ODataWriterCore.Scope(ODataWriterCore.WriterState.Start, null, entitySet, entityType, false, outputContext.MessageWriterSettings.MetadataDocumentUri.SelectedProperties()));
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002AACC File Offset: 0x00028CCC
		protected ODataWriterCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002AAD9 File Offset: 0x00028CD9
		protected ODataWriterCore.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002AAE6 File Offset: 0x00028CE6
		protected bool SkipWriting
		{
			get
			{
				return this.CurrentScope.SkipWriting;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002AAF3 File Offset: 0x00028CF3
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count == 2;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002AB04 File Offset: 0x00028D04
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

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0002AB30 File Offset: 0x00028D30
		protected IEdmEntityType ParentEntryEntityType
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopes.Parent;
				return parent.EntityType;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002AB50 File Offset: 0x00028D50
		protected IEdmEntitySet ParentEntryEntitySet
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopes.Parent;
				return parent.EntitySet;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002AB6F File Offset: 0x00028D6F
		protected int FeedScopeEntryCount
		{
			get
			{
				return ((ODataWriterCore.FeedScope)this.CurrentScope).EntryCount;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002AB84 File Offset: 0x00028D84
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

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002ABEA File Offset: 0x00028DEA
		protected IEdmEntityType EntryEntityType
		{
			get
			{
				return this.CurrentScope.EntityType;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002ABF8 File Offset: 0x00028DF8
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

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002AC5A File Offset: 0x00028E5A
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

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002AC74 File Offset: 0x00028E74
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

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002ACAC File Offset: 0x00028EAC
		public sealed override void WriteStart(ODataFeed feed)
		{
			this.VerifyCanWriteStartFeed(true, feed);
			this.WriteStartFeedImplementation(feed);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002ACBD File Offset: 0x00028EBD
		public sealed override void WriteStart(ODataEntry entry)
		{
			this.VerifyCanWriteStartEntry(true, entry);
			this.WriteStartEntryImplementation(entry);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002ACCE File Offset: 0x00028ECE
		public sealed override void WriteStart(ODataNavigationLink navigationLink)
		{
			this.VerifyCanWriteStartNavigationLink(true, navigationLink);
			this.WriteStartNavigationLinkImplementation(navigationLink);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002ACDF File Offset: 0x00028EDF
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002AD02 File Offset: 0x00028F02
		public sealed override void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, true);
			this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002AD14 File Offset: 0x00028F14
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

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002AD6E File Offset: 0x00028F6E
		protected static bool IsErrorState(ODataWriterCore.WriterState state)
		{
			return state == ODataWriterCore.WriterState.Error;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002AD74 File Offset: 0x00028F74
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

		// Token: 0x06000C3C RID: 3132
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000C3D RID: 3133
		protected abstract void FlushSynchronously();

		// Token: 0x06000C3E RID: 3134
		protected abstract void StartPayload();

		// Token: 0x06000C3F RID: 3135
		protected abstract void StartEntry(ODataEntry entry);

		// Token: 0x06000C40 RID: 3136
		protected abstract void EndEntry(ODataEntry entry);

		// Token: 0x06000C41 RID: 3137
		protected abstract void StartFeed(ODataFeed feed);

		// Token: 0x06000C42 RID: 3138
		protected abstract void EndPayload();

		// Token: 0x06000C43 RID: 3139
		protected abstract void EndFeed(ODataFeed feed);

		// Token: 0x06000C44 RID: 3140
		protected abstract void WriteDeferredNavigationLink(ODataNavigationLink navigationLink);

		// Token: 0x06000C45 RID: 3141
		protected abstract void StartNavigationLinkWithContent(ODataNavigationLink navigationLink);

		// Token: 0x06000C46 RID: 3142
		protected abstract void EndNavigationLinkWithContent(ODataNavigationLink navigationLink);

		// Token: 0x06000C47 RID: 3143
		protected abstract void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000C48 RID: 3144
		protected abstract ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties);

		// Token: 0x06000C49 RID: 3145
		protected abstract ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties);

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002ADA0 File Offset: 0x00028FA0
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

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002ADE2 File Offset: 0x00028FE2
		protected virtual ODataWriterCore.NavigationLinkScope CreateNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataWriterCore.NavigationLinkScope(writerState, navLink, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002ADF2 File Offset: 0x00028FF2
		protected virtual void PrepareEntryForWriteStart(ODataEntry entry, ODataFeedAndEntryTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002ADF4 File Offset: 0x00028FF4
		protected virtual void ValidateEntryMediaResource(ODataEntry entry, IEdmEntityType entityType)
		{
			bool flag = this.outputContext.UseDefaultFormatBehavior || this.outputContext.UseServerFormatBehavior;
			ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.outputContext.Model, flag);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002AE30 File Offset: 0x00029030
		protected IEdmEntityType ValidateEntryType(ODataEntry entry)
		{
			if (entry.TypeName == null && this.CurrentScope.EntityType != null)
			{
				return this.CurrentScope.EntityType;
			}
			return (IEdmEntityType)TypeNameOracle.ResolveAndValidateTypeName(this.outputContext.Model, entry.TypeName, EdmTypeKind.Entity);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002AE6F File Offset: 0x0002906F
		protected void ValidateNoDeltaLinkForExpandedFeed(ODataFeed feed)
		{
			if (feed.DeltaLink != null)
			{
				throw new ODataException(Strings.ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002AE8A File Offset: 0x0002908A
		private void VerifyCanWriteStartFeed(bool synchronousCall, ODataFeed feed)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002AF3C File Offset: 0x0002913C
		private void WriteStartFeedImplementation(ODataFeed feed)
		{
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.Feed);
			this.EnterScope(ODataWriterCore.WriterState.Feed, feed);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					if (feed.Count != null)
					{
						if (!this.IsTopLevel)
						{
							throw new ODataException(Strings.ODataWriterCore_OnlyTopLevelFeedsSupportInlineCount);
						}
						if (!this.outputContext.WritingResponse)
						{
							this.ThrowODataException(Strings.ODataWriterCore_InlineCountInRequest, feed);
						}
						ODataVersionChecker.CheckCount(this.outputContext.Version);
					}
					this.StartFeed(feed);
				});
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002AF92 File Offset: 0x00029192
		private void VerifyCanWriteStartEntry(bool synchronousCall, ODataEntry entry)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataWriterCore.WriterState.NavigationLink)
			{
				ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002B0C0 File Offset: 0x000292C0
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
							WriterValidationUtils.ValidateEntryInExpandedLink(edmEntityType, parentNavigationLinkScope.EntityType);
							entryScope.EntityTypeFromMetadata = parentNavigationLinkScope.EntityType;
						}
						else if (this.CurrentFeedValidator != null)
						{
							this.CurrentFeedValidator.ValidateEntry(edmEntityType);
						}
						entryScope.EntityType = edmEntityType;
						this.PrepareEntryForWriteStart(entry, entryScope.GetOrCreateTypeContext(this.outputContext.Model, this.outputContext.WritingResponse), entryScope.SelectedProperties);
						this.ValidateEntryMediaResource(entry, edmEntityType);
						WriterValidationUtils.ValidateEntryAtStart(entry);
					}
					this.StartEntry(entry);
				});
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002B122 File Offset: 0x00029322
		private void VerifyCanWriteStartNavigationLink(bool synchronousCall, ODataNavigationLink navigationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNavigationLink>(navigationLink, "navigationLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002B13C File Offset: 0x0002933C
		private void WriteStartNavigationLinkImplementation(ODataNavigationLink navigationLink)
		{
			this.EnterScope(ODataWriterCore.WriterState.NavigationLink, navigationLink);
			ODataEntry odataEntry = (ODataEntry)this.scopes.Parent.Item;
			if (odataEntry.MetadataBuilder != null)
			{
				navigationLink.SetMetadataBuilder(odataEntry.MetadataBuilder);
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002B17B File Offset: 0x0002937B
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002B2F0 File Offset: 0x000294F0
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
						if (odataEntry != null)
						{
							WriterValidationUtils.ValidateEntryAtEnd(odataEntry);
						}
						this.EndEntry(odataEntry);
						this.DecreaseEntryDepth();
					}
					break;
				case ODataWriterCore.WriterState.Feed:
					if (!this.SkipWriting)
					{
						ODataFeed odataFeed = (ODataFeed)currentScope.Item;
						WriterValidationUtils.ValidateFeedAtEnd(odataFeed, !this.outputContext.WritingResponse, this.outputContext.Version);
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

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002B304 File Offset: 0x00029504
		private void MarkNavigationLinkAsProcessed(ODataNavigationLink link)
		{
			ODataEntry odataEntry = (ODataEntry)this.scopes.Parent.Item;
			odataEntry.MetadataBuilder.MarkNavigationLinkProcessed(link.Name);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002B338 File Offset: 0x00029538
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool synchronousCall)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002B390 File Offset: 0x00029590
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
					WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
					this.WriteEntityReferenceInNavigationLinkContent((ODataNavigationLink)this.CurrentScope.Item, entityReferenceLink);
				});
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002B3F2 File Offset: 0x000295F2
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002B401 File Offset: 0x00029601
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002B41E File Offset: 0x0002961E
		private void ThrowODataException(string errorMessage, ODataItem item)
		{
			this.EnterScope(ODataWriterCore.WriterState.Error, item);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002B42E File Offset: 0x0002962E
		private void StartPayloadInStartState()
		{
			if (this.State == ODataWriterCore.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002B564 File Offset: 0x00029764
		private void CheckForNavigationLinkWithContent(ODataPayloadKind contentPayloadKind)
		{
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			if (currentScope.State == ODataWriterCore.WriterState.NavigationLink || currentScope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
			{
				ODataNavigationLink currentNavigationLink = (ODataNavigationLink)currentScope.Item;
				this.InterceptException(delegate
				{
					IEdmNavigationProperty edmNavigationProperty = WriterValidationUtils.ValidateNavigationLink(currentNavigationLink, this.ParentEntryEntityType, new ODataPayloadKind?(contentPayloadKind), this.outputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
					if (edmNavigationProperty != null)
					{
						this.CurrentScope.EntityType = edmNavigationProperty.ToEntityType();
						IEdmEntitySet parentEntryEntitySet = this.ParentEntryEntitySet;
						this.CurrentScope.EntitySet = ((parentEntryEntitySet == null) ? null : parentEntryEntitySet.FindNavigationTarget(edmNavigationProperty));
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

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002B65C File Offset: 0x0002985C
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

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002B6A4 File Offset: 0x000298A4
		private void IncreaseEntryDepth()
		{
			this.currentEntryDepth++;
			if (this.currentEntryDepth > this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth)
			{
				this.ThrowODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth), null);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002B702 File Offset: 0x00029902
		private void DecreaseEntryDepth()
		{
			this.currentEntryDepth--;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002B730 File Offset: 0x00029930
		private void EnterScope(ODataWriterCore.WriterState newState, ODataItem item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			bool flag = this.SkipWriting;
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			IEdmEntitySet edmEntitySet = null;
			IEdmEntityType edmEntityType = null;
			SelectedPropertiesNode selectedPropertiesNode = currentScope.SelectedProperties;
			if (newState == ODataWriterCore.WriterState.Entry || newState == ODataWriterCore.WriterState.Feed)
			{
				edmEntitySet = currentScope.EntitySet;
				edmEntityType = currentScope.EntityType;
			}
			ODataWriterCore.WriterState state = currentScope.State;
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
						IEdmEntityType entityType = currentScope.EntityType;
						IEdmNavigationProperty edmNavigationProperty = WriterValidationUtils.ValidateNavigationLink(odataNavigationLink, entityType, default(ODataPayloadKind?), this.outputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
						if (edmNavigationProperty != null)
						{
							edmEntityType = edmNavigationProperty.ToEntityType();
							IEdmEntitySet entitySet = currentScope.EntitySet;
							edmEntitySet = ((entitySet == null) ? null : entitySet.FindNavigationTarget(edmNavigationProperty));
						}
					}
				}
			}
			else if (newState == ODataWriterCore.WriterState.Entry && state == ODataWriterCore.WriterState.Feed)
			{
				((ODataWriterCore.FeedScope)currentScope).EntryCount++;
			}
			this.PushScope(newState, item, edmEntitySet, edmEntityType, flag, selectedPropertiesNode);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002B898 File Offset: 0x00029A98
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				ODataWriterCore.Scope scope = this.scopes.Pop();
				this.PushScope(ODataWriterCore.WriterState.Completed, null, scope.EntitySet, scope.EntityType, false, scope.SelectedProperties);
				this.InterceptException(new Action(this.EndPayload));
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002B8FC File Offset: 0x00029AFC
		private void PromoteNavigationLinkScope()
		{
			this.ValidateTransition(ODataWriterCore.WriterState.NavigationLinkWithContent);
			ODataWriterCore.NavigationLinkScope navigationLinkScope = (ODataWriterCore.NavigationLinkScope)this.scopes.Pop();
			ODataWriterCore.NavigationLinkScope navigationLinkScope2 = navigationLinkScope.Clone(ODataWriterCore.WriterState.NavigationLinkWithContent);
			this.scopes.Push(navigationLinkScope2);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002B938 File Offset: 0x00029B38
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

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002BB3C File Offset: 0x00029D3C
		private void PushScope(ODataWriterCore.WriterState state, ODataItem item, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			ODataWriterCore.Scope scope;
			switch (state)
			{
			case ODataWriterCore.WriterState.Start:
			case ODataWriterCore.WriterState.Completed:
			case ODataWriterCore.WriterState.Error:
				scope = new ODataWriterCore.Scope(state, item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			case ODataWriterCore.WriterState.Entry:
				scope = this.CreateEntryScope((ODataEntry)item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			case ODataWriterCore.WriterState.Feed:
				scope = this.CreateFeedScope((ODataFeed)item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			case ODataWriterCore.WriterState.NavigationLink:
			case ODataWriterCore.WriterState.NavigationLinkWithContent:
				scope = this.CreateNavigationLinkScope(state, (ODataNavigationLink)item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			this.scopes.Push(scope);
		}

		// Token: 0x04000450 RID: 1104
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000451 RID: 1105
		private readonly bool writingFeed;

		// Token: 0x04000452 RID: 1106
		private readonly ODataWriterCore.ScopeStack scopes = new ODataWriterCore.ScopeStack();

		// Token: 0x04000453 RID: 1107
		private readonly FeedWithoutExpectedTypeValidator feedValidator;

		// Token: 0x04000454 RID: 1108
		private int currentEntryDepth;

		// Token: 0x020001A6 RID: 422
		internal enum WriterState
		{
			// Token: 0x04000456 RID: 1110
			Start,
			// Token: 0x04000457 RID: 1111
			Entry,
			// Token: 0x04000458 RID: 1112
			Feed,
			// Token: 0x04000459 RID: 1113
			NavigationLink,
			// Token: 0x0400045A RID: 1114
			NavigationLinkWithContent,
			// Token: 0x0400045B RID: 1115
			Completed,
			// Token: 0x0400045C RID: 1116
			Error
		}

		// Token: 0x020001A7 RID: 423
		internal sealed class ScopeStack
		{
			// Token: 0x06000C69 RID: 3177 RVA: 0x0002BBE2 File Offset: 0x00029DE2
			internal ScopeStack()
			{
			}

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002BBF5 File Offset: 0x00029DF5
			internal int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0002BC04 File Offset: 0x00029E04
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

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0002BC38 File Offset: 0x00029E38
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

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0002BC82 File Offset: 0x00029E82
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

			// Token: 0x06000C6E RID: 3182 RVA: 0x0002BC94 File Offset: 0x00029E94
			internal void Push(ODataWriterCore.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x06000C6F RID: 3183 RVA: 0x0002BCA2 File Offset: 0x00029EA2
			internal ODataWriterCore.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x06000C70 RID: 3184 RVA: 0x0002BCAF File Offset: 0x00029EAF
			internal ODataWriterCore.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x0400045D RID: 1117
			private readonly Stack<ODataWriterCore.Scope> scopes = new Stack<ODataWriterCore.Scope>();
		}

		// Token: 0x020001A8 RID: 424
		internal class Scope
		{
			// Token: 0x06000C71 RID: 3185 RVA: 0x0002BCBC File Offset: 0x00029EBC
			internal Scope(ODataWriterCore.WriterState state, ODataItem item, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
			{
				this.state = state;
				this.item = item;
				this.entityType = entityType;
				this.entitySet = entitySet;
				this.skipWriting = skipWriting;
				this.selectedProperties = selectedProperties;
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002BCF1 File Offset: 0x00029EF1
			// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0002BCF9 File Offset: 0x00029EF9
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

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002BD02 File Offset: 0x00029F02
			internal ODataWriterCore.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002BD0A File Offset: 0x00029F0A
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002BD12 File Offset: 0x00029F12
			// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0002BD1A File Offset: 0x00029F1A
			internal IEdmEntitySet EntitySet
			{
				get
				{
					return this.entitySet;
				}
				set
				{
					this.entitySet = value;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002BD23 File Offset: 0x00029F23
			internal SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002BD2B File Offset: 0x00029F2B
			internal bool SkipWriting
			{
				get
				{
					return this.skipWriting;
				}
			}

			// Token: 0x0400045E RID: 1118
			private readonly ODataWriterCore.WriterState state;

			// Token: 0x0400045F RID: 1119
			private readonly ODataItem item;

			// Token: 0x04000460 RID: 1120
			private readonly bool skipWriting;

			// Token: 0x04000461 RID: 1121
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x04000462 RID: 1122
			private IEdmEntitySet entitySet;

			// Token: 0x04000463 RID: 1123
			private IEdmEntityType entityType;
		}

		// Token: 0x020001A9 RID: 425
		internal abstract class FeedScope : ODataWriterCore.Scope
		{
			// Token: 0x06000C7A RID: 3194 RVA: 0x0002BD33 File Offset: 0x00029F33
			internal FeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(ODataWriterCore.WriterState.Feed, feed, entitySet, entityType, skipWriting, selectedProperties)
			{
				this.serializationInfo = feed.SerializationInfo;
			}

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0002BD4F File Offset: 0x00029F4F
			// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0002BD57 File Offset: 0x00029F57
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

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0002BD60 File Offset: 0x00029F60
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

			// Token: 0x06000C7E RID: 3198 RVA: 0x0002BD7B File Offset: 0x00029F7B
			internal ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.EntitySet, EdmTypeWriterResolver.Instance.GetElementType(base.EntitySet), base.EntityType, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000464 RID: 1124
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x04000465 RID: 1125
			private int entryCount;

			// Token: 0x04000466 RID: 1126
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x04000467 RID: 1127
			private ODataFeedAndEntryTypeContext typeContext;
		}

		// Token: 0x020001AA RID: 426
		internal class EntryScope : ODataWriterCore.Scope
		{
			// Token: 0x06000C7F RID: 3199 RVA: 0x0002BDBA File Offset: 0x00029FBA
			internal EntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties)
				: base(ODataWriterCore.WriterState.Entry, entry, entitySet, entityType, skipWriting, selectedProperties)
			{
				if (entry != null)
				{
					this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(writerBehavior.AllowDuplicatePropertyNames, writingResponse);
					this.odataEntryTypeName = entry.TypeName;
				}
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002BDF5 File Offset: 0x00029FF5
			// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0002BDFD File Offset: 0x00029FFD
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

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002BE06 File Offset: 0x0002A006
			public ODataFeedAndEntrySerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002BE0E File Offset: 0x0002A00E
			internal DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
			{
				get
				{
					return this.duplicatePropertyNamesChecker;
				}
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002BE16 File Offset: 0x0002A016
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

			// Token: 0x06000C85 RID: 3205 RVA: 0x0002BE31 File Offset: 0x0002A031
			public ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.EntitySet, EdmTypeWriterResolver.Instance.GetElementType(base.EntitySet), this.EntityTypeFromMetadata, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000468 RID: 1128
			private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

			// Token: 0x04000469 RID: 1129
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x0400046A RID: 1130
			private readonly string odataEntryTypeName;

			// Token: 0x0400046B RID: 1131
			private IEdmEntityType entityTypeFromMetadata;

			// Token: 0x0400046C RID: 1132
			private ODataFeedAndEntryTypeContext typeContext;

			// Token: 0x0400046D RID: 1133
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x020001AB RID: 427
		internal class NavigationLinkScope : ODataWriterCore.Scope
		{
			// Token: 0x06000C86 RID: 3206 RVA: 0x0002BE70 File Offset: 0x0002A070
			internal NavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(writerState, navLink, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x0002BE81 File Offset: 0x0002A081
			internal virtual ODataWriterCore.NavigationLinkScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataWriterCore.NavigationLinkScope(newWriterState, (ODataNavigationLink)base.Item, base.EntitySet, base.EntityType, base.SkipWriting, base.SelectedProperties);
			}
		}
	}
}
