using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.OData.Client.Annotation;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E8 RID: 232
	[SuppressMessage("Microsoft.Maintainability", "CA1506", Justification = "Central class of the API, likely to have many cross-references")]
	public class DataServiceContext
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00020CF6 File Offset: 0x0001EEF6
		public DataServiceContext()
			: this(null)
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00020CFF File Offset: 0x0001EEFF
		public DataServiceContext(Uri serviceRoot)
			: this(serviceRoot, ODataProtocolVersion.V4)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00020D09 File Offset: 0x0001EF09
		public DataServiceContext(Uri serviceRoot, ODataProtocolVersion maxProtocolVersion)
			: this(serviceRoot, maxProtocolVersion, DataServiceContext.ClientEdmModelCache.GetModel(maxProtocolVersion))
		{
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00020D1C File Offset: 0x0001EF1C
		internal DataServiceContext(Uri serviceRoot, ODataProtocolVersion maxProtocolVersion, ClientEdmModel model)
		{
			this.model = model;
			this.baseUriResolver = UriResolver.CreateFromBaseUri(serviceRoot, "serviceRoot");
			this.maxProtocolVersion = Util.CheckEnumerationValue(maxProtocolVersion, "maxProtocolVersion");
			this.entityParameterSendOption = EntityParameterSendOption.SendFullProperties;
			this.mergeOption = MergeOption.AppendOnly;
			this.entityTracker = new EntityTracker(model);
			this.MaxProtocolVersionAsVersion = Util.GetVersionFromMaxProtocolVersion(maxProtocolVersion);
			this.formatTracker = new DataServiceClientFormat(this);
			this.urlKeyDelimiter = DataServiceUrlKeyDelimiter.Parentheses;
			this.Configurations = new DataServiceClientConfigurations(this);
			this.httpStack = HttpStack.Auto;
			this.UsingDataServiceCollection = false;
			this.UsePostTunneling = false;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060007F4 RID: 2036 RVA: 0x00020E10 File Offset: 0x0001F010
		// (remove) Token: 0x060007F5 RID: 2037 RVA: 0x00020E48 File Offset: 0x0001F048
		public event EventHandler<SendingRequest2EventArgs> SendingRequest2;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060007F6 RID: 2038 RVA: 0x00020E7D File Offset: 0x0001F07D
		// (remove) Token: 0x060007F7 RID: 2039 RVA: 0x00020E86 File Offset: 0x0001F086
		public event EventHandler<BuildingRequestEventArgs> BuildingRequest
		{
			add
			{
				this.InnerBuildingRequest += value;
			}
			remove
			{
				this.InnerBuildingRequest -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060007F8 RID: 2040 RVA: 0x00020E90 File Offset: 0x0001F090
		// (remove) Token: 0x060007F9 RID: 2041 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public event EventHandler<ReceivingResponseEventArgs> ReceivingResponse;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060007FA RID: 2042 RVA: 0x00020F00 File Offset: 0x0001F100
		// (remove) Token: 0x060007FB RID: 2043 RVA: 0x00020F38 File Offset: 0x0001F138
		internal event EventHandler<SaveChangesEventArgs> ChangesSaved;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060007FC RID: 2044 RVA: 0x00020F70 File Offset: 0x0001F170
		// (remove) Token: 0x060007FD RID: 2045 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		private event EventHandler<SendingRequestEventArgs> InnerSendingRequest;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060007FE RID: 2046 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		// (remove) Token: 0x060007FF RID: 2047 RVA: 0x00021018 File Offset: 0x0001F218
		private event EventHandler<BuildingRequestEventArgs> InnerBuildingRequest;

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002104D File Offset: 0x0001F24D
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0002105A File Offset: 0x0001F25A
		public Func<string, Uri> ResolveEntitySet
		{
			get
			{
				return this.baseUriResolver.ResolveEntitySet;
			}
			set
			{
				this.baseUriResolver = this.baseUriResolver.CloneWithOverrideValue(value);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002106E File Offset: 0x0001F26E
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002107B File Offset: 0x0001F27B
		public Uri BaseUri
		{
			get
			{
				return this.baseUriResolver.RawBaseUriValue;
			}
			set
			{
				if (this.baseUriResolver == null)
				{
					this.baseUriResolver = UriResolver.CreateFromBaseUri(value, "serviceRoot");
					return;
				}
				this.baseUriResolver = this.baseUriResolver.CloneWithOverrideValue(value, null);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000210AA File Offset: 0x0001F2AA
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x000210B2 File Offset: 0x0001F2B2
		public DataServiceResponsePreference AddAndUpdateResponsePreference
		{
			get
			{
				return this.addAndUpdateResponsePreference;
			}
			set
			{
				if (value != DataServiceResponsePreference.None)
				{
					this.EnsureMinimumProtocolVersionV3();
				}
				this.addAndUpdateResponsePreference = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000210C4 File Offset: 0x0001F2C4
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000210CC File Offset: 0x0001F2CC
		public ODataProtocolVersion MaxProtocolVersion
		{
			get
			{
				return this.maxProtocolVersion;
			}
			internal set
			{
				this.maxProtocolVersion = value;
				this.MaxProtocolVersionAsVersion = Util.GetVersionFromMaxProtocolVersion(this.maxProtocolVersion);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x000210E6 File Offset: 0x0001F2E6
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x000210EE File Offset: 0x0001F2EE
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x000210F7 File Offset: 0x0001F2F7
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x000210FF File Offset: 0x0001F2FF
		public EntityParameterSendOption EntityParameterSendOption
		{
			get
			{
				return this.entityParameterSendOption;
			}
			set
			{
				this.entityParameterSendOption = Util.CheckEnumerationValue(value, "EntityParameterSendOption");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00021112 File Offset: 0x0001F312
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0002111A File Offset: 0x0001F31A
		public MergeOption MergeOption
		{
			get
			{
				return this.mergeOption;
			}
			set
			{
				this.mergeOption = Util.CheckEnumerationValue(value, "MergeOption");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0002112D File Offset: 0x0001F32D
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00021135 File Offset: 0x0001F335
		public bool ApplyingChanges
		{
			get
			{
				return this.applyingChanges;
			}
			internal set
			{
				this.applyingChanges = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0002113E File Offset: 0x0001F33E
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x00021146 File Offset: 0x0001F346
		public Func<Type, string> ResolveName
		{
			get
			{
				return this.resolveName;
			}
			set
			{
				this.resolveName = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0002114F File Offset: 0x0001F34F
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00021157 File Offset: 0x0001F357
		public Func<string, Type> ResolveType
		{
			get
			{
				return this.resolveType;
			}
			set
			{
				this.resolveType = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00021160 File Offset: 0x0001F360
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00021168 File Offset: 0x0001F368
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw Error.ArgumentOutOfRange("Timeout");
				}
				this.timeout = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00021180 File Offset: 0x0001F380
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00021188 File Offset: 0x0001F388
		public bool UsePostTunneling
		{
			get
			{
				return this.postTunneling;
			}
			set
			{
				this.postTunneling = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00021191 File Offset: 0x0001F391
		public ReadOnlyCollection<LinkDescriptor> Links
		{
			get
			{
				return new ReadOnlyCollection<LinkDescriptor>(this.entityTracker.Links.OrderBy((LinkDescriptor l) => l.ChangeOrder).ToList<LinkDescriptor>());
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000211CC File Offset: 0x0001F3CC
		public ReadOnlyCollection<EntityDescriptor> Entities
		{
			get
			{
				return new ReadOnlyCollection<EntityDescriptor>(this.entityTracker.Entities.OrderBy((EntityDescriptor d) => d.ChangeOrder).ToList<EntityDescriptor>());
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00021207 File Offset: 0x0001F407
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0002120F File Offset: 0x0001F40F
		public SaveChangesOptions SaveChangesDefaultOptions
		{
			get
			{
				return this.saveChangesDefaultOptions;
			}
			set
			{
				this.ValidateSaveChangesOptions(value);
				this.saveChangesDefaultOptions = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0002121F File Offset: 0x0001F41F
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00021227 File Offset: 0x0001F427
		public bool IgnoreResourceNotFoundException
		{
			get
			{
				return this.ignoreResourceNotFoundException;
			}
			set
			{
				this.ignoreResourceNotFoundException = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00021230 File Offset: 0x0001F430
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00021238 File Offset: 0x0001F438
		public DataServiceClientConfigurations Configurations { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00021241 File Offset: 0x0001F441
		public DataServiceClientFormat Format
		{
			get
			{
				return this.formatTracker;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00021249 File Offset: 0x0001F449
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00021251 File Offset: 0x0001F451
		public DataServiceUrlKeyDelimiter UrlKeyDelimiter
		{
			get
			{
				return this.urlKeyDelimiter;
			}
			set
			{
				Util.CheckArgumentNull<DataServiceUrlKeyDelimiter>(value, "value");
				this.urlKeyDelimiter = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00021266 File Offset: 0x0001F466
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0002126E File Offset: 0x0001F46E
		public EntityTracker EntityTracker
		{
			get
			{
				return this.entityTracker;
			}
			set
			{
				this.entityTracker = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00021277 File Offset: 0x0001F477
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0002127F File Offset: 0x0001F47F
		public bool DisableInstanceAnnotationMaterialization { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00021288 File Offset: 0x0001F488
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00021290 File Offset: 0x0001F490
		public bool EnableWritingODataAnnotationWithoutPrefix { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00021299 File Offset: 0x0001F499
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x000212A1 File Offset: 0x0001F4A1
		internal UndeclaredPropertyBehavior UndeclaredPropertyBehavior
		{
			get
			{
				return this.undeclaredPropertyBehavior;
			}
			set
			{
				this.undeclaredPropertyBehavior = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x000212AA File Offset: 0x0001F4AA
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x000212B2 File Offset: 0x0001F4B2
		internal bool UseDefaultCredentials { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x000212BB File Offset: 0x0001F4BB
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x000212C3 File Offset: 0x0001F4C3
		internal HttpStack HttpStack
		{
			get
			{
				return this.httpStack;
			}
			set
			{
				this.httpStack = Util.CheckEnumerationValue(value, "HttpStack");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000212D6 File Offset: 0x0001F4D6
		internal bool HasSendingRequest2EventHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.SendingRequest2 != null;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000212E1 File Offset: 0x0001F4E1
		internal bool HasBuildingRequestEventHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.InnerBuildingRequest != null;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00021241 File Offset: 0x0001F441
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x000212EC File Offset: 0x0001F4EC
		internal DataServiceClientFormat FormatTracker
		{
			get
			{
				return this.formatTracker;
			}
			set
			{
				this.formatTracker = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000212F5 File Offset: 0x0001F4F5
		internal UriResolver BaseUriResolver
		{
			get
			{
				return this.baseUriResolver;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000212FD File Offset: 0x0001F4FD
		internal ClientEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00021305 File Offset: 0x0001F505
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0002130D File Offset: 0x0001F50D
		internal bool UsingDataServiceCollection { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00021316 File Offset: 0x0001F516
		internal WeakDictionary<object, IDictionary<string, object>> InstanceAnnotations
		{
			get
			{
				return this.instanceAnnotations;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002131E File Offset: 0x0001F51E
		internal WeakDictionary<object, IList<IEdmVocabularyAnnotation>> MetadataAnnotationsDictionary
		{
			get
			{
				return this.metadataAnnotationsDictionary;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00021328 File Offset: 0x0001F528
		public bool TryGetAnnotation<TResult>(object source, string term, string qualifier, out TResult annotation)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<string>(term, "term");
			if (qualifier != null)
			{
				return AnnotationHelper.TryGetMetadataAnnotation<TResult>(this, source, term, qualifier, out annotation);
			}
			IDictionary<string, object> dictionary;
			object obj;
			if (this.InstanceAnnotations.TryGetValue(source, out dictionary) && dictionary.TryGetValue(term, out obj))
			{
				if (obj is TResult)
				{
					annotation = (TResult)((object)obj);
					return true;
				}
				if (ClientTypeUtil.CanAssignNull(typeof(TResult)) && obj == null)
				{
					annotation = default(TResult);
					return true;
				}
			}
			return AnnotationHelper.TryGetMetadataAnnotation<TResult>(this, source, term, null, out annotation);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000213B9 File Offset: 0x0001F5B9
		public bool TryGetAnnotation<TResult>(object source, string term, out TResult annotation)
		{
			return this.TryGetAnnotation<TResult>(source, term, null, out annotation);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public bool TryGetAnnotation<TFunc, TResult>(Expression<TFunc> expression, string term, string qualifier, out TResult annotation)
		{
			Util.CheckArgumentNull<Expression<TFunc>>(expression, "expression");
			Util.CheckArgumentNull<string>(term, "term");
			Expression expression2 = null;
			MemberInfo memberInfo = null;
			object obj = null;
			if (expression.Body.NodeType == ExpressionType.MemberAccess)
			{
				MemberExpression memberExpression = (MemberExpression)expression.Body;
				expression2 = memberExpression.Expression;
				memberInfo = memberExpression.Member;
			}
			else if (expression.Body.NodeType == ExpressionType.Call)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression.Body;
				memberInfo = methodCallExpression.Method;
				expression2 = methodCallExpression.Object;
			}
			if (expression2 != null)
			{
				try
				{
					obj = Expression.Lambda(expression2, new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]);
				}
				catch (InvalidCastException)
				{
					annotation = default(TResult);
					return false;
				}
			}
			object obj2 = new Tuple<object, MemberInfo>(obj, memberInfo);
			if (obj2 != null)
			{
				return this.TryGetAnnotation<TResult>(obj2, term, qualifier, out annotation);
			}
			annotation = default(TResult);
			return false;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000214B4 File Offset: 0x0001F6B4
		public bool TryGetAnnotation<TFunc, TResult>(Expression<TFunc> expression, string term, out TResult annotation)
		{
			return this.TryGetAnnotation<TFunc, TResult>(expression, term, null, out annotation);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000214C0 File Offset: 0x0001F6C0
		public EntityDescriptor GetEntityDescriptor(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			return this.entityTracker.TryGetEntityDescriptor(entity);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000214DA File Offset: 0x0001F6DA
		public LinkDescriptor GetLinkDescriptor(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			Util.CheckArgumentNull<object>(target, "target");
			return this.entityTracker.TryGetLinkDescriptor(source, sourceProperty, target);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00021510 File Offset: 0x0001F710
		public void CancelRequest(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			BaseAsyncResult baseAsyncResult = asyncResult as BaseAsyncResult;
			if (baseAsyncResult == null || this != baseAsyncResult.Source)
			{
				object obj = null;
				if (baseAsyncResult != null)
				{
					DataServiceQuery dataServiceQuery = baseAsyncResult.Source as DataServiceQuery;
					if (dataServiceQuery != null)
					{
						DataServiceQueryProvider dataServiceQueryProvider = dataServiceQuery.Provider as DataServiceQueryProvider;
						if (dataServiceQueryProvider != null)
						{
							obj = dataServiceQueryProvider.Context;
						}
					}
				}
				if (this != obj)
				{
					throw Error.Argument(Strings.Context_DidNotOriginateAsync, "asyncResult");
				}
			}
			if (!baseAsyncResult.IsCompletedInternally)
			{
				baseAsyncResult.SetAborted();
				ODataRequestMessageWrapper abortable = baseAsyncResult.Abortable;
				if (abortable != null)
				{
					abortable.Abort();
				}
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000215A0 File Offset: 0x0001F7A0
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads", Justification = "required for this feature")]
		public DataServiceQuery<T> CreateQuery<T>(string entitySetName)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			DataServiceContext.ValidateEntitySetName(ref entitySetName);
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(typeof(IOrderedQueryable<T>), null, Expression.Constant(entitySetName), typeof(T), null, CountOption.None, null, null, null, null);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(resourceSetExpression, new DataServiceQueryProvider(this));
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000215F4 File Offset: 0x0001F7F4
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads", Justification = "required for this feature")]
		public DataServiceQuery<T> CreateQuery<T>(string resourcePath, bool isComposable)
		{
			Util.CheckArgumentNullAndEmpty(resourcePath, "entitySetName");
			DataServiceContext.ValidateEntitySetName(ref resourcePath);
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(typeof(IOrderedQueryable<T>), null, Expression.Constant(resourcePath), typeof(T), null, CountOption.None, null, null, null, null);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(resourceSetExpression, new DataServiceQueryProvider(this), isComposable);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002164C File Offset: 0x0001F84C
		public DataServiceQuery<T> CreateFunctionQuery<T>()
		{
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(typeof(IOrderedQueryable<T>), null, null, typeof(T), null, CountOption.None, null, null, null, null);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(resourceSetExpression, new DataServiceQueryProvider(this));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00021688 File Offset: 0x0001F888
		public DataServiceQuery<T> CreateFunctionQuery<T>(string path, string functionName, bool isComposable, params UriOperationParameter[] parameters)
		{
			Dictionary<string, string> dictionary = this.SerializeOperationParameters(parameters);
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(typeof(IOrderedQueryable<T>), null, Expression.Constant(path), typeof(T), null, CountOption.None, null, null, null, null, functionName, dictionary, false);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(resourceSetExpression, new DataServiceQueryProvider(this), isComposable);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000216D8 File Offset: 0x0001F8D8
		public DataServiceQuerySingle<T> CreateFunctionQuerySingle<T>(string path, string functionName, bool isComposable, params UriOperationParameter[] parameters)
		{
			Dictionary<string, string> dictionary = this.SerializeOperationParameters(parameters);
			SingletonResourceExpression singletonResourceExpression = new SingletonResourceExpression(typeof(IOrderedQueryable<T>), null, Expression.Constant(path), typeof(T), null, CountOption.None, null, null, null, null, functionName, dictionary, false);
			return new DataServiceQuerySingle<T>(new DataServiceQuery<T>.DataServiceOrderedQuery(singletonResourceExpression, new DataServiceQueryProvider(this)), isComposable);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002172C File Offset: 0x0001F92C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "required for this feature")]
		[SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads", Justification = "required for this feature")]
		public DataServiceQuery<T> CreateSingletonQuery<T>(string singletonName)
		{
			Util.CheckArgumentNullAndEmpty(singletonName, "singletonName");
			DataServiceContext.ValidateEntitySetName(ref singletonName);
			SingletonResourceExpression singletonResourceExpression = new SingletonResourceExpression(typeof(IOrderedQueryable<T>), null, Expression.Constant(singletonName), typeof(T), null, CountOption.None, null, null, null, null);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(singletonResourceExpression, new DataServiceQueryProvider(this));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00021780 File Offset: 0x0001F980
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "required for this feature")]
		public Uri GetMetadataUri()
		{
			return UriUtil.CreateUri(UriUtil.UriToString(this.BaseUriResolver.GetBaseUriWithSlash()) + "$metadata", UriKind.Absolute);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000217AF File Offset: 0x0001F9AF
		public IAsyncResult BeginLoadProperty(object entity, string propertyName, AsyncCallback callback, object state)
		{
			return this.BeginLoadProperty(entity, propertyName, null, callback, state);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000217BD File Offset: 0x0001F9BD
		public Task<QueryOperationResponse> LoadPropertyAsync(object entity, string propertyName)
		{
			return Task<QueryOperationResponse>.Factory.FromAsync<object, string>(new Func<object, string, AsyncCallback, object, IAsyncResult>(this.BeginLoadProperty), new Func<IAsyncResult, QueryOperationResponse>(this.EndLoadProperty), entity, propertyName, null);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000217E4 File Offset: 0x0001F9E4
		public IAsyncResult BeginLoadProperty(object entity, string propertyName, Uri nextLinkUri, AsyncCallback callback, object state)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, callback, state, nextLinkUri, null);
			loadPropertyResult.BeginExecuteQuery();
			return loadPropertyResult;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00021807 File Offset: 0x0001FA07
		public Task<QueryOperationResponse> LoadPropertyAsync(object entity, string propertyName, Uri nextLinkUri)
		{
			return Task<QueryOperationResponse>.Factory.FromAsync<object, string, Uri>(new Func<object, string, Uri, AsyncCallback, object, IAsyncResult>(this.BeginLoadProperty), new Func<IAsyncResult, QueryOperationResponse>(this.EndLoadProperty), entity, propertyName, nextLinkUri, null);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00021830 File Offset: 0x0001FA30
		public IAsyncResult BeginLoadProperty(object entity, string propertyName, DataServiceQueryContinuation continuation, AsyncCallback callback, object state)
		{
			Util.CheckArgumentNull<DataServiceQueryContinuation>(continuation, "continuation");
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, callback, state, null, continuation);
			loadPropertyResult.BeginExecuteQuery();
			return loadPropertyResult;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002185F File Offset: 0x0001FA5F
		public Task<QueryOperationResponse> LoadPropertyAsync(object entity, string propertyName, DataServiceQueryContinuation continuation)
		{
			return Task<QueryOperationResponse>.Factory.FromAsync<object, string, DataServiceQueryContinuation>(new Func<object, string, DataServiceQueryContinuation, AsyncCallback, object, IAsyncResult>(this.BeginLoadProperty), new Func<IAsyncResult, QueryOperationResponse>(this.EndLoadProperty), entity, propertyName, continuation, null);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00021888 File Offset: 0x0001FA88
		public QueryOperationResponse EndLoadProperty(IAsyncResult asyncResult)
		{
			LoadPropertyResult loadPropertyResult = BaseAsyncResult.EndExecute<LoadPropertyResult>(this, "LoadProperty", asyncResult);
			return loadPropertyResult.LoadProperty();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000218A8 File Offset: 0x0001FAA8
		public QueryOperationResponse LoadProperty(object entity, string propertyName)
		{
			return this.LoadProperty(entity, propertyName, null);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000218B4 File Offset: 0x0001FAB4
		public QueryOperationResponse LoadProperty(object entity, string propertyName, Uri nextLinkUri)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, null, null, nextLinkUri, null);
			loadPropertyResult.ExecuteQuery();
			return loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000218DC File Offset: 0x0001FADC
		public QueryOperationResponse LoadProperty(object entity, string propertyName, DataServiceQueryContinuation continuation)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, null, null, null, continuation);
			loadPropertyResult.ExecuteQuery();
			return loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00021904 File Offset: 0x0001FB04
		[SuppressMessage("Microsoft.Design", "CA1011", Justification = "allows compiler to infer 'T'")]
		public QueryOperationResponse<T> LoadProperty<T>(object entity, string propertyName, DataServiceQueryContinuation<T> continuation)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, null, null, null, continuation);
			loadPropertyResult.ExecuteQuery();
			return (QueryOperationResponse<T>)loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00021930 File Offset: 0x0001FB30
		public Uri GetReadStreamUri(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			return entityDescriptor.ReadStreamUri;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002195C File Offset: 0x0001FB5C
		public Uri GetReadStreamUri(object entity, string name)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNullAndEmpty(name, "name");
			this.EnsureMinimumProtocolVersionV3();
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			StreamDescriptor streamDescriptor;
			if (entityDescriptor.TryGetNamedStreamInfo(name, out streamDescriptor))
			{
				return streamDescriptor.SelfLink ?? streamDescriptor.EditLink;
			}
			return null;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000219B0 File Offset: 0x0001FBB0
		public IAsyncResult BeginGetReadStream(object entity, DataServiceRequestArgs args, AsyncCallback callback, object state)
		{
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, callback, state, null);
			getReadStreamResult.Begin();
			return getReadStreamResult;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000219D1 File Offset: 0x0001FBD1
		public Task<DataServiceStreamResponse> GetReadStreamAsync(object entity, DataServiceRequestArgs args)
		{
			return Task<DataServiceStreamResponse>.Factory.FromAsync<object, DataServiceRequestArgs>(new Func<object, DataServiceRequestArgs, AsyncCallback, object, IAsyncResult>(this.BeginGetReadStream), new Func<IAsyncResult, DataServiceStreamResponse>(this.EndGetReadStream), entity, args, null);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000219F8 File Offset: 0x0001FBF8
		public IAsyncResult BeginGetReadStream(object entity, string name, DataServiceRequestArgs args, AsyncCallback callback, object state)
		{
			Util.CheckArgumentNullAndEmpty(name, "name");
			this.EnsureMinimumProtocolVersionV3();
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, callback, state, name);
			getReadStreamResult.Begin();
			return getReadStreamResult;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00021A2B File Offset: 0x0001FC2B
		public Task<DataServiceStreamResponse> GetReadStreamAsync(object entity, string name, DataServiceRequestArgs args)
		{
			return Task<DataServiceStreamResponse>.Factory.FromAsync<object, string, DataServiceRequestArgs>(new Func<object, string, DataServiceRequestArgs, AsyncCallback, object, IAsyncResult>(this.BeginGetReadStream), new Func<IAsyncResult, DataServiceStreamResponse>(this.EndGetReadStream), entity, name, args, null);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00021A54 File Offset: 0x0001FC54
		public DataServiceStreamResponse EndGetReadStream(IAsyncResult asyncResult)
		{
			GetReadStreamResult getReadStreamResult = BaseAsyncResult.EndExecute<GetReadStreamResult>(this, "GetReadStream", asyncResult);
			return getReadStreamResult.End();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00021A74 File Offset: 0x0001FC74
		public DataServiceStreamResponse GetReadStream(object entity)
		{
			DataServiceRequestArgs dataServiceRequestArgs = new DataServiceRequestArgs();
			return this.GetReadStream(entity, dataServiceRequestArgs);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00021A90 File Offset: 0x0001FC90
		public DataServiceStreamResponse GetReadStream(object entity, string acceptContentType)
		{
			Util.CheckArgumentNullAndEmpty(acceptContentType, "acceptContentType");
			return this.GetReadStream(entity, new DataServiceRequestArgs
			{
				AcceptContentType = acceptContentType
			});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public DataServiceStreamResponse GetReadStream(object entity, DataServiceRequestArgs args)
		{
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, null, null, null);
			return getReadStreamResult.Execute();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		public DataServiceStreamResponse GetReadStream(object entity, string name, DataServiceRequestArgs args)
		{
			Util.CheckArgumentNullAndEmpty(name, "name");
			this.EnsureMinimumProtocolVersionV3();
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, null, null, name);
			return getReadStreamResult.Execute();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00021B10 File Offset: 0x0001FD10
		public void SetSaveStream(object entity, Stream stream, bool closeStream, string contentType, string slug)
		{
			Util.CheckArgumentNull<string>(contentType, "contentType");
			Util.CheckArgumentNull<string>(slug, "slug");
			this.SetSaveStream(entity, stream, closeStream, new DataServiceRequestArgs
			{
				ContentType = contentType,
				Slug = slug
			});
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00021B58 File Offset: 0x0001FD58
		public void SetSaveStream(object entity, Stream stream, bool closeStream, DataServiceRequestArgs args)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNull<Stream>(stream, "stream");
			Util.CheckArgumentNull<DataServiceRequestArgs>(args, "args");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(entity.GetType()));
			if (clientTypeAnnotation.MediaDataMember != null)
			{
				throw new ArgumentException(Strings.Context_SetSaveStreamOnMediaEntryProperty(clientTypeAnnotation.ElementTypeName), "entity");
			}
			entityDescriptor.SaveStream = new DataServiceSaveStream(stream, closeStream, args);
			EntityStates state = entityDescriptor.State;
			if (state != EntityStates.Unchanged)
			{
				if (state == EntityStates.Added)
				{
					entityDescriptor.StreamState = EntityStates.Added;
					return;
				}
				if (state != EntityStates.Modified)
				{
					throw new DataServiceClientException(Strings.Context_SetSaveStreamOnInvalidEntityState(Enum.GetName(typeof(EntityStates), entityDescriptor.State)));
				}
			}
			entityDescriptor.StreamState = EntityStates.Modified;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00021C2C File Offset: 0x0001FE2C
		public void SetSaveStream(object entity, string name, Stream stream, bool closeStream, string contentType)
		{
			Util.CheckArgumentNullAndEmpty(contentType, "contentType");
			this.SetSaveStream(entity, name, stream, closeStream, new DataServiceRequestArgs
			{
				ContentType = contentType
			});
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00021C60 File Offset: 0x0001FE60
		public void SetSaveStream(object entity, string name, Stream stream, bool closeStream, DataServiceRequestArgs args)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNullAndEmpty(name, "name");
			Util.CheckArgumentNull<Stream>(stream, "stream");
			Util.CheckArgumentNull<DataServiceRequestArgs>(args, "args");
			this.EnsureMinimumProtocolVersionV3();
			if (string.IsNullOrEmpty(args.ContentType))
			{
				throw Error.Argument(Strings.Context_ContentTypeRequiredForNamedStream, "args");
			}
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			if (entityDescriptor.State == EntityStates.Deleted)
			{
				throw new DataServiceClientException(Strings.Context_SetSaveStreamOnInvalidEntityState(Enum.GetName(typeof(EntityStates), entityDescriptor.State)));
			}
			StreamDescriptor streamDescriptor = entityDescriptor.AddStreamInfoIfNotPresent(name);
			streamDescriptor.SaveStream = new DataServiceSaveStream(stream, closeStream, args);
			streamDescriptor.State = EntityStates.Modified;
			this.entityTracker.IncrementChange(streamDescriptor);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00021D28 File Offset: 0x0001FF28
		public IAsyncResult BeginExecuteBatch(AsyncCallback callback, object state, params DataServiceRequest[] queries)
		{
			Util.CheckArgumentNotEmpty<DataServiceRequest>(queries, "queries");
			BatchSaveResult batchSaveResult = new BatchSaveResult(this, "ExecuteBatch", queries, SaveChangesOptions.BatchWithSingleChangeset, callback, state);
			batchSaveResult.BatchBeginRequest();
			return batchSaveResult;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00021D58 File Offset: 0x0001FF58
		public Task<DataServiceResponse> ExecuteBatchAsync(params DataServiceRequest[] queries)
		{
			return Task<DataServiceResponse>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginExecuteBatch(callback, state, queries), new Func<IAsyncResult, DataServiceResponse>(this.EndExecuteBatch), null);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00021D9C File Offset: 0x0001FF9C
		public DataServiceResponse EndExecuteBatch(IAsyncResult asyncResult)
		{
			BatchSaveResult batchSaveResult = BaseAsyncResult.EndExecute<BatchSaveResult>(this, "ExecuteBatch", asyncResult);
			return batchSaveResult.EndRequest();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00021DBC File Offset: 0x0001FFBC
		public DataServiceResponse ExecuteBatch(params DataServiceRequest[] queries)
		{
			Util.CheckArgumentNotEmpty<DataServiceRequest>(queries, "queries");
			BatchSaveResult batchSaveResult = new BatchSaveResult(this, "ExecuteBatch", queries, SaveChangesOptions.BatchWithSingleChangeset, null, null);
			batchSaveResult.BatchRequest();
			return batchSaveResult.EndRequest();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00021DF0 File Offset: 0x0001FFF0
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state)
		{
			return this.InnerBeginExecute<TElement>(requestUri, callback, state, "GET", "Execute", null, new OperationParameter[0]);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00021E1F File Offset: 0x0002001F
		public Task<IEnumerable<TElement>> ExecuteAsync<TElement>(Uri requestUri)
		{
			return Task<IEnumerable<TElement>>.Factory.FromAsync<Uri>(new Func<Uri, AsyncCallback, object, IAsyncResult>(this.BeginExecute<TElement>), new Func<IAsyncResult, IEnumerable<TElement>>(this.EndExecute<TElement>), requestUri, null);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00021E45 File Offset: 0x00020045
		public IAsyncResult BeginExecute(Uri requestUri, AsyncCallback callback, object state, string httpMethod, params OperationParameter[] operationParameters)
		{
			return this.InnerBeginExecute<object>(requestUri, callback, state, httpMethod, "ExecuteVoid", new bool?(false), operationParameters);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00021E60 File Offset: 0x00020060
		public Task<OperationResponse> ExecuteAsync(Uri requestUri, string httpMethod, params OperationParameter[] operationParameters)
		{
			return Task<OperationResponse>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginExecute(requestUri, callback, state, httpMethod, operationParameters), new Func<IAsyncResult, OperationResponse>(this.EndExecute), null);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00021EB2 File Offset: 0x000200B2
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state, string httpMethod, bool singleResult, params OperationParameter[] operationParameters)
		{
			return this.InnerBeginExecute<TElement>(requestUri, callback, state, httpMethod, "Execute", new bool?(singleResult), operationParameters);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00021ED0 File Offset: 0x000200D0
		public Task<IEnumerable<TElement>> ExecuteAsync<TElement>(Uri requestUri, string httpMethod, bool singleResult, params OperationParameter[] operationParameters)
		{
			return Task<IEnumerable<TElement>>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginExecute<TElement>(requestUri, callback, state, httpMethod, singleResult, operationParameters), new Func<IAsyncResult, IEnumerable<TElement>>(this.EndExecute<TElement>), null);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00021F2C File Offset: 0x0002012C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state, string httpMethod, params OperationParameter[] operationParameters)
		{
			bool? flag = this.IsSingletonType<TElement>();
			return this.InnerBeginExecute<TElement>(requestUri, callback, state, httpMethod, "Execute", flag, operationParameters);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00021F54 File Offset: 0x00020154
		public Task<IEnumerable<TElement>> ExecuteAsync<TElement>(Uri requestUri, string httpMethod, params OperationParameter[] operationParameters)
		{
			return Task<IEnumerable<TElement>>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginExecute<TElement>(requestUri, callback, state, httpMethod, operationParameters), new Func<IAsyncResult, IEnumerable<TElement>>(this.EndExecute<TElement>), null);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00021FA8 File Offset: 0x000201A8
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IAsyncResult BeginExecute<T>(DataServiceQueryContinuation<T> continuation, AsyncCallback callback, object state)
		{
			Util.CheckArgumentNull<DataServiceQueryContinuation<T>>(continuation, "continuation");
			QueryComponents queryComponents = continuation.CreateQueryComponents();
			Uri uri = queryComponents.Uri;
			return new DataServiceRequest<T>(uri, queryComponents, continuation.Plan).BeginExecute(this, this, callback, state, "Execute");
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00021FEA File Offset: 0x000201EA
		public Task<IEnumerable<TElement>> ExecuteAsync<TElement>(DataServiceQueryContinuation<TElement> continuation)
		{
			return Task<IEnumerable<TElement>>.Factory.FromAsync<DataServiceQueryContinuation<TElement>>(new Func<DataServiceQueryContinuation<TElement>, AsyncCallback, object, IAsyncResult>(this.BeginExecute<TElement>), new Func<IAsyncResult, IEnumerable<TElement>>(this.EndExecute<TElement>), continuation, null);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00022010 File Offset: 0x00020210
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IEnumerable<TElement> EndExecute<TElement>(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			return DataServiceRequest.EndExecute<TElement>(this, this, "Execute", asyncResult);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002202C File Offset: 0x0002022C
		public OperationResponse EndExecute(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			QueryOperationResponse<object> queryOperationResponse = (QueryOperationResponse<object>)DataServiceRequest.EndExecute<object>(this, this, "ExecuteVoid", asyncResult);
			if (queryOperationResponse.Any<object>())
			{
				throw new DataServiceClientException(Strings.Context_EndExecuteExpectedVoidResponse);
			}
			return queryOperationResponse;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002206C File Offset: 0x0002026C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IEnumerable<TElement> Execute<TElement>(Uri requestUri)
		{
			return this.InnerSynchExecute<TElement>(requestUri, "GET", null, new OperationParameter[0]);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00022094 File Offset: 0x00020294
		public QueryOperationResponse<T> Execute<T>(DataServiceQueryContinuation<T> continuation)
		{
			Util.CheckArgumentNull<DataServiceQueryContinuation<T>>(continuation, "continuation");
			QueryComponents queryComponents = continuation.CreateQueryComponents();
			Uri uri = queryComponents.Uri;
			DataServiceRequest dataServiceRequest = new DataServiceRequest<T>(uri, queryComponents, continuation.Plan);
			return dataServiceRequest.Execute<T>(this, queryComponents);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000220D4 File Offset: 0x000202D4
		public OperationResponse Execute(Uri requestUri, string httpMethod, params OperationParameter[] operationParameters)
		{
			QueryOperationResponse<object> queryOperationResponse = (QueryOperationResponse<object>)this.Execute<object>(requestUri, httpMethod, false, operationParameters);
			if (queryOperationResponse.Any<object>())
			{
				throw new DataServiceClientException(Strings.Context_ExecuteExpectedVoidResponse);
			}
			return queryOperationResponse;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00022105 File Offset: 0x00020305
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Just for CTP")]
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IEnumerable<TElement> Execute<TElement>(Uri requestUri, string httpMethod, bool singleResult, params OperationParameter[] operationParameters)
		{
			return this.InnerSynchExecute<TElement>(requestUri, httpMethod, new bool?(singleResult), operationParameters);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00022118 File Offset: 0x00020318
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Just for CTP")]
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IEnumerable<TElement> Execute<TElement>(Uri requestUri, string httpMethod, params OperationParameter[] operationParameters)
		{
			bool? flag = this.IsSingletonType<TElement>();
			return this.InnerSynchExecute<TElement>(requestUri, httpMethod, flag, operationParameters);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00022136 File Offset: 0x00020336
		public IAsyncResult BeginSaveChanges(AsyncCallback callback, object state)
		{
			return this.BeginSaveChanges(this.SaveChangesDefaultOptions, callback, state);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00022146 File Offset: 0x00020346
		public Task<DataServiceResponse> SaveChangesAsync()
		{
			return this.SaveChangesAsync(this.SaveChangesDefaultOptions);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00022154 File Offset: 0x00020354
		public IAsyncResult BeginSaveChanges(SaveChangesOptions options, AsyncCallback callback, object state)
		{
			this.ValidateSaveChangesOptions(options);
			BaseSaveResult baseSaveResult = BaseSaveResult.CreateSaveResult(this, "SaveChanges", null, options, callback, state);
			if (baseSaveResult.IsBatchRequest)
			{
				((BatchSaveResult)baseSaveResult).BatchBeginRequest();
			}
			else
			{
				((SaveResult)baseSaveResult).BeginCreateNextChange();
			}
			return baseSaveResult;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00022199 File Offset: 0x00020399
		public Task<DataServiceResponse> SaveChangesAsync(SaveChangesOptions options)
		{
			return Task<DataServiceResponse>.Factory.FromAsync<SaveChangesOptions>(new Func<SaveChangesOptions, AsyncCallback, object, IAsyncResult>(this.BeginSaveChanges), new Func<IAsyncResult, DataServiceResponse>(this.EndSaveChanges), options, null);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000221C0 File Offset: 0x000203C0
		public DataServiceResponse EndSaveChanges(IAsyncResult asyncResult)
		{
			BaseSaveResult baseSaveResult = BaseAsyncResult.EndExecute<BaseSaveResult>(this, "SaveChanges", asyncResult);
			DataServiceResponse dataServiceResponse = baseSaveResult.EndRequest();
			if (this.ChangesSaved != null)
			{
				this.ChangesSaved(this, new SaveChangesEventArgs(dataServiceResponse));
			}
			return dataServiceResponse;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000221FC File Offset: 0x000203FC
		public DataServiceResponse SaveChanges()
		{
			return this.SaveChanges(this.SaveChangesDefaultOptions);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002220C File Offset: 0x0002040C
		public DataServiceResponse SaveChanges(SaveChangesOptions options)
		{
			this.ValidateSaveChangesOptions(options);
			BaseSaveResult baseSaveResult = BaseSaveResult.CreateSaveResult(this, "SaveChanges", null, options, null, null);
			if (baseSaveResult.IsBatchRequest)
			{
				((BatchSaveResult)baseSaveResult).BatchRequest();
			}
			else
			{
				((SaveResult)baseSaveResult).CreateNextChange();
			}
			DataServiceResponse dataServiceResponse = baseSaveResult.EndRequest();
			if (this.ChangesSaved != null)
			{
				this.ChangesSaved(this, new SaveChangesEventArgs(dataServiceResponse));
			}
			return dataServiceResponse;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00022274 File Offset: 0x00020474
		public void AddLink(object source, string sourceProperty, object target)
		{
			this.EnsureRelatable(source, sourceProperty, target, EntityStates.Added);
			LinkDescriptor linkDescriptor = new LinkDescriptor(source, sourceProperty, target, this.model);
			this.entityTracker.AddLink(linkDescriptor);
			linkDescriptor.State = EntityStates.Added;
			this.entityTracker.IncrementChange(linkDescriptor);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000222BA File Offset: 0x000204BA
		public void AttachLink(object source, string sourceProperty, object target)
		{
			this.AttachLink(source, sourceProperty, target, MergeOption.NoTracking);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000222C8 File Offset: 0x000204C8
		public bool DetachLink(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			LinkDescriptor linkDescriptor = this.entityTracker.TryGetLinkDescriptor(source, sourceProperty, target);
			if (linkDescriptor == null)
			{
				return false;
			}
			this.entityTracker.DetachExistingLink(linkDescriptor, false);
			return true;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00022310 File Offset: 0x00020510
		public void DeleteLink(object source, string sourceProperty, object target)
		{
			bool flag = this.EnsureRelatable(source, sourceProperty, target, EntityStates.Deleted);
			LinkDescriptor linkDescriptor = this.entityTracker.TryGetLinkDescriptor(source, sourceProperty, target);
			if (linkDescriptor != null && EntityStates.Added == linkDescriptor.State)
			{
				this.entityTracker.DetachExistingLink(linkDescriptor, false);
				return;
			}
			if (flag)
			{
				throw Error.InvalidOperation(Strings.Context_NoRelationWithInsertEnd);
			}
			if (linkDescriptor == null)
			{
				LinkDescriptor linkDescriptor2 = new LinkDescriptor(source, sourceProperty, target, this.model);
				this.entityTracker.AddLink(linkDescriptor2);
				linkDescriptor = linkDescriptor2;
			}
			if (EntityStates.Deleted != linkDescriptor.State)
			{
				linkDescriptor.State = EntityStates.Deleted;
				this.entityTracker.IncrementChange(linkDescriptor);
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002239C File Offset: 0x0002059C
		public void SetLink(object source, string sourceProperty, object target)
		{
			this.EnsureRelatable(source, sourceProperty, target, EntityStates.Modified);
			LinkDescriptor linkDescriptor = this.entityTracker.DetachReferenceLink(source, sourceProperty, target, MergeOption.NoTracking);
			if (linkDescriptor == null)
			{
				linkDescriptor = new LinkDescriptor(source, sourceProperty, target, this.model);
				this.entityTracker.AddLink(linkDescriptor);
			}
			if (EntityStates.Modified != linkDescriptor.State)
			{
				linkDescriptor.State = EntityStates.Modified;
				this.entityTracker.IncrementChange(linkDescriptor);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00022404 File Offset: 0x00020604
		public void AddObject(string entitySetName, object entity)
		{
			DataServiceContext.ValidateEntitySetName(ref entitySetName);
			DataServiceContext.ValidateEntityType(entity, this.Model);
			EntityDescriptor entityDescriptor = new EntityDescriptor(this.model)
			{
				Entity = entity,
				State = EntityStates.Added,
				EntitySetName = entitySetName
			};
			entityDescriptor.SetEntitySetUriForInsert(this.BaseUriResolver.GetEntitySetUri(entitySetName));
			this.EntityTracker.AddEntityDescriptor(entityDescriptor);
			this.EntityTracker.IncrementChange(entityDescriptor);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00022470 File Offset: 0x00020670
		public void AddRelatedObject(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			Util.CheckArgumentNull<object>(target, "target");
			DataServiceContext.ValidateEntityType(source, this.Model);
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(source);
			if (entityDescriptor.State == EntityStates.Deleted)
			{
				throw Error.InvalidOperation(Strings.Context_AddRelatedObjectSourceDeleted);
			}
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType()));
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(sourceProperty, UndeclaredPropertyBehavior.ThrowException);
			if (property.IsKnownType || !property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_AddRelatedObjectCollectionOnly);
			}
			ClientTypeAnnotation clientTypeAnnotation2 = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(target.GetType()));
			DataServiceContext.ValidateEntityType(target, this.Model);
			ClientTypeAnnotation clientTypeAnnotation3 = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(property.EntityCollectionItemType));
			if (!clientTypeAnnotation3.ElementType.IsAssignableFrom(clientTypeAnnotation2.ElementType))
			{
				throw Error.Argument(Strings.Context_RelationNotRefOrCollection, "target");
			}
			EntityDescriptor entityDescriptor2 = new EntityDescriptor(this.model)
			{
				Entity = target,
				State = EntityStates.Added
			};
			entityDescriptor2.SetParentForInsert(entityDescriptor, sourceProperty);
			this.EntityTracker.AddEntityDescriptor(entityDescriptor2);
			LinkDescriptor relatedEnd = entityDescriptor2.GetRelatedEnd();
			relatedEnd.State = EntityStates.Added;
			this.entityTracker.AddLink(relatedEnd);
			this.entityTracker.IncrementChange(entityDescriptor2);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000225D5 File Offset: 0x000207D5
		public void AttachTo(string entitySetName, object entity)
		{
			this.AttachTo(entitySetName, entity, null);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000225E0 File Offset: 0x000207E0
		[SuppressMessage("Microsoft.Naming", "CA1704", MessageId = "etag", Justification = "represents ETag in request")]
		public void AttachTo(string entitySetName, object entity, string etag)
		{
			DataServiceContext.ValidateEntitySetName(ref entitySetName);
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = new EntityDescriptor(this.model)
			{
				Entity = entity,
				ETag = etag,
				State = EntityStates.Unchanged,
				EntitySetName = entitySetName
			};
			ODataResourceMetadataBuilder entityMetadataBuilderInternal = this.GetEntityMetadataBuilderInternal(entityDescriptor);
			entityDescriptor.EditLink = entityMetadataBuilderInternal.GetEditLink();
			entityDescriptor.Identity = entityMetadataBuilderInternal.GetId();
			this.entityTracker.InternalAttachEntityDescriptor(entityDescriptor, true);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00022656 File Offset: 0x00020856
		public void DeleteObject(object entity)
		{
			this.DeleteObjectInternal(entity, false);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00022660 File Offset: 0x00020860
		public bool Detach(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.TryGetEntityDescriptor(entity);
			return entityDescriptor != null && this.entityTracker.DetachResource(entityDescriptor);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00022697 File Offset: 0x00020897
		public void UpdateObject(object entity)
		{
			this.UpdateObjectInternal(entity, false);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000226A4 File Offset: 0x000208A4
		public void UpdateRelatedObject(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			Util.CheckArgumentNull<object>(target, "target");
			DataServiceContext.ValidateEntityType(source, this.Model);
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(source);
			if (entityDescriptor.State == EntityStates.Deleted)
			{
				throw Error.InvalidOperation(Strings.Context_AddRelatedObjectSourceDeleted);
			}
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType()));
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(sourceProperty, UndeclaredPropertyBehavior.ThrowException);
			if (property.IsKnownType || property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_UpdateRelatedObjectNonCollectionOnly);
			}
			ClientTypeAnnotation clientTypeAnnotation2 = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(target.GetType()));
			DataServiceContext.ValidateEntityType(target, this.Model);
			ClientTypeAnnotation clientTypeAnnotation3 = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(property.PropertyType));
			if (!clientTypeAnnotation3.ElementType.IsAssignableFrom(clientTypeAnnotation2.ElementType))
			{
				throw Error.Argument(Strings.Context_RelationNotRefOrCollection, "target");
			}
			EntityDescriptor entityDescriptor2 = this.entityTracker.TryGetEntityDescriptor(target);
			if (entityDescriptor2 != null)
			{
				this.UpdateObject(target);
				return;
			}
			entityDescriptor2 = new EntityDescriptor(this.model)
			{
				Entity = target,
				State = EntityStates.Modified,
				EditLink = entityDescriptor.GetNestedResourceInfo(this.baseUriResolver, property)
			};
			entityDescriptor2.SetParentForUpdate(entityDescriptor, sourceProperty);
			this.EntityTracker.AddEntityDescriptor(entityDescriptor2);
			this.entityTracker.IncrementChange(entityDescriptor2);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002281C File Offset: 0x00020A1C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ChangeState", Justification = "Method name, will be removed when string is added to resources.")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AddObject", Justification = "Method name, will be removed when string is added to resources.")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AddRelatedObject", Justification = "Method name, will be removed when string is added to resources.")]
		public void ChangeState(object entity, EntityStates state)
		{
			switch (state)
			{
			case EntityStates.Detached:
				this.Detach(entity);
				return;
			case EntityStates.Unchanged:
				this.SetStateToUnchanged(entity);
				return;
			case EntityStates.Detached | EntityStates.Unchanged:
				break;
			case EntityStates.Added:
				throw Error.NotSupported(Strings.Context_CannotChangeStateToAdded);
			default:
				if (state == EntityStates.Deleted)
				{
					this.DeleteObjectInternal(entity, true);
					return;
				}
				if (state == EntityStates.Modified)
				{
					this.UpdateObjectInternal(entity, true);
					return;
				}
				break;
			}
			throw Error.InternalError(InternalError.UnvalidatedEntityState);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00022880 File Offset: 0x00020A80
		public bool TryGetEntity<TEntity>(Uri identity, out TEntity entity) where TEntity : class
		{
			entity = default(TEntity);
			Util.CheckArgumentNull<Uri>(identity, "relativeUri");
			EntityStates entityStates;
			entity = (TEntity)((object)this.EntityTracker.TryGetEntity(identity, out entityStates));
			return entity != null;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000228C8 File Offset: 0x00020AC8
		public bool TryGetUri(object entity, out Uri identity)
		{
			identity = null;
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.TryGetEntityDescriptor(entity);
			if (entityDescriptor != null && null != entityDescriptor.Identity && entityDescriptor == this.entityTracker.TryGetEntityDescriptor(entityDescriptor.Identity))
			{
				identity = entityDescriptor.Identity;
			}
			return null != identity;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00022928 File Offset: 0x00020B28
		internal virtual IEdmVocabularyAnnotatable GetEdmOperationOrOperationImport(MethodInfo methodInfo)
		{
			Type declaringType = methodInfo.DeclaringType;
			if (declaringType.IsSubclassOf(typeof(DataServiceContext)))
			{
				return AnnotationHelper.GetEdmOperationImport(this, methodInfo);
			}
			return AnnotationHelper.GetEdmOperation(this, methodInfo);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00022960 File Offset: 0x00020B60
		internal Task<QueryOperationResponse> LoadPropertyAllPagesAsync(object entity, string propertyName)
		{
			Task<QueryOperationResponse> task = Task<QueryOperationResponse>.Factory.FromAsync<object, string>(new Func<object, string, AsyncCallback, object, IAsyncResult>(this.BeginLoadProperty), new Func<IAsyncResult, QueryOperationResponse>(this.EndLoadProperty), entity, propertyName, null);
			return task.ContinueWith<QueryOperationResponse>((Task<QueryOperationResponse> t) => this.ContinuePage(t.Result, entity, propertyName));
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000229CC File Offset: 0x00020BCC
		internal QueryOperationResponse LoadPropertyAllPages(object entity, string propertyName)
		{
			DataServiceQueryContinuation dataServiceQueryContinuation = null;
			QueryOperationResponse queryOperationResponse;
			do
			{
				if (dataServiceQueryContinuation == null)
				{
					queryOperationResponse = this.LoadProperty(entity, propertyName, null);
				}
				else
				{
					queryOperationResponse = this.LoadProperty(entity, propertyName, dataServiceQueryContinuation);
				}
				dataServiceQueryContinuation = queryOperationResponse.GetContinuation();
			}
			while (dataServiceQueryContinuation != null);
			return queryOperationResponse;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00022A00 File Offset: 0x00020C00
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		internal QueryOperationResponse<TElement> InnerSynchExecute<TElement>(Uri requestUri, string httpMethod, bool? singleResult, params OperationParameter[] operationParameters)
		{
			List<UriOperationParameter> list = null;
			List<BodyOperationParameter> list2 = null;
			this.ValidateExecuteParameters<TElement>(ref requestUri, httpMethod, ref singleResult, out list2, out list, operationParameters);
			QueryComponents queryComponents = new QueryComponents(requestUri, Util.ODataVersionEmpty, typeof(TElement), null, null, httpMethod, singleResult, list2, list);
			requestUri = queryComponents.Uri;
			DataServiceRequest dataServiceRequest = new DataServiceRequest<TElement>(requestUri, queryComponents, null);
			return dataServiceRequest.Execute<TElement>(this, queryComponents);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00022A58 File Offset: 0x00020C58
		internal IAsyncResult InnerBeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state, string httpMethod, string method, bool? singleResult, params OperationParameter[] operationParameters)
		{
			List<UriOperationParameter> list = null;
			List<BodyOperationParameter> list2 = null;
			this.ValidateExecuteParameters<TElement>(ref requestUri, httpMethod, ref singleResult, out list2, out list, operationParameters);
			QueryComponents queryComponents = new QueryComponents(requestUri, Util.ODataVersionEmpty, typeof(TElement), null, null, httpMethod, singleResult, list2, list);
			requestUri = queryComponents.Uri;
			return new DataServiceRequest<TElement>(requestUri, queryComponents, null).BeginExecute(this, this, callback, state, method);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00022AB4 File Offset: 0x00020CB4
		internal void AttachLink(object source, string sourceProperty, object target, MergeOption linkMerge)
		{
			this.EnsureRelatable(source, sourceProperty, target, EntityStates.Unchanged);
			this.entityTracker.AttachLink(source, sourceProperty, target, linkMerge);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00022AD4 File Offset: 0x00020CD4
		internal ODataRequestMessageWrapper CreateODataRequestMessage(BuildingRequestEventArgs requestMessageArgs, Descriptor descriptor)
		{
			ODataRequestMessageWrapper odataRequestMessageWrapper = new RequestInfo(this).WriteHelper.CreateRequestMessage(requestMessageArgs);
			odataRequestMessageWrapper.FireSendingRequest2(descriptor);
			return odataRequestMessageWrapper;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00022AFC File Offset: 0x00020CFC
		internal Type ResolveTypeFromName(string wireName)
		{
			Func<string, Type> func = this.ResolveType;
			if (func != null)
			{
				return func(wireName);
			}
			return null;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00022B1C File Offset: 0x00020D1C
		internal string ResolveNameFromTypeInternal(Type type)
		{
			Func<Type, string> func = this.ResolveName;
			if (func == null)
			{
				return null;
			}
			return func(type);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00022B3C File Offset: 0x00020D3C
		internal void FireSendingRequest(SendingRequestEventArgs eventArgs)
		{
			this.InnerSendingRequest(this, eventArgs);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00022B4B File Offset: 0x00020D4B
		internal void FireSendingRequest2(SendingRequest2EventArgs eventArgs)
		{
			this.SendingRequest2(this, eventArgs);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00022B5A File Offset: 0x00020D5A
		internal void FireReceivingResponseEvent(ReceivingResponseEventArgs receivingResponseEventArgs)
		{
			if (this.ReceivingResponse != null)
			{
				this.ReceivingResponse(this, receivingResponseEventArgs);
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00022B71 File Offset: 0x00020D71
		internal IODataResponseMessage GetSyncronousResponse(ODataRequestMessageWrapper request, bool handleWebException)
		{
			return this.GetResponseHelper(request, null, handleWebException);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00022B7C File Offset: 0x00020D7C
		internal IODataResponseMessage EndGetResponse(ODataRequestMessageWrapper request, IAsyncResult asyncResult)
		{
			return this.GetResponseHelper(request, asyncResult, true);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00022B87 File Offset: 0x00020D87
		internal void InternalSendRequest(HttpWebRequest request)
		{
			if (this.sendRequest != null)
			{
				this.sendRequest(request);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00022B9D File Offset: 0x00020D9D
		internal Stream InternalGetRequestWrappingStream(Stream requestStream)
		{
			if (this.getRequestWrappingStream == null)
			{
				return requestStream;
			}
			return this.getRequestWrappingStream(requestStream);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00022BB5 File Offset: 0x00020DB5
		internal void InternalSendResponse(HttpWebResponse response)
		{
			if (this.sendResponse != null)
			{
				this.sendResponse(response);
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00022BCB File Offset: 0x00020DCB
		internal Stream InternalGetResponseWrappingStream(Stream responseStream)
		{
			if (this.getResponseWrappingStream == null)
			{
				return responseStream;
			}
			return this.getResponseWrappingStream(responseStream);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022BE3 File Offset: 0x00020DE3
		internal virtual ODataResourceMetadataBuilder GetEntityMetadataBuilder(string entitySetName, IEdmStructuredValue entityInstance)
		{
			return new ConventionalODataEntityMetadataBuilder(this.baseUriResolver, entitySetName, entityInstance, this.UrlKeyDelimiter);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00022BF8 File Offset: 0x00020DF8
		internal BuildingRequestEventArgs CreateRequestArgsAndFireBuildingRequest(string method, Uri requestUri, HeaderCollection headers, HttpStack stack, Descriptor descriptor)
		{
			BuildingRequestEventArgs buildingRequestEventArgs = new BuildingRequestEventArgs(method, requestUri, headers, descriptor, stack);
			buildingRequestEventArgs.HeaderCollection.SetDefaultHeaders();
			return this.FireBuildingRequest(buildingRequestEventArgs);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00022C24 File Offset: 0x00020E24
		protected internal Type DefaultResolveType(string typeName, string fullNamespace, string languageDependentNamespace)
		{
			DataServiceContext.<>c__DisplayClass248_0 CS$<>8__locals1 = new DataServiceContext.<>c__DisplayClass248_0();
			CS$<>8__locals1.typeName = typeName;
			CS$<>8__locals1.languageDependentNamespace = languageDependentNamespace;
			if (CS$<>8__locals1.typeName == null || !CS$<>8__locals1.typeName.StartsWith(fullNamespace, StringComparison.Ordinal))
			{
				return null;
			}
			int namespaceLength = ((fullNamespace != null) ? fullNamespace.Length : 0);
			Type type = base.GetType().GetAssembly().GetType(CS$<>8__locals1.languageDependentNamespace + CS$<>8__locals1.typeName.Substring(namespaceLength), false);
			if (type == null)
			{
				return base.GetType().GetAssembly().GetTypes()
					.ToList<Type>()
					.Where(delegate(Type t)
					{
						string text = CS$<>8__locals1.typeName.Substring(namespaceLength + 1);
						OriginalNameAttribute originalNameAttribute = (OriginalNameAttribute)t.GetCustomAttributes(typeof(OriginalNameAttribute), true).SingleOrDefault<object>();
						return originalNameAttribute != null && originalNameAttribute.OriginalName == text && t.Namespace == CS$<>8__locals1.languageDependentNamespace;
					})
					.SingleOrDefault<Type>();
			}
			return type;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00022CF4 File Offset: 0x00020EF4
		private bool? IsSingletonType<TElement>()
		{
			Type typeFromHandle = typeof(TElement);
			IEdmTypeReference edmTypeReference = this.Model.GetOrCreateEdmType(typeFromHandle).ToEdmTypeReference(ClientTypeUtil.CanAssignNull(typeFromHandle));
			if (edmTypeReference.IsPrimitive() || edmTypeReference.IsComplex())
			{
				return new bool?(true);
			}
			if (edmTypeReference.IsCollection())
			{
				IEdmTypeReference edmTypeReference2 = edmTypeReference.AsCollection().ElementType();
				if (edmTypeReference2.IsPrimitive() || edmTypeReference2.IsComplex())
				{
					return new bool?(false);
				}
			}
			return null;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00022D70 File Offset: 0x00020F70
		private QueryOperationResponse ContinuePage(QueryOperationResponse response, object entity, string propertyName)
		{
			DataServiceQueryContinuation continuation = response.GetContinuation();
			if (continuation != null)
			{
				Task<QueryOperationResponse> task = Task<QueryOperationResponse>.Factory.FromAsync(this.BeginLoadProperty(entity, propertyName, continuation, null, null), new Func<IAsyncResult, QueryOperationResponse>(this.EndLoadProperty));
				Task<QueryOperationResponse> task2 = task.ContinueWith<QueryOperationResponse>((Task<QueryOperationResponse> t) => this.ContinuePage(t.Result, entity, propertyName));
				Task.WaitAll(new Task[] { task, task2 });
				return task2.Result;
			}
			return response;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00022DFC File Offset: 0x00020FFC
		private static void ValidateEntitySetName(ref string entitySetName)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			entitySetName = entitySetName.Trim(UriUtil.ForwardSlash);
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			Uri uri = UriUtil.CreateUri(entitySetName, UriKind.RelativeOrAbsolute);
			if (uri.IsAbsoluteUri || !string.IsNullOrEmpty(UriUtil.CreateUri(new Uri("http://ConstBaseUri/ConstService.svc/"), uri).GetComponents(UriComponents.Query | UriComponents.Fragment, UriFormat.SafeUnescaped)))
			{
				throw Error.Argument(Strings.Context_EntitySetName, "entitySetName");
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00022E6F File Offset: 0x0002106F
		private static void ValidateEntityType(object entity, ClientEdmModel model)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			if (!ClientTypeUtil.TypeIsEntity(entity.GetType(), model))
			{
				throw Error.Argument(Strings.Content_EntityIsNotEntityType, "entity");
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00022E9C File Offset: 0x0002109C
		private static void ValidateOperationParameters(string httpMethod, OperationParameter[] parameters, out List<BodyOperationParameter> bodyOperationParameters, out List<UriOperationParameter> uriOperationParameters)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<UriOperationParameter> list = new List<UriOperationParameter>();
			List<BodyOperationParameter> list2 = new List<BodyOperationParameter>();
			foreach (OperationParameter operationParameter in parameters)
			{
				if (operationParameter == null)
				{
					throw new ArgumentException(Strings.Context_NullElementInOperationParameterArray);
				}
				if (string.IsNullOrEmpty(operationParameter.Name))
				{
					throw new ArgumentException(Strings.Context_MissingOperationParameterName);
				}
				string text = operationParameter.Name.Trim();
				BodyOperationParameter bodyOperationParameter = operationParameter as BodyOperationParameter;
				if (bodyOperationParameter != null)
				{
					if (string.CompareOrdinal("GET", httpMethod) == 0)
					{
						throw new ArgumentException(Strings.Context_BodyOperationParametersNotAllowedWithGet);
					}
					if (!hashSet2.Add(text))
					{
						throw new ArgumentException(Strings.Context_DuplicateBodyOperationParameterName);
					}
					list2.Add(bodyOperationParameter);
				}
				else
				{
					UriOperationParameter uriOperationParameter = operationParameter as UriOperationParameter;
					if (uriOperationParameter != null)
					{
						if (!hashSet.Add(text))
						{
							throw new ArgumentException(Strings.Context_DuplicateUriOperationParameterName);
						}
						list.Add(uriOperationParameter);
					}
				}
			}
			uriOperationParameters = (list.Any<UriOperationParameter>() ? list : null);
			bodyOperationParameters = (list2.Any<BodyOperationParameter>() ? list2 : null);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00022FAE File Offset: 0x000211AE
		private BuildingRequestEventArgs FireBuildingRequest(BuildingRequestEventArgs buildingRequestEventArgs)
		{
			if (this.HasBuildingRequestEventHandlers)
			{
				this.InnerBuildingRequest(this, buildingRequestEventArgs);
				return buildingRequestEventArgs.Clone();
			}
			return buildingRequestEventArgs;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00022FD0 File Offset: 0x000211D0
		private void ValidateSaveChangesOptions(SaveChangesOptions options)
		{
			if ((options | (SaveChangesOptions.BatchWithSingleChangeset | SaveChangesOptions.ContinueOnError | SaveChangesOptions.ReplaceOnUpdate | SaveChangesOptions.BatchWithIndependentOperations | SaveChangesOptions.PostOnlySetProperties)) != (SaveChangesOptions.BatchWithSingleChangeset | SaveChangesOptions.ContinueOnError | SaveChangesOptions.ReplaceOnUpdate | SaveChangesOptions.BatchWithIndependentOperations | SaveChangesOptions.PostOnlySetProperties))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.BatchWithSingleChangeset | SaveChangesOptions.BatchWithIndependentOperations))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.BatchWithSingleChangeset | SaveChangesOptions.ContinueOnError))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.ContinueOnError | SaveChangesOptions.BatchWithIndependentOperations))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.PostOnlySetProperties) && !this.UsingDataServiceCollection)
			{
				throw Error.InvalidOperation(Strings.Context_MustBeUsedWith("SaveChangesOptions.OnlyPostExplicitProperties", "DataServiceCollection"));
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00023054 File Offset: 0x00021254
		private void ValidateExecuteParameters<TElement>(ref Uri requestUri, string httpMethod, ref bool? singleResult, out List<BodyOperationParameter> bodyOperationParameters, out List<UriOperationParameter> uriOperationParameters, params OperationParameter[] operationParameters)
		{
			if (string.CompareOrdinal("GET", httpMethod) != 0 && string.CompareOrdinal("POST", httpMethod) != 0 && string.CompareOrdinal("DELETE", httpMethod) != 0)
			{
				throw new ArgumentException(Strings.Context_ExecuteExpectsGetOrPostOrDelete, "httpMethod");
			}
			if (ClientTypeUtil.TypeOrElementTypeIsStructured(typeof(TElement)))
			{
				singleResult = null;
			}
			if (operationParameters != null && operationParameters.Length != 0)
			{
				DataServiceContext.ValidateOperationParameters(httpMethod, operationParameters, out bodyOperationParameters, out uriOperationParameters);
			}
			else
			{
				uriOperationParameters = null;
				bodyOperationParameters = null;
			}
			requestUri = this.BaseUriResolver.GetOrCreateAbsoluteUri(requestUri);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000230E0 File Offset: 0x000212E0
		private LoadPropertyResult CreateLoadPropertyRequest(object entity, string propertyName, AsyncCallback callback, object state, Uri requestUri, DataServiceQueryContinuation continuation)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			Util.CheckArgumentNullAndEmpty(propertyName, "propertyName");
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(entity.GetType()));
			if (EntityStates.Added == entityDescriptor.State)
			{
				throw Error.InvalidOperation(Strings.Context_NoLoadWithInsertEnd);
			}
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(propertyName, UndeclaredPropertyBehavior.ThrowException);
			bool flag = requestUri != null || continuation != null;
			ProjectionPlan projectionPlan;
			if (continuation == null)
			{
				projectionPlan = null;
			}
			else
			{
				projectionPlan = continuation.Plan;
				requestUri = continuation.NextLinkUri;
			}
			bool flag2 = clientTypeAnnotation.MediaDataMember != null && propertyName == clientTypeAnnotation.MediaDataMember.PropertyName;
			if (requestUri == null)
			{
				if (flag2)
				{
					Uri uri = UriUtil.CreateUri("$value", UriKind.Relative);
					requestUri = UriUtil.CreateUri(entityDescriptor.GetResourceUri(this.BaseUriResolver, true), uri);
				}
				else
				{
					requestUri = entityDescriptor.GetNestedResourceInfo(this.baseUriResolver, property);
				}
			}
			Version odataVersion = Util.ODataVersion4;
			HeaderCollection headerCollection = new HeaderCollection();
			headerCollection.SetRequestVersion(odataVersion, this.MaxProtocolVersionAsVersion);
			if (flag2)
			{
				this.Format.SetRequestAcceptHeaderForStream(headerCollection);
			}
			else
			{
				this.formatTracker.SetRequestAcceptHeader(headerCollection);
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateODataRequestMessage(this.CreateRequestArgsAndFireBuildingRequest("GET", requestUri, headerCollection, this.HttpStack, null), null);
			DataServiceRequest instance = DataServiceRequest.GetInstance(property.PropertyType, requestUri);
			instance.PayloadKind = ODataPayloadKind.IndividualProperty;
			return new LoadPropertyResult(entity, propertyName, this, odataRequestMessageWrapper, callback, state, instance, projectionPlan, flag);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00023260 File Offset: 0x00021460
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Pending")]
		private bool EnsureRelatable(object source, string sourceProperty, object target, EntityStates state)
		{
			Util.CheckArgumentNull<object>(source, "source");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(source);
			EntityDescriptor entityDescriptor2 = null;
			if (target != null || (EntityStates.Modified != state && EntityStates.Unchanged != state))
			{
				Util.CheckArgumentNull<object>(target, "target");
				entityDescriptor2 = this.entityTracker.GetEntityDescriptor(target);
			}
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType()));
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(sourceProperty, UndeclaredPropertyBehavior.ThrowException);
			if (property.IsKnownType)
			{
				throw Error.InvalidOperation(Strings.Context_RelationNotRefOrCollection);
			}
			if (EntityStates.Unchanged == state && target == null && property.IsEntityCollection)
			{
				Util.CheckArgumentNull<object>(target, "target");
				entityDescriptor2 = this.entityTracker.GetEntityDescriptor(target);
			}
			if ((EntityStates.Added == state || EntityStates.Deleted == state) && !property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_AddLinkCollectionOnly);
			}
			if (EntityStates.Modified == state && property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_SetLinkReferenceOnly);
			}
			clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(property.EntityCollectionItemType ?? property.PropertyType));
			if (target != null && !clientTypeAnnotation.ElementType.IsInstanceOfType(target))
			{
				throw Error.Argument(Strings.Context_RelationNotRefOrCollection, "target");
			}
			if ((EntityStates.Added == state || EntityStates.Unchanged == state) && (entityDescriptor.State == EntityStates.Deleted || (entityDescriptor2 != null && entityDescriptor2.State == EntityStates.Deleted)))
			{
				throw Error.InvalidOperation(Strings.Context_NoRelationWithDeleteEnd);
			}
			if ((EntityStates.Deleted != state && EntityStates.Unchanged != state) || (entityDescriptor.State != EntityStates.Added && (entityDescriptor2 == null || entityDescriptor2.State != EntityStates.Added)))
			{
				return false;
			}
			if (EntityStates.Deleted == state)
			{
				return true;
			}
			throw Error.InvalidOperation(Strings.Context_NoRelationWithInsertEnd);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000233F8 File Offset: 0x000215F8
		private GetReadStreamResult CreateGetReadStreamResult(object entity, DataServiceRequestArgs args, AsyncCallback callback, object state, string name)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNull<DataServiceRequestArgs>(args, "args");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			Version version;
			Uri uri;
			StreamDescriptor defaultStreamDescriptor;
			if (name == null)
			{
				version = null;
				uri = entityDescriptor.ReadStreamUri;
				if (uri == null)
				{
					throw new ArgumentException(Strings.Context_EntityNotMediaLinkEntry, "entity");
				}
				defaultStreamDescriptor = entityDescriptor.DefaultStreamDescriptor;
			}
			else
			{
				version = Util.ODataVersion4;
				if (!entityDescriptor.TryGetNamedStreamInfo(name, out defaultStreamDescriptor))
				{
					throw new ArgumentException(Strings.Context_EntityDoesNotContainNamedStream(name), "name");
				}
				uri = defaultStreamDescriptor.SelfLink ?? defaultStreamDescriptor.EditLink;
				if (uri == null)
				{
					throw new ArgumentException(Strings.Context_MissingSelfAndEditLinkForNamedStream(name), "name");
				}
			}
			HeaderCollection headerCollection = args.HeaderCollection.Copy();
			headerCollection.SetRequestVersion(version, this.MaxProtocolVersionAsVersion);
			this.Format.SetRequestAcceptHeaderForStream(headerCollection);
			BuildingRequestEventArgs buildingRequestEventArgs = this.CreateRequestArgsAndFireBuildingRequest("GET", uri, headerCollection, HttpStack.Auto, defaultStreamDescriptor);
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateODataRequestMessage(buildingRequestEventArgs, defaultStreamDescriptor);
			return new GetReadStreamResult(this, "GetReadStream", odataRequestMessageWrapper, callback, state, defaultStreamDescriptor);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000234FF File Offset: 0x000216FF
		private void EnsureMinimumProtocolVersionV3()
		{
			if (this.MaxProtocolVersionAsVersion < Util.ODataVersion4)
			{
				throw Error.InvalidOperation(Strings.Context_RequestVersionIsBiggerThanProtocolVersion(Util.ODataVersion4, this.MaxProtocolVersionAsVersion));
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002352C File Offset: 0x0002172C
		private ODataResourceMetadataBuilder GetEntityMetadataBuilderInternal(EntityDescriptor descriptor)
		{
			ODataResourceMetadataBuilder entityMetadataBuilder = this.GetEntityMetadataBuilder(descriptor.EntitySetName, descriptor.EdmValue);
			if (entityMetadataBuilder == null)
			{
				throw new InvalidOperationException(Strings.Context_EntityMetadataBuilderIsRequired);
			}
			return entityMetadataBuilder;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002355C File Offset: 0x0002175C
		private IODataResponseMessage GetResponseHelper(ODataRequestMessageWrapper request, IAsyncResult asyncResult, bool handleWebException)
		{
			IODataResponseMessage iodataResponseMessage = null;
			try
			{
				if (asyncResult == null)
				{
					iodataResponseMessage = request.GetResponse();
				}
				else
				{
					iodataResponseMessage = request.EndGetResponse(asyncResult);
				}
				this.FireReceivingResponseEvent(new ReceivingResponseEventArgs(iodataResponseMessage, request.Descriptor));
			}
			catch (DataServiceTransportException ex)
			{
				iodataResponseMessage = ex.Response;
				this.FireReceivingResponseEvent(new ReceivingResponseEventArgs(iodataResponseMessage, request.Descriptor));
				if (!handleWebException || iodataResponseMessage == null)
				{
					throw;
				}
			}
			return iodataResponseMessage;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000235C8 File Offset: 0x000217C8
		private void UpdateObjectInternal(object entity, bool failIfNotUnchanged)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.TryGetEntityDescriptor(entity);
			if (entityDescriptor == null)
			{
				throw Error.Argument(Strings.Context_EntityNotContained, "entity");
			}
			if (entityDescriptor.State == EntityStates.Modified)
			{
				return;
			}
			if (entityDescriptor.State == EntityStates.Unchanged)
			{
				entityDescriptor.State = EntityStates.Modified;
				this.entityTracker.IncrementChange(entityDescriptor);
				return;
			}
			if (failIfNotUnchanged)
			{
				throw Error.InvalidOperation(Strings.Context_CannotChangeStateToModifiedIfNotUnchanged);
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00023638 File Offset: 0x00021838
		private void DeleteObjectInternal(object entity, bool failIfInAddedState)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			EntityStates state = entityDescriptor.State;
			if (EntityStates.Added != state)
			{
				if (EntityStates.Deleted != state)
				{
					entityDescriptor.State = EntityStates.Deleted;
					this.entityTracker.IncrementChange(entityDescriptor);
				}
				return;
			}
			if (failIfInAddedState)
			{
				throw Error.InvalidOperation(Strings.Context_CannotChangeStateIfAdded(EntityStates.Deleted));
			}
			this.entityTracker.DetachResource(entityDescriptor);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000236A4 File Offset: 0x000218A4
		private void SetStateToUnchanged(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			if (entityDescriptor.State == EntityStates.Added)
			{
				throw Error.InvalidOperation(Strings.Context_CannotChangeStateIfAdded(EntityStates.Unchanged));
			}
			entityDescriptor.State = EntityStates.Unchanged;
		}

		// Token: 0x0400037D RID: 893
		internal Version MaxProtocolVersionAsVersion;

		// Token: 0x0400037E RID: 894
		private const string ServiceRootParameterName = "serviceRoot";

		// Token: 0x0400037F RID: 895
		private readonly ClientEdmModel model;

		// Token: 0x04000380 RID: 896
		private readonly WeakDictionary<object, IDictionary<string, object>> instanceAnnotations = new WeakDictionary<object, IDictionary<string, object>>(InstanceAnnotationDictWeakKeyComparer.Default)
		{
			RemoveCollectedEntriesRules = new List<Func<object, bool>>
			{
				new Func<object, bool>(InstanceAnnotationDictWeakKeyComparer.Default.RemoveRule)
			},
			CreateWeakKey = new Func<object, object>(InstanceAnnotationDictWeakKeyComparer.Default.CreateKey)
		};

		// Token: 0x04000381 RID: 897
		private readonly WeakDictionary<object, IList<IEdmVocabularyAnnotation>> metadataAnnotationsDictionary = new WeakDictionary<object, IList<IEdmVocabularyAnnotation>>(EqualityComparer<object>.Default);

		// Token: 0x04000382 RID: 898
		private DataServiceClientFormat formatTracker;

		// Token: 0x04000383 RID: 899
		private ODataProtocolVersion maxProtocolVersion;

		// Token: 0x04000384 RID: 900
		private EntityTracker entityTracker;

		// Token: 0x04000385 RID: 901
		private DataServiceResponsePreference addAndUpdateResponsePreference;

		// Token: 0x04000386 RID: 902
		private UriResolver baseUriResolver;

		// Token: 0x04000387 RID: 903
		private ICredentials credentials;

		// Token: 0x04000388 RID: 904
		private Func<Type, string> resolveName;

		// Token: 0x04000389 RID: 905
		private Func<string, Type> resolveType;

		// Token: 0x0400038A RID: 906
		private int timeout;

		// Token: 0x0400038B RID: 907
		private bool postTunneling;

		// Token: 0x0400038C RID: 908
		private EntityParameterSendOption entityParameterSendOption;

		// Token: 0x0400038D RID: 909
		private MergeOption mergeOption;

		// Token: 0x0400038E RID: 910
		private SaveChangesOptions saveChangesDefaultOptions;

		// Token: 0x0400038F RID: 911
		private bool ignoreResourceNotFoundException;

		// Token: 0x04000390 RID: 912
		private UndeclaredPropertyBehavior undeclaredPropertyBehavior;

		// Token: 0x04000391 RID: 913
		private DataServiceUrlKeyDelimiter urlKeyDelimiter;

		// Token: 0x04000392 RID: 914
		private HttpStack httpStack;

		// Token: 0x04000393 RID: 915
		private Action<object> sendRequest;

		// Token: 0x04000394 RID: 916
		private Func<Stream, Stream> getRequestWrappingStream;

		// Token: 0x04000395 RID: 917
		private Action<object> sendResponse;

		// Token: 0x04000396 RID: 918
		private Func<Stream, Stream> getResponseWrappingStream;

		// Token: 0x04000397 RID: 919
		private bool applyingChanges;

		// Token: 0x020001B2 RID: 434
		private static class ClientEdmModelCache
		{
			// Token: 0x06000ECB RID: 3787 RVA: 0x00031ABB File Offset: 0x0002FCBB
			internal static ClientEdmModel GetModel(ODataProtocolVersion maxProtocolVersion)
			{
				Util.CheckEnumerationValue(maxProtocolVersion, "maxProtocolVersion");
				return DataServiceContext.ClientEdmModelCache.modelCache[maxProtocolVersion];
			}

			// Token: 0x06000ECC RID: 3788 RVA: 0x00031AD4 File Offset: 0x0002FCD4
			private static Dictionary<ODataProtocolVersion, ClientEdmModel> CreateClientEdmModelCache()
			{
				Dictionary<ODataProtocolVersion, ClientEdmModel> dictionary = new Dictionary<ODataProtocolVersion, ClientEdmModel>(EqualityComparer<ODataProtocolVersion>.Default);
				IEnumerable<ODataProtocolVersion> enumerable = Enum.GetValues(typeof(ODataProtocolVersion)).Cast<ODataProtocolVersion>();
				foreach (ODataProtocolVersion odataProtocolVersion in enumerable)
				{
					ClientEdmModel clientEdmModel = new ClientEdmModel(odataProtocolVersion);
					clientEdmModel.SetEdmVersion(odataProtocolVersion.ToVersion());
					dictionary.Add(odataProtocolVersion, clientEdmModel);
				}
				return dictionary;
			}

			// Token: 0x040007C6 RID: 1990
			private static readonly Dictionary<ODataProtocolVersion, ClientEdmModel> modelCache = DataServiceContext.ClientEdmModelCache.CreateClientEdmModelCache();
		}
	}
}
