using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000092 RID: 146
	internal abstract class ODataReaderCore : ODataReader
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0000F820 File Offset: 0x0000DA20
		protected ODataReaderCore(ODataInputContext inputContext, bool readingResourceSet, bool readingDelta, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.readingResourceSet = readingResourceSet;
			this.readingDelta = readingDelta;
			this.listener = listener;
			this.currentResourceDepth = 0;
			if (this.readingResourceSet && this.inputContext.Model.IsUserModel())
			{
				this.resourceSetValidator = new ResourceSetWithoutExpectedTypeValidator();
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000F887 File Offset: 0x0000DA87
		public sealed override ODataReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public sealed override ODataItem Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000F8C1 File Offset: 0x0000DAC1
		protected ODataResource CurrentResource
		{
			get
			{
				return (ODataResource)this.Item;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000F8CE File Offset: 0x0000DACE
		protected ODataResourceSet CurrentResourceSet
		{
			get
			{
				return (ODataResourceSet)this.Item;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000F8DB File Offset: 0x0000DADB
		protected int CurrentResourceDepth
		{
			get
			{
				return this.currentResourceDepth;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000F8E3 File Offset: 0x0000DAE3
		protected ODataNestedResourceInfo CurrentNestedResourceInfo
		{
			get
			{
				return (ODataNestedResourceInfo)this.Item;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		protected ODataEntityReferenceLink CurrentEntityReferenceLink
		{
			get
			{
				return (ODataEntityReferenceLink)this.Item;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000F900 File Offset: 0x0000DB00
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000F91F File Offset: 0x0000DB1F
		protected IEdmStructuredType CurrentResourceType
		{
			get
			{
				return this.scopes.Peek().ResourceType;
			}
			set
			{
				this.scopes.Peek().ResourceType = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000F934 File Offset: 0x0000DB34
		protected IEdmNavigationSource CurrentNavigationSource
		{
			get
			{
				return this.scopes.Peek().NavigationSource;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000F953 File Offset: 0x0000DB53
		protected ODataReaderCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000F960 File Offset: 0x0000DB60
		protected Stack<ODataReaderCore.Scope> Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000F968 File Offset: 0x0000DB68
		protected ODataReaderCore.Scope ParentScope
		{
			get
			{
				return Enumerable.First<ODataReaderCore.Scope>(Enumerable.Skip<ODataReaderCore.Scope>(this.scopes, 1));
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000F97B File Offset: 0x0000DB7B
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000F990 File Offset: 0x0000DB90
		protected ODataReaderCore.Scope ExpandedLinkContentParentScope
		{
			get
			{
				if (this.scopes.Count > 1)
				{
					ODataReaderCore.Scope scope = Enumerable.First<ODataReaderCore.Scope>(Enumerable.Skip<ODataReaderCore.Scope>(this.scopes, 1));
					if (scope.State == ODataReaderState.NestedResourceInfoStart)
					{
						return scope;
					}
				}
				return null;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000F9C9 File Offset: 0x0000DBC9
		protected bool IsExpandedLinkContent
		{
			get
			{
				return this.ExpandedLinkContentParentScope != null;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		protected bool ReadingResourceSet
		{
			get
			{
				return this.readingResourceSet;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.readingDelta || this.listener != null;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000F9F1 File Offset: 0x0000DBF1
		protected ResourceSetWithoutExpectedTypeValidator CurrentResourceSetValidator
		{
			get
			{
				if (this.scopes.Count != 3)
				{
					return null;
				}
				return this.resourceSetValidator;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000FA09 File Offset: 0x0000DC09
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000FA24 File Offset: 0x0000DC24
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

		// Token: 0x060005AF RID: 1455
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060005B0 RID: 1456
		protected abstract bool ReadAtResourceSetStartImplementation();

		// Token: 0x060005B1 RID: 1457
		protected abstract bool ReadAtResourceSetEndImplementation();

		// Token: 0x060005B2 RID: 1458
		protected abstract bool ReadAtResourceStartImplementation();

		// Token: 0x060005B3 RID: 1459
		protected abstract bool ReadAtResourceEndImplementation();

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000FA90 File Offset: 0x0000DC90
		protected virtual bool ReadAtPrimitiveImplementation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005B5 RID: 1461
		protected abstract bool ReadAtNestedResourceInfoStartImplementation();

		// Token: 0x060005B6 RID: 1462
		protected abstract bool ReadAtNestedResourceInfoEndImplementation();

		// Token: 0x060005B7 RID: 1463
		protected abstract bool ReadAtEntityReferenceLink();

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000FA97 File Offset: 0x0000DC97
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

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
		protected void ReplaceScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected void PopScope(ODataReaderState state)
		{
			ODataReaderCore.Scope scope = this.scopes.Pop();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
		protected void EndEntry(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000FB08 File Offset: 0x0000DD08
		protected void ApplyResourceTypeNameFromPayload(string resourceTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			ODataTypeAnnotation odataTypeAnnotation;
			IEdmStructuredTypeReference edmStructuredTypeReference = (IEdmStructuredTypeReference)this.inputContext.MessageReaderSettings.Validator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, new bool?(true), null, this.CurrentResourceType.ToTypeReference(), resourceTypeNameFromPayload, this.inputContext.Model, () => EdmTypeKind.Entity, out edmTypeKind, out odataTypeAnnotation);
			IEdmStructuredType edmStructuredType = null;
			ODataResource currentResource = this.CurrentResource;
			if (edmStructuredTypeReference != null)
			{
				edmStructuredType = edmStructuredTypeReference.StructuredDefinition();
				currentResource.TypeName = edmStructuredType.FullTypeName();
				if (odataTypeAnnotation != null)
				{
					currentResource.TypeAnnotation = odataTypeAnnotation;
				}
			}
			else if (resourceTypeNameFromPayload != null)
			{
				currentResource.TypeName = resourceTypeNameFromPayload;
			}
			this.CurrentResourceType = edmStructuredType;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		protected void IncreaseResourceDepth()
		{
			this.currentResourceDepth++;
			if (this.currentResourceDepth > this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth)
			{
				throw new ODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth));
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000FC19 File Offset: 0x0000DE19
		protected void DecreaseResourceDepth()
		{
			this.currentResourceDepth--;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000FC2C File Offset: 0x0000DE2C
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
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000FCFC File Offset: 0x0000DEFC
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

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000FD40 File Offset: 0x0000DF40
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataReaderState.Exception || this.State == ODataReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000FD7D File Offset: 0x0000DF7D
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x040002A6 RID: 678
		private readonly ODataInputContext inputContext;

		// Token: 0x040002A7 RID: 679
		private readonly bool readingResourceSet;

		// Token: 0x040002A8 RID: 680
		private readonly bool readingDelta;

		// Token: 0x040002A9 RID: 681
		private readonly Stack<ODataReaderCore.Scope> scopes = new Stack<ODataReaderCore.Scope>();

		// Token: 0x040002AA RID: 682
		private readonly IODataReaderWriterListener listener;

		// Token: 0x040002AB RID: 683
		private readonly ResourceSetWithoutExpectedTypeValidator resourceSetValidator;

		// Token: 0x040002AC RID: 684
		private int currentResourceDepth;

		// Token: 0x02000285 RID: 645
		protected internal class Scope
		{
			// Token: 0x060017E1 RID: 6113 RVA: 0x00047A2A File Offset: 0x00045C2A
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			internal Scope(ODataReaderState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.ResourceType = expectedResourceType;
				this.NavigationSource = navigationSource;
				this.odataUri = odataUri;
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00047A57 File Offset: 0x00045C57
			internal ODataReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00047A5F File Offset: 0x00045C5F
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00047A67 File Offset: 0x00045C67
			internal ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00047A6F File Offset: 0x00045C6F
			// (set) Token: 0x060017E6 RID: 6118 RVA: 0x00047A77 File Offset: 0x00045C77
			internal IEdmNavigationSource NavigationSource { get; set; }

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00047A80 File Offset: 0x00045C80
			// (set) Token: 0x060017E8 RID: 6120 RVA: 0x00047A88 File Offset: 0x00045C88
			internal IEdmStructuredType ResourceType { get; set; }

			// Token: 0x04000B53 RID: 2899
			private readonly ODataReaderState state;

			// Token: 0x04000B54 RID: 2900
			private readonly ODataItem item;

			// Token: 0x04000B55 RID: 2901
			private readonly ODataUri odataUri;
		}
	}
}
