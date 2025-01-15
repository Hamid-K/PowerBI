using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200011C RID: 284
	[OriginalName("TelemetryHostData")]
	public class TelemetryHostData : INotifyPropertyChanged
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x000178AC File Offset: 0x00015AAC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static TelemetryHostData CreateTelemetryHostData(bool isPublicBuild, bool isEnabled, int numberOfProcessors, int numberOfCores, bool isVirtualMachine, int countInstances, int count14xInstances, int count13xInstances, int count12xInstances, int count11xInstances)
		{
			return new TelemetryHostData
			{
				IsPublicBuild = isPublicBuild,
				IsEnabled = isEnabled,
				NumberOfProcessors = numberOfProcessors,
				NumberOfCores = numberOfCores,
				IsVirtualMachine = isVirtualMachine,
				CountInstances = countInstances,
				Count14xInstances = count14xInstances,
				Count13xInstances = count13xInstances,
				Count12xInstances = count12xInstances,
				Count11xInstances = count11xInstances
			};
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0001790A File Offset: 0x00015B0A
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00017912 File Offset: 0x00015B12
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Build")]
		public string Build
		{
			get
			{
				return this._Build;
			}
			set
			{
				this._Build = value;
				this.OnPropertyChanged("Build");
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00017926 File Offset: 0x00015B26
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0001792E File Offset: 0x00015B2E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExternalUser")]
		public string ExternalUser
		{
			get
			{
				return this._ExternalUser;
			}
			set
			{
				this._ExternalUser = value;
				this.OnPropertyChanged("ExternalUser");
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00017942 File Offset: 0x00015B42
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0001794A File Offset: 0x00015B4A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsPublicBuild")]
		public bool IsPublicBuild
		{
			get
			{
				return this._IsPublicBuild;
			}
			set
			{
				this._IsPublicBuild = value;
				this.OnPropertyChanged("IsPublicBuild");
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0001795E File Offset: 0x00015B5E
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00017966 File Offset: 0x00015B66
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Host")]
		public string Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.OnPropertyChanged("Host");
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001797A File Offset: 0x00015B7A
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x00017982 File Offset: 0x00015B82
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HashedUserId")]
		public string HashedUserId
		{
			get
			{
				return this._HashedUserId;
			}
			set
			{
				this._HashedUserId = value;
				this.OnPropertyChanged("HashedUserId");
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00017996 File Offset: 0x00015B96
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0001799E File Offset: 0x00015B9E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("InstallationId")]
		public string InstallationId
		{
			get
			{
				return this._InstallationId;
			}
			set
			{
				this._InstallationId = value;
				this.OnPropertyChanged("InstallationId");
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000179B2 File Offset: 0x00015BB2
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x000179BA File Offset: 0x00015BBA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsEnabled")]
		public bool IsEnabled
		{
			get
			{
				return this._IsEnabled;
			}
			set
			{
				this._IsEnabled = value;
				this.OnPropertyChanged("IsEnabled");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000179CE File Offset: 0x00015BCE
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x000179D6 File Offset: 0x00015BD6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Edition")]
		public string Edition
		{
			get
			{
				return this._Edition;
			}
			set
			{
				this._Edition = value;
				this.OnPropertyChanged("Edition");
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000179EA File Offset: 0x00015BEA
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x000179F2 File Offset: 0x00015BF2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AuthenticationTypes")]
		public string AuthenticationTypes
		{
			get
			{
				return this._AuthenticationTypes;
			}
			set
			{
				this._AuthenticationTypes = value;
				this.OnPropertyChanged("AuthenticationTypes");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00017A06 File Offset: 0x00015C06
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00017A0E File Offset: 0x00015C0E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("NumberOfProcessors")]
		public int NumberOfProcessors
		{
			get
			{
				return this._NumberOfProcessors;
			}
			set
			{
				this._NumberOfProcessors = value;
				this.OnPropertyChanged("NumberOfProcessors");
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00017A22 File Offset: 0x00015C22
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00017A2A File Offset: 0x00015C2A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("NumberOfCores")]
		public int NumberOfCores
		{
			get
			{
				return this._NumberOfCores;
			}
			set
			{
				this._NumberOfCores = value;
				this.OnPropertyChanged("NumberOfCores");
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00017A3E File Offset: 0x00015C3E
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00017A46 File Offset: 0x00015C46
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsVirtualMachine")]
		public bool IsVirtualMachine
		{
			get
			{
				return this._IsVirtualMachine;
			}
			set
			{
				this._IsVirtualMachine = value;
				this.OnPropertyChanged("IsVirtualMachine");
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00017A5A File Offset: 0x00015C5A
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00017A62 File Offset: 0x00015C62
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MachineId")]
		public string MachineId
		{
			get
			{
				return this._MachineId;
			}
			set
			{
				this._MachineId = value;
				this.OnPropertyChanged("MachineId");
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00017A76 File Offset: 0x00015C76
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00017A7E File Offset: 0x00015C7E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CountInstances")]
		public int CountInstances
		{
			get
			{
				return this._CountInstances;
			}
			set
			{
				this._CountInstances = value;
				this.OnPropertyChanged("CountInstances");
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00017A92 File Offset: 0x00015C92
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00017A9A File Offset: 0x00015C9A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count14xInstances")]
		public int Count14xInstances
		{
			get
			{
				return this._Count14xInstances;
			}
			set
			{
				this._Count14xInstances = value;
				this.OnPropertyChanged("Count14xInstances");
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00017AAE File Offset: 0x00015CAE
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00017AB6 File Offset: 0x00015CB6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count13xInstances")]
		public int Count13xInstances
		{
			get
			{
				return this._Count13xInstances;
			}
			set
			{
				this._Count13xInstances = value;
				this.OnPropertyChanged("Count13xInstances");
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00017ACA File Offset: 0x00015CCA
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00017AD2 File Offset: 0x00015CD2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count12xInstances")]
		public int Count12xInstances
		{
			get
			{
				return this._Count12xInstances;
			}
			set
			{
				this._Count12xInstances = value;
				this.OnPropertyChanged("Count12xInstances");
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00017AE6 File Offset: 0x00015CE6
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00017AEE File Offset: 0x00015CEE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count11xInstances")]
		public int Count11xInstances
		{
			get
			{
				return this._Count11xInstances;
			}
			set
			{
				this._Count11xInstances = value;
				this.OnPropertyChanged("Count11xInstances");
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00017B02 File Offset: 0x00015D02
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00017B0A File Offset: 0x00015D0A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ProductSku")]
		public string ProductSku
		{
			get
			{
				return this._ProductSku;
			}
			set
			{
				this._ProductSku = value;
				this.OnPropertyChanged("ProductSku");
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06000C5A RID: 3162 RVA: 0x00017B20 File Offset: 0x00015D20
		// (remove) Token: 0x06000C5B RID: 3163 RVA: 0x00017B58 File Offset: 0x00015D58
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000C5C RID: 3164 RVA: 0x00017B8D File Offset: 0x00015D8D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000582 RID: 1410
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Build;

		// Token: 0x04000583 RID: 1411
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ExternalUser;

		// Token: 0x04000584 RID: 1412
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsPublicBuild;

		// Token: 0x04000585 RID: 1413
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Host;

		// Token: 0x04000586 RID: 1414
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _HashedUserId;

		// Token: 0x04000587 RID: 1415
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _InstallationId;

		// Token: 0x04000588 RID: 1416
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsEnabled;

		// Token: 0x04000589 RID: 1417
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Edition;

		// Token: 0x0400058A RID: 1418
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AuthenticationTypes;

		// Token: 0x0400058B RID: 1419
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _NumberOfProcessors;

		// Token: 0x0400058C RID: 1420
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _NumberOfCores;

		// Token: 0x0400058D RID: 1421
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsVirtualMachine;

		// Token: 0x0400058E RID: 1422
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _MachineId;

		// Token: 0x0400058F RID: 1423
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _CountInstances;

		// Token: 0x04000590 RID: 1424
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count14xInstances;

		// Token: 0x04000591 RID: 1425
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count13xInstances;

		// Token: 0x04000592 RID: 1426
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count12xInstances;

		// Token: 0x04000593 RID: 1427
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count11xInstances;

		// Token: 0x04000594 RID: 1428
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ProductSku;
	}
}
