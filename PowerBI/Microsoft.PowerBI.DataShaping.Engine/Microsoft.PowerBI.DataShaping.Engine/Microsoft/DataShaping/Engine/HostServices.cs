using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000018 RID: 24
	public sealed class HostServices
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000031D4 File Offset: 0x000013D4
		public HostServices(Microsoft.DataShaping.ServiceContracts.ITracer tracer, IDumper dumper, Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, IConnectionFactory connectionFactory, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, IDataTransformPluginFactory dataTransformPluginFactory, IConnectionUserImpersonator connectionUserImpersonator, IFeatureSwitchProvider featureSwitchProvider)
		{
			this.Tracer = tracer;
			this.Dumper = dumper;
			this.TelemetryService = telemetryService;
			this.ConnectionPool = connectionPool;
			this.ConnectionFactory = connectionFactory;
			this.ConnectionStringResolver = connectionStringResolver;
			this.DataTransformPluginFactory = dataTransformPluginFactory;
			this.ConnectionUserImpersonator = connectionUserImpersonator;
			this.FeatureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000322C File Offset: 0x0000142C
		public Microsoft.DataShaping.ServiceContracts.ITracer Tracer { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003234 File Offset: 0x00001434
		public IDumper Dumper { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000323C File Offset: 0x0000143C
		public Microsoft.DataShaping.ServiceContracts.ITelemetryService TelemetryService { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003244 File Offset: 0x00001444
		public IConnectionPool ConnectionPool { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000324C File Offset: 0x0000144C
		public IConnectionFactory ConnectionFactory { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003254 File Offset: 0x00001454
		public IConnectionStringResolver ConnectionStringResolver { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000325C File Offset: 0x0000145C
		public IDataTransformPluginFactory DataTransformPluginFactory { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003264 File Offset: 0x00001464
		public IConnectionUserImpersonator ConnectionUserImpersonator { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000326C File Offset: 0x0000146C
		public IFeatureSwitchProvider FeatureSwitchProvider { get; }
	}
}
