using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000C3 RID: 195
	internal abstract class ODataWriterCore : ODataWriter, IODataOutputInStreamErrorListener, IODataStreamListener
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x000141A8 File Offset: 0x000123A8
		protected ODataWriterCore(ODataOutputContext outputContext, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool writingResourceSet, bool writingDelta = false, IODataReaderWriterListener listener = null)
		{
			this.outputContext = outputContext;
			this.writingResourceSet = writingResourceSet;
			this.writingDelta = writingDelta;
			this.WriterValidator = outputContext.WriterValidator;
			this.Version = outputContext.MessageWriterSettings.Version;
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
			this.CurrentScope.DerivedTypeConstraints = this.outputContext.Model.GetDerivedTypeConstraints(navigationSource);
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00014289 File Offset: 0x00012489
		internal ODataVersion? Version { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00014291 File Offset: 0x00012491
		protected ODataWriterCore.Scope CurrentScope
		{
			get
			{
				return this.scopeStack.Peek();
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001429E File Offset: 0x0001249E
		protected ODataWriterCore.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000142AB File Offset: 0x000124AB
		protected bool SkipWriting
		{
			get
			{
				return this.CurrentScope.SkipWriting;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000142B8 File Offset: 0x000124B8
		protected bool IsTopLevel
		{
			get
			{
				return this.scopeStack.Count == 2;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000142C8 File Offset: 0x000124C8
		protected int ScopeLevel
		{
			get
			{
				return this.scopeStack.Count;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000142D8 File Offset: 0x000124D8
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00014304 File Offset: 0x00012504
		protected ODataNestedResourceInfo BelongingNestedResourceInfo
		{
			get
			{
				ODataWriterCore.Scope scope = this.scopeStack.ParentOrNull;
				if (scope is ODataWriterCore.NestedResourceInfoScope)
				{
					return scope.Item as ODataNestedResourceInfo;
				}
				if (!(scope is ODataWriterCore.ResourceSetBaseScope))
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00014358 File Offset: 0x00012558
		protected IEdmStructuredType ParentResourceType
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopeStack.Parent;
				return parent.ResourceType;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00014378 File Offset: 0x00012578
		protected IEdmNavigationSource ParentResourceNavigationSource
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopeStack.Parent;
				if (parent != null)
				{
					return parent.NavigationSource;
				}
				return null;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001439C File Offset: 0x0001259C
		protected ODataWriterCore.Scope ParentScope
		{
			get
			{
				return this.scopeStack.Scopes.Skip(1).First<ODataWriterCore.Scope>();
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000143B4 File Offset: 0x000125B4
		protected int ResourceSetScopeResourceCount
		{
			get
			{
				return ((ODataWriterCore.ResourceSetBaseScope)this.CurrentScope).ResourceCount;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000143C8 File Offset: 0x000125C8
		protected IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
		{
			get
			{
				ODataWriterCore.WriterState state = this.State;
				ODataWriterCore.ResourceBaseScope resourceBaseScope;
				if (state != ODataWriterCore.WriterState.Resource)
				{
					switch (state)
					{
					case ODataWriterCore.WriterState.DeletedResource:
						goto IL_0031;
					case ODataWriterCore.WriterState.NestedResourceInfo:
					case ODataWriterCore.WriterState.NestedResourceInfoWithContent:
					case ODataWriterCore.WriterState.Property:
						resourceBaseScope = (ODataWriterCore.ResourceBaseScope)this.scopeStack.Parent;
						goto IL_0063;
					}
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_PropertyAndAnnotationCollector));
				}
				IL_0031:
				resourceBaseScope = (ODataWriterCore.ResourceBaseScope)this.CurrentScope;
				IL_0063:
				return resourceBaseScope.DuplicatePropertyNameChecker;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001443E File Offset: 0x0001263E
		protected IEdmStructuredType ResourceType
		{
			get
			{
				return this.CurrentScope.ResourceType;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001444C File Offset: 0x0001264C
		protected ODataWriterCore.NestedResourceInfoScope ParentNestedResourceInfoScope
		{
			get
			{
				ODataWriterCore.Scope scope = this.scopeStack.Parent;
				if (scope.State == ODataWriterCore.WriterState.Start)
				{
					return null;
				}
				if (scope.State == ODataWriterCore.WriterState.ResourceSet || scope.State == ODataWriterCore.WriterState.DeltaResourceSet)
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

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x000144D8 File Offset: 0x000126D8
		private ResourceSetWithoutExpectedTypeValidator CurrentResourceSetValidator
		{
			get
			{
				ODataWriterCore.ResourceSetBaseScope resourceSetBaseScope = this.ParentScope as ODataWriterCore.ResourceSetBaseScope;
				if (resourceSetBaseScope != null)
				{
					return resourceSetBaseScope.ResourceTypeValidator;
				}
				return null;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000144FC File Offset: 0x000126FC
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

		// Token: 0x060008B1 RID: 2225 RVA: 0x00014534 File Offset: 0x00012734
		public sealed override Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.EnterScope(ODataWriterCore.WriterState.Error, null);
			});
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00014554 File Offset: 0x00012754
		public sealed override void WriteStart(ODataResourceSet resourceSet)
		{
			this.VerifyCanWriteStartResourceSet(true, resourceSet);
			this.WriteStartResourceSetImplementation(resourceSet);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00014568 File Offset: 0x00012768
		public sealed override Task WriteStartAsync(ODataResourceSet resourceSet)
		{
			this.VerifyCanWriteStartResourceSet(false, resourceSet);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartResourceSetImplementation(resourceSet);
			});
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000145A7 File Offset: 0x000127A7
		public sealed override void WriteStart(ODataDeltaResourceSet deltaResourceSet)
		{
			this.VerifyCanWriteStartDeltaResourceSet(true, deltaResourceSet);
			this.WriteStartDeltaResourceSetImplementation(deltaResourceSet);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000145B8 File Offset: 0x000127B8
		public sealed override Task WriteStartAsync(ODataDeltaResourceSet deltaResourceSet)
		{
			this.VerifyCanWriteStartDeltaResourceSet(false, deltaResourceSet);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartDeltaResourceSetImplementation(deltaResourceSet);
			});
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000145F7 File Offset: 0x000127F7
		public sealed override void WriteStart(ODataResource resource)
		{
			this.VerifyCanWriteStartResource(true, resource);
			this.WriteStartResourceImplementation(resource);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00014608 File Offset: 0x00012808
		public sealed override Task WriteStartAsync(ODataResource resource)
		{
			this.VerifyCanWriteStartResource(false, resource);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartResourceImplementation(resource);
			});
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00014647 File Offset: 0x00012847
		public sealed override void WriteStart(ODataDeletedResource deletedResource)
		{
			this.VerifyCanWriteStartDeletedResource(true, deletedResource);
			this.WriteStartDeletedResourceImplementation(deletedResource);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00014658 File Offset: 0x00012858
		public sealed override Task WriteStartAsync(ODataDeletedResource deletedResource)
		{
			this.VerifyCanWriteStartDeletedResource(false, deletedResource);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartDeletedResourceImplementation(deletedResource);
			});
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00014697 File Offset: 0x00012897
		public override void WriteDeltaLink(ODataDeltaLink deltaLink)
		{
			this.VerifyCanWriteLink(true, deltaLink);
			this.WriteDeltaLinkImplementation(deltaLink);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000146A8 File Offset: 0x000128A8
		public override Task WriteDeltaLinkAsync(ODataDeltaLink deltaLink)
		{
			this.VerifyCanWriteLink(false, deltaLink);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteDeltaLinkImplementation(deltaLink);
			});
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00014697 File Offset: 0x00012897
		public override void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaLink)
		{
			this.VerifyCanWriteLink(true, deltaLink);
			this.WriteDeltaLinkImplementation(deltaLink);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000146E8 File Offset: 0x000128E8
		public override Task WriteDeltaDeletedLinkAsync(ODataDeltaDeletedLink deltaLink)
		{
			this.VerifyCanWriteLink(false, deltaLink);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteDeltaLinkImplementation(deltaLink);
			});
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00014727 File Offset: 0x00012927
		public sealed override void WritePrimitive(ODataPrimitiveValue primitiveValue)
		{
			this.VerifyCanWritePrimitive(true, primitiveValue);
			this.WritePrimitiveValueImplementation(primitiveValue);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00014738 File Offset: 0x00012938
		public sealed override Task WritePrimitiveAsync(ODataPrimitiveValue primitiveValue)
		{
			this.VerifyCanWritePrimitive(false, primitiveValue);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WritePrimitiveValueImplementation(primitiveValue);
			});
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00014777 File Offset: 0x00012977
		public sealed override void WriteStart(ODataPropertyInfo primitiveProperty)
		{
			this.VerifyCanWriteProperty(true, primitiveProperty);
			this.WriteStartPropertyImplementation(primitiveProperty);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00014788 File Offset: 0x00012988
		public sealed override Task WriteStartAsync(ODataProperty primitiveProperty)
		{
			this.VerifyCanWriteProperty(false, primitiveProperty);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartPropertyImplementation(primitiveProperty);
			});
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000147C7 File Offset: 0x000129C7
		public sealed override Stream CreateBinaryWriteStream()
		{
			this.VerifyCanCreateWriteStream(true);
			return this.CreateWriteStreamImplementation();
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000147D6 File Offset: 0x000129D6
		public sealed override Task<Stream> CreateBinaryWriteStreamAsync()
		{
			this.VerifyCanCreateWriteStream(false);
			return TaskUtils.GetTaskForSynchronousOperation<Stream>(() => this.CreateWriteStreamImplementation());
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000147F0 File Offset: 0x000129F0
		public sealed override TextWriter CreateTextWriter()
		{
			this.VerifyCanCreateTextWriter(true);
			return this.CreateTextWriterImplementation();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000147FF File Offset: 0x000129FF
		public sealed override Task<TextWriter> CreateTextWriterAsync()
		{
			this.VerifyCanCreateWriteStream(false);
			return TaskUtils.GetTaskForSynchronousOperation<TextWriter>(() => this.CreateTextWriterImplementation());
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00014819 File Offset: 0x00012A19
		public sealed override void WriteStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.VerifyCanWriteStartNestedResourceInfo(true, nestedResourceInfo);
			this.WriteStartNestedResourceInfoImplementation(nestedResourceInfo);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001482C File Offset: 0x00012A2C
		public sealed override Task WriteStartAsync(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.VerifyCanWriteStartNestedResourceInfo(false, nestedResourceInfo);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartNestedResourceInfoImplementation(nestedResourceInfo);
			});
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001486B File Offset: 0x00012A6B
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001488F File Offset: 0x00012A8F
		public sealed override Task WriteEndAsync()
		{
			this.VerifyCanWriteEnd(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndImplementation)).FollowOnSuccessWithTask(delegate(Task task)
			{
				if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
				{
					return this.FlushAsync();
				}
				return TaskUtils.CompletedTask;
			});
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000148BA File Offset: 0x00012ABA
		public sealed override void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, true);
			this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000148CC File Offset: 0x00012ACC
		public sealed override Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
			});
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001490C File Offset: 0x00012B0C
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

		// Token: 0x060008CD RID: 2253 RVA: 0x0000239D File Offset: 0x0000059D
		void IODataStreamListener.StreamRequested()
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00014971 File Offset: 0x00012B71
		Task IODataStreamListener.StreamRequestedAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				((IODataStreamListener)this).StreamRequested();
			});
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00014984 File Offset: 0x00012B84
		void IODataStreamListener.StreamDisposed()
		{
			if (this.State == ODataWriterCore.WriterState.Stream)
			{
				this.EndBinaryStream();
			}
			else if (this.State == ODataWriterCore.WriterState.String)
			{
				this.EndTextWriter();
			}
			this.LeaveScope();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000149B0 File Offset: 0x00012BB0
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

		// Token: 0x060008D1 RID: 2257 RVA: 0x00014A37 File Offset: 0x00012C37
		protected static bool IsErrorState(ODataWriterCore.WriterState state)
		{
			return state == ODataWriterCore.WriterState.Error;
		}

		// Token: 0x060008D2 RID: 2258
		protected abstract void VerifyNotDisposed();

		// Token: 0x060008D3 RID: 2259
		protected abstract void FlushSynchronously();

		// Token: 0x060008D4 RID: 2260
		protected abstract Task FlushAsynchronously();

		// Token: 0x060008D5 RID: 2261
		protected abstract void StartPayload();

		// Token: 0x060008D6 RID: 2262
		protected abstract void StartResource(ODataResource resource);

		// Token: 0x060008D7 RID: 2263
		protected abstract void EndResource(ODataResource resource);

		// Token: 0x060008D8 RID: 2264 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void StartProperty(ODataPropertyInfo property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void EndProperty(ODataPropertyInfo property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008DA RID: 2266
		protected abstract void StartResourceSet(ODataResourceSet resourceSet);

		// Token: 0x060008DB RID: 2267 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void StartDeltaResourceSet(ODataDeltaResourceSet deltaResourceSet)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void StartDeletedResource(ODataDeletedResource deletedEntry)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void StartDeltaLink(ODataDeltaLinkBase deltaLink)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual Stream StartBinaryStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void EndBinaryStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual TextWriter StartTextWriter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void EndTextWriter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E2 RID: 2274
		protected abstract void EndPayload();

		// Token: 0x060008E3 RID: 2275
		protected abstract void EndResourceSet(ODataResourceSet resourceSet);

		// Token: 0x060008E4 RID: 2276 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void EndDeltaResourceSet(ODataDeltaResourceSet deltaResourceSet)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void EndDeletedResource(ODataDeletedResource deletedResource)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void WritePrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E7 RID: 2279
		protected abstract void WriteDeferredNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x060008E8 RID: 2280
		protected abstract void StartNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x060008E9 RID: 2281
		protected abstract void EndNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x060008EA RID: 2282
		protected abstract void WriteEntityReferenceInNavigationLinkContent(ODataNestedResourceInfo parentNestedResourceInfo, ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x060008EB RID: 2283
		protected abstract ODataWriterCore.ResourceSetScope CreateResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared);

		// Token: 0x060008EC RID: 2284 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual ODataWriterCore.DeltaResourceSetScope CreateDeltaResourceSetScope(ODataDeltaResourceSet deltaResourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008ED RID: 2285
		protected abstract ODataWriterCore.ResourceScope CreateResourceScope(ODataResource resource, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared);

		// Token: 0x060008EE RID: 2286 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual ODataWriterCore.DeletedResourceScope CreateDeletedResourceScope(ODataDeletedResource resource, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual ODataWriterCore.PropertyInfoScope CreatePropertyInfoScope(ODataPropertyInfo property, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual ODataWriterCore.DeltaLinkScope CreateDeltaLinkScope(ODataDeltaLinkBase link, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00014A40 File Offset: 0x00012C40
		protected ODataResourceSerializationInfo GetResourceSerializationInfo(ODataResourceBase resource)
		{
			ODataResourceSerializationInfo odataResourceSerializationInfo = ((resource == null) ? null : resource.SerializationInfo);
			if (odataResourceSerializationInfo != null)
			{
				return odataResourceSerializationInfo;
			}
			ODataResourceSetBase odataResourceSetBase = this.CurrentScope.Item as ODataResourceSetBase;
			if (odataResourceSetBase != null)
			{
				return odataResourceSetBase.SerializationInfo;
			}
			return null;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00014A7C File Offset: 0x00012C7C
		protected ODataResourceSerializationInfo GetLinkSerializationInfo(ODataItem item)
		{
			ODataDeltaSerializationInfo odataDeltaSerializationInfo = null;
			ODataResourceSerializationInfo odataResourceSerializationInfo = null;
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
			if (odataDeltaSerializationInfo == null)
			{
				ODataWriterCore.DeltaResourceSetScope deltaResourceSetScope = this.CurrentScope as ODataWriterCore.DeltaResourceSetScope;
				if (deltaResourceSetScope != null)
				{
					ODataDeltaResourceSet odataDeltaResourceSet = (ODataDeltaResourceSet)deltaResourceSetScope.Item;
					ODataResourceSerializationInfo serializationInfo = odataDeltaResourceSet.SerializationInfo;
					if (serializationInfo != null)
					{
						odataResourceSerializationInfo = serializationInfo;
					}
				}
			}
			else
			{
				odataResourceSerializationInfo = new ODataResourceSerializationInfo
				{
					NavigationSourceName = odataDeltaSerializationInfo.NavigationSourceName
				};
			}
			return odataResourceSerializationInfo;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00014AF6 File Offset: 0x00012CF6
		protected virtual ODataWriterCore.NestedResourceInfoScope CreateNestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataWriterCore.NestedResourceInfoScope(writerState, navLink, navigationSource, itemType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000239D File Offset: 0x0000059D
		protected virtual void PrepareResourceForWriteStart(ODataWriterCore.ResourceScope resourceScope, ODataResource resource, bool writingResponse, SelectedPropertiesNode selectedProperties)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000239D File Offset: 0x0000059D
		protected virtual void PrepareDeletedResourceForWriteStart(ODataWriterCore.DeletedResourceScope resourceScope, ODataDeletedResource deletedResource, bool writingResponse, SelectedPropertiesNode selectedProperties)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00014B08 File Offset: 0x00012D08
		protected IEdmStructuredType GetResourceType(ODataResourceBase resource)
		{
			return TypeNameOracle.ResolveAndValidateTypeFromTypeName(this.outputContext.Model, this.CurrentScope.ResourceType, resource.TypeName, this.WriterValidator);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00014B31 File Offset: 0x00012D31
		protected IEdmStructuredType GetResourceSetType(ODataResourceSetBase resourceSet)
		{
			return TypeNameOracle.ResolveAndValidateTypeFromTypeName(this.outputContext.Model, this.CurrentScope.ResourceType, EdmLibraryExtensions.GetCollectionItemTypeName(resourceSet.TypeName), this.WriterValidator);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00014B5F File Offset: 0x00012D5F
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "An instance field is used in a debug assert.")]
		protected void ValidateNoDeltaLinkForExpandedResourceSet(ODataResourceSet resourceSet)
		{
			if (resourceSet.DeltaLink != null)
			{
				throw new ODataException(Strings.ODataWriterCore_DeltaLinkNotSupportedOnExpandedResourceSet);
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00014B7A File Offset: 0x00012D7A
		private void VerifyCanWriteStartResourceSet(bool synchronousCall, ODataResourceSet resourceSet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceSet>(resourceSet, "resourceSet");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00014B9C File Offset: 0x00012D9C
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

		// Token: 0x060008FB RID: 2299 RVA: 0x00014BF1 File Offset: 0x00012DF1
		private void VerifyCanWriteStartDeltaResourceSet(bool synchronousCall, ODataDeltaResourceSet deltaResourceSet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaResourceSet>(deltaResourceSet, "deltaResourceSet");
			this.VerifyWritingDelta();
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00014C18 File Offset: 0x00012E18
		private void WriteStartDeltaResourceSetImplementation(ODataDeltaResourceSet deltaResourceSet)
		{
			this.CheckForNestedResourceInfoWithContent(ODataPayloadKind.ResourceSet, deltaResourceSet);
			this.EnterScope(ODataWriterCore.WriterState.DeltaResourceSet, deltaResourceSet);
			this.InterceptException(delegate
			{
				if (!this.outputContext.WritingResponse)
				{
					if (deltaResourceSet.NextPageLink != null)
					{
						this.ThrowODataException(Strings.ODataWriterCore_QueryNextLinkInRequest, deltaResourceSet);
					}
					if (deltaResourceSet.DeltaLink != null)
					{
						this.ThrowODataException(Strings.ODataWriterCore_QueryDeltaLinkInRequest, deltaResourceSet);
					}
				}
				this.StartDeltaResourceSet(deltaResourceSet);
			});
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00014C65 File Offset: 0x00012E65
		private void VerifyCanWriteStartResource(bool synchronousCall, ODataResource resource)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00014C74 File Offset: 0x00012E74
		private void VerifyCanWriteStartDeletedResource(bool synchronousCall, ODataDeletedResource resource)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeletedResource>(resource, "resource");
			this.VerifyWritingDelta();
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00014C98 File Offset: 0x00012E98
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
						this.ValidateResourceForResourceSet(resource, resourceScope);
						this.PrepareResourceForWriteStart(resourceScope, resource, this.outputContext.WritingResponse, resourceScope.SelectedProperties);
					}
					this.StartResource(resource);
				});
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00014CFC File Offset: 0x00012EFC
		private void WriteStartDeletedResourceImplementation(ODataDeletedResource resource)
		{
			this.StartPayloadInStartState();
			this.CheckForNestedResourceInfoWithContent(ODataPayloadKind.Resource, resource);
			this.EnterScope(ODataWriterCore.WriterState.DeletedResource, resource);
			this.IncreaseResourceDepth();
			this.InterceptException(delegate
			{
				ODataWriterCore.DeletedResourceScope deletedResourceScope = this.CurrentScope as ODataWriterCore.DeletedResourceScope;
				this.ValidateResourceForResourceSet(resource, deletedResourceScope);
				this.PrepareDeletedResourceForWriteStart(deletedResourceScope, resource, this.outputContext.WritingResponse, deletedResourceScope.SelectedProperties);
				this.StartDeletedResource(resource);
			});
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00014D55 File Offset: 0x00012F55
		private void VerifyCanWriteProperty(bool synchronousCall, ODataPropertyInfo property)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPropertyInfo>(property, "property");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00014D70 File Offset: 0x00012F70
		private void WriteStartPropertyImplementation(ODataPropertyInfo property)
		{
			this.EnterScope(ODataWriterCore.WriterState.Property, property);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					this.StartProperty(property);
					if (property is ODataProperty)
					{
						ODataWriterCore.PropertyInfoScope propertyInfoScope = this.CurrentScope as ODataWriterCore.PropertyInfoScope;
						propertyInfoScope.ValueWritten = true;
					}
				});
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00014DB9 File Offset: 0x00012FB9
		private void WriteDeltaLinkImplementation(ODataDeltaLinkBase deltaLink)
		{
			this.EnterScope((deltaLink is ODataDeltaLink) ? ODataWriterCore.WriterState.DeltaLink : ODataWriterCore.WriterState.DeltaDeletedLink, deltaLink);
			this.StartDeltaLink(deltaLink);
			this.WriteEnd();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00014DDB File Offset: 0x00012FDB
		private void VerifyCanWriteStartNestedResourceInfo(bool synchronousCall, ODataNestedResourceInfo nestedResourceInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNestedResourceInfo>(nestedResourceInfo, "nestedResourceInfo");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00014DF8 File Offset: 0x00012FF8
		private void WriteStartNestedResourceInfoImplementation(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.EnterScope(ODataWriterCore.WriterState.NestedResourceInfo, nestedResourceInfo);
			ODataResourceBase odataResourceBase = (ODataResourceBase)this.scopeStack.Parent.Item;
			if (odataResourceBase.MetadataBuilder != null)
			{
				nestedResourceInfo.MetadataBuilder = odataResourceBase.MetadataBuilder;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00014C65 File Offset: 0x00012E65
		private void VerifyCanWritePrimitive(bool synchronousCall, ODataPrimitiveValue primitiveValue)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00014E38 File Offset: 0x00013038
		private void WritePrimitiveValueImplementation(ODataPrimitiveValue primitiveValue)
		{
			this.InterceptException(delegate
			{
				this.EnterScope(ODataWriterCore.WriterState.Primitive, primitiveValue);
				if (this.CurrentResourceSetValidator != null && primitiveValue != null)
				{
					IEdmType definition = EdmLibraryExtensions.GetPrimitiveTypeReference(primitiveValue.Value.GetType()).Definition;
					this.CurrentResourceSetValidator.ValidateResource(definition);
				}
				this.WritePrimitiveValue(primitiveValue);
				this.WriteEnd();
			});
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00014C65 File Offset: 0x00012E65
		private void VerifyCanCreateWriteStream(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00014E6B File Offset: 0x0001306B
		private Stream CreateWriteStreamImplementation()
		{
			this.EnterScope(ODataWriterCore.WriterState.Stream, null);
			return new ODataNotificationStream(this.StartBinaryStream(), this);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00014C65 File Offset: 0x00012E65
		private void VerifyCanCreateTextWriter(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00014E82 File Offset: 0x00013082
		private TextWriter CreateTextWriterImplementation()
		{
			this.EnterScope(ODataWriterCore.WriterState.String, null);
			return new ODataNotificationWriter(this.StartTextWriter(), this);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00014C65 File Offset: 0x00012E65
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00014E99 File Offset: 0x00013099
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
				case ODataWriterCore.WriterState.DeltaResourceSet:
					if (!this.SkipWriting)
					{
						ODataDeltaResourceSet odataDeltaResourceSet = (ODataDeltaResourceSet)currentScope.Item;
						WriterValidationUtils.ValidateDeltaResourceSetAtEnd(odataDeltaResourceSet, !this.outputContext.WritingResponse);
						this.EndDeltaResourceSet(odataDeltaResourceSet);
					}
					break;
				case ODataWriterCore.WriterState.DeletedResource:
					if (!this.SkipWriting)
					{
						ODataDeletedResource odataDeletedResource = (ODataDeletedResource)currentScope.Item;
						this.EndDeletedResource(odataDeletedResource);
						this.DecreaseResourceDepth();
					}
					break;
				case ODataWriterCore.WriterState.DeltaLink:
				case ODataWriterCore.WriterState.DeltaDeletedLink:
				case ODataWriterCore.WriterState.Primitive:
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
				case ODataWriterCore.WriterState.Property:
				{
					ODataPropertyInfo odataPropertyInfo = (ODataPropertyInfo)currentScope.Item;
					this.EndProperty(odataPropertyInfo);
					break;
				}
				case ODataWriterCore.WriterState.Stream:
				case ODataWriterCore.WriterState.String:
					throw new ODataException(Strings.ODataWriterCore_StreamNotDisposed);
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_WriteEnd_UnreachableCodePath));
				}
				this.LeaveScope();
			});
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00014EB0 File Offset: 0x000130B0
		private void MarkNestedResourceInfoAsProcessed(ODataNestedResourceInfo link)
		{
			ODataResourceBase odataResourceBase = (ODataResourceBase)this.scopeStack.Parent.Item;
			odataResourceBase.MetadataBuilder.MarkNestedResourceInfoProcessed(link.Name);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00014EE4 File Offset: 0x000130E4
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool synchronousCall)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00014EFF File Offset: 0x000130FF
		private void VerifyCanWriteLink(bool synchronousCall, ODataDeltaLinkBase deltaLink)
		{
			this.VerifyWritingDelta();
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaLinkBase>(deltaLink, "delta link");
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00014F20 File Offset: 0x00013120
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			this.CheckForNestedResourceInfoWithContent(ODataPayloadKind.EntityReferenceLink, null);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
					ODataNestedResourceInfo odataNestedResourceInfo = this.CurrentScope.Item as ODataNestedResourceInfo;
					if (odataNestedResourceInfo == null)
					{
						ODataWriterCore.NestedResourceInfoScope parentNestedResourceInfoScope = this.ParentNestedResourceInfoScope;
						odataNestedResourceInfo = (ODataNestedResourceInfo)parentNestedResourceInfoScope.Item;
					}
					this.WriteEntityReferenceInNavigationLinkContent(odataNestedResourceInfo, entityReferenceLink);
				});
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00014C65 File Offset: 0x00012E65
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00014F63 File Offset: 0x00013163
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00014F98 File Offset: 0x00013198
		private void VerifyWritingDelta()
		{
			if (!this.writingDelta)
			{
				throw new ODataException(Strings.ODataWriterCore_CannotWriteDeltaWithResourceSetWriter);
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00014FAD File Offset: 0x000131AD
		private void ThrowODataException(string errorMessage, ODataItem item)
		{
			this.EnterScope(ODataWriterCore.WriterState.Error, item);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00014FBE File Offset: 0x000131BE
		private void StartPayloadInStartState()
		{
			if (this.State == ODataWriterCore.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00014FDC File Offset: 0x000131DC
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
							this.CurrentScope.ItemType = edmStructuralProperty.Type.Definition.AsElementType();
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
								this.CurrentScope.NavigationSource = ((parentResourceNavigationSource2 == null) ? null : parentResourceNavigationSource2.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), this.CurrentScope.ODataUri.Path.ToList<ODataPathSegment>(), out edmPathExpression));
							}
						}
					}
				});
				if (currentScope.State == ODataWriterCore.WriterState.NestedResourceInfoWithContent)
				{
					if (currentNestedResourceInfo.IsCollection != true)
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
							if ((currentNestedResourceInfo.SerializationInfo == null || !currentNestedResourceInfo.SerializationInfo.IsComplex) && (this.CurrentScope.ItemType == null || this.CurrentScope.ItemType.IsEntityOrEntityCollectionType()))
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
				ODataWriterCore.Scope parentNestedResourceInfoScope = this.ParentNestedResourceInfoScope;
				if (parentNestedResourceInfoScope.State != ODataWriterCore.WriterState.NestedResourceInfo && parentNestedResourceInfoScope.State != ODataWriterCore.WriterState.NestedResourceInfoWithContent)
				{
					this.ThrowODataException(Strings.ODataWriterCore_EntityReferenceLinkWithoutNavigationLink, null);
				}
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000150E0 File Offset: 0x000132E0
		private void ValidateResourceForResourceSet(ODataResourceBase resource, ODataWriterCore.ResourceBaseScope resourceScope)
		{
			IEdmStructuredType resourceType = this.GetResourceType(resource);
			ODataWriterCore.NestedResourceInfoScope parentNestedResourceInfoScope = this.ParentNestedResourceInfoScope;
			if (parentNestedResourceInfoScope != null)
			{
				this.WriterValidator.ValidateResourceInNestedResourceInfo(resourceType, parentNestedResourceInfoScope.ResourceType);
				resourceScope.ResourceTypeFromMetadata = parentNestedResourceInfoScope.ResourceType;
				this.WriterValidator.ValidateDerivedTypeConstraint(resourceType, resourceScope.ResourceTypeFromMetadata, parentNestedResourceInfoScope.DerivedTypeConstraints, "property", ((ODataNestedResourceInfo)parentNestedResourceInfoScope.Item).Name);
			}
			else
			{
				resourceScope.ResourceTypeFromMetadata = this.ParentScope.ResourceType;
				if (this.CurrentResourceSetValidator != null)
				{
					if (this.ParentScope.State == ODataWriterCore.WriterState.DeltaResourceSet && this.currentResourceDepth <= 1 && resourceScope.NavigationSource != null)
					{
						if (!resourceScope.NavigationSource.EntityType().IsAssignableFrom(resourceType))
						{
							throw new ODataException(Strings.ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes(resourceType.FullTypeName(), resourceScope.NavigationSource.EntityType()));
						}
						resourceScope.ResourceTypeFromMetadata = resourceScope.NavigationSource.EntityType();
					}
					else
					{
						this.CurrentResourceSetValidator.ValidateResource(resourceType);
					}
				}
				if (this.ParentScope.NavigationSource != null)
				{
					this.WriterValidator.ValidateDerivedTypeConstraint(resourceType, resourceScope.ResourceTypeFromMetadata, this.ParentScope.DerivedTypeConstraints, "navigation source", this.ParentScope.NavigationSource.Name);
				}
			}
			resourceScope.ResourceType = resourceType;
			if (this.ParentScope.State == ODataWriterCore.WriterState.DeltaResourceSet)
			{
				IEdmEntityType edmEntityType = resourceType as IEdmEntityType;
				if (resource.Id == null && edmEntityType != null && (resource is ODataDeletedResource || this.outputContext.MessageWriterSettings.Version > ODataVersion.V4) && !ODataWriterCore.HasKeyProperties(edmEntityType, resource.Properties))
				{
					throw new ODataException(Strings.ODataWriterCore_DeltaResourceWithoutIdOrKeyProperties);
				}
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00015290 File Offset: 0x00013490
		private static bool HasKeyProperties(IEdmEntityType entityType, IEnumerable<ODataProperty> properties)
		{
			return properties != null && entityType.Key().All((IEdmStructuralProperty keyProp) => properties.Select((ODataProperty p) => p.Name).Contains(keyProp.Name));
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000152CC File Offset: 0x000134CC
		private void InterceptException(Action action)
		{
			try
			{
				action();
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

		// Token: 0x0600091B RID: 2331 RVA: 0x00015314 File Offset: 0x00013514
		private void IncreaseResourceDepth()
		{
			this.currentResourceDepth++;
			if (this.currentResourceDepth > this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth)
			{
				this.ThrowODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth), null);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00015372 File Offset: 0x00013572
		private void DecreaseResourceDepth()
		{
			this.currentResourceDepth--;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00015382 File Offset: 0x00013582
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

		// Token: 0x0600091E RID: 2334 RVA: 0x000153B0 File Offset: 0x000135B0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug only cast.")]
		private void EnterScope(ODataWriterCore.WriterState newState, ODataItem item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			bool skipWriting = this.SkipWriting;
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			IEdmNavigationSource edmNavigationSource = null;
			IEdmType edmType = null;
			SelectedPropertiesNode selectedPropertiesNode = currentScope.SelectedProperties;
			ODataUri odataUri = currentScope.ODataUri.Clone();
			if (odataUri.Path == null)
			{
				odataUri.Path = new ODataPath(new ODataPathSegment[0]);
			}
			IEnumerable<string> enumerable = null;
			ODataWriterCore.WriterState state = currentScope.State;
			if (newState == ODataWriterCore.WriterState.Resource || newState == ODataWriterCore.WriterState.ResourceSet || newState == ODataWriterCore.WriterState.Primitive || newState == ODataWriterCore.WriterState.DeltaResourceSet || newState == ODataWriterCore.WriterState.DeletedResource)
			{
				ODataResourceBase odataResourceBase = item as ODataResourceBase;
				if (odataResourceBase != null)
				{
					IEdmModel model = this.outputContext.Model;
					if (model != null && model.IsUserModel())
					{
						try
						{
							string typeName = odataResourceBase.TypeName;
							if (!string.IsNullOrEmpty(typeName))
							{
								edmType = TypeNameOracle.ResolveAndValidateTypeName(model, typeName, EdmTypeKind.None, new bool?(true), this.outputContext.WriterValidator);
							}
							ODataResourceSerializationInfo serializationInfo = odataResourceBase.SerializationInfo;
							if (serializationInfo != null)
							{
								if (serializationInfo.NavigationSourceName != null)
								{
									ODataUriParser odataUriParser = new ODataUriParser(model, new Uri(serializationInfo.NavigationSourceName, UriKind.Relative), this.outputContext.Container);
									odataUri = odataUriParser.ParseUri();
									edmNavigationSource = odataUri.Path.NavigationSource();
									edmType = edmType ?? edmNavigationSource.EntityType();
								}
								if (typeName == null)
								{
									if (!string.IsNullOrEmpty(serializationInfo.ExpectedTypeName))
									{
										edmType = TypeNameOracle.ResolveAndValidateTypeName(model, serializationInfo.ExpectedTypeName, EdmTypeKind.None, new bool?(true), this.outputContext.WriterValidator);
									}
									else if (!string.IsNullOrEmpty(serializationInfo.NavigationSourceEntityTypeName))
									{
										edmType = TypeNameOracle.ResolveAndValidateTypeName(model, serializationInfo.NavigationSourceEntityTypeName, EdmTypeKind.Entity, new bool?(true), this.outputContext.WriterValidator);
									}
								}
							}
						}
						catch (ODataException)
						{
						}
					}
				}
				if (edmNavigationSource == null)
				{
					enumerable = currentScope.DerivedTypeConstraints;
				}
				else
				{
					enumerable = this.outputContext.Model.GetDerivedTypeConstraints(edmNavigationSource);
				}
				edmNavigationSource = edmNavigationSource ?? currentScope.NavigationSource;
				edmType = edmType ?? currentScope.ItemType;
				if (edmType == null && (state == ODataWriterCore.WriterState.Start || state == ODataWriterCore.WriterState.NestedResourceInfo || state == ODataWriterCore.WriterState.NestedResourceInfoWithContent) && (newState == ODataWriterCore.WriterState.ResourceSet || newState == ODataWriterCore.WriterState.DeltaResourceSet))
				{
					ODataResourceSetBase odataResourceSetBase = item as ODataResourceSetBase;
					if (odataResourceSetBase != null && odataResourceSetBase.TypeName != null && this.outputContext.Model.IsUserModel())
					{
						IEdmCollectionType edmCollectionType = TypeNameOracle.ResolveAndValidateTypeName(this.outputContext.Model, odataResourceSetBase.TypeName, EdmTypeKind.Collection, new bool?(false), this.outputContext.WriterValidator) as IEdmCollectionType;
						if (edmCollectionType != null)
						{
							edmType = edmCollectionType.ElementType.Definition;
						}
					}
				}
			}
			if ((state == ODataWriterCore.WriterState.Resource || state == ODataWriterCore.WriterState.DeletedResource) && newState == ODataWriterCore.WriterState.NestedResourceInfo)
			{
				ODataNestedResourceInfo odataNestedResourceInfo = (ODataNestedResourceInfo)item;
				if (!skipWriting)
				{
					selectedPropertiesNode = currentScope.SelectedProperties.GetSelectedPropertiesForNavigationProperty(currentScope.ResourceType, odataNestedResourceInfo.Name);
					if (this.outputContext.WritingResponse || this.writingDelta)
					{
						ODataPath odataPath = odataUri.Path;
						IEdmStructuredType resourceType = currentScope.ResourceType;
						ODataWriterCore.ResourceBaseScope resourceBaseScope = currentScope as ODataWriterCore.ResourceBaseScope;
						TypeSegment typeSegment = null;
						if (resourceBaseScope.ResourceTypeFromMetadata != resourceType)
						{
							typeSegment = new TypeSegment(resourceType, null);
						}
						IEdmStructuralProperty edmStructuralProperty = this.WriterValidator.ValidatePropertyDefined(odataNestedResourceInfo.Name, resourceType) as IEdmStructuralProperty;
						if (edmStructuralProperty != null)
						{
							odataPath = this.AppendEntitySetKeySegment(odataPath, false);
							edmType = ((edmStructuralProperty.Type == null) ? null : edmStructuralProperty.Type.Definition.AsElementType());
							edmNavigationSource = null;
							if (typeSegment != null)
							{
								odataPath.Add(typeSegment);
							}
							odataPath = odataPath.AppendPropertySegment(edmStructuralProperty);
							enumerable = this.outputContext.Model.GetDerivedTypeConstraints(edmStructuralProperty);
						}
						else
						{
							IEdmNavigationProperty edmNavigationProperty = this.WriterValidator.ValidateNestedResourceInfo(odataNestedResourceInfo, resourceType, null);
							if (edmNavigationProperty != null)
							{
								enumerable = this.outputContext.Model.GetDerivedTypeConstraints(edmNavigationProperty);
								edmType = edmNavigationProperty.ToEntityType();
								if (odataNestedResourceInfo.IsCollection == null)
								{
									odataNestedResourceInfo.IsCollection = new bool?(edmNavigationProperty.Type.IsEntityCollectionType());
								}
								if (odataNestedResourceInfo.IsCollection == null)
								{
									odataNestedResourceInfo.IsCollection = new bool?(edmNavigationProperty.Type.IsEntityCollectionType());
								}
								IEdmNavigationSource navigationSource = currentScope.NavigationSource;
								if (typeSegment != null)
								{
									odataPath.Add(typeSegment);
								}
								IEdmPathExpression edmPathExpression;
								edmNavigationSource = ((navigationSource == null) ? null : navigationSource.FindNavigationTarget(edmNavigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), odataPath.ToList<ODataPathSegment>(), out edmPathExpression));
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
			else if ((state == ODataWriterCore.WriterState.ResourceSet || state == ODataWriterCore.WriterState.DeltaResourceSet) && (newState == ODataWriterCore.WriterState.Resource || newState == ODataWriterCore.WriterState.Primitive || newState == ODataWriterCore.WriterState.ResourceSet || newState == ODataWriterCore.WriterState.DeletedResource) && (state == ODataWriterCore.WriterState.ResourceSet || state == ODataWriterCore.WriterState.DeltaResourceSet))
			{
				ODataWriterCore.ResourceSetBaseScope resourceSetBaseScope = (ODataWriterCore.ResourceSetBaseScope)currentScope;
				int resourceCount = resourceSetBaseScope.ResourceCount;
				resourceSetBaseScope.ResourceCount = resourceCount + 1;
			}
			if (edmNavigationSource == null)
			{
				edmNavigationSource = this.CurrentScope.NavigationSource ?? odataUri.Path.TargetNavigationSource();
			}
			this.PushScope(newState, item, edmNavigationSource, edmType, skipWriting, selectedPropertiesNode, odataUri, enumerable);
			this.NotifyListener(newState);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000159AC File Offset: 0x00013BAC
		private ODataPath AppendEntitySetKeySegment(ODataPath odataPath, bool throwIfFail)
		{
			ODataPath odataPath2 = odataPath;
			try
			{
				if (EdmExtensionMethods.HasKey(this.CurrentScope.NavigationSource, this.CurrentScope.ResourceType))
				{
					IEdmEntityType edmEntityType = this.CurrentScope.ResourceType as IEdmEntityType;
					ODataResourceBase odataResourceBase = this.CurrentScope.Item as ODataResourceBase;
					KeyValuePair<string, object>[] keyProperties = ODataResourceMetadataContext.GetKeyProperties(odataResourceBase, this.GetResourceSerializationInfo(odataResourceBase), edmEntityType);
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

		// Token: 0x06000920 RID: 2336 RVA: 0x00015A38 File Offset: 0x00013C38
		private void LeaveScope()
		{
			this.scopeStack.Pop();
			if (this.scopeStack.Count == 1)
			{
				ODataWriterCore.Scope scope = this.scopeStack.Pop();
				this.PushScope(ODataWriterCore.WriterState.Completed, null, scope.NavigationSource, scope.ResourceType, false, scope.SelectedProperties, scope.ODataUri, null);
				this.InterceptException(new Action(this.EndPayload));
				this.NotifyListener(ODataWriterCore.WriterState.Completed);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00015AAC File Offset: 0x00013CAC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Second cast only in debug.")]
		private void PromoteNestedResourceInfoScope(ODataItem content)
		{
			this.ValidateTransition(ODataWriterCore.WriterState.NestedResourceInfoWithContent);
			ODataWriterCore.NestedResourceInfoScope nestedResourceInfoScope = (ODataWriterCore.NestedResourceInfoScope)this.scopeStack.Pop();
			ODataWriterCore.NestedResourceInfoScope nestedResourceInfoScope2 = nestedResourceInfoScope.Clone(ODataWriterCore.WriterState.NestedResourceInfoWithContent);
			this.scopeStack.Push(nestedResourceInfoScope2);
			if (nestedResourceInfoScope2.ItemType == null && content != null && !this.SkipWriting && !(content is ODataPrimitiveValue))
			{
				ODataPrimitiveValue odataPrimitiveValue = content as ODataPrimitiveValue;
				if (odataPrimitiveValue != null)
				{
					nestedResourceInfoScope2.ItemType = EdmLibraryExtensions.GetPrimitiveTypeReference(odataPrimitiveValue.GetType()).Definition;
					return;
				}
				ODataResourceBase odataResourceBase = content as ODataResourceBase;
				nestedResourceInfoScope2.ResourceType = ((odataResourceBase != null) ? this.GetResourceType(odataResourceBase) : this.GetResourceSetType(content as ODataResourceSetBase));
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00015B48 File Offset: 0x00013D48
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
				if (newState != ODataWriterCore.WriterState.ResourceSet && newState != ODataWriterCore.WriterState.Resource && newState != ODataWriterCore.WriterState.DeltaResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				if ((newState == ODataWriterCore.WriterState.ResourceSet || newState == ODataWriterCore.WriterState.DeltaResourceSet) && !this.writingResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelResourceSetWithResourceWriter);
				}
				if (newState == ODataWriterCore.WriterState.Resource && this.writingResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelResourceWithResourceSetWriter);
				}
				return;
			case ODataWriterCore.WriterState.Resource:
			case ODataWriterCore.WriterState.DeletedResource:
				if (this.CurrentScope.Item == null)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromNullResource(this.State.ToString(), newState.ToString()));
				}
				if (newState != ODataWriterCore.WriterState.NestedResourceInfo && newState != ODataWriterCore.WriterState.Property)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromResource(this.State.ToString(), newState.ToString()));
				}
				if (newState == ODataWriterCore.WriterState.DeletedResource && this.ParentScope.State != ODataWriterCore.WriterState.DeltaResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromResourceSet(this.State.ToString(), newState.ToString()));
				}
				if (this.State == ODataWriterCore.WriterState.DeletedResource && this.Version < ODataVersion.V401 && newState == ODataWriterCore.WriterState.NestedResourceInfo)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFrom40DeletedResource(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.ResourceSet:
				if (newState != ODataWriterCore.WriterState.Resource && this.CurrentScope.ResourceType != null && (this.CurrentScope.ResourceType.TypeKind != EdmTypeKind.Untyped || (newState != ODataWriterCore.WriterState.Primitive && newState != ODataWriterCore.WriterState.Stream && newState != ODataWriterCore.WriterState.String && newState != ODataWriterCore.WriterState.ResourceSet)))
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromResourceSet(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.DeltaResourceSet:
				if (newState != ODataWriterCore.WriterState.Resource && newState != ODataWriterCore.WriterState.DeletedResource && (this.ScopeLevel >= 3 || (newState != ODataWriterCore.WriterState.DeltaDeletedLink && newState != ODataWriterCore.WriterState.DeltaLink)))
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
				if (newState != ODataWriterCore.WriterState.ResourceSet && newState != ODataWriterCore.WriterState.Resource && newState != ODataWriterCore.WriterState.Primitive && (this.Version < ODataVersion.V401 || (newState != ODataWriterCore.WriterState.DeltaResourceSet && newState != ODataWriterCore.WriterState.DeletedResource)))
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromExpandedLink(this.State.ToString(), newState.ToString()));
				}
				return;
			case ODataWriterCore.WriterState.Property:
			{
				ODataWriterCore.PropertyInfoScope propertyInfoScope = this.CurrentScope as ODataWriterCore.PropertyInfoScope;
				if (propertyInfoScope.ValueWritten)
				{
					ODataPropertyInfo odataPropertyInfo = propertyInfoScope.Item as ODataPropertyInfo;
					throw new ODataException(Strings.ODataWriterCore_PropertyValueAlreadyWritten(odataPropertyInfo.Name));
				}
				if (newState == ODataWriterCore.WriterState.Stream || newState == ODataWriterCore.WriterState.String || newState == ODataWriterCore.WriterState.Primitive)
				{
					propertyInfoScope.ValueWritten = true;
					return;
				}
				throw new ODataException(Strings.ODataWriterCore_InvalidStateTransition(this.State.ToString(), newState.ToString()));
			}
			case ODataWriterCore.WriterState.Stream:
			case ODataWriterCore.WriterState.String:
				throw new ODataException(Strings.ODataWriterCore_StreamNotDisposed);
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

		// Token: 0x06000923 RID: 2339 RVA: 0x00015F90 File Offset: 0x00014190
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
		private void PushScope(ODataWriterCore.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, IEnumerable<string> derivedTypeConstraints)
		{
			IEdmStructuredType edmStructuredType = itemType as IEdmStructuredType;
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
			case ODataWriterCore.WriterState.Stream:
			case ODataWriterCore.WriterState.String:
			case ODataWriterCore.WriterState.Completed:
			case ODataWriterCore.WriterState.Error:
				scope = new ODataWriterCore.Scope(state, item, navigationSource, itemType, skipWriting, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.Resource:
				scope = this.CreateResourceScope((ODataResource)item, navigationSource, edmStructuredType, skipWriting, selectedProperties, odataUri, flag);
				break;
			case ODataWriterCore.WriterState.ResourceSet:
				scope = this.CreateResourceSetScope((ODataResourceSet)item, navigationSource, itemType, skipWriting, selectedProperties, odataUri, flag);
				if (this.outputContext.Model.IsUserModel())
				{
					((ODataWriterCore.ResourceSetBaseScope)scope).ResourceTypeValidator = new ResourceSetWithoutExpectedTypeValidator(itemType);
				}
				break;
			case ODataWriterCore.WriterState.DeltaResourceSet:
				scope = this.CreateDeltaResourceSetScope((ODataDeltaResourceSet)item, navigationSource, edmStructuredType, skipWriting, selectedProperties, odataUri, flag);
				if (this.outputContext.Model.IsUserModel())
				{
					((ODataWriterCore.ResourceSetBaseScope)scope).ResourceTypeValidator = new ResourceSetWithoutExpectedTypeValidator(edmStructuredType);
				}
				break;
			case ODataWriterCore.WriterState.DeletedResource:
				scope = this.CreateDeletedResourceScope((ODataDeletedResource)item, navigationSource, (IEdmEntityType)itemType, skipWriting, selectedProperties, odataUri, flag);
				break;
			case ODataWriterCore.WriterState.DeltaLink:
			case ODataWriterCore.WriterState.DeltaDeletedLink:
				scope = this.CreateDeltaLinkScope((ODataDeltaLinkBase)item, navigationSource, (IEdmEntityType)itemType, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.NestedResourceInfo:
			case ODataWriterCore.WriterState.NestedResourceInfoWithContent:
				scope = this.CreateNestedResourceInfoScope(state, (ODataNestedResourceInfo)item, navigationSource, itemType, skipWriting, selectedProperties, odataUri);
				break;
			case ODataWriterCore.WriterState.Property:
				scope = this.CreatePropertyInfoScope((ODataPropertyInfo)item, navigationSource, edmStructuredType, selectedProperties, odataUri);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			scope.DerivedTypeConstraints = derivedTypeConstraints;
			this.scopeStack.Push(scope);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001616C File Offset: 0x0001436C
		private bool IsUndeclared(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (nestedResourceInfo.SerializationInfo != null)
			{
				return nestedResourceInfo.SerializationInfo.IsUndeclared;
			}
			return this.ParentResourceType != null && this.ParentResourceType.FindProperty((this.CurrentScope.Item as ODataNestedResourceInfo).Name) == null;
		}

		// Token: 0x04000334 RID: 820
		protected readonly IWriterValidator WriterValidator;

		// Token: 0x04000335 RID: 821
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000336 RID: 822
		private readonly bool writingResourceSet;

		// Token: 0x04000337 RID: 823
		private readonly bool writingDelta;

		// Token: 0x04000338 RID: 824
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000339 RID: 825
		private readonly ODataWriterCore.ScopeStack scopeStack = new ODataWriterCore.ScopeStack();

		// Token: 0x0400033A RID: 826
		private int currentResourceDepth;

		// Token: 0x0200030F RID: 783
		internal enum WriterState
		{
			// Token: 0x04000D50 RID: 3408
			Start,
			// Token: 0x04000D51 RID: 3409
			Resource,
			// Token: 0x04000D52 RID: 3410
			ResourceSet,
			// Token: 0x04000D53 RID: 3411
			DeltaResourceSet,
			// Token: 0x04000D54 RID: 3412
			DeletedResource,
			// Token: 0x04000D55 RID: 3413
			DeltaLink,
			// Token: 0x04000D56 RID: 3414
			DeltaDeletedLink,
			// Token: 0x04000D57 RID: 3415
			NestedResourceInfo,
			// Token: 0x04000D58 RID: 3416
			NestedResourceInfoWithContent,
			// Token: 0x04000D59 RID: 3417
			Primitive,
			// Token: 0x04000D5A RID: 3418
			Property,
			// Token: 0x04000D5B RID: 3419
			Stream,
			// Token: 0x04000D5C RID: 3420
			String,
			// Token: 0x04000D5D RID: 3421
			Completed,
			// Token: 0x04000D5E RID: 3422
			Error
		}

		// Token: 0x02000310 RID: 784
		internal sealed class ScopeStack
		{
			// Token: 0x06001DD7 RID: 7639 RVA: 0x000580A8 File Offset: 0x000562A8
			internal ScopeStack()
			{
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000580BB File Offset: 0x000562BB
			internal int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000580C8 File Offset: 0x000562C8
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

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001DDA RID: 7642 RVA: 0x000580FC File Offset: 0x000562FC
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

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00058146 File Offset: 0x00056346
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

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00058158 File Offset: 0x00056358
			internal Stack<ODataWriterCore.Scope> Scopes
			{
				get
				{
					return this.scopes;
				}
			}

			// Token: 0x06001DDD RID: 7645 RVA: 0x00058160 File Offset: 0x00056360
			internal void Push(ODataWriterCore.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x06001DDE RID: 7646 RVA: 0x0005816E File Offset: 0x0005636E
			internal ODataWriterCore.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x06001DDF RID: 7647 RVA: 0x0005817B File Offset: 0x0005637B
			internal ODataWriterCore.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x04000D5F RID: 3423
			private readonly Stack<ODataWriterCore.Scope> scopes = new Stack<ODataWriterCore.Scope>();
		}

		// Token: 0x02000311 RID: 785
		internal class Scope
		{
			// Token: 0x06001DE0 RID: 7648 RVA: 0x00058188 File Offset: 0x00056388
			internal Scope(ODataWriterCore.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.itemType = itemType;
				this.resourceType = itemType as IEdmStructuredType;
				this.navigationSource = navigationSource;
				this.skipWriting = skipWriting;
				this.selectedProperties = selectedProperties;
				this.odataUri = odataUri;
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000581DD File Offset: 0x000563DD
			// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x000581E5 File Offset: 0x000563E5
			public IEdmStructuredType ResourceType
			{
				get
				{
					return this.resourceType;
				}
				set
				{
					this.resourceType = value;
					this.itemType = value;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000581F5 File Offset: 0x000563F5
			// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x000581FD File Offset: 0x000563FD
			public IEdmType ItemType
			{
				get
				{
					return this.itemType;
				}
				set
				{
					this.itemType = value;
					this.resourceType = value as IEdmStructuredType;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00058212 File Offset: 0x00056412
			internal ODataWriterCore.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0005821A File Offset: 0x0005641A
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00058222 File Offset: 0x00056422
			// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0005822A File Offset: 0x0005642A
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

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00058233 File Offset: 0x00056433
			internal SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06001DEA RID: 7658 RVA: 0x0005823B File Offset: 0x0005643B
			internal ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00058243 File Offset: 0x00056443
			internal bool SkipWriting
			{
				get
				{
					return this.skipWriting;
				}
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0005824B File Offset: 0x0005644B
			// (set) Token: 0x06001DED RID: 7661 RVA: 0x00058253 File Offset: 0x00056453
			internal IEnumerable<string> DerivedTypeConstraints { get; set; }

			// Token: 0x04000D60 RID: 3424
			private readonly ODataWriterCore.WriterState state;

			// Token: 0x04000D61 RID: 3425
			private readonly ODataItem item;

			// Token: 0x04000D62 RID: 3426
			private readonly bool skipWriting;

			// Token: 0x04000D63 RID: 3427
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x04000D64 RID: 3428
			private IEdmNavigationSource navigationSource;

			// Token: 0x04000D65 RID: 3429
			private IEdmStructuredType resourceType;

			// Token: 0x04000D66 RID: 3430
			private IEdmType itemType;

			// Token: 0x04000D67 RID: 3431
			private ODataUri odataUri;
		}

		// Token: 0x02000312 RID: 786
		internal abstract class ResourceSetBaseScope : ODataWriterCore.Scope
		{
			// Token: 0x06001DEE RID: 7662 RVA: 0x0005825C File Offset: 0x0005645C
			internal ResourceSetBaseScope(ODataWriterCore.WriterState writerState, ODataResourceSetBase resourceSet, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, resourceSet, navigationSource, itemType, skipWriting, selectedProperties, odataUri)
			{
				this.serializationInfo = resourceSet.SerializationInfo;
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0005827B File Offset: 0x0005647B
			// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x00058283 File Offset: 0x00056483
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

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0005828C File Offset: 0x0005648C
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

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000582A7 File Offset: 0x000564A7
			// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x000582AF File Offset: 0x000564AF
			internal ResourceSetWithoutExpectedTypeValidator ResourceTypeValidator
			{
				get
				{
					return this.resourceTypeValidator;
				}
				set
				{
					this.resourceTypeValidator = value;
				}
			}

			// Token: 0x06001DF4 RID: 7668 RVA: 0x000582B8 File Offset: 0x000564B8
			internal ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse)
			{
				if (this.typeContext == null)
				{
					bool flag = writingResponse && (base.ResourceType == null || base.ResourceType.TypeKind == EdmTypeKind.Entity);
					this.typeContext = ODataResourceTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), base.ResourceType, flag);
				}
				return this.typeContext;
			}

			// Token: 0x04000D69 RID: 3433
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000D6A RID: 3434
			private ResourceSetWithoutExpectedTypeValidator resourceTypeValidator;

			// Token: 0x04000D6B RID: 3435
			private int resourceCount;

			// Token: 0x04000D6C RID: 3436
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x04000D6D RID: 3437
			private ODataResourceTypeContext typeContext;
		}

		// Token: 0x02000313 RID: 787
		internal abstract class ResourceSetScope : ODataWriterCore.ResourceSetBaseScope
		{
			// Token: 0x06001DF5 RID: 7669 RVA: 0x00058321 File Offset: 0x00056521
			protected ResourceSetScope(ODataResourceSet item, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.ResourceSet, item, navigationSource, itemType, skipWriting, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x02000314 RID: 788
		internal abstract class DeltaResourceSetScope : ODataWriterCore.ResourceSetBaseScope
		{
			// Token: 0x06001DF6 RID: 7670 RVA: 0x00058333 File Offset: 0x00056533
			protected DeltaResourceSetScope(ODataDeltaResourceSet item, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.DeltaResourceSet, item, navigationSource, resourceType, false, selectedProperties, odataUri)
			{
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x00058344 File Offset: 0x00056544
			// (set) Token: 0x06001DF8 RID: 7672 RVA: 0x0005834C File Offset: 0x0005654C
			public ODataContextUrlInfo ContextUriInfo { get; set; }
		}

		// Token: 0x02000315 RID: 789
		internal class ResourceBaseScope : ODataWriterCore.Scope
		{
			// Token: 0x06001DF9 RID: 7673 RVA: 0x00058355 File Offset: 0x00056555
			internal ResourceBaseScope(ODataWriterCore.WriterState state, ODataResourceBase resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, resource, navigationSource, itemType, skipWriting, selectedProperties, odataUri)
			{
				if (resource != null)
				{
					this.duplicatePropertyNameChecker = writerSettings.Validator.CreateDuplicatePropertyNameChecker();
				}
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06001DFA RID: 7674 RVA: 0x00058385 File Offset: 0x00056585
			// (set) Token: 0x06001DFB RID: 7675 RVA: 0x0005838D File Offset: 0x0005658D
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

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x06001DFC RID: 7676 RVA: 0x00058396 File Offset: 0x00056596
			public ODataResourceSerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0005839E File Offset: 0x0005659E
			internal IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
			{
				get
				{
					return this.duplicatePropertyNameChecker;
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000583A6 File Offset: 0x000565A6
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

			// Token: 0x06001DFF RID: 7679 RVA: 0x000583C4 File Offset: 0x000565C4
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

			// Token: 0x04000D6F RID: 3439
			private readonly IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

			// Token: 0x04000D70 RID: 3440
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000D71 RID: 3441
			private IEdmStructuredType resourceTypeFromMetadata;

			// Token: 0x04000D72 RID: 3442
			private ODataResourceTypeContext typeContext;

			// Token: 0x04000D73 RID: 3443
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x02000316 RID: 790
		internal class ResourceScope : ODataWriterCore.ResourceBaseScope
		{
			// Token: 0x06001E00 RID: 7680 RVA: 0x00058430 File Offset: 0x00056630
			protected ResourceScope(ODataResource resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.Resource, resource, serializationInfo, navigationSource, resourceType, skipWriting, writerSettings, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x02000317 RID: 791
		internal class DeletedResourceScope : ODataWriterCore.ResourceBaseScope
		{
			// Token: 0x06001E01 RID: 7681 RVA: 0x00058454 File Offset: 0x00056654
			protected DeletedResourceScope(ODataDeletedResource resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.DeletedResource, resource, serializationInfo, navigationSource, entityType, false, writerSettings, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x02000318 RID: 792
		internal abstract class DeltaLinkScope : ODataWriterCore.Scope
		{
			// Token: 0x06001E02 RID: 7682 RVA: 0x00058474 File Offset: 0x00056674
			protected DeltaLinkScope(ODataWriterCore.WriterState state, ODataItem link, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, link, navigationSource, entityType, false, selectedProperties, odataUri)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x06001E03 RID: 7683 RVA: 0x000584A3 File Offset: 0x000566A3
			public ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataResourceTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), this.fakeEntityType, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000D74 RID: 3444
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000D75 RID: 3445
			private readonly EdmEntityType fakeEntityType = new EdmEntityType("MyNS", "Fake");

			// Token: 0x04000D76 RID: 3446
			private ODataResourceTypeContext typeContext;
		}

		// Token: 0x02000319 RID: 793
		internal class PropertyInfoScope : ODataWriterCore.Scope
		{
			// Token: 0x06001E04 RID: 7684 RVA: 0x000584E1 File Offset: 0x000566E1
			internal PropertyInfoScope(ODataPropertyInfo property, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataWriterCore.WriterState.Property, property, navigationSource, resourceType, false, selectedProperties, odataUri)
			{
				this.ValueWritten = false;
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x06001E05 RID: 7685 RVA: 0x000584FA File Offset: 0x000566FA
			public ODataPropertyInfo Property
			{
				get
				{
					return base.Item as ODataProperty;
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x06001E06 RID: 7686 RVA: 0x00058507 File Offset: 0x00056707
			// (set) Token: 0x06001E07 RID: 7687 RVA: 0x0005850F File Offset: 0x0005670F
			internal bool ValueWritten { get; set; }
		}

		// Token: 0x0200031A RID: 794
		internal class NestedResourceInfoScope : ODataWriterCore.Scope
		{
			// Token: 0x06001E08 RID: 7688 RVA: 0x00058518 File Offset: 0x00056718
			internal NestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, navLink, navigationSource, itemType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x06001E09 RID: 7689 RVA: 0x0005852B File Offset: 0x0005672B
			internal virtual ODataWriterCore.NestedResourceInfoScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataWriterCore.NestedResourceInfoScope(newWriterState, (ODataNestedResourceInfo)base.Item, base.NavigationSource, base.ItemType, base.SkipWriting, base.SelectedProperties, base.ODataUri)
				{
					DerivedTypeConstraints = base.DerivedTypeConstraints
				};
			}
		}
	}
}
