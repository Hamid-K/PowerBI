using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000A5 RID: 165
	internal abstract class ODataWriterCore : ODataWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00010718 File Offset: 0x0000E918
		protected ODataWriterCore(ODataOutputContext outputContext, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool writingResourceSet, bool writingDelta = false, IODataReaderWriterListener listener = null)
		{
			this.outputContext = outputContext;
			this.writingResourceSet = writingResourceSet;
			this.writingDelta = writingDelta;
			this.WriterValidator = outputContext.WriterValidator;
			if (this.writingResourceSet && this.outputContext.Model.IsUserModel())
			{
				this.resourceSetValidator = new ResourceSetWithoutExpectedTypeValidator();
			}
			if (navigationSource != null && resourceType == null)
			{
				resourceType = this.outputContext.EdmTypeResolver.GetElementType(navigationSource);
			}
			ODataUri odataUri = outputContext.MessageWriterSettings.ODataUri.Clone();
			if (!writingResourceSet && odataUri != null && odataUri.Path != null)
			{
				odataUri.Path = odataUri.Path.TrimEndingKeySegment();
			}
			this.listener = listener;
			this.scopeStack.Push(new ODataWriterCore.Scope(ODataWriterCore.WriterState.Start, null, navigationSource, resourceType, false, outputContext.MessageWriterSettings.SelectedProperties, odataUri));
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000107F1 File Offset: 0x0000E9F1
		protected ODataWriterCore.Scope CurrentScope
		{
			get
			{
				return this.scopeStack.Peek();
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000107FE File Offset: 0x0000E9FE
		protected ODataWriterCore.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001080B File Offset: 0x0000EA0B
		protected bool SkipWriting
		{
			get
			{
				return this.CurrentScope.SkipWriting;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00010818 File Offset: 0x0000EA18
		protected bool IsTopLevel
		{
			get
			{
				return this.scopeStack.Count == 2;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00010828 File Offset: 0x0000EA28
		protected int ScopeLevel
		{
			get
			{
				return this.scopeStack.Count;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00010838 File Offset: 0x0000EA38
		protected ODataNestedResourceInfo ParentNestedResourceInfo
		{
			get
			{
				ODataWriterCore.Scope parentOrNull = this.scopeStack.ParentOrNull;
				if (parentOrNull != null)
				{
					return parentOrNull.Item as ODataNestedResourceInfo;
				}
				return null;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00010864 File Offset: 0x0000EA64
		protected ODataNestedResourceInfo BelongingNestedResourceInfo
		{
			get
			{
				ODataWriterCore.Scope scope = this.scopeStack.ParentOrNull;
				if (scope is ODataWriterCore.NestedResourceInfoScope)
				{
					return scope.Item as ODataNestedResourceInfo;
				}
				if (!(scope is ODataWriterCore.ResourceSetScope))
				{
					return null;
				}
				scope = this.scopeStack.ParentOfParent;
				if (scope != null)
				{
					return scope.Item as ODataNestedResourceInfo;
				}
				return null;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000108B8 File Offset: 0x0000EAB8
		protected IEdmStructuredType ParentResourceType
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopeStack.Parent;
				return parent.ResourceType;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000108D8 File Offset: 0x0000EAD8
		protected IEdmNavigationSource ParentResourceNavigationSource
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopeStack.Parent;
				return parent.NavigationSource;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000108F7 File Offset: 0x0000EAF7
		protected ODataWriterCore.Scope ParentScope
		{
			get
			{
				return Enumerable.First<ODataWriterCore.Scope>(Enumerable.Skip<ODataWriterCore.Scope>(this.scopeStack.Scopes, 1));
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001090F File Offset: 0x0000EB0F
		protected int ResourceSetScopeResourceCount
		{
			get
			{
				return ((ODataWriterCore.ResourceSetScope)this.CurrentScope).ResourceCount;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00010924 File Offset: 0x0000EB24
		protected IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
		{
			get
			{
				ODataWriterCore.ResourceScope resourceScope;
				switch (this.State)
				{
				case ODataWriterCore.WriterState.Resource:
					resourceScope = (ODataWriterCore.ResourceScope)this.CurrentScope;
					goto IL_0053;
				case ODataWriterCore.WriterState.NestedResourceInfo:
				case ODataWriterCore.WriterState.NestedResourceInfoWithContent:
					resourceScope = (ODataWriterCore.ResourceScope)this.scopeStack.Parent;
					goto IL_0053;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_PropertyAndAnnotationCollector));
				IL_0053:
				return resourceScope.DuplicatePropertyNameChecker;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001098A File Offset: 0x0000EB8A
		protected IEdmStructuredType ResourceType
		{
			get
			{
				return this.CurrentScope.ResourceType;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00010998 File Offset: 0x0000EB98
		protected ODataWriterCore.NestedResourceInfoScope ParentNestedResourceInfoScope
		{
			get
			{
				ODataWriterCore.Scope scope = this.scopeStack.Parent;
				if (scope.State == ODataWriterCore.WriterState.Start)
				{
					return null;
				}
				if (scope.State == ODataWriterCore.WriterState.ResourceSet)
				{
					scope = this.scopeStack.ParentOfParent;
					if (scope.State == ODataWriterCore.WriterState.Start || (scope.State == ODataWriterCore.WriterState.ResourceSet && scope.ResourceType != null && scope.ResourceType.TypeKind == EdmTypeKind.Untyped))
					{
						return null;
					}
				}
				if (scope.State == ODataWriterCore.WriterState.NestedResourceInfoWithContent)
				{
					return (ODataWriterCore.NestedResourceInfoScope)scope;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ParentNestedResourceInfoScope));
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00010A19 File Offset: 0x0000EC19
		private ResourceSetWithoutExpectedTypeValidator CurrentResourceSetValidator
		{
			get
			{
				if (this.scopeStack.Count != 3)
				{
					return null;
				}
				return this.resourceSetValidator;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00010A34 File Offset: 0x0000EC34
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

		// Token: 0x06000639 RID: 1593 RVA: 0x00010A6C File Offset: 0x0000EC6C
		public sealed override void WriteStart(ODataResourceSet resourceSet)
		{
			this.VerifyCanWriteStartResourceSet(true, resourceSet);
			this.WriteStartResourceSetImplementation(resourceSet);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00010A7D File Offset: 0x0000EC7D
		public sealed override void WriteStart(ODataResource resource)
		{
			this.VerifyCanWriteStartResource(true, resource);
			this.WriteStartResourceImplementation(resource);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00010A8E File Offset: 0x0000EC8E
		public sealed override void WritePrimitive(ODataPrimitiveValue primitiveValue)
		{
			this.VerifyCanWritePrimitive(true, primitiveValue);
			this.WritePrimitiveValueImplementation(primitiveValue);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00010A9F File Offset: 0x0000EC9F
		public sealed override void WriteStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.VerifyCanWriteStartNestedResourceInfo(true, nestedResourceInfo);
			this.WriteStartNestedResourceInfoImplementation(nestedResourceInfo);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00010AD3 File Offset: 0x0000ECD3
		public sealed override void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, true);
			this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00010AE4 File Offset: 0x0000ECE4
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

		// Token: 0x06000640 RID: 1600 RVA: 0x00010B48 File Offset: 0x0000ED48
		protected ODataWriterCore.ResourceScope GetParentResourceScope()
		{
			ODataWriterCore.ScopeStack scopeStack = new ODataWriterCore.ScopeStack();
			ODataWriterCore.Scope scope = null;
			if (this.scopeStack.Count > 0)
			{
				scopeStack.Push(this.scopeStack.Pop());
			}
			while (this.scopeStack.Count > 0)
			{
				ODataWriterCore.Scope scope2 = this.scopeStack.Pop();
				scopeStack.Push(scope2);
				if (scope2 is ODataWriterCore.ResourceScope)
				{
					scope = scope2;
					IL_006B:
					while (scopeStack.Count > 0)
					{
						ODataWriterCore.Scope scope3 = scopeStack.Pop();
						this.scopeStack.Push(scope3);
					}
					return scope as ODataWriterCore.ResourceScope;
				}
			}
			goto IL_006B;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00010BCF File Offset: 0x0000EDCF
		protected static bool IsErrorState(ODataWriterCore.WriterState state)
		{
			return state == ODataWriterCore.WriterState.Error;
		}

		// Token: 0x06000642 RID: 1602
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000643 RID: 1603
		protected abstract void FlushSynchronously();

		// Token: 0x06000644 RID: 1604
		protected abstract void StartPayload();

		// Token: 0x06000645 RID: 1605
		protected abstract void StartResource(ODataResource resource);

		// Token: 0x06000646 RID: 1606
		protected abstract void EndResource(ODataResource resource);

		// Token: 0x06000647 RID: 1607
		protected abstract void StartResourceSet(ODataResourceSet resourceSet);

		// Token: 0x06000648 RID: 1608
		protected abstract void EndPayload();

		// Token: 0x06000649 RID: 1609
		protected abstract void EndResourceSet(ODataResourceSet resourceSet);

		// Token: 0x0600064A RID: 1610 RVA: 0x0000FA90 File Offset: 0x0000DC90
		protected virtual void WritePrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600064B RID: 1611
		protected abstract void WriteDeferredNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x0600064C RID: 1612
		protected abstract void StartNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x0600064D RID: 1613
		protected abstract void EndNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x0600064E RID: 1614
		protected abstract void WriteEntityReferenceInNavigationLinkContent(ODataNestedResourceInfo parentNestedResourceInfo, ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x0600064F RID: 1615
		protected abstract ODataWriterCore.ResourceSetScope CreateResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared);

		// Token: 0x06000650 RID: 1616
		protected abstract ODataWriterCore.ResourceScope CreateResourceScope(ODataResource resource, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared);

		// Token: 0x06000651 RID: 1617 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		protected ODataResourceSerializationInfo GetResourceSerializationInfo(ODataResource resource)
		{
			ODataResourceSerializationInfo odataResourceSerializationInfo = ((resource == null) ? null : resource.SerializationInfo);
			if (odataResourceSerializationInfo != null)
			{
				return odataResourceSerializationInfo;
			}
			ODataWriterCore.ResourceSetScope resourceSetScope = this.CurrentScope as ODataWriterCore.ResourceSetScope;
			if (resourceSetScope != null)
			{
				ODataResourceSet odataResourceSet = (ODataResourceSet)resourceSetScope.Item;
				return odataResourceSet.SerializationInfo;
			}
			return null;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00010C1A File Offset: 0x0000EE1A
		protected virtual ODataWriterCore.NestedResourceInfoScope CreateNestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataWriterCore.NestedResourceInfoScope(writerState, navLink, navigationSource, resourceType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000250D File Offset: 0x0000070D
		protected virtual void PrepareResourceForWriteStart(ODataWriterCore.ResourceScope resourceScope, ODataResource resource, bool writingResponse, SelectedPropertiesNode selectedProperties)
		{
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00010C2C File Offset: 0x0000EE2C
		protected IEdmStructuredType GetResourceType(ODataResource resource)
		{
			return TypeNameOracle.ResolveAndValidateTypeFromTypeName(this.outputContext.Model, this.CurrentScope.ResourceType, resource.TypeName, this.WriterValidator);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00010C55 File Offset: 0x0000EE55
		protected IEdmStructuredType GetResourceSetType(ODataResourceSet resourceSet)
		{
			return TypeNameOracle.ResolveAndValidateTypeFromTypeName(this.outputContext.Model, this.CurrentScope.ResourceType, EdmLibraryExtensions.GetCollectionItemTypeName(resourceSet.TypeName), this.WriterValidator);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00010C83 File Offset: 0x0000EE83
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "An instance field is used in a debug assert.")]
		protected void ValidateNoDeltaLinkForExpandedResourceSet(ODataResourceSet resourceSet)
		{
			if (resourceSet.DeltaLink != null)
			{
				throw new ODataException(Strings.ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00010C9E File Offset: 0x0000EE9E
		private void VerifyCanWriteStartResourceSet(bool synchronousCall, ODataResourceSet resourceSet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceSet>(resourceSet, "resourceSet");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		private void WriteStartResourceSetImplementation(ODataResourceSet resourceSet)
		{
			this.CheckForNestedResourceInfoWithContent(ODataPayloadKind.ResourceSet, resourceSet);
			this.EnterScope(ODataWriterCore.WriterState.ResourceSet, resourceSet);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					if (resourceSet.Count != null && !this.outputContext.WritingResponse)
					{
						this.ThrowODataException(Strings.ODataWriterCore_QueryCountInRequest, resourceSet);
					}
					this.StartResourceSet(resourceSet);
				});
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00010D15 File Offset: 0x0000EF15
		private void VerifyCanWriteStartResource(bool synchronousCall, ODataResource resource)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00010D24 File Offset: 0x0000EF24
		private void WriteStartResourceImplementation(ODataResource resource)
		{
			this.StartPayloadInStartState();
			this.CheckForNestedResourceInfoWithContent(ODataPayloadKind.Resource, resource);
			this.EnterScope(ODataWriterCore.WriterState.Resource, resource);
			if (!this.SkipWriting)
			{
				this.IncreaseResourceDepth();
				this.InterceptException(delegate
				{
					if (resource != null)
					{
						ODataWriterCore.ResourceScope resourceScope = (ODataWriterCore.ResourceScope)this.CurrentScope;
						IEdmStructuredType resourceType = this.GetResourceType(resource);
						ODataWriterCore.NestedResourceInfoScope parentNestedResourceInfoScope = this.ParentNestedResourceInfoScope;
						if (parentNestedResourceInfoScope != null)
						{
							this.WriterValidator.ValidateResourceInNestedResourceInfo(resourceType, parentNestedResourceInfoScope.ResourceType);
							resourceScope.ResourceTypeFromMetadata = parentNestedResourceInfoScope.ResourceType;
						}
						else
						{
							resourceScope.ResourceTypeFromMetadata = this.ParentScope.ResourceType;
							if (this.CurrentResourceSetValidator != null)
							{
								this.CurrentResourceSetValidator.ValidateResource(resourceType);
							}
						}
						resourceScope.ResourceType = resourceType;
						this.PrepareResourceForWriteStart(resourceScope, resource, this.outputContext.WritingResponse, resourceScope.SelectedProperties);
					}
					this.StartResource(resource);
				});
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00010D85 File Offset: 0x0000EF85
		private void VerifyCanWriteStartNestedResourceInfo(bool synchronousCall, ODataNestedResourceInfo nestedResourceInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNestedResourceInfo>(nestedResourceInfo, "nestedResourceInfo");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		private void WriteStartNestedResourceInfoImplementation(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.EnterScope(ODataWriterCore.WriterState.NestedResourceInfo, nestedResourceInfo);
			ODataResource odataResource = (ODataResource)this.scopeStack.Parent.Item;
			if (odataResource.MetadataBuilder != null)
			{
				nestedResourceInfo.MetadataBuilder = odataResource.MetadataBuilder;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00010D15 File Offset: 0x0000EF15
		private void VerifyCanWritePrimitive(bool synchronousCall, ODataPrimitiveValue primitiveValue)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00010DDF File Offset: 0x0000EFDF
		private void WritePrimitiveValueImplementation(ODataPrimitiveValue primitiveValue)
		{
			this.EnterScope(ODataWriterCore.WriterState.Primitive, primitiveValue);
			this.WritePrimitiveValue(primitiveValue);
			this.WriteEnd();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00010D15 File Offset: 0x0000EF15
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00010DF6 File Offset: 0x0000EFF6
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
				case ODataWriterCore.WriterState.Resource:
					if (!this.SkipWriting)
					{
						ODataResource odataResource = (ODataResource)currentScope.Item;
						this.EndResource(odataResource);
						this.DecreaseResourceDepth();
					}
					break;
				case ODataWriterCore.WriterState.ResourceSet:
					if (!this.SkipWriting)
					{
						ODataResourceSet odataResourceSet = (ODataResourceSet)currentScope.Item;
						WriterValidationUtils.ValidateResourceSetAtEnd(odataResourceSet, !this.outputContext.WritingResponse);
						this.EndResourceSet(odataResourceSet);
					}
					break;
				case ODataWriterCore.WriterState.NestedResourceInfo:
					if (!this.outputContext.WritingResponse)
					{
						throw new ODataException(Strings.ODataWriterCore_DeferredLinkInRequest);
					}
					if (!this.SkipWriting)
					{
						ODataNestedResourceInfo odataNestedResourceInfo = (ODataNestedResourceInfo)currentScope.Item;
						this.DuplicatePropertyNameChecker.ValidatePropertyUniqueness(odataNestedResourceInfo);
						this.WriteDeferredNestedResourceInfo(odataNestedResourceInfo);
						this.MarkNestedResourceInfoAsProcessed(odataNestedResourceInfo);
					}
					break;
				case ODataWriterCore.WriterState.NestedResourceInfoWithContent:
					if (!this.SkipWriting)
					{
						ODataNestedResourceInfo odataNestedResourceInfo2 = (ODataNestedResourceInfo)currentScope.Item;
						this.EndNestedResourceInfoWithContent(odataNestedResourceInfo2);
						this.MarkNestedResourceInfoAsProcessed(odataNestedResourceInfo2);
					}
					break;
				case ODataWriterCore.WriterState.Primitive:
					break;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_WriteEnd_UnreachableCodePath));
				}
				this.LeaveScope();
			});
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00010E0C File Offset: 0x0000F00C
		private void MarkNestedResourceInfoAsProcessed(ODataNestedResourceInfo link)
		{
			ODataResource odataResource = (ODataResource)this.scopeStack.Parent.Item;
			odataResource.MetadataBuilder.MarkNestedResourceInfoProcessed(link.Name);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00010E40 File Offset: 0x0000F040
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool synchronousCall)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00010E5C File Offset: 0x0000F05C
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			if (this.outputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataWriterCore_EntityReferenceLinkInResponse, null);
			}
			this.CheckForNestedResourceInfoWithContent(ODataPayloadKind.EntityReferenceLink, null);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
					this.WriteEntityReferenceInNavigationLinkContent((ODataNestedResourceInfo)this.CurrentScope.Item, entityReferenceLink);
				});
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00010D15 File Offset: 0x0000EF15
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00010ED5 File Offset: 0x0000F0D5
		private void ThrowODataException(string errorMessage, ODataItem item)
		{
			this.EnterScope(ODataWriterCore.WriterState.Error, item);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00010EE5 File Offset: 0x0000F0E5
		private void StartPayloadInStartState()
		{
			if (this.State == ODataWriterCore.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00010F04 File Offset: 0x0000F104
		private void CheckForNestedResourceInfoWithContent(ODataPayloadKind contentPayloadKind, ODataItem contentPayload)
		{
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			if (currentScope.State == ODataWriterCore.WriterState.NestedResourceInfo || currentScope.State == ODataWriterCore.WriterState.NestedResourceInfoWithContent)
			{
				ODataNestedResourceInfo currentNestedResourceInfo = (ODataNestedResourceInfo)currentScope.Item;
				this.InterceptException(delegate
				{
					if (this.ParentResourceType != null)
					{
						IEdmStructuralProperty edmStructuralProperty = this.ParentResourceType.FindProperty(currentNestedResourceInfo.Name) as IEdmStructuralProperty;
						if (edmStructuralProperty != null)
						{
							this.CurrentScope.ResourceType = edmStructuralProperty.Type.ToStructuredType();
							IEdmNavigationSource parentResourceNavigationSource = this.ParentResourceNavigationSource;
							this.CurrentScope.NavigationSource = parentResourceNavigationSource;
							return;
						}
						IEdmNavigationProperty edmNavigationProperty = this.WriterValidator.ValidateNestedResourceInfo(currentNestedResourceInfo, this.ParentResourceType, new ODataPayloadKind?(contentPayloadKind));
						if (edmNavigationProperty != null)
						{
							this.CurrentScope.ResourceType = edmNavigationProperty.ToEntityType();
							IEdmNavigationSource parentResourceNavigationSource2 = this.ParentResourceNavigationSource;
							if (this.CurrentScope.NavigationSource == null)
							{
								IEdmPathExpression edmPathExpression;
								this.CurrentScope.NavigationSource = ((parentResourceNavigationSource2 == null) ? null : parentResourceNavigationSource2.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), Enumerable.ToList<ODataPathSegment>(this.CurrentScope.ODataUri.Path), out edmPathExpression));
							}
						}
					}
				});
				if (currentScope.State == ODataWriterCore.WriterState.NestedResourceInfoWithContent)
				{
					if (this.outputContext.WritingResponse || currentNestedResourceInfo.IsCollection != true)
					{
						this.ThrowODataException(Strings.ODataWriterCore_MultipleItemsInNestedResourceInfoWithContent, currentNestedResourceInfo);
						return;
					}
				}
				else
				{
					this.PromoteNestedResourceInfoScope(contentPayload);
					if (!this.SkipWriting)
					{
						this.InterceptException(delegate
						{
							if ((currentNestedResourceInfo.SerializationInfo == null || !currentNestedResourceInfo.SerializationInfo.IsComplex) && (this.CurrentScope.ResourceType == null || this.CurrentScope.ResourceType.IsEntityOrEntityCollectionType()))
							{
								this.DuplicatePropertyNameChecker.ValidatePropertyUniqueness(currentNestedResourceInfo);
								this.StartNestedResourceInfoWithContent(currentNestedResourceInfo);
							}
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

		// Token: 0x06000669 RID: 1641 RVA: 0x00010FFC File Offset: 0x0000F1FC
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

		// Token: 0x0600066A RID: 1642 RVA: 0x00011044 File Offset: 0x0000F244
		private void IncreaseResourceDepth()
		{
			this.currentResourceDepth++;
			if (this.currentResourceDepth > this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth)
			{
				this.ThrowODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth), null);
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000110A2 File Offset: 0x0000F2A2
		private void DecreaseResourceDepth()
		{
			this.currentResourceDepth--;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000110B2 File Offset: 0x0000F2B2
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

		// Token: 0x0600066D RID: 1645 RVA: 0x000110E0 File Offset: 0x0000F2E0
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
			IEdmStructuredType edmStructuredType = null;
			SelectedPropertiesNode selectedPropertiesNode = currentScope.SelectedProperties;
			ODataUri odataUri = currentScope.ODataUri.Clone();
			if (odataUri.Path == null)
			{
				odataUri.Path = new ODataPath(new ODataPathSegment[0]);
			}
			ODataWriterCore.WriterState state = currentScope.State;
			if (newState == ODataWriterCore.WriterState.Resource || newState == ODataWriterCore.WriterState.ResourceSet || newState == ODataWriterCore.WriterState.Primitive)
			{
				edmNavigationSource = currentScope.NavigationSource;
				edmStructuredType = currentScope.ResourceType;
				if (edmStructuredType == null && (state == ODataWriterCore.WriterState.Start || state == ODataWriterCore.WriterState.NestedResourceInfo || state == ODataWriterCore.WriterState.NestedResourceInfoWithContent) && newState == ODataWriterCore.WriterState.ResourceSet)
				{
					ODataResourceSet odataResourceSet = item as ODataResourceSet;
					if (odataResourceSet != null && odataResourceSet.TypeName != null && this.outputContext.Model.IsUserModel())
					{
						IEdmCollectionType edmCollectionType = TypeNameOracle.ResolveAndValidateTypeName(this.outputContext.Model, odataResourceSet.TypeName, EdmTypeKind.Collection, new bool?(false), this.outputContext.WriterValidator) as IEdmCollectionType;
						if (edmCollectionType != null)
						{
							edmStructuredType = edmCollectionType.ElementType.Definition as IEdmStructuredType;
						}
					}
				}
			}
			if (this.writingDelta)
			{
				flag = state == ODataWriterCore.WriterState.Start && newState == ODataWriterCore.WriterState.Resource;
			}
			if (state == ODataWriterCore.WriterState.Resource && newState == ODataWriterCore.WriterState.NestedResourceInfo)
			{
				ODataNestedResourceInfo odataNestedResourceInfo = (ODataNestedResourceInfo)item;
				if (!flag)
				{
					selectedPropertiesNode = currentScope.SelectedProperties.GetSelectedPropertiesForNavigationProperty(currentScope.ResourceType, odataNestedResourceInfo.Name);
					if (this.outputContext.WritingResponse)
					{
						ODataPath odataPath = odataUri.Path;
						IEdmStructuredType resourceType = currentScope.ResourceType;
						ODataWriterCore.ResourceScope resourceScope = currentScope as ODataWriterCore.ResourceScope;
						TypeSegment typeSegment = null;
						if (resourceScope.ResourceTypeFromMetadata != resourceType)
						{
							typeSegment = new TypeSegment(resourceType, null);
						}
						IEdmStructuralProperty edmStructuralProperty = this.WriterValidator.ValidatePropertyDefined(odataNestedResourceInfo.Name, resourceType) as IEdmStructuralProperty;
						if (edmStructuralProperty != null)
						{
							odataPath = this.AppendEntitySetKeySegment(odataPath, false);
							edmStructuredType = edmStructuralProperty.Type.ToStructuredType();
							edmNavigationSource = null;
							if (typeSegment != null)
							{
								odataPath.Add(typeSegment);
							}
							odataPath = odataPath.AppendPropertySegment(edmStructuralProperty);
						}
						else
						{
							IEdmNavigationProperty edmNavigationProperty = this.WriterValidator.ValidateNestedResourceInfo(odataNestedResourceInfo, resourceType, default(ODataPayloadKind?));
							if (edmNavigationProperty != null)
							{
								edmStructuredType = edmNavigationProperty.ToEntityType();
								IEdmNavigationSource navigationSource = currentScope.NavigationSource;
								if (typeSegment != null)
								{
									odataPath.Add(typeSegment);
								}
								IEdmPathExpression edmPathExpression;
								edmNavigationSource = ((navigationSource == null) ? null : navigationSource.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), Enumerable.ToList<ODataPathSegment>(odataPath), out edmPathExpression));
								SelectExpandClause selectAndExpand = odataUri.SelectAndExpand;
								TypeSegment typeSegment2 = null;
								if (selectAndExpand != null)
								{
									SelectExpandClause selectExpandClause;
									selectAndExpand.GetSubSelectExpandClause(odataNestedResourceInfo.Name, out selectExpandClause, out typeSegment2);
									odataUri.SelectAndExpand = selectExpandClause;
								}
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
									if (odataPath.Count == 0)
									{
										throw new ODataException(Strings.ODataWriterCore_PathInODataUriMustBeSetWhenWritingContainedElement);
									}
									odataPath = this.AppendEntitySetKeySegment(odataPath, true);
									if (odataPath != null && typeSegment2 != null)
									{
										odataPath.Add(typeSegment2);
									}
									IEdmContainedEntitySet edmContainedEntitySet = (IEdmContainedEntitySet)edmNavigationSource;
									odataPath = odataPath.AppendNavigationPropertySegment(edmContainedEntitySet.NavigationProperty, edmContainedEntitySet);
									break;
								}
								default:
									odataPath = null;
									break;
								}
							}
						}
						odataUri.Path = odataPath;
					}
				}
			}
			else if (state == ODataWriterCore.WriterState.ResourceSet && (newState == ODataWriterCore.WriterState.Resource || newState == ODataWriterCore.WriterState.Primitive || newState == ODataWriterCore.WriterState.ResourceSet))
			{
				ODataWriterCore.ResourceSetScope resourceSetScope = (ODataWriterCore.ResourceSetScope)currentScope;
				int resourceCount = resourceSetScope.ResourceCount;
				resourceSetScope.ResourceCount = resourceCount + 1;
			}
			if (edmNavigationSource == null)
			{
				edmNavigationSource = this.CurrentScope.NavigationSource ?? odataUri.Path.TargetNavigationSource();
			}
			this.PushScope(newState, item, edmNavigationSource, edmStructuredType, flag, selectedPropertiesNode, odataUri);
			this.NotifyListener(newState);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000114C4 File Offset: 0x0000F6C4
		private ODataPath AppendEntitySetKeySegment(ODataPath odataPath, bool throwIfFail)
		{
			ODataPath odataPath2 = odataPath;
			try
			{
				if (EdmExtensionMethods.HasKey(this.CurrentScope.NavigationSource, this.CurrentScope.ResourceType))
				{
					IEdmEntityType edmEntityType = this.CurrentScope.ResourceType as IEdmEntityType;
					ODataResource odataResource = this.CurrentScope.Item as ODataResource;
					KeyValuePair<string, object>[] keyProperties = ODataResourceMetadataContext.GetKeyProperties(odataResource, this.GetResourceSerializationInfo(odataResource), edmEntityType);
					odataPath2 = odataPath2.AppendKeySegment(keyProperties, edmEntityType, this.CurrentScope.NavigationSource);
				}
			}
			catch (ODataException)
			{
				if (throwIfFail)
				{
					throw;
				}
			}
			return odataPath2;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00011550 File Offset: 0x0000F750
		private void LeaveScope()
		{
			this.scopeStack.Pop();
			if (this.scopeStack.Count == 1)
			{
				ODataWriterCore.Scope scope = this.scopeStack.Pop();
				this.PushScope(ODataWriterCore.WriterState.Completed, null, scope.NavigationSource, scope.ResourceType, false, scope.SelectedProperties, scope.ODataUri);
				this.InterceptException(new Action(this.EndPayload));
				this.NotifyListener(ODataWriterCore.WriterState.Completed);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000115C0 File Offset: 0x0000F7C0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Second cast only in debug.")]
		private void PromoteNestedResourceInfoScope(ODataItem content)
		{
			this.ValidateTransition(ODataWriterCore.WriterState.NestedResourceInfoWithContent);
			ODataWriterCore.NestedResourceInfoScope nestedResourceInfoScope = (ODataWriterCore.NestedResourceInfoScope)this.scopeStack.Pop();
			ODataWriterCore.NestedResourceInfoScope nestedResourceInfoScope2 = nestedResourceInfoScope.Clone(ODataWriterCore.WriterState.NestedResourceInfoWithContent);
			this.scopeStack.Push(nestedResourceInfoScope2);
			if (nestedResourceInfoScope2.ResourceType == null && content != null && !this.SkipWriting)
			{
				ODataResource odataResource = content as ODataResource;
				nestedResourceInfoScope2.ResourceType = ((odataResource != null) ? this.GetResourceType(odataResource) : this.GetResourceSetType(content as ODataResourceSet));
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00011634 File Offset: 0x0000F834
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
				if (newState != ODataWriterCore.WriterState.ResourceSet && newState != ODataWriterCore.WriterState.Resource)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				if (newState == ODataWriterCore.WriterState.ResourceSet && !this.writingResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter);
				}
				if (newState == ODataWriterCore.WriterState.Resource && this.writingResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter);
				}
				return;
			case ODataWriterCore.WriterState.Resource:
				if (this.CurrentScope.Item == null)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromNullResource(this.State.ToString(), newState.ToString()));
				}
				if (newState != ODataWriterCore.WriterState.NestedResourceInfo)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromResource(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.ResourceSet:
				if (newState != ODataWriterCore.WriterState.Resource && (this.CurrentScope.ResourceType == null || this.CurrentScope.ResourceType.TypeKind != EdmTypeKind.Untyped || (newState != ODataWriterCore.WriterState.Primitive && newState != ODataWriterCore.WriterState.ResourceSet)))
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromResourceSet(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.NestedResourceInfo:
				if (newState != ODataWriterCore.WriterState.NestedResourceInfoWithContent)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidStateTransition(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.NestedResourceInfoWithContent:
				if (newState != ODataWriterCore.WriterState.ResourceSet && newState != ODataWriterCore.WriterState.Resource)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromExpandedLink(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataWriterCore.WriterState.Error:
				if (newState != ODataWriterCore.WriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				return;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ValidateTransition_UnreachableCodePath));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00011898 File Offset: 0x0000FA98
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
		private void PushScope(ODataWriterCore.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			bool flag = false;
			if ((state == ODataWriterCore.WriterState.Resource || state == ODataWriterCore.WriterState.ResourceSet) && (this.CurrentScope.State == ODataWriterCore.WriterState.NestedResourceInfo || this.CurrentScope.State == ODataWriterCore.WriterState.NestedResourceInfoWithContent))
			{
				flag = this.IsUndeclared(this.CurrentScope.Item as ODataNestedResourceInfo);
			}
			ODataWriterCore.Scope scope;
			switch (state)
			{
			case ODataWriterCore.WriterState.Start:
			case ODataWriterCore.WriterState.Primitive:
			case ODataWriterCore.WriterState.Completed:
			case ODataWriterCore.WriterState.Error:
				scope = new ODataWriterCore.Scope(state, item, navigationSource, resourceType, skipWriting, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.Resource:
				scope = this.CreateResourceScope((ODataResource)item, navigationSource, resourceType, skipWriting, selectedProperties, odataUri, flag);
				break;
			case ODataWriterCore.WriterState.ResourceSet:
				scope = this.CreateResourceSetScope((ODataResourceSet)item, navigationSource, resourceType, skipWriting, selectedProperties, odataUri, flag);
				break;
			case ODataWriterCore.WriterState.NestedResourceInfo:
			case ODataWriterCore.WriterState.NestedResourceInfoWithContent:
				scope = this.CreateNestedResourceInfoScope(state, (ODataNestedResourceInfo)item, navigationSource, resourceType, skipWriting, selectedProperties, odataUri);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			this.scopeStack.Push(scope);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00011988 File Offset: 0x0000FB88
		private bool IsUndeclared(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (nestedResourceInfo.SerializationInfo != null)
			{
				return nestedResourceInfo.SerializationInfo.IsUndeclared;
			}
			return this.ParentResourceType != null && this.ParentResourceType.FindProperty((this.CurrentScope.Item as ODataNestedResourceInfo).Name) == null;
		}

		// Token: 0x040002D4 RID: 724
		protected readonly IWriterValidator WriterValidator;

		// Token: 0x040002D5 RID: 725
		private readonly ODataOutputContext outputContext;

		// Token: 0x040002D6 RID: 726
		private readonly bool writingResourceSet;

		// Token: 0x040002D7 RID: 727
		private readonly bool writingDelta;

		// Token: 0x040002D8 RID: 728
		private readonly IODataReaderWriterListener listener;

		// Token: 0x040002D9 RID: 729
		private readonly ODataWriterCore.ScopeStack scopeStack = new ODataWriterCore.ScopeStack();

		// Token: 0x040002DA RID: 730
		private readonly ResourceSetWithoutExpectedTypeValidator resourceSetValidator;

		// Token: 0x040002DB RID: 731
		private int currentResourceDepth;

		// Token: 0x02000289 RID: 649
		internal enum WriterState
		{
			// Token: 0x04000B61 RID: 2913
			Start,
			// Token: 0x04000B62 RID: 2914
			Resource,
			// Token: 0x04000B63 RID: 2915
			ResourceSet,
			// Token: 0x04000B64 RID: 2916
			NestedResourceInfo,
			// Token: 0x04000B65 RID: 2917
			NestedResourceInfoWithContent,
			// Token: 0x04000B66 RID: 2918
			Primitive,
			// Token: 0x04000B67 RID: 2919
			Completed,
			// Token: 0x04000B68 RID: 2920
			Error
		}

		// Token: 0x0200028A RID: 650
		internal sealed class ScopeStack
		{
			// Token: 0x060017F4 RID: 6132 RVA: 0x00047B69 File Offset: 0x00045D69
			internal ScopeStack()
			{
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00047B7C File Offset: 0x00045D7C
			internal int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00047B8C File Offset: 0x00045D8C
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

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00047BC0 File Offset: 0x00045DC0
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

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00047C0A File Offset: 0x00045E0A
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

			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00047C1C File Offset: 0x00045E1C
			internal Stack<ODataWriterCore.Scope> Scopes
			{
				get
				{
					return this.scopes;
				}
			}

			// Token: 0x060017FA RID: 6138 RVA: 0x00047C24 File Offset: 0x00045E24
			internal void Push(ODataWriterCore.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x060017FB RID: 6139 RVA: 0x00047C32 File Offset: 0x00045E32
			internal ODataWriterCore.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x060017FC RID: 6140 RVA: 0x00047C3F File Offset: 0x00045E3F
			internal ODataWriterCore.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x04000B69 RID: 2921
			private readonly Stack<ODataWriterCore.Scope> scopes = new Stack<ODataWriterCore.Scope>();
		}

		// Token: 0x0200028B RID: 651
		internal class Scope
		{
			// Token: 0x060017FD RID: 6141 RVA: 0x00047C4C File Offset: 0x00045E4C
			internal Scope(ODataWriterCore.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.resourceType = resourceType;
				this.navigationSource = navigationSource;
				this.skipWriting = skipWriting;
				this.selectedProperties = selectedProperties;
				this.odataUri = odataUri;
			}

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x060017FE RID: 6142 RVA: 0x00047C89 File Offset: 0x00045E89
			// (set) Token: 0x060017FF RID: 6143 RVA: 0x00047C91 File Offset: 0x00045E91
			public IEdmStructuredType ResourceType
			{
				get
				{
					return this.resourceType;
				}
				set
				{
					this.resourceType = value;
				}
			}

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x06001800 RID: 6144 RVA: 0x00047C9A File Offset: 0x00045E9A
			internal ODataWriterCore.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x06001801 RID: 6145 RVA: 0x00047CA2 File Offset: 0x00045EA2
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x06001802 RID: 6146 RVA: 0x00047CAA File Offset: 0x00045EAA
			// (set) Token: 0x06001803 RID: 6147 RVA: 0x00047CB2 File Offset: 0x00045EB2
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

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x06001804 RID: 6148 RVA: 0x00047CBB File Offset: 0x00045EBB
			internal SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x06001805 RID: 6149 RVA: 0x00047CC3 File Offset: 0x00045EC3
			internal ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x06001806 RID: 6150 RVA: 0x00047CCB File Offset: 0x00045ECB
			internal bool SkipWriting
			{
				get
				{
					return this.skipWriting;
				}
			}

			// Token: 0x04000B6A RID: 2922
			private readonly ODataWriterCore.WriterState state;

			// Token: 0x04000B6B RID: 2923
			private readonly ODataItem item;

			// Token: 0x04000B6C RID: 2924
			private readonly bool skipWriting;

			// Token: 0x04000B6D RID: 2925
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x04000B6E RID: 2926
			private IEdmNavigationSource navigationSource;

			// Token: 0x04000B6F RID: 2927
			private IEdmStructuredType resourceType;

			// Token: 0x04000B70 RID: 2928
			private ODataUri odataUri;
		}

		// Token: 0x0200028C RID: 652
		internal abstract class ResourceSetScope : ODataWriterCore.Scope
		{
			// Token: 0x06001807 RID: 6151 RVA: 0x00047CD3 File Offset: 0x00045ED3
			internal ResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.ResourceSet, resourceSet, navigationSource, resourceType, skipWriting, selectedProperties, odataUri)
			{
				this.serializationInfo = resourceSet.SerializationInfo;
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x06001808 RID: 6152 RVA: 0x00047CF1 File Offset: 0x00045EF1
			// (set) Token: 0x06001809 RID: 6153 RVA: 0x00047CF9 File Offset: 0x00045EF9
			internal int ResourceCount
			{
				get
				{
					return this.resourceCount;
				}
				set
				{
					this.resourceCount = value;
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x0600180A RID: 6154 RVA: 0x00047D02 File Offset: 0x00045F02
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

			// Token: 0x0600180B RID: 6155 RVA: 0x00047D20 File Offset: 0x00045F20
			internal ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse)
			{
				if (this.typeContext == null)
				{
					bool flag = writingResponse && (base.ResourceType == null || base.ResourceType.TypeKind == EdmTypeKind.Entity);
					this.typeContext = ODataResourceTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), base.ResourceType, flag);
				}
				return this.typeContext;
			}

			// Token: 0x04000B71 RID: 2929
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000B72 RID: 2930
			private int resourceCount;

			// Token: 0x04000B73 RID: 2931
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x04000B74 RID: 2932
			private ODataResourceTypeContext typeContext;
		}

		// Token: 0x0200028D RID: 653
		internal class ResourceScope : ODataWriterCore.Scope
		{
			// Token: 0x0600180C RID: 6156 RVA: 0x00047D89 File Offset: 0x00045F89
			internal ResourceScope(ODataResource resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.Resource, resource, navigationSource, resourceType, skipWriting, selectedProperties, odataUri)
			{
				if (resource != null)
				{
					this.duplicatePropertyNameChecker = writerSettings.Validator.CreateDuplicatePropertyNameChecker();
				}
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x0600180D RID: 6157 RVA: 0x00047DB8 File Offset: 0x00045FB8
			// (set) Token: 0x0600180E RID: 6158 RVA: 0x00047DC0 File Offset: 0x00045FC0
			public IEdmStructuredType ResourceTypeFromMetadata
			{
				get
				{
					return this.resourceTypeFromMetadata;
				}
				internal set
				{
					this.resourceTypeFromMetadata = value;
				}
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x0600180F RID: 6159 RVA: 0x00047DC9 File Offset: 0x00045FC9
			public ODataResourceSerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x06001810 RID: 6160 RVA: 0x00047DD1 File Offset: 0x00045FD1
			internal IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
			{
				get
				{
					return this.duplicatePropertyNameChecker;
				}
			}

			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x06001811 RID: 6161 RVA: 0x00047DD9 File Offset: 0x00045FD9
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

			// Token: 0x06001812 RID: 6162 RVA: 0x00047DF4 File Offset: 0x00045FF4
			public ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse)
			{
				if (this.typeContext == null)
				{
					IEdmStructuredType edmStructuredType = this.ResourceTypeFromMetadata ?? base.ResourceType;
					bool flag = writingResponse && (edmStructuredType == null || edmStructuredType.TypeKind == EdmTypeKind.Entity);
					this.typeContext = ODataResourceTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), edmStructuredType, flag);
				}
				return this.typeContext;
			}

			// Token: 0x04000B75 RID: 2933
			private readonly IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

			// Token: 0x04000B76 RID: 2934
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000B77 RID: 2935
			private IEdmStructuredType resourceTypeFromMetadata;

			// Token: 0x04000B78 RID: 2936
			private ODataResourceTypeContext typeContext;

			// Token: 0x04000B79 RID: 2937
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x0200028E RID: 654
		internal class NestedResourceInfoScope : ODataWriterCore.Scope
		{
			// Token: 0x06001813 RID: 6163 RVA: 0x00047E5F File Offset: 0x0004605F
			internal NestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, navLink, navigationSource, resourceType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x06001814 RID: 6164 RVA: 0x00047E72 File Offset: 0x00046072
			internal virtual ODataWriterCore.NestedResourceInfoScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataWriterCore.NestedResourceInfoScope(newWriterState, (ODataNestedResourceInfo)base.Item, base.NavigationSource, base.ResourceType, base.SkipWriting, base.SelectedProperties, base.ODataUri);
			}
		}
	}
}
