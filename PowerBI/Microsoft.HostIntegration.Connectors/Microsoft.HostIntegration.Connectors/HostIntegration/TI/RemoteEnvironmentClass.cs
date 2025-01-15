using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000744 RID: 1860
	[Guid("221B49C7-1D1F-4d0a-A1A5-77ED5EDFED7A")]
	[DataContract]
	[Serializable]
	public class RemoteEnvironmentClass : IRemoteEnvironmentClass
	{
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000C6794 File Offset: 0x000C4994
		// (set) Token: 0x06003A77 RID: 14967 RVA: 0x000C679C File Offset: 0x000C499C
		[DataMember]
		public Guid DevDefinesGuid
		{
			get
			{
				return this.devDefinesGuid;
			}
			set
			{
				this.devDefinesGuid = value;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000C67A5 File Offset: 0x000C49A5
		// (set) Token: 0x06003A79 RID: 14969 RVA: 0x000C67AD File Offset: 0x000C49AD
		[DataMember]
		public string RemoteEnvironmentVersion
		{
			get
			{
				return this.remoteEnvironmentVersion;
			}
			set
			{
				this.remoteEnvironmentVersion = value;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000C67B6 File Offset: 0x000C49B6
		// (set) Token: 0x06003A7B RID: 14971 RVA: 0x000C67BE File Offset: 0x000C49BE
		[DataMember]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000C67C7 File Offset: 0x000C49C7
		// (set) Token: 0x06003A7D RID: 14973 RVA: 0x000C67CF File Offset: 0x000C49CF
		[DataMember]
		public Transport Transport
		{
			get
			{
				return this.transport;
			}
			set
			{
				this.transport = value;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000C67D8 File Offset: 0x000C49D8
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x000C67E0 File Offset: 0x000C49E0
		[DataMember]
		public string AggregateConverterName
		{
			get
			{
				return this.aggregateConverterName;
			}
			set
			{
				this.aggregateConverterName = value;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000C67E9 File Offset: 0x000C49E9
		// (set) Token: 0x06003A81 RID: 14977 RVA: 0x000C67F1 File Offset: 0x000C49F1
		[DataMember]
		public string PrimitiveConverterName
		{
			get
			{
				return this.primitiveConverterName;
			}
			set
			{
				this.primitiveConverterName = value;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000C67FA File Offset: 0x000C49FA
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x000C6802 File Offset: 0x000C4A02
		[DataMember]
		public string PrimitiveConverterClassId
		{
			get
			{
				return this.primitiveConverterClassId;
			}
			set
			{
				this.primitiveConverterClassId = value;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000C680B File Offset: 0x000C4A0B
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x000C6813 File Offset: 0x000C4A13
		[DataMember]
		public string TransportName
		{
			get
			{
				return this.transportName;
			}
			set
			{
				this.transportName = value;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000C681C File Offset: 0x000C4A1C
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000C6824 File Offset: 0x000C4A24
		[DataMember]
		public ProgrammingModel ProgrammingModel
		{
			get
			{
				return this.programmingModel;
			}
			set
			{
				this.programmingModel = value;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000C682D File Offset: 0x000C4A2D
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000C6835 File Offset: 0x000C4A35
		[DataMember]
		public string ProgrammingModelName
		{
			get
			{
				return this.programmingModelName;
			}
			set
			{
				this.programmingModelName = value;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000C683E File Offset: 0x000C4A3E
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000C6846 File Offset: 0x000C4A46
		[DataMember]
		public HostEnvironment HostEnvironment
		{
			get
			{
				return this.hostEnvironment;
			}
			set
			{
				this.hostEnvironment = value;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000C684F File Offset: 0x000C4A4F
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000C6857 File Offset: 0x000C4A57
		[DataMember]
		public HostPlatformTypes HostType
		{
			get
			{
				return this.hostType;
			}
			set
			{
				this.hostType = value;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000C6860 File Offset: 0x000C4A60
		// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000C6868 File Offset: 0x000C4A68
		[DataMember]
		public string HostEnvironmentName
		{
			get
			{
				return this.hostEnvironmentName;
			}
			set
			{
				this.hostEnvironmentName = value;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000C6871 File Offset: 0x000C4A71
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000C6879 File Offset: 0x000C4A79
		[DataMember]
		public string HostPlatformName
		{
			get
			{
				return this.hostPlatformName;
			}
			set
			{
				this.hostPlatformName = value;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000C6882 File Offset: 0x000C4A82
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x000C688A File Offset: 0x000C4A8A
		[DataMember]
		public HostLanguage[] HostLanguages
		{
			get
			{
				return this.hostLanguages;
			}
			set
			{
				this.hostLanguages = value;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000C6893 File Offset: 0x000C4A93
		// (set) Token: 0x06003A95 RID: 14997 RVA: 0x000C689B File Offset: 0x000C4A9B
		[DataMember]
		public RECPropertyPage[] RECPropertyPages
		{
			get
			{
				return this.propertyPages;
			}
			set
			{
				this.propertyPages = value;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000C68A4 File Offset: 0x000C4AA4
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000C68AC File Offset: 0x000C4AAC
		[DataMember]
		public string VendorName
		{
			get
			{
				return this.vendorName;
			}
			set
			{
				this.vendorName = value;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000C68B5 File Offset: 0x000C4AB5
		// (set) Token: 0x06003A99 RID: 15001 RVA: 0x000C68BD File Offset: 0x000C4ABD
		[DataMember]
		public Guid VendorID
		{
			get
			{
				return this.vendorID;
			}
			set
			{
				this.vendorID = value;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003A9A RID: 15002 RVA: 0x000C68C6 File Offset: 0x000C4AC6
		// (set) Token: 0x06003A9B RID: 15003 RVA: 0x000C68CE File Offset: 0x000C4ACE
		[DataMember]
		public string RemoteEnvironmentClassID
		{
			get
			{
				return this.remoteEnvironmentClassID;
			}
			set
			{
				this.remoteEnvironmentClassID = value;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000C68D7 File Offset: 0x000C4AD7
		// (set) Token: 0x06003A9D RID: 15005 RVA: 0x000C68DF File Offset: 0x000C4ADF
		[DataMember]
		public RemoteEnvironmentTypes RemoteEnvironmentType
		{
			get
			{
				return this.remoteEnvironmentType;
			}
			set
			{
				this.remoteEnvironmentType = value;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000C68E8 File Offset: 0x000C4AE8
		// (set) Token: 0x06003A9F RID: 15007 RVA: 0x000C68F0 File Offset: 0x000C4AF0
		[DataMember]
		public string StateMachineName
		{
			get
			{
				return this.stateMachineName;
			}
			set
			{
				this.stateMachineName = value;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x000C68F9 File Offset: 0x000C4AF9
		// (set) Token: 0x06003AA1 RID: 15009 RVA: 0x000C6901 File Offset: 0x000C4B01
		[DataMember]
		public string StateMachineFullName
		{
			get
			{
				return this.stateMachineFullName;
			}
			set
			{
				this.stateMachineFullName = value;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000C690A File Offset: 0x000C4B0A
		// (set) Token: 0x06003AA3 RID: 15011 RVA: 0x000C6912 File Offset: 0x000C4B12
		[DataMember]
		public string AggregateConverterFullName
		{
			get
			{
				return this.aggregateConverterFullName;
			}
			set
			{
				this.aggregateConverterFullName = value;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000C691B File Offset: 0x000C4B1B
		public string AggregateConverterJsonFullName
		{
			get
			{
				return this.aggregateConverterJsonFullName;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x000C6923 File Offset: 0x000C4B23
		// (set) Token: 0x06003AA6 RID: 15014 RVA: 0x000C692B File Offset: 0x000C4B2B
		[DataMember]
		public string PrimitiveConverterFullName
		{
			get
			{
				return this.primitiveConverterFullName;
			}
			set
			{
				this.primitiveConverterFullName = value;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x000C6934 File Offset: 0x000C4B34
		// (set) Token: 0x06003AA8 RID: 15016 RVA: 0x000C693C File Offset: 0x000C4B3C
		[DataMember]
		public string TransportFullName
		{
			get
			{
				return this.transportFullName;
			}
			set
			{
				this.transportFullName = value;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x000C6945 File Offset: 0x000C4B45
		// (set) Token: 0x06003AAA RID: 15018 RVA: 0x000C694D File Offset: 0x000C4B4D
		[DataMember]
		public string TransportProtocolName
		{
			get
			{
				return this.transportProtocolName;
			}
			set
			{
				this.transportProtocolName = value;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000C6956 File Offset: 0x000C4B56
		// (set) Token: 0x06003AAC RID: 15020 RVA: 0x000C695E File Offset: 0x000C4B5E
		[DataMember]
		public bool IsSupportedByManagedRuntime
		{
			get
			{
				return this.isSupportedByManagedRuntime;
			}
			set
			{
				this.isSupportedByManagedRuntime = value;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000C6967 File Offset: 0x000C4B67
		// (set) Token: 0x06003AAE RID: 15022 RVA: 0x000C696F File Offset: 0x000C4B6F
		[DataMember]
		public bool IsForNew
		{
			get
			{
				return this.isForNew;
			}
			set
			{
				this.isForNew = value;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000C6978 File Offset: 0x000C4B78
		// (set) Token: 0x06003AB0 RID: 15024 RVA: 0x000C6980 File Offset: 0x000C4B80
		[DataMember]
		public bool PersistentConnectionsSupported
		{
			get
			{
				return this.persistentConnectionsSupported;
			}
			set
			{
				this.persistentConnectionsSupported = value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000C6989 File Offset: 0x000C4B89
		// (set) Token: 0x06003AB2 RID: 15026 RVA: 0x000C6991 File Offset: 0x000C4B91
		[DataMember]
		public bool TransactionsAreSupported
		{
			get
			{
				return this.transactionsAreSupported;
			}
			set
			{
				this.transactionsAreSupported = value;
			}
		}

		// Token: 0x0400232A RID: 9002
		internal string name;

		// Token: 0x0400232B RID: 9003
		internal string aggregateConverterName;

		// Token: 0x0400232C RID: 9004
		internal string primitiveConverterName;

		// Token: 0x0400232D RID: 9005
		internal string primitiveConverterClassId;

		// Token: 0x0400232E RID: 9006
		internal string transportName;

		// Token: 0x0400232F RID: 9007
		internal string programmingModelName;

		// Token: 0x04002330 RID: 9008
		internal string hostEnvironmentName;

		// Token: 0x04002331 RID: 9009
		internal string hostPlatformName;

		// Token: 0x04002332 RID: 9010
		internal HostPlatformTypes hostType;

		// Token: 0x04002333 RID: 9011
		internal string vendorName;

		// Token: 0x04002334 RID: 9012
		internal Guid vendorID;

		// Token: 0x04002335 RID: 9013
		internal string remoteEnvironmentVersion;

		// Token: 0x04002336 RID: 9014
		internal string aggregateConverterFullName;

		// Token: 0x04002337 RID: 9015
		internal string aggregateConverterJsonFullName;

		// Token: 0x04002338 RID: 9016
		internal string primitiveConverterFullName;

		// Token: 0x04002339 RID: 9017
		internal string transportFullName;

		// Token: 0x0400233A RID: 9018
		internal string stateMachineName;

		// Token: 0x0400233B RID: 9019
		internal string stateMachineFullName;

		// Token: 0x0400233C RID: 9020
		internal string transportProtocolName;

		// Token: 0x0400233D RID: 9021
		internal Transport transport;

		// Token: 0x0400233E RID: 9022
		internal ProgrammingModel programmingModel;

		// Token: 0x0400233F RID: 9023
		internal HostEnvironment hostEnvironment;

		// Token: 0x04002340 RID: 9024
		internal HostLanguage[] hostLanguages;

		// Token: 0x04002341 RID: 9025
		internal RECPropertyPage[] propertyPages;

		// Token: 0x04002342 RID: 9026
		internal Guid devDefinesGuid;

		// Token: 0x04002343 RID: 9027
		internal string remoteEnvironmentClassID;

		// Token: 0x04002344 RID: 9028
		internal RemoteEnvironmentTypes remoteEnvironmentType;

		// Token: 0x04002345 RID: 9029
		internal bool isSupportedByManagedRuntime;

		// Token: 0x04002346 RID: 9030
		internal bool isForNew;

		// Token: 0x04002347 RID: 9031
		internal bool persistentConnectionsSupported;

		// Token: 0x04002348 RID: 9032
		internal bool transactionsAreSupported;
	}
}
