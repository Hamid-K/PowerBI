using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000B4 RID: 180
	internal abstract class ODataReaderCore : ODataReader, IODataStreamListener
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x00012C28 File Offset: 0x00010E28
		protected ODataReaderCore(ODataInputContext inputContext, bool readingResourceSet, bool readingDelta, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.readingResourceSet = readingResourceSet;
			this.readingDelta = readingDelta;
			this.listener = listener;
			this.currentResourceDepth = 0;
			this.Version = inputContext.MessageReaderSettings.Version;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00012C7B File Offset: 0x00010E7B
		public sealed override ODataReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00012C98 File Offset: 0x00010E98
		public sealed override ODataItem Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00012CB5 File Offset: 0x00010EB5
		internal ODataVersion? Version { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00012CBD File Offset: 0x00010EBD
		protected ODataResourceSet CurrentResourceSet
		{
			get
			{
				return (ODataResourceSet)this.Item;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00012CCA File Offset: 0x00010ECA
		protected ODataDeltaResourceSet CurrentDeltaResourceSet
		{
			get
			{
				return (ODataDeltaResourceSet)this.Item;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00012CD7 File Offset: 0x00010ED7
		protected ODataDeltaLink CurrentDeltaLink
		{
			get
			{
				return (ODataDeltaLink)this.Item;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00012CE4 File Offset: 0x00010EE4
		protected ODataDeltaDeletedLink CurrentDeltaDeletedLink
		{
			get
			{
				return (ODataDeltaDeletedLink)this.Item;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00012CF1 File Offset: 0x00010EF1
		protected int CurrentResourceDepth
		{
			get
			{
				return this.currentResourceDepth;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00012CF9 File Offset: 0x00010EF9
		protected ODataNestedResourceInfo CurrentNestedResourceInfo
		{
			get
			{
				return (ODataNestedResourceInfo)this.Item;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00012D06 File Offset: 0x00010F06
		protected ODataEntityReferenceLink CurrentEntityReferenceLink
		{
			get
			{
				return (ODataEntityReferenceLink)this.Item;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00012D13 File Offset: 0x00010F13
		protected IEdmType CurrentResourceType
		{
			get
			{
				if (this.CurrentResourceTypeReference != null)
				{
					return this.CurrentResourceTypeReference.Definition;
				}
				return null;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00012D2C File Offset: 0x00010F2C
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00012D4B File Offset: 0x00010F4B
		protected IEdmTypeReference CurrentResourceTypeReference
		{
			get
			{
				return this.scopes.Peek().ResourceTypeReference;
			}
			set
			{
				this.scopes.Peek().ResourceTypeReference = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00012D60 File Offset: 0x00010F60
		protected IEdmNavigationSource CurrentNavigationSource
		{
			get
			{
				return this.scopes.Peek().NavigationSource;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00012D7F File Offset: 0x00010F7F
		protected ODataReaderCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00012D8C File Offset: 0x00010F8C
		protected Stack<ODataReaderCore.Scope> Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00012D94 File Offset: 0x00010F94
		protected ODataReaderCore.Scope ParentScope
		{
			get
			{
				return this.scopes.Skip(1).First<ODataReaderCore.Scope>();
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00012DA7 File Offset: 0x00010FA7
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00012DBC File Offset: 0x00010FBC
		protected ODataReaderCore.Scope ExpandedLinkContentParentScope
		{
			get
			{
				if (this.scopes.Count > 1)
				{
					ODataReaderCore.Scope scope = this.scopes.Skip(1).First<ODataReaderCore.Scope>();
					if (scope.State == ODataReaderState.NestedResourceInfoStart)
					{
						return scope;
					}
				}
				return null;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00012DF5 File Offset: 0x00010FF5
		protected bool IsExpandedLinkContent
		{
			get
			{
				return this.ExpandedLinkContentParentScope != null;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00012E00 File Offset: 0x00011000
		protected bool ReadingResourceSet
		{
			get
			{
				return this.readingResourceSet;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00012E08 File Offset: 0x00011008
		protected bool ReadingDelta
		{
			get
			{
				return this.readingDelta;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00012E10 File Offset: 0x00011010
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00012E1B File Offset: 0x0001101B
		protected ResourceSetWithoutExpectedTypeValidator CurrentResourceSetValidator
		{
			get
			{
				if (this.ParentScope != null)
				{
					return this.ParentScope.ResourceTypeValidator;
				}
				return null;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00012E32 File Offset: 0x00011032
		protected DerivedTypeValidator CurrentDerivedTypeValidator
		{
			get
			{
				if (this.ParentScope != null)
				{
					return this.ParentScope.DerivedTypeValidator;
				}
				return null;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00012E49 File Offset: 0x00011049
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00012E64 File Offset: 0x00011064
		public sealed override Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Exception, null, null));
			});
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00012E84 File Offset: 0x00011084
		public sealed override Stream CreateReadStream()
		{
			if (this.State != ODataReaderState.Stream)
			{
				throw new ODataException(Strings.ODataReaderCore_CreateReadStreamCalledInInvalidState);
			}
			ODataReaderCore.StreamScope streamScope = this.CurrentScope as ODataReaderCore.StreamScope;
			if (streamScope.StreamingState != ODataReaderCore.StreamingState.None)
			{
				throw new ODataException(Strings.ODataReaderCore_CreateReadStreamCalledInInvalidState);
			}
			streamScope.StreamingState = ODataReaderCore.StreamingState.Streaming;
			return new ODataNotificationStream(this.InterceptException<Stream>(new Func<Stream>(this.CreateReadStreamImplementation)), this);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00012EE8 File Offset: 0x000110E8
		public sealed override TextReader CreateTextReader()
		{
			if (this.State != ODataReaderState.Stream)
			{
				throw new ODataException(Strings.ODataReaderCore_CreateTextReaderCalledInInvalidState);
			}
			ODataReaderCore.StreamScope streamScope = this.CurrentScope as ODataReaderCore.StreamScope;
			if (streamScope.StreamingState != ODataReaderCore.StreamingState.None)
			{
				throw new ODataException(Strings.ODataReaderCore_CreateReadStreamCalledInInvalidState);
			}
			streamScope.StreamingState = ODataReaderCore.StreamingState.Streaming;
			return new ODataNotificationReader(this.InterceptException<TextReader>(new Func<TextReader>(this.CreateTextReaderImplementation)), this);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0000239D File Offset: 0x0000059D
		void IODataStreamListener.StreamRequested()
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00012F49 File Offset: 0x00011149
		Task IODataStreamListener.StreamRequestedAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				((IODataStreamListener)this).StreamRequested();
			});
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00012F5C File Offset: 0x0001115C
		void IODataStreamListener.StreamDisposed()
		{
			ODataReaderCore.StreamScope streamScope = this.CurrentScope as ODataReaderCore.StreamScope;
			streamScope.StreamingState = ODataReaderCore.StreamingState.Completed;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00012F7C File Offset: 0x0001117C
		internal ODataReaderCore.Scope SeekScope<T>(int maxDepth) where T : ODataReaderCore.Scope
		{
			int num = 1;
			foreach (ODataReaderCore.Scope scope in this.scopes)
			{
				if (num > maxDepth)
				{
					return null;
				}
				if (scope is T)
				{
					return scope;
				}
				num++;
			}
			return null;
		}

		// Token: 0x060007F1 RID: 2033
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060007F2 RID: 2034
		protected abstract bool ReadAtResourceSetStartImplementation();

		// Token: 0x060007F3 RID: 2035
		protected abstract bool ReadAtResourceSetEndImplementation();

		// Token: 0x060007F4 RID: 2036
		protected abstract bool ReadAtResourceStartImplementation();

		// Token: 0x060007F5 RID: 2037
		protected abstract bool ReadAtResourceEndImplementation();

		// Token: 0x060007F6 RID: 2038 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtPrimitiveImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtNestedPropertyInfoImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtStreamImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual Stream CreateReadStreamImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual TextReader CreateTextReaderImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FB RID: 2043
		protected abstract bool ReadAtNestedResourceInfoStartImplementation();

		// Token: 0x060007FC RID: 2044
		protected abstract bool ReadAtNestedResourceInfoEndImplementation();

		// Token: 0x060007FD RID: 2045
		protected abstract bool ReadAtEntityReferenceLink();

		// Token: 0x060007FE RID: 2046 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtDeltaResourceSetStartImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtDeltaResourceSetEndImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtDeletedResourceStartImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtDeletedResourceEndImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtDeltaLinkImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual bool ReadAtDeltaDeletedLinkImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00012FE8 File Offset: 0x000111E8
		protected void EnterScope(ODataReaderCore.Scope scope)
		{
			if ((scope.State == ODataReaderState.ResourceSetStart || scope.State == ODataReaderState.DeltaResourceSetStart) && this.inputContext.Model.IsUserModel())
			{
				scope.ResourceTypeValidator = new ResourceSetWithoutExpectedTypeValidator(scope.ResourceType);
			}
			if (scope.State == ODataReaderState.ResourceSetStart || scope.State == ODataReaderState.DeltaResourceSetStart)
			{
				scope.DerivedTypeValidator = this.CurrentScope.DerivedTypeValidator;
			}
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

		// Token: 0x06000805 RID: 2053 RVA: 0x0001308D File Offset: 0x0001128D
		protected void ReplaceScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000130A4 File Offset: 0x000112A4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected void PopScope(ODataReaderState state)
		{
			ODataReaderCore.Scope scope = this.scopes.Pop();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001308D File Offset: 0x0001128D
		protected void EndEntry(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000130C0 File Offset: 0x000112C0
		protected void ApplyResourceTypeNameFromPayload(string resourceTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			ODataTypeAnnotation odataTypeAnnotation;
			IEdmStructuredTypeReference edmStructuredTypeReference = (IEdmStructuredTypeReference)this.inputContext.MessageReaderSettings.Validator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, new bool?(true), null, this.CurrentResourceTypeReference, resourceTypeNameFromPayload, this.inputContext.Model, () => EdmTypeKind.Entity, out edmTypeKind, out odataTypeAnnotation);
			ODataResourceBase odataResourceBase = this.Item as ODataResourceBase;
			if (edmStructuredTypeReference != null)
			{
				IEdmStructuredType edmStructuredType = edmStructuredTypeReference.StructuredDefinition();
				odataResourceBase.TypeName = edmStructuredType.FullTypeName();
				if (odataTypeAnnotation != null)
				{
					odataResourceBase.TypeAnnotation = odataTypeAnnotation;
				}
			}
			else if (resourceTypeNameFromPayload != null)
			{
				odataResourceBase.TypeName = resourceTypeNameFromPayload;
			}
			this.CurrentResourceTypeReference = edmStructuredTypeReference;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001316A File Offset: 0x0001136A
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00013172 File Offset: 0x00011372
		protected virtual Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00013188 File Offset: 0x00011388
		protected void IncreaseResourceDepth()
		{
			this.currentResourceDepth++;
			if (this.currentResourceDepth > this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth)
			{
				throw new ODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth));
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000131E5 File Offset: 0x000113E5
		protected void DecreaseResourceDepth()
		{
			this.currentResourceDepth--;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000131F8 File Offset: 0x000113F8
		private bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataReaderState.ResourceSetStart:
				flag = this.ReadAtResourceSetStartImplementation();
				break;
			case ODataReaderState.ResourceSetEnd:
				flag = this.ReadAtResourceSetEndImplementation();
				break;
			case ODataReaderState.ResourceStart:
				this.IncreaseResourceDepth();
				flag = this.ReadAtResourceStartImplementation();
				break;
			case ODataReaderState.ResourceEnd:
				this.DecreaseResourceDepth();
				flag = this.ReadAtResourceEndImplementation();
				break;
			case ODataReaderState.NestedResourceInfoStart:
				flag = this.ReadAtNestedResourceInfoStartImplementation();
				break;
			case ODataReaderState.NestedResourceInfoEnd:
				flag = this.ReadAtNestedResourceInfoEndImplementation();
				break;
			case ODataReaderState.EntityReferenceLink:
				flag = this.ReadAtEntityReferenceLink();
				break;
			case ODataReaderState.Exception:
			case ODataReaderState.Completed:
				throw new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State));
			case ODataReaderState.Primitive:
				flag = this.ReadAtPrimitiveImplementation();
				break;
			case ODataReaderState.DeltaResourceSetStart:
				flag = this.ReadAtDeltaResourceSetStartImplementation();
				break;
			case ODataReaderState.DeltaResourceSetEnd:
				flag = this.ReadAtDeltaResourceSetEndImplementation();
				break;
			case ODataReaderState.DeletedResourceStart:
				flag = this.ReadAtDeletedResourceStartImplementation();
				break;
			case ODataReaderState.DeletedResourceEnd:
				flag = this.ReadAtDeletedResourceEndImplementation();
				break;
			case ODataReaderState.DeltaLink:
				flag = this.ReadAtDeltaLinkImplementation();
				break;
			case ODataReaderState.DeltaDeletedLink:
				flag = this.ReadAtDeltaDeletedLinkImplementation();
				break;
			case ODataReaderState.NestedProperty:
				flag = this.ReadAtNestedPropertyInfoImplementation();
				break;
			case ODataReaderState.Stream:
				flag = this.ReadAtStreamImplementation();
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00013348 File Offset: 0x00011548
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Exception, null, null));
				}
				throw;
			}
			return t;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00013388 File Offset: 0x00011588
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataReaderState.Exception || this.State == ODataReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
			if (this.State == ODataReaderState.Stream)
			{
				ODataReaderCore.StreamScope streamScope = this.CurrentScope as ODataReaderCore.StreamScope;
				if (streamScope.StreamingState != ODataReaderCore.StreamingState.Completed)
				{
					throw new ODataException(Strings.ODataReaderCore_ReadCalledWithOpenStream);
				}
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000133FA File Offset: 0x000115FA
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.inputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
				}
			}
			else if (this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x04000306 RID: 774
		private readonly ODataInputContext inputContext;

		// Token: 0x04000307 RID: 775
		private readonly bool readingResourceSet;

		// Token: 0x04000308 RID: 776
		private readonly bool readingDelta;

		// Token: 0x04000309 RID: 777
		private readonly Stack<ODataReaderCore.Scope> scopes = new Stack<ODataReaderCore.Scope>();

		// Token: 0x0400030A RID: 778
		private readonly IODataReaderWriterListener listener;

		// Token: 0x0400030B RID: 779
		private int currentResourceDepth;

		// Token: 0x02000303 RID: 771
		internal enum StreamingState
		{
			// Token: 0x04000D30 RID: 3376
			None,
			// Token: 0x04000D31 RID: 3377
			Streaming,
			// Token: 0x04000D32 RID: 3378
			Completed
		}

		// Token: 0x02000304 RID: 772
		protected internal class Scope
		{
			// Token: 0x06001DAD RID: 7597 RVA: 0x00057E7D File Offset: 0x0005607D
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			internal Scope(ODataReaderState state, ODataItem item, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.odataUri = odataUri;
			}

			// Token: 0x06001DAE RID: 7598 RVA: 0x00057E9A File Offset: 0x0005609A
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			internal Scope(ODataReaderState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmTypeReference expectedResourceTypeReference, ODataUri odataUri)
				: this(state, item, odataUri)
			{
				this.NavigationSource = navigationSource;
				this.ResourceTypeReference = expectedResourceTypeReference;
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001DAF RID: 7599 RVA: 0x00057EB5 File Offset: 0x000560B5
			internal ODataReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00057EBD File Offset: 0x000560BD
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00057EC5 File Offset: 0x000560C5
			internal ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00057ECD File Offset: 0x000560CD
			// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x00057ED5 File Offset: 0x000560D5
			internal IEdmNavigationSource NavigationSource { get; set; }

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00057EDE File Offset: 0x000560DE
			internal IEdmType ResourceType
			{
				get
				{
					if (this.ResourceTypeReference != null)
					{
						return this.ResourceTypeReference.Definition;
					}
					return null;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00057EF5 File Offset: 0x000560F5
			// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x00057EFD File Offset: 0x000560FD
			internal IEdmTypeReference ResourceTypeReference { get; set; }

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00057F06 File Offset: 0x00056106
			// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x00057F0E File Offset: 0x0005610E
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

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00057F17 File Offset: 0x00056117
			// (set) Token: 0x06001DBA RID: 7610 RVA: 0x00057F1F File Offset: 0x0005611F
			internal DerivedTypeValidator DerivedTypeValidator { get; set; }

			// Token: 0x04000D33 RID: 3379
			private readonly ODataReaderState state;

			// Token: 0x04000D34 RID: 3380
			private readonly ODataItem item;

			// Token: 0x04000D35 RID: 3381
			private readonly ODataUri odataUri;

			// Token: 0x04000D36 RID: 3382
			private ResourceSetWithoutExpectedTypeValidator resourceTypeValidator;
		}

		// Token: 0x02000305 RID: 773
		protected internal class StreamScope : ODataReaderCore.Scope
		{
			// Token: 0x06001DBB RID: 7611 RVA: 0x00057F28 File Offset: 0x00056128
			internal StreamScope(ODataReaderState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmTypeReference expectedResourceType, ODataUri odataUri)
				: base(state, item, navigationSource, expectedResourceType, odataUri)
			{
				this.StreamingState = ODataReaderCore.StreamingState.None;
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001DBC RID: 7612 RVA: 0x00057F3E File Offset: 0x0005613E
			// (set) Token: 0x06001DBD RID: 7613 RVA: 0x00057F46 File Offset: 0x00056146
			internal ODataReaderCore.StreamingState StreamingState { get; set; }
		}
	}
}
