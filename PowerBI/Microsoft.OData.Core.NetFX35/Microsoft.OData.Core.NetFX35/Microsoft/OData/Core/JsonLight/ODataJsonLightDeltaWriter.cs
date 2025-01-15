using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000CC RID: 204
	internal sealed class ODataJsonLightDeltaWriter : ODataDeltaWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x0001A494 File Offset: 0x00018694
		public ODataJsonLightDeltaWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightEntryAndFeedSerializer = new ODataJsonLightEntryAndFeedSerializer(this.jsonLightOutputContext);
			this.NavigationSource = navigationSource;
			this.EntityType = entityType;
			if (navigationSource != null && entityType == null)
			{
				entityType = this.jsonLightOutputContext.EdmTypeResolver.GetElementType(navigationSource);
			}
			ODataUri odataUri = this.jsonLightOutputContext.MessageWriterSettings.ODataUri.Clone();
			this.scopes.Push(new ODataJsonLightDeltaWriter.Scope(ODataJsonLightDeltaWriter.WriterState.Start, null, navigationSource, entityType, this.jsonLightOutputContext.MessageWriterSettings.SelectedProperties, odataUri));
			this.jsonWriter = jsonLightOutputContext.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, jsonLightOutputContext.MessageWriterSettings.ODataSimplified);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001A554 File Offset: 0x00018754
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0001A55C File Offset: 0x0001875C
		public IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001A565 File Offset: 0x00018765
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001A56D File Offset: 0x0001876D
		public IEdmEntityType EntityType { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001A576 File Offset: 0x00018776
		private ODataJsonLightDeltaWriter.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001A583 File Offset: 0x00018783
		private ODataJsonLightDeltaWriter.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001A590 File Offset: 0x00018790
		private ODataJsonLightDeltaWriter.JsonLightDeltaFeedScope CurrentDeltaFeedScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightDeltaFeedScope;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001A5AC File Offset: 0x000187AC
		private ODataJsonLightDeltaWriter.JsonLightDeltaEntryScope CurrentDeltaEntryScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightDeltaEntryScope;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001A5C8 File Offset: 0x000187C8
		private ODataJsonLightDeltaWriter.JsonLightDeltaLinkScope CurrentDeltaLinkScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightDeltaLinkScope;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001A5E4 File Offset: 0x000187E4
		private ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyScope CurrentExpandedNavigationPropertyScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyScope;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001A5FE File Offset: 0x000187FE
		private bool IsTopLevel
		{
			get
			{
				return this.scopes.Count == 2;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001A60E File Offset: 0x0001880E
		private IEdmEntityType DeltaEntryEntityType
		{
			get
			{
				return this.CurrentScope.EntityType;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001A61C File Offset: 0x0001881C
		private DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				switch (this.State)
				{
				case ODataJsonLightDeltaWriter.WriterState.DeltaEntry:
				case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
					return this.CurrentDeltaEntryScope.DuplicatePropertyNamesChecker;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_DuplicatePropertyNamesChecker));
				}
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001A65E File Offset: 0x0001885E
		public override void WriteStart(ODataDeltaFeed deltaFeed)
		{
			this.VerifyCanWriteStartDeltaFeed(true, deltaFeed);
			this.WriteStartDeltaFeedImplementation(deltaFeed);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001A66F File Offset: 0x0001886F
		public override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataJsonLightDeltaWriter.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001A692 File Offset: 0x00018892
		public override void WriteStart(ODataNavigationLink navigationLink)
		{
			this.VerifyCanWriteNavigationLink(true, navigationLink);
			this.WriteStartNavigationLinkImplementation(navigationLink);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001A6A3 File Offset: 0x000188A3
		public override void WriteStart(ODataFeed expandedFeed)
		{
			this.VerifyCanWriteExpandedFeed(true, expandedFeed);
			this.WriteStartExpandedFeedImplementation(expandedFeed);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001A6B4 File Offset: 0x000188B4
		public override void WriteStart(ODataEntry deltaEntry)
		{
			this.VerifyCanWriteEntry(true, deltaEntry);
			this.WriteStartDeltaEntryImplementation(deltaEntry);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001A6C5 File Offset: 0x000188C5
		public override void WriteDeltaDeletedEntry(ODataDeltaDeletedEntry deltaDeletedEntry)
		{
			this.VerifyCanWriteEntry(true, deltaDeletedEntry);
			this.WriteStartDeltaEntryImplementation(deltaDeletedEntry);
			this.WriteEnd();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001A6DC File Offset: 0x000188DC
		public override void WriteDeltaLink(ODataDeltaLink deltaLink)
		{
			this.VerifyCanWriteLink(true, deltaLink);
			this.WriteStartDeltaLinkImplementation(deltaLink);
			this.WriteEnd();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001A6F3 File Offset: 0x000188F3
		public override void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.VerifyCanWriteLink(true, deltaDeletedLink);
			this.WriteStartDeltaLinkImplementation(deltaDeletedLink);
			this.WriteEnd();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001A70A File Offset: 0x0001890A
		public override void Flush()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001A718 File Offset: 0x00018918
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.VerifyNotDisposed();
			if (this.State == ODataJsonLightDeltaWriter.WriterState.Completed)
			{
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), ODataJsonLightDeltaWriter.WriterState.Error.ToString()));
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.Error, this.CurrentScope.Item);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001A772 File Offset: 0x00018972
		private void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001A77F File Offset: 0x0001897F
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.jsonLightOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001A79C File Offset: 0x0001899C
		private void VerifyCanWriteStartDeltaFeed(bool synchronousCall, ODataDeltaFeed deltaFeed)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaFeed>(deltaFeed, " delta feed");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001A7BC File Offset: 0x000189BC
		private void VerifyCanWriteNavigationLink(bool synchronousCall, ODataNavigationLink navigationLink)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataNavigationLink>(navigationLink, "navigationLink");
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001A7D6 File Offset: 0x000189D6
		private void VerifyCanWriteExpandedFeed(bool synchronousCall, ODataFeed expandedFeed)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(expandedFeed, "expandedFeed");
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001A7F0 File Offset: 0x000189F0
		private void VerifyCanWriteEntry(bool synchronousCall, ODataItem entry)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataItem>(entry, "entry");
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001A80A File Offset: 0x00018A0A
		private void VerifyCanWriteLink(bool synchronousCall, ODataDeltaLink deltaLink)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaLink>(deltaLink, "delta link");
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001A824 File Offset: 0x00018A24
		private void VerifyCanWriteLink(bool synchronousCall, ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaDeletedLink>(deltaDeletedLink, "delta deleted link");
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001A83E File Offset: 0x00018A3E
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001A850 File Offset: 0x00018A50
		private void ValidateTransition(ODataJsonLightDeltaWriter.WriterState newState)
		{
			if (!ODataJsonLightDeltaWriter.IsErrorState(this.State) && ODataJsonLightDeltaWriter.IsErrorState(newState))
			{
				return;
			}
			if (this.State != ODataJsonLightDeltaWriter.WriterState.DeltaEntry && newState == ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty)
			{
				throw new ODataException(Strings.ODataJsonLightDeltaWriter_InvalidTransitionToExpandedNavigationProperty(this.State.ToString(), newState.ToString()));
			}
			switch (this.State)
			{
			case ODataJsonLightDeltaWriter.WriterState.Start:
				if (newState != ODataJsonLightDeltaWriter.WriterState.DeltaFeed)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaEntry:
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
			case ODataJsonLightDeltaWriter.WriterState.DeltaLink:
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink:
				if (this.CurrentScope.Item == null)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromNullEntry(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaFeed:
				if (newState != ODataJsonLightDeltaWriter.WriterState.DeltaEntry && newState != ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry && newState != ODataJsonLightDeltaWriter.WriterState.DeltaLink && newState != ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromFeed(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty:
				throw new ODataException(Strings.ODataJsonLightDeltaWriter_InvalidTransitionFromExpandedNavigationProperty(this.State.ToString(), newState.ToString()));
			case ODataJsonLightDeltaWriter.WriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataJsonLightDeltaWriter.WriterState.Error:
				if (newState != ODataJsonLightDeltaWriter.WriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001AA08 File Offset: 0x00018C08
		private void ValidateEntryMediaResource(ODataEntry entry, IEdmEntityType entityType)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.AutoComputePayloadMetadataInJson && this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
			{
				return;
			}
			bool flag = this.jsonLightOutputContext.UseDefaultFormatBehavior || this.jsonLightOutputContext.UseServerFormatBehavior;
			ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.jsonLightOutputContext.Model, flag);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001AA84 File Offset: 0x00018C84
		private void WriteStartDeltaFeedImplementation(ODataDeltaFeed feed)
		{
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaFeed, feed);
			this.InterceptException(delegate
			{
				this.StartDeltaFeed(feed);
			});
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001AAEC File Offset: 0x00018CEC
		private void WriteStartNavigationLinkImplementation(ODataNavigationLink navigationLink)
		{
			if (!ODataJsonLightDeltaWriter.IsExpandedNavigationPropertyState(this.State))
			{
				this.EnterScope(ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty, navigationLink);
			}
			this.InterceptException(delegate
			{
				this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteStart(navigationLink);
			});
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001AB60 File Offset: 0x00018D60
		private void WriteStartExpandedFeedImplementation(ODataFeed expandedFeed)
		{
			if (!ODataJsonLightDeltaWriter.IsExpandedNavigationPropertyState(this.State))
			{
				throw new ODataException(Strings.ODataJsonLightDeltaWriter_WriteStartExpandedFeedCalledInInvalidState(this.State.ToString()));
			}
			this.InterceptException(delegate
			{
				this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteStart(expandedFeed);
			});
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001AC04 File Offset: 0x00018E04
		private void WriteStartDeltaEntryImplementation(ODataEntry entry)
		{
			if (ODataJsonLightDeltaWriter.IsExpandedNavigationPropertyState(this.State))
			{
				this.InterceptException(delegate
				{
					this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteStart(entry);
				});
				return;
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaEntry, entry);
			this.InterceptException(delegate
			{
				this.PreStartDeltaEntry(entry);
				this.StartDeltaEntry(entry);
			});
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001AC8C File Offset: 0x00018E8C
		private void WriteStartDeltaEntryImplementation(ODataDeltaDeletedEntry entry)
		{
			this.StartPayloadInStartState();
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry, entry);
			this.InterceptException(delegate
			{
				this.StartDeltaDeletedEntry(entry);
			});
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		private void ResolveEntityType(ODataEntry entry)
		{
			ODataJsonLightDeltaWriter.DeltaEntryScope currentDeltaEntryScope = this.CurrentDeltaEntryScope;
			IEdmEntityType edmEntityType = null;
			if (entry.SerializationInfo != null && this.jsonLightOutputContext.Model != null && this.jsonLightOutputContext.Model != EdmCoreModel.Instance && entry.SerializationInfo.NavigationSourceKind == EdmNavigationSourceKind.EntitySet)
			{
				IEdmEntitySet edmEntitySet = this.jsonLightOutputContext.Model.FindDeclaredEntitySet(entry.SerializationInfo.NavigationSourceName);
				if (edmEntitySet != null)
				{
					edmEntityType = edmEntitySet.EntityType();
				}
			}
			IEdmEntityType edmEntityType2 = null;
			if (!string.IsNullOrEmpty(entry.TypeName) && this.jsonLightOutputContext.Model != null && this.jsonLightOutputContext.Model != EdmCoreModel.Instance)
			{
				edmEntityType2 = TypeNameOracle.ResolveAndValidateTypeName(this.jsonLightOutputContext.Model, entry.TypeName, EdmTypeKind.Entity, this.jsonLightOutputContext.WriterValidator) as IEdmEntityType;
			}
			IEdmEntityType entityType = this.CurrentDeltaEntryScope.EntityType;
			currentDeltaEntryScope.EntityTypeFromMetadata = entityType;
			if (edmEntityType2 != null)
			{
				currentDeltaEntryScope.EntityType = edmEntityType2;
				return;
			}
			if (edmEntityType != null)
			{
				currentDeltaEntryScope.EntityType = edmEntityType;
				return;
			}
			if (entityType != null)
			{
				currentDeltaEntryScope.EntityType = entityType;
				return;
			}
			currentDeltaEntryScope.EntityType = null;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001ADDA File Offset: 0x00018FDA
		private void PreStartDeltaEntry(ODataEntry entry)
		{
			this.ResolveEntityType(entry);
			this.PrepareEntryForWriteStart(entry);
			this.ValidateEntryMediaResource(entry, this.CurrentDeltaEntryScope.EntityType);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001ADFC File Offset: 0x00018FFC
		private void PrepareEntryForWriteStart(ODataEntry entry)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.AutoComputePayloadMetadataInJson)
			{
				ODataJsonLightDeltaWriter.DeltaEntryScope currentDeltaEntryScope = this.CurrentDeltaEntryScope;
				ODataEntityMetadataBuilder odataEntityMetadataBuilder = this.jsonLightOutputContext.MetadataLevel.CreateEntityMetadataBuilder(entry, currentDeltaEntryScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, true), currentDeltaEntryScope.SerializationInfo, currentDeltaEntryScope.EntityType, currentDeltaEntryScope.SelectedProperties, true, this.jsonLightOutputContext.MessageWriterSettings.UseKeyAsSegment, this.jsonLightOutputContext.MessageWriterSettings.ODataUri);
				this.jsonLightOutputContext.MetadataLevel.InjectMetadataBuilder(entry, odataEntityMetadataBuilder);
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001AE8B File Offset: 0x0001908B
		private void WriteStartDeltaLinkImplementation(ODataDeltaLink deltaLink)
		{
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaLink, deltaLink);
			this.StartDeltaLink(deltaLink);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001AE9C File Offset: 0x0001909C
		private void WriteStartDeltaLinkImplementation(ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink, deltaDeletedLink);
			this.StartDeltaLink(deltaDeletedLink);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001AF85 File Offset: 0x00019185
		private void WriteEndImplementation()
		{
			if (this.State == ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty)
			{
				if (this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteEnd())
				{
					this.LeaveScope();
				}
				return;
			}
			this.InterceptException(delegate
			{
				ODataJsonLightDeltaWriter.Scope currentScope = this.CurrentScope;
				switch (currentScope.State)
				{
				case ODataJsonLightDeltaWriter.WriterState.Start:
				case ODataJsonLightDeltaWriter.WriterState.Completed:
				case ODataJsonLightDeltaWriter.WriterState.Error:
					throw new ODataException(Strings.ODataWriterCore_WriteEndCalledInInvalidState(currentScope.State.ToString()));
				case ODataJsonLightDeltaWriter.WriterState.DeltaEntry:
				{
					ODataEntry odataEntry = (ODataEntry)currentScope.Item;
					this.EndDeltaEntry();
					goto IL_00C2;
				}
				case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
				{
					ODataDeltaDeletedEntry odataDeltaDeletedEntry = (ODataDeltaDeletedEntry)currentScope.Item;
					this.EndDeltaEntry();
					goto IL_00C2;
				}
				case ODataJsonLightDeltaWriter.WriterState.DeltaFeed:
				{
					ODataDeltaFeed odataDeltaFeed = (ODataDeltaFeed)currentScope.Item;
					this.jsonLightOutputContext.WriterValidator.ValidateFeedAtEnd(ODataJsonLightDeltaWriter.DeltaConverter.ToODataFeed(odataDeltaFeed), false);
					this.EndDeltaFeed(odataDeltaFeed);
					goto IL_00C2;
				}
				case ODataJsonLightDeltaWriter.WriterState.DeltaLink:
				case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink:
					this.EndDeltaLink();
					goto IL_00C2;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_WriteEnd_UnreachableCodePath));
				IL_00C2:
				this.LeaveScope();
			});
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001AFBC File Offset: 0x000191BC
		private void WriteDeltaFeedCount(ODataDeltaFeed feed)
		{
			long? count = feed.Count;
			if (count != null)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
				this.jsonWriter.WriteValue(count.Value);
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001AFFC File Offset: 0x000191FC
		private void WriteDeltaFeedNextLink(ODataDeltaFeed feed)
		{
			Uri nextPageLink = feed.NextPageLink;
			if (nextPageLink != null && !this.CurrentDeltaFeedScope.NextPageLinkWritten)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
				this.jsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(nextPageLink));
				this.CurrentDeltaFeedScope.NextPageLinkWritten = true;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001B05C File Offset: 0x0001925C
		private void WriteDeltaFeedDeltaLink(ODataDeltaFeed feed)
		{
			Uri deltaLink = feed.DeltaLink;
			if (deltaLink != null && !this.CurrentDeltaFeedScope.DeltaLinkWritten)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.deltaLink");
				this.jsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(deltaLink));
				this.CurrentDeltaFeedScope.DeltaLinkWritten = true;
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001B0B9 File Offset: 0x000192B9
		private void WriteDeltaFeedContextUri()
		{
			this.CurrentDeltaFeedScope.ContextUriInfo = this.jsonLightEntryAndFeedSerializer.WriteDeltaContextUri(this.CurrentDeltaFeedScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, true), ODataDeltaKind.Feed, null);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001B0EA File Offset: 0x000192EA
		private void WriteDeltaFeedInstanceAnnotations(ODataDeltaFeed feed)
		{
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(feed.InstanceAnnotations, this.CurrentDeltaFeedScope.InstanceAnnotationWriteTracker, false, null);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001B10F File Offset: 0x0001930F
		private void WriteDeltaFeedValueStart()
		{
			this.jsonWriter.WriteValuePropertyName();
			this.jsonWriter.StartArrayScope();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001B127 File Offset: 0x00019327
		private void WriteDeltaEntryId(ODataDeltaDeletedEntry entry)
		{
			this.jsonWriter.WriteName("id");
			this.jsonWriter.WriteValue(entry.Id);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001B14C File Offset: 0x0001934C
		private void WriteDeltaEntryReason(ODataDeltaDeletedEntry entry)
		{
			if (entry.Reason == null)
			{
				return;
			}
			this.jsonWriter.WriteName("reason");
			switch (entry.Reason.Value)
			{
			case DeltaDeletedEntryReason.Deleted:
				this.jsonWriter.WriteValue("deleted");
				return;
			case DeltaDeletedEntryReason.Changed:
				this.jsonWriter.WriteValue("changed");
				return;
			default:
				return;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001B1B9 File Offset: 0x000193B9
		private void WriteDeltaEntryContextUri(ODataDeltaKind kind)
		{
			this.jsonLightEntryAndFeedSerializer.WriteDeltaContextUri(this.CurrentDeltaEntryScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, true), kind, this.GetParentDeltaFeedScope().ContextUriInfo);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001B1EA File Offset: 0x000193EA
		private void WriteDeltaEntryStartMetadata()
		{
			this.jsonLightEntryAndFeedSerializer.WriteEntryStartMetadataProperties(this.CurrentDeltaEntryScope);
			this.jsonLightEntryAndFeedSerializer.WriteEntryMetadataProperties(this.CurrentDeltaEntryScope);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001B20E File Offset: 0x0001940E
		private void WriteDeltaEntryEndMetadata()
		{
			this.jsonLightEntryAndFeedSerializer.WriteEntryEndMetadataProperties(this.CurrentDeltaEntryScope, this.CurrentDeltaEntryScope.DuplicatePropertyNamesChecker);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001B22C File Offset: 0x0001942C
		private void WriteDeltaEntryInstanceAnnotations(ODataEntry entry)
		{
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(entry.InstanceAnnotations, this.CurrentDeltaEntryScope.InstanceAnnotationWriteTracker, false, null);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001B254 File Offset: 0x00019454
		private void WriteDeltaEntryProperties(ODataEntry entry)
		{
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataJsonLightDeltaWriter.GetProjectedPropertiesAnnotation(this.CurrentDeltaEntryScope);
			this.jsonLightEntryAndFeedSerializer.WriteProperties(this.DeltaEntryEntityType, entry.Properties, false, this.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001B28C File Offset: 0x0001948C
		private void WriteDeltaLinkContextUri(ODataDeltaKind kind)
		{
			this.jsonLightEntryAndFeedSerializer.WriteDeltaContextUri(this.CurrentDeltaLinkScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, true), kind, null);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001B2B3 File Offset: 0x000194B3
		private void WriteDeltaLinkSource(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("source");
			this.jsonWriter.WriteValue(UriUtils.UriToString(link.Source));
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001B2DB File Offset: 0x000194DB
		private void WriteDeltaLinkRelationship(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("relationship");
			this.jsonWriter.WriteValue(link.Relationship);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001B2FE File Offset: 0x000194FE
		private void WriteDeltaLinkTarget(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("target");
			this.jsonWriter.WriteValue(UriUtils.UriToString(link.Target));
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001B326 File Offset: 0x00019526
		private void StartDeltaFeed(ODataDeltaFeed feed)
		{
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaFeedContextUri();
			this.WriteDeltaFeedCount(feed);
			this.WriteDeltaFeedNextLink(feed);
			this.WriteDeltaFeedDeltaLink(feed);
			this.WriteDeltaFeedInstanceAnnotations(feed);
			this.WriteDeltaFeedValueStart();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001B35B File Offset: 0x0001955B
		private void StartDeltaEntry(ODataEntry entry)
		{
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaEntryContextUri(ODataDeltaKind.Entry);
			this.WriteDeltaEntryStartMetadata();
			this.WriteDeltaEntryInstanceAnnotations(entry);
			this.WriteDeltaEntryProperties(entry);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001B383 File Offset: 0x00019583
		private void StartDeltaDeletedEntry(ODataDeltaDeletedEntry entry)
		{
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaEntryContextUri(ODataDeltaKind.DeletedEntry);
			this.WriteDeltaEntryId(entry);
			this.WriteDeltaEntryReason(entry);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001B3A5 File Offset: 0x000195A5
		private void StartDeltaLink(ODataDeltaLinkBase link)
		{
			this.jsonWriter.StartObjectScope();
			if (link is ODataDeltaLink)
			{
				this.WriteDeltaLinkContextUri(ODataDeltaKind.Link);
			}
			else
			{
				this.WriteDeltaLinkContextUri(ODataDeltaKind.DeletedLink);
			}
			this.WriteDeltaLinkSource(link);
			this.WriteDeltaLinkRelationship(link);
			this.WriteDeltaLinkTarget(link);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001B3E0 File Offset: 0x000195E0
		private void EndDeltaFeed(ODataDeltaFeed feed)
		{
			this.jsonWriter.EndArrayScope();
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(feed.InstanceAnnotations, this.CurrentDeltaFeedScope.InstanceAnnotationWriteTracker, false, null);
			this.WriteDeltaFeedNextLink(feed);
			this.WriteDeltaFeedDeltaLink(feed);
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001B434 File Offset: 0x00019634
		private void EndDeltaEntry()
		{
			if (this.CurrentScope.State == ODataJsonLightDeltaWriter.WriterState.DeltaEntry)
			{
				this.WriteDeltaEntryEndMetadata();
			}
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001B455 File Offset: 0x00019655
		private void EndDeltaLink()
		{
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001B480 File Offset: 0x00019680
		private void EnterScope(ODataJsonLightDeltaWriter.WriterState newState, ODataItem item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			ODataJsonLightDeltaWriter.Scope currentScope = this.CurrentScope;
			IEdmNavigationSource edmNavigationSource = null;
			IEdmEntityType edmEntityType = null;
			SelectedPropertiesNode selectedProperties = currentScope.SelectedProperties;
			ODataUri odataUri = currentScope.ODataUri.Clone();
			if (newState == ODataJsonLightDeltaWriter.WriterState.DeltaEntry || newState == ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry || newState == ODataJsonLightDeltaWriter.WriterState.DeltaLink || newState == ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink || newState == ODataJsonLightDeltaWriter.WriterState.DeltaFeed || newState == ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty)
			{
				edmNavigationSource = currentScope.NavigationSource;
				edmEntityType = currentScope.EntityType;
			}
			this.PushScope(newState, item, edmNavigationSource, edmEntityType, selectedProperties, odataUri);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001B534 File Offset: 0x00019734
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				ODataJsonLightDeltaWriter.Scope scope = this.scopes.Pop();
				this.PushScope(ODataJsonLightDeltaWriter.WriterState.Completed, null, scope.NavigationSource, scope.EntityType, scope.SelectedProperties, scope.ODataUri);
				this.InterceptException(new Action(this.EndPayload));
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001B59C File Offset: 0x0001979C
		private void PushScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			ODataJsonLightDeltaWriter.Scope scope;
			switch (state)
			{
			case ODataJsonLightDeltaWriter.WriterState.Start:
			case ODataJsonLightDeltaWriter.WriterState.Completed:
			case ODataJsonLightDeltaWriter.WriterState.Error:
				scope = new ODataJsonLightDeltaWriter.Scope(state, item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaEntry:
				scope = this.CreateDeltaEntryScope(ODataJsonLightDeltaWriter.WriterState.DeltaEntry, item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
				scope = this.CreateDeltaEntryScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry, item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaFeed:
				scope = this.CreateDeltaFeedScope(item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaLink:
				scope = this.CreateDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState.DeltaLink, item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink:
				scope = this.CreateDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink, item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty:
				scope = this.CreateExpandedNavigationPropertyScope(item, navigationSource, entityType, selectedProperties, odataUri);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			this.scopes.Push(scope);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001B674 File Offset: 0x00019874
		private ODataJsonLightDeltaWriter.DeltaFeedScope GetParentDeltaFeedScope()
		{
			ODataJsonLightDeltaWriter.ScopeStack scopeStack = new ODataJsonLightDeltaWriter.ScopeStack();
			ODataJsonLightDeltaWriter.Scope scope = null;
			if (this.scopes.Count > 0)
			{
				scopeStack.Push(this.scopes.Pop());
			}
			while (this.scopes.Count > 0)
			{
				ODataJsonLightDeltaWriter.Scope scope2 = this.scopes.Pop();
				scopeStack.Push(scope2);
				if (scope2 is ODataJsonLightDeltaWriter.DeltaFeedScope)
				{
					scope = scope2;
					IL_006B:
					while (scopeStack.Count > 0)
					{
						ODataJsonLightDeltaWriter.Scope scope3 = scopeStack.Pop();
						this.scopes.Push(scope3);
					}
					return scope as ODataJsonLightDeltaWriter.DeltaFeedScope;
				}
			}
			goto IL_006B;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001B6FB File Offset: 0x000198FB
		private ODataJsonLightDeltaWriter.DeltaFeedScope CreateDeltaFeedScope(ODataItem feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightDeltaFeedScope(feed, navigationSource, entityType, selectedProperties, odataUri);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001B709 File Offset: 0x00019909
		private ODataJsonLightDeltaWriter.DeltaEntryScope CreateDeltaEntryScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem entry, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightDeltaEntryScope(state, entry, this.GetEntrySerializationInfo(entry), navigationSource, entityType, this.jsonLightOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties, odataUri);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001B730 File Offset: 0x00019930
		private ODataJsonLightDeltaWriter.DeltaLinkScope CreateDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem link, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightDeltaLinkScope(state, link, this.GetLinkSerializationInfo(link), navigationSource, entityType, this.jsonLightOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties, odataUri);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001B757 File Offset: 0x00019957
		private ODataJsonLightDeltaWriter.ExpandedNavigationPropertyScope CreateExpandedNavigationPropertyScope(ODataItem navigationLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyScope(navigationLink, navigationSource, entityType, selectedProperties, odataUri, this.CurrentDeltaEntryScope.Entry, this.jsonLightOutputContext);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001B776 File Offset: 0x00019976
		private void StartPayloadInStartState()
		{
			if (this.State == ODataJsonLightDeltaWriter.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001B792 File Offset: 0x00019992
		private void StartPayload()
		{
			this.jsonLightEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001B79F File Offset: 0x0001999F
		private void EndPayload()
		{
			this.jsonLightEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001B7AC File Offset: 0x000199AC
		private void InterceptException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch
			{
				if (!ODataJsonLightDeltaWriter.IsErrorState(this.State))
				{
					this.EnterScope(ODataJsonLightDeltaWriter.WriterState.Error, this.CurrentScope.Item);
				}
				throw;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001B7F4 File Offset: 0x000199F4
		private ODataDeltaSerializationInfo GetParentFeedSerializationInfo()
		{
			ODataJsonLightDeltaWriter.DeltaFeedScope deltaFeedScope = this.CurrentScope as ODataJsonLightDeltaWriter.DeltaFeedScope;
			if (deltaFeedScope != null)
			{
				ODataDeltaFeed odataDeltaFeed = (ODataDeltaFeed)deltaFeedScope.Item;
				return ODataJsonLightDeltaWriter.DeltaConverter.ToDeltaSerializationInfo(odataDeltaFeed.SerializationInfo);
			}
			return null;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001B82C File Offset: 0x00019A2C
		private ODataFeedAndEntrySerializationInfo GetEntrySerializationInfo(ODataItem item)
		{
			ODataFeedAndEntrySerializationInfo odataFeedAndEntrySerializationInfo = null;
			ODataEntry odataEntry = item as ODataEntry;
			if (odataEntry != null)
			{
				odataFeedAndEntrySerializationInfo = odataEntry.SerializationInfo;
			}
			ODataDeltaDeletedEntry odataDeltaDeletedEntry = item as ODataDeltaDeletedEntry;
			if (odataDeltaDeletedEntry != null)
			{
				odataFeedAndEntrySerializationInfo = ODataJsonLightDeltaWriter.DeltaConverter.ToFeedAndEntrySerializationInfo(odataDeltaDeletedEntry.SerializationInfo);
			}
			if (odataFeedAndEntrySerializationInfo == null)
			{
				odataFeedAndEntrySerializationInfo = ODataJsonLightDeltaWriter.DeltaConverter.ToFeedAndEntrySerializationInfo(this.GetParentFeedSerializationInfo());
			}
			return odataFeedAndEntrySerializationInfo;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001B874 File Offset: 0x00019A74
		private ODataDeltaSerializationInfo GetLinkSerializationInfo(ODataItem item)
		{
			ODataDeltaSerializationInfo odataDeltaSerializationInfo = null;
			ODataDeltaLink odataDeltaLink = item as ODataDeltaLink;
			if (odataDeltaLink != null)
			{
				odataDeltaSerializationInfo = odataDeltaLink.SerializationInfo;
			}
			ODataDeltaDeletedLink odataDeltaDeletedLink = item as ODataDeltaDeletedLink;
			if (odataDeltaDeletedLink != null)
			{
				odataDeltaSerializationInfo = odataDeltaDeletedLink.SerializationInfo;
			}
			return odataDeltaSerializationInfo ?? this.GetParentFeedSerializationInfo();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		private static bool IsErrorState(ODataJsonLightDeltaWriter.WriterState state)
		{
			return state == ODataJsonLightDeltaWriter.WriterState.Error;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001B8B6 File Offset: 0x00019AB6
		private static bool IsExpandedNavigationPropertyState(ODataJsonLightDeltaWriter.WriterState state)
		{
			return state == ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001B8BC File Offset: 0x00019ABC
		private static ProjectedPropertiesAnnotation GetProjectedPropertiesAnnotation(ODataJsonLightDeltaWriter.Scope currentScope)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataJsonLightDeltaWriter.Scope>(currentScope, "currentScope");
			ODataItem item = currentScope.Item;
			if (item != null)
			{
				return item.GetAnnotation<ProjectedPropertiesAnnotation>();
			}
			return null;
		}

		// Token: 0x04000350 RID: 848
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000351 RID: 849
		private readonly ODataJsonLightEntryAndFeedSerializer jsonLightEntryAndFeedSerializer;

		// Token: 0x04000352 RID: 850
		private readonly ODataJsonLightDeltaWriter.ScopeStack scopes = new ODataJsonLightDeltaWriter.ScopeStack();

		// Token: 0x04000353 RID: 851
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x04000354 RID: 852
		private readonly IJsonWriter jsonWriter;

		// Token: 0x020000CD RID: 205
		private enum WriterState
		{
			// Token: 0x04000358 RID: 856
			Start,
			// Token: 0x04000359 RID: 857
			DeltaEntry,
			// Token: 0x0400035A RID: 858
			DeltaDeletedEntry,
			// Token: 0x0400035B RID: 859
			DeltaFeed,
			// Token: 0x0400035C RID: 860
			DeltaLink,
			// Token: 0x0400035D RID: 861
			DeltaDeletedLink,
			// Token: 0x0400035E RID: 862
			ExpandedNavigationProperty,
			// Token: 0x0400035F RID: 863
			Completed,
			// Token: 0x04000360 RID: 864
			Error
		}

		// Token: 0x020000CE RID: 206
		[Flags]
		private enum JsonLightEntryMetadataProperty
		{
			// Token: 0x04000362 RID: 866
			EditLink = 1,
			// Token: 0x04000363 RID: 867
			ReadLink = 2,
			// Token: 0x04000364 RID: 868
			MediaEditLink = 4,
			// Token: 0x04000365 RID: 869
			MediaReadLink = 8,
			// Token: 0x04000366 RID: 870
			MediaContentType = 16,
			// Token: 0x04000367 RID: 871
			MediaETag = 32
		}

		// Token: 0x020000CF RID: 207
		private class Scope
		{
			// Token: 0x060007D6 RID: 2006 RVA: 0x0001B8E6 File Offset: 0x00019AE6
			public Scope(ODataJsonLightDeltaWriter.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.EntityType = entityType;
				this.NavigationSource = navigationSource;
				this.selectedProperties = selectedProperties;
				this.odataUri = odataUri;
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001B91B File Offset: 0x00019B1B
			// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0001B923 File Offset: 0x00019B23
			public IEdmEntityType EntityType { get; set; }

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001B92C File Offset: 0x00019B2C
			public ODataJsonLightDeltaWriter.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001B934 File Offset: 0x00019B34
			public ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x060007DB RID: 2011 RVA: 0x0001B93C File Offset: 0x00019B3C
			// (set) Token: 0x060007DC RID: 2012 RVA: 0x0001B944 File Offset: 0x00019B44
			public IEdmNavigationSource NavigationSource { get; private set; }

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001B94D File Offset: 0x00019B4D
			public SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001B955 File Offset: 0x00019B55
			public ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x04000368 RID: 872
			private readonly ODataJsonLightDeltaWriter.WriterState state;

			// Token: 0x04000369 RID: 873
			private readonly ODataItem item;

			// Token: 0x0400036A RID: 874
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x0400036B RID: 875
			private readonly ODataUri odataUri;
		}

		// Token: 0x020000D0 RID: 208
		private abstract class DeltaEntryScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x060007DF RID: 2015 RVA: 0x0001B95D File Offset: 0x00019B5D
			protected DeltaEntryScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, entry, navigationSource, entityType, selectedProperties, odataUri)
			{
				this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(writerBehavior.AllowDuplicatePropertyNames, true, false);
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001B98A File Offset: 0x00019B8A
			// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001B992 File Offset: 0x00019B92
			public IEdmEntityType EntityTypeFromMetadata { get; set; }

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001B99B File Offset: 0x00019B9B
			public ODataFeedAndEntrySerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001B9A3 File Offset: 0x00019BA3
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
			{
				get
				{
					return this.duplicatePropertyNamesChecker;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001B9AB File Offset: 0x00019BAB
			public InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
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

			// Token: 0x060007E5 RID: 2021 RVA: 0x0001B9C8 File Offset: 0x00019BC8
			public ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), this.EntityTypeFromMetadata ?? base.EntityType, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x0400036E RID: 878
			private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

			// Token: 0x0400036F RID: 879
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x04000370 RID: 880
			private ODataFeedAndEntryTypeContext typeContext;

			// Token: 0x04000371 RID: 881
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x020000D1 RID: 209
		private abstract class DeltaFeedScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x060007E6 RID: 2022 RVA: 0x0001BA1C File Offset: 0x00019C1C
			protected DeltaFeedScope(ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataJsonLightDeltaWriter.WriterState.DeltaFeed, item, navigationSource, entityType, selectedProperties, odataUri)
			{
				ODataDeltaFeed odataDeltaFeed = item as ODataDeltaFeed;
				this.serializationInfo = odataDeltaFeed.SerializationInfo;
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001BA4A File Offset: 0x00019C4A
			public InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
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

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001BA65 File Offset: 0x00019C65
			// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001BA6D File Offset: 0x00019C6D
			public ODataContextUrlInfo ContextUriInfo { get; set; }

			// Token: 0x060007EA RID: 2026 RVA: 0x0001BA78 File Offset: 0x00019C78
			public ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(ODataJsonLightDeltaWriter.DeltaConverter.ToFeedAndEntrySerializationInfo(this.serializationInfo), base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), base.EntityType, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000373 RID: 883
			private readonly ODataDeltaFeedSerializationInfo serializationInfo;

			// Token: 0x04000374 RID: 884
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x04000375 RID: 885
			private ODataFeedAndEntryTypeContext typeContext;
		}

		// Token: 0x020000D2 RID: 210
		private abstract class DeltaLinkScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x060007EB RID: 2027 RVA: 0x0001BAC7 File Offset: 0x00019CC7
			protected DeltaLinkScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem link, ODataDeltaSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, link, navigationSource, entityType, selectedProperties, odataUri)
			{
				this.serializationInfo = ODataJsonLightDeltaWriter.DeltaConverter.ToFeedAndEntrySerializationInfo(serializationInfo);
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x0001BAFA File Offset: 0x00019CFA
			public ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), this.fakeEntityType, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000377 RID: 887
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x04000378 RID: 888
			private readonly EdmEntityType fakeEntityType = new EdmEntityType("MyNS", "Fake");

			// Token: 0x04000379 RID: 889
			private ODataFeedAndEntryTypeContext typeContext;
		}

		// Token: 0x020000D3 RID: 211
		private abstract class ExpandedNavigationPropertyScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x060007ED RID: 2029 RVA: 0x0001BB39 File Offset: 0x00019D39
			protected ExpandedNavigationPropertyScope(ODataItem navigationLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataJsonLightDeltaWriter.WriterState.ExpandedNavigationProperty, navigationLink, navigationSource, entityType, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x020000D4 RID: 212
		private sealed class JsonLightDeltaEntryScope : ODataJsonLightDeltaWriter.DeltaEntryScope, IODataJsonLightWriterEntryState
		{
			// Token: 0x060007EE RID: 2030 RVA: 0x0001BB4C File Offset: 0x00019D4C
			public JsonLightDeltaEntryScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, entry, serializationInfo, navigationSource, entityType, writerBehavior, selectedProperties, odataUri)
			{
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001BB6C File Offset: 0x00019D6C
			public ODataEntry Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001BB79 File Offset: 0x00019D79
			// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001BB82 File Offset: 0x00019D82
			public bool EditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.EditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.EditLink);
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001BB8B File Offset: 0x00019D8B
			// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001BB94 File Offset: 0x00019D94
			public bool ReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.ReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.ReadLink);
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001BB9D File Offset: 0x00019D9D
			// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0001BBA6 File Offset: 0x00019DA6
			public bool MediaEditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaEditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaEditLink);
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001BBAF File Offset: 0x00019DAF
			// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001BBB8 File Offset: 0x00019DB8
			public bool MediaReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaReadLink);
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001BBC1 File Offset: 0x00019DC1
			// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001BBCB File Offset: 0x00019DCB
			public bool MediaContentTypeWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaContentType);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaContentType);
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001BBD5 File Offset: 0x00019DD5
			// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001BBDF File Offset: 0x00019DDF
			public bool MediaETagWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaETag);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaETag);
				}
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x0001BBE9 File Offset: 0x00019DE9
			private void SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x0001BBF9 File Offset: 0x00019DF9
			private bool IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x0400037A RID: 890
			private int alreadyWrittenMetadataProperties;
		}

		// Token: 0x020000D5 RID: 213
		private sealed class JsonLightExpandedNavigationPropertyScope : ODataJsonLightDeltaWriter.ExpandedNavigationPropertyScope
		{
			// Token: 0x060007FE RID: 2046 RVA: 0x0001BC06 File Offset: 0x00019E06
			public JsonLightExpandedNavigationPropertyScope(ODataItem navigationLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri, ODataEntry parentDeltaEntry, ODataJsonLightOutputContext jsonLightOutputContext)
				: base(navigationLink, navigationSource, entityType, selectedProperties, odataUri)
			{
				this.jsonLightExpandedNavigationPropertyWriter = new ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyWriter(navigationSource, entityType, parentDeltaEntry, jsonLightOutputContext);
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001BC26 File Offset: 0x00019E26
			public ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyWriter JsonLightExpandedNavigationPropertyWriter
			{
				get
				{
					return this.jsonLightExpandedNavigationPropertyWriter;
				}
			}

			// Token: 0x0400037B RID: 891
			private ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyWriter jsonLightExpandedNavigationPropertyWriter;
		}

		// Token: 0x020000D6 RID: 214
		private sealed class JsonLightDeltaFeedScope : ODataJsonLightDeltaWriter.DeltaFeedScope
		{
			// Token: 0x06000800 RID: 2048 RVA: 0x0001BC2E File Offset: 0x00019E2E
			public JsonLightDeltaFeedScope(ODataItem feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(feed, navigationSource, entityType, selectedProperties, odataUri)
			{
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001BC3D File Offset: 0x00019E3D
			// (set) Token: 0x06000802 RID: 2050 RVA: 0x0001BC45 File Offset: 0x00019E45
			public bool NextPageLinkWritten { get; set; }

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001BC4E File Offset: 0x00019E4E
			// (set) Token: 0x06000804 RID: 2052 RVA: 0x0001BC56 File Offset: 0x00019E56
			public bool DeltaLinkWritten { get; set; }
		}

		// Token: 0x020000D7 RID: 215
		private sealed class JsonLightDeltaLinkScope : ODataJsonLightDeltaWriter.DeltaLinkScope
		{
			// Token: 0x06000805 RID: 2053 RVA: 0x0001BC60 File Offset: 0x00019E60
			public JsonLightDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem link, ODataDeltaSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, link, serializationInfo, navigationSource, entityType, writerBehavior, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x020000D8 RID: 216
		private sealed class ScopeStack
		{
			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001BC80 File Offset: 0x00019E80
			public int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x0001BC8D File Offset: 0x00019E8D
			public void Push(ODataJsonLightDeltaWriter.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x0001BC9B File Offset: 0x00019E9B
			public ODataJsonLightDeltaWriter.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x06000809 RID: 2057 RVA: 0x0001BCA8 File Offset: 0x00019EA8
			public ODataJsonLightDeltaWriter.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x0400037E RID: 894
			private readonly Stack<ODataJsonLightDeltaWriter.Scope> scopes = new Stack<ODataJsonLightDeltaWriter.Scope>();
		}

		// Token: 0x020000D9 RID: 217
		private static class DeltaConverter
		{
			// Token: 0x0600080B RID: 2059 RVA: 0x0001BCC8 File Offset: 0x00019EC8
			public static ODataFeedAndEntrySerializationInfo ToFeedAndEntrySerializationInfo(ODataDeltaSerializationInfo serializationInfo)
			{
				if (serializationInfo == null)
				{
					return null;
				}
				return new ODataFeedAndEntrySerializationInfo
				{
					NavigationSourceName = serializationInfo.NavigationSourceName,
					NavigationSourceKind = EdmNavigationSourceKind.EntitySet,
					NavigationSourceEntityTypeName = "null",
					ExpectedTypeName = "null"
				};
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x0001BD0C File Offset: 0x00019F0C
			public static ODataFeedAndEntrySerializationInfo ToFeedAndEntrySerializationInfo(ODataDeltaFeedSerializationInfo serializationInfo)
			{
				if (serializationInfo == null)
				{
					return null;
				}
				return new ODataFeedAndEntrySerializationInfo
				{
					NavigationSourceName = serializationInfo.EntitySetName,
					NavigationSourceKind = EdmNavigationSourceKind.EntitySet,
					NavigationSourceEntityTypeName = serializationInfo.EntityTypeName,
					ExpectedTypeName = serializationInfo.ExpectedTypeName
				};
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x0001BD50 File Offset: 0x00019F50
			public static ODataDeltaSerializationInfo ToDeltaSerializationInfo(ODataDeltaFeedSerializationInfo serializationInfo)
			{
				if (serializationInfo == null)
				{
					return null;
				}
				return new ODataDeltaSerializationInfo
				{
					NavigationSourceName = serializationInfo.EntitySetName
				};
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x0001BD78 File Offset: 0x00019F78
			public static ODataFeed ToODataFeed(ODataDeltaFeed deltaFeed)
			{
				ODataFeed odataFeed = ODataJsonLightDeltaWriter.DeltaConverter.Clone(deltaFeed);
				odataFeed.SetSerializationInfo(ODataJsonLightDeltaWriter.DeltaConverter.ToFeedAndEntrySerializationInfo(deltaFeed.SerializationInfo));
				return odataFeed;
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x0001BDA0 File Offset: 0x00019FA0
			private static ODataFeed Clone(ODataFeedBase feedBase)
			{
				return new ODataFeed
				{
					Count = feedBase.Count,
					DeltaLink = feedBase.DeltaLink,
					Id = feedBase.Id,
					InstanceAnnotations = feedBase.InstanceAnnotations,
					NextPageLink = feedBase.NextPageLink
				};
			}
		}

		// Token: 0x020000DA RID: 218
		private sealed class JsonLightExpandedNavigationPropertyWriter
		{
			// Token: 0x06000810 RID: 2064 RVA: 0x0001BDF0 File Offset: 0x00019FF0
			public JsonLightExpandedNavigationPropertyWriter(IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataEntry parentDeltaEntry, ODataJsonLightOutputContext jsonLightOutputContext)
			{
				this.parentDeltaEntry = parentDeltaEntry;
				this.entryWriter = new ODataJsonLightWriter(jsonLightOutputContext, navigationSource, entityType, false, false, true, null);
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x0001BE12 File Offset: 0x0001A012
			public void WriteStart(ODataEntry entry)
			{
				this.IncreaseEntryDepth();
				this.entryWriter.WriteStart(entry);
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x0001BE26 File Offset: 0x0001A026
			public void WriteStart(ODataFeed feed)
			{
				this.IncreaseEntryDepth();
				this.entryWriter.WriteStart(feed);
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x0001BE3A File Offset: 0x0001A03A
			public void WriteStart(ODataNavigationLink navigationLink)
			{
				this.IncreaseEntryDepth();
				this.entryWriter.WriteStart(navigationLink);
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x0001BE4E File Offset: 0x0001A04E
			public bool WriteEnd()
			{
				this.entryWriter.WriteEnd();
				return this.DecreaseEntryDepth();
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x0001BE61 File Offset: 0x0001A061
			private void IncreaseEntryDepth()
			{
				if (this.currentEntryDepth == 0)
				{
					this.entryWriter.WriteStart(this.parentDeltaEntry);
				}
				this.currentEntryDepth++;
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x0001BE8A File Offset: 0x0001A08A
			private bool DecreaseEntryDepth()
			{
				this.currentEntryDepth--;
				if (this.currentEntryDepth == 0)
				{
					this.entryWriter.WriteEnd();
					return true;
				}
				return false;
			}

			// Token: 0x0400037F RID: 895
			private readonly ODataWriter entryWriter;

			// Token: 0x04000380 RID: 896
			private readonly ODataEntry parentDeltaEntry;

			// Token: 0x04000381 RID: 897
			private int currentEntryDepth;
		}
	}
}
