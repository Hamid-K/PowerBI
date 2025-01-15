using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.CacheManager
{
	// Token: 0x02000E5E RID: 3678
	internal sealed class CacheManagerModule : Module
	{
		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x060062E0 RID: 25312 RVA: 0x00153786 File Offset: 0x00151986
		public override string Name
		{
			get
			{
				return "MashupCacheManager";
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x0015378D File Offset: 0x0015198D
		public override Keys ExportKeys
		{
			get
			{
				if (CacheManagerModule.exportKeys == null)
				{
					CacheManagerModule.exportKeys = Keys.New(3, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "CacheManager.Cache";
						case 1:
							return "CacheManager.Caches";
						case 2:
							return "CacheManager.InvokeWithCaches";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return CacheManagerModule.exportKeys;
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x060062E2 RID: 25314 RVA: 0x001537C5 File Offset: 0x001519C5
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { CacheManagerModule.dataSource };
			}
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x001537D8 File Offset: 0x001519D8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new CacheManagerModule.CacheFunctionValue(hostEnvironment);
				case 1:
					return new CacheManagerModule.CachesFunctionValue(hostEnvironment);
				case 2:
					return new CacheManagerModule.InvokeWithCachesFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x0015380C File Offset: 0x00151A0C
		private static void ValidateCacheGroup(IEngineHost engineHost, ICacheManager cacheManager, string cacheGroupIdentifier)
		{
			if (cacheManager == null)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.CacheManagerNotAvailable, Value.Null, null);
			}
			if (cacheGroupIdentifier != cacheManager.CacheGroup)
			{
				throw new ResourceAccessForbiddenException(Resource.New("MashupCacheManager", "MashupCacheManager"), Strings.CacheManagerNotAvailable, null, null);
			}
		}

		// Token: 0x040035C7 RID: 13767
		private const string IdentityKey = "Identity";

		// Token: 0x040035C8 RID: 13768
		private const string DataKey = "Data";

		// Token: 0x040035C9 RID: 13769
		private const string IsReadOnlyKey = "IsReadOnly";

		// Token: 0x040035CA RID: 13770
		private static readonly Keys cacheKeys = Keys.New("Identity", "Data");

		// Token: 0x040035CB RID: 13771
		private static ResourceKindInfo dataSource = new SingletonResourceKindInfo("MashupCacheManager", "MashupCacheManager", "MashupCacheManager", new AuthenticationInfo[]
		{
			new KeyAuthenticationInfo()
		}, null, null, false, false, null);

		// Token: 0x040035CC RID: 13772
		private static Keys exportKeys;

		// Token: 0x02000E5F RID: 3679
		private enum Exports
		{
			// Token: 0x040035CE RID: 13774
			Cache,
			// Token: 0x040035CF RID: 13775
			Caches,
			// Token: 0x040035D0 RID: 13776
			InvokeWithCaches,
			// Token: 0x040035D1 RID: 13777
			Count
		}

		// Token: 0x02000E60 RID: 3680
		private sealed class CacheFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x060062E7 RID: 25319 RVA: 0x001538A9 File Offset: 0x00151AA9
			public CacheFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Table, "cacheGroupIdentifier", TypeValue.Text, "configuration", TypeValue.Any)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060062E8 RID: 25320 RVA: 0x001538D4 File Offset: 0x00151AD4
			public override TableValue TypedInvoke(TextValue cacheGroupIdentifier, Value configuration)
			{
				ICacheManager cacheManager = this.engineHost.QueryService<ICacheManager>();
				CacheManagerModule.ValidateCacheGroup(this.engineHost, cacheManager, cacheGroupIdentifier.String);
				if (!configuration.IsText)
				{
					RecordValue recordValue = CacheManagerModule.CacheFunctionValue.FixupCacheValues(configuration.AsRecord);
					return new CacheManagerModule.CacheTableValue(cacheManager.CreateCache(recordValue));
				}
				CacheManagerCacheInfo cacheManagerCacheInfo = cacheManager.ListCaches().FirstOrDefault((CacheManagerCacheInfo c) => c.Identifier == configuration.AsString);
				if (cacheManagerCacheInfo.Identifier == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Cache_NotFound, configuration, null);
				}
				return new CacheManagerModule.CacheTableValue(cacheManagerCacheInfo);
			}

			// Token: 0x060062E9 RID: 25321 RVA: 0x00153970 File Offset: 0x00151B70
			private static RecordValue FixupCacheValues(RecordValue config)
			{
				return RecordValue.New(config.Keys, delegate(int i)
				{
					ValueKind kind = config[i].Kind;
					if (kind == ValueKind.Record)
					{
						return CacheManagerModule.CacheFunctionValue.FixupCacheValues(config[i].AsRecord);
					}
					if (kind == ValueKind.Table)
					{
						return TextValue.New(CacheManagerModule.CacheTableValue.AsCacheReference(config[i].AsTable).Identifier);
					}
					return config[i];
				});
			}

			// Token: 0x040035D2 RID: 13778
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000E63 RID: 3683
		private sealed class CachesFunctionValue : NativeFunctionValue1<TableValue, TextValue>
		{
			// Token: 0x060062EE RID: 25326 RVA: 0x00153A2D File Offset: 0x00151C2D
			public CachesFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Table, "cacheGroupIdentifier", TypeValue.Text)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060062EF RID: 25327 RVA: 0x00153A4C File Offset: 0x00151C4C
			public override TableValue TypedInvoke(TextValue cacheGroupIdentifier)
			{
				ICacheManager cacheManager = this.engineHost.QueryService<ICacheManager>();
				CacheManagerModule.ValidateCacheGroup(this.engineHost, cacheManager, cacheGroupIdentifier.String);
				return new QueryTableValue(new CacheManagerModule.CachesQuery(this.engineHost, new CacheManagerModule.CachesTableValue(this.engineHost, cacheGroupIdentifier.String)));
			}

			// Token: 0x040035D5 RID: 13781
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000E64 RID: 3684
		private sealed class InvokeWithCachesFunctionValue : NativeFunctionValue2<Value, RecordValue, FunctionValue>
		{
			// Token: 0x060062F0 RID: 25328 RVA: 0x00153A98 File Offset: 0x00151C98
			public InvokeWithCachesFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, "caches", TypeValue.Record, "function", TypeValue.Function)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060062F1 RID: 25329 RVA: 0x00153AC0 File Offset: 0x00151CC0
			public override Value TypedInvoke(RecordValue caches, FunctionValue function)
			{
				if (caches.Keys.Length != 1 || caches.Keys[0] != "Metadata" || !caches[0].IsTable)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.ArgumentOutOfRange(PiiFree.New("caches")), caches, null);
				}
				ICacheManager cacheManager = this.engineHost.QueryService<ICacheManager>();
				if (cacheManager == null)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.CacheManagerNotAvailable, Value.Null, null);
				}
				ICacheSet cache = cacheManager.GetCache(CacheManagerModule.CacheTableValue.AsCacheReference(caches[0].AsTable).Identifier);
				ICacheSet metadata = this.engineHost.QueryService<ICacheSets>().Metadata;
				IScopedReplaceable<ICacheSet> scopedReplaceable = metadata as IScopedReplaceable<ICacheSet>;
				if (metadata == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.CacheNotReplaceable, null, null);
				}
				Value value;
				using (scopedReplaceable.ReplaceWith(cache))
				{
					value = function.Invoke();
				}
				return value;
			}

			// Token: 0x040035D6 RID: 13782
			private const string MetadataCache = "Metadata";

			// Token: 0x040035D7 RID: 13783
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000E65 RID: 3685
		private sealed class CacheTableValue : TableValue
		{
			// Token: 0x060062F2 RID: 25330 RVA: 0x00153BA8 File Offset: 0x00151DA8
			public CacheTableValue(CacheManagerCacheInfo cacheInfo)
			{
				this.cacheInfo = cacheInfo;
			}

			// Token: 0x17001CE0 RID: 7392
			// (get) Token: 0x060062F3 RID: 25331 RVA: 0x00153BB7 File Offset: 0x00151DB7
			public override TypeValue Type
			{
				get
				{
					return TableTypeValue.New(RecordTypeValue.New(CacheManagerModule.CacheTableValue.columns));
				}
			}

			// Token: 0x17001CE1 RID: 7393
			// (get) Token: 0x060062F4 RID: 25332 RVA: 0x00153BC8 File Offset: 0x00151DC8
			public new string Identity
			{
				get
				{
					return this.cacheInfo.Identifier;
				}
			}

			// Token: 0x060062F5 RID: 25333 RVA: 0x00153BD8 File Offset: 0x00151DD8
			public static CacheManagerCacheInfo AsCacheReference(TableValue table)
			{
				CacheManagerModule.CacheTableValue cacheTableValue = table as CacheManagerModule.CacheTableValue;
				if (cacheTableValue != null)
				{
					return cacheTableValue.cacheInfo;
				}
				throw ValueException.NewExpressionError<Message2>(Strings.ValueException_CastTypeMismatch_Complex("table", "cache"), null, null);
			}

			// Token: 0x060062F6 RID: 25334 RVA: 0x00153C0C File Offset: 0x00151E0C
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Value_Enumeration_NotSupported, null, null);
			}

			// Token: 0x040035D8 RID: 13784
			private static readonly Keys columns = Keys.New("Key", "Value");

			// Token: 0x040035D9 RID: 13785
			private readonly CacheManagerCacheInfo cacheInfo;
		}

		// Token: 0x02000E66 RID: 3686
		private sealed class CachesTableValue : TableValue
		{
			// Token: 0x060062F8 RID: 25336 RVA: 0x00153C30 File Offset: 0x00151E30
			public CachesTableValue(IEngineHost engineHost, string cacheGroupIdentifier)
			{
				this.engineHost = engineHost;
				this.cacheGroupIdentifier = cacheGroupIdentifier;
			}

			// Token: 0x17001CE2 RID: 7394
			// (get) Token: 0x060062F9 RID: 25337 RVA: 0x00153C46 File Offset: 0x00151E46
			public override TypeValue Type
			{
				get
				{
					return TableTypeValue.New(RecordTypeValue.New(CacheManagerModule.cacheKeys));
				}
			}

			// Token: 0x060062FA RID: 25338 RVA: 0x00153C57 File Offset: 0x00151E57
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				CacheManagerCacheInfo[] array = this.engineHost.QueryService<ICacheManager>().ListCaches();
				foreach (CacheManagerCacheInfo cacheManagerCacheInfo in array)
				{
					yield return RecordValue.New(CacheManagerModule.cacheKeys, new Value[]
					{
						TextValue.New(cacheManagerCacheInfo.Identifier),
						Value.Null
					});
				}
				CacheManagerCacheInfo[] array2 = null;
				yield break;
			}

			// Token: 0x060062FB RID: 25339 RVA: 0x00153C66 File Offset: 0x00151E66
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(new QueryTableValue(rowsToInsert), countOnlyTable));
			}

			// Token: 0x060062FC RID: 25340 RVA: 0x00153C8C File Offset: 0x00151E8C
			private ActionValue InsertRows(TableValue rowsToInsert, bool countOnlyTable)
			{
				if (countOnlyTable)
				{
					List<IValueReference> list = new List<IValueReference>();
					ICacheManager cacheManager = this.engineHost.QueryService<ICacheManager>();
					foreach (IValueReference valueReference in rowsToInsert)
					{
						RecordValue asRecord = valueReference.Value.AsRecord;
						string asString = asRecord["Identity"].AsString;
						CacheManagerModule.CacheTableValue cacheTableValue = asRecord["Data"].AsTable as CacheManagerModule.CacheTableValue;
						if (cacheTableValue == null)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Value_NotCache, null, null);
						}
						bool? flag = null;
						Value value;
						if (asRecord.TryGetValue("IsReadOnly", out value))
						{
							flag = (value.IsNull ? null : new bool?(value.AsLogical.Boolean));
						}
						list.Add(new CacheManagerModule.UpdateCacheActionValue(cacheManager, cacheTableValue.Identity, asString, flag));
					}
					list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(list.Count)));
					list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
					return ActionValue.New(ListValue.New(list));
				}
				throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
			}

			// Token: 0x040035DA RID: 13786
			private readonly IEngineHost engineHost;

			// Token: 0x040035DB RID: 13787
			private readonly string cacheGroupIdentifier;
		}

		// Token: 0x02000E69 RID: 3689
		private sealed class CachesQuery : FilteredTableQuery
		{
			// Token: 0x06006305 RID: 25349 RVA: 0x00153EC0 File Offset: 0x001520C0
			public CachesQuery(IEngineHost engineHost, CacheManagerModule.CachesTableValue cachesTable)
				: base(cachesTable, engineHost)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x06006306 RID: 25350 RVA: 0x00153ED1 File Offset: 0x001520D1
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
			{
				new List<ActionValue>();
				return base.UpdateRows(columnUpdates, selector);
			}

			// Token: 0x06006307 RID: 25351 RVA: 0x00153EE1 File Offset: 0x001520E1
			public override ActionValue DeleteRows(FunctionValue selector)
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(selector, countOnlyTable));
			}

			// Token: 0x06006308 RID: 25352 RVA: 0x00153F06 File Offset: 0x00152106
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				return base.Table.InsertRows(rowsToInsert);
			}

			// Token: 0x06006309 RID: 25353 RVA: 0x00153F14 File Offset: 0x00152114
			private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
			{
				if (countOnlyTable)
				{
					List<IValueReference> list = new List<IValueReference>();
					ICacheManager cacheManager = this.engineHost.QueryService<ICacheManager>();
					foreach (IValueReference valueReference in this.SelectRows(selector).GetRows())
					{
						string asString = valueReference.Value.AsRecord["Identity"].AsString;
						list.Add(new CacheManagerModule.DeleteCacheActionValue(cacheManager, asString));
					}
					list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(list.Count)));
					list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
					return ActionValue.New(ListValue.New(list));
				}
				throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, base.Table, null);
			}

			// Token: 0x040035E3 RID: 13795
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000E6B RID: 3691
		private sealed class UpdateCacheActionValue : ActionValue
		{
			// Token: 0x0600630C RID: 25356 RVA: 0x00154008 File Offset: 0x00152208
			public UpdateCacheActionValue(ICacheManager manager, string oldIdentity, string newIdentity, bool? readOnly)
			{
				this.manager = manager;
				this.oldIdentity = oldIdentity;
				this.newIdentity = newIdentity;
				this.readOnly = readOnly;
			}

			// Token: 0x0600630D RID: 25357 RVA: 0x0015402D File Offset: 0x0015222D
			public override Value Execute()
			{
				this.manager.UpdateCache(this.oldIdentity, this.newIdentity, this.readOnly);
				return Value.Null;
			}

			// Token: 0x040035E6 RID: 13798
			private readonly ICacheManager manager;

			// Token: 0x040035E7 RID: 13799
			private readonly string oldIdentity;

			// Token: 0x040035E8 RID: 13800
			private readonly string newIdentity;

			// Token: 0x040035E9 RID: 13801
			private readonly bool? readOnly;
		}

		// Token: 0x02000E6C RID: 3692
		private sealed class DeleteCacheActionValue : ActionValue
		{
			// Token: 0x0600630E RID: 25358 RVA: 0x00154051 File Offset: 0x00152251
			public DeleteCacheActionValue(ICacheManager manager, string identity)
			{
				this.manager = manager;
				this.identity = identity;
			}

			// Token: 0x0600630F RID: 25359 RVA: 0x00154067 File Offset: 0x00152267
			public override Value Execute()
			{
				this.manager.DeleteCache(this.identity);
				return Value.Null;
			}

			// Token: 0x040035EA RID: 13802
			private readonly ICacheManager manager;

			// Token: 0x040035EB RID: 13803
			private readonly string identity;
		}
	}
}
