using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DocumentHost;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;
using Microsoft.Mashup.Security;
using Microsoft.Mashup.Storage;
using Microsoft.Mashup.Storage.Memory;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000014 RID: 20
	internal class MashupResource
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000477D File Offset: 0x0000297D
		static MashupResource()
		{
			ITracingService service = ProviderTracing.Service;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004798 File Offset: 0x00002998
		public MashupResource(ProviderContext context, ConnectionContext connection, IEnumerable<string> resourceNames, string expression, bool executeAction, object[] parameters, string[] optionalModules, string correlationId = null)
		{
			this.providerContext = context;
			this.connectionContext = connection;
			this.resourceNames = resourceNames;
			this.expression = expression;
			this.executeAction = executeAction;
			this.parameters = parameters;
			this.requiredModules = optionalModules;
			MashupResourceProperties mashupResourceProperties = connection.CreateMashupResourceProperties();
			this.package = mashupResourceProperties.Package;
			this.partitionValues = mashupResourceProperties.PartitionValues;
			this.filterDataTable = mashupResourceProperties.FilterDataTable;
			this.allowAutomaticCredentials = mashupResourceProperties.AllowAutomaticCredentials;
			this.allowWindowsAuthentication = mashupResourceProperties.AllowWindowsAuthentication;
			this.allowNativeQueries = mashupResourceProperties.AllowNativeQueries;
			this.fastCombine = mashupResourceProperties.FastCombine;
			this.maxCacheSize = mashupResourceProperties.MaxCacheSize;
			this.memoryCache = mashupResourceProperties.MemoryCache;
			this.cachePath = mashupResourceProperties.CachePath;
			this.maxTempSize = mashupResourceProperties.MaxTempSize;
			this.tempPath = mashupResourceProperties.TempPath;
			this.cacheEncryptionCertificateThumbprint = mashupResourceProperties.CacheEncryptionCertificateThumbprint;
			this.session = mashupResourceProperties.Session;
			this.activityId = mashupResourceProperties.ActivityId;
			this.correlationId = correlationId ?? mashupResourceProperties.CorrelationId;
			this.legacyRedirects = mashupResourceProperties.LegacyRedirects;
			this.throwFoldingFailures = mashupResourceProperties.ThrowFoldingFailures;
			this.throwOnVolatileFunctions = mashupResourceProperties.ThrowOnVolatileFunctions;
			this.ignorePreviouslyCachedData = mashupResourceProperties.IgnorePreviouslyCachedData;
			this.threadIdentity = mashupResourceProperties.ThreadIdentity;
			this.uiCulture = mashupResourceProperties.UiCulture;
			DateTimeOffset? dateTimeOffset;
			this.utcNow = ((mashupResourceProperties.Now != null) ? new DateTime?(dateTimeOffset.GetValueOrDefault().UtcDateTime) : null);
			this.configurationProperties = mashupResourceProperties.ConfigurationProperties;
			this.tracingOptions = mashupResourceProperties.TracingOptions;
			this.credentialsStorage = mashupResourceProperties.CredentialsStorage;
			this.queryPermissionsStorage = mashupResourceProperties.QueryPermissionsStorage;
			this.firewallStorage = mashupResourceProperties.FirewallStorage;
			this.firewallPlan = mashupResourceProperties.FirewallPlan;
			this.dataSourcePool = mashupResourceProperties.DataSourcePool;
			this.debug = mashupResourceProperties.Debug;
			this.cacheGroup = mashupResourceProperties.CacheGroup;
			this.metadataCache = mashupResourceProperties.MetadataCache;
			this.partitionProgressUpdater = mashupResourceProperties.PartitionProgressUpdater;
			this.containerEvaluationMonitorService = mashupResourceProperties.ContainerEvaluationMonitorService;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000049C8 File Offset: 0x00002BC8
		public void Abort()
		{
			object obj = this.lockObject;
			CompositeEvaluation localEvaluation;
			lock (obj)
			{
				localEvaluation = this.evaluation;
			}
			if (localEvaluation != null)
			{
				Impersonation.RunAsProcessUser(delegate
				{
					localEvaluation.Cancel();
				});
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A30 File Offset: 0x00002C30
		public T StartEvaluationAndGetResultSource<T>(int timeout)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.evaluation != null)
				{
					throw new InvalidOperationException();
				}
				this.evaluation = new CompositeEvaluation();
			}
			return Impersonation.RunAsProcessUser<T>(delegate
			{
				ManualResetEvent complete = new ManualResetEvent(false);
				T result2;
				try
				{
					EvaluationResult2<T> result = default(EvaluationResult2<T>);
					this.evaluation.Add(this.StartEvaluation<T>(delegate(EvaluationResult2<T> r)
					{
						result = r;
						complete.Set();
					}));
					TimeSpan timeSpan = ((timeout != 0) ? TimeSpan.FromSeconds((double)timeout) : TimeSpan.FromMilliseconds(-1.0));
					if (!complete.WaitOne(timeSpan))
					{
						this.evaluation.Cancel();
						if (!complete.WaitOne(MashupResource.cancelTimeout))
						{
							using (IHostTrace hostTrace = ProviderTracing.CreateTrace("MashupResource/StartEvaluationAndGetResultSource/cancelTimeout", null, TraceEventType.Information, null))
							{
								hostTrace.Add("cancelTimeout", MashupResource.cancelTimeout.ToString(), false);
							}
							complete.WaitOne();
						}
						if (result.Exception != null)
						{
							result = new EvaluationResult2<T>(this.providerContext.CreateHostingKindException(ProviderErrorStrings.CommandTimeoutExpired, "Timeout"));
						}
					}
					result2 = result.Result;
				}
				finally
				{
					complete.Close();
				}
				return result2;
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public static Guid? GetActivityId()
		{
			Guid currentThreadActivityId = EventSource.CurrentThreadActivityId;
			if (!(currentThreadActivityId != Guid.Empty))
			{
				return null;
			}
			return new Guid?(currentThreadActivityId);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public IEvaluation StartEvaluation<T>(Action<EvaluationResult2<T>> callback)
		{
			MashupCacheSettings2 dataCacheSettings = this.connectionContext.ConnectionPool.DataCacheSettings;
			MashupCacheSettings2 metadataCacheSettings = this.connectionContext.ConnectionPool.MetadataCacheSettings;
			EvaluationSettings evaluationSettings = new EvaluationSettings
			{
				CachePath = this.cachePath,
				MetadataCache = ((metadataCacheSettings != null) ? this.CreateCacheSettings(metadataCacheSettings, -1L) : null),
				DataCache = this.CreateCacheSettings(dataCacheSettings, this.maxCacheSize),
				MaxWorkingSetInMB = this.connectionContext.ConnectionPool.ContainerMaxWorkingSetInMB,
				TempPath = this.tempPath,
				MaxTempSize = this.maxTempSize,
				Session = (this.session ?? Guid.NewGuid().ToString()),
				Credentials = new MemoryCredentialsStorage(this.credentialsStorage.GetCredentials()),
				QueryPermissions = this.queryPermissionsStorage.GetQueryPermissions(),
				FirewallRules = this.firewallStorage.GetFirewallRules(),
				ConnectionGovernanceManager = ((this.dataSourcePool != null) ? DataSourcePoolHandle.GetConnectionGovernanceManager(this.dataSourcePool) : null),
				AllowAutomaticCredentials = this.allowAutomaticCredentials,
				AllowWindowsAuthentication = this.allowWindowsAuthentication,
				AllowNativeQueries = this.allowNativeQueries,
				ActivityId = this.activityId,
				CorrelationId = this.correlationId,
				LegacyRedirects = this.legacyRedirects,
				AllowActions = this.executeAction,
				ThrowOnFoldingFailure = this.throwFoldingFailures,
				ThrowOnVolatileFunctions = this.throwOnVolatileFunctions,
				ThreadIdentity = this.threadIdentity,
				UtcNow = this.utcNow,
				ConfigurationProperties = this.configurationProperties,
				TracingOptions = this.tracingOptions,
				GCBetweenEvaluations = this.connectionContext.ConnectionPool.GCBetweenEvaluations,
				CacheGroup = this.cacheGroup,
				NamedMetadataCache = this.metadataCache
			};
			Func<IEnumerable<IResource>> <>9__2;
			return Impersonation.RunAsProcessUser<IEvaluation>(delegate
			{
				IMutableEvaluationSession evaluationSession;
				try
				{
					evaluationSession = EngineHost.CreateSession(evaluationSettings, MashupEngines.Version1);
				}
				catch (Exception ex)
				{
					this.providerContext.ThrowIfTranslatedException(ex);
					throw;
				}
				IEvaluationConstants evaluationConstants = evaluationSession.EngineHost.GetEvaluationConstants();
				evaluationSession.EngineHost.Add(new SimpleEngineHost<ICredentialRefreshService>(new CredentialRefreshService(this.providerContext, this.connectionContext, evaluationConstants)));
				if (this.connectionContext.IsDataSourceSettingUpdatable)
				{
					evaluationSession.EngineHost.Add(new SimpleEngineHost<ICredentialService>(new DynamicCredentialService(evaluationSession.EngineHost.QueryService<ICredentialService>(), this.connectionContext, evaluationConstants)));
					evaluationSession.EngineHost.Add(new SimpleEngineHost<IQueryPermissionService>(new DynamicQueryPermissionService(evaluationSession.EngineHost.QueryService<IQueryPermissionService>(), this.connectionContext, evaluationConstants)));
					evaluationSession.EngineHost.Add(new SimpleEngineHost<IFirewallRuleService>(new DynamicFirewallRuleService(evaluationSession.EngineHost.QueryService<IFirewallRuleService>(), this.connectionContext, evaluationConstants)));
				}
				if (this.connectionContext.IsRequestTracingEnabled)
				{
					Func<IEnumerable<IResource>> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = () => this.connectionContext.TracedResources);
					}
					Microsoft.Mashup.EngineHost.Services.RequestTracingService requestTracingService = new Microsoft.Mashup.EngineHost.Services.RequestTracingService(func, new Action<RequestTraceData>(this.connectionContext.TraceRequest));
					evaluationSession.EngineHost.Add(new SimpleEngineHost<IRequestTracingService>(requestTracingService));
				}
				if (this.connectionContext.SubscribedChannels.Any<string>())
				{
					DiagnosticsService diagnosticsService = new DiagnosticsService(this.connectionContext);
					evaluationSession.EngineHost.Add(new SimpleEngineHost<IDiagnosticsService>(diagnosticsService));
				}
				if (this.containerEvaluationMonitorService != null)
				{
					evaluationSession.EngineHost.Add(new SimpleEngineHost<IContainerEvaluationMonitorService>(this.containerEvaluationMonitorService));
				}
				Action<MutableEngineHost> testHook = MashupResource.TestHook;
				if (testHook != null)
				{
					testHook(evaluationSession.EngineHost);
				}
				IPartitionedDocument document = this.package.PartitionedDocument(PartitioningScheme.MemberLet1, MashupEngines.Version1);
				IFirewallPlan firewallPlan = (this.fastCombine ? null : this.firewallPlan);
				string text = this.expression;
				string singleResourceName = this.resourceNames.SingleOrDefaultIfZeroOrMany<string>();
				IPartitionKey specificPartitionKey;
				if (singleResourceName != null)
				{
					string text2 = document.Package.SectionNames.SingleOrDefaultIfZeroOrMany<string>();
					if (text2 == null)
					{
						Func<ISectionMember, bool> <>9__6;
						text2 = (from d in document.Package.SectionNames.Select((string name) => document.GetSectionDocument(name)).Where(delegate(ISectionDocument d)
							{
								IEnumerable<ISectionMember> members = d.Section.Members;
								Func<ISectionMember, bool> func2;
								if ((func2 = <>9__6) == null)
								{
									func2 = (<>9__6 = (ISectionMember member) => member.Export && member.Name.Name == singleResourceName);
								}
								return members.Any(func2);
							})
							select d.Section.SectionName.Name).SingleOrDefaultIfZeroOrMany<string>();
					}
					if (text2 == null)
					{
						throw this.providerContext.CreateMashupKindException(ProviderErrorStrings.Resource_UnsupportedCommand(singleResourceName));
					}
					IMemberLetPartitionKey memberLetPartitionKey = new FormulaPathPartitionKey(text2, singleResourceName);
					specificPartitionKey = ((IMemberLetPartitionedDocument)document).GetSpecificPartitionKey(memberLetPartitionKey);
				}
				else
				{
					document = document.AddExpressionPartition(MashupEngines.Version1, text, out specificPartitionKey);
					IEnumerable<PackageEdit> enumerable = document.ReferencePartition(specificPartitionKey, out text);
					document = document.ApplyEdits(enumerable);
					if (firewallPlan != null)
					{
						IEnumerable<IPartitionKey> partitionInputs = document.GetPartitionInputs(specificPartitionKey);
						int num;
						if (!firewallPlan.PartitionPlans.Any<IFirewallPartitionPlan>())
						{
							num = 1;
						}
						else
						{
							num = firewallPlan.PartitionPlans.Max((IFirewallPartitionPlan p) => p.EvaluationOrder) + 1;
						}
						int num2 = num;
						IFirewallPlanCreator firewallPlanCreator = evaluationSession.EngineHost.QueryService<IFirewallPlanCreator>();
						IFirewallPartitionPlan firewallPartitionPlan = firewallPlanCreator.CreatePartitionPlan(specificPartitionKey, num2, false, partitionInputs);
						firewallPlan = firewallPlanCreator.CreatePlan(firewallPlan.PartitionPlans.Concat(new IFirewallPartitionPlan[] { firewallPartitionPlan }));
					}
				}
				if (firewallPlan != null)
				{
					firewallPlan = evaluationSession.EngineHost.QueryService<IFirewallPlanMinimizer>().TrimPlanForPartition(firewallPlan, document, specificPartitionKey);
				}
				IHttpUriRewritingService httpUriRewritingService;
				if (HttpHostMapping.TryCreateService(out httpUriRewritingService))
				{
					evaluationSession.EngineHost.Add(new SimpleEngineHost<IHttpUriRewritingService>(httpUriRewritingService));
				}
				ILibraryService libraryService = evaluationSession.EngineHost.QueryService<ILibraryService>();
				DocumentEvaluationConfig documentEvaluationConfig = new DocumentEvaluationConfig
				{
					debug = this.debug,
					enableFirewall = !this.fastCombine,
					requiredModules = ((LibraryService)libraryService).GetModules(this.requiredModules)
				};
				IDocumentEvaluationConfigService documentEvaluationConfigService = new DocumentEvaluationConfigService(documentEvaluationConfig);
				evaluationSession.EngineHost.Add(new SimpleEngineHost<IDocumentEvaluationConfigService>(documentEvaluationConfigService));
				IVariableService variableService = evaluationSession.EngineHost.QueryService<IVariableService>();
				DocumentEvaluationParameters documentEvaluationParameters = new DocumentEvaluationParameters
				{
					config = documentEvaluationConfig,
					document = document,
					partitionKey = specificPartitionKey,
					expression = text,
					firewallPlan = firewallPlan,
					executeAction = this.executeAction,
					uiCulture = this.uiCulture,
					reportRelationships = false
				};
				documentEvaluationParameters = documentEvaluationParameters.InvokeWithArguments(MashupEngines.Version1, variableService, this.parameters).PartitionValues(this.partitionValues).FilterWithDataTable(MashupEngines.Version1, variableService, MashupResource.DeserializeDataTable(this.filterDataTable));
				if (this.partitionProgressUpdater != null)
				{
					IProgressReader progressReader = evaluationSession.EngineHost.QueryService<IProgressReader>();
					this.partitionProgressUpdater(() => from d in progressReader.ReadAllProgress()
						where d.Length != 0
						select PartitionProgress.FromBytes(d));
				}
				return ((IDocumentEvaluator<T>)this.CreateStandaloneEvaluator(evaluationSession.EngineHost)).BeginGetResult(documentEvaluationParameters, delegate(EvaluationResult2<T> result)
				{
					callback.InvokeThenOnDispose(this.TranslateException<T>(result), delegate
					{
						try
						{
							result.Dispose<T>();
						}
						finally
						{
							evaluationSession.Dispose();
						}
					});
				});
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004CD8 File Offset: 0x00002ED8
		private CacheSettings CreateCacheSettings(MashupCacheSettings2 mashupCacheSettings, long maxSize)
		{
			object obj = this.memoryCache || mashupCacheSettings.InMemory;
			if (maxSize == -1L)
			{
				maxSize = mashupCacheSettings.MaxSize;
			}
			if (maxSize == -1L)
			{
				maxSize = 4294967296L;
			}
			object obj2 = obj;
			PersistentCacheMode persistentCacheMode = ((obj2 != null) ? PersistentCacheMode.Memory : PersistentCacheMode.Disk);
			if (obj2 != null && this.session != null)
			{
				persistentCacheMode |= PersistentCacheMode.Remote;
			}
			return new CacheSettings
			{
				RefreshData = this.ignorePreviouslyCachedData,
				MaxCacheSize = maxSize,
				MaxCacheEntrySize = new long?(maxSize / 10L),
				CacheMode = persistentCacheMode,
				CacheEncryptionCertificateThumbprint = this.cacheEncryptionCertificateThumbprint,
				CacheTTL = new TimeSpan?((this.session != null) ? mashupCacheSettings.TimeToLive : TimeSpan.MaxValue)
			};
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004D84 File Offset: 0x00002F84
		private EvaluationResult2<T> TranslateException<T>(EvaluationResult2<T> result)
		{
			if (result.Exception != null)
			{
				return new EvaluationResult2<T>(this.providerContext.TranslateException(result.Exception));
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004DA8 File Offset: 0x00002FA8
		private IDocumentEvaluator CreateStandaloneEvaluator(IEngineHost engineHost)
		{
			IEngine engine = engineHost.QueryService<IEngine>();
			EvaluatorContext context = this.connectionContext.ConnectionPool.CreateEvaluatorContext();
			return new AnalyzingDocumentEvaluator(engineHost, new DocumentAnalyzer(engineHost, (IEngineHost host) => context.DocumentEvaluator(host, engine), null), (IEngineHost host) => context.DocumentEvaluator(host, engine));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E04 File Offset: 0x00003004
		private static DataTable DeserializeDataTable(byte[] dataTableBytes)
		{
			if (dataTableBytes == null)
			{
				return null;
			}
			IDataReader dataReader = new PageReaderDataReader(new OleDbPageReader(new MemoryStream(dataTableBytes)));
			DataTable dataTable = new DataTable
			{
				Locale = CultureInfo.InvariantCulture
			};
			object[] array = new object[dataReader.FieldCount];
			for (int i = 0; i < dataReader.FieldCount; i++)
			{
				dataTable.Columns.Add(dataReader.GetName(i), typeof(object));
			}
			while (dataReader.Read())
			{
				dataReader.GetValues(array);
				dataTable.Rows.Add(array);
			}
			return dataTable;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004E94 File Offset: 0x00003094
		public static void RetrieveValueDetails(ValueException2 valueException, Action<string, object> updateValue)
		{
			if (valueException == null || valueException.Detail == null)
			{
				return;
			}
			if (valueException.Detail.IsRecord)
			{
				IRecordValue detailRecord = valueException.Detail.AsRecord;
				using (IEnumerator<string> enumerator = detailRecord.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						IValue value;
						string text;
						if (!MashupResource.TryGetValue(() => detailRecord[key], out value, out text))
						{
							updateValue(key, text);
						}
						else
						{
							object objectFromIValue = MashupResource.GetObjectFromIValue(value);
							if (objectFromIValue != null)
							{
								updateValue(key, objectFromIValue);
							}
						}
					}
					return;
				}
			}
			object objectFromIValue2 = MashupResource.GetObjectFromIValue(valueException.Detail);
			if (objectFromIValue2 != null)
			{
				updateValue("Detail", objectFromIValue2);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004F84 File Offset: 0x00003184
		private static object GetObjectFromIValue(IValue value)
		{
			if (value.IsText)
			{
				return value.AsString;
			}
			if (value.IsNumber)
			{
				return value.AsNumber.ToObject();
			}
			if (value.IsLogical)
			{
				return value.AsBoolean;
			}
			if (value.IsTime)
			{
				return value.AsTime.AsClrTimeSpan;
			}
			if (value.IsDate)
			{
				return value.AsDate.AsClrDateTime;
			}
			if (value.IsDateTime)
			{
				return value.AsDateTime.AsClrDateTime;
			}
			if (value.IsDateTimeZone)
			{
				return value.AsDateTimeZone.AsClrDateTimeOffset;
			}
			if (value.IsRecord)
			{
				return MashupResource.ToString(value.AsRecord);
			}
			if (value.IsList)
			{
				return MashupResource.ToString(value.AsList);
			}
			if (value.IsTable)
			{
				return MashupResource.ToString(value.AsTable);
			}
			return null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000506C File Offset: 0x0000326C
		private static bool TryGetValue(Func<IValue> getValue, out IValue value, out string errorMessage)
		{
			value = null;
			errorMessage = null;
			try
			{
				value = getValue();
				return true;
			}
			catch (ValueException2 valueException)
			{
				errorMessage = "error " + MashupEngines.Version1.Text(valueException.ToString()).ToSource();
			}
			return false;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000050C4 File Offset: 0x000032C4
		private static string ToString(IRecordValue detail)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			int length = detail.Keys.Length;
			int i;
			Func<IValue> <>9__0;
			int j;
			for (i = 0; i < length; i = j + 1)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} = ", MashupEngines.Version1.EscapeFieldIdentifier(detail.Keys[i]));
				Func<IValue> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = () => detail[detail.Keys[i]]);
				}
				IValue value;
				string text;
				if (MashupResource.TryGetValue(func, out value, out text))
				{
					stringBuilder.Append(value.ToSource());
				}
				else
				{
					stringBuilder.Append(text);
				}
				j = i;
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000051C8 File Offset: 0x000033C8
		private static string ToString(IListValue detail)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			int i;
			Func<IValue> <>9__0;
			int j;
			for (i = 0; i < detail.Count; i = j + 1)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				Func<IValue> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = () => detail[i]);
				}
				IValue value;
				string text;
				if (MashupResource.TryGetValue(func, out value, out text))
				{
					stringBuilder.Append(value.ToSource());
				}
				else
				{
					stringBuilder.Append(text);
				}
				j = i;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000528C File Offset: 0x0000348C
		private static string ToString(ITableValue detail)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("#table(");
			stringBuilder.Append("{");
			IKeys keys = detail.Type.AsTableType.ItemType.Fields.Keys;
			for (int i = 0; i < keys.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(MashupEngines.Version1.EscapeString(keys[i]));
			}
			stringBuilder.Append("}, {})");
			return stringBuilder.ToString();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000531D File Offset: 0x0000351D
		public static bool TryGetPropertiesFromException(Exception e, out ISerializedException properties)
		{
			return MashupEngines.Version1.TryGetPropertiesFromException(e, out properties);
		}

		// Token: 0x04000042 RID: 66
		private const long DefaultMaxCacheSize = 4294967296L;

		// Token: 0x04000043 RID: 67
		private static readonly TimeSpan cancelTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000044 RID: 68
		internal static Action<MutableEngineHost> TestHook;

		// Token: 0x04000045 RID: 69
		private readonly object lockObject = new object();

		// Token: 0x04000046 RID: 70
		private readonly ProviderContext providerContext;

		// Token: 0x04000047 RID: 71
		private readonly ConnectionContext connectionContext;

		// Token: 0x04000048 RID: 72
		private readonly IEnumerable<string> resourceNames;

		// Token: 0x04000049 RID: 73
		private readonly string expression;

		// Token: 0x0400004A RID: 74
		private readonly object[] parameters;

		// Token: 0x0400004B RID: 75
		private readonly bool executeAction;

		// Token: 0x0400004C RID: 76
		private readonly string[] requiredModules;

		// Token: 0x0400004D RID: 77
		private readonly IPackage package;

		// Token: 0x0400004E RID: 78
		private readonly bool partitionValues;

		// Token: 0x0400004F RID: 79
		private readonly byte[] filterDataTable;

		// Token: 0x04000050 RID: 80
		private readonly bool allowAutomaticCredentials;

		// Token: 0x04000051 RID: 81
		private readonly bool allowWindowsAuthentication;

		// Token: 0x04000052 RID: 82
		private readonly bool allowNativeQueries;

		// Token: 0x04000053 RID: 83
		private readonly bool fastCombine;

		// Token: 0x04000054 RID: 84
		private readonly long maxCacheSize;

		// Token: 0x04000055 RID: 85
		private readonly bool memoryCache;

		// Token: 0x04000056 RID: 86
		private readonly string cachePath;

		// Token: 0x04000057 RID: 87
		private readonly long maxTempSize;

		// Token: 0x04000058 RID: 88
		private readonly string tempPath;

		// Token: 0x04000059 RID: 89
		private readonly string cacheEncryptionCertificateThumbprint;

		// Token: 0x0400005A RID: 90
		private readonly string session;

		// Token: 0x0400005B RID: 91
		private readonly Guid? activityId;

		// Token: 0x0400005C RID: 92
		private readonly string correlationId;

		// Token: 0x0400005D RID: 93
		private readonly bool legacyRedirects;

		// Token: 0x0400005E RID: 94
		private readonly bool throwFoldingFailures;

		// Token: 0x0400005F RID: 95
		private readonly bool throwOnVolatileFunctions;

		// Token: 0x04000060 RID: 96
		private readonly bool ignorePreviouslyCachedData;

		// Token: 0x04000061 RID: 97
		private readonly SafeHandle threadIdentity;

		// Token: 0x04000062 RID: 98
		private readonly string uiCulture;

		// Token: 0x04000063 RID: 99
		private readonly DateTime? utcNow;

		// Token: 0x04000064 RID: 100
		private readonly IDictionary<string, object> configurationProperties;

		// Token: 0x04000065 RID: 101
		private readonly string[] tracingOptions;

		// Token: 0x04000066 RID: 102
		private CompositeEvaluation evaluation;

		// Token: 0x04000067 RID: 103
		private readonly CredentialsStorage credentialsStorage;

		// Token: 0x04000068 RID: 104
		private readonly QueryPermissionsStorage queryPermissionsStorage;

		// Token: 0x04000069 RID: 105
		private readonly FirewallStorage firewallStorage;

		// Token: 0x0400006A RID: 106
		private readonly IFirewallPlan firewallPlan;

		// Token: 0x0400006B RID: 107
		private readonly string dataSourcePool;

		// Token: 0x0400006C RID: 108
		private readonly bool debug;

		// Token: 0x0400006D RID: 109
		private readonly string cacheGroup;

		// Token: 0x0400006E RID: 110
		private readonly string metadataCache;

		// Token: 0x0400006F RID: 111
		private readonly Action<Func<IEnumerable<PartitionProgress>>> partitionProgressUpdater;

		// Token: 0x04000070 RID: 112
		private readonly IContainerEvaluationMonitorService containerEvaluationMonitorService;

		// Token: 0x02000024 RID: 36
		public sealed class ParameterErrors
		{
			// Token: 0x060001B2 RID: 434 RVA: 0x00006A03 File Offset: 0x00004C03
			public ParameterErrors(ProviderContext context, int parameter)
			{
				this.context = context;
				this.parameter = parameter;
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x00006A19 File Offset: 0x00004C19
			public Func<T> TranslateExceptions<T>(Func<T> getValue)
			{
				return delegate
				{
					T t;
					try
					{
						t = getValue();
					}
					catch (Exception ex)
					{
						Exception ex2 = this.TranslateException(ex);
						if (ex2 != ex)
						{
							throw ex2;
						}
						throw;
					}
					return t;
				};
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x00006A3C File Offset: 0x00004C3C
			public Exception TranslateException(Exception exception)
			{
				if (!ProviderTracing.TraceIsSafeException("MashupResource/TranslateException", exception, null, null))
				{
					return exception;
				}
				ValueException2 valueException = this.context.GetValueException(exception);
				if (valueException != null)
				{
					return valueException;
				}
				return new ErrorException(ProviderErrorStrings.ParameterError(this.parameter), null, null, false, true, exception.ToErrorException());
			}

			// Token: 0x040000AF RID: 175
			private readonly int parameter;

			// Token: 0x040000B0 RID: 176
			private readonly ProviderContext context;
		}
	}
}
