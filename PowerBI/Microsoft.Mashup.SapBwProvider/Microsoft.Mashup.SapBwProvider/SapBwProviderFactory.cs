using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000030 RID: 48
	public sealed class SapBwProviderFactory : DbProviderFactory, IDestinationConfiguration, ISapBwProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000278 RID: 632 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		// (remove) Token: 0x06000279 RID: 633 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

		// Token: 0x0600027A RID: 634 RVA: 0x0000AC18 File Offset: 0x00008E18
		private SapBwProviderFactory()
		{
			RfcDestinationManager.RegisterDestinationConfiguration(this);
			RfcTrace.DefaultTraceLevel = 0U;
			this.destinations = new ConcurrentDictionary<string, RfcConfigParameters>();
			this.functions = new ConcurrentDictionary<string, RfcFunctionMetadata>();
			this.structures = new ConcurrentDictionary<string, IRfcStructure>();
			this.fileTracers = new ConcurrentDictionary<string, IFileTracer>();
			this.rfcCustomRepository = Repository.DeserializeCustomRepositoryOrNull("Microsoft.Mashup.SapBwProvider.RfcMetadataRepository.json");
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000AC73 File Offset: 0x00008E73
		public static SapBwProviderFactory Instance
		{
			get
			{
				return SapBwProviderFactory.lazy.Value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000AC7F File Offset: 0x00008E7F
		public ConcurrentDictionary<string, IRfcStructure> Structures
		{
			get
			{
				return this.structures;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000AC87 File Offset: 0x00008E87
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AC8C File Offset: 0x00008E8C
		public RfcConfigParameters GetParameters(string destinationName)
		{
			RfcConfigParameters rfcConfigParameters;
			if (this.destinations.TryGetValue(destinationName, out rfcConfigParameters))
			{
				return rfcConfigParameters;
			}
			throw new SapBwException(Resources.CannotFindDestination);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000ACBA File Offset: 0x00008EBA
		public bool ChangeEventsSupported()
		{
			return true;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000ACBD File Offset: 0x00008EBD
		public override DbConnection CreateConnection()
		{
			return new SapBwConnection(SapBwProviderFactory.Instance);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000ACC9 File Offset: 0x00008EC9
		public override DbCommand CreateCommand()
		{
			return new SapBwCommand();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public override DbParameter CreateParameter()
		{
			return new SapBwParameter();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000ACD7 File Offset: 0x00008ED7
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SapBwConnectionStringBuilder();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		public IFileTracer GetFileTracerOrNull(string driverTracePath)
		{
			IFileTracer fileTracer = null;
			if (!string.IsNullOrEmpty(driverTracePath) && !this.fileTracers.TryGetValue(driverTracePath, out fileTracer) && Directory.Exists(driverTracePath))
			{
				if (this.fileTracers.Count == 0)
				{
					RfcTrace.TraceDirectory = driverTracePath;
				}
				fileTracer = new FileTracer(driverTracePath);
				this.fileTracers.TryAdd(driverTracePath, fileTracer);
			}
			return fileTracer;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AD38 File Offset: 0x00008F38
		public IDestination GetDestination(string destinationName, RfcConfigParameters parameters, Func<string, IDisposable> impersonationWrapper)
		{
			IDestination destination2;
			try
			{
				bool flag = false;
				RfcDestination rfcDestination = RfcDestinationManager.TryGetDestination(destinationName);
				if (rfcDestination == null)
				{
					this.AddOrEditDestination(parameters);
					rfcDestination = RfcDestinationManager.GetDestination(destinationName);
					flag = true;
				}
				IDestination destination = new Destination(rfcDestination);
				if (impersonationWrapper != null)
				{
					destination = new ImpersonatedDestination(destination, impersonationWrapper);
				}
				if (flag)
				{
					destination.Ping();
				}
				destination2 = destination;
			}
			catch (Exception ex)
			{
				this.RemoveDestination(destinationName);
				throw SapBwException.FromException(ex);
			}
			return destination2;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public IRfcFunction CreateFunction(string functionName, IDestination destination)
		{
			IRfcFunction rfcFunction = null;
			if (this.rfcCustomRepository != null)
			{
				try
				{
					rfcFunction = this.rfcCustomRepository.CreateFunction(functionName);
				}
				catch (RfcBaseException)
				{
					rfcFunction = null;
				}
			}
			if (rfcFunction == null && destination != null)
			{
				RfcFunctionMetadata rfcFunctionMetadata;
				if (this.functions.TryGetValue(functionName, out rfcFunctionMetadata))
				{
					rfcFunction = rfcFunctionMetadata.CreateFunction();
				}
				else
				{
					rfcFunction = destination.CreateFunction(functionName);
					this.functions[functionName] = rfcFunction.Metadata;
				}
			}
			if (rfcFunction == null)
			{
				throw new SapBwException(Resources.FailedToFindFunctionMetadata(functionName));
			}
			return rfcFunction;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AE2C File Offset: 0x0000902C
		public void AddOrEditDestination(RfcConfigParameters parameters)
		{
			string text = parameters["NAME"];
			if (this.destinations.ContainsKey(text) && this.ConfigurationChanged != null)
			{
				RfcConfigurationEventArgs rfcConfigurationEventArgs = new RfcConfigurationEventArgs(0, parameters);
				this.ConfigurationChanged.Invoke(text, rfcConfigurationEventArgs);
			}
			this.destinations[text] = parameters;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AE80 File Offset: 0x00009080
		public void RemoveDestination(string destinationName)
		{
			RfcConfigParameters rfcConfigParameters;
			if (this.destinations.TryRemove(destinationName, out rfcConfigParameters) && this.ConfigurationChanged != null)
			{
				this.ConfigurationChanged.Invoke(destinationName, new RfcConfigurationEventArgs(1));
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AEB8 File Offset: 0x000090B8
		protected override void Finalize()
		{
			try
			{
				if (RfcDestinationManager.IsDestinationConfigurationRegistered())
				{
					if (this.destinations != null)
					{
						foreach (string text in new List<string>(this.destinations.Keys))
						{
							this.RemoveDestination(text);
						}
					}
					RfcDestinationManager.UnregisterDestinationConfiguration(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040001AF RID: 431
		private static readonly Lazy<SapBwProviderFactory> lazy = new Lazy<SapBwProviderFactory>(() => new SapBwProviderFactory());

		// Token: 0x040001B0 RID: 432
		private readonly ConcurrentDictionary<string, RfcConfigParameters> destinations;

		// Token: 0x040001B1 RID: 433
		private readonly ConcurrentDictionary<string, RfcFunctionMetadata> functions;

		// Token: 0x040001B2 RID: 434
		private readonly ConcurrentDictionary<string, IRfcStructure> structures;

		// Token: 0x040001B3 RID: 435
		private readonly ConcurrentDictionary<string, IFileTracer> fileTracers;

		// Token: 0x040001B4 RID: 436
		private readonly RfcCustomRepository rfcCustomRepository;
	}
}
